using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ClientApplicationMVC.Hubs
{
    public class ChatHub : Hub
    {
        // If you define a “public void” function within this class, 
        // you will be able to call it through javascript on the client side
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}