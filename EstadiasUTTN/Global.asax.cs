using EstadiasUTTN.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EstadiasUTTN
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            HttpException httpexception = exception as HttpException;
            RouteData route = new RouteData();
            route.Values.Add("controller", "Error");

            if(httpexception != null)
            {
                switch(httpexception.GetHttpCode())
                {
                    case 404:
                        route.Values.Add("action", "PageNotFound");
                        break;
                    case 500:
                        route.Values.Add("action", "InternalServerError");
                        break;
                    default:
                        route.Values.Add("action", "General");
                        break;
                }
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
            }
            IController errorcontroller = new ErrorController();
            errorcontroller.Execute(new RequestContext(new HttpContextWrapper(Context), route));
        }
    }
}
