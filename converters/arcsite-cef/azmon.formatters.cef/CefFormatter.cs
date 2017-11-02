using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

            sb.AppendFormat("{0} {1} {2}|{3}|{4}|{5}|{6}|{7}|{8}|",
                evt.Timestamp.ToString(_formatString),
                evt.Host,
                evt.CefVersion,
                EscapeValue(evt.DeviceVendor),
                EscapeValue(evt.DeviceProduct),
                EscapeValue(evt.DeviceVersion),
                EscapeValue(evt.DeviceEventClassID),
                EscapeValue(evt.Name),
                EscapeValue(evt.Severity)
            );
            CefTransformerCustom.FillCustomProperties(evt.CustomProperties, sb);

            return sb.ToString();
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

    //public class CefExtensionFormatter
    //{
    //    private readonly Action<CefCustomProperties, StringBuilder>[] actions;

    //    public CefExtensionFormatter()
    //    {
    //        actions = CreateFormatFunctions();
    //    }

    //    public string FormatExtensions(CefCustomProperties props)
    //    {
    //        var sb = new StringBuilder();
    //        foreach (var a in actions)
    //            a(props, sb);
    //        return sb.ToString();
    //    }

    //    protected Action<CefCustomProperties, StringBuilder>[] CreateFormatFunctions()
    //    {
    //        var list = new List<Action<CefCustomProperties, StringBuilder>>();

    //        var type = typeof(CefCustomProperties);
    //        var props = type.GetProperties();
    //        foreach (var p in props)
    //        {
    //            // Get the attribute for formatting data
    //            var attribute = p.GetCustomAttributes(false).OfType<CefFieldAttribute>().First();
    //            if (attribute == null)
    //                continue;

    //            // Generate a lambda for the property extraction
    //            // TODO - fill these in
    //            Func<CefCustomProperties, bool> hasValue = (e) => false;
    //            Func<CefCustomProperties, string> getValue = (e) => "";

                
    //            // Generate a lambda for the property formatting
    //            Action<CefCustomProperties, StringBuilder> action = (cef, sb) =>
    //            {
    //                sb.Append(attribute.KeyName);
    //                sb.Append('=');

    //                // TODO switch to a function
    //                var value = EscapeCustomRecord(
    //                    FormatValue(attribute.DataType, getValue(cef)));
    //                sb.Append(value);
    //            };

    //            // Add the lambda chain to the list
    //            list.Add(action);                
    //        }
    //        return list.ToArray();
    //    }       

    //    public static string EscapeCustomRecord(string rec)
    //    {
    //        return rec
    //            .Replace("=", "\\=")  // Equals must be escaped \\
    //            .Replace("\\", "\\\\")  // Backslash must be escaped \\
    //            .Replace("\r", "\\r")   // Newlines must be encoded
    //            .Replace("\n", "\\n");             
    //    }

    //    public static string FormatValue(CefDataType dataType, object val)
    //    {

    //        // Format the result based on the type
    //        switch (dataType)
    //        {
    //            case CefDataType.FloatingPoint:
    //                break;

    //            case CefDataType.Integer:
    //                break;

    //            case CefDataType.IPv4Address:
    //                break;

    //            case CefDataType.IPv6Address:
    //                break;

    //            case CefDataType.Long:
    //                break;

    //            case CefDataType.MACAddress:
    //                break;

    //            case CefDataType.String:
    //                break;

    //            case CefDataType.TimeStamp:
    //                break;
    //        }
    //        return "";
    //    }
    //}

    public enum CefDataType
	{
		String,
        FloatingPoint,
		Long,
		Integer,
		IPv4Address,
		IPv6Address,
        TimeStamp,
        MACAddress
    }
}