package com.microsoft.azure.samples.mapping;

import com.microsoft.azure.eventhubs.EventData;

/**
 * Created by masimms on 9/13/17.
 */
public interface IEventMapper {
    String getMapperName();
    boolean canParseEvent(EventData data, String payload);
    void parseEvent(EventData data, String payload);
    boolean isFinal();
}
