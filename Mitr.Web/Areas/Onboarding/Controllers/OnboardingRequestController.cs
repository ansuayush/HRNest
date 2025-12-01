using Mitr.BLL;
using Mitr.CommonClass;
using Mitr.CommonLib;
using Mitr.Interface;
using Mitr.Model;
using Mitr.Model.Onboarding;
using Mitr.Model.Procurement;
using Mitr.Models;
using Newtonsoft.Json;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMan = Mitr.Models.UserMan;
using Mitr.ModelsMasterHelper;
using Mitr.ModelsMaster;
using static Mitr.Models.Employee;
using System.Web.UI.WebControls;

namespace Mitr.Areas.Onboarding.Controllers
{
    public class OnboardingRequestController : Controller
    {
        IOnboarding _iOnboarding;
        // GET: Onboarding/OnboardingRequest
        public OnboardingRequestController()
        {
            _iOnboarding = new OnboardingBAL();
        }
        [HttpGet]
        public JsonResult BindonboardingProcessData(int CandidateId, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = CandidateId;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpGet]
        public JsonResult BindonboardingCandidateStatus(int CandidateId, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = CandidateId;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpGet]
        public JsonResult BindHRScreenOnboard()
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 1;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _iOnboarding.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveConsultant(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 1;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveOfferData(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 2;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveOfferLetterBodyData(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 14;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveOfferLetterData(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 13;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult UpdateOfferDownloadDate(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 8;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveJoiningKitData(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 4;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SavePreRegistrationData(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 5;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveAcceptRejectOffer(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 3;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveRejectOfferFromRespones(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 15;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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

        [HttpPost]
        public JsonResult SaveOnboardUser(string src, UserMan.Add users, string Command)
        {
            PostResponse PostResult = new PostResponse();
            string IPAddress = ClsCommon.GetIPAddress();
            int LoginID = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            int ID = 0;
            UserMan.Add modal = new UserMan.Add();
            Employee.RegistrationDetails empdetails = new Employee.RegistrationDetails();
            IEmployeeHelper employee = new EmployeeModal();
            if (ModelState.IsValid)
            {
                users.ID = ID;
                users.LoginID = LoginID;
                users.IPAddress = IPAddress;

                modal.ID = users.USER_ID;
                modal.LoginID = LoginID;
                modal.IPAddress = IPAddress;
                modal.UserType = users.UserType;
                modal.user_name = users.user_name;
                modal.Name = users.Name;
                modal.email = users.email;
                modal.RoleIDs = users.RoleIDs.ToString();
                modal.Priority = 0;
                modal.password = users.password;
                modal.FirstLogin = false;
                PostResult = Common_SPU.SetLoginUsersMapping(modal);
                if (PostResult.Status)
                {
                    modal.USER_ID = PostResult.ID;
                }
                else
                {
                    PostResult.StatusCode = -1;
                    PostResult.SuccessMessage = PostResult.SuccessMessage;
                }
            }
            //if (PostResult.StatusCode != -1)
            //{
            //    if (empdetails.EmploymentTerm.ToString() != "TermBase")
            //    {
            //        empdetails.Contract_EndDate = "";
            //    }
            //    PostResult = employee.SetEMP_RegistrationDetails(empdetails);
            //if (Modal.SalaryAccount != null)
            //{
            //    Modal.SalaryAccount.Doctype = "Salary";
            //    Modal.SalaryAccount.EMPID = PostResult.ID;
            //    employee.SetEMP_Account(Modal.SalaryAccount);
            //}
            //if (Modal.ReimbursementAccount != null)
            //{
            //    Modal.ReimbursementAccount.Doctype = "Reimbursement";
            //    Modal.ReimbursementAccount.EMPID = PostResult.ID;
            //    employee.SetEMP_Account(Modal.ReimbursementAccount);
            //}
            //}
            if (PostResult.Status)
            {
                //PostResult.RedirectURL = "/Tools/LoginUsersList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Tools/LoginUsersList");
            }
            string jsonData = JsonConvert.SerializeObject(PostResult);
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult SaveRegistrationDetails(string src, PreRegistrationDetails Modal, string Command)
        {
            ViewBag.TabIndex = 1;
            PostResponse PostResult = new PostResponse();
            IEmployeeMappingHelper employee = new EmployeeMappingModal();
            if (Modal != null)
            {

                if (Modal.AddUser != null)
                {
                    string IPAddress = ClsCommon.GetIPAddress();
                    int LoginID = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                    Modal.AddUser.LoginID = LoginID;
                    Modal.AddUser.IPAddress = IPAddress;
                    Modal.AddUser.Priority = 0;
                    Modal.AddUser.FirstLogin = false;
                    var User = Common_SPU.SetLoginUsers(Modal.AddUser);
                    if (User.Status)
                    {
                        Modal.RegistrationDet.USER_ID = User.ID;
                    }
                    else
                    {
                        PostResult.StatusCode = -1;
                        PostResult.SuccessMessage = User.SuccessMessage;
                        Modal.RegistrationDet.USER_ID = 0;
                    }
                }
                //if (PostResult.StatusCode != -1)
                //{
                    if (Modal.RegistrationDet.EmploymentTerm.ToString() != "TermBase")
                    {
                        Modal.RegistrationDet.Contract_EndDate = "";
                    }
                    PostResult = employee.SetEMP_RegistrationDetails(Modal.RegistrationDet);
                    if (Modal.RegistrationDet.SalaryAccount != null)
                    {
                        Modal.RegistrationDet.SalaryAccount.Doctype = "Salary";
                        Modal.RegistrationDet.SalaryAccount.EMPID = PostResult.ID;
                        employee.SetEMP_Onboarding_Account(Modal.RegistrationDet.SalaryAccount);
                    }
                    if (Modal.RegistrationDet.ReimbursementAccount != null)
                    {
                        Modal.RegistrationDet.ReimbursementAccount.Doctype = "Reimbursement";
                        Modal.RegistrationDet.ReimbursementAccount.EMPID = PostResult.ID;
                        employee.SetEMP_Onboarding_Account(Modal.RegistrationDet.ReimbursementAccount);
                    }
                //}

            }
            if (PostResult.Status)
            {
                //PostResult.RedirectURL = "/Employee/PersonalDetails?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Employee/PersonalDetails*" + PostResult.ID);
            }
            // return Json(PostResult, JsonRequestBehavior.AllowGet);
            //if (PostResult.SuccessMessage == "Update Successfully")
            //{
            //    PostResult.SuccessMessage = "PreRegistration has been issued to candidate.";
            //}
            string jsonData = JsonConvert.SerializeObject(PostResult);
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult SaveOnboardMitrUserData(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 10;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
                
                //if (data.ErrorMessage == "Updated Successfully")
                //{
                //    data.ErrorMessage = "PreRegistration has been issued to candidate.";
                //}
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
        [HttpPost]
        public JsonResult DeleteListRecordNotes(int Id, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = Id;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingDeleteListData.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveAttachmentDocuments(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 6;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveUserAttachmentDocuments(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 16;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpGet]
        public JsonResult BindonboardingAttachments(int CandidateId, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = CandidateId;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpGet]
        public JsonResult BindonboardingDeclaration(int CandidateId, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = CandidateId;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveOrientationSchedule(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 7;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveOrientationRescheduleDateTime(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 11;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveOrientationRescheduleFeedback(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 12;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult SaveRegistrationData(OnboardingProcess onboardingProcess)
        {
            onboardingProcess.Id = 0;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = 9;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult DeleteOrientationSchedule(int Id, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = Id;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingDeleteListData.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult DeleteAttachmentDocuments(int Id, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = Id;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingDeleteListData.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpPost]
        public JsonResult UpdateUserStatus(int Id, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = Id;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _iOnboarding.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingDeleteListData.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpGet]
        public JsonResult BindUserOnboard(int Id, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = Id;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _iOnboarding.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult DownloadFile(int Id)
        {
            IDigitalLibrary _objIDigitalLibrary = new DigitalLibraryBAL();
            SearchModel objSearchModel = new SearchModel();
            objSearchModel.Id = 0;
            objSearchModel.AttachmentId = Id; ;
            objSearchModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);

            string errorMessage = string.Empty;
            int roleId = 0;
            int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

            var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.AttachementSearch.ToString())), roleId, userid, "GET", out errorMessage);

            //  string filePath = data.data.Tables[0].Rows[0]["FileUrl"].ToString();
            // string fullName = Server.MapPath("~" + filePath);
            string jsonData = JsonConvert.SerializeObject(data);
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //byte[] fileBytes = GetFile(fullName);
            //return File(
            //    fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }
        public JsonResult GetUserDetails(string id)
        {
            GetResponse getResponse = new GetResponse();
            int LoginID = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            IToolsHelper Tools = new ToolsModal();
            UserMan.Add Modal = new UserMan.Add();
            if (!string.IsNullOrEmpty(id))
            {
                getResponse.ID = Convert.ToInt64(id);
                getResponse.LoginID = LoginID;
                Modal = Tools.GetLoginUser(getResponse);
            }
            string jsonData = JsonConvert.SerializeObject(Modal);
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult UpdateOnboardEmployeeStatus(string id)
        {
            CommandResult PostResult = new CommandResult();
            GetResponse getResponse = new GetResponse();
            int LoginID = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            IToolsHelper Tools = new ToolsModal();
            UserMan.Add Modal = new UserMan.Add();
            if (!string.IsNullOrEmpty(id))
            {
                PostResult= Common_SPU.OnboardingUpdateEmpStatus(Convert.ToInt32(id));
            }
            string jsonData = JsonConvert.SerializeObject(PostResult);
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult OrientationOnboard(int CandidateId, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = CandidateId;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _iOnboarding.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
        [HttpGet]
        public JsonResult GetOfferLetterByID(int CandidateId, int inputData)
        {
            OnboardingProcess onboardingProcess = new OnboardingProcess();
            onboardingProcess.Id = CandidateId;
            onboardingProcess.IsGrid = 1;
            onboardingProcess.InputData = inputData;
            onboardingProcess.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(onboardingProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _iOnboarding.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDOnboarding), Constants.ScreenIDOnboarding.OnboardingProcess.ToString())), roleId, userid, "GET", out errorMessage);
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
