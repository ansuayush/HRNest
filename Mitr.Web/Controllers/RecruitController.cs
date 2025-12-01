using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using static Mitr.Models.Exit.Req_Process;
using static Mitr.Models.InterviewSelection;
using static Mitr.Models.MPRReports;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class RecruitController : Controller
    {
        IRecruit_Helper Rec;
        IMasterHelper Master;
        public RecruitController()
        {
            Rec = new Recruit_Modal();
            Master = new MasterModal();
        }

        public ActionResult Pendancy(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _Pendancy(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_PendancyDirect(Modal.Approve);
            return PartialView(ds);
        }

        [HttpPost]
        public ActionResult _ViewBudget(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProjectDetID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ProjectDetID, out ID);
            DataSet ds = new DataSet();
            if (ID > 0)
            {
                ds = Common_SPU.fnGetREC_ViewBudget(ID);
            }
            return PartialView(ds);
        }

        public ActionResult Initiate(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProjectDetID = GetQueryString[2];
            ViewBag.ProjectID = GetQueryString[3];
            long ID = 0, ProjectID;
            long.TryParse(ViewBag.ProjectDetID, out ID);
            long.TryParse(ViewBag.ProjectID, out ProjectID);
            Recruit.Initiate Modal = new Recruit.Initiate();
            Modal = Rec.GetREC_Initiate(ProjectID);
            Modal.ProjectDetailID = Convert.ToInt64(ViewBag.ProjectDetID);
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "WorkLocation";
            ViewBag.WorkLocation = ClsCommon.GetDropDownList(getDropDown);
            getDropDown.Doctype = "skills";
            ViewBag.Skills = ClsCommon.GetDropDownList(getDropDown);
            ViewBag.JobId = Master.GetJobList(0);



            return View(Modal);
        }

        [HttpPost]

        [ValidateInput(false)]
        public ActionResult Initiate(string src, Recruit.Initiate Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProjectDetID = GetQueryString[2];
            long ProjectDetID = 0;
            long.TryParse(ViewBag.ProjectDetID, out ProjectDetID);
            PostResult.SuccessMessage = "Initiate Request Can't Update";
            if (string.IsNullOrEmpty(Modal.Experience))
            {
                PostResult.SuccessMessage = " Experience can't be blank";
                ModelState.AddModelError("basic-addon1", PostResult.SuccessMessage);
            }
            if (string.IsNullOrEmpty(Modal.Qualification))
            {
                PostResult.SuccessMessage = "Qualification can't be blank";
                ModelState.AddModelError("Qualification", PostResult.SuccessMessage);
            }
            if (string.IsNullOrEmpty(Modal.JobDescription))
            {
                PostResult.SuccessMessage = "Job Description can't be blank";
                ModelState.AddModelError("JobDescription", PostResult.SuccessMessage);
            }

            if (ModelState.IsValid)
            {

                string Skills = "";
                Skills = string.Join(",", Modal.SkillsID);
                //PostResult = Common_SPU.fnSetREC_Initiate(0, Modal.ProjectDetailID, Modal.Job_SubTitle, Modal.DueDate, Modal.JobDescription, Modal.Qualification, Skills, Modal.Experience, Modal.LocationID, Modal.JobID, Modal.Time_Per);
                PostResult = Common_SPU.fnSetREC_Initiate(0, Modal.ProjectDetailID, Modal.Job_SubTitle, Modal.DueDate, Modal.JobDescription, Modal.Qualification, Skills, Modal.Experience, Modal.LocationID, Modal.JobID, Modal.Time_Per, Modal.ProjectID);
                PostResult.RedirectURL = "/Recruit/Pendancy?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruit/Pendancy");

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Requests(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _Requests(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_RequestsListDirect(Modal.Approve);
            return PartialView(ds);
        }

        public ActionResult FillRequests(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.REC_ReqID, out ID);
            Recruit.Request.Fill obj = new Recruit.Request.Fill();
            obj = Rec.GetREC_Request(ID);

            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "skills";
            ViewBag.Skills = ClsCommon.GetDropDownList(getDropDown);

            return View(obj);
        }


        public ActionResult ViewRequests(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.REC_ReqID, out ID);
            Recruit.Request.View obj = new Recruit.Request.View();
            obj = Rec.GetREC_Request_View(ID);
            return PartialView(obj);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FillRequests(string src, Recruit.Request.Fill Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Recruitment Can't Update";

            if (Modal.REC_Type == "Internal" && Modal.Project_TagID != null)
            {
                PostResult.SuccessMessage = "Project Vacancy Tagging can be use in case of External Process Only";
                ModelState.AddModelError("FinalApproverID", PostResult.SuccessMessage);
            }
            else if (Modal.REC_Type == "Internal" && Modal.RecommendersID == null)
            {
                PostResult.SuccessMessage = "Staff Category can't be blank";
                ModelState.AddModelError("Staff_Cat", PostResult.SuccessMessage);
            }
            else if (Modal.REC_Type == "Internal" && (Modal.FinalApproverID == null || Modal.FinalApproverID == 0))
            {
                PostResult.SuccessMessage = "Final Approver can't be blank";
                ModelState.AddModelError("FinalApproverID", PostResult.SuccessMessage);
            }
            else if (Modal.REC_Type == "External" && string.IsNullOrEmpty(Modal.Staff_Cat))
            {
                PostResult.SuccessMessage = "Staff Category can't be blank";
                ModelState.AddModelError("Staff_Cat", PostResult.SuccessMessage);
            }
            else if (Modal.REC_Type == "Direct" && string.IsNullOrEmpty(Modal.Staff_Cat))
            {
                PostResult.SuccessMessage = "Staff Category can't be blank";
                ModelState.AddModelError("Staff_Cat", PostResult.SuccessMessage);
            }

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    string Skills = "", Project_Tag = "";
                    Skills = string.Join(",", Modal.SkillsID);
                    if (Modal.Project_TagID != null)
                    {
                        Project_Tag = string.Join(",", Modal.Project_TagID);
                    }
                    PostResult = Common_SPU.fnSetREC_FillRequest(REC_ReqID, Modal.Job_SubTitle, Modal.DueDate, Modal.JobDescription, Modal.Qualification, Skills, Modal.Experience, Modal.Staff_Cat, Modal.REC_Type, Project_Tag);

                    if (PostResult.Status && Modal.REC_Type == "Internal")
                    {
                        string RecommendersID = "";
                        long Id = 0;
                        RecommendersID = string.Join(",", Modal.RecommendersID);
                        if (!string.IsNullOrEmpty(RecommendersID))
                        {
                            if (RecommendersID.Contains(','))
                            {
                                int count = 0;
                                foreach (var item in RecommendersID.Split(','))
                                {
                                    count++;
                                    long.TryParse(item, out Id);
                                    Common_SPU.fnSetREC_IApprovers(REC_ReqID, Id, "Recommenders", count, 1);
                                }
                            }
                            else
                            {
                                long.TryParse(RecommendersID, out Id);
                                Common_SPU.fnSetREC_IApprovers(REC_ReqID, Id, "Recommenders", 1, 1);
                            }
                        }
                        if (Modal.FinalApproverID != 0)
                        {
                            Common_SPU.fnSetREC_IApprovers(REC_ReqID, Modal.FinalApproverID ?? 0, "Final", 0, 1);
                        }
                        // Fire mail
                        Common_SPU.fnCreateMail_REC_IApprovers(REC_ReqID);
                    }
                }
            }
            if (PostResult.Status)
            {
                TempData["Success"] = "Y";
                TempData["SuccessMsg"] = PostResult.SuccessMessage;
                PostResult.RedirectURL = "/Recruit/Requests?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruit/Requests");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _IAvailStaffList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.PillarID = GetQueryString[3];
            ViewBag.LocationID = GetQueryString[4];
            ViewBag.JobID = GetQueryString[5];
            long REC_ReqID = 0, PillarID = 0, LocationID = 0, JobID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            long.TryParse(ViewBag.PillarID, out PillarID);
            long.TryParse(ViewBag.LocationID, out LocationID);
            long.TryParse(ViewBag.JobID, out JobID);
            List<Recruit.InternalStaff> List = new List<Recruit.InternalStaff>();
            List = Rec.GetIAvailStaff(REC_ReqID, PillarID, LocationID, JobID);
            return PartialView(List);
        }


        [HttpPost]
        public ActionResult _IAvailStaffList(string src, List<InternalStaff> Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Internal Staff Can't Update";
            if (Modal == null || Modal.Where(x => !string.IsNullOrEmpty(x.Chck)).ToList().Count == 0)
            {
                PostResult.SuccessMessage = "Please Select Atleast one employee";
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    foreach (var item in Modal.Where(x => !string.IsNullOrEmpty(x.Chck)).ToList())
                    {
                        PostResult = Common_SPU.fnSetREC_IStaf(REC_ReqID, item.EmpID, item.Relocation, item.JobTitle, item.Pillar, item.RelocationByHR, item.JobTitleByHR, item.PillarByHR, 0, 1);

                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _IStaffList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_IStaff(REC_ReqID);
            return PartialView(ds);
        }


        public ActionResult Preference(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _Preference(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_IPreferenceList(Modal.Approve);
            return PartialView(ds);
        }

        public ActionResult SelectPreference(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            List<Recruit.Recom_Preference> List = new List<Recruit.Recom_Preference>();
            List = Rec.GetREC_Recom_Preference(REC_ReqID);
            return View(List);
        }
        [HttpPost]
        public ActionResult SelectPreference(string src, List<Recruit.Recom_Preference> Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Preference Can't Update";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    foreach (var item in Modal.ToList())
                    {
                        PostResult = Common_SPU.fnSetREC_IPreferences(REC_ReqID, item.EmpID, item.comment, item._Preference);
                    }
                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Recruit/Preference?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruit/Preference");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult FinalPreference(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _FinalPreference(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_IFinalList(Modal.Approve);
            return PartialView(ds);
        }

        public ActionResult SelectFinal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            Recruit.Final_Preference List = new Recruit.Final_Preference();
            List = Rec.GetREC_Final_Preference(REC_ReqID);
            return View(List);
        }
        [HttpPost]
        public ActionResult SelectFinal(string src, Recruit.Final_Preference Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Preference Can't Update";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    PostResult = Common_SPU.fnSetREC_IFinalPreferences(REC_ReqID, Modal.ApproveCandidate, Modal.Remarks);
                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Recruit/FinalPreference?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruit/FinalPreference");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult ERequests(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            EXRecruit.Tabs Modal = new EXRecruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ERequests(string src, EXRecruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.spu_GetREC_ERequestsList(Modal.Approve);
            return PartialView(ds);
        }
        public ActionResult _EViewRequests(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.IsEdit = GetQueryString[3];
            long ID = 0;
            long.TryParse(ViewBag.REC_ReqID, out ID);
            EXRecruit.View obj = new EXRecruit.View();
            obj = Rec.GetEREC_Request_View(ID);
            return PartialView(obj);
        }

        [HttpPost]
        public ActionResult SetProjectTagging(string src, FormCollection Collection, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Tags Can't Update";
            if (Command == "Save")
            {
                string Project_TagID = "";
                if (Collection.GetValue("Project_TagID") != null)
                {
                    Project_TagID = Collection.GetValue("Project_TagID").AttemptedValue;
                }
                PostResult = Common_SPU.fnSetREC_ChangeTag(REC_ReqID, Project_TagID);
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult EVacancyHR(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.REC_ReqID, out ID);
            EXRecruit.Vacancy.HR obj = new EXRecruit.Vacancy.HR();
            obj = Rec.GetVacancy_HR(ID);
            return View(obj);
        }

        [HttpPost]
        public ActionResult EVacancyHR(string src, EXRecruit.Vacancy.HR Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Vacancy Can't Update";
            if (Command == "Save")
            {
                ModelState.Clear();
            }
            else if (Command == "Submit" && Modal.Upload == null && Modal.AttachmentID == 0)
            {
                ModelState.AddModelError("Upload", "Attachment Can't be blank");
                PostResult.SuccessMessage = "Attachment Can't be blank";
            }
            if (ModelState.IsValid)
            {

                if (Modal.Upload != null)
                {
                    var RvFile = clsApplicationSetting.ValidateFile(Modal.Upload);
                    if (RvFile.IsValid)
                    {
                        Modal.AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentID, RvFile.FileName, RvFile.FileExt, "", REC_ReqID.ToString(), "External Recurit HR");
                        if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt)))
                        {
                            System.IO.File.Delete("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt);
                        }
                        Modal.Upload.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt));
                    }
                    else
                    {
                        PostResult.Status = RvFile.IsValid;
                        PostResult.SuccessMessage = RvFile.Message;
                        return Json(PostResult, JsonRequestBehavior.AllowGet);
                    }
                }

                PostResult = Common_SPU.fnSetREC_EVacancyAnno_HR(REC_ReqID, Modal.HRVacancyDes, Modal.AttachmentID, Command);
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Recruit/ERequests?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruit/ERequests");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ECommRequests(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            EXRecruit.Tabs Modal = new EXRecruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ECommRequests(string src, EXRecruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_EVacancyAnno_COMMList(Modal.Approve);
            return PartialView(ds);
        }

        public ActionResult EVacancy_Comm(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.REC_ReqID, out ID);
            EXRecruit.Vacancy.Comm obj = new EXRecruit.Vacancy.Comm();
            obj = Rec.GetVacancy_Comm(ID);
            return View(obj);

        }

        [HttpPost]
        public ActionResult EVacancy_Comm(string src, EXRecruit.Vacancy.Comm Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Vacancy Can't Update";
            if (Command == "Save")
            {
                ModelState.Clear();
            }
            else if (Command == "Submit" && Modal.Upload == null && Modal.CommHRAttachID == 0)
            {
                ModelState.AddModelError("Upload", "Attachment Can't be blank");
                PostResult.SuccessMessage = "Attachment Can't be blank";
            }
            if (ModelState.IsValid)
            {

                if (Modal.Upload != null)
                {
                    var RvFile = clsApplicationSetting.ValidateFile(Modal.Upload);
                    if (RvFile.IsValid)
                    {
                        Modal.CommHRAttachID = Common_SPU.fnSetAttachments(Modal.CommHRAttachID, RvFile.FileName, RvFile.FileExt, "", REC_ReqID.ToString(), "External Recurit COMM");
                        if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.CommHRAttachID + RvFile.FileExt)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/Attachments/" + Modal.CommHRAttachID + RvFile.FileExt));
                        }
                        Modal.Upload.SaveAs(Server.MapPath("~/Attachments/" + Modal.CommHRAttachID + RvFile.FileExt));
                    }
                    else
                    {
                        PostResult.Status = RvFile.IsValid;
                        PostResult.SuccessMessage = RvFile.Message;
                        return Json(PostResult, JsonRequestBehavior.AllowGet);
                    }
                }

                PostResult = Common_SPU.fnSetREC_EVacancyAnno_COMM(REC_ReqID, Modal.CommVacancyDes, Modal.CommHRAttachID, Command);
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Recruit/ECommRequests?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruit/ECommRequests");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult EVacancyAnnouncement(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            EXRecruit.Tabs Modal = new EXRecruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _EVacancyAnnouncement(string src, EXRecruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_EVacancyAnnoList(Modal.Approve);
            return PartialView(ds);
        }

        public ActionResult EVacancyFinal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.REC_ReqID, out ID);
            EXRecruit.Vacancy.Final obj = new EXRecruit.Vacancy.Final();
            obj = Rec.GetVacancy_Final(ID);
            return View(obj);
        }


        [HttpPost]
        public ActionResult EVacancyFinal(string src, EXRecruit.Vacancy.Final Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Vacancy Can't Update";
            if (Command == "Save")
            {
                ModelState.Clear();
            }
            if (ModelState.IsValid)
            {
                int Web = (Modal.Web_Announce != null ? 1 : 0);
                int Internal = (Modal.Internal_Announce != null ? 1 : 0);
                int Other = (Modal.Other_Announce != null ? 1 : 0);

                PostResult = Common_SPU.fnSetREC_EVacancyAnno_Final(REC_ReqID, Modal.StartDate, Modal.EndDate, Web, Internal, Other, Command);
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Recruit/EVacancyAnnouncement?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruit/EVacancyAnnouncement");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult VacancyResponse(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _VacancyResponse(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_EVacancyResponseList(Modal.Approve);
            return PartialView(ds);
        }
        public ActionResult VacancyScreening(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);

        }
        [HttpPost]
        public ActionResult _ScreeningApplicationList(string src, Recruit.Tabs Modal, FormCollection Collection)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.Approve = Modal.Approve;
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            string Applied = "", Worked = "";
            if (Collection.GetValue("Applied") != null)
            {
                Applied = Collection.GetValue("Applied").AttemptedValue;
            }
            if (Collection.GetValue("Worked") != null)
            {
                Worked = Collection.GetValue("Worked").AttemptedValue;
            }
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_EScreeningApplicationList(REC_ReqID, Applied, Worked, Modal.Approve);
            return PartialView(ds);

        }

        [HttpPost]
        public ActionResult SetScreening(string src, FormCollection Collection, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Screening Can't Update";
            string ddScreeningApprover = "", HDNSelectedAPPID = "", Remarks = "";
            if (Command == "Reject")
            {
                if (Collection.GetValue("HDNSelectedAPPID") != null)
                {
                    HDNSelectedAPPID = Collection.GetValue("HDNSelectedAPPID").AttemptedValue;
                }
                if (Collection.GetValue("Remarks") != null)
                {
                    Remarks = Collection.GetValue("Remarks").AttemptedValue;
                }
                if (string.IsNullOrEmpty(Remarks))
                {
                    PostResult.SuccessMessage = "Remarks can't be blank";
                }
                else if (string.IsNullOrEmpty(HDNSelectedAPPID))
                {
                    PostResult.SuccessMessage = "Select atleast one Application";
                }
                else
                {
                    if (HDNSelectedAPPID.Contains(","))
                    {
                        foreach (var item in HDNSelectedAPPID.Split(','))
                        {
                            PostResult = Common_SPU.fnSetREC_EScreening(REC_ReqID, item, 2, Remarks);
                        }
                    }
                    else
                    {
                        PostResult = Common_SPU.fnSetREC_EScreening(REC_ReqID, HDNSelectedAPPID, 2, Remarks);
                    }
                }

            }
            else if (Command == "Screened")
            {
                if (Collection.GetValue("HDNSelectedAPPID") != null)
                {
                    HDNSelectedAPPID = Collection.GetValue("HDNSelectedAPPID").AttemptedValue;
                }
                if (!string.IsNullOrEmpty(HDNSelectedAPPID))
                {
                    if (HDNSelectedAPPID.Contains(","))
                    {
                        foreach (var item in HDNSelectedAPPID.Split(','))
                        {
                            PostResult = Common_SPU.fnSetREC_EScreening(REC_ReqID, item, 1, Remarks);
                        }
                    }
                    else
                    {
                        PostResult = Common_SPU.fnSetREC_EScreening(REC_ReqID, HDNSelectedAPPID, 1, Remarks);
                    }
                }
                else
                {
                    PostResult.SuccessMessage = "Select atleast one Application";
                }
            }
            else if (Command == "Selected")
            {

                if (Collection.GetValue("HDNSelectedAPPID") != null)
                {
                    HDNSelectedAPPID = Collection.GetValue("HDNSelectedAPPID").AttemptedValue;
                }
                if (!string.IsNullOrEmpty(HDNSelectedAPPID))
                {
                    if (HDNSelectedAPPID.Contains(","))
                    {
                        foreach (var item in HDNSelectedAPPID.Split(','))
                        {
                            PostResult = Common_SPU.fnSetREC_EScreening(REC_ReqID, item, 3, Remarks);
                        }
                    }
                    else
                    {
                        PostResult = Common_SPU.fnSetREC_EScreening(REC_ReqID, HDNSelectedAPPID, 3, Remarks);
                    }
                }
                else
                {
                    PostResult.SuccessMessage = "Select atleast one Application";
                }
            }

            else if (Command == "Forward")
            {

                if (Collection.GetValue("HDNSelectedAPPID") != null)
                {
                    HDNSelectedAPPID = Collection.GetValue("HDNSelectedAPPID").AttemptedValue;
                }
                if (!string.IsNullOrEmpty(HDNSelectedAPPID))
                {
                    if (HDNSelectedAPPID.Contains(","))
                    {
                        foreach (var item in HDNSelectedAPPID.Split(','))
                        {
                            PostResult = Common_SPU.fnSetREC_EScreening(REC_ReqID, item, 4, Remarks);
                        }
                    }
                    else
                    {
                        PostResult = Common_SPU.fnSetREC_EScreening(REC_ReqID, HDNSelectedAPPID, 4, Remarks);
                    }
                }
                else
                {
                    PostResult.SuccessMessage = "Select atleast one Application";
                }

                // Set Approver
                if (Collection.GetValue("ddScreeningApprover") != null)
                {
                    ddScreeningApprover = Collection.GetValue("ddScreeningApprover").AttemptedValue;
                }
                if (!string.IsNullOrEmpty(ddScreeningApprover))
                {
                    int ApCount = 0;
                    if (ddScreeningApprover.Contains(","))
                    {
                        foreach (var item in ddScreeningApprover.Split(','))
                        {
                            ApCount++;
                            PostResult = Common_SPU.fnSetREC_EScreening_Approvers(REC_ReqID, ApCount, item);
                        }
                    }
                    else
                    {
                        ApCount = 1;
                        PostResult = Common_SPU.fnSetREC_EScreening_Approvers(REC_ReqID, ApCount, ddScreeningApprover);
                    }
                    clsDataBaseHelper.ExecuteNonQuery("update REC_ScreeningApprovers set isdeleted=1 , deletedat=getdate() where REC_ReqID=" + REC_ReqID + " and SrNo>" + ApCount);
                }
                else
                {
                    PostResult.SuccessMessage = "Select atleast one Approver";
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ShortlistedCV(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ShortlistedCV(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_EShortlistedCVList(Modal.Approve);
            return PartialView(ds);
        }

        public ActionResult FinishShortlisting(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _FinishShortlisting(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.Approve = Modal.Approve;
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_EShortListedApplicationList(REC_ReqID, Modal.Approve);
            return PartialView(ds);
        }

        [HttpPost]
        public ActionResult SetFinalShortlisting(string src, FormCollection Collection, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Screening Can't Update";
            string HDNSelectedAPPID = "", Comment = "";
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            if (Command == "Reject")
            {

                if (Collection.GetValue("Comment") != null)
                {
                    Comment = Collection.GetValue("Comment").AttemptedValue;
                }
                if (Collection.GetValue("HDNSelectedAPPID") != null)
                {
                    HDNSelectedAPPID = Collection.GetValue("HDNSelectedAPPID").AttemptedValue;
                }
                if (string.IsNullOrEmpty(HDNSelectedAPPID))
                {
                    PostResult.SuccessMessage = "Select atleast one Application";
                }
                else if (string.IsNullOrEmpty(Comment))
                {
                    PostResult.SuccessMessage = "Comment can't be blank";
                }
                else
                {
                    if (HDNSelectedAPPID.Contains(","))
                    {
                        foreach (var item in HDNSelectedAPPID.Split(','))
                        {
                            PostResult = Common_SPU.fnSetREC_EShortlisted(REC_ReqID, item, EMPID, 1, Comment, 2);
                        }
                    }
                    else
                    {
                        PostResult = Common_SPU.fnSetREC_EShortlisted(REC_ReqID, HDNSelectedAPPID, EMPID, 1, Comment, 2);
                    }
                }
            }
            else if (Command == "Selected")
            {
                if (Collection.GetValue("HDNSelectedAPPID") != null)
                {
                    HDNSelectedAPPID = Collection.GetValue("HDNSelectedAPPID").AttemptedValue;
                }
                if (string.IsNullOrEmpty(HDNSelectedAPPID))
                {
                    PostResult.SuccessMessage = "Select atleast one Application";
                }
                else
                {
                    if (HDNSelectedAPPID.Contains(","))
                    {
                        foreach (var item in HDNSelectedAPPID.Split(','))
                        {
                            PostResult = Common_SPU.fnSetREC_EShortlisted(REC_ReqID, item, EMPID, 1, Comment, 1);
                        }
                    }
                    else
                    {
                        PostResult = Common_SPU.fnSetREC_EShortlisted(REC_ReqID, HDNSelectedAPPID, EMPID, 1, Comment, 1);
                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);


        }



        public ActionResult _ViewApplications(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_AppID = GetQueryString[2];
            long REC_AppID;
            long.TryParse(ViewBag.REC_AppID, out REC_AppID);
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_ApplicationView(REC_AppID);
            return PartialView(ds);
        }



        public ActionResult ConfirmedCV(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ConfirmedCV(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_EConfirmedCVList(Modal.Approve);
            return PartialView(ds);
        }

        public ActionResult FinishConfirmedCV(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            EXRecruit.Final_ConfirmedCV Modal = new EXRecruit.Final_ConfirmedCV();
            Modal = Rec.GetFinishConfirmedCV(REC_ReqID);
            return View(Modal);
        }

        [HttpPost]
        public ActionResult FinishConfirmedCV(string src, FormCollection Collection, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Preference Can't Update";
            string Remarks = "", ApproveCandidate = "";



            if (Collection.GetValue("Remarks") != null)
            {
                Remarks = Collection.GetValue("Remarks").AttemptedValue;
            }
            if (Collection.GetValue("ApproveCandidate") != null)
            {
                ApproveCandidate = Collection.GetValue("ApproveCandidate").AttemptedValue;
            }
            if (!string.IsNullOrEmpty(ApproveCandidate))
            {
                if (Command.Equals("Add"))
                {
                    if (ApproveCandidate.Contains(","))
                    {
                        foreach (var item in ApproveCandidate.Split(','))
                        {
                            PostResult = Common_SPU.fnSetREC_EFinalConfirmedCV(REC_ReqID, item, Remarks);
                        }
                    }
                    else
                    {
                        PostResult = Common_SPU.fnSetREC_EFinalConfirmedCV(REC_ReqID, ApproveCandidate, Remarks);
                    }
                }
                else if (Command.Equals("Confirm"))
                {
                    int Approved = 1;
                    if (ApproveCandidate.Contains(","))
                    {
                        foreach (var item in ApproveCandidate.Split(','))
                        {
                            PostResult = Common_SPU.fnSetREC_EFinalConfirmedStatusCV(REC_ReqID, item, Remarks, Approved);
                        }
                    }
                    else
                    {
                        PostResult = Common_SPU.fnSetREC_EFinalConfirmedStatusCV(REC_ReqID, ApproveCandidate, Remarks, Approved);
                    }
                }
                else if (Command.Equals("Drop"))
                {
                    int Approved = 2;
                    if (ApproveCandidate.Contains(","))
                    {
                        foreach (var item in ApproveCandidate.Split(','))
                        {
                            PostResult = Common_SPU.fnSetREC_EFinalConfirmedStatusCV(REC_ReqID, item, Remarks, Approved);
                        }
                    }
                    else
                    {
                        PostResult = Common_SPU.fnSetREC_EFinalConfirmedStatusCV(REC_ReqID, ApproveCandidate, Remarks, Approved);
                    }
                }
            }
            else
            {
                PostResult.SuccessMessage = "Select atleast one Application";
            }

            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Recruit/ConfirmedCV?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruit/ConfirmedCV");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult InterviewSetting(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _InterviewSetting(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_EInterviewSettingList(Modal.Approve);
            return PartialView(ds);

        }

        public ActionResult InterviewRound(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            List<Interview.Round> List = new List<Interview.Round>();
            List = Rec.GetInterviewRound(REC_ReqID, 0);

            return View(List);
        }
        [HttpPost]
        public JsonResult SendEmailConfirmation(string src)
        {
            CommandResult PostResult = new CommandResult();
            string IPAddress = ClsCommon.GetIPAddress();
            int LoginID = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            string REC_InterviewSetID = GetQueryString[3].ToString();
            string REC_ReqID = GetQueryString[2].ToString();
            string RoundName = GetQueryString[4].ToString();
            if (ModelState.IsValid)
            {
                PostResult = Common_SPU.fnSetRecruitmentRoundsEmailConfirmation(REC_InterviewSetID, "8", REC_ReqID, "Recruitment Round", RoundName);
                if (PostResult.Status)
                {
                    //PostResult.ID = PostResult.ID;
                }
                else
                {
                    PostResult.StatusCode = -1;
                    PostResult.SuccessMessage = PostResult.SuccessMessage;
                }
            }

            if (PostResult.Status)
            {
                //PostResult.RedirectURL = "/Tools/LoginUsersList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Tools/LoginUsersList");
            }
            string jsonData = JsonConvert.SerializeObject(PostResult);
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult _AddInterviewRound(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.REC_InterviewSetID = GetQueryString[3];
            long REC_ReqID = 0, REC_InterviewSetID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            long.TryParse(ViewBag.REC_InterviewSetID, out REC_InterviewSetID);
            Interview.Round Modal = new Interview.Round();
            if (REC_InterviewSetID > 0)
            {
                Modal = Rec.GetInterviewRound(REC_ReqID, REC_InterviewSetID).FirstOrDefault();
            }
            ViewBag.EmployeeList = CommonSpecial.GetAllEmployeeList();
            return PartialView(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _AddInterviewRound(string src, Interview.Round Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.REC_InterviewSetID = GetQueryString[3];
            long REC_ReqID = 0, REC_InterviewSetID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            long.TryParse(ViewBag.REC_InterviewSetID, out REC_InterviewSetID);
            PostResult.SuccessMessage = "Round and details is not Saved";
            if (Modal.SlotList == null)
            {
                PostResult.SuccessMessage = "Slot Can't be blank";
                ModelState.AddModelError("RoundName", PostResult.SuccessMessage);

            }
            else if (Modal.SlotList.Any(x => Convert.ToDateTime(x.SlotDate) < DateTime.Now.Date))
            {
                PostResult.SuccessMessage = "Slot's date can only future dates";
                ModelState.AddModelError("RoundName", PostResult.SuccessMessage);
            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    int IsNegotiationRound = (Modal.IsNegotiationRound ? 1 : 0);


                    PostResult = Common_SPU.fnSetREC_EInterviewSetting(REC_InterviewSetID, REC_ReqID, 1, IsNegotiationRound, "Round", 0, "", Modal.RoundName, "", Modal.RoundDesc, "", 0, "", "", "", "", "", "", Modal.Priority, 1);
                    if (PostResult.Status && Modal.MemberList != null)
                    {
                        int Count = 0;
                        for (int i = 0; i < Modal.MemberList.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(Modal.MemberList[i].Name))
                            {
                                Count++;
                                Common_SPU.fnSetREC_EInterviewSetting(Modal.MemberList[i].REC_InterviewSetID ?? 0, REC_ReqID, Count, 0, "Member", PostResult.ID, "", "", "", "", Modal.MemberList[i].RoundMemberType, Modal.MemberList[i].EMPID, Modal.MemberList[i].Name, Modal.MemberList[i].Email, "", "", "", "", Modal.Priority, 1);
                            }
                        }
                        clsDataBaseHelper.ExecuteNonQuery("update REC_InterviewSetting set isdeleted=1,deletedat=getdate() where doctype='Member' and  Srno>" + Count + " and  LinkID=" + PostResult.ID + "");
                    }

                    if (PostResult.Status && Modal.SlotList != null)
                    {
                        int Count = 0;
                        for (int i = 0; i < Modal.SlotList.Count; i++)
                        {

                            if (!string.IsNullOrEmpty(Modal.SlotList[i].SlotDate))
                            {
                                Count++;
                                Common_SPU.fnSetREC_EInterviewSetting(Modal.SlotList[i].REC_InterviewSetID ?? 0, REC_ReqID, Count, 0, "Slot", PostResult.ID, "", "", "", "", "", 0, "", "", Modal.SlotList[i].SlotDate, Modal.SlotList[i].MAXCV, Modal.SlotList[i].FromTime, Modal.SlotList[i].ToTime, Modal.Priority, 1);
                            }
                        }
                        clsDataBaseHelper.ExecuteNonQuery("update REC_InterviewSetting set isdeleted=1,deletedat=getdate() where doctype='Slot' and Srno>" + Count + " and  LinkID=" + PostResult.ID + "");
                    }
                }
                if (PostResult.Status)
                {
                    TempData["Success"] = (PostResult.Status ? "Y" : "N");
                    TempData["SuccessMsg"] = PostResult.SuccessMessage;
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }




        public ActionResult SelectionProcessList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _SelectionProcessList(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_ESelectionProcessList(Modal.Approve);
            return PartialView(ds);

        }
        public ActionResult SelectionProcess(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            List<InterviewSelection.Round> List = new List<InterviewSelection.Round>();
            List = Rec.GetInterviewSelectionRound(REC_ReqID);
            return View(List);
        }

        public ActionResult _MoveToPoolOrReject(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.REC_AppID = GetQueryString[3];
            ViewBag.REC_InterviewSetID = GetQueryString[4];
            long REC_ReqID = 0, REC_AppID = 0, REC_InterviewSetID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            long.TryParse(ViewBag.REC_AppID, out REC_AppID);
            long.TryParse(ViewBag.REC_InterviewSetID, out REC_InterviewSetID);
            InterviewSelection.Reject Modal = new InterviewSelection.Reject();
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _MoveToPoolOrReject(string src, InterviewSelection.Reject Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.REC_AppID = GetQueryString[3];
            ViewBag.REC_InterviewSetID = GetQueryString[4];
            long REC_ReqID = 0, REC_AppID = 0, REC_InterviewSetID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            long.TryParse(ViewBag.REC_AppID, out REC_AppID);
            long.TryParse(ViewBag.REC_InterviewSetID, out REC_InterviewSetID);
            PostResult.SuccessMessage = "Reject is not Saved";
            if (ModelState.IsValid)
            {
                if (Command == "Reject")
                {
                    if (!string.IsNullOrEmpty(Modal.ChkMove))
                    {
                        Common_SPU.fnSetREC_TalentPoolSelection(REC_AppID, Modal.Reason);
                    }
                    PostResult = Common_SPU.fnSetREC_EInterviewDirect(REC_InterviewSetID, REC_ReqID, REC_AppID, 0, "", Modal.Reason, 0, "", 2, "");
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult _MoveToRound(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.REC_AppID = GetQueryString[3];
            ViewBag.REC_InterviewSetID = GetQueryString[4];
            long REC_ReqID = 0, REC_AppID = 0, REC_InterviewSetID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            long.TryParse(ViewBag.REC_AppID, out REC_AppID);
            long.TryParse(ViewBag.REC_InterviewSetID, out REC_InterviewSetID);
            InterviewSelection.MoveToRound Modal = new InterviewSelection.MoveToRound();
            //List<InterviewSelection.MoveToRound> Modal = new List<InterviewSelection.MoveToRound>();
            Modal = Rec.GetMoveToRound(REC_AppID);
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _MoveToRound(string src, InterviewSelection.MoveToRound Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.REC_AppID = GetQueryString[3];
            ViewBag.REC_InterviewSetID = GetQueryString[4];
            long REC_ReqID = 0, REC_AppID = 0, REC_InterviewSetID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            long.TryParse(ViewBag.REC_AppID, out REC_AppID);
            long.TryParse(ViewBag.REC_InterviewSetID, out REC_InterviewSetID);
            PostResult.SuccessMessage = "Next Round is not Saved";
            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    PostResult = Common_SPU.fnSetREC_EInterview(Modal.RoundID, REC_ReqID, REC_AppID, Modal.SlotID, Modal.Score, Modal.Reason, 0, "", 1, "");
                    if (Modal.AttachmentsList != null)
                    {
                        foreach (var item in Modal.AttachmentsList)
                        {
                            if (item.Upload != null)
                            {
                                var RvFile = clsApplicationSetting.ValidateFile(item.Upload);
                                if (RvFile.IsValid)
                                {
                                    item.ID = Common_SPU.fnSetAttachments(item.ID, RvFile.FileName, RvFile.FileExt, item.Description, REC_AppID.ToString(), "Interview Process");
                                    if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + item.ID + RvFile.FileExt)))
                                    {
                                        System.IO.File.Delete(Server.MapPath("~/Attachments/" + item.ID + RvFile.FileExt));
                                    }
                                    item.Upload.SaveAs(Server.MapPath("~/Attachments/" + item.ID + RvFile.FileExt));
                                }
                                else
                                {
                                    PostResult.Status = RvFile.IsValid;
                                    PostResult.SuccessMessage = RvFile.Message;
                                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                                }
                            }
                            //else
                            //{
                            //    item.ID = Common_SPU.fnSetAttachments(item.ID, item.FileName, item.FileExt, item.Description, REC_AppID.ToString(), "Interview Process");
                            //}
                        }
                    }

                    QulifiedRound qulifiedRound = new QulifiedRound();
                    qulifiedRound.RoundID = (int)Modal.RoundID;
                    qulifiedRound.RoundName = "Round 1".ToString();
                    qulifiedRound.REC_InterviewSetID = (int)Modal.RoundID;
                    qulifiedRound.REC_ReqID = (int)REC_ReqID;
                    qulifiedRound.REC_InterviewID = (int)PostResult.ID;
                    qulifiedRound.Score = Modal.Score;
                    qulifiedRound.Remarks = Modal.Reason;
                    qulifiedRound.PanelMember = "";
                    qulifiedRound.REC_AppID = (int)REC_AppID;
                    Modal.QulifiedRound = qulifiedRound;
                    if (Modal.QulifiedRound != null)
                    {
                        PostResult = Common_SPU.fnSetREC_InterviewRoundHistory(Modal.QulifiedRound);
                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult _SelectCandidate(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.REC_AppID = GetQueryString[3];
            ViewBag.REC_InterviewSetID = GetQueryString[4];
            long REC_ReqID = 0, REC_AppID = 0, REC_InterviewSetID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            long.TryParse(ViewBag.REC_AppID, out REC_AppID);
            long.TryParse(ViewBag.REC_InterviewSetID, out REC_InterviewSetID);
            InterviewSelection.Approve Modal = new InterviewSelection.Approve();
            Modal = Rec.GetInterviewApprove(REC_AppID);
            return PartialView(Modal);
        }


        [HttpPost]
        public ActionResult _SelectCandidate(string src, InterviewSelection.Approve Modal, string Command, FormCollection Collection)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            ViewBag.REC_AppID = GetQueryString[3];
            ViewBag.REC_InterviewSetID = GetQueryString[4];
            long REC_ReqID = 0, REC_AppID = 0, REC_InterviewSetID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            long.TryParse(ViewBag.REC_AppID, out REC_AppID);
            long.TryParse(ViewBag.REC_InterviewSetID, out REC_InterviewSetID);
            PostResult.SuccessMessage = "Select is not Saved";
            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    string REC_Tags = "";
                    if (Collection.GetValue("TagRECRequests") != null)
                    {
                        REC_Tags = Collection.GetValue("TagRECRequests").AttemptedValue;
                    }
                    PostResult = Common_SPU.fnSetREC_EInterviewConfirm(Modal.RoundID, REC_ReqID, REC_AppID, Modal.SlotID, Modal.Score, Modal.Reason, Modal.NegotiationSalary ?? 0, Modal.ExpectedJDate, 4, REC_Tags);
                    if (Modal.AttachmentsList != null)
                    {
                        foreach (var item in Modal.AttachmentsList)
                        {
                            if (item.Upload != null)
                            {
                                var RvFile = clsApplicationSetting.ValidateFile(item.Upload);
                                if (RvFile.IsValid)
                                {
                                    item.ID = Common_SPU.fnSetAttachments(item.ID, RvFile.FileName, RvFile.FileExt, item.Description, REC_AppID.ToString(), "Interview Process");
                                    if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + item.ID + RvFile.FileExt)))
                                    {
                                        System.IO.File.Delete(Server.MapPath("~/Attachments/" + item.ID + RvFile.FileExt));
                                    }
                                    item.Upload.SaveAs(Server.MapPath("~/Attachments/" + item.ID + RvFile.FileExt));
                                }
                                else
                                {
                                    PostResult.Status = RvFile.IsValid;
                                    PostResult.SuccessMessage = RvFile.Message;
                                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                item.ID = Common_SPU.fnSetAttachments(item.ID, item.FileName, item.FileExt, item.Description, REC_AppID.ToString(), "Interview Process");
                            }
                        }
                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult _TalentPoolList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_TalentPoolList(REC_ReqID);
            return PartialView(ds);
        }


        [HttpPost]
        public ActionResult SetImportTalentPool(string src, FormCollection Collection, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            PostResult.SuccessMessage = "Import Talent can't be done";
            string HDNTalentID = "";
            if (Command == "Selected")
            {
                if (Collection.GetValue("HDNTalentID") != null)
                {
                    HDNTalentID = Collection.GetValue("HDNTalentID").AttemptedValue;
                }
                if (string.IsNullOrEmpty(HDNTalentID))
                {
                    PostResult.SuccessMessage = "Select atleast one Application";
                }
                else
                {
                    if (HDNTalentID.Contains(","))
                    {
                        foreach (var item in HDNTalentID.Split(','))
                        {
                            PostResult = Common_SPU.fnSetREC_ImportFromTalentPool(REC_ReqID, item);
                        }
                    }
                    else
                    {
                        PostResult = Common_SPU.fnSetREC_ImportFromTalentPool(REC_ReqID, HDNTalentID);
                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult SelectionProcessList_View(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Recruit.Tabs Modal = new Recruit.Tabs();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _SelectionProcessList_View(string src, Recruit.Tabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_ESelectionProcess_ViewList(Modal.Approve);
            return PartialView(ds);

        }
        public ActionResult SelectionProcess_View(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.REC_ReqID = GetQueryString[2];
            long REC_ReqID = 0;
            long.TryParse(ViewBag.REC_ReqID, out REC_ReqID);
            List<InterviewSelection.Round> List = new List<InterviewSelection.Round>();
            List = Rec.GetInterviewSelectionRound(REC_ReqID);
            return View(List);
        }

        [HttpPost]
        public ActionResult _MoveToPoolConfirmCV(string src, InterviewSelection.Reject Modal, string Command)
        {
            CommandResult PostResult = new CommandResult();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long REC_ReqID = 0;
            long.TryParse(GetQueryString[2], out REC_ReqID);

            if (ModelState.IsValid)
            {
                if (Command == "MoveToPool" && !string.IsNullOrEmpty(Modal.ChkMove))
                {
                    var appIds = Modal.REC_AppID.Split(',').Select(long.Parse).ToArray();
                    for (int i = 0; i < appIds.Length; i++)
                    {
                        Common_SPU.fnSetREC_TalentPool(appIds[i], Modal.Reason);
                    }
                    PostResult.SuccessMessage = "Candidates moved to talent pool successfully.";
                }
                else if (Command == "Drop" && !string.IsNullOrEmpty(Modal.ChkMove))
                {
                    int Approved = 2;
                    string item = "";
                    var appIds = Modal.REC_AppID.Split(',').Select(long.Parse).ToArray();
                    for (int i = 0; i < appIds.Length; i++)
                    {
                        PostResult = Common_SPU.fnSetREC_EFinalConfirmedStatusCVOrDrop(appIds[i], REC_ReqID, item, Modal.Reason, Approved);
                    }
                    PostResult.SuccessMessage = "Candidates Droped successfully.";
                }
            }
            else
            {
                PostResult.SuccessMessage = "Reason  can't be blank";
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }


    }
}