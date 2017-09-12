package com.microsoft.azure.samples.ephreader;

import java.util.function.Consumer;
import com.microsoft.azure.eventprocessorhost.ExceptionReceivedEventArgs;
import com.microsoft.azure.samples.App;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ErrorNotificationHandler implements Consumer<ExceptionReceivedEventArgs>
{
    static Logger logger = LoggerFactory.getLogger(App.class);

    private final String eventHubName;

    public ErrorNotificationHandler(String eventHubName)
    {
        this.eventHubName = eventHubName;
    }

    @Override
    public void accept(ExceptionReceivedEventArgs t)
    {
        logger.error( "EventProcessor host error on event hub {} partition {} action {} error {}",
            this.eventHubName, t.getPartitionId(), t.getAction(), t.getException());
    }
}