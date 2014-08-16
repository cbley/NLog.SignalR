using Microsoft.AspNet.SignalR.Client;

namespace NLog.SignalR
{
    public interface IHubProxyFactory
    {
        IHubProxy Create(string uri, string hubName);
    }
}