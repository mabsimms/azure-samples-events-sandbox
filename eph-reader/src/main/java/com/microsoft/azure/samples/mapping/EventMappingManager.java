package com.microsoft.azure.samples.mapping;

import com.google.common.collect.ImmutableSet;
import com.microsoft.azure.eventhubs.EventData;

import java.nio.charset.Charset;


/**
 * Created by masimms on 9/13/17.
 */
public class EventMappingManager {

    private static final Charset UTF8_CHARSET = Charset.forName("UTF-8");

    final ImmutableSet<IEventMapper> mappers;

    public EventMappingManager()
    {
        mappers = ImmutableSet.of(
            new DefaultMapper()
        );
    }

    public void processEvent(EventData data)
    {
        String payload = new String(data.getBytes(), UTF8_CHARSET );

        for (IEventMapper map : mappers )
        {
            if (map.canParseEvent(data, payload))
                map.parseEvent(data, payload);
            if (map.isFinal())
                return;
        }
    }
}
