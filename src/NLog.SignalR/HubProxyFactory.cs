using System;
using Microsoft.AspNet.SignalR.Client;

namespace NLog.SignalR
{
    public class HubProxyFactory : IHubProxyFactory
    {
        public IHubProxy Create(string uri, string hubName)
        {
            IHubProxy proxy = null;

            try
            {
                var hubConnection = new HubConnection(uri);
                proxy = hubConnection.CreateHubProxy(hubName);
                hubConnection.Start().Wait();

                proxy.Invoke("Notify", hubConnection.ConnectionId);
            }
            catch (Exception ex)
            {
                proxy = null;
            }

            return proxy;
        }
    }
}