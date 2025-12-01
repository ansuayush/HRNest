using System.Web.Mvc;

namespace Mitr.Areas.Grievance
{
    public class GrievanceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Grievance";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                   "Grievance",
                   "Grievance/{action}/{id}",
                   new { controller = "Grievance", action = "Index", id = UrlParameter.Optional }
               );
            context.MapRoute(
                "Grievance_default",
                "Grievance/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}