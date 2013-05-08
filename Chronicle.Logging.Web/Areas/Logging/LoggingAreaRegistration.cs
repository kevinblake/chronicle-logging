using System.Web.Mvc;

namespace Chronicle.Logging.Web.Areas.Logging
{
    public class LoggingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Logging";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Logging_default",
                "Logging/{controller}/{action}/{id}",
                new { action = "Index", controller = "Report", id = UrlParameter.Optional }
            );
        }
    }
}
