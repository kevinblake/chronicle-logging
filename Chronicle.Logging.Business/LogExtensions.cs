using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Chronicle.Logging.Business.Entities;
using Chronicle.Logging.Business.Reporting;

namespace Chronicle.Logging.Business
{
	public static class LogExtensions
	{
		public static string[] GetCategories(this DbSet<Log> logs)
		{
			return logs.Select(l => l.Category).Distinct().ToArray();
		}

		public static string[] GetLevels(this DbSet<Log> logs)
		{
			return logs.Select(l => l.Level).Distinct().ToArray();
		}

		public static Log GetById(this DbSet<Log> logs, Guid logId)
		{
			return logs.SingleOrDefault(l => l.Id == logId);
		}

        public static IEnumerable<Log> GetPage(this DbSet<Log> logs, int pageNumber, int pageSize)
        {
            return logs.OrderByDescending(l => l.Date).Skip(pageSize * (pageNumber -1)).Take(pageSize);
        }

		public static IEnumerable<LogSummary> GetLogSummary(this DbSet<Log> logs, DateTime startDate, DateTime endDate)
		{
			string[] categories = logs.GetCategories();

			var filteredLogs = logs.Where(l => l.Date > startDate && l.Date <= endDate);

			IEnumerable<LogSummary> summary =
				categories.Select(
					c =>
					new LogSummary
						{
							Category = c,
							Debug = filteredLogs.Count(l => l.Category == c && l.Level == "DEBUG"),
							Error = filteredLogs.Count(l => l.Category == c && l.Level == "ERROR"),
							Fatal = filteredLogs.Count(l => l.Category == c && l.Level == "FATAL"),
							Warn = filteredLogs.Count(l => l.Category == c && l.Level == "WARN"),
							Info = filteredLogs.Count(l => l.Category == c && l.Level == "INFO")
						});
			return summary;
		}

		public static IEnumerable<LogSummary> GetLogSummary(this DbSet<Log> logs)
		{
			return logs.GetLogSummary(DateTime.UtcNow.Subtract(TimeSpan.FromDays(7)), DateTime.UtcNow);
		}

        public static IEnumerable<LogSummary> GetLogSummary(this DbSet<Log> logs, int pastDays)
        {
            return logs.GetLogSummary(DateTime.UtcNow.Subtract(TimeSpan.FromDays(pastDays)), DateTime.UtcNow);
        }
	}
}