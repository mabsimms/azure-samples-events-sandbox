using azmon.events;
using azmon.formatters.cef;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace testconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var transformer = new CefTransformer();
            var formatter = new CefFormatter();

            var file = @"c:\temp\azmon\0a1a4a8e-ac21-4390-b5cd-1ad049918298.json";
            var content = System.IO.File.ReadAllText(file);
            var jobj = JObject.Parse(content);
            var records = (JArray)jobj["records"];
            foreach (var r in records)
            {
                var txt = r.ToString();
                var baseRecord = JsonConvert.DeserializeObject(txt, typeof(AzureEventBase))
                    as AzureEventBase;
                if (baseRecord == null)
                    continue;
                baseRecord.UpdateFields();
                baseRecord.RawJson = txt;

                var cefEvent = transformer.Convert(baseRecord);
                var cefString = formatter.CefEventToCefRecord(cefEvent);
                Console.WriteLine(cefString);
            }             
        }
    }
}
