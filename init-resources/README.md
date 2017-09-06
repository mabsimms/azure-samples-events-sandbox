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

This script requires [Azure CLI 2.0](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest) (which will automatically install python) be installed.  To execute the script:

```
pip install -r requirements.txt
python createmonenv.py
```