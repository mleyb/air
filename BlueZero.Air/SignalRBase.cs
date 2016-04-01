using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BlueZero.Air
{
    public abstract class SignalRBase<T> : Hub where T : IHub
    {
        private Lazy<IHubContext> hub = new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<T>());

        public IHubContext Hub
        {
            get { return hub.Value; }
        }
    }
}