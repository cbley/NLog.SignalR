using System;
using System.ComponentModel;
using Microsoft.AspNet.SignalR.Client;
using NLog.Common;
using NLog.Config;
using NLog.Targets;

namespace NLog.SignalR
{
    [Target("SignalR")]
    public class SignalRTarget : TargetWithLayout
    {
        public static SignalRTarget Instance = new SignalRTarget();
        public Action<String, LogEventInfo> LogEventHandler;

        [RequiredParameter]
        public string Uri { get; set; }

        [DefaultValue("LoggingHub")]
        public string HubName { get; set; }

        [DefaultValue("Log")]
        public string MethodName { get; set; }


        public IHubProxyFactory ProxyFactory { get; set; }

        private IHubProxy _proxy;

        public SignalRTarget()
        {
            HubName = "LoggingHub";
            MethodName = "Log";
            ProxyFactory = new HubProxyFactory();
        }

        protected override void Write(LogEventInfo logEvent)
        {
            Log(logEvent);
        }

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            Log(logEvent.LogEvent);
        }

        private void Log(LogEventInfo logEvent)
        {
            EnsureProxyExists();

            if (_proxy == null)
                return;
            
            var renderedMessage = Layout.Render(logEvent);
            var item = new LogEvent(logEvent, renderedMessage);
            _proxy.Invoke(MethodName, item);
        }

        private void EnsureProxyExists()
        {
            if (_proxy == null)
                _proxy = ProxyFactory.Create(Uri, HubName);
        }
    }
}
