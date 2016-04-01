using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WebMatrix.WebData;

namespace BlueZero.Air
{
    public class NotifierHub : SignalRBase<NotifierHub>
    {
        public static class MembershipManager
        {
            public static void AddConnection(string connectionId, string groupName)
            {                
                var context = GlobalHost.ConnectionManager.GetHubContext<NotifierHub>();

                context.Groups.Add(connectionId, groupName);
            }

            public static void RemoveConnection(string connectionId, string groupName)
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<NotifierHub>();

                context.Clients.Group(connectionId, groupName);
            }
        }

        public NotifierHub()
        {
        }

        public static void SendToCurrentUser(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotifierHub>();

            context.Clients.Group(WebSecurity.CurrentUserId.ToString()).send(message);
        }

        public static void SendToUser(int userId, string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotifierHub>();

            // user Id is group name
            context.Clients.Group(userId.ToString()).send(message);
        }

        public static void Broadcast(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotifierHub>();

            context.Clients.All.send(message);
        }

        public override Task OnConnected()
        {
            MembershipManager.AddConnection(Context.ConnectionId, WebSecurity.CurrentUserId.ToString());
            
            return Clients.All.joined(Context.ConnectionId, DateTime.Now.ToString());
        }

        public override Task OnDisconnected()
        {
            return Clients.All.leave(Context.ConnectionId, DateTime.Now.ToString());
        }

        public override Task OnReconnected()
        {
            MembershipManager.AddConnection(Context.ConnectionId, WebSecurity.CurrentUserId.ToString());

            return Clients.All.rejoined(Context.ConnectionId, DateTime.Now.ToString());
        }
    }
}