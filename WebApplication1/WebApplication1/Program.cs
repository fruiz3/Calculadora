using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Calculadora;
namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            JournalList.Listado = new Dictionary<int, string>();

            // Configuración inicial de Nlog.
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
            config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logfile);

            NLog.LogManager.Configuration = config;

            

            CreateWebHostBuilder(args).Build().Run();


            
        }




    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }

    public static class JournalList
    {
        public static IDictionary<int, string> Listado;
        public static int id = 0;
    }
}
