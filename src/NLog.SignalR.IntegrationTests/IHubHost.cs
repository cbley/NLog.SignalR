namespace NLog.SignalR.IntegrationTests
{
    public interface IHubHost
    {
        void Start();
        void Stop();
    }
}