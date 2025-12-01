using Mitr.CommonLib;
using Mitr.BLL; 
using Mitr.Interface;
using Mitr.Model;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using CsvHelper;
using Mitr.Model.Procurement;
using System.Globalization;
using System.Collections;

namespace Mitr.Areas.DigitalLibrary.Controllers
{
    [RouteArea("")]
    public class DigitalLibraryController : BaseController
    {
        IDigitalLibrary _objIDigitalLibrary;
        public DigitalLibraryController()
        {
            _objIDigitalLibrary = new DigitalLibraryBAL();
        }

        public FileResult DLManual()
        {
            string fileName= ConfigurationManager.AppSettings["UserManualFileName"];
            string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);
            baseurl = baseurl + "/DLUserManual/" + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(baseurl);            
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult MySharedContent()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();
        }
        public ActionResult  SharedContentReport()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();
        }
        public ActionResult ContentBulkUpload()
        {




            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            
            return View();
        }
        private void Upload(string filePathTobeSaved)
        {
            HttpContext.Request.Files[0].SaveAs(filePathTobeSaved);
        }
        public JsonResult UploadBulkDocument()
        {
            try
            {

                string returrMessage = string.Empty;
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                string[] testfiles;
                string fname = "";
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }
                string fileName = fname;
                string newFile = Guid.NewGuid().ToString();
                string[] splString = fileName.Split('.');
                int extIndex=1;
                if(splString.Length>0)
                {
                    extIndex = splString.Length - 1;
                }
                string uploadNewFileName = newFile + "." + splString[extIndex];
                string filePathTobeSaved = "";

                if (splString[1] == "xls" || splString[1] == "xlsx")
                {
                    string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);
                    filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                    Upload(filePathTobeSaved);

                    //string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);
                    //baseurl = baseurl + "/Bulk.xlsx";
                    DataSet ds = clsDataBaseHelper.GetExcelDataAsDataSet(filePathTobeSaved);
                    DataTable dt = ds.Tables[0];
                    var isValid = true;
                    foreach (var item in dt.Columns)
                    {
                        string strContain= "SN,Category,SubCategory,Title,Subtitle,ProjectCode,ProjectName,FundedBy,Author,Date,PlaceofOrigin,ThematicArea,Tag,AbstractSummary,DocumentType,LocalPath,Published,Source,ProposalCode,Accepted,ProjectLead,Copyright,ReportNo,Documentsuploadedby,AttachmentType";
                        
                        if(!strContain.Contains(item.ToString()))
                        {
                            isValid = false;                           
                        } 

                    }
                    string jsonData = string.Empty;
                    if (isValid)
                    {
                        string dsxml = ds.GetXml();

                        CommonMethods objCommonMethods = new CommonMethods();
                         
                        string stringTOXml = dsxml;

                        string errorMessage = string.Empty;
                        int roleId = 1;
                        int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                        var data = _objIDigitalLibrary.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.BulkUpload.ToString())), roleId, userid, "Save", out errorMessage);
                        System.IO.File.Delete(filePathTobeSaved);
                        jsonData = JsonConvert.SerializeObject(data);
                    }
                    else
                    {
                        
                        CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                        objCustomResponseModel.ValidationInput = 0;
                        objCustomResponseModel.ErrorMessage = "Please upload right excel with these columns(SN,Category,SubCategory,Title,Subtitle,ProjectCode,ProjectName,FundedBy,Author,Date,PlaceofOrigin,ThematicArea,Tag,AbstractSummary,DocumentType,LocalPath,Published,Source,ProposalCode,Accepted,ProjectLead,Copyright,ReportNo,Documentsuploadedby,AttachmentType)";
                        objCustomResponseModel.data = null;
                        objCustomResponseModel.CustomMessage = "Please upload right excel with these columns(SN,Category,SubCategory,Title,Subtitle,ProjectCode,ProjectName,FundedBy,Author,Date,PlaceofOrigin,ThematicArea,Tag,AbstractSummary,DocumentType,LocalPath,Published,Source,ProposalCode,Accepted,ProjectLead,Copyright,ReportNo,Documentsuploadedby,AttachmentType)";
                        objCustomResponseModel.IsSuccessStatusCode = true;
                        objCustomResponseModel.CustumException = "Please upload right excel with these columns(SN,Category,SubCategory,Title,Subtitle,ProjectCode,ProjectName,FundedBy,Author,Date,PlaceofOrigin,ThematicArea,Tag,AbstractSummary,DocumentType,LocalPath,Published,Source,ProposalCode,Accepted,ProjectLead,Copyright,ReportNo,Documentsuploadedby,AttachmentType)";
                        objCustomResponseModel.CommomDropDownData = null;

                          jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                    }
                    return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                    objCustomResponseModel.ValidationInput = 0;
                    objCustomResponseModel.ErrorMessage = "Invalid file type. Please upload excel file";
                    objCustomResponseModel.data = null;
                    objCustomResponseModel.CustomMessage = "Invalid file type. Please upload excel file";
                    objCustomResponseModel.IsSuccessStatusCode = true;
                    objCustomResponseModel.CustumException = "Invalid file type. Please upload excel file";
                    objCustomResponseModel.CommomDropDownData = null;

                    string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                    return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
               


            }
            catch (Exception ex)
            {
                CommonMethods.Error(ex);
                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = ex.Message + " " + ex.StackTrace;
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = "";
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = ex.Message + " " + ex.StackTrace;
                objCustomResponseModel.CommomDropDownData = null;
                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
        public ActionResult DigitalLib()
        {

            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsHOD = clsApplicationSetting.GetSessionValue("IsHOD");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        public ActionResult SearchBox()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        public ActionResult Masters()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            return View();
        }

        public ActionResult MyContent(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.HODID = clsApplicationSetting.GetSessionValue("HODID");
            ViewBag.EDIDToUpload = clsApplicationSetting.GetSessionValue("EDIDToUpload");
            ViewBag.ManagerID = clsApplicationSetting.GetSessionValue("ManagerID");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            if (id==null)
            {
                id = 1;
            }
            ViewBag.TabId = id;
            return View();
        }

        
       public ActionResult MyTeamPendingDocument(int id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.TabId = id;
            return View();
        }
        public ActionResult TeamContentRequest(int ? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            if(id==null)
            {
                id = 1;
            }
            ViewBag.TabId = id;
            return View();
        }
        public ActionResult RejectedDocument()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }


        #region ProjectTeamLead Master
        public ActionResult ProjectTeamLead()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        [HttpPost]
        public JsonResult SaveProjectTeamLead(TeamLeadMaster objTeamLeadMaster)
        {

            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objTeamLeadMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.TeamLeadMaster.ToString())), roleId, userid, "Save", out errorMessage);
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
        public JsonResult GetProjectTeamLeadById(int id)
        {
            TeamLeadMaster objTeamLeadMaster = new TeamLeadMaster();
            objTeamLeadMaster.Id = id;
            objTeamLeadMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objTeamLeadMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.TeamLeadMaster.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindProjectTeamLead()
        {
            TeamLeadMaster objTeamLeadMaster = new TeamLeadMaster();
            objTeamLeadMaster.Id = 0;
            objTeamLeadMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objTeamLeadMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
                ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.TeamLeadMaster.ToString())), roleId, userid, "GET", out errorMessage);
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

        #region CategoryEdOdMapping
        public ActionResult CategoryEdOdMapping()
        {

            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        [HttpPost]
        public JsonResult SaveEdOdMapping(CategoryEDODLinkupModel objCategoryEDODLinkupModel)
        {
            objCategoryEDODLinkupModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objCategoryEDODLinkupModel);

            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.CategoryEDODLinkup.ToString())), roleId, userid, "Save", out errorMessage);
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
        public JsonResult GetEdOdMappingById(int id)
        {
            CategoryEDODLinkupModel objCategoryEDODLinkupModel = new CategoryEDODLinkupModel();
            objCategoryEDODLinkupModel.Id = id;
            objCategoryEDODLinkupModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objCategoryEDODLinkupModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.CategoryEDODLinkup.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindEdOdMappingMaster()
        {
            CategoryEDODLinkupModel objCategoryEDODLinkupModel = new CategoryEDODLinkupModel();
            objCategoryEDODLinkupModel.Id = 0;
            objCategoryEDODLinkupModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objCategoryEDODLinkupModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.CategoryEDODLinkup.ToString())), roleId, userid, "GET", out errorMessage);
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

        #region Sub Category Details
        [HttpGet]
        public JsonResult BindCategory()
        {
            SubCategoryMaster objSubCategoryMaster = new SubCategoryMaster();
            objSubCategoryMaster.ID =Convert.ToInt32(Constants.SubCategory);
            objSubCategoryMaster.IsGrid = 1;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.SubCategory.ToString())), roleId, userid, "GET", out errorMessage);
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
        public ActionResult SubCategory()
        {

            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        [HttpGet]
        public JsonResult GetSubCategory(int id)
        {
            SubCategoryMaster objSubCategoryMaster = new SubCategoryMaster();
            objSubCategoryMaster.ID = id;
            objSubCategoryMaster.IsGrid = 2;
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.SubCategory.ToString())), roleId, userid, "GET", out errorMessage);
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
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.DLContentReqNo.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveSubCategory(SubCategoryMaster objSubCategoryMaster)
        {
            objSubCategoryMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSubCategoryMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.SubCategory.ToString())), roleId, userid, "GET", out errorMessage);
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

        #region Content Details

        public ActionResult ContentDetail()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.HODID = clsApplicationSetting.GetSessionValue("HODID");
            ViewBag.EDIDToUpload = clsApplicationSetting.GetSessionValue("EDIDToUpload");
            ViewBag.ManagerID = clsApplicationSetting.GetSessionValue("ManagerID");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();


        }
        [HttpGet]
        public JsonResult GetProjectDetails(int id)
        {
            ProjectDetailModel objProjectDetailModel = new ProjectDetailModel();
            objProjectDetailModel.Id = id;
            objProjectDetailModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objProjectDetailModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.ProjectDetails.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveDLContent(ContentModel objContentModel)
        {
            string concatTags = string.Empty;
            if (objContentModel.Tags != null)
            {
                for (int i = 0; i < objContentModel.Tags.Length; i++)
                {
                    concatTags = concatTags + "," + objContentModel.Tags[i].ToString();
                }
                concatTags = concatTags.Remove(0, 1);
                objContentModel.Tag_Id = concatTags;
            }

            if (objContentModel.PlaceIds != null)
            {
                string PlaceIDs = string.Empty;
                for (int i = 0; i < objContentModel.PlaceIds.Length; i++)
                {
                    PlaceIDs = PlaceIDs + "," + objContentModel.PlaceIds[i].ToString();
                }
                PlaceIDs = PlaceIDs.Remove(0, 1);
                objContentModel.PlaceId = PlaceIDs;
            }

            string AutherIDs = string.Empty;
            if (objContentModel.Author_Id != null)
            {
                for (int i = 0; i < objContentModel.Author_Id.Length; i++)
                {
                    AutherIDs = AutherIDs + "," + objContentModel.Author_Id[i].ToString();
                }
                AutherIDs = AutherIDs.Remove(0, 1);
                objContentModel.Author_CoordinatorId = AutherIDs;
            }
           

            objContentModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objContentModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.DLContent.ToString())), roleId, userid, "Save", out errorMessage);
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

        #region TagMaster

        public ActionResult Tag()
        {

            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        [HttpPost]
        public JsonResult SaveTag(TagMaster objTagMaster)
        {
            objTagMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objTagMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.TagMAster.ToString())), roleId, userid, "Save", out errorMessage);
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
        public JsonResult GetTagMasterById(int id)
        {
            TagMaster objTagMaster = new TagMaster();
            objTagMaster.Id = id;
            objTagMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objTagMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.TagMAster.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindTagMaster()
        {
            TagMaster objTagMaster = new TagMaster();
            objTagMaster.Id = 0;
            objTagMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objTagMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.TagMAster.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult BindTagMasterBasedonThemeticId(int tID)
        {
            TagMaster objTagMaster = new TagMaster();
            objTagMaster.Thematic_id = tID;
            objTagMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objTagMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.TagMAster.ToString())), roleId, userid, "GET", out errorMessage);
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

        #region Searchbox
        [HttpPost]
        public JsonResult GetSearchContent(SearchModel objSearchModel)
        {
            objSearchModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            objSearchModel.IsPM = string.IsNullOrWhiteSpace(clsApplicationSetting.GetSessionValue("IsPM"))==false ?Convert.ToInt32(clsApplicationSetting.GetSessionValue("IsPM")):0;
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.SearchContentDetails.ToString())), roleId, userid, "Get", out errorMessage);
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
        public JsonResult GetAtatchemntById(int id)
        {
            SearchModel objSearchModel = new SearchModel();
            objSearchModel.Id = id;
            objSearchModel.AttachmentId = 0;
            objSearchModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.AttachementSearch.ToString())), roleId, userid, "GET", out errorMessage);
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
        #endregion


        #region MyContent

        [HttpPost]
        public JsonResult DLContentApproval(List<ContentApproval> objContentModel)
        {
            // objContentModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objContentModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.Approval.ToString())), roleId, userid, "Save", out errorMessage);
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

        public JsonResult GetMyContent()
        {
            SearchModel objSearchModel = new SearchModel();
            objSearchModel.Id = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            objSearchModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.MyContent.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetMyRequestContent()
        {
            SearchModel objSearchModel = new SearchModel();
            objSearchModel.Id = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            objSearchModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.MyRequestApproval.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetMyContentForApproval()
        {
            SearchModel objSearchModel = new SearchModel();
            objSearchModel.Id = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            objSearchModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.Approval.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetContentView(int id)
        {
            TagMaster objSearchModel = new TagMaster();
            objSearchModel.Id = id;
            objSearchModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.ContentUpdate.ToString())), roleId, userid, "GET", out errorMessage);
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

        #endregion

        #region MyTeamContent
        [HttpGet]
        public JsonResult GetMyTeamContent()
        {
            SearchModel objSearchModel = new SearchModel();
            objSearchModel.Id = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            objSearchModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.MyTeamContent.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult UpdateDLContent(List<ContentModel> objContentModel)
        {
            // objContentModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objContentModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.ContentUpdate.ToString())), roleId, userid, "Save", out errorMessage);
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

        #region Dashboard

        public ActionResult MyPendingDocument(int id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.TabId = id;
            return View();
        }
        #endregion

        #region Rejected Document

        [HttpGet]
        public JsonResult GetRejectedDocument()
        {
            SearchModel objSearchModel = new SearchModel();
            objSearchModel.Id = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            objSearchModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.RejectedDocument.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult ArchiveOrDeleteRejectedContent(List<ContentModel> objContentModel)
        {
            // objContentModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objContentModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.ContentUpdate.ToString())), roleId, userid, "Save", out errorMessage);
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
        public JsonResult DownloadFile(int Id)
        {

            SearchModel objSearchModel = new SearchModel();
            objSearchModel.Id = 0;
            objSearchModel.AttachmentId = Id;            ;
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

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        [HttpGet]
        public JsonResult GetMatrix(string category)
        {
            CategoryMaster objSearchModel = new CategoryMaster();           
            objSearchModel.CategoryCode = category;
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.Matrix.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult Dashboard()
        {
            CategoryMaster objSearchModel = new CategoryMaster();
            objSearchModel.CategoryCode = "";
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.Dashboard.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult SaveSharedContent(TagMaster objTagMaster)
        {
            objTagMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objTagMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.SharedContent.ToString())), roleId, userid, "Save", out errorMessage);
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
        public JsonResult GetSharedContent(SearchModel objSearchModel)
        {
            objSearchModel.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            objSearchModel.IsPM = string.IsNullOrWhiteSpace(clsApplicationSetting.GetSessionValue("IsPM")) == false ? Convert.ToInt32(clsApplicationSetting.GetSessionValue("IsPM")) : 0;
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objSearchModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.SharedContent.ToString())), roleId, userid, "Get", out errorMessage);
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
        public JsonResult GetSharedContentCount()
        {
            TagMaster objTagMaster = new TagMaster();
            objTagMaster.Id = 0;
            objTagMaster.UserGrade = clsApplicationSetting.GetSessionValue("Grade");
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objTagMaster);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.ContentCount.ToString())), roleId, userid, "Save", out errorMessage);
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
        public JsonResult  ContentReport(DateTime ? fromDate, DateTime ? todate, string  category, string documentType)
        {
            ShareContentReport objShareContentReport = new ShareContentReport();
            objShareContentReport.FromDate = fromDate;
            objShareContentReport.ToDate = todate;
            objShareContentReport.Category = category;
            objShareContentReport.DocumentType = documentType;
            CommonMethods objCommonMethods = new CommonMethods();
             
            string stringTOXml = objCommonMethods.GetXMLFromObject(objShareContentReport);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.SharedContentReport.ToString())), roleId, userid, "Get", out errorMessage);
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
        public ActionResult EmployeesExcelToExport(DateTime? fromDate, DateTime? todate, string category, string documentType)
        {
            try
            {
                ShareContentReport objShareContentReport = new ShareContentReport();
                objShareContentReport.FromDate = fromDate;
                objShareContentReport.ToDate = todate;
                objShareContentReport.Category = category;
                objShareContentReport.DocumentType = documentType;
                CommonMethods objCommonMethods = new CommonMethods();
                 
                string stringTOXml = objCommonMethods.GetXMLFromObject(objShareContentReport);
                try
                {
                    string errorMessage = string.Empty;
                    int roleId = 1;
                    int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                    var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.SharedContentReport.ToString())), roleId, userid, "Get", out errorMessage);
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

                ISheet sheet1 = workbook.CreateSheet("Sheet 1");
                //make a header row
                IRow row1 = sheet1.CreateRow(0);

                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Columns[j].ToString());

                    ICellStyle style = workbook.CreateCellStyle();

                    //Set border style 
                    //style.BorderBottom = BorderStyle.Thick;
                    //style.BottomBorderColor = HSSFColor.Black.Index;

                    //Set font style
                    IFont font = workbook.CreateFont();
                    //font.Color = HSSFColor.White.Index;
                    font.FontName = "Arial";
                    font.FontHeight = 200;
                    font.IsBold = true;
                    style.SetFont(font);

                    //Set background color
                    //style.FillForegroundColor = IndexedColors.DarkBlue.Index;
                    //style.FillPattern = FillPattern.SolidForeground;
                    style.Alignment = HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Center;

                    //Apply the style
                    cell.CellStyle = style;
                }

                //loops through data
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        string columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(dt.Rows[i][columnName].ToString());
                    }
                }

                using (var exportData = new MemoryStream())
                {
                    //Response.ClearContent();
                    Response.Clear();
                    //Response.Flush();
                    //Response.Buffer = true;
                    workbook.Write(exportData, false);
                    string attach = string.Format("attachment;filename={0}", "SharedContentReport.xls");
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


        [HttpGet]
        public JsonResult DLDownloadReportInCSV(string ProjectCode,string Location,DateTime? FromDate, DateTime? ToDate, string Category, string SubCategory,string RequesterLocation, int FromExport)
        {
            ShareContentReport objShareContentReport = new ShareContentReport();
            objShareContentReport.FromDate = FromDate;
            objShareContentReport.ToDate = ToDate;
            objShareContentReport.Category = Category;
            objShareContentReport.SubCategory = SubCategory;
            objShareContentReport.ProjectCode = ProjectCode;
            objShareContentReport.Location = Location;
            objShareContentReport.RequesterLocation = RequesterLocation;
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objShareContentReport);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.DLDownloadReport.ToString())), roleId, userid, "Get", out errorMessage);

                if (FromExport == 1)
                {
                    DataSet ds = data.data;
                    List<DownloadReportModel> lstModel = new List<DownloadReportModel>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DownloadReportModel model = new DownloadReportModel();
                        model.Req_No = Convert.ToInt32(ds.Tables[0].Rows[i]["Req_No"]);
                        model.Req_Date = Convert.ToString(ds.Tables[0].Rows[i]["Req_Date"]);
                        model.Req_By = Convert.ToString(ds.Tables[0].Rows[i]["ReqBy"]);
                        model.Reqester_Location = Convert.ToString(ds.Tables[0].Rows[i]["RequesterLocation"]);
                        model.Category = Convert.ToString(ds.Tables[0].Rows[i]["Category"]);
                        model.Sub_Category = Convert.ToString(ds.Tables[0].Rows[i]["SubCategory"]);
                        model.Project_Code = Convert.ToString(ds.Tables[0].Rows[i]["ProjectCode"]);

                        model.Project_Name = Convert.ToString(ds.Tables[0].Rows[i]["ProjectName"]);
                        model.Themetic_Area = Convert.ToString(ds.Tables[0].Rows[i]["ThemeticArea"]);
                        model.Funded_By = Convert.ToString(ds.Tables[0].Rows[i]["FundedBy"]);
                        model.Proposal_No = Convert.ToString(ds.Tables[0].Rows[i]["Proposal_No"]);

                        model.Report_No = Convert.ToString(ds.Tables[0].Rows[i]["Report_No"]);
                        model.Copyright = Convert.ToString(ds.Tables[0].Rows[i]["Copyright"]);
                        model.Project_Manager = Convert.ToString(ds.Tables[0].Rows[i]["ProjectManager"]);
                        model.Title = Convert.ToString(ds.Tables[0].Rows[i]["Title"]);

                        model.Sub_Title = Convert.ToString(ds.Tables[0].Rows[i]["Sub_Title"]);
                        model.Tags = Convert.ToString(ds.Tables[0].Rows[i]["Tags"]);
                        model.Abstract_Summary = Convert.ToString(ds.Tables[0].Rows[i]["Abstract_Summary"]);
                        model.Remark = Convert.ToString(ds.Tables[0].Rows[i]["Remark"]);


                        model.Document_Category = Convert.ToString(ds.Tables[0].Rows[i]["Document_Category"]);
                        model.Auth_Name = Convert.ToString(ds.Tables[0].Rows[i]["AuthName"]);
                        model.Accepted = Convert.ToString(ds.Tables[0].Rows[i]["Accepted"]);
                        model.File_Name = Convert.ToString(ds.Tables[0].Rows[i]["FileName"]);

                        model.File_Type = Convert.ToString(ds.Tables[0].Rows[i]["FileType"]);
                        model.File_Size = Convert.ToString(ds.Tables[0].Rows[i]["FileSize"]);
                        model.Content_Origin = Convert.ToString(ds.Tables[0].Rows[i]["ContentOrigin"]);
                        model.Status = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                        model.Reason = Convert.ToString(ds.Tables[0].Rows[i]["Reason"]);

                        lstModel.Add(model);
                    }
                    string baseurl = HttpContext.Server.MapPath("~");
                    baseurl = baseurl + "/Attachments/SampleCSVFile/DownloadDLReport.csv";


                    using (var writer = new StreamWriter(baseurl))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(lstModel as IEnumerable);
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
        public ActionResult DownloadDLReport()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();
        }
        public ActionResult UploadDLReport()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();
        }
        public ActionResult SharedDLReport()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();
        }

        [HttpGet]
        public JsonResult DLSharedReportInCSV(string ProjectCode, string Location, DateTime? FromDate, DateTime? ToDate, string Category, string SubCategory, string RequesterLocation, int FromExport)
        {
            ShareContentReport objShareContentReport = new ShareContentReport();
            objShareContentReport.FromDate = FromDate;
            objShareContentReport.ToDate = ToDate;
            objShareContentReport.Category = Category;
            objShareContentReport.SubCategory = SubCategory;
            objShareContentReport.ProjectCode = ProjectCode;
            objShareContentReport.Location = Location;
            objShareContentReport.RequesterLocation= RequesterLocation;
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objShareContentReport);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.DLSharedReport.ToString())), roleId, userid, "Get", out errorMessage);

                if (FromExport == 1)
                {
                    DataSet ds = data.data;
                    List<SharedReportModel> lstModel = new List<SharedReportModel>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SharedReportModel model = new SharedReportModel();
                        model.Req_No = Convert.ToInt32(ds.Tables[0].Rows[i]["Req_No"]);
                        model.DateofSharing = Convert.ToString(ds.Tables[0].Rows[i]["DateOfSharing"]);
                        model.Shared_by = Convert.ToString(ds.Tables[0].Rows[i]["SharedBy"]);
                        model.Reqester_Location = Convert.ToString(ds.Tables[0].Rows[i]["RequesterLocation"]);
                        model.Shared_With = Convert.ToString(ds.Tables[0].Rows[i]["SharedWith"]);
                        model.Category = Convert.ToString(ds.Tables[0].Rows[i]["Category"]);
                        model.Sub_Category = Convert.ToString(ds.Tables[0].Rows[i]["SubCategory"]);
                        model.Project_Code = Convert.ToString(ds.Tables[0].Rows[i]["ProjectCode"]);

                        model.Project_Name = Convert.ToString(ds.Tables[0].Rows[i]["ProjectName"]);
                        model.Themetic_Area = Convert.ToString(ds.Tables[0].Rows[i]["ThemeticArea"]);
                        model.Funded_By = Convert.ToString(ds.Tables[0].Rows[i]["FundedBy"]);
                        model.Proposal_No = Convert.ToString(ds.Tables[0].Rows[i]["Proposal_No"]);

                        model.Report_No = Convert.ToString(ds.Tables[0].Rows[i]["Report_No"]);
                        model.Copyright = Convert.ToString(ds.Tables[0].Rows[i]["Copyright"]);
                        model.Project_Manager = Convert.ToString(ds.Tables[0].Rows[i]["ProjectManager"]);
                        model.Title = Convert.ToString(ds.Tables[0].Rows[i]["Title"]);

                        model.Sub_Title = Convert.ToString(ds.Tables[0].Rows[i]["Sub_Title"]);
                        model.Tags = Convert.ToString(ds.Tables[0].Rows[i]["Tags"]);
                        model.Abstract_Summary = Convert.ToString(ds.Tables[0].Rows[i]["Abstract_Summary"]);
                        model.Remark = Convert.ToString(ds.Tables[0].Rows[i]["Remark"]);


                        model.Document_Category = Convert.ToString(ds.Tables[0].Rows[i]["Document_Category"]);
                        model.Auth_Name = Convert.ToString(ds.Tables[0].Rows[i]["AuthName"]);
                        model.Accepted = Convert.ToString(ds.Tables[0].Rows[i]["Accepted"]);
                        model.File_Name = Convert.ToString(ds.Tables[0].Rows[i]["FileName"]);

                        model.File_Type = Convert.ToString(ds.Tables[0].Rows[i]["FileType"]);
                        model.File_Size = Convert.ToString(ds.Tables[0].Rows[i]["FileSize"]);
                        model.Content_Origin = Convert.ToString(ds.Tables[0].Rows[i]["ContentOrigin"]);
                        model.Status = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                       

                        lstModel.Add(model);
                    }
                    string baseurl = HttpContext.Server.MapPath("~");
                    baseurl = baseurl + "/Attachments/SampleCSVFile/SharedDLReport.csv";
              
                    using (var writer = new StreamWriter(baseurl))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(lstModel as IEnumerable);
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
        public JsonResult DLUploadReportInCSV(string ProjectCode, string Location, DateTime? FromDate, DateTime? ToDate, string Category, string SubCategory, string RequesterLocation, int FromExport)
        {
            ShareContentReport objShareContentReport = new ShareContentReport();
            objShareContentReport.FromDate = FromDate;
            objShareContentReport.ToDate = ToDate;
            objShareContentReport.Category = Category;
            objShareContentReport.SubCategory = SubCategory;
            objShareContentReport.ProjectCode = ProjectCode;
            objShareContentReport.Location = Location;
            objShareContentReport.RequesterLocation= RequesterLocation;
            CommonMethods objCommonMethods = new CommonMethods();

            string stringTOXml = objCommonMethods.GetXMLFromObject(objShareContentReport);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

                var data = _objIDigitalLibrary.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.DLUploadReport.ToString())), roleId, userid, "Get", out errorMessage);

                if (FromExport == 1)
                {
                    DataSet ds = data.data;
                    List<UploadReportModel> lstModel = new List<UploadReportModel>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        UploadReportModel model = new UploadReportModel();
                        model.Req_No = Convert.ToInt32(ds.Tables[0].Rows[i]["Req_No"]);
                        model.Req_Date = Convert.ToString(ds.Tables[0].Rows[i]["Req_Date"]);
                        model.Req_By = Convert.ToString(ds.Tables[0].Rows[i]["ReqBy"]);
                        model.Reqester_Location = Convert.ToString(ds.Tables[0].Rows[i]["RequesterLocation"]);
                        model.Category = Convert.ToString(ds.Tables[0].Rows[i]["Category"]);
                        model.Sub_Category = Convert.ToString(ds.Tables[0].Rows[i]["SubCategory"]);
                        model.Project_Code = Convert.ToString(ds.Tables[0].Rows[i]["ProjectCode"]);

                        model.Project_Name = Convert.ToString(ds.Tables[0].Rows[i]["ProjectName"]);
                        model.Themetic_Area = Convert.ToString(ds.Tables[0].Rows[i]["ThemeticArea"]);
                        model.Funded_By = Convert.ToString(ds.Tables[0].Rows[i]["FundedBy"]);
                        model.Proposal_No = Convert.ToString(ds.Tables[0].Rows[i]["Proposal_No"]);

                        model.Report_No = Convert.ToString(ds.Tables[0].Rows[i]["Report_No"]);
                        model.Copyright = Convert.ToString(ds.Tables[0].Rows[i]["Copyright"]);
                        model.Project_Manager = Convert.ToString(ds.Tables[0].Rows[i]["ProjectManager"]);
                        model.Title = Convert.ToString(ds.Tables[0].Rows[i]["Title"]);

                        model.Sub_Title = Convert.ToString(ds.Tables[0].Rows[i]["Sub_Title"]);
                        model.Tags = Convert.ToString(ds.Tables[0].Rows[i]["Tags"]);
                        model.Abstract_Summary = Convert.ToString(ds.Tables[0].Rows[i]["Abstract_Summary"]);
                        model.Remark = Convert.ToString(ds.Tables[0].Rows[i]["Remark"]);


                        model.Document_Category = Convert.ToString(ds.Tables[0].Rows[i]["Document_Category"]);
                        model.Auth_Name = Convert.ToString(ds.Tables[0].Rows[i]["AuthName"]);
                        model.Accepted = Convert.ToString(ds.Tables[0].Rows[i]["Accepted"]);
                        model.File_Name = Convert.ToString(ds.Tables[0].Rows[i]["FileName"]);

                        model.File_Type = Convert.ToString(ds.Tables[0].Rows[i]["FileType"]);
                        model.File_Size = Convert.ToString(ds.Tables[0].Rows[i]["FileSize"]);
                        model.Content_Origin = Convert.ToString(ds.Tables[0].Rows[i]["ContentOrigin"]);
                        model.Status = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                        model.If_Resubmitted = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                        model.Reason = Convert.ToString(ds.Tables[0].Rows[i]["Reason"]);

                        lstModel.Add(model);
                    }
                    string baseurl = HttpContext.Server.MapPath("~");
                    baseurl = baseurl + "/Attachments/SampleCSVFile/UploadDLReport.csv";

                    using (var writer = new StreamWriter(baseurl))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(lstModel as IEnumerable);
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

    }
}