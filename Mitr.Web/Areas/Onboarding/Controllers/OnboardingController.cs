using Mitr.Model.Procurement;
using Mitr.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mitr.CommonLib;
using Mitr.Interface;
using System.IO;
using System.Web.Http;
using PdfSharp.Pdf;
using PdfSharp;
using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using Mitr.ModelsMaster;
using System.Data;
using System.Web.UI.WebControls;

namespace Mitr.Areas.Onboarding.Controllers
{
    //Onboarding 
    public class OnboardingController : BaseController
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IEmployeeHelper employee;
        public OnboardingController()
        {
            getResponse = new GetResponse();
            employee = new EmployeeModal();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
        }

        // GET: Onboarding/Onboarding
       
        public ActionResult HRScreenOnboard()
        {
            return View();
        }
        public ActionResult UserScreenOnboard()
        {
            return RedirectToAction("Logout", "Account", new { area = "" });
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Offer(int? id, int? sourceCode)
        {
            ViewBag.RequestId = id;
            ViewBag.StatusCode = sourceCode;
            return View();
        }
        public ActionResult Joiningkit(int? id)
        {
            ViewBag.RequestId = id;
            return View();
        }
        public ActionResult PreRegistration(int? id)
        {
            ViewBag.RequestId = id;
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            //ViewBag.IsApproved = isApproved != null ? isApproved.ToString() : "0";
            return View();
        }
        public ActionResult OrientationSchedule(int? id)
        {
            ViewBag.RequestId = id;
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            //ViewBag.IsApproved = isApproved != null ? isApproved.ToString() : "0";
            return View();
        }
        public ActionResult Registration(int? id)
        {
            ViewBag.RequestId = id;
            ViewBag.IPAddress = ClsCommon.GetIPAddress();
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            //ViewBag.IsApproved = isApproved != null ? isApproved.ToString() : "0";
            return View();
        }
        public ActionResult Attachment(int? id)
        {
            ViewBag.RequestId = id;
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            //ViewBag.IsApproved = isApproved != null ? isApproved.ToString() : "0";
            return View();
        }
        public ActionResult UserOnboarding(int? id)
        {
            ViewBag.RequestId = id;
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            ViewBag.MenuID = "1";
            //ViewBag.IsApproved = isApproved != null ? isApproved.ToString() : "0";
            return View();
        }
        public ActionResult _ViewApplications(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_AppID = GetQueryString[2];
            long REC_AppID;
            long.TryParse(ViewBag.REC_AppID, out REC_AppID);
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_ApplicationView(REC_AppID);
            return PartialView(ds);
        }
        public ActionResult DeclarationDetails(int? id)
        {
            ViewBag.RequestId = id;
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            //ViewBag.IsApproved = isApproved != null ? isApproved.ToString() : "0";
            return View();
        }
        public ActionResult Orientation(int? id)
        {
            ViewBag.RequestId = id;
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            //ViewBag.IsApproved = isApproved != null ? isApproved.ToString() : "0";
            return View();
        }

        public ActionResult _PendingDeclaration(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[1];// clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.LoginId = GetQueryString[2];
            int EMPID = 0, LoginId=0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginId, out LoginId);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";
            getResponse.LoginID = LoginId;
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
            ViewBag.EMPID = GetQueryString[1];// clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.LoginId = GetQueryString[2];
            int EMPID = 0, LoginId = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginId, out LoginId);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = EMPID;
            getResponse.Doctype = "Declaration";
            getResponse.LoginID = LoginId;
            result.declarationslist = employee.GetOnboardingDeclartionApprove(getResponse);
            return PartialView(result);
        }

        [System.Web.Http.HttpGet]
        public ActionResult Declaration(string src)
        {

            //ViewBag.TabIndex = 6;
            //ViewBag.src = src;
            //string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            //ViewBag.GetQueryString = GetQueryString;
            //ViewBag.MenuID = GetQueryString[0];
            //ViewBag.EMPID = GetQueryString[2];
            //int EMPID = 0;
            //int.TryParse(ViewBag.EMPID, out EMPID);
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID = 1;
            getResponse.Doctype = "Declaration";
            result.declarationslist = employee.GetEmployeeDeclartionList(getResponse);
            return View(result);
        }
        [System.Web.Http.HttpPost]
        public ActionResult Declaration1(string src, Employee.Declaration modal)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[1];// clsApplicationSetting.GetSessionValue("EMPID");
            ViewBag.LoginId = GetQueryString[2];
            int EMPID = 0, LoginId=0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.LoginId, out LoginId);
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
                        item.UserId = LoginId;
                        PostResult = employee.SetHRDeclarationEmployee(item);
                    }

                }
                if (PostResult.Status == true)
                {
                    Common_SPU.OnboardingUpdateEmpStatus(EMPID);
                    string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Onboarding/Declaration*" + PostResult.ID);
                    PostResult.SuccessMessage = "Action Update Data";
                    PostResult.AdditionalMessage = Url;
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Onboarding/Declaration*" + PostResult.ID);
                    PostResult.SuccessMessage = "Action Update Data";
                    PostResult.AdditionalMessage = Url;
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Onboarding/Declaration*" + PostResult.ID);
                PostResult.SuccessMessage = "there is some problem, Please try again...";
                PostResult.AdditionalMessage = Url;
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            return View();


        }
      
    }
}