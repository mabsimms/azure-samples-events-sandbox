using System;
using System.Collections.Generic;
using System.Text;

namespace azmon.events
{
    public class AzureEventBase
    {
        public AzureEventBase()
        {
        }
     
        // Raw fields
        public DateTime time { get; set; }
        public string resourceId { get; set; }
        public string operationName { get; set; }
        public string category { get; set; }
        public string resultType { get; set; }
        public string resultSignature { get; set; }
        public int durationMs { get; set; }
        public string callerIpAddress { get; set; }
        public string correlationId { get; set; }
        public Identity identity { get; set; }
        public string level { get; set; }
        public string location { get; set; }

        public Properties properties { get; set; }
        public string RawJson { get; set;  }
      
        // Parsed fields
        public string subscriptionId { get; private set; }
        public string providerName { get; private set; } 
        public string resourceName { get; private set;  }
        public string resourceGroup { get; private set; }
        public string eventType { get; private set; }

        public string shortDescription { get; private set; }
        public string longDescription { get; private set; } 

        public void UpdateFields()
        {
            if (String.IsNullOrEmpty(resourceId))
                return;
            // Example resource string
            // /SUBSCRIPTIONS/3E9C25FC-55B3-4837-9BBA-02B6EB204331/RESOURCEGROUPS/MASDEMO-K8-RG/PROVIDERS/MICROSOFT.CONTAINERSERVICE/CONTAINERSERVICES/DEMOK8
            var tokens = resourceId.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 8)
                return;

            this.subscriptionId = tokens[1];
            this.resourceGroup = tokens[3];
            this.providerName = tokens[5];
            this.resourceName = tokens[7];

            // TODO - generate a unique signature name for the event type
            this.eventType = "Azure Event";

            // TODO - generate the descriptions
            if (this.properties != null && 
                !String.IsNullOrEmpty(this.properties.statusMessage))
            {
                this.shortDescription = this.properties.statusMessage.TrimStart('"').TrimEnd('"');
            }
            else
            {
                this.shortDescription = "TODO";
            }
            
            this.longDescription = "TODO";
        }
    }

    public class Authorization
    {
        public string scope { get; set; }
        public string action { get; set; }
    }

    public class Claims
    {
    }

    public class Identity
    {
        public Authorization authorization { get; set; }
        public Claims claims { get; set; }
    }

    public class Properties
    {
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }
}
