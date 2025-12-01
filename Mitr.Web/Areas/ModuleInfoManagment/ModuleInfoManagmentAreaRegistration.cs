using System.Web.Mvc;

namespace Mitr.Areas.ModuleInfoManagment
{
    public class ModuleInfoManagmentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ModuleInfoManagment";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ModuleInfoManagment",
                "ModuleInfoManagment/{action}/{id}",
                new { controller = "ModuleInfoManagment", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "ModuleInfoManagment_default",
                "ModuleInfoManagment/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }

}