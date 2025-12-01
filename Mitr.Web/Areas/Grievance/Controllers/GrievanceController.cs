using Mitr.CommonClass;
using Mitr.DAL;
using Mitr.Model;
using Mitr.Models;
using Mitr.ModelsMaster;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ClsCommon = Mitr.DAL.ClsCommon;
using Common_SPU = Mitr.DAL.Common_SPU;

namespace Mitr.Areas.Grievance.Controllers
{
    [RouteArea("")]
    [CheckLoginFilter]
    public class GrievanceController : Controller
    {
        // GET: Grievance/Grievance
        public ActionResult Index()
        {
            return View();
        }


        #region "Private"

        MasterModal master;
        GrievanceModal Grievance;
        long LoginID = 0, LocationID = 0, Employeeid = 0;
        string IPAddress = string.Empty;
        static bool IsAnonimus = false;

        GetResponseModel GetResponseModel;

        #endregion
        public GrievanceController()
        {
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long.TryParse(clsApplicationSetting.GetSessionValue("LocationID"), out LocationID);
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out Employeeid);
            GetResponseModel = new GetResponseModel();
            IPAddress = ClsCommon.GetIPAddress();
            GetResponseModel.IPAddress = IPAddress;
            GetResponseModel.LoginID = LoginID;
            Grievance = new GrievanceModal();
            master = new MasterModal();
        }

