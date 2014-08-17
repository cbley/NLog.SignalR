using System.Collections.Generic;

namespace NLog.SignalR.IntegrationTests.Hubs
{
    public class Test
    {
        private static readonly Test Instance = new Test();

        private Test()
        {
            SignalRLogEvents = new List<LogEvent>();
        }

        public static Test Current
        {
            get { return Instance; }
        }

        public IList<LogEvent> SignalRLogEvents { get; private set; }
    }
}
