using System;

namespace azmon.formatters.cef
{
    public class CefEvent
    {
        public CefEvent()
        {
            this.CustomProperties = new CefCustomProperties();
        }

        public DateTime Timestamp { get; set;  }

        /// <summary>
        /// Identifier for the host (source of the event)
        /// </summary>
        /// <value>The host.</value>
		public string Host { get; set;  }

		/// <summary>
		/// Version is an integer and identifies the version of the CEF format. Event 
		/// consumers use this information to determine what the following fields 
		/// represent. The current CEF version is 0 (CEF:0).
		/// </summary>
        /// <value>CEF:0</value>
		public string CefVersion { get; set; }

		/// <summary>
		/// Device Vendor, Device Product and Device Version are strings that 
        /// uniquely identify the type of sending device. No two products may 
        /// use the same device-vendor and device-product pair. There is
		/// no central authority managing these pairs.  Event producers must 
        /// ensure that they assign unique name pairs.
        /// </summary>
        /// <value>The device vendor.</value>
        public string DeviceVendor { get; set;  }

		/// <summary>
		/// Device Vendor, Device Product and Device Version are strings that 
		/// uniquely identify the type of sending device. No two products may 
		/// use the same device-vendor and device-product pair. There is
		/// no central authority managing these pairs.  Event producers must 
		/// ensure that they assign unique name pairs.
		/// </summary>
		/// <value>The device product.</value>
		public string DeviceProduct { get; set;  }

		/// <summary>
		/// Device Vendor, Device Product and Device Version are strings that 
		/// uniquely identify the type of sending device. No two products may 
		/// use the same device-vendor and device-product pair. There is
		/// no central authority managing these pairs.  Event producers must 
		/// ensure that they assign unique name pairs.
		/// </summary>
		/// <value>The device version.</value>
		public string DeviceVersion { get; set;  }

		/// <summary>
		/// Device Event Class ID is a unique identifier per event-type. This 
        /// can be a string or an integer. Device Event Class ID identifies the 
        /// type of event reported. In the intrusion detection system (IDS) world, 
        /// each signature or rule that detects certain activity has a unique 
        /// Device Event Class ID assigned. This is a requirement for other 
        /// types of devices as well, and helps correlation engines process 
        /// the events. Also known as Signature ID.
		/// </summary>
		/// <value>The device event class identifier.</value>
		public string DeviceEventClassID { get; set;  }

		/// <summary>
		/// Name is a string representing a human-readable and understandable 
        /// description of the event. The event name should not contain 
        /// information that is specifically mentioned in other fields. 
        /// For example: "Port scan from 10.0.0.1 targeting 20.1.1.1" is not a 
        /// good event name. It should be: "Port scan". The other information 
        /// is redundant and can be picked up from the other fields.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set;  }

		/// <summary>
		/// Severity is a string or integer and reflects the importance of the 
        /// event. The valid string values are Unknown, Low, Medium, High, and 
        /// Very-High. The valid integer values are 0-3=Low, 4-6=Medium, 
        /// 7- 8=High, and 9-10=Very-High.
		/// </summary>
		/// <value>The severity.</value>
		public string Severity { get; set; }
          
        public CefCustomProperties CustomProperties { get; private set; }
    }

    public class CefCustomProperties
    {
        [CefField(KeyName = "act", FieldName = "deviceAction", DataType = CefDataType.String, Length = 63)]
        public string act { get; set; }

        [CefField(KeyName = "app", FieldName = "applicationProtocol", DataType = CefDataType.String, Length = 31)]
        public string app { get; set; }

        [CefField(KeyName = "c6a1", FieldName = "deviceCustomIPv6Address1", DataType = CefDataType.IPv6Address)]
        public string c6a1 { get; set; }

        [CefField(KeyName = "c6a1Label", FieldName = "deviceCustomIPv6Address1Label", DataType = CefDataType.String, Length = 1023)]
        public string c6a1Label { get; set; }

        [CefField(KeyName = "c6a3", FieldName = "deviceCustomIPv6Address3", DataType = CefDataType.IPv6Address)]
        public string c6a3 { get; set; }

        [CefField(KeyName = "c6a4", FieldName = "deviceCustomIPv6Address4", DataType = CefDataType.IPv6Address)]
        public string c6a4 { get; set; }

        [CefField(KeyName = "C6a4Label", FieldName = "deviceCustomIPv6Address4Label", DataType = CefDataType.String, Length = 1023)]
        public string C6a4Label { get; set; }

        [CefField(KeyName = "cat", FieldName = "deviceEventCategory", DataType = CefDataType.String, Length = 1023)]
        public string cat { get; set; }

