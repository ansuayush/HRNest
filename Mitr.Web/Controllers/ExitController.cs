using Mitr.BLL;
using Mitr.CommonClass;
using Mitr.CommonLib;
using Mitr.Interface;
using Mitr.Model;
using Mitr.Model.Onboarding;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class ExitController : Controller
    {
        IExitHelper Ext;
        IToolsHelper Tools;
        IExit _iExit;
        public ExitController()
        {
            Ext = new ExitModal();
            Tools = new ToolsModal();
            _iExit = new ExitBLL();
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


        public ActionResult _RequestView(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            long Exit_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);
            return PartialView(Ext.GetExit_Req_View(Exit_ID));
        }


        [HttpPost]
        public ActionResult _ExitRequestList(Exit.Tabs Modal, string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Exit.Request.List> modal = new List<Exit.Request.List>();
            modal = Ext.GetExitRequestList(Modal.Approve);
            return PartialView(modal);
        }

        public ActionResult MyExitRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Exit.Request.Add result = new Exit.Request.Add();
            result = Ext.GetExitRequest(0);
            return View(result);
        }

        [HttpPost]
        public ActionResult MyExitRequest(string src, Exit.Request.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Request can't be Added";
            if (Modal.IsServeNotice == 0 && Modal.RelievingDate == null)
            {
                PostResult.SuccessMessage = "Relieving Date can't be blank";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (Modal.IsServeNotice == 0 && Modal.ReasonForNotServingNotice == null)
            {
                PostResult.SuccessMessage = "Reason For Not Serving Notice can't be blank";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    long EMPID = 0;
                    long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
                    Modal.EMP_ID = EMPID;
                    DateTime Actual_RelievingDate;
                    DateTime.TryParse(Modal.Actual_RelievingDate, out Actual_RelievingDate);
                    Modal.Actual_RelievingDate = Actual_RelievingDate.ToString("yyyy-MM-dd");
                    if (Modal.IsServeNotice == 1)
                    {
                        Modal.RelievingDate = Modal.Actual_RelievingDate;
                    }
                    PostResult = Common_SPU.SetExit_Request(Modal);
                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Exit/MyExitRequest?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Exit/MyExitRequest");
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        // Exit Requests Received

        public ActionResult RequestReceived(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Exit.Tabs Modal = new Exit.Tabs();
            Modal.Approve = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _RequestReceived(Exit.Tabs Modal, string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            List<Exit.Req_Received.List> modal = new List<Exit.Req_Received.List>();
            modal = Ext.GetExit_ReqRecList(Modal.Approve);
            return PartialView(modal);
        }

        public ActionResult RequestProcessing(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            long Exit_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);
            Exit.Request_View Modal = new Exit.Request_View();
            Modal = Ext.GetExit_Req_View(Exit_ID);
            return View(Modal);
        }
        public ActionResult _RequestProcessing(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            ViewBag.ActionType = GetQueryString[3];
            string ActionType = ViewBag.ActionType;

            ViewBag.RetainedAt_Level = GetQueryString[4];
            string RetainedAt_Level = ViewBag.RetainedAt_Level;

            long Exit_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);

            if (ActionType == "forward")
            {
                long HODID = 0;
                long.TryParse(clsApplicationSetting.GetSessionValue("HODID"), out HODID);
                Exit.Req_Process.Forward modal = new Exit.Req_Process.Forward();
                modal = Ext.GetHR_ExitForward(Exit_ID);
                modal.LevelList.FirstOrDefault().ID = HODID;
                return PartialView("_HR_ExitForward", modal);
            }
            else if (ActionType == "approve")
            {
                Exit.Req_Process.Approve modal = new Exit.Req_Process.Approve();
                modal = Ext.GetHR_ExitApproveAndProcess(Exit_ID);
                return PartialView("_HR_ExitApproveAndProcess", modal);
            }
            else
            {
                Exit.Req_Process.Resolution modal = new Exit.Req_Process.Resolution();
                modal.RetainedAt_Level = RetainedAt_Level;
                return PartialView("_ExitRetained", modal);
            }

        }

        [HttpPost]
        public ActionResult _ExitRetained(string src, Exit.Req_Process.Resolution Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            long Exit_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);

            ViewBag.RetainedAt_Level = GetQueryString[3];
            string RetainedAt_Level = ViewBag.RetainedAt_Level;

            PostResult.SuccessMessage = "Retained Can't Update";
            long AttachmentID = 0;
            if (ModelState.IsValid)
            {

                if (Modal.UploadFile != null)
                {
                    var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadFile);
                    if (RvFile.IsValid)
                    {
                        AttachmentID = Common_SPU.fnSetAttachments(0, RvFile.FileName, RvFile.FileExt, "");
                        if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + AttachmentID + RvFile.FileExt)))
                        {
                            System.IO.File.Delete("~/Attachments/" + AttachmentID + RvFile.FileExt);
                        }
                        Modal.UploadFile.SaveAs(Server.MapPath("~/Attachments/" + AttachmentID + RvFile.FileExt));
                    }
                    else
                    {
                        PostResult.Status = RvFile.IsValid;
                        PostResult.SuccessMessage = RvFile.Message;
                        return Json(PostResult, JsonRequestBehavior.AllowGet);
                    }
                }

                PostResult = Common_SPU.SetExit_Retained(Exit_ID, Modal.RetainedAt_Level, Modal.Retained_Remarks, AttachmentID);
            }
            if (PostResult.Status)
            {
                if (RetainedAt_Level == "HR")
                {
                    PostResult.RedirectURL = "/Exit/RequestReceived?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Exit/RequestReceived");
                }
                else
                {
                    PostResult.RedirectURL = "/Exit/LevelApprovalsList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Exit/LevelApprovalsList");
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult _HR_ExitForward(string src, Exit.Req_Process.Forward Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            long Exit_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);
            PostResult.SuccessMessage = "Approvers Can't Update";
            if (Modal.LevelList.FirstOrDefault().ID == null)
            {
                PostResult.SuccessMessage = "Level 1 Can't be blank";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (Modal.LevelList.Where(x => x.ID != null).GroupBy(x => x.ID).Count() != Modal.LevelList.Where(x => x.ID != null).Count())
            {
                PostResult.SuccessMessage = "Approvers must be unique";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                foreach (var item in Modal.LevelList.Where(x => x.ID != null).ToList())
                {
                    PostResult = Common_SPU.SetExit_Approvers(0, Exit_ID, item.LevelType, item.ID ?? 0);
                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Exit/RequestReceived?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Exit/RequestReceived");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult _HR_ExitApproveAndProcess(string src, Exit.Req_Process.Approve Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            long Exit_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);
            try
            {
                PostResult.SuccessMessage = "Approved Can't Update";
                if (Modal.AttachmentList != null)
                {
                    foreach (Exit.Attachment item in Modal.AttachmentList.Where(x => x.Upload != null).ToList())
                    {
                        var RvFile = clsApplicationSetting.ValidateFile(item.Upload);
                        if (!RvFile.IsValid)
                        {
                            PostResult.Status = RvFile.IsValid;
                            PostResult.SuccessMessage = RvFile.Message;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (ModelState.IsValid)
                {
                    int count = 0, DeactiveSurvey = 0;
                    DateTime RelievingDate;
                    DeactiveSurvey = (string.IsNullOrEmpty(Modal.DeactiveSurvey) ? 0 : 1);
                    DateTime.TryParse(Modal.RelievingDate, out RelievingDate);
                    foreach (Exit.Req_Process.Approve.Department item in Modal.DepartmentList)
                    {

                        count++;
                        item.Exit_ID = Exit_ID;
                        item.Priority = count;
                        Ext.SetExit_Handover_Persons(item);
                    }
                    // task save
                    if (Modal.Exit_TaskList != null)
                    {
                        foreach (Exit.Exit_Task item in Modal.Exit_TaskList)
                        {
                            item.Exit_ID = Exit_ID;
                            Ext.SetExit_Tasks(item);
                        }
                    }
                    // Attachmnets
                    if (Modal.AttachmentList != null)
                    {
                        foreach (Exit.Attachment item in Modal.AttachmentList)
                        {
                            if (item.Upload != null)
                            {
                                long AttachmentID = 0;
                                var RvFile = clsApplicationSetting.ValidateFile(item.Upload);
                                if (RvFile.IsValid)
                                {
                                    AttachmentID = Common_SPU.fnSetAttachments(0, RvFile.FileName, RvFile.FileExt, item.Decription, Exit_ID.ToString(), "Exit_Attachments");
                                    if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + AttachmentID + RvFile.FileExt)))
                                    {
                                        System.IO.File.Delete("~/Attachments/" + AttachmentID + RvFile.FileExt);
                                    }
                                    item.Upload.SaveAs(Server.MapPath("~/Attachments/" + AttachmentID + RvFile.FileExt));
                                }
                            }
                        }
                    }
                    DateTime dtdate = DateTime.ParseExact(Modal.RelievingDate, "dd/MM/yyyy", null);
                    //DateTime.TryParse(Modal.RelievingDate, out dtdate);

                    int Survey = (string.IsNullOrEmpty(Modal.DeactiveSurvey) ? 0 : 1);
                    PostResult = Ext.SetExit_Approved(Exit_ID, Modal.Approved_Remarks, Survey, dtdate);
                }
                if (PostResult.Status)
                {
                    PostResult.RedirectURL = "/Exit/RequestReceived?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Exit/RequestReceived");
                }
            }
            catch (Exception ex)
            {

                PostResult.StatusCode = -1;
                PostResult.SuccessMessage = ex.Message.ToString();
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ApprovedRequestsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Exit.Tabs Modal = new Exit.Tabs();
            Modal.Approve = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ApprovedRequestsList(Exit.Tabs Modal, string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Exit.Req_Approved.List> modal = new List<Exit.Req_Approved.List>();
            modal = Ext.GetExit_Req_ApprovedList(Modal.Approve);
            return PartialView(modal);
        }



        public ActionResult LevelApprovalsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Exit.Tabs Modal = new Exit.Tabs();
            Modal.Approve = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _LevelApprovalsList(Exit.Tabs Modal, string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Exit.LevelApprovals.List> result = new List<Exit.LevelApprovals.List>();
            result = Ext.GetExit_ApproversList(Modal.Approve);
            return PartialView(result);
        }
        public ActionResult LevelApprovals_Action(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            ViewBag.Exit_APP_ID = GetQueryString[3];
            long Exit_ID = 0, Exit_APP_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);
            long.TryParse(ViewBag.Exit_APP_ID, out Exit_APP_ID);
            Exit.LevelApprovals.Add result = new Exit.LevelApprovals.Add();
            result = Ext.GetExit_ApproversAdd(Exit_APP_ID);


            ViewBag.ViewModal = Ext.GetExit_Req_View(Exit_ID);

            return View(result);
        }

        [HttpPost]
        public ActionResult LevelApprovals_Action(string src, Exit.LevelApprovals.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            ViewBag.Exit_APP_ID = GetQueryString[3];
            long Exit_ID = 0, Exit_APP_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);
            long.TryParse(ViewBag.Exit_APP_ID, out Exit_APP_ID);

            PostResult.SuccessMessage = "Approver's action Can't Update";
            if (Modal.Approved == 2 && string.IsNullOrEmpty(Modal.Suggested_RDate))
            {
                PostResult.SuccessMessage = "Suggested Date Can't be blank";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            else if (Modal.Approved == 2 && string.IsNullOrEmpty(Modal.Suggested_RDate))
            {
                PostResult.SuccessMessage = "Reason Can't be blank";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                if (Modal.Approved == 0)
                {
                    Modal.Approved = 1;
                }
                PostResult = Ext.SetExit_Approvers_Action(Modal);
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Exit/LevelApprovalsList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Exit/LevelApprovalsList");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult StartExitProcess(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            long Exit_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);
            Exit.StartExitProcess result = new Exit.StartExitProcess();
            result = Ext.GetExit_StarExitProcess(Exit_ID);
            return View(result);
        }

        [HttpPost]
        public ActionResult StartExitProcess(string src, Exit.StartExitProcess Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            long Exit_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);


            PostResult.SuccessMessage = "Action Can't Update";
            if (ModelState.IsValid)
            {
                Modal.Exit_ID = Exit_ID;
                PostResult = Ext.SetExit_StartProcess(Modal);
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Exit/ApprovedRequestsList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Exit/ApprovedRequestsList");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult NOCRequestList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Exit.Tabs Modal = new Exit.Tabs();
            Modal.Approve = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _NOCRequestList(Exit.Tabs Modal, string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            List<Exit.NOC_Request.List> result = new List<Exit.NOC_Request.List>();
            result = Ext.GetExit_NOC_List(Modal.Approve);
            return PartialView(result);
        }

        public ActionResult NOCDashboard(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Exit.Tabs Modal = new Exit.Tabs();
            Modal.Approve = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _NOCDashboard(Exit.Tabs Modal, string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Exit.NOC_Dashboard.List> result = new List<Exit.NOC_Dashboard.List>();
            result = Ext.GetExit_NOC_Dashboard_List(Modal.Approve);
            return PartialView(result);
        }

        public ActionResult ExitConfirmation(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Exit.Tabs Modal = new Exit.Tabs();
            Modal.Approve = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ExitConfirmation(Exit.Tabs Modal, string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Exit.Confirmation.List> result = new List<Exit.Confirmation.List>();
            result = Ext.GetExit_Confirmation_List(Modal.Approve);
            return PartialView(result);
        }


        public ActionResult _Exit_TaskList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            long Exit_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);
            string TableName = "Exit";
            List<User_Task.Task.List> Modal = new List<User_Task.Task.List>();
            Modal = Tools.GetUser_TaskList(0, Exit_ID, TableName);
            return PartialView(Modal);

        }

        public ActionResult _ApproversSuggationsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Exit_ID = GetQueryString[2];
            long Exit_ID = 0;
            long.TryParse(ViewBag.Exit_ID, out Exit_ID);

            List<Exit.LevelApprovers_Details> result = new List<Exit.LevelApprovers_Details>();
            result = Ext.GetExit_ApproversSuggationsList(Exit_ID);
            return PartialView(result);
        }
        [HttpGet]
        public JsonResult GetExitEmployeeDetails(string src)
        {
            try
            {
                string Exit_HP_ID = "", Exit_ID = ""; int Approved = 0;
                ViewBag.src = src;
                string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
                ViewBag.GetQueryString = GetQueryString;
                Exit_HP_ID = GetQueryString[2];
                Exit_ID = GetQueryString[3];
                Approved = Convert.ToInt32(GetQueryString[4] == "" ? "0" : GetQueryString[4]);
                List<Exit.NOC_Request.List> result1 = new List<Exit.NOC_Request.List>();
                var result = Common_SPU.GetExitEmployeeDetails(Approved, Exit_HP_ID, Exit_ID);
                string jsonData = JsonConvert.SerializeObject(result);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                CommonMethods.Error(ex);
                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = "";
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = ex.Message + " " + ex.StackTrace;
                objCustomResponseModel.CommomDropDownData = null;
                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [HttpPost]
        public JsonResult SaveAssetsDetails(Exit exitProcess)
        {
            exitProcess.Id = 0;
            exitProcess.IsGrid = 1;
            exitProcess.InputData = 1;
            exitProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(exitProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iExit.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDExit), Constants.ScreenIDExit.ExitAssetsDetail.ToString())), roleId, userid, "GET", out errorMessage);
                string jsonData = JsonConvert.SerializeObject(data);

                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception ex)
            {
                CommonMethods.Error(ex);
                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = "";
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = ex.Message + " " + ex.StackTrace;
                objCustomResponseModel.CommomDropDownData = null;
                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

    }
}