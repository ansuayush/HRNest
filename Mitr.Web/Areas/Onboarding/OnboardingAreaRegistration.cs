using System.Web.Mvc;

namespace Mitr.Areas.Onboarding
{
    public class OnboardingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Onboarding";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Onboarding",
                "Onboarding/{action}/{id}",
                new { controller = "Onboarding", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
               "OnboardingEmployee",
               "OnboardingEmployee/{action}/{id}",
               new { controller = "OnboardingEmployee", action = "Index", id = UrlParameter.Optional }
           );
            context.MapRoute(
              "UserProcess",
              "UserProcess/{action}/{id}",
              new { controller = "UserProcess", action = "Index", id = UrlParameter.Optional }
          );
            context.MapRoute(
                "Onboarding_default",
                "Onboarding/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}