        [CefField(KeyName = "cfp1", FieldName = "deviceCustomFloatingPoint1", DataType = CefDataType.FloatingPoint)]
        public string cfp1 { get; set; }

        [CefField(KeyName = "cfp1Label", FieldName = "deviceCustomFloatingPoint1Label", DataType = CefDataType.String, Length = 1023)]
        public string cfp1Label { get; set; }

        [CefField(KeyName = "cfp2", FieldName = "deviceCustomFloatingPoint2", DataType = CefDataType.FloatingPoint)]
        public string cfp2 { get; set; }

        [CefField(KeyName = "cfp2Label", FieldName = "deviceCustomFloatingPoint2Label", DataType = CefDataType.String, Length = 1023)]
        public string cfp2Label { get; set; }

        [CefField(KeyName = "cfp3", FieldName = "deviceCustomFloatingPoint3", DataType = CefDataType.FloatingPoint)]
        public string cfp3 { get; set; }

        [CefField(KeyName = "cfp3Label", FieldName = "deviceCustomFloatingPoint3Label", DataType = CefDataType.String, Length = 1023)]
        public string cfp3Label { get; set; }

        [CefField(KeyName = "cfp4", FieldName = "deviceCustomFloatingPoint4", DataType = CefDataType.FloatingPoint)]
        public string cfp4 { get; set; }

        [CefField(KeyName = "cfp4Label", FieldName = "deviceCustomFloatingPoint4Label", DataType = CefDataType.String, Length = 1023)]
        public string cfp4Label { get; set; }

        [CefField(KeyName = "cn1", FieldName = "deviceCustomNumber1", DataType = CefDataType.Long)]
        public string cn1 { get; set; }

        [CefField(KeyName = "cn1Label", FieldName = "deviceCustomNumber1Label", DataType = CefDataType.String, Length = 1023)]
        public string cn1Label { get; set; }

        [CefField(KeyName = "cn2", FieldName = "DeviceCustomNumber2", DataType = CefDataType.Long)]
        public string cn2 { get; set; }

        [CefField(KeyName = "cn2Label", FieldName = "deviceCustomNumber2Label", DataType = CefDataType.String, Length = 1023)]
        public string cn2Label { get; set; }

        [CefField(KeyName = "cn3", FieldName = "deviceCustomNumber3", DataType = CefDataType.Long)]
        public string cn3 { get; set; }

        [CefField(KeyName = "cn3Label", FieldName = "deviceCustomNumber3Label", DataType = CefDataType.String, Length = 1023)]
        public string cn3Label { get; set; }

        [CefField(KeyName = "cnt", FieldName = "baseEventCount", DataType = CefDataType.Integer)]
        public int? cnt { get; set; }

        [CefField(KeyName = "cs1", FieldName = "deviceCustomString1", DataType = CefDataType.String, Length = 4000)]
        public string cs1 { get; set; }

        [CefField(KeyName = "cs1Label", FieldName = "deviceCustomString1Label", DataType = CefDataType.String, Length = 1023)]
        public string cs1Label { get; set; }

        [CefField(KeyName = "cs2", FieldName = "deviceCustomString2", DataType = CefDataType.String, Length = 4000)]
        public string cs2 { get; set; }

        [CefField(KeyName = "cs2Label", FieldName = "deviceCustomString2Label", DataType = CefDataType.String, Length = 1023)]
        public string cs2Label { get; set; }

        [CefField(KeyName = "cs3Label", FieldName = "deviceCustomString3Label", DataType = CefDataType.String, Length = 1023)]
        public string cs3Label { get; set; }

        [CefField(KeyName = "cs4", FieldName = "deviceCustomString4", DataType = CefDataType.String, Length = 4000)]
        public string cs4 { get; set; }

        [CefField(KeyName = "cs4Label", FieldName = "deviceCustomString4Label", DataType = CefDataType.String, Length = 1023)]
        public string cs4Label { get; set; }

        [CefField(KeyName = "cs5", FieldName = "deviceCustomString5", DataType = CefDataType.String, Length = 4000)]
        public string cs5 { get; set; }

        [CefField(KeyName = "cs5Label", FieldName = "deviceCustomString5Label", DataType = CefDataType.String, Length = 1023)]
        public string cs5Label { get; set; }

        [CefField(KeyName = "cs6", FieldName = "deviceCustomString6", DataType = CefDataType.String, Length = 4000)]
        public string cs6 { get; set; }

        [CefField(KeyName = "cs6Label", FieldName = "deviceCustomString6Label", DataType = CefDataType.String, Length = 1023)]
        public string cs6Label { get; set; }

