using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using PushSharp;
using PushSharp.Android;

namespace BlueZero.Air
{
    public interface IGCMPushService
    {
        void SendNotification(string deviceRegistrationId, string json);
    }
    
    public class GCMPushService : IGCMPushService
    {
        private const string AppPackageName = "com.bluezero.phaeton";
        private const string SenderId = "";
        private const string APIKey = "";

        private ILog _log;

        public GCMPushService(ILog log)
        {
            _log = log;
        }

        public void SendNotification(string deviceRegistrationId, string json)
        {
            try
            {
                PushBroker push = new PushBroker();

                //Wire up the events
                //push.Events.OnDeviceSubscriptionExpired += new Common.ChannelEvents.DeviceSubscriptionExpired(Events_OnDeviceSubscriptionExpired);
                //push.Events.OnDeviceSubscriptionIdChanged += new Common.ChannelEvents.DeviceSubscriptionIdChanged(Events_OnDeviceSubscriptionIdChanged);
                //push.Events.OnChannelException += new Common.ChannelEvents.ChannelExceptionDelegate(Events_OnChannelException);
                //push.Events.OnNotificationSendFailure += new Common.ChannelEvents.NotificationSendFailureDelegate(Events_OnNotificationSendFailure);
                //push.Events.OnNotificationSent += new Common.ChannelEvents.NotificationSentDelegate(Events_OnNotificationSent);

                //Configure and start Android GCM
                push.RegisterGcmService(new GcmPushChannelSettings(SenderId, APIKey, AppPackageName));

                //Fluent construction of an Android GCM Notification
                push.QueueNotification(new GcmNotification()
                  .ForDeviceRegistrationId(deviceRegistrationId)
                  .WithCollapseKey("NONE")
                  .WithJson(json));

                //Stop and wait for the queues to drain
                push.StopAllServices(true);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Exception on attempt to queue GCM notification: {0}", ex.ToString());
            }
        }
    }
}