using System;
using System.Configuration;
using System.Globalization;
using System.Text;
using Chronicle.Logging.Business.DataAccess;

namespace Chronicle.Logging.Business.Notifications
{
	public class LogNotifier : AdminMailer
	{
		/// <summary>
		///   Notify Admins of logging summary
		/// </summary>
		public void NotifyAdmins()
		{
			const string subjectlineDateFormat = "dd/MM/yyyy HH:mm";

			var log4NetPath = ConfigurationManager.AppSettings.Get("BaseLog4NetUrl");

			this.ToAddress = ConfigurationManager.AppSettings.Get("ErrorReporting.ToAddress");
			this.ToName = ConfigurationManager.AppSettings.Get("ErrorReporting.ToName");
			this.FromAddress = ConfigurationManager.AppSettings.Get("ErrorReporting.FromAddress");
			this.FromName = ConfigurationManager.AppSettings.Get("ErrorReporting.FromName");

			this.SubjectLine = string.Format(
				"Log Summary for period {0} - {1}",
				this.StartDate.ToString(subjectlineDateFormat),
				this.EndDate.ToString(subjectlineDateFormat));

			var summaryText = new StringBuilder();

			using (var db = new LogDbContext())
			{

				var logSummary = db.Logs.GetLogSummary(this.StartDate, this.EndDate);

				var totalFatal = 0;
				var totalError = 0;

				foreach (var summary in logSummary)
				{
					summaryText.AppendLine(summary.Category);
					summaryText.AppendLine();


					summaryText.AppendLine();
					summaryText.Append("Fatal: ");
					summaryText.Append(summary.Fatal.ToString(CultureInfo.InvariantCulture));
					summaryText.Append(" (" + log4NetPath + "?filter=" + summary.Category + "&levelfilter=" + "FATAL )");
					summaryText.AppendLine();

					summaryText.Append("Errors: ");
					summaryText.Append(summary.Error.ToString(CultureInfo.InvariantCulture));
					summaryText.Append(" (" + log4NetPath + "?filter=" + summary.Category + "&levelfilter=" + "ERROR )");
					summaryText.AppendLine();
					summaryText.AppendLine();

					totalFatal += summary.Fatal;
					totalError += summary.Error;
				}

				var summaryHeader = new StringBuilder();

				summaryHeader.AppendFormat("{0} Fatal, and {1} Errors received during this period.", totalFatal, totalError);
				summaryHeader.AppendLine();

				this.BodyText = summaryHeader.ToString() + "\n" + summaryText.ToString() + log4NetPath;

				if (totalFatal + totalError > 0)
				{
					this.Send();
				}
			}

		}

		/// <summary>
		///   Gets or sets StartDate.
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		///   Gets or sets EndDate.
		/// </summary>
		public DateTime EndDate { get; set; }
	}
}