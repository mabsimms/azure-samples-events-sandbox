package com.microsoft.azure.samples.ephreader;

import com.microsoft.azure.eventhubs.EventData;
import com.microsoft.azure.samples.App;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.concurrent.*;
import java.util.function.Consumer;

public class EventConcurrentDispatcher
{
    static Logger logger = LoggerFactory.getLogger(App.class);

    final int maximumDegreeOfConcurrency;
    final ExecutorService executor;
    final Consumer<EventData> function;

    public EventConcurrentDispatcher(int maxDop, Consumer<EventData> function)
    {
        this.maximumDegreeOfConcurrency = maxDop;
        this.function = function;

        this.executor = new ThreadPoolExecutor(
            maximumDegreeOfConcurrency, maximumDegreeOfConcurrency, 0L, TimeUnit.MILLISECONDS,
            new LinkedBlockingQueue<Runnable>());
    }

    public Future submit(EventData data)
    {
        Future fut = executor.submit(() -> function.accept(data));
        return fut;
    }

    public void shutdown()
    {
        this.executor.shutdown();
    }
}
