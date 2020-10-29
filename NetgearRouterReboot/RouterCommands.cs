using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetgearRouterReboot
{
    public class RouterCommands
    {
        public async Task<string> RouterLogin(RouterConfig config)
        {
            HttpClient client = new HttpClient();

            //Get Router Details from config
            var userName = config.RouterUserName;
            var passwd = config.RouterPassword;
            var routerIP = config.RouterIPAddress;

            //Create authorization token from username and password
            var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");

            //Add authorization token to HttpClient
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(authToken));

            //Build request url
            var url = string.Concat("http://", routerIP, "/ADVANCED_home2.htm");

            //Send request to router with authentication header and get response
            var response = await client.GetAsync(url);

            //Get text string (html) from response
            var responseString = await response.Content.ReadAsStringAsync();

            //Create a new HTML Document (Html Agility Pack) https://html-agility-pack.net/
            var htmlDoc = new HtmlDocument();
            
            //Load response string/html into Html Document
            htmlDoc.LoadHtml(responseString);
            
            //Find the first form node in page (should ony be one anyway)
            var form = htmlDoc.DocumentNode.SelectSingleNode("//form");
            
            //The form node has an action property that contains a post url
            //and an id for authenticating the POST. This gets the contents 
            //of the action attribute.
            var formAction = form.Attributes["action"].Value;

            //Now that we have the form attribute string we need 
            //to find the position of id= in the string and add
            //3 to get to the end of that position.
            var idPosition = formAction.IndexOf("id=") + 3;

            //Now we take the length of the whole form action 
            //string and subtract the id= position from it
            //to determine the length of the actual id value
            var idLength = formAction.Length - idPosition;

            //Now we can use the start position and length of the id
            //to remove it from the form action string.
            var id = formAction.Substring(idPosition, idLength);

            //return form id as result.
            return id;
        }

        public async Task<bool> RebootRouter(string id, RouterConfig config)
        {
            HttpClient client = new HttpClient();

            //Get Router Details from config            
            var userName = config.RouterUserName;
            var passwd = config.RouterPassword;
            var routerIP = config.RouterIPAddress;

            //Create authorization token from username and password
            var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");

            //Add authorization token to HttpClient
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(authToken));

            //Build request url
            var url = string.Concat("http://", routerIP, "/newgui_adv_home.cgi?id=", id);

            //Add form post values for reboot
            var values = new Dictionary<string, string>
            {
                { "id", id },
                { "buttonType", "2" }
            };

            //Encode form values
            var content = new FormUrlEncodedContent(values);

            //Send request to router with authentication header 
            //and form content and get response
            var response = await client.PostAsync(url,content);

            if (response != null && response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
            
        }
    }
}
