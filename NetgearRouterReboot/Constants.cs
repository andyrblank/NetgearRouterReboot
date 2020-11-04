using System;
using System.Collections.Generic;
using System.Text;

namespace NetgearRouterReboot
{
    public static class Constants
    {
        #region Exit Codes
        public const int ERROR_SUCCESS = 0;
        public const int ERROR_FAIL = 1;
        public const int ERROR_BAD_IPADDRESS = 2;
        public const int ERROR_LOGIN_FAILED = 3;
        public const int ERROR_BAD_USERNAME = 4;
        public const int ERROR_BAD_PASSWORD = 5;
        #endregion

        #region Status Messages
        public const string ProgramStartMessage = "Validating router parameters";        
        public const string LoggingIntoRouter = "Logging into router...";
        public const string RequestingReboot = "Requesting Reboot...";
        #endregion

        #region successMessages
        public const string LoginSuccess = "Router Login was successful.";
        public const string RebootSuccess = "Reboot was successful.";
        #endregion

        #region Error Messages
        public const string IPAddressInvalid = "IP Address provided was invalid.";
        public const string LoginFailed = "Router login failed.";
        public const string UsernameRequired = "The username for the router is required.";
        public const string PasswordRequired = "The password for the router is required.";
        public const string RebootFailed = "Reboot failed.";
        #endregion
    }
}
