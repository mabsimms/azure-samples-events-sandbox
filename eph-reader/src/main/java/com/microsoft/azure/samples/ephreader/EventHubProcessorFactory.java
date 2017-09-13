package com.microsoft.azure.samples.ephreader;

import com.codahale.metrics.MetricRegistry;
import com.microsoft.azure.eventhubs.EventData;
import com.microsoft.azure.eventprocessorhost.IEventProcessor;
import com.microsoft.azure.eventprocessorhost.IEventProcessorFactory;
import com.microsoft.azure.eventprocessorhost.PartitionContext;

import java.util.function.Consumer;

public class EventHubProcessorFactory implements IEventProcessorFactory {

    private final MetricRegistry metricRegistry;
    private final EventConcurrentDispatcher dispatcher;

    public EventHubProcessorFactory(MetricRegistry metricRegistry, EventConcurrentDispatcher dispatcher)
    {
        this.metricRegistry = metricRegistry;
        this.dispatcher = dispatcher;
    }

    @Override
    public IEventProcessor createEventProcessor(PartitionContext partitionContext) throws Exception
    {
        EventProcessor processor = new EventProcessor(metricRegistry, dispatcher);
        return processor;
    }
}
