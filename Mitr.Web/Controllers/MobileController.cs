using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Mitr.CommonClass;
using Mitr.Models;
using System.Web;

namespace Mitr.Controllers
{
    [RoutePrefix("api/Mobile")]
    public class MobileController : ApiController
    {

        [HttpGet]
        [Route("GetLogin")]
        public JsonResult<UserApi> GetLogin(string sUserID, string Password)
        {

            bool IsValidUser = false;
            bool IsValidUserIOS = false;
            var context = (HttpContextWrapper)Request.Properties["MS_HttpContext"];
            // var sessionId = context.Request.Params["ASP.NET_SessionId"];
            //fnGetLogin
            string sessionId = "b0mgkltlwpuxpiu00ydoxlud";
            UserApi user = new UserApi();
            DataSet ds = Common_SPU.fnGetLogin(sUserID, clsApplicationSetting.Encrypt(Password), sessionId);
            //DataSet ds = Common_SPU.fnGetMobileLogin(sUserID, clsApplicationSetting.Encrypt(Password), sessionId);
            IsValidUser = Convert.ToBoolean(ds.Tables[0].Rows[0]["status"].ToString());
            if (IsValidUser == false)
            {
                ds = Common_SPU.fnGetMobileLogin(sUserID, clsApplicationSetting.Encrypt(Password), sessionId);
                IsValidUser = Convert.ToBoolean(ds.Tables[0].Rows[0]["status"].ToString());
            }
            if (IsValidUser == false)
            {
                ds = Common_SPU.fnGetMobileLoginGlobal(sUserID, clsApplicationSetting.Encrypt(Password), sessionId);
                IsValidUserIOS = Convert.ToBoolean(ds.Tables[0].Rows[0]["status"].ToString());
            }
            if (IsValidUser)
            {
                string Url = "/Account/MDashboard?src=" + clsApplicationSetting.EncryptQueryString("0" + "*/Account/MDashboard*" + "*" + ds.Tables[0].Rows[0]["EMPID"] + "*" + "Mobile" + "*" + ds.Tables[0].Rows[0]["FullName"].ToString() + "*" + ds.Tables[0].Rows[0]["Designation"].ToString() + "*" + ds.Tables[0].Rows[0]["AttachmentID"].ToString() + "*" + ds.Tables[0].Rows[0]["LoginID"].ToString() + "*" + ds.Tables[0].Rows[0]["LocationID"].ToString());
                IsValidUser = Convert.ToBoolean(ds.Tables[0].Rows[0]["status"].ToString());
                user.Url = Url;
                user.UserID = Convert.ToInt64(ds.Tables[0].Rows[0]["LoginID"]);
                user.EmpName = ds.Tables[0].Rows[0]["FullName"].ToString();
                user.EMPCode = ds.Tables[0].Rows[0]["EMPCode"].ToString();
                user.Desgination = ds.Tables[0].Rows[0]["Designation"].ToString();
                user.AttachmentID = ds.Tables[0].Rows[0]["AttachmentID"].ToString();
                user.Status = "Success";
            }
            else if (IsValidUserIOS)
            {
                string Url = "/Account/MIOSDashboard?src=" + clsApplicationSetting.EncryptQueryString("0" + "*/Account/MIOSDashboard*" + "*" + ds.Tables[0].Rows[0]["LoginID"] + "*" + "IOSMobile" + "*");
                //string Url = "/Account/MIOSDashboard?src=" + clsApplicationSetting.EncryptQueryString("0" + "*/Account/MIOSDashboard*" + "*" + ds.Tables[0].Rows[0]["EMPID"] + "*" + "IOSMobile" + "*" + ds.Tables[0].Rows[0]["FullName"].ToString() + "*" + ds.Tables[0].Rows[0]["Designation"].ToString() + "*" + ds.Tables[0].Rows[0]["AttachmentID"].ToString() + "*" + ds.Tables[0].Rows[0]["LoginID"].ToString() + "*" + ds.Tables[0].Rows[0]["LocationID"].ToString());
                IsValidUser = Convert.ToBoolean(ds.Tables[0].Rows[0]["status"].ToString());
                user.Url = Url;
                user.UserID = Convert.ToInt64(ds.Tables[0].Rows[0]["LoginID"]);
                user.EmpName = ds.Tables[0].Rows[0]["FullName"].ToString();
                user.EMPCode = ds.Tables[0].Rows[0]["EMPCode"].ToString();
                user.Desgination = ds.Tables[0].Rows[0]["Designation"].ToString();
                user.AttachmentID = ds.Tables[0].Rows[0]["AttachmentID"].ToString();
                user.Status = "Success";
            }
            else
            {


                user.Url = "";
                user.UserID = 0;
                user.EmpName = "";
                user.EMPCode = "";
                user.Desgination = "";
                user.AttachmentID = "";
                user.Status = "Fail";
            }

            return Json<UserApi>(user);
        }

