using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TaskManager.BusinessLogic.Services;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.Helpers;
using WebMatrix.WebData;


namespace TaskManager
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			ModelBinders.Binders.DefaultBinder = new PerpetuumSoft.Knockout.KnockoutModelBinder();
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

		    Bootstrapper.Initialise();

			//CultureInfo culture = new CultureInfo("ru-RU");
			//Thread.CurrentThread.CurrentCulture = culture;

			if (!WebSecurity.Initialized)
			{
				WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", true);
			}
			
		}

		
	}
}
