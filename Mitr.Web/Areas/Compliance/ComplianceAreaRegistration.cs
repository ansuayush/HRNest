using System.Web.Mvc;

namespace Mitr.Areas.Compliance
{
    public class ComplianceAreaRegistration : AreaRegistration 
    {
        
        public override string AreaName
        {
            get
            {
                return "Compliance";
            }
        }



        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Compliance",
                "Compliance/{action}/{id}",
                new { controller = "Compliance", action = "Index", id = UrlParameter.Optional }
            );
            
            context.MapRoute(
                  "Compliance_default",
                  "Compliance/{controller}/{action}/{id}",
                  new { action = "Index", id = UrlParameter.Optional }
              );
        }
    }
}