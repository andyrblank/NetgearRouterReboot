using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace NetgearRouterReboot
{
    class Program
    {
        private const int ERROR_SUCCESS = 0;
        private const int ERROR_FAIL = 1;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Loading Router Configuration...");

            //Load settings from appsettings.json
            IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)            
            .Build();

            //Fill RouterConfig model with values from appsettings.json
            var routerConfig = new RouterConfig() {
                RouterUserName = Configuration.GetSection("RouterUserName").Value,
                RouterPassword = Configuration.GetSection("RouterPassword").Value,
                RouterIPAddress = Configuration.GetSection("RouterIPAddress").Value
            };               

            var routerCommands = new RouterCommands();

            Console.WriteLine("Logging into router...");
            var id = await routerCommands.RouterLogin(routerConfig);

            Console.WriteLine("Requesting Reboot...");
            var rebootSuccess = await routerCommands.RebootRouter(id,routerConfig);

            if (rebootSuccess)
            {
                Console.WriteLine("Reboot was successful.");
                Environment.ExitCode = ERROR_SUCCESS;
            }
            else
            {
                Console.WriteLine("Reboot failed.");
                Environment.ExitCode = ERROR_FAIL;
            }

            Console.WriteLine("Done.");
        }
                
    }
}
