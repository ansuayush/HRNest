using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class SalaryStructureController : Controller
    {
        // GET: SalaryStructure
        IMasterHelper Master;
        ISalaryStructureHelper Salary;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        public SalaryStructureController()
        {
            getResponse = new GetResponse();
            Master = new MasterModal();
            Salary = new SalaryStructureModal();
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
        public ActionResult StepList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "SS_STEP";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        public ActionResult _StepAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "SS_STEP";
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
        public ActionResult ComponentGroupList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "SS_ComponentGroup";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        public ActionResult _ComponentGroupAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "SS_ComponentGroup";
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
        public ActionResult SubCategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "SS_Subcategory";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        public ActionResult _SubcategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "SS_Subcategory";
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
        public ActionResult ComponentList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "SS_Component";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        public ActionResult _ComponentAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "SS_Component";
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
            getDropDown.Doctype = "SS_ComponentGroup";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);

        }
        public ActionResult GradeMasterList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            getResponse.LoginID = LoginID;
            List<SalaryStructure.Grade.List> Modal = new List<SalaryStructure.Grade.List>();
            Modal = Salary.GetGradeList(getResponse);
            return View(Modal);
        }
        public ActionResult _GradeMasterAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            SalaryStructure.Grade.Add Modal = new SalaryStructure.Grade.Add();
            if (ID > 0)
            {
                getResponse.LoginID = LoginID;
                getResponse.ID = ID;
                Modal = Salary.GetGrade(getResponse);
            }
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "JOB TITLE";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult _GradeMasterAdd(string src, SalaryStructure.Grade.Add Modal, string Command)
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
                Modal.id = ID;
                Result = Salary.fnSetGrade(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EarningStructure(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<SalaryStructure.EarningStructure> Modal = new List<SalaryStructure.EarningStructure>();
            Modal = Salary.GetEarningStructureMaster("");
            return View(Modal);
        }
        public ActionResult _EarningStructureAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Category = GetQueryString[2];
            //long ID = 0;   
            string Category = GetQueryString[2];
            List<SalaryStructure.EarningStructureList> Modal = new List<SalaryStructure.EarningStructureList>();
            if (Category != "")
            {
                Modal = Salary.GetEarningStructureList(0, Category, "").Where(n => n.Doctype == "Live").ToList();
                ViewBag.Legacy = Salary.GetEarningStructureList(0, Category, "").Where(n => n.Doctype == "Legacy").ToList();
            }
            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult _EarningStructureAdd(string src, List<SalaryStructure.EarningStructureList> Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            //long ID = 0;
            decimal Percentage = 0, PercentageTotal = 0;
            Result.SuccessMessage = "Can't Update";
            foreach (var item in Modal)
            {
                Percentage = Convert.ToDecimal(string.IsNullOrEmpty(item.Percentage) ? "0" : item.Percentage);
                PercentageTotal = PercentageTotal + Percentage;
                if (Percentage > 0 && string.IsNullOrEmpty(item.StartMonth))
                {
                    ModelState.AddModelError("Id", "");
                    Result.SuccessMessage = "Enter Start Date in " + item.Component;
                }
            }
            if (PercentageTotal != 100)
            {
                ModelState.AddModelError("Id", "");
                Result.SuccessMessage = "Sum of Percentage should be 100%";
            }

            if (ModelState.IsValid)
            {
                for (int i = 0; i < Modal.Count; i++)
                {
                    SalaryStructure.EarningStructureList modallst = new SalaryStructure.EarningStructureList();
                    modallst = Modal[i];
                    modallst.LoginID = LoginID;
                    Result = Salary.fnSetEarningStructure(modallst);
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BenefitStructureList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            getResponse.LoginID = LoginID;
            List<SalaryStructure.BenefitStructureList> Modal = new List<SalaryStructure.BenefitStructureList>();
            Modal = Salary.GetBenefitStructureList(getResponse);
            return View(Modal);
        }
        public ActionResult _BenefitStructureAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            SalaryStructure.BenefitStructureAdd Modal = new SalaryStructure.BenefitStructureAdd();
            getResponse.LoginID = LoginID;
            getResponse.AdditionalID = ID;
            Modal = Salary.GetBenefitStructureAdd(getResponse);

            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "SS_FORMULA";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult _BenefitStructureAdd(string src, SalaryStructure.BenefitStructureAdd Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "Can't Update";
            int BenefitCount = 0;
            foreach (var item in Modal.LstDet)
            {
                if (item.isactive == 1)
                {
                    BenefitCount = BenefitCount + 1;
                    if (item.CalculationType.ToString() == "")
                    {
                        ModelState.AddModelError("Id", "");
                        Result.SuccessMessage = "Select Formula/Amount for Benefit  " + item.Benefit;
                    }
                    else if (item.CalculationType.ToString() == "Formula" && item.Formulaid == 0)
                    {
                        ModelState.AddModelError("Id", "");
                        Result.SuccessMessage = "Select Formula for Benefit  " + item.Benefit;
                    }
                    else if (item.CalculationType.ToString() == "Amount" && string.IsNullOrEmpty(item.Amount.ToString()))
                    {
                        ModelState.AddModelError("Id", "");
                        Result.SuccessMessage = "Enter Amount for Benefit  " + item.Benefit;
                    }
                }
            }
            if (BenefitCount == 0)
            {
                ModelState.AddModelError("Id", "");
                Result.SuccessMessage = "Choose atleast one Benefit";
            }

            if (ModelState.IsValid)
            {
                Modal.LoginID = Convert.ToInt32(LoginID);
                Modal.IPAddress = IPAddress;
                Modal.id = ID;
                Result = Salary.fnSetBenefitStructure(Modal);
                if (Result.Status && Command == "AddNew")
                {
                    Result.OtherID = 1;
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmployeeSalaryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ListTabs Modal = new ListTabs();
            Modal.Approve = 0;
            return View(Modal);
        }
        //[OutputCache(Duration = 100)]
        public ActionResult _EmployeeSalaryList(string src, ListTabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<SalaryStructure.EmployeeSalary.List> result = new List<SalaryStructure.EmployeeSalary.List>();
            getResponse.Approve = Modal.Approve;
            ViewBag.Approve = Modal.Approve;
            result = Salary.GetEmployeeSalaryList(getResponse);
            return PartialView(result);
        }
        public ActionResult AddEmployeeSalary(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.Pageid = GetQueryString[4];
            int EMPID = 0, Pageid = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.Pageid, out Pageid);
            SalaryStructure.EmployeeSalary.Add Modal = new SalaryStructure.EmployeeSalary.Add();
            getResponse.ID = 0;
            getResponse.AdditionalID = EMPID;
            getResponse.AdditionalID1 = Pageid;

            Modal = Salary.GetEmployeeSalaryAdd(getResponse);
            Modal.OldEffectiveDate = Modal.EffectiveDate;
            return View(Modal);
        }
        public ActionResult _SalaryStructure(string EmpSalaryid, string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string Empid = GetQueryString[2];
            string AnnualSalary = GetQueryString[3];
            string Structureid = GetQueryString[4];
            string Days = GetQueryString[5];
            string Hours = GetQueryString[6];
            SalaryStructure.EmployeeSalary.Add Modal = new SalaryStructure.EmployeeSalary.Add();
            List<SalaryStructure.EmployeeSalary.StructureList> result = new List<SalaryStructure.EmployeeSalary.StructureList>();
            SalaryStructure.GetStructureListResponse Res = new SalaryStructure.GetStructureListResponse();
            Modal.AnnualSalary = Convert.ToDecimal(AnnualSalary);
            Modal.Empid = Convert.ToInt64(Empid);
            Modal.Structureid = Convert.ToInt64(Structureid);
            Modal.Days = Days;
            Modal.WorkingHours = string.IsNullOrEmpty(Hours) ? 0 : Convert.ToDecimal(Hours);
            Res.AnnualSalary = AnnualSalary;
            Res.Empid = Convert.ToInt64(Empid);
            Res.EmployeeSalaryid = 0;
            Res.EmployeeSalaryid = Convert.ToInt64(EmpSalaryid);
            Res.Structureid = Convert.ToInt64(Structureid);
            Modal.lstSalryStructure = Salary.GetEmployeeSalaryStructureList(Res);
            
            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult AddEmployeeSalary(string src, SalaryStructure.EmployeeSalary.Add Modal, string Command, FormCollection Form)
        {
            PostResponse Result = new PostResponse();
            string _Path = clsApplicationSetting.GetPhysicalPath();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long Structureid = Convert.ToInt64(string.IsNullOrEmpty(Form.GetValue("hfStructureid").AttemptedValue.ToString()) ? "0" : Form.GetValue("hfStructureid").AttemptedValue.ToString());
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "Can't Update";
            if (Command == "Draft")
            {
                Modal.IsDraft = 1;
            }
            else
            {
                Modal.IsDraft = 0;
            }
            if (Structureid != Modal.Structureid)
            {
                ModelState.AddModelError("Id", "");
                Result.SuccessMessage = "Please Process the Salary Structure";
            }

            DateTime todaysDate = Convert.ToDateTime(Modal.EffectiveDate);
            int month = todaysDate.Month;
            int year = todaysDate.Year;
            string SQL = "select top (1) *  from timesheet_log where isdeleted='0' and emp_id='"+Modal.Empid+"' order by Id desc";
            DataTable dt = clsDataBaseHelper.ExecuteDataTable(SQL, "A");
            int chkmonth=0;
            int chkyear = 0;
            foreach (DataRow item in dt.Rows)
            {

                chkmonth = Convert.ToInt32(item["month"]);
                chkyear = Convert.ToInt32(item["year"]);

            }
            //if ((year== chkyear ) && ( chkmonth > month || chkmonth == month))
            //{
            //    ModelState.AddModelError("Id", "");
            //    Result.SuccessMessage = "Please change effective date";
            //}
            //if (Convert.ToDateTime(Modal.OldEffectiveDate)> Convert.ToDateTime(Modal.EffectiveDate))
            //{
            //    ModelState.AddModelError("Id", "");
            //    Result.SuccessMessage = "Please change effective date";
            //}
            if (ModelState.IsValid)
            {
                Modal.LoginID = Convert.ToInt32(LoginID);
                Result = Salary.fnSetEmployeeSalary(Modal);
                if (Modal.lstAttachment != null)
                {
                    //int count = 0;
                    string AttachmentIdsDelete = "0";
                    foreach (var item in Modal.lstAttachment)
                    {
                        long AttachmentID = 0;
                        if (item.UploadFile != null)
                        {
                            var RvFile = clsApplicationSetting.ValidateFile(item.UploadFile);
                            if (RvFile.IsValid)
                            {
                                AttachmentID = Common_SPU.fnSetAttachments(item.AttachmentID, RvFile.FileName, RvFile.FileExt, item.Description, Result.ID.ToString(), "SS_EmpSalary");
                                item.UploadFile.SaveAs(Path.Combine(_Path, AttachmentID + RvFile.FileExt));
                                AttachmentIdsDelete = AttachmentIdsDelete + "," + AttachmentID.ToString();
                            }

                        }
                        else
                        {
                            AttachmentIdsDelete = AttachmentIdsDelete + "," + item.AttachmentID.ToString();
                        }



                    }
                    ClsCommon.fnSetDataString("update master_attachment set isdeleted=1,deletedat=current_timestamp,deletedby=" + Modal.LoginID.ToString() + " where TableName='SS_EmpSalary' and TableID=" + Result.ID.ToString() + " and  id not in (" + AttachmentIdsDelete + ") "); ;
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/SalaryStructure/EmployeeSalaryList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/SalaryStructure/EmployeeSalaryList*" + Result.ID);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _SalaryStructureDetail(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string Id = GetQueryString[2];
            SalaryStructure.EmployeeSalary.Detail Modal = new SalaryStructure.EmployeeSalary.Detail();
            getResponse.ID = Convert.ToInt64(Id);
            getResponse.AdditionalID = 0;
            Modal = Salary.GetEmployeeSalaryDetail(getResponse);
            return PartialView(Modal);
        }
        public ActionResult DeductionEntry(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            SalaryStructure.Tabs Modal = new SalaryStructure.Tabs();
            Modal.Approve = "MITR";
            Modal.Date = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _DeductionEntry(string src, string Emptype, string Date)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Status = Emptype;
            SalaryStructure.DeductionEntry modal = new SalaryStructure.DeductionEntry();
            modal = Salary.GetDeductionEntry(Emptype, Date);
            return PartialView(modal);
        }
        [HttpPost]
        public ActionResult _DeductionEntry(string src, SalaryStructure.DeductionEntry Modal, string Command, FormCollection Collection)
        {
            ViewBag.TabIndex = 1;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            DateTime SelectedMonth = Convert.ToDateTime(Convert.ToDateTime(Modal.Month).ToString("dd-MM-yyyy"));
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            string msg = "Action Can't Update";
            PostResult.SuccessMessage = msg;
            if (!Modal.Emplist.Any(x => x.Checkbox != null))
            {
                msg = "Please Choose Employee.";
                PostResult.SuccessMessage = msg;
                ModelState.AddModelError("Month", msg);
            }

            var ComponentList = Modal.Component.GroupBy(x => x.Id)
                .Select(x => new
                {
                    Id = x.Select(ex => ex.Id).FirstOrDefault(),
                    Component = x.Select(ex => ex.Component).FirstOrDefault()
                })
                .ToList();
            string Empid = "0"; string Componentid = "0"; string ComponentStr = ""; string Qry = ""; string Amount = "0";
            if (ModelState.IsValid)
            {
                foreach (var item in Modal.Emplist)
                {
                    if (item.Checkbox != null)
                    {
                        Empid = item.Empid.ToString();
                        Qry = "";
                        foreach (var itemCom in ComponentList)
                        {
                            Componentid = itemCom.Id.ToString();
                            ComponentStr = Empid + "-" + Componentid;
                            Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                            Qry = Qry + (Qry != "" ? "," : "") + Componentid + "#" + Amount;
                        }
                        PostResult = Salary.fnSetDeductionEntry(SelectedMonth.Month, Convert.ToDateTime(Modal.Month).Year, Convert.ToInt64(Empid), Qry);
                    }

                }
            }

            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/SalaryStructure/DeductionEntry?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/SalaryStructure/DeductionEntry*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

      
        public ActionResult OtherEarningEntry(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            SalaryStructure.Tabs Modal = new SalaryStructure.Tabs();
            Modal.Approve = "MITR";
            Modal.Date = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _OtherEarningEntry(string src, string Emptype, string Date)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Status = Emptype;
            SalaryStructure.OtherEarningEntry modal = new SalaryStructure.OtherEarningEntry();
            modal = Salary.GetOtherEarningEntry(Emptype, Date);
            return PartialView(modal);
        }



        [HttpPost]
        public ActionResult _OtherEarningEntry(string src, SalaryStructure.OtherEarningEntry Modal, string Command, FormCollection Collection)
        {
            ViewBag.TabIndex = 1;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            DateTime SelectedMonth = Convert.ToDateTime(Convert.ToDateTime(Modal.Month).ToString("dd-MM-yyyy"));
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            string msg = "Action Can't Update";
            PostResult.SuccessMessage = msg;
            if (!Modal.Emplist.Any(x => x.Checkbox != null))
            {
                msg = "Please Choose Employee.";
                PostResult.SuccessMessage = msg;
                ModelState.AddModelError("Month", msg);
            }

            var ComponentList = Modal.Component.GroupBy(x => x.Id)
                .Select(x => new
                {
                    Id = x.Select(ex => ex.Id).FirstOrDefault(),
                    Component = x.Select(ex => ex.Component).FirstOrDefault()
                })
                .ToList();
            string Empid = "0"; string Componentid = "0"; string ComponentStr = ""; string Qry = ""; string Amount = "0";
            if (ModelState.IsValid)
            {

                foreach (var item in Modal.Emplist)
                {
                    if (item.Checkbox != null)
                    {
                        Empid = item.Empid.ToString();
                        Qry = "";
                        foreach (var itemCom in ComponentList)
                        {
                            Componentid = itemCom.Id.ToString();
                            ComponentStr = Empid + "-" + Componentid;
                            Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                            Qry = Qry + (Qry != "" ? "," : "") + Componentid + "#" + Amount;
                        }
                        PostResult = Salary.fnSetOtherEarningEntry(SelectedMonth.Month, Convert.ToDateTime(Modal.Month).Year, Convert.ToInt64(Empid), Qry);
                    }

                }
            }

            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/SalaryStructure/OtherEarningEntry?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/SalaryStructure/OtherEarningEntry*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
    }
}