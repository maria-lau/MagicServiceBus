using System;
using System.Threading.Tasks;
using System.Web;
using Messages.DataTypes.Collections;
using Microsoft.AspNet.SignalR;

namespace ClientApplicationMVC.Hubs
{
    public class ChatHub : Hub
    {
        public static ConnectionDictionary connectionList;

        public override Task OnConnected()
        {
            if(connectionList == null)
            {
                connectionList = new ConnectionDictionary();
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            connectionList.RemoveByConnectionID(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }


        // If you define a “public void” function within this class, 
        // you will be able to call it through javascript on the client side
        public void send(string receiver, string message)
        {
            string senderusername = connectionList.getUsername(Context.ConnectionId);
            string receiverID = connectionList.getConnectionID(receiver);
            if (receiverID == null)
            {
                return;
            }
            else
            {
                this.Clients.Client(receiverID).instantMessage(senderusername, message);
            }
        }

        public void userOnline(string username)
        {
            connectionList.Add(username, Context.ConnectionId);
        }
    }
}