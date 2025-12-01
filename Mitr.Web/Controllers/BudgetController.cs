using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class BudgetController : Controller
    {

        IMasterHelper Master;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IFundRaisingHelper Fund;
        IBudgetHelper Budget;
        IActivityHelper Activity;
        public BudgetController()
        {
            getResponse = new GetResponse();
            Master = new MasterModal();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            Budget = new BudgetModel();
            Fund = new FundRaisingModal();
            Activity = new ActivityModal();
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

        public ActionResult OtherBudget(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Bt_OtherBudget";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);

            return View(Modal);
        }

        //[HttpPost]
        //public ActionResult SaveMasterAll(string src, MasterAll.Add Modal, string Command)
        //{
        //    PostResponse Result = new PostResponse();
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    ViewBag.ID = GetQueryString[2];
        //    long ID = 0;
        //    long.TryParse(ViewBag.ID, out ID);
        //    Result.SuccessMessage = "Masters Can't Update";
        //    if (ModelState.IsValid)
        //    {
        //        Result.Status = true;
        //        Modal.LoginID = LoginID;
        //        Modal.IPAddress = IPAddress;
        //        Modal.ID = ID;
        //        Result = Master.fnSetMasterAll(Modal);
        //    }
        //    return Json(Result, JsonRequestBehavior.AllowGet);

        //}

        [HttpPost]
        public ActionResult _BudggetAddOther(string src, MasterAll.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            //long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.VendorID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.VendorID, out ID);
            //bool status = false;
            //string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Masters is not Saved";
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Y";
                TempData["SuccessMsg"] = "Masters save Successfully";
                PostResult.Status = true;
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ID = ID;
                PostResult = Master.fnSetMasterAll(Modal);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _BudggetAddOther(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "Bt_OtherBudget";
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

        public ActionResult TravelBudgetMaster(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            BudgetMaster.AddTravelBudget model = new BudgetMaster.AddTravelBudget();
            model.TravelBudgetList = Budget.GetTravelBudgetListYear(0);
            return View(model);
        }
        public ActionResult _ListTravelBudgetList(string src, string TravelYear)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelYear = Master.GetFinYearList(0);
            BudgetMaster.AddTravelBudget model = new BudgetMaster.AddTravelBudget();
            model.TravelBudgetList = Budget.GetTravelBudgetList(Convert.ToInt64(TravelYear));
            model.Id = 0;
            model.TravelYear = Convert.ToInt64(TravelYear);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult AddTravelBudget(string src, BudgetMaster.AddTravelBudget model)
        {

            //long success = 0;
            int ID = 0;
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            PostResult.SuccessMessage = "Action Can't Save";
            long Leadid = 0;
            if (model.TravelYear == 0)
            {
                ModelState.AddModelError("Id", "");
                PostResult.SuccessMessage = "Select Travel Year.";
            }
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {

                    Leadid = PostResult.ID;
                    PostResult.ID = model.TravelYear;
                    string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/_ListTravelBudgetList*" + PostResult.ID);
                    PostResult = Budget.SetTravelBudgetDetails(model);
                    if (PostResult.Status == true)
                    {
                        PostResult.SuccessMessage = "Action Update Data";
                        PostResult.ID = model.TravelYear;
                        PostResult.AdditionalMessage = Url;
                        ModelState.Clear();
                        return Json(PostResult, JsonRequestBehavior.AllowGet);


                    }
                }
                else
                {
                    PostResult.Status = true;
                    model.Id = ID;
                    Leadid = PostResult.ID;
                    PostResult.ID = model.TravelYear;
                    string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/_ListTravelBudgetList*" + PostResult.ID);
                    PostResult = Budget.SetTravelBudgetDetails(model);
                    if (PostResult.Status == true)
                    {
                        PostResult.SuccessMessage = "Action save Data";
                        PostResult.ID = model.TravelYear;
                        PostResult.AdditionalMessage = Url;
                        ModelState.Clear();
                        return Json(PostResult, JsonRequestBehavior.AllowGet);

                    }
                }

            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddTravelBudget(string src)
        {
            PostResponse PostResult = new PostResponse();
            //int ID = 0;
            //long Leadid = 0;
            ViewBag.src = src; BudgetMaster.AddTravelBudget model = new BudgetMaster.AddTravelBudget();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long YearId = Convert.ToInt64(GetQueryString[2]);
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            if (YearId > 0)
            {
                model.TravelBudgetList = Budget.GetTravelBudgetList(YearId);
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/_ListTravelBudgetList*" + PostResult.ID);
                ViewBag.TravelYear = Master.GetFinYearList(0);
                model.TravelYear = YearId;
                ViewBag.SetTravelYear = YearId;
                ViewBag.Seturl = Url;
            }
            else
            {
                model.TravelBudgetList = Budget.GetTravelBudgetList(model.TravelYear);
                ViewBag.TravelYear = Master.GetFinYearList(0);
                ViewBag.SetTravelYear = 0;
                //  model.AirFare = 0;
                //  model.PerDiem = 0;
                // model.Hotel = 0;
                // model.LocalTravel = 0;
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/_ListTravelBudgetList*" + PostResult.ID);
                ViewBag.Seturl = Url;
            }

            return View(model);
        }
        public ActionResult TrainingWorkshopSeminar(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            List<BudgetMaster.TrainingWorkshopSeminarTypes> model = new List<BudgetMaster.TrainingWorkshopSeminarTypes>();
            model = Budget.GetTrainingWorkTypeList(0);
            return View(model);
        }
        public ActionResult AddTrainingWorkshop(string src)
        {
            BudgetMaster.AddTrainingWorkshopSeminarTypes model = new BudgetMaster.AddTrainingWorkshopSeminarTypes();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long TrainingId = Convert.ToInt64(GetQueryString[2]);
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            getResponse.ID = TrainingId;
            model = Budget.SetTrainingWorkshop(getResponse);
            return View(model);

        }
        [HttpPost]
        public ActionResult AddTrainingWorkshop(string src, BudgetMaster.AddTrainingWorkshopSeminarTypes model)
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
            if (model.ListTrainingDetails == null)
            {
                ModelState.AddModelError("Id", "");
                PostResult.SuccessMessage = "Enter Training Details.";
            }
            if (ModelState.IsValid)
            {

                PostResult = Budget.SetTraining(model);
                if (PostResult.Status)
                {
                    foreach (var item in model.ListTrainingDetails)
                    {
                        item.TrainingWorkshopid = PostResult.ID;
                        Budget.SetTrainingDetails(item);
                    }

                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Budget/TrainingWorkshopSeminar?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/TrainingWorkshopSeminar*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult InflationRateMaster(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            BudgetMaster.AddInflationRate model = new BudgetMaster.AddInflationRate();
            model.InfMainList = Budget.GetInflationRateList(0);
            return View(model);
        }

        public ActionResult _listInflationRate(string src, string TravelYear)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            BudgetMaster.AddInflationRate model = new BudgetMaster.AddInflationRate();
            model.InflationList = Budget.GetInflationRateYearWiseList(0, Convert.ToInt64(TravelYear));
            model.Id = 0;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult AddInflationRate(string src, BudgetMaster.AddInflationRate model)
        {
            //long success = 0;
            //int ID = 0;
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long YearId = Convert.ToInt64(GetQueryString[2]);
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            PostResult.SuccessMessage = "Action Can't Save";
            //long Leadid = 0;
            if (model.InflationList == null)
            {
                ModelState.AddModelError("Id", "");
                PostResult.SuccessMessage = "Enter Inflation Details.";
            }
            if (ModelState.IsValid)
            {
                PostResult.Status = true;
                if (PostResult.Status)
                {
                    foreach (var item in model.InflationList)
                    {

                        item.Year = PostResult.ID;
                        item.Year = model.Year;
                        Budget.SetInflationDetails(item);
                    }

                }
            }
            if (PostResult.Status)
            {
                PostResult.SuccessMessage = "Action  Updated";
                PostResult.RedirectURL = "/Budget/InflationRateMaster?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/InflationRateMaster*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);


        }
        public ActionResult AddInflationRate(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long YearId = Convert.ToInt64(GetQueryString[2]);
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            BudgetMaster.AddInflationRate model = new BudgetMaster.AddInflationRate();
            PostResponse PostResult = new PostResponse();
            if (YearId > 0)
            {
                model.InflationList = Budget.GetInflationRateYearWiseList(0, YearId);
                ViewBag.TravelYear = Master.GetFinYearList(0);
                model.Year = YearId;
                model.Id = 0;
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/_listInflationRate*" + PostResult.ID);
                ViewBag.Seturl = Url;
                ViewBag.SetTravelYear = YearId;
            }
            else
            {

                model.InflationList = Budget.GetInflationRateYearWiseList(0, 0);
                ViewBag.TravelYear = Master.GetFinYearList(0);
                model.Id = 0;
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/_listInflationRate*" + PostResult.ID);
                ViewBag.Seturl = Url;


            }


            return View(model);
        }
        public ActionResult IndirectCostRateMaster(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            List<BudgetMaster.IndirectCostRate> model = new List<BudgetMaster.IndirectCostRate>();
            model = Budget.GetIndirectCostDetails(0);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddIndirectCostRate(string src, BudgetMaster.IndirectCostRate model)
        {


            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long YearId = Convert.ToInt64(GetQueryString[2]);
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            PostResult.SuccessMessage = "Action Can't Save";
            if (model.listIndirectCostRate == null)
            {
                ModelState.AddModelError("Id", "");
                PostResult.SuccessMessage = "Enter Inflation Details.";
            }
            if (ModelState.IsValid)
            {
                PostResult.Status = true;
                if (PostResult.Status)
                {
                    foreach (var item in model.listIndirectCostRate)
                    {

                        item.Year = PostResult.ID;
                        item.Year = model.Year;
                        Budget.SetIndirectcostRate(item);
                    }
                }

            }
            if (PostResult.Status)
            {
                PostResult.SuccessMessage = "Action  Updated";
                PostResult.RedirectURL = "/Budget/IndirectCostRateMaster?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/IndirectCostRateMaster*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddIndirectCostRate(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long YearId = Convert.ToInt64(GetQueryString[2]);
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            PostResponse PostResult = new PostResponse();
            BudgetMaster.IndirectCostRate model = new BudgetMaster.IndirectCostRate();
            if (YearId > 0)
            {
                model.listIndirectCostRate = Budget.GetindirectCostRate(0, YearId);
                ViewBag.TravelYear = Master.GetFinYearList(0);
                model.Year = YearId;
                model.Id = 0;
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/_listIndirectCostRate*" + PostResult.ID);
                ViewBag.Seturl = Url;
                ViewBag.SetTravelYear = YearId;
            }
            else
            {
                ViewBag.TravelYear = Master.GetFinYearList(0);
                model.listIndirectCostRate = Budget.GetindirectCostRate(0, 0);
                model.Id = 0;
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/_listIndirectCostRate*" + PostResult.ID);
                ViewBag.Seturl = Url;
            }

            return View(model);
        }

        public ActionResult _listIndirectCostRate(string src, string TravelYear)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            BudgetMaster.IndirectCostRate model = new BudgetMaster.IndirectCostRate();
            model.listIndirectCostRate = Budget.GetindirectCostRate(0, Convert.ToInt64(TravelYear));
            model.Id = 0;
            return PartialView(model);
        }
        public ActionResult FringeBenefitRateMaster(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            List<BudgetMaster.FringeBenefitRate> model = new List<BudgetMaster.FringeBenefitRate>();
            model = Budget.GetFringeBenefitRateDetails(0);
            return View(model);
        }
        public ActionResult _listFringeBenefitRate(string src, string TravelYear)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            BudgetMaster.FringeBenefitRate model = new BudgetMaster.FringeBenefitRate();
            model.listAddFringeBenefitRate = Budget.GetFringeBenefitRate(0, Convert.ToInt64(TravelYear));
            model.Id = 0;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult AddFringeBenefitRate(string src, BudgetMaster.FringeBenefitRate model)
        {

            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long YearId = Convert.ToInt64(GetQueryString[2]);
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            PostResult.SuccessMessage = "Action Can't Save";
            if (model.listAddFringeBenefitRate == null)
            {
                ModelState.AddModelError("Id", "");
                PostResult.SuccessMessage = "Enter Inflation Details.";
            }
            if (ModelState.IsValid)
            {
                PostResult.Status = true;
                if (PostResult.Status)
                {
                    foreach (var item in model.listAddFringeBenefitRate)
                    {

                        item.Year = PostResult.ID;
                        item.Year = model.Year;
                        Budget.SetFringeBenefitRate(item);
                    }
                }

            }

            if (PostResult.Status)
            {
                PostResult.SuccessMessage = "Action  Updated";
                PostResult.RedirectURL = "/Budget/FringeBenefitRateMaster?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/FringeBenefitRateMaster*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddFringeBenefitRate(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            PostResponse PostResult = new PostResponse();
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long YearId = Convert.ToInt64(GetQueryString[2]);
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            BudgetMaster.FringeBenefitRate model = new BudgetMaster.FringeBenefitRate();
            if (YearId > 0)
            {
                ViewBag.TravelYear = Master.GetFinYearList(0);
                model.listAddFringeBenefitRate = Budget.GetFringeBenefitRate(0, YearId);
                model.Id = 0;
                model.Year = YearId;
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/_listFringeBenefitRate*" + PostResult.ID);
                ViewBag.Seturl = Url;
                ViewBag.SetTravelYear = YearId;
            }
            else
            {
                ViewBag.TravelYear = Master.GetFinYearList(0);
                model.listAddFringeBenefitRate = Budget.GetFringeBenefitRate(0, 0);
                model.Id = 0;
                string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/_listFringeBenefitRate*" + PostResult.ID);
                ViewBag.Seturl = Url;
            }
            return View(model);
        }
        public ActionResult BudgetAuthoritySetting(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            BudgetMaster.BudgetAuthoritySetting model = new BudgetMaster.BudgetAuthoritySetting();
            ViewBag.EMPList = Budget.GetBudgetSettingMainEmpList(0);
            return View();
        }
        public ActionResult AddBudgetAuthoritySetting(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            PostResponse PostResult = new PostResponse();
            BudgetMaster.BudgetAuthoritySetting model = new BudgetMaster.BudgetAuthoritySetting();
            var emplist = Budget.GetBudgetSettingEmpList();
            ViewBag.EmpList = emplist;
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EmpId = Convert.ToInt64(GetQueryString[2]);
            if (EmpId > 0)
            {
                model.FinancePerson = EmpId;
                model.EMPList = Budget.GetBudgetSettingMainEmpList(EmpId);
                foreach (var i in model.EMPList)
                {
                    model.Id = i.Id;
                    model.FinancePerson = i.FinancePerson;
                }
            }

            return View(model);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddBudgetAuthoritySetting(string src, BudgetMaster.BudgetAuthoritySetting model)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            PostResponse PostResult = new PostResponse();
            var emplist = Budget.GetBudgetSettingEmpList();
            ViewBag.EmpList = emplist;
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Action Can't Save";
            if (ModelState.IsValid)
            {
                PostResult = Budget.SetbudgetSettingEmployee(model);
            }
            if (PostResult.Status)
            {
                PostResult.SuccessMessage = "Action  Updated";
                PostResult.RedirectURL = "/Budget/BudgetAuthoritySetting?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/BudgetAuthoritySetting*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateBudget(string src)
        {
            BudgetMaster.Budget modal = new BudgetMaster.Budget();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "ThematicArea";
            ViewBag.ThenaticArea = ClsCommon.GetDropDownList(getDropDown);
            getDropDown.Doctype = "WorkLocation";
            ViewBag.WorkLocation = ClsCommon.GetDropDownList(getDropDown);
            modal.listGetBudgetRpf = Budget.GetRFPBudgetProjectList(0);
            modal.listSublineActivityList = Budget.GetSublineActivityList();
            modal.listSublineActivitydraftList = Budget.GetSublineActivityDraftList();
            modal.listAloneBudget = Budget.GetStandaloneBudgetList();
            if (GetQueryString.Length > 3 && GetQueryString[3] != "")
            {
                ViewBag.TabIndex = GetQueryString[3];
            }
            else
            {
                ViewBag.TabIndex = 3;
            }


            return View(modal);
        }

        [HttpPost]
        public ActionResult CreateBudget(string src, BudgetMaster.Budget modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "ThematicArea";
            ViewBag.ThenaticArea = ClsCommon.GetDropDownList(getDropDown);
            getDropDown.Doctype = "WorkLocation";
            ViewBag.WorkLocation = ClsCommon.GetDropDownList(getDropDown);
            PostResult.SuccessMessage = "Action Can't Save";
            modal.StartDate = "1899-12-31";
            modal.EndDate = "1899-12-31";
            if (Command == "StandaloneBudget")
            {
                if (modal.ThematicAreaId != null && modal.LocationId != "" && modal.Purpose != "")
                {
                    PostResult = Budget.SetBudgetCreate(modal);
                }
                else
                {
                    ModelState.AddModelError("Id", "");
                    PostResult.SuccessMessage = "Enter New Budget Details.";
                }
            }
            else if (Command == "Budget")
            {
                if (modal.ThematicAreaId == null)
                {
                    modal.ThematicAreaId = "0";
                }
                PostResult = Budget.SetBudgetCreate(modal);

            }



            if (PostResult.Status)
            {
                if (modal.Projectid > 0)
                {
                    PostResult.OtherID = modal.Projectid;
                }
                else
                {
                    PostResult.OtherID = 0;
                }
                PostResult.SuccessMessage = "Action  Saved";
                PostResult.RedirectURL = "/Budget/Outcomesactivities?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/Outcomesactivities*" + PostResult.ID + "*" + PostResult.OtherID + "*" + "New" + "*");

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Outcomesactivities(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long BudgetId = Convert.ToInt64(GetQueryString[2]);
            BudgetMaster.OutcomesActivities modal = new BudgetMaster.OutcomesActivities();
            modal.BudgetId = Convert.ToInt64(GetQueryString[2]);
            if (GetQueryString[3] != "0")
            {
                if (Convert.ToInt64(GetQueryString[3]) > 0)
                {
                    modal = Budget.GetbudgetOutcome(0, BudgetId, 0);
                    var ProjectDetails = Budget.GetRFPBudgetProjectList(Convert.ToInt64(GetQueryString[3])).FirstOrDefault();
                    ViewBag.DonarName = ProjectDetails.donor_Name;
                    ViewBag.doc = ProjectDetails.doc_no;
                    ViewBag.proj = ProjectDetails.proj_name;
                    modal.StartDate = ProjectDetails.start_date;
                    modal.EndDate = ProjectDetails.end_date;
                    ViewBag.startdate = Convert.ToDateTime(modal.StartDate).ToString("dd/MM/yyyy");
                    ViewBag.enddate = Convert.ToDateTime(modal.EndDate).ToString("dd/MM/yyyy");
                    //modal.StartDate = Convert.ToString(ProjectDetails.start_date.ToString("dd/MM/yyyy"));
                    //modal.EndDate = Convert.ToString(ProjectDetails.end_date.ToString("dd/MM/yyyy"));
                    modal.ProjectId = Convert.ToInt64(GetQueryString[3]);

                }
            }
            else
            {
                modal.ProjectId = 0;
                modal = Budget.GetbudgetOutcome(0, BudgetId, 0);
            }



            if (GetQueryString[4] == "Edit")
            {
                ViewBag.menulock = GetQueryString[4];
            }
            else
            {
                ViewBag.menulock = "New";
            }



            return View(modal);
        }
        [HttpPost]
        public ActionResult Outcomesactivities(string src, BudgetMaster.OutcomesActivities modal, string Command)
        {
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            PostResult.SuccessMessage = "Action Can't Save";
            if (ModelState.IsValid)
            {
                if (modal.StartDate == null && modal.EndDate == null)
                {
                    ModelState.AddModelError("Id", "");
                    PostResult.SuccessMessage = "Enter Start Date and End Date.";
                }
                if (modal.ListActivitydetails == null)
                {
                    ModelState.AddModelError("Id", "");
                    PostResult.SuccessMessage = "Enter Activity Details.";
                }
                if (modal.ListOutcomeDetails == null)
                {
                    ModelState.AddModelError("Id", "");
                    PostResult.SuccessMessage = "Enter Outcome Details.";
                }
                if (Command == "Add")
                {
                    if (modal.StartDate != null && modal.EndDate != null && modal.ListActivitydetails != null && modal.ListOutcomeDetails != null)
                    {
                        if (modal.BudgetId > 0)
                        {
                            BudgetMaster.Budget modalbudget = new BudgetMaster.Budget();
                            modalbudget.Id = modal.BudgetId;

                            modalbudget.StartDate = modal.StartDate;
                            modalbudget.EndDate = modal.EndDate;
                            PostResult = Budget.SetBudgetCreate(modalbudget);
                            foreach (var item in modal.ListActivitydetails)
                            {

                                item.BudgetId = modal.BudgetId;
                                item.Activity = item.Activity;
                                item.isdeleted = 2;
                                Budget.SetBudgetActivity(item);
                            }
                            foreach (var itemoutcome in modal.ListOutcomeDetails)
                            {
                                itemoutcome.BudgetId = modal.BudgetId;
                                itemoutcome.Outcome = itemoutcome.Outcome;
                                itemoutcome.isdeleted = 2;
                                Budget.SetBudgetOutCome(itemoutcome);
                            }
                        }
                        else
                        {
                            PostResult.Status = false;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Id", "");
                        PostResult.SuccessMessage = "Enter Form Details.";
                    }


                }
                else if (Command == "Next")
                {
                    if (modal.StartDate != null && modal.EndDate != null && modal.ListActivitydetails != null && modal.ListOutcomeDetails != null)
                    {
                        if (modal.BudgetId > 0)
                        {
                            BudgetMaster.Budget modalbudget = new BudgetMaster.Budget();
                            modalbudget.Id = modal.BudgetId;

                            modalbudget.StartDate = modal.StartDate;
                            modalbudget.EndDate = modal.EndDate;
                            PostResult = Budget.SetBudgetCreate(modalbudget);
                            foreach (var item in modal.ListActivitydetails)
                            {

                                item.BudgetId = modal.BudgetId;
                                item.Activity = item.Activity;
                                item.isdeleted = modal.isdeleted;
                                Budget.SetBudgetActivity(item);
                            }
                            foreach (var itemoutcome in modal.ListOutcomeDetails)
                            {
                                itemoutcome.BudgetId = modal.BudgetId;
                                itemoutcome.Outcome = itemoutcome.Outcome;
                                itemoutcome.isdeleted = modal.isdeleted;
                                Budget.SetBudgetOutCome(itemoutcome);
                            }
                        }
                        else
                        {
                            PostResult.Status = false;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Id", "");
                        PostResult.SuccessMessage = "Enter Form Details.";
                    }
                }
                else if (Command == "Submit")
                {
                    if (modal.StartDate != null && modal.EndDate != null && modal.ListActivitydetails != null && modal.ListOutcomeDetails != null)
                    {
                        if (modal.BudgetId > 0)
                        {
                            BudgetMaster.Budget modalbudget = new BudgetMaster.Budget();
                            modalbudget.Id = modal.BudgetId;

                            modalbudget.StartDate = modal.StartDate;
                            modalbudget.EndDate = modal.EndDate;
                            PostResult = Budget.SetBudgetCreate(modalbudget);
                            foreach (var item in modal.ListActivitydetails)
                            {

                                item.BudgetId = modal.BudgetId;
                                item.Activity = item.Activity;
                                item.isdeleted = 0;
                                Budget.SetBudgetActivity(item);
                            }
                            foreach (var itemoutcome in modal.ListOutcomeDetails)
                            {
                                itemoutcome.BudgetId = modal.BudgetId;
                                itemoutcome.Outcome = itemoutcome.Outcome;
                                itemoutcome.isdeleted = 0;
                                Budget.SetBudgetOutCome(itemoutcome);
                            }
                        }
                        else
                        {
                            PostResult.Status = false;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Id", "");
                        PostResult.SuccessMessage = "Enter Form Details.";
                    }
                }
                else
                {
                    PostResult.Status = false;
                }
            }
            if (PostResult.Status)
            {
                if (Command == "Add" || Command == "Submit")
                {
                    PostResult.ID = modal.BudgetId;
                    PostResult.SuccessMessage = "Action  Saved";
                    PostResult.RedirectURL = "/Budget/Outcomesactivities?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/Outcomesactivities*" + modal.BudgetId + "*" + modal.ProjectId + "*" + "Edit" + "*");
                }

                else
                {
                    PostResult.ID = modal.BudgetId;
                    PostResult.SuccessMessage = "Action  Saved";
                    PostResult.RedirectURL = "/Budget/Entersubactivity?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/Entersubactivity*" + PostResult.ID + "*" + GetQueryString[4] + "*");
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Entersubactivity(string src)
        {
            long BudgetId = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            BudgetId = Convert.ToInt64(GetQueryString[2]);
            BudgetMaster.SetbudgetsubActivity modal = new BudgetMaster.SetbudgetsubActivity();
            modal = Budget.SetbudgetSubActivity(0, BudgetId, 0);
            modal.BudgetId = BudgetId;
            modal.Projectid = modal.Projectid;
            ViewBag.menulock = GetQueryString[3];
            return View(modal);
        }
        [HttpPost]
        public ActionResult Entersubactivity(string src, BudgetMaster.SetbudgetsubActivity modal, string Command)
        {
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Action Can not Saved";

            if (Command == "Add")
            {
                if (modal.BudgetId > 0)
                {
                    if (modal.listActivity != null)
                    {
                        foreach (var list in modal.listActivity)
                        {
                            BudgetMaster.BudgetActivity budgetActivity = list;

                            foreach (var newlistactivity in budgetActivity.listSubActivity)
                            {
                                newlistactivity.Activityid = budgetActivity.Id;
                                newlistactivity.isdeleted = 2;
                                Budget.SetBudgetSubActivityList(newlistactivity);
                                PostResult.Status = true;

                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Id", "");
                        PostResult.SuccessMessage = "Enter Form Details.";
                    }

                }
            }
            else if (Command == "Next")
            {
                if (modal.BudgetId > 0)
                {
                    if (modal.listActivity != null)
                    {
                        foreach (var list in modal.listActivity)
                        {
                            BudgetMaster.BudgetActivity budgetActivity = list;

                            foreach (var newlistactivity in budgetActivity.listSubActivity)
                            {
                                newlistactivity.Activityid = budgetActivity.Id;
                                newlistactivity.isdeleted = modal.isdeleted;
                                Budget.SetBudgetSubActivityList(newlistactivity);
                                PostResult.Status = true;

                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Id", "");
                        PostResult.SuccessMessage = "Enter Form Details.";
                    }

                }
            }
            else if (Command == "Submit")
            {
                if (modal.BudgetId > 0)
                {
                    if (modal.listActivity != null)
                    {
                        foreach (var list in modal.listActivity)
                        {
                            BudgetMaster.BudgetActivity budgetActivity = list;

                            foreach (var newlistactivity in budgetActivity.listSubActivity)
                            {
                                newlistactivity.Activityid = budgetActivity.Id;
                                newlistactivity.isdeleted = 0;
                                Budget.SetBudgetSubActivityList(newlistactivity);
                                PostResult.Status = true;

                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Id", "");
                        PostResult.SuccessMessage = "Enter Form Details.";
                    }

                }
            }
            else
            {
                PostResult.Status = false;
            }


            if (PostResult.Status)
            {
                if (Command == "Next")
                {
                    PostResult.ID = modal.BudgetId;
                    PostResult.SuccessMessage = "Action  Saved";
                    PostResult.RedirectURL = "/Budget/TentativeperiodofBudget?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/TentativeperiodofBudget*" + PostResult.ID + "*" + GetQueryString[3] + "*");
                }
                else
                {
                    PostResult.ID = modal.BudgetId;
                    PostResult.SuccessMessage = "Action  Saved";
                    PostResult.RedirectURL = "/Budget/Entersubactivity?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/Entersubactivity*" + modal.BudgetId + "*" + "Edit" + "*");
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TentativeperiodofBudget(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long BudgetId = Convert.ToInt64(GetQueryString[2]);
            BudgetMaster.SetTentativePeriodofBudget modal = new BudgetMaster.SetTentativePeriodofBudget();
            modal = Budget.GetBudgetTentativePeriod(0, BudgetId);
            ViewBag.InflationRate = modal.ListinflationDetails;
            modal.BudgetId = BudgetId;
            modal.ProjectId = modal.ProjectId;
            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Currency_master";
            getMasterResponse.IsActive = "0,1";
            ViewBag.Currency = Master.GetMasterAllList(getMasterResponse);
            ViewBag.menulock = GetQueryString[3];
            return View(modal);
        }

        [HttpPost]
        public ActionResult TentativeperiodofBudget(string src, BudgetMaster.SetTentativePeriodofBudget modal, string Command)
        {
            ViewBag.src = src;
            int TabIndex = 0;
            //bool status = false;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Action can not Saved";
            BudgetMaster.SetTentativePeriodofBudget newmodal = new BudgetMaster.SetTentativePeriodofBudget();
            newmodal = Budget.GetBudgetTentativePeriod(0, modal.BudgetId);
            decimal InflationHRRate = newmodal.ListinflationDetails[0].Rate;
            List<decimal> Newlistindirect = modal.ListindirectDetails.Select(x => x.Rate).ToList();
            List<decimal> Oldlistindirect = newmodal.ListindirectDetails.Select(x => x.Rate).ToList();
            bool isEqual = Enumerable.SequenceEqual(Newlistindirect.OrderBy(e => e), Oldlistindirect.OrderBy(e => e));
            if (isEqual) { }
            else
            {
                if (modal.ReasonIndirectRate == null)
                {
                    PostResult.SuccessMessage = "Reason for change in indirect rate will be mandatory.";
                    ModelState.AddModelError("ReasonInflationRate", "Reason for change in indirect rate will be mandatory ");
                }

            }
            PostResult.Status = false;
            if ((modal.ListinflationDetails[0].Rate > InflationHRRate || modal.ListinflationDetails[0].Rate < InflationHRRate) && modal.ReasonInflationRate == null)
            {
                if (modal.ReasonInflationRate == null)
                {
                    PostResult.SuccessMessage = "Justification for change in inflation rate is mandatory.";
                    ModelState.AddModelError("ReasonInflationRate", "Justification for change in inflation rate is mandatory");

                }

            }
            else if (modal.FringeRateCore > newmodal.FringeRateCore)
            {
                PostResult.SuccessMessage = "You cannot increase the fringe benefit rate.";
                ModelState.AddModelError("ReasonInflationRate", "FringeRateCore  and FringeRateTerm rate can’t increase ");
            }
            else if (modal.FringeRateTerm > newmodal.FringeRateTerm)
            {
                PostResult.SuccessMessage = "You cannot increase the fringe benefit rate.";
                ModelState.AddModelError("ReasonInflationRate", "FringeRateCore  and FringeRateTerm rate can’t increase ");
            }
            else if (modal.FringeRateCore > newmodal.FringeRateCore && modal.FringeRateTerm > newmodal.FringeRateTerm)
            {
                PostResult.SuccessMessage = "You cannot increase the fringe benefit rate.";
                ModelState.AddModelError("ReasonInflationRate", "FringeRateCore  and FringeRateTerm rate can’t increase ");
            }
            else if ((modal.FringeRateCore < newmodal.FringeRateCore && modal.FringeRateTerm < newmodal.FringeRateTerm) && modal.ReasonFringeRate == null)
            {
                if (modal.ReasonFringeRate == null)
                {
                    PostResult.SuccessMessage = "Reason for less fringe benefit rate is mandatory.";
                    ModelState.AddModelError("ReasonInflationRate", "FringeRateCore  and FringeRateTerm rate can’t increase  ");
                }

            }
            else if ((modal.FringeRateCore < newmodal.FringeRateCore) && modal.ReasonFringeRate == null)
            {
                if (modal.ReasonFringeRate == null)
                {
                    PostResult.SuccessMessage = "Reason for less fringe benefit rate is mandatory.";
                    ModelState.AddModelError("ReasonInflationRate", "FringeRateCore  and FringeRateTerm rate can’t increase  ");
                }

            }
            else if ((modal.FringeRateTerm < newmodal.FringeRateTerm) && modal.ReasonFringeRate == null)
            {
                if (modal.ReasonFringeRate == null)
                {
                    PostResult.SuccessMessage = "Reason for less fringe benefit rate is mandatory.";
                    ModelState.AddModelError("ReasonInflationRate", "FringeRateCore  and FringeRateTerm rate can’t increase  ");
                }

            }

            if (ModelState.IsValid)
            {

                if (Command == "Submit")
                {

                    if (modal.BudgetId > 0)
                    {
                        modal.isdeleted = 0;
                        PostResult = Budget.SetBudgetTentativePeriod(modal);
                        if (PostResult.Status)
                        {
                            foreach (var list in modal.ListinflationDetails)
                            {
                                list.BudgetId = modal.BudgetId;
                                Budget.SetBudgetInflationRate(list);

                            }
                            foreach (var listIndirect in modal.ListindirectDetails)
                            {
                                listIndirect.BudgetId = modal.BudgetId;
                                Budget.SetBudgetIndirectnRate(listIndirect);

                            }
                        }
                    }
                }
                else if (Command == "Save")
                {
                    if (modal.BudgetId > 0)
                    {
                        modal.isdeleted = 2;
                        PostResult = Budget.SetBudgetTentativePeriod(modal);
                        if (PostResult.Status)
                        {
                            foreach (var list in modal.ListinflationDetails)
                            {
                                list.BudgetId = modal.BudgetId;
                                Budget.SetBudgetInflationRate(list);

                            }
                            foreach (var listIndirect in modal.ListindirectDetails)
                            {
                                listIndirect.BudgetId = modal.BudgetId;
                                Budget.SetBudgetIndirectnRate(listIndirect);

                            }
                        }
                    }
                }
                else
                {
                    PostResult.ID = modal.BudgetId;
                    PostResult.RedirectURL = "/Budget/TentativeperiodofBudget?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/TentativeperiodofBudget*" + PostResult.ID + "*" + GetQueryString[3] + "*");
                }




            }
            if (PostResult.Status)
            {
                PostResult.ID = modal.BudgetId;
                PostResult.SuccessMessage = "Action  Saved";
                if (Command == "Save")
                {
                    TabIndex = 1;
                    PostResult.RedirectURL = "/Budget/CreateBudget?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/CreateBudget*" + PostResult.ID + "*" + TabIndex);
                }
                else
                {
                    TabIndex = 2;
                    PostResult.RedirectURL = "/Budget/CreateBudget?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/CreateBudget*" + PostResult.ID + "*" + TabIndex);
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StandaloneBudgetReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        public ActionResult AddSallaryBudget(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            var emplist = Budget.GetBudgetSettingEmpList();
            ViewBag.EmpList = emplist;
            return View();
        }
        public ActionResult _AddStaffSallary(string src)
        {
            BudgetMaster.SetBudgetEmployeeSallary Modal = new BudgetMaster.SetBudgetEmployeeSallary();
            //string startDate = "2022-04-03 00:00:00.000";
            //string endDate = "2023-09-05 00:00:00.000";
            var emplist = Budget.GetBudgetSettingEmpList();
            ViewBag.EmpList = emplist;

            return PartialView(Modal);
        }
        public ActionResult _UpdateTotalCost(string src)
        {
            return PartialView();
        }
        public ActionResult _UpdateStaffWiseSallary(string src)
        {
            return PartialView();
        }
        public ActionResult _UpdateStaffSallary(string src)
        {
            return PartialView();
        }

        
        public ActionResult AddTravelTransportBudget(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }

        public ActionResult _AddTravelTransportBudget(string src)
        {
            return PartialView();
        }
        public ActionResult _ViewEditTravelBudget(string src)
        {
            return PartialView();
        }
        public ActionResult AddTrainingBudget(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        public ActionResult _AddTraining(string src)
        {
            return PartialView();
        }
        public ActionResult _AddNewTraining(string src)
        {
            return PartialView();
        }

     
        public ActionResult AddbudgetSubActivity(string src)
        {
            long ProjectId = 0;
            BudgetMaster.SetBudget modal = new BudgetMaster.SetBudget();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            long LedgerId = 0;
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (GetQueryString.Length > 2 && GetQueryString[2] != "")
            {
                ProjectId = Convert.ToInt64(GetQueryString[2]);
            }
            LedgerId = Convert.ToInt64(GetQueryString[3]);
            if (ProjectId > 0 && LedgerId == 1001)
            {
                modal = Budget.GetBudgetSublineActivity(0, "Budget", ProjectId, LedgerId);
                modal.Ledgerid = LedgerId;
                ViewBag.LedgerId = modal.Ledgerid;

            }
            else if (ProjectId > 0 && LedgerId != 1001 && LedgerId > 0)
            {
                modal = Budget.GetBudgetSublineActivity(0, "Budget", ProjectId, LedgerId);
                modal.Ledgerid = LedgerId;
                ViewBag.LedgerId = modal.Ledgerid;
            }
            else
            {

                modal = Budget.GetBudgetSublineActivity(0, "Budget", 0, 0);
                modal.Projectid = ProjectId;
            }
            ViewBag.Id = ProjectId;
            ModelState.Clear();
            return View(modal);
        }
        [HttpPost]

        public ActionResult AddbudgetSubActivity(string src, BudgetMaster.SetBudget modal)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.Status = true;
            if (modal.listBudgetSublineActivity == null)
            {
                PostResult.Status = false;
                ModelState.AddModelError("Id", "");
                PostResult.SuccessMessage = "Enter Activity Details.";
            }
            if (modal.LedgerName.Replace(" ", "") == "TravelandTransportation")
            {
                foreach (var item in modal.listBudgetSublineActivity)
                {
                    if (item.Categoryid == null)
                    {
                        PostResult.Status = false;
                        ModelState.AddModelError("Id", "");
                        PostResult.SuccessMessage = "Category is required for the travel and transportation.";
                        break;
                    }
                }
            }
            modal.EntryType = "Manual";
            long Ledgerid = 0;
            if (modal.Finyearid == null)
            {
                modal.Finyearid = 0;
            }
            if (PostResult.Status)
            {
                PostResult = Budget.SetBudgetSublineActivity(modal);
                if (PostResult.ID > 0)
                {
                    foreach (var item in modal.listBudgetSublineActivity)
                    {
                        item.BudgetSublineid = PostResult.ID;
                        if (item.Categoryid == null)
                        {
                            item.Categoryid = 0;
                        }

                        if (item.Id == null)
                        {
                            item.Id = 0;
                        }
                        if (item.Ledgerid == 0)
                        {
                            item.Ledgerid = modal.Ledgerid;
                        }
                        Ledgerid = item.Ledgerid;
                        Budget.SetBudgetSublineActivityDet(item);
                    }
                    PostResult.Status = true;
                }
            }
            
            if (PostResult.Status)
            {
                //  int TabIndex = 2;
                PostResult.SuccessMessage = "Action  Saved";
                // PostResult.RedirectURL = "/Budget/CreateBudget?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/CreateBudget*" + PostResult.ID + "*" + TabIndex);
                // PostResult.RedirectURL = "/Budget/CreateBudget?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/CreateBudget*" + PostResult.ID);
                PostResult.RedirectURL = "/Budget/AddbudgetSubActivity?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/AddbudgetSubActivity*" + modal.Projectid + "*" + Ledgerid);
            }
            else
            {
                PostResult.ID = 1;
                PostResult.StatusCode = 1;
                PostResult.OtherID = 1;
                // PostResult.SuccessMessage = "Action  Not Saved";
                //  PostResult.RedirectURL = "/Budget/CreateBudget?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/CreateBudget*" + PostResult.ID);
                PostResult.RedirectURL = "/Budget/AddbudgetSubActivity?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/AddbudgetSubActivity*" + PostResult.ID);
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult AddbudgetSubActivity(string src, BudgetMaster.SetBudget modal)
        //{


        //    PostResponse PostResult = new PostResponse();
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    PostResult.Status = false;
        //    if (modal.listBudgetSublineActivity == null)
        //    {
        //        ModelState.AddModelError("Id", "");
        //        PostResult.SuccessMessage = "Enter Activity Details.";
        //    }

        //    modal.EntryType = "Manual";
        //    PostResult = Budget.SetBudgetSublineActivity(modal);
        //    if (PostResult.ID > 0)
        //    {
        //        foreach (var item in modal.listBudgetSublineActivity)
        //        {
        //            item.BudgetSublineid = PostResult.ID;
        //            Budget.SetBudgetSublineActivityDet(item);
        //        }
        //        PostResult.Status = true;
        //    }

        //    if (PostResult.Status)
        //    {

        //        PostResult.SuccessMessage = "Action  Saved";
        //        PostResult.RedirectURL = "/Budget/AddbudgetSubActivity?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/AddbudgetSubActivity*" + PostResult.ID);
        //    }
        //    else
        //    {
        //        //ModelState.AddModelError("Id", "");
        //        //PostResult.SuccessMessage = "Enter Start Date and End Date.";
        //        PostResult.SuccessMessage = "Action not Saved";
        //        PostResult.StatusCode = 0;
        //        PostResult.RedirectURL = "/Budget/AddbudgetSubActivity?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/AddbudgetSubActivity*" + PostResult.ID);
        //    }
        //    return Json(PostResult, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult _BudgetSublineActivity(string src)
        
        {
            long Id = 0;
            BudgetMaster.SetBudget modal = new BudgetMaster.SetBudget();
            string ledger = "";
            modal.listLedger = null;
            modal.listProjectBudget = null;
            modal.listProjectBudgetEmployee = null;
            modal.listProjectBudgetLedger = null;
            modal.listProjectBudgetYear = null;
            modal.ActivityList = null;
            GetResponse getDropDown = new GetResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Id = Convert.ToInt64(GetQueryString[6]);
            long ProjectId = Convert.ToInt64(GetQueryString[2]);
            long ledger_Id = Convert.ToInt64(GetQueryString[3]);
            ledger = Convert.ToString(GetQueryString[9]);
            if (Id > 0)
            {
                if (ledger_Id == 1001)
                {
                    modal = Budget.GetBudgetSublineActivityAll("Budget", ProjectId, ledger_Id);
                    modal.Ledgerid = 0;
                    modal.Id = Id;
                    ViewBag.ProjectName = Convert.ToString(GetQueryString[7]);
                    ViewBag.LedgerCode = Convert.ToString(GetQueryString[8]);
                    ViewBag.Id = modal.Id;
                }
                else
                {
                    modal = Budget.GetBudgetSublineActivity(0, "Budget", ProjectId, ledger_Id);
                    foreach (var item in modal.listBudgetSublineActivity)
                    {
                        item.Ledgerid = ledger_Id;
                    }
                    modal.Ledgerid = ledger_Id;
                    modal.Id = Id;
                    ViewBag.ProjectName = Convert.ToString(GetQueryString[7]);
                    ViewBag.LedgerCode = Convert.ToString(GetQueryString[8]);
                    ViewBag.Id = modal.Id;
                }

            }
            // All record filter by ledger then ledger id=1001 GetBudgetSublineActivityAll
            else if (ledger_Id == 1001)
            {
                modal = Budget.GetBudgetSublineActivityAll("Budget", ProjectId, ledger_Id);
                modal.Ledgerid = 0;
                modal.Id = Id;
                ViewBag.ProjectName = Convert.ToString(GetQueryString[7]);
                ViewBag.LedgerCode = Convert.ToString(GetQueryString[8]);
                ViewBag.Id = modal.Id;
            }
            else
            {

                modal = Budget.GetBudgetSublineActivity(0, "Budget", ProjectId, ledger_Id);
                foreach (var item in modal.listBudgetSublineActivity)
                {
                    item.Ledgerid = ledger_Id;
                }
                modal.Ledgerid = ledger_Id;
                modal.Id = modal.Id;


            }

            var Typecategory = Convert.ToString(GetQueryString[4]);
            if (Typecategory == "MASTER")
            {
                getDropDown.ID = Convert.ToInt64(GetQueryString[3]);
                getDropDown.AdditionalID = Convert.ToInt64(GetQueryString[5]);
                getDropDown.Doctype = "CATEGORY";
                getDropDown.LoginID = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
                // modal.listLedger = ClsCommon.GetBudgetDropDownList(getDropDown);
                if (ledger.Replace(" ", "") == "HumanResource")
                {

                    DropDownList ddladdemp = new DropDownList();
                    ddladdemp.ID = -1;
                    ddladdemp.Name = "TBA";
                    modal.listemplist = ClsCommon.GetBudgetDropDownList(getDropDown);
                    modal.listemplist.Add(ddladdemp);
                }
                else
                {
                    modal.listemplist = ClsCommon.GetBudgetDropDownList(getDropDown);
                }

            }

            getDropDown.Doctype = "BudgetProject_Activity";
            getDropDown.ID = ProjectId;
            getDropDown.LoginID = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
            modal.ActivityList = ClsCommon.GetDropDownList(getDropDown);
            // SubactityList bind
            getDropDown.Doctype = "Budget_SubActivityBudget";
            getDropDown.ID = modal.Budgetid;
            getDropDown.LoginID = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
            modal.SubActivityList = ClsCommon.GetDropDownList(getDropDown);

            return PartialView(modal);
        }


        public static IEnumerable<(string Month, int Year)> MonthsBetween(DateTime startDate, DateTime endDate)
        {
            DateTime iterator;
            DateTime limit;

            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (iterator <= limit)
            {
                yield return (
                    dateTimeFormat.GetMonthName(iterator.Month),
                    iterator.Year
                );

                iterator = iterator.AddMonths(1);
            }
        }
        #region SublineRowAdd
        public ActionResult AddbudgetSubActivityLineItem(string src)
        {
            long ProjectId = 0;
            BudgetMaster.SetBudget modal = new BudgetMaster.SetBudget();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            long LedgerId = 0;
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (GetQueryString.Length > 2 && GetQueryString[2] != "")
            {
                ProjectId = Convert.ToInt64(GetQueryString[2]);
            }
            LedgerId = Convert.ToInt64(GetQueryString[3]);
            if (ProjectId > 0 && LedgerId == 1001)
            {
                modal = Budget.GetBudgetSublineActivity(0, "Budget", ProjectId, LedgerId);
                modal.Ledgerid = LedgerId;
                ViewBag.LedgerId = modal.Ledgerid;

            }
            else if (ProjectId > 0 && LedgerId != 1001 && LedgerId > 0)
            {
                modal = Budget.GetBudgetSublineActivity(0, "Budget", ProjectId, LedgerId);
                modal.Ledgerid = LedgerId;
                ViewBag.LedgerId = modal.Ledgerid;
            }
            else
            {

                modal = Budget.GetBudgetSublineActivity(0, "Budget", 0, 0);
                modal.Projectid = ProjectId;
            }
            ViewBag.Id = ProjectId;
            ModelState.Clear();
            return View(modal);
        }
        [HttpPost]
        public ActionResult AddbudgetSubActivityLineItem(string src, BudgetMaster.SetBudget modal)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.Status = true;
            if (modal.listBudgetSublineActivity == null)
            {
                PostResult.Status = false;
                ModelState.AddModelError("Id", "");
                PostResult.SuccessMessage = "Enter Activity Details.";
            }
            if (modal.LedgerName.Replace(" ", "") == "TravelandTransportation")
            {
                foreach (var item in modal.listBudgetSublineActivity)
                {
                    if (item.Categoryid == null)
                    {
                        PostResult.Status = false;
                        ModelState.AddModelError("Id", "");
                        PostResult.SuccessMessage = "Category is required for the travel and transportation.";
                        break;
                    }
                }
            }
            modal.EntryType = "Manual";
            long Ledgerid = 0;
            if (modal.Finyearid == null)
            {
                modal.Finyearid = 0;
            }
            if (PostResult.Status)
            {
                PostResult = Budget.SetBudgetSublineActivity(modal);
                if (PostResult.ID > 0)
                {
                    foreach (var item in modal.listBudgetSublineActivity)
                    {
                        item.BudgetSublineid = PostResult.ID;
                        if (item.Categoryid == null)
                        {
                            item.Categoryid = 0;
                        }

                        if (item.Id == null)
                        {
                            item.Id = 0;
                        }
                        if (item.Ledgerid == 0)
                        {
                            item.Ledgerid = modal.Ledgerid;
                        }
                        Ledgerid = item.Ledgerid;
                        Budget.SetBudgetSublineActivityDet(item);
                    }
                    PostResult.Status = true;
                }
            }

            if (PostResult.Status)
            {

                PostResult.SuccessMessage = "Action  Saved";
                PostResult.RedirectURL = "/Budget/AddbudgetSubActivityLineItem?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/AddbudgetSubActivityLineItem*" + modal.Projectid + "*" + Ledgerid);
            }
            else
            {
                PostResult.ID = 1;
                PostResult.StatusCode = 1;
                PostResult.OtherID = 1;
                PostResult.RedirectURL = "/Budget/AddbudgetSubActivityLineItem?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Budget/AddbudgetSubActivityLineItem*" + PostResult.ID);
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _BudgetSublineActivityLine(string src)
        {
            long Id = 0;
            BudgetMaster.SetBudget modal = new BudgetMaster.SetBudget();
            string ledger = "";
            modal.listLedger = null;
            modal.listProjectBudget = null;
            modal.listProjectBudgetEmployee = null;
            modal.listProjectBudgetLedger = null;
            modal.listProjectBudgetYear = null;
            modal.ActivityList = null;
            GetResponse getDropDown = new GetResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Id = Convert.ToInt64(GetQueryString[6]);
            long ProjectId = Convert.ToInt64(GetQueryString[2]);
            long ledger_Id = Convert.ToInt64(GetQueryString[3]);
            ledger = Convert.ToString(GetQueryString[9]);
            if (Id > 0)
            {
                if (ledger_Id == 1001)
                {
                    modal = Budget.GetBudgetSublineActivityAll("Budget", ProjectId, ledger_Id);
                    modal.Ledgerid = 0;
                    modal.Id = Id;
                    ViewBag.ProjectName = Convert.ToString(GetQueryString[7]);
                    ViewBag.LedgerCode = Convert.ToString(GetQueryString[8]);
                    ViewBag.Id = modal.Id;
                }
                else
                {
                    modal = Budget.GetBudgetSublineActivity(0, "Budget", ProjectId, ledger_Id);
                    foreach (var item in modal.listBudgetSublineActivity)
                    {
                        item.Ledgerid = ledger_Id;
                    }
                    modal.Ledgerid = ledger_Id;
                    modal.Id = Id;
                    ViewBag.ProjectName = Convert.ToString(GetQueryString[7]);
                    ViewBag.LedgerCode = Convert.ToString(GetQueryString[8]);
                    ViewBag.Id = modal.Id;
                }

            }
            // All record filter by ledger then ledger id=1001 GetBudgetSublineActivityAll
            else if (ledger_Id == 1001)
            {
                modal = Budget.GetBudgetSublineActivityAll("Budget", ProjectId, ledger_Id);
                modal.Ledgerid = 0;
                modal.Id = Id;
                ViewBag.ProjectName = Convert.ToString(GetQueryString[7]);
                ViewBag.LedgerCode = Convert.ToString(GetQueryString[8]);
                ViewBag.Id = modal.Id;
            }
            else
            {

                modal = Budget.GetBudgetSublineActivity(0, "Budget", ProjectId, ledger_Id);
                foreach (var item in modal.listBudgetSublineActivity)
                {
                    item.Ledgerid = ledger_Id;
                }
                modal.Ledgerid = ledger_Id;
                modal.Id = modal.Id;


            }

            var Typecategory = Convert.ToString(GetQueryString[4]);
            if (Typecategory == "MASTER")
            {
                getDropDown.ID = Convert.ToInt64(GetQueryString[3]);
                getDropDown.AdditionalID = Convert.ToInt64(GetQueryString[5]);
                getDropDown.Doctype = "CATEGORY";
                getDropDown.LoginID = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
                // modal.listLedger = ClsCommon.GetBudgetDropDownList(getDropDown);
                if (ledger.Replace(" ", "") == "HumanResource")
                {

                    DropDownList ddladdemp = new DropDownList();
                    ddladdemp.ID = -1;
                    ddladdemp.Name = "TBA";
                    modal.listemplist = ClsCommon.GetBudgetDropDownList(getDropDown);
                    modal.listemplist.Add(ddladdemp);
                }
                else
                {
                    modal.listemplist = ClsCommon.GetBudgetDropDownList(getDropDown);
                }

            }

            getDropDown.Doctype = "BudgetProject_Activity";
            getDropDown.ID = ProjectId;
            getDropDown.LoginID = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
            modal.ActivityList = ClsCommon.GetDropDownList(getDropDown);
            // SubactityList bind
            getDropDown.Doctype = "Budget_SubActivityBudget";
            getDropDown.ID = modal.Budgetid;
            getDropDown.LoginID = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
            modal.SubActivityList = ClsCommon.GetDropDownList(getDropDown);

            return PartialView(modal);
        }
        #endregion

        [HttpPost]
        public JsonResult UploadBudgetData()
        {
            PostResponse PostResult = new PostResponse();
            string _imgname = string.Empty;
            string retvalue = "";
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
                    _imgname = "LeaveBalance_UPLOAD";

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
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt); // Table 1
                    string dsXml = ds.GetXml();

                    string sDeletedoptions = "0";
                    try
                    {
                        long countuploaded = 0;
                        var data= Budget.SetBudgetSublineActivityImport(dsXml);
                        countuploaded = data.ID;
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
    }
}