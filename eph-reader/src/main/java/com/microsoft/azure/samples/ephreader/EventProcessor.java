package com.microsoft.azure.samples.ephreader;

import com.codahale.metrics.MetricRegistry;
import com.codahale.metrics.Timer;
import com.google.common.base.Stopwatch;
import com.microsoft.azure.eventhubs.EventData;
import com.microsoft.azure.eventprocessorhost.CloseReason;
import com.microsoft.azure.eventprocessorhost.IEventProcessor;
import com.microsoft.azure.eventprocessorhost.PartitionContext;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.slf4j.MDC;

import java.awt.*;
import java.util.function.Consumer;

import static com.codahale.metrics.MetricRegistry.name;
import static java.util.concurrent.TimeUnit.MILLISECONDS;

public class EventProcessor implements IEventProcessor
{
    static Logger logger = LoggerFactory.getLogger(EventProcessor.class);

    private Consumer<EventData> processorFunction;
    private MetricRegistry metricRegistry;

    private final Timer messageProcessMetric;
    private final Timer batchProcessMetric;


    public EventProcessor(MetricRegistry metricRegistry, Consumer<EventData> processorFunc)
    {
        this.metricRegistry = metricRegistry;
        this.processorFunction = processorFunc;

        messageProcessMetric = metricRegistry.timer(name(EventProcessor.class, "process-message"));
        batchProcessMetric = metricRegistry.timer(name(EventProcessor.class, "process-batch"));
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
        logger.info("EventProcessor on event hub {} opening partition {} for consumer {} for reason {}",
                context.getEventHubPath(), context.getPartitionId(), context.getConsumerGroupName(), reason);
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
                logger.debug("EventProcessor on event hub {} partition {} consumer {} processing ZERO events",
                        context.getEventHubPath(), context.getPartitionId(), context.getConsumerGroupName());
                return;
            }

            EventData lastEvent = null;
            for (EventData data : messages) {
                processEvent(data);
                lastEvent = data;
            }

            if (lastEvent != null) {
                logger.debug("EventProcessor on event hub {} partition {} consumer {} checkpointing at {}:{}",
                        context.getEventHubPath(), context.getPartitionId(), context.getConsumerGroupName(),
                        lastEvent.getSystemProperties().getOffset(), lastEvent.getSystemProperties().getSequenceNumber());
                context.checkpoint(lastEvent);
            } else {
                logger.debug("EventProcessor on event hub {} partition {} consumer {} processing ZERO events; no checkpoint",
                        context.getEventHubPath(), context.getPartitionId(), context.getConsumerGroupName());
            }
        }
        finally
        {
            batchContext.stop();
        }
    }

    protected void processEvent(EventData evt)
    {
        final Timer.Context context = messageProcessMetric.time();
        boolean success = true;

        try
        {
            MDC.put("Request-Id", "");
            this.processorFunction.accept(evt);
            MDC.remove("Request-Id");
        }
        catch (Exception e)
        {
            success = false;
        //    logger.warn();
        }
        finally
        {
            context.stop();
        }

        // If the message could not be successfully processed ensure that it is published to a storage for later
        // review.
        if (!success)
        {

        }
    }
}