using System.Web.Mvc;

namespace Mitr.Areas.EmployeeSalary
{
    public class EmployeeSalaryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EmployeeSalary";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                    "EmployeeSalary",
                    "EmployeeSalary/{action}/{id}",
                    new { controller = "EmployeeSalary", action = "Index", id = UrlParameter.Optional }
                );
            context.MapRoute(
                "EmployeeSalary_default",
                "EmployeeSalary/{controller}/{action}/{id}",new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}