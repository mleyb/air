using System;
using System.Diagnostics;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BlueZero.Air
{
    [HubName("trace")]
    public class TraceHub : SignalRBase<TraceHub>
    {
        private const string Log4NetGroup = "Log4NetGroup";

        public TraceHub()
        {
            SignalRAppender.Instance.MessageLogged = OnMessageLogged;
        }

        public void Listen() 
        {
            Groups.Add(Context.ConnectionId, Log4NetGroup);         
        }

        private void OnMessageLogged(LogEntry e)
        {
            Clients.Group(Log4NetGroup).onTraceEvent(e.FormattedEvent, e.LoggingEvent);            
        }
    }
}