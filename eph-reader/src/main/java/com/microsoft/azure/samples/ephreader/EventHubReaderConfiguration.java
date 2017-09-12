package com.microsoft.azure.samples.ephreader;

import java.time.Duration;
import java.util.function.Function;

public class EventHubReaderConfiguration
{
    public String ConsumerGroupName;
    public String EventHubNamespace;
    public String EventHubName;
    public String SasKeyName;
    public String SasKeyValue;

    public String StorageAccountName;
    public String StorageAccountKey;
    public String StorageContainerName;
    public String HostName;
    public Boolean InvokeProcessorAfterReceiveTimeout;
    public int MaxBatchSize;
    public int PrefetchCount;
    public Duration ReceiveTimeout;
    public Function<String,Object> InitialOffsetProvider;
}
