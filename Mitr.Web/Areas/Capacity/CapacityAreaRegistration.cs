using System.Web.Mvc;

namespace Mitr.Areas.Capacity
{
    public class CapacityAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Capacity";
            }
        }

       

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Capacity",
                "Capacity/{action}/{id}",
                new { controller = "Capacity", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
               "HR",
               "HR/{action}/{id}",
               new { controller = "HR", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
              "Supervisor",
              "Supervisor/{action}/{id}",
              new { controller = "Supervisor", action = "Index", id = UrlParameter.Optional }
          );
            context.MapRoute(
                  "Capacity_default",
                  "Capacity/{controller}/{action}/{id}",
                  new { action = "Index", id = UrlParameter.Optional }
              );

        }
    }
}