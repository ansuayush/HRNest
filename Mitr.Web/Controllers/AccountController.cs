using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.IO;
using CaptchaMvc.HtmlHelpers;
using Mitr.Models.Captcha;
using System.Text;

namespace Mitr.Controllers
{
    public class AccountController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IEmployeeHelper employee;
        //

        public AccountController()
        {
            getResponse = new GetResponse();
            employee = new EmployeeModal();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
        }

        public ActionResult Login(string ReturnURL)
        {
            ViewBag.ReturnURL = ReturnURL;
            Login Modal = new Login();
            return View(Modal);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        //  [ValidateGoogleCaptcha]
        public ActionResult Login(Login Modal, string ReturnURL, string Command)
        {

            ViewBag.ReturnURL = ReturnURL;
            DataSet UserDataSet = default(DataSet);
            bool IsValidUser = false;
            UserDataSet = null;
            if (Command == "Login")
            {

                if (ModelState.IsValid)
                {
                    if (this.IsCaptchaValid("Captcha is not valid"))
                    {
                        UserDataSet = Common_SPU.fnGetLogin(Modal.Username, clsApplicationSetting.Encrypt(Modal.Password), HttpContext.Session.SessionID);

                        IsValidUser = Convert.ToBoolean(UserDataSet.Tables[0].Rows[0]["status"].ToString());
                        if (IsValidUser)
                        {
                            if (!string.IsNullOrEmpty(UserDataSet.Tables[0].Rows[0]["RoleIDs"].ToString()))
                            {
                                clsApplicationSetting.SetSessionValue("UserType", UserDataSet.Tables[0].Rows[0]["UserType"].ToString());
                                clsApplicationSetting.SetSessionValue("UserID", UserDataSet.Tables[0].Rows[0]["User_Name"].ToString());
                                clsApplicationSetting.SetSessionValue("EMPID", UserDataSet.Tables[0].Rows[0]["EMPID"].ToString());
                                clsApplicationSetting.SetSessionValue("EMPCode", UserDataSet.Tables[0].Rows[0]["EMPCode"].ToString());
                                clsApplicationSetting.SetSessionValue("UserName", UserDataSet.Tables[0].Rows[0]["FullName"].ToString());
                                clsApplicationSetting.SetSessionValue("LocationID", UserDataSet.Tables[0].Rows[0]["LocationID"].ToString());
                                clsApplicationSetting.SetSessionValue("AttachmentID", UserDataSet.Tables[0].Rows[0]["AttachmentID"].ToString());
                                clsApplicationSetting.SetSessionValue("contentType", UserDataSet.Tables[0].Rows[0]["content_Type"].ToString());
                                clsApplicationSetting.SetSessionValue("RoleID", UserDataSet.Tables[0].Rows[0]["RoleID"].ToString());
                                clsApplicationSetting.SetSessionValue("RoleIDs", UserDataSet.Tables[0].Rows[0]["RoleIDs"].ToString());
                                clsApplicationSetting.SetSessionValue("RolesName", UserDataSet.Tables[0].Rows[0]["RolesName"].ToString());
                                clsApplicationSetting.SetSessionValue("Designation", UserDataSet.Tables[0].Rows[0]["Designation"].ToString());
                                clsApplicationSetting.SetSessionValue("LoginID", UserDataSet.Tables[0].Rows[0]["LoginID"].ToString());
                                clsApplicationSetting.SetSessionValue("HODID", UserDataSet.Tables[0].Rows[0]["HODID"].ToString());
                                clsApplicationSetting.SetSessionValue("EDID", UserDataSet.Tables[0].Rows[0]["EDID"].ToString());
                                clsApplicationSetting.SetSessionValue("IsHOD", UserDataSet.Tables[0].Rows[0]["IsHOD"].ToString());
                                clsApplicationSetting.SetSessionValue("IsED", UserDataSet.Tables[0].Rows[0]["IsED"].ToString());
                                clsApplicationSetting.SetSessionValue("Gender", UserDataSet.Tables[0].Rows[0]["Gender"].ToString());
                                clsApplicationSetting.SetSessionValue("EMPStatus", UserDataSet.Tables[0].Rows[0]["emp_Status"].ToString());
                                clsApplicationSetting.SetSessionValue("MaritalStatus", UserDataSet.Tables[0].Rows[0]["marital_Status"].ToString());

                                if (UserDataSet.Tables.Count > 1)
                                {
                                    clsApplicationSetting.SetSessionValue("Grade", UserDataSet.Tables[1].Rows[0]["Grade"].ToString());
                                    clsApplicationSetting.SetSessionValue("IsPM", UserDataSet.Tables[1].Rows[0]["IsPM"].ToString());
                                    clsApplicationSetting.SetSessionValue("ManagerID", UserDataSet.Tables[1].Rows[0]["ManagerID"].ToString());
                                    clsApplicationSetting.SetSessionValue("EDIDToUpload", UserDataSet.Tables[1].Rows[0]["EDIDToUpload"].ToString());
                                    clsApplicationSetting.SetSessionValue("IsProcurementApprover", UserDataSet.Tables[1].Rows[0]["IsProcurementApprover"].ToString());
                                    clsApplicationSetting.SetSessionValue("IsModuleAdmin", UserDataSet.Tables[1].Rows[0]["IsModuleAdmin"].ToString());

                                }
                                if (UserDataSet.Tables.Count > 2)
                                {
                                    if (UserDataSet.Tables[2].Rows.Count > 0)
                                    {
                                        clsApplicationSetting.SetSessionValue("Id", UserDataSet.Tables[2].Rows[0]["Id"].ToString());
                                        clsApplicationSetting.SetSessionValue("UserManID", UserDataSet.Tables[2].Rows[0]["UserManID"].ToString());
                                        clsApplicationSetting.SetSessionValue("CandidateId", UserDataSet.Tables[2].Rows[0]["CandidateId"].ToString());
                                        clsApplicationSetting.SetSessionValue("Status", UserDataSet.Tables[2].Rows[0]["Status"].ToString());
                                    }
                                    else
                                    {
                                        clsApplicationSetting.SetSessionValue("Status", string.Empty);
                                    }
                                }
                                if (!string.IsNullOrEmpty(ReturnURL))
                                {
                                    return Redirect(clsApplicationSetting.DecryptFriendly(ReturnURL));
                                }
                                else if (!string.IsNullOrEmpty(clsApplicationSetting.GetSessionValue("Status")) )
                                {
                                    // User process login
                                    if (Convert.ToInt32(clsApplicationSetting.GetSessionValue("Status").ToString()) == 0)
                                    {
                                        return RedirectToAction("Welcome", "UserProcess");
                                    }
                                    else {
                                        return RedirectToAction("Dashboard", "Account");
                                    }
                                }
                                else 
                                {
                                    return RedirectToAction("Dashboard", "Account");
                                }
                            }
                            else
                            {

                                TempData["LoginErrorMsg"] = "Role is not define for this user. Please Contact your system administrator";
                                return View();
                            }
                        }
                        else
                        {
                            TempData["LoginErrorMsg"] = "User ID Or Password are not correct. Please Contact your system administrator";
                            return View();
                        }


                    }
                    TempData["LoginErrorMsg"] = "Captcha is not valid";
                    return View();
                }
            }
            return View(Modal);

        }

        public ActionResult Logout()
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);

