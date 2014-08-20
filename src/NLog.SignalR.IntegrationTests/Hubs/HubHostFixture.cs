using NUnit.Framework;

namespace NLog.SignalR.IntegrationTests.Hubs
{
    public class HubHostFixture
    {
        private IHubHost _host;
        public const string BaseUrl = "http://localhost:1234";

        [TestFixtureSetUp]
        public void Init()
        {
            StartHost();
        }

        protected void StartHost()
        {
            _host = new HubHost(BaseUrl);
            _host.Start();
        }

        protected void StopHost()
        {
            _host.Stop();
            _host = null;
        }

        [SetUp]
        public void SetUp()
        {
            Test.Current.SignalRLogEvents.Clear();
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            StopHost();
        }
    }
}