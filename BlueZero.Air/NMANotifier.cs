using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NMALib;

namespace BlueZero.Air
{
    public static class NMANotifier
    {
        public static void SendNotification(string description, string @event, NMANotificationPriority priority)
        {
            var notification = new NMANotification
            {
                Description = description,
                Event = @event,
                Priority = priority
            };

            Task.Run(() => new NMAClient().PostNotification(notification));
        }
    }
}