using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using PettiInn.DAL.Manager.Core;

namespace PettiInn.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        internal static IDALFactory DALFactory = null;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new AreaHttpControllerSelector(GlobalConfiguration.Configuration));

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            this.InitializeDAL();
        }

        private void InitializeDAL()
        {
            var kernel = new StandardKernel();
            var assembly = this.GetWebConfigAs<string>("DAL.Assembly");
            kernel.Load(Assembly.Load(assembly));
            DALFactory = kernel.Get<IDALFactory>();
        }
    }
}