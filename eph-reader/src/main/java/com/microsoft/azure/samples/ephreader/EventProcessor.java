package com.microsoft.azure.samples.ephreader;

import com.codahale.metrics.MetricRegistry;
import com.codahale.metrics.Timer;
import com.google.common.collect.Iterables;
import com.microsoft.azure.eventhubs.EventData;
import com.microsoft.azure.eventprocessorhost.CloseReason;
import com.microsoft.azure.eventprocessorhost.IEventProcessor;
import com.microsoft.azure.eventprocessorhost.PartitionContext;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.*;
import java.util.function.Consumer;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import static com.codahale.metrics.MetricRegistry.name;

public class EventProcessor implements IEventProcessor
{
    static Logger logger = LogManager.getLogger();

    private MetricRegistry metricRegistry;

    private final Timer messageProcessMetric;
    private final Timer batchProcessMetric;
    private final ExecutorService executor;
    private final DispatchMode mode;
    private final int dop;

    private final Consumer<EventData> single;
    private final Consumer<EventData[]> batch;

    public EventProcessor(MetricRegistry metricRegistry, DispatchMode mode, int dop,
        Consumer<EventData> singleFunction, Consumer<EventData[]> batchFunction)
    {
        this.metricRegistry = metricRegistry;
        this.mode = mode;
        this.batch = batchFunction;
        this.single = singleFunction;
        this.dop = dop;

        messageProcessMetric = metricRegistry.timer(name(EventProcessor.class, "process-message"));
        batchProcessMetric = metricRegistry.timer(name(EventProcessor.class, "process-batch"));

        this.executor = new ThreadPoolExecutor(dop, dop , 0L, TimeUnit.MILLISECONDS,
            new LinkedBlockingQueue<Runnable>());
    }

    @Override
    public void onOpen(PartitionContext context) throws Exception
    {
         logger.info("EventProcessor on event hub {} opening partition {} for consumer {}",
            context.getEventHubPath(), context.getPartitionId(), context.getConsumerGroupName());
    }

    @Override
    public void onClose(PartitionContext context, CloseReason reason) throws Exception
    {
        logger.info("EventProcessor on event hub {} closing partition {} for consumer {} for reason {}",
                context.getEventHubPath(), context.getPartitionId(), context.getConsumerGroupName(), reason);
        this.executor.shutdown();
    }

    @Override
    public void onError(PartitionContext context, Throwable error)
    {
        logger.error("EventProcessor on event hub {} opening partition {} for consumer {} with error {}",
                context.getEventHubPath(), context.getPartitionId(), context.getConsumerGroupName(), error);
    }

    @Override
    public void onEvents(PartitionContext context, Iterable<EventData> messages) throws Exception
    {
        final Timer.Context batchContext = batchProcessMetric.time();
        try {
            if (messages == null) {
                logger.info("EventProcessor on event hub {} partition {} consumer {} processing ZERO events",
                        context.getEventHubPath(), context.getPartitionId(), context.getConsumerGroupName());
                return;
            }

            EventData[] messageArray = Iterables.toArray(messages, EventData.class);

            if (mode == DispatchMode.Batch)
            {
                executor.execute( () -> ProcessBatch(messageArray));
            }
            else
            {
                List<Callable<Void>> callableList = new ArrayList<Callable<Void>>();
                for (EventData evt : messageArray) {
                    callableList.add( () -> { processEvent(evt); return null; });
                }
                executor.invokeAll(callableList);
            }

            if (messageArray.length > 0) {
                EventData lastEvent = messageArray[messageArray.length-1];
                logger.info("EventProcessor on event hub {} partition {} consumer {} checkpointing at {}:{}",
                        context.getEventHubPath(), context.getPartitionId(), context.getConsumerGroupName(),
                        lastEvent.getSystemProperties().getOffset(), lastEvent.getSystemProperties().getSequenceNumber());
                context.checkpoint(lastEvent);
            } else {
                logger.info("EventProcessor on event hub {} partition {} consumer {} processing ZERO events; no checkpoint",
                        context.getEventHubPath(), context.getPartitionId(), context.getConsumerGroupName());
            }
        }
        finally
        {
            batchContext.stop();
        }
    }

    private void ProcessBatch(EventData[] messages)
    {
        final Timer.Context context = messageProcessMetric.time();
        boolean success = true;

        try
        {
            this.batch.accept(messages);
        }
        catch (Exception e)
        {
            success = false;
        }
        finally
        {
            context.stop();
        }

        // If the message could not be successfully processed ensure that it is published to a storage for later
        // review.
        if (!success) {
            // TODO
        }
    }

    protected void processEvent(EventData evt)
    {
        final Timer.Context context = messageProcessMetric.time();
        boolean success = true;

        try
        {
            //MDC.put("Request-Id", "");
            this.single.accept(evt);
            //MDC.remove("Request-Id");
        }
        catch (Exception e)
        {
            success = false;
        }
        finally
        {
            context.stop();
        }

        // If the message could not be successfully processed ensure that it is published to a storage for later
        // review.
        if (!success)
        {
            // TODO
        }
    }
}