            clsDataBaseHelper.ExecuteNonQuery("update userman set IsLogin=0,LoginOutTime=getdate() where ID=" + LoginID + "");
            clsApplicationSetting.ClearSessionValues();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult PageNotFound()
        {
            return View();
        }
        [CheckLoginFilter]
        public ActionResult Dashboard(string Message)
        {
            if (!System.IO.File.Exists(clsApplicationSetting.GetPhysicalPath("json") + "/AdminMenu.json"))
            {
                ClsCommon.CreateMenuJSon();
            }
            if (!System.IO.File.Exists(clsApplicationSetting.GetPhysicalPath("json") + "/config.json"))
            {
                ClsCommon.CreateConfigJson();
            }
            Dashboard.DashboardList Modal = new Dashboard.DashboardList();
            Modal = employee.GetDashboardEmpInfo();
            return View(Modal);

        }

        public ActionResult RegistrationDetails(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = "";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];

            ViewBag.src = src;
            if (GetQueryString.Length > 3)
            {
                ViewBag.UserId = GetQueryString[4];
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.LocationId = GetQueryString[5];
            }

            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.RegistrationDetails Modal = new Employee.RegistrationDetails();
            getResponse.ID = EMPID;
            getResponse.Doctype = "RegistrationDetails";
            Modal = employee.GetEmployeeRegistrationDetails(getResponse);
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
        public ActionResult PersonalDetails(string src)
        {
            ViewBag.TabIndex = 2;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];

