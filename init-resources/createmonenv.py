from azure.common.client_factory import get_client_from_cli_profile
from azure.mgmt.compute import ComputeManagementClient
from azure.mgmt.storage import StorageManagementClient
from azure.mgmt.eventhub import EventHubManagementClient
from azure.mgmt.resource import ResourceManagementClient
from azure.mgmt.monitor import MonitorManagementClient

LOCATION='eastus2'
GROUP_NAME = 'azure-events-rg'

def run_example():
    computeClient = get_client_from_cli_profile(ComputeManagementClient)

    group_name = "azure-events-" + LOCATION + "-rg"
    eventhub_namespace = "azuremonevtseh"

    # TODO - this needs to be more randomly generated
    storage_name = "azuremonstoremas"

    # Step 1 - create the enclosing resource group if it does not exist
    create_resource_group(group_name, LOCATION)

    # Step 2 - create the storage account for archive of events
    #create_storage_account(storage_name, group_name, LOCATION)

    # Step 3 - create the event hub namespace and associated event hubs and policies
    create_eventhub_resources(eventhub_namespace, group_name, LOCATION)

    # Step 4 - configure azure monitor to write azure events to that event hub
    # The event hub resource ID is the ARM identifier of the root management key for that
    # event hub
    configure_azure_monitor(group_name, LOCATION, eventhub_namespace, ["global"])


def create_resource_group(name, location):
    print('Creating or updating resource group ' + name + ' in region ' + location)
    resource_group_params = {'location': location}

    resourceClient = get_client_from_cli_profile(ResourceManagementClient)
    print_item(resourceClient.resource_groups.create_or_update(name, resource_group_params))

def create_storage_account(name, resourceGroupName, location):
    print('Creating storage account ' + name + ' in region ' + location)
    storageClient = get_client_from_cli_profile(StorageManagementClient)

    parameters = {}
    from azure.mgmt.storage.v2017_06_01.models import SkuName
    from azure.mgmt.storage.v2017_06_01.models import StorageAccountCreateParameters
    from azure.mgmt.storage.v2017_06_01.models import Sku
    from azure.mgmt.storage.v2017_06_01.models import Kind

    async_operation = storageClient.storage_accounts.create(resourceGroupName, name,
        StorageAccountCreateParameters(
            sku=Sku(SkuName.standard_ragrs),
            kind=Kind.storage,
            location=location
        )
    )
    result = async_operation.result()
    print(result)

def create_eventhub_resources(namespace, resourceGroupName, location):
    print('Creating event nub namespace ' + namespace + ' in group ' + resourceGroupName + ' in location ' + location)
    eventHubClient = get_client_from_cli_profile(EventHubManagementClient)

    async_create_ns = eventHubClient.namespaces.create_or_update(
        resourceGroupName,
        namespace,
        {
            'location': location
        }
    )
    ns = async_create_ns.result()
    print(ns)

    # Create an event hub named diaglogs for VM diagnostics logs (activity logs will go to an implicitly craeated namespace)
    eventHubName = 'diaglogs'
    eventHub = eventHubClient.event_hubs.create_or_update(
        resourceGroupName,
        namespace,
        eventHubName,
        {}
    )
    print(eventHub)

    # Create listen and send policies
    listen_rule = eventHubClient.event_hubs.create_or_update_authorization_rule(
        resourceGroupName,
        namespace,
        eventHubName,
        'listen',
        rights=[ "Listen" ]
    )
    print(listen_rule)

    send_rule = eventHubClient.event_hubs.create_or_update_authorization_rule(
        resourceGroupName,
        namespace,
        eventHubName,
        'send',
        rights=["Send"]
    )
    print(send_rule)

def configure_azure_monitor(resource_group_name, location, event_hub_namespace, location_list):
    print("Configuring Azure Monitor to publish activity events")
    monitorClient = get_client_from_cli_profile(MonitorManagementClient)
    eventHubClient = get_client_from_cli_profile(EventHubManagementClient)

    root_manage_key = eventHubClient.namespaces.get_authorization_rule(
        resource_group_name, event_hub_namespace, "RootManageSharedAccessKey")
    event_hub_rule_id = root_manage_key.id
    print("Using event hub policy key " + event_hub_rule_id)

    # If there are any extant log profiles not named "azure-events" delete them
    log_profiles = monitorClient.log_profiles.list()
    for lp in log_profiles:
        print("Found existing log profile with name " + lp.name + " and id " + lp.id)
        if lp.name != "default":
            print("Deleting log profile " + lp.name)
            monitorClient.log_profiles.delete(lp.name)

    # Create an Azure Log Profile
    from azure.mgmt.monitor.models import LogProfileResource
    from azure.mgmt.monitor.models import RetentionPolicy
    log_profile = monitorClient.log_profiles.create_or_update(
        "default",
        LogProfileResource(
            location = location,
            service_bus_rule_id = event_hub_rule_id,
            locations = location_list,
            categories = ["Write", "Delete", "Action"],
            retention_policy = RetentionPolicy(
                enabled = True,
                days = 1
            )
        )
    )
    print(log_profile)
    
    print("Creating listen policy for implicitly created event hug")
    event_hub_name_implicit = "insights-operational-logs"
    auth_rule = eventHubClient.event_hubs.create_or_update_authorization_rule(resource_group_name, event_hub_namespace, event_hub_name_implicit, "listen", "Listen")
    key = list_keys(resource_group_name, event_hub_namespace, event_hub_name_implicit, "listen")
    print("Use authorization rule with Listen rights named " + key.key_name + " and connection string " + key.primary_connection_string)
    

def print_item(group):
    """Print a ResourceGroup instance."""
    print("\tName: {}".format(group.name))
    print("\tId: {}".format(group.id))
    print("\tLocation: {}".format(group.location))
    print("\tTags: {}".format(group.tags))
    print_properties(group.properties)


def print_properties(props):
    """Print a ResourceGroup propertyies instance."""
    if props and props.provisioning_state:
        print("\tProperties:")
        print("\t\tProvisioning State: {}".format(props.provisioning_state))
    print("\n\n")


if __name__ == "__main__":
    run_example()