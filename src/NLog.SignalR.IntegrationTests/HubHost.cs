using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;

namespace NLog.SignalR.IntegrationTests
{
    public class HubHost : IHubHost
    {
        public const string BaseUrl = "http://localhost:1234";
        private readonly Uri _baseUri;
        private IDisposable _webApp;

        public HubHost()
        {
            _baseUri = new Uri(BaseUrl);
        }

        public void Start()
        {
            var url = string.Format("http://+:{0}", _baseUri.Port);
            _webApp = WebApp.Start<Startup>(url);
        }

        public void Stop()
        {
            _webApp.Dispose();
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