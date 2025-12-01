using Mitr.App_Start;
using Mitr.CommonClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;


namespace Mitr
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //public string con = ClsCommon.connectionstring();
        protected void Application_Start()
        {
            //Register Syncfusion license
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(clsApplicationSetting.GetWebConfigValue("SyncfusionLicensing"));

          

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //here in Application Start we will start Sql Dependency
            // SqlDependency.Start(con);
            // Increase the JSON deserialization limits
            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.MaxDepth = 1024;

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception is HttpUnhandledException)
            {



            }
            //Exception exception = Server.GetLastError();
            //Response.Clear();
            //ClsCommon.LogError("Error during Application_Error. The query was executed :", exception.ToString(), "Global.asax", "Global.asax", "Global.asax", "");
            //Context.Response.Redirect("~/Account/PageNotFound");
        }
        //protected void Session_Start(object sender, EventArgs e)
        //{
        //    NotificationComponent NC = new NotificationComponent();
        //    NC.InvokeNotification();
        //}
        protected void Application_End()
        {
            //here we will stop Sql Dependency
            //SqlDependency.Stop(con);
        }
    }
}