        [CefField(KeyName = "destinationDnsDomain", FieldName = "destinationDnsDomain", DataType = CefDataType.String, Length = 255)]
        public string destinationDnsDomain { get; set; }

        [CefField(KeyName = "destinationServiceName", FieldName = "destinationServiceName", DataType = CefDataType.String, Length = 1023)]
        public string destinationServiceName { get; set; }

        [CefField(KeyName = "destinationTranslatedAddress", FieldName = "destinationTranslatedAddress", DataType = CefDataType.IPv4Address)]
        public string destinationTranslatedAddress { get; set; }

        [CefField(KeyName = "destinationTranslatedPort", FieldName = "destinationTranslatedPort", DataType = CefDataType.Integer)]
        public int? destinationTranslatedPort { get; set; }

        [CefField(KeyName = "deviceCustomDate1", FieldName = "deviceCustomDate1", DataType = CefDataType.TimeStamp)]
        public string deviceCustomDate1 { get; set; }

        [CefField(KeyName = "deviceCustomDate1Label", FieldName = "deviceCustomDate1Label", DataType = CefDataType.String, Length = 1023)]
        public string deviceCustomDate1Label { get; set; }

        [CefField(KeyName = "deviceCustomDate2", FieldName = "deviceCustomDate2", DataType = CefDataType.TimeStamp)]
        public string deviceCustomDate2 { get; set; }

        [CefField(KeyName = "deviceCustomDate2Label", FieldName = "deviceCustomDate2Label", DataType = CefDataType.String, Length = 1023)]
        public string deviceCustomDate2Label { get; set; }

        [CefField(KeyName = "deviceDirection", FieldName = "deviceDirection", DataType = CefDataType.Integer)]
        public int? deviceDirection { get; set; }

        [CefField(KeyName = "deviceDnsDomain", FieldName = "deviceDnsDomain", DataType = CefDataType.String, Length = 255)]
        public string deviceDnsDomain { get; set; }

        [CefField(KeyName = "deviceExternalId", FieldName = "deviceExternalId", DataType = CefDataType.String, Length = 255)]
        public string deviceExternalId { get; set; }

        [CefField(KeyName = "deviceFacility", FieldName = "deviceFacility", DataType = CefDataType.String, Length = 1023)]
        public string deviceFacility { get; set; }

        [CefField(KeyName = "deviceInboundInterface", FieldName = "deviceInboundInterface", DataType = CefDataType.String, Length = 128)]
        public string deviceInboundInterface { get; set; }

        [CefField(KeyName = "deviceNtDomain", FieldName = "deviceNtDomain", DataType = CefDataType.String, Length = 255)]
        public string deviceNtDomain { get; set; }

        [CefField(KeyName = "DeviceOutboundInterface", FieldName = "deviceOutboundInterface", DataType = CefDataType.String, Length = 128)]
        public string DeviceOutboundInterface { get; set; }

        [CefField(KeyName = "DevicePayloadId", FieldName = "devicePayloadId", DataType = CefDataType.String, Length = 128)]
        public string DevicePayloadId { get; set; }

        [CefField(KeyName = "deviceProcessName", FieldName = "deviceProcessName", DataType = CefDataType.String, Length = 1023)]
        public string deviceProcessName { get; set; }

        [CefField(KeyName = "dhost", FieldName = "destinationHostName", DataType = CefDataType.String, Length = 1023)]
        public string dhost { get; set; }

        [CefField(KeyName = "dmac", FieldName = "destinationMacAddress", DataType = CefDataType.MACAddress)]
        public string dmac { get; set; }

        [CefField(KeyName = "dntdom", FieldName = "destinationNtDomain", DataType = CefDataType.String, Length = 255)]
        public string dntdom { get; set; }

        [CefField(KeyName = "dpid", FieldName = "destinationProcessId", DataType = CefDataType.Integer)]
        public int? dpid { get; set; }

        [CefField(KeyName = "dpriv", FieldName = "destinationUserPrivileges", DataType = CefDataType.String, Length = 1023)]
        public string dpriv { get; set; }

        [CefField(KeyName = "dproc", FieldName = "destinationProcessName", DataType = CefDataType.String, Length = 1023)]
        public string dproc { get; set; }

        [CefField(KeyName = "dpt", FieldName = "destinationPort", DataType = CefDataType.Integer)]
        public int? dpt { get; set; }

        [CefField(KeyName = "dst", FieldName = "destinationAddress", DataType = CefDataType.IPv4Address)]
        public string dst { get; set; }

