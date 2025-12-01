using Mitr.BLL;
using Mitr.CommonLib;
using Mitr.Interface;
using Mitr.Model;
using Mitr.Model.Procurement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Areas.Procurement.Controllers
{
    public class ProcureVendorRegisController : BaseController
    {
        // GET: Procurement/VenderType    

        IProcurement _objIProcurement;
        public ProcureVendorRegisController()
        {
            _objIProcurement = new ProcurementBAL();
        }
        [HttpGet]
        public JsonResult BindProcureVendorRegis()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureVendorRegistration.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetProcureVendorRegis(int id)
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = id;
            objSubCategoryMaster.IsGrid = 2;
            objSubCategoryMaster.IsAttachment = 0;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureVendorApproval.ToString())), roleId, userid, "GET", out errorMessage);
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

                return new JsonResult { Data = objCustomResponseModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }

        [HttpPost]
        public JsonResult SaveProcureVendorRegis(ProcurementVendorRegistration objSubCategoryMaster)
        {
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureVendorRegistration.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveTempProcureVendorRegis(string VendorName, int procureId, int vendorId, bool isdeleted)
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            objSubCategoryMaster.VendorId = vendorId;
            objSubCategoryMaster.VendorName = VendorName;
            objSubCategoryMaster.IsDeleted = isdeleted;
            objSubCategoryMaster.Procure_Id = procureId;
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ApproveProcureRegistration.ToString())), roleId, userid, "GET", out errorMessage);
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

                return new JsonResult { Data = objCustomResponseModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }

         
        [HttpPost]
        public JsonResult SaveBankDetails(VendorTypeModel objSubCategoryMaster)
        {
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureBankDetails.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetMaxReqNoForAmend(int procureId)
        {
            CommonMethods objCommonMethods = new CommonMethods();
            AmendmentProcess objAmendmentProcess = new AmendmentProcess();
            objAmendmentProcess.Procure_Request_Id = procureId;
            string stringTOXml = objCommonMethods.GetXMLFromObject(objAmendmentProcess);

             
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = 4;
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

                return new JsonResult { Data = objCustomResponseModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


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
                int userid = 1;
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

                return new JsonResult { Data = objCustomResponseModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }

        [HttpGet]
        public JsonResult GetProcureforApprovals()
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = 0;
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureApproval.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult ApproveReject(ApproveRejectModel objSubCategoryMaster)
        {
            
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureApproval.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetDownloadFileDetails(int id)
        {
            ProcurementVendorRegistration objSubCategoryMaster = new ProcurementVendorRegistration();
            objSubCategoryMaster.Id = id ;
            objSubCategoryMaster.IsGrid = 2;
            objSubCategoryMaster.IsAttachment = 1;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureVendorApproval.ToString())), roleId, userid, "GET", out errorMessage);
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

                return new JsonResult { Data = objCustomResponseModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }

        [HttpPost]
        public JsonResult SaveImpanelDetails(ProcurementVendorRegistrationImpanel objSubCategoryMaster)
        {
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureVendorApproval.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult VendorSearch(VendorSearchModel objVendorSearchModel)
        {           

            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objVendorSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.VendorSearch.ToString())), roleId, userid, "GET", out errorMessage);
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

                return new JsonResult { Data = objCustomResponseModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
    }
}