using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;

namespace NLog.SignalR.IntegrationTests.Hubs
{
    public class HubHost : IHubHost
    {
        private bool _active;
        private readonly Uri _baseUri;
        private IDisposable _webApp;

        public HubHost(string baseUrl)
        {
            _baseUri = new Uri(baseUrl);
        }

        public void Start()
        {
            var url = string.Format("http://+:{0}", _baseUri.Port);
            _webApp = WebApp.Start<Startup>(url);
            _active = true;
        }

        public void Stop()
        {
            if (_active)
            {
                _webApp.Dispose();
                _webApp = null;
                _active = false;
            }
        }

        internal class LoggingHub : Hub<ILoggingHub>
        {
            public void Log(LogEvent logEvent)
            {
                Test.Current.SignalRLogEvents.Add(logEvent);
                Clients.All.Log(logEvent);
            }
        }

        internal class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                app.MapSignalR();
            }
        }
    }
}