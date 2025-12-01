using Mitr.CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Areas.Capacity.Controllers
{
    public class CapacityController : Controller
    {
        //code chnages for capacity
        // GET: Capacity/Capacity
        public ActionResult TrainingType()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
            
        }
        public ActionResult TrainingListOld()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();

        }
        public ActionResult TrainingList()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();

        }
        public ActionResult AddTrainingType(int ? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.Id = id;
            return View();
        }
        
        public ActionResult TrainingCalendar(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.Id = id;
            return View();
        }

        public ActionResult TrainingRequest(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.EMPID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.Id = id;
            return View();
        }
        public ActionResult AddTrainingRequest(int? id)
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.EMPID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.Id = id;
            return View();
        }
        public ActionResult ViewTrainingRequest(string id)
        {
            ViewBag.ID = id;
            long ReqID;
            long.TryParse(ViewBag.ReqID, out ReqID);
            return View();
        }
        //public ActionResult _AddTrainingRequest(string src)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    ViewBag.ID = GetQueryString[1];
        //    long ReqID;
        //    long.TryParse(ViewBag.ReqID, out ReqID);
        //    return PartialView();
        //}
        public ActionResult TrainingUserList()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();

        }
       
    }
}