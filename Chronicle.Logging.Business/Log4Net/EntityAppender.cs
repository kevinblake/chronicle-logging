using System;
using System.Diagnostics;
using Chronicle.Logging.Business.DataAccess;
using Chronicle.Logging.Business.Entities;
using log4net.Appender;
using log4net.Core;

namespace Chronicle.Logging.Business.Log4Net
{
	public class EntityAppender : AppenderSkeleton
	{
		protected override void Append(LoggingEvent loggingEvent)
		{
			try
			{
			    using (var db = new LogDbContext())
			    {
			        db.Logs.Add(
			            new Log
			                {
			                    Id = Guid.NewGuid(),
			                    UserName = loggingEvent.UserName,
			                    Date = DateTime.UtcNow,
			                    Level = loggingEvent.Level.ToString(),
			                    Message = loggingEvent.RenderedMessage,
			                    Thread = loggingEvent.ThreadName,
			                    Exception = loggingEvent.GetExceptionString(),
			                    Logger = loggingEvent.LoggerName,
			                    Custom = string.Empty,
			                    Category = loggingEvent.LoggerName
			                });

			        db.SaveChanges();
			    }
			}
			catch (Exception e)
			{
				try
				{
					Trace.Write("Error writing log to database {0}", e.Message);
					EventLog.WriteEntry("Logging", "Error writing log to database", EventLogEntryType.Error);
				}
				catch (Exception)
				{
					// Ignore error
				}
			}
		}
	}
}