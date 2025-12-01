using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    public class ExitEmployeeController : Controller
    {
        // GET: ExitEmployee

        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IExitEmployee exitEmployee;
        IMasterHelper Master;
        public ExitEmployeeController()
        {
            getResponse = new GetResponse();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            exitEmployee = new ExitEmployeeModal();
            Master = new MasterModal();


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

        public ActionResult SignatoryMaster(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<EmployeeExit.SignatoryMaster> Modal = new List<EmployeeExit.SignatoryMaster>();
            Modal = exitEmployee.GetSignatoryMaster(0);
            return View(Modal);
        }

        public ActionResult _AddAuthorized(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            EmployeeExit.SignatoryMaster Modal = new EmployeeExit.SignatoryMaster();
            ViewBag.EmpList = CommonSpecial.GetAllEmployeeOfficeList();

            if (ID > 0)
            {
                Modal = exitEmployee.GetSignatoryMaster(ID).FirstOrDefault();
            }

            return PartialView(Modal);

        }

        [HttpPost]
        public ActionResult _AddAuthorized(string src, EmployeeExit.SignatoryMaster Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Action Can't Save";
            string _Path = clsApplicationSetting.GetPhysicalPath("SignatoryMaster");
            if (Modal.Attachmentid ==null)
            {
                if (Modal.UploadFile == null)
                {
                    ModelState.AddModelError("EMPId", "Hey! You missed this field");
                    PostResult.SuccessMessage = "Hey! You missed this field";
                }
            }
           
        
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    if (Modal.UploadFile != null)
                    {
                        var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadFile);
                        if (RvFile.IsValid)
                        {
                            Modal.Attachmentid = Common_SPU.fnSetAttachments(0, RvFile.FileName, RvFile.FileExt, "");
                            Modal.UploadFile.SaveAs(Path.Combine(_Path, Modal.Attachmentid + RvFile.FileExt));
                        }
                    }
                    PostResult = exitEmployee.fnsetAddSignatoryMaster(Modal);
                    if (PostResult.Status == true)
                    {
                       // PostResult.SuccessMessage = "Action save Data";
                        PostResult.ID = Convert.ToInt64(Modal.Id);
                        ModelState.Clear();
                        return Json(PostResult, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ResignationRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            EmployeeExit.ResignationRequest Modal = new EmployeeExit.ResignationRequest();
            Modal = exitEmployee.GetResignation(0, EMPID, "R");
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Exit_Reason";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> listreason = new List<MasterAll.List>();
            listreason = Master.GetMasterAllList(getMasterResponse);
            ViewBag.reason = listreason;
            ViewBag.EMPID = EMPID;
            return View(Modal);
        }


        [HttpPost]
        public ActionResult ResignationRequest(string src, EmployeeExit.ResignationRequest Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long EMPID = 0;
            ViewBag.src = src;
            PostResult.SuccessMessage = "Data not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime dReleivingdate = new DateTime() ;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            if (Modal.iNoticePeriodServe == 0)
            {
                if (Modal.sNReleivingdate == null)
                {
                    ModelState.AddModelError("lReasonid", "Please fill relieving date");
                    PostResult.SuccessMessage = "Please fill relieving date";
                }
                if (string.IsNullOrEmpty(Modal.sReasonNoticePeriod))
                {
                    ModelState.AddModelError("lReasonid", "Please fill reason for serving full notice period");
                    PostResult.SuccessMessage = "Please fill reason for serving full notice period";
                }
            }
            if (Modal.iNoticePeriodServe == 0)
            {
                 dReleivingdate = Convert.ToDateTime(Modal.sNReleivingdate);
            }
            else
            {
                dReleivingdate = Convert.ToDateTime("1899-12-31");
            }
            

            string  ReasonNoticePeriod = "";
            if (!string.IsNullOrEmpty(Modal.sReasonNoticePeriod))
            {
                ReasonNoticePeriod = Modal.sReasonNoticePeriod;
            }
            if (ModelState.IsValid)
            {
              

                if (Command == "Save" )
                {
                    CommandResult Result = Common_SPU.FnSetResignation(Modal.lId.ToString(), EMPID, Modal.lReasonid, Modal.sComment, Modal.iNoticePeriodServe, dReleivingdate, Modal.iRelievingDay, ReasonNoticePeriod, "R",0);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    if(PostResult.ID==0)
                    {
                        PostResult.SuccessMessage = "Request Allready send HR";
                    }
                    


                }


              
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Resignationonbehalf (string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            EmployeeExit.ResignationRequest Modal = new EmployeeExit.ResignationRequest();
            Modal = exitEmployee.GetResignation(0, 0, "R");
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Exit_Reason";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> listreason = new List<MasterAll.List>();
            listreason = Master.GetMasterAllList(getMasterResponse);
            ViewBag.reason = listreason;
            ViewBag.EmpList = CommonSpecial.GetAllEmployeeOnbehalf();
            return View(Modal);

        }
        [HttpPost]
        public ActionResult _Resignationonbehalf(string src, EmployeeExit.ResignationRequest resignationRequest)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            EmployeeExit.ResignationRequest Modal = new EmployeeExit.ResignationRequest();
            Modal = exitEmployee.GetResignation(0, resignationRequest.lEmpid, "R");
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Exit_Reason";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> listreason = new List<MasterAll.List>();
            listreason = Master.GetMasterAllList(getMasterResponse);
            ViewBag.reason = listreason;
       
            return PartialView(Modal);

        }

        [HttpPost]
        public ActionResult _ResignationonbehalfSave(string src, EmployeeExit.ResignationRequest Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long EMPID = 0;
            ViewBag.src = src;
            PostResult.SuccessMessage = "Data not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime dReleivingdate = new DateTime();
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            if (Modal.lReasonid == 0)
            {
                ModelState.AddModelError("lReasonid", "Please Select Reason for Resignation");
                PostResult.SuccessMessage = "Please Select Reason for Resignation";
            }
            if (Modal.iNoticePeriodServe == 0)
            {
                if (Modal.sNReleivingdate == null)
                {
                    ModelState.AddModelError("lReasonid", "Please fill relieving date");
                    PostResult.SuccessMessage = "Please fill relieving date";
                }
                if (string.IsNullOrEmpty(Modal.sReasonNoticePeriod))
                {
                    ModelState.AddModelError("lReasonid", "Please fill reason for serving full notice period");
                    PostResult.SuccessMessage = "Please fill reason for serving full notice period";
                }
            }
            if (Modal.iNoticePeriodServe == 0)
            { 
                dReleivingdate = Convert.ToDateTime(Modal.sNReleivingdate);
            }
            else
            {
                dReleivingdate = Convert.ToDateTime("1899-12-31");
            }


            string ReasonNoticePeriod = "";
            if (!string.IsNullOrEmpty(Modal.sReasonNoticePeriod))
            {
                ReasonNoticePeriod = Modal.sReasonNoticePeriod;
            }
            if (ModelState.IsValid)
            {


                if (Command == "Save")
                {
                     CommandResult Result = Common_SPU.FnSetResignation(Modal.lId.ToString(), Modal.lEmpid, Modal.lReasonid, Modal.sComment, Modal.iNoticePeriodServe, dReleivingdate, Modal.iRelievingDay, ReasonNoticePeriod, "R", EMPID);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    if (PostResult.ID == 0)
                    {
                        PostResult.SuccessMessage = "Request Allready send HR";
                    }



                }



            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult HRApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ListTabs Modal = new ListTabs();
            Modal.Approve = 0;
            return View(Modal);

        }

        public ActionResult _HRApproval(string src, ListTabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<EmployeeExit.HRList> result = new List<EmployeeExit.HRList>();
            getResponse.Approve = Modal.Approve;
            result = exitEmployee.GetExit_HRList(Modal.Approve);
            ViewBag.Approve = Modal.Approve;
            return PartialView(result);
        }

        public ActionResult _ResignationHistory(string src, ListTabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Project.List> result = new List<Project.List>();
            getResponse.Approve = Modal.Approve;

            return PartialView(result);
        }

        public ActionResult HRApproveAction(string src, int? id)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = id;
            return View();

        }

        public ActionResult levelApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
            return View();

        }

        public ActionResult LevelApproveAction(string src, int? id,string Level)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = id;
            ViewBag.Level = Level;
            return View();

        }

        public ActionResult EmployeeApproveAction(string src, int? id)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = id;
            return View();

        }
        public ActionResult ExitProcessRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
            return View();

        }
        public ActionResult StartExitProcess(string src, int? id)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = id;
            return View();

        }
        public ActionResult HRRecordResulation(string src, int? id)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = id;
            return View();

        }
        public ActionResult NOCRequestList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
            return View();

        }
        public ActionResult AdminNOCRequestList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
            ViewBag.Doctype = "Admin";
            return View();

        }
        public ActionResult ITNOCRequestList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
            ViewBag.Doctype = "IT";
            return View();

        }
        public ActionResult HRNOCRequestList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
            ViewBag.Doctype = "HR";
            return View();

        }
        public ActionResult FinanceNOCList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
            ViewBag.Doctype = "Finance";
            return View();

        }
        public ActionResult HandoverTaskList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
           // ViewBag.Doctype = "Finance";
            return View();

        }
        public ActionResult Assigntask(string src,int ? id)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
            ViewBag.ID = id;
            return View();

        }
        public ActionResult AssigntaskSave(string src, int? id)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
            ViewBag.ID = id;
            return View();

        }
        public ActionResult OrganisationExit(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            EmployeeExit.ResignationRequest Modal = new EmployeeExit.ResignationRequest();
            Modal = exitEmployee.GetResignation(0, 0, "R");
            return View(Modal);

        }
        public ActionResult HeadFinanceNOCList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.EMPID = EMPID;
            ViewBag.Doctype = "HeadFinance";
            return View();

        }
    }
}