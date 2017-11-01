using System;

namespace azmon.formatters.cef
{
    public class CefFieldAttribute : Attribute
    {
        public CefFieldAttribute() {}
        public CefFieldAttribute(string keyName, CefDataType dataType)
        {
            this.KeyName = keyName;
            this.DataType = dataType;
        }

        public CefFieldAttribute(string keyName, string fieldName, CefDataType dataType)
        {
			this.KeyName = keyName;
			this.DataType = dataType;
            this.FieldName = fieldName;
        }

        public CefFieldAttribute(string keyName, string fieldName, CefDataType dataType, int length)
        {
            this.KeyName = keyName;
            this.DataType = dataType;
            this.FieldName = fieldName;
            this.Length = length;
        }

		public string KeyName { get; set;  }
        public string FieldName { get; set;  }
        public CefDataType DataType { get; set; }
        public int Length { get; set;  }
    }

    public class CefFormatter
    {
		/// <summary>
		/// Cef uses https://docs.oracle.com/javase/7/docs/api/java/text/SimpleDateFormat.html
		/// Allowable forms are:
		/// Milliseconds since January 1, 1970 (integer). 
		/// MMM dd HH:mm:ss.SSS zzz
		/// MMM dd HH:mm:sss.SSS
		/// MMM dd HH:mm:ss zzz
		/// MMM dd HH:mm:ss
		/// MMM dd yyyy HH: mm:ss.SSS zzz
		/// MMM dd yyyy HH:mm:ss.SSS
		/// MMM dd yyyy HH: mm:ss zzz
		/// MMM dd yyyy HH:mm:ss
		///
		/// This sample defaults to MMM dd yyyy HH:mm:ss.SSS, or in .NET format
		/// MMM dd yyyy HH:mm:ss.fff
		/// </summary>
		public const string DefaultFormatString = "MMM dd yyyy HH:mm:ss.fff";

        private readonly string _formatString;

        public CefFormatter()
        {
            // Timestamp needs to be written out in a compatible format to 
            _formatString = DefaultFormatString;	
        }

        public string CefEventToCefRecord(CefEvent evt)
        {
            var sb = new System.Text.StringBuilder();

            var customExtensions = FormatExtensions(evt);
            sb.AppendFormat("{0} {1} {2}|{3}|{4}|{5}|{6}|{7}|{8}",
                evt.Timestamp.ToString(_formatString),
                evt.Host,
                evt.CefVersion,
                EscapeValue(evt.DeviceVendor),
                EscapeValue(evt.DeviceProduct),
                EscapeValue(evt.DeviceVersion),
                EscapeValue(evt.DeviceEventClassID),
                EscapeValue(evt.Name),
                EscapeValue(evt.Severity),
                customExtensions
            );
            return sb.ToString();
        }

        private string FormatExtensions(CefEvent evt)
        {
            return string.Empty;
        }

        public string EscapeValue(string str)
        {
            return str
                .Replace("|", "\\")     // Pipe must be escaped \|
                .Replace("\\", "\\\\")  // Backslash must be escaped \\
                .Replace("\r", "\\r")   // Newlines must be encoded
                .Replace("\n", "\\n")
            ;
        }
    }

    public class CefExtensionFormatter
    {
        protected static readonly CefExtensionField[] producerFields = new CefExtensionField[]
        {
            new CefExtensionField("act", "deviceAction", CefDataType.String, 63),
            new CefExtensionField("app", "applicationProtocol", CefDataType.String, 31),
            new CefExtensionField("cnt", "baseEventCount", CefDataType.Integer), 
            new CefExtensionField("c6a1", "rdeviceCustomIPv6Address1", CefDataType.IPv6Address), 
        };
        

        protected class CefExtensionField
        {
            public CefExtensionField() {}
            public CefExtensionField(string keyName, string fullName, CefDataType dataType, int length, string meaning = null)
            {
                this.KeyName = keyName;
                this.FullName = fullName;
                this.DataType = dataType;
                this.Length = length;
                this.Meaning = meaning ?? String.Empty; 
            }

			public CefExtensionField(string keyName, string fullName, CefDataType dataType)
			{
				this.KeyName = keyName;
				this.FullName = fullName;
				this.DataType = dataType;
				this.Length = -1;
				this.Meaning = String.Empty;
			}

            public CefExtensionField(string keyName, string fullName, CefDataType dataType, string meaning)
            {
                this.KeyName = keyName;
                this.FullName = fullName;
                this.DataType = dataType;
                this.Length = -1;
                this.Meaning = meaning ?? String.Empty; 
            }

            public string KeyName { get; set; }
            public string FullName { get; set; } 
            public CefDataType DataType { get; set; } 
            public int Length { get; set; }
            public string Meaning { get; set; }

        }

       
    }

    public enum CefDataType
	{
		String,
		Float,
		Long,
		Integer,
		IPv4Address,
		IPv6Address
	}
}