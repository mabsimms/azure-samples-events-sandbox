package com.microsoft.azure.samples.ephreader;

import com.microsoft.azure.eventhubs.EventData;
import com.microsoft.azure.samples.App;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.UnsupportedEncodingException;
import java.nio.charset.Charset;
import java.util.Map;

public class MessageDispatcher {

    static Logger logger = LoggerFactory.getLogger(App.class);
    private final Charset UTF8_CHARSET = Charset.forName("UTF-8");

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
