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
    public class ProcureCommitteeController : BaseController
    {
        // GET: Procurement/VenderType    

        IProcurement _objIProcurement;
        public ProcureCommitteeController()
        {
            _objIProcurement = new ProcurementBAL();
        }
        [HttpGet]
        public JsonResult BindProcureCommittee()
        {
            ProcureCommitteeModel objSubCategoryMaster = new ProcureCommitteeModel();
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

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureCommitteeMembers.ToString())), roleId, userid, "GET", out errorMessage);
                string jsonData = JsonConvert.SerializeObject(data);
                
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet,MaxJsonLength=int.MaxValue };

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
        public JsonResult GetProcureCommittee(int id)
        {
            ProcureCommitteeModel objSubCategoryMaster = new ProcureCommitteeModel();
            objSubCategoryMaster.Id = id;
            objSubCategoryMaster.IsGrid = 2;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIProcurement.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureCommitteeMembers.ToString())), roleId, userid, "GET", out errorMessage);
                string jsonData = JsonConvert.SerializeObject(data);

                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet , MaxJsonLength = int.MaxValue };

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
                int userid = 2;
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
        [HttpPost]
        public JsonResult SaveProcureCommittee(ProcureCommitteeModel objSubCategoryMaster)
        {
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
            List<ProcureCommitteeLocation> myObjects = JsonConvert.DeserializeObject<List<ProcureCommitteeLocation>>(objSubCategoryMaster.LocationData);
            objSubCategoryMaster.ProcureCommitteeLocationList = myObjects;
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIProcurement.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDProcurment), Constants.ScreenIDProcurment.ProcureCommitteeMembers.ToString())), roleId, userid, "GET", out errorMessage);
                string jsonData = JsonConvert.SerializeObject(data);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet , MaxJsonLength = int.MaxValue };

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