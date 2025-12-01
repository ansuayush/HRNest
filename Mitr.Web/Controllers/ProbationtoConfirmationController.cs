using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    [CheckLoginFilter]

    public class ProbationtoConfirmationController : Controller
    {
        IPTCHelper PTC;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;

        
        public ProbationtoConfirmationController()
        {
            getResponse = new GetResponse();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            PTC = new PTCModal();

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

        public ActionResult Hierarchy(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PTC Modal = new PTC();
            Modal.Approved = 0;
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _Hierarchy(string src, PTC Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PTC.Hierarchy> List = new List<PTC.Hierarchy>();
            List = PTC.GetPTC_HierarchyList(Modal.Approved);
            if (Modal.Approved == 0)
                ViewBag.SetClass1 = "active";
            else
                ViewBag.SetClass2 = "active";
            return PartialView(List);
        }

        public ActionResult _ModifyHierarchy(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long Id = 0, Approved = 0, EMPID = 0;
            long.TryParse(GetQueryString[2], out Id);
            long.TryParse(GetQueryString[3], out Approved);
            long.TryParse(GetQueryString[4], out EMPID);
            PTC.Hierarchy.Update Modal = new PTC.Hierarchy.Update();
            Modal = PTC.GetPTC_HierarchyUpdate(Id, Approved, EMPID);
            Modal.EMPID = EMPID;
            return PartialView(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _ModifyHierarchy(string src, PTC.Hierarchy.Update Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "Hierarchy not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            if (ModelState.IsValid)
            {
                CommandResult Result = Common_SPU.fnSetPTC_Master(Modal.Id, Modal.EMPID, Modal.Confirmer);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CreationofObjective(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PTC Modal = new PTC();
            Modal.Approved = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _Objective(string src, PTC Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PTC.CreationofObjective> List = new List<PTC.CreationofObjective>();
            List = PTC.GetPTC_CreationofObjective(Modal.Approved);
            if (Modal.Approved == 0)
                ViewBag.SetClass1 = "active";
            else if (Modal.Approved == 1)
                ViewBag.SetClass2 = "active";
            else
                ViewBag.SetClass3 = "active";
            return PartialView(List);
        }

        public ActionResult CreateObjectives(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long Id = 0, Approved = 0, EMPID = 0;
            long.TryParse(GetQueryString[2], out Id);
            long.TryParse(GetQueryString[3], out Approved);
            long.TryParse(GetQueryString[4], out EMPID);
            PTC.ProbationObjectives Modal = new PTC.ProbationObjectives();
            Modal = PTC.GetPTC_ProbationObjectives(Id, Approved, EMPID);
            Modal.EMPID = EMPID;
            return View(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult CreateObjectives(string src, PTC.ProbationObjectives Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long approved = 0;
            long draft = 0;
            ViewBag.src = src;
            PostResult.SuccessMessage = "Data not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "Submit" && Modal.objectivesperiods.Sum(x => x.Weights) < 100 || Modal.objectivesperiods.Sum(x => x.Weights) > 100)
            {
                PostResult.SuccessMessage = "Total weights should be in total of 100";
                ModelState.AddModelError("Commentors", PostResult.SuccessMessage);
            }
            if (ModelState.IsValid)
            {
                if (Command == "Submit")
                {
                    approved = 2;
                    draft = 0;
                }

                if (Command == "Save")
                    draft = 2;
                if (Command == "Save" || Command == "Submit")
                {
                    CommandResult Result = Common_SPU.fnSetPTC_ProbationObjective(Modal.Id, Modal.EMPID, Modal.Commentors, approved, draft);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    if (Result.ID > 0)
                    {
                        if (Modal.objectivesperiods.Count > 0)
                        {
                            foreach (var item in Modal.objectivesperiods)
                            {

                                CommandResult Resultperiods = Common_SPU.fnSetPTC_spu_SetPTC_ProbationObjectivePeriodUpdate(item.Id, Result.ID, Modal.EMPID);



                            }
                        }

                    }
                    if (Command == "Save")
                    {
                        PostResult.SuccessMessage = "Saved successfully.";
                        PostResult.RedirectURL = "/ProbationtoConfirmation/CreateObjectives?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/CreateObjectives*" + PostResult.ID + "*" + Modal.Approved + "*" + Modal.EMPID);
                    }
                    else
                    {
                        PostResult.SuccessMessage = "Submitted successfully.";
                        PostResult.StatusCode = 2;
                        PostResult.RedirectURL = "/ProbationtoConfirmation/CreationofObjective?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/CreationofObjective*");
                    }

                }

                else if (Command == "Approve")
                {
                    approved = 1;
                    draft = 0;
                    CommandResult Result = Common_SPU.fnSetPTC_ProbationObjective(Modal.Id, Modal.EMPID, "", approved, draft);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = "Approved successfully";
                    // PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    PostResult.StatusCode = 2;
                    PostResult.RedirectURL = "/ProbationtoConfirmation/CreationofObjective?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/CreationofObjective*");

                }
                else
                {
                    approved = 4;
                    draft = 0;
                    CommandResult Result = Common_SPU.fnSetPTC_ProbationObjective(Modal.Id, Modal.EMPID, "", approved, draft);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = "Resubmitted successfully";
                    // PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    if (Result.ID > 0)
                    {
                        CommandResult ResultResubmit = Common_SPU.fnSetPTC_ProbationObjectiveResubmit(Modal.Id, Modal.ResubmitComment);

                    }
                    PostResult.StatusCode = 2;
                    PostResult.RedirectURL = "/ProbationtoConfirmation/CreationofObjective?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/CreationofObjective*");
                }


            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult _ProbationObjectives(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long Id = 0, Approved = 0, EMPID = 0;
            long.TryParse(GetQueryString[2], out Id);
            long.TryParse(GetQueryString[3], out Approved);
            long.TryParse(GetQueryString[4], out EMPID);
            PTC.ObjectivesperiodNew Modal = new PTC.ObjectivesperiodNew();
            Modal = PTC.GetPTC_ObjectiveUpdate(Id);
            Modal.EMPID = EMPID;
            return PartialView(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _ProbationObjectives(string src, PTC.ObjectivesperiodNew Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            PostResult.SuccessMessage = "Probation Objectives not updated";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (ModelState.IsValid)
            {
                if (Modal.Id > 0)
                {
                    CommandResult Result = Common_SPU.fnSetPTC_spu_SetPTC_ProbationObjectivePeriod(Modal.Id, Modal.PBID, Modal.KPAID, Modal.UOMId, Modal.Weights, Modal.ProbationObjective, Modal.Remarks, Modal.EMPID);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = 2;
                    PostResult.ID = Result.ID;
                }
                else
                {
                    CommandResult Result = Common_SPU.fnSetPTC_spu_SetPTC_ProbationObjectivePeriod(Modal.Id, Modal.PBID, Modal.KPAID, Modal.UOMId, Modal.Weights, Modal.ProbationObjective, Modal.Remarks, Modal.EMPID);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = 2;
                    PostResult.ID = Result.ID;
                }


            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult _MyGoalSheetList(string src, PTC Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PTC.CreationofObjective> List = new List<PTC.CreationofObjective>();
            List = PTC.GetPTC_CreationofObjectiveMyList(Modal.Approved);
            if (Modal.Approved == 0)
                ViewBag.SetClass1 = "active";
            else if (Modal.Approved == 1)
                ViewBag.SetClass2 = "active";



            return PartialView(List);
        }
        public ActionResult MyGoalSheetList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PTC Modal = new PTC();
            Modal.Approved = 0;
            return View(Modal);
        }


        public ActionResult MyGoalSheet(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long Id = 0, Approved = 0, EMPID = 0;
            long.TryParse(GetQueryString[2], out Id);
            long.TryParse(GetQueryString[3], out Approved);
            long.TryParse(GetQueryString[4], out EMPID);
            PTC.ProbationObjectives Modal = new PTC.ProbationObjectives();
            Modal = PTC.GetPTC_ProbationObjectives(Id, Approved, EMPID);
            Modal.EMPID = EMPID;
            return View(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult MyGoalSheet(string src, PTC.ProbationObjectives Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long approved = 0;
            long draft = 0;
            ViewBag.src = src;
            PostResult.SuccessMessage = "Data not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            //if (Command == "Submit")
            //{
            //    if (Modal.UserComment == null)
            //    {
            //        PostResult.SuccessMessage = "Enter User Comment";
            //        ModelState.AddModelError("UserComment", PostResult.SuccessMessage);
            //    }

            //}
            if (ModelState.IsValid)
            {
                if (Command == "Submit")
                {
                    approved = 3;
                    draft = 0;
                }

                if (Command == "Save")
                {
                    if (Modal.UserComment == null)
                    {
                        Modal.UserComment = "";
                    }
                    approved = Modal.Approved;
                    draft = 3;
                }

                CommandResult Result = Common_SPU.fnSetPTC_ProbationObjectiveUser(Modal.Id, Modal.EMPID, Modal.UserComment, approved, draft);
                PostResult.Status = Result.Status;
                PostResult.SuccessMessage = Result.SuccessMessage;
                PostResult.StatusCode = Result.StatusCode;
                PostResult.ID = Result.ID;
                if (Result.ID > 0)
                {
                    if (Modal.objectivesperiods.Count > 0)
                    {
                        foreach (var item in Modal.objectivesperiods)
                        {
                            if (item.Id > 0)
                            {
                                CommandResult Resultperiods = Common_SPU.fnSetPTC_spu_SetPTC_ProbationObjectivePeriodUser(item.Id, Result.ID, item.Target);
                            }


                        }
                    }

                }
                if (Command == "Save")
                {
                    PostResult.SuccessMessage = "Saved successfully.";
                    PostResult.RedirectURL = "/ProbationtoConfirmation/MyGoalSheet?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/MyGoalSheet*" + PostResult.ID + "*" + Modal.Approved + "*" + Modal.EMPID);
                }
                else
                {
                    PostResult.SuccessMessage = "Submitted successfully.";
                    PostResult.RedirectURL = "/ProbationtoConfirmation/MyGoalSheetList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/MyGoalSheetList*");
                    PostResult.StatusCode = 2;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SelfAppraisal(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PTC.Appraisal.SelfAppraisal Modal = new PTC.Appraisal.SelfAppraisal();
            Modal = PTC.GetPTC_SelfAppraisal(0);
            return View(Modal);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult SelfAppraisal(string src, PTC.Appraisal.SelfAppraisal Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long approved = 0;
            long draft = 0;
            ViewBag.src = src;
            PostResult.SuccessMessage = "Data not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Modal.KPAL == null)
            {
                PostResult.SuccessMessage = "Dnt have KPA";
                ModelState.AddModelError("Self_Achievement", PostResult.SuccessMessage);
            }

            else if (Modal.KPAL != null && Modal.KPAL.Any(x => string.IsNullOrEmpty(x.Self_Achievement)) && (Command == "Submit"))
            {
                PostResult.SuccessMessage = "Fill All Achievement";
                ModelState.AddModelError("Self_Achievement", PostResult.SuccessMessage);
                PostResult.StatusCode = 2;
            }
            if (Command == "Submit")
            {
                if (Modal.feedbackQuestions != null)
                {
                    if (Modal.feedbackQuestions.Any(x => string.IsNullOrEmpty(x.UserAnswer)))
                    {
                        PostResult.SuccessMessage = "Enter 	Employee's Response";
                        ModelState.AddModelError("Self_Achievement", PostResult.SuccessMessage);
                    }
                }
            }
            if (Command == "FinalSubmit")
            {
                if (string.IsNullOrEmpty(Modal.UserFinalComment))
                {
                    PostResult.SuccessMessage = "Enter  Comment";
                    ModelState.AddModelError("UserFinalComment", PostResult.SuccessMessage);
                }
            }
            if (ModelState.IsValid)
            {
                if (Command == "Submit")
                {
                    approved = 2;
                    draft = 0;
                }

                if (Command == "Save")
                {
                    draft = 2;
                    approved = Modal.Approved;
                }

                if (Command == "Save" || Command == "Submit")
                {
                    CommandResult Result = Common_SPU.fnSetPTC_SelfAppraisal(Modal.ID, Modal.UserComment, approved, draft, "", "");
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    if (Result.ID > 0)
                    {
                        if (Modal.KPAL.Count > 0)
                        {
                            foreach (var item in Modal.KPAL)
                            {

                                CommandResult Resultperiods = Common_SPU.fnSetPTC_SelfAppraisalKPA(item.ID, Result.ID, item.KPAID, item.ProbationObjective, item.UOMId, Convert.ToInt64(item.Weights), item.TargetAchievement, item.Self_Achievement, item.Target, item.Self_Comment, "", 0);



                            }
                        }

                        if (Modal.TrainingL.Count > 0)
                        {
                            foreach (var item in Modal.TrainingL)
                            {

                                CommandResult Resultperiods = Common_SPU.fnSetPTC_AppraisalTraining(item.ID, Result.ID, Convert.ToInt64(item.TrainingTypeID), item.TrainingRemarks, "");



                            }
                        }
                        if (Modal.feedbackQuestions != null)
                        {

                            foreach (var item in Modal.feedbackQuestions)
                            {

                                CommandResult Resultperiods = Common_SPU.fnSetPTC_AppraisalQuestion(item.ID, Result.ID, item.Question, item.UserAnswer, item.QID, "");



                            }
                        }

                        if (Modal.AttachmentsL != null)
                        {
                            for (int i = 0; i < Modal.AttachmentsL.Count; i++)
                            {
                                if (Modal.AttachmentsL[i].UploadFile != null)
                                {
                                    var RvFile = clsApplicationSetting.ValidateFile(Modal.AttachmentsL[i].UploadFile);
                                    if (RvFile.IsValid)
                                    {
                                        Modal.AttachmentsL[i].AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentsL[i].AttachmentID, RvFile.FileName, RvFile.FileExt, Modal.AttachmentsL[i].Descrip, PostResult.ID.ToString(), "PTCAppraisal");
                                        if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentsL[i].AttachmentID + RvFile.FileExt)))
                                        {
                                            System.IO.File.Delete("~/Attachments/" + Modal.AttachmentsL[i].AttachmentID + RvFile.FileExt);
                                        }
                                        Modal.AttachmentsL[i].UploadFile.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentsL[i].AttachmentID + RvFile.FileExt));
                                    }
                                }
                                else
                                {
                                    if (Modal.AttachmentsL[i].AttachmentID > 0)
                                    {
                                        Common_SPU.fnSetAttachments(Modal.AttachmentsL[i].AttachmentID, Modal.AttachmentsL[i].FileName, "", Modal.AttachmentsL[i].Descrip, PostResult.ID.ToString(), "PTCAppraisal");
                                    }
                                }
                            }
                        }
                    }
                    if (Command == "Save")
                    {
                        PostResult.SuccessMessage = "Saved successfully.";
                    }
                    else
                    {
                        PostResult.SuccessMessage = "Submitted successfully.";
                    }

                }


                else if (Command == "FinalSubmit")
                {
                    CommandResult Result = Common_SPU.fnSetPTC_SubmitFinalAppraisal(Modal.ID, Modal.UserFinalComment, 5);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = "Submitted successfully.";
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;

                }

                PostResult.RedirectURL = "/ProbationtoConfirmation/SelfAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/SelfAppraisal*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult TeamAppraisal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PTC Modal = new PTC();
            Modal.Approved = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _TeamAppraisal(string src, PTC Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PTC.Appraisal.TeamList> List = new List<PTC.Appraisal.TeamList>();
            List = PTC.GetPTC_TeamList(Modal.Approved);
            if (Modal.Approved == 0)
                ViewBag.SetClass1 = "active";
            else if ((Modal.Approved == 1))
                ViewBag.SetClass2 = "active";
            else if ((Modal.Approved == 2))
                ViewBag.SetClass3 = "active";
            else if ((Modal.Approved == 3))
                ViewBag.SetClass4 = "active";
            return PartialView(List);
        }

        public ActionResult ReviewTeamAppraisal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long Id = 0, Approved = 0, EMPID = 0;
            long.TryParse(GetQueryString[2], out Id);
            PTC.Appraisal.SelfAppraisal Modal = new PTC.Appraisal.SelfAppraisal();
            Modal = PTC.GetPTC_SelfAppraisal(Id);
            return View(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult ReviewTeamAppraisal(string src, PTC.Appraisal.SelfAppraisal Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long approved = 0;
            long draft = 0;
            ViewBag.src = src;
            PostResult.SuccessMessage = "Data not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "Resubmit")
            {
                if (Modal.ResubmitComment == null)
                {
                    PostResult.SuccessMessage = "Enter  Comment";
                    ModelState.AddModelError("ResubmitComment", PostResult.SuccessMessage);
                }

            }
            if (Command == "FinalSubmit")
            {
                if (string.IsNullOrEmpty(Modal.HodFinalComment))
                {
                    PostResult.SuccessMessage = "Enter  Comment";
                    ModelState.AddModelError("HodFinalComment", PostResult.SuccessMessage);
                }
                if (Modal.appraisalReview.AppraisalType == "Confirm with Modification")
                {
                    if (string.IsNullOrEmpty(Modal.appraisalReview.Reason))
                    {
                        PostResult.SuccessMessage = "Enter  Comment";
                        ModelState.AddModelError("HodFinalComment", PostResult.SuccessMessage);
                    }
                }

            }

            if (Command == "Submit")
            {
                if (Modal.feedbackQuestions.Any(x => string.IsNullOrEmpty(x.SupervisiorAnswer)))
                {
                    PostResult.SuccessMessage = "Enter  Appraiser's Response";
                    ModelState.AddModelError("HodFinalComment", PostResult.SuccessMessage);
                }
                if (Modal.KPAL.Any(x => string.IsNullOrEmpty(x.HOD_Comment)))
                {
                    PostResult.SuccessMessage = "Enter Appraiser Comment";
                    ModelState.AddModelError("HodFinalComment", PostResult.SuccessMessage);
                }
                if (Modal.KPAL.Any(x => string.IsNullOrEmpty(x.HOD_Score)) || Modal.KPAL.Any(x => Convert.ToInt32(x.HOD_Score) == 0))
                {
                    PostResult.SuccessMessage = "Enter Appraiser Score";
                    ModelState.AddModelError("HodFinalComment", PostResult.SuccessMessage);
                }
                if (string.IsNullOrEmpty(Modal.HodComment))
                {
                    PostResult.SuccessMessage = "Enter Assessment  Comment";
                    ModelState.AddModelError("HodComment", PostResult.SuccessMessage);
                }


            }
            if (ModelState.IsValid)
            {
                if (Command == "Submit")
                {
                    approved = 3;
                    draft = 0;
                }

                if (Command == "Save")
                {
                    draft = 3;
                    approved = Modal.Approved;
                }

                if (Command == "Save" || Command == "Submit")
                {
                    CommandResult Result = Common_SPU.fnSetPTC_SelfAppraisal(Modal.ID, Modal.UserComment, approved, draft, Modal.HodComment, "");
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = Result.SuccessMessage;
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    if (Result.ID > 0)
                    {
                        if (Modal.KPAL.Count > 0)
                        {
                            foreach (var item in Modal.KPAL)
                            {

                                CommandResult Resultperiods = Common_SPU.fnSetPTC_SelfAppraisalKPA(item.ID, Result.ID, item.KPAID, item.ProbationObjective, item.UOMId, Convert.ToInt64(item.Weights), item.TargetAchievement, item.Self_Achievement, item.Target, item.Self_Comment, item.HOD_Comment, Convert.ToInt64(item.HOD_Score));



                            }
                        }

                        if (Modal.TrainingL.Count > 0)
                        {
                            foreach (var item in Modal.TrainingL)
                            {

                                CommandResult Resultperiods = Common_SPU.fnSetPTC_AppraisalTraining(item.ID, Result.ID, Convert.ToInt64(item.TrainingTypeID), item.TrainingRemarks, item.ReasonforDeclining);



                            }
                        }
                        if (Modal.feedbackQuestions.Count > 0)
                        {
                            foreach (var item in Modal.feedbackQuestions)
                            {

                                CommandResult Resultperiods = Common_SPU.fnSetPTC_AppraisalQuestion(item.ID, Result.ID, item.Question, item.UserAnswer, item.QID, item.SupervisiorAnswer);



                            }
                        }


                    }
                    if (Command == "Save")
                    {
                        PostResult.SuccessMessage = "Saved successfully.";
                    }
                    else
                    {
                        PostResult.SuccessMessage = "Submitted successfully.";
                    }

                }


                else if (Command == "Resubmit")
                {
                    approved = 4;
                    draft = 0;
                    CommandResult Result = Common_SPU.fnSetPTC_SelfAppraisal(Modal.ID, Modal.UserComment, approved, draft, Modal.HodComment, Modal.ResubmitComment);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = "ReSubmitted successfully.";
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;

                }
                else if (Command == "FinalSubmit")
                {
                    approved = 1;
                    draft = 0;
                    CommandResult Result = Common_SPU.fnSetPTC_FinalAppraisal(Modal.ID, Modal.HodFinalComment, approved);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = "Final Submission successfully.";
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    if (Modal.appraisalReview != null)
                    {

                        if (Modal.appraisalReview.AppraisalType == "Confirm")
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(Modal.ID, Modal.appraisalReview.AppraisalID, Modal.appraisalReview.AppraisalType, Modal.appraisalReview.FinalScore, Modal.appraisalReview.SystemScore, 0, "", 0, "HOD", "", "", Modal.EMPID);
                        }
                        else if (Modal.appraisalReview.AppraisalType == "Probation Extension")
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(Modal.ID, Modal.appraisalReview.AppraisalID, Modal.appraisalReview.AppraisalType, Modal.appraisalReview.FinalScore, Modal.appraisalReview.SystemScore, 0, "", 0, "HOD", "", Modal.appraisalReview.ProbationEndDate, Modal.EMPID);
                        }
                        else if (Modal.appraisalReview.AppraisalType == "Termination")
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(Modal.ID, Modal.appraisalReview.AppraisalID, Modal.appraisalReview.AppraisalType, Modal.appraisalReview.FinalScore, Modal.appraisalReview.SystemScore, 0, "", 0, "HOD", "", "", Modal.EMPID);
                        }
                        else
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(Modal.ID, Modal.appraisalReview.AppraisalID, Modal.appraisalReview.AppraisalType, Modal.appraisalReview.FinalScore, Modal.appraisalReview.SystemScore, Modal.appraisalReview.ModificationTypeId, Modal.appraisalReview.ModificationType, Convert.ToInt64(Modal.appraisalReview.TypeId), "HOD", Modal.appraisalReview.Reason, "", Modal.EMPID);
                            //CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(Modal.ID, Modal.appraisalReview.AppraisalID, Modal.appraisalReview.AppraisalType, Modal.appraisalReview.FinalScore, Modal.appraisalReview.SystemScore, Modal.appraisalReview.ModificationTypeId, Modal.appraisalReview.ModificationType, Modal.appraisalReview.TypeId, Modal.appraisalReview.TypeName, Modal.appraisalReview.Reason, "");
                        }

                    }

                }
                if (Command == "FinalSubmit" || Command == "Resubmit" || Command == "Submit")
                    PostResult.RedirectURL = "/ProbationtoConfirmation/TeamAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/TeamAppraisal*");
                else
                    PostResult.RedirectURL = "/ProbationtoConfirmation/ReviewTeamAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/ReviewTeamAppraisal*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ConfimerAppraisal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PTC Modal = new PTC();
            Modal.Approved = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ConfimerAppraisal(string src, PTC Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PTC.Appraisal.TeamList> List = new List<PTC.Appraisal.TeamList>();
            List = PTC.GetPTC_ConfimerList(Modal.Approved);
            if (Modal.Approved == 0)
                ViewBag.SetClass1 = "active";
            else if ((Modal.Approved == 1))
                ViewBag.SetClass2 = "active";
            else if ((Modal.Approved == 2))
                ViewBag.SetClass3 = "active";
            return PartialView(List);
        }

        public ActionResult ReviewConfimerAppraisal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long Id = 0, Approved = 0, EMPID = 0;
            long.TryParse(GetQueryString[2], out Id);
            long.TryParse(GetQueryString[5], out EMPID);
            PTC.Appraisal.SelfAppraisal Modal = new PTC.Appraisal.SelfAppraisal();
            Modal = PTC.GetPTC_ConfimerAppraisal(Id, EMPID);
            return View(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult ReviewConfimerAppraisal(string src, PTC.Appraisal.SelfAppraisal Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long approved = 0;
            long draft = 0;
            ViewBag.src = src;
            PostResult.SuccessMessage = "Data not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            if (Command == "Approved")
            {
                if (Modal.IsAgree == "No" && Modal.ConfirmerReview.AppraisalType == "Confirm with Modification")
                {
                    if (string.IsNullOrEmpty(Modal.ConfirmerReview.Reason))
                    {
                        PostResult.SuccessMessage = "Enter  Comment";
                        ModelState.AddModelError("Comment", PostResult.SuccessMessage);
                    }
                }


            }
            if (ModelState.IsValid)
            {



                if (Command == "Approved")
                {
                    approved = 1;
                    draft = 0;
                    CommandResult Result = Common_SPU.fnSetPTC_ConfimerAppraisal(0, Modal.Comment, approved, Modal.IsAgree, Modal.EMPID);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = "Approved successfully.";
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    if (Modal.ConfirmerReview != null)
                    {

                        if (Modal.ConfirmerReview.AppraisalType == "Confirm")
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(PostResult.ID, Modal.ConfirmerReview.AppraisalID, Modal.ConfirmerReview.AppraisalType, Modal.ConfirmerReview.FinalScore, Modal.ConfirmerReview.SystemScore, 0, "", 0, "Confimer", "", "", Modal.EMPID);
                        }
                        else if (Modal.ConfirmerReview.AppraisalType == "Probation Extension")
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(PostResult.ID, Modal.ConfirmerReview.AppraisalID, Modal.ConfirmerReview.AppraisalType, Modal.ConfirmerReview.FinalScore, Modal.ConfirmerReview.SystemScore, 0, "", 0, "Confimer", "", Modal.ConfirmerReview.ProbationEndDate, Modal.EMPID);
                        }
                        else if (Modal.ConfirmerReview.AppraisalType == "Termination")
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(PostResult.ID, Modal.ConfirmerReview.AppraisalID, Modal.ConfirmerReview.AppraisalType, Modal.ConfirmerReview.FinalScore, Modal.ConfirmerReview.SystemScore, 0, "", 0, "Confimer", "", "", Modal.EMPID);
                        }
                        else
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(PostResult.ID, Modal.ConfirmerReview.AppraisalID, Modal.ConfirmerReview.AppraisalType, Modal.ConfirmerReview.FinalScore, Modal.ConfirmerReview.SystemScore, Modal.ConfirmerReview.ModificationTypeId, Modal.ConfirmerReview.ModificationType, Convert.ToInt64(Modal.ConfirmerReview.TypeId), "Confimer", Modal.ConfirmerReview.Reason, "", Modal.EMPID);

                        }

                    }

                }
                PostResult.RedirectURL = "/ProbationtoConfirmation/ConfimerAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/ConfimerAppraisal*");
                // PostResult.RedirectURL = "/ProbationtoConfirmation/ReviewConfimerAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/ReviewConfimerAppraisal*" + PostResult.ID + "*" + 0 + "*" + 0 + "*" + Modal.EMPID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CMCApprovall(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PTC Modal = new PTC();
            Modal.Approved = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _CMCApprovall(string src, PTC Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PTC.Appraisal.TeamList> List = new List<PTC.Appraisal.TeamList>();
            List = PTC.GetPTC_CMCList(Modal.Approved);
            if (Modal.Approved == 0)
                ViewBag.SetClass1 = "active";
            else if ((Modal.Approved == 1))
                ViewBag.SetClass2 = "active";
            else if ((Modal.Approved == 2))
                ViewBag.SetClass3 = "active";
            return PartialView(List);
        }

        public ActionResult CMCAppraisal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long Id = 0, Approved = 0, EMPID = 0;
            long.TryParse(GetQueryString[2], out Id);
            long.TryParse(GetQueryString[5], out EMPID);
            PTC.Appraisal.CMCAppraisal Modal = new PTC.Appraisal.CMCAppraisal();
            Modal = PTC.GetPTC_CMCAppraisal(Id, EMPID);
            return View(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult CMCAppraisal(string src, PTC.Appraisal.CMCAppraisal Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long approved = 0;
            long draft = 0;
            ViewBag.src = src;
            PostResult.SuccessMessage = "Data not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            if (Command == "Approved")
            {
                if (Modal.IsAgree == "No" && Modal.CMCReview.AppraisalType == "Confirm with Modification")
                {
                    if (string.IsNullOrEmpty(Modal.CMCReview.Reason))
                    {
                        PostResult.SuccessMessage = "Enter  Comment";
                        ModelState.AddModelError("CMCComment", PostResult.SuccessMessage);
                    }
                }

            }
            if (ModelState.IsValid)
            {



                if (Command == "Approved")
                {
                    approved = 1;
                    draft = 0;
                    CommandResult Result = Common_SPU.fnSetPTC_CMCAppraisal(0, Modal.CMCComment, approved, Modal.CMCIsAgree, Modal.EMPID);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = "Approved successfully.";
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                    if (Modal.CMCReview != null)
                    {

                        if (Modal.CMCReview.AppraisalType == "Confirm")
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(PostResult.ID, Modal.CMCReview.AppraisalID, Modal.CMCReview.AppraisalType, Modal.CMCReview.FinalScore, Modal.CMCReview.SystemScore, 0, "", 0, "CMC", "", "", Modal.EMPID);
                        }
                        else if (Modal.CMCReview.AppraisalType == "Probation Extension")
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(PostResult.ID, Modal.CMCReview.AppraisalID, Modal.CMCReview.AppraisalType, Modal.CMCReview.FinalScore, Modal.CMCReview.SystemScore, 0, "", 0, "CMC", "", Modal.CMCReview.ProbationEndDate, Modal.EMPID);
                        }
                        else if (Modal.CMCReview.AppraisalType == "Termination")
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(PostResult.ID, Modal.CMCReview.AppraisalID, Modal.CMCReview.AppraisalType, Modal.CMCReview.FinalScore, Modal.CMCReview.SystemScore, 0, "", 0, "CMC", "", "", Modal.EMPID);
                        }
                        else
                        {
                            CommandResult ResultReview = Common_SPU.fnSetPTC_FinalSubmitAppraisalReview(PostResult.ID, Modal.CMCReview.AppraisalID, Modal.CMCReview.AppraisalType, Modal.CMCReview.FinalScore, Modal.CMCReview.SystemScore, Modal.CMCReview.ModificationTypeId, Modal.CMCReview.ModificationType, Convert.ToInt64(Modal.CMCReview.TypeId), "CMC", Modal.CMCReview.Reason, "", Modal.EMPID);

                        }

                    }

                }

                PostResult.RedirectURL = "/ProbationtoConfirmation/CMCApprovall?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/CMCApprovall*");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult HRApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PTC Modal = new PTC();
            Modal.Approved = 0;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _HRApproval(string src, PTC Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PTC.Appraisal.TeamList> List = new List<PTC.Appraisal.TeamList>();
            List = PTC.GetPTC_HRList(Modal.Approved);
            if (Modal.Approved == 0)
                ViewBag.SetClass1 = "active";
            else if ((Modal.Approved == 1))
                ViewBag.SetClass2 = "active";
            else if ((Modal.Approved == 2))
                ViewBag.SetClass3 = "active";
            return PartialView(List);
        }
        public ActionResult HRAppraisal(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long Id = 0, Approved = 0, EMPID = 0;
            long.TryParse(GetQueryString[2], out Id);
            long.TryParse(GetQueryString[5], out EMPID);
            PTC.Appraisal.HRAppraisal Modal = new PTC.Appraisal.HRAppraisal();
            Modal = PTC.GetPTC_HRAppraisal(Id, EMPID);
            return View(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult HRAppraisal(string src, PTC.Appraisal.HRAppraisal Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long approved = 0;

            ViewBag.src = src;
            PostResult.SuccessMessage = "Data not saved";
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            if (Command == "Reject")
            {
                if (Modal.HRComment == null)
                {
                    PostResult.SuccessMessage = "Enter  Comment";
                    ModelState.AddModelError("HRComment", PostResult.SuccessMessage);
                }

            }
            if (ModelState.IsValid)
            {



                if (Command == "Approved")
                {
                    approved = 1;

                    CommandResult Result = Common_SPU.fnSetPTC_HRAppraisal(0, "", approved, Modal.EMPID);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = "Approved successfully.";
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;


                }
                else
                {
                    approved = 2;
                    CommandResult Result = Common_SPU.fnSetPTC_HRAppraisal(0, Modal.HRComment, approved, Modal.EMPID);
                    PostResult.Status = Result.Status;
                    PostResult.SuccessMessage = "Reject successfully.";
                    PostResult.StatusCode = Result.StatusCode;
                    PostResult.ID = Result.ID;
                }
                PostResult.RedirectURL = "/ProbationtoConfirmation/HRApproval?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/HRApproval*");
                // PostResult.RedirectURL = "/ProbationtoConfirmation/HRAppraisal?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/ProbationtoConfirmation/HRAppraisal*" + PostResult.ID + "*" + 0 + "*" + 0 + "*" + Modal.EMPID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PTCReport(string src)
        {
            ViewBag.src = src;
            GetResponse getDropDown = new GetResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PTC Modal = new PTC();
            Modal.EMPID = 0;
            Modal.TypeId = 2;
            Modal.Type = "Appraisal";
            getDropDown.Doctype = "Employee";
            ViewBag.EMPList = ClsCommon.GetDropDownList(getDropDown);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _PTCReport(string src, PTC Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PTC.Hierarchy> List = new List<PTC.Hierarchy>();
            List = PTC.GetPTC_Report(Modal.Type, Modal.EMPID);
            return PartialView(List);
        }

    }
}