        [CefField(KeyName = "duid", FieldName = "destinationUserId", DataType = CefDataType.String, Length = 1023)]
        public string duid { get; set; }

        [CefField(KeyName = "duser", FieldName = "destinationUserName", DataType = CefDataType.String, Length = 1023)]
        public string duser { get; set; }

        [CefField(KeyName = "dvc", FieldName = "deviceAddress", DataType = CefDataType.IPv4Address)]
        public string dvc { get; set; }

        [CefField(KeyName = "dvchost", FieldName = "deviceHostName", DataType = CefDataType.String, Length = 100)]
        public string dvchost { get; set; }

        [CefField(KeyName = "dvcmac", FieldName = "deviceMacAddress", DataType = CefDataType.MACAddress)]
        public string dvcmac { get; set; }

        [CefField(KeyName = "dvcpid", FieldName = "deviceProcessId", DataType = CefDataType.Integer)]
        public int? dvcpid { get; set; }

        [CefField(KeyName = "end", FieldName = "endTime", DataType = CefDataType.TimeStamp)]
        public string end { get; set; }

        [CefField(KeyName = "externalId", FieldName = "externalId", DataType = CefDataType.String, Length = 40)]
        public string externalId { get; set; }

        [CefField(KeyName = "fileCreateTime", FieldName = "fileCreateTime", DataType = CefDataType.TimeStamp)]
        public string fileCreateTime { get; set; }

        [CefField(KeyName = "fileHash", FieldName = "fileHash", DataType = CefDataType.String, Length = 255)]
        public string fileHash { get; set; }

        [CefField(KeyName = "fileModificationTime", FieldName = "fileModificationTime", DataType = CefDataType.TimeStamp)]
        public string fileModificationTime { get; set; }

        [CefField(KeyName = "filePath", FieldName = "filePath", DataType = CefDataType.String, Length = 1023)]
        public string filePath { get; set; }

        [CefField(KeyName = "filePermission", FieldName = "filePermission", DataType = CefDataType.String, Length = 1023)]
        public string filePermission { get; set; }

        [CefField(KeyName = "fileType", FieldName = "fileType", DataType = CefDataType.String, Length = 1023)]
        public string fileType { get; set; }

        [CefField(KeyName = "flexDate1", FieldName = "flexDate1", DataType = CefDataType.TimeStamp)]
        public string flexDate1 { get; set; }

        [CefField(KeyName = "flexDate1Label", FieldName = "flexDate1Label", DataType = CefDataType.String, Length = 128)]
        public string flexDate1Label { get; set; }

        [CefField(KeyName = "flexString1", FieldName = "flexString1", DataType = CefDataType.String, Length = 1023)]
        public string flexString1 { get; set; }

        [CefField(KeyName = "flexString1Label", FieldName = "flexString2Label", DataType = CefDataType.String, Length = 128)]
        public string flexString1Label { get; set; }

        [CefField(KeyName = "flexString2Label", FieldName = "flexString2Label", DataType = CefDataType.String, Length = 128)]
        public string flexString2Label { get; set; }

        [CefField(KeyName = "fname", FieldName = "filename", DataType = CefDataType.String, Length = 1023)]
        public string fname { get; set; }

        [CefField(KeyName = "fsize", FieldName = "fileSize", DataType = CefDataType.Integer)]
        public int? fsize { get; set; }

        [CefField(KeyName = "in", FieldName = "bytesIn", DataType = CefDataType.Integer)]
        public int? inb { get; set; }

        [CefField(KeyName = "msg", FieldName = "message", DataType = CefDataType.String, Length = 1023)]
        public string msg { get; set; }

        [CefField(KeyName = "oldFileCreateTime", FieldName = "oldFileCreateTime", DataType = CefDataType.TimeStamp)]
        public string oldFileCreateTime { get; set; }

        [CefField(KeyName = "oldFileHash", FieldName = "oldFileHash", DataType = CefDataType.String, Length = 255)]
        public string oldFileHash { get; set; }

        [CefField(KeyName = "oldFileId", FieldName = "oldFileId", DataType = CefDataType.String, Length = 1023)]
        public string oldFileId { get; set; }

        [CefField(KeyName = "oldFileModificationTime", FieldName = "oldFileModificationTime", DataType = CefDataType.TimeStamp)]
        public string oldFileModificationTime { get; set; }

        [CefField(KeyName = "oldFileName", FieldName = "oldFileName", DataType = CefDataType.String, Length = 1023)]
        public string oldFileName { get; set; }

