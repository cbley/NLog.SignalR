# NLog.SignalR

NLog.SignalR is a [NLog](https://github.com/jkowalski/NLog) target for [SignalR](https://github.com/SignalR/SignalR). Allowing you to stream log events from application to client-side JavaScript.

## Getting started

See the included Demo ASP.NET MVC project for reference.

#### Reference NLog.SignalR.dll in your project

#### Configure NLog

Add the assembly and new target to NLog.config:

	<?xml version="1.0" encoding="utf-8" ?>
	<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
	      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	  <extensions>
	    <add assembly="NLog.SignalR"/>
	  </extensions>
	  <targets>
	    <target xsi:type="SignalR" name="signalr"
	            layout="${longdate} ${uppercase:${level}} ${message}" />
	  </targets>

	  <rules>
	    <logger name="*" minlevel="Trace" writeTo="signalr" />
	  </rules>
	</nlog>

#### Setup client-side JavaScript to listen for log events

    var nlog = $.connection.signalRTargetHub;

    nlog.logEvent = function (message, logEventInfo) {
    	// called everytime a new log message is sent, use message or
    	// logEventInfo to update the page or whatever you want to do 
    	// with logs
    };

    $.connection.hub.start(function () {
        nlog.listen();
    });

## Caveats

* Currently the web project comsuming the SignalR log events has to be in the same App Domain as the project emitting the events.

## Feedback

Feel free to tweet me [@cbley](http://twitter.com/cbley) or submit a GitHub issue.

## License

The MIT License

Copyright (c) 2012 Christopher Bley

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
