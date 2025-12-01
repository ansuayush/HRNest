using DocumentFormat.OpenXml.Office2010.Excel;
using Mitr.CommonClass;
using Mitr.Interface;
using Mitr.Model;
using Mitr.Model.Onboarding;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using Newtonsoft.Json;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Mitr.Areas.Onboarding.Controllers
{
    //[CheckLoginFilter]
    public class UserProcessController : BaseController
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IEmployeeHelper employee;

        public UserProcessController()
        {
            getResponse = new GetResponse();
            employee = new EmployeeModal();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            ViewBag.RequestId = clsApplicationSetting.GetSessionValue("CandidateId");


        }
        // GET: UserProcess
        public ActionResult CompleteProfileDetails()
        {
            //ViewBag.RequestId = id;
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.RequestId = clsApplicationSetting.GetSessionValue("CandidateId");
            ViewBag.ScreenLock = clsApplicationSetting.GetSessionValue("UserScreenLock");
            return View();
        }
        public ActionResult MeetingDetails()
        {
            //ViewBag.RequestId = id;
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.RequestId = clsApplicationSetting.GetSessionValue("CandidateId");
            //ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            return View();
        }
        public ActionResult DeclarationDetails(string src)
        {
            //ViewBag.RequestId = id;
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.RequestId = clsApplicationSetting.GetSessionValue("CandidateId");

            ViewBag.TabIndex = 6;
            //ViewBag.src = src;
            //string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            //ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = "0";// GetQueryString[0];
            ViewBag.EMPID = clsApplicationSetting.GetSessionValue("EMPID");
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";
            result.declarationslist = employee.GetEmployeeDeclartionList(getResponse);
            result.declarationslist = result.declarationslist.ToList();
            return PartialView(result);
        }
        public ActionResult _ApproveDeclaration(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = clsApplicationSetting.GetSessionValue("EMPID");
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";
            result.declarationslist = employee.GetUserDeclartionApprovedList(getResponse);
           
            result.declarationslist = result.declarationslist.ToList();
            return PartialView(result);

             
        }
        public ActionResult _PendingDeclaration(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = clsApplicationSetting.GetSessionValue("EMPID");
            int EMPID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";
            result.declarationslist = employee.GetEmployeeDeclartionList(getResponse);
            result.declarationslist = result.declarationslist.ToList();
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult DeclarationDetails(string src, Employee.Declaration modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string CandidateId = GetQueryString[2].ToString();
            ViewBag.EMPID = clsApplicationSetting.GetSessionValue("EMPID");
            int EMPID = 0;
            int docCount = 0;
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
                            docCount++;
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
                if (modal.declarationslist.Count == docCount)
                {
                    Employee.Declaration result = new Employee.Declaration();
                    getResponse.ID = EMPID;
                    result.declarationslist = employee.GetUserDeclartionList(getResponse);
                    if (result.declarationslist.Count == 0) {
                        int rtnVal = UpdateOnboardUserStatus(CandidateId);
                        if (rtnVal > 0)
                        {
                            return RedirectToAction("Dashboard", "Account", new { area = "" });
                        }
                    }                                       
                }
                //if (PostResult.Status == true)
                //{
                //    string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/UserProcess/DeclarationDetails*" + PostResult.ID);
                //    PostResult.SuccessMessage = "Action Update Data";
                //    PostResult.AdditionalMessage = Url;
                //    return Json(PostResult, JsonRequestBehavior.AllowGet);
                //}
            }
            else
            {
                Employee.Declaration result = new Employee.Declaration();
                getResponse.ID = EMPID;
                result.declarationslist = employee.GetUserDeclartionList(getResponse);
                if (result.declarationslist.Count == 0)
                {
                    int rtnVal = UpdateOnboardUserStatus(CandidateId);
                    if (rtnVal > 0)
                    {
                        return RedirectToAction("Dashboard", "Account", new { area = "" });
                    }
                }
            }

            return View();
        }
        
        public ActionResult Declaration(string src)
        {

            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = clsApplicationSetting.GetSessionValue("EMPID");
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
            }
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";

            result.declarationslist = employee.GetEmployeeDeclartionList(getResponse);
            return View(result);
        }

        public ActionResult Welcome()
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Id = clsApplicationSetting.GetSessionValue("Id");
            ViewBag.UserManID = clsApplicationSetting.GetSessionValue("UserManID");
            ViewBag.CandidateId = clsApplicationSetting.GetSessionValue("CandidateId");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.RequestId = clsApplicationSetting.GetSessionValue("CandidateId");
            string candidateId = clsApplicationSetting.GetSessionValue("CandidateId").ToString();
            if (!String.IsNullOrEmpty(candidateId))
            {
                int id = Convert.ToInt32(candidateId);
                DataSet result = Common_SPU.GetONB_UserProcessStatus(Convert.ToInt32(id));
                string VideoRevied = "", IsScreenLock = "";
                foreach (DataRow item in result.Tables[0].Rows)
                {
                    VideoRevied = item["VideoReviewed"].ToString();
                    if (!string.IsNullOrEmpty(item["IsScreenLock"].ToString()))
                    {
                        IsScreenLock = item["IsScreenLock"].ToString();
                    }
                    string ReviewComments = item["ReviewComments"].ToString();
                    if (VideoRevied.Equals("Lock"))
                    {
                        clsApplicationSetting.SetSessionValue("UserScreenLock", IsScreenLock.ToString());
                    }
                    else
                    {
                        clsApplicationSetting.SetSessionValue("UserScreenLock", IsScreenLock.ToString());
                    }
                }
                if (!string.IsNullOrEmpty(VideoRevied))
                {
                    if (VideoRevied != "Welcome")
                    {
                        return RedirectToAction(VideoRevied, "UserProcess");
                    }
                }
            }
            return View();
        }
        public ActionResult Introduction()
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Id = clsApplicationSetting.GetSessionValue("Id");
            ViewBag.UserManID = clsApplicationSetting.GetSessionValue("UserManID");
            ViewBag.CandidateId = clsApplicationSetting.GetSessionValue("CandidateId");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.RequestId = clsApplicationSetting.GetSessionValue("CandidateId");
            return View();
        }
        public ActionResult Joined()
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Id = clsApplicationSetting.GetSessionValue("Id");
            ViewBag.UserManID = clsApplicationSetting.GetSessionValue("UserManID");
            ViewBag.CandidateId = clsApplicationSetting.GetSessionValue("CandidateId");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.RequestId = clsApplicationSetting.GetSessionValue("CandidateId");
            return View();
        }
        public ActionResult Attachments()
        {
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.RequestId = clsApplicationSetting.GetSessionValue("CandidateId");
            ViewBag.ScreenLock = clsApplicationSetting.GetSessionValue("UserScreenLock");
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Logout", "Account", new { area = "" });
        }
        public int UpdateOnboardUserStatus(string candidateId)
        {
            int rtn = 0;
            try
            {
                if (!string.IsNullOrEmpty(candidateId))
                {
                    int id = Convert.ToInt32(candidateId);
                    rtn = Common_SPU.UpdateOnboardUserStatus(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rtn;
        }
        [HttpGet]
        public JsonResult UpdateUserStatusData(string Id, string UserManID)
        {
            try
            {
                var result = Common_SPU.UpdateUserStatus(Convert.ToInt32(Id), Convert.ToInt32(UserManID));
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
        public JsonResult UpdateVideoReview(string CandidateId, string VideoReviewed)
        {
            try
            {
                var result = Common_SPU.UpdateVideoReviewPreRegistration(CandidateId, VideoReviewed);
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
        public JsonResult UpdateReviewComments(string CandidateId, string VideoReviewed)
        {
            try
            {
                var result = Common_SPU.UpdateReviewCommentsPreRegistration(CandidateId, VideoReviewed);
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
    }

}