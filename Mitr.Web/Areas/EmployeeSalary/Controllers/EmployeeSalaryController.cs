using Mitr.BLL;
using Mitr.DAL;
using Mitr.Interface;
using Mitr.Model;
using Mitr.Model.EmployeeSalary;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Mitr.Areas.EmployeeSalary.Controllers
{
    [RouteArea("")]
    public class EmployeeSalaryController : Controller
    {
        IEmployeeSalary _objIEmployeeSalary;

        long LoginID = 0, EmpID = 0;
        public EmployeeSalaryController()
        {
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long.TryParse(clsApplicationSetting.GetSessionValue("EmpID"), out EmpID);
            _objIEmployeeSalary = new EmployeeSalary_BAL();
        }

        #region "Bonus"
        public ActionResult BonusPaymentList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Bonus Modal = new Bonus();
            List<Bonus.FinList> FList = new List<Bonus.FinList>();
            FList = _objIEmployeeSalary.GetFinYearList();
            ViewBag.FinYearList = FList;
            //Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();

            return View(Modal);
        }
        [HttpPost]
        public ActionResult _BonusPaymentList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = GetQueryString[2];
            ViewBag.Component = GetQueryString[3];
            ViewBag.PaidDate = GetQueryString[4];
            long FYID;
            long.TryParse(ViewBag.FYID, out FYID);

            string Component = Convert.ToString(GetQueryString[3]);
            string PaidDate = Convert.ToString(GetQueryString[4]);
            List<Bonus> Modal = new List<Bonus>();
            Modal = _objIEmployeeSalary.GetBonusPaymentList(FYID, Component, PaidDate);
            return PartialView(Modal);
        }
        public ActionResult _BonusPaymentEntry(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EmpID = GetQueryString[2];
            ViewBag.EmpName = GetQueryString[3];
            ViewBag.Component = GetQueryString[4];
            string Paid_Date = Convert.ToString(GetQueryString[5]);
            BonusPaymentEntry Modal = new BonusPaymentEntry();
            Modal.Component = Convert.ToString(GetQueryString[4]);
            Modal.DocNo = _objIEmployeeSalary.GetBonusPaymentDocNo();
            if (!string.IsNullOrEmpty(Paid_Date))
            {
                Modal.PaidDate = Paid_Date;
            }
            else
            {
                Modal.PaidDate = DateTime.Now.ToString("yyyy-MM-dd");
                Modal.PaidDate = (string.Compare(Modal.PaidDate, "1900-01-01") == 0) ? Modal.PaidDate = string.Empty : Modal.PaidDate;
            }
            
           // ViewBag.ProjectMappedList = _objIEmployeeSalary.GetSelectedProjectsList("Map");
            ViewBag.ProjectMappedList = _objIEmployeeSalary.GetSelectedProjectsBonusList();
            Modal.ProjectPaaymentAmountList = _objIEmployeeSalary.AddMultipleProjectPaaymentAmountList();
            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult DeleteBonusPaidEntry(string src, BonusPaymentEntry Modal)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            ViewBag.EmpID = GetQueryString[3];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            //long.TryParse(ViewBag.EmpID, out EmpID);
            if (!ModelState.IsValid)
            {
                Modal.ID = ID;
                Modal.EmpID = EmpID;
                Modal.IsDeleted = 1;
                Modal.Createdby = LoginID;
                Result = _objIEmployeeSalary.fnSetBonusPaymentEntry(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _BonusPaymentDetails(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EmpID = GetQueryString[2];
            ViewBag.EmpName = GetQueryString[3];
            //ViewBag.ID = GetQueryString[4];
            long ID = 0, EmpID = 0;
            //long.TryParse(ViewBag.ID, out ID);
            long.TryParse(ViewBag.EmpID, out EmpID);

            string Component = Convert.ToString(GetQueryString[4]);
            BonusPaymentEntry Modal = new BonusPaymentEntry();
            List<BonusPaymentEntry> BonusPaymentEntryList = new List<BonusPaymentEntry>();
            Modal.ID = ID;
            Modal.EmpID = EmpID;
            Modal.BonusPaymentEntryList = _objIEmployeeSalary.GetBonusPaymentEntryList(EmpID, Component);

            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult BonusPaymentEntry(string src, BonusPaymentEntry Modal)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EmpID = GetQueryString[2];
            long EmpID = 0;
            long.TryParse(ViewBag.EmpID, out EmpID);

            Result.SuccessMessage = "Bonus Payment Entry Can't Update";
            if (!ModelState.IsValid)
            {
                Modal.Createdby = LoginID;
                Modal.EmpID = EmpID;
                foreach (BonusPaymentEntry _modal in Modal.ProjectPaaymentAmountList)
                {
                    Modal.ProjectID = _modal.ProjectID;
                    Modal.PaidAmount = _modal.PaidAmount;
                    if (_modal.PaidAmount <= 0)
                    {
                        Result.SuccessMessage = "Paid amount must be greater than zero.";
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }

                    Result = _objIEmployeeSalary.fnSetBonusPaymentEntry(Modal);
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Other Benefit Payment"

        public ActionResult OtherBenefitPayment(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            OtherBenefitPayment Modal = new OtherBenefitPayment();
            Modal.PaidDate = DateTime.Now.ToString("yyyy-MM");
            List<Bonus.FinList> FList = new List<Bonus.FinList>();
            FList = _objIEmployeeSalary.GetFinYearList();
            ViewBag.FinYearList = FList;
            GetResponseModel employee = new GetResponseModel();
            employee.Doctype = "GR_Employee";
            employee.LoginID = LoginID;
            Modal.EmployeeList = ClsCommon.GetDropDownList(employee);
            Modal.EmpID = EmpID;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _OtherBenefitPayment(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = GetQueryString[2];
            ViewBag.empID = GetQueryString[3];
            ViewBag.PaidDate = GetQueryString[4];
            long FYID, Employeeid;
            long.TryParse(ViewBag.FYID, out FYID);
            long.TryParse(ViewBag.empID, out Employeeid);

            EmpID = (Employeeid > EmpID) ? Employeeid : EmpID;

            string PaidDate = Convert.ToString(Convert.ToDateTime(GetQueryString[4]));
            OtherBenefitPayment Modal = new OtherBenefitPayment();

            List<OtherBenefitPayment> List = new List<OtherBenefitPayment>();
            List<OtherBenefitPayment.OtherBenefitPaymentComponent> ComponentList = new List<OtherBenefitPayment.OtherBenefitPaymentComponent>();
            Modal.PaidDate = PaidDate;

            List = _objIEmployeeSalary.GetOtherBenefitPaymentList(FYID, EmpID, PaidDate);
            ComponentList = _objIEmployeeSalary.GetOtherBenefitPaymentComponentList(FYID, EmpID, PaidDate);
            Modal.OtherBenefitPaymentList = List;
            Modal.OtherBenefitPaymentComponentList = ComponentList;
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult OtherBenefitPayment(OtherBenefitPayment Modal, FormCollection form)
        {
            PostResponseModel Result = new PostResponseModel();
            Result.SuccessMessage = "Other Benefit Payment Entry Can't Update";

            if (Convert.ToDateTime(Modal.PaidDate) >= Convert.ToDateTime(Modal.OtherBenefitPaymentList[0].fromDate) && Convert.ToDateTime(Modal.PaidDate) >= Convert.ToDateTime(Modal.OtherBenefitPaymentList[0].Todate)
             || Convert.ToDateTime(Modal.PaidDate) <= Convert.ToDateTime(Modal.OtherBenefitPaymentList[0].fromDate) && Convert.ToDateTime(Modal.PaidDate) <= Convert.ToDateTime(Modal.OtherBenefitPaymentList[0].Todate))
            {
                Result.SuccessMessage = "Please select correct month in selected financial year.";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }

            if (ModelState.IsValid)
            {
                foreach (OtherBenefitPayment modal in Modal.OtherBenefitPaymentList)
                {
                    foreach (OtherBenefitPayment.OtherBenefitPaymentComponent _componentmodal in Modal.OtherBenefitPaymentComponentList)
                    {
                        if (modal.EmpID == _componentmodal.EmpID)
                        {
                            OtherBenefitPayment.OtherBenefitPaymentComponent _modal = new OtherBenefitPayment.OtherBenefitPaymentComponent();

                            _modal.EmpID = modal.EmpID;
                            _modal.ComponentID = _componentmodal.ComponentID;
                            _modal.PaidDate = Modal.PaidDate;
                            _modal.PaidAmount = _componentmodal.PaidAmount;
                            _modal.Createdby = LoginID;
                            _modal.IsDeleted = 0;
                            Result = _objIEmployeeSalary.fnSetOtherBenefitPayment(_modal);
                        }
                    }
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Special Allowance"
        public ActionResult SpecialAllowanceList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            SpecialAllowance Modal = new SpecialAllowance();
            List<SpecialAllowance.FinList> FList = new List<SpecialAllowance.FinList>();
            FList = _objIEmployeeSalary.GetFinancialYearList();
            ViewBag.FinYearList = FList;
            Modal.FinancialYearList = FList;
            if (TempData.ContainsKey("FYId"))
            {
                if (Convert.ToInt32(TempData["FYId"].ToString()) >0)
                {
                    Modal.FYID = Convert.ToInt32(TempData["FYId"].ToString());
                }

            }
            if (TempData.ContainsKey("PaidDate"))
            {
                if (TempData["PaidDate"].ToString() != "")
                {
                    Modal.PaidDate = TempData["PaidDate"].ToString();
                }

            }
            //Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _SpecialAllowanceList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = GetQueryString[2];
            ViewBag.Component = GetQueryString[3];
            ViewBag.PaidDate = GetQueryString[4];
            long FYID;
            long.TryParse(ViewBag.FYID, out FYID);

            string Component = Convert.ToString(GetQueryString[3]);
            string PaidDate = Convert.ToString(GetQueryString[4]);
            List<SpecialAllowance> Modal = new List<SpecialAllowance>();
            Modal = _objIEmployeeSalary.GetSpecialAllowanceList(FYID, Component, PaidDate);
            TempData["FYId"] =FYID;

            return PartialView(Modal);
        }
        public ActionResult _SpecialAllowanceEntry(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EmpID = GetQueryString[2];
            ViewBag.EmpName = GetQueryString[3];
            ViewBag.Component = GetQueryString[4];
            string Paid_Date = Convert.ToString(GetQueryString[5]);

            SpecialAllowanceEntry Modal = new SpecialAllowanceEntry();
            Modal.Component = Convert.ToString(GetQueryString[4]);
            Modal.DocNo = _objIEmployeeSalary.GetSpecialAllowanceDocNo();
            //PostResponseModel PostResult = new PostResponseModel();
            //PostResponseModel PostResult = new PostResponseModel();
            //if (Modal.Component=="0")
            //{
            //    PostResult.SuccessMessage = "Invalid Button Click.";
            //    return PartialView(Modal);
            //return Json(PostResult, JsonRequestBehavior.AllowGet);
            //}

            if (!string.IsNullOrEmpty(Paid_Date))
            {
                Modal.PaidDate = Paid_Date;
                TempData["PaidDate"] = Modal.PaidDate;
            }
            else
            {
                Modal.PaidDate = DateTime.Now.ToString("yyyy-MM-dd");
                Modal.PaidDate = (string.Compare(Modal.PaidDate, "1900-01-01") == 0) ? Modal.PaidDate = string.Empty : Modal.PaidDate;
            }
            //ViewBag.ProjectMappedList = _objIEmployeeSalary.GetSelectedProjectsList("Map");
            ViewBag.ProjectMappedList = _objIEmployeeSalary.GetSelectedProjectsBonusList();
            Modal.ProjectPaymentAmountList = _objIEmployeeSalary.AddMultipleProjectPaymentAmountList();
            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult DeleteSpecialAllowancePayment(string src, SpecialAllowanceEntry Modal)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            ViewBag.EmpID = GetQueryString[3];
            long ID = 0, EmpID = 0;
            long.TryParse(ViewBag.ID, out ID);
            long.TryParse(ViewBag.EmpID, out EmpID);
            if (!ModelState.IsValid)
            {
                Modal.ID = ID;
                Modal.EmpID = EmpID;
                Modal.IsDeleted = 1;
                Modal.Createdby = LoginID;
                Result = _objIEmployeeSalary.fnSetSpecialAllowancePaymentEntry(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _ViewSpecialAllowance(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EmpID = GetQueryString[2];
            ViewBag.EmpName = GetQueryString[3];
            //ViewBag.ID = GetQueryString[4];
            long ID = 0, EmpID = 0;
            //long.TryParse(ViewBag.ID, out ID);
            long.TryParse(ViewBag.EmpID, out EmpID);
            string Component = Convert.ToString(GetQueryString[4]);
            SpecialAllowanceEntry Modal = new SpecialAllowanceEntry();
            //List<SpecialAllowanceEntry> SpecialAllowanceEntryList = new List<SpecialAllowanceEntry>();
            Modal.ID = ID;
            Modal.EmpID = EmpID;
            Modal.SpecialAllowanceEntryList = _objIEmployeeSalary.GetSpecialAllowanceEntryList(EmpID, Component);

            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult SpecialAllowanceEntry(string src, SpecialAllowanceEntry Modal)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EmpID = GetQueryString[2];
            long EmpID = 0;
            long.TryParse(ViewBag.EmpID, out EmpID);

            Result.SuccessMessage = "Special Allowance Payment Entry Can't Update";
            if (!ModelState.IsValid)
            {
                Modal.Createdby = LoginID;
                Modal.EmpID = EmpID;
                foreach (SpecialAllowanceEntry _modal in Modal.ProjectPaymentAmountList)
                {
                    Modal.ProjectID = _modal.ProjectID;
                    Modal.PaidAmount = _modal.PaidAmount;
                    if (_modal.PaidAmount <= 0)
                    {
                        Result.SuccessMessage = "Paid amount must be greater than zero.";
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                    Result = _objIEmployeeSalary.fnSetSpecialAllowancePaymentEntry(Modal);
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SpecialAllowanceCalculate(string src)
        {
            PostResponseModel Result = new PostResponseModel();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FYID = GetQueryString[2];
            long FYID = 0;
            long.TryParse(ViewBag.FYID, out FYID);
            Result.SuccessMessage = "Special Allowance Calculate Can't Update";
            Result = _objIEmployeeSalary.fnSetSpecialAllowanceCalculate(FYID);
            return Json(Result);
            //return RedirectToAction("SpecialAllowanceList", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/EmployeeSalary/SpecialAllowanceList") });
        }

        #endregion
    }
}
