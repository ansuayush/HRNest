using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class EmployeeController : Controller
    {
        IEmployeeHelper employee;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        public EmployeeController()
        {
            getResponse = new GetResponse();
            employee = new EmployeeModal();
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

        public ActionResult EmployeeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ListTabs Modal = new ListTabs();
            Modal.Approve = 0;
            return View(Modal);
        }
        public ActionResult _EmployeeList(string src, ListTabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Employee.List> result = new List<Employee.List>();
            getResponse.Approve = Modal.Approve;
            ViewBag.Approve = Modal.Approve;
            result = employee.GetEmployeeList(getResponse);
            return PartialView(result);
        }
        //public ActionResult RegistrationDetails(string src)
        //{
        //    ViewBag.TabIndex = 1;
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    ViewBag.EMPID = GetQueryString[2];
        //    int EMPID = 0;
        //    int.TryParse(ViewBag.EMPID, out EMPID);
        //    Employee.RegistrationDetails Modal = new Employee.RegistrationDetails();
        //    getResponse.ID = EMPID;
        //    getResponse.Doctype = "RegistrationDetails";
        //    Modal = employee.GetEmployeeRegistrationDetails(getResponse);
        //    try
        //    {
        //        if (Modal.UserDetails != null)
        //        {
        //            Modal.UserDetails.password = clsApplicationSetting.Decrypt(Modal.UserDetails.password);
        //        }
        //    }
        //    catch { }


        //    return View(Modal);
        //}
        public ActionResult RegistrationDetails(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.RegistrationDetails Modal = new Employee.RegistrationDetails();

            getResponse.ID = EMPID;
            getResponse.Doctype = "RegistrationDetails";
            Modal = employee.GetEmployeeRegistrationDetails(getResponse);
            ViewBag.lastworkingday = Modal.lastworking_day;
            try
            {
                if (Modal.UserDetails != null)
                {
                    Modal.UserDetails.password = clsApplicationSetting.Decrypt(Modal.UserDetails.password);
                }
            }
            catch { }
            return View(Modal);
        }
        [HttpPost]
        public ActionResult RegistrationDetails(string src, Employee.RegistrationDetails Modal, string Command)
        {
            ViewBag.TabIndex = 1;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            PostResult.SuccessMessage = "Action Can't Update";
            //if (  Convert.ToDateTime(Modal.doc).Date<= Convert.ToDateTime(Modal.Probation_EndDate).Date)
            //{
            //    ModelState.AddModelError("Id", "");
            //    PostResult.SuccessMessage = "Confirmation Date is allways greater then Probation Period End Date.";
            //}

            if (Modal.lastworking_day != null)
            {
                if (Modal.NoticePeriodPayable == null && Modal.NoticePeriodWaived == null)
                {
                    ModelState.AddModelError("Id", "");
                    PostResult.SuccessMessage = "Notice Period Waived and Notice Period Payable can not be blank.";
                }

            }
            if (Convert.ToString(Modal.emp_status) == "Confirmed")
            {
                if (Modal.doc == null)
                {
                    ModelState.AddModelError("Id", "");
                    PostResult.SuccessMessage = "Kindly fill the Date of Confirmation";
                }
            }
            ModelState["ReimbursementAccount.AccountType"].Errors.Clear();
            if (string.IsNullOrEmpty(Modal.IsMitrUser))
            {
                ModelState["UserDetails.user_name"].Errors.Clear();
                ModelState["UserDetails.Name"].Errors.Clear();
                ModelState["UserDetails.RoleIDs"].Errors.Clear();
                ModelState["UserDetails.password"].Errors.Clear();
                ModelState["JobID"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                Modal.ID = EMPID;
                if (Modal.UserDetails != null && !string.IsNullOrEmpty(Modal.UserDetails.RoleIDs))
                {
                    var User = Common_SPU.SetLoginUsers(Modal.UserDetails);
                    if (User.Status)
                    {
                        Modal.USER_ID = User.ID;
                    }
                    else
                    {
                        PostResult.StatusCode = -1;
                        PostResult.SuccessMessage = User.SuccessMessage;
                    }
                }
                if (PostResult.StatusCode != -1)
                {
                    if (Modal.EmploymentTerm.ToString() != "TermBase")
                    {
                        Modal.Contract_EndDate = "";
                    }
                    PostResult = employee.SetEMP_RegistrationDetails(Modal);
                    if (Modal.SalaryAccount != null)
                    {
                        Modal.SalaryAccount.Doctype = "Salary";
                        Modal.SalaryAccount.EMPID = PostResult.ID;
                        employee.SetEMP_Account(Modal.SalaryAccount);
                    }
                    if (Modal.ReimbursementAccount != null)
                    {
                        Modal.ReimbursementAccount.Doctype = "Reimbursement";
                        Modal.ReimbursementAccount.EMPID = PostResult.ID;
                        employee.SetEMP_Account(Modal.ReimbursementAccount);
                    }
                }

            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Employee/PersonalDetails?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Employee/PersonalDetails*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult PersonalDetails(string src)
        {
            ViewBag.TabIndex = 2;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.PersonalDetails Modal = new Employee.PersonalDetails();
            getResponse.ID = EMPID;
            getResponse.Doctype = "PersonalDetails";
            Modal = employee.GetEmployeePersonalDetails(getResponse);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult PersonalDetails(string src, Employee.PersonalDetails Modal, string Command)
        {
            ViewBag.TabIndex = 2;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            PostResult.SuccessMessage = "Action Can't Update";
            if (Modal.ReferencesList.Count < 3)
            {
                PostResult.SuccessMessage = "References should be minimum of 3";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            Modal.id = EMPID;
            PostResult = employee.SetEMP_PersonalDetails(Modal);
            if (Modal.LocalAddress != null)
            {
                Modal.LocalAddress.Doctype = "Local";
                Modal.LocalAddress.TableName = "EMP";
                Modal.LocalAddress.TableID = PostResult.ID;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(Modal.LocalAddress);
                Address localAddressCopy = Newtonsoft.Json.JsonConvert.DeserializeObject<Address>(json);
                employee.SetAddress(localAddressCopy);
            }
            if (Modal.PermanentAddress != null)
            {
                Modal.PermanentAddress.Doctype = "Permanent";
                Modal.PermanentAddress.TableName = "EMP";
                Modal.PermanentAddress.TableID = PostResult.ID;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(Modal.PermanentAddress);
                Address PermanentAddressCopy = Newtonsoft.Json.JsonConvert.DeserializeObject<Address>(json);
                employee.SetAddress(PermanentAddressCopy);
            }
            if (Modal.ReferencesList != null)
            {
                foreach (var item in Modal.ReferencesList)
                {
                    item.EMPID = PostResult.ID;
                    employee.SetMaster_Emp_References(item);
                }
            }
            if (Modal.QualificationList != null)
            {
                foreach (var item in Modal.QualificationList)
                {
                    item.EMPID = PostResult.ID;
                    employee.SetMaster_Emp_Qualification(item);
                }
            }

            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Employee/GeneralInfo?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Employee/GeneralInfo*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GeneralInfo(string src)
        {
            ViewBag.TabIndex = 3;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.GeneralInfo Modal = new Employee.GeneralInfo();
            getResponse.ID = EMPID;
            getResponse.Doctype = "GeneralInfo";
            Modal = employee.GetEmployeeGeneralInfo(getResponse);

            return View(Modal);
        }
        [HttpPost]
        public ActionResult GeneralInfo(string src, Employee.GeneralInfo Modal, string Command)
        {
            ViewBag.TabIndex = 3;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            PostResult.SuccessMessage = "Action Can't Update";
            int AirlinePreferenceExist = 0;
            if (ModelState.IsValid)
            {
                Modal.id = EMPID;
                PostResult = employee.SetEMP_GeneralInfo(Modal);
                if (Modal.AirlinePreferencesList != null)
                {
                    foreach (var item in Modal.AirlinePreferencesList)
                    {
                        if (!string.IsNullOrEmpty(item.AirlineName))
                        {
                            AirlinePreferenceExist = AirlinePreferenceExist + 1;
                            item.EMPID = EMPID;
                            employee.SetAirlinePreferences(item);
                        }
                    }
                    if (AirlinePreferenceExist == 0)
                    {
                        ClsCommon.fnSetDataString("update AirlinePreferences set isdeleted=1,deletedat=CURRENT_TIMESTAMP,deletedby=" + clsApplicationSetting.GetSessionValue("LoginID") + " where Empid=" + Modal.id.ToString() + " and isdeleted=0 and IsActive=1");

                    }
                }
                if (Modal.employmentDetails != null)
                {
                    Modal.employmentDetails.EMPID = EMPID;
                    employee.SetEMP_LastEmployment(Modal.employmentDetails);
                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Employee/IDInformation?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Employee/IDInformation*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult IDInformation(string src)
        {
            ViewBag.TabIndex = 4;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.IDInfo result = new Employee.IDInfo();
            getResponse.ID = EMPID;
            getResponse.Doctype = "IDInformation";
            result = employee.GetEmployeeIDInfoList(getResponse);
            return View(result);
        }

        [HttpPost]
        public ActionResult IDInformation(string src, Employee.IDInfo Modal, string Command)
        {
            ViewBag.TabIndex = 3;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            PostResult.SuccessMessage = "Action Can't Update";
            if (ModelState.IsValid)
            {
                Modal.id = EMPID;

                if (Modal.EMPAttachmentsList != null)
                {
                    int count = 0;
                    foreach (var item in Modal.EMPAttachmentsList.Where(x => !string.IsNullOrEmpty(x.Chk)).ToList())
                    {
                        count++;
                        item.EMPID = EMPID;
                        item.Priority = count;
                        item.IsOpted = (string.IsNullOrEmpty(item.Chk) ? 0 : 1);
                        PostResult = employee.SetEMP_Attachments(item);
                        if (!PostResult.Status)
                        {
                            break;
                        }
                    }
                }
                if (Modal.Accident != null)
                {
                    Modal.Accident.EMPID = EMPID;
                    Modal.Accident.InsuranceType = "Accident";
                    employee.SetEMP_Insurance(Modal.Accident);
                }
                if (Modal.Medical != null)
                {
                    Modal.Medical.EMPID = EMPID;
                    Modal.Medical.InsuranceType = "Medical";
                    employee.SetEMP_Insurance(Modal.Medical);
                }


            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Employee/Attachments?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Employee/Attachments*" + EMPID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Attachments(string src)
        {
            ViewBag.TabIndex = 5;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.Attachments result = new Employee.Attachments();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Attachments";
            result = employee.GetEmployeeAttachments(getResponse);
            return View(result);
        }

        [HttpPost]
        public ActionResult Attachments(string src, Employee.Attachments Modal, string Command)
        {
            ViewBag.TabIndex = 5;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            PostResult.SuccessMessage = "Action Can't Update";
            string _Path = clsApplicationSetting.GetPhysicalPath();
            if (ModelState.IsValid)
            {
                Modal.id = EMPID;
                var isOnboardEmp = Common_SPU.OnboardingEmpCheck(EMPID);
                if (isOnboardEmp == 1)
                {
                    _Path = clsApplicationSetting.GetPhysicalPath("onboarding");
                }
                if (Modal.EMPAttachmentsList != null)
                {
                    int count = 0;
                    foreach (var item in Modal.EMPAttachmentsList)
                    {
                        item.EMPID = EMPID;
                        item.Priority = count;
                        long AttachmentID = 0;
                        if (item.Upload != null)
                        {
                            var RvFile = clsApplicationSetting.ValidateFile(item.Upload);
                            if (RvFile.IsValid)
                            {
                                AttachmentID = Common_SPU.fnSetAttachments(0, RvFile.FileName, RvFile.FileExt, "");
                                if (isOnboardEmp == 1)
                                {
                                    Common_SPU.SetAttachmentsMappingUpdateFileName(AttachmentID);
                                }
                                item.Upload.SaveAs(Path.Combine(_Path, AttachmentID + RvFile.FileExt));
                                PostResult = employee.SetEMP_Attachments(item, AttachmentID);
                            }
                            else
                            {
                                PostResult.Status = RvFile.IsValid;
                                PostResult.SuccessMessage = RvFile.Message;
                                return Json(PostResult, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if (item.AttachmentID != null && item.AttachmentID != 0)
                            {
                                if (isOnboardEmp == 1)
                                {
                                    if (!string.IsNullOrEmpty(item.AttachmentURL))
                                    {
                                        string[] strSplit = item.AttachmentURL.Split('/');
                                        string fileName = string.Empty;
                                        string fileExt = string.Empty;
                                        if (strSplit.Length > 0)
                                        {
                                            string[] splFile = strSplit[3].Split('.');
                                            fileName = splFile[0];
                                            fileExt = "." + splFile[1];
                                        }
                                        AttachmentID = Common_SPU.SetAttachmentsMapping(0, fileName, fileExt, "");
                                    }
                                }
                                else
                                {
                                    AttachmentID = item.AttachmentID ?? 0;
                                }
                                PostResult = employee.SetEMP_Attachments(item, AttachmentID);
                            }
                        }
                    }
                }

                if (Modal.EMPOtherAttachmentsList != null)
                {
                    int count = 0;
                    foreach (var item in Modal.EMPOtherAttachmentsList)
                    {
                        item.EMPID = EMPID;
                        item.OfficialName = "Other";
                        item.Priority = count;
                        long AttachmentID = 0;
                        if (item.Upload != null)
                        {
                            var RvFile = clsApplicationSetting.ValidateFile(item.Upload);
                            if (RvFile.IsValid)
                            {
                                AttachmentID = Common_SPU.fnSetAttachments(0, RvFile.FileName, RvFile.FileExt, "");
                                if (isOnboardEmp == 1)
                                {
                                    Common_SPU.SetAttachmentsMappingUpdateFileName(AttachmentID);
                                }
                                item.Upload.SaveAs(Path.Combine(_Path, AttachmentID + RvFile.FileExt));
                                PostResult = employee.SetEMP_Attachments(item, AttachmentID);
                            }
                            else
                            {
                                PostResult.Status = RvFile.IsValid;
                                PostResult.SuccessMessage = RvFile.Message;
                                return Json(PostResult, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if (item.AttachmentID != null && item.AttachmentID != 0)
                            {
                                if (isOnboardEmp == 1)
                                {
                                    if (!string.IsNullOrEmpty(item.AttachmentURL))
                                    {
                                        string[] strSplit = item.AttachmentURL.Split('/');
                                        string fileName = string.Empty;
                                        string fileExt = string.Empty;
                                        if (strSplit.Length > 0)
                                        {
                                            string[] splFile = strSplit[3].Split('.');
                                            fileName = splFile[0];
                                            fileExt = "." + splFile[1];
                                        }
                                        AttachmentID = Common_SPU.SetAttachmentsMapping(0, fileName, fileExt, "");
                                    }
                                }
                                else
                                {
                                    AttachmentID = item.AttachmentID ?? 0;
                                }
                                PostResult = employee.SetEMP_Attachments(item, AttachmentID);
                            }
                        }
                    }
                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Employee/Declaration?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Employee/Declaration*" + EMPID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _PendingDeclaration(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";
            result.declarationslist = employee.GetEmployeeDeclartionList(getResponse);
            result.declarationslist = result.declarationslist.Where(x => x.DEId == 0 && x.Accept == 0).ToList();
            return PartialView(result);
        }

        public ActionResult _ApproveDeclaration(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";
            result.declarationslist = employee.GetEmployeeDeclartionApprovedList(getResponse);
            return PartialView(result);
        }
        public ActionResult Declaration(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";
            // result = employee.GetEmployeeDeclaration(getResponse);
            result.EmpId = EMPID;
            result.declarationslist = employee.GetEmployeeDeclartionList(getResponse);

            return View(result);
        }
        [HttpPost]
        public ActionResult Declaration(string src, Employee.Declaration modal)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            if (modal.declarationslist != null)
            {
                foreach (var item in modal.declarationslist)
                {
                    if (item.UploadAttachment != null)
                    {
                        var RvFile = clsApplicationSetting.ValidateFile(item.UploadAttachment);
                        if (RvFile.IsValid)
                        {
                            item.AttachmentId = 0;
                            item.AttachmentId = Common_SPU.fnSetAttachments(item.AttachmentId, RvFile.FileName, RvFile.FileExt, "");
                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + item.AttachmentId + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + item.AttachmentId + RvFile.FileExt);
                            }
                            item.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + item.AttachmentId + RvFile.FileExt));
                        }
                        else
                        {
                            PostResult.SuccessMessage = RvFile.Message;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);

                        }
                        item.EmpId = EMPID;
                        PostResult = employee.SetDeclarationEmployee(item);
                    }

                }
                if (PostResult.Status == true)
                {
                    string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Employee/Declaration*" + PostResult.ID);
                    PostResult.SuccessMessage = "Action Update Data";
                    PostResult.AdditionalMessage = Url;
                    return Json(PostResult, JsonRequestBehavior.AllowGet);


                }
            }
            else
            {
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Employee/Declaration*" + PostResult.ID);
                PostResult.SuccessMessage = "there is some problem, Please try again...";
                PostResult.AdditionalMessage = Url;
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            return View();


        }
        public ActionResult LeaveBalanceList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ListTabs Modal = new ListTabs();
            Modal.Approve = 0;
            return View(Modal);
        }
        public ActionResult _LeaveBalanceList(string src, ListTabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Employee.List> result = new List<Employee.List>();
            getResponse.Approve = Modal.Approve;
            ViewBag.Approve = Modal.Approve;
            result = employee.GetEmployeeLeaveBalanceList(getResponse);

            return PartialView(result);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _LeaveBalanceAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long Empid = 0;
            long.TryParse(GetQueryString[2], out Empid);
            Employee.LeaveBalance Model = new Employee.LeaveBalance();
            getResponse.ID = 0;
            getResponse.AdditionalID = Empid;
            if (Empid > 0)
            {
                Model = employee.GetEmployeeLeaveBalance(getResponse);
                Model.srno = Model.lstLeaveBalanceDet.Count + 1;
                Model.emp_id = Empid;
            }
            return PartialView(Model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeFilter(ActionFor = "W")]
        public ActionResult _LeaveBalanceAdd(string src, Employee.LeaveBalance Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "Leave Balance not saved";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    PostResult = employee.SetEmployeeLeaveBalance(Modal);
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult UploadLeaveBalance()
        {
            PostResponse PostResult = new PostResponse();
            string _imgname = string.Empty;
            string retvalue = "";
            string saveloc = "/Attachments/";
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {

                var pic = System.Web.HttpContext.Current.Request.Files["FileAttachment"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);
                    var _comPath = AppDomain.CurrentDomain.BaseDirectory + fileName;
                    var imgname = Path.GetFileNameWithoutExtension(_comPath);
                    _imgname = "LeaveBalance_UPLOAD";

                    var comPath = Server.MapPath(saveloc) + imgname + _ext;
                    imgname = imgname + _ext;
                    string fullname = _imgname;

                    var path = comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(comPath);
                    // Res = FnImportExcel(comPath, _ext);
                    DataTable dt = new DataTable();
                    dt = ClsCommon.FnConvertExceltoDatatable(comPath, _ext);
                    System.IO.File.Delete(comPath);

                    string sDeletedoptions = "0";
                    try
                    {
                        string Token = "", EmpName = "", EmpCode = "", Category = "", Designation = "", Department = "", Location = "", Gender = ""
                        , JoiningDate = "", LeaveType = "", OpeningBal = "", AllotedHours = "", StartDate = "";

                        int IsLast = 0;
                        Token = ClsCommon.RandomString() + DateTime.Now.ToString();
                        long countuploaded = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (i + 1 == dt.Rows.Count)
                                {
                                    IsLast = 1;
                                }
                                try
                                {
                                    if (dt.Rows[i][0].ToString() != "")
                                    {
                                        EmpName = Convert.ToString(dt.Rows[i][0]);
                                        Category = Convert.ToString(dt.Rows[i][1]);
                                        EmpCode = Convert.ToString(dt.Rows[i][2]);
                                        Designation = Convert.ToString(dt.Rows[i][3]);
                                        Department = Convert.ToString(dt.Rows[i][4]);
                                        Location = Convert.ToString(dt.Rows[i][5]);
                                        Gender = Convert.ToString(dt.Rows[i][6]);
                                        JoiningDate = Convert.ToString(dt.Rows[i][7]);
                                        JoiningDate = string.IsNullOrEmpty(JoiningDate) ? "1899-12-31" : Convert.ToDateTime(JoiningDate).ToString();
                                        LeaveType = Convert.ToString(dt.Rows[i][8]);
                                        OpeningBal = Convert.ToString(dt.Rows[i][9]);
                                        AllotedHours = Convert.ToString(dt.Rows[i][10]);
                                        StartDate = Convert.ToString(dt.Rows[i][11]);
                                        StartDate = string.IsNullOrEmpty(StartDate) ? "1899-12-31" : Convert.ToDateTime(StartDate).ToString();
                                        PostResult = Common_SPU.FnSetLeaveOpeningImport(Token, EmpName, Category, EmpCode, Designation, Department, Location, Gender, JoiningDate, LeaveType, OpeningBal, AllotedHours, StartDate, IsLast);
                                        if (PostResult.Status)
                                        {
                                            countuploaded = countuploaded + 1;
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }

                        if (countuploaded > 0)
                        {
                            PostResult.SuccessMessage = countuploaded.ToString() + " Records Uploaded Successfully";
                            PostResult.ID = countuploaded;
                        }
                        else
                        {
                            PostResult.SuccessMessage = "No Records Uploaded Successfully";
                            PostResult.ID = countuploaded;
                        }


                    }
                    catch (Exception ex)
                    {
                        PostResult.SuccessMessage = ex.Message.ToString(); ;
                        PostResult.ID = 0;
                    }
                    //  return Res;

                }
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeesExcelToExport()
        {
            try
            {
                DataSet ds = Common_SPU.fnGetEmployeesData();
                WriteExcelEmployeesData(ds.Tables[0], "xls");
            }
            catch (Exception ex)
            {
                //"EmployeeList", Modal
            }
            return View();
        }


        public void WriteExcelEmployeesData(DataTable dt, string extension)
        {
            try
            {
                IWorkbook workbook;

                if (extension == "xlsx")
                {
                    workbook = new XSSFWorkbook();
                }
                else if (extension == "xls")
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    throw new Exception("This format is not supported");
                }

                ISheet sheet1 = workbook.CreateSheet("Sheet 1");
                //make a header row
                IRow row1 = sheet1.CreateRow(0);

                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Columns[j].ToString());

                    ICellStyle style = workbook.CreateCellStyle();

                    //Set border style 
                    //style.BorderBottom = BorderStyle.Thick;
                    //style.BottomBorderColor = HSSFColor.Black.Index;

                    //Set font style
                    IFont font = workbook.CreateFont();
                    //font.Color = HSSFColor.White.Index;
                    font.FontName = "Arial";
                    font.FontHeight = 200;
                    font.IsBold = true;
                    style.SetFont(font);

                    //Set background color
                    //style.FillForegroundColor = IndexedColors.DarkBlue.Index;
                    //style.FillPattern = FillPattern.SolidForeground;
                    style.Alignment = HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Center;

                    //Apply the style
                    cell.CellStyle = style;
                }

                //loops through data
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        string columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(dt.Rows[i][columnName].ToString());
                    }
                }

                using (var exportData = new MemoryStream())
                {
                    //Response.ClearContent();
                    Response.Clear();
                    //Response.Flush();
                    //Response.Buffer = true;
                    workbook.Write(exportData);
                    string attach = string.Format("attachment;filename={0}", "EmployeesData.xls");
                    if (extension == "xlsx") //xlsx file format
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("Content-Disposition", attach);
                        Response.BinaryWrite(exportData.ToArray());
                    }
                    else if (extension == "xls")  //xls file format
                    {
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", attach);
                        Response.BinaryWrite(exportData.GetBuffer());
                    }
                    Response.End();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public ActionResult EmployeeUpdateDetails(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        public ActionResult _PendingEmployeeDetails(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Employee.List> list = new List<Employee.List>();
            getResponse.ID = 0;
            list = employee.GetEmployeePendingApprovedList(getResponse);
            return PartialView(list);
        }
        public ActionResult _Approvedemployeedetails(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Employee.List> list = new List<Employee.List>();
            getResponse.ID = 1;
            list = employee.GetEmployeePendingApprovedList(getResponse);
            return PartialView(list);
        }

        public ActionResult _BasicInformation(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.Status = GetQueryString[3];
            ViewBag.Id = GetQueryString[4];
            int EMPID = 0;
            int ID = 0;
            int Status = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.Status, out Status);
            int.TryParse(ViewBag.Id, out ID);
            Employee.RegistrationDetails Modal = new Employee.RegistrationDetails();
            getResponse.ID = EMPID;
            getResponse.Doctype = "RegistrationDetails";
            Modal = employee.GetEmployeeRegistrationDetails(getResponse);

            getResponse.Doctype = "Salary";
            getResponse.AdditionalID = ID;
            getResponse.Approve = Status;
            Modal.SalaryAccountNew = employee.GetEmployeeAccountdetailsUpdateDetails(getResponse);
            getResponse.Doctype = "Reimbursement";
            getResponse.Approve = Status;
            Modal.ReimbursementAccountNew = employee.GetEmployeeAccountdetailsUpdateDetails(getResponse);
            ViewBag.status = Status;
            Modal.SalaryAccountNew.MUID = ID;
            Modal.ReimbursementAccountNew.MUID = ID;
            return PartialView(Modal);
        }
        public ActionResult _PersonalInformation(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.Status = GetQueryString[3];
            ViewBag.Id = GetQueryString[4];
            int ID = 0;
            int EMPID = 0;
            int Status = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.Status, out Status);
            int.TryParse(ViewBag.Id, out ID);
            Employee.PersonalUpdateAll alldet = new Employee.PersonalUpdateAll();
            Employee.PersonalDetails Modal = new Employee.PersonalDetails();
            getResponse.ID = EMPID;
            getResponse.Doctype = "PersonalDetails";
            Modal = employee.GetEmployeePersonalDetails(getResponse);
            Employee.PersonalDetailsnew ModalNew = new Employee.PersonalDetailsnew();
            getResponse.Approve = Status;
            getResponse.AdditionalID = ID;
            ModalNew = employee.GetEmployeePersonalDetailsNew(getResponse);
            Modal.MUID = ID;
            alldet.PersonalDetails = Modal;
            alldet.PersonalDetailsnew = ModalNew;
            ViewBag.status = Status;
            return PartialView(alldet);
        }
        public ActionResult _GenralInformation(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.Status = GetQueryString[3];
            ViewBag.Id = GetQueryString[4];
            int ID = 0;
            int EMPID = 0;
            int Status = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.Status, out Status);
            int.TryParse(ViewBag.Id, out ID);
            Employee.GeneralInfo Modal = new Employee.GeneralInfo();
            getResponse.ID = EMPID;
            if (GetQueryString[5] == "MealandSeatPreference")
            {
                var data = employee.GetEmployeeGeneralInfoMealandSeat(getResponse);
                getResponse.ID = EMPID;
                getResponse.Doctype = "GeneralInfo";
                Modal = employee.GetEmployeeGeneralInfo(getResponse);
                Modal.MealPreferenceID = data.MealPreferenceID;
                Modal.SeatPreferencesID = data.SeatPreferencesID;
                Modal.AirlinePreferencesList = null;
                getResponse.AdditionalID = ID;
                Modal.EMPID = EMPID;
                Modal.id = ID;
                Modal.generalInfo = employee.GetEmployeeGeneralInfoMasterUpdateMealandSeat(getResponse);
            }
            else
            {
                getResponse.AdditionalID = ID;
                Modal = employee.GetEmployeeGeneralInfoFlyer(getResponse);
            }
            ViewBag.status = Status;
            return PartialView(Modal);
        }
        public ActionResult _IdInformation(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.Status = GetQueryString[3];
            ViewBag.Id = GetQueryString[4];
            string DocType = GetQueryString[5];
            int ID = 0;
            int EMPID = 0;
            int Status = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.Status, out Status);
            int.TryParse(ViewBag.Id, out ID);
            Employee.IDInfoNew Model = new Employee.IDInfoNew();
            Employee.IDInfo result = new Employee.IDInfo();
            getResponse.ID = EMPID;
            getResponse.Doctype = "IDInformation";
            result = employee.GetEmployeeIDInfoList(getResponse);
            var oldvoterId = result.EMPAttachmentsList.Where(x => x.OfficialName == "Voter ID").Select(x => x.No).FirstOrDefault();
            var PassportNo = result.EMPAttachmentsList.Where(x => x.OfficialName == "Passport").Select(x => x.No).FirstOrDefault();
            var PassportName = result.EMPAttachmentsList.Where(x => x.OfficialName == "Passport").Select(x => x.Name).FirstOrDefault();
            var PlaceOfIssue = result.EMPAttachmentsList.Where(x => x.OfficialName == "Passport").Select(x => x.PlaceOfIssue).FirstOrDefault();
            var ExpiryDate = result.EMPAttachmentsList.Where(x => x.OfficialName == "Passport").Select(x => x.ExpiryDate).FirstOrDefault();
            var DlName = result.EMPAttachmentsList.Where(x => x.OfficialName == "Driving License").Select(x => x.Name).FirstOrDefault();
            var DlNo = result.EMPAttachmentsList.Where(x => x.OfficialName == "Driving License").Select(x => x.No).FirstOrDefault();
            var DlIssueDate = result.EMPAttachmentsList.Where(x => x.OfficialName == "Driving License").Select(x => x.IssueDate).FirstOrDefault();
            var DlExpiryDate = result.EMPAttachmentsList.Where(x => x.OfficialName == "Driving License").Select(x => x.ExpiryDate).FirstOrDefault();
            var DlPlaceOfIssue = result.EMPAttachmentsList.Where(x => x.OfficialName == "Driving License").Select(x => x.PlaceOfIssue).FirstOrDefault();
            var DlRemarks = result.EMPAttachmentsList.Where(x => x.OfficialName == "Driving License").Select(x => x.Remarks).FirstOrDefault();
            var DNNo = result.EMPAttachmentsList.Where(x => x.OfficialName == "Director Identification Number").Select(x => x.No).FirstOrDefault();
            var DNName = result.EMPAttachmentsList.Where(x => x.OfficialName == "Director Identification Number").Select(x => x.Name).FirstOrDefault();
            ViewBag.VoterId = oldvoterId;
            ViewBag.PassportName = PassportName;
            ViewBag.PassportNo = PassportNo;
            ViewBag.PlaceOfIssue = PlaceOfIssue;
            ViewBag.ExpiryDate = ExpiryDate;
            ViewBag.DlName = DlName;
            ViewBag.DlNo = DlNo;
            ViewBag.DlIssueDate = DlIssueDate;
            ViewBag.DlExpiryDate = DlExpiryDate;
            ViewBag.DlPlaceOfIssue = DlPlaceOfIssue;
            ViewBag.DlRemarks = DlRemarks;
            ViewBag.DNNo = DNNo;
            ViewBag.DNName = DNName;
            getResponse.AdditionalID = ID;
            getResponse.Doctype = DocType;
            getResponse.Approve = Status;
            Model = employee.GetEmployeeIDInfoNew(getResponse);
            Model.EMPId = EMPID;
            Model.MUID = ID;
            ViewBag.status = Status;
            return PartialView(Model);
        }

        public ActionResult _AttachementInformation(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.Status = GetQueryString[3];
            ViewBag.Id = GetQueryString[4];
            int EMPID = 0;
            int Status = 0;
            int ID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.Status, out Status);
            int.TryParse(ViewBag.Id, out ID);
            //if (GetQueryString.Length > 3)	
            //{	
            //    ViewBag.Mobile = GetQueryString[3];	
            //}	
            Employee.Attachments result = new Employee.Attachments();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Attachments";
            result = employee.GetEmployeeAttachments(getResponse);
            var AdharId = result.EMPAttachmentsList.Where(x => x.OfficialName == "Voter ID").Select(x => x.AttachmentURL).FirstOrDefault();
            var PanId = result.EMPAttachmentsList.Where(x => x.OfficialName == "PAN").Select(x => x.AttachmentURL).FirstOrDefault();
            var VoterId = result.EMPAttachmentsList.Where(x => x.OfficialName == "Voter ID").Select(x => x.AttachmentURL).FirstOrDefault();
            var Passport = result.EMPAttachmentsList.Where(x => x.OfficialName == "Passport").Select(x => x.AttachmentURL).FirstOrDefault();
            var Din = result.EMPAttachmentsList.Where(x => x.OfficialName == "Director Identification Number").Select(x => x.AttachmentURL).FirstOrDefault();
            var Dl = result.EMPAttachmentsList.Where(x => x.OfficialName == "Driving License").Select(x => x.AttachmentURL).FirstOrDefault();
            ViewBag.VoterId = VoterId;
            ViewBag.Passport = Passport;
            ViewBag.Din = Din;
            ViewBag.Dl = Dl;
            ViewBag.AdharId = AdharId;
            ViewBag.PanId = PanId;
            Employee.AttachmentsNew Model = new Employee.AttachmentsNew();
            getResponse.ID = EMPID;
            getResponse.Approve = Status;
            getResponse.Doctype = "Voter ID";
            getResponse.AdditionalID = ID;
            Model.AttachementVoter = employee.GetEmployeeAttachment(getResponse);
            getResponse.Doctype = "Passport";
            Model.AttachementPassport = employee.GetEmployeeAttachment(getResponse);
            getResponse.Doctype = "Driving License";
            Model.AttachementDl = employee.GetEmployeeAttachment(getResponse);
            getResponse.Doctype = "Director Identification Number";
            Model.AttachementDin = employee.GetEmployeeAttachment(getResponse);
            getResponse.Doctype = "Aadhaar";
            Model.AttachementAdhar = employee.GetEmployeeAttachment(getResponse);
            getResponse.Doctype = "PAN";
            Model.AttachementPan = employee.GetEmployeeAttachment(getResponse);
            Model.EMPId = EMPID;
            ViewBag.status = Status;
            return PartialView(Model);
        }
    }
}

