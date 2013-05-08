using System.Collections.Generic;
using Chronicle.Logging.Business.Entities;
using Chronicle.Logging.Business.Reporting;

namespace Chronicle.Logging.Web.Areas.Logging.Models
{
    public class ReportViewModel
    {
        public IEnumerable<LogSummary> LogSummary { get; set; }

        public IEnumerable<Log> Logs { get; set; }
    }
}