using System;

namespace azmon.formatters.cef
{
    public class CefEvent
    {
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
          

    }
}