using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Chronicle.Logging.Business;
using Chronicle.Logging.Business.DataAccess;
using Chronicle.Logging.Business.Entities;
using Chronicle.Logging.Business.Reporting;
using Chronicle.Logging.Web.Areas.Logging.Models;
using MvcJqGrid;

namespace Chronicle.Logging.Web.Areas.Logging.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Logging/Report/

        public ActionResult Index()
        {
            IEnumerable<LogSummary> summary;
            IEnumerable<Log> logs;

            using (var db = new LogDbContext())
            {
                summary = db.Logs.GetLogSummary(7).ToList();
                logs = db.Logs.GetPage(1, 20).ToList();
            }

            return View(new ReportViewModel
                {
                    LogSummary = summary,
                    Logs = logs
                });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetReport(GridSettings gridSettings)
        {
            using (var db = new LogDbContext())
            {
            // create json data
            int pageIndex = gridSettings.PageIndex;
            int pageSize = gridSettings.PageSize;
            int totalRecords = db.Logs.Count();
            var totalPages = (int) Math.Ceiling(totalRecords/(float) pageSize);

            var startRow = (pageIndex - 1) * pageSize;
            var endRow = startRow + pageSize;
                    
				if (String.IsNullOrEmpty(gridSettings.SortColumn))
				{
					gridSettings.SortColumn = "Date";
				}

            var jsonData = new
                {
                    total = totalPages,
                    page = pageIndex,
                    records = totalRecords,
					rows = db.Logs.OrderByField(gridSettings.SortColumn, gridSettings.SortOrder == "asc").Skip(startRow).Take(gridSettings.PageSize).ToArray()
                };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}

}