using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class LeaveController : Controller
    {
        ILeaveHelper Leave;
        GetResponse getResponse;
        long LoginID = 0;
        string IPAddress = "";
        public LeaveController()
        {
            getResponse = new GetResponse();
            Leave = new LeaveModal();
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
        public ActionResult TestCalender(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }


        public ActionResult LeaveDashboard(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.AttachmentRequiredCount = Leave.GetAttachmentRequiredCount();
            if (clsApplicationSetting.GetSessionValue("IsHOD") == "Y")
            {
                SeniorLeaveDashboard Modal = new SeniorLeaveDashboard();
                long EMPID = 0;
                long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
                Modal = Leave.GetSeniorLeaveDashboard(EMPID);

                return View("SeniorLeaveDashboard", Modal);
            }
            else
            {
                LeaveDashboard Modal = new LeaveDashboard();
                long EMPID = 0;
                long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
                Modal = Leave.GetLeaveDashboard(EMPID);

                return View(Modal);
            }
        }
        //public ActionResult SeniorLeaveDashboard(string src)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    LeaveDashboard Modal = new LeaveDashboard();
        //    long EMPID = 0;
        //    long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
        //    Modal = Leave.GetLeaveDashboard(EMPID);
        //    return View(Modal);
        //}

        public ActionResult _ViewLeaveDetails(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0, LeaveLogID = 0;
            DateTime dtDate;
            long.TryParse(GetQueryString[2], out EMPID);
            long.TryParse(GetQueryString[3], out LeaveLogID);
            DateTime.TryParse(GetQueryString[4], out dtDate);
            LeaveDetails Modal = new LeaveDetails();
            Modal = Leave.GetLeaveDetails(EMPID, LeaveLogID, dtDate);
            return PartialView(Modal);
        }
        public ActionResult _SetLeaveApprovalStatus(string src, ActionOnLeave Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.IsValidForEDApproval = Common_SPU.fnIsLeaveRequestForEDApproval(Modal.LeaveLogID);
            Modal.IsValidForEDApproval = (Modal.IsValidForEDApproval == "ED and HOD is Same" ? "" : Modal.IsValidForEDApproval);
            int edid = 0;
            int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select master_emp.ed_name from leave_log inner join master_emp on leave_log.emp_id=master_emp.id where leave_log.ID=" + Modal.LeaveLogID), out edid);
            if (clsApplicationSetting.GetSessionValue("EMPID").ToString() == edid.ToString())
            {
                Modal.isED = true;
            }
            return PartialView(Modal);
        }


        [AuthorizeFilter(ActionFor = "W")]
        [HttpPost]
        public JsonResult _SetLeaveApprovalStatus(ActionOnLeave Modal, string src, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            PostResult.ID = Modal.LeaveLogID;
            try
            {
                string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                int Approved = 0;
                int.TryParse(Command, out Approved);
                if ((Approved == 2 || Approved == 5) && string.IsNullOrEmpty(Modal.Reason))
                {
                    ModelState.AddModelError("Reason", "Please record your reason for rejection");
                    PostResult.SuccessMessage = "Please record your reason for rejection";
                }
                //else if ((Approved == 3) && string.IsNullOrEmpty(Modal.Reason))
                //{
                //    ModelState.AddModelError("Reason", "Reason can't be blank");
                //    PostResult.SuccessMessage = "Reason can't be blank";
                //}
                if (ModelState.IsValid)
                {
                    if (Approved == 10)
                    {
                        Common_SPU.fnSetRFC_LeaveRequest_Approve(Modal.LeaveLogID);
                        PostResult.Status = true;
                        PostResult.StatusCode = 10;
                        PostResult.SuccessMessage = "RFC successfully Accecpted";
                        TempData["Success"] = "Y";
                        TempData["SuccessMsg"] = PostResult.SuccessMessage;
                    }
                    else
                    {
                        string HODRemarks = "", EDRemarks = "";
                        if (Approved == 1 || Approved == 2 || Approved == 3)
                        {
                            HODRemarks = Modal.Reason;
                        }
                        else
                        {
                            HODRemarks = Modal.HODRemarks;
                        }
                        if (Approved == 4 || Approved == 5)
                        {
                            EDRemarks = Modal.Reason;
                        }
                        Common_SPU.fnSetLeaveApprovalHOD(Modal.LeaveLogID, Approved, HODRemarks, EDRemarks);
                        int sendcode = 0;
                        int.TryParse(Command, out sendcode);
                        PostResult.Status = true;
                        PostResult.StatusCode = sendcode;
                        PostResult.SuccessMessage = "Request Successfully Submitted";
                        TempData["Success"] = "Y";
                        TempData["SuccessMsg"] = PostResult.SuccessMessage;
                        // Fire Mail
                        Common_SPU.fnCreateMail_Leave(Modal.LeaveLogID);

                    }
                }
                else
                {
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during _SetLeaveApprovalStatus. The query was executed :", ex.ToString(), "fnSetLeaveApprovalHOD", "LeaveController", "LeaveController", "");

                PostResult.SuccessMessage = "Processed Not Done";
                TempData["Success"] = "N";
                TempData["SuccessMsg"] = PostResult.SuccessMessage;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }


        public ActionResult _LeaveBalance(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            ViewBag.Date = GetQueryString[2];
            DateTime.TryParse(GetQueryString[2], out MyDate);
            if (MyDate.Year == 0001)
            {
                MyDate = DateTime.Now;
            }
            long EMPIDDD = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);
            List<LeaveBalanceDetails> Modal = new List<LeaveBalanceDetails>();
            Modal = Leave.GetLeaveBalanceList(EMPIDDD, MyDate.Month, MyDate.Year);
            return PartialView(Modal);
        }

        public ActionResult ApplyLeave(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Leave Modal = new Leave();
            long EMPIDDD = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);
            Modal.LeaveTypeList = Leave.GetLeaveTypebyEmp(EMPIDDD.ToString());
            Modal.LeaveEmp = Leave.GetLeaveEmpDetails(EMPIDDD);
            if (Modal.LeaveEmp != null)
            {
                Modal.EmergencyContactNo = Modal.LeaveEmp.EmergencyContactNo;
                Modal.EmergencyContactName = Modal.LeaveEmp.EmergencyContactName;
                Modal.EmergencyContactRelation = Modal.LeaveEmp.EmergencyContactRelation;
            }
            List<LeaveTran> TranList = new List<LeaveTran>();
            LeaveTran TranObj = new LeaveTran();
            TranList.Add(TranObj);
            Modal.LeaveTranList = TranList;

            return View(Modal);
        }

        [HttpPost]

  
        public ActionResult ApplyLeave(string src, Leave Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            //
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long SaveID = 0, AttachmentRequired = 0;
            decimal SumLeaveHrs = 0;
            long TotalLeaveHours = 0;
            long CheckTotalLeaveHours = 0;
            bool IsValidaDate = true;
            string RES = "", CLMsg = "", ApplySandwichPolicy = "N";
            PostResult.SuccessMessage = "Apply Leave Failed";
            long EMPIDDD = 0, LeaveID = 0;
            string PastCLMsg = "";
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);

            string SQL = @"select top 1 SS_EmployeeSalary.workingHours from SS_EmployeeSalary left join master_all as tblSubcategory on tblSubcategory.id=SS_EmployeeSalary.subcategoryid and tblSubcategory.table_name='SS_Subcategory' where empid=" + EMPIDDD + " and   SS_EmployeeSalary.isdeleted=0 order by SS_EmployeeSalary.id desc";
            int WorkHours = Convert.ToInt32(clsDataBaseHelper.ExecuteSingleResult(SQL));

            string NSQL = @"select top 1 case when master_all.field_name1='6 Days' then 'Sunday' else 'Sunday,Saturday' end from SS_EmployeeSalary inner join master_all on SS_EmployeeSalary.Subcategoryid=master_all.id where SS_EmployeeSalary.Empid=" + EMPIDDD + " and  SS_EmployeeSalary.isdeleted=0  order by SS_EmployeeSalary.id desc";
            string WorkDay = clsDataBaseHelper.ExecuteSingleResult(NSQL);

            long.TryParse((Modal.LeaveTypeID ?? 0) == 0 ? Modal.LeaveTranList.OrderBy(x => x.LeaveType).Select(x => x.LeaveType).FirstOrDefault().ToString() : (Modal.LeaveTypeID ?? 0).ToString(), out LeaveID);
            DateTime StartDate, EndDate, ExpectedDeliveryDate;
            DateTime.TryParse(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).Select(x => x.LeaveDate).FirstOrDefault().ToString(), out StartDate);
            DateTime.TryParse(Modal.LeaveTranList.OrderByDescending(x => x.LeaveDate).Select(x => x.LeaveDate).FirstOrDefault().ToString(), out EndDate);
            DateTime.TryParse(Modal.ExpectedDeliveryDate, out ExpectedDeliveryDate);
            List<DateTime> leavelist = new List<DateTime>();
            if (WorkHours == 8)
            {
                leavelist = Modal.LeaveTranList.Where(x => x.LeaveType == "1" && x.LeaveHours == 8).Select(x => x.LeaveDate).ToList();
            }
            else if (WorkHours == 7)
            {
                leavelist = Modal.LeaveTranList.Where(x => x.LeaveType == "1" && x.LeaveHours == 7).Select(x => x.LeaveDate).ToList();
            }
            else if (WorkHours == 6)
            {
                leavelist = Modal.LeaveTranList.Where(x => x.LeaveType == "1" && x.LeaveHours == 6).Select(x => x.LeaveDate).ToList();
            }
            //leavelist = Modal.LeaveTranList.Where(x => x.LeaveType == "1").Select(x => x.LeaveDate).ToList();
            List<DateTime> CheckList = CommonSpecial.GetLeaveList(leavelist);
            List<LeaveTran> _listLeave = new List<LeaveTran>();
            if (CheckList.Count > 0)
            {
                foreach (var item in CheckList)
                {
                    LeaveTran _leaveTran = new LeaveTran();
                    _leaveTran.LeaveDate = item;
                    _listLeave.Add(_leaveTran);
                }
            }

            var Listleave = Modal.LeaveTranList.Where(item => _listLeave.Any(item2 => Convert.ToDateTime((item2.LeaveDate).ToString("dd/MMM/yyyy")) == Convert.ToDateTime((item.LeaveDate).ToString("dd/MMM/yyyy"))));
            SumLeaveHrs = Listleave.Sum(x => x.LeaveHours);
            PastCLMsg = Leave.ValidateCLSL(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList());

            int weekendDays = 0;
            //if(CheckList.Count==0 && Modal.LeaveTranList.Count>= 3) code comment sk 3092024
            if (CheckList.Count == 0 && leavelist.Count >= 3)
            {
                for (DateTime date = StartDate; date.Date <= EndDate.Date; date = date.AddDays(1))
                {
                    if (WorkDay == "Sunday,Saturday")
                    {
                        if ((date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday))
                        {
                            weekendDays++;
                        }
                    }
                    else
                    {
                        if (date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            weekendDays++;
                        }
                    }

                }
            }
            if (weekendDays > 0)
            {
                SumLeaveHrs = Modal.LeaveTranList.Sum(x => x.LeaveHours);
            }


            if (WorkHours == 8)
            {
                if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 24 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
                {
                    IsValidaDate = false;
                    PostResult.SuccessMessage = "Fitness Certificate is mandatory";
                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                }
            }
            else if (WorkHours == 7)
            {
                if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 21 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
                {
                    IsValidaDate = false;
                    PostResult.SuccessMessage = "Fitness Certificate is mandatory";
                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                }
            }
            else if (WorkHours == 6)
            {
                if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 18 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
                {
                    IsValidaDate = false;
                    PostResult.SuccessMessage = "Fitness Certificate is mandatory";
                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                }
            }
            if (Modal.LeaveTranList.Any(x => x.LeaveType == "5") || Modal.LeaveTranList.Any(x => x.LeaveType == "7"))
            {
                ApplySandwichPolicy = Modal.LeaveTranList.Any(x => x.LeaveType == "5") ? "Y" : "N";
                if (Modal.LeaveTranList.Any(x => x.LeaveType == "5") && ExpectedDeliveryDate.Year < 1900)
                {
                    PostResult.SuccessMessage = "Expected Delivery Date can't be blank";
                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                    IsValidaDate = false;
                }
                else if (Modal.LeaveTranList.Any(x => x.LeaveType == "7") && ExpectedDeliveryDate.Year < 1900)
                {
                    PostResult.SuccessMessage = "Expected Child birth date can't be blank";
                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                    IsValidaDate = false;
                }
                else if (Modal.LeaveTranList.Any(x => x.LeaveType == "7") && Modal.LeaveTranList.Any(x => x.LeaveDate < ExpectedDeliveryDate.Date))
                {
                    PostResult.SuccessMessage = "You Can't apply for Leave before " + ExpectedDeliveryDate.Date.ToString("dd-MMM-yyyy");
                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                    IsValidaDate = false;
                }
            }
            if (IsValidaDate)
            {
                DataSet ValidaDateDS = Common_SPU.fnGetWorkingDays(StartDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"), ApplySandwichPolicy);
                if (StartDate.Date > EndDate.Date)
                {
                    PostResult.SuccessMessage = "Oops! Leave date doesn't seem valid. End date should be after start date.";
                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);

                }
                else if (ValidaDateDS.Tables[0].Rows.Count == 0)
                {
                    PostResult.SuccessMessage = "You can't apply leave for week-off/ Holiday. Please verify the dates.";
                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                }
                else
                {
                    //DateTime[] dates = new DateTime[ValidaDateDS.Tables[0].Rows.Count];

                    //for (int i = 0; i < ValidaDateDS.Tables[0].Rows.Count; i++)
                    //{
                    //    dates[i] = Convert.ToDateTime(ValidaDateDS.Tables[0].Rows[i]["date"]).Date.ToString("dd-MMM-yyyy");
                    //}

                    DateTime[] dates = new DateTime[ValidaDateDS.Tables[0].Rows.Count];

                    for (int i = 0; i < ValidaDateDS.Tables[0].Rows.Count; i++)
                    {
                        dates[i] = Convert.ToDateTime(ValidaDateDS.Tables[0].Rows[i]["date"]).Date;
                    }

                    var a = Modal.LeaveTranList.Select(x => x.LeaveDate.Date).Except(dates);
                    if (a.Count() > 0)
                    {
                        var datess = string.Join(",", a.Select(x => x.Date.ToString("dd-MMM-yyyy")));
                        IsValidaDate = false;
                        PostResult.SuccessMessage = "You can't apply leave for week-off/ Holiday. Please verify the dates. (" + datess + " )";
                        ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                    }
                }
                if (IsValidaDate)
                {
                    RES = Leave.ValidateLeaveRequest(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList(), ExpectedDeliveryDate);
                    if (!string.IsNullOrEmpty(RES.Trim()))
                    {
                        PostResult.SuccessMessage = RES;
                        ModelState.AddModelError("Remarks", RES);
                    }

                    if (string.IsNullOrEmpty(RES.Trim()) && Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList().Where(x => x.LeaveType == "1").Count() > 0)
                    {
                        CLMsg = Leave.ValidateCLSL(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList());
                        if (!string.IsNullOrEmpty(CLMsg))
                        {
                            if (WorkHours == 8)
                            {
                                if (SumLeaveHrs >= 24)
                                {
                                    AttachmentRequired = 1;
                                }
                            }
                            else if (WorkHours == 7)
                            {
                                if (SumLeaveHrs >= 21)
                                {
                                    AttachmentRequired = 1;
                                }
                            }
                            else if (WorkHours == 6)
                            {
                                if (SumLeaveHrs >= 18)
                                {
                                    AttachmentRequired = 1;
                                }
                            }

                            // code commented by shailendra 10092024
                            if (CLMsg == "Past CL/SL")
                            {
                                if (Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null) && WorkHours == 8 && SumLeaveHrs >= 24)
                                {
                                    PostResult.SuccessMessage = "Fitness Certificate is mandatory";
                                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                                }
                                else if (Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null) && WorkHours == 7 && SumLeaveHrs >= 21)
                                {
                                    PostResult.SuccessMessage = "Fitness Certificate is mandatory";
                                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                                }
                                else if (Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null) && WorkHours == 6 && SumLeaveHrs >= 18)
                                {
                                    PostResult.SuccessMessage = "Fitness Certificate is mandatory";
                                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                                }
                                else
                                {
                                    AttachmentRequired = 2;
                                }
                            }
                            // comment by shailendra 30092024 
                            //if (CLMsg == "Medical Certificate" && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Medical Certificate").Any(x => x.UploadFile == null))
                            //{
                            //    PostResult.SuccessMessage = "Medical Certificate is mandatory";
                            //    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
                            //}
                            else if (CLMsg == "After Maternity Leave")
                            {
                                AttachmentRequired = 0;
                            }
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    // Validating Attachment
                    // Upload Attachment
                    bool AllAttachmentValid = true;
                    for (int i = 0; i < Modal.LeaveAttachmentList.Count; i++)
                    {
                        if (Modal.LeaveAttachmentList[i].UploadFile != null)
                        {
                            var RvFile = clsApplicationSetting.ValidateFile(Modal.LeaveAttachmentList[i].UploadFile);
                            if (!RvFile.IsValid)
                            {
                                AllAttachmentValid = false;
                                PostResult.SuccessMessage = RvFile.Message;
                                TempData["Success"] = (PostResult.Status ? "Y" : "N");
                                TempData["SuccessMsg"] = PostResult.SuccessMessage;
                                return Json(PostResult, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (AllAttachmentValid)
                    {

                        long.TryParse(Modal.LeaveTranList.Sum(x => x.LeaveHours).ToString(), out TotalLeaveHours);
                        SaveID = Common_SPU.fnSetLeaveLog(0, EMPIDDD, StartDate, EndDate, ExpectedDeliveryDate.Date, LeaveID, Modal.EmergencyContactNo, Modal.EmergencyContactName, Modal.Remarks, TotalLeaveHours.ToString(), "", AttachmentRequired, Modal.EmergencyContactRelation);

                        if (SaveID > 0)
                        {
                            int count = 0;
                            decimal dHrs = 0;
                            DateTime dtDate = Convert.ToDateTime(StartDate);

                            foreach (var item in Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList())
                            {
                                count++;

                                if (dtDate.Month != Convert.ToDateTime(item.LeaveDate).Month)
                                {
                                    clsDataBaseHelper.ExecuteNonQuery("update leave_log  set AttachmentRequired=0, end_date='" + dtDate.ToString("yyyy-MM-dd") + "',leave_hours=" + dHrs + " where id=" + SaveID);
                                    // Upload Attachment
                                    for (int i = 0; i < Modal.LeaveAttachmentList.Count; i++)
                                    {
                                        if (Modal.LeaveAttachmentList[i].UploadFile != null)
                                        {
                                            string FileExt = System.IO.Path.GetExtension(Modal.LeaveAttachmentList[i].UploadFile.FileName).ToLower();

                                            long AttachmentID = 0;
                                            AttachmentID = Common_SPU.fnSetAttachments(0, Modal.LeaveAttachmentList[i].AttachmentType, FileExt, Modal.LeaveAttachmentList[i].AttachmentType, SaveID.ToString(), "ApplyLeave");
                                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + AttachmentID + FileExt)))
                                            {
                                                System.IO.File.Delete("~/Attachments/" + AttachmentID + FileExt);
                                            }
                                            Modal.LeaveAttachmentList[i].UploadFile.SaveAs(Server.MapPath("~/Attachments/" + AttachmentID + FileExt));
                                        }
                                    }
                                    Common_SPU.fnCreateMail_Leave(SaveID);
                                    count = 1;
                                    SaveID = Common_SPU.fnSetLeaveLog(0, EMPIDDD, item.LeaveDate, EndDate, ExpectedDeliveryDate.Date, LeaveID, Modal.EmergencyContactNo, Modal.EmergencyContactName, Modal.Remarks, (TotalLeaveHours - dHrs).ToString(), "", AttachmentRequired, Modal.EmergencyContactRelation);
                                    dHrs = dHrs + item.LeaveHours;
                                    Common_SPU.fnSetLeaveDet(SaveID, item.LeaveDate, item.LeaveHours.ToString(), count, Convert.ToInt32(item.LeaveType), Modal.Remarks);
                                }
                                else
                                {
                                    dHrs = dHrs + item.LeaveHours;
                                    Common_SPU.fnSetLeaveDet(SaveID, item.LeaveDate, item.LeaveHours.ToString(), count, Convert.ToInt32(item.LeaveType), Modal.Remarks);
                                }
                                dtDate = Convert.ToDateTime(item.LeaveDate);
                            }
                            Common_SPU.fnDelLeaveDetLog(SaveID, count);

                            // Upload Attachment

                            for (int i = 0; i < Modal.LeaveAttachmentList.Count; i++)
                            {
                                if (Modal.LeaveAttachmentList[i].UploadFile != null)
                                {
                                    string FileExt = System.IO.Path.GetExtension(Modal.LeaveAttachmentList[i].UploadFile.FileName).ToLower();

                                    long AttachmentID = 0;
                                    AttachmentID = Common_SPU.fnSetAttachments(0, Modal.LeaveAttachmentList[i].AttachmentType, FileExt, Modal.LeaveAttachmentList[i].AttachmentType, SaveID.ToString(), "ApplyLeave");
                                    if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + AttachmentID + FileExt)))
                                    {
                                        System.IO.File.Delete("~/Attachments/" + AttachmentID + FileExt);
                                    }
                                    Modal.LeaveAttachmentList[i].UploadFile.SaveAs(Server.MapPath("~/Attachments/" + AttachmentID + FileExt));
                                }
                            }

                            // Fire Mail
                            Common_SPU.fnCreateMail_Leave(SaveID);
                            PostResult.Status = true;

                            //if (!string.IsNullOrEmpty(CLMsg))
                            //{
                            //    PostResult.SuccessMessage = "Apply Leave Successfully. You are requested to attach Medical and physical certificate before submitting activity log";

                            //}
                            if (SaveID > 0)
                            {
                                PostResult.SuccessMessage = "Your leave request has been successfully submitted.";
                            }
                            if (WorkHours == 8)
                            {
                                if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 24 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
                                {
                                    PostResult.SuccessMessage = "Apply Leave Successfully. You are requested to attach Medical and physical certificate before submitting activity log";
                                }
                            }
                            else if (WorkHours == 7)
                            {
                                if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 21 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
                                {
                                    PostResult.SuccessMessage = "Apply Leave Successfully. You are requested to attach Medical and physical certificate before submitting activity log";
                                }
                            }
                            else if (WorkHours == 6)
                            {
                                if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 18 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
                                {
                                    PostResult.SuccessMessage = "Apply Leave Successfully. You are requested to attach Medical and physical certificate before submitting activity log";
                                }
                            }
                            else
                            {
                                PostResult.SuccessMessage = "Your leave request has been successfully submitted";


                            }
                            TempData["Success"] = (PostResult.Status ? "Y" : "N");
                            TempData["SuccessMsg"] = PostResult.SuccessMessage;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            TempData["Success"] = (PostResult.Status ? "Y" : "N");
            TempData["SuccessMsg"] = PostResult.SuccessMessage;
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }




        //public ActionResult ApplyLeave(string src, Leave Modal, string Command)
        //{
        //    PostResponse PostResult = new PostResponse();
        //    //
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    long SaveID = 0, AttachmentRequired = 0;
        //    decimal SumLeaveHrs = 0;
        //    long TotalLeaveHours = 0;
        //    long CheckTotalLeaveHours = 0;
        //    bool IsValidaDate = true;
        //    string RES = "", CLMsg = "", ApplySandwichPolicy = "N";
        //    PostResult.SuccessMessage = "Apply Leave Failed";
        //    long EMPIDDD = 0, LeaveID = 0;
        //    string PastCLMsg = "";
        //    long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);

        //    string SQL = @"select top 1 SS_EmployeeSalary.workingHours from SS_EmployeeSalary left join master_all as tblSubcategory on tblSubcategory.id=SS_EmployeeSalary.subcategoryid and tblSubcategory.table_name='SS_Subcategory' where empid=" + EMPIDDD + " and   SS_EmployeeSalary.isdeleted=0 order by SS_EmployeeSalary.id desc";
        //    int WorkHours = Convert.ToInt32(clsDataBaseHelper.ExecuteSingleResult(SQL));

        //    string NSQL = @"select top 1 case when master_all.field_name1='6 Days' then 'Sunday' else 'Sunday,Saturday' end from SS_EmployeeSalary inner join master_all on SS_EmployeeSalary.Subcategoryid=master_all.id where SS_EmployeeSalary.Empid=" + EMPIDDD + " and  SS_EmployeeSalary.isdeleted=0  order by SS_EmployeeSalary.id desc";
        //    string WorkDay = clsDataBaseHelper.ExecuteSingleResult(NSQL);

        //    long.TryParse((Modal.LeaveTypeID ?? 0) == 0 ? Modal.LeaveTranList.OrderBy(x => x.LeaveType).Select(x => x.LeaveType).FirstOrDefault().ToString() : (Modal.LeaveTypeID ?? 0).ToString(), out LeaveID);
        //    DateTime StartDate, EndDate, ExpectedDeliveryDate;
        //    DateTime.TryParse(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).Select(x => x.LeaveDate).FirstOrDefault().ToString(), out StartDate);
        //    DateTime.TryParse(Modal.LeaveTranList.OrderByDescending(x => x.LeaveDate).Select(x => x.LeaveDate).FirstOrDefault().ToString(), out EndDate);
        //    DateTime.TryParse(Modal.ExpectedDeliveryDate, out ExpectedDeliveryDate);
        //    List<DateTime> leavelist = new List<DateTime>();
        //    if (WorkHours == 8)
        //    {
        //        leavelist = Modal.LeaveTranList.Where(x => x.LeaveType == "1" && x.LeaveHours == 8).Select(x => x.LeaveDate).ToList();
        //    }
        //    else if (WorkHours == 7)
        //    {
        //        leavelist = Modal.LeaveTranList.Where(x => x.LeaveType == "1" && x.LeaveHours == 7).Select(x => x.LeaveDate).ToList();
        //    }
        //    else if (WorkHours == 6)
        //    {
        //        leavelist = Modal.LeaveTranList.Where(x => x.LeaveType == "1" && x.LeaveHours == 6).Select(x => x.LeaveDate).ToList();
        //    }
        //    //leavelist = Modal.LeaveTranList.Where(x => x.LeaveType == "1").Select(x => x.LeaveDate).ToList();
        //    List<DateTime> CheckList = CommonSpecial.GetLeaveList(leavelist);
        //    List<LeaveTran> _listLeave = new List<LeaveTran>();
        //    if (CheckList.Count > 0)
        //    {
        //        foreach (var item in CheckList)
        //        {
        //            LeaveTran _leaveTran = new LeaveTran();
        //            _leaveTran.LeaveDate = item;
        //            _listLeave.Add(_leaveTran);
        //        }
        //    }

        //    var Listleave = Modal.LeaveTranList.Where(item => _listLeave.Any(item2 => Convert.ToDateTime((item2.LeaveDate).ToString("dd/MMM/yyyy")) == Convert.ToDateTime((item.LeaveDate).ToString("dd/MMM/yyyy"))));
        //    SumLeaveHrs = Listleave.Sum(x => x.LeaveHours);
        //    PastCLMsg = Leave.ValidateCLSL(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList());

        //    int weekendDays = 0;
        //    if(CheckList.Count==0 && Modal.LeaveTranList.Count>= 3)
        //    {
        //        for (DateTime date = StartDate; date.Date <= EndDate.Date; date = date.AddDays(1))
        //        {
        //            if (WorkDay == "Sunday,Saturday")
        //            {
        //                if ((date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday))
        //                {
        //                    weekendDays++;
        //                }
        //            }
        //            else
        //            {
        //                if (date.DayOfWeek == DayOfWeek.Sunday)
        //                {
        //                    weekendDays++;
        //                }
        //            }

        //        }
        //    }
        //    if (weekendDays > 0)
        //    {
        //        SumLeaveHrs = Modal.LeaveTranList.Sum(x => x.LeaveHours);
        //    }


        //    if (WorkHours == 8)
        //    {
        //        if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 24 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
        //        {
        //            IsValidaDate = false;
        //            PostResult.SuccessMessage = "Fitness Certificate is mandatory";
        //            ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
        //        }
        //    }
        //    else if (WorkHours == 7)
        //    {
        //        if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 21 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
        //        {
        //            IsValidaDate = false;
        //            PostResult.SuccessMessage = "Fitness Certificate is mandatory";
        //            ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
        //        }
        //    }
        //    else if (WorkHours == 6)
        //    {
        //        if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 18 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
        //        {
        //            IsValidaDate = false;
        //            PostResult.SuccessMessage = "Fitness Certificate is mandatory";
        //            ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
        //        }
        //    }
        //    if (Modal.LeaveTranList.Any(x => x.LeaveType == "5") || Modal.LeaveTranList.Any(x => x.LeaveType == "7"))
        //    {
        //        ApplySandwichPolicy = Modal.LeaveTranList.Any(x => x.LeaveType == "5") ? "Y" : "N";
        //        if (Modal.LeaveTranList.Any(x => x.LeaveType == "5") && ExpectedDeliveryDate.Year < 1900)
        //        {
        //            PostResult.SuccessMessage = "Expected Delivery Date can't be blank";
        //            ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
        //            IsValidaDate = false;
        //        }
        //        else if (Modal.LeaveTranList.Any(x => x.LeaveType == "7") && ExpectedDeliveryDate.Year < 1900)
        //        {
        //            PostResult.SuccessMessage = "Expected Child birth date can't be blank";
        //            ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
        //            IsValidaDate = false;
        //        }
        //        else if (Modal.LeaveTranList.Any(x => x.LeaveType == "7") && Modal.LeaveTranList.Any(x => x.LeaveDate < ExpectedDeliveryDate.Date))
        //        {
        //            PostResult.SuccessMessage = "You Can't apply for Leave before " + ExpectedDeliveryDate.Date.ToString("dd-MMM-yyyy");
        //            ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
        //            IsValidaDate = false;
        //        }
        //    }
        //    if (IsValidaDate)
        //    {
        //        DataSet ValidaDateDS = Common_SPU.fnGetWorkingDays(StartDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"), ApplySandwichPolicy);
        //        if (StartDate.Date > EndDate.Date)
        //        {
        //            PostResult.SuccessMessage = "Oops! Leave date doesn't seem valid. End date should be after start date.";
        //            ModelState.AddModelError("Remarks", PostResult.SuccessMessage);

        //        }
        //        else if (ValidaDateDS.Tables[0].Rows.Count == 0)
        //        {
        //            PostResult.SuccessMessage = "You can't apply leave for week-off/ Holiday. Please verify the dates.";
        //            ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
        //        }
        //        else
        //        {
        //            DateTime[] dates = new DateTime[ValidaDateDS.Tables[0].Rows.Count];
        //            for (int i = 0; i < ValidaDateDS.Tables[0].Rows.Count; i++)
        //            {
        //                dates[i] = Convert.ToDateTime(ValidaDateDS.Tables[0].Rows[i]["date"]).Date;
        //            }
        //            var a = Modal.LeaveTranList.Select(x => x.LeaveDate.Date).Except(dates);
        //            if (a.Count() > 0)
        //            {
        //                var datess = string.Join(",", a.Select(x => x.Date.ToString("dd-MMM-yyyy")));
        //                IsValidaDate = false;
        //                PostResult.SuccessMessage = "You can't apply leave for week-off/ Holiday. Please verify the dates. (" + datess + " )";
        //                ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
        //            }
        //        }
        //        if (IsValidaDate)
        //        {
        //            RES = Leave.ValidateLeaveRequest(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList(), ExpectedDeliveryDate);
        //            if (!string.IsNullOrEmpty(RES.Trim()))
        //            {
        //                PostResult.SuccessMessage = RES;
        //                ModelState.AddModelError("Remarks", RES);
        //            }

        //            if (string.IsNullOrEmpty(RES.Trim()) && Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList().Where(x => x.LeaveType == "1").Count() > 0)
        //            {
        //                CLMsg = Leave.ValidateCLSL(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList());
        //                if (!string.IsNullOrEmpty(CLMsg))
        //                {
        //                    AttachmentRequired = 1;
        //                    // code commented by shailendra 10092024
        //                    //if (CLMsg == "Past CL/SL")
        //                    //{
        //                    //    if (Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
        //                    //    {
        //                    //        PostResult.SuccessMessage = "Fitness Certificate is mandatory";
        //                    //        ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        AttachmentRequired = 2;
        //                    //    }
        //                    //}
        //                    if (CLMsg == "Medical Certificate" && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Medical Certificate").Any(x => x.UploadFile == null))
        //                    {
        //                        PostResult.SuccessMessage = "Medical Certificate is mandatory";
        //                        ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
        //                    }
        //                    else if (CLMsg == "After Maternity Leave")
        //                    {
        //                        AttachmentRequired = 0;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        if (Command == "Add")
        //        {
        //            // Validating Attachment
        //            // Upload Attachment
        //            bool AllAttachmentValid = true;
        //            for (int i = 0; i < Modal.LeaveAttachmentList.Count; i++)
        //            {
        //                if (Modal.LeaveAttachmentList[i].UploadFile != null)
        //                {
        //                    var RvFile = clsApplicationSetting.ValidateFile(Modal.LeaveAttachmentList[i].UploadFile);
        //                    if (!RvFile.IsValid)
        //                    {
        //                        AllAttachmentValid = false;
        //                        PostResult.SuccessMessage = RvFile.Message;
        //                        TempData["Success"] = (PostResult.Status ? "Y" : "N");
        //                        TempData["SuccessMsg"] = PostResult.SuccessMessage;
        //                        return Json(PostResult, JsonRequestBehavior.AllowGet);
        //                    }
        //                }
        //            }
        //            if (AllAttachmentValid)
        //            {

        //                long.TryParse(Modal.LeaveTranList.Sum(x => x.LeaveHours).ToString(), out TotalLeaveHours);
        //                SaveID = Common_SPU.fnSetLeaveLog(0, EMPIDDD, StartDate, EndDate, ExpectedDeliveryDate.Date, LeaveID, Modal.EmergencyContactNo, Modal.EmergencyContactName, Modal.Remarks, TotalLeaveHours.ToString(), "", AttachmentRequired, Modal.EmergencyContactRelation);

        //                if (SaveID > 0)
        //                {
        //                    int count = 0;
        //                    decimal dHrs = 0;
        //                    DateTime dtDate = Convert.ToDateTime(StartDate);

        //                    foreach (var item in Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList())
        //                    {
        //                        count++;

        //                        if (dtDate.Month != Convert.ToDateTime(item.LeaveDate).Month)
        //                        {
        //                            clsDataBaseHelper.ExecuteNonQuery("update leave_log  set AttachmentRequired=0, end_date='" + dtDate.ToString("yyyy-MM-dd") + "',leave_hours=" + dHrs + " where id=" + SaveID);
        //                            // Upload Attachment
        //                            for (int i = 0; i < Modal.LeaveAttachmentList.Count; i++)
        //                            {
        //                                if (Modal.LeaveAttachmentList[i].UploadFile != null)
        //                                {
        //                                    string FileExt = System.IO.Path.GetExtension(Modal.LeaveAttachmentList[i].UploadFile.FileName).ToLower();

        //                                    long AttachmentID = 0;
        //                                    AttachmentID = Common_SPU.fnSetAttachments(0, Modal.LeaveAttachmentList[i].AttachmentType, FileExt, Modal.LeaveAttachmentList[i].AttachmentType, SaveID.ToString(), "ApplyLeave");
        //                                    if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + AttachmentID + FileExt)))
        //                                    {
        //                                        System.IO.File.Delete("~/Attachments/" + AttachmentID + FileExt);
        //                                    }
        //                                    Modal.LeaveAttachmentList[i].UploadFile.SaveAs(Server.MapPath("~/Attachments/" + AttachmentID + FileExt));
        //                                }
        //                            }
        //                            Common_SPU.fnCreateMail_Leave(SaveID);
        //                            count = 1;
        //                            SaveID = Common_SPU.fnSetLeaveLog(0, EMPIDDD, item.LeaveDate, EndDate, ExpectedDeliveryDate.Date, LeaveID, Modal.EmergencyContactNo, Modal.EmergencyContactName, Modal.Remarks, (TotalLeaveHours - dHrs).ToString(), "", AttachmentRequired, Modal.EmergencyContactRelation);
        //                            dHrs = dHrs + item.LeaveHours;
        //                            Common_SPU.fnSetLeaveDet(SaveID, item.LeaveDate, item.LeaveHours.ToString(), count, Convert.ToInt32(item.LeaveType), Modal.Remarks);
        //                        }
        //                        else
        //                        {
        //                            dHrs = dHrs + item.LeaveHours;
        //                            Common_SPU.fnSetLeaveDet(SaveID, item.LeaveDate, item.LeaveHours.ToString(), count, Convert.ToInt32(item.LeaveType), Modal.Remarks);
        //                        }
        //                        dtDate = Convert.ToDateTime(item.LeaveDate);
        //                    }
        //                    Common_SPU.fnDelLeaveDetLog(SaveID, count);

        //                    // Upload Attachment

        //                    for (int i = 0; i < Modal.LeaveAttachmentList.Count; i++)
        //                    {
        //                        if (Modal.LeaveAttachmentList[i].UploadFile != null)
        //                        {
        //                            string FileExt = System.IO.Path.GetExtension(Modal.LeaveAttachmentList[i].UploadFile.FileName).ToLower();

        //                            long AttachmentID = 0;
        //                            AttachmentID = Common_SPU.fnSetAttachments(0, Modal.LeaveAttachmentList[i].AttachmentType, FileExt, Modal.LeaveAttachmentList[i].AttachmentType, SaveID.ToString(), "ApplyLeave");
        //                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + AttachmentID + FileExt)))
        //                            {
        //                                System.IO.File.Delete("~/Attachments/" + AttachmentID + FileExt);
        //                            }
        //                            Modal.LeaveAttachmentList[i].UploadFile.SaveAs(Server.MapPath("~/Attachments/" + AttachmentID + FileExt));
        //                        }
        //                    }

        //                    // Fire Mail
        //                    Common_SPU.fnCreateMail_Leave(SaveID);
        //                    PostResult.Status = true;

        //                    //if (!string.IsNullOrEmpty(CLMsg))
        //                    //{
        //                    //    PostResult.SuccessMessage = "Apply Leave Successfully. You are requested to attach Medical and physical certificate before submitting activity log";

        //                    //}
        //                    if (SaveID>0)
        //                    {
        //                        PostResult.SuccessMessage = "Your leave request has been successfully submitted.";
        //                    }
        //                    if (WorkHours == 8)
        //                    {
        //                        if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 24 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
        //                        {
        //                            PostResult.SuccessMessage = "Apply Leave Successfully. You are requested to attach Medical and physical certificate before submitting activity log";
        //                        }
        //                    }
        //                    else if (WorkHours == 7)
        //                    {
        //                        if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 21 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
        //                        {
        //                            PostResult.SuccessMessage = "Apply Leave Successfully. You are requested to attach Medical and physical certificate before submitting activity log";
        //                        }
        //                    }
        //                    else if (WorkHours == 6)
        //                    {
        //                        if (PastCLMsg == "Past CL/SL" && SumLeaveHrs >= 18 && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
        //                        {
        //                            PostResult.SuccessMessage = "Apply Leave Successfully. You are requested to attach Medical and physical certificate before submitting activity log";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        PostResult.SuccessMessage = "Your leave request has been successfully submitted";


        //                    }
        //                    TempData["Success"] = (PostResult.Status ? "Y" : "N");
        //                    TempData["SuccessMsg"] = PostResult.SuccessMessage;
        //                    return Json(PostResult, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //        }
        //    }
        //    TempData["Success"] = (PostResult.Status ? "Y" : "N");
        //    TempData["SuccessMsg"] = PostResult.SuccessMessage;
        //    return Json(PostResult, JsonRequestBehavior.AllowGet);

        //}


        public ActionResult MyLeaveRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now;
            AllListModal Modal = new AllListModal();
            return View(Modal);

        }
        [HttpPost]
        public ActionResult _MyLeaveRequest(string src, AllListModal Modal, string Command)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet ds = new DataSet();
            ViewBag.Approve = Modal.Approve;
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ds = Common_SPU.fnGetLeaveLog(EMPID, Modal.Approve);
            return PartialView(ds);
        }


        public ActionResult LeaveRequestApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now;
            AllListModal Modal = new AllListModal();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _LeaveRequestApproval(string src, AllListModal Modal)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            long EMPIDDD = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetLeaveLogSenior(EMPIDDD, Modal.Approve);
            return PartialView(ds);
        }
        public ActionResult LeaveNonMITR(string src)
        {
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            LeaveNonMitrMonth Modal = new LeaveNonMitrMonth();

            DateTime MyDate = DateTime.Now;
            getResponse.ID = EMPID;
            getResponse.Doctype = "EmployeeNonMITR";
            Modal.EmpList = ClsCommon.GetDropDownList(getResponse);
            getResponse.Doctype = "LeaveALL";
            Modal.LeaveList = ClsCommon.GetDropDownList(getResponse);
            if (GetQueryString.Length > 2)
            {
                DateTime.TryParse(GetQueryString[2], out MyDate);
            }
            Modal.Month = MyDate.ToString("yyyy-MM");
            Modal.LeaveTypeids = "1,4";
            return View(Modal);
        }
        [HttpPost]

        public ActionResult _LeaveNonMITR(LeaveNonMitrMonth Modal, string src, string Command, FormCollection Form)
        {
            string Tab = Form.GetValue("hfTab").AttemptedValue;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            DateTime MyDate;
            Modal.Empid = EMPID;
            Modal.Month = Modal.Month + "-01";
            DateTime.TryParse(Modal.Month, out MyDate);
            IActivityHelper Activity;
            Activity = new ActivityModal();
            if (Tab == "DAILYLOG")
            {

                //List<DailyLogNonMitr> modal = new List<DailyLogNonMitr>();
                DailyLogNonMitr modal = new DailyLogNonMitr();
                ViewBag.SelectedDate = MyDate;
                GetResponse getDropDown = new GetResponse();
                getDropDown.Doctype = "projectactivity";
                ViewBag.ActivityList = ClsCommon.GetDropDownList(getDropDown);
                ViewBag.ProjectList = Activity.GetActivityProjectsList(MyDate);
                //modal = Activity.GetDailyLogNonMitrList(Modal.Month, Modal.Empids);
                modal = Activity.GetDailyLogNonMitr(Modal.Month, Modal.Empids);
                return PartialView("~/Views/Leave/_DailyLogNonMitr.cshtml", modal);
            }
            else if (Tab == "MONTHLYLOG")
            {
                //DataSet Ds = new DataSet();
                //ViewBag.SelectedDate = MyDate;               
                //Ds = Common_SPU.fnGetActivityLogNonMITR(MyDate.Month, MyDate.Year, Modal.Empids,"");
                MonthlyLogNonMitr modal = new MonthlyLogNonMitr();
                ViewBag.SelectedDate = MyDate;
                ViewBag.TAB = Tab;
                modal = Activity.GetMonthlyLogNonMitr(Modal.Month, Modal.Empids);
                return PartialView("~/Views/Leave/_MonthlyActiveLogNonMitr.cshtml", modal);
            }
            else if (Tab == "SUBMITTED")
            {
                MonthlyLogNonMitr modal = new MonthlyLogNonMitr();
                ViewBag.SelectedDate = MyDate;
                ViewBag.TAB = Tab;
                modal = Activity.GetMonthlyLogNonMitr(Modal.Month, Modal.Empids, "SUBMITTED");
                return PartialView("~/Views/Leave/_MonthlyActiveLogNonMitr.cshtml", modal);
            }
            else
            {
                List<LeaveNonMITRAdd> modal = new List<LeaveNonMITRAdd>();
                ViewBag.SelectedDate = MyDate;
                ViewBag.HolidayList = Activity.GetHolidayLeaveDailyLog(Modal.Empids, Modal.Month).Where(n => n.HolidayType == "Holiday").ToList();
                modal = Leave.GetLeaveNonMITRAddList(Modal);
                return PartialView(modal);
            }

        }
        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _RequestForCompensatoryOffNonMitr(string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string Empids = GetQueryString[3];
            List<RequestCompensatoryOffList> ViweModal = new List<RequestCompensatoryOffList>();
            DateTime Date;
            DateTime.TryParse(GetQueryString[2], out Date);
            IActivityHelper Activity;
            Activity = new ActivityModal();
            ViweModal = Activity.GetCompensatoryOffNonMITRList(Date.Month, Date.Year, Empids);
            return PartialView(ViweModal);
        }
        public ActionResult _LeaveAttachmentNonMitr(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string Empid = GetQueryString[2];
            string Leaveid = GetQueryString[3];
            string Month = GetQueryString[4];
            string id = GetQueryString[5];
            ViewBag.Empid = GetQueryString[2];
            ViewBag.Leaveid = GetQueryString[3];
            ViewBag.Month = GetQueryString[4];
            ViewBag.id = GetQueryString[5];
            string tablename = id == "0" ? Empid + "#" + Leaveid + "#" + Month : "ApplyLeave";
            // string tablename = Empid + "#" + Leaveid + "#" + Month ;
            List<Attachments> modal = new List<Attachments>();
            modal = ClsCommon.GetAttachmentList(0, id, tablename);

            string[] AttachmentType = new string[2] { "Medical Certificate", "Fitness Certificate" };
            foreach (var item in AttachmentType)
            {
                if (modal.Count == 0)
                {
                    Attachments objAttch = new Attachments();
                    objAttch.FileName = item;
                    modal.Add(objAttch);

                }
                else if (modal.Where(n => n.FileName == item).ToList().Count == 0)
                {
                    Attachments objAttch = new Attachments();
                    objAttch.FileName = item;
                    modal.Add(objAttch);
                }

            }
            //return PartialView(Modal);
            return PartialView("~/Views/Leave/_LeaveAttachmentNonMitr.cshtml", modal);

        }
        [HttpPost]
        public ActionResult _LeaveAttachmentNonMitr(List<Attachments> Modal, string src, string Command, FormCollection Collection)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long SaveID = 0;
            PostResult.SuccessMessage = "Apply Leave Failed";

            long EMPIDDD = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);
            if (!Modal.Any(x => x.UploadFile != null))
            {
                PostResult.SuccessMessage = "Please Upload Attachment";
                ModelState.AddModelError("[0].UploadFile", PostResult.SuccessMessage);
            }
            string Empid = Collection.GetValue("Empid").AttemptedValue;
            string Leaveid = Collection.GetValue("Leaveid").AttemptedValue;
            string Month = Collection.GetValue("Month").AttemptedValue;
            string id = Collection.GetValue("id").AttemptedValue;
            string tablename = id == "0" ? Empid + "#" + Leaveid + "#" + Month : "ApplyLeave";
            SaveID = Convert.ToInt64(id);
            // string tablename =  Empid + "#" + Leaveid + "#" + Month ;
            PostResult.AdditionalMessage = Empid + "_" + Leaveid;
            // Upload Attachment
            long CountUploaded = 0;
            if (ModelState.IsValid)
            {
                for (int i = 0; i < Modal.Count; i++)
                {
                    if (Modal[i].UploadFile != null)
                    {
                        string FileExt = System.IO.Path.GetExtension(Modal[i].UploadFile.FileName).ToLower();

                        long AttachmentID = 0;
                        AttachmentID = Common_SPU.fnSetAttachments(0, Modal[i].FileName.ToString(), FileExt, Modal[i].FileName.ToString(), SaveID.ToString(), tablename);
                        if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + AttachmentID + FileExt)))
                        {
                            System.IO.File.Delete("~/Attachments/" + AttachmentID + FileExt);
                        }
                        Modal[i].UploadFile.SaveAs(Server.MapPath("~/Attachments/" + AttachmentID + FileExt));
                        CountUploaded = CountUploaded + AttachmentID;
                    }
                }
            }

            if (CountUploaded > 0)
            {
                PostResult.SuccessMessage = "Attachment Saved";
                PostResult.Status = true;
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _SaveLeaveNonMITR(List<LeaveNonMITRAdd> Modal, string src, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long SaveID = 0;
            int AttachmentRequired = 0;
            //long TotalLeaveHours = 0;
            //bool IsValidaDate = true;
            //string RES = "", CLMsg = "", ApplySandwichPolicy = "N";
            PostResult.SuccessMessage = "Apply Leave Failed";
            long EMPIDDD = 0;
            int CLSLCount = 0, CLSLStart = 0, IsContinue = 0;
            int AttachmentReqLeaveCount = 4;
            string DayName = "";
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);
            IActivityHelper Activity;
            Activity = new ActivityModal();

            if (!Modal.Any(x => x.isSelect == 1))
            {
                PostResult.SuccessMessage = "Please Make a Selection";
                ModelState.AddModelError("[0].isSelect", PostResult.SuccessMessage);
            }

            string EmpNameStr = "";

            foreach (var item in Modal.Where(x => x.isSelect == 1))
            {
                List<HolidayDailyLogNonMitr> HolidayList = new List<HolidayDailyLogNonMitr>();
                HolidayList = Activity.GetHolidayLeaveDailyLog(item.Empid.ToString(), item.LeaveDate.ToString("yyyy-MM-dd"));
                CLSLCount = 0; CLSLStart = 0; IsContinue = 0; DayName = "";
                for (int i = 1; i <= DateTime.DaysInMonth(item.LeaveDate.Year, item.LeaveDate.Month); i++)
                {
                    DayName = Convert.ToDateTime(item.LeaveDate.ToString("yyyy-MM") + "-" + (i < 10 ? "0" + i.ToString() : i.ToString())).ToString("dddd");
                    switch (i)
                    {
                        case 1:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day1).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day1).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            break;
                        case 2:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day2).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day2).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }

                            }
                            break;
                        case 3:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day3).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day3).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }

                            }

                            break;
                        case 4:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day4).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day4).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }

                            }

                            break;
                        case 5:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day5).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day5).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }

                            }

                            break;
                        case 6:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day6).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day6).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }

                            }
                            break;
                        case 7:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day7).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day7).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }

                            }
                            break;
                        case 8:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day8).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day8).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }

                            }
                            break;
                        case 9:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day9).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day9).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 10:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day10).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day10).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 11:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day11).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day11).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 12:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day12).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day12).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 13:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day13).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day13).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 14:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day14).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day14).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 15:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day15).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day15).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 16:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day16).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day16).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 17:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day17).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day17).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 18:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day18).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day18).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 19:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day19).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day19).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 20:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day20).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day20).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 21:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day21).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day21).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 22:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day22).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day22).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 23:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day23).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day23).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 24:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day24).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day24).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 25:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day25).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day25).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 26:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day26).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day26).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 27:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day27).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day27).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 28:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day28).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day28).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 29:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day29).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day29).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 30:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day30).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day30).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        case 31:
                            if ((Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID == 1 && n.IsAttachment == 0).Select(x => x.Day31).FirstOrDefault() > 0) && HolidayList.Where(n => n.DayNo == i.ToString()).ToList().Count == 0)
                            {
                                CLSLStart = CLSLStart == 0 ? i : CLSLStart;
                                CLSLCount = CLSLCount + 1;
                                IsContinue = IsContinue + 1;
                            }
                            else
                            {
                                if (!item.FixHoliday.Contains(DayName) && IsContinue < AttachmentReqLeaveCount && Modal.Where(n => n.Empid == item.Empid && n.LeaveTypeID != 1).Select(x => x.Day31).FirstOrDefault() == 0)
                                {
                                    IsContinue = 0;
                                }
                            }
                            if (IsContinue == 1)
                            {

                            }
                            break;
                        default:
                            break;
                    }


                }
                //if (CLSLCount >= AttachmentReqLeaveCount && IsContinue >= AttachmentReqLeaveCount)
                //{
                //    EmpNameStr = EmpNameStr + (EmpNameStr != "" ? ", " : "") + item.EmpName;
                //    PostResult.SuccessMessage = "Attach medical and fitness certificate for " + EmpNameStr;
                //    ModelState.AddModelError("[0].isSelect", PostResult.SuccessMessage);
                //}
            }


            //if (Modal.OrderBy(x => x.LeaveDate).ToList().Where(x => x.isSelect == 1 && x.IsAttachment==0).Count() > 0)
            //{                
            //    foreach (var item in Modal.Where(n=>n.LeaveTypeID==1 && n.IsAttachment == 0 && (n.Day1>0 || n.Day2> 0 || n.Day3 > 0) ))
            //    {

            //    }
            //    CLMsg = Leave.ValidateCLSL(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList());
            //    if (!string.IsNullOrEmpty(CLMsg))
            //    {
            //        AttachmentRequired = 1;
            //        if (CLMsg == "Past CL/SL")
            //        {
            //            if (Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
            //            {
            //                PostResult.SuccessMessage = "Fitness Certificate is mandatory";
            //                ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
            //            }
            //            else
            //            {
            //                AttachmentRequired = 2;
            //            }
            //        }
            //        else if (CLMsg == "Medical Certificate" && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Medical Certificate").Any(x => x.UploadFile == null))
            //        {
            //            PostResult.SuccessMessage = "Medical Certificate is mandatory";
            //            ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
            //        }
            //        else if (CLMsg == "After Maternity Leave")
            //        {
            //            AttachmentRequired = 0;
            //        }
            //    }
            //}


            // long.TryParse((Modal.LeaveTypeID ?? 0) == 0 ? Modal.LeaveTranList.OrderBy(x => x.LeaveType).Select(x => x.LeaveType).FirstOrDefault().ToString() : (Modal.LeaveTypeID ?? 0).ToString(), out LeaveID);
            //DateTime StartDate, EndDate, ExpectedDeliveryDate;
            //DateTime.TryParse(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).Select(x => x.LeaveDate).FirstOrDefault().ToString(), out StartDate);
            //DateTime.TryParse(Modal.LeaveTranList.OrderByDescending(x => x.LeaveDate).Select(x => x.LeaveDate).FirstOrDefault().ToString(), out EndDate);
            //DateTime.TryParse(Modal.ExpectedDeliveryDate, out ExpectedDeliveryDate);

            //if (Modal.LeaveTranList.Any(x => x.LeaveType == "5") || Modal.LeaveTranList.Any(x => x.LeaveType == "7"))
            //{
            //    ApplySandwichPolicy = "Y";
            //    if (Modal.LeaveTranList.Any(x => x.LeaveType == "5") && ExpectedDeliveryDate.Year < 1900)
            //    {
            //        PostResult.SuccessMessage = "Expected Delivery Date can't be blank";
            //        ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
            //        IsValidaDate = false;
            //    }
            //    else if (Modal.LeaveTranList.Any(x => x.LeaveType == "7") && ExpectedDeliveryDate.Year < 1900)
            //    {
            //        PostResult.SuccessMessage = "Expected Child birth date can't be blank";
            //        ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
            //        IsValidaDate = false;
            //    }
            //    else if (Modal.LeaveTranList.Any(x => x.LeaveType == "7") && Modal.LeaveTranList.Any(x => x.LeaveDate < ExpectedDeliveryDate.Date))
            //    {
            //        PostResult.SuccessMessage = "You Can't apply for Leave before " + ExpectedDeliveryDate.Date.ToString("dd-MMM-yyyy");
            //        ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
            //        IsValidaDate = false;
            //    }
            //}
            //if (IsValidaDate)
            //{
            //    DataSet ValidaDateDS = Common_SPU.fnGetWorkingDays(StartDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"), ApplySandwichPolicy);
            //    if (StartDate.Date > EndDate.Date)
            //    {
            //        PostResult.SuccessMessage = "Oops! Leave date doesn't seem valid. End date should be after start date.";
            //        ModelState.AddModelError("Remarks", PostResult.SuccessMessage);

            //    }
            //    else if (ValidaDateDS.Tables[0].Rows.Count == 0)
            //    {
            //        PostResult.SuccessMessage = "You can't apply leave for week-off/ Holiday. Please verify the dates.";
            //        ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
            //    }
            //    else
            //    {
            //        DateTime[] dates = new DateTime[ValidaDateDS.Tables[0].Rows.Count];
            //        for (int i = 0; i < ValidaDateDS.Tables[0].Rows.Count; i++)
            //        {
            //            dates[i] = Convert.ToDateTime(ValidaDateDS.Tables[0].Rows[i]["date"]).Date;
            //        }
            //        var a = Modal.LeaveTranList.Select(x => x.LeaveDate.Date).Except(dates);
            //        if (a.Count() > 0)
            //        {
            //            var datess = string.Join(",", a.Select(x => x.Date.ToString("dd-MMM-yyyy")));
            //            IsValidaDate = false;
            //            PostResult.SuccessMessage = "You can't apply leave for week-off/ Holiday. Please verify the dates. (" + datess + " )";
            //            ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
            //        }
            //    }
            //    if (IsValidaDate)
            //    {
            //        RES = Leave.ValidateLeaveRequest(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList(), ExpectedDeliveryDate);
            //        if (!string.IsNullOrEmpty(RES.Trim()))
            //        {
            //            PostResult.SuccessMessage = RES;
            //            ModelState.AddModelError("Remarks", RES);
            //        }

            //        if (string.IsNullOrEmpty(RES.Trim()) && Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList().Where(x => x.LeaveType == "1").Count() > 0)
            //        {
            //         CLMsg = Leave.ValidateCLSL(Modal.LeaveTranList.OrderBy(x => x.LeaveDate).ToList());
            //            if (!string.IsNullOrEmpty(CLMsg))
            //            {
            //                AttachmentRequired = 1;
            //                if (CLMsg == "Past CL/SL")
            //                {
            //                    if (Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Fitness Certificate").Any(x => x.UploadFile == null))
            //                    {
            //                        PostResult.SuccessMessage = "Fitness Certificate is mandatory";
            //                        ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
            //                    }
            //                    else
            //                    {
            //                        AttachmentRequired = 2;
            //                    }
            //                }
            //                else if (CLMsg == "Medical Certificate" && Modal.LeaveAttachmentList.Where(x => x.AttachmentType == "Medical Certificate").Any(x => x.UploadFile == null))
            //                {
            //                    PostResult.SuccessMessage = "Medical Certificate is mandatory";
            //                    ModelState.AddModelError("Remarks", PostResult.SuccessMessage);
            //                }
            //                else if (CLMsg == "After Maternity Leave")
            //                {
            //                    AttachmentRequired = 0;
            //                }
            //            }
            //        }
            //    }
            //}


            // for validation testing

            int EmpidNew = 0, EmpidOld = 0, IsSelect = 0;

            foreach (var item in Modal)
            {
                EmpidNew = Convert.ToInt32(item.Empid);
                if (EmpidNew != EmpidOld)
                {
                    IsSelect = item.isSelect;
                }
                if (IsSelect == 1)
                {
                    long sumday = 0;
                    var day1 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day1);
                    var day2 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day2);
                    var day3 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day3); var day4 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day4); var day5 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day5);
                    var day6 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day6); var day7 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day7); var day8 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day8);
                    var day9 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day9); var day10 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day10); var day11 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day11);
                    var day12 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day12); var day13 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day13); var day14 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day14);
                    var day15 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day15); var day16 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day16); var day17 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day17);
                    var day18 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day18); var day19 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day19); var day20 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day20);
                    var day21 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day21); var day22 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day22); var day23 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day23);
                    var day24 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day24); var day25 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day25); var day26 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day26);
                    var day27 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day27); var day28 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day28); var day29 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day29);
                    var day30 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day30); var day31 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day31);
                    sumday = Convert.ToInt64(item.Day1 + item.Day2 + item.Day3 + item.Day4 + item.Day5 + item.Day6 + item.Day7 + item.Day8 + item.Day9 + item.Day10 + item.Day11 + item.Day12 + item.Day13 + item.Day14 + item.Day15 + item.Day16 + item.Day17 + item.Day18 + item.Day19 + item.Day20 + item.Day21 + item.Day22 + item.Day23 + item.Day24 + item.Day25 + item.Day26 + item.Day27 + item.Day28 + item.Day29 + item.Day30 + item.Day31);
                    // if (Convert.ToInt64(Convert.ToInt64(item.Balance)+Convert.ToInt64(item.Availed)) < sumday)


                    if (Convert.ToInt32(item.LeaveTypeID) != 3)
                    {
                        if (Convert.ToInt64(item.Balance) > 0)
                        {
                            if (Convert.ToInt64(item.Balance) < sumday)
                            {
                                PostResult.SuccessMessage = "Leave hrs should not be more than Balance  for " + item.EmpName;
                                ModelState.AddModelError("[0].isSelect", PostResult.SuccessMessage);
                                break;
                            }
                        }

                    }
                    if (day1 > 8 || day2 > 8 || day3 > 8 || day4 > 8 || day5 > 8 || day6 > 8 || day7 > 8 || day8 > 8 || day9 > 8 || day9 > 8 || day10 > 8 || day11 > 8 || day12 > 8 || day13 > 8 || day14 > 8 || day15 > 8 || day16 > 8 || day17 > 8 || day18 > 8 || day19 > 8 || day20 > 8 || day21 > 8 || day22 > 8 || day23 > 8 || day23 > 8 || day24 > 8 || day25 > 8 || day26 > 8 || day27 > 8 || day28 > 8 || day29 > 8 || day30 > 8 || day31 > 8)
                    //if (day1 > 8 )
                    {
                        PostResult.SuccessMessage = "Leave hrs should not be more than 8 hours for " + item.EmpName;
                        ModelState.AddModelError("[0].isSelect", PostResult.SuccessMessage);
                        break;
                    }



                }
                EmpidOld = EmpidNew;
            }


            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    // Validating Attachment
                    // Upload Attachment
                    bool AllAttachmentValid = true;
                    //for (int i = 0; i < Modal.LeaveAttachmentList.Count; i++)
                    //{
                    //    if (Modal.LeaveAttachmentList[i].UploadFile != null)
                    //    {
                    //        var RvFile = clsApplicationSetting.ValidateFile(Modal.LeaveAttachmentList[i].UploadFile);
                    //        if (!RvFile.IsValid)
                    //        {
                    //            AllAttachmentValid = false;
                    //            PostResult.SuccessMessage = RvFile.Message;
                    //            TempData["Success"] = (PostResult.Status ? "Y" : "N");
                    //            TempData["SuccessMsg"] = PostResult.SuccessMessage;
                    //            return Json(PostResult, JsonRequestBehavior.AllowGet);
                    //        }
                    //    }
                    //}
                    if (AllAttachmentValid)
                    {

                        //long.TryParse(Modal.LeaveTranList.Sum(x => x.LeaveHours).ToString(), out TotalLeaveHours);
                        // int EmpidNew = 0, EmpidOld = 0, IsSelect = 0;




                        foreach (var item in Modal)
                        {
                            EmpidNew = Convert.ToInt32(item.Empid);
                            if (EmpidNew != EmpidOld)
                            {
                                IsSelect = item.isSelect;
                            }
                            if (IsSelect == 1)
                            {
                                //  long sumday = 0;
                                //  var day1 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day1);
                                //  var day2 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day2);
                                //  var day3 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day3); var day4 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day4); var day5 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day5);
                                //  var day6 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day6); var day7 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day7); var day8 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day8);
                                //  var day9 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day9); var day10 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day10); var day11 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day11);
                                //  var day12 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day12); var day13 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day13); var day14 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day14);
                                //  var day15 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day15); var day16 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day16); var day17 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day17);
                                //  var day18 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day18); var day19 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day19); var day20 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day20);
                                //  var day21 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day21); var day22 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day22); var day23 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day23);
                                //  var day24 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day24); var day25 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day25); var day26 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day26);
                                //  var day27 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day27); var day28 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day28); var day29 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day29);
                                //  var day30 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day30); var day31 = Modal.Where(x => x.Empid == EmpidNew).Sum(x => x.Day31);
                                //  sumday = Convert.ToInt64(item.Day1 + item.Day2 + item.Day3 + item.Day4 + item.Day5 + item.Day6 + item.Day7 + item.Day8 + item.Day9 + item.Day10 + item.Day11 + item.Day12 + item.Day13 + item.Day14 + item.Day15 + item.Day16 + item.Day17 + item.Day18 + item.Day19 + item.Day20 + item.Day21 + item.Day22 + item.Day23 + item.Day24 + item.Day25 + item.Day26 + item.Day27 + item.Day28 + item.Day29 + item.Day30 + item.Day31 );
                                //  // if (Convert.ToInt64(Convert.ToInt64(item.Balance)+Convert.ToInt64(item.Availed)) < sumday)
                                //  if (Convert.ToInt64(item.Prevbalance) < sumday)
                                //  {
                                //      PostResult.SuccessMessage = "Leave hrs should not be more than Balance  for " + item.EmpName;
                                //      ModelState.AddModelError("[0].isSelect", PostResult.SuccessMessage);
                                //      break;
                                //  }
                                //  if (day1 > 8 || day2 > 8 || day3 > 8 || day4 > 8 || day5 > 8 || day6 > 8 || day7 > 8 || day8 > 8 || day9 > 8 || day9 > 8 || day10 > 8 || day11 > 8 || day12 > 8 || day13 > 8 || day14 > 8 || day15 > 8 || day16 > 8 || day17 > 8 || day18 > 8 || day19 > 8 || day20 > 8 || day21 > 8 || day22 > 8 || day23 > 8 || day23 > 8 || day24 > 8 || day25 > 8 || day26 > 8 || day27 > 8 || day28 > 8 || day29 > 8 || day30 > 8 || day31 > 8)
                                //  {
                                //      PostResult.SuccessMessage = "Leave hrs should not be more than 8 hours for " + item.EmpName;
                                //      ModelState.AddModelError("[0].isSelect", PostResult.SuccessMessage);
                                //      break;
                                //  }
                                //  else
                                //  {
                                //      SaveID = Common_SPU.fnSetLeaveNonMitr(0, Convert.ToInt32(item.Empid), item.LeaveDate, Convert.ToInt32(item.LeaveTypeID), 0, "", AttachmentRequired, "1900-12-30"
                                //, item.Day1, item.Day2, item.Day3, item.Day4, item.Day5, item.Day6, item.Day7, item.Day8, item.Day9, item.Day10
                                //, item.Day11, item.Day12, item.Day13, item.Day14, item.Day15, item.Day16, item.Day17, item.Day18, item.Day19, item.Day20
                                //, item.Day21, item.Day22, item.Day23, item.Day24, item.Day25, item.Day26, item.Day27, item.Day28, item.Day29, item.Day30, item.Day31, item.IsAttachment);
                                //  }

                                if (Convert.ToInt32(item.LeaveTypeID) == 3)
                                {
                                    SaveID = Common_SPU.fnSetLeaveNonMitr(0, Convert.ToInt32(item.Empid), item.LeaveDate, Convert.ToInt32(item.LeaveTypeID), 0, "", AttachmentRequired, "1900-12-30"
                                     , item.Day1, item.Day2, item.Day3, item.Day4, item.Day5, item.Day6, item.Day7, item.Day8, item.Day9, item.Day10, item.Day11, item.Day12, item.Day13, item.Day14, item.Day15, item.Day16, item.Day17, item.Day18, item.Day19, item.Day20
                                   , item.Day21, item.Day22, item.Day23, item.Day24, item.Day25, item.Day26, item.Day27, item.Day28, item.Day29, item.Day30, item.Day31, item.IsAttachment);
                                }
                                else if (Convert.ToInt64(item.Balance) > 0)
                                {
                                    SaveID = Common_SPU.fnSetLeaveNonMitr(0, Convert.ToInt32(item.Empid), item.LeaveDate, Convert.ToInt32(item.LeaveTypeID), 0, "", AttachmentRequired, "1900-12-30"
                                   , item.Day1, item.Day2, item.Day3, item.Day4, item.Day5, item.Day6, item.Day7, item.Day8, item.Day9, item.Day10, item.Day11, item.Day12, item.Day13, item.Day14, item.Day15, item.Day16, item.Day17, item.Day18, item.Day19, item.Day20
                                   , item.Day21, item.Day22, item.Day23, item.Day24, item.Day25, item.Day26, item.Day27, item.Day28, item.Day29, item.Day30, item.Day31, item.IsAttachment);
                                }
                                //SaveID = Common_SPU.fnSetLeaveNonMitr(0, Convert.ToInt32(item.Empid), item.LeaveDate, Convert.ToInt32(item.LeaveTypeID), 0, "", AttachmentRequired, "1900-12-30"
                                //  , item.Day1, item.Day2, item.Day3, item.Day4, item.Day5, item.Day6, item.Day7, item.Day8, item.Day9, item.Day10, item.Day11, item.Day12, item.Day13, item.Day14, item.Day15, item.Day16, item.Day17, item.Day18, item.Day19, item.Day20
                                //  , item.Day21, item.Day22, item.Day23, item.Day24, item.Day25, item.Day26, item.Day27, item.Day28, item.Day29, item.Day30, item.Day31, item.IsAttachment);

                            }
                            EmpidOld = EmpidNew;
                        }
                        if (SaveID > 0)
                        {
                            PostResult.SuccessMessage = "Your request has been successfully submitted";
                            PostResult.Status = true;
                        }

                        //if (SaveID > 0)
                        //{
                        //    int count = 0;
                        //    decimal dHrs = 0;
                        //    DateTime dtDate = Convert.ToDateTime(StartDate);

                        //    // Upload Attachment

                        //    for (int i = 0; i < Modal.LeaveAttachmentList.Count; i++)
                        //    {
                        //        if (Modal.LeaveAttachmentList[i].UploadFile != null)
                        //        {
                        //            string FileExt = System.IO.Path.GetExtension(Modal.LeaveAttachmentList[i].UploadFile.FileName).ToLower();

                        //            long AttachmentID = 0;
                        //            AttachmentID = Common_SPU.fnSetAttachments(0, Modal.LeaveAttachmentList[i].AttachmentType, FileExt, Modal.LeaveAttachmentList[i].AttachmentType, SaveID.ToString(), "ApplyLeave");
                        //            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + AttachmentID + FileExt)))
                        //            {
                        //                System.IO.File.Delete("~/Attachments/" + AttachmentID + FileExt);
                        //            }
                        //            Modal.LeaveAttachmentList[i].UploadFile.SaveAs(Server.MapPath("~/Attachments/" + AttachmentID + FileExt));
                        //        }
                        //    }

                        //    // Fire Mail
                        //    Common_SPU.fnCreateMail_Leave(SaveID);
                        //    PostResult.Status = true;

                        //    if (!string.IsNullOrEmpty(CLMsg))
                        //    {
                        //        PostResult.SuccessMessage = "Apply Leave Successfully. You are requested to attach Medical and physical certificate before submitting activity log";

                        //    }
                        //    else
                        //    {
                        //        PostResult.SuccessMessage = "Your request has been successfully submitted";


                        //    }
                        //    TempData["Success"] = (PostResult.Status ? "Y" : "N");
                        //    TempData["SuccessMsg"] = PostResult.SuccessMessage;
                        //    return Json(PostResult, JsonRequestBehavior.AllowGet);
                        //}
                    }
                }
            }
            TempData["Success"] = (PostResult.Status ? "Y" : "N");
            TempData["SuccessMsg"] = PostResult.SuccessMessage;
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public JsonResult _SaveDailyLog(DailyLogNonMitr Modal, string src, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            //int TotalLeaveWithAttachmentPending = 0;
            PostResult.OtherID = 1;
            //int.TryParse(GetQueryString[2], out TotalLeaveWithAttachmentPending);
            DateTime SelectedDate = DateTime.Now.Date;
            if (GetQueryString.Length > 2)
            {
                DateTime.TryParse(GetQueryString[2], out SelectedDate);
            }
            if (!Modal.LstDailyLog.Any(x => x.isSelect == 1))
            {
                PostResult.SuccessMessage = "Please Make a Selection";
                ModelState.AddModelError("LstDailyLog[0].proj_id", PostResult.SuccessMessage);
            }
            if (Command == "Save")
            {
                if (Modal.LstDailyLog.Any(x => x.isSelect == 1 & (x.proj_id == 0 || x.ActivityID == 0)))
                {
                    PostResult.SuccessMessage = "Please Choose Project and Activity in selected row";
                    ModelState.AddModelError("LstDailyLog[0].proj_id", PostResult.SuccessMessage);
                }
                string Error = GetDateError_Daily(Modal);
                if (Error != "")
                {
                    PostResult.SuccessMessage = Error;
                    ModelState.AddModelError("LstDailyLog[0].proj_id", PostResult.SuccessMessage);
                }
            }

            long SaveID = 0, EMPID = 0;
            double Total = 0;
            int count = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            //if (Modal.DailyLogList == null && Command != "Recall")
            //{
            //    PostResult.SuccessMessage = "Log Table Can't be blank";
            //    ModelState.AddModelError("SelectedDate", PostResult.SuccessMessage);

            //}
            if (ModelState.IsValid)
            {
                if (Command == "Recall")
                {
                    // Common_SPU.fnSetRecallAcivityLog(Modal.SelectedDate.Month, Modal.SelectedDate.Year, Modal.RecallRemarks);
                    PostResult.Status = true;
                    PostResult.SuccessMessage = "Activity log recalled successfully";
                    PostResult.RedirectURL = "/Activity/DailyLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/DailyLog*" + SelectedDate.ToString("yyyy-MM"));
                    PostResult.ID = SaveID;
                }
                else if (Command == "Save")
                {
                    PostResult.SuccessMessage = "Activity Log for the month submitted Not Saved";
                    foreach (var item in Modal.LstDailyLog)
                    {
                        if (item.isSelect == 1)
                        {
                            count = count + 1;
                            SaveID = Common_SPU.fnSetDailyLogNonMitr(item.DailyLogID.ToString(), item.emp_id, item.month, item.year,
                                item.proj_id.ToString(), item.description,
                                  item.Day1, item.Day2, item.Day3, item.Day4, item.Day5, item.Day6, item.Day7, item.Day8, item.Day9, item.Day10,
                                  item.Day11, item.Day12, item.Day13, item.Day14, item.Day15, item.Day16, item.Day17, item.Day18, item.Day19, item.Day20,
                                  item.Day21, item.Day22, item.Day23, item.Day24, item.Day25, item.Day26, item.Day27, item.Day28, item.Day29, item.Day30,
                                  item.Day31, Total, count, item.ActivityID);
                            //clsDataBaseHelper.ExecuteNonQuery("UPDATE daily_log SET ISDELETED=1 WHERE EMP_ID=" + EMPID + " AND month=" + Modal.SelectedDate.Month.ToString() + " and year=" + Modal.SelectedDate.Year.ToString() + " AND srno>" + i);

                            CommonSpecial.prcUpdateCompOff(SelectedDate, item.emp_id);
                        }

                    }
                    if (SaveID > 0)
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = "Activity Log saved successfully";
                        PostResult.RedirectURL = "/Activity/DailyLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/DailyLog*" + SelectedDate.ToString("yyyy-MM"));
                        PostResult.ID = SaveID;
                    }
                }
                else if (Command == "Back")
                {
                    int EntryCount = 0;
                    foreach (var item in Modal.LstDailyLog)
                    {
                        if (item.isSelect == 1)
                        {
                            SaveID = Common_SPU.FnSetReverseNonMitr(0, item.emp_id, item.month, item.year, "LEAVE");
                            if (SaveID > 0)
                            {
                                EntryCount = EntryCount + 1;
                            }
                        }
                    }
                    if (EntryCount > 0)
                    {
                        PostResult.OtherID = 0;
                        PostResult.Status = true;
                        PostResult.SuccessMessage = "Activity Log Reversed successfully";
                        PostResult.RedirectURL = "/Activity/DailyLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/DailyLog*" + SelectedDate.ToString("yyyy-MM"));
                        PostResult.ID = SaveID;
                    }
                }
                else if (Command == "VerifyAndSave")
                {
                    PostResult.StatusCode = 1;
                    PostResult.SuccessMessage = "Activity Log for the month submitted Not Saved";

                    //if (string.IsNullOrEmpty(ValidateDates))
                    //{
                    //    if (TotalLeaveWithAttachmentPending > 0)
                    //    {
                    //        CommonSpecial.AdjustLeaveType(EMPID, Modal.SelectedDate);
                    //    }
                    //    for (int i = 0; i < Modal.DailyLogList.Count; i++)
                    //    {
                    //        Total = (Modal.DailyLogList[i].Day1 + Modal.DailyLogList[i].Day2 + Modal.DailyLogList[i].Day3 + Modal.DailyLogList[i].Day4 + Modal.DailyLogList[i].Day5 + Modal.DailyLogList[i].Day6 + Modal.DailyLogList[i].Day7 + Modal.DailyLogList[i].Day8 + Modal.DailyLogList[i].Day9 + Modal.DailyLogList[i].Day10 +
                    //              Modal.DailyLogList[i].Day11 + Modal.DailyLogList[i].Day12 + Modal.DailyLogList[i].Day13 + Modal.DailyLogList[i].Day14 + Modal.DailyLogList[i].Day15 + Modal.DailyLogList[i].Day16 + Modal.DailyLogList[i].Day17 + Modal.DailyLogList[i].Day18 + Modal.DailyLogList[i].Day19 + Modal.DailyLogList[i].Day20 +
                    //              Modal.DailyLogList[i].Day21 + Modal.DailyLogList[i].Day22 + Modal.DailyLogList[i].Day23 + Modal.DailyLogList[i].Day24 + Modal.DailyLogList[i].Day25 + Modal.DailyLogList[i].Day26 + Modal.DailyLogList[i].Day27 + Modal.DailyLogList[i].Day28 + Modal.DailyLogList[i].Day29 + Modal.DailyLogList[i].Day30 +
                    //              Modal.DailyLogList[i].Day31);

                    //        SaveID = Common_SPU.fnSetDailyLog(Modal.DailyLogList[i].DailyLogID.ToString(), Modal.SelectedDate.Month, Modal.SelectedDate.Year,
                    //            Modal.DailyLogList[i].proj_id.ToString(), Modal.DailyLogList[i].description,
                    //              Modal.DailyLogList[i].Day1, Modal.DailyLogList[i].Day2, Modal.DailyLogList[i].Day3, Modal.DailyLogList[i].Day4, Modal.DailyLogList[i].Day5, Modal.DailyLogList[i].Day6, Modal.DailyLogList[i].Day7, Modal.DailyLogList[i].Day8, Modal.DailyLogList[i].Day9, Modal.DailyLogList[i].Day10,
                    //              Modal.DailyLogList[i].Day11, Modal.DailyLogList[i].Day12, Modal.DailyLogList[i].Day13, Modal.DailyLogList[i].Day14, Modal.DailyLogList[i].Day15, Modal.DailyLogList[i].Day16, Modal.DailyLogList[i].Day17, Modal.DailyLogList[i].Day18, Modal.DailyLogList[i].Day19, Modal.DailyLogList[i].Day20,
                    //              Modal.DailyLogList[i].Day21, Modal.DailyLogList[i].Day22, Modal.DailyLogList[i].Day23, Modal.DailyLogList[i].Day24, Modal.DailyLogList[i].Day25, Modal.DailyLogList[i].Day26, Modal.DailyLogList[i].Day27, Modal.DailyLogList[i].Day28, Modal.DailyLogList[i].Day29, Modal.DailyLogList[i].Day30,
                    //              Modal.DailyLogList[i].Day31, Total, i, Modal.DailyLogList[i].ActivityID);
                    //        clsDataBaseHelper.ExecuteNonQuery("UPDATE daily_log SET ISDELETED=1 WHERE EMP_ID=" + EMPID + " AND month=" + Modal.DailyLogList[i].month + " and year=" + Modal.DailyLogList[i].year + " AND srno>" + i);
                    //        CommonSpecial.prcUpdateCompOff(Modal.SelectedDate, EMPID);
                    //    }
                    if (SaveID > 0)
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = "Activity Log submitted successfully";
                        PostResult.RedirectURL = "/Activity/MonthlyActiveLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/MonthlyActiveLog*" + SelectedDate.ToString("yyyy-MM"));
                        PostResult.ID = SaveID;
                        //TempData["Success"] = "Y";
                        //TempData["SuccessMsg"] = PostResult.SuccessMessage;
                    }
                }
                else
                {
                    PostResult.SuccessMessage = "";
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public JsonResult SubmitActiveLog(MonthlyLogNonMitr Modal, FormCollection Collection, string src, string Command)
        {
            PostResponse result = new PostResponse();
            result.SuccessMessage = "Monthly Activity Log not Submitted";
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            result.OtherID = 1;
            //long EMPID = 0;
            DateTime Month;
            DateTime Doj, LastWorkingDay; ;
            Month = Convert.ToDateTime(Collection.GetValue("hdnSelectedDate").AttemptedValue);
            string MonthYear = Month.Year.ToString() + "-" + Month.Month.ToString(); //StartDate.ToString("yyyy-MM");
            string DateError = "";
            IActivityHelper Activity;
            Activity = new ActivityModal();
            long SaveID = 0, SavedCount = 0;
            if (!Modal.LstMonthlyLog.Any(x => x.isSelect == 1))
            {
                result.SuccessMessage = "Please Make a Selection";
                ModelState.AddModelError("LstMonthlyLog[0].proj_id", result.SuccessMessage);
            }
            string FixHolidays = "", HourWorked = "0", LeaveAvailed = "0", LeavePendingForApproval = "0", CompensatoryHours = "0", CompensatoryHoursApproved = "0", txtCC = "";
            if (Command == "Submit")
            {
                foreach (var item in Modal.LstMonthlyLog.Where(n => n.Doctype == "A"))
                {
                    if (item.isSelect == 1)
                    {
                        FixHolidays = item.FixHoliday;

                        DateTime StartDate = new DateTime(Month.Year, Month.Month, 1);
                        DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
                        Doj = item.Doj;
                        LastWorkingDay = item.LastWorkingDay;
                        HourWorked = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.MWHrs).FirstOrDefault().ToString();
                        LeaveAvailed = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.TLAHrs).FirstOrDefault().ToString();
                        LeavePendingForApproval = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.TLPHrs).FirstOrDefault().ToString();
                        CompensatoryHours = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.TCHrs).FirstOrDefault().ToString();
                        CompensatoryHoursApproved = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.TCAHrs).FirstOrDefault().ToString();
                        txtCC = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.EMailCC).FirstOrDefault().ToString();
                        double WorkingHours = Convert.ToDouble(Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.WorkingHours).FirstOrDefault().ToString());
                        int count = 0;
                        double hdnGrandTotal_ = 0;
                        for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
                        {
                            count++;
                            switch (date.Day)
                            {
                                case 1:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day1).FirstOrDefault().ToString());
                                    break;
                                case 2:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day2).FirstOrDefault().ToString());
                                    break;
                                case 3:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day3).FirstOrDefault().ToString());
                                    break;
                                case 4:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day4).FirstOrDefault().ToString());
                                    break;
                                case 5:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day5).FirstOrDefault().ToString());
                                    break;
                                case 6:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day6).FirstOrDefault().ToString());
                                    break;
                                case 7:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day7).FirstOrDefault().ToString());
                                    break;
                                case 8:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day8).FirstOrDefault().ToString());
                                    break;
                                case 9:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day9).FirstOrDefault().ToString());
                                    break;
                                case 10:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day10).FirstOrDefault().ToString());
                                    break;
                                case 11:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day11).FirstOrDefault().ToString());
                                    break;
                                case 12:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day12).FirstOrDefault().ToString());
                                    break;
                                case 13:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day13).FirstOrDefault().ToString());
                                    break;
                                case 14:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day14).FirstOrDefault().ToString());
                                    break;
                                case 15:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day15).FirstOrDefault().ToString());
                                    break;
                                case 16:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day16).FirstOrDefault().ToString());
                                    break;
                                case 17:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day17).FirstOrDefault().ToString());
                                    break;
                                case 18:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day18).FirstOrDefault().ToString());
                                    break;
                                case 19:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day19).FirstOrDefault().ToString());
                                    break;
                                case 20:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day20).FirstOrDefault().ToString());
                                    break;
                                case 21:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day21).FirstOrDefault().ToString());
                                    break;
                                case 22:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day22).FirstOrDefault().ToString());
                                    break;
                                case 23:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day23).FirstOrDefault().ToString());
                                    break;
                                case 24:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day24).FirstOrDefault().ToString());
                                    break;
                                case 25:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day25).FirstOrDefault().ToString());
                                    break;
                                case 26:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day26).FirstOrDefault().ToString());
                                    break;
                                case 27:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day27).FirstOrDefault().ToString());
                                    break;
                                case 28:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day28).FirstOrDefault().ToString());
                                    break;
                                case 29:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day29).FirstOrDefault().ToString());
                                    break;
                                case 30:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day30).FirstOrDefault().ToString());
                                    break;
                                case 31:
                                    hdnGrandTotal_ = Convert.ToDouble(Modal.LstMonthlyLog.Where(n => n.emp_id == item.emp_id && n.Doctype == "G").Select(n => n.Day31).FirstOrDefault().ToString());
                                    break;
                                default:
                                    hdnGrandTotal_ = WorkingHours;
                                    break;
                            }


                            if (hdnGrandTotal_ < WorkingHours || WorkingHours == 0 || hdnGrandTotal_ > 24)
                            {
                                bool isHoliday = false;
                                if (Modal.LstHoliday.Any(x => x.Date == date.ToString("dd/MM/yyyy") && (x.Empid == item.emp_id || x.Empid == 0)))
                                {
                                    if (Modal.LstHoliday.Where(x => x.Date == date.ToString("dd/MM/yyyy") && (x.Empid == item.emp_id || x.Empid == 0)).ToList().Count > 0)
                                    {
                                        isHoliday = true;
                                    }
                                }
                                if (!FixHolidays.Contains(date.ToString("dddd")) && !isHoliday && Doj < Convert.ToDateTime(MonthYear + "-" + date.Day.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + date.Day.ToString()))
                                {
                                    DateError += count + ", ";
                                }
                            }
                        }
                        if (DateError != "")
                        {
                            DateError = "Working hours can not be less then 8 hours Or can not be Greater than 24 hours, Please Verify " + DateError;
                            ModelState.AddModelError("LstMonthlyLog[0].proj_id", DateError);
                            result.SuccessMessage = DateError;
                        }
                    }

                }
            }

            if (ModelState.IsValid)
            {
                int EmpidNew = 0, EmpidOld = 0, IsSelect = 0, EntryCount = 0;
                if (Command == "Submit")
                {

                    foreach (var item in Modal.LstMonthlyLog.Where(n => n.Doctype == "A"))
                    {
                        EmpidNew = Convert.ToInt32(item.emp_id);
                        IsSelect = item.isSelect;
                        if (EmpidNew != EmpidOld)
                        {
                            IsSelect = item.isSelect;
                            EntryCount = 0;

                        }
                        if (IsSelect == 1)
                        {
                            EntryCount = EntryCount + 1;
                            HourWorked = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.MWHrs).FirstOrDefault().ToString();
                            LeaveAvailed = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.TLAHrs).FirstOrDefault().ToString();
                            LeavePendingForApproval = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.TLPHrs).FirstOrDefault().ToString();
                            CompensatoryHours = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.TCHrs).FirstOrDefault().ToString();
                            CompensatoryHoursApproved = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.TCAHrs).FirstOrDefault().ToString();
                            txtCC = Modal.LstHoursSummary.Where(n => n.Empid == item.emp_id).Select(n => n.EMailCC).FirstOrDefault().ToString();

                            SaveID = Common_SPU.fnSetActiveLog(item.DailyLogID.ToString(), item.emp_id.ToString(), item.ActivityID.ToString(), item.description, item.month.ToString(), item.year.ToString(),
                                "", item.proj_id.ToString(),
                                  item.Day1.ToString(), item.Day2.ToString(), item.Day3.ToString(), item.Day4.ToString(), item.Day5.ToString(), item.Day6.ToString(), item.Day7.ToString(), item.Day8.ToString(), item.Day9.ToString(), item.Day10.ToString(),
                                  item.Day11.ToString(), item.Day12.ToString(), item.Day13.ToString(), item.Day14.ToString(), item.Day15.ToString(), item.Day16.ToString(), item.Day17.ToString(), item.Day18.ToString(), item.Day19.ToString(), item.Day20.ToString(),
                                  item.Day21.ToString(), item.Day22.ToString(), item.Day23.ToString(), item.Day24.ToString(), item.Day25.ToString(), item.Day26.ToString(), item.Day27.ToString(), item.Day28.ToString(), item.Day29.ToString(), item.Day30.ToString(),
                                  item.Day31.ToString(), item.Total.ToString(), EntryCount, HourWorked, LeaveAvailed, CompensatoryHours);
                            SavedCount = SavedCount + SaveID;
                            //if (EntryCount > 0)
                            //{
                            //    string SQL = "UPDATE active_log SET ISDELETED = 1,deletedat=CURRENT_TIMESTAMP WHERE EMP_ID = " + item.emp_id.ToString() + " AND month = " + item.month.ToString() + " AND year = " + item.year.ToString() + " AND srno> " + EntryCount + "";                                
                            //    clsDataBaseHelper.ExecuteNonQuery(SQL);                                
                            //}


                            // fire mail
                            // Common_SPU.fnCreateMail_ActivityLog(Month.Month, Month.Year, EMPID);

                            //fire mail


                        }

                        EmpidOld = EmpidNew;
                    }
                    Common_SPU.fnCreateMail_Termbaseactivitylog(Convert.ToString(Convert.ToDateTime(Month).ToString("yyyy/MM/dd")), "", "", 0);
                    result.Status = true;
                    result.SuccessMessage = "Monthly Activity Log Added Successfully";
                    result.RedirectURL = "/Leave/DailyLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/DailyLog*" + "4");
                }
                else if (Command == "Back")
                {
                    foreach (var item in Modal.LstMonthlyLog.Where(n => n.Doctype == "A"))
                    {
                        EmpidNew = Convert.ToInt32(item.emp_id);
                        IsSelect = item.isSelect;
                        if (EmpidNew != EmpidOld)
                        {
                            IsSelect = item.isSelect;
                            EntryCount = 0;

                        }
                        if (IsSelect == 1)
                        {

                            SaveID = Common_SPU.FnSetReverseNonMitr(0, item.emp_id, item.month, item.year, "DAILY_LOG");
                            SavedCount = SavedCount + SaveID;

                        }
                        EmpidOld = EmpidNew;
                    }
                    result.OtherID = 0;
                    result.Status = true;
                    result.SuccessMessage = "Monthly Activity Reversed Successfully";
                    result.RedirectURL = "/Leave/DailyLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/DailyLog*" + "4");
                }

            }

            TempData["Success"] = (result.Status ? "Y" : "N");
            TempData["SuccessMsg"] = result.SuccessMessage;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public JsonResult _ActionRequestForCompensatoryOff(string src, List<RequestCompensatoryOffList> Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            //long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Count = 0;
            bool status = false;
            string Msg = "Comp Off Request is not Saved";
            if (Modal.Any(x => x.Applied_hours > x.hours))
            {
                ModelState.AddModelError("Applied_hours", "");
                PostResult.SuccessMessage = "Hours Applied can not be greater than Hours Worked.";
            }
            if (ModelState.IsValid)
            {
                if (Command == "Apply")
                {
                    foreach (var item in Modal)
                    {
                        if (!string.IsNullOrEmpty(item.CheckSelected))
                        {
                            Count++;
                            clsDataBaseHelper.ExecuteNonQuery("update compensatory_off set hours='" + item.Applied_hours + "', mailsent=1 where id=" + item.CompensatoryOffID + "");
                        }
                    }
                    if (Count > 0)
                    {
                        status = true;
                        Msg = "Comp off request submitted";
                    }

                }
                if (status)
                {
                    //TempData["Success"] = "Y";
                    //TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;

                }
                else
                {
                    PostResult.Status = false;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }




        private string GetDateError_Daily(DailyLogNonMitr Modal)
        {
            string ValidateDates = "";
            DateTime SelectedDate = Convert.ToDateTime(Modal.LstDailyLog[0].year.ToString() + "-" + Modal.LstDailyLog[0].month.ToString() + "-01");
            DateTime StartDate = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
            DateTime Doj, LastWorkingDay;
            string MonthYear = StartDate.ToString("yyyy-MM");
            string FixHolidays = "";
            double WorkingHours = 0;
            long Empid = 0;
            var Empllist = Modal.LstDailyLog.GroupBy(d => new { d.emp_id }).Select(m => new { m.Key.emp_id });
            for (int i = 0; i < Empllist.ToList().Count; i++)
            {
                Empid = Modal.LstDailyLog[i].emp_id;
                FixHolidays = Modal.LstDailyLog[i].FixHoliday;
                WorkingHours = Modal.LstDailyLog[i].WorkingHours;
                Doj = Modal.LstDailyLog[i].Doj;
                LastWorkingDay = Modal.LstDailyLog[i].LastWorkingDay;
                if (Modal.LstDailyLog.Any(x => x.isSelect == 1 && x.emp_id == Empid))
                {
                    double Day1 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day1);
                    double Day2 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day2);
                    double Day3 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day3);
                    double Day4 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day4);
                    double Day5 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day5);
                    double Day6 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day6);
                    double Day7 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day7);
                    double Day8 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day8);
                    double Day9 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day9);
                    double Day10 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day10);
                    double Day11 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day11);
                    double Day12 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day12);
                    double Day13 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day13);
                    double Day14 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day14);
                    double Day15 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day15);
                    double Day16 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day16);
                    double Day17 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day17);
                    double Day18 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day18);
                    double Day19 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day19);
                    double Day20 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day20);
                    double Day21 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day21);
                    double Day22 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day22);
                    double Day23 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day23);
                    double Day24 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day24);
                    double Day25 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day25);
                    double Day26 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day26);
                    double Day27 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day27);
                    double Day28 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day28);
                    double Day29 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day29);
                    double Day30 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day30);
                    double Day31 = Modal.LstDailyLog.Where(x => x.emp_id == Empid).Sum(x => x.Day31);

                    for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
                    {
                        bool isvalid = true;
                        if (Modal.LstHoliday.Any(x => x.Date == date.ToString("dd/MM/yyyy") && (x.Empid == Empid || x.Empid == 0)) || FixHolidays.Contains(date.ToString("dddd")))
                        {
                            isvalid = false;
                        }
                        if (isvalid)
                        {
                            int DateError = 0;
                            int.TryParse(date.ToString("dd"), out DateError);
                            switch (DateError)
                            {
                                case 1:

                                    if ((Day1 < WorkingHours || WorkingHours == 0 || Day1 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "1 ,";
                                    }
                                    break;
                                case 2:
                                    if ((Day2 < WorkingHours || WorkingHours == 0 || Day2 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "2 ,";
                                    }
                                    break;
                                case 3:
                                    if ((Day3 < WorkingHours || WorkingHours == 0 || Day3 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "3 ,";
                                    }
                                    break;
                                case 4:
                                    if ((Day4 < WorkingHours || WorkingHours == 0 || Day4 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "4 ,";
                                    }
                                    break;
                                case 5:
                                    if ((Day5 < WorkingHours || WorkingHours == 0 || Day5 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "5 ,";
                                    }
                                    break;
                                case 6:
                                    if ((Day6 < WorkingHours || WorkingHours == 0 || Day6 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "6 ,";
                                    }
                                    break;
                                case 7:
                                    if ((Day7 < WorkingHours || WorkingHours == 0 || Day7 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "7 ,";
                                    }
                                    break;
                                case 8:
                                    if ((Day8 < WorkingHours || WorkingHours == 0 || Day8 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "8 ,";
                                    }
                                    break;
                                case 9:
                                    if ((Day9 < WorkingHours || WorkingHours == 0 || Day9 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "9 ,";
                                    }
                                    break;
                                case 10:
                                    if ((Day10 < WorkingHours || WorkingHours == 0 || Day10 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "10 ,";
                                    }
                                    break;
                                case 11:
                                    if ((Day11 < WorkingHours || WorkingHours == 0 || Day11 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "11 ,";
                                    }
                                    break;
                                case 12:
                                    if ((Day12 < WorkingHours || WorkingHours == 0 || Day12 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "12 ,";
                                    }
                                    break;
                                case 13:
                                    if ((Day13 < WorkingHours || WorkingHours == 0 || Day13 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "13 ,";
                                    }
                                    break;
                                case 14:
                                    if ((Day14 < WorkingHours || WorkingHours == 0 || Day14 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "14 ,";
                                    }
                                    break;
                                case 15:
                                    if ((Day15 < WorkingHours || WorkingHours == 0 || Day15 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "15 ,";
                                    }
                                    break;
                                case 16:
                                    if ((Day16 < WorkingHours || WorkingHours == 0 || Day16 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "16 ,";
                                    }
                                    break;
                                case 17:
                                    if ((Day17 < WorkingHours || WorkingHours == 0 || Day17 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "17 ,";
                                    }
                                    break;
                                case 18:
                                    if ((Day18 < WorkingHours || WorkingHours == 0 || Day18 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "18 ,";
                                    }
                                    break;
                                case 19:
                                    if ((Day19 < WorkingHours || WorkingHours == 0 || Day19 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "19 ,";
                                    }
                                    break;
                                case 20:
                                    if ((Day20 < WorkingHours || WorkingHours == 0 || Day20 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "20 ,";
                                    }
                                    break;
                                case 21:
                                    if ((Day21 < WorkingHours || WorkingHours == 0 || Day21 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "21 ,";
                                    }
                                    break;
                                case 22:
                                    if ((Day22 < WorkingHours || WorkingHours == 0 || Day22 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "22 ,";
                                    }
                                    break;
                                case 23:
                                    if ((Day23 < WorkingHours || WorkingHours == 0 || Day23 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "23 ,";
                                    }
                                    break;
                                case 24:
                                    if ((Day24 < WorkingHours || WorkingHours == 0 || Day24 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "24 ,";
                                    }
                                    break;
                                case 25:
                                    if ((Day25 < WorkingHours || WorkingHours == 0 || Day25 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "25 ,";
                                    }
                                    break;
                                case 26:
                                    if ((Day26 < WorkingHours || WorkingHours == 0 || Day26 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "26 ,";
                                    }
                                    break;
                                case 27:
                                    if ((Day27 < WorkingHours || WorkingHours == 0 || Day27 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "27 ,";
                                    }
                                    break;
                                case 28:
                                    if ((Day28 < WorkingHours || WorkingHours == 0 || Day28 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "28 ,";
                                    }
                                    break;
                                case 29:
                                    if ((Day29 < WorkingHours || WorkingHours == 0 || Day29 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "29 ,";
                                    }
                                    break;
                                case 30:
                                    if ((Day30 < WorkingHours || WorkingHours == 0 || Day30 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "30 ,";
                                    }
                                    break;
                                case 31:
                                    if ((Day31 < WorkingHours || WorkingHours == 0 || Day31 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                                    {
                                        ValidateDates += "31 ,";
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }


            }

            return ValidateDates.Trim().TrimEnd(',');
        }
    }
}