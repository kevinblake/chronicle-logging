Chronicle Logging
=================

Log4net wrapper including an Entity Framework Code First provider, with an ASP.NET MVC jqGrid-based reporting site to view logs.

Usage
=====

Add new loggers (if required) to Chronicle.Logging.Business.LoggingContext

Then log from your application, for example:
LoggingContext.General.Debug("This is a debug message");

Reporting site is viewed at: /Logging/

Notes
=====

* The reporting site is not yet secured.  It should be before any production rollout.
* Reporting site is not very pretty, but contains a summary and list of errors as a proof of concept
* TODO: Complete NuGet package for simpler installation
* TODO: Bundling and minification of js/css
* TODO: Complete styling of summary


