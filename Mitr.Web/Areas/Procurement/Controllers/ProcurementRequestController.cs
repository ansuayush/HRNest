
using CsvHelper;
using Mitr.BLL;
using Mitr.CommonClass;
using Mitr.CommonLib;
using Mitr.Interface;
using Mitr.Model;
using Mitr.Model.Common;
using Mitr.Model.Procurement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Mitr.Areas.Procurement.Controllers
{
    public class ProcurementRequestController : BaseController
    {
        IProcurement _objIProcurement;
        IGeneric _objIGeneric;
        public ProcurementRequestController()
        {
            //Testing checkin
            _objIProcurement = new ProcurementBAL();
            _objIGeneric = new GenericBussinessBL();
        }
        [HttpGet]
        public JsonResult GetMaxReqNo()
        {

            CommonMethods objCommonMethods = new CommonMethods();
            AmendmentProcess objAmendmentProcess = new AmendmentProcess();
            objAmendmentProcess.Procure_Request_Id = 0;
            string stringTOXml = objCommonMethods.GetXMLFromObject(objAmendmentProcess);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = 3;
                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureMaxNo.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindProjectDetails(int Id, int IsBindLine, string lineITems, string SublineItem)
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = Id;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.IsBindLine = IsBindLine;
            objSubCategoryMaster.ProjectLineIds = lineITems;
            objSubCategoryMaster.SublineItem = SublineItem;

            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureProjectDetails.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindProcureApproveRequest()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.IsApprover = 1;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ManageProcureRegistration.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindEmpSalry()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.IsApprover = 1;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ManageProcureRegistration.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindProcureRequest()
        {

            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.IsApprover = 0;
            string isHOD = clsApplicationSetting.GetSessionValue("IsED");
            string IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            string IsModuleAdmin = clsApplicationSetting.GetSessionValue("IsModuleAdmin");
            if (isHOD == "Y" || IsPM == "1" || IsModuleAdmin == "Y")
            {
                objSubCategoryMaster.IsApprover = 1;
            }


            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ManageProcureRegistration.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveProcurementRegistration(ProcureRequestModel objProcureRequestModel)
        {
            objProcureRequestModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            objProcureRequestModel.IPAddress = ClsCommon.GetIPAddress();
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objProcureRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ManageProcureRegistration.ToString())), roleId, userid, "POST", out errorMessage);
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
        public JsonResult ApproveRejectProcurementRegistration(List<ProcureRequestModel> objProcureRequestModel)
        {
            ;
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objProcureRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ApproveProcureRegistration.ToString())), roleId, userid, "POST", out errorMessage);
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
        public JsonResult BindRFPRequest()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.InputData = 1;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindFinanceApprovals()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.InputData = 11;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindRFPModuleAdmin()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.InputData = 9;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindRFPProcess()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.InputData = 5;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult PaymentApprovalAcknowledgement()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.InputData = 6;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindQuatationEntry()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.InputData = 4;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();



            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindProcurementActionRequest()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.InputData = 7;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();



            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveRFPEntry(RFPEntryModel objProcureRequestModel)
        {
            objProcureRequestModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            objProcureRequestModel.IPAddress = ClsCommon.GetIPAddress();
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objProcureRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "POST", out errorMessage);
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
        public JsonResult SaveQuotationEntry(QuotationEntryModel objProcureRequestModel)
        {

            objProcureRequestModel.IPAddress = ClsCommon.GetIPAddress();
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objProcureRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.QuotationEntry.ToString())), roleId, userid, "POST", out errorMessage);
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
        public JsonResult GetQuotationEntry(int Id)
        {
            QuotationEntryModel objSubCategoryMaster = new QuotationEntryModel();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.Procure_Request_Id = Id;

            CommonMethods objCommonMethods = new CommonMethods();



            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.QuotationEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveQuotationEntryConsultant(QuotationEntryConsultantModel objProcureRequestModel)
        {

            objProcureRequestModel.IPAddress = ClsCommon.GetIPAddress();
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objProcureRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.QuotationConsultant.ToString())), roleId, userid, "POST", out errorMessage);
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
        public JsonResult GetQuotationEntryConsultant(int Id)
        {
            QuotationEntryConsultantModel objSubCategoryMaster = new QuotationEntryConsultantModel();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.Procure_Request_Id = Id;

            CommonMethods objCommonMethods = new CommonMethods();



            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.QuotationConsultant.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveQuotationEntrySubgrant(QuotationEntrySubgrantModel objProcureRequestModel)
        {

            objProcureRequestModel.IPAddress = ClsCommon.GetIPAddress();
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objProcureRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.QuotationSubgrant.ToString())), roleId, userid, "POST", out errorMessage);
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
        public JsonResult GetQuotationEntrySubgrant(int Id)
        {
            QuotationEntrySubgrantModel objSubCategoryMaster = new QuotationEntrySubgrantModel();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.Procure_Request_Id = Id;

            CommonMethods objCommonMethods = new CommonMethods();



            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.QuotationSubgrant.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindProcurementCommitteeData()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.InputData = 8;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();



            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindAuthorisedSignatory()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.InputData = 10;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindFinanceApproval()
        {
            //var records = new List<Foo>
            //    {
            //                new Foo { Id = 1, Name = "one" },
            //                new Foo { Id = 2, Name = "two" },
            //    };
            //using (var writer = new StreamWriter("path\\to\\file.csv"))
            //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            //{
            //    csv.WriteRecords(records);
            //}

            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.InputData = 11;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.RFPEntry.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SetPOContractNumber(QuotationEntryModel objSubCategoryMaster)
        {


            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.QuotationPOContractNumber.ToString())), roleId, userid, "GET", out errorMessage);

                if (objSubCategoryMaster.ContractType == "Consultant")
                {
                    if (data.data.Tables.Count > 0)
                    {
                        int isAmend = Convert.ToInt32(data.data.Tables[0].Rows[0]["IsAmend"].ToString());
                        if (isAmend == 1)
                        {
                            var filePath = HttpContext.Server.MapPath("~/Attachments/HtmlTemplate/amendment_Consultant.html");

                            System.IO.StreamReader objReader;
                            objReader = new System.IO.StreamReader(filePath);
                            string content = objReader.ReadToEnd();
                            objReader.Close();
                            content = Regex.Replace(content, "~BaseMonth~", data.data.Tables[0].Rows[0]["BaseMonth"].ToString());
                            content = Regex.Replace(content, "~BaseDate~", data.data.Tables[0].Rows[0]["BaseDay"].ToString());
                            content = Regex.Replace(content, "~BaseYear~", data.data.Tables[0].Rows[0]["BaseYear"].ToString());
                            content = Regex.Replace(content, "~CurrentDownloadDate~", data.data.Tables[0].Rows[0]["RevisedEndDate"].ToString());
                            content = Regex.Replace(content, "~spVendorAddress~", data.data.Tables[0].Rows[0]["VendorAddress"].ToString());                            

                            content = Regex.Replace(content, "~sup~", data.data.Tables[0].Rows[0]["Sup"].ToString());
                            content = Regex.Replace(content, "~Month~", data.data.Tables[0].Rows[0]["Month"].ToString());
                            content = Regex.Replace(content, "~Date~", data.data.Tables[0].Rows[0]["Day"].ToString());
                            content = Regex.Replace(content, "~Year~", data.data.Tables[0].Rows[0]["Year"].ToString());
                            content = Regex.Replace(content, "~AmendmentNo~", data.data.Tables[0].Rows[0]["AmendmentNo"].ToString());
                            content = Regex.Replace(content, "~ProjectCode~", data.data.Tables[0].Rows[0]["ProjectCode"].ToString());
                            content = Regex.Replace(content, "~ContractAmendmentDate~", data.data.Tables[0].Rows[0]["RevisedEndDate"].ToString());
                            content = Regex.Replace(content, "~ReqNo~", data.data.Tables[0].Rows[0]["Req_No"].ToString());
                            content = Regex.Replace(content, "~SrNumber~", data.data.Tables[0].Rows[0]["PONumber"].ToString());
                            content = Regex.Replace(content, "~CompanyAddress~", data.data.Tables[0].Rows[0]["CompanyAddress"].ToString());
                            
                            content = Regex.Replace(content, "~spSignedVendorNameBetween~", data.data.Tables[0].Rows[0]["VendorName"].ToString());
                            content = Regex.Replace(content, "~spSignedVendorName~", data.data.Tables[0].Rows[0]["VendorName"].ToString());                            
                            content = Regex.Replace(content, "~spSignedVendorPOC~", data.data.Tables[0].Rows[0]["VPOCName"].ToString());
                            content = Regex.Replace(content, "~spSignedDisignation~", data.data.Tables[0].Rows[0]["VDesignation"].ToString());
                            content = Regex.Replace(content, "~FromDate~", data.data.Tables[0].Rows[0]["EstimatedStartDate"].ToString());
                            content = Regex.Replace(content, "~EndDate~", data.data.Tables[0].Rows[0]["EstimatedEndDate"].ToString());
                            content = Regex.Replace(content, "~spRecommedAmountNumber~", data.data.Tables[0].Rows[0]["RecommendAmount"].ToString());
                            content = Regex.Replace(content, "~spRecommedAmountWord~", data.data.Tables[0].Rows[0]["RecommendAmountWord"].ToString());

                         
                                


                            data.HtmlView = content;
                        }
                    }

                }

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
        public JsonResult GetInvoiceSubmitForRating(int Id, int ContractType, int IsSubmit)
        {
            ProcurementVendorRating objSubCategoryMaster = new ProcurementVendorRating();
            objSubCategoryMaster.Id = Id;
            objSubCategoryMaster.ContractType = ContractType;
            objSubCategoryMaster.IsSubmit = IsSubmit;

            CommonMethods objCommonMethods = new CommonMethods();



            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.CheckVendorRating.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveQuotationAmendmentEntry(QuotationEntryModel objProcureRequestModel)
        {

            objProcureRequestModel.IPAddress = ClsCommon.GetIPAddress();
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objProcureRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.QuotationEntryAmend.ToString())), roleId, userid, "POST", out errorMessage);
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
        public JsonResult SaveQuotationEntryAmendmentConsultant(QuotationEntryConsultantModel objProcureRequestModel)
        {

            objProcureRequestModel.IPAddress = ClsCommon.GetIPAddress();
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objProcureRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.QuotationEntryAmendConsultant.ToString())), roleId, userid, "POST", out errorMessage);
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
        public JsonResult SaveQuotationPOC(QuotationEntryModel objProcureRequestModel)
        {

            objProcureRequestModel.IPAddress = ClsCommon.GetIPAddress();
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objProcureRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.UpdatePOC.ToString())), roleId, userid, "POST", out errorMessage);
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
        public JsonResult GetProcurementReport(string modelData, string screenId)
        {
            GenericOperationModel objGenericOperationModel = new GenericOperationModel();
            int roleId = Convert.ToInt32(clsApplicationSetting.GetSessionValue("RoleID"));
            int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            objGenericOperationModel.UserID = userid;
            objGenericOperationModel.RoleId = roleId;
            string xmlString = string.Empty;
            if (modelData != "" || modelData != null)
            {
                // Parse JSON data into JObject
                JObject json = JObject.Parse(modelData);

                // Convert JSON to XML and get as an XML string
                XDocument xmlDocument = JsonConvert.DeserializeXNode(json.ToString(), "Root");
                // Convert the XML to a string
                xmlString = xmlDocument.ToString();
                objGenericOperationModel.ModelData = "";
            }


            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objGenericOperationModel);
            try
            {
                string errorMessage = string.Empty;
                var data = _objIGeneric.GetGenericRecords(stringTOXml, xmlString, screenId, roleId, userid, "Get", out errorMessage);
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

       public ActionResult ExportProcurementReport(string modelData, string screenId)
        {
            try
            {
                GenericOperationModel objGenericOperationModel = new GenericOperationModel();
                int roleId = Convert.ToInt32(clsApplicationSetting.GetSessionValue("RoleID"));
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                objGenericOperationModel.UserID = userid;
                objGenericOperationModel.RoleId = roleId;
                string xmlString = string.Empty;
                if (modelData != "" || modelData != null)
                {
                    // Parse JSON data into JObject
                    JObject json = JObject.Parse(modelData);

                    // Convert JSON to XML and get as an XML string
                    XDocument xmlDocument = JsonConvert.DeserializeXNode(json.ToString(), "Root");
                    // Convert the XML to a string
                    xmlString = xmlDocument.ToString();
                    objGenericOperationModel.ModelData = "";
                }


                CommonMethods objCommonMethods = new CommonMethods();

                string stringTOXml = objCommonMethods.GetXMLFromObject(objGenericOperationModel);
                try
                {
                    string errorMessage = string.Empty;
                    var data = _objIGeneric.GetGenericRecords(stringTOXml, xmlString, screenId, roleId, userid, "Get", out errorMessage);
                    DataSet ds = data.data;
                    WriteExcelEmployeesData(ds.Tables[0], "xls");

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
                // Define a cell style for center alignment
                ICellStyle headerStyle = workbook.CreateCellStyle();
                headerStyle.Alignment = HorizontalAlignment.Center;
                headerStyle.VerticalAlignment = VerticalAlignment.Center;
                // Create a new workbook                
                ISheet sheet = workbook.CreateSheet("Report");

                // Define header row 1
                IRow headerRow1 = sheet.CreateRow(0);

                headerRow1.CreateCell(0).SetCellValue("SNo.");
                headerRow1.CreateCell(1).SetCellValue("Agreement Number");
                headerRow1.CreateCell(2).SetCellValue("Vendor/Consultant Details");
                headerRow1.CreateCell(4).SetCellValue("Start Date");
                headerRow1.CreateCell(5).SetCellValue("Final End Date");
                headerRow1.CreateCell(6).SetCellValue("Project No");
                headerRow1.CreateCell(7).SetCellValue("POC");
                headerRow1.CreateCell(8).SetCellValue("Contract Amount (with GST)");
                headerRow1.CreateCell(10).SetCellValue("Paid Amount");
                headerRow1.CreateCell(12).SetCellValue("Balance Amount");
                headerRow1.CreateCell(14).SetCellValue("Status");
                headerRow1.CreateCell(15).SetCellValue("Rating");
                headerRow1.CreateCell(16).SetCellValue("Empanelled (Y/N)");               

                // Merge cells for header row 1
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 2, 3)); // Vendor/Consultant Details
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 8, 9)); // Contract Amount
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 10, 11)); // Paid Amount
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 12, 13)); // Balance Amount

                // Apply the header style to headerRow1 cells
                for (int i = 0; i <= 16; i++)
                {
                    if (headerRow1.GetCell(i) != null)
                    {
                        headerRow1.GetCell(i).CellStyle = headerStyle;
                    }
                }

                // Define header row 2
                IRow headerRow2 = sheet.CreateRow(1);

                headerRow2.CreateCell(2).SetCellValue("Name");
                headerRow2.CreateCell(3).SetCellValue("Vendor Number");

                headerRow2.CreateCell(8).SetCellValue("Fixed/PO");
                headerRow2.CreateCell(9).SetCellValue("Reimb");
                headerRow2.CreateCell(10).SetCellValue("Fixed/PO");
                headerRow2.CreateCell(11).SetCellValue("Reimb");
                headerRow2.CreateCell(12).SetCellValue("Fixed/PO");
                headerRow2.CreateCell(13).SetCellValue("Reimb");

                ICellStyle rightAlignStyle = workbook.CreateCellStyle();
                rightAlignStyle.Alignment = HorizontalAlignment.Right;

                ICellStyle leftAlignStyle = workbook.CreateCellStyle();
                leftAlignStyle.Alignment = HorizontalAlignment.Left;

                //loops through data
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int roNumber = i + 1;
                    IRow row = sheet.CreateRow(roNumber + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       
                        if (j == 8 || j == 9 || j == 10 || j == 11 || j == 12 || j == 13 || j == 15)
                        {
                            ICell cell1 = row.CreateCell(j);
                            cell1.CellStyle = rightAlignStyle;
                            string columnName = dt.Columns[j].ToString();
                            cell1.SetCellValue(dt.Rows[i][columnName].ToString());
                           
                        } 
                        else
                        {
                            ICell cell2 = row.CreateCell(j);
                            cell2.CellStyle = leftAlignStyle;
                            string columnName = dt.Columns[j].ToString();
                            cell2.SetCellValue(dt.Rows[i][columnName].ToString());                             
                        }
                       
                    }
                }
                // Apply the header style to headerRow2 cells
                for (int i = 0; i <= 16; i++)
                {
                    if (headerRow2.GetCell(i) != null)
                    {
                        if (i == 8 || i == 9 || i == 10 ||i== 11 || i == 12 || i == 13 || i == 15)
                        {
                            headerRow2.GetCell(i).CellStyle = rightAlignStyle;
                        }
                        else
                        {
                            headerRow2.GetCell(i).CellStyle = headerStyle;
                        }
                           
                    }
                }
                // Adjust column widths
                for (int i = 0; i <= 16; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                using (var exportData = new MemoryStream())
                {
                    //Response.ClearContent();
                    Response.Clear();
                    //Response.Flush();
                    //Response.Buffer = true;
                    workbook.Write(exportData, false);
                    string attach = string.Format("attachment;filename={0}", "ProcurementReport.xls");
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

    }
}