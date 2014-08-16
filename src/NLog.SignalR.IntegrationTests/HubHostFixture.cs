using NUnit.Framework;

namespace NLog.SignalR.IntegrationTests
{
    public class HubHostFixture
    {
        private IHubHost _host;
        [TestFixtureSetUp]
        public void Init()
        {
            _host = new HubHost();
            _host.Start();
        }

        [SetUp]
        public void SetUp()
        {
            Test.Current.SignalRLogEvents.Clear();
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            _host.Stop();
        }
    }
}