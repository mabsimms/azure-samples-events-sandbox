package com.microsoft.azure.samples.ephreader;

import com.microsoft.azure.eventhubs.EventData;
import com.microsoft.azure.samples.App;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.nio.charset.Charset;
import java.util.Map;

public class MessageDispatcher {

    static Logger logger = LogManager.getLogger(App.class);
    private final Charset UTF8_CHARSET = Charset.forName("UTF-8");

    public void ProcessMessages(EventData[] data)
    {
        logger.debug("Processing {} messages as batch");
        for (EventData evt: data)
        {
           ProcessMessage(evt);
        }
    }

    public void ProcessMessage(EventData data)
    {
        byte[] payload = data.getBytes();
        String payloadStr;

        payloadStr = new String(payload, UTF8_CHARSET);
        logger.debug("Received message size {} content {} partition key {} offset {} enqueued at {}",
            payload.length, payloadStr,
            data.getSystemProperties().getPartitionKey(),
            data.getSystemProperties().getOffset(),
            data.getSystemProperties().getEnqueuedTime()
        );

        for (Map.Entry<String, Object> kv : data.getProperties().entrySet())
        {
            logger.debug("Property {} = {}", kv.getKey(), kv.getValue());
        }
    }
}
