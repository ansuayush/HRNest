using System.Web.Mvc;

namespace Mitr.Areas.GenericArea
{
    public class GenericAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GenericArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Generic",
                "Generic/{action}/{id}",
                new { controller = "Generic", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "GenericArea_default",
                "GenericArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}