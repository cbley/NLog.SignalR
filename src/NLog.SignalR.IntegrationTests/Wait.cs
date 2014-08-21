using System;
using System.Diagnostics;
using System.Threading;
using NLog.SignalR.IntegrationTests.Hubs;

namespace NLog.SignalR.IntegrationTests
{
    public class Wait
    {
        public static void For(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public static void Until(Func<Test, bool> predicate, int seconds = 15)
        {
            var watch = new Stopwatch();
            watch.Start();

            while (predicate(Test.Current) == false && watch.Elapsed.Seconds < seconds)
            { }

            watch.Stop();
        }
    }
}