            ViewBag.src = src;
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            if (GetQueryString.Length > 3)
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.LocationId = GetQueryString[5];
            }
            Employee.PersonalDetails Modal = new Employee.PersonalDetails();
            getResponse.ID = EMPID;
            getResponse.Doctype = "PersonalDetails";
            Modal = employee.GetEmployeePersonalDetails(getResponse);

            return View(Modal);
        }
        public ActionResult GeneralInfo(string src)
        {
            ViewBag.TabIndex = 3;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];

            ViewBag.src = src;
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            if (GetQueryString.Length > 3)
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.LocationId = GetQueryString[5];
            }
            Employee.GeneralInfo Modal = new Employee.GeneralInfo();
            getResponse.ID = EMPID;
            getResponse.Doctype = "GeneralInfo";
            Modal = employee.GetEmployeeGeneralInfo(getResponse);

            return View(Modal);
        }
        public ActionResult IDInformation(string src)
        {
            ViewBag.TabIndex = 4;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];

            ViewBag.src = src;
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            if (GetQueryString.Length > 3)
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.LocationId = GetQueryString[5];
            }
            Employee.IDInfo result = new Employee.IDInfo();
            getResponse.ID = EMPID;
            getResponse.Doctype = "IDInformation";
            result = employee.GetEmployeeIDInfoList(getResponse);
            return View(result);
        }
        public ActionResult OfficePolicies(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.src = src;
            if (GetQueryString.Length > 3)
            {
                if (GetQueryString[4] == "IOSMobile")
                {
                    ViewBag.Mobile = GetQueryString[4];
                    ViewBag.UserId = GetQueryString[3];
                }
                else
                {
                    ViewBag.Mobile = GetQueryString[3];
                    ViewBag.UserId = GetQueryString[4];
                    ViewBag.EMPID = GetQueryString[2];
                    ViewBag.LocationId = GetQueryString[5];
                }
            }
            var model = employee.GetOfficePoliciesListList(new GetResponse { Doctype = "OfficePolicy" });
            return View(model);
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
            if (GetQueryString.Length > 3)
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.LocationId = GetQueryString[5];
                getResponse.LoginID = Convert.ToInt64(GetQueryString[4]);
            }
            Employee.Attachments result = new Employee.Attachments();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Attachments";
            result = employee.GetEmployeeAttachments(getResponse);
            return View(result);
        }

        public ActionResult _NewPendingDeclaration(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            //ViewBag.EMPID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            if (GetQueryString.Length > 3)
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.LocationId = GetQueryString[5];
            }
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
            // ViewBag.EMPID = clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            if (GetQueryString.Length > 3)
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.LocationId = GetQueryString[5];
            }
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
            if (GetQueryString.Length > 3)
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.LocationId = GetQueryString[5];
            }
            else
            {
                ViewBag.UserId = 0;
                ViewBag.LocationId = "0";
            }
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";

            result.declarationslist = employee.GetEmployeeDeclartionList(getResponse);
            return View(result);
        }
        public ActionResult Test(string src)
        {

            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.Mobile = GetQueryString[3];
            Employee.Declaration result = new Employee.Declaration();
            return View(result);
        }
        [HttpPost]
        public ActionResult Declaration(string src, Employee.Declaration modal)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            int EMPID = 0;
            int UserId = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (GetQueryString.Length > 3)
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.LocationId = GetQueryString[5];
                ViewBag.EMPID = GetQueryString[2];
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
            }
            else
            {
                ViewBag.EMPID = clsApplicationSetting.GetSessionValue("EMPID");
                int.TryParse(ViewBag.EMPID, out EMPID);
            }

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
                            item.AttachmentId = Common_SPU.fnSetAttachmentsforMobile(item.AttachmentId, RvFile.FileName, RvFile.FileExt, "", UserId);
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
                        item.UserId = UserId;
                        PostResult = employee.SetDeclarationEmployee(item);
                    }

                }
                if (PostResult.Status == true)
                {
                    string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Account/Declaration*" + PostResult.ID);
                    PostResult.SuccessMessage = "Action Update Data";
                    PostResult.AdditionalMessage = Url;
                    return Json(PostResult, JsonRequestBehavior.AllowGet);


                }
            }
            else
            {
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Account/Declaration*" + PostResult.ID);
                PostResult.SuccessMessage = "there is some problem, Please try again...";
                PostResult.AdditionalMessage = Url;
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            return View();


        }
        [SimpleCheckLogin]
        public ActionResult OTPValidate()
        {
            long EmpId = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EmpId);
            if (EmpId > 0)
            {
                string MobileNo = SendUserOtp();

                if (!string.IsNullOrEmpty(MobileNo))
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = "OTP is successfully sent to " + MobileNo;
                }
                else
                {
                    TempData["Success"] = "N";
                    TempData["SuccessMsg"] = "Mobile is not updated";
                }
            }
            else
            {
                TempData["Success"] = "N";
                TempData["SuccessMsg"] = "OTP not required for current login user...";
            }
            return View();
        }



        private string SendUserOtp()
        {
            string Status = "";
            string Mobile = clsDataBaseHelper.fnGetOther_FieldName("master_emp inner join Address on master_emp.id=Address.TableID", "Address.Phone_no", "master_emp.User_id", clsApplicationSetting.GetSessionValue("LoginID"), " and address.TableName='EMP' and address.Doctype='Local' and address.isdeleted=0 and master_emp.isdeleted=0");
            if (!string.IsNullOrEmpty(Mobile))
            {
                string OTP = Common_SPU.FnSetOtp();
                string sMsg = "OTP for login is " + OTP + ".Please don't share it with anyone. This is confidential and to be used by you only. Thanks & Regards, Centre for Catalyzing Change";
                //string sMsg = "OTP for login is " + OTP + ".Please do not share it with anyone by any means. This is confidential and to be used by you only. Thanks & Regards, Centre for Catalyzing Change";//"OTP for login is " + OTP + ". Do not share it with anyone by any means. This is confidential and to be used by you only.\n\nThanks & Regards,\nCentre for Catalyzing Change";
                sMsg = HttpUtility.UrlEncode(sMsg, System.Text.Encoding.GetEncoding("ISO-8859-1"));
                SendMailHelper.SendSMS(Mobile, sMsg);
                Common_SPU.fnCreateMail_OTP(OTP);
                Status = Mobile;
            }
            return Status;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult OTPValidate(string src, Miscellaneous Modal, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.BackURL = Request.UrlReferrer.ToString();

            if (Command == "Resend")
            {
                long EmpId = 0;
                long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EmpId);
                if (EmpId > 0)
                {
                    //string Mobile = clsDataBaseHelper.fnGetOther_FieldName("master_emp inner join Address on master_emp.Address_id=Address.ID", "Address.Phone_no", "master_emp.User_id", clsApplicationSetting.GetSessionValue("LoginID"), " and  master_emp.isdeleted=0");
                    string MobileNo = SendUserOtp();
                    if (!string.IsNullOrEmpty(MobileNo))
                    {
                        //string OTP = Common_SPU.FnSetOtp();
                        //string sMsg = "Your OTP for MITR is " + OTP + ". Do not share OTP with anyone including C3 staff. Thanks and Regards, Centre for Catalyzing Change";
                        //SendMailHelper.SendSMS(Mobile, sMsg);

                        TempData["Success"] = "Y";
                        TempData["SuccessMsg"] = "OTP is successfully resent to " + MobileNo;
                    }
                    else
                    {
                        TempData["Success"] = "N";
                        TempData["SuccessMsg"] = "Mobile is not updated";
                    }
                }
                else
                {
                    TempData["Success"] = "N";
                    TempData["SuccessMsg"] = "OTP not required for current login user...";
                }
                return View(Modal);
                //return RedirectToAction("OTPValidate", "Account");
            }
            if (string.IsNullOrEmpty(Modal.OTP))
            {
                ModelState.AddModelError("OTP", "OTP can't be blank");
            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    string OTP = Modal.OTP;
                    long EmpId = 0;
                    long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EmpId);
                    if (EmpId > 0)
                    {
                        OTP = clsDataBaseHelper.fnGetOther_FieldName("OTP", "OTP", "User_ID", clsApplicationSetting.GetSessionValue("LoginID"), " and isdeleted=0");
                    }
                    //OTP = Modal.OTP;// Temporary because no sms Integration
                    if (OTP == Modal.OTP)
                    {
                        clsApplicationSetting.SetSessionValue("OTP", "Y");
                        TempData["Success"] = "Y";
                        TempData["SuccessMsg"] = "OTP is Matched";
                        return RedirectToAction("EssPortal", "Personnel", new { src = clsApplicationSetting.EncryptQueryString(119 + "*/Personnel/EssPortal") });
                    }
                    else
                    {
                        ModelState.AddModelError("OTP", "OTP is not Matched");
                        TempData["Success"] = "N";
                        TempData["SuccessMsg"] = "OTP is not Matched";
                        return View(Modal);
                    }
                }
                else
                {
                    return View(Modal);
                }
                //return RedirectToAction("OTPValidate", "Account");
            }
            else
            {
                return View(Modal);
            }
        }

        [HttpGet]
        public ActionResult GetOnlineUser()
        {
            NotificationComponent _messageRepository = new NotificationComponent();
            return PartialView("_LoggedInUser", _messageRepository.InvokeNotification());
        }

        public string Encryption(string Token, string Value)
        {
            string Output;
            string ActualToken = clsApplicationSetting.GetConfigValue("Token");

            if (Token == ActualToken)
            {
                Value = Value.Replace("-SLASH-", "/");
                Value = Value.Replace("-STAR-", "*");
                Output = clsApplicationSetting.EncryptFriendly(Value);
            }
            else
            {
                Output = "Invalid Token";
            }
            return Output;
        }
        public string Decryption(string Token, string Value)
        {
            string Output;
            string ActualToken = clsApplicationSetting.GetConfigValue("Token");
            if (Token == ActualToken)
            {
                Output = clsApplicationSetting.DecryptFriendly(Value);
            }
            else
            {
                Output = "Invalid Token";
            }
            return Output;
        }

        [HttpPost]
        public ActionResult _ForgotPassword()
        {

            ForgotPassword.Request Modal = new ForgotPassword.Request();
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _SubmitForgotPassword(ForgotPassword.Request Modal, string Command)
        {

            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Not found anything";
            if (ModelState.IsValid)
            {
                var getDateTime = DateTime.Now.ToString("yyyy-MM-ddhh:mm:sstt");
                Modal.Token = clsApplicationSetting.EncryptQueryString(getDateTime);
                Modal.IPAddress = IPAddress;
                PostResult = Common_SPU.fnCreateMail_ForgotPassword(Modal);
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangePassword(string Token)
        {
            ChangePassword Modal = new ChangePassword();

            if (string.IsNullOrEmpty(Token) && LoginID == 0)
            {
                return RedirectToAction("PageNotFound");
            }
            else if (!string.IsNullOrEmpty(Token))
            {
                GetValidateToken getResp = new GetValidateToken();
                getResp.LoginID = LoginID;
                getResp.IPAddress = IPAddress;
                getResp.Token = Token;
                getResp.Doctype = "ChangePassword";
                var Sat = Common_SPU.fnGetValidateToken(getResp);
                if (Sat.Status)
                {
                    Modal.Token = Token;
                    Modal.OldPassword = "NA";
                    Modal.LoginID = Sat.ID;
                }
                else
                {
                    TempData["LoginErrorMsg"] = Sat.SuccessMessage;
                    return RedirectToAction("PageNotFound");
                }
            }
            else
            {
                Modal.LoginID = LoginID;
            }
            return View(Modal);
        }
        public ActionResult MChangePassword(string src)
        {
            ViewBag.src = src;
            int EMPID = 0;
            int UserId = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (GetQueryString.Length > 3)
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.LocationId = GetQueryString[5];
                ViewBag.EMPID = GetQueryString[2];
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
            }
            ChangePassword changePassword = new ChangePassword();
            changePassword.LoginID = UserId;
            return View(changePassword);
        }
        [HttpPost]
        public ActionResult MChangePassword(ChangePassword Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Not found anything";
            if (Modal.NewPassword != Modal.ConfirmPassword)
            {
                PostResult.SuccessMessage = "Password and Confirm Password must be same";
                //TempData["LoginErrorMsg"] = PostResult.SuccessMessage;
                ModelState.AddModelError("ConfirmPassword", PostResult.SuccessMessage);
            }
            if (ModelState.IsValid)
            {
                Modal.IPAddress = IPAddress;
                Modal.NewPassword = clsApplicationSetting.Encrypt(Modal.NewPassword);
                Modal.OldPassword = clsApplicationSetting.Encrypt(Modal.OldPassword);
                PostResult = Common_SPU.fnSetPasswordChangeForMobile(Modal);
                TempData["LoginErrorMsg"] = PostResult.SuccessMessage;
            }
            if (PostResult.Status)
            {
                return RedirectToAction("logout", "");
            }
            return View(Modal);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Not found anything";
            if (Modal.NewPassword != Modal.ConfirmPassword)
            {
                PostResult.SuccessMessage = "Password and Confirm Password must be same";
               // TempData["LoginErrorMsg"] = PostResult.SuccessMessage;
               ModelState.AddModelError("ConfirmPassword", PostResult.SuccessMessage);
            }
            if (ModelState.IsValid)
            {
                Modal.IPAddress = IPAddress;
                Modal.NewPassword = clsApplicationSetting.Encrypt(Modal.NewPassword);
                Modal.OldPassword = clsApplicationSetting.Encrypt(Modal.OldPassword);
                PostResult = Common_SPU.fnSetPasswordChange(Modal);
                TempData["LoginErrorMsg"] = PostResult.SuccessMessage;
            }
            if (PostResult.Status)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(Modal);
        }

        public ActionResult TravelDeskTicket()
        {

            return View();
        }
        public ActionResult MDashboard(string src)
        {
            ViewBag.src = src;
            bool IsValidUser = false;
            int EMPID = 0;
            int UserId = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.EmpId = GetQueryString[3];
            ViewBag.UserId = GetQueryString[8];
            int.TryParse(ViewBag.EmpId, out EMPID);
            int.TryParse(ViewBag.UserId, out UserId);

            if (EMPID > 0 && UserId > 0)
            {
                DataSet ds = Common_SPU.fnGetLoginDataforMobile(UserId, EMPID);
                IsValidUser = Convert.ToBoolean(ds.Tables[0].Rows[0]["status"].ToString());
                if (IsValidUser == false)
                {
                    ds = Common_SPU.fnGetLoginDataforMobilenonmitr(UserId, EMPID);
                    IsValidUser = Convert.ToBoolean(ds.Tables[0].Rows[0]["status"].ToString());

                }
                if (IsValidUser)
                {

                    ViewBag.MenuID = GetQueryString[0];
                    ViewBag.Mobile = "Mobile";
                    ViewBag.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                    ViewBag.Desgination = ds.Tables[0].Rows[0]["Designation"].ToString();
                    ViewBag.img = ds.Tables[0].Rows[0]["AttachmentID"].ToString();
                    ViewBag.LocationId = ds.Tables[0].Rows[0]["LocationID"].ToString();
                }

                //return RedirectToAction("MDashboard", "Account");

            }


            return View();

        }


        public ActionResult MIOSDashboard(string src)
        {
            ViewBag.src = src;
            int EMPID = 0;
            int UserId = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.UserId = GetQueryString[3];
            int.TryParse(ViewBag.UserId, out UserId);
            if (UserId > 0)
            {
                DataSet ds = Common_SPU.fnGetMobileLoginIOS(UserId);
                ViewBag.FullName = ds.Tables[0].Rows[0]["FullName"].ToString(); ;
                ViewBag.Mobile = "IOSMobile";
            }


            return View();

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updateSallaryDetailsEmp(EmployeeMasterUpdate obj)
        {
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;

            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(obj.src);

            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.EMPID = GetQueryString[2];
                if (GetQueryString.Length > 3)
                {
                    Mobile = GetQueryString[3];
                }
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
                obj.Mobile = Mobile;
                obj.LoginID = UserId;
                obj.EMPID = EMPID;

            }

            PostResult = employee.UpdatebankdetailsEmployee(obj);
            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
                Common_SPU.fnCreateMail_UserUpdateProfile();



            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updatePersonalDetailsDetailsEmp(EmployeeMasterUpdate obj)
        {
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(obj.src);
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.EMPID = GetQueryString[2];
                ViewBag.UserId = GetQueryString[4];
                if (GetQueryString.Length > 3)
                {
                    Mobile = GetQueryString[3];
                }
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
                obj.Mobile = Mobile;
                obj.LoginID = UserId;
                obj.EMPID = EMPID;

            }

            PostResult = employee.UpdatebankdetailsEmployee(obj);
            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
                Common_SPU.fnCreateMail_UserUpdateProfile();


            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updateLocalDetailsDetailsEmp(EmployeeMasterUpdate obj)
        {
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(obj.src);
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.EMPID = GetQueryString[2];
                ViewBag.UserId = GetQueryString[4];
                if (GetQueryString.Length > 3)
                {
                    Mobile = GetQueryString[3];
                }
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
                obj.Mobile = Mobile;
                obj.LoginID = UserId;
                obj.EMPID = EMPID;

            }

            PostResult = employee.UpdatebankdetailsEmployee(obj);
            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
                Common_SPU.fnCreateMail_UserUpdateProfile();



            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updateMealDetailsDetailsEmp(EmployeeMasterUpdate obj)
        {
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(obj.src);
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.EMPID = GetQueryString[2];
                ViewBag.UserId = GetQueryString[4];
                if (GetQueryString.Length > 3)
                {
                    Mobile = GetQueryString[3];
                }
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
                obj.Mobile = Mobile;
                obj.LoginID = UserId;
                obj.EMPID = EMPID;

            }

            PostResult = employee.UpdatebankdetailsEmployee(obj);
            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";

                Common_SPU.fnCreateMail_UserUpdateProfile();

            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updateMedicalDetailsDetailsEmp(EmployeeMasterUpdate obj)
        {
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(obj.src);
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.EMPID = GetQueryString[2];
                ViewBag.UserId = GetQueryString[4];
                if (GetQueryString.Length > 3)
                {
                    Mobile = GetQueryString[3];
                }
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
                obj.Mobile = Mobile;
                obj.LoginID = UserId;
                obj.EMPID = EMPID;

            }

            PostResult = employee.UpdatebankdetailsEmployee(obj);
            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";

                Common_SPU.fnCreateMail_UserUpdateProfile();

            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult updateQualificationDetailsDetailsEmp(List<Qualification> qualifications)
        {
            PostResponse PostResult = new PostResponse();
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            if (qualifications.Count > 0)
            {
                foreach (var item in qualifications.Where(x => x.QID == 0).ToList())
                {

                    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(item.src);
                    if (GetQueryString.Length > 3 && GetQueryString[3] != "")
                    {
                        ViewBag.GetQueryString = GetQueryString;
                        ViewBag.MenuID = GetQueryString[0];
                        ViewBag.EMPID = GetQueryString[2];
                        ViewBag.UserId = GetQueryString[4];
                        if (GetQueryString.Length > 3)
                        {
                            Mobile = GetQueryString[3];
                        }
                        int.TryParse(ViewBag.EMPID, out EMPID);
                        int.TryParse(ViewBag.UserId, out UserId);
                        obj.Mobile = Mobile;
                        obj.LoginID = UserId;
                        obj.EMPID = EMPID;

                    }
                    if (item.Course != null)
                    {
                        obj.Course = item.Course;
                        obj.University = item.University;
                        obj.Location = item.Location;
                        obj.Year = item.Year;
                        obj.Doctype = "Qualification";
                        PostResult = employee.UpdatebankdetailsEmployee(obj);
                    }
                    else
                    {
                        PostResult.Status = false;
                    }
          
                }
            }
            //    PostResult = employee.UpdatebankdetailsEmployee(obj);
            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
                Common_SPU.fnCreateMail_UserUpdateProfile();


            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult updateFlyerDetailsDetailsEmp(List<Flyer> Flyer)
        {
            PostResponse PostResult = new PostResponse();
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            if (Flyer.Count > 0)
            {
                foreach (var item in Flyer.Where(x => x.ID == 0).ToList())
                {
                    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(item.src);
                    if (GetQueryString.Length > 3 && GetQueryString[3] != "")
                    {
                        ViewBag.GetQueryString = GetQueryString;
                        ViewBag.MenuID = GetQueryString[0];
                        ViewBag.EMPID = GetQueryString[2];
                        ViewBag.UserId = GetQueryString[4];
                        if (GetQueryString.Length > 3)
                        {
                            Mobile = GetQueryString[3];
                        }
                        int.TryParse(ViewBag.EMPID, out EMPID);
                        int.TryParse(ViewBag.UserId, out UserId);
                        obj.Mobile = Mobile;
                        obj.LoginID = UserId;
                        obj.EMPID = EMPID;

                    }
                    if (item.AirlineName != null)
                    {
                        obj.FlyerNumber = item.FlyerNumber;
                        obj.AirlineName = item.AirlineName;
                        obj.Doctype = "Flyer";
                        PostResult = employee.UpdatebankdetailsEmployee(obj);
                    }
                    else
                    {
                        PostResult.Status = false;
                    }
                   
                }
            }
            //    PostResult = employee.UpdatebankdetailsEmployee(obj);
            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
                Common_SPU.fnCreateMail_UserUpdateProfile();



            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updatePIODetailsDetailsEmp(string PIO, string PIOName)
        {
            PostResponse PostResult = new PostResponse();
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            obj.PIO = PIO;
            obj.PIOName = PIOName;
            obj.Doctype = "PIO/OCI";
            PostResult = employee.UpdatebankdetailsEmployee(obj);
            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";

                Common_SPU.fnCreateMail_UserUpdateProfile();

            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updateProfessinalDetailsDetailsEmp(string Ammount, string Remarks)
        {
            PostResponse PostResult = new PostResponse();
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            obj.Ammount = Convert.ToDecimal(Ammount);
            obj.Remarks = Remarks;
            obj.Doctype = "ProfessionalTax ";
            PostResult = employee.UpdatebankdetailsEmployee(obj);
            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
                Common_SPU.fnCreateMail_UserUpdateProfile();


            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updateOPFDetailsDetailsEmp(string OPF)
        {
            PostResponse PostResult = new PostResponse();
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            obj.OldPFNo = OPF;
            obj.Doctype = "Old PF";
            PostResult = employee.UpdatebankdetailsEmployee(obj);

            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
                Common_SPU.fnCreateMail_UserUpdateProfile();


            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult updatePFDetailsEmp()
        {
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        obj.UploadAttachment = file;
                        var RvFile = clsApplicationSetting.ValidateFile(obj.UploadAttachment);
                        if (RvFile.IsValid)
                        {
                            obj.AttachmentID = Common_SPU.fnSetAttachments(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", "0", "PF");
                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + obj.AttachmentID + RvFile.FileExt);
                            }
                            obj.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt));
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }

        [HttpPost]
        public ActionResult updateDinDetailsEmp(string src)
        {
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            PostResponse PostResult = new PostResponse();
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        obj.UploadAttachment = file;
                        var RvFile = clsApplicationSetting.ValidateFile(obj.UploadAttachment);
                        if (RvFile.IsValid)
                        {
                            obj.AttachmentID = Common_SPU.fnSetAttachments(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", "0", "Director Identification Number");
                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + obj.AttachmentID + RvFile.FileExt);
                            }
                            obj.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt));
                            obj.Doctype = "Director Identification Number doc";
                            PostResult = employee.UpdatebankdetailsEmployee(obj);
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }
        [HttpPost]
        public ActionResult updateVoterDetailsEmp(string src)
        {
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];

                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        obj.UploadAttachment = file;
                        var RvFile = clsApplicationSetting.ValidateFile(obj.UploadAttachment);
                        if (RvFile.IsValid)
                        {
                            if (GetQueryString.Length > 3)
                            {
                                ViewBag.UserId = GetQueryString[4];
                                int.TryParse(ViewBag.UserId, out UserId);
                                obj.AttachmentID = Common_SPU.fnSetAttachmentsforMobile(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", UserId, "", "Voter ID");
                            }
                            else
                            {
                                obj.AttachmentID = Common_SPU.fnSetAttachments(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", "0", "Voter ID");
                            }


                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + obj.AttachmentID + RvFile.FileExt);
                            }
                            obj.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt));
                            obj.Doctype = "Voter ID doc";

                            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
                            {
                                ViewBag.GetQueryString = GetQueryString;
                                ViewBag.MenuID = GetQueryString[0];
                                ViewBag.EMPID = GetQueryString[2];
                                ViewBag.UserId = GetQueryString[4];
                                if (GetQueryString.Length > 3)
                                {
                                    Mobile = GetQueryString[3];
                                }
                                int.TryParse(ViewBag.EMPID, out EMPID);
                                int.TryParse(ViewBag.UserId, out UserId);
                                obj.Mobile = Mobile;
                                obj.LoginID = UserId;
                                obj.EMPID = EMPID;

                            }
                            PostResult = employee.UpdatebankdetailsEmployee(obj);
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }
        [HttpPost]
        public ActionResult updatePassportDetailsEmp(string src)
        {
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        obj.UploadAttachment = file;
                        var RvFile = clsApplicationSetting.ValidateFile(obj.UploadAttachment);
                        if (RvFile.IsValid)
                        {

                            if (GetQueryString.Length > 3)
                            {
                                ViewBag.UserId = GetQueryString[4];
                                int.TryParse(ViewBag.UserId, out UserId);
                                obj.AttachmentID = Common_SPU.fnSetAttachmentsforMobile(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", UserId, "", "Passport");
                            }
                            else
                            {
                                obj.AttachmentID = Common_SPU.fnSetAttachments(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", "0", "Passport");
                            }

                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + obj.AttachmentID + RvFile.FileExt);
                            }
                            obj.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt));
                            obj.Doctype = "Passport doc";

                            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
                            {
                                ViewBag.GetQueryString = GetQueryString;
                                ViewBag.MenuID = GetQueryString[0];
                                ViewBag.EMPID = GetQueryString[2];
                                ViewBag.UserId = GetQueryString[4];
                                if (GetQueryString.Length > 3)
                                {
                                    Mobile = GetQueryString[3];
                                }
                                int.TryParse(ViewBag.EMPID, out EMPID);
                                int.TryParse(ViewBag.UserId, out UserId);
                                obj.Mobile = Mobile;
                                obj.LoginID = UserId;
                                obj.EMPID = EMPID;

                            }
                            PostResult = employee.UpdatebankdetailsEmployee(obj);
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }

        [HttpPost]
        public ActionResult updateDlDetailsEmp(string src)
        {
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        obj.UploadAttachment = file;
                        var RvFile = clsApplicationSetting.ValidateFile(obj.UploadAttachment);
                        if (RvFile.IsValid)
                        {
                            if (GetQueryString.Length > 3)
                            {
                                ViewBag.UserId = GetQueryString[4];
                                int.TryParse(ViewBag.UserId, out UserId);
                                obj.AttachmentID = Common_SPU.fnSetAttachmentsforMobile(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", UserId, "", "Driving License");
                            }
                            else
                            {
                                obj.AttachmentID = Common_SPU.fnSetAttachments(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", "0", "Driving License");
                            }


                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + obj.AttachmentID + RvFile.FileExt);
                            }
                            obj.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt));
                            obj.Doctype = "Driving License doc";

                            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
                            {
                                ViewBag.GetQueryString = GetQueryString;
                                ViewBag.MenuID = GetQueryString[0];
                                ViewBag.EMPID = GetQueryString[2];
                                ViewBag.UserId = GetQueryString[4];
                                if (GetQueryString.Length > 3)
                                {
                                    Mobile = GetQueryString[3];
                                }
                                int.TryParse(ViewBag.EMPID, out EMPID);
                                int.TryParse(ViewBag.UserId, out UserId);
                                obj.Mobile = Mobile;
                                obj.LoginID = UserId;
                                obj.EMPID = EMPID;

                            }
                            PostResult = employee.UpdatebankdetailsEmployee(obj);
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updateVotersDetailsEmp(string VoterId, string src)
        {
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.EMPID = GetQueryString[2];
                ViewBag.UserId = GetQueryString[4];
                if (GetQueryString.Length > 3)
                {
                    Mobile = GetQueryString[3];
                }
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
                obj.Mobile = Mobile;
                obj.LoginID = UserId;
                obj.EMPID = EMPID;

            }
            obj.VoterId = VoterId;
            obj.Doctype = "Voter ID";
            PostResult = employee.UpdatebankdetailsEmployee(obj);

            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updatePassportDetailsEmp(string PassportNo, string PlaceOfissue, string PassportName, string ExpDate, string src)
        {
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.EMPID = GetQueryString[2];
                ViewBag.UserId = GetQueryString[4];
                if (GetQueryString.Length > 3)
                {
                    Mobile = GetQueryString[3];
                }
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
                obj.Mobile = Mobile;
                obj.LoginID = UserId;
                obj.EMPID = EMPID;

            }
            obj.PassportName = PassportName;
            obj.PassportPlaceOfissue = PlaceOfissue;
            obj.PassportName = PassportName;
            obj.PassportExpDate = ExpDate;
            obj.Doctype = "Passport";
            PostResult = employee.UpdatebankdetailsEmployee(obj);

            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updateDinDetailsEmp(string DinNo, string DinName, string src)
        {
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.EMPID = GetQueryString[2];
                ViewBag.UserId = GetQueryString[4];
                if (GetQueryString.Length > 3)
                {
                    Mobile = GetQueryString[3];
                }
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
                obj.Mobile = Mobile;
                obj.LoginID = UserId;
                obj.EMPID = EMPID;

            }
            obj.DinNo = DinNo;
            obj.DinName = DinName;
            obj.Doctype = "Director Identification Number";
            PostResult = employee.UpdatebankdetailsEmployee(obj);

            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult updateDlDetailsEmp(string DlNo, string DlName, string IssueDate, string ExpDate, string PlaceOfIssue, string Remarks, string src)
        {
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.EMPID = GetQueryString[2];
                ViewBag.UserId = GetQueryString[4];
                if (GetQueryString.Length > 3)
                {
                    Mobile = GetQueryString[3];
                }
                int.TryParse(ViewBag.EMPID, out EMPID);
                int.TryParse(ViewBag.UserId, out UserId);
                obj.Mobile = Mobile;
                obj.LoginID = UserId;
                obj.EMPID = EMPID;

            }
            obj.DlNo = DlNo;
            obj.DlName = DlName;
            obj.IssueDate = IssueDate;
            obj.DlExpDate = ExpDate;
            obj.DlPlaceOfissue = PlaceOfIssue;
            obj.DlRemarks = Remarks;
            obj.Doctype = "Driving License";
            PostResult = employee.UpdatebankdetailsEmployee(obj);

            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Action Save Data";
            }
            else
            {
                PostResult.SuccessMessage = "Action Save not Data";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult updateAdharDetailsEmp(string src)
        {
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            if (Request.Files.Count > 0)
            {
                try
                {

                    //  Get all files from Request object
                    if (GetQueryString.Length > 3 && GetQueryString[3] != "")
                    {
                        ViewBag.GetQueryString = GetQueryString;
                        ViewBag.MenuID = GetQueryString[0];
                        ViewBag.EMPID = GetQueryString[2];

                        if (GetQueryString.Length > 3)
                        {
                            Mobile = GetQueryString[3];
                        }
                        int.TryParse(ViewBag.EMPID, out EMPID);

                        obj.Mobile = Mobile;
                        obj.LoginID = UserId;
                        obj.EMPID = EMPID;

                    }
                    if (EMPID == 0)
                    {
                        EMPID = Convert.ToInt32(clsApplicationSetting.GetSessionValue("EMPID"));
                    }
                    string SQL = "select EAttachID from EMP_Attachments where EMPID = " + EMPID + " and OfficialName = 'Aadhaar' and isdeleted = 0";
                    DataTable dt = clsDataBaseHelper.ExecuteDataTable(SQL, "A");
                    long Id = 0;
                    foreach (DataRow item in dt.Rows)
                    {

                        Id = Convert.ToInt32(item["EAttachID"]);

                    }
                    if (Id > 0)
                    {
                        HttpFileCollectionBase files = Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                            //string filename = Path.GetFileName(Request.Files[i].FileName);  

                            HttpPostedFileBase file = files[i];
                            string fname;

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }
                            obj.UploadAttachment = file;
                            var RvFile = clsApplicationSetting.ValidateFile(obj.UploadAttachment);
                            if (RvFile.IsValid)
                            {
                                if (GetQueryString.Length > 3)
                                {
                                    ViewBag.UserId = GetQueryString[4];
                                    int.TryParse(ViewBag.UserId, out UserId);
                                    obj.AttachmentID = Common_SPU.fnSetAttachmentsforMobile(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", UserId, "", "Aadhaar");
                                }
                                else
                                {
                                    obj.AttachmentID = Common_SPU.fnSetAttachments(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", "0", "Aadhaar");
                                }

                                if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt)))
                                {
                                    System.IO.File.Delete("~/Attachments/" + obj.AttachmentID + RvFile.FileExt);
                                }
                                obj.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt));
                                obj.Doctype = "Aadhaar doc";


                                PostResult = employee.UpdatebankdetailsEmployee(obj);
                            }
                        }
                        // Returns message that successfully uploaded  
                        return Json("File Uploaded Successfully!");
                    }
                    else
                    {
                        return Json("First Add Aadhaar Details!");
                    }

                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }

        [HttpPost]
        public ActionResult updatePanDetailsEmp(string src)
        {
            EmployeeMasterUpdate obj = new EmployeeMasterUpdate();
            PostResponse PostResult = new PostResponse();
            int EMPID = 0;
            int UserId = 0;
            string Mobile = string.Empty;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            if (Request.Files.Count > 0)
            {
                try
                {
                    if (GetQueryString.Length > 3 && GetQueryString[3] != "")
                    {
                        ViewBag.GetQueryString = GetQueryString;
                        ViewBag.MenuID = GetQueryString[0];
                        ViewBag.EMPID = GetQueryString[2];
                        ViewBag.UserId = GetQueryString[4];
                        if (GetQueryString.Length > 3)
                        {
                            Mobile = GetQueryString[3];
                        }
                        int.TryParse(ViewBag.EMPID, out EMPID);
                        int.TryParse(ViewBag.UserId, out UserId);
                        obj.Mobile = Mobile;
                        obj.LoginID = UserId;
                        obj.EMPID = EMPID;

                    }
                    if (EMPID == 0)
                    {
                        EMPID = Convert.ToInt32(clsApplicationSetting.GetSessionValue("EMPID"));
                    }
                    string SQL = "select EAttachID from EMP_Attachments where EMPID = " + EMPID + " and OfficialName = 'PAN' and isdeleted = 0";
                    DataTable dt = clsDataBaseHelper.ExecuteDataTable(SQL, "A");
                    long Id = 0;
                    foreach (DataRow item in dt.Rows)
                    {

                        Id = Convert.ToInt32(item["EAttachID"]);

                    }
                    //  Get all files from Request object  
                    if (Id > 0)
                    {
                        HttpFileCollectionBase files = Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                            //string filename = Path.GetFileName(Request.Files[i].FileName);  

                            HttpPostedFileBase file = files[i];
                            string fname;

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }
                            obj.UploadAttachment = file;
                            var RvFile = clsApplicationSetting.ValidateFile(obj.UploadAttachment);
                            if (RvFile.IsValid)
                            {
                                // obj.AttachmentID = Common_SPU.fnSetAttachments(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", "0", "PAN");

                                if (GetQueryString.Length > 3)
                                {
                                    ViewBag.UserId = GetQueryString[4];
                                    int.TryParse(ViewBag.UserId, out UserId);
                                    obj.AttachmentID = Common_SPU.fnSetAttachmentsforMobile(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", UserId, "", "PAN");
                                }
                                else
                                {
                                    obj.AttachmentID = Common_SPU.fnSetAttachments(obj.AttachmentID, RvFile.FileName, RvFile.FileExt, "", "0", "PAN");
                                }


                                if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt)))
                                {
                                    System.IO.File.Delete("~/Attachments/" + obj.AttachmentID + RvFile.FileExt);
                                }
                                obj.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + obj.AttachmentID + RvFile.FileExt));
                                obj.Doctype = "PAN doc";


                                PostResult = employee.UpdatebankdetailsEmployee(obj);
                            }
                        }
                        // Returns message that successfully uploaded  
                        return Json("File Uploaded Successfully!");
                    }
                    else
                    {
                        return Json("First Add Pan Details!");
                    }

                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }


        public ActionResult RegistrationDetailsIOS(string src)
        {
            int UserId = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.UserId = GetQueryString[2];
            int.TryParse(ViewBag.UserId, out UserId);
            if (GetQueryString.Length > 3)
            {
                ViewBag.UserId = GetQueryString[2];
                ViewBag.Mobile = GetQueryString[3];

            }


            Employee.RegistrationDetailsIOS Modal = new Employee.RegistrationDetailsIOS();

            DataSet ds = Common_SPU.fnGetMobileLoginIOS(UserId);
            try
            {
                if (Convert.ToInt64(ds.Tables[0].Rows[0]["Id"]) > 0)
                {
                    Modal.EmployeeName = ds.Tables[0].Rows[0]["FullName"].ToString();
                    Modal.Mobile = ds.Tables[0].Rows[0]["Mobile_no"].ToString();
                    Modal.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                    Modal.ZipCode = ds.Tables[0].Rows[0]["ZipCode"].ToString();
                    Modal.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    Modal.EMail = ds.Tables[0].Rows[0]["email"].ToString();
                    Modal.Id = Convert.ToInt64(ds.Tables[0].Rows[0]["ID"]);
                    Modal.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                }
            }
            catch { }


            return View(Modal);
        }
        [HttpPost]
        public ActionResult RegistrationDetailsIOS(string src, Employee.RegistrationDetailsIOS Modal)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            int UserId = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.UserId = GetQueryString[2];
            int.TryParse(ViewBag.UserId, out UserId);
            if (GetQueryString.Length > 3)
            {
                ViewBag.UserId = GetQueryString[2];
                ViewBag.Mobile = GetQueryString[3];

            }


            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "User is not Saved";
            bool status = false;
            string Msg = "";
            if (ModelState.IsValid)
            {


                SaveID = Common_SPU.fnSetGlobalUser(Modal.Id, Modal.EmployeeName, Modal.Mobile, Modal.EMail, Modal.Address, Modal.ZipCode, Modal.DOB, Modal.CompanyName);
                status = true;
                Msg = "User Updated Successfully";

                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);



        }
    }

}

