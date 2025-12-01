using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    public class CryReportsController : Controller
    {
        // GET: CryReports
        public ActionResult RPT_TimeSheet(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now.AddMonths(-1);           
            return View();
        }
        public ActionResult RPT_ActivityLog(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now.AddMonths(-1);
            return View();
        }
    }
}