from azure.common.client_factory import get_client_from_cli_profile
from azure.mgmt.compute import ComputeManagementClient
from azure.mgmt.storage import StorageManagementClient
from azure.mgmt.eventhub import EventHubManagementClient
from azure.mgmt.resource import ResourceManagementClient
from azure.mgmt.monitor import MonitorManagementClient

LOCATION='eastus2'

def run_example():
    event_hub_client = get_client_from_cli_profile(EventHubManagementClient)
    monitor_client = get_client_from_cli_profile(MonitorManagementClient)
   
    # Get the logging profile and use that to determine the event hub configuration
    log_profile = monitor_client.log_profiles.get("default")
    event_hub_rule_id = log_profile.service_bus_rule_id

    # Extract the resource group, namespace and event hub name
    print("Azure Monitor configured with event hub " + event_hub_rule_id)
    tokens = event_hub_rule_id.split('/')
    resource_group_name = tokens[4]
    event_hub_namespace = tokens[8]
    event_hub_name = "insights-operational-logs"

    key = event_hub_client.event_hubs.list_keys(resource_group_name, event_hub_namespace, event_hub_name, "listen")

    print("Subscription ID =         " + tokens[2])
    print("Resource Group =          " + resource_group_name)
    print("Event Hub Namespace =     " + event_hub_namespace)
    print("Event Hub Name =          " + event_hub_name)
    print("Authorization rule name = " + key.key_name)
    print("Authorization token =     " + key.primary_key)
    print()
    print("Use this as the connection string for reading events:")
    print()
    print(key.primary_connection_string)    
    print()
    print("Use this values in the event hub properties file for eph-reader:")
    print()    
    print("EventHubNamespace = \"{}\"".format(event_hub_namespace))
    print("EventHubName = \"{}\"".format(event_hub_name))
    print("SasKeyName = \"{}\"".format(key.key_name))
    print("SasKeyValue = \"{}\"".format(key.primary_key))

    print()
    print("Use these as the environment variables for eph-reader (instead of properties file")
    print()
    print("export EventHubNamespace=\"{}\"".format(event_hub_namespace))
    print("export EventHubName=\"{}\"".format(event_hub_name))
    print("export SasKeyName=\"{}\"".format(key.key_name))
    print("export SasKeyValue=\"{}\"".format(key.primary_key))
    print()
    print("Note: you will also have to supply a storage account for handling checkpoint data")
    
if __name__ == "__main__":
    run_example()