        [CefField(KeyName = "oldFilePath", FieldName = "oldFilePath", DataType = CefDataType.String, Length = 1023)]
        public string oldFilePath { get; set; }

        [CefField(KeyName = "oldFilePermission", FieldName = "oldFilePermission", DataType = CefDataType.String, Length = 1023)]
        public string oldFilePermission { get; set; }

        [CefField(KeyName = "oldFileSize", FieldName = "oldFileSize", DataType = CefDataType.Integer)]
        public int? oldFileSize { get; set; }

        [CefField(KeyName = "out", FieldName = "bytesOut", DataType = CefDataType.Integer)]
        public int? outb { get; set; }

        [CefField(KeyName = "outcome", FieldName = "eventOutcome", DataType = CefDataType.String, Length = 63)]
        public string outcome { get; set; }

        [CefField(KeyName = "proto", FieldName = "transportProtocol", DataType = CefDataType.String, Length = 31)]
        public string proto { get; set; }

        [CefField(KeyName = "reason", FieldName = "Reason", DataType = CefDataType.String, Length = 1023)]
        public string reason { get; set; }

        [CefField(KeyName = "request", FieldName = "requestUrl", DataType = CefDataType.String, Length = 1023)]
        public string request { get; set; }

        [CefField(KeyName = "requestClientApplication", FieldName = "requestClientApplication", DataType = CefDataType.String, Length = 1023)]
        public string requestClientApplication { get; set; }

        [CefField(KeyName = "requestContext", FieldName = "requestContext", DataType = CefDataType.String, Length = 2048)]
        public string requestContext { get; set; }

        [CefField(KeyName = "requestCookies", FieldName = "requestCookies", DataType = CefDataType.String, Length = 1023)]
        public string requestCookies { get; set; }

        [CefField(KeyName = "requestMethod", FieldName = "requestMethod", DataType = CefDataType.String, Length = 1023)]
        public string requestMethod { get; set; }

        [CefField(KeyName = "rt", FieldName = "deviceReceiptTime", DataType = CefDataType.TimeStamp)]
        public string rt { get; set; }

        [CefField(KeyName = "shost", FieldName = "sourceHostName", DataType = CefDataType.String, Length = 1023)]
        public string shost { get; set; }

        [CefField(KeyName = "smac", FieldName = "sourceMacAddress", DataType = CefDataType.MACAddress)]
        public string smac { get; set; }

        [CefField(KeyName = "sntdom", FieldName = "sourceNtDomain", DataType = CefDataType.String, Length = 255)]
        public string sntdom { get; set; }

        [CefField(KeyName = "sourceDnsDomain", FieldName = "sourceDnsDomain", DataType = CefDataType.String, Length = 255)]
        public string sourceDnsDomain { get; set; }

        [CefField(KeyName = "sourceServiceName", FieldName = "sourceServiceName", DataType = CefDataType.String, Length = 1023)]
        public string sourceServiceName { get; set; }

        [CefField(KeyName = "sourceTranslatedAddress", FieldName = "sourceTranslatedAddress", DataType = CefDataType.IPv4Address)]
        public string sourceTranslatedAddress { get; set; }

        [CefField(KeyName = "sourceTranslatedPort", FieldName = "sourceTranslatedPort", DataType = CefDataType.Integer)]
        public int? sourceTranslatedPort { get; set; }

        [CefField(KeyName = "spid", FieldName = "sourceProcessId", DataType = CefDataType.Integer)]
        public int? spid { get; set; }

        [CefField(KeyName = "spriv", FieldName = "sourceUserPrivileges", DataType = CefDataType.String, Length = 1023)]
        public string spriv { get; set; }

        [CefField(KeyName = "sproc", FieldName = "sourceProcessName", DataType = CefDataType.String, Length = 1023)]
        public string sproc { get; set; }

        [CefField(KeyName = "spt", FieldName = "sourcePort", DataType = CefDataType.Integer)]
        public int? spt { get; set; }

        [CefField(KeyName = "src", FieldName = "sourceAddress", DataType = CefDataType.IPv4Address)]
        public string src { get; set; }

        [CefField(KeyName = "start", FieldName = "startTime", DataType = CefDataType.TimeStamp)]
        public string start { get; set; }

        [CefField(KeyName = "suid", FieldName = "sourceUserId", DataType = CefDataType.String, Length = 1023)]
        public string suid { get; set; }

        [CefField(KeyName = "suser", FieldName = "sourceUserName", DataType = CefDataType.String, Length = 1023)]
        public string suser { get; set; }

        [CefField(KeyName = "type", FieldName = "type", DataType = CefDataType.Integer)]
        public int? type { get; set; }
    }
}