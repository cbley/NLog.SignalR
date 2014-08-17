# NLog.SignalR

NLog.SignalR is a [SignalR](https://github.com/SignalR/SignalR) target for [NLog](https://github.com/jkowalski/NLog), allowing you to send log messages straight to a SignalR hub in real-time.

## Getting started

See the included Sample at /src/NLog.Signalr.Sample.Web and /src/NLog.SignalR.Sample.Command for an example of running two clients (web and console) at the same time and having log messages appear on the web log page from both sources.

To add to your own projects do the following.

#### Add NLog.SignalR.dll to your project(s) via [NuGet](http://www.nuget.org)

	> install-package NLog.SignalR

#### Configure NLog

Add the assembly and new target to NLog.config:

	<?xml version="1.0" encoding="utf-8" ?>
	<nlog 	xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      		xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      		autoReload="true"
      		throwExceptions="false">

  		<extensions>
    			<add assembly="NLog.SignalR" />
  		</extensions>

    		<targets async="true">
    			<target xsi:type="SignalR"
            			name="signalr"
            			uri="http://localhost:1860"
            			hubName ="LoggingHub"
            			methodName ="Log"
            			layout="${message}"
            		/>

  		</targets>
  		
		<rules>
    			<logger name="*" minlevel="Trace" writeTo="signalr" />
  		</rules>
	</nlog>

Note:  the only required property for the target is the uri.  The target will default hubName, methodName, and layout properties.

#### Set up Hub in server project to receive log events

There are two methods you can use for setting up the Hub in your web project.  One is to set up a strongly-typed hub, using the interface provided by the NLog.SignalR component.

	public class LoggingHub : Hub<ILoggingHub>
    	{
        	public void Log(LogEvent logEvent)
        	{
            		Clients.Others.Log(logEvent);
        	}
    	}

The other way is to simply set up a Hub in your web project using dynamic types.

	public class LoggingHub : Hub
    	{
        	public void Log(dynamic logEvent)
    		{
            		Clients.Others.Log(logEvent);
        	}
    	}

#### Set up client-side JavaScript to listen for log events

    	<script>
        
		    $(function() {
            	
			    var nlog = $.connection.loggingHub;
                nlog.client.log = function(logEvent) {
				    //Put code here to handle the logEvent that is sent.
			    };

                $.connection.hub.start().done(function() {
				    //Put code here that you want to execute after connecting to the Hub.
			    });
		    })		

        <Script>

## Feedback

Feel free to tweet [@cbley](http://twitter.com/cbley) or [@tmeinershagen](http://twitter.com/tmeinershagen) for questions or comments on the code.  You can also submit a GitHub issue [here](https://github.com/cbley/NLog.SignalR/issues).

## License

The MIT License

Copyright (c) 2012 Christopher Bley, Todd Meinershagen

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
