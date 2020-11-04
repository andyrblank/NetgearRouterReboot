using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.CommandLine;
using System.Net;
using static NetgearRouterReboot.Constants;

namespace NetgearRouterReboot
{
    class Program
    {
        /// <summary>
        /// Netgear Router Reboot
        /// </summary>
        /// <param name="username">This is the username for your Netgear router login. The default on most routers is admin.</param>
        /// <param name="password">This is your password for your Netgear router login.</param>
        /// <param name="ipaddress">This is the address of your Netgear router on your network. Typically this is something like 192.168.1.1 or 192.168.0.1</param>
        public static void Main(string username, string password, string ipaddress = "192.168.1.1")
        {
            Console.WriteLine(ProgramStartMessage);

            #region Validate Parameters

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine(UsernameRequired);
                Environment.ExitCode = ERROR_BAD_USERNAME;
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine(PasswordRequired);
                Environment.ExitCode = ERROR_BAD_PASSWORD;
                return;
            }

            IPAddress ip;
            var isIpAddressValid = IPAddress.TryParse(ipaddress, out ip);

            if (!isIpAddressValid)
            {
                Console.WriteLine(IPAddressInvalid);
                Environment.ExitCode = ERROR_BAD_IPADDRESS;
                return;
            }

            #endregion

            var routerConfig = new RouterConfig() {
                RouterUserName = username,
                RouterPassword = password,
                RouterIPAddress = ipaddress
            };               

            var routerCommands = new RouterCommands();

            #region Router Login

            Console.WriteLine(LoggingIntoRouter);
            //Send credentials to ip address and attempt to login.
            //Return value (loginResponse) should be form id for
            //authenticating the router reboot form post.
            string loginResponse;
            try
            {
                loginResponse = routerCommands.RouterLogin(routerConfig).Result;
            }
            catch (Exception)
            {
                Environment.ExitCode = ERROR_LOGIN_FAILED;
                return;
            }    

            if (loginResponse == LoginFailed)
            {
                Environment.ExitCode = ERROR_LOGIN_FAILED;
                return;
            }
            else
            {
                Console.WriteLine(LoginSuccess);
            }

            #endregion

            #region Router Reboot

            Console.WriteLine(RequestingReboot);
            bool rebootSuccess = false;
            try
            {
                rebootSuccess = routerCommands.RebootRouter(loginResponse, routerConfig).Result;
            }
            catch (Exception)
            {
                rebootSuccess = false;
            }

            if (rebootSuccess)
            {
                Console.WriteLine(RebootSuccess);
                Environment.ExitCode = ERROR_SUCCESS;
            }
            else
            {
                Console.WriteLine(RebootFailed);
                Environment.ExitCode = ERROR_FAIL;
            }

            #endregion
        }

    }
}
