using Mitr.BLL;
using Mitr.CommonLib;
using Mitr.Interface;
using Mitr.Model;
using Mitr.Model.Capacity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Areas.Capacity.Controllers
{
    public class CapacityRequestController : Controller
    {
        ICapacityTrainingType _objICapacityTrainingType;
        public CapacityRequestController()
        {
            //Testing checkin
            _objICapacityTrainingType = new CapacityTrainingTypeBAL();
        }

        [HttpGet]
        public JsonResult BindTrainingType()
        {
            TrainingTypeModel objTrainingTypeMaster = new TrainingTypeModel();
            objTrainingTypeMaster.id = 0;
            objTrainingTypeMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingTypeMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TraniningTypeMaster.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetTrainingType(int id)
        {
            TrainingTypeModel objTrainingTypeMaster = new TrainingTypeModel();
            objTrainingTypeMaster.id = id;
            objTrainingTypeMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingTypeMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TraniningTypeMaster.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetTrainingDesc(string id)
        {
            int trainingTypeId = 0;
            if (!string.IsNullOrEmpty(id)) {
                trainingTypeId=Convert.ToInt32(id);
            }
            TrainingTypeModel objTrainingTypeMaster = new TrainingTypeModel();
            objTrainingTypeMaster.id = trainingTypeId;
            objTrainingTypeMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingTypeMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TraniningTypeMaster.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult UpdateStatusTrainingType(int id, int inputData)
        {
            TrainingTypeModel trainingHRRequest = new TrainingTypeModel();
            trainingHRRequest.id = id;
            trainingHRRequest.IsGrid = 1;
            trainingHRRequest.InputData = inputData;
            trainingHRRequest.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(trainingHRRequest);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.MasterTainingTypeStatusUpdate.ToString())), roleId, userid, "POST", out errorMessage);
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
        public JsonResult UpdateStatusTrainingType1(TrainingTypeModel objTrainingTypeMaster)
        {
            objTrainingTypeMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            objTrainingTypeMaster.UserId = 1;
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingTypeMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TraniningTypeMaster.ToString())), roleId, userid, "Save", out errorMessage);
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
        public JsonResult SaveTrainingType(TrainingTypeModel objTrainingTypeMaster)
        {
            objTrainingTypeMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            objTrainingTypeMaster.UserId = 2;
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingTypeMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 2;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TraniningTypeMaster.ToString())), roleId, userid, "Save", out errorMessage);
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

        #region TrainingCalendar
        [HttpGet]
        public JsonResult BindTrainingCalendar()
        {
            TrainingCalenderModel objTrainingCalendarMaster = new TrainingCalenderModel();
            objTrainingCalendarMaster.id = 0;
            objTrainingCalendarMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingCalendarMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingCalenderMaster.ToString())), roleId, userid, "GET", out errorMessage);
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
        #endregion

        public JsonResult SaveTrainingCalendarDetails(TrainingCalenderModel objTrainingCalenderModel)
        {
            objTrainingCalenderModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");

            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingCalenderModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingCalenderMaster.ToString())), roleId, userid, "Save", out errorMessage);
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
        public JsonResult GetTrainingCalendarById(int id)
        {
            TrainingCalenderModel objTrainingCalenderModel = new TrainingCalenderModel();
            objTrainingCalenderModel.id = id;
            objTrainingCalenderModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingCalenderModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingCalenderMaster.ToString())), roleId, userid, "GET", out errorMessage);
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

            string stringTOXml = "";
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = 3;
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingReqMaxNo.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetMaxReqCode()
        {

            TrainingUserRequestModel trainingUserRequest = new TrainingUserRequestModel();
            int loginUserId = 0;
            if (!string.IsNullOrEmpty(clsApplicationSetting.GetSessionValue("EMPID")))
            {
                loginUserId = Convert.ToInt32(clsApplicationSetting.GetSessionValue("EMPID"));
            }
            trainingUserRequest.Id = loginUserId;
            trainingUserRequest.IsGrid = 1;
            trainingUserRequest.InputData = 1;
            trainingUserRequest.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(trainingUserRequest);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = 4;
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingRequestNumber.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetTrainingDescFromTrainingcalender(TrainingRequestModel objTrainingReqModel)
        {

            objTrainingReqModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");

            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingReqModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.GetTrainingDesc.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindTrainingRequest()
        {
            TrainingRequestModel objTrainingRequestModel = new TrainingRequestModel();
            objTrainingRequestModel.id = 0;
            objTrainingRequestModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = "";//_objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingRequest.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveTrainingRequest(TrainingRequestModel objTrainingRequestModel)
        {
            objTrainingRequestModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");

            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingRequest.ToString())), roleId, userid, "Save", out errorMessage);
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
        public JsonResult GetTrainingRequest(int id, int inputData)
        {
            TrainingHRRequestModel trainingHRRequest = new TrainingHRRequestModel();
            trainingHRRequest.Id = id;
            trainingHRRequest.IsGrid = 1;
            trainingHRRequest.InputData = inputData;
            trainingHRRequest.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(trainingHRRequest);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.GetAllHRTrainingRequest.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveUserRequest(TrainingUserRequestModel trainingUserRequest)
        {
            trainingUserRequest.Id = 0;
            trainingUserRequest.IsGrid = 1;
            trainingUserRequest.InputData = 1;
            trainingUserRequest.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(trainingUserRequest);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objICapacityTrainingType.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.UserTrainingRequest.ToString())), roleId, userid, "Save", out errorMessage);
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
        public JsonResult ViewTrainingRequest(int id)
        {
            TrainingRequestModel objTrainingRequestModel = new TrainingRequestModel();
            objTrainingRequestModel.id = id;
            objTrainingRequestModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objTrainingRequestModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingRequest.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetAllTrainingRequestPending()
        {
            TrainingUserRequestModel trainingUserRequest=new TrainingUserRequestModel ();
            int loginUserId = 0;
            if (!string.IsNullOrEmpty(clsApplicationSetting.GetSessionValue("EMPID")))
            {
                loginUserId= Convert.ToInt32(clsApplicationSetting.GetSessionValue("EMPID"));
            }
            trainingUserRequest.Id = loginUserId;
            trainingUserRequest.IsGrid = 1;
            trainingUserRequest.InputData = 1;
            trainingUserRequest.UserId = 5;
            trainingUserRequest.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(trainingUserRequest);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = 5;
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingRequestNumber.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetAllTrainingRequestProcesses()
        {
            TrainingUserRequestModel trainingUserRequest = new TrainingUserRequestModel();
            int loginUserId = 0;
            if (!string.IsNullOrEmpty(clsApplicationSetting.GetSessionValue("EMPID")))
            {
                loginUserId = Convert.ToInt32(clsApplicationSetting.GetSessionValue("EMPID"));
            }
            trainingUserRequest.Id = loginUserId;
            trainingUserRequest.IsGrid = 1;
            trainingUserRequest.InputData = 1;
            trainingUserRequest.UserId = 6;
            trainingUserRequest.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
            string stringTOXml = objCommonMethods.GetXMLFromObject(trainingUserRequest);

            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = 6;
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingRequestNumber.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetAllTrainingRequestCompleted()
        {
            TrainingUserRequestModel trainingUserRequest = new TrainingUserRequestModel();
            int loginUserId = 0;
            if (!string.IsNullOrEmpty(clsApplicationSetting.GetSessionValue("EMPID")))
            {
                loginUserId = Convert.ToInt32(clsApplicationSetting.GetSessionValue("EMPID"));
            }
            trainingUserRequest.Id = loginUserId;
            trainingUserRequest.IsGrid = 1;
            trainingUserRequest.InputData = 1;
            trainingUserRequest.UserId = 7;
            trainingUserRequest.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
            string stringTOXml = objCommonMethods.GetXMLFromObject(trainingUserRequest);

            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = 7;
                var data = _objICapacityTrainingType.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenIDCapacity), Constants.ScreenIDCapacity.TrainingRequestNumber.ToString())), roleId, userid, "GET", out errorMessage);
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