using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace IFZRBot
{
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);

            if (message.Contains("!if") && message.StartsWith("!if"))
            {
                string cmd = message.Remove(0, 3);
                switch (cmd)
                {
                    case "hunt":
                        string result = "Hunting";
                        Clients.All.addNewMessageToPage("IFZRBot", result);
                        break;
                }
            }

        }
    }
}