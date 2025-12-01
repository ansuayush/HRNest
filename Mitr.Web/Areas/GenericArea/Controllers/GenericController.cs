using Mitr.BLL;
using Mitr.Interface;
using Mitr.Model.Common;
using Mitr.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Mitr.Controllers;
using System.Data;

namespace Mitr.Areas.GenericArea.Controllers
{
    public class GenericController : BaseController
    {
        IGeneric _objIGeneric;
        public GenericController()
        {
            //Testing checkin
            _objIGeneric = new GenericBussinessBL();
        }

        /// <summary>
        /// This method is used to perform common methods insert update and delete and get also.
        /// </summary>
        /// <param name="objGenericOperationModel"></param>
        /// <returns></returns>
        [SessionExpireFilterAttribute]

        [HttpPost]
        public JsonResult PerformOperation(GenericOperationModel objGenericOperationModel)
        {
            int roleId = Convert.ToInt32(clsApplicationSetting.GetSessionValue("RoleID"));
            int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            objGenericOperationModel.UserID = userid;
            objGenericOperationModel.RoleId = roleId;
            string xmlString = string.Empty;
            if (objGenericOperationModel.ModelData != "" || objGenericOperationModel.ModelData != null)
            {
                // Parse JSON data into JObject
                JObject json = JObject.Parse(objGenericOperationModel.ModelData);

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

                var data = _objIGeneric.PerformGenericOperation(stringTOXml, xmlString, objGenericOperationModel.ScreenID, roleId, userid, "ADD", out errorMessage);
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


        /// <summary>
        /// This method is used to perform common methods insert update and delete and get also.
        /// </summary>
        /// <param name="objGenericOperationModel"></param>
        /// <returns></returns>
        [SessionExpireFilterAttribute]
        [HttpGet]
        public JsonResult GetRecords(string modelData, string screenId)
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



        [HttpPost]
        public JsonResult GetRecordsPaging(string modelData, string screenId, int draw, int start, int length, string searchValue)
        {
            GenericOperationModel objGenericOperationModel = new GenericOperationModel();
            int roleId = Convert.ToInt32(clsApplicationSetting.GetSessionValue("RoleID"));
            int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            objGenericOperationModel.UserID = userid;
            objGenericOperationModel.RoleId = roleId;
            objGenericOperationModel.PageNumber = start;
            objGenericOperationModel.PageSize= length;
            objGenericOperationModel.ScreenID = screenId;
            string xmlString = string.Empty;

            if (!string.IsNullOrEmpty(modelData))
            {
                JObject json = JObject.Parse(modelData);
                XDocument xmlDocument = JsonConvert.DeserializeXNode(json.ToString(), "Root");
                xmlString = xmlDocument.ToString();
                objGenericOperationModel.ModelData = "";
            }

            CommonMethods objCommonMethods = new CommonMethods();
            string stringTOXml = objCommonMethods.GetXMLFromObject(objGenericOperationModel);

            try
            {
                string errorMessage = string.Empty;
                var data = _objIGeneric.GetGenericRecords(stringTOXml, xmlString, screenId, roleId, userid, "Get", out errorMessage);

                // Convert to List if needed and filter
                var filteredData = data.data.Tables[0]
                .AsEnumerable()
                .Where(row => string.IsNullOrEmpty(searchValue) || row["VendorName"].ToString().Contains(searchValue))
                .ToList();
                int recordsTotal = data.data.Tables[0].Rows.Count;
                int recordsFiltered = filteredData.Count;


                // Paging
                var pagedData = filteredData.Skip(start).Take(length).ToList();
                var result = new
                {
                    draw = draw,
                    recordsTotal = recordsTotal,
                    recordsFiltered = recordsFiltered,
                    data = pagedData
                };

                var jsonSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Avoid circular reference
                };

                string jsonData = JsonConvert.SerializeObject(result, jsonSettings);

                return Json(jsonData, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                CommonMethods.Error(ex);
                var errorResult = new
                {
                    draw = draw,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<object>(),
                    error = ex.Message
                };
                return Json(errorResult, JsonRequestBehavior.AllowGet);
            }
        }

    }
}