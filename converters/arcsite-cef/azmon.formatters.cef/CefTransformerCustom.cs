using System;
using System.Collections.Generic;
using System.Text;

namespace azmon.formatters.cef
{
    class CefTransformerCustom
    {
        public const string DefaultFormatString = "MMM dd yyyy HH:mm:ss.fff";

      

        internal static string FormatValue(object obj, CefDataType type, int? length)
        {
            string baseVal = String.Empty;  

            // Format the result based on the type
            switch (type)
            {
                case CefDataType.TimeStamp:
                    var dt = (DateTime)obj;
                    if (dt != null)
                        baseVal = dt.ToString(DefaultFormatString);
                    else
                        baseVal = DateTime.UtcNow.ToString(DefaultFormatString);
                    break;

                case CefDataType.FloatingPoint:
                case CefDataType.Integer:
                case CefDataType.IPv4Address:
                case CefDataType.IPv6Address:
                case CefDataType.Long:
                case CefDataType.MACAddress:
                case CefDataType.String:
                default:
                    baseVal = obj.ToString();
                    break;
            }

            if (length.HasValue && length.Value > 0 && baseVal.Length > length.Value)
                baseVal = baseVal.Substring(0, length.Value - 1);
            return baseVal;
            
        }

        internal static void FillCustomProperties(CefCustomProperties props, 
            StringBuilder sb)
        {
            if (props.act != null)
            {
                var val = FormatValue(props.act, CefDataType.String, 63);
                sb.Append(props.act);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.app != null)
            {
                var val = FormatValue(props.app, CefDataType.String, 31);
                sb.Append(props.app);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.c6a1 != null)
            {
                var val = FormatValue(props.c6a1, CefDataType.IPv6Address, 0);
                sb.Append(props.c6a1);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.c6a1Label != null)
            {
                var val = FormatValue(props.c6a1Label, CefDataType.String, 1023);
                sb.Append(props.c6a1Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.c6a3 != null)
            {
                var val = FormatValue(props.c6a3, CefDataType.IPv6Address, 0);
                sb.Append(props.c6a3);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.c6a4 != null)
            {
                var val = FormatValue(props.c6a4, CefDataType.IPv6Address, 0);
                sb.Append(props.c6a4);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.C6a4Label != null)
            {
                var val = FormatValue(props.C6a4Label, CefDataType.String, 1023);
                sb.Append(props.C6a4Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cat != null)
            {
                var val = FormatValue(props.cat, CefDataType.String, 1023);
                sb.Append(props.cat);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cfp1 != null)
            {
                var val = FormatValue(props.cfp1, CefDataType.FloatingPoint, 0);
                sb.Append(props.cfp1);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cfp1Label != null)
            {
                var val = FormatValue(props.cfp1Label, CefDataType.String, 1023);
                sb.Append(props.cfp1Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cfp2 != null)
            {
                var val = FormatValue(props.cfp2, CefDataType.FloatingPoint, 0);
                sb.Append(props.cfp2);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cfp2Label != null)
            {
                var val = FormatValue(props.cfp2Label, CefDataType.String, 1023);
                sb.Append(props.cfp2Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cfp3 != null)
            {
                var val = FormatValue(props.cfp3, CefDataType.FloatingPoint, 0);
                sb.Append(props.cfp3);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cfp3Label != null)
            {
                var val = FormatValue(props.cfp3Label, CefDataType.String, 1023);
                sb.Append(props.cfp3Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cfp4 != null)
            {
                var val = FormatValue(props.cfp4, CefDataType.FloatingPoint, 0);
                sb.Append(props.cfp4);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cfp4Label != null)
            {
                var val = FormatValue(props.cfp4Label, CefDataType.String, 1023);
                sb.Append(props.cfp4Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cn1 != null)
            {
                var val = FormatValue(props.cn1, CefDataType.Long, 0);
                sb.Append(props.cn1);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cn1Label != null)
            {
                var val = FormatValue(props.cn1Label, CefDataType.String, 1023);
                sb.Append(props.cn1Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cn2 != null)
            {
                var val = FormatValue(props.cn2, CefDataType.Long, 0);
                sb.Append(props.cn2);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cn2Label != null)
            {
                var val = FormatValue(props.cn2Label, CefDataType.String, 1023);
                sb.Append(props.cn2Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cn3 != null)
            {
                var val = FormatValue(props.cn3, CefDataType.Long, 0);
                sb.Append(props.cn3);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cn3Label != null)
            {
                var val = FormatValue(props.cn3Label, CefDataType.String, 1023);
                sb.Append(props.cn3Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cnt != null)
            {
                var val = FormatValue(props.cnt, CefDataType.Integer, 0);
                sb.Append(props.cnt);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs1 != null)
            {
                var val = FormatValue(props.cs1, CefDataType.String, 4000);
                sb.Append(props.cs1);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs1Label != null)
            {
                var val = FormatValue(props.cs1Label, CefDataType.String, 1023);
                sb.Append(props.cs1Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs2 != null)
            {
                var val = FormatValue(props.cs2, CefDataType.String, 4000);
                sb.Append(props.cs2);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs2Label != null)
            {
                var val = FormatValue(props.cs2Label, CefDataType.String, 1023);
                sb.Append(props.cs2Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs3Label != null)
            {
                var val = FormatValue(props.cs3Label, CefDataType.String, 1023);
                sb.Append(props.cs3Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs4 != null)
            {
                var val = FormatValue(props.cs4, CefDataType.String, 4000);
                sb.Append(props.cs4);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs4Label != null)
            {
                var val = FormatValue(props.cs4Label, CefDataType.String, 1023);
                sb.Append(props.cs4Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs5 != null)
            {
                var val = FormatValue(props.cs5, CefDataType.String, 4000);
                sb.Append(props.cs5);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs5Label != null)
            {
                var val = FormatValue(props.cs5Label, CefDataType.String, 1023);
                sb.Append(props.cs5Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs6 != null)
            {
                var val = FormatValue(props.cs6, CefDataType.String, 4000);
                sb.Append(props.cs6);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.cs6Label != null)
            {
                var val = FormatValue(props.cs6Label, CefDataType.String, 1023);
                sb.Append(props.cs6Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.destinationDnsDomain != null)
            {
                var val = FormatValue(props.destinationDnsDomain, CefDataType.String, 255);
                sb.Append(props.destinationDnsDomain);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.destinationServiceName != null)
            {
                var val = FormatValue(props.destinationServiceName, CefDataType.String, 1023);
                sb.Append(props.destinationServiceName);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.destinationTranslatedAddress != null)
            {
                var val = FormatValue(props.destinationTranslatedAddress, CefDataType.IPv4Address, 0);
                sb.Append(props.destinationTranslatedAddress);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.destinationTranslatedPort != null)
            {
                var val = FormatValue(props.destinationTranslatedPort, CefDataType.Integer, 0);
                sb.Append(props.destinationTranslatedPort);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceCustomDate1 != null)
            {
                var val = FormatValue(props.deviceCustomDate1, CefDataType.TimeStamp, 0);
                sb.Append(props.deviceCustomDate1);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceCustomDate1Label != null)
            {
                var val = FormatValue(props.deviceCustomDate1Label, CefDataType.String, 1023);
                sb.Append(props.deviceCustomDate1Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceCustomDate2 != null)
            {
                var val = FormatValue(props.deviceCustomDate2, CefDataType.TimeStamp, 0);
                sb.Append(props.deviceCustomDate2);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceCustomDate2Label != null)
            {
                var val = FormatValue(props.deviceCustomDate2Label, CefDataType.String, 1023);
                sb.Append(props.deviceCustomDate2Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceDirection != null)
            {
                var val = FormatValue(props.deviceDirection, CefDataType.Integer, 0);
                sb.Append(props.deviceDirection);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceDnsDomain != null)
            {
                var val = FormatValue(props.deviceDnsDomain, CefDataType.String, 255);
                sb.Append(props.deviceDnsDomain);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceExternalId != null)
            {
                var val = FormatValue(props.deviceExternalId, CefDataType.String, 255);
                sb.Append(props.deviceExternalId);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceFacility != null)
            {
                var val = FormatValue(props.deviceFacility, CefDataType.String, 1023);
                sb.Append(props.deviceFacility);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceInboundInterface != null)
            {
                var val = FormatValue(props.deviceInboundInterface, CefDataType.String, 128);
                sb.Append(props.deviceInboundInterface);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceNtDomain != null)
            {
                var val = FormatValue(props.deviceNtDomain, CefDataType.String, 255);
                sb.Append(props.deviceNtDomain);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.DeviceOutboundInterface != null)
            {
                var val = FormatValue(props.DeviceOutboundInterface, CefDataType.String, 128);
                sb.Append(props.DeviceOutboundInterface);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.DevicePayloadId != null)
            {
                var val = FormatValue(props.DevicePayloadId, CefDataType.String, 128);
                sb.Append(props.DevicePayloadId);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.deviceProcessName != null)
            {
                var val = FormatValue(props.deviceProcessName, CefDataType.String, 1023);
                sb.Append(props.deviceProcessName);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dhost != null)
            {
                var val = FormatValue(props.dhost, CefDataType.String, 1023);
                sb.Append(props.dhost);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dmac != null)
            {
                var val = FormatValue(props.dmac, CefDataType.MACAddress, 0);
                sb.Append(props.dmac);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dntdom != null)
            {
                var val = FormatValue(props.dntdom, CefDataType.String, 255);
                sb.Append(props.dntdom);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dpid != null)
            {
                var val = FormatValue(props.dpid, CefDataType.Integer, 0);
                sb.Append(props.dpid);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dpriv != null)
            {
                var val = FormatValue(props.dpriv, CefDataType.String, 1023);
                sb.Append(props.dpriv);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dproc != null)
            {
                var val = FormatValue(props.dproc, CefDataType.String, 1023);
                sb.Append(props.dproc);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dpt != null)
            {
                var val = FormatValue(props.dpt, CefDataType.Integer, 0);
                sb.Append(props.dpt);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dst != null)
            {
                var val = FormatValue(props.dst, CefDataType.IPv4Address, 0);
                sb.Append(props.dst);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.duid != null)
            {
                var val = FormatValue(props.duid, CefDataType.String, 1023);
                sb.Append(props.duid);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.duser != null)
            {
                var val = FormatValue(props.duser, CefDataType.String, 1023);
                sb.Append(props.duser);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dvc != null)
            {
                var val = FormatValue(props.dvc, CefDataType.IPv4Address, 0);
                sb.Append(props.dvc);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dvchost != null)
            {
                var val = FormatValue(props.dvchost, CefDataType.String, 100);
                sb.Append(props.dvchost);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dvcmac != null)
            {
                var val = FormatValue(props.dvcmac, CefDataType.MACAddress, 0);
                sb.Append(props.dvcmac);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.dvcpid != null)
            {
                var val = FormatValue(props.dvcpid, CefDataType.Integer, 0);
                sb.Append(props.dvcpid);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.end != null)
            {
                var val = FormatValue(props.end, CefDataType.TimeStamp, 0);
                sb.Append(props.end);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.externalId != null)
            {
                var val = FormatValue(props.externalId, CefDataType.String, 40);
                sb.Append(props.externalId);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.fileCreateTime != null)
            {
                var val = FormatValue(props.fileCreateTime, CefDataType.TimeStamp, 0);
                sb.Append(props.fileCreateTime);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.fileHash != null)
            {
                var val = FormatValue(props.fileHash, CefDataType.String, 255);
                sb.Append(props.fileHash);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.fileModificationTime != null)
            {
                var val = FormatValue(props.fileModificationTime, CefDataType.TimeStamp, 0);
                sb.Append(props.fileModificationTime);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.filePath != null)
            {
                var val = FormatValue(props.filePath, CefDataType.String, 1023);
                sb.Append(props.filePath);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.filePermission != null)
            {
                var val = FormatValue(props.filePermission, CefDataType.String, 1023);
                sb.Append(props.filePermission);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.fileType != null)
            {
                var val = FormatValue(props.fileType, CefDataType.String, 1023);
                sb.Append(props.fileType);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.flexDate1 != null)
            {
                var val = FormatValue(props.flexDate1, CefDataType.TimeStamp, 0);
                sb.Append(props.flexDate1);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.flexDate1Label != null)
            {
                var val = FormatValue(props.flexDate1Label, CefDataType.String, 128);
                sb.Append(props.flexDate1Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.flexString1 != null)
            {
                var val = FormatValue(props.flexString1, CefDataType.String, 1023);
                sb.Append(props.flexString1);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.flexString1Label != null)
            {
                var val = FormatValue(props.flexString1Label, CefDataType.String, 128);
                sb.Append(props.flexString1Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.flexString2Label != null)
            {
                var val = FormatValue(props.flexString2Label, CefDataType.String, 128);
                sb.Append(props.flexString2Label);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.fname != null)
            {
                var val = FormatValue(props.fname, CefDataType.String, 1023);
                sb.Append(props.fname);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.fsize != null)
            {
                var val = FormatValue(props.fsize, CefDataType.Integer, 0);
                sb.Append(props.fsize);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.inb != null) {
                var val = FormatValue(props.inb, CefDataType.Integer, 0);
                sb.Append(props.inb);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.msg != null)
            {
                var val = FormatValue(props.msg, CefDataType.String, 1023);
                sb.Append(props.msg);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.oldFileCreateTime != null)
            {
                var val = FormatValue(props.oldFileCreateTime, CefDataType.TimeStamp, 0);
                sb.Append(props.oldFileCreateTime);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.oldFileHash != null)
            {
                var val = FormatValue(props.oldFileHash, CefDataType.String, 255);
                sb.Append(props.oldFileHash);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.oldFileId != null)
            {
                var val = FormatValue(props.oldFileId, CefDataType.String, 1023);
                sb.Append(props.oldFileId);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.oldFileModificationTime != null)
            {
                var val = FormatValue(props.oldFileModificationTime, CefDataType.TimeStamp, 0);
                sb.Append(props.oldFileModificationTime);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.oldFileName != null)
            {
                var val = FormatValue(props.oldFileName, CefDataType.String, 1023);
                sb.Append(props.oldFileName);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.oldFilePath != null)
            {
                var val = FormatValue(props.oldFilePath, CefDataType.String, 1023);
                sb.Append(props.oldFilePath);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.oldFilePermission != null)
            {
                var val = FormatValue(props.oldFilePermission, CefDataType.String, 1023);
                sb.Append(props.oldFilePermission);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.oldFileSize != null)
            {
                var val = FormatValue(props.oldFileSize, CefDataType.Integer, 0);
                sb.Append(props.oldFileSize);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.outb != null) {
                var val = FormatValue(props.outb, CefDataType.Integer, 0);
                sb.Append(props.outb);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.outcome != null)
            {
                var val = FormatValue(props.outcome, CefDataType.String, 63);
                sb.Append(props.outcome);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.proto != null)
            {
                var val = FormatValue(props.proto, CefDataType.String, 31);
                sb.Append(props.proto);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.reason != null)
            {
                var val = FormatValue(props.reason, CefDataType.String, 1023);
                sb.Append(props.reason);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.request != null)
            {
                var val = FormatValue(props.request, CefDataType.String, 1023);
                sb.Append(props.request);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.requestClientApplication != null)
            {
                var val = FormatValue(props.requestClientApplication, CefDataType.String, 1023);
                sb.Append(props.requestClientApplication);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.requestContext != null)
            {
                var val = FormatValue(props.requestContext, CefDataType.String, 2048);
                sb.Append(props.requestContext);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.requestCookies != null)
            {
                var val = FormatValue(props.requestCookies, CefDataType.String, 1023);
                sb.Append(props.requestCookies);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.requestMethod != null)
            {
                var val = FormatValue(props.requestMethod, CefDataType.String, 1023);
                sb.Append(props.requestMethod);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.rt != null)
            {
                var val = FormatValue(props.rt, CefDataType.TimeStamp, 0);
                sb.Append(props.rt);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.shost != null)
            {
                var val = FormatValue(props.shost, CefDataType.String, 1023);
                sb.Append(props.shost);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.smac != null)
            {
                var val = FormatValue(props.smac, CefDataType.MACAddress, 0);
                sb.Append(props.smac);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.sntdom != null)
            {
                var val = FormatValue(props.sntdom, CefDataType.String, 255);
                sb.Append(props.sntdom);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.sourceDnsDomain != null)
            {
                var val = FormatValue(props.sourceDnsDomain, CefDataType.String, 255);
                sb.Append(props.sourceDnsDomain);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.sourceServiceName != null)
            {
                var val = FormatValue(props.sourceServiceName, CefDataType.String, 1023);
                sb.Append(props.sourceServiceName);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.sourceTranslatedAddress != null)
            {
                var val = FormatValue(props.sourceTranslatedAddress, CefDataType.IPv4Address, 0);
                sb.Append(props.sourceTranslatedAddress);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.sourceTranslatedPort != null)
            {
                var val = FormatValue(props.sourceTranslatedPort, CefDataType.Integer, 0);
                sb.Append(props.sourceTranslatedPort);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.spid != null)
            {
                var val = FormatValue(props.spid, CefDataType.Integer, 0);
                sb.Append(props.spid);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.spriv != null)
            {
                var val = FormatValue(props.spriv, CefDataType.String, 1023);
                sb.Append(props.spriv);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.sproc != null)
            {
                var val = FormatValue(props.sproc, CefDataType.String, 1023);
                sb.Append(props.sproc);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.spt != null)
            {
                var val = FormatValue(props.spt, CefDataType.Integer, 0);
                sb.Append(props.spt);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.src != null)
            {
                var val = FormatValue(props.src, CefDataType.IPv4Address, 0);
                sb.Append(props.src);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.start != null)
            {
                var val = FormatValue(props.start, CefDataType.TimeStamp, 0);
                sb.Append(props.start);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.suid != null)
            {
                var val = FormatValue(props.suid, CefDataType.String, 1023);
                sb.Append(props.suid);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.suser != null)
            {
                var val = FormatValue(props.suser, CefDataType.String, 1023);
                sb.Append(props.suser);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }
            if (props.type != null)
            {
                var val = FormatValue(props.type, CefDataType.Integer, 0);
                sb.Append(props.type);
                sb.Append('=');
                sb.Append(val);
                sb.Append(' ');
            }


        }
    }
}