        #region "Category"
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        public ActionResult CategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getResponse = new GetMasterResponse();
            getResponse.LoginID = LoginID;
            getResponse.IPAddress = IPAddress;
            getResponse.TableName = "GR_Category";
            getResponse.IsActive = "0,1";
            List<Models.MasterAll.List> Modal = new List<Models.MasterAll.List>();
            Modal = master.GetMasterAllList(getResponse);
            return View(Modal);
        }
        public ActionResult _CategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "GR_Category";
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            Models.MasterAll.Add Modal = new Models.MasterAll.Add();
            if (ID > 0)
            {
                GetMasterResponse getMasterResponse = new GetMasterResponse();
                getMasterResponse.LoginID = LoginID;
                getMasterResponse.IPAddress = IPAddress;
                getMasterResponse.TableName = TableName;
                getMasterResponse.ID = ID;
                Modal = master.GetMasterAll(getMasterResponse);
            }
            Modal.table_name = TableName;
            return PartialView(Modal);

        }
        [HttpPost]
        public ActionResult SaveMasterAll(string src, Models.MasterAll.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);

            Result.SuccessMessage = "Masters Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ID = ID;
                Result = master.fnSetMasterAll(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Sub-Category"
        public ActionResult SubCategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long subCategoryID = 0;
            int isDeleted = 0;

            List<SubCategory> Modal = new List<SubCategory>();
            Modal = Grievance.GetSubCategoryList(subCategoryID, isDeleted);
            List<SubcategoryGRAssigneeDetails> obj = Grievance.GetSubcategoryGRAssigneeDetails();
            ViewBag.ExportList = obj;
            return View(Modal);
        }
        public ActionResult _SubCategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            SubCategory Modal = new SubCategory();
            if (ID > 0)
            {
                Modal.SubCategoryID = ID;
                Modal = Grievance.GetSubCategoryDetail(Modal);
            }
            GetResponseModel getDropDown = new GetResponseModel();
            getDropDown.Doctype = "GR_Category";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);

        }
        [HttpPost]
        public ActionResult SubCategoryAdd(string src, SubCategory Modal, string Command)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);

            Result.SuccessMessage = "Sub-Category Can't Update";
            if (ModelState.IsValid)
            {
                Modal.Createdby = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.SubCategoryID = ID;
                Result = Grievance.fnSetSubCategory(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult _SubCategoryDetailsAdd(string src)
        {
            ClsCommon.MultipleLoginID = string.Empty;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            ViewBag.CatID = GetQueryString[3];
            long ID = 0, CatID = 0;
            long.TryParse(ViewBag.ID, out ID);
            long.TryParse(ViewBag.CatID, out CatID);

            SubCategoryAssigneeDetails Modal = new SubCategoryAssigneeDetails();
            if (ID > 0)
            {
                long id = 0;
                long subCategoryID = ID;
                GetResponseModel workLocation = new GetResponseModel();
                workLocation.Doctype = "WorkLocation";
                Modal.WorkLocationList = ClsCommon.GetDropDownList(workLocation);
                GetResponseModel employee = new GetResponseModel();
                employee.Doctype = "GR_Employee";
                employee.LoginID = LoginID;
                Modal.EmployeeList = ClsCommon.GetDropDownList(employee);
                GetResponseModel InternalExternal = new GetResponseModel();
                InternalExternal.Doctype = "InternalExternalMamber";
                InternalExternal.LoginID = LoginID;
                List<DropdownModel> Level1List = new List<DropdownModel>();
                Level1List.Add(new DropdownModel { ID = string.Empty, Name = "Select" });
                ViewBag.Level1List = Level1List;
                List<DropdownModel> Level2List = new List<DropdownModel>();
                Level2List.Add(new DropdownModel { ID = string.Empty, Name = "Select" });
                ViewBag.Level2List = Level2List;
                List<DropdownModel> Level3List = new List<DropdownModel>();
                Level3List.Add(new DropdownModel { ID = string.Empty, Name = "Select" });
                ViewBag.Level3List = Level3List;


                if (Modal.SubCategoryAssigneeList != null) Modal.SubCategoryAssigneeList.Clear();
                Modal.SubCategoryAssigneeList = Grievance.GetSubCategoryAssigneeList(id, CatID, subCategoryID);


                if (Modal.SubCategorySLAPolicy != null && Modal.SubCategorySLAPolicy.Count <= 0)
                {
                    subCategoryID = 0;
                    Modal.SubCategorySLAPolicy = Grievance.GetSubCategorySLAPolicyList(id, subCategoryID);
                }
                if (subCategoryID > 0)
                {
                    Modal.SubCategorySLAPolicy = Grievance.GetSubCategorySLAPolicyList(id, subCategoryID);
                }
            }
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult SubCategoryAssigneeAdd(string src, SubCategoryAssigneeDetails Modal, string Command)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            ViewBag.CatID = GetQueryString[3];
            long ID = 0, CatID = 0;
            long.TryParse(ViewBag.ID, out ID);
            long.TryParse(ViewBag.CatID, out CatID);
            if (Modal.Level1Id != null)
            {
                Modal.Level1 = string.Join(",", Modal.Level1Id);
            }
            if (Modal.Level2Id != null)
            {
                Modal.Level2 = string.Join(",", Modal.Level2Id);
            }
            if (Modal.Level3Id != null)
            {
                Modal.Level3 = string.Join(",", Modal.Level3Id);
            }

            Result.SuccessMessage = "Sub-Category Assignee Details Can't Update";
            if (ModelState.IsValid)
            {

                Modal.Createdby = LoginID;
                Modal.SubCategoryid = ID;
                Modal.Categoryid = CatID;
                Result = Grievance.fnSetSubCategoryAssignee(Modal);
                ModelState.Clear();
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _DeleteSubCategoryAssignee(string src)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            ViewBag.SubcatID = GetQueryString[3];
            ViewBag.CatID = GetQueryString[4];
            long ID = 0, SubcatID = 0, CatID = 0;
            long.TryParse(ViewBag.ID, out ID);
            long.TryParse(ViewBag.SubcatID, out SubcatID);
            long.TryParse(ViewBag.CatID, out CatID);

            if (SubcatID > 0)
            {
                TempData["ID"] = SubcatID;
            }

            if (ModelState.IsValid)
            {
                SubCategoryAssigneeDetails Modal = new SubCategoryAssigneeDetails();
                Modal.Createdby = LoginID;
                Modal.Categoryid = CatID;
                Modal.SubCategoryid = SubcatID;
                Modal.ID = ID;
                Modal.DeleteDat = DateTime.Now.ToString();
                Modal.IsDeleted = 1;
                Result = Grievance.fnDeleteSubCategoryAssignee(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubCategorySLAPolicyAdd(string src, SubCategoryAssigneeDetails Modal, string Command)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            ViewBag.CatID = GetQueryString[3];
            long ID = 0, CatID = 0;
            long.TryParse(ViewBag.ID, out ID);
            long.TryParse(ViewBag.CatID, out CatID);
            if (Modal.SubCategorySLAPolicy != null)
            {
                for (int i = 0; i < Modal.SubCategorySLAPolicy.Count; i++)
                {
                    if (Modal.SubCategorySLAPolicy[i].ActionDefault)
                    {
                        if (string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].FirstResponse)
                            && string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].FollowUpEvery)
                            && string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].EscalationLevel1)
                            && string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].EscalationLevel2)
                            && string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].EscalationLevel3)
                            && string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].TicketAutoCloseAfter)
                            )
                        {
                            Result.SuccessMessage = "Please enter minimum one field is required in selected row.";
                            return Json(Result, JsonRequestBehavior.AllowGet);
                        }
                    }

                }


            }
            if (!ModelState.IsValid)
            {
                if (ID > 0)
                {
                    SubCategorySLAPolicy sla = new SubCategorySLAPolicy();
                    for (int i = 0; i < Modal.SubCategorySLAPolicy.Count; i++)
                    {
                        sla.ID = Modal.SubCategorySLAPolicy[i].ID;
                        sla.Categoryid = CatID;
                        sla.SubCategoryid = ID;
                        sla.Priority = Modal.SubCategorySLAPolicy[i].Priority;
                        sla.ActionDefault = Modal.SubCategorySLAPolicy[i].ActionDefault;
                        sla.ActionFreezed = Modal.SubCategorySLAPolicy[i].ActionFreezed;
                        sla.FirstResponse = string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].FirstResponse) ? "0" : Modal.SubCategorySLAPolicy[i].FirstResponse;
                        sla.FollowUpEvery = string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].FollowUpEvery) ? "0" : Modal.SubCategorySLAPolicy[i].FollowUpEvery;
                        sla.EscalationLevel1 = string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].EscalationLevel1) ? "0" : Modal.SubCategorySLAPolicy[i].EscalationLevel1;
                        sla.EscalationLevel2 = string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].EscalationLevel2) ? "0" : Modal.SubCategorySLAPolicy[i].EscalationLevel2;
                        sla.EscalationLevel3 = string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].EscalationLevel3) ? "0" : Modal.SubCategorySLAPolicy[i].EscalationLevel3;
                        sla.TicketAutoCloseAfter = string.IsNullOrEmpty(Modal.SubCategorySLAPolicy[i].TicketAutoCloseAfter) ? "0" : Modal.SubCategorySLAPolicy[i].TicketAutoCloseAfter;
                        sla.Createdby = LoginID;
                        sla.IsActive = true;
                        Result = Grievance.fnSetSubCategorySLAPolicy(sla);
                        ModelState.Clear();
                    }
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region "External Member"
        public ActionResult ExternalMemberList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<ExternalMember> Modal = new List<ExternalMember>();
            Modal = Grievance.GetExternalMemberList(0);
            return View(Modal);
        }
        [HttpGet]
        public ActionResult ExternalMemberAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            ExternalMember Modal = new ExternalMember();
            Modal.ExternalCode = Grievance.GetExternalCode();
            Modal.DOJ = DateTime.Now.ToString("yyyy-MM-dd");
            if (ID > 0)
            {
                Modal.ID = ID;

                Modal = Grievance.GetExternalMemberDetail(Modal);
                Modal.DOL = (string.Compare(Modal.DOL, "1900-01-01") == 0) ? Modal.DOL = string.Empty : Modal.DOL;
            }
            ViewBag.CountryList = master.GetCountryList(0).Where(x => x.IsActive).ToList();
            GetResponseModel state = new GetResponseModel();
            state.Doctype = "State";
            state.ID = 1;
            Modal.StateList = ClsCommon.GetDropDownList(state);
            GetResponseModel city = new GetResponseModel();
            city.Doctype = "City";
            Modal.CityList = ClsCommon.GetDropDownList(city);
            return View(Modal);
        }

        [HttpPost]
        public ActionResult ExternalMemberAdd(string src, ExternalMember Modal, string Command)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "External Member is not Saved";
            //if (ID <= 0)
            //{
            //    if (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) > Convert.ToDateTime(Modal.DOJ))
            //    {
            //        TempData["SuccessMsg"] = "Joining date can not be less than from current date.";
            //        return RedirectToAction("ExternalMemberList", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Grievance/ExternalMemberList") });
            //    }
            //}
            if (!ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Command))
                    {
                        Modal.Createdby = LoginID;
                        Modal.ID = ID;
                        Modal.IsActive = true;
                        Result = Grievance.fnSetExternalMember(Modal);
                        ModelState.Clear();
                        if (Result.Status)
                        {
                            TempData["Success"] = "Y";
                            TempData["SuccessMsg"] = Result.SuccessMessage;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClsCommon.LogError("Error during ExternalMemberAdd. The query was executed :", ex.ToString(), "spu_GetExternalMember", "GrievanceController", "GrievanceController", "");
                }
            }
            return RedirectToAction("ExternalMemberList", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Grievance/ExternalMemberList") });
        }
        #endregion

        #region "User-Grievance"
        [HttpGet]
        public ActionResult UserGrievanceList(string src)
        {
            ViewBag.src = src;
            int UserId = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            UserGrievance modal = new UserGrievance();
            modal.ID = 0;
            modal.Categoryid = 0;
            modal.Subcatid = 0;
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.EmpId = GetQueryString[2];
                ViewBag.LocationId = GetQueryString[5];
                int.TryParse(ViewBag.UserId, out UserId);
                modal.Locationid = Convert.ToInt64(GetQueryString[5]);
                modal.Createdby = UserId;
            }
            else
            {
                modal.Locationid = LocationID;
                modal.Createdby = LoginID;
            }
            modal.LoginPage = "UserAccident";
            List<UserGrievance> Modal = Grievance.GetUserGrievanceList(modal);
            return View(Modal);
        }
        [HttpGet]
        public ActionResult _UserGrievanceAdd(string src)
        {
            ViewBag.src = src;
            int UserId = 0;
            int Locationid = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[1];
            ViewBag.SubcatAssingid = GetQueryString[2];
            long ID = 0, SubcatAssingid = 0;
            long.TryParse(ViewBag.ID, out ID);
            long.TryParse(ViewBag.SubcatAssingid, out SubcatAssingid);
            UserGrievance Modal = new UserGrievance();
            Modal.SubcatAssingid = SubcatAssingid;
            GetResponseModel getDropDown = new GetResponseModel();
            getDropDown.Doctype = "GR_Category";
            Modal.CategoryList = ClsCommon.GetDropDownList(getDropDown);
            GetResponseModel subCategory = new GetResponseModel();
            subCategory.Doctype = "Gr_SubCategory";
            Modal.SubCategoryList = ClsCommon.GetDropDownList(subCategory);
            List<DropdownModel> PriorityList = new List<DropdownModel>();
            PriorityList.Add(new DropdownModel { ID = string.Empty, Name = "Select" });
            Modal.PriorityList = PriorityList;
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.LocationId = GetQueryString[5];
                ViewBag.EmpId = GetQueryString[2];
                int.TryParse(ViewBag.UserId, out UserId);
                int.TryParse(ViewBag.LocationId, out Locationid);
                Modal.Mobile = ViewBag.Mobile;
                Modal.UserId = UserId;
                Modal.Locationid = Locationid;

            }
            return PartialView(Modal);

        }
        [HttpPost]
        public ActionResult UserGrievanceAdd(string src, UserGrievance Modal, string Command)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "Grievance Can't Update";
            string _Path = clsApplicationSetting.GetPhysicalPath("Grievance");
            if (ModelState.IsValid)
            {
                if (Modal.UploadFile1 != null)
                {
                    var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadFile1);
                    if (RvFile.IsValid)
                    {
                        if (Modal.Mobile == "Mobile")
                        {
                            Modal.Attachmentid1 = Common_SPU.fnSetAttachmentsforMobile(0, RvFile.FileName, RvFile.FileExt, Modal.UserId, "");
                        }
                        else
                        {
                            Modal.Attachmentid1 = Common_SPU.fnSetAttachments(0, RvFile.FileName, RvFile.FileExt, "");
                        }

                        Modal.UploadFile1.SaveAs(Path.Combine(_Path, Modal.Attachmentid1 + RvFile.FileExt));
                    }
                    else
                    {
                        Result.Status = RvFile.IsValid;
                        Result.SuccessMessage = RvFile.Message;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                if (Modal.UploadFile2 != null)
                {

                    var RvFile2 = clsApplicationSetting.ValidateFile(Modal.UploadFile2);
                    if (RvFile2.IsValid)
                    {
                        if (Modal.Mobile == "Mobile")
                        {
                            Modal.Attachmentid2 = Common_SPU.fnSetAttachmentsforMobile(0, RvFile2.FileName, RvFile2.FileExt, Modal.UserId, "");
                        }
                        else
                        {
                            Modal.Attachmentid2 = Common_SPU.fnSetAttachments(0, RvFile2.FileName, RvFile2.FileExt, "");
                        }

                        Modal.UploadFile2.SaveAs(Path.Combine(_Path, Modal.Attachmentid2 + RvFile2.FileExt));
                    }
                    else
                    {
                        Result.Status = RvFile2.IsValid;
                        Result.SuccessMessage = RvFile2.Message;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Modal.Mobile == "Mobile")
                {
                    Modal.Createdby = Modal.UserId;
                    Modal.Locationid = Modal.Locationid;
                    Modal.Type = "Mobile";
                }
                else
                {
                    Modal.Createdby = LoginID;
                    Modal.Locationid = LocationID;
                    Modal.Type = "Web";
                }

                Modal.ID = ID;
                Modal.Statusid = 1;
                Result = Grievance.fnSetUserGrievance(Modal);
                ModelState.Clear();
                if (Result.Status)
                {
                    if (Modal.Mobile == "Mobile")
                    {
                        Common_SPU.fnCreateMail_GrievanceForMobile(Result.ID, "", Modal.UserId);
                    }
                    else
                    {
                        Common_SPU.fnCreateMail_Grievance(Result.ID, "");
                    }

                }

            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UserGrievanceEdit(string src)
        {
            ViewBag.src = src;
            long ID = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (GetQueryString.Length > 3)
            {
                ViewBag.Mobile = GetQueryString[3];
                ViewBag.UserId = GetQueryString[4];
                ViewBag.ID = GetQueryString[5];
                ViewBag.EmpId = GetQueryString[2];
                ViewBag.LocationId = GetQueryString[6];
                long.TryParse(ViewBag.ID, out ID);
            }
            else
            {
                ViewBag.ID = GetQueryString[2];
                long.TryParse(ViewBag.ID, out ID);
            }

            ViewBag._Path = clsApplicationSetting.GetPhysicalPath("Grievance");
            UserGrievance Modal = new UserGrievance();
            if (ID > 0)
            {
                Modal.ID = ID;
                Modal = Grievance.GetUserGrievanceDetails(Modal);
                Modal.UserGrievanceAccidentList = Grievance.GetUserGrievanceAccidentList(ID);
            }
            return View(Modal);
        }

        [HttpGet]
        public ActionResult _UserGrievanceNoteAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            long ID = 0, Categoryid = 0, Subcatid = 0, SubcatAssingid = 0;

            UserGrievanceAccident Modal = new UserGrievanceAccident();
            if (GetQueryString.Length > 3 && GetQueryString[3] == "Mobile")
            {

                ViewBag.ID = GetQueryString[5];
                long.TryParse(ViewBag.ID, out ID);
                ViewBag.Subcatid = GetQueryString[6];
                ViewBag.Categoryid = GetQueryString[7];
                ViewBag.SubcatAssingid = GetQueryString[8];
                long.TryParse(ViewBag.Subcatid, out Subcatid);
                long.TryParse(ViewBag.Categoryid, out Categoryid);
                long.TryParse(ViewBag.SubcatAssingid, out SubcatAssingid);
                LocationID = Convert.ToInt64(GetQueryString[9]); ;

                Modal.UserId = Convert.ToInt64(GetQueryString[4]);
                Modal.Mobile = Convert.ToString(GetQueryString[3]);
                Modal.EmpId = Convert.ToInt64(GetQueryString[2]);
                Modal.Locationid = LocationID;


            }
            else
            {
                ViewBag.ID = GetQueryString[2];
                ViewBag.Subcatid = GetQueryString[3];
                ViewBag.Categoryid = GetQueryString[4];
                ViewBag.SubcatAssingid = GetQueryString[5];
                long.TryParse(ViewBag.Subcatid, out Subcatid);
                long.TryParse(ViewBag.Categoryid, out Categoryid);
                long.TryParse(ViewBag.SubcatAssingid, out SubcatAssingid);
                long.TryParse(ViewBag.ID, out ID);
            }

            GetResponseModel GrEscalation = new GetResponseModel();
            GrEscalation.Doctype = "GrAccident";
            GrEscalation.ID = ID;
            Modal.ChooseList = ClsCommon.GR_GetDropDownList(GrEscalation);
            GetResponseModel escalate = new GetResponseModel();
            escalate.Doctype = "GrEscalatedTo";
            escalate.ID = Categoryid;
            escalate.AdditionalID = Subcatid;
            escalate.AdditionalID1 = ID;
            Modal.EscalateList = ClsCommon.GR_GetDropDownList(escalate);
            GetResponseModel employee = new GetResponseModel();
            employee.Doctype = "GrEmployee";
            employee.ID = Categoryid;
            employee.AdditionalID = Subcatid;
            employee.AdditionalID1 = LocationID;
            Modal.EmployeeList = ClsCommon.GR_GetDropDownList(employee);
            return PartialView(Modal);

        }

        [HttpPost]
        public ActionResult UserGrievanceNotesAdd(string src, UserGrievanceAccident Modal, string Command)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            long ID = 0, Categoryid = 0, Subcatid = 0, SubcatAssingid = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            ViewBag.Subcatid = GetQueryString[3];
            ViewBag.Categoryid = GetQueryString[4];
            ViewBag.SubcatAssingid = GetQueryString[5];
            long.TryParse(ViewBag.ID, out ID);
            long.TryParse(ViewBag.Subcatid, out Subcatid);
            long.TryParse(ViewBag.Categoryid, out Categoryid);
            long.TryParse(ViewBag.SubcatAssingid, out SubcatAssingid);
            //if (GetQueryString.Length > 3 && GetQueryString[3] == "Mobile")
            //{
            //    LoginID = Convert.ToInt64(GetQueryString[5]);
            //    ViewBag.ID = GetQueryString[5];
            //    ViewBag.Subcatid = GetQueryString[6];
            //    ViewBag.Categoryid = GetQueryString[7];
            //    ViewBag.SubcatAssingid = GetQueryString[8];
            //    long.TryParse(ViewBag.ID, out ID);
            //    long.TryParse(ViewBag.Subcatid, out Subcatid);
            //    long.TryParse(ViewBag.Categoryid, out Categoryid);
            //    long.TryParse(ViewBag.SubcatAssingid, out SubcatAssingid);
            //}
            //else
            //{
            //    ViewBag.ID = GetQueryString[2];
            //    ViewBag.Subcatid = GetQueryString[3];
            //    ViewBag.Categoryid = GetQueryString[4];
            //    ViewBag.SubcatAssingid = GetQueryString[5];
            //    long.TryParse(ViewBag.ID, out ID);
            //    long.TryParse(ViewBag.Subcatid, out Subcatid);
            //    long.TryParse(ViewBag.Categoryid, out Categoryid);
            //    long.TryParse(ViewBag.SubcatAssingid, out SubcatAssingid);
            //}
            if (Modal.Mobile == "Mobile")
            {
                LoginID = Modal.UserId;
                LocationID = Modal.Locationid;
            }
            if (!ModelState.IsValid)
            {
                Result.SuccessMessage = "Grievance Can't Update";
                string _Path = clsApplicationSetting.GetPhysicalPath("Grievance");


                if (Modal.UploadFile != null)
                {
                    var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadFile);
                    if (RvFile.IsValid)
                    {
                        if (Modal.Mobile == "Mobile")
                        {
                            Modal.Attachmentid = Common_SPU.fnSetAttachmentsforMobile(0, RvFile.FileName, RvFile.FileExt, Modal.UserId, "");

                        }
                        else
                        {
                            Modal.Attachmentid = Common_SPU.fnSetAttachments(0, RvFile.FileName, RvFile.FileExt, "");
                        }

                        Modal.UploadFile.SaveAs(Path.Combine(_Path, Modal.Attachmentid + RvFile.FileExt));
                    }
                    else
                    {
                        Result.Status = RvFile.IsValid;
                        Result.SuccessMessage = RvFile.Message;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                if (Modal.Choosename == "7")
                {
                    Modal.ResolvedCategoryid = Modal.Categoryid;
                    Modal.ResolvedSubCategoryid = Modal.SubCategoryid;
                    if (Modal.EmployeeId != null)
                    {
                        Modal.Employee = string.Join(",", Modal.EmployeeId);
                    }
                }
                else if (Modal.Choosename == "6")
                {
                    if (Modal.Escalateid != null)
                    {
                        Modal.Escalateid = Modal.Escalateid;
                    }
                }
                else
                {
                    if (Modal.EmployeeId != null)
                    {
                        Modal.Employee = string.Join(",", Modal.EmployeeId);
                        Modal.Escalateid = Modal.EmployeeId[0];
                    }
                }
                Modal.Createdby = LoginID;
                Modal.Grievanceid = ID;
                Modal.Categoryid = Categoryid;
                Modal.SubCategoryid = Subcatid;
                Modal.SubcatAssingid = SubcatAssingid;
                Modal.Locationid = LocationID;
                if (Modal.EscalateNextlevelid > 0)
                    Modal.Escalateid = Modal.EscalateNextlevelid;

                string empid = Convert.ToString(Modal.Escalateid);
                string ChooseOption = Modal.Choosename;
                Result = Grievance.fnSetUserGrievanceAccident(Modal);
                ModelState.Clear();
                if (Result.Status)
                {
                    if (Modal.ToCCMailId == null)
                    {
                        Modal.ToCCMailId = "";
                    }
                    if (Modal.Mobile == "Mobile")
                    {


                        Common_SPU.fnCreateMail_Grievance_ActionForMobile(Result.ID, ChooseOption, empid, Modal.EmpId, Modal.ToCCMailId);
                    }
                    else
                    {
                        Common_SPU.fnCreateMail_Grievance_Action(Result.ID, ChooseOption, empid, Modal.ToCCMailId);
                    }
                   

                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region "Grievance Committee"
        [HttpGet]
        public ActionResult GrievanceCommitteeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            UserGrievance modal = new UserGrievance();
            modal.ID = 0;
            modal.Categoryid = 0;
            modal.Subcatid = 0;
            modal.Locationid = LocationID;
            modal.Createdby = Employeeid;
            modal.LoginPage = "Committee";
            List<UserGrievance> Modal = Grievance.GetUserGrievanceList(modal);
            return View(Modal);
        }

        [HttpGet]
        public ActionResult GrievanceCommitteeEdit(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            ViewBag._Path = clsApplicationSetting.GetPhysicalPath("Grievance");
            UserGrievance Modal = new UserGrievance();
            if (ID > 0)
            {
                Modal.ID = ID;
                Modal.LoginPage = "Committee";
                Modal.Createdby = Employeeid;
                Modal = Grievance.GetUserGrievanceDetails(Modal);
                if (Modal != null) IsAnonimus = Modal.IsAnonymous;
                Modal.UserGrievanceAccidentList = Grievance.GetUserGrievanceAccidentList(ID);
            }
            return View(Modal);
        }

        [HttpGet]
        public ActionResult _GrievanceCommitteeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long ID = 0, Categoryid = 0, Subcatid = 0;
            if (GetQueryString.Length > 3 && GetQueryString[3] == "Mobile")
            {
                ViewBag.ID = GetQueryString[5];
                ViewBag.Subcatid = GetQueryString[6];
                ViewBag.Categoryid = GetQueryString[7];

                long.TryParse(ViewBag.ID, out ID);
                long.TryParse(ViewBag.Categoryid, out Categoryid);
                long.TryParse(ViewBag.Subcatid, out Subcatid);
            }
            else
            {

                ViewBag.ID = GetQueryString[2];
                ViewBag.Subcatid = GetQueryString[3];
                ViewBag.Categoryid = GetQueryString[4];

                long.TryParse(ViewBag.ID, out ID);
                long.TryParse(ViewBag.Categoryid, out Categoryid);
                long.TryParse(ViewBag.Subcatid, out Subcatid);
            }

            UserGrievanceAccident Modal = new UserGrievanceAccident();

            GetResponseModel GrEscalation = new GetResponseModel();
            GrEscalation.Doctype = "GrCommittee";
            GrEscalation.ID = ID;
            if (IsAnonimus)
                GrEscalation.AdditionalID = 20000;
            else GrEscalation.AdditionalID = 7;
            GrEscalation.LoginID = Employeeid;
            Modal.ChooseList = ClsCommon.GR_GetDropDownList(GrEscalation);

            GetResponseModel escalate = new GetResponseModel();
            escalate.Doctype = "GrEscalatedTo";
            escalate.ID = Categoryid;
            escalate.AdditionalID = Subcatid;
            escalate.AdditionalID1 = ID;
            escalate.LoginID = Employeeid;
            Modal.EscalateList = ClsCommon.GR_GetDropDownList(escalate);

            GetResponseModel nextlevels = new GetResponseModel();
            nextlevels.Doctype = "ForwardtoNextlevels";
            nextlevels.ID = Categoryid;
            nextlevels.AdditionalID = Subcatid;
            nextlevels.AdditionalID1 = ID;
            nextlevels.LoginID = Employeeid;
            Modal.EscalateNextlevelsList = ClsCommon.GR_GetDropDownList(nextlevels);

            GetResponseModel InternalExternal = new GetResponseModel();
            InternalExternal.Doctype = "InternalExternalMamber";
            Modal.EmployeeList = ClsCommon.GetDropDownList(InternalExternal);
            GetResponseModel getDropDown = new GetResponseModel();
            getDropDown.Doctype = "GR_Category";
            Modal.CategoryList = ClsCommon.GetDropDownList(getDropDown);
            GetResponseModel subCategory = new GetResponseModel();
            subCategory.Doctype = "Gr_SubCategory";
            Modal.SubCategoryList = ClsCommon.GetDropDownList(subCategory);
            GetResponseModel emplist = new GetResponseModel();
            getDropDown.Doctype = "Employee";
            ViewBag.EmpList = ClsCommon.GetDropDownList(getDropDown);

            return PartialView(Modal);
        }

        [HttpGet]
        public ActionResult _ViewSLAPolicy(string src)
        {
            ClsCommon.MultipleLoginID = string.Empty;
            ViewBag.src = src;
            long ID = 0, subCategoryID = 0, CatID = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (GetQueryString.Length > 3 && GetQueryString[3] == "Mobile")
            {
                ViewBag.ID = GetQueryString[6];
                ViewBag.subCategoryID = GetQueryString[7];
                ViewBag.CatID = GetQueryString[8];
                long.TryParse(ViewBag.ID, out ID);
                long.TryParse(ViewBag.subCategoryID, out subCategoryID);
                long.TryParse(ViewBag.CatID, out CatID);
            }
            else
            {
                ViewBag.ID = GetQueryString[2];
                ViewBag.subCategoryID = GetQueryString[3];
                ViewBag.CatID = GetQueryString[4];
                long.TryParse(ViewBag.ID, out ID);
                long.TryParse(ViewBag.subCategoryID, out subCategoryID);
                long.TryParse(ViewBag.CatID, out CatID);
            }


            SubCategoryAssigneeDetails Modal = new SubCategoryAssigneeDetails();
            if (ID > 0)
            {
                long id = CatID;

                if (Modal.SubCategorySLAPolicy != null && Modal.SubCategorySLAPolicy.Count <= 0)
                {
                    subCategoryID = 0;
                    Modal.SubCategorySLAPolicy = Grievance.GetSubCategorySLAPolicyList(id, subCategoryID);
                }
                if (subCategoryID > 0)
                {
                    Modal.SubCategorySLAPolicy = Grievance.GetSubCategorySLAPolicyList(id, subCategoryID);
                }
            }
            return PartialView(Modal);
        }

        #endregion
    }
}