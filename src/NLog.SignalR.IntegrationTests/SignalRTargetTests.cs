﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NLog.Config;
using NLog.SignalR.IntegrationTests.Hubs;
using NUnit.Framework;

namespace NLog.SignalR.IntegrationTests
{
    [TestFixture]
    public class given_hub_running : HubHostFixture
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Test]
        public void given_nlog_configured_to_use_signalr_target_for_hub_when_logging_events_should_log_to_signalr()
        {
            var target = new SignalRTarget
            {
                Name = "signalr",
                Uri = HubHost.BaseUrl,
                Layout = "${message}"
            };
            SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Trace);

            const string expectedMessage = "This is a sample trace message.";
            Logger.Trace(expectedMessage);
            Logger.Error(expectedMessage);

            WaitForAsyncWrite();

            Test.Current.SignalRLogEvents.Should().Contain(x => x.Level == "Trace" && x.Message == expectedMessage);
            Test.Current.SignalRLogEvents.Should().Contain(x => x.Level == "Error" && x.Message == expectedMessage);
        }

        [Test]
        public void given_nlog_configured_to_use_signalr_target_for_hub_when_logging_events_from_multiple_threads_should_log_to_signalr()
        {
            var target = new SignalRTarget
            {
                Name = "signalr",
                Uri = HubHost.BaseUrl,
                Layout = "${message}"
            };
            SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Trace);

            const string expectedMessage = "This is a sample trace message.";

            Action action1 = () =>
            {
                Logger.Trace(expectedMessage);
                WaitForAsyncWrite();
            };

            Action action2 = () =>
            {
                Logger.Error(expectedMessage);
                WaitForAsyncWrite();
            };

            Parallel.Invoke(action1, action2);
            
            Test.Current.SignalRLogEvents.Should().Contain(x => x.Level == "Trace" && x.Message == expectedMessage);
            Test.Current.SignalRLogEvents.Should().Contain(x => x.Level == "Error" && x.Message == expectedMessage);
        }

        [Test]
        public void
            given_nlog_configured_to_use_signalr_target_for_wrong_hub_uri_when_logging_events_should_not_log_to_signalr()
        {
            var target = new SignalRTarget
            {
                Name = "signalr",
                Uri = "http://localhost:1450",
                Layout = "${message}"
            };
            SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Trace);

            const string expectedMessage = "This is a sample trace message.";
            Logger.Trace(expectedMessage);

            WaitForAsyncWrite();

            Test.Current.SignalRLogEvents.Should().NotContain(x => x.Level == "Trace" && x.Message == expectedMessage);
        }

        [Test]
        public void
            given_nlog_configured_to_use_signalr_target_for_hub_with_wrong_hub_name_when_logging_events_should_not_log_to_signalr
            ()
        {
            var target = new SignalRTarget
            {
                Name = "signalr",
                Uri = HubHost.BaseUrl,
                HubName = "TestHub",
                Layout = "${message}"
            };
            SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Trace);

            const string expectedMessage = "This is a sample trace message.";
            Logger.Trace(expectedMessage);

            WaitForAsyncWrite();

            Test.Current.SignalRLogEvents.Should().NotContain(x => x.Level == "Trace" && x.Message == expectedMessage);
        }

        [Test]
        public void
            given_nlog_configured_to_use_signalr_target_for_hub_with_wrong_method_name_when_logging_events_should_not_log_to_signalr
            ()
        {
            var target = new SignalRTarget
            {
                Name = "signalr",
                Uri = HubHost.BaseUrl,
                MethodName = "Test",
                Layout = "${message}"
            };
            SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Trace);

            const string expectedMessage = "This is a sample trace message.";
            Logger.Trace(expectedMessage);

            WaitForAsyncWrite();

            Test.Current.SignalRLogEvents.Should().NotContain(x => x.Level == "Trace" && x.Message == expectedMessage);
        }

        private static void WaitForAsyncWrite()
        {
            Thread.Sleep(1000);
        }
    }

    [TestFixture]
    public class given_hub_not_running
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Test]
        public void given_nlog_configured_to_use_signalr_target_for_hub_when_logging_events_should_not_log_to_signalr()
        {
            var target = new SignalRTarget
            {
                Name = "signalr",
                Uri = HubHost.BaseUrl,
                Layout = "${message}"
            };
            SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Trace);

            const string expectedMessage = "This is a sample trace message.";
            Logger.Trace(expectedMessage);

            Thread.Sleep(1000);

            Test.Current.SignalRLogEvents.Should().NotContain(x => x.Level == "Trace" && x.Message == expectedMessage);
        }
    }
}
