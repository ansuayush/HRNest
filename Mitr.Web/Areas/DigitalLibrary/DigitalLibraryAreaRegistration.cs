using System.Web.Mvc;

namespace Mitr.Areas.DigitalLibrary
{
    public class DigitalLibraryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DigitalLibrary";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DigitalLibrary",
                "DigitalLibrary/{action}/{id}",
                new { controller = "DigitalLibrary", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "DigitalLibrary_default",
                "DigitalLibrary/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}