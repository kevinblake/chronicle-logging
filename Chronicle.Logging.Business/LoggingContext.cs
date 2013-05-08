using log4net;
using log4net.Config;

namespace Chronicle.Logging.Business
{
	public static class LoggingContext
	{
		public static ILog General
		{
			get
			{
				XmlConfigurator.Configure();

				return LogManager.GetLogger("General");
			}
		}

		public static ILog PublicSite
		{
			get
			{
				XmlConfigurator.Configure();

				return LogManager.GetLogger("PublicSite");
			}
		}

		public static ILog Service
		{
			get
			{
				XmlConfigurator.Configure();

				return LogManager.GetLogger("Service");
			}
		}

	}
}