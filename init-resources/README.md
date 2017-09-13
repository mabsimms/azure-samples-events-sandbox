# Initialize Event Resources

This python script configures the active Azure subscription (using the az login credentials and 
selected subscription) with the resources and configuration required to stream Azure activity logs
to an event hub.  Running this script will:

1. Create (or update) a resource group in the subscription named `azure-events-eastus2-rg`
2. Create an event hub namespace in that resource named `azuremonevtseh`, with 
an event hub called `diaglogs` and its attendant send/listen security policies.
3. Update the Azure Monitor log profile `default` configuration to write Azure
activity logs to that event hub namespace (Azure Monitor will create an event hub
called `insights-operational-logs` behind the scenes into which activity events will be written).

## Usage

The resource creation script requires [Azure CLI 2.0](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest) (which will automatically install python) be installed.  To execute the script:

```
pip install -r requirements.txt
python createmonenv.py
```

This script is (by design) very verbose about what it is creating and the associated data records for the 
Azure objects.  Once the script has finished running the targeted Azure subscription will be configured
to send its operational logs (from Azure resources) to an implicitly created Event Hub (currently called
`insights-operational-logs`).  To retrieve the necessary event hub names, etc, along with the authorization 
token (with listen only permissions) that makes up the connection string, run the other script:

```
python listmonenv.py
```

Which will look up the log profile for the current subscription, grab the 
event hub resource identifer and use that to pull the (currently hard coded 
name) authorization rule and connection string with `listen` rights to that specific event hub (`insights-operational-logs`).  This will produce output
similar to:

```
Azure Monitor configured with event hub /subscriptions/3e9c21fc-55b3-4237-9bba-02b6eb204331/resourceGroups/azure-events-eastus2-rg/providers/Microsoft.EventHub/namespaces/azuremonevtseh/AuthorizationRules/RootManageSharedAccessKey
Subscription ID =         3e9c21fc-55b3-4237-9bba-02b6eb204331
Resource Group =          azure-events-eastus2-rg
Event Hub Namespace =     azuremonevtseh
Event Hub Name =          insights-operational-logs
Authorization rule name = listen
Authorization token =     CxCbQfeUwbTStkszMyYiQPpgm9QGrEG+ybfv+D4fgZY=

Use this as the connection string for reading events:

Endpoint=sb://azuremonevtseh.servicebus.windows.net/;SharedAccessKeyName=listen;SharedAccessKey=CxCbQfeUwbTStkszMyYiQPpgm9QGrEG+ybfv+D4fgZY=;EntityPath=insights-operational-logs

Use this values in the event hub properties file for eph-reader:

EventHubNamespace = "azuremonevtseh"
EventHubName = "insights-operational-logs"
SasKeyName = "listen"
SasKeyValue = "CxCbQfeUwbTStkszMyYiQPpgm9QGrEG+ybfv+D4fgZY="

Use these as the environment variables for eph-reader (instead of properties file

export EventHubNamespace="azuremonevtseh"
export EventHubName="insights-operational-logs"
export SasKeyName="listen"
export SasKeyValue="CxCbQfeUwbTStkszMyYiQPpgm9QGrEG+ybfv+D4fgZY="

Note: you will also have to supply a storage account for handling checkpoint data
```

## Open Issues

- The creation script needs to take an Azure location as a parameter (command line)
- Storage account names (which are inherently globally scoped) are not being randomly generated

