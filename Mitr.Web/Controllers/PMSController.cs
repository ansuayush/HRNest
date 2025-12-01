using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class PMSController : Controller
    {
        IPMSHelper PMS;
        IExportHelper Export;
        IBudgetHelper Budget;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        public PMSController()
        {
            getResponse = new GetResponse();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            PMS = new PMSModal();
            Export = new ExportModal();
            Budget = new BudgetModel();
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

        public ActionResult CalenderYearList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PMS.CalenderYear.List> List = new List<PMS.CalenderYear.List>();
            List = PMS.GetCalenderYearList();
            return View(List);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CalenderYearAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long ID = 0;
            long.TryParse(GetQueryString[2], out ID);
            ViewBag.ID = ID;

            PMS.CalenderYear.Add List = new PMS.CalenderYear.Add();
            if (ID > 0)
            {
                List = PMS.GetCalenderYear(ID);
            }
            return PartialView(List);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CalenderYearAdd(string src, PMS.CalenderYear.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "Calender Year Saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long ID = 0;
            long.TryParse(GetQueryString[2], out ID);
            ViewBag.ID = ID;
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    CommandResult Result = Common_SPU.fnSetFinanYear(Modal.ID, Modal.year, Modal.from_date, Modal.to_date);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;

                    // Copy Last year PMS Master into new year  // Commented on 09-02-2022 Manish
                    //if (!string.IsNullOrEmpty(Modal.CopyNextYear))
                    //{
                    //    Result = Common_SPU.fnCopyPMSToNextYear(PostResult.ID);
                    //    //PostResult.Status = Result.Status;
                    //    PostResult.SuccessMessage = Result.SuccessMessage;
                    //    PostResult.StatusCode = Result.StatusCode;
                    //    //PostResult.ID = Result.ID;
                    //}
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult UOMList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            return View(Modal);
        }
        [HttpPost]

        public ActionResult _UOMList(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PMS.UOM.List> List = new List<PMS.UOM.List>();
            List = PMS.GetPMSUOMList(0, Modal.FYID);
            return PartialView(List);
        }

        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _UOMAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PMSUOMID = 0, FYID = 0;
            long.TryParse(GetQueryString[2], out PMSUOMID);
            long.TryParse(GetQueryString[3], out FYID);
            ViewBag.PMSUOMID = PMSUOMID;
            ViewBag.FYID = FYID;
            PMS.UOM.Add List = new PMS.UOM.Add();
            if (PMSUOMID > 0)
            {
                List = PMS.GetPMSUOM(PMSUOMID);
            }
            return PartialView(List);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _UOMAdd(string src, PMS.UOM.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "UOM not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PMSUOMID = 0, FYID = 0;
            long.TryParse(GetQueryString[2], out PMSUOMID);
            long.TryParse(GetQueryString[3], out FYID);
            ViewBag.PMSUOMID = PMSUOMID;
            ViewBag.FYID = FYID;
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    string Rating = (Modal.ChkAutoRating != null ? "Yes" : "No");
                    CommandResult Result = Common_SPU.fnSetPMSUOM(PMSUOMID, FYID, Modal.Name, Modal.Type, Rating, Modal.Priority, 1);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult KPASummary(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PMS.KPA.KPASummary> List = new List<PMS.KPA.KPASummary>();
            List = PMS.GetPMS_KPASummaryList();
            return View(List);
        }
        public ActionResult KPAList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long FYID = 0;
            if (GetQueryString.Length > 2)
            {
                long.TryParse(GetQueryString[2], out FYID);
            }
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            if (FYID == 0)
            {
                Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            }
            else
            {
                Modal.FYID = FYID;
            }
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _KPAList(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<PMS.KPA.List> List = new List<PMS.KPA.List>();
            List = PMS.GetPMS_KPAList(0, Modal.FYID, "", "0,1");
            return PartialView(List);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _KPAAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PMS_KPAID = 0, FYID = 0;
            int IsCopy = 0;
            long.TryParse(GetQueryString[2], out PMS_KPAID);
            long.TryParse(GetQueryString[3], out FYID);
            int.TryParse(GetQueryString[4], out IsCopy);
            ViewBag.PMS_KPAID = PMS_KPAID;
            ViewBag.FYID = FYID;
            PMS.KPA.Add List = new PMS.KPA.Add();
            if (PMS_KPAID > 0)
            {
                List = PMS.GetPMS_KPA(PMS_KPAID, FYID);
            }
            ViewBag.UOMList = PMS.GetPMSUOMList(0, FYID, "1");
            if (IsCopy == 1)
            {
                ViewBag.PMS_KPAID = 0;
                List.Approved = 0;
                List.PMS_KPAID = 0;

            }
            return PartialView(List);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _KPAAdd(string src, PMS.KPA.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "KPA not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PMS_KPAID = 0, FYID = 0;
            long.TryParse(GetQueryString[2], out PMS_KPAID);
            long.TryParse(GetQueryString[3], out FYID);
            ViewBag.PMS_KPAID = PMS_KPAID;
            ViewBag.FYID = FYID;
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    string Rating = (Modal.ChkAutoRating != null ? "Yes" : "No");
                    string probation = (Modal.ChkProbation != null ? "Yes" : "No");
                    CommandResult Result = Common_SPU.fnSetPMS_KPA(Modal.PMS_KPAID, FYID, Modal.Area, Modal.UOMID, Modal.IncType, Modal.IsMonitoring, Modal.IsMandatory, Rating, probation, Modal.Priority, 1);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult KPAApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            ViewBag.FYID = Modal.FYID;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _KPAApproval(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = Modal.FYID;
            List<PMS.KPA.List> List = new List<PMS.KPA.List>();
            List = PMS.GetPMS_KPAList(0, Modal.FYID, "Approval");
            return PartialView(List);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult ApproveKPA(string src, FormCollection Collection, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "KPA Approved not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = GetQueryString[2];
            long FYID = 0;
            long.TryParse(ViewBag.FYID, out FYID);
            string txtComments = Collection.GetValue("txtComments").AttemptedValue;
            if (string.IsNullOrEmpty(Command))
            {
                PostResult.SuccessMessage = "Invalid Button Click.";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }

            else if (Command == "Resubmit" && string.IsNullOrEmpty(txtComments))
            {
                PostResult.SuccessMessage = "Remarks Can't be blank.";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int Approved = (Command == "Resubmit" ? 2 : Command == "Approved" ? 1 : 0);
                CommandResult Result = Common_SPU.fnSetPMS_KPAApproval(FYID, Approved, txtComments);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult HierarchyPB(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            return View(Modal);
        }
        public ActionResult Hierarchy(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _Hierarchy(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PMS.Hierarchy> List = new List<PMS.Hierarchy>();
            List = PMS.GetPMS_HierarchyList(Modal.FYID);
            return PartialView(List);
        }
        [HttpPost]
        public ActionResult _HierarchyPB(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PMS.Hierarchy> List = new List<PMS.Hierarchy>();
            List = PMS.GetPMS_HierarchyListPB(Modal.FYID);
            return PartialView(List);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AuthorizeFilter(ActionFor = "W")]
        //public ActionResult SetHierarchy(string src, FormCollection Collection, string Command)
        //{
        //    PostResponse PostResult = new PostResponse();

        //    ViewBag.src = src;
        //    PostResult.SuccessMessage = "Hierarchy Approved not saved";
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];

        //    string SelectedRows = Collection.GetValue("HDNSelectedKPAID").AttemptedValue;
        //    if (string.IsNullOrEmpty(Command))
        //    {
        //        PostResult.SuccessMessage = "Invalid Button Click.";
        //        return Json(PostResult, JsonRequestBehavior.AllowGet);
        //    }
        //    else if (string.IsNullOrEmpty(SelectedRows))
        //    {
        //        PostResult.SuccessMessage = "Select atlest one Checkbox.";
        //        return Json(PostResult, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        int Approved = (Command == "Resubmit" ? 2 : Command == "Approved" ? 1 : 0);
        //        CommandResult Result = Common_SPU.fnSetPMS_HierarchyApproval(SelectedRows, Approved);
        //        PostResult.Status = Result.Status;
        //        PostResult.SuccessMessage = Result.SuccessMessage;
        //        PostResult.StatusCode = Result.StatusCode;
        //        PostResult.ID = Result.ID;
        //    }

        //    return Json(PostResult, JsonRequestBehavior.AllowGet);

        //}

        public ActionResult _ModifyHierarchy(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PMS_EID = 0;
            long.TryParse(GetQueryString[2], out PMS_EID);
            PMS.Hierarchy.Update Modal = new PMS.Hierarchy.Update();
            Modal = PMS.GetPMS_HierarchyUpdate(PMS_EID);
            return PartialView(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _ModifyHierarchy(string src, PMS.Hierarchy.Update Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "Hierarchy not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PMS_EID = 0;
            long.TryParse(GetQueryString[2], out PMS_EID);
            if (ModelState.IsValid)
            {
                string C = (Modal.SelectedCommentors != null ? string.Join(",", Modal.SelectedCommentors) : "");
                CommandResult Result = Common_SPU.fnSetPMS_Essential(PMS_EID, 0, "Hierarchy", C, Modal.Confirmer, "", "", "", "", "", "", "", "", "", "", 1, 1);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GoalSheetActivation(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _GoalSheetActivation(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<PMS.GoalSheetAct> List = new List<PMS.GoalSheetAct>();
            List = PMS.GetPMS_GoalSheetActList(Modal.FYID);
            return PartialView(List);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult GoalSheetApproval(string src, FormCollection Collection, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "Hierarchy Approved not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            string SelectedRows = Collection.GetValue("HDNSelectedKPAID").AttemptedValue;
            string StartDate = Collection.GetValue("txtStartDate").AttemptedValue;
            string EndDate = Collection.GetValue("txtEndDate").AttemptedValue;
            DateTime Start, End;
            DateTime.TryParse(StartDate, out Start);
            DateTime.TryParse(EndDate, out End);

            if (string.IsNullOrEmpty(Command))
            {
                PostResult.SuccessMessage = "Invalid Button Click.";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (string.IsNullOrEmpty(SelectedRows))
            {
                PostResult.SuccessMessage = "Select atlest one Checkbox.";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (Start.Year < 1900)
            {
                PostResult.SuccessMessage = "Invalid Start Date";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (End.Year < 1900)
            {
                PostResult.SuccessMessage = "Invalid End Date";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (End < Start)
            {
                PostResult.SuccessMessage = "Invalid Start and End Dates";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else
            {

                int Approved = (Command == "Resubmit" ? 2 : Command == "Approved" ? 1 : 0);
                CommandResult Result = Common_SPU.fnSetPMS_GoalSheetApproval(SelectedRows, Approved, Start, End);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult OSQuestion(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _OSQuestion(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<PMS.OSQuestion.List> List = new List<PMS.OSQuestion.List>();
            List = PMS.GetPMS_QuestionList(Modal.FYID);
            return PartialView(List);
        }

        public ActionResult _AddOSQuestion(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PMS_EID = 0, FYID = 0;
            long.TryParse(GetQueryString[2], out FYID);
            long.TryParse(GetQueryString[3], out PMS_EID);
            ViewBag.FYID = FYID;
            ViewBag.PMS_EID = PMS_EID;

            List<PMS.DesignationList> DList = new List<PMS.DesignationList>();
            DList = PMS.GetDesignationList(0, "1");
            ViewBag.DesignationList = DList;

            PMS.OSQuestion.Add obj = new PMS.OSQuestion.Add();
            if (PMS_EID > 0)
            {
                obj = PMS.GetPMS_Question(PMS_EID);
            }
            return PartialView(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AddOSQuestion(string src, PMS.OSQuestion.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "Question not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PMS_EID = 0, FYID = 0;
            long.TryParse(GetQueryString[2], out FYID);
            long.TryParse(GetQueryString[3], out PMS_EID);
            ViewBag.FYID = FYID;
            ViewBag.PMS_EID = PMS_EID;
            if (string.IsNullOrEmpty(Command))
            {
                PostResult.SuccessMessage = "Invalid Button Click.";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (Modal.QApplyDesignation == "Specific" && Modal.SelectedDesig == null)
            {
                PostResult.SuccessMessage = "Hey! You have to choose atleast one designation.";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (Modal.ChkValues == null)
            {
                PostResult.SuccessMessage = "Hey! You have to choose atleast one Question For.";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string DID = (Modal.SelectedDesig != null ? string.Join(",", Modal.SelectedDesig) : "");
                string ChkValues = (Modal.ChkValues != null ? string.Join(",", Modal.ChkValues) : "");
                CommandResult Result = Common_SPU.fnSetPMS_Essential(PMS_EID, FYID, "Question", "", 0, "", "", "", "", "", "", Modal.Question, Modal.QApplyDesignation, DID, ChkValues, Modal.Priority, 1);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult AppraisalActivation(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _AppraisalActivation(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<PMS.AppraisalAct> List = new List<PMS.AppraisalAct>();
            List = PMS.GetPMS_AppraisalActList(Modal.FYID);
            return PartialView(List);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult AppraisalActApproval(string src, FormCollection Collection, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "Appraisal Dates not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            string SelectedRows = Collection.GetValue("HDNSelectedKPAID").AttemptedValue;

            string EntryStart = Collection.GetValue("txtAppraisalEntryStart").AttemptedValue;
            string EntryEnd = Collection.GetValue("txtAppraisalEntryEnd").AttemptedValue;
            string ReviewStart = Collection.GetValue("txtAppraisalReviewStart").AttemptedValue;
            string ReviewEnd = Collection.GetValue("txtAppraisalReviewEnd").AttemptedValue;

            DateTime EStart, EEnd, RStart, REnd;
            DateTime.TryParse(EntryStart, out EStart);
            DateTime.TryParse(EntryEnd, out EEnd);
            DateTime.TryParse(ReviewStart, out RStart);
            DateTime.TryParse(ReviewEnd, out REnd);

            if (string.IsNullOrEmpty(Command))
            {
                PostResult.SuccessMessage = "Invalid Button Click.";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (string.IsNullOrEmpty(SelectedRows))
            {
                PostResult.SuccessMessage = "Select atlest one Checkbox.";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (EStart.Year < 1900)
            {
                PostResult.SuccessMessage = "Invalid Appraisal Entry Start Date";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (EEnd.Year < 1900)
            {
                PostResult.SuccessMessage = "Invalid Appraisal Entry End Date";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (EEnd < EStart)
            {
                PostResult.SuccessMessage = "Invalid Appraisal Entry Start and End Dates";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (RStart.Year < 1900)
            {
                PostResult.SuccessMessage = "Invalid Appraisal Review Start Date";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (REnd.Year < 1900)
            {
                PostResult.SuccessMessage = "Invalid Appraisal Review End Date";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (REnd < RStart)
            {
                PostResult.SuccessMessage = "Invalid Appraisal Review Start and End Dates";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else
            {

                int Approved = (Command == "Resubmit" ? 2 : Command == "Approved" ? 1 : 0);
                CommandResult Result = Common_SPU.fnSetPMS_AppraisalActDate(SelectedRows, Approved, EStart, EEnd, RStart, REnd);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MyGoalSheet(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            long FYID = 0;
            if (GetQueryString.Length > 2)
            {
                long.TryParse(GetQueryString[2], out FYID);
            }
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            if (FYID == 0)
            {
                Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            }
            else
            {
                Modal.FYID = FYID;
            }
            ViewBag.FYID = Modal.FYID.ToString();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _MyGoalSheet(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = Modal.FYID.ToString();
            PMS.GoalSheet.MySheet obj = new PMS.GoalSheet.MySheet();
            obj = PMS.GetMyGoalSheet(Modal.FYID, 0);
            return PartialView(obj);
        }
        public ActionResult _AddGoalSheet(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            long FYID = 0, PMS_GSDID = 0;
            ViewBag.FYID = GetQueryString[2];
            ViewBag.PMS_GSDID = GetQueryString[3];

            long.TryParse(ViewBag.FYID, out FYID);
            long.TryParse(ViewBag.PMS_GSDID, out PMS_GSDID);


            PMS.GoalSheet.Add obj = new PMS.GoalSheet.Add();
            ViewBag.KPAList = PMS.GetPMS_KPAList(0, FYID, "", "1").Where(x => x.Approved == 1).ToList();
            ViewBag.UOMList = PMS.GetPMSUOMList(0, FYID, "1");
            obj = PMS.GetPMS_GoalSheet_Det(PMS_GSDID);
            return PartialView(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AddGoalSheet(string src, PMS.GoalSheet.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "My Goal Sheet Entry not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long FYID = 0;
            ViewBag.FYID = GetQueryString[2];
            long.TryParse(ViewBag.FYID, out FYID);
            if (Modal.UOMType == "Number")
            {
                int checkcount = Modal.Target.ToString().Length;
                if (checkcount > 9)
                {
                    ModelState.AddModelError("Target", "Target not more then 9 number");
                    PostResult.SuccessMessage = "Target not more then 9 number";
                }
              
            }
            if (ModelState.IsValid)
            {
                CommandResult Result = Common_SPU.fnSetPMS_GoalSheet_Det(Modal.PMS_GSDID, FYID, Modal.KPAID, Modal.PIndicator, Modal.Target, Modal.DetRemarks, Modal.Priority, 1, Modal.UOMID);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeFilter(ActionFor = "W")]
        public ActionResult SubmitGoalSheet(string src, PMS.GoalSheet.MySheet Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "My Goal Sheet Entry not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.GoalSheetDet == null)
            {
                PostResult.SuccessMessage = "No KPA has entered, please add your KPA";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                CommandResult Result = Common_SPU.fnSetPMS_GoalSheet(Modal.FYID, Modal.Comment, 1, 1, Command);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult TeamGoalSheet(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _TeamGoalSheet(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = Modal.FYID.ToString();
            List<PMS.GoalSheet.TeamGoalSheet> obj = new List<PMS.GoalSheet.TeamGoalSheet>();
            obj = PMS.GetTeamGoalSheet(Modal.FYID, 0);
            return PartialView(obj);
        }

        public ActionResult _ViewGoalSheet(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PMS_GSID = GetQueryString[2];
            long PMS_GSID = 0;
            long.TryParse(ViewBag.PMS_GSID, out PMS_GSID);
            long FYID = 0;
            ViewBag.FYID = GetQueryString[3];
            long.TryParse(ViewBag.FYID, out FYID);

            PMS.GoalSheet.EMPGoalSheet obj = new PMS.GoalSheet.EMPGoalSheet();
            // FYID is GoalSheet Main ID 
            obj = PMS.GetEMPGoalSheet(PMS_GSID, "Appraiser");
            ViewBag.UOMList = PMS.GetPMSUOMList(0, FYID, "1");
            return PartialView(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SubmitTeamSheet(string src, PMS.GoalSheet.EMPGoalSheet Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.SuccessMessage = "My Team's Goal Sheet not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.GoalSheetDet == null)
            {
                PostResult.SuccessMessage = "Dnt have Goal Sheet";
                ModelState.AddModelError("Reason", PostResult.SuccessMessage);
            }
            else if (Command == "Approve" && Modal.GoalSheetDet.Sum(x => x.Weight) < 100 || Modal.GoalSheetDet.Sum(x => x.Weight) > 100)
            {
                PostResult.SuccessMessage = "Total weights should be in total of 100";
                ModelState.AddModelError("Reason", PostResult.SuccessMessage);
            }
       
            else if (Command == "Resubmit" && string.IsNullOrEmpty(Modal.Reason))
            {
                PostResult.SuccessMessage = "Reason can't be blank if you want to resubmit";
                ModelState.AddModelError("Reason", PostResult.SuccessMessage);
            }
            if (Command == "Approve")
            {
                for (int i = 0; i < Modal.GoalSheetDet.Count; i++)
                {
                    if (Modal.GoalSheetDet[i].Weight == 0)
                    {
                        PostResult.SuccessMessage = "Total weights should be in total of 100";
                        ModelState.AddModelError("Reason", PostResult.SuccessMessage);
                        break;
                    }
                  
                }
            }
            if (ModelState.IsValid)
            {
                for (int i = 0; i < Modal.GoalSheetDet.Count; i++)
                {
                    // Common_SPU.fnSetPMS_GoalSheet_Det(Modal.GoalSheetDet[i].PMS_GSDID, 0, 0, "", "", "", 0, 0, 0, Modal.GoalSheetDet[i].Weight, "Appraiser");
                    if (Command == "Resubmit")
                    {
                        Common_SPU.fnSetPMS_GoalSheet_Det(Modal.GoalSheetDet[i].PMS_GSDID, 0, 0, Modal.GoalSheetDet[i].RPIndicator, Modal.GoalSheetDet[i].RTarget, "", 0, 0, Modal.GoalSheetDet[i].UOMID, Modal.GoalSheetDet[i].Weight, "Appraiser");
                    }
                    else if (Command == "Approve")
                    {
                        Common_SPU.fnSetPMS_GoalSheet_Det(Modal.GoalSheetDet[i].PMS_GSDID, 0, 0, Modal.GoalSheetDet[i].RPIndicator, Modal.GoalSheetDet[i].RTarget, "", 0, 0, Modal.GoalSheetDet[i].UOMID, Modal.GoalSheetDet[i].Weight, "Appraiser");
                    }
                    else
                    {
                        Common_SPU.fnSetPMS_GoalSheet_Det(Modal.GoalSheetDet[i].PMS_GSDID, 0, 0, Modal.GoalSheetDet[i].PIndicator, Modal.GoalSheetDet[i].Target, "", 0, 0, Modal.GoalSheetDet[i].UOMID, Modal.GoalSheetDet[i].Weight, "Appraiser");
                    }
                      

                }
                int Approved = 0; string Reason = "";
                if (Command == "Resubmit")
                {
                    Approved = 2;
                    Reason = Modal.Reason;
                }
                else if (Command == "Approve")
                {
                    Approved = 1;
                }
                if (Approved != 0 && (Command== "Approve" || Command == "Resubmit" ))
                {
                    CommandResult Result = Common_SPU.fnSetPMS_GSApproval(Modal.PMS_GSID, Modal.Reason, "Appraiser", Approved);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                }
                else
                {
                    PostResult.Status = true;
                    PostResult.SuccessMessage = "Goal Sheet Saved Successfully";
                    PostResult.StatusCode = 1;
                    PostResult.ID = 1;
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GroupGoalSheet(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _GroupGoalSheet(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = Modal.FYID.ToString();
            List<PMS.GroupGoalSheet.List> list = new List<PMS.GroupGoalSheet.List>();
            list = PMS.GetGroupGoalSheet(Modal.FYID, 0);
            return PartialView(list);
        }
        public ActionResult _ViewGroupGoalSheet(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PMS_GSID = GetQueryString[2];
            long PMS_GSID = 0;
            long.TryParse(ViewBag.PMS_GSID, out PMS_GSID);
            PMS.GoalSheet.EMPGoalSheet obj = new PMS.GoalSheet.EMPGoalSheet();
            // FYID is GoalSheet Main ID 
            obj = PMS.GetEMPGoalSheet(PMS_GSID, "Confirmer");
            return PartialView(obj);
        }

        [HttpPost]

        public ActionResult SubmitGroupSheet(string src, PMS.GoalSheet.EMPGoalSheet Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.SuccessMessage = "Group Goal Sheet not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.GoalSheetDet == null)
            {
                PostResult.SuccessMessage = "Dnt have Goal Sheet";
                ModelState.AddModelError("Reason", PostResult.SuccessMessage);
            }
            else if (Command == "Resubmit" && string.IsNullOrEmpty(Modal.Reason))
            {
                PostResult.SuccessMessage = "Reason can't be blank if you want to resubmit";
                ModelState.AddModelError("Reason", PostResult.SuccessMessage);
            }
            if (ModelState.IsValid)
            {
                int Approved = 0; string Reason = "";
                if (Command == "Resubmit")
                {
                    Approved = 3;
                    Reason = Modal.Reason;
                }
                else if (Command == "Approve")
                {
                    Approved = 4;
                }
                if (Approved != 0)
                {
                    CommandResult Result = Common_SPU.fnSetPMS_GSApproval(Modal.PMS_GSID, Modal.Reason, "Confirmer", Approved);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult CommentRequests(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _CommentRequests(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = Modal.FYID.ToString();
            List<PMS.Feedback.List> list = new List<PMS.Feedback.List>();
            list = PMS.GetFeedbackList(Modal.FYID);
            return PartialView(list);
        }
        public ActionResult Evaluation(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            long FYID = 0, EMPID = 0;
            long.TryParse(ViewBag.FYID, out FYID);
            long.TryParse(ViewBag.EMPID, out EMPID);
            PMS.Feedback.Add obj = new PMS.Feedback.Add();
            obj = PMS.GetFeedback(FYID, EMPID);
            return View(obj);
        }
        [HttpPost]
        //[AuthorizeFilter(ActionFor = "W")]
        public ActionResult Evaluation(string src, PMS.Feedback.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.SuccessMessage = "Group Goal Sheet not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            long FYID = 0, EMPID = 0;
            long.TryParse(ViewBag.FYID, out FYID);
            long.TryParse(ViewBag.EMPID, out EMPID);
            if (Modal.QuestionsList == null)
            {
                PostResult.SuccessMessage = "Dnt have Questions";
                ModelState.AddModelError("Reason", PostResult.SuccessMessage);
            }
            else if (Command == "Submit" && Modal.QuestionsList.Any(x => string.IsNullOrEmpty(x.A)))
            {
                PostResult.SuccessMessage = "Answer can't be blank for any Question";
                ModelState.AddModelError("OverAllComment", PostResult.SuccessMessage);
            }
            if (Command == "Submit" && string.IsNullOrEmpty(Modal.OverAllRating))
            {
                PostResult.SuccessMessage = "Please select any of the rating";
                ModelState.AddModelError("OverAllComment", PostResult.SuccessMessage);
            }
            if (Command == "Submit" && string.IsNullOrEmpty(Modal.FinalComment))
            {
                PostResult.SuccessMessage = "Final Assessment is mandatory";
                ModelState.AddModelError("OverAllComment", PostResult.SuccessMessage);
            }
            if (ModelState.IsValid)
            {
                int Isdeleted = 2;
                if (Command == "Submit")
                {
                    Isdeleted = 0;
                }

                for (int i = 0; i < Modal.QuestionsList.Count; i++)
                {
                    CommandResult Result = Common_SPU.fnSetPMS_QA(FYID, EMPID, "Commenter", Modal.QuestionsList[i].Q, Modal.QuestionsList[i].A, Modal.FinalComment, Modal.OverAllRating, Isdeleted);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    PostResult.RedirectURL = "/PMS/Evaluation?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/Evaluation");

                }


            }
            if (Command != "Save")
                PostResult.RedirectURL = "/PMS/CommentRequests?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/CommentRequests");
            else
                PostResult.RedirectURL = "/PMS/Evaluation?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/Evaluation*" + FYID+"*"+EMPID);
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SelfAppraisal(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            ViewBag.FYID = Modal.FYID.ToString();
            return View(Modal);
        }
        [WhitespaceFilter]
        [HttpPost]

        public ActionResult _SelfAppraisal(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = Modal.FYID.ToString();
            PMS.SelfAppraisal obj = new PMS.SelfAppraisal();
            obj = PMS.GetSelfAppraisal(Modal.FYID);
            return PartialView(obj);
        }
        //public ActionResult PerformanceAppraisalReport(string src)
        //{

        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    PMS Modal = new PMS();
        //    List<PMS.FinList> FList = new List<PMS.FinList>();
        //    long EMPID = 0;
        //    long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
        //    FList = PMS.GetFinYearList();
        //    ViewBag.FinYearList = FList;
        //    //ViewBag.EmpList = CommonSpecial.GetAllEmployeeList();
        //    Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
        //    ViewBag.FYID = Modal.FYID.ToString();
        //    return View(Modal);
        //}
        //[HttpPost]
        //public ActionResult PerformanceAppraisalReport(PMS.SelfAppraisal Modal, string src, string Command)
        //{
        //    PostResponse PostResult = new PostResponse();
        //    PostResult.Status = false;
        //    PostResult.SuccessMessage = "";
        //    ViewBag.src = src;
        //    long EMPID = 0;
        //    long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    string Filename = "";
        //    if (ModelState.IsValid)
        //    {
        //        if (Command == "Download")
        //        {
        //            Filename = PMS.GetAppraisal_RPT(EMPID, Modal.FYID);
        //            PostResult.Status = true;
        //            PostResult.SuccessMessage = Filename;

        //        }
        //    }
        //    return Json(PostResult, JsonRequestBehavior.AllowGet);
        //    // return RedirectToAction("PerformanceAppraisalReport", "PMS", new { src = src });

        //}

        public ActionResult PerformanceAppraisalReport(string src)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.Status = false;
            PostResult.SuccessMessage = "";
            ViewBag.src = src;
            long EMPID = 0;

            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long FYID = 0;
            if (GetQueryString.Length < 3)
            {
                long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
                List<PMS.FinList> FList = new List<PMS.FinList>();
                var orderByResult = from s in PMS.GetFinYearList()
                                    orderby s.ID descending
                                    select s.ID;
                FYID = Convert.ToInt64(orderByResult.FirstOrDefault());

            }
            else
            {
                EMPID = Convert.ToInt32(GetQueryString[2].ToString());
                FYID = Convert.ToInt32(GetQueryString[3].ToString());
            }
            string Filename = "";
            if (ModelState.IsValid)
            {
                Filename = Export.GetAppraisal_RPT(EMPID, FYID);
                PostResult.Status = true;
                PostResult.SuccessMessage = Filename;

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]

        public ActionResult SetSelfAppraisal(string src, PMS.SelfAppraisal Modal, string Command)
       {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.SuccessMessage = "Self Appraisal not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            if (Modal.KPAL == null)
            {
                PostResult.SuccessMessage = "Dnt have KPA";
                ModelState.AddModelError("FYID", PostResult.SuccessMessage);
            }
            else if (Modal.KPAL != null && Modal.KPAL.Any(x => string.IsNullOrEmpty(x.Self_Achievement)) && Command != "Save")
            {
                PostResult.SuccessMessage = "Fill All Achievement";
                ModelState.AddModelError("FYID", PostResult.SuccessMessage);
                PostResult.StatusCode = 2;
            }

            else if (Modal.QAL != null && Command == "Submit" && Modal.QAL.Any(x => string.IsNullOrEmpty(x.Answer)))
            {
                PostResult.SuccessMessage = "Fill All Answer";
                ModelState.AddModelError("FYID", PostResult.SuccessMessage);
            }
            if (Modal.CommentsL != null && Command == "Approved")
            {
                for (int i = 0; i < Modal.CommentsL.Count; i++)
                {
                    if (Modal.CommentsL[i].Doctype == "Self Comment 2" && string.IsNullOrEmpty(Modal.CommentsL[i].Comment))
                    {
                        PostResult.SuccessMessage = "Final Response";
                        ModelState.AddModelError("FYID", PostResult.SuccessMessage);
                    }
                }
            }
            if (Modal.TrainingL != null && Modal.TrainingL.Any(x => string.IsNullOrEmpty(x.TrainingRemarks) && x.TrainingType == "Other"))
            {
                PostResult.SuccessMessage = "Enter Training Description";
                ModelState.AddModelError("FYID", PostResult.SuccessMessage);
            }
            if (Modal.AttachmentsL != null)
            {
                if (Modal.AttachmentsL.Any(x => string.IsNullOrEmpty(x.Descrip) && x.UploadFile != null))
                {
                    PostResult.SuccessMessage = "Enter Description of Document";
                    ModelState.AddModelError("FYID", PostResult.SuccessMessage);
                }
            }

            //if (!ModelState.IsValid)
            //{
            if (Command == "Approved")
            {
                long EMPID = 0;
                long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);

                CommandResult Result = Common_SPU.fnSetPMS_Appraisal(Modal.FYID, Modal.EMPID, 1, 1, Command);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
                PostResult.RedirectURL = "/PMS/SelfAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/SelfAppraisal");

                //Comment
                if (Modal.CommentsL != null)
                {
                    for (int i = 0; i < Modal.CommentsL.Count; i++)
                    {
                        Common_SPU.fnSetPMSComments(Modal.CommentsL[i].PMS_CommentID, Modal.FYID, Modal.EMPID, Modal.CommentsL[i].Comment, "Self", 1, PostResult.ID, Modal.Approved == 1 ? Modal.CommentsL[i].Doctype : Modal.CommentsL[i].TableName);
                    }
                }
                // add attachemt by shailendra
                if (Modal.AttachmentsL != null)
                {
                    for (int i = 0; i < Modal.AttachmentsL.Count; i++)
                    {
                        if (Modal.AttachmentsL[i].UploadFile != null)
                        {
                            var RvFile = clsApplicationSetting.ValidateFile(Modal.AttachmentsL[i].UploadFile);
                            if (RvFile.IsValid)
                            {
                                Modal.AttachmentsL[i].AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentsL[i].AttachmentID, Modal.AttachmentsL[i].FileName, RvFile.FileExt, Modal.AttachmentsL[i].Descrip, PostResult.ID.ToString(), "Appraisal");
                                if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentsL[i].AttachmentID + RvFile.FileExt)))
                                {
                                    System.IO.File.Delete("~/Attachments/" + Modal.AttachmentsL[i].AttachmentID + RvFile.FileExt);
                                }
                                Modal.AttachmentsL[i].UploadFile.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentsL[i].AttachmentID + RvFile.FileExt));
                            }
                        }
                        else
                        {
                            if (Modal.AttachmentsL[i].AttachmentID > 0)
                            {
                                Common_SPU.fnSetAttachments(Modal.AttachmentsL[i].AttachmentID, Modal.AttachmentsL[i].FileName, "", Modal.AttachmentsL[i].Descrip, PostResult.ID.ToString(), "Appraisal");
                            }
                        }
                    }
                }
                // Traninng 
                if (Modal.TrainingL != null)
                {
                    for (int i = 0; i < Modal.TrainingL.Count; i++)
                    {
                        if (Modal.TrainingL[i].TrainingTypeID != null)
                        {
                            if (Modal.TrainingL[i].Isdeleted == 0)
                            {
                                Common_SPU.fnSetPMS_Appraisal_det(Modal.TrainingL[i].PMS_ADID, PostResult.ID, "Training", Modal.TrainingL[i].TrainingTypeID ?? 0, Modal.TrainingL[i].TrainingType, Modal.TrainingL[i].TrainingRemarks, 0, 0, 0, "", "", "", "", "", "", "", "", 0, "", "", "", "");
                            }
                        }
                    }
                }

                Result = Common_SPU.fnSetPMS_AppraisalApproval(Modal.PMS_AID, 0, "Self", 0, "", "", 0, 0, 0);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
                PostResult.RedirectURL = "/PMS/SelfAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/SelfAppraisal");
            }
            else
            {
                try
                {
                    long EMPID = 0;
                    long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);

                    CommandResult Result = Common_SPU.fnSetPMS_Appraisal(Modal.FYID, Modal.EMPID, 1, 1, Command);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    PostResult.RedirectURL = "/PMS/SelfAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/SelfAppraisal");

                    // KPA set
                    if (Modal.KPAL != null)
                    {
                        for (int i = 0; i < Modal.KPAL.Count; i++)
                        {
                            Common_SPU.fnSetPMS_Appraisal_det(Modal.KPAL[i].PMS_ADID, PostResult.ID, "KPA", 0, "", "", Modal.KPAL[i].GoalSheetID, Modal.KPAL[i].GoalSheet_DetID,
                                Modal.KPAL[i].KPAID, Modal.KPAL[i].KPA_Area, Modal.KPAL[i].KPA_PIndicator, Modal.KPAL[i].kPA_Target, Modal.KPAL[i].KPA_Weight, Modal.KPAL[i].KPA_IncType, Modal.KPAL[i].KPA_IsMonitoring, Modal.KPAL[i].KPA_IsMandatory, Modal.KPAL[i].KPA_AutoRating,
                                Modal.KPAL[i].UOMID, Modal.KPAL[i].UOM_Name, Modal.KPAL[i].Self_Achievement, Modal.KPAL[i].Self_Comment, Modal.KPAL[i].KPA_TargetAchieved);
                        }
                    }
                    // Traninng 
                    if (Modal.TrainingL != null)
                    {
                        for (int i = 0; i < Modal.TrainingL.Count; i++)
                        {
                            if (Modal.TrainingL[i].TrainingTypeID != null)
                            {
                                if (Modal.TrainingL[i].Isdeleted == 0)
                                {
                                    Common_SPU.fnSetPMS_Appraisal_det(Modal.TrainingL[i].PMS_ADID, PostResult.ID, "Training", Modal.TrainingL[i].TrainingTypeID ?? 0, Modal.TrainingL[i].TrainingType, Modal.TrainingL[i].TrainingRemarks, 0, 0, 0, "", "", "", "", "", "", "", "", 0, "", "", "", string.Empty);
                                }
                            }
                        }
                    }
                    //Question 
                    if (Modal.QAL != null)
                    {
                        for (int i = 0; i < Modal.QAL.Count; i++)
                        {
                            Common_SPU.fnSetPMS_QA(Modal.FYID, Modal.EMPID, "Self", Modal.QAL[i].Question, Modal.QAL[i].Answer, "", "", 0);
                        }
                    }
                    //Comment
                    if (Modal.CommentsL != null)
                    {
                        for (int i = 0; i < Modal.CommentsL.Count; i++)
                        {
                            Common_SPU.fnSetPMSComments(Modal.CommentsL[i].PMS_CommentID, Modal.FYID, Modal.EMPID, Modal.CommentsL[i].Comment, "Self", 1, PostResult.ID, Modal.CommentsL[i].Doctype);
                        }
                    }
                    // Attachment
                    if (Modal.AttachmentsL != null)
                    {
                        for (int i = 0; i < Modal.AttachmentsL.Count; i++)
                        {
                            if (Modal.AttachmentsL[i].UploadFile != null)
                            {
                                var RvFile = clsApplicationSetting.ValidateFile(Modal.AttachmentsL[i].UploadFile);
                                if (RvFile.IsValid)
                                {
                                    Modal.AttachmentsL[i].AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentsL[i].AttachmentID, Modal.AttachmentsL[i].FileName, RvFile.FileExt, Modal.AttachmentsL[i].Descrip, PostResult.ID.ToString(), "Appraisal");
                                    if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentsL[i].AttachmentID + RvFile.FileExt)))
                                    {
                                        System.IO.File.Delete("~/Attachments/" + Modal.AttachmentsL[i].AttachmentID + RvFile.FileExt);
                                    }
                                    Modal.AttachmentsL[i].UploadFile.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentsL[i].AttachmentID + RvFile.FileExt));
                                }
                            }
                            else
                            {
                                if (Modal.AttachmentsL[i].AttachmentID > 0)
                                {
                                    Common_SPU.fnSetAttachments(Modal.AttachmentsL[i].AttachmentID, Modal.AttachmentsL[i].FileName, "", Modal.AttachmentsL[i].Descrip, PostResult.ID.ToString(), "Appraisal");
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    ClsCommon.LogError("Error during SetSelfAppraisal. The query was executed :", ex.ToString(), "spu_GetConsultant", "PMSController", "PMSController", "");
                }
            }
            //}
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult TeamAppraisalList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            ViewBag.FYID = Modal.FYID.ToString();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _TeamAppraisalList(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = Modal.FYID.ToString();
            List<PMS.TeamAppraisal.List> list = new List<PMS.TeamAppraisal.List>();
            list = PMS.GetTeamAppraisalList(Modal.FYID, "Appraisal");
            ViewBag.TeamList = PMS.GetTeamAppraisalList(Modal.FYID, "TEAM");
            return PartialView(list);
        }

        public ActionResult TeamAppraisal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PMS_AID = GetQueryString[2];
            long PMS_AID = 0;
            long.TryParse(ViewBag.PMS_AID, out PMS_AID);
            PMS.TeamAppraisal.Add obj = new PMS.TeamAppraisal.Add();
            obj = PMS.GetTeamAppraisal(PMS_AID);
            return View(obj);
        }
        [HttpPost]
        public ActionResult SetTeamAppraisal(string src, PMS.TeamAppraisal.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.SuccessMessage = "Team Appraisal not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.KPAL == null)
            {
                PostResult.SuccessMessage = "Dnt have KPA";
                ModelState.AddModelError("FYID", PostResult.SuccessMessage);
            }
            else if (Modal.KPAL.Any(x => string.IsNullOrEmpty(x.HOD_Score)))
            {
                PostResult.SuccessMessage = "Fill All Score";
                ModelState.AddModelError("FYID", PostResult.SuccessMessage);
                PostResult.StatusCode = 2;
            }
            else if (Command == "Submit" && Modal.KPAL.Any(x => string.IsNullOrEmpty(x.HOD_Comment)))
            {
                PostResult.SuccessMessage = "Fill All Comment";
                ModelState.AddModelError("FYID", PostResult.SuccessMessage);
                PostResult.StatusCode = 2;
            }
            else if (Modal.Approved == 3 && Modal.Team_Score == 0)
            {
                PostResult.SuccessMessage = "Overall Score Mandatory";
                ModelState.AddModelError("FYID", PostResult.SuccessMessage);
            }
            //else if (Modal.Approved == 3 && string.IsNullOrEmpty(Modal.TeamRecommendation))
            //{
            //    PostResult.SuccessMessage = "Recommendation Mandatory";
            //    ModelState.AddModelError("FYID", PostResult.SuccessMessage);
            //}            
            if (Modal.TeamCommentL != null)
            {
                for (int i = 0; i < Modal.TeamCommentL.Count; i++)
                {
                    if (Modal.TeamCommentL[i].Doctype == "Team Comment 2" && string.IsNullOrEmpty(Modal.TeamCommentL[i].Comment) && Command == "Approved")
                    {
                        PostResult.SuccessMessage = "Please fill your Final Assessment";
                        ModelState.AddModelError("FYID", PostResult.SuccessMessage);
                    }
                    if (Modal.TeamCommentL[i].Doctype == "Team Comment 1" && string.IsNullOrEmpty(Modal.TeamCommentL[i].Comment) && Command == "Submit")
                    {
                        PostResult.SuccessMessage = "Please fill your Assessment";
                        ModelState.AddModelError("FYID", PostResult.SuccessMessage);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // KPA set
                    if (Modal.KPAL != null)
                    {
                        for (int i = 0; i < Modal.KPAL.Count; i++)
                        {
                            Common_SPU.fnSetPMS_Appraisal_Det_Team(Modal.KPAL[i].PMS_ADID, Modal.PMS_AID, "KPA", 0, "", "",
                                Modal.KPAL[i].HOD_Score, Modal.KPAL[i].HOD_Comment);
                        }
                    }
                    // Traninng 
                    if (Modal.TrainingL != null)
                    {
                        for (int i = 0; i < Modal.TrainingL.Count; i++)
                        {
                            if (Modal.TrainingL[i].TrainingTypeID != null)
                            {
                                if (Modal.TrainingL[i].isdeleted == 0)
                                {
                                    Common_SPU.fnSetPMS_Appraisal_Det_Team(Modal.TrainingL[i].PMS_ADID, Modal.PMS_AID, "Training", Modal.TrainingL[i].TrainingTypeID ?? 0, Modal.TrainingL[i].TrainingType, Modal.TrainingL[i].TrainingRemarks, "0", "");
                                }
                            }
                        }
                    }
                    //Question 
                    if (Modal.QAL != null)
                    {
                        for (int i = 0; i < Modal.QAL.Count; i++)
                        {

                            if (Modal.QAL[i].QuestionFor.ToLower().Contains("appraiser"))
                            {
                                Common_SPU.fnSetPMS_QA(Modal.FYID, Modal.EMPID, "Team", Modal.QAL[i].Question, Modal.QAL[i].FinalComment, "", "", 0);
                            }
                        }
                    }
                    //Question
                    if (Modal.QALEmp != null)
                    {
                        for (int i = 0; i < Modal.QALEmp.Count; i++)
                        {

                            if (Modal.QALEmp[i].QuestionFor.ToLower().Contains("appraiser"))
                            {
                                Common_SPU.fnSetPMS_QA(Modal.FYID, Modal.EMPID, "Team", Modal.QALEmp[i].Question, Modal.QALEmp[i].FinalComment, "", "", 0);
                            }
                        }
                    }
                    //Comment
                    if (Modal.TeamCommentL != null)
                    {
                        for (int i = 0; i < Modal.TeamCommentL.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(Modal.TeamCommentL[i].Comment))
                            {
                                Common_SPU.fnSetPMSComments(Modal.TeamCommentL[i].PMS_CommentID, Modal.FYID, Modal.EMPID, Modal.TeamCommentL[i].Comment, "Team", 1, PostResult.ID, Modal.TeamCommentL[i].Doctype);
                            }
                        }
                    }

                    if (Modal.Approved == 1 || Modal.Approved == 3)
                    {
                        int Approved = 0;
                        if (Command == "Resubmit")
                        {
                            Approved = 2;
                        }
                        else if (Command == "Approved")
                        {
                            Approved = 4;
                        }
                        CommandResult Result = Common_SPU.fnSetPMS_AppraisalApproval(Modal.PMS_AID, Approved, "Team", Modal.Team_Score, Modal.TeamRecommendation, "", 0, 0, 0);
                        PostResult.Status = Result.Status;
                        PostResult.SuccessMessage = Result.SuccessMessage;
                        PostResult.StatusCode = Result.StatusCode;
                        PostResult.ID = Result.ID;
                        PostResult.RedirectURL = "/PMS/TeamAppraisalList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/TeamAppraisalList");
                    }
                    else
                    {
                        int Approved = 0;
                        if (Command == "Resubmit")
                        {
                            Approved = 2;
                        }
                        else if (Command == "Submit")
                        {
                            Approved = 1;
                        }
                        if (Command != "Save")
                        {
                            CommandResult Result = Common_SPU.fnSetPMS_AppraisalApprovalReSubmit(Modal.PMS_AID, Approved,Modal.Reason, "Team", 0, "", "", 0, 0, 0);
                            PostResult.Status = Result.Status;
                            PostResult.SuccessMessage = Result.SuccessMessage;
                            PostResult.StatusCode = Result.StatusCode;
                            PostResult.ID = Result.ID;
                        }
                        else
                        {
                            PostResult.Status = true;
                            PostResult.SuccessMessage = "Save Successfully";
                        }
                        if (PostResult.Status)
                        {
                            if (Command != "Save")
                                PostResult.RedirectURL = "/PMS/TeamAppraisalList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/TeamAppraisalList");
                            else
                                PostResult.RedirectURL = "/PMS/TeamAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/TeamAppraisal*"+ Modal.PMS_AID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClsCommon.LogError("Error during SetSelfAppraisal. The query was executed :", ex.ToString(), "spu_GetConsultant", "PMSController", "PMSController", "");
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GroupAppraisalList(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            ViewBag.FYID = Modal.FYID.ToString();
            return View(Modal);
        }
        public ActionResult GroupAppraisal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PMS_AID = GetQueryString[2];
            long PMS_AID = 0;
            long.TryParse(ViewBag.PMS_AID, out PMS_AID);
            PMS.GroupAppraisal.Add obj = new PMS.GroupAppraisal.Add();
            obj = PMS.GetGroupAppraisal(PMS_AID);
            return View(obj);
        }
        [HttpPost]
        public ActionResult _GroupAppraisalList(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = Modal.FYID.ToString();
            List<PMS.GroupAppraisal.List> list = new List<PMS.GroupAppraisal.List>();
            list = PMS.GetGroupAppraisalList(Modal.FYID, "Appraisal");
            ViewBag.TeamList = PMS.GetGroupAppraisalList(Modal.FYID, "TEAM");
            return PartialView(list);
        }
        [HttpPost]
        public ActionResult SetGroupAppraisal(string src, PMS.GroupAppraisal.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.SuccessMessage = "Group Appraisal action not taken";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.chkAgreeWithAppraisal == "No" && string.IsNullOrEmpty(Modal.Group_Comment))
            {
                PostResult.SuccessMessage = "Comment can't be blank";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (Modal.chkAgreeWithAppraisal == "No" && (Modal.Group_Score ?? 0) == 0)
            {
                PostResult.SuccessMessage = "Score can't be blank";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (Modal.chkAgreeWithAppraisal != "No" && ClsCommon.FnCheckPMS_CommentatorRatingPending(Modal.EMPID.ToString(), Modal.FYID.ToString()) > 0)
            {
                PostResult.SuccessMessage = "Can not submit as Additional Reviewer's feedback is pending";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (Modal.No_Conformers > Modal.No_ApprovedConformers)
            {
                PostResult.SuccessMessage = "Can not submit as Additional Reviewer's feedback is pending.";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (Modal.chkAgreeWithAppraisal == "Yes")
                    {
                        // Modal.Group_Comment = "";
                        if (Modal.Group_Comment == null)
                        {
                            Modal.Group_Comment = "";
                        }
                        Modal.Group_Score = 0;
                    }
                    if (Command == "Resubmit")
                    {
                        CommandResult Result = Common_SPU.fnSetPMS_AppraisalApproval(Modal.PMS_AID, 6, "Group", 0, "", Modal.Group_Comment, Modal.Group_Score ?? 0, 0, 0, "");
                        PostResult.Status = Result.Status;
                        PostResult.SuccessMessage = Result.SuccessMessage;
                        PostResult.StatusCode = Result.StatusCode;
                        PostResult.ID = Result.ID;
                    }
                    else if (Command == "Approved")
                    {

                        CommandResult Result = Common_SPU.fnSetPMS_AppraisalApproval(Modal.PMS_AID, 5, "Group", 0, "", Modal.Group_Comment, Modal.Group_Score ?? 0, 0, 0, "");
                        PostResult.Status = Result.Status;
                        PostResult.SuccessMessage = Result.SuccessMessage;
                        PostResult.StatusCode = Result.StatusCode;
                        PostResult.ID = Result.ID;
                    }

                    if (PostResult.Status)
                    {
                        PostResult.RedirectURL = "/PMS/GroupAppraisalList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/GroupAppraisalList");
                    }
                    else
                    {
                        PostResult.RedirectURL = "/PMS/GroupAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/GroupAppraisal");
                    }

                }
                catch (Exception ex)
                {
                    ClsCommon.LogError("Error during GroupAppraisal. The query was executed :", ex.ToString(), "GroupAppraisal", "PMSController", "PMSController", "");
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CMCAppraisalList(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Tab = "Pending";
            try
            {
                ViewBag.Tab = GetQueryString[2];
            }
            catch { ViewBag.Tab = "Pending"; }

            PMS Modal = new PMS();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            ViewBag.FYID = (Modal.FYID - 1).ToString();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _CMCAppraisalList(string src, PMS Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Tab = GetQueryString[2];
            ViewBag.FYID = Modal.FYID.ToString();
            List<PMS.CMCAppraisal.List> list = new List<PMS.CMCAppraisal.List>();
            list = PMS.GetCMCAppraisalList(Modal.FYID);
            return PartialView(list);
        }
        public ActionResult CMCAppraisal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PMS_AID = GetQueryString[2];
            long PMS_AID = 0;
            long.TryParse(ViewBag.PMS_AID, out PMS_AID);
            PMS.CMCAppraisal.Add obj = new PMS.CMCAppraisal.Add();
            obj = PMS.GetCMCAppraisal(PMS_AID);
            return View(obj);
        }

        [HttpPost]
        public ActionResult SetCMCAppraisal(string src, PMS.CMCAppraisal.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.SuccessMessage = "CMC Appraisal action not taken";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.chkAgreeWithAppraisal == "No" && string.IsNullOrEmpty(Modal.CMC_Comment))
            {
                PostResult.SuccessMessage = "Comment can't be blank";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Modal.chkAgreeWithAppraisal == "Yes")
                    {
                        Modal.CMC_Comment = "";
                    }
                    if (Command == "Resubmit")
                    {
                        CommandResult Result = Common_SPU.fnSetPMS_AppraisalApproval(Modal.PMS_AID, 8, "CMC", 0, "", "", 0, Modal.CMC_Increment, Modal.CMC_Score, Modal.CMC_Comment);
                        PostResult.Status = Result.Status;
                        PostResult.SuccessMessage = Result.SuccessMessage;
                        PostResult.StatusCode = Result.StatusCode;
                        PostResult.ID = Result.ID;
                    }
                    else if (Command == "Approved")
                    {

                        CommandResult Result = Common_SPU.fnSetPMS_AppraisalApproval(Modal.PMS_AID, 7, "CMC", 0, "", "", 0, Modal.CMC_Increment, Modal.CMC_Score, Modal.CMC_Comment);
                        PostResult.Status = Result.Status;
                        PostResult.SuccessMessage = Result.SuccessMessage;
                        PostResult.StatusCode = Result.StatusCode;
                        PostResult.ID = Result.ID;
                    }

                    if (PostResult.Status)
                    {
                        PostResult.RedirectURL = "/PMS/CMCAppraisalList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/CMCAppraisalList");
                    }
                    else
                    {
                        PostResult.RedirectURL = "/PMS/CMCAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/CMCAppraisal");
                    }

                }
                catch (Exception ex)
                {
                    ClsCommon.LogError("Error during CMCAppraisal. The query was executed :", ex.ToString(), "CMCAppraisal", "PMSController", "PMSController", "");
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ViewAppraisal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PMS_AID = GetQueryString[2];
            long PMS_AID = 0;
            long.TryParse(ViewBag.PMS_AID, out PMS_AID);
            PMS.Appraisal.Add obj = new PMS.Appraisal.Add();
            obj = PMS.GetAppraisal_View(PMS_AID);
            return View(obj);
        }
        public ActionResult TrainingTypList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PMS.TrainingTypeMaster.List> Modal = new List<PMS.TrainingTypeMaster.List>();
            Modal = PMS.GetTrainingTypeList(0);
            return View(Modal);
        }
        public ActionResult AddTrainingType(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            PMS.TrainingTypeMaster.Add Modal = new PMS.TrainingTypeMaster.Add();
            getResponse.ID = ID;
            Modal = PMS.GetTrainingType(getResponse);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult AddTrainingType(string src, PMS.TrainingTypeMaster.Add Modal, string Command)
        {
            ViewBag.TabIndex = 1;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            PostResult.SuccessMessage = "Action Can't Update";
            if (ModelState.IsValid)
            {
                Modal.Id = ID;
                PostResult = PMS.SetTrainingType(Modal);
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/PMS/TrainingTypList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/PMS/TrainingTypList*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PMSStatusList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            //PMS Modal = new PMS();
            //List<PMS.FinList> FList = new List<PMS.FinList>();
            //FList = PMS.GetFinYearList();
            //ViewBag.FinYearList = FList;
            //Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();           
            //return View(Modal);
            //PMS Modal = new PMS();

            PMS.PMSStatus Modal = new PMS.PMSStatus();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            ViewBag.FYID = Modal.FYID;
            List<DropDownList> empList = new List<DropDownList>();
            GetResponse employee = new GetResponse();
            employee.Doctype = "GR_Employee";
            employee.LoginID = LoginID;
            empList = ClsCommon.GetDropDownList(employee);
            ViewBag.EmployeeList = empList;
            //Modal.Empid = empList.Select(x => x.ID).FirstOrDefault();
            //List<PMS.PMSStatus> List = new List<PMS.PMSStatus>();
            //Modal.PMSStatusList = PMS.GetPMSStatusList(Modal.Empid, Modal.FYID);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _PMSStatusList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Empid = GetQueryString[2];
            ViewBag.FYID = GetQueryString[3];
            long Empid = 0, FYID = 0;
            long.TryParse(ViewBag.Empid, out Empid);
            long.TryParse(ViewBag.FYID, out FYID);

            List<PMS.PMSStatus> List = new List<PMS.PMSStatus>();
            List = PMS.GetPMSStatusList(Empid, FYID);
            return PartialView(List);
        }
        public ActionResult AppraisalReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            PMS.PMSStatus Modal = new PMS.PMSStatus();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            ViewBag.FYID = Modal.FYID;
            List<DropDownList> empList = new List<DropDownList>();
            GetResponse employee = new GetResponse();
            employee.Doctype = "GR_Employee";
            employee.LoginID = LoginID;
            empList = ClsCommon.GetDropDownList(employee);
            ViewBag.EmployeeList = empList;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _AppraisalReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Empid = GetQueryString[2];
            ViewBag.FYID = GetQueryString[3];
            long Empid = 0, FYID = 0;
            long.TryParse(ViewBag.Empid, out Empid);
            long.TryParse(ViewBag.FYID, out FYID);

            List<PMS.PMSStatus> List = new List<PMS.PMSStatus>();
            List = PMS.GetAppraisalReportList(Empid, FYID);
            return PartialView(List);
        }

        public ActionResult AdditionalReviewerReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            PMS.PMSStatus Modal = new PMS.PMSStatus();
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            ViewBag.FYID = Modal.FYID;
            List<DropDownList> empList = new List<DropDownList>();
            GetResponse employee = new GetResponse();
            employee.Doctype = "GR_Employee";
            employee.LoginID = LoginID;
            empList = ClsCommon.GetDropDownList(employee);
            ViewBag.EmployeeList = empList;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _AdditionalReviewerReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Empid = GetQueryString[2];
            ViewBag.FYID = GetQueryString[3];
            long Empid = 0, FYID = 0;
            long.TryParse(ViewBag.Empid, out Empid);
            long.TryParse(ViewBag.FYID, out FYID);

            List<PMS.PMSStatus> List = new List<PMS.PMSStatus>();
            List = PMS.GetAdditionalReviewerReportList(Empid, FYID);
            return PartialView(List);
        }

        public ActionResult RequiredSkillReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PMS.TrainingSkills Modal = new PMS.TrainingSkills();
            ViewBag.FinYearList = PMS.GetFinYearList();
            BudgetMaster.EmployeeList employeeList = new BudgetMaster.EmployeeList();
            employeeList.Id = 0;
            employeeList.Name = "All";
            List<BudgetMaster.EmployeeList> list = Budget.GetBudgetSettingEmpList();
            list.Insert(0, employeeList);
            ViewBag.EmployeeList = list;

            return View(Modal);

        }
        public ActionResult _RequiredSkillReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Empid = GetQueryString[2];
            ViewBag.FYID = GetQueryString[3];
            long Empid = 0, FYID = 0;
            long.TryParse(ViewBag.Empid, out Empid);
            long.TryParse(ViewBag.FYID, out FYID);
            PMS.TrainingSkills Modal = new PMS.TrainingSkills();
            GetResponse getResponse = new GetResponse();
            getResponse.LoginID = Empid;
            getResponse.ID = FYID;
            Modal.trainingSkills = PMS.GetEmployeeTrainingReportList(getResponse);
            return PartialView(Modal);

        }

    }
}