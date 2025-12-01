using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;

namespace Mitr.Controllers
{
    public class CommonAjaxController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";

        public string DateFormat = "dd-MMM-yyyy", DateFormatC = "dd-MMM-yy hh:mm:ss tt", DateFormatE = "yyyy-MM-dd";

        ISalaryStructureHelper Salary;
        IToolsHelper Tools;
        IActivityHelper Activity;
        ILeaveHelper Leave;
        ITravelHelper Travel;
        IMasterHelper Master;
        ITicketHelper Ticket;
        IMPRHelper MPRObj;
        IExitHelper Exits;
        IFundRaisingHelper Fund;
        IExportHelper Export;
        IRecruit_Helper Rec;


        public CommonAjaxController()
        {
            Rec = new Recruit_Modal();
            Tools = new ToolsModal();
            Activity = new ActivityModal();
            Leave = new LeaveModal();
            Travel = new TravelModal();
            Master = new MasterModal();
            Ticket = new TicketModal();
            MPRObj = new MPRModal();
            Exits = new ExitModal();
            Fund = new FundRaisingModal();
            Export = new ExportModal();
            Salary = new SalaryStructureModal();
            var d = clsApplicationSetting.GetConfigValue("DateFormat");
            var dC = clsApplicationSetting.GetConfigValue("DateFormatC");
            DateFormat = (!string.IsNullOrEmpty(d) ? d : DateFormat);
            DateFormatC = (!string.IsNullOrEmpty(dC) ? dC : DateFormatC);

            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetGenerateValuesJSon(GetGenerateValues Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            PostResult = Common_SPU.fnGetGenerateValues(Modal);
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetDropDownListJson(GetResponse Modal)
        {
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return Json(ClsCommon.GetDropDownList(Modal), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateColumn_CommonJson(GetUpdateColumnResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            PostResult = Common_SPU.fnGetUpdateColumnResponse(Modal);
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DelRecord_CommonJson(GetResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            PostResult = Common_SPU.fnDelRecord_Common(Modal);
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DelRecordTraining_CommonJson(GetResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            PostResult = Common_SPU.fnDelRecord_CommonTraining(Modal);
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CheckRecordExistsJSon(GetRecordExitsResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            PostResult = Common_SPU.fnGetCheckRecordExist(Modal);
            PostResult.AdditionalMessage = Modal.Value;
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CheckUserDetailsExistsJSon(GetRecordExitsResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Doctype = "EMP_Registration".ToString();
            PostResult = Common_SPU.fnGetCheckRecordExist(Modal);
            PostResult.AdditionalMessage = Modal.Value;
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CheckOnboardingUserDetailsExistsJSon(GetRecordExitsResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Doctype = "EMP_Registration".ToString();
            PostResult = Common_SPU.GetOnboardingCheckRecordExist(Modal);
            PostResult.AdditionalMessage = Modal.Value;
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CapacityTrainingNameRecordExistJSon(GetRecordExitsResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Doctype = "EMP_Registration".ToString();
            PostResult = Common_SPU.GetCapacityTrainingNameRecordExist(Modal);
            PostResult.AdditionalMessage = Modal.Value;
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CapacityTrainingTypeRecordExist(GetRecordExitsResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Doctype = "EMP_Registration".ToString();
            PostResult = Common_SPU.GetCapacityTrainingTypeRecordExist(Modal);
            PostResult.AdditionalMessage = Modal.Value;
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CheckUserDetailsExistsLeaveJSon(long EMPID,string LastWorkingDate)
        {
         
            GetRecordExitsResponse Modal = new GetRecordExitsResponse();
            PostResponse PostResult = new PostResponse();
            Modal.ID = EMPID;
            Modal.Doctype = LastWorkingDate;
            PostResult = Common_SPU.fnGetCheckRecordLeaveExist(Modal);
            if (PostResult.ID > 0)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ActiveDeactiveJson(string Type, string ID, string Value, string ColomanName = "IsActive")
        {
            switch (Type.ToLower())
            {
                case "uomlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "PMS_UOM", "PMSUOMID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);
                case "pmsquestion":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "PMS_Essential", "PMS_EID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "appraisaltraining":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "PMS_Appraisal_Det", "PMS_ADID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "pmskpa":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "PMS_KPA", "PMS_KPAID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "mprlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "MPR", "MPRID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "mprsubseclist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "MPR_SubSection", "MPRSubSID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "indicatorlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "MPR_indicator", "indicatorID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "mprsec":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "MPR_Section", "MPRSID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "locationgrouplist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "LocationGroup", "LocationGroupID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "jobpostlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "JobPost", "JobPostID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "joblist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Job", "JobID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "rolelist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "User_Role", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "menulist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Login_Menu", "MenuID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "userslist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "UserMan", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);
                case "usersnonlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "nonmitruserman", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);
                case "countrylist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Master_Country", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "statelist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Master_State", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "citylist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Master_City", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "cmslist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "CMS", "CMSID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "cmssectionlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "CMSSection", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "diemlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_diem", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "freemeallist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_freemeal", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "kmratelist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_km", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "masterall":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_All", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "currencylist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_Currency", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "costcenterlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_costcenter", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "sublineitemlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_sublineitem", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "vendorlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_vendor", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "depreciationratelist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_depre_rate", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "finyearlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Master_Country", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "membershipperiodlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "MembershipPeriod", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "holidaylist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Holiday", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);
                case "traveldeshk":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "TravelDesk", "EmployeeId", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);
                case "thematicarealist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_thematicarea", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "locationlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_Location", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "consultantlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_consultant", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "subgrantdetailslist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "subgrantdet", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "donorlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_donor", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "donardetailslist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "donor_detail", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "announcementlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Announcement", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "leavelist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_leave", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "designationlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_design", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "bloodgrouplist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Blood_group", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "empsalarylist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "emp_salary", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "employeelist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_emp", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "departmentlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Master_Department", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "calenderyearlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "financial_year", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "pfaquestionlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "pfaquestion", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "performanceincrementlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "performIncrem", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "recruitmentrequestlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Recruitment", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "conceptnotelist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "concept_note", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "addsubmissionofnarrativeproposal":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "project_regdet", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "emailtemplatelist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "EmailTemplate", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "hiringconsultantlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "EmailTemplate", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "updateconceptstatus":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "concept_note", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "companyuploadlist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "bylaws", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "procurementcommitteelist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "master_procurcomm", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "gradelist":
                    return Json(ClsCommon.UpdateIsActiveCommon("isdeleted", "SS_GradeMaster", "id", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);

                case "tagmaster":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "DL_TagMaster", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);
                case "declarationmaster":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "DeclarationMaster", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);
                case "officelist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Exist_OfficeListing", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);
                case "signatorylist":
                    return Json(ClsCommon.UpdateIsActiveCommon(ColomanName, "Exist_SignatoryMaster", "ID", ID, Value).ToString() + "::" + Value, JsonRequestBehavior.AllowGet);
                default:
                    return Json("");
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateColomnJSon(string ID, string Value, string Type)
        {
            string ColomanName = "Priority";
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "something went wrong while updating priority";
            switch (Type.ToLower())
            {
                case "pmsquestion":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "PMS_ESSENTIAL", "PMS_EID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "pmskpa":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "PMS_KPA", "PMS_KPAID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);

                case "mprlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "MPR", "MPRID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);

                case "mprsubseclist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "MPR_SubSection", "MPRSubSID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "indicatorlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "MPR_indicator", "indicatorID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "mprsec":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "MPR_Section", "MPRSID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);

                case "locationgrouplist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "LocationGroup", "LocationGroupID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "jobpostlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "JobPost", "JobPostID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "joblist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "Job", "JobID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "cmssectionlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "CMSSection", "CMSSectionID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "rolelist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "Login_Role", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "menulist":
                    if (ClsCommon.UpdateColom_Common("MenuPriority", "Login_Menu", "MenuID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "userslist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "UserMan", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "countrylist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "UserMan", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(ClsCommon.UpdateColom_Common(ColomanName, "Master_Country", "ID", ID, Value), JsonRequestBehavior.AllowGet);
                case "statelist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "Master_State", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "citylist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "Master_City", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "diemlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_diem", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "freemeallist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_freemeal", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "kmratelist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_km", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "masterall":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_All", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "currencylist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_Currency", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "costcenterlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_costcenter", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "sublineitemlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_sublineitem", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);

                case "vendorlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_vendor", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "depreciationratelist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_depre_rate", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "finyearlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "FinYear", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "membershipperiodlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "MembershipPeriod", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "holidaylist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "Holiday", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "thematicarealist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_thematicarea", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "locationlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_location", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "consultantlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_consultant", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "subgrantdetailslist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "subgrantdet", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "donorlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_donor", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "donordetailslist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "donor_detail", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "announcementlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "Announcement", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "leavelist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_leave", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "designationlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_design", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "bloodgrouplist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "Blood_group", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "empsalarylist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "emp_salary", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "employeelist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_emp", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "calenderyearlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "financial_year", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "pfaquestionList":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "pfaquestion", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "performanceincrementlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "performIncrem", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "recruitmentrequestlist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "Recruitment", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "conceptnotelist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "concept_note", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "emailtemplatelist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "EmailTemplate", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                case "procurementcommitteelist":
                    if (ClsCommon.UpdateColom_Common(ColomanName, "master_procurcomm", "ID", ID, Value))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = ColomanName + " updated successfully";
                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                default:
                    return Json(PostResult);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult fnCheckDuplicate(string sType, string lId, string sFieldValue)
        {
            PostResponse PostResult = new PostResponse();
            switch (sType.ToLower())
            {
                case "leavemaster":
                    if (ClsCommon.fnCheckDuplicateColValue("master_leave", "ID", lId, "Leave_Name", sFieldValue))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = sFieldValue + "is Already Exists, You Can't Insert duplicate Value";

                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);

                case "consultant name":
                    if (ClsCommon.fnCheckDuplicateColValue("master_consultant", "ID", lId, "consultant_name", sFieldValue))
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = sFieldValue + "is Already Exists, You Can't Insert duplicate Value";

                    }
                    return Json(PostResult, JsonRequestBehavior.AllowGet);

                default:
                    return Json(PostResult);
            }

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SetLeaveShowInSummaryJSON(string LeaveID, int ShowInSummary)
        {
            return Json(ClsCommon.SetLeaveShowInSummary(LeaveID, ShowInSummary) + "::" + ShowInSummary.ToString(), JsonRequestBehavior.AllowGet);

        }




        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPushNotificationListJSON(string ListType)
        {
            Singleton NotiObj = Singleton.Instance;
            NotiObj.SendMailbySingleton();
            return Json(NotiObj.GetPushNotificationList(ListType), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ClearRecentNotificationJSON(string ID)
        {
            Singleton NotiObj = Singleton.Instance;
            return Json(NotiObj.ClearRecentNotification(ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDateTimeJson()
        {
            string MyTime = DateTime.Now.ToString("dddd, dd-MMM-yyyy hh:mm:ss tt");
            return Json(MyTime, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetUserStrickyNotesJson()
        {
            long ID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out ID);
            string Notes = clsDataBaseHelper.ExecuteSingleResult("select StrickyNotes from users where ID=" + ID);
            return Json(Notes, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SetUserStrickyNotesJson(string StrickyNotes)
        {
            long ID = 0;
            bool tre = false;
            try
            {
                long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out ID);
                clsDataBaseHelper.ExecuteNonQuery("update users Set StrickyNotes='" + ClsCommon.EnsureString(StrickyNotes) + "' where ID=" + ID);
                tre = true;
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during SetUserStrickyNotesJson. The query was executed :", ex.ToString(), "", "CommonAjax", "CommonAjax", "");
            }
            return Json(tre, JsonRequestBehavior.AllowGet);


        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CheckEmailTemplateNameExistJSON(string TemplateID, string TemplateName)
        {
            return Json(Tools.CheckEmailTemplateNameExist(TemplateID, TemplateName), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult EncryptQueryStringJSON(string Value)
        {
            return Json(clsApplicationSetting.EncryptQueryString(Value), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DecryptQueryStringJSON(string Value)
        {
            return Json(clsApplicationSetting.DecryptQueryString(Value), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetConfigValueJSON(string Key)
        {
            return Json(clsApplicationSetting.GetConfigValue(Key), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SaveEmployeeProjectJson(string ProjectIDs)
        {

            long Result = 0;
            try
            {
                foreach (var item in ProjectIDs.Split(','))
                {
                    Result = Common_SPU.fnSetProjectEMP_SelfMapping(0, Convert.ToInt64(item), "Map", "");
                }
                String SQL = @"update ProjectEMP_SelfMapping set Isdeleted=1 ,deletedat=getdate(),
                                IPAddress='" + ClsCommon.GetIPAddress() + "' where ProRegID not in (" + ProjectIDs + ") " +
                                " and EMPID=" + clsApplicationSetting.GetSessionValue("EMPID") + "";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during SaveEmployeeProjectJson. The query was executed :", ex.ToString(), "SaveEmployeeProjectJson", "CommonAjax", "CommonAjax", "");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SaveStandardTaskJson(string Message)
        {

            long Result = 0;
            try
            {
                Result = Common_SPU.fnSetProjectEMP_SelfMapping(0, 0, "Task", Message);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during SaveStandardTask. The query was executed :", ex.ToString(), "SaveStandardTask", "CommonAjax", "CommonAjax", "");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSavedTaskJson()
        {
            string SearchText = "";
            return Json(Activity.GetSelectedProjectsList("Task", SearchText), JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetConsolidateEMPjson(string Date)
        {
            DateTime MyDate;
            DateTime.TryParse(Date, out MyDate);
            return Json(Activity.GetConsolidateEMPList(MyDate.Month, MyDate.Year), JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetCalenedarDataJson()
        {
            long EMPIDDD = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);
            return Json(Leave.GetCalenedarData(EMPIDDD).Where(x => x.description != null).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetAllHolidayForCalenedarJson()
        {
            long EMPIDDD = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);
            return Json(Leave.GetHoliTrvAndLeaveList(EMPIDDD), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LeaveRowAdd(string _LeaveTypeID, string _StartDate, string _EndDate)
        {
            PostResponse PostResult = new PostResponse();
            string InnerHTML = "<tbody>", ApplySandwichPolicy = "N";
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            int LeaveTypeID = 0;
            int Count = 0;
            try
            {
                string ConnectWith = "";
                int.TryParse(_LeaveTypeID, out LeaveTypeID);
                DateTime.TryParse(_StartDate, out StartDate);
                DateTime.TryParse(_EndDate, out EndDate);
                //if (LeaveTypeID == 5 || LeaveTypeID == 7)
                //if (LeaveTypeID == 5)
                //{
                //    ApplySandwichPolicy = "Y";
                //}

                string SQL = @"select top 1 SS_EmployeeSalary.workingHours from SS_EmployeeSalary left join master_all as tblSubcategory on tblSubcategory.id=SS_EmployeeSalary.subcategoryid and tblSubcategory.table_name='SS_Subcategory' where empid=" + clsApplicationSetting.GetSessionValue("EMPID") + " and   SS_EmployeeSalary.isdeleted=0 order by SS_EmployeeSalary.id desc";
                string WorkingHours = clsDataBaseHelper.ExecuteSingleResult(SQL);
                DataSet ValidaDateDS = Common_SPU.fnGetWorkingDays(StartDate.ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"), ApplySandwichPolicy);
                if (StartDate.Date > EndDate.Date)
                {
                    PostResult.StatusCode = 1;
                    PostResult.SuccessMessage = "Oops! Leave date doesn't seem valid. End date should be after start date.";

                }
                else if (ValidaDateDS.Tables[0].Rows.Count == 0)
                {
                    PostResult.StatusCode = 2;
                    PostResult.SuccessMessage = "You can't apply leave for week-off/ Holiday. Please verify the dates.";
                }
                else
                {
                    foreach (DataRow Valitem in ValidaDateDS.Tables[0].Rows)
                    {
                        PostResult.Status = true;
                        InnerHTML += "<tr>";
                        InnerHTML += "<td><label id='lblCount_" + Count + "'>" + (Count + 1) + "</label></td>";

                        InnerHTML += "<td><input type='date' class='form-control txtDate' data-val='true' data-val-required=\"Date Can't be Blank\" id='LeaveTranList_" + Count + "__LeaveDate' name='LeaveTranList[" + Count + "].LeaveDate' placeholder='Date' value='" + Convert.ToDateTime(Valitem["date"]).ToString("yyyy-MM-dd") + "'>";
                        InnerHTML += "<span class='field-validation-valid text-danger' data-valmsg-for='LeaveTranList[" + Count + "].LeaveDate' data-valmsg-replace='true'></span>";
                        InnerHTML += "</td>";


                        InnerHTML += "<td><select class='form-control  applyselect ddleaveType'  id=\"LeaveTranList_" + Count + "__LeaveType\" name=\"LeaveTranList[" + Count + "].LeaveType\">";
                        foreach (var item in Leave.GetLeaveType())
                        {
                            InnerHTML += "<option " + (LeaveTypeID == item.ID ? "selected" : "") + " value=" + item.ID + ">" + item.LeaveName + "</option>";
                            if (LeaveTypeID == item.ID)
                            {
                                ConnectWith = item.LeaveName;
                            }
                        }
                        InnerHTML += "</select>";
                        InnerHTML += "<span class='field-validation-valid text-danger' data-valmsg-for='LeaveTranList[" + Count + "].LeaveType' data-valmsg-replace='true'></span>";
                        InnerHTML += "</td>";


                        InnerHTML += "<td class='lvtdslt'><select class='applyselect txthours' id=\"LeaveTranList_" + Count + "__LeaveHours\" name=\"LeaveTranList[" + Count + "].LeaveHours\" >";
                        if (clsApplicationSetting.GetConfigValue("LeaveApplyRule") == "Hourly")
                        {
                            //for (int i = 1; i <= Convert.ToInt32(clsApplicationSetting.GetConfigValue("WorkingHours")); i++) code comment by shailendra 17092024
                            for (int i = 1; i <= Convert.ToInt32(WorkingHours); i++)
                            {
                                // InnerHTML += "<option " + (Convert.ToInt32(clsApplicationSetting.GetConfigValue("WorkingHours")) == i ? "selected" : "") + " value=" + i + ">" + i + "</option>";
                                InnerHTML += "<option " + (Convert.ToInt32(WorkingHours) == i ? "selected" : "") + " value=" + i + ">" + i + "</option>";
                            }
                        }
                        else
                        {
                            if (clsApplicationSetting.GetConfigValue("DailyLeaveOption").Contains(','))
                            {
                                foreach (var item in clsApplicationSetting.GetConfigValue("DailyLeaveOption").Split(','))
                                {
                                    InnerHTML += "<option " + (clsApplicationSetting.GetConfigValue("WorkingHours") == item.Split('@')[1] ? "selected" : "") + " value=" + item.Split('@')[1] + ">" + item.Split('@')[0] + "</option>";
                                }
                            }
                        }
                        InnerHTML += "</select>";
                        InnerHTML += "<span class='field-validation-valid text-danger' data-valmsg-for='LeaveTranList[" + Count + "].LeaveType' data-valmsg-replace='true'></span>";
                        InnerHTML += "</td>";
                        InnerHTML += "<td>" + (Count > 0 ? "<a class='deleterow'> <span class='close'>X</span></a>" : "") + "</td>";
                        InnerHTML += "</tr>";
                        Count++;
                    }

                }
            }
            catch (Exception ex)
            {
                PostResult.Status = false;
                ClsCommon.LogError("Error during LeaveRequestDateWiseRowAdd. The query was executed :", ex.ToString(), "LeaveRequestDateWiseRowAdd", "CommonAjax", "CommonAjax", "");
            }
            InnerHTML += "</tbody>";
            PostResult.ViewAsString = InnerHTML;
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DeleteLeaveLogJSON(long leaveLogID)
        {
            TempData["Success"] = "Y";
            TempData["SuccessMsg"] = "Your leave request deleted successfully";
            // Fire Mail
            Common_SPU.fnCreateMail_Leave(leaveLogID);
            return Json(Common_SPU.fnDelLeaveLog(leaveLogID), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult RFCLeaveRequestJSON(long leaveLogID, string RFCRemarks)
        {

            Common_SPU.fnsetRFC_LeaveRequest(leaveLogID, RFCRemarks);
            TempData["Success"] = "Y";
            TempData["SuccessMsg"] = "Request for cancellation submitted successfully";

            // Fire Mail
            Common_SPU.fnCreateMail_Leave(leaveLogID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ApprovedLeaveRequestJSON(long leaveLogID)
        {

            PostResponse PostResult = new PostResponse();
            PostResult.ID = leaveLogID;
            int RFC = 0;
            int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select RFC from leave_log where ID=" + leaveLogID), out RFC);
            int edid = 0;
            int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select master_emp.ed_name from leave_log inner join master_emp on leave_log.emp_id=master_emp.id where leave_log.ID=" + leaveLogID), out edid);
            string isEd = "";
            if (clsApplicationSetting.GetSessionValue("EMPID").ToString() == edid.ToString())
            {
                isEd = "Y";
            }
            if (RFC > 0)
            {
                Common_SPU.fnSetRFC_LeaveRequest_Approve(leaveLogID);
                PostResult.Status = true;
                PostResult.SuccessMessage = "RFC successfully Accecpted";
                TempData["Success"] = "Y";
                TempData["SuccessMsg"] = PostResult.SuccessMessage;
            }
            else
            {
                int Approve = (isEd == "Y" ? 4 : 1);
                string RetValue = Common_SPU.fnIsLeaveRequestForEDApproval(leaveLogID);
                if (RetValue == "ED and HOD is Same")
                {
                    Approve = 1;
                    RetValue = "";
                }
                if (string.IsNullOrEmpty(RetValue) || isEd == "Y")
                {

                    Common_SPU.fnSetLeaveApprovalHOD(leaveLogID, Approve, "", "");
                    PostResult.Status = true;
                    PostResult.SuccessMessage = "Leave Request Approved Successfully";
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = "Leave Request Approved Successfully";
                    // Fire Mail
                    Common_SPU.fnCreateMail_Leave(leaveLogID);
                }
                else
                {
                    PostResult.StatusCode = 1;
                    PostResult.SuccessMessage = RetValue;
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetProjectDetail_DropdownJson(long ProjectID)
        {
            return Json(Travel.GetProjectDetail_Dropdown(ProjectID), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetBudgetSubcat_DropdownJson(long ActivityId)
        {
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "Budget_SubActivity";
            getDropDown.ID = ActivityId;
            getDropDown.LoginID = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
            var subactivity = ClsCommon.GetDropDownList(getDropDown);
            return Json(subactivity, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetApprasel_DropdownJson(string Type)
        {
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = Type;
            getDropDown.ID = 0;
            getDropDown.LoginID = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
            var subactivity = ClsCommon.GetDropDownList(getDropDown);
            return Json(subactivity, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetKPI_DropdownJson(long KPID)
        {
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "KPIData";
            getDropDown.ID = KPID;
            getDropDown.LoginID = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
            var KPI = ClsCommon.GetDropDownList(getDropDown);
            return Json(KPI, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSessionValueJson(string SessionName)
        {
            PostResponse PostResult = new PostResponse();

            if (clsApplicationSetting.IsSessionExpired(SessionName))
            {
                PostResult.SuccessMessage = "Session Is Expired";
            }
            else
            {
                PostResult.Status = true;
                PostResult.SuccessMessage = clsApplicationSetting.GetSessionValue(SessionName);

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteTravelDocJSON(string TravelDocID)
        {
            string SQL = "";
            long Result = 0;
            try
            {
                SQL = @"update TravelDocuments set Isdeleted=1 ,deletedat=getdate(),
                                IPAddress='" + ClsCommon.GetIPAddress() + "' where TravelDocID=" + TravelDocID;
                Result = clsDataBaseHelper.ExecuteNonQuery(SQL);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during DeleteTravelDocJSON. The query was executed :", ex.ToString(), SQL, "CommonAjax", "CommonAjax", "");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteAttachmentJSON(long AttachmentID)
        {
            int Result = 0;
            string MasterLoginID = (!clsApplicationSetting.IsSessionExpired("LoginID") ? clsApplicationSetting.GetSessionValue("LoginID") : "0");
            string SQL = "UPDATE master_attachment SET  isdeleted=1, deletedby='" + MasterLoginID + "',deletedat=GETDATE() WHERE id =" + AttachmentID + "";
            Result = clsDataBaseHelper.ExecuteNonQuery(SQL);
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DeleteTravelRequestJson(string ID)
        {
            return Json(Common_SPU.fnDelTravelReq(ID), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedTravelRequestJson(long ID)
        {
            if (ID > 0)
            {
                Common_SPU.fnCreateMail_Travel(ID, "Approved");
            }
            return Json(Common_SPU.fnSetTravelRequestAction(ID, 1, "", "Approved"), JsonRequestBehavior.AllowGet);


        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedExpenceTravelRequestJson(long ID, decimal Paidamount, string PaidDate)
        {
            return Json(Common_SPU.fnSetTravelRequestExpenceAction(ID, Paidamount, PaidDate, "Confirm", ""), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LockedRequestJson(long EMPId, int Lock)
        {
            return Json(Common_SPU.fnSetEMPLockAction(EMPId, Lock), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult EditRequestJson(long PMSGSID)
        {
            return Json(Common_SPU.fnSetPMSEditAction(PMSGSID), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedAdvanceTravelRequestJson(long ID, decimal Paidamount, string PaidDate)
        {
            return Json(Common_SPU.fnSetTravelRequestAdvanceAction(ID, Paidamount, PaidDate, "Confirm", ""), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedTravelfinanceCardJson(long ID, decimal Paidamount, string PaidDate, string UatNo)
        {
            return Json(Common_SPU.fnSetTravelFinanceOfficeCardAction(ID, Paidamount, PaidDate, "Confirm", "", UatNo), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ActiveDeclaration(long ID, string Doctype)
        {
            return Json(Common_SPU.fnSetActiveDeclartion(ID, Doctype), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ShowDeclarationContent(long ID)
        {
            string SQL = "select Content from DeclarationMaster where Id=" + ID + " and isdeleted=0 ";
            DataTable dt = clsDataBaseHelper.ExecuteDataTable(SQL, "A");
            var data = "";
            foreach (DataRow item in dt.Rows)
            {

                data = Convert.ToString(item["Content"]);

            }
            //  var result = ClsCommon.ConvertDataTableToJson(dt);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ShowDeclarationApprovedContent(long ID)
        {
            string SQL = "select DeclarationMaster.Content,DeclarationEmployee.Accept,DeclarationEmployee.Status,DeclarationEmployee.Remark from DeclarationMaster left join DeclarationEmployee on DeclarationEmployee.DeclarationId = DeclarationMaster.Id where DeclarationEmployee.ID = " + ID + " and DeclarationMaster.isdeleted = 0 and DeclarationEmployee.isdeleted = 0";
            DataTable dt = clsDataBaseHelper.ExecuteDataTable(SQL, "A");
            Employee.Declaration result = new Employee.Declaration();
            foreach (DataRow item in dt.Rows)
            {

                result.Content = Convert.ToString(item["Content"]);
                result.Accept = Convert.ToInt32(item["Accept"]);
                result.Remark = Convert.ToString(item["Remark"]);
                result.Status = Convert.ToString(item["Status"]);

            }
            //  var result = ClsCommon.ConvertDataTableToJson(dt);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CancelTravelfinanceCardJson(long ID, string Reason)
        {
            return Json(Common_SPU.fnSetTravelFinanceOfficeCardAction(ID, 0, "1899-12-31", "Reject", Reason, ""), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DeclrationApproved(long ID, string Accept, string Remark, string status, string UserId, string EmpId,string DueDate)
        {
            return Json(Common_SPU.fnSetDeclartionEmpAccept(ID, Accept, Remark, status, UserId, EmpId, Convert.ToDateTime(DueDate).ToString("yyyy-MM-dd")), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult HRDeclrationApproved(long ID, string Accept, string Remark, string status, string UserId, string EmpId, string DueDate)
        {
            return Json(Common_SPU.SetHRDeclartionEmpAccept(ID, Accept, Remark, status, UserId, EmpId, Convert.ToDateTime(DueDate).ToString("yyyy-MM-dd")), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DeclrationPendingCount(string CanID)
        {
            bool IsStatus = false;
            GetResponse getResponse=new GetResponse();
            IEmployeeHelper employee =  new EmployeeModal();
            Employee.Declaration result = new Employee.Declaration();
            getResponse.ID =Convert.ToInt32( clsApplicationSetting.GetSessionValue("EMPID"));
            result.declarationslist = employee.GetUserDeclartionList(getResponse);
            if (result.declarationslist.Count == 0)
            {
                int rtnVal = UpdateOnboardUserStatus(CanID);
                IsStatus = true;
            }
            return Json(IsStatus, JsonRequestBehavior.AllowGet);
        }
        public int UpdateOnboardUserStatus(string candidateId)
        {
            int rtn = 0;
            try
            {
                if (!string.IsNullOrEmpty(candidateId))
                {
                    int id = Convert.ToInt32(candidateId);
                    rtn = Common_SPU.UpdateOnboardUserStatus(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rtn;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedManagerTravelRequestJson(long TravelRequestID, long ProjectDetId)
        {
            return Json(Common_SPU.fnSetTravelRequestApprovalManager(TravelRequestID, ProjectDetId, "Confirm", ""), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult TransferTravelRequestJson(long TravelRequestDetID, long TravelDeskId)
        {
            return Json(Common_SPU.fnSetTravelRequestTransferTrevelDesk(TravelRequestDetID, TravelDeskId), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult RejectManagerTravelRequestJson(long TravelRequestID, long ProjectDetId)
        {
            return Json(Common_SPU.fnSetTravelRequestApprovalManager(TravelRequestID, ProjectDetId, "Reject", ""), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CancelAdvanceTravelRequestJson(long ID, string Reason)
        {
            return Json(Common_SPU.fnSetTravelRequestAdvanceAction(ID, 0, "1899-12-31", "Reject", Reason), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult RFCTravelRequestSend(long ID, string Reason)
        {
            return Json(Common_SPU.fnSetTravelRequestRFCSend(ID, Reason), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CancelExpenceTravelRequestJson(long ID, string Reason)
        {
            return Json(Common_SPU.fnSetTravelRequestExpenceAction(ID, 0, "1899-12-31", "Reject", Reason), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Delete_IStaff(string REC_IStaffID)
        {
            int Result = 0;
            string MasterLoginID = (!clsApplicationSetting.IsSessionExpired("LoginID") ? clsApplicationSetting.GetSessionValue("LoginID") : "0");
            string SQL = "UPDATE REC_IStaff SET  isdeleted=1, deletedby='" + MasterLoginID + "',deletedat=GETDATE() WHERE REC_IStaffID =" + REC_IStaffID + "";
            Result = clsDataBaseHelper.ExecuteNonQuery(SQL);
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteTicketSettingJSON(string SubSettingID)
        {
            string SQL = "";
            long Result = 0;
            try
            {
                SQL = @"update Ticket_Setting set Isdeleted=1 ,deletedat=getdate(),
                                IPAddress='" + ClsCommon.GetIPAddress() + "' where SubSettingID=" + SubSettingID;
                Result = clsDataBaseHelper.ExecuteNonQuery(SQL);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during DeleteTicketSettingJSON. The query was executed :", ex.ToString(), SQL, "CommonAjax", "CommonAjax", "");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]

        public ActionResult IsSessionEndJSON(string ReturnURL)
        {
            PostResponse PostResult = new PostResponse();
            if (clsApplicationSetting.IsSessionExpired("LoginID"))
            {
                if (!string.IsNullOrEmpty(ReturnURL))
                {
                    PostResult.RedirectURL = "/Account/Login?ReturnURL=" + clsApplicationSetting.EncryptFriendly(ReturnURL);
                }
                PostResult.Status = true;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult FillTicketSubCategoryJson(long CategoryID)
        {
            string SQL = "select SubCategoryID,Name,RelatedTo from Ticket_SubCategory where CategoryID=" + CategoryID + " and isdeleted=0 and IsActive=1";
            DataTable dt = clsDataBaseHelper.ExecuteDataTable(SQL, "A");
            var result = ClsCommon.ConvertDataTableToJson(dt);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult FillTicketOtherDetailsJson(long SubCategoryID, long LocationGroupID)
        {
            Helpdesk.TicketOtherDet List = new Helpdesk.TicketOtherDet();
            var a = Ticket.GetTicket_AssignmentList(0, SubCategoryID, "Assignment").Where(x => x.LocationGroupID == LocationGroupID && x.IsActive).FirstOrDefault();
            List.AssignedTo = a.PrimaryAssignee;
            List.AssignedToName = a.PrimaryAssigneeName;
            List.TicketPriorityList = Ticket.GetTicket_SMAList(0, SubCategoryID, "SLA").Where(x => x.IsActive).ToList();
            return Json(List, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult MyLeaveAttachmentJSON()
        {
            if (clsApplicationSetting.IsSessionExpired("MyLeaveAttachment"))
            {
                clsApplicationSetting.SetSessionValue("MyLeaveAttachment", "Y");
            }
            else
            {
                clsApplicationSetting.ClearCookiesValue("MyLeaveAttachment");
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult MyTeamLeaveAttachmentJSON()
        {
            if (clsApplicationSetting.IsSessionExpired("MyTeamLeaveAttachment"))
            {
                clsApplicationSetting.SetSessionValue("MyTeamLeaveAttachment", "Y");
            }
            else
            {
                clsApplicationSetting.ClearCookiesValue("MyTeamLeaveAttachment");
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSubSectionJSON(long MPRSubSID)
        {
            MPR.SubSection.SubSecAdd Modal = new MPR.SubSection.SubSecAdd();
            if (MPRSubSID > 0)
            {
                Modal = MPRObj.GetMPRSubSec(MPRSubSID);
            }
            return Json(Modal, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadPendingSMPRJSON()
        {
            return Json(Common_SPU.fnAutoCreateSMPR(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DeleteProjectEMP_SelfMappingJson(string ID)
        {
            int result = 0;
            long IDD = 0;
            long.TryParse(ID, out IDD);
            Common_SPU.fnDelProjectEMP_SelfMapping(IDD);
            result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult AutoSync_EMPwithPMSJson(long FYID)
        {
            var Result = Common_SPU.fnAutoSync_EMPwithPMS(FYID);
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SubmitKPA(long FYID)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "KPA not submit";
            try
            {

                if (FYID != 0)
                {
                    clsDataBaseHelper.ExecuteNonQuery("update PMS_KPA set Approved=0, isdeleted=0 where FYID=" + FYID + " and isdeleted in (0,2)");
                    PostResult.Status = true;
                    PostResult.SuccessMessage = "KPA Submitted Successfully";
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during SetSelfAppraisal. The query was executed :", ex.ToString(), "spu_GetConsultant", "PMSController", "PMSController", "");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult NotifyMPR(string Month)
        {
            CommandResult Result = new CommandResult();
            Result.SuccessMessage = "Notification not send";
            try
            {
                DateTime dt;
                DateTime.TryParse(Month, out dt);
                Result = Common_SPU.fnCreateMail_SMPR_Notify(dt);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during NotifyMPR. The query was executed :", ex.ToString(), "spu_CreateMail_SMPR_Notify", "CommonAjaxController", "CommonAjaxController", "");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult UpdateKPATeamJSON(string PMS_ADID, string PMS_AID, string HOD_Score, string HOD_Comment)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Team appraisal not saved";
            try
            {
                Common_SPU.fnSetPMS_Appraisal_Det_Team(Convert.ToInt64(PMS_ADID), Convert.ToInt64(PMS_AID), "KPA", 0, "", "", HOD_Score, HOD_Comment);
                PostResult.Status = true;
                PostResult.SuccessMessage = "Team appraisal saved successfully";
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during UpdateKPATeamJSON. The query was executed :", ex.ToString(), "UpdateKPATeamJSON", "CommonAjaxController", "CommonAjaxController", "");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult RemoveAppraisalJSON(long TrainingID, string Reason = "")
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Training not deleted";
            try
            {
                clsDataBaseHelper.ExecuteNonQuery("update PMS_Appraisal_Det set isdeleted=1,HOD_Comment='" + Reason + "',deletedat=getdate(),deletedby=" + Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID")) + " where PMS_ADID=" + TrainingID + "");
                PostResult.Status = true;
                PostResult.SuccessMessage = "Training Removed Successfully";
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during RemoveAppraisalJSON. The query was executed :", ex.ToString(), "RemoveAppraisalJSON", "CommonAjaxController", "CommonAjaxController", "");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DeleteJobRoundJSON(long JobDetailsID)
        {
            CommandResult Result = new CommandResult();
            Result.SuccessMessage = "Round not deleted";
            try
            {

                Result = Common_SPU.fnDelJobRound(JobDetailsID);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during DeleteJobRoundJSON. The query was executed :", ex.ToString(), "DeleteJobRoundJSON", "CommonAjaxController", "CommonAjaxController", "");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DeleteInterviewRoundJSON(long REC_InterviewSetID)
        {
            CommandResult Result = new CommandResult();
            Result.SuccessMessage = "Round not deleted";
            try
            {

                Result = Common_SPU.fnSetREC_DelInterviewRound(REC_InterviewSetID);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during DeleteJobRoundJSON. The query was executed :", ex.ToString(), "DeleteInterviewRoundJSON", "CommonAjaxController", "CommonAjaxController", "");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult REC_SyncInterviewRoundJSON(long REC_ReqID)
        {
            CommandResult Result = new CommandResult();
            Result.SuccessMessage = "Round not deleted";
            try
            {

                Result = Common_SPU.fnSetREC_SyncInterviewRound(REC_ReqID);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during REC_SyncInterviewRoundJSON. The query was executed :", ex.ToString(), "fnSetREC_SyncInterviewRound", "CommonAjaxController", "CommonAjaxController", "");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetInterviewSlot(long RoundID)
        {
            List<InterviewSelection.ddList> List = new List<InterviewSelection.ddList>();
            InterviewSelection.ddList obj = new InterviewSelection.ddList();
            string SQL = "select REC_InterviewSetID ,SlotDate,FromTime,ToTime  from REC_InterviewSetting where LinkID=" + RoundID + " and DocType='slot' and isdeleted=0 and IsActive=1";
            DataTable dt = clsDataBaseHelper.ExecuteDataTable(SQL, "A");
            foreach (DataRow item in dt.Rows)
            {
                obj = new InterviewSelection.ddList();
                obj.ID = Convert.ToInt64(item["REC_InterviewSetID"]);
                obj.Name = Convert.ToDateTime(item["SlotDate"]).ToString(DateFormat) + " [" + Convert.ToDateTime(item["FromTime"]).ToString("hh:mm") + "] To [" + Convert.ToDateTime(item["ToTime"]).ToString("hh:mm") + "]";

                List.Add(obj);
            }
            return Json(List, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetEMPList_ByLocationJson(string LocationID)
        {
            List<Exit.EMPList> result = new List<Exit.EMPList>();
            int ID = 0;
            int.TryParse(LocationID, out ID);
            result = Exits.GetEMPList_ByLocation(ID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SetExit_HandoverDefaultJson(string Exit_ID)
        {
            int ID = 0;
            int.TryParse(Exit_ID, out ID);
            PostResponse Result = new PostResponse();
            Result = Exits.SetExit_Handover_Persons_Default(ID);
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetMasterAllData(long ID, string TableName)
        {
            MasterAll.List PostResult = new MasterAll.List();
            PostResult = Common_SPU.GetMasterAllData(ID, TableName);
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetFR_ProspectData(string Prospectid)
        {
            FundRaising.Prospect.List result = new FundRaising.Prospect.List();
            int ID = 0;
            int.TryParse(Prospectid, out ID);
            result = Fund.GetProspectList(ID).FirstOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateFR_ReferralStatusJSon(long ID, string Status)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "something went wrong while updating.";
            if (ClsCommon.UpdateFR_ReferralStatus(Status, ID))
            {
                PostResult.Status = true;
                PostResult.SuccessMessage = " updated successfully";
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadProspect()
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

                    string sDeletedoptions = "0";
                    try
                    {
                        string Token = "", ProspectName = "", ProspectType = "", Sector = "", Company = "", Country = "", KFA = "", State = "", Address = ""
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
                                        PostResult = Common_SPU.FnSetFrProspectImport(Token, ProspectName, ProspectType, Sector, Company, Country, KFA, State, Budget, C3Fit, C3FitScore, Responsible, Accountable, InformedTo, Consented, Website, OtherSupport, Comment, WebLink, ContactPerson, Designation, ContactNo, EmailId, LinkedInId, Address, SecratoryName, SecratoryPhone, SecratoryEmail, SecratoryOtherInfo, IsLast);
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
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadProfileImageJSON(string FolderName, string SaveInTable)
        {
            try
            {
                string _imgname = string.Empty;
                string sFolderPath = "";
                sFolderPath = "/Attachments/";
                long lLoginid = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
                long lAttachmentid = Convert.ToInt64(clsApplicationSetting.GetSessionValue("AttachmentID"));
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    for (int i = 0; i < System.Web.HttpContext.Current.Request.Files.Count; i++)
                    {
                        var pic = System.Web.HttpContext.Current.Request.Files[i];
                        if (pic.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(pic.FileName);
                            string _ext = Path.GetExtension(pic.FileName);
                            lAttachmentid = Common_SPU.fnSetAttachments(lAttachmentid, fileName, _ext, "", lLoginid.ToString(), "PROFILE");

                            clsApplicationSetting.SetSessionValue("AttachmentID", lAttachmentid.ToString());

                            // string CompleteFileName = Common.fnGetOther_FieldName("master_attachment", "FileName", "id", lAttachmentid.ToString(), "");
                            string CompleteFileName = lAttachmentid.ToString() + _ext;
                            var _comPath = Server.MapPath(sFolderPath) + CompleteFileName;

                            var path = _comPath;
                            pic.SaveAs(path);
                        }
                    }

                }
                return Json(Convert.ToString(lAttachmentid), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(Convert.ToString("0"), JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadEmployee()
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
                    _imgname = "EMPLOYEE_UPLOAD";
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

                    string sDeletedoptions = "0";
                    try
                    {
                        string Token = "", emp_code = "", emp_name = "", email = "", MitrUser = "", doj = "", EmploymentTerm = "", Contract_EndDate = "", DepartmentID = "", thematicarea_IDs = "", WorkLocationID = "",
metro = "", design_id = "", JobID = "", emp_status = "", Probation_EndDate = "", doc = "", hod_name = "", SecondaryHODID = "", ed_name = "", HRID = "", AppraiserID = "",
co_ot = "", ResidentialStatus = "", SkillsIDs = "", NoticePeriod = "", dor = "", lastworking_day = "", BankIDS = "", AccountTypeS = "", AccountNameS = "", AccountNoS = "",
BranchNameS = "", BranchAddressS = "", IFSCCodeS = "", SwiftCodeS = "", OtherDetailsS = "", BankIDR = "", AccountTypeR = "", AccountNameR = "", AccountNoR = "", BranchNameR = "",
BranchAddressR = "", IFSCCodeR = "", SwiftCodeR = "", OtherDetailsR = "", PsychometricTest = "", father_name = "", mother_name = "", gender = "", dob = "", marital_status = "",
SpouseName = "", PartnerName = "", NomineeName = "", NomineeRelation = "", children = "", Nationality = "", lane1P1 = "", lane2P1 = "", zip_codeP1 = "", CountryIDP1 = "", StateIDP1 = "",
CityIDP1 = "", lane1P2 = "", lane2P2 = "", zip_codeP2 = "", CountryIDP2 = "", StateIDP2 = "", CityIDP2 = "", SpecialAbility = "", AnyMedicalCondition = "", PhysicianName = "", PhysicianNumber = "",
PhysicianAlternate_No = "", BloodGroup = "", emergContact_no = "", emergContact_Name = "", emergContact_Relation = "", CourseE = "", UniversityE = "", LocationE = "", YearE = "", CourseP = "",
UniversityP = "", LocationP = "", YearP = "", aireline_no = "", frequentflyer_no = "", seat_preference = "", MealPreference = "", CompanyName = "", DOJLE = "", DORLE = "", TotalExperence = "",
AnnualCTC = "", Designation = "", Location = "", EmploymentTermLE = "", ShareSomething = "", IsConsiderIncome = "", IncomeAmount = "", TDSDeduction = "", pan_no = "", NameonPAN = "",
AadharNo = "", NameonAadhar = "", Voterno = "", PassportNo = "", NameonPassport = "", PlaceofIssue = "", PassportExpiryDate = "", DirectorIdentificationNumber = "", DIN = "",
NameonDIN = "", UAN = "", NameonUAN = "", OldPF = "", OldPFNo = "", PIO_OCI = "", NameonPIO_OCI = "", DrivingLicense = "", DLNo = "", NameonDL = "", IssueDate = "", ExpiryDate = "",
PlaceofIssueID = "", MobileP1 = "", MobileP2 = "";
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
                                        emp_code = Convert.ToString(dt.Rows[i][0]);
                                        emp_name = Convert.ToString(dt.Rows[i][1]);
                                        email = Convert.ToString(dt.Rows[i][2]);
                                        MitrUser = Convert.ToString(dt.Rows[i][3]);
                                        doj = Convert.ToString(dt.Rows[i][4]);
                                        EmploymentTerm = Convert.ToString(dt.Rows[i][5]);
                                        Contract_EndDate = Convert.ToString(dt.Rows[i][6]);
                                        DepartmentID = Convert.ToString(dt.Rows[i][7]);
                                        thematicarea_IDs = Convert.ToString(dt.Rows[i][8]);
                                        WorkLocationID = Convert.ToString(dt.Rows[i][9]);
                                        metro = Convert.ToString(dt.Rows[i][10]);
                                        design_id = Convert.ToString(dt.Rows[i][11]);
                                        JobID = Convert.ToString(dt.Rows[i][12]);
                                        emp_status = Convert.ToString(dt.Rows[i][13]);
                                        Probation_EndDate = Convert.ToString(dt.Rows[i][14]);
                                        doc = Convert.ToString(dt.Rows[i][15]);
                                        hod_name = Convert.ToString(dt.Rows[i][16]);
                                        SecondaryHODID = Convert.ToString(dt.Rows[i][17]);
                                        ed_name = Convert.ToString(dt.Rows[i][18]);
                                        HRID = Convert.ToString(dt.Rows[i][19]);
                                        AppraiserID = Convert.ToString(dt.Rows[i][20]);
                                        co_ot = Convert.ToString(dt.Rows[i][21]);
                                        ResidentialStatus = Convert.ToString(dt.Rows[i][22]);
                                        SkillsIDs = Convert.ToString(dt.Rows[i][23]);
                                        NoticePeriod = Convert.ToString(dt.Rows[i][24]);
                                        dor = Convert.ToString(dt.Rows[i][25]);
                                        lastworking_day = Convert.ToString(dt.Rows[i][26]);
                                        BankIDS = Convert.ToString(dt.Rows[i][28]);
                                        AccountTypeS = Convert.ToString(dt.Rows[i][29]);
                                        AccountNameS = Convert.ToString(dt.Rows[i][30]);
                                        AccountNoS = Convert.ToString(dt.Rows[i][31]);
                                        BranchAddressS = Convert.ToString(dt.Rows[i][32]);
                                        IFSCCodeS = Convert.ToString(dt.Rows[i][33]);
                                        SwiftCodeS = Convert.ToString(dt.Rows[i][34]);
                                        OtherDetailsS = Convert.ToString(dt.Rows[i][35]);
                                        BankIDR = Convert.ToString(dt.Rows[i][37]);
                                        AccountTypeR = Convert.ToString(dt.Rows[i][38]);
                                        AccountNameR = Convert.ToString(dt.Rows[i][39]);
                                        AccountNoR = Convert.ToString(dt.Rows[i][40]);
                                        BranchAddressR = Convert.ToString(dt.Rows[i][41]);
                                        IFSCCodeR = Convert.ToString(dt.Rows[i][42]);
                                        SwiftCodeR = Convert.ToString(dt.Rows[i][43]);
                                        OtherDetailsR = Convert.ToString(dt.Rows[i][44]);
                                        mother_name = Convert.ToString(dt.Rows[i][45]);
                                        gender = Convert.ToString(dt.Rows[i][46]);
                                        dob = Convert.ToString(dt.Rows[i][47]);
                                        marital_status = Convert.ToString(dt.Rows[i][48]);
                                        SpouseName = Convert.ToString(dt.Rows[i][49]);
                                        PartnerName = Convert.ToString(dt.Rows[i][50]);
                                        NomineeName = Convert.ToString(dt.Rows[i][51]);
                                        NomineeRelation = Convert.ToString(dt.Rows[i][52]);
                                        children = Convert.ToString(dt.Rows[i][53]);
                                        Nationality = Convert.ToString(dt.Rows[i][54]);
                                        lane1P1 = Convert.ToString(dt.Rows[i][56]);
                                        lane2P1 = Convert.ToString(dt.Rows[i][57]);
                                        zip_codeP1 = Convert.ToString(dt.Rows[i][58]);
                                        CountryIDP1 = Convert.ToString(dt.Rows[i][59]);
                                        StateIDP1 = Convert.ToString(dt.Rows[i][60]);
                                        CityIDP1 = Convert.ToString(dt.Rows[i][61]);

                                        lane1P2 = Convert.ToString(dt.Rows[i][63]);
                                        lane2P2 = Convert.ToString(dt.Rows[i][64]);
                                        zip_codeP2 = Convert.ToString(dt.Rows[i][65]);
                                        CountryIDP2 = Convert.ToString(dt.Rows[i][66]);
                                        StateIDP2 = Convert.ToString(dt.Rows[i][67]);
                                        CityIDP2 = Convert.ToString(dt.Rows[i][68]);

                                        SpecialAbility = Convert.ToString(dt.Rows[i][70]);
                                        AnyMedicalCondition = Convert.ToString(dt.Rows[i][71]);
                                        PhysicianName = Convert.ToString(dt.Rows[i][72]);
                                        PhysicianNumber = Convert.ToString(dt.Rows[i][73]);
                                        PhysicianAlternate_No = Convert.ToString(dt.Rows[i][74]);
                                        BloodGroup = Convert.ToString(dt.Rows[i][75]);
                                        emergContact_no = Convert.ToString(dt.Rows[i][76]);
                                        emergContact_Name = Convert.ToString(dt.Rows[i][77]);
                                        emergContact_Relation = Convert.ToString(dt.Rows[i][78]);

                                        CourseE = Convert.ToString(dt.Rows[i][80]);
                                        UniversityE = Convert.ToString(dt.Rows[i][81]);
                                        LocationE = Convert.ToString(dt.Rows[i][82]);
                                        YearE = Convert.ToString(dt.Rows[i][83]);

                                        CourseP = Convert.ToString(dt.Rows[i][85]);
                                        UniversityP = Convert.ToString(dt.Rows[i][86]);
                                        LocationP = Convert.ToString(dt.Rows[i][87]);
                                        YearP = Convert.ToString(dt.Rows[i][88]);

                                        aireline_no = Convert.ToString(dt.Rows[i][90]);
                                        frequentflyer_no = Convert.ToString(dt.Rows[i][91]);
                                        seat_preference = Convert.ToString(dt.Rows[i][92]);
                                        MealPreference = Convert.ToString(dt.Rows[i][93]);

                                        CompanyName = Convert.ToString(dt.Rows[i][95]);
                                        DOJLE = Convert.ToString(dt.Rows[i][96]);
                                        DORLE = Convert.ToString(dt.Rows[i][97]);
                                        TotalExperence = Convert.ToString(dt.Rows[i][98]);
                                        AnnualCTC = Convert.ToString(dt.Rows[i][99]);
                                        Designation = Convert.ToString(dt.Rows[i][100]);
                                        Location = Convert.ToString(dt.Rows[i][101]);
                                        EmploymentTermLE = Convert.ToString(dt.Rows[i][102]);
                                        ShareSomething = Convert.ToString(dt.Rows[i][103]);
                                        IsConsiderIncome = Convert.ToString(dt.Rows[i][104]);
                                        IncomeAmount = Convert.ToString(dt.Rows[i][105]);
                                        TDSDeduction = Convert.ToString(dt.Rows[i][106]);

                                        pan_no = Convert.ToString(dt.Rows[i][108]);
                                        NameonPAN = Convert.ToString(dt.Rows[i][109]);
                                        AadharNo = Convert.ToString(dt.Rows[i][110]);
                                        NameonAadhar = Convert.ToString(dt.Rows[i][111]);
                                        Voterno = Convert.ToString(dt.Rows[i][112]);
                                        PassportNo = Convert.ToString(dt.Rows[i][113]);
                                        NameonPassport = Convert.ToString(dt.Rows[i][114]);
                                        PlaceofIssue = Convert.ToString(dt.Rows[i][115]);
                                        PassportExpiryDate = Convert.ToString(dt.Rows[i][116]);
                                        DirectorIdentificationNumber = Convert.ToString(dt.Rows[i][117]);
                                        DIN = Convert.ToString(dt.Rows[i][118]);
                                        NameonDIN = Convert.ToString(dt.Rows[i][119]);
                                        UAN = Convert.ToString(dt.Rows[i][120]);
                                        NameonUAN = Convert.ToString(dt.Rows[i][121]);
                                        OldPF = Convert.ToString(dt.Rows[i][122]);
                                        OldPFNo = Convert.ToString(dt.Rows[i][123]);
                                        PIO_OCI = Convert.ToString(dt.Rows[i][124]);
                                        NameonPIO_OCI = Convert.ToString(dt.Rows[i][125]);
                                        DrivingLicense = Convert.ToString(dt.Rows[i][126]);
                                        DLNo = Convert.ToString(dt.Rows[i][127]);
                                        NameonDL = Convert.ToString(dt.Rows[i][128]);
                                        IssueDate = Convert.ToString(dt.Rows[i][129]);
                                        ExpiryDate = Convert.ToString(dt.Rows[i][130]);
                                        PlaceofIssueID = Convert.ToString(dt.Rows[i][131]);
                                        MobileP1 = Convert.ToString(dt.Rows[i][132]);
                                        MobileP2 = Convert.ToString(dt.Rows[i][133]);
                                        PostResult = Common_SPU.FnSetEmployeeImport(Token, emp_code, emp_name, email, MitrUser, doj, EmploymentTerm, Contract_EndDate, DepartmentID, thematicarea_IDs, WorkLocationID, metro, design_id, JobID, emp_status, Probation_EndDate, doc, hod_name, SecondaryHODID, ed_name, HRID, AppraiserID, co_ot, ResidentialStatus, SkillsIDs, NoticePeriod, dor, lastworking_day, BankIDS, AccountTypeS, AccountNameS, AccountNoS, BranchNameS, BranchAddressS, IFSCCodeS, SwiftCodeS, OtherDetailsS, BankIDR, AccountTypeR, AccountNameR, AccountNoR, BranchNameR, BranchAddressR, IFSCCodeR, SwiftCodeR, OtherDetailsR, PsychometricTest, father_name, mother_name, gender, dob, marital_status,
SpouseName, PartnerName, NomineeName, NomineeRelation, children, Nationality, lane1P1, lane2P1, zip_codeP1, CountryIDP1, StateIDP1, CityIDP1, lane1P2, lane2P2, zip_codeP2, CountryIDP2, StateIDP2, CityIDP2, SpecialAbility, AnyMedicalCondition, PhysicianName, PhysicianNumber, PhysicianAlternate_No, BloodGroup, emergContact_no, emergContact_Name, emergContact_Relation, CourseE, UniversityE, LocationE, YearE, CourseP, UniversityP, LocationP, YearP, aireline_no, frequentflyer_no, seat_preference, MealPreference, CompanyName, DOJLE, DORLE, TotalExperence, AnnualCTC, Designation, Location, EmploymentTermLE, ShareSomething,
IsConsiderIncome, IncomeAmount, TDSDeduction, pan_no, NameonPAN, AadharNo, NameonAadhar, Voterno, PassportNo, NameonPassport, PlaceofIssue, PassportExpiryDate, DirectorIdentificationNumber, DIN, NameonDIN, UAN, NameonUAN, OldPF, OldPFNo, PIO_OCI, NameonPIO_OCI, DrivingLicense, DLNo, NameonDL, IssueDate, ExpiryDate, PlaceofIssueID, MobileP1, MobileP2, IsLast);
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
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult PerformanceAppraisalReport(string src)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.Status = false;
            PostResult.SuccessMessage = "";
            ViewBag.src = src;
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long FYID = 0;
            EMPID = Convert.ToInt32(GetQueryString[2].ToString());
            FYID = Convert.ToInt32(GetQueryString[3].ToString());
            string Filename = "";
            if (ModelState.IsValid)
            {
                Filename = Export.GetAppraisal_RPT(EMPID, FYID);
                PostResult.Status = true;
                PostResult.SuccessMessage = Filename;

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult RefreshSalaryComponentAmtFromConsolidate(string src, string Emptype, string Date)
        {
            long Retid = 0;
            try
            {
                Retid = Common_SPU.FNSaveSalaryComponentAmtFromConsolidate(0, Date);
                return Json(Convert.ToString(Retid), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(Convert.ToString("0"), JsonRequestBehavior.AllowGet);

            }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetJobTypeFill(long JobId)
        {
            var Data = Master.GetJobList(JobId);

            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult _Displayreport(long REC_InterviewSetID)
        {

            var data = Rec.GetInterviewRound_AttachementList(REC_InterviewSetID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult PrintUserReport_CommonJson(long EmpId, long TravelId)
        {
            PostResponse PostResult = new PostResponse();

            if (EmpId == Convert.ToInt64(clsApplicationSetting.GetSessionValue("EMPID")))
            {
                PostResult = Travel.GetPrintTERUser(EmpId, TravelId);
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedBankdDetails(string EmpId, string DocType)
        {
            return Json(Common_SPU.fnSetBankDetailsSetAction(Convert.ToInt64(EmpId), DocType), JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedPersonalDetailsJson(long EmpId, string Column, string DocType, string ColumnUpdate, long UID)
        {
            return Json(Common_SPU.fnSetPersonalDetailsSetAction(EmpId, Column, DocType, ColumnUpdate, UID), JsonRequestBehavior.AllowGet);
            //return Json(null, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult RejectPersonalDetailsJson(long EmpId, string Column, string DocType, string ColumnUpdate, long UID)
        {
            return Json(Common_SPU.fnSetPersonalDetailsRejectAction(EmpId, Column, DocType, ColumnUpdate, UID), JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedPersonalAirlineDetailsJson(long EmpId, string AirlineName, string DocType, string FlyerNo, string AirId)
        {
            return Json(Common_SPU.fnSetPersonalTravelPreferenceSetAction(EmpId, AirlineName, DocType, FlyerNo, Convert.ToInt64(AirId)), JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult RejectdPersonalAirlineDetailsJson(long EmpId, string AirlineName, string DocType, string FlyerNo, string AirId)
        {
            return Json(Common_SPU.fnRejectSetPersonalTravelPreferenceSetAction(EmpId, AirlineName, DocType, FlyerNo, Convert.ToInt64(AirId)), JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedPersonalQualificationDetailsJson(long EmpId, string Course, string DocType, string University, string Location, string Year, string QId)
        {
            return Json(Common_SPU.fnSetPersonalQualificationSetAction(EmpId, Course, DocType, University, Location, Year, Convert.ToInt64(QId)), JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult RejectPersonalQualificationDetailsJson(long EmpId, string Course, string DocType, string University, string Location, string Year, string QId)
        {
            return Json(Common_SPU.fnRejectSetPersonalQualificationSetAction(EmpId, Course, DocType, University, Location, Year, Convert.ToInt64(QId)), JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ApprovedPersonalDocDetailsJson(long AttachmentID, long MUID, long EMPID, string DocType)
        {

            return Json(Common_SPU.fnApprovedetPersonaDocSetAction(AttachmentID, MUID, EMPID, DocType), JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult RejectPersonalDocDetailsJson(long AttachmentID, long MUID, long EMPID, string DocType)
        {
            return Json(Common_SPU.fnRejectetPersonaDocSetAction(AttachmentID, MUID, EMPID, DocType), JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetDropDownListJsonGR(Mitr.Model.GetResponseModel Modal)
        {
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return Json(Mitr.DAL.ClsCommon.GetDropDownList(Modal), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDropDownLevelData(string id)
        {
            List<MPRReports.SubSection> Modal = new List<MPRReports.SubSection>();

            if (!string.IsNullOrEmpty(id))
            {
                Modal = Master.GetMPR_Reports_SubHeader(id);
            }
            return new JsonResult { Data = Modal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetDropDownLevel(string id)
        {
            SubCategoryAssigneeDetails Modal = new SubCategoryAssigneeDetails();
            Mitr.Model.GetResponseModel employee = new Mitr.Model.GetResponseModel();
            if (!string.IsNullOrEmpty(id))
            {
                employee.Doctype = "LEVELFILTER";
                employee.ID = 0;
                employee.AdditionalID = 0;
                employee.AdditionalID1 = 0;
                Mitr.DAL.ClsCommon.MultipleLoginID = (Mitr.DAL.ClsCommon.MultipleLoginID == "") ? (LoginID + "," + id) : (Mitr.DAL.ClsCommon.MultipleLoginID + "," + id);
                employee.MultipleLoginID = Mitr.DAL.ClsCommon.MultipleLoginID;
                if (!string.IsNullOrEmpty(Convert.ToString(ViewBag.Level1List)))
                    ViewBag.Level1List.Clear();
                Modal.Level1List = Mitr.DAL.ClsCommon.GetDropDownListFilterLevelName(employee);
                Modal.Level2List = Mitr.DAL.ClsCommon.GetDropDownListFilterLevelName(employee);
                Modal.Level3List = Mitr.DAL.ClsCommon.GetDropDownListFilterLevelName(employee);
            }

            return Json(Modal, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OtherEarningEntrydoc()
        {
            PostResponse PostResult = new PostResponse();
            long countuploaded = 0;
            string _imgname = string.Empty;
            string retvalue = "";
            string saveloc = "/Attachments/";
            SalaryStructure.OtherEarningEntry modal = new SalaryStructure.OtherEarningEntry();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string date = System.Web.HttpContext.Current.Request.Form["Date"];
                string EmpType = System.Web.HttpContext.Current.Request.Form["Emptype"];
                var pic = System.Web.HttpContext.Current.Request.Files["FileAttachment"];
                if (pic.ContentLength > 0)
                {

                    modal = Salary.GetOtherEarningEntry(EmpType, date);
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);
                    var _comPath = AppDomain.CurrentDomain.BaseDirectory + fileName;
                    var imgname = Path.GetFileNameWithoutExtension(_comPath);
                    _imgname = "EMPLOYEE_Other Earning Entry";
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

                    string sDeletedoptions = "0";
                    try
                    {
                        string Token = "";


                        int IsLast = 0;
                        Token = ClsCommon.RandomString() + DateTime.Now.ToString();



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
                                        var dataemplist = modal.Emplist.Where(x => x.emp_code == (dt.Rows[i][0].ToString()).Trim()).FirstOrDefault();
                                        string[] columnNames = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
                                        if (dataemplist != null)
                                        {
                                           
                                            var component = modal.Component.Where(x => x.Empid == dataemplist.Empid).ToList();
                                            int count = 1;
                                          //  int m = 0;
                                            for (int k = 1; k < columnNames.Length; k++)
                                            {
                                                for (int j = 0; j < component.Count; j++)
                                                {
                                                    if (columnNames[k].Trim() == component[j].Component.Trim())
                                                    {
                                                        component[j].Amt = float.Parse(dt.Rows[i][count].ToString());
                                                        count++;
                                                       // m++;
                                                        break;
                                                    }
                                                }
                                            }




                                            countuploaded++;
                                        }


                                    }

                                }
                                catch
                                {
                                }
                            }
                            //TempData["OtherEarningData"] = modal;
                        }

                        //if (countuploaded > 0)
                        //{

                        // PostResult.SuccessMessage = countuploaded.ToString() + " Records Uploaded Successfully";
                        PostResult.SuccessMessage = " Records Uploaded Successfully";
                        PostResult.ID = countuploaded;
                        PostResult.OtherEarningEntry = modal;
                        // TempData["Success"] = "Y";
                        //}
                        //else
                        //{
                        //    PostResult.SuccessMessage = "No Records Uploaded Successfully";
                        //    PostResult.ID = countuploaded;
                        //    PostResult.OtherEarningEntry = modal;
                        //}


                    }
                    catch (Exception ex)
                    {
                        PostResult.SuccessMessage = ex.Message.ToString();
                        PostResult.ID = 0;
                    }
                    //  return Res;

                }
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeductionEntrydoc()
        {
            PostResponse PostResult = new PostResponse();
            long countuploaded = 0;
            string _imgname = string.Empty;
            string retvalue = "";
            string saveloc = "/Attachments/";
            SalaryStructure.DeductionEntry modal = new SalaryStructure.DeductionEntry();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string date = System.Web.HttpContext.Current.Request.Form["Date"];
                string EmpType = System.Web.HttpContext.Current.Request.Form["Emptype"];
                var pic = System.Web.HttpContext.Current.Request.Files["FileAttachment"];
                if (pic.ContentLength > 0)
                {

                    modal = Salary.GetDeductionEntry(EmpType, date);
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);
                    var _comPath = AppDomain.CurrentDomain.BaseDirectory + fileName;
                    var imgname = Path.GetFileNameWithoutExtension(_comPath);
                    _imgname = "EMPLOYEE_Other Earning Entry";
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

                    string sDeletedoptions = "0";
                    try
                    {
                        string Token = "";


                        int IsLast = 0;
                        Token = ClsCommon.RandomString() + DateTime.Now.ToString();

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
                                        var dataemplist = modal.Emplist.Where(x => x.emp_code == (dt.Rows[i][0].ToString()).Trim()).FirstOrDefault();
                                        string[] columnNames = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
                                        if (dataemplist != null)
                                        {

                                            var component = modal.Component.Where(x => x.Empid == dataemplist.Empid).ToList();
                                            int count = 1;
                                            //for (int j = 0; j < component.Count; j++)
                                            //{

                                            //    component[j].Amt = float.Parse(dt.Rows[i][count].ToString());
                                            //    count++;

                                            //}
                                            for (int k = 1; k < columnNames.Length; k++)
                                            {
                                                for (int j = 0; j < component.Count; j++)
                                                {
                                                    if (columnNames[k].Trim() == component[j].Component.Trim())
                                                    {
                                                        component[j].Amt = float.Parse(dt.Rows[i][count].ToString());
                                                        count++;
                                                        // m++;
                                                        break;
                                                    }
                                                }
                                            }

                                            countuploaded++;
                                        }




                                    }

                                }
                                catch
                                {
                                }
                            }
                            //TempData["OtherEarningData"] = modal;
                        }

                        //if (countuploaded > 0)
                        //{

                        // PostResult.SuccessMessage = countuploaded.ToString() + " Records Uploaded Successfully";
                        PostResult.SuccessMessage = " Records Uploaded Successfully";
                        PostResult.ID = countuploaded;
                        PostResult.DeductionEntry = modal;
                        // TempData["Success"] = "Y";
                        //}
                        //else
                        //{
                        //    PostResult.SuccessMessage = "No Records Uploaded Successfully";
                        //    PostResult.ID = countuploaded;
                        //    PostResult.DeductionEntry = modal;
                        //}


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
        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult SaveSalaryData(List<SalaryDump> TempSalary)
        //{
        //    string jsondata = new JavaScriptSerializer().Serialize(TempSalary);
        //    string path = Server.MapPath("~/Attachments/Salary/");
        //    // Write that JSON to txt file,  
        //    System.IO.File.WriteAllText(path + "Sallary.txt", jsondata);
        //    return Json(1, JsonRequestBehavior.AllowGet);
        //}


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OtherBenefitPayment()
        {
            PostResponse PostResult = new PostResponse();
            long countuploaded = 0;
            string _imgname = string.Empty;
            string retvalue = "";
            string saveloc = "/Attachments/";
      
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string FYID = System.Web.HttpContext.Current.Request.Form["FYId"];
                string Date = System.Web.HttpContext.Current.Request.Form["Date"];
                string PaidDate = Convert.ToString(Convert.ToDateTime(Date));
                string EMP_ID = System.Web.HttpContext.Current.Request.Form["EMPID"];
                var pic = System.Web.HttpContext.Current.Request.Files["FileAttachment"];
                long empId = 0;
                if(EMP_ID=="")
                {
                    empId=0;
                }
                else
                {
                    empId = Convert.ToInt64(EMP_ID);
                }
                if (pic.ContentLength > 0)
                {

                    long EMPID = 0;
                    SalaryStructure.OtherBenefitPayment Modal = new SalaryStructure.OtherBenefitPayment();
                    List<SalaryStructure.OtherBenefitPayment> List = new List<SalaryStructure.OtherBenefitPayment>();
                    List<SalaryStructure.OtherBenefitPayment.OtherBenefitPaymentComponent> ComponentList = new List<SalaryStructure.OtherBenefitPayment.OtherBenefitPaymentComponent>();
                    EMPID = (empId > EMPID) ? empId : EMPID;
                    List = Salary.GetOtherBenefitPaymentList(Convert.ToInt64(FYID), EMPID, PaidDate);
                    ComponentList = Salary.GetOtherBenefitPaymentComponentList(Convert.ToInt64(FYID), EMPID, PaidDate);
                    Modal.OtherBenefitPaymentList = List;
                    Modal.OtherBenefitPaymentComponentList = ComponentList;
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);
                    var _comPath = AppDomain.CurrentDomain.BaseDirectory + fileName;
                    var imgname = Path.GetFileNameWithoutExtension(_comPath);
                    _imgname = "EMPLOYEE_Other Earning Entry";
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

                    string sDeletedoptions = "0";
                    try
                    {
                        string Token = "";


                        int IsLast = 0;
                        Token = ClsCommon.RandomString() + DateTime.Now.ToString();

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
                                        var dataemplist = List.Where(x => x.EmpCode == (dt.Rows[i][0].ToString()).Trim()).FirstOrDefault();
                                        string[] columnNames = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
                                        if (dataemplist != null)
                                        {

                                            var component = ComponentList.Where(x => x.EmpID == dataemplist.EmpID).ToList();
                                            int count = 1;
                                            //for (int j = 0; j < component.Count; j++)
                                            //{

                                            //    component[j].Amt = float.Parse(dt.Rows[i][count].ToString());
                                            //    count++;

                                            //}
                                            for (int k = 1; k < columnNames.Length; k++)
                                            {
                                                for (int j = 0; j < component.Count; j++)
                                                {
                                                    if (columnNames[k].Trim() == component[j].Component.Trim())
                                                    {
                                                        component[j].PaidAmount = Convert.ToDecimal(dt.Rows[i][count].ToString());
                                                        count++;
                                                        // m++;
                                                        break;
                                                    }
                                                }
                                            }

                                            countuploaded++;
                                        }




                                    }

                                }
                                catch
                                {
                                }
                            }
                            //TempData["OtherEarningData"] = modal;
                        }

                        //if (countuploaded > 0)
                        //{

                        // PostResult.SuccessMessage = countuploaded.ToString() + " Records Uploaded Successfully";
                        PostResult.SuccessMessage = " Records Uploaded Successfully";
                        PostResult.ID = countuploaded;
                        PostResult.otherBenefitPayment = Modal;
                        // TempData["Success"] = "Y";
                        //}
                        //else
                        //{
                        //    PostResult.SuccessMessage = "No Records Uploaded Successfully";
                        //    PostResult.ID = countuploaded;
                        //    PostResult.DeductionEntry = modal;
                        //}


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

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadSubCategoryList()
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
                    _imgname = "SubCategory_UPLOAD";
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

                    string sDeletedoptions = "0";
                    try
                    {
                        string CategoryName = "", SubCategoryName = "", Description = "", Anonymous = "", Token="", Location="", Primary="", Levelone="", Leveltwo = "", Levelthree = "";

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
                                        CategoryName = Convert.ToString(dt.Rows[i][0]);
                                        SubCategoryName = Convert.ToString(dt.Rows[i][1]);
                                        Location = Convert.ToString(dt.Rows[i][2]);
                                        Primary = Convert.ToString(dt.Rows[i][3]);
                                        Levelone = Convert.ToString(dt.Rows[i][4]);
                                        Leveltwo = Convert.ToString(dt.Rows[i][5]);
                                        Levelthree = Convert.ToString(dt.Rows[i][6]);
                                        //PostResult = Common_SPU.FnSetSubcateImport(Token, CategoryName, SubCategoryName, Description, "",  IsLast);
                                        PostResult = Common_SPU.FnSetSubcateImportAssign(Token, CategoryName, SubCategoryName, Location, Primary, Levelone, Leveltwo, Levelthree, IsLast);
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
    }

}