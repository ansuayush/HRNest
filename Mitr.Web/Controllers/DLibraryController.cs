using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class DLibraryController : Controller
    {
        IMasterHelper Master;
        IDLibraryHelper Library;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        public DLibraryController()
        {
            getResponse = new GetResponse();
            Library = new DLibraryModel();
            Master = new MasterModal();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        public ActionResult SubCategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "DL_SUBCATEGORY";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _SubCategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "DL_SUBCATEGORY";
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            MasterAll.Add Modal = new MasterAll.Add();
            if (ID > 0)
            {
                GetMasterResponse getMasterResponse = new GetMasterResponse();
                getMasterResponse.LoginID = LoginID;
                getMasterResponse.IPAddress = IPAddress;
                getMasterResponse.TableName = TableName;
                getMasterResponse.ID = ID;
                Modal = Master.GetMasterAll(getMasterResponse);
            }
            Modal.table_name = TableName;
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "DL_CATEGORY";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult SaveMasterAll(string src, MasterAll.Add Modal, string Command)
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
                Result = Master.fnSetMasterAll(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TagList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            List<DLibrary.TagList> result = new List<DLibrary.TagList>();
            getResponse.ID = ID;
            result = Library.GetTagLists(getResponse);
            return View(result);
        }
        public ActionResult _TagAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            DLibrary.TagAdd model = new DLibrary.TagAdd();
            if (ID > 0)
            {
                model = Library.GetTag(ID);
            }
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "ThematicArea";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SaveTag(string src, DLibrary.TagAdd Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "Masters Can't Update";
            if (ModelState.IsValid)
            {
                Modal.ID = ID;
                Result = Library.SetTag(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DigitalLibrary(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "DL_SUBCATEGORY";
            getMasterResponse.IsActive = "0,1";
            ViewBag.SubCategory = Master.GetMasterAllList(getMasterResponse);
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "DL_CATEGORY";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return View();
        }

       
        public JsonResult ProjectDetails(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string projectid = GetQueryString[2];
            DLibrary.ProjectList _list = new DLibrary.ProjectList();
            if (projectid != "")
            {
                _list = Library.GetProjectDetails(projectid);
            }
            return Json(_list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubCategoryByCategory(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string groupId = GetQueryString[2];
            List<DropDownList> _list = new List<DropDownList>();
            if (groupId != "")
            {
                string Doctype = "SubcategoryByCategory";
                _list = Library.GetSubCategoryByCategory(groupId, Doctype);
            }
            return Json(_list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ContentForm(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            getResponse.ID = ID;
            getResponse.Doctype = "ContentFormDetails";
            DLibrary.ContentForm model = new DLibrary.ContentForm();
            model = Library.GetContentFormDetails(getResponse);
            return View(model);
        }

        [HttpPost]
        public ActionResult ContentForm(string src, DLibrary.ContentForm model)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "Action Can't Update";
            model.Id = ID;
            Result = Library.AddContentForm(model);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}
