using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Test Api";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:8885")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddConsole();
                        logging.AddDebug();
                    })
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

    }
}
