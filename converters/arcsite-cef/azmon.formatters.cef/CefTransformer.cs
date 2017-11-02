using azmon.events;
using System;
using System.Collections.Generic;
using System.Text;

namespace azmon.formatters.cef
{
    public class CefTransformer
    {
        public CefEvent Convert(AzureEventBase evt)
        {
            var cef = new CefEvent()
            {
                Timestamp = evt.time,
                Host = evt.resourceName,

                // Device Vendor - need to check difference between Microsoft and 3rd party events
                DeviceVendor = "Microsoft",
                DeviceProduct = evt.providerName,
                DeviceVersion = "1",

                DeviceEventClassID = evt.eventType,

                Name = evt.shortDescription,

                Severity = MapSeverity(evt.level)                                  
            };

            // TODO - set custom properties
            cef.CustomProperties.act = evt.operationName;
            cef.CustomProperties.destinationServiceName = evt.providerName;
            //cef.CustomProperties.destinationDnsDomain = 
            cef.CustomProperties.deviceExternalId = evt.resourceId;
            cef.CustomProperties.duser = "TODO-pull-from-identity";

            // TODO - put the real time on here
            //cef.CustomProperties.end = "TODO";
            cef.CustomProperties.src = evt.callerIpAddress;

            //cef.CustomProperties.act

            return cef;
        }

        /*
         * Severity is a string or integer and reflects the importance of the event. 
         * The valid string values are Unknown, Low, Medium, High, and Very-High.
         * The valid integer values are 0-3=Low, 4-6=Medium, 7- 8=High, and 9-10=Very-High. 
         */
        private static readonly Dictionary<string, string> LevelMap = 
            new Dictionary<string, string>()
        {
            { "Critical", "9" },
            { "Error", "7" },
            { "Warning", "6" },
            { "Informational", "4" },
            { "Information", "4" },              
            { "Verbose", "3" }
        };

        private string MapSeverity(string level)
        {
            if (LevelMap.ContainsKey(level))
                return LevelMap[level];
            return "Unknown";
        }
    }
}
