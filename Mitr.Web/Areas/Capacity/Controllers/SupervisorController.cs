using Mitr.CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Areas.Capacity.Controllers
{
    public class SupervisorController : Controller
    {
        // GET: Capacity/Capacity 
        public ActionResult AssessmentRequest(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.Id = id;
            ViewBag.EmpID = clsApplicationSetting.GetSessionValue("EMPID");
            return View();
        }
    }
}