package com.microsoft.azure.samples.ephreader;

import com.codahale.metrics.MetricRegistry;
import com.microsoft.azure.eventhubs.EventData;
import com.microsoft.azure.eventprocessorhost.IEventProcessor;
import com.microsoft.azure.eventprocessorhost.IEventProcessorFactory;
import com.microsoft.azure.eventprocessorhost.PartitionContext;

import javax.xml.ws.Dispatch;
import java.util.function.Consumer;

public class EventHubProcessorFactory implements IEventProcessorFactory {

    private final MetricRegistry metricRegistry;
    private final Consumer<EventData> single;
    private final Consumer<EventData[]> batch;
    private final DispatchMode mode;
    private final int dop;

    public EventHubProcessorFactory(MetricRegistry metricRegistry, DispatchMode mode,
        Consumer<EventData> single, Consumer<EventData[]> batch, int dop)
    {
        this.metricRegistry = metricRegistry;
        this.single = single;
        this.batch = batch;
        this.mode = mode;
        this.dop = dop;
    }

    @Override
    public IEventProcessor createEventProcessor(PartitionContext partitionContext) throws Exception
    {
        EventProcessor processor = new EventProcessor(metricRegistry, mode, dop, single, batch);
        return processor;
    }
}
