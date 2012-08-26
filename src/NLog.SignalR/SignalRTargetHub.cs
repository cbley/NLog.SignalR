using System;
using SignalR.Hubs;

namespace NLog.SignalR
{
    public class SignalRTargetHub : Hub
    {
        private const String NLogGroup = "NLogGroup";

        public SignalRTargetHub()
        {
            SignalRTarget.Instance.LogEventHandler = Send;
        }

        public void Listen()
        {
            Groups.Add(Context.ConnectionId, NLogGroup);
        }

        public void Send(String message, LogEventInfo logEventInfo)
        {
            Clients[NLogGroup].logEvent(message, logEventInfo);
        }
    }
}