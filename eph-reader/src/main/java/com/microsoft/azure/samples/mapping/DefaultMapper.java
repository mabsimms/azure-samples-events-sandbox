package com.microsoft.azure.samples.mapping;

import com.microsoft.azure.eventhubs.EventData;

import java.nio.charset.Charset;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

/**
 * Created by masimms on 9/13/17.
 */
public class DefaultMapper implements IEventMapper {
    private final String eventHubName = "";
    private final Charset UTF8_CHARSET = Charset.forName("UTF-8");

    @Override
    public String getMapperName() {
        return "Default";
    }

    @Override
    public boolean canParseEvent(EventData data, String payload) {
        return true;
    }

    @Override
    public void parseEvent(EventData data, String payload) {
        // Write out the event to a file
        String fileName = eventHubName + "_" +
            data.getSystemProperties().getPartitionKey() +
                data.getSystemProperties().getSequenceNumber() +
                ".json";
        try {
            Files.write(Paths.get(fileName), data.getBytes());
        } catch (IOException e)
        {
            // TODO
        }

        // TODO - write out the properties
    }

    @Override
    public boolean isFinal() {
        return true;
    }
}
