<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>WindowsAzure.ServiceBus</NuGetReference>
  <Namespace>Microsoft.ServiceBus.Messaging</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var conn = "Endpoint=sb://azuremonevtseh.servicebus.windows.net/;SharedAccessKeyName=listen;SharedAccessKey=CxCbQaeUwbTStksKMyYiQPpgm9QGrEG+ybfv+D4fgZY=;EntityPath=insights-operational-logs";
	var client = EventHubClient.CreateFromConnectionString(conn);
	
	
	PullData(client).GetAwaiter().GetResult();
}

public async Task PullData(EventHubClient client)
{
	Console.WriteLine("Extracting partition information:");
	var partitionInfo = await client.GetRuntimeInformationAsync();
	var consumer = client.GetDefaultConsumerGroup();
	var taskList = new List<Task>();
	var tokenSource = new CancellationTokenSource();
	var collection = new BlockingCollection<EventData>();
	
	foreach (var p in partitionInfo.PartitionIds)
	{
		Console.WriteLine(" -> Creating receiver for partition {0}", p);
		var reader = await consumer.CreateReceiverAsync(p);
		
		var t = Task.Factory.StartNew( () => ReceiveLoop(reader, tokenSource.Token, collection), 
			TaskCreationOptions.LongRunning);
		taskList.Add(t);
	}

	foreach (var evt in collection.GetConsumingEnumerable(tokenSource.Token))
	{
		ProcessEvent(evt);
	}
}

public void ProcessEvent(EventData evt)
{
	var rootDirectory = @"C:\temp\azmon\";
	
	Console.WriteLine("Received event from partition {0} at offset {1} timestamp {2}", 
		evt.PartitionKey, evt.Offset, evt.EnqueuedTimeUtc);
	var messageId = Guid.NewGuid();
	var fileName = Path.Combine(rootDirectory,  messageId.ToString() + ".json");
	var headerFileName = Path.Combine(rootDirectory, messageId.ToString() + ".header.json");
	
	File.WriteAllBytes(fileName, evt.GetBytes());
	Console.WriteLine("  -> Content written as {0}", fileName);
	
	var headerData = new
	{
		Properties = evt.Properties,
		SystemProperties = evt.SystemProperties,
		EnqueuedTime = evt.EnqueuedTimeUtc,
		PartitionKey = evt.PartitionKey,
		Offset = evt.Offset,
		SequenceNumber = evt.SequenceNumber
	};
	var headerText = JsonConvert.SerializeObject(headerData);
	File.WriteAllText(headerFileName, headerText);
	Console.WriteLine("  -> Header written as {0}", headerFileName);
	Console.WriteLine();
}

public async Task ReceiveLoop(EventHubReceiver receiver, CancellationToken token, 
	BlockingCollection<EventData> collection)
{
	while (!token.IsCancellationRequested)
	{
		var evt = await receiver.ReceiveAsync(1, TimeSpan.FromSeconds(15));
		if (evt != null)
		{
			foreach (var e in evt)
			{				
				collection.Add(e);
			}
		}
	}	
}

// Define other methods and classes here
