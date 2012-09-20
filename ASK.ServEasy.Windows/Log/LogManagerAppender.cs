using System.Collections.Generic;
using log4net.Appender;

namespace ASK.ServEasy.Windows.Log
{
	class LogManagerAppender : IAppender
	{
		private Queue<LogMessage> theMessages;

		public LogManagerAppender()
		{
			theMessages = new Queue<LogMessage>();
		}

		public void PushLogMessages()
		{
			while (theMessages.Count > 0)
			{
				LogManager.Instance.ProcessLogMessage(theMessages.Dequeue());
			}
		}

		public void Close()
		{
		}

		public void DoAppend(log4net.Core.LoggingEvent logEvent)
		{
			LogMessage logMsg = new LogMessage(logEvent);
			theMessages.Enqueue(logMsg);
		}

		public string Name
		{
			get
			{
				return "LogManagerAppender";
			}
			set
			{
				
			}
		}
	}
}
