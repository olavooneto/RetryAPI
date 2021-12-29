using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RetryApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            // get culture names
            List<string> list = new List<string>();
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                string specName = "(none)";
                try { specName = CultureInfo.CreateSpecificCulture(ci.Name).Name; } catch { }
                list.Add(String.Format("{0,-12}{1,-12}{2}", ci.Name, specName, ci.EnglishName));
            }

            list.Sort();  // sort by name

            // write to console
            Console.WriteLine("CULTURE   SPEC.CULTURE  ENGLISH NAME");
            Console.WriteLine("--------------------------------------------------------------");
            foreach (string str in list)
                Console.WriteLine(str);

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
