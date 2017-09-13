package com.microsoft.azure.samples;

import com.codahale.metrics.MetricRegistry;
import com.microsoft.azure.eventhubs.EventData;
import com.microsoft.azure.eventprocessorhost.EventProcessorHost;
import com.microsoft.azure.eventprocessorhost.EventProcessorOptions;
import com.microsoft.azure.samples.ephreader.*;
import com.microsoft.azure.servicebus.ConnectionStringBuilder;
import com.microsoft.azure.servicebus.StringUtil;

import java.io.IOException;
import java.io.InputStream;
import java.net.InetAddress;
import java.time.Duration;
import java.time.Instant;
import java.util.Map;
import java.util.Properties;
import java.util.function.Consumer;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

/**
 * Hello world!
 *
 */
public class App 
{
    static Logger logger = LogManager.getLogger(App.class);
    static MetricRegistry metricsRegistry = new MetricRegistry();

    public static void main( String[] args )
    {
        logger.info("Pulling configuration information");

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Instantiate configuration
        EventHubReaderConfiguration config;
        try {
            config = GetReaderConfiguration();
        } catch (IOException ie) {
            logger.error("Could not instantiate event hub configuration", ie);
            return;
        }

        final String storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=" +
                config.StorageAccountName + ";AccountKey=" + config.StorageAccountKey;

        ConnectionStringBuilder eventHubConnectionString = new ConnectionStringBuilder(
                config.EventHubNamespace, config.EventHubName, config.SasKeyName, config.SasKeyValue);

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Configure metrics reporting - todo add the custom reporter back in
//        final Slf4jReporter reporter = Slf4jReporter.forRegistry(metricsRegistry)
 //               .outputTo(LoggerFactory.getLogger("com.microsoft.azure.samples.metrics"))
  //              .convertRatesTo(TimeUnit.SECONDS)
   //             .convertDurationsTo(TimeUnit.MILLISECONDS)
    //            .build();
     //   reporter.start(15, TimeUnit.SECONDS);
        // TODO - output to graphite as well if configured

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Create the processor host for this instance
        logger.info("Creating event processor with host {} event hub namespace {} hub name {} consumer group {} against storage account {} in container {}",
                config.HostName, config.EventHubNamespace, config.EventHubName, config.ConsumerGroupName, config.StorageAccountName, config.StorageContainerName);
        EventProcessorHost host = new EventProcessorHost(
            config.HostName,
            config.EventHubName,
            config.ConsumerGroupName,
            eventHubConnectionString.toString(),
            storageConnectionString,
            config.StorageContainerName);

        logger.info("Configuring event processor max batch size {} prefetch size {} receive timeout {} invoke processor after timeout {} initial offset {}",
                config.MaxBatchSize, config.PrefetchCount, config.ReceiveTimeout, config.InvokeProcessorAfterReceiveTimeout,
                config.InitialOffsetProvider.apply(""));

        EventProcessorOptions processorOptions = new EventProcessorOptions();
        processorOptions.setExceptionNotification(new ErrorNotificationHandler(config.EventHubName));
        processorOptions.setInvokeProcessorAfterReceiveTimeout(config.InvokeProcessorAfterReceiveTimeout);
        processorOptions.setMaxBatchSize(config.MaxBatchSize);
        processorOptions.setPrefetchCount(config.PrefetchCount);
        processorOptions.setReceiveTimeOut(config.ReceiveTimeout);
        processorOptions.setInitialOffsetProvider(config.InitialOffsetProvider);

        MessageDispatcher runner = new MessageDispatcher();
        Consumer<EventData> single = (evt) -> runner.ProcessMessage(evt);
        Consumer<EventData[]> batch = (evts) -> runner.ProcessMessages(evts);

        try
        {
            EventHubProcessorFactory factory = new EventHubProcessorFactory(metricsRegistry,
                config.DispatchMode, single, batch, config.DispatchConcurrency);
            host.registerEventProcessorFactory(factory);
        }
        catch (Exception e)
        {
            logger.error("TODO");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // Wait for shutdown
        System.out.println("Press enter to close the event hub reader");
        try
        {
            System.in.read();

            logger.info("Unregistering event processor");
            host.unregisterEventProcessor();
        }
        catch(Exception e)
        {
            logger.error("Error during shutdown", e);
        }
    }

    private static EventHubReaderConfiguration GetReaderConfiguration() throws java.io.IOException
    {
        Properties props = LoadProperties("eventhub.properties");
        if (props == null)
            props = new Properties();
        Map<String, String> env = System.getenv();

        EventHubReaderConfiguration config = new EventHubReaderConfiguration();

        try {
            String hostname = InetAddress.getLocalHost().getHostName();
            config.HostName = hostname;
        } catch (IOException e) {
            config.HostName = "localhost";
        }

        // TODO - find a less manual way to do this
        if (props.containsKey("EventHubNamespace"))
            config.EventHubNamespace = props.getProperty("EventHubNamespace");
        if (env.containsKey("EventHubNamespace"))
            config.EventHubNamespace = env.get("EventHubNamespace");
        if (StringUtil.isNullOrEmpty(config.EventHubNamespace))
            throw new IllegalArgumentException("No value provided for EventHubNamespace in eventhub.properties");

        if (props.containsKey("EventHubName"))
            config.EventHubName = props.getProperty("EventHubName");
        if (env.containsKey("EventHubName"))
            config.EventHubName = env.get("EventHubName");
        if (StringUtil.isNullOrEmpty(config.EventHubName))
            throw new IllegalArgumentException("No value provided for EventHubName in eventhub.properties");

        if (props.containsKey("SasKeyName"))
            config.SasKeyName = props.getProperty("SasKeyName");
        if (env.containsKey("SasKeyName"))
            config.SasKeyName = env.get("SasKeyName");
        if (StringUtil.isNullOrEmpty(config.SasKeyName))
            throw new IllegalArgumentException("No value provided for SasKeyName in eventhub.properties or in environment variable SasKeyName");

        if (props.containsKey("SasKeyValue"))
            config.SasKeyValue = props.getProperty("SasKeyValue");
        if (env.containsKey("SasKeyValue"))
            config.SasKeyValue = env.get("SasKeyValue");
        if (StringUtil.isNullOrEmpty(config.SasKeyValue))
            throw new IllegalArgumentException("No value provided for SasKeyValue in eventhub.properties in environment variable SasKeyValue");

        if (props.containsKey("ConsumerGroupName"))
            config.ConsumerGroupName = props.getProperty("ConsumerGroupName");
        else
            config.ConsumerGroupName = "$Default";

        if (props.containsKey("MaxBatchSize"))
            config.MaxBatchSize = Integer.parseInt(props.getProperty("MaxBatchSize"));
        else
            config.MaxBatchSize = 100;

        if (props.containsKey("PrefetchCount"))
            config.PrefetchCount = Integer.parseInt(props.getProperty("PrefetchCount"));
        else
            config.PrefetchCount = 300;

        if (props.containsKey("DispatchConcurrency"))
            config.DispatchConcurrency = Integer.parseInt(props.getProperty("DispatchConcurrency"));
        else
            config.DispatchConcurrency = 1;  // default to "single threaded" mode

        if (props.containsKey("ReceiveTimeout"))
            config.ReceiveTimeout = Duration.parse(props.getProperty("ReceiveTimeout"));
        else
            config.ReceiveTimeout = Duration.parse("PT30S");

        if (props.containsKey("InvokeProcessorAfterReceiveTimeout "))
            config.InvokeProcessorAfterReceiveTimeout = Boolean.parseBoolean(props.getProperty("InvokeProcessorAfterReceiveTimeout "));
        else
            config.InvokeProcessorAfterReceiveTimeout = true;

        if (props.containsKey("StorageAccountName"))
            config.StorageAccountName = props.getProperty("StorageAccountName");
        if (env.containsKey("StorageAccountName"))
            config.StorageAccountName = env.get("StorageAccountName");
        if (StringUtil.isNullOrEmpty(config.StorageAccountName))
            throw new IllegalArgumentException("No value provided for StorageAccountName in eventhub.properties in environment variable StorageAccountName");

        if (props.containsKey("StorageAccountKey"))
            config.StorageAccountKey = props.getProperty("StorageAccountKey");
        if (env.containsKey("StorageAccountKey"))
            config.StorageAccountKey = env.get("StorageAccountKey");
        if (StringUtil.isNullOrEmpty(config.StorageAccountKey))
            throw new IllegalArgumentException("No value provided for StorageAccountKey in eventhub.properties or in environment variable StorageAccountKey ");

        if (props.containsKey("StorageContainerName"))
            config.StorageContainerName = props.getProperty("StorageContainerName");
        else
            config.StorageContainerName = "checkpoint";

        String offsetType = null;
        if (props.containsKey("OffsetType"))
            offsetType = props.getProperty("OffsetType");
        if (env.containsKey("OffsetType"))
            offsetType = env.get("OffsetType");
        if (StringUtil.isNullOrEmpty(offsetType))
            offsetType = "CurrentTime";

        // Starting offset as string or starting UTC time for receiving messages. This is only used when Offset is not
        // provided and receiver is being created for the very first time. This corresponds to either
        // CreateReceiverAsync(String, ReceiverOptions) or CreateReceiverAsync(String, DateTime, ReceiverOptions)
        // depending on the type of return value from delegate.
        switch (offsetType)
        {
            case "CurrentTime":
                config.InitialOffsetProvider = (s) -> Instant.now();
                break;

            case "StartOfStream":
                config.InitialOffsetProvider = (s) -> "0";
                break;

            default:
                logger.warn("No valid option provided for OffsetType, defaulting to CurrentTime");
                config.InitialOffsetProvider = (s) -> Instant.now();
        }

        return config;
    }

    static Properties LoadProperties(String resourceName) throws IOException
    {
        logger.info("Loading resource {}", resourceName);
        ClassLoader loader = Thread.currentThread().getContextClassLoader();
        Properties props = new Properties();
        try(InputStream resourceStream = loader.getResourceAsStream(resourceName)) {
            props.load(resourceStream);
        }
        return props;
    }
}
