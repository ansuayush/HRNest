using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class MPRController : Controller
    {
        IMPRHelper MPR;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        public MPRController()
        {
            getResponse = new GetResponse();
            MPR = new MPRModal();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult Section(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        public ActionResult _SectionList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MPR.Section.SecList> Modal = new List<MPR.Section.SecList>();
            Modal = MPR.GetMPRSecList(0);
            return PartialView(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _SectionAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MPRSID = GetQueryString[2];
            long MPRSID = 0;
            long.TryParse(ViewBag.MPRSID, out MPRSID);
            MPR.Section.SecAdd Modal = new MPR.Section.SecAdd();
            if (MPRSID > 0)
            {
                Modal = MPR.GetMPRSec(MPRSID);
            }

            return PartialView(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _SectionAdd(string src, MPR.Section.SecAdd Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "MPR Section not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    CommandResult Result = Common_SPU.fnSetMPRSec(Modal.MPRSID, Modal.SecName, Modal.SecDesc, Modal.Priority, 1);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                }
                if (PostResult.Status)
                {

                    PostResult.ViewAsString = RenderRazorViewToString("_SectionList", MPR.GetMPRSecList(0));
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult SubSection(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        public ActionResult _SubSectionList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MPR.SubSection.SubSecList> Modal = new List<MPR.SubSection.SubSecList>();
            Modal = MPR.GetMPRSubSecList(0);
            return PartialView(Modal);
        }

        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _SubSectionAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MPRSubSID = GetQueryString[2];
            long MPRSubSID = 0;
            long.TryParse(ViewBag.MPRSubSID, out MPRSubSID);
            MPR.SubSection.SubSecAdd Modal = new MPR.SubSection.SubSecAdd();
            ViewBag.MPRSecList = MPR.GetMPRSecList(0, "1");
            if (MPRSubSID > 0)
            {
                Modal = MPR.GetMPRSubSec(MPRSubSID);
            }
            return PartialView(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _SubSectionAdd(string src, MPR.SubSection.SubSecAdd Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.SuccessMessage = "MPR Sub Section not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.NoOfCol == 2 && string.IsNullOrEmpty(Modal.ColText2))
            {
                PostResult.SuccessMessage = "Colomn 2 Text can't be blank";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (Modal.NoOfCol == 3 && string.IsNullOrEmpty(Modal.ColText3))
            {
                PostResult.SuccessMessage = "Colomn 3 Text can't be blank";
                PostResult.SuccessMessage = "";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    CommandResult Result = Common_SPU.fnSetMPR_SubSec(Modal.MPRSubSID, Modal.SubSecName, Modal.MPRSID, Modal.NoOfCol,
                        Modal.ColText1, Modal.ColDataType1, (string.IsNullOrEmpty(Modal.ColSuffix1) ? "No" : "Yes"),
                        Modal.ColText2, Modal.ColDataType2, (string.IsNullOrEmpty(Modal.ColSuffix2) ? "No" : "Yes"),
                        Modal.ColText3, Modal.ColDataType3, (string.IsNullOrEmpty(Modal.ColSuffix3) ? "No" : "Yes"),
                        Modal.Priority, 1);

                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                }
                if (PostResult.Status)
                {
                    PostResult.ViewAsString = RenderRazorViewToString("_SubSectionList", MPR.GetMPRSubSecList(0));
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Indicator(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        public ActionResult _IndicatorList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MPR.Indicator.IndicatorList> Modal = new List<MPR.Indicator.IndicatorList>();
            Modal = MPR.GetMPRIndicatorList(0);
            return PartialView(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _IndicatorAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.IndicatorID = GetQueryString[2];
            long IndicatorID = 0;
            long.TryParse(ViewBag.IndicatorID, out IndicatorID);
            MPR.Indicator.IndicatorAdd Modal = new MPR.Indicator.IndicatorAdd();
            ViewBag.GetMPRSubSecList = MPR.GetMPRSubSecList(0, "1");

            if (IndicatorID > 0)
            {
                Modal = MPR.GetMPRIndicator(IndicatorID);
            }
            return PartialView(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _IndicatorAdd(string src, MPR.Indicator.IndicatorAdd Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.SuccessMessage = "MPR Indicator not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.NoOfCol == 2 && string.IsNullOrEmpty(Modal.ColText2))
            {
                PostResult.SuccessMessage = "Colomn 2 Text can't be blank";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (Modal.NoOfCol == 3 && string.IsNullOrEmpty(Modal.ColText3))
            {
                PostResult.SuccessMessage = "Colomn 3 Text can't be blank";
                PostResult.SuccessMessage = "";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    CommandResult Result = Common_SPU.fnSetMPR_Indicator(Modal.IndicatorID, Modal.MPRSubSecID, Modal.IndicatorName, Modal.AnswerIs, Modal.NoOfCol,
                        Modal.ColText1, Modal.ColDataType1, (string.IsNullOrEmpty(Modal.ColSuffix1) ? "No" : "Yes"),
                        Modal.ColText2, Modal.ColDataType2, (string.IsNullOrEmpty(Modal.ColSuffix2) ? "No" : "Yes"),
                        Modal.ColText3, Modal.ColDataType3, (string.IsNullOrEmpty(Modal.ColSuffix3) ? "No" : "Yes"), (string.IsNullOrEmpty(Modal.IsOrganisational) ? "No" : "Yes"),
                        Modal.Priority, 1);

                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                }
                if (PostResult.Status)
                {
                    PostResult.ViewAsString = RenderRazorViewToString("_IndicatorList", MPR.GetMPRIndicatorList(0));
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult MPRList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MPR.MPRList> Modal = new List<MPR.MPRList>();
            Modal = MPR.GetMPRList();
            return View(Modal);
        }

        public ActionResult CreateMPR(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MPRID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.MPRID, out ID);
            MPR.CreateMPR Modal = new MPR.CreateMPR();
            Modal = MPR.GetCreateMPR(ID);
            return View(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult CreateMPR(string src, MPR.CreateMPR Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "MPR Can't Saved";
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MPRID = GetQueryString[2];
            long MPRID = 0;
            long.TryParse(GetQueryString[2], out MPRID);

            if (Command == "Add")
            {
                if (Modal.CreateMPRDetails != null)
                {
                    foreach (var item in Modal.CreateMPRDetails)
                    {
                        if (!string.IsNullOrEmpty(item.ChkIsActive) && (item.ApproverID == 0 || item.Executor1ID == null || item.Executor2ID == null))
                        {
                            PostResult.SuccessMessage = "Executor and Section Approver can't blank for indicator " + item.IndicatorName;
                            ModelState.AddModelError("MPRName", PostResult.SuccessMessage);
                            break;
                        }
                    }
                }
                if (ModelState.IsValid)
                {

                    PostResult = Common_SPU.fnSetMPR(MPRID, Modal.MPRCode, Modal.MPRName, Modal.MPRDate, Modal.Version, Modal.InitiateDate, Modal.ProjectID, Modal.StateID, Modal.ApproverLevel1, Modal.ApproverLevel2, Modal.Priority, 1);

                    if (PostResult.ID > 0)
                    {
                        int Count = 0;
                        foreach (var item in Modal.CreateMPRDetails)
                        {

                            Count++;
                            Common_SPU.fnSetMPRDet(PostResult.ID, Count, item.DocType, item.IndicatorID,
                                item.Executor1ID ?? 0, item.Executor2ID ?? 0, item.ApproverID ?? 0, item.Priority, (string.IsNullOrEmpty(item.ChkIsActive) ? 0 : 1));

                        }
                    }
                    if (PostResult.Status)
                    {
                        Common_SPU.fnAutoCreateSMPR();
                        PostResult.RedirectURL = "/MPR/MPRList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/MPR/MPRList*0");

                    }


                }
            }
            else if (Command == "Amend")
            {
                PostResult = Common_SPU.fnSetMPRAmend(MPRID);
                if (PostResult.Status)
                {
                    PostResult.RedirectURL = "/MPR/CreateMPR?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/MPR/CreateMPR*" + PostResult.ID);
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrganisationalTargets(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MPR.Targets.TargetsList> List = new List<MPR.Targets.TargetsList>();
            List = MPR.GetMPRTargetsList(0, "Org");
            return View(List);
        }
        public ActionResult ProjectTargets(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FinYearList = MPR.GetFinYearList();
            MPR.Targets.TargetAction Obj = new MPR.Targets.TargetAction();
            return View(Obj);
        }
        public ActionResult _ProjectTargets(string src, MPR.Targets.TargetAction Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MPR.Targets.TargetsList> List = new List<MPR.Targets.TargetsList>();
            List = MPR.GetMPRTargetsList(Modal.FinID, "Pro");
            if (Modal.Approve == 1)
            {
                List = List.Where(x => x.MPRTargetID > 0).ToList();
            }
            else
            {
                List = List.Where(x => x.MPRTargetID == 0).ToList();
            }
            return PartialView(List);
        }

        public ActionResult TargetsSetting(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MPR.Targets.addTargetSetting Modal = new MPR.Targets.addTargetSetting();
            long FINID = 0, ProjectID = 0;
            if (GetQueryString.Length > 2)
            {
                long.TryParse(GetQueryString[2], out FINID);
            }
            if (GetQueryString.Length > 3)
            {
                long.TryParse(GetQueryString[3], out ProjectID);
            }
            ViewBag.FINID = FINID;
            ViewBag.ProjectID = ProjectID;
            if (ProjectID == 0)
            {
                Modal = MPR.AddTargetSetting(FINID, ProjectID);
            }
            else
            {
                Modal = MPR.AddTargetSetting(FINID, ProjectID);
            }
            return View(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult TargetsSetting(string src, MPR.Targets.addTargetSetting Modal, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long FINID = 0, ProjectID = 0;
            if (GetQueryString.Length > 2)
            {
                long.TryParse(GetQueryString[2], out FINID);
            }
            if (GetQueryString.Length > 3)
            {
                long.TryParse(GetQueryString[3], out ProjectID);
            }
            ViewBag.FINID = FINID;
            ViewBag.ProjectID = ProjectID;
            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    CommandResult Result = Common_SPU.fnSetMPRTarget(FINID, Modal.Quarter, ProjectID, 0, 1);

                    if (Result.ID > 0)
                    {
                        for (int i = 0; i < Modal.TargetSetting.Count; i++)
                        {
                            Common_SPU.fnSetMPRTargetDet(Result.ID, Modal.TargetSetting[i].IndicatorID, Modal.TargetSetting[i].ColVal1, Modal.TargetSetting[i].ColVal2, Modal.TargetSetting[i].ColVal3, 0, 1);
                        }
                    }

                    TempData["Success"] = (Result.Status ? "Y" : "N");
                    TempData["SuccessMsg"] = Result.SuccessMessage;
                }
                if (ProjectID == 0)
                {
                    return RedirectToAction("OrganisationalTargets", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/MPR/OrganisationalTargets") });
                }
                else
                {

                    return RedirectToAction("ProjectTargets", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/MPR/ProjectTargets") });
                }
            }
            else
            {
                return View(Modal);
            }

        }


        public ActionResult MPRDashboard(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MPRDashboard List = new MPRDashboard();
            List = MPR.MPRDashboard();
            List.Month = DateTime.Now.ToString("yyyy-MM");
            return View(List);
        }

        [HttpPost]
        public ActionResult _MPRDashboard(string src, MPRDashboard Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(Modal.Month, out MyDate);
            //List<MPRDashboard.List> List = new List<MPRDashboard.List>();
            //List = MPR.MPRDashboardList(Modal.StateID, Modal.ProjectID, MyDate.Month, MyDate.Year);
            //return PartialView(List);
            MPRDashboard.ListDashboard listDashboard = new MPRDashboard.ListDashboard();
            listDashboard = MPR.MPRDashboardListCountNew(Modal.StateID, Modal.ProjectID, MyDate.Month, MyDate.Year, "Header"); 
            return PartialView(listDashboard);
        }
        [HttpPost]
        public ActionResult _MPRDashboardListData(string src, MPRDashboard Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long StateID = 0, ProjectID=0;
            
            long.TryParse(GetQueryString[3], out StateID);
            long.TryParse(GetQueryString[4], out ProjectID);
            DateTime MyDate;
            DateTime.TryParse(GetQueryString[2], out MyDate);
            List<MPRDashboard.List> List = new List<MPRDashboard.List>();
            List = MPR.MPRDashboardListNew(StateID, ProjectID, MyDate.Month, MyDate.Year, Convert.ToString(GetQueryString[5]));
            ViewBag.Type = Convert.ToString(GetQueryString[5]);
            return PartialView(List);
        }

        public ActionResult SMPRApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            SMPR.Action Modal = new SMPR.Action();
            Modal.Submitted = "0";
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            ViewBag.MenuID = GetQueryString[0];
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _SMPRApproval(string src, SMPR.Action Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(Modal.Month, out MyDate);
            List<SMPR.List> List = new List<SMPR.List>();
            List = MPR.GetSMPRList(Modal.Submitted, MyDate.Month, MyDate.Year, "Section Approval");
            return PartialView(List);
        }

        public ActionResult SMPRList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            SMPR.Action Modal = new SMPR.Action();
            Modal.Submitted = "0";
            ViewBag.MenuID = GetQueryString[0];
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _SMPRList(string src, SMPR.Action Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(Modal.Month, out MyDate);
            List<SMPR.List> List = new List<SMPR.List>();
            List = MPR.GetSMPRList(Modal.Submitted, MyDate.Month, MyDate.Year);
            return PartialView(List);
        }

        public ActionResult SMPRAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long ID = 0;
            long.TryParse(GetQueryString[2], out ID);
            ViewBag.SMPRID = ID;
            SMPR.SMPREntry List = new SMPR.SMPREntry();
            List = MPR.GetSMPREntry(ID);
            return View(List);
        }
        [HttpPost]
        public ActionResult SMPRAdd(string src, SMPR.SMPREntry Modal, string Command)
        {
            CommandResult Result = new CommandResult();
            Result.SuccessMessage = "SMPR Not saved";
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Submitted = 0;
            long SMPRID = 0;
            long.TryParse(GetQueryString[2], out SMPRID);
            ViewBag.SMPRID = SMPRID;
            try
            {
                if (Command == "Save")
                {
                    ModelState.Clear();
                }

                Submitted = (Command == "Submit" ? 1 : 0);

                if (ModelState.IsValid)
                {
                    var SubSection = Modal.SMPRSubSectionList.GroupBy(x => x.SubSecID)
                         .Select(x => new
                         {
                             SubSecID = x.Key,
                             SecID = x.Select(ex => ex.SecID).FirstOrDefault(),
                             SecName = x.Select(ex => ex.SecName).FirstOrDefault(),
                             SubSecName = x.Select(ex => ex.SubSecName).FirstOrDefault(),
                             OverallReason = x.Select(ex => ex.OverallReason).FirstOrDefault(),

                         })
                         .ToList();

                    foreach (var item in SubSection)
                    {
                       
                        if (!string.IsNullOrEmpty(item.OverallReason))
                        {
                            Common_SPU.fnSetSMPRComments(0, SMPRID, item.OverallReason, "Executor", item.SubSecID, 1);
                        }
                        else
                        {
                            Common_SPU.fnSetSMPRComments(0, SMPRID, "", "Executor", item.SubSecID, 1);
                        }
                    }
                    for (int d = 0; d < Modal.SMPRDetList.Count; d++)
                    {
                        Result = Common_SPU.fnSetSMPR_Det(Modal.SMPRDetList[d].SMPRDetID, Modal.SMPRDetList[d].ExecuterVal1,
                            Modal.SMPRDetList[d].ExecuterVal2, Modal.SMPRDetList[d].ExecuterVal3, Submitted);
                    }
                    TempData["Success"] = (Result.Status ? "Y" : "N");
                    TempData["SuccessMsg"] = Result.SuccessMessage;
                    if (Result.Status)
                    {
                        if(Command == "Submit") { Common_SPU.fnCreateMail_SMPR(SMPRID, "Executor"); }
                      
                        return RedirectToAction("SMPRList", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/MPR/SMPRList") });
                    }

                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during ConsultantAdd. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterController", "MasterController", "");
            }
            TempData["Success"] = (Result.Status ? "Y" : "N");
            TempData["SuccessMsg"] = Result.SuccessMessage;
            Modal.SMPRCommentsList = MPR.CommentsList(SMPRID, 0);
            return View(Modal);
        }


        public ActionResult SetSMPRApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long ID = 0;
            long.TryParse(GetQueryString[2], out ID);
            ViewBag.SMPRID = ID;
            SMPR.SMPRApproval List = new SMPR.SMPRApproval();
            List = MPR.GetSMPRApproval(ID);
            return View(List);
        }
        [HttpPost]
        public ActionResult SetSMPRApproval(string src, SMPR.SMPRApproval Modal, string Command)
        {
            CommandResult Result = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long SMPRID = 0;
            long.TryParse(GetQueryString[2], out SMPRID);
            ViewBag.SMPRID = SMPRID;
            try
            {
                if (ModelState.IsValid)
                {
                    if (Command == "Approved")
                    {
                        Result = Common_SPU.fnSetSMPRApproval(SMPRID, 0, Modal.ApprovalRemarks, "SectionApprover");
                        Common_SPU.fnCreateMail_SMPR(SMPRID, "SectionApproval");
                    }
                    else if (Command == "Resubmit")
                    {
                        if (Modal.ExecuterDetailsList.Count > 0)
                        {
                            for (int i = 0; i < Modal.ExecuterDetailsList.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(Modal.ExecuterDetailsList[i].Chk))
                                {
                                    Result = Common_SPU.fnSetSMPRApproval(SMPRID, Modal.ExecuterDetailsList[i].ExecutorID, Modal.ExecuterDetailsList[i].Comment, "SectionResubmit");
                                }
                            }
                            Common_SPU.fnCreateMail_SMPR(SMPRID, "SectionApprovalResubmit");
                        }
                    }
                    TempData["Success"] = (Result.Status ? "Y" : "N");
                    TempData["SuccessMsg"] = Result.SuccessMessage;
                    if (Result.Status)
                    {
                        return RedirectToAction("SMPRApproval", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/MPR/SMPRApproval") });
                    }
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during ConsultantAdd. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterController", "MasterController", "");
            }
            return View(Modal);
        }



        public ActionResult ApproverLevel2(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            SMPR.Action Modal = new SMPR.Action();
            Modal.Submitted = "3";
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            ViewBag.MenuID = GetQueryString[0];
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ApproverLevel2(string src, SMPR.Action Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(Modal.Month, out MyDate);
            List<SMPR.List> List = new List<SMPR.List>();
            List = MPR.GetSMPRList(Modal.Submitted, MyDate.Month, MyDate.Year, "Level2Approval");
            return PartialView(List);
        }

        [HttpPost]
        public ActionResult _SMPRComments(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<SMPR.SMPRComments> List = new List<SMPR.SMPRComments>();
            long SMPRID = 0;
            long.TryParse(GetQueryString[2], out SMPRID);
            long SubSecID = 0;
            long.TryParse(GetQueryString[3], out SubSecID);
            List = MPR.CommentsList(SMPRID, SubSecID);
            return PartialView(List);
        }
        [HttpPost]
        public ActionResult _ApproverComments(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<SMPR.SMPRComments> List = new List<SMPR.SMPRComments>();
            long SMPRID = 0;
            long.TryParse(GetQueryString[2], out SMPRID);
            long SubSecID = 0;
            long.TryParse(GetQueryString[3], out SubSecID);
            List = MPR.CommentsList(SMPRID, SubSecID);
            return PartialView(List);
        }


        public ActionResult ApproverLevel1(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            SMPR.Action Modal = new SMPR.Action();
            Modal.Submitted = "1";
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            ViewBag.MenuID = GetQueryString[0];
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ApproverLevel1(string src, SMPR.Action Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(Modal.Month, out MyDate);
            List<SMPR.List> List = new List<SMPR.List>();
            List = MPR.GetSMPRList(Modal.Submitted, Convert.ToInt32(MyDate.Month), MyDate.Year, "Level1Approval");
            return PartialView(List);
        }

        public ActionResult Level1Approver(string src)
        {
             ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long ID = 0;
            long.TryParse(GetQueryString[2], out ID);
            ViewBag.SMPRID = ID;
            SMPR.LevelApprover List = new SMPR.LevelApprover();
            List = MPR.GetLevelApprover(ID);
            return View(List);
        }

        [HttpPost]
        public ActionResult Level1Approver(string src, SMPR.LevelApprover Modal, string Command)
        {
            CommandResult Result = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long SMPRID = 0;
            long.TryParse(GetQueryString[2], out SMPRID);
            ViewBag.SMPRID = SMPRID;
            try
            {
                if (ModelState.IsValid)
                {
                    if (Command == "Approved")
                    {
                        Result = Common_SPU.fnSetSMPRApproval(SMPRID, 0, Modal.Level1Remarks, "Level1Approver");
                        Common_SPU.fnCreateMail_SMPR(SMPRID, "ApprLevel1Approval");
                    }
                    else if (Command == "Resubmit")
                    {
                        if (Modal.ExecuterDetailsList.Count > 0)
                        {
                            for (int i = 0; i < Modal.ExecuterDetailsList.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(Modal.ExecuterDetailsList[i].Chk))
                                {
                                    Result = Common_SPU.fnSetSMPRApproval(SMPRID, Modal.ExecuterDetailsList[i].ExecutorID, Modal.ExecuterDetailsList[i].Comment, "Level1Resubmit");
                                }
                            }
                            Common_SPU.fnCreateMail_SMPR(SMPRID, "Level1Resubmit");
                        }
                    }
                    TempData["Success"] = (Result.Status ? "Y" : "N");
                    TempData["SuccessMsg"] = Result.SuccessMessage;
                    if (Result.Status)
                    {
                        return RedirectToAction("ApproverLevel1", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/MPR/ApproverLevel1") });
                    }
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during Level1Approver. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterController", "MasterController", "");
            }
            return View(Modal);
        }


        public ActionResult Level2Approver(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long ID = 0;
            long.TryParse(GetQueryString[2], out ID);
            ViewBag.SMPRID = ID;
            SMPR.LevelApprover2 List = new SMPR.LevelApprover2();
            List = MPR.GetLevel2Approver(ID);
            return View(List);
        }
        [HttpPost]
        public ActionResult Level2Approver(string src, SMPR.LevelApprover2 Modal, string Command)
        {
            CommandResult Result = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long SMPRID = 0;
            long.TryParse(GetQueryString[2], out SMPRID);
            ViewBag.SMPRID = SMPRID;
            try
            {
                if (ModelState.IsValid)
                {
                    if (Command == "Approved")
                    {
                        Result = Common_SPU.fnSetSMPRApproval(SMPRID, 0, Modal.Level2Remarks, "Level2Approver");
                        Common_SPU.fnCreateMail_SMPR(SMPRID, "ApprLevel2Approval");
                    }
                    else if (Command == "Resubmit")
                    {
                        if (Modal.ExecuterDetailsList.Count > 0)
                        {
                            for (int i = 0; i < Modal.ExecuterDetailsList.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(Modal.ExecuterDetailsList[i].Chk))
                                {
                                    Result = Common_SPU.fnSetSMPRApproval(SMPRID, Modal.ExecuterDetailsList[i].ExecutorID, Modal.ExecuterDetailsList[i].Comment, "Level2Resubmit");
                                }
                            }
                            Common_SPU.fnCreateMail_SMPR(SMPRID, "Level2Resubmit");
                        }
                    }
                    TempData["Success"] = (Result.Status ? "Y" : "N");
                    TempData["SuccessMsg"] = Result.SuccessMessage;
                    if (Result.Status)
                    {
                        return RedirectToAction("ApproverLevel2", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/MPR/ApproverLevel2") });
                    }
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during Level1Approver. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterController", "MasterController", "");
            }
            return View(Modal);
        }

        public ActionResult _SMPRStatus(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long SMPRID = 0;
            long.TryParse(GetQueryString[2], out SMPRID);
            string Operation = "";
            Operation = GetQueryString[3];
            ViewBag.SMPRID = SMPRID;
            ViewBag.Operation = Operation;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetSMPRStatus(SMPRID, Operation);
            return PartialView(ds);
        }

        [HttpPost]
        public ActionResult SetSMPRLock(string src, SMPR.SMPRLock Modal)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime dtdate = DateTime.Now;
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime.TryParse(Modal.dtdate, out dtdate);
                    CommandResult Result = Common_SPU.fnSetSMPRLock(Modal.SMPRID, Modal.Lock, dtdate, Modal.LockRemarks);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during SetSMPRLock. The query was executed :", ex.ToString(), "fnSetSMPRLock", "MPRController", "MPRController", "");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SMPRLockUnlockList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            LockUnlock List = new LockUnlock();
            List.Month = DateTime.Now.ToString("yyyy-MM");
            return View(List);

        }
        [HttpPost]
        public ActionResult _SMPRLockUnlockList(string src, LockUnlock Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(Modal.Month, out MyDate);
            List<LockUnlock.List> List = new List<LockUnlock.List>();
            List = MPR.LockUnlockList(MyDate);
            return PartialView(List);
        }

        public ActionResult _LockHistoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long SMPRID = 0;
            long.TryParse(GetQueryString[2], out SMPRID);
            ViewBag.SMPRID = SMPRID;
            List<LockUnlock.HistoryList> List = new List<LockUnlock.HistoryList>();
            List = MPR.SMPRLockHistoryList(SMPRID);
            return PartialView(List);
        }

        public ActionResult MPRSetting(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MPRSetting List = new MPRSetting();
            List = MPR.MPRSettingList(0);
            return View(List);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult MPRSetting(string src, MPRSetting Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.SuccessMessage = "MPR Setting not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (ModelState.IsValid)
            {

                CommandResult Result = Common_SPU.fnSetMPRSetting(Modal.MPRSettingID, Modal.MPRDueDate);

                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MPRReports(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MPRReports.Header Modal = new MPRReports.Header();
            Modal = MPR.MPRReportHeader();
            return View(Modal);
        }
        public ActionResult MPRNewReports(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MPRReports.Header Modal = new MPRReports.Header();
            Modal = MPR.MPRReportHeader();
            return View(Modal);
        }
        public ActionResult MPRReportExcel(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MPRReports.Header Modal = new MPRReports.Header();
            Modal = MPR.MPRReportHeader();
            return View(Modal);
        }

        [HttpGet]
        public  JsonResult GetDropDownLevel(string id)
        {
            List<MPRReports.SubSection> Modal = new List<MPRReports.SubSection>();
            
            if (!string.IsNullOrEmpty(id))
            {               
                Modal = MPR.GetMPR_Reports_SubHeader(id);
            }
            return new JsonResult { Data = Modal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
           
        }
        

        //[HttpPost]
        //public ActionResult _MPRReports(string src, MPRReports.Header Modal)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];

        //    //trying to made by HTML but not possible due to Data is not coming accordingly

        //    //if (!string.IsNullOrEmpty(Modal.ReportName))
        //    //{
        //    //    GetMPRResponse getMPR = new GetMPRResponse();
        //    //    getMPR.LoginID = LoginID;
        //    //    getMPR.IPAddress = IPAddress;
        //    //    getMPR.ProjectIDs = (Modal.ProjectID != null ? string.Join(",", Modal.ProjectID) : "");
        //    //    getMPR.SectionIDs = (Modal.SectionID != null ? string.Join(",", Modal.SectionID) : "");
        //    //    getMPR.StartDate = Modal.StartDate;
        //    //    getMPR.Enddate = Modal.EndDate;
        //    //    List<MPRReports.List> result = new List<MPRReports.List>();
        //    //    result = MPR.GetMPRReports(getMPR);
        //    //    return PartialView("_AchievementVSTarget", result);
        //    //}
        //    if (Modal.ReportName == "1")
        //    {
        //        DateTime StartDate, EndDate;
        //        string Sections = "";
        //        DateTime.TryParse(Modal.StartDate, out StartDate);
        //        DateTime.TryParse(Modal.EndDate, out EndDate);
        //        Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
        //        ViewBag.Data = Common_SPU.fnGetMPRReport_Ach_Vs_OrgTrg(StartDate, EndDate, Sections);
        //        return PartialView("_MPRReport_Ach_Vs_OrgTrg");
        //    }
        //    else if (Modal.ReportName == "2")
        //    {
        //        DateTime StartDate, EndDate;
        //        string Sections = "", Projects = "";
        //        DateTime.TryParse(Modal.StartDate, out StartDate);
        //        DateTime.TryParse(Modal.EndDate, out EndDate);
        //        Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
        //        Projects = (Modal.ProjectID != null ? string.Join(Modal.ProjectID.ToString(), ",") : "");
        //        ViewBag.Data = Common_SPU.fnGetMPRReport_Ach_Vs_Trg(StartDate, EndDate, Projects, Sections);
        //        return PartialView("_MPRReport_Ach_Vs_Trg");
        //    }
        //    else if (Modal.ReportName == "3")
        //    {
        //        DateTime StartDate, EndDate;
        //        string Sections = "", Projects = "", States = "";
        //        DateTime.TryParse(Modal.StartDate, out StartDate);
        //        DateTime.TryParse(Modal.EndDate, out EndDate);
        //        Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
        //        Projects = (Modal.ProjectID != null ? string.Join(Modal.ProjectID.ToString(), ",") : "");
        //        States = (Modal.StateID != null ? string.Join(Modal.StateID.ToString(), ",") : "");

        //        ViewBag.Data = Common_SPU.fnGetMPRReport_PS_Achv(StartDate, EndDate, Projects, States, Sections);
        //        return PartialView("_MPRReport_PS_Achv");
        //    }
        //    else if (Modal.ReportName == "4")
        //    {
        //        DateTime StartDate, EndDate;
        //        string Sections = "", Projects = "", States = "";
        //        DateTime.TryParse(Modal.StartDate, out StartDate);
        //        DateTime.TryParse(Modal.EndDate, out EndDate);
        //        Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
        //        Projects = (Modal.ProjectID != null ? string.Join(Modal.ProjectID.ToString(), ",") : "");
        //        States = (Modal.StateID != null ? string.Join(Modal.StateID.ToString(), ",") : "");

        //        ViewBag.Data = Common_SPU.fnGetMPRReport_PS_Achv(StartDate, EndDate, Projects, States, Sections);
        //        return PartialView("_MPRReport_SP_Achv");
        //    }
        //    else
        //    {
        //        return PartialView("_NoRecordsFound");
        //    }

        //}
        [HttpPost]
        public ActionResult _MPRReports(string src, MPRReports.Header Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.ReportName == "1")
            {
                DateTime StartDate, EndDate;
                string Sections = "";
                DateTime.TryParse(Modal.StartDate, out StartDate);
                DateTime.TryParse(Modal.EndDate, out EndDate);
                Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
                ViewBag.Data = Common_SPU.fnGetMPRReport_Ach_Vs_OrgTrg(StartDate, EndDate, Sections);
                return PartialView("_MPRReport_Ach_Vs_OrgTrg");
            }
            else if (Modal.ReportName == "2")
            {
                DateTime StartDate, EndDate;
                string Sections = "", Projects = "";
                DateTime.TryParse(Modal.StartDate, out StartDate);
                DateTime.TryParse(Modal.EndDate, out EndDate);
                Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
                Projects = (Modal.ProjectID != null ? string.Join(Modal.ProjectID.ToString(), ",") : "");
                ViewBag.Data = Common_SPU.fnGetMPRReport_Ach_Vs_Trg(StartDate, EndDate, Projects, Sections);
                return PartialView("_MPRReport_Ach_Vs_Trg");
            }
            else if (Modal.ReportName == "3")
            {
                //DateTime StartDate, EndDate;
                //string Sections = "", Projects = "", States = "";
                //DateTime.TryParse(Modal.StartDate, out StartDate);
                //DateTime.TryParse(Modal.EndDate, out EndDate);
                //Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
                //Projects = (Modal.ProjectID != null ? string.Join(Modal.ProjectID.ToString(), ",") : "");
                //States = (Modal.StateID != null ? string.Join(Modal.StateID.ToString(), ",") : "");
                //ViewBag.Data = Common_SPU.fnGetMPRReport_PS_Achv(StartDate, EndDate, Projects, States, Sections);
                //return PartialView("_MPRStateProjectReport");                
                GetMPRResponse getMPR = new GetMPRResponse();
                getMPR.LoginID = LoginID;
                getMPR.IPAddress = IPAddress;
                getMPR.ProjectIDs = (Modal.ProjectID != null ? string.Join(",", Modal.ProjectID) : "");
                getMPR.SectionIDs = (Modal.SectionID != null ? string.Join(",", Modal.SectionID) : "");

                getMPR.StartDate = Modal.StartDate;
                getMPR.Enddate = Modal.EndDate;
                List<MPRReports.AchievementList> result = new List<MPRReports.AchievementList>();
                result = MPR.GetStateProjectAchievementReports(getMPR);
                return PartialView("_MPRStateProjectReport", result);
            }
            else if (Modal.ReportName == "4")
            {
                //DateTime StartDate, EndDate;
                //string Sections = "", Projects = "", States = "";
                //DateTime.TryParse(Modal.StartDate, out StartDate);
                //DateTime.TryParse(Modal.EndDate, out EndDate);
                //Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
                //Projects = (Modal.ProjectID != null ? string.Join(Modal.ProjectID.ToString(), ",") : "");
                //States = (Modal.StateID != null ? string.Join(Modal.StateID.ToString(), ",") : "");

                //ViewBag.Data = Common_SPU.fnGetMPRReport_PS_Achv(StartDate, EndDate, Projects, States, Sections);
                //return PartialView("_MPRReport_SP_Achv");
                GetMPRResponse getMPR = new GetMPRResponse();
                getMPR.LoginID = LoginID;
                getMPR.IPAddress = IPAddress;
                getMPR.ProjectIDs = (Modal.ProjectID != null ? string.Join(",", Modal.ProjectID) : "");
                getMPR.SectionIDs = (Modal.SectionID != null ? string.Join(",", Modal.SectionID) : "");

                getMPR.StartDate = Modal.StartDate;
                getMPR.Enddate = Modal.EndDate;
                List<MPRReports.AchievementList> result = new List<MPRReports.AchievementList>();
                result = MPR.GetStateProjectAchievementReports(getMPR);
                return PartialView("_MPRProjectStateReport", result);
            }

            else
            {
                return PartialView("_NoRecordsFound");
            }

        }


        [HttpPost]
        public ActionResult _MPRNewReports(string src, MPRReports.Header Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.ReportName == "1")
            {
                DateTime StartDate, EndDate;
                string Sections = "";
                DateTime.TryParse(Modal.StartDate, out StartDate);
                DateTime.TryParse(Modal.EndDate, out EndDate);
                Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
                ViewBag.Data = Common_SPU.fnGetMPRReport_Ach_Vs_OrgTrg(StartDate, EndDate, Sections);
                return PartialView("_MPRReport_Ach_Vs_OrgTrg");
            }
            else if (Modal.ReportName == "2")
            {
                DateTime StartDate, EndDate;
                string Sections = "", Projects = "";
                DateTime.TryParse(Modal.StartDate, out StartDate);
                DateTime.TryParse(Modal.EndDate, out EndDate);
                Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
                Projects = (Modal.ProjectID != null ? string.Join(Modal.ProjectID.ToString(), ",") : "");
                ViewBag.Data = Common_SPU.fnGetMPRReport_Ach_Vs_Trg(StartDate, EndDate, Projects, Sections);
                return PartialView("_MPRReport_Ach_Vs_Trg");
            }
            else if (Modal.ReportName == "3")
            {
                //DateTime StartDate, EndDate;
                //string Sections = "", Projects = "", States = "";
                //DateTime.TryParse(Modal.StartDate, out StartDate);
                //DateTime.TryParse(Modal.EndDate, out EndDate);
                //Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
                //Projects = (Modal.ProjectID != null ? string.Join(Modal.ProjectID.ToString(), ",") : "");
                //States = (Modal.StateID != null ? string.Join(Modal.StateID.ToString(), ",") : "");
                //ViewBag.Data = Common_SPU.fnGetMPRReport_PS_Achv(StartDate, EndDate, Projects, States, Sections);
                //return PartialView("_MPRStateProjectReport");                
                GetMPRResponse getMPR = new GetMPRResponse();
                getMPR.LoginID = LoginID;
                getMPR.IPAddress = IPAddress;
                getMPR.ProjectIDs = (Modal.ProjectID != null ? string.Join(",", Modal.ProjectID) : "");
                getMPR.SectionIDs = (Modal.SectionID != null ? string.Join(",", Modal.SectionID) : "");
                getMPR.SubSectionIDs= (Modal.MPRSubSID != null ? string.Join(",", Modal.MPRSubSID) : "");
                getMPR.StateIDs = (Modal.StateID != null ? string.Join(",", Modal.StateID) : "");
                getMPR.StartDate = Modal.StartDate;
                getMPR.Enddate = Modal.EndDate;
                getMPR.MPRIDs = (Modal.MPRID != null ? string.Join(",", Modal.MPRID) : "");
                List<MPRReports.AchievementList> result = new List<MPRReports.AchievementList>();
                result = MPR.GetNewStateProjectAchievementReports(getMPR,1);
                return PartialView("_MPRStateProjectReport", result);
            }
            else if (Modal.ReportName == "4")
            {
                //DateTime StartDate, EndDate;
                //string Sections = "", Projects = "", States = "";
                //DateTime.TryParse(Modal.StartDate, out StartDate);
                //DateTime.TryParse(Modal.EndDate, out EndDate);
                //Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
                //Projects = (Modal.ProjectID != null ? string.Join(Modal.ProjectID.ToString(), ",") : "");
                //States = (Modal.StateID != null ? string.Join(Modal.StateID.ToString(), ",") : "");

                //ViewBag.Data = Common_SPU.fnGetMPRReport_PS_Achv(StartDate, EndDate, Projects, States, Sections);
                //return PartialView("_MPRReport_SP_Achv");
                GetMPRResponse getMPR = new GetMPRResponse();
                getMPR.LoginID = LoginID;
                getMPR.IPAddress = IPAddress;
                getMPR.ProjectIDs = (Modal.ProjectID != null ? string.Join(",", Modal.ProjectID) : "");
                getMPR.SectionIDs = (Modal.SectionID != null ? string.Join(",", Modal.SectionID) : "");
                getMPR.SubSectionIDs = (Modal.MPRSubSID != null ? string.Join(",", Modal.MPRSubSID) : "");
                getMPR.StateIDs = (Modal.StateID != null ? string.Join(",", Modal.StateID) : "");
                getMPR.StartDate = Modal.StartDate;
                getMPR.Enddate = Modal.EndDate;
                getMPR.MPRIDs = (Modal.MPRID != null ? string.Join(",", Modal.MPRID) : "");
                List<MPRReports.AchievementList> result = new List<MPRReports.AchievementList>();
                result = MPR.GetNewStateProjectAchievementReports(getMPR,1);
                return PartialView("_MPRProjectStateReport", result);
            }
            else if (Modal.ReportName == "5")
            {
                //DateTime StartDate, EndDate;
                //string Sections = "", Projects = "", States = "";
                //DateTime.TryParse(Modal.StartDate, out StartDate);
                //DateTime.TryParse(Modal.EndDate, out EndDate);
                //Sections = (Modal.SectionID != null ? string.Join(Modal.SectionID.ToString(), ",") : "");
                //Projects = (Modal.ProjectID != null ? string.Join(Modal.ProjectID.ToString(), ",") : "");
                //States = (Modal.StateID != null ? string.Join(Modal.StateID.ToString(), ",") : "");

                //ViewBag.Data = Common_SPU.fnGetMPRReport_PS_Achv(StartDate, EndDate, Projects, States, Sections);
                //return PartialView("_MPRReport_SP_Achv");
                GetMPRResponse getMPR = new GetMPRResponse();
                getMPR.LoginID = LoginID;
                getMPR.IPAddress = IPAddress;
                getMPR.ProjectIDs = (Modal.ProjectID != null ? string.Join(",", Modal.ProjectID) : "");
                getMPR.SectionIDs = (Modal.SectionID != null ? string.Join(",", Modal.SectionID) : "");
                getMPR.SubSectionIDs = (Modal.MPRSubSID != null ? string.Join(",", Modal.MPRSubSID) : "");
                getMPR.StateIDs = (Modal.StateID != null ? string.Join(",", Modal.StateID) : "");
                getMPR.StartDate = Modal.StartDate;
                getMPR.Enddate = Modal.EndDate;
                getMPR.IsInActiveRecords = Modal.IsInActiveRecords;
                getMPR.MPRIDs = (Modal.MPRID != null ? string.Join(",", Modal.MPRID) : "");
                List<MPRReports.AchievementList> result = new List<MPRReports.AchievementList>();
                result = MPR.GetNewStateProjectAchievementReports(getMPR,2);
                return PartialView("_MPRMonthWiseReport", result);
            }
            else
            {
                return PartialView("_NoRecordsFound");
            }

        }

        [HttpPost]
        public ActionResult _MPRNewExcelReports(string src, MPRReports.Header Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
           
                 
                GetMPRResponse getMPR = new GetMPRResponse();
                getMPR.StartDate = Modal.StartDate;
                getMPR.Enddate = Modal.EndDate;
              
                List<MPRReports.MPRReportExcel> result = new List<MPRReports.MPRReportExcel>();
                result = MPR.GetMPRReportExcel(getMPR);
                return PartialView("_MPRReportExcel", result);
           
             

        }
    }
}