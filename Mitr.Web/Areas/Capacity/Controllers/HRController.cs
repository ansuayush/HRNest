using Mitr.CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Areas.Capacity.Controllers
{
    public class HRController : Controller
    {
        // GET: Capacity/Capacity 
        public ActionResult TrainingAttendance(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.Id = id;
            return View();
        }
        public ActionResult AssessmentRequest(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.EmpID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.Id = id;
            return View();
        }
        public ActionResult FeedbackRequests(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.EmpID = clsApplicationSetting.GetSessionValue("EMPID");
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
            ViewBag.EmpID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.Id = id;
            return View();
        }
        public ActionResult ClubbedTrainingRequest(string src)
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            string[] GetQueryString =  clsApplicationSetting.DecryptQueryString(src);
            ViewBag.EmpID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.ReqIDs = GetQueryString[0];
            return View();
        }        
        public ActionResult DraftTrainingRequest(string id)
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.EmpID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.ID = id;
            long ReqID;
            long.TryParse(ViewBag.ReqID, out ReqID);
            return View();
        }
        public ActionResult ProcessTrainingRequest(string id)
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.EmpID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.ID = id;
            long ReqID;
            long.TryParse(ViewBag.ReqID, out ReqID);
            return View();
        }
        public ActionResult AssessmentRequestDetails(string id)
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.EmpID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.ID = id;
            long ReqID;
            long.TryParse(ViewBag.ReqID, out ReqID);
            return View();
        }
        public ActionResult AssessmentRequestDetailsView(string id)
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.EmpID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.ID = id;
            long ReqID;
            long.TryParse(ViewBag.ReqID, out ReqID);
            return View();
        }
        
        public ActionResult PendingUserViewTrainingRequest(string id)
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.ID = id;
            long ReqID;
            long.TryParse(ViewBag.ReqID, out ReqID);
            return View();
        }
        public ActionResult PendingHRViewTrainingRequest(string id)
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
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
        //    ViewBag.ReqID = GetQueryString[1];
        //    long ReqID;
        //    long.TryParse(ViewBag.ReqID, out ReqID);
        //    return PartialView();
        //}
        public ActionResult TrainingRequestList(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.Id = id;
            return View();
        }
        public ActionResult TrainingRequestCompleted(string id)
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.ID = id;
            long ReqID;
            long.TryParse(ViewBag.ReqID, out ReqID);
            return View();
        }
        
    }
}