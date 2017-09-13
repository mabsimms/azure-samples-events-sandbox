package com.microsoft.azure.samples.ephreader;

import java.util.function.Consumer;
import com.microsoft.azure.eventprocessorhost.ExceptionReceivedEventArgs;
import com.microsoft.azure.samples.App;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class ErrorNotificationHandler implements Consumer<ExceptionReceivedEventArgs>
{
    static Logger logger = LogManager.getLogger();

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