using System;
using Microsoft.AspNet.SignalR.Client;

namespace NLog.SignalR
{
    public class HubProxy
    {
        private readonly SignalRTarget _target;
        public HubConnection Connection;
        private IHubProxy _proxy;

        public HubProxy(SignalRTarget target)
        {
            _target = target;
        }

        public void Log(LogEvent logEvent)
        {
            EnsureProxyExists();

            if (_proxy != null)
                _proxy.Invoke(_target.MethodName, logEvent);
        }

        public void EnsureProxyExists()
        {
            if (_proxy == null || Connection.State == ConnectionState.Disconnected)
            {
                try
                {
                    Connection = new HubConnection(_target.Uri);
                    _proxy = Connection.CreateHubProxy(_target.HubName);
                    Connection.Start().Wait();

                    _proxy.Invoke("Notify", Connection.ConnectionId);
                }
                catch (Exception ex)
                {
                    _proxy = null;
                }
            }
        }
    }
}
