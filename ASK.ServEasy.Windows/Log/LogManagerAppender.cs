using System.Collections.Generic;
using log4net.Appender;

namespace ASK.ServEasy.Windows.Log
{
	class LogManagerAppender : IAppender
	{
		private List<LogMessage> theMessages;

		public LogManagerAppender()
		{
			theMessages = new List<LogMessage>();
		}

		public void PushLogMessages()
		{
			lock(theMessages)
			{
				foreach(LogMessage msg in theMessages)
					LogManager.Instance.ProcessLogMessage(msg);
				theMessages.Clear();
			}
		}

		public void Close()
		{
			
		}

		public void DoAppend(log4net.Core.LoggingEvent logEvent)
		{
			LogMessage logMsg = new LogMessage(logEvent);

			lock (theMessages)
				theMessages.Add(logMsg);
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
