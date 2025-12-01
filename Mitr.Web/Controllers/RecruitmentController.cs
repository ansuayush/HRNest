using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class RecruitmentController : Controller
    {
        IRecruitmentHelper Recruitment;
        IMasterHelper Master;
        public RecruitmentController()
        {
            Recruitment = new RecruitmentModal();
            Master = new MasterModal();
        }
        public ActionResult RecruitmentPendancy(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            AllListModal Modal = new AllListModal();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _RecruitmentPendancy(string src, AllListModal Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetREC_Pendancy(Modal.Approve);
            return PartialView(ds);
        }

      



        public ActionResult _InternalStaffList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProjectDetID = GetQueryString[2];
            ViewBag.LocationID = GetQueryString[3];
            ViewBag.RecruitmentRequestID = GetQueryString[4];
            long ID = 0, LocationID = 0, RecruitmentRequestID = 0;
            long.TryParse(ViewBag.ProjectDetID, out ID);
            long.TryParse(ViewBag.LocationID, out LocationID);
            long.TryParse(ViewBag.RecruitmentRequestID, out RecruitmentRequestID);
            List<InternalStaff> List = new List<InternalStaff>();
            List = Recruitment.GetInternalStaffList(ID, LocationID);
            return PartialView(List);
        }

        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _InternalStaffList(string src, List<InternalStaff> Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProjectDetID = GetQueryString[2];
            ViewBag.LocationID = GetQueryString[3];
            ViewBag.RecruitmentRequestID = GetQueryString[4];
            long ID = 0, LocationID = 0, RecruitmentRequestID = 0;
            long.TryParse(ViewBag.ProjectDetID, out ID);
            long.TryParse(ViewBag.LocationID, out LocationID);
            long.TryParse(ViewBag.RecruitmentRequestID, out RecruitmentRequestID);
            bool status = false;
            string Msg = "Internal Staff Can't Update";
            if (Modal.Where(x => !string.IsNullOrEmpty(x.Chck)).ToList().Count == 0)
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
                        SaveID = Common_SPU.fnSetRecruitmentCandidate_Internal(0, RecruitmentRequestID, item.EmpID, item.Relocation, item.JobTitle, item.Pillar, item.RelocationByHR, item.JobTitleByHR, item.PillarByHR, 0, 1);

                    }


                    if (SaveID != 0)
                    {
                        status = true;
                        Msg = "Internal Staff Updated Successfully";
                    }
                }

                PostResult.Status = status;
                PostResult.SuccessMessage = Msg;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult InitiateRecruitment(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProjectDetID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ProjectDetID, out ID);

            ViewBag.LocationList = CommonSpecial.GetAllLocationList();
            InitiateRecruitment Modal = new InitiateRecruitment();
            if (ID > 0)
            {
                Modal = Recruitment.GetInitiateRecruitment(ID);
            }
            if (Modal.ProjectDetailID != 0)
            {
                Modal.RecruitmentRequestID = Common_SPU.fnSetCreateInitialRecruitmentRequest(Modal.ProjectDetailID, Modal.JobID, "", "", Modal.LocationID, 0, 1, 2);
            }
            Modal.EmployeeList = CommonSpecial.GetAllEmployeeList();
            return View(Modal);
        }


        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult InitiateRecruitment(string src, InitiateRecruitment Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProjectDetID = GetQueryString[2];
            long ProjectDetID = 0;
            long.TryParse(ViewBag.ProjectDetID, out ProjectDetID);
            bool status = false;
            string Msg = "Recruitment Request Can't Update";
            if (ModelState.IsValid)
            {
                if (Command == "Forward")
                {
                    SaveID = Common_SPU.fnSetCreateInitialRecruitmentRequest(ProjectDetID, Modal.JobID, Modal.ChkType, Modal.StaffCategory, Modal.LocationID, 0, 1, 0);

                    if (Modal.ChkType == "Internal" && Modal.InternalLevelList != null)
                    {
                        for (int i = 0; i < Modal.InternalLevelList.Count; i++)
                        {
                            if (Modal.InternalLevelList[i].ApprovalEMPID1 != 0)
                            {
                                Common_SPU.fnSetRecruitmentInternal_Level(0, SaveID, Modal.InternalLevelList[i].DocType, Modal.InternalLevelList[i].ApprovalEMPID1, 1, 1);
                            }
                            if (Modal.InternalLevelList[i].ApprovalEMPID2 != 0)
                            {
                                Common_SPU.fnSetRecruitmentInternal_Level(0, SaveID, Modal.InternalLevelList[i].DocType, Modal.InternalLevelList[i].ApprovalEMPID2, 2, 1);
                            }
                        }

                    }
                    if (SaveID != 0)
                    {
                        status = true;
                        Msg = "Recruitment Request Saved Successfully";
                        PostResult.RedirectURL = "/Recruitment/RecruitmentPendancy?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruitment/RecruitmentPendancy*0");
                    }
                }

                PostResult.Status = status;
                PostResult.SuccessMessage = Msg;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult _SelectedCandidate_InternalList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RecruitmentRequestID = GetQueryString[2];
            ViewBag.DeletedRequired = GetQueryString[3];
            long RecruitmentRequestID = 0;
            long.TryParse(ViewBag.RecruitmentRequestID, out RecruitmentRequestID);
            List<InternalStaff> List = new List<InternalStaff>();
            List = Recruitment.GetSelectedRecruitmentCandidate_InternalList(0, RecruitmentRequestID);
            return PartialView(List);
        }


        public ActionResult PendingForPereferencesList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<RecruitmentRequest> List = new List<RecruitmentRequest>();
            List = Recruitment.GetRecruitmentRequestInternal(0, "Level");
            return View(List);
        }

        public ActionResult FinalizePereferencesList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<RecruitmentRequest> List = new List<RecruitmentRequest>();
            List = Recruitment.GetRecruitmentRequestInternal(0, "Final");
            return View(List);
        }

        public ActionResult RecruitmentPereferences(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RecruitmentRequestID = GetQueryString[2];
            long RecruitmentRequestID = 0;
            long.TryParse(ViewBag.RecruitmentRequestID, out RecruitmentRequestID);
            RecruitmentRequest Modal = new RecruitmentRequest();
            Modal = Recruitment.GetRecruitmentPreferenceRaw(RecruitmentRequestID);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult RecruitmentPereferences(string src, RecruitmentRequest Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RecruitmentRequestID = GetQueryString[2];
            long RecruitmentRequestID = 0;
            long.TryParse(ViewBag.RecruitmentRequestID, out RecruitmentRequestID);
            bool status = false;
            string Msg = "Pereferences Can't Update";
            if (ModelState.IsValid)
            {
                if (Command == "Submit")
                {
                    if (Modal.RecruitmentPreferenceList != null)
                    {
                        for (int i = 0; i < Modal.RecruitmentPreferenceList.Count; i++)
                        {

                            SaveID = Common_SPU.fnSetRecruitmentCandidateLevel_Mapping(Modal.RecruitmentPreferenceList[i].CanLevelMapID, Modal.RecruitmentPreferenceList[i].RecruitmentRequestID, Modal.RecruitmentPreferenceList[i].EmpID, Modal.RecruitmentPreferenceList[i].DocType, Modal.RecruitmentPreferenceList[i].Preference);
                        }
                    }
                    if (SaveID != 0)
                    {
                        status = true;
                        Msg = "Pereferences Request Saved Successfully";
                        PostResult.RedirectURL = "/Recruitment/PendingForPereferencesList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruitment/PendingForPereferencesList*0");
                    }
                }

                PostResult.Status = status;
                PostResult.SuccessMessage = Msg;
                TempData["Success"] = (status ? "Y" : "N");
                TempData["SuccessMsg"] = Msg;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult RecruitmentFinalize(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RecruitmentRequestID = GetQueryString[2];
            long RecruitmentRequestID = 0;
            long.TryParse(ViewBag.RecruitmentRequestID, out RecruitmentRequestID);
            FinalSelection Modal = new FinalSelection();
            Modal.RecruitmentRequest = Recruitment.GetRecruitmentRequestInternal(RecruitmentRequestID, "").FirstOrDefault();
            ViewBag.FinalizedDataSet = Common_SPU.fnGetRecruitmentInternal_Finalized(RecruitmentRequestID);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult RecruitmentFinalize(string src, FinalSelection Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RecruitmentRequestID = GetQueryString[2];
            long RecruitmentRequestID = 0;
            long.TryParse(ViewBag.RecruitmentRequestID, out RecruitmentRequestID);
            bool status = false;
            string Msg = "Final Candidate Can't Update";
            PostResult.SuccessMessage = Msg;
            string SQL = "";
            if (ModelState.IsValid)
            {
                if (Command == "Submit")
                {
                    try
                    {
                        ArrayList ArStr = new ArrayList();
                        SQL = "update RecruitmentCandidate_Internal set Approved=0 where RecruitmentRequestID=" + RecruitmentRequestID;
                        ArStr.Add(SQL);

                        SQL = "update RecruitmentCandidate_Internal set Approved=1 where RecruitmentRequestID=" + RecruitmentRequestID + " and EMPID=" + Modal.ApproveCandidate + "";
                        ArStr.Add(SQL);

                        SaveID = clsDataBaseHelper.executeArrayOfSql(ArStr);
                        if (SaveID != 0)
                        {
                            status = true;
                            Msg = "Final Candidate Saved Successfully";
                            PostResult.RedirectURL = "/Recruitment/FinalizePereferencesList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruitment/FinalizePereferencesList*0");
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsCommon.LogError("Error during RecruitmentFinalize. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
                    }
                }

                PostResult.Status = status;
                PostResult.SuccessMessage = Msg;
                TempData["Success"] = (status ? "Y" : "N");
                TempData["SuccessMsg"] = Msg;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult RecruitmentPendingExtList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<RecruitmentRequestExternal> List = new List<RecruitmentRequestExternal>();
            List = Recruitment.GetRecruitmentRequestExternal(0, "Manager");
            return View(List);
        }

        public ActionResult PendingComExtList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<JobPost> List = new List<JobPost>();
            List = Recruitment.GetJobPost(0, "Communication");
            return View(List);
        }

        public ActionResult PostRecruitment(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RecruitmentRequestID = GetQueryString[2];
            ViewBag.ComingFrom = GetQueryString[3];
            long RecruitmentRequestID = 0;
            long.TryParse(ViewBag.RecruitmentRequestID, out RecruitmentRequestID);
            RecruitmentRequestExternal Modal = new RecruitmentRequestExternal();
            if (RecruitmentRequestID > 0)
            {
                Modal = Recruitment.GetRecruitmentRequestExternal(RecruitmentRequestID, "Manager").FirstOrDefault();
            }
            return View(Modal);
        }
        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult PostRecruitment(string src, RecruitmentRequestExternal Modal, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RecruitmentRequestID = GetQueryString[2];
            
            long RecruitmentRequestID = 0, SaveID = 0;
            long.TryParse(ViewBag.RecruitmentRequestID, out RecruitmentRequestID);
            bool status = false;
            string Msg = "Request Can't Sent";
            if (ModelState.IsValid)
            {
                if (Command == "Forward")
                {
                   
                    SaveID = Common_SPU.fnSetJobPost(RecruitmentRequestID, Modal.JobTitle, Modal.DueDate, Modal.StartDate, Modal.EndDate, Modal.JobDescription, Modal.Qualification, Modal.Skills, Modal.Experience, Modal.Location,
                        Modal.AttachmentID, Modal.Announcement, Modal.AnnouncementType, Modal.AnnouncementStartDate, Modal.AnnouncementEndDate, Modal.Link, 0, 1, 1);

                    if (SaveID != 0)
                    {
                        status = true;
                        Msg = "Request Sent Successfully";
                    }
                }
            }
            else
            {
                return View(Modal);
            }
            TempData["Success"] = (status ? "Y" : "N");
            TempData["SuccessMsg"] = Msg;
            return RedirectToAction("RecruitmentPendingExtList", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruitment/RecruitmentPendingExtList") });
            
        }


        public ActionResult ApproveJobPost(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.JobPostID = GetQueryString[2];
            long JobPostID = 0;
            long.TryParse(ViewBag.JobPostID, out JobPostID);
            JobPost Modal = new JobPost();
            if (JobPostID > 0)
            {
                Modal = Recruitment.GetJobPost(JobPostID, "").FirstOrDefault();
            }
            return View(Modal);
        }
        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult ApproveJobPost(string src, JobPost Modal, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RecruitmentRequestID = GetQueryString[2];
            long JobPostID = 0, SaveID = 0;
            long.TryParse(ViewBag.JobPostID, out JobPostID);
            bool status = false;
            string Msg = "Job Post Can't Approved";
            if (ModelState.IsValid)
            {
                if (Command == "Forward")
                {
                    SaveID = Common_SPU.fnSetJobPost(Modal.RecruitmentRequestID, Modal.JobTitle, Modal.DueDate, Modal.StartDate, Modal.EndDate, Modal.JobDescription, Modal.Qualification, Modal.Skills, Modal.Experience, Modal.Location,
                        Modal.AttachmentID, Modal.Announcement, Modal.AnnouncementType, Modal.AnnouncementStartDate, Modal.AnnouncementEndDate, Modal.Link, 1, 1, 1);

                    if (SaveID != 0)
                    {
                        status = true;
                        Msg = "Job Post Approved Successfully";
                    }
                }
            }
            else
            {
                return View(Modal);
            }
            TempData["Success"] = (status ? "Y" : "N");
            TempData["SuccessMsg"] = Msg;

            return RedirectToAction("PendingComExtList", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Recruitment/PendingComExtList") });

        }

        public ActionResult LiveJobsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<JobPost> List = new List<JobPost>();
            List = Recruitment.GetJobPost(0, "Live");
            return View(List);
        }



    }
}