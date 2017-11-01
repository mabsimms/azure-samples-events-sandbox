using System;
using System.Linq;

namespace azmon.formatters.cef.testconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = System.IO.File.ReadAllText(@"fielddata.txt")
                .Replace("\n", "").Replace("\r", "");
            Console.WriteLine("Data count: " + data.Length);

            var fields = data.Split('.');
            foreach (var f in fields)
            {
                var tokens = f.Split(' ').Take(4).ToArray();
                if (tokens.Length == 4)
                {
                    Console.WriteLine("key={0}, name={1}, type={2}, len={3}",
                        tokens[0],
                        tokens[1],
                        tokens[2],
                        tokens[3]);
                }
                else{
                    Console.WriteLine("Could not parse: {0}", f);
                }
            }
        }
    }
}