        [HttpGet]
        [Route("GetLogout")]
        public JsonResult<UserApi> Logout(long LoginID)
        {
            UserApi user = new UserApi();
            if (LoginID > 0)
            {
                clsDataBaseHelper.ExecuteNonQuery("update userman set IsLogin=0,LoginOutTime=getdate() where ID=" + LoginID + "");
                //  clsApplicationSetting.ClearSessionValues();
                user.Url = "";
                user.UserID = 0;
                user.EmpName = "";
                user.EMPCode = "";
                user.Desgination = "";
                user.Status = "Success";
            }
            else
            {
                user.Url = "";
                user.UserID = 0;
                user.EmpName = "";
                user.EMPCode = "";
                user.Desgination = "";
                user.Status = "Fail";
            }

            return Json<UserApi>(user);
        }


        [HttpGet]
        [Route("GetSignUp")]
        public JsonResult<UserApi> GetSignUp(string UserName, string Email, string Mobile)
        {

            UserApi user = new UserApi();
            CommandResult Result = new CommandResult();

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Mobile))
            {
                string firstUserName = UserName.Substring(0, 4);
                string mobile = Mobile.Substring(0, 4);
                string PasswordGen = firstUserName + "@" + mobile;
                string Password = clsApplicationSetting.Encrypt(PasswordGen);
                Result = Common_SPU.fnSetUser_Registration(0, UserName, Email, Mobile, Password);
                user.Response = Result.SuccessMessage;
                user.Status = "Success";
                user.Url = "";
                user.UserID = 0;
                user.EmpName = "";
                user.EMPCode = "";
                user.Desgination = "";
                user.AttachmentID = "";

            }
            else
            {


                user.Response = "Fail To saved";
                user.Status = "Fail";
                user.Url = "";
                user.UserID = 0;
                user.EmpName = "";
                user.EMPCode = "";
                user.Desgination = "";
                user.AttachmentID = "";
            }

