using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace Mitr.Areas.Onboarding.Controllers
{
    public class OnboardingEmployeeController : BaseController
    {
        long LoginID = 0;
        string IPAddress = "";
        // GET: Onboarding/OnboardingEmployee
        GetResponse getResponse;
        IEmployeeMappingHelper employee;
        public OnboardingEmployeeController()
        {
            getResponse = new GetResponse();
            employee = new EmployeeMappingModal();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
        }
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Onboard()
        {
            //ViewBag.RequestId = id;
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.MenuID = "1";
            //ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            return View();
        }
        [HttpGet]
        public ActionResult RegistrationDetails(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID=0, CandidateID=0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
            Employee.RegistrationDetails Modal = new Employee.RegistrationDetails();

            getResponse.ID = CandidateID;
            getResponse.Doctype = "RegistrationDetails";
            DataSet result = Common_SPU.GetOnboardingCurrentMasterEmpStatus(CandidateID, 1);
            bool IsMasterEmployee = false;
            if (result.Tables[0].Rows.Count > 0)
            {
                IsMasterEmployee = true;
            }
            else
            {
                IsMasterEmployee = false;
            }
            
            if (IsMasterEmployee == true)
            {
                getResponse.ID = EMPID;
                Modal = employee.GetEmployeeRegistrationDetails(getResponse);
            }
            else
            {
                getResponse.ID = CandidateID;
                Modal = employee.GetMappingEmployeeRegistrationDetails(getResponse);
            }

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
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID=0, CandidateID=0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
            PostResult.SuccessMessage = "Action Can't Update";
            DataSet result1 = Common_SPU.GetCandidateMasterID(EMPID);
            int CandidateLoginId = 0;
            int CandidateEmployeeID = 0;
            foreach (DataRow item in result1.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(item["user_id"].ToString()))
                {
                    if (!string.IsNullOrEmpty(item["user_id"].ToString()))
                    {
                        CandidateLoginId = Convert.ToInt32(item["user_id"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["user_id"].ToString()))
                    {
                        CandidateEmployeeID = Convert.ToInt32(item["id"].ToString());                        
                    }
                }
            }

            if (Modal.lastworking_day != null)
            {
                if (Modal.NoticePeriodPayable == null && Modal.NoticePeriodWaived == null)
                {
                    ModelState.AddModelError("Id", "");
                    PostResult.SuccessMessage = "Notice Period Waived and Notice Period Payable can not be blank.";
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
                    var User = Common_SPU.SetLoginUsersMapping(Modal.UserDetails);
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
                    PostResult = employee.SetEMP_OnboardingRegistrationDetails(Modal);
                    if (Modal.SalaryAccount != null)
                    {
                        Modal.SalaryAccount.Doctype = "Salary";
                        Modal.SalaryAccount.EMPID = EMPID;
                        employee.SetEMP_Onboarding_Account(Modal.SalaryAccount);
                    }
                    if (Modal.ReimbursementAccount != null)
                    {
                        Modal.ReimbursementAccount.Doctype = "Reimbursement";
                        Modal.ReimbursementAccount.EMPID = EMPID;
                        employee.SetEMP_Onboarding_Account(Modal.ReimbursementAccount);
                    }
                }

            }
            if (PostResult.Status)
            {
                //Common_SPU.OnboardingUpdateEmpMasterStatus(CandidateID, "RegistrationDetails");
                Common_SPU.RegistrationCompletedStatus(CandidateID,1, "RegistrationDetails", EMPID);
                
                PostResult.RedirectURL = "/OnboardingEmployee/PersonalDetails?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/OnboardingEmployee/PersonalDetails*"  +CandidateID + "*" + EMPID + "*" + LoginID);
                // return RedirectToAction("PersonalDetails", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + " */OnboardingEmployee/PersonalDetails*"+CandidateID+"*"+EMPID+"*"+LoginID)});
                }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult PersonalDetails(string src)
        {
            ViewBag.TabIndex = 2;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
            Employee.PersonalDetails Modal = new Employee.PersonalDetails();
            getResponse.ID = EMPID;
            getResponse.Doctype = "PersonalDetails";
            //DataSet result = Common_SPU.GetOnboardingEmpStatus(CandidateID);
            DataSet result = Common_SPU.GetOnboardingCurrentMasterEmpStatus(CandidateID, 2);
            bool IsMasterEmployee = false;
            if (result.Tables[0].Rows.Count > 0)
            {
                IsMasterEmployee = true;
            }
            else
            {
                IsMasterEmployee = false;
            }
            if (IsMasterEmployee == true)
            {
                getResponse.ID = EMPID;
                Modal = employee.GetEmployeePersonalDetails(getResponse);
            }
            else
            {
                getResponse.ID = CandidateID;
                Modal = employee.GetMappingEmployeePersonalDetails(getResponse);
            }
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
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
            PostResult.SuccessMessage = "Action Can't Update";
            if (Modal.ReferencesList.Count < 2)
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
                Common_SPU.OnboardingUpdateEmpMasterStatus(CandidateID, "PersonalDetails");
                Common_SPU.RegistrationCompletedStatus(CandidateID, 2, "PersonalDetails", EMPID);
                PostResult.RedirectURL = "/OnboardingEmployee/GeneralInformation?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/OnboardingEmployee/GeneralInformation*" +CandidateID + "*" + EMPID + "*" + LoginID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult GeneralInformation(string src)
        {
            ViewBag.TabIndex = 3;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
            Employee.GeneralInfo Modal = new Employee.GeneralInfo();
            getResponse.ID = EMPID;
            getResponse.Doctype = "GeneralInfo";
            DataSet result = Common_SPU.GetOnboardingCurrentMasterEmpStatus(CandidateID,3);
            bool IsMasterEmployee = false;
            if (result.Tables[0].Rows.Count > 0)
            {
                IsMasterEmployee = true;
            }
            else
            {
                IsMasterEmployee = false;
            }
           
            if (IsMasterEmployee == true)
            {
                getResponse.ID = EMPID;
                Modal = employee.GetEmployeeGeneralInfo(getResponse);
                Modal.design_id = Modal.employmentDetails.Designation;
            }
            else
            {
                getResponse.ID = CandidateID;
                Modal = employee.GetMappingEmployeeGeneralInfo(getResponse);
            }


            return View(Modal);
        }
        [HttpPost]
        public ActionResult GeneralInformation(string src, Employee.GeneralInfo Modal, string Command)
        {
            ViewBag.TabIndex = 3;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
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
                        ClsCommon.fnSetDataString("update AirlinePreferences set isdeleted=1,deletedat=CURRENT_TIMESTAMP,deletedby=" + clsApplicationSetting.GetSessionValue("LoginID") + " where Empid=" + EMPID.ToString() + " and isdeleted=0 and IsActive=1");

                    }
                }
                if (Modal.employmentDetails != null)
                {
                    Modal.employmentDetails.EMPID = EMPID;
                    Modal.employmentDetails.Designation = Modal.design_id!= null?Modal.design_id:string.Empty;
                    employee.SetEMP_LastEmployment(Modal.employmentDetails);
                }
            }
            if (PostResult.Status)
            {
                Common_SPU.OnboardingUpdateEmpMasterStatus(CandidateID, "GeneralInformation");
                Common_SPU.RegistrationCompletedStatus(CandidateID, 3, "GeneralInformation", EMPID);
                PostResult.RedirectURL = "/OnboardingEmployee/IDInformations?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/OnboardingEmployee/IDInformations*" + CandidateID + "*" + EMPID + "*" + LoginID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult IDInformations(string src)
        {
            ViewBag.TabIndex = 4;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
            Employee.IDInfo result = new Employee.IDInfo();
            getResponse.ID = EMPID;
            getResponse.Doctype = "IDInformation";
            DataSet ds = Common_SPU.GetOnboardingCurrentMasterEmpStatus(CandidateID, 4);
            bool IsMasterEmployee = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMasterEmployee = true;
            }
            else
            {
                IsMasterEmployee = false;
            }
          
            if (IsMasterEmployee == true)
            {
                getResponse.ID = EMPID;
                result = employee.GetEmployeeIDInfoList(getResponse);
            }
            else
            {
                getResponse.ID = CandidateID;
                result = employee.GetMappingEmployeeIDInfoList(getResponse);
            }

            return View(result);
        }
        [HttpPost]
        public ActionResult IDInformations(string src, Employee.IDInfo Modal, string Command)
        {
            ViewBag.TabIndex = 3;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
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
                Common_SPU.OnboardingUpdateEmpMasterStatus(CandidateID, "IDInformations");
                Common_SPU.RegistrationCompletedStatus(CandidateID, 4, "IDInformations", EMPID);
                PostResult.RedirectURL = "/OnboardingEmployee/Attachments?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/OnboardingEmployee/Attachments*"  +CandidateID + "*" + EMPID + "*" + LoginID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Attachments(string src)
        {
            ViewBag.TabIndex = 5;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
            Employee.Attachments result = new Employee.Attachments();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Attachments";
            DataSet ds = Common_SPU.GetOnboardingCurrentMasterEmpStatus(CandidateID, 5);
            bool IsMasterEmployee = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMasterEmployee = true;
            }
            else
            {
                IsMasterEmployee = false;
            }
            
            if (IsMasterEmployee == true)
            {
                getResponse.ID = EMPID;
                result = employee.GetEmployeeAttachments(getResponse);
            }
            else
            {
                getResponse.ID = CandidateID;
                result = employee.GetMappingEmployeeAttachments(getResponse);
            }

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
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
            PostResult.SuccessMessage = "Action Can't Update";
            string _Path = clsApplicationSetting.GetPhysicalPath("onboarding");
            if (ModelState.IsValid)
            {
                Modal.id = EMPID;

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
                                AttachmentID = Common_SPU.SetAttachmentsMapping(0, RvFile.FileName, RvFile.FileExt, "");
                                var id = Common_SPU.SetAttachmentsMappingUpdateFileName(AttachmentID);
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
                                    PostResult = employee.SetEMP_Attachments(item, AttachmentID);
                                }
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
                                AttachmentID = Common_SPU.SetAttachmentsMapping(0, RvFile.FileName, RvFile.FileExt, "");
                                var id = Common_SPU.SetAttachmentsMappingUpdateFileName(AttachmentID);
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

                                    PostResult = employee.SetEMP_Attachments(item, AttachmentID);
                                }
                            }
                        }
                    }
                }
            }
            if (PostResult.Status)
            {
                Common_SPU.OnboardingUpdateEmpMasterStatus(CandidateID, "Attachments");
                Common_SPU.RegistrationCompletedStatus(CandidateID, 5, "Attachments", EMPID);
                PostResult.RedirectURL = "/OnboardingEmployee/Declaration?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/OnboardingEmployee/Declaration*" + CandidateID + "*" + EMPID + "*" + LoginID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Declaration(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
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
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
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
                            item.AttachmentId = Common_SPU.SetAttachmentsMapping(item.AttachmentId, RvFile.FileName, RvFile.FileExt, "");
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
                    string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/OnboardingEmployee/Declaration*"  +CandidateID + "*" + EMPID + "*" + LoginID);
                    PostResult.SuccessMessage = "Action Update Data";
                    PostResult.AdditionalMessage = Url;
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/OnboardingEmployee/Declaration*" + CandidateID + "*" + EMPID + "*" + LoginID);
                PostResult.SuccessMessage = "there is some problem, Please try again...";
                PostResult.AdditionalMessage = Url;
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            return View();


        }
        public ActionResult _PendingDeclaration(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";
            result.declarationslist = employee.GetOnboardingDeclartionPending(getResponse);
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
            ViewBag.CandidateID = GetQueryString[2];
            ViewBag.EMPID = GetQueryString[3];
            ViewBag.LoginID = GetQueryString[4];
            int EMPID = 0, LoginID = 0, CandidateID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginID, out LoginID);
            int.TryParse(ViewBag.CandidateID, out CandidateID);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";
            result.declarationslist = employee.GetOnboardingDeclartionApprove(getResponse);
            return PartialView(result);
        }
    }
}