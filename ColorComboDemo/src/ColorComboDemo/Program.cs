using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ColorComboDemo
{
    public class Program
    {
        /// <summary>
        ///     Main is the standard application entry point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Constructs a standard ASP.NET Core MVC host
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            // And execute out webapp!  To the Home Controller
            host.Run();
        }
    }
}
