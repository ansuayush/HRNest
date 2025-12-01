
using Mitr.BLL;
using Mitr.CommonLib;
using Mitr.Model;  
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Areas.DigitalLibrary.Controllers
{
    public class CommonMethodController:Controller
    {

        //[SessionExpireFilterAttributeUserAdmin]

        [HttpPost]
        public JsonResult SaveMaster(MasterModel objMasterModel)
        {

            CommonMethods objCommonMethods = new CommonMethods();
            CommonBussinessBLL objCommonBussinessBLL = new CommonBussinessBLL();
            string stringTOXml = objCommonMethods.GetXMLFromObject(objMasterModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = 1;
                var data = objCommonBussinessBLL.PerformOperation(stringTOXml,Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.Master.ToString())), roleId, userid, "ADD", out errorMessage);
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
        public JsonResult BindMaster()
        {
            MasterModel objMasterModel = new MasterModel();
            objMasterModel.ID = 1;
            objMasterModel.IsGrid = 1;
            CommonMethods objCommonMethods = new CommonMethods();
            CommonBussinessBLL objCommonBussinessBLL = new CommonBussinessBLL();
            string stringTOXml = objCommonMethods.GetXMLFromObject(objMasterModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = 0;
                var data = objCommonBussinessBLL.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.Master.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetMaster(int id)
        {
            MasterModel objMasterModel = new MasterModel();
            objMasterModel.ID = id;
            objMasterModel.IsGrid = 2;
            CommonMethods objCommonMethods = new CommonMethods();
            CommonBussinessBLL objCommonBussinessBLL = new CommonBussinessBLL();
            string stringTOXml = objCommonMethods.GetXMLFromObject(objMasterModel);
            try
            {
                string errorMessage = string.Empty;
                int roleId = 0;
                int userid = 0;
                var data = objCommonBussinessBLL.GetRecords(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.Master.ToString())), roleId, userid, "GET", out errorMessage);
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
        public JsonResult GetDropdown(int ? ParentId, int masterTableType, bool isMasterTableType, bool isManualTable, int manualTable, int manualTableId)
        {           
            CommonBussinessBLL objCommonBussinessBLL = new CommonBussinessBLL();
            try
            {
               
                string errorMessage = string.Empty;
                var data= objCommonBussinessBLL.GetDropDown(masterTableType, ParentId, isMasterTableType, isManualTable, manualTable, manualTableId);
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

        private void Upload(string filePathTobeSaved)
        {
            HttpContext.Request.Files[0].SaveAs(filePathTobeSaved);
        }
        private void CreateDirectoryIfNotExists(string NewDirectory)
        {
            if (!Directory.Exists(NewDirectory))
            {
                //If No any such directory then creates the new one
                Directory.CreateDirectory(NewDirectory);
            }

        }
       [HttpPost]
        public JsonResult DeleteFile(FileModel objFileModel)
        {
            try
            {

                string baseurl = HttpContext.Server.MapPath("~");
                baseurl = baseurl + '/' + objFileModel.FileUrl;
                System.IO.File.Delete(baseurl);

                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "File has been deleted";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = "File has been deleted";
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = "";
                objCustomResponseModel.CommomDropDownData = null;

                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


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
        [HttpPost]
        public JsonResult DeleteFileOnEdit(FileModel objFileModel)
        {
            try
            {
                TagMaster objTagMaster = new TagMaster();
                objTagMaster.Id = objFileModel.Id;
                CommonMethods objCommonMethods = new CommonMethods();
                CommonBussinessBLL objCommonBussinessBLL = new CommonBussinessBLL();
                string stringTOXml = objCommonMethods.GetXMLFromObject(objTagMaster);

                string errorMessage = string.Empty;
                int roleId = 1;
                int userid = objTagMaster.UserId;
                var data = objCommonBussinessBLL.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.AttachementSearch.ToString())), roleId, userid, "Save", out errorMessage);
                string jsonData = JsonConvert.SerializeObject(data);
                if (objFileModel.AttachmentType != "Video")
                {
                    string baseurl = HttpContext.Server.MapPath("~");
                    baseurl = baseurl + '/' + objFileModel.FileUrl;
                    System.IO.File.Delete(baseurl);
                }

                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


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
        public JsonResult UploadDocument(string categoryText, string type, string SubCate)
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

                int extIndex = 1;
                if (splString.Length > 0)
                {
                    extIndex = splString.Length - 1;
                }

                string uploadNewFileName = newFile + "." + splString[extIndex];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);
                if (categoryText.ToLower().Contains('/')) 
                {                   
                    categoryText= categoryText.Replace("/", " ");
                }
                if (SubCate.ToLower().Contains('/'))
                {
                    SubCate = SubCate.Replace("/", " ");
                }
                baseurl = baseurl + "/" + categoryText+"/"+ SubCate;
                CreateDirectoryIfNotExists(baseurl);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);

                returrMessage = ConfigurationManager.AppSettings["RootFolder"].Replace("~", "") + "/" + categoryText + "/" + SubCate + "/"+ uploadNewFileName;

                FileModel objFileModel = new FileModel();
                objFileModel.ActualFileName = fileName;
                objFileModel.NewFileName = uploadNewFileName;
                objFileModel.FileUrl = returrMessage;
                objFileModel.FileSize = ConvertBytesToMegabytes(file.ContentLength).ToString();

                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = returrMessage;
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException ="";
                objCustomResponseModel.CommomDropDownData = null;
                objCustomResponseModel.FileModel = objFileModel;
                
                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

               
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
        double ConvertBytesToMegabytes(long bytes)
        {
            return Math.Round(((bytes / 1024f) / 1024f),2);
        }
        
      public JsonResult UploadOnBoardindDocument()
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

                int extIndex = 1;
                if (splString.Length > 0)
                {
                    extIndex = splString.Length - 1;
                }

                string uploadNewFileName = newFile + "." + splString[extIndex];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);

                baseurl = baseurl + "/OnboardingDocuments";
                CreateDirectoryIfNotExists(baseurl);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);

                returrMessage = ConfigurationManager.AppSettings["RootFolder"].Replace("~", "") + "/OnboardingDocuments/" + uploadNewFileName;

                FileModel objFileModel = new FileModel();
                objFileModel.ActualFileName = fileName;
                objFileModel.NewFileName = uploadNewFileName;
                objFileModel.FileUrl = returrMessage;
                objFileModel.FileSize = ConvertBytesToMegabytes(file.ContentLength).ToString();

                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = returrMessage;
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = "";
                objCustomResponseModel.CommomDropDownData = null;
                objCustomResponseModel.FileModel = objFileModel;

                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


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
      public JsonResult UploadOtherDocument()
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

                int extIndex = 1;
                if (splString.Length > 0)
                {
                    extIndex = splString.Length - 1;
                }

                string uploadNewFileName = newFile + "." + splString[extIndex];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);

                baseurl = baseurl + "/ProcurementDocument";
                CreateDirectoryIfNotExists(baseurl);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);

                returrMessage = ConfigurationManager.AppSettings["RootFolder"].Replace("~", "") + "/ProcurementDocument/" + uploadNewFileName;

                FileModel objFileModel = new FileModel();
                objFileModel.ActualFileName = fileName;
                objFileModel.NewFileName = uploadNewFileName;
                objFileModel.FileUrl = returrMessage;
                objFileModel.FileSize = ConvertBytesToMegabytes(file.ContentLength).ToString();

                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = returrMessage;
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = "";
                objCustomResponseModel.CommomDropDownData = null;
                objCustomResponseModel.FileModel = objFileModel;

                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


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
        public JsonResult UploadComplianceUserTransation()
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

                int extIndex = 1;
                if (splString.Length > 0)
                {
                    extIndex = splString.Length - 1;
                }

                string uploadNewFileName = newFile + "." + splString[extIndex];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);

                baseurl = baseurl + "/ComplianceUserTransation";
                CreateDirectoryIfNotExists(baseurl);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);

                returrMessage = ConfigurationManager.AppSettings["RootFolder"].Replace("~", "") + "/ComplianceUserTransation/" + uploadNewFileName;

                FileModel objFileModel = new FileModel();
                objFileModel.ActualFileName = fileName;
                objFileModel.NewFileName = uploadNewFileName;
                objFileModel.FileUrl = returrMessage;
                objFileModel.FileSize = ConvertBytesToMegabytes(file.ContentLength).ToString();

                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = returrMessage;
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = "";
                objCustomResponseModel.CommomDropDownData = null;
                objCustomResponseModel.FileModel = objFileModel;

                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


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
        public JsonResult UploadComplianceDocument()
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

                int extIndex = 1;
                if (splString.Length > 0)
                {
                    extIndex = splString.Length - 1;
                }

                string uploadNewFileName = newFile + "." + splString[extIndex];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);

                baseurl = baseurl + "/Compliance";
                CreateDirectoryIfNotExists(baseurl);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);

                returrMessage = ConfigurationManager.AppSettings["RootFolder"].Replace("~", "") + "/Compliance/" + uploadNewFileName;

                FileModel objFileModel = new FileModel();
                objFileModel.ActualFileName = fileName;
                objFileModel.NewFileName = uploadNewFileName;
                objFileModel.FileUrl = returrMessage;
                objFileModel.FileSize = ConvertBytesToMegabytes(file.ContentLength).ToString();

                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = returrMessage;
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = "";
                objCustomResponseModel.CommomDropDownData = null;
                objCustomResponseModel.FileModel = objFileModel;

                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


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

        public JsonResult UploadCapacityDocument()
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

                int extIndex = 1;
                if (splString.Length > 0)
                {
                    extIndex = splString.Length - 1;
                }

                string uploadNewFileName = newFile + "." + splString[extIndex];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);

                baseurl = baseurl + "/CapacityDocument";
                CreateDirectoryIfNotExists(baseurl);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);

                returrMessage = ConfigurationManager.AppSettings["RootFolder"].Replace("~", "") + "/CapacityDocument/" + uploadNewFileName;

                FileModel objFileModel = new FileModel();
                objFileModel.ActualFileName = fileName;
                objFileModel.NewFileName = uploadNewFileName;
                objFileModel.FileUrl = returrMessage;
                objFileModel.FileSize = ConvertBytesToMegabytes(file.ContentLength).ToString();

                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = returrMessage;
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = "";
                objCustomResponseModel.CommomDropDownData = null;
                objCustomResponseModel.FileModel = objFileModel;

                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


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
        public JsonResult UploadComplianceLegacy()
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

                int extIndex = 1;
                if (splString.Length > 0)
                {
                    extIndex = splString.Length - 1;
                }

                string uploadNewFileName = newFile + "." + splString[extIndex];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);

                baseurl = baseurl + "/ComplianceLegacy";
                CreateDirectoryIfNotExists(baseurl);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);

                returrMessage = ConfigurationManager.AppSettings["RootFolder"].Replace("~", "") + "/ComplianceLegacy/" + uploadNewFileName;

                FileModel objFileModel = new FileModel();
                objFileModel.ActualFileName = fileName;
                objFileModel.NewFileName = uploadNewFileName;
                objFileModel.FileUrl = returrMessage;
                objFileModel.FileSize = ConvertBytesToMegabytes(file.ContentLength).ToString();

                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = returrMessage;
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = "";
                objCustomResponseModel.CommomDropDownData = null;
                objCustomResponseModel.FileModel = objFileModel;

                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


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
        public JsonResult UploadPolicyDocument()
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

                int extIndex = 1;
                if (splString.Length > 0)
                {
                    extIndex = splString.Length - 1;
                }

                string uploadNewFileName = newFile + "." + splString[extIndex];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);

                baseurl = baseurl + "/OfficePolicyDocument";
                CreateDirectoryIfNotExists(baseurl);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);

                returrMessage = ConfigurationManager.AppSettings["RootFolder"].Replace("~", "") + "/OfficePolicyDocument/" + uploadNewFileName;

                FileModel objFileModel = new FileModel();
                objFileModel.ActualFileName = fileName;
                objFileModel.NewFileName = uploadNewFileName;
                objFileModel.FileUrl = returrMessage;
                objFileModel.FileSize = ConvertBytesToMegabytes(file.ContentLength).ToString();

                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = returrMessage;
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = "";
                objCustomResponseModel.CommomDropDownData = null;
                objCustomResponseModel.FileModel = objFileModel;

                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


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




        public JsonResult UploadExitDocument()
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

                int extIndex = 1;
                if (splString.Length > 0)
                {
                    extIndex = splString.Length - 1;
                }

                string uploadNewFileName = newFile + "." + splString[extIndex];
                string filePathTobeSaved = "";
                string baseurl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["RootFolder"]);

                baseurl = baseurl + "/ExitDocument";
                CreateDirectoryIfNotExists(baseurl);
                filePathTobeSaved = baseurl + "/" + uploadNewFileName;
                Upload(filePathTobeSaved);

                returrMessage = ConfigurationManager.AppSettings["RootFolder"].Replace("~", "") + "/ExitDocument/" + uploadNewFileName;

                FileModel objFileModel = new FileModel();
                objFileModel.ActualFileName = fileName;
                objFileModel.NewFileName = uploadNewFileName;
                objFileModel.FileUrl = returrMessage;
                objFileModel.FileSize = ConvertBytesToMegabytes(file.ContentLength).ToString();

                CustomResponseModel objCustomResponseModel = new CustomResponseModel();
                objCustomResponseModel.ValidationInput = 0;
                objCustomResponseModel.ErrorMessage = "";
                objCustomResponseModel.data = null;
                objCustomResponseModel.CustomMessage = returrMessage;
                objCustomResponseModel.IsSuccessStatusCode = true;
                objCustomResponseModel.CustumException = "";
                objCustomResponseModel.CommomDropDownData = null;
                objCustomResponseModel.FileModel = objFileModel;

                string jsonData = JsonConvert.SerializeObject(objCustomResponseModel);
                return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


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
    }
}