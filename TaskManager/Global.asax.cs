using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TaskManager.Helpers;
using TaskManager.Models;
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

            //CultureInfo culture = new CultureInfo("ru-RU");
            //Thread.CurrentThread.CurrentCulture = culture;

            if (!Database.Exists("DefaultConnection"))
            {
                using (var context = new TaskManagerContext())
                {
                    try
                    {
                        ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                }
            }

            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", true);
            }
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredIfValidator));
        }

        
    }
}