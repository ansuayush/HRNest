using Mitr.BLL;
using Mitr.CommonClass;
using Mitr.Interface;
using Mitr.Model;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class PersonnelController : Controller
    {
        IPersonnelHelper Personnel;
        ISalaryStructureHelper Salary;
        IEmployeeSalary _objIEmployeeSalary;
        IEmployeeHelper employee;
        long LoginID = 0;
        GetResponse getResponse;
        public PersonnelController()
        {
            getResponse = new GetResponse();
            Personnel = new PersonnelModal();
            Salary = new SalaryStructureModal();
            _objIEmployeeSalary = new EmployeeSalary_BAL();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            employee = new EmployeeModal();
        }

       [AuthorizeFilter(ActionFor = "OTP")]
        public ActionResult EssPortal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet Modal = new DataSet();
            Modal = Common_SPU.ESSMenuDetails();
            return View(Modal);
        }

        public ActionResult ESSPDF(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Doctype = GetQueryString[2];
            DataSet Modal = new DataSet();
            Modal = Common_SPU.GetEss(ViewBag.Doctype);
            return View(Modal);
        }

        public ActionResult LeaveAvailed(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet Modal = new DataSet();
            Modal = Personnel.GetLeaveType();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _LeaveAvailed(FormCollection Collection, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string ddLeaveType = "";
            LeaveAvailedReport modal = new LeaveAvailedReport();
            if (Command == "Search")
            {
                 ddLeaveType = Collection.GetValue("ddLeaveType").AttemptedValue;
                 modal = Personnel.GetLeaveAvailedReport(ddLeaveType);
            }
            return PartialView(modal);
        }

        public ActionResult SalaryDetails(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            SalaryDetails Modal = new SalaryDetails();
            Modal.FinyearList = Personnel.GetFinYearList();
            return View(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _SalaryDetails(string src, SalaryDetails Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return PartialView();
        }
        public ActionResult EmpHistory(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TempDateModal Modal = new TempDateModal();
            return View(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _EmpHistory(string src, TempDateModal Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return PartialView();
        }

        public ActionResult ViewEmpAttachment(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet Modal = new DataSet();
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            Modal = Common_SPU.fnGetEmpAttachment_Ess(EMPID);
            return View(Modal);

        }
        public ActionResult CompOffReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TempDateModal Modal = new TempDateModal();
            Modal.StartDate = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd");
            Modal.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _CompOffReport(TempDateModal Modal, string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet ds = Common_SPU.fnGetCompensatoryOff_Ess(Modal.StartDate, Modal.EndDate);


            return PartialView(ds);
        }
        public ActionResult SalarySlip(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime dt = Convert.ToDateTime(GetQueryString[2]);
            string Emptype = GetQueryString[3];
            DataSet Modal = new DataSet();
            Modal = Common_SPU.GetSalarySlip(dt, Emptype, "SALARYSLIP");
            return View(Modal);
        }
        public ActionResult SalaryRegister(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime dt = Convert.ToDateTime(GetQueryString[2]);
            DataSet Modal = new DataSet();
            Modal = Common_SPU.GetSalarySlip(dt, "", "SALARYREGISTER");
            return View(Modal);
        }
        //For Dump Purpose Only
        //public ActionResult FNFDetail(string src)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    DateTime dt = DateTime.Now;
        //    DataSet Modal = new DataSet();
        //    Modal = Common_SPU.GetFNFDetail(dt, "", "FNF");
        //    return View(Modal);
        //}

        public ActionResult AnualSallaryDetail(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "EmployeeAnualReport";
            ViewBag.Employee = ClsCommon.GetDropDownList(getDropDown);
            ViewBag.FinYear = Personnel.GetFinYearListData();
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            DeclarationReport modal = new DeclarationReport();
            modal.EId = EMPID;
            return View(modal);
        }
        public ActionResult AnualReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            FNFReport modal = new FNFReport();
            modal = Personnel.GetFandFDetails(Convert.ToInt64(GetQueryString[2]), Convert.ToInt64(GetQueryString[3]));
            if (modal != null)
            {
                modal.Id = Convert.ToInt64(GetQueryString[2]);
                modal.FId = Convert.ToInt64(GetQueryString[3]);
            }
            else
            {
                // modal.Id = 0;
            }

            return View(modal);
        }
        public ActionResult FNFDetail(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "EmployeeF&F";
            ViewBag.Employee = ClsCommon.GetDropDownList(getDropDown);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            DeclarationReport modal = new DeclarationReport();
            modal.FYId = EMPID;
            return View(modal);
        }
        public ActionResult FNFReport(string src)
        {
            ViewBag.src = src;
            long EMPId = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            FNFReport modal = new FNFReport();
            EMPId = Convert.ToInt64(GetQueryString[2]);
            if (EMPId > 0)
            {
                modal = Personnel.GetFandFDetails(Convert.ToInt64(GetQueryString[2]), 0);
            }
        
            if (modal != null)
            {
                modal.Id = Convert.ToInt64(GetQueryString[2]);
            }
            else
            {
                modal.Id = 0;
            }

            return View(modal);
        }
        public ActionResult SalarySlipActivation(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now.AddMonths(-1);
            SalarySlipActivation Modal = new SalarySlipActivation();
            Modal.Date = MyDate.ToString("yyyy-MM");
            Modal.MitrUser = 0;
            return View(Modal);
        }
        public ActionResult _SalarySlipActivation(string src, string Emptype, DateTime Dt, string Status)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Status = Emptype;
            SalarySlipActivationEntry modal = new SalarySlipActivationEntry();
            modal = Personnel.GetSalarySlipActivationEntry(Dt.Month, Dt.Year, Emptype, "SALARYSLIP");
            if (Status != "")
            {
                modal.Emplist = modal.Emplist.Where(n => n.IsActive == Status).ToList();
            }
            return PartialView(modal);
        }
        [HttpPost]
        public ActionResult _SalarySlipActivation(string src, SalarySlipActivationEntry Modal, string Command, FormCollection Collection)
        {
            ViewBag.TabIndex = 1;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            DateTime SelectedMonth = Convert.ToDateTime(Convert.ToDateTime(Modal.Month).ToString("dd-MM-yyyy"));
            //string Emptype = Collection.GetValue("Emptype").AttemptedValue;
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            string msg = "Action Can't Update";
            PostResult.SuccessMessage = msg;
            if (!Modal.Emplist.Any(x => x.Checkbox != "0"))
            {
                msg = "Please Choose Employee.";
                PostResult.SuccessMessage = msg;
                ModelState.AddModelError("Month", msg);
            }
            int IsActive = Command == "Activate" ? 1 : 0;

            string Empid = "0";
            if (ModelState.IsValid)
            {
                foreach (var item in Modal.Emplist.Where(n => n.Checkbox != "0"))
                {
                    Empid = item.Empid.ToString();
                    PostResult = Personnel.fnSetSalarySlipActivationEntry(SelectedMonth.Month, Convert.ToDateTime(Modal.Month).Year, Convert.ToInt64(Empid), IsActive, "SALARYSLIP");
                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Personnel/SalarySlipActivation?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Personnel/SalarySlipActivation");
            }
            //PostResult.AdditionalMessage = Emptype;
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReimbursementBooking(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Personnels.ReimbursementBooking.FYList> Modal = new List<Personnels.ReimbursementBooking.FYList>();
            Modal = Personnel.GetReimbursementBooking(0);
            return View(Modal);
        }

        public ActionResult ReimbursementHead(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int Id = 0;
            int.TryParse(ViewBag.ID, out Id);
            Personnels.ReimbursementBooking.ReimbursementHead Modal = new Personnels.ReimbursementBooking.ReimbursementHead();
            Modal = Personnel.GetReimbursementHead(Id);
            return View(Modal);
        }

        public ActionResult ReimbLoadVoucher(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string[] newstring = GetQueryString[2].Split(',');
            ViewBag.ID = newstring[1];
            int.TryParse(newstring[0], out int FYId);
            int HeadId = 0;
            int.TryParse(ViewBag.ID, out HeadId);
            //Personnels.ReimbursementBooking.ClaimVoucher Modal = new Personnels.ReimbursementBooking.ClaimVoucher();
            //Modal = Personnel.GetClaimVoucher(fyid, Id);
            Personnels.ReimbursementBooking.ReimbursementHead Modal = new Personnels.ReimbursementBooking.ReimbursementHead();
            Modal = Personnel.GetReimbursementHead(FYId, HeadId);
            return View(Modal);
        }

        public ActionResult ReimbAddVoucher(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string[] newstring = GetQueryString[2].Split(',');
            ViewBag.ID = newstring[2];
            int.TryParse(newstring[0], out int FYId);
            int.TryParse(newstring[1], out int HeadId);
            int.TryParse(ViewBag.ID, out int ClaimId);
            Personnels.ReimbursementBooking.ClaimVoucher Modal = new Personnels.ReimbursementBooking.ClaimVoucher();
            Modal = Personnel.GetClaimVoucher(FYId, HeadId, ClaimId);
            return PartialView(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReimbAddVoucher(string src, Personnels.ReimbursementBooking.ClaimVoucher obj, string Command)
        {
            PostResponse PostResult = new PostResponse();
            try
            {
                ViewBag.src = src;
                string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                string[] newstring = GetQueryString[2].Split(',');
                ViewBag.ID = newstring[2];
                int.TryParse(newstring[0], out int FYId);
                int.TryParse(newstring[1], out int HeadId);
                long.TryParse(ViewBag.ID, out long ClaimId);
                bool status = false;
                string Msg = "";
                TempData["Success"] = "N";
                TempData["SaveUpdateMessage"] = "Data not saved";
                bool ValidData = true;

                if (string.IsNullOrEmpty(obj.RequestNo))
                {
                    ValidData = false;
                }
                else if (string.IsNullOrEmpty(obj.RequestDate))
                {
                    ValidData = false;
                }
                else
                {
                    if (obj.ClaimType == "RLIC")
                    {
                        foreach (var item in obj.insuranceentry)
                        {
                            if (string.IsNullOrEmpty(item.InsuredPerson) || string.IsNullOrEmpty(item.StartDate) || string.IsNullOrEmpty(item.EndDate) || string.IsNullOrEmpty(item.PolicyNo) || string.IsNullOrEmpty(item.InsuredPerson) || item.PremiumAmount == 0 || string.IsNullOrEmpty(item.ReceiptNo) || string.IsNullOrEmpty(item.ReceiptDate))
                            {
                                ValidData = false;
                                if (string.IsNullOrEmpty(item.InsuredPerson))
                                {
                                    Msg = "Insured person name can't blank";
                                    ModelState.AddModelError("InsuredPerson", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.StartDate))
                                {
                                    Msg = "Start date can't blank";
                                    ModelState.AddModelError("StartDate", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.EndDate))
                                {
                                    Msg = "End date can't blank";
                                    ModelState.AddModelError("EndDate", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.PolicyNo))
                                {
                                    Msg = "Policy can't blank";
                                    ModelState.AddModelError("PolicyNo", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.InsuredPerson))
                                {
                                    Msg = "Insured person can't blank";
                                    ModelState.AddModelError("InsuredPerson", Msg);
                                }
                                else if (item.PremiumAmount == 0)
                                {
                                    Msg = "Premium amount can't blank or zero";
                                    ModelState.AddModelError("PremiumAmount", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.ReceiptNo))
                                {
                                    Msg = "Receipt no can't blank";
                                    ModelState.AddModelError("ReceiptNo", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.ReceiptDate))
                                {
                                    Msg = "Receipt date can't blank";
                                    ModelState.AddModelError("ReceiptDate", Msg);
                                }
                            }
                        }
                    }
                    if (obj.ClaimType == "RMed")
                    {
                        foreach (var item in obj.medicalentry)
                        {
                            if (string.IsNullOrEmpty(item.BillNo) || string.IsNullOrEmpty(item.BillDate) || string.IsNullOrEmpty(item.PatientName) || string.IsNullOrEmpty(item.PatientRelation) || item.BillAmt == 0)
                            {
                                ValidData = false;
                                if (string.IsNullOrEmpty(item.BillNo))
                                {
                                    Msg = "Bill no can't blank";
                                    ModelState.AddModelError("BillNo", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.BillDate))
                                {
                                    Msg = "Bill date can't blank";
                                    ModelState.AddModelError("BillDate", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.PatientName))
                                {
                                    Msg = "Patient name can't blank";
                                    ModelState.AddModelError("PatientName", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.PatientRelation))
                                {
                                    Msg = "Patient relation can't blank";
                                    ModelState.AddModelError("PatientRelation", Msg);
                                }
                                else if (item.BillAmt == 0)
                                {
                                    Msg = "Bill amount can't blank or zero";
                                    ModelState.AddModelError("BillAmt", Msg);
                                }
                            }
                        }
                    }
                    if (obj.ClaimType == "ROth")
                    {
                        foreach (var item in obj.otherentry)
                        {
                            if (string.IsNullOrEmpty(item.BillNo) || string.IsNullOrEmpty(item.BillDate) || string.IsNullOrEmpty(item.Particulars) || string.IsNullOrEmpty(item.DetailsBill) || item.BillAmt == 0 || string.IsNullOrEmpty(item.AttachmentNo))
                            {
                                ValidData = false;
                                if (string.IsNullOrEmpty(item.BillNo))
                                {
                                    Msg = "Bill no can't blank";
                                    ModelState.AddModelError("BillNo", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.BillDate))
                                {
                                    Msg = "Bill date can't blank";
                                    ModelState.AddModelError("BillDate", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.Particulars))
                                {
                                    Msg = "Particulars can't blank";
                                    ModelState.AddModelError("Particulars", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.DetailsBill))
                                {
                                    Msg = "Bill details can't blank";
                                    ModelState.AddModelError("DetailsBill", Msg);
                                }
                                else if (item.BillAmt == 0)
                                {
                                    Msg = "Bill amount can't blank or zero";
                                    ModelState.AddModelError("BillAmt", Msg);
                                }
                                else if (string.IsNullOrEmpty(item.AttachmentNo))
                                {
                                    Msg = "Attachment no can't blank";
                                    ModelState.AddModelError("AttachmentNo", Msg);
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(obj.ClaimType) && ValidData)
                {
                    if (Command == "Draft")
                    {
                        obj.IsDeleted = 2;
                    }
                    else
                    {
                        obj.IsDeleted = 0;
                    }
                    PostResult = Personnel.fnSetClaimVoucher(obj.ClaimId, obj.HeadId, obj.ClaimType, obj.FYId, obj.RequestNo, obj.RequestDate, obj.TotalAmount, obj.IsDeleted);
                    ClaimId = PostResult.ID;
                    if (obj.ClaimType == "RLIC")
                    {
                        int Count = 0;
                        foreach (var item in obj.insuranceentry)
                        {
                            Count += 1;
                            status = Personnel.fnSetClaimVoucherLIC(ClaimId, Count, item.InsuranceCompany, item.PolicyNo, item.StartDate, item.EndDate, item.InsuredPerson, item.PremiumAmount, item.ReceiptNo, item.ReceiptDate, ClsCommon.EnsureString(item.Reason));
                        }
                        Personnel.fnDelClaimVoucherSrno(ClaimId, Count);
                    }
                    if (obj.ClaimType == "RMed")
                    {
                        int Count = 0;
                        foreach (var item in obj.medicalentry)
                        {
                            Count += 1;
                            status = Personnel.fnSetClaimVoucherMed(ClaimId, Count, item.BillNo, item.BillDate, item.BillAmt, item.PatientName, item.PatientRelation, ClsCommon.EnsureString(item.Remark));
                        }
                        Personnel.fnDelClaimVoucherSrno(ClaimId, Count);
                    }
                    if (obj.ClaimType == "ROth")
                    {
                        int Count = 0;
                        foreach (var item in obj.otherentry)
                        {
                            Count += 1;
                            status = Personnel.fnSetClaimVoucherMed(ClaimId, Count, item.BillNo, item.BillDate, item.BillAmt, item.DetailsBill, item.AttachmentNo, ClsCommon.EnsureString(item.Particulars));
                        }
                        Personnel.fnDelClaimVoucherSrno(ClaimId, Count);
                    }
                    //if (Command == "Add")
                    //{
                    //    if (clsCommon.fnCheckDuplicateColValue("Bank", "Bank_ID", obj.BankId.ToString(), "Bank_NAME", obj.BankName))
                    //    {
                    //        Msg = obj.BankName + " already exists!";
                    //        ModelState.AddModelError("BankName", Msg);
                    //    }
                    //    else
                    //    {
                    //        lBankId = clsMaster.fnSetBank(obj.BankId, obj.BankName, obj.BsrCode, obj.Remark, obj.bankdet);
                    //        status = true;
                    //        Msg = "Bank data updated successfully";
                    //    }
                    //}
                    if (status)
                    {
                        Msg = "Data saved successfully.";
                        TempData["Success"] = "Y";
                        TempData["SaveUpdateMessage"] = Msg;
                        PostResult.Status = true;
                        PostResult.SuccessMessage = Msg;
                    }
                }
                if (!status)
                {
                    TempData["SaveUpdateMessage"] = Msg;
                    PostResult.SuccessMessage = Msg;
                }
            }
            catch (Exception ex)
            {
                TempData["SaveUpdateMessage"] = ex.Message.ToString();
                PostResult.SuccessMessage = ex.Message.ToString();
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FinancePaymentEntry(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Personnels.FinancePaymentEntry Modal = new Personnels.FinancePaymentEntry();
            Modal.Approve = 0;
            Modal.finyear = Personnel.GetFinYearList(0);
            long FYId = 23;
            string Yr = DateTime.Now.Year.ToString();
            foreach (var item in Modal.finyear)
            {
                if (item.Year.Contains(Yr))
                {
                    FYId = item.ID;
                }
            }
            Modal.claimemployees = Personnel.GetClaimEmployees(0, FYId);
            return View(Modal);
        }

        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _FinancePaymentEntry(Personnels.FinancePaymentEntry Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            List<Personnels.FinancePaymentList> ViweModal = new List<Personnels.FinancePaymentList>();
            ViweModal = Personnel.GetFinancePaymentList(Modal.EmpId, Modal.FYId, Modal.Approve);
            return PartialView(ViweModal);
        }

        public ActionResult _FinanceViewVoucher(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string[] newstring = GetQueryString[2].Split(',');
            ViewBag.ID = newstring[3];
            int.TryParse(newstring[0], out int FYId);
            int.TryParse(newstring[1], out int EmpId);
            int.TryParse(newstring[2], out int HeadId);
            int.TryParse(newstring[4], out int action);
            int.TryParse(ViewBag.ID, out int ClaimId);
            Personnels.FinanceViewVoucher Modal = new Personnels.FinanceViewVoucher();
            Modal = Personnel.GetFinanceViewVoucher(FYId, EmpId, HeadId, ClaimId);
            Modal.Action = ((action == 1 || action == 3) ? "Payment" : "Reject");
            ViewBag.Print = (action == 3 ? "Y" : "");
            return PartialView(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _FinanceViewVoucher(string src, Personnels.FinanceViewVoucher obj, string Command)
        {
            PostResponse PostResult = new PostResponse();
            try
            {
                ViewBag.src = src;
                string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                string[] newstring = GetQueryString[2].Split(',');
                ViewBag.ID = newstring[3];
                int.TryParse(newstring[0], out int FYId);
                int.TryParse(newstring[1], out int EmpId);
                int.TryParse(newstring[2], out int HeadId);
                int.TryParse(newstring[4], out int action);
                int.TryParse(ViewBag.ID, out int ClaimId);
                bool status = false;
                string Msg = "";
                TempData["Success"] = "N";
                TempData["SaveUpdateMessage"] = "Data not saved";
                bool ValidData = true;

                if (obj.Action == "Reject")
                {
                    if (string.IsNullOrEmpty(obj.Reason))
                    {
                        ValidData = false;
                        Msg = "Enter rejection reason";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(obj.PaidDate))
                    {
                        ValidData = false;
                        Msg = "Select paid date";
                    }
                    else if (obj.PaidAmount == 0)
                    {
                        ValidData = false;
                        Msg = "Paid amount can not be blank";
                    }
                    else if (obj.UptoPaid + obj.PaidAmount > obj.Entitlement)
                    {
                        ValidData = false;
                        Msg = "Total Paid amount can not be greater than Entitlement amount";
                    }
                    else if (obj.ProjectId == 0)
                    {
                        ValidData = false;
                        Msg = "Select pproject code";
                    }
                    else if (obj.PaidAmount != obj.TotalAmount && string.IsNullOrEmpty(obj.Reason))
                    {
                        ValidData = false;
                        Msg = "Enter reason of change of paid amount";
                    }
                    else if (string.IsNullOrEmpty(obj.AccountNo))
                    {
                        ValidData = false;
                        Msg = "Account no can not be blank";
                    }
                    else if (string.IsNullOrEmpty(obj.BankName))
                    {
                        ValidData = false;
                        Msg = "Bank name can not be blank";
                    }
                    else if (string.IsNullOrEmpty(obj.NeftCode))
                    {
                        ValidData = false;
                        Msg = "Neft code can not be blank";
                    }
                }
                if (!string.IsNullOrEmpty(obj.ClaimType) && ValidData)
                {
                    if (obj.Action == "Reject")
                    {
                        obj.Approved = 2;
                        status = Personnel.fnSetClaimVoucherStatus(obj.ClaimId, obj.Approved, obj.Reason);
                        PostResult.ID = obj.ClaimId;
                        PostResult.StatusCode = 1;
                    }
                    else
                    {
                        obj.Approved = 1;
                        PostResult = Personnel.fnSetFinancePayment(obj.ClaimId, obj.EmpId, obj.FYId, obj.ClaimType, obj.PaidDate, obj.PaidAmount, obj.ProjectId, obj.Reason, obj.AccountNo, obj.BankName, obj.NeftCode, obj.Branch);
                        status = Personnel.fnSetClaimVoucherStatus(obj.ClaimId, obj.Approved, ClsCommon.EnsureString(obj.Reason));
                    }

                    if (status)
                    {
                        Msg = "Data saved successfully.";
                        TempData["Success"] = "Y";
                        TempData["SuccessMsg"] = Msg;
                        TempData["SaveUpdateMessage"] = Msg;
                        PostResult.Status = true;
                        PostResult.SuccessMessage = Msg;
                    }
                }
                if (!status)
                {
                    TempData["SuccessMsg"] = Msg;
                    TempData["SaveUpdateMessage"] = Msg;
                    PostResult.SuccessMessage = Msg;
                }
            }
            catch (Exception ex)
            {
                TempData["SaveUpdateMessage"] = ex.Message.ToString();
                PostResult.SuccessMessage = ex.Message.ToString();
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SpecialAllowance(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            SpecialAllowance Modal = new SpecialAllowance();
            List<DropDownList> empList = new List<DropDownList>();
            GetResponse employee = new GetResponse();
            employee.Doctype = "GR_Employee";
            employee.LoginID = LoginID;
            empList = ClsCommon.GetDropDownList(employee);
            ViewBag.EmployeeList = empList;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _SpecialAllowance(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Empid = GetQueryString[2];
            long Empid = 0;
            long.TryParse(ViewBag.Empid, out Empid);

            List<SpecialAllowance> List = new List<SpecialAllowance>();
            List = Personnel.GetSpecialAllowanceList(Empid);
            return PartialView(List);
        }

        //public ActionResult BonusPaymentEntry(string src)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    BonusPaymentEntry Modal = new BonusPaymentEntry();

        //    List<DropDownList> empList = new List<DropDownList>();
        //    GetResponse employee = new GetResponse();
        //    employee.Doctype = "GR_Employee";
        //    employee.LoginID = LoginID;
        //    empList = ClsCommon.GetDropDownList(employee);
        //    ViewBag.EmployeeList = empList;

        //    return View(Modal);
        //}
        //[HttpPost]
        //public ActionResult _BonusPaymentEntry(string src)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    ViewBag.Empid = GetQueryString[2];
        //    long Empid = 0;
        //    long.TryParse(ViewBag.Empid, out Empid);

        //    List<BonusPaymentEntry> List = new List<BonusPaymentEntry>();
        //    //List = Personnel.GetSpecialAllowanceList(Empid);
        //    return PartialView(List);
        //}


        public ActionResult ConsolidatedActivityLog(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now.AddMonths(-1);
            ConsolidateReport Modal = new ConsolidateReport();
            Modal.Date = MyDate.ToString("yyyy-MM");
            Modal.ToDate = MyDate.ToString("yyyy-MM");
            Modal.MitrUser = 0;
            List<DropDownList> empList = new List<DropDownList>();
            GetResponse employee = new GetResponse();
            employee.Doctype = "ActivityLog_Employee";
            employee.LoginID = LoginID;
            empList = ClsCommon.GetDropDownList(employee);
            long EmpID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EmpID"), out EmpID);
            ViewBag.EmpID = EmpID;
            ViewBag.EmployeeList = empList;


            return View(Modal);
        }

        public ActionResult _ConsolidatedActivityLog(string src, long Empid, string FromDate, string ToDate)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Empid = GetQueryString[2];
            string FDate = Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd");
            int Tyear = Convert.ToDateTime(ToDate).Year;
            int Tmonth = Convert.ToDateTime(ToDate).Month;
            var TDate = new DateTime(Tyear, Tmonth, DateTime.DaysInMonth(Tyear, Tmonth)).ToString("yyyy-MM-dd");
            ViewBag.FromDate = Convert.ToDateTime(FromDate).ToString("MMM-yyyy");
            ViewBag.ToDate = Convert.ToDateTime(ToDate).ToString("MMM-yyyy");
            ViewBag.Empid = Empid;
            ConsolidatedActivityLog List = new ConsolidatedActivityLog();
            List = Personnel.GetConsolidatedActivityLogReport(Empid, FDate, TDate);
            return PartialView(List);
        }


        public ActionResult EmploymentHistory(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now.AddMonths(-1);
            SalaryStructure.EmployeeSalary.Add Modal = new SalaryStructure.EmployeeSalary.Add();
            Modal.FromDate = MyDate.AddYears(-1).ToString("yyyy-MM");
            Modal.ToDate = MyDate.ToString("yyyy-MM");
            //Modal.MitrUser = 0;

            List<DropDownList> empList = new List<DropDownList>();
            GetResponse employee = new GetResponse();
            employee.Doctype = "ActivityLog_Employee";
            employee.LoginID = LoginID;
            empList = ClsCommon.GetDropDownList(employee);
            long EmpID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EmpID"), out EmpID);
            ViewBag.EmpID = EmpID;
            ViewBag.EmployeeList = empList;


            return View(Modal);
        }

        public ActionResult _EmploymentHistory(string src, string FromDate, string ToDate)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            //ViewBag.Pageid = GetQueryString[4];
            long EMPID = Convert.ToInt64(clsApplicationSetting.GetSessionValue("EMPID"));
            //int.TryParse(ViewBag.EMPID, out EMPID);
            //int.TryParse(ViewBag.Pageid, out Pageid);

            DateTime dtDate = Convert.ToDateTime(ToDate);
            DateTime dtDate1 = dtDate.AddMonths(1).AddDays(-1);


            //int month = Convert.ToDateTime(ToDate).Month;
            //int Year = Convert.ToDateTime(ToDate).Year;
            //var lastDayOfMonth = DateTime.DaysInMonth(Year, month);            
            //ToDate = Convert.ToString(Convert.ToDateTime(lastDayOfMonth+"/"+ month +"/"+ Year));


            SalaryStructure.EmployeeSalary.Add Modal = new SalaryStructure.EmployeeSalary.Add();
            GetResponse getResponse = new GetResponse();
            getResponse.ID = 0;
            getResponse.AdditionalID = EMPID;
            getResponse.AdditionalID1 = 0;
            getResponse.FromDate = Convert.ToString(Convert.ToDateTime(FromDate));
            getResponse.ToDate = Convert.ToString(Convert.ToDateTime(dtDate1));
            Modal = Salary.GetEmploymentHistory(getResponse);
            SalaryStructure.GetStructureListResponse Res = new SalaryStructure.GetStructureListResponse();
            Res.Empid = EMPID;
            Res.FromDate = Convert.ToString(Convert.ToDateTime(FromDate));
            Res.ToDate = Convert.ToString(Convert.ToDateTime(dtDate1));
            Modal.lstSalryStructure = Salary.GetEmploymentHistoryStructure_List(Res);

            return PartialView(Modal);
        }



        public ActionResult FNFDetailReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            PMS.PMSStatus Modal = new PMS.PMSStatus();
            List<TempFinYearList> FList = new List<TempFinYearList>();
            FList = Personnel.GetFinYearList();
            ViewBag.FinYearList = FList;
            Modal.FYID = FList.Select(x => x.ID).FirstOrDefault();
            ViewBag.FYID = Modal.FYID;
            List<DropDownList> empList = new List<DropDownList>();
            GetResponse employee = new GetResponse();
            employee.Doctype = "GR_Employee";
            employee.LoginID = LoginID;
            empList = ClsCommon.GetDropDownList(employee);
            ViewBag.EmployeeList = empList;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _FNFDetailReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Empid = GetQueryString[2];
            ViewBag.FYID = GetQueryString[3];
            long Empid = 0, FYID = 0;
            long.TryParse(ViewBag.Empid, out Empid);
            long.TryParse(ViewBag.FYID, out FYID);

            List<PMS.PMSStatus> List = new List<PMS.PMSStatus>();
            //List = PMS.GetAppraisalReportList(Empid, FYID);
            return PartialView(List);
        }
        public ActionResult FullFinalEntry(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            FNFReport modal = new FNFReport();
            modal.Id = Convert.ToInt64(GetQueryString[2]);
            return View(modal);
        }
        public ActionResult _BonusPaymentDetails(string src)
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

            ViewBag.ProjectMappedList = _objIEmployeeSalary.GetSelectedProjectsBonusList();
            Modal.ProjectPaaymentAmountList = _objIEmployeeSalary.AddMultipleProjectPaaymentAmountList();
            Modal.EmpID = Convert.ToInt64(GetQueryString[2]);
            Modal.ID = Convert.ToInt64(GetQueryString[6]);
            if (TempData.ContainsKey("Date"))
                Modal.PaidDate = TempData["Date"].ToString();
            TempData.Keep();
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _BonusPaymentDetails(string src, BonusPaymentEntry Modal)
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
                Modal.ID = 0;
                Modal.Createdby = LoginID;
                Modal.EmpID = EmpID;
                foreach (BonusPaymentEntry _modal in Modal.ProjectPaaymentAmountList)
                {
                    Modal.ProjectID = _modal.ProjectID;
                    Modal.PaidAmount = _modal.PaidAmount;
                    if (_modal.PaidAmount == 0)
                    {
                        Result.SuccessMessage = "Paid amount must be greater than zero.";
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }

                    Result = _objIEmployeeSalary.fnSetBonusPaymentEntry(Modal);
                    TempData["Date"] = Modal.PaidDate;
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _BonusPaymentDetailsOther(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EmpID = GetQueryString[2];
            ViewBag.EmpName = GetQueryString[3];
            ViewBag.Component = GetQueryString[4];
            BonusPaymentEntry Modal = new BonusPaymentEntry();
            Modal.Component = Convert.ToString(GetQueryString[4]);
            Modal.PaidAmount = Convert.ToDecimal(GetQueryString[5]);
            Modal.EmpID = Convert.ToInt64(GetQueryString[2]);
            Modal.ID = Convert.ToInt64(GetQueryString[6]);
            Modal.DocType = Modal.Component;
            if (TempData.ContainsKey("Date"))
                Modal.PaidDate = TempData["Date"].ToString();
            TempData.Keep();
            return PartialView(Modal);
        }


        [HttpPost]
        public ActionResult _BonusPaymentDetailsOther(string src, BonusPaymentEntry Modal)
        {

            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EmpID = GetQueryString[2];
            long EmpID = 0;
            long.TryParse(ViewBag.EmpID, out EmpID);

            Result.SuccessMessage = "" + Modal.DocType + " Payment Entry Can't Save";
            if (ModelState.IsValid)
            {

           
                char separator = '-';
                string[] date = Modal.PaidDate.Split(new char[] { separator });
                Modal.Month = Convert.ToInt32(date[1]);
                Modal.Year = Convert.ToInt32(date[0]);
                TempData["Date"] = Modal.PaidDate;
                Result = Personnel.SetComponentDetails(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _BonusPaymentMedicalDetails(string src)
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
            Modal.DocNo = _objIEmployeeSalary.GetSpecialAllowanceDocNo();
            if (!string.IsNullOrEmpty(Paid_Date))
            {
                Modal.PaidDate = Paid_Date;
            }
            else
            {
                Modal.PaidDate = DateTime.Now.ToString("yyyy-MM-dd");
                Modal.PaidDate = (string.Compare(Modal.PaidDate, "1900-01-01") == 0) ? Modal.PaidDate = string.Empty : Modal.PaidDate;
            }

            ViewBag.ProjectMappedList = _objIEmployeeSalary.GetSelectedProjectsBonusList();
            Modal.ProjectPaaymentAmountList = _objIEmployeeSalary.AddMultipleProjectPaaymentAmountList();
            Modal.EmpID = Convert.ToInt64(GetQueryString[2]);
            Modal.ID = Convert.ToInt64(GetQueryString[6]);

            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult _BonusPaymentMedicalDetails(string src, BonusPaymentEntry Modal)
        {

            PostResponse Result = new PostResponse();
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
                    Modal.Component = "1";
                    if (_modal.PaidAmount == 0)
                    {
                        Result.SuccessMessage = "Paid amount must be greater than zero.";
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }

                    Result = Personnel.fnSetSpecialAllowancePaymentEntry(Modal);
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FullFinalReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ListTabs Modal = new ListTabs();
            List<Bonus.FinList> FList = new List<Bonus.FinList>();
            FList = _objIEmployeeSalary.GetFinYearList();
            ViewBag.FinYearList = FList;
           // ViewBag.FinYearList = Personnel.GetFinYearList(0);
            if (TempData.ContainsKey("SetFYID"))
                Modal.FYID = Convert.ToInt32(TempData["SetFYID"].ToString());
            TempData.Keep();
            Modal.Approve = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _FullFinalReport(string src, ListTabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Employee.List> result = new List<Employee.List>();
            getResponse.Approve = Modal.Approve;
            getResponse.AdditionalID = Modal.SetFYID;
            ViewBag.Approve = Modal.Approve;
            result = employee.GetEmployeeListFinalReport(getResponse);
            TempData["SetFYID"] = Modal.SetFYID;
            return PartialView(result);
        }
    }
}