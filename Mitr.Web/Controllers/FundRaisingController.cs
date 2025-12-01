using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.OleDb;
using System.Web.UI.WebControls;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class FundRaisingController : Controller
    {
        // GET: FundRaising
        IMasterHelper Master;
        IFundRaisingHelper Fund;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        public FundRaisingController()
        {
            getResponse = new GetResponse();
            Master = new MasterModal();
            Fund = new FundRaisingModal();
            // MPR = new MPRModal();
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
        public ActionResult StageLevelList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "FR_STAGELEVEL";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        public ActionResult _StageLevelAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "FR_STAGELEVEL";
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
        public ActionResult StageList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "FR_STAGE";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        public ActionResult _StageAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "FR_STAGE";
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
            getDropDown.Doctype = "FR_STAGELEVEL";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);

        }
        public ActionResult KeyFocusAreaList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "FR_KFA";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        public ActionResult _KeyFocusAreaAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "FR_KFA";
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

            return PartialView(Modal);

        }
        public ActionResult CategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "FR_CATEGORY";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        public ActionResult _CategoryListAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "FR_CATEGORY";
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

            return PartialView(Modal);

        }
        public ActionResult C3FitList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "FR_C3FIT";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        public ActionResult _C3FitListAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "FR_C3FIT";
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

            return PartialView(Modal);

        }
        public ActionResult ActivityList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "FR_ACTIVITY";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        public ActionResult _ActivityAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "FR_ACTIVITY";
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
            getDropDown.Doctype = "FR_STAGE";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);

        }
        public ActionResult ProspectList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];            
            List<FundRaising.Prospect.List> Modal = new List<FundRaising.Prospect.List>();
            Modal = Fund.GetProspectList(0);
            ViewBag.ExportList = Common_SPU.fnGetFRProspectListExport(0);           
            return View(Modal);
        }
        public ActionResult AddProspect(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            FundRaising.Prospect.Add Modal = new FundRaising.Prospect.Add();
            getResponse.ID = ID;           
            Modal = Fund.GetFundRaisingDetail(getResponse);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult AddProspect(string src, FundRaising.Prospect.Add Modal, string Command)
        {
            ViewBag.TabIndex = 1;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            PostResult.SuccessMessage = "Action Can't Update";
            if (Modal.lstContactDetails==null)
            {
                ModelState.AddModelError("Id", "");
                PostResult.SuccessMessage = "Enter Contact Details.";                
            }
            if (ModelState.IsValid)
            {
                Modal.Id = ID;
                PostResult = Fund.SetProspect(Modal);
                if (PostResult.Status)
                {
                    string Retid = "0";
                    long Prospectid = PostResult.ID;
                    foreach (var item in Modal.lstContactDetails)
                    {
                        item.ProspectId = Prospectid;
                        PostResult= Fund.SetProspectContact(item);
                        Retid = Retid + "," + PostResult.ID.ToString();
                    }
                    ClsCommon.fnSetDataString("update FR_ProspectContacts set isdeleted=1,deletedat=CURRENT_TIMESTAMP,deletedby="+ LoginID.ToString() + " where id not in ("+ Retid + ") and ProspectId="+ Prospectid.ToString() + "");
                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/FundRaising/ProspectList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/FundRaising/ProspectList*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        //Not in use, Common Ajax UploadProspect is currently working 
        [HttpPost]
        public JsonResult UploadProspect(string src)
        {
            PostResponse PostResult = new PostResponse();
            string _imgname = string.Empty;
            //string retvalue = "";
            string saveloc = "/Attachments/";
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {

                var pic = System.Web.HttpContext.Current.Request.Files["FileAttachment"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);
                    var _comPath = AppDomain.CurrentDomain.BaseDirectory + fileName;
                    var imgname = Path.GetFileNameWithoutExtension(_comPath);
                    _imgname = "PROSPECT_UPLOAD";

                    var comPath = Server.MapPath(saveloc) + imgname + _ext;
                    imgname = imgname + _ext;
                    string fullname = _imgname;

                    var path = comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(comPath);
                    // Res = FnImportExcel(comPath, _ext);
                    DataTable dt = new DataTable();
                    dt = ClsCommon.FnConvertExceltoDatatable(comPath, _ext);
                    System.IO.File.Delete(comPath);

                    //string sDeletedoptions = "0";
                    try
                    {
                        string Token = "", ProspectName = "", ProspectType = "", Sector = "", Company = "", Country = "", KFA = "", State = "",Address=""
, Budget = "", C3Fit = "", C3FitScore = "", Responsible = "", Accountable = "", InformedTo = "", Consented = "", Website = "", OtherSupport = "", Comment = "", WebLink = "", ContactPerson = "", Designation = "", ContactNo = "", EmailId = "", LinkedInId = "", SecratoryName = "", SecratoryPhone = "", SecratoryEmail = "", SecratoryOtherInfo = "";
                        int IsLast = 0;
                        Token = ClsCommon.RandomString() + DateTime.Now.ToString();
                        long countuploaded = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (i + 1 == dt.Rows.Count)
                                {
                                    IsLast = 1;
                                }
                                try
                                {
                                    if (dt.Rows[i][0].ToString() != "")
                                    {
                                        //Basic
                                        ProspectName = Convert.ToString(dt.Rows[i][0]);
                                        ProspectType = Convert.ToString(dt.Rows[i][1]);
                                        Sector = Convert.ToString(dt.Rows[i][2]);
                                        Company = Convert.ToString(dt.Rows[i][3]);
                                        Country = Convert.ToString(dt.Rows[i][4]);
                                        KFA = Convert.ToString(dt.Rows[i][5]);
                                        State = Convert.ToString(dt.Rows[i][6]);
                                        Budget = Convert.ToString(dt.Rows[i][7]);
                                        C3Fit = Convert.ToString(dt.Rows[i][8]);
                                        C3FitScore = Convert.ToString(dt.Rows[i][9]);
                                        Responsible = Convert.ToString(dt.Rows[i][10]);
                                        Accountable = Convert.ToString(dt.Rows[i][11]);
                                        InformedTo = Convert.ToString(dt.Rows[i][12]);
                                        Consented = Convert.ToString(dt.Rows[i][13]);
                                        Website = Convert.ToString(dt.Rows[i][14]);
                                        OtherSupport = Convert.ToString(dt.Rows[i][15]);
                                        Comment = Convert.ToString(dt.Rows[i][16]);
                                        WebLink = Convert.ToString(dt.Rows[i][17]);
                                        ContactPerson = Convert.ToString(dt.Rows[i][18]);
                                        Designation = Convert.ToString(dt.Rows[i][19]);
                                        ContactNo = Convert.ToString(dt.Rows[i][20]);
                                        EmailId = Convert.ToString(dt.Rows[i][21]);
                                        LinkedInId = Convert.ToString(dt.Rows[i][22]);
                                        Address = Convert.ToString(dt.Rows[i][23]);
                                        SecratoryName = Convert.ToString(dt.Rows[i][24]);
                                        SecratoryPhone = Convert.ToString(dt.Rows[i][25]);
                                        SecratoryEmail = Convert.ToString(dt.Rows[i][26]);
                                        SecratoryOtherInfo = Convert.ToString(dt.Rows[i][27]);
                                        PostResult = Common_SPU.FnSetFrProspectImport(Token, ProspectName, ProspectType, Sector, Company, Country, KFA, State, Budget, C3Fit, C3FitScore, Responsible, Accountable, InformedTo, Consented, Website, OtherSupport, Comment, WebLink, ContactPerson, Designation, ContactNo, EmailId, LinkedInId,Address, SecratoryName, SecratoryPhone, SecratoryEmail, SecratoryOtherInfo, IsLast);
                                        if (PostResult.Status)
                                        {
                                            countuploaded = countuploaded + 1;

                                        }
                                    }



                                }
                                catch
                                {
                                }
                            }
                        }

                        if (countuploaded > 0)
                        {
                            PostResult.SuccessMessage = countuploaded.ToString() + " Records Uploaded Successfully";
                            PostResult.ID = countuploaded;
                        }
                        else
                        {
                            PostResult.SuccessMessage = "No Records Uploaded Successfully";
                            PostResult.ID = countuploaded;
                        }
                    }
                    catch (Exception ex)
                    {
                        PostResult.SuccessMessage = ex.Message.ToString(); ;
                        PostResult.ID = 0;
                    }
                    //  return Res;
                }
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LeadList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            FundRaising.Tabs Modal = new FundRaising.Tabs();
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "FR_STAGELEVEL";
            ViewBag.StageLevel = ClsCommon.GetDropDownList(getDropDown);
            Modal.Approve = "PENDING";

            return View(Modal);
        }
       
        //public ActionResult _LeadList(string Status,long StageLevelid)
        //{
        //    List<FundRaising.Lead.List> modal = new List<FundRaising.Lead.List>();
        //    modal = Fund.GetLeadList(0, Status, StageLevelid);
        //    //return PartialView(modal);
        //    return PartialView("~/Views/FundRaising/_LeadList.cshtml", modal);
        //}
        public ActionResult _LeadList(string src, ListTabs Modal, FormCollection Form)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
           long StageLevelid= Convert.ToInt64(Form.GetValue("StageLevelid").AttemptedValue);
            List<FundRaising.Lead.List> modal = new List<FundRaising.Lead.List>();
            getResponse.Approve = Modal.Approve;
            string status = Modal.Approve == 0 ? "PENDING" : "PROCESSED";
            modal = Fund.GetLeadList(0, status, StageLevelid);
            return PartialView(modal);
        }
        public ActionResult AddLead(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            FundRaising.Lead.Add Modal = new FundRaising.Lead.Add();
            getResponse.ID = ID;
            Modal = Fund.GetLead(getResponse);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult AddLead(string src, FundRaising.Lead.Add Modal, string Command)
        {
            ViewBag.TabIndex = 1;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            PostResult.SuccessMessage = "Action Can't Update";
            string NotDeleteIds = "0";
            long Leadid = 0;
            if (ModelState.IsValid)
            {
                Modal.Id = ID;
                PostResult = Fund.SetLead(Modal);
                Leadid = PostResult.ID;
                if (PostResult.Status)
                {
                    if (Modal.lstRefferals!=null)
                    {
                        foreach (var item in Modal.lstRefferals)
                        {
                            item.LeadId = Leadid;
                            PostResult= Fund.SetLeadReferrals(item);
                            NotDeleteIds = NotDeleteIds + "," + PostResult.ID.ToString();
                            
                        }
                        ClsCommon.FundRaisingDelete(Leadid, NotDeleteIds, "FR_LeadReferrals", "LeadId");
                    }

                    if (Modal.lstLeadActivity != null)
                    {
                        NotDeleteIds = "0";
                        foreach (var item in Modal.lstLeadActivity)
                        {
                            item.LeadId = Leadid;
                            PostResult = Fund.SetLeadActivity(item);
                            NotDeleteIds = NotDeleteIds + "," + PostResult.ID.ToString();
                        }
                        ClsCommon.FundRaisingDelete(Leadid, NotDeleteIds, "FR_LeadActivity", "LeadId");
                    }


                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/FundRaising/LeadList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/FundRaising/LeadList*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ReferralTask(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            FundRaising.Tabs Modal = new FundRaising.Tabs();
            Modal.Approve = "PENDING";
            return View(Modal);
        }
        public ActionResult _ReferralTask(string src, String status)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Status = status;
            List<FundRaising.RefferalsTask> modal = new List<FundRaising.RefferalsTask>();
          //  getResponse.Approve = Modal.Approve;
          // string status = Modal.Approve == 0 ? "PENDING" : "PROCESSED";
            modal = Fund.GetReferralTaskList(0, status);
            return PartialView(modal);
        }
        public ActionResult LeadReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            FundRaising.Reports Modal = new FundRaising.Reports();
            Modal.Doctype = "LEAD";
            return View(Modal);
        }
        public ActionResult EmployeeFilterReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            FundRaising.Reports Modal = new FundRaising.Reports();
            Modal.Doctype = "EmployeeFilter";
            return View(Modal);
        }
        public ActionResult ProspectMasterReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            FundRaising.Reports Modal = new FundRaising.Reports();
            Modal.Doctype = "PROSPECT";
            return View(Modal);
        }
        //[HttpPost]

        //public ActionResult _Reports(string src, FundRaising.Reports modal)
        //{  
        //    DataSet ds = new DataSet();
        //    ds = Common_SPU.fnGetReport_FR(modal.StartDate, modal.EndDate, modal.Id, modal.Doctype);           
        //    return PartialView(ds);
        //}
        public ActionResult _Reports(string src)
        {
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            string StartDate = GetQueryString[3];
            string EndDate = GetQueryString[4];
            string Doctype = GetQueryString[2];
            ViewBag.Doctype = Doctype;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetReport_FR(StartDate, EndDate, 0, Doctype);
            return PartialView(ds);
        }
        public ActionResult LeadDashboard(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            FundRaising.LeadDashboard modal = new FundRaising.LeadDashboard();
            modal = Fund.GetLeadDashboardList();
            return View(modal);
        }
     
    }
}