            return Json<UserApi>(user);
        }

        [HttpGet]
        [Route("GetLoginStatusUser")]
        public JsonResult<UserApi> GetLoginStatusUser(string sUserID, string Password)
        {
            bool IsValidUser = false;
            bool IsValidUserIOS = false;
            var context = (HttpContextWrapper)Request.Properties["MS_HttpContext"];
            // var sessionId = context.Request.Params["ASP.NET_SessionId"];
            //fnGetLogin
            string sessionId = "b0mgkltlwpuxpiu00ydoxlud";
            UserApi user = new UserApi();
            DataSet ds = Common_SPU.fnGetLogin(sUserID, clsApplicationSetting.Encrypt(Password), sessionId);
            //DataSet ds = Common_SPU.fnGetMobileLogin(sUserID, clsApplicationSetting.Encrypt(Password), sessionId);
            IsValidUser = Convert.ToBoolean(ds.Tables[0].Rows[0]["status"].ToString());
            if (IsValidUser == false)
            {
                ds = Common_SPU.fnGetMobileLogin(sUserID, clsApplicationSetting.Encrypt(Password), sessionId);
                IsValidUser = Convert.ToBoolean(ds.Tables[0].Rows[0]["status"].ToString());
            }
            if (IsValidUser == false)
            {
                ds = Common_SPU.fnGetMobileLoginGlobal(sUserID, clsApplicationSetting.Encrypt(Password), sessionId);
                IsValidUserIOS = Convert.ToBoolean(ds.Tables[0].Rows[0]["status"].ToString());
            }
            if (IsValidUser)
            {
                user.Url = "";
                user.UserID = Convert.ToInt64(ds.Tables[0].Rows[0]["LoginID"]);
                user.EmpName = ds.Tables[0].Rows[0]["FullName"].ToString();
                user.EMPCode = ds.Tables[0].Rows[0]["EMPCode"].ToString();
                user.Desgination = ds.Tables[0].Rows[0]["Designation"].ToString();
                user.AttachmentID = ds.Tables[0].Rows[0]["AttachmentID"].ToString();
                user.Status = "Success";
            }
            else if (IsValidUserIOS)
            {
                user.Url = "";
                user.UserID = Convert.ToInt64(ds.Tables[0].Rows[0]["LoginID"]);
                user.EmpName = ds.Tables[0].Rows[0]["FullName"].ToString();
                user.EMPCode = ds.Tables[0].Rows[0]["EMPCode"].ToString();
                user.Desgination = ds.Tables[0].Rows[0]["Designation"].ToString();
                user.AttachmentID = ds.Tables[0].Rows[0]["AttachmentID"].ToString();
                user.Status = "Success";
            }
            else
            {


                user.Url = "";
                user.UserID = 0;
                user.EmpName = "";
                user.EMPCode = "";
                user.Desgination = "";
                user.AttachmentID = "";
                user.Status = "Fail";
            }

            return Json<UserApi>(user);

          
        }

        [HttpGet]
        [Route("GetLoginPassword")]
        public JsonResult<PostResponse> GetLoginPassword(string sUserID)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Not found anything";
            ForgotPassword.Request Modal = new ForgotPassword.Request();
            if (ModelState.IsValid)
            {
                var getDateTime = DateTime.Now.ToString("yyyy-MM-ddhh:mm:sstt");
                Modal.Username = sUserID;
                Modal.Token = clsApplicationSetting.EncryptQueryString(getDateTime);
                Modal.IPAddress = ClsCommon.GetIPAddress(); 
                PostResult = Common_SPU.fnCreateMail_ForgotPassword(Modal);
            }
            return Json<PostResponse>(PostResult);
        }

        [HttpGet]
        [Route("GetDeleted")]
        public JsonResult<UserApi> Deleted(string sUserID, string Password)
        {
            UserApi user = new UserApi();
            if (!string.IsNullOrEmpty(sUserID) && !string.IsNullOrEmpty(Password))
            {
                Password = clsApplicationSetting.Encrypt(Password);
                clsDataBaseHelper.ExecuteNonQuery("update USERRegistration set isdeleted=1,deletedat=GETDATE(),deletedby=1 where email='" + sUserID + "' and password='" + Password + " 'and isdeleted=0");
                //  clsApplicationSetting.ClearSessionValues();
                user.Url = "";
                user.UserID = 0;
                user.EmpName = "";
                user.EMPCode = "";
                user.Desgination = "";
                user.Status = "Success";
            }
            else
            {
                user.Url = "";
                user.UserID = 0;
                user.EmpName = "";
                user.EMPCode = "";
                user.Desgination = "";
                user.Status = "Fail";
            }

            return Json<UserApi>(user);
        }

        [HttpGet]
        [Route("SetProfile")]
        public JsonResult<Employee.RegistrationDetailsIOS> SetProfile(string EmployeeName, string Mobile,string EMail,string Address,string ZipCode,string DOB,string CompanyName)
        {
            Employee.RegistrationDetailsIOS user = new Employee.RegistrationDetailsIOS();
            long SaveID = 0;
            if (!string.IsNullOrEmpty(EmployeeName) && !string.IsNullOrEmpty(Mobile))
            {   

                SaveID = Common_SPU.fnSetGlobalUser(0,EmployeeName, Mobile, EMail, Address, ZipCode, DOB, CompanyName);
                user.Id = SaveID;
                user.Status = "saved successfully";
            }
            else
            {
               
                user.Status = "Fail";
            }

            return Json<Employee.RegistrationDetailsIOS>(user);
        }


        [HttpGet]
        [Route("GetProfile")]
        public JsonResult<Employee.RegistrationDetailsIOS> GetProfile(string User_Id)
        {
            Employee.RegistrationDetailsIOS Modal = new Employee.RegistrationDetailsIOS();
         
            long UserId = Convert.ToInt32(User_Id);
            if (UserId>0)
            {
            

                DataSet ds = Common_SPU.fnGetMobileLoginIOS(UserId);
                try
                {
                    if (Convert.ToInt64(ds.Tables[0].Rows[0]["Id"]) > 0)
                    {
                        Modal.EmployeeName = ds.Tables[0].Rows[0]["FullName"].ToString()?? "";
                        Modal.Mobile = ds.Tables[0].Rows[0]["Mobile_no"].ToString()??"";
                        Modal.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString()??"";
                        Modal.ZipCode = ds.Tables[0].Rows[0]["ZipCode"].ToString()??"";
                        Modal.Address = ds.Tables[0].Rows[0]["Address"].ToString()??"";
                        Modal.EMail = ds.Tables[0].Rows[0]["email"].ToString()??"";
                        Modal.Id = Convert.ToInt64(ds.Tables[0].Rows[0]["ID"]);
                        Modal.DOB = ds.Tables[0].Rows[0]["DOB"].ToString()??"";
                    }
                }
                catch { }
             
                Modal.Status = "Success";
            }
            else
            {
                Modal.Status = "Fail";
            }

            return Json<Employee.RegistrationDetailsIOS>(Modal);
        }
    }
}
