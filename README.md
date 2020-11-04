# Netgear Router Reboot
A DotNet Core experiment in rebooting a Netgear router via command line so I can schedule my router reboots. 

Code is based off of WGET code sample found here: https://community.netgear.com/t5/DSL-Modems-Routers/Rebooting-D7000v2-via-wget-in-script-SOLVED/m-p/1763558.

# Usage
  NetgearRouterReboot [options]

Options:
  * --username <username>      This is the username for your Netgear router login. The default on most routers is admin.
  * --password <password>      This is your password for your Netgear router login.
  * --ipaddress <ipaddress>    This is the address of your Netgear router on your network. Typically this is something like 192.168.1.1 or * 192.168.0.1 [default: 192.168.1.1]
  * --version                  Show version information
  * -?, -h, --help             Show help and usage information
  
  ### Example
  `NetgearRouterReboot.exe --username admin --password MyRouterPassword --ipaddress 192.168.1.1`
  
  # Windows(x86) Binary Download
  This is a self-contained exe for the Netgear Router Reboot: [NetgearRouterReboot.zip](https://drive.google.com/file/d/1EDlEAjJKgskf1HnSpSdaUKcDNHSzUlKc/view?usp=sharing)
  