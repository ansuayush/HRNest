using DocumentFormat.OpenXml.Office2010.Excel;
using Mitr.Areas.Career.Models;
using Mitr.Areas.Career.ModelsMaster;
using Mitr.Areas.Career.ModelsMasterHelper;
using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using Syncfusion.EJ2.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Mitr.Areas.Career.Models.JobApplication;
using static NPOI.SS.Format.CellNumberFormatter;

namespace Mitr.Areas.Career.Controllers
{
    public class HomeController : Controller
    {
        IApplicationHelper App;
        IRecruit_Helper Rec;
        public HomeController()
        {
            App = new ApplicationModal();
            Rec = new Recruit_Modal();
        }
        // GET: Career/Home
        public ActionResult Thanks()
        {
            return View();
        }

        public ActionResult Application(string Code)
        {
            ViewBag.Code = Code;
            JobApplication.Apply Modal = new JobApplication.Apply();
            List<Questions> questions = new List<Questions>
            {
                new Questions { Question = "Are you related to any of C3''s current staff member",QuestionOption="ddlStaffmember" },
                new Questions { Question = "If required, are you willing to relocate",QuestionOption="ddlRelocate" },
                new Questions { Question = "Notice Period Required" , QuestionOption = "ddlNoticePeriod"},
                new Questions { Question = "Were you ever associated with C3 in the past",QuestionOption="ddlAssociated" },
                new Questions { Question = "Have you applied with C3 before",QuestionOption="ddlApplied" },
                new Questions { Question = "Have you ever been charged with an offence (except for minor traffic challans)",QuestionOption="ddlOffencer" }
            };

            Modal = App.GetApplyJob(Code);
            Modal.QuestionsList = questions;
            string[] array = Code.Split('-');
            string RECReqID = array[1].ToString();
            EXRecruit.Vacancy.Final obj = new EXRecruit.Vacancy.Final();
            if (!string.IsNullOrEmpty(RECReqID))
            {
                obj = Rec.GetVacancy_Final(Convert.ToInt64(RECReqID));
                bool isValide = false;
                DateTime enddate = Convert.ToDateTime(obj.EndDate);
                enddate = enddate.AddDays(1);
                if (enddate < System.DateTime.Now)
                {
                    isValide = true;
                    Modal.Status = false;
                    Modal.StatusMessage = "We have completed the hiring process, and this vacancy is now closed.";
                }
            }
            return View(Modal);
        }

        [HttpPost]
        public ActionResult Application(JobApplication.Apply Modal, string Code, string Command)
        {
            ViewBag.Code = Code;
            CommandResult PostResult = new CommandResult();
            PostResult.SuccessMessage = "Application Can't Update";
            if (ModelState.IsValid)
            {
                try
                {
                    if (Modal.Upload != null)
                    {
                        var RvFile = clsApplicationSetting.ValidateFile(Modal.Upload);
                        if (RvFile.IsValid)
                        {
                            Modal.CVAttachID = Common_SPU.fnSetAttachments(Modal.CVAttachID, RvFile.FileName, RvFile.FileExt, "");
                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.CVAttachID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + Modal.CVAttachID + RvFile.FileExt);
                            }
                            Modal.Upload.SaveAs(Server.MapPath("~/Attachments/" + Modal.CVAttachID + RvFile.FileExt));
                        }
                        else
                        {
                            PostResult.Status = RvFile.IsValid;
                            PostResult.SuccessMessage = RvFile.Message;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);
                        }
                    }
                    string ThematicID = "", Skills = "";
                    if (Modal.ThematicAreaID != null)
                    {
                        ThematicID = string.Join(",", Modal.ThematicAreaID);
                    }
                    if (Modal.SkillsID != null)
                    {
                        Skills = string.Join(",", Modal.SkillsID);
                    }
                    PostResult = Common_SPU.fnSetREC_ApplyJob(Modal.Code, Modal.Name, Modal.DOB, Modal.Gender, Modal.Nationality, Modal.Mobile, Modal.EmailID,
                        Modal.Address, Modal.TotalExperience, ThematicID, Modal.Resposibilities, Modal.ChangeReason, Modal.BreakReason, Modal.SpecialSkills, Modal.CVAttachID, Modal.CurrentSalary ?? 0, Modal.ExpectedSalary ?? 0
                        );

                    if (PostResult.Status)
                    {
                        int Qcount = 0;
                        if (Modal.ProQualList != null)
                        {
                            clsDataBaseHelper.ExecuteNonQuery("update REC_Applications_Det set isdeleted=1 , deletedat=getdate() where doctype='Professional' and REC_AppID=" + PostResult.ID);
                            foreach (var item in Modal.ProQualList)
                            {
                                Qcount++;
                                if (!string.IsNullOrEmpty(item.Employer))
                                {
                                    Common_SPU.fnSetREC_ApplyJob_Det(0, PostResult.ID, "Professional", item.Employer, item.Post, item.CTC.ToString(), "", "", "", "", "", "", "", "", "", "", "", item.Period, item.Location, "", "", "", Qcount, "", "", "");
                                }
                            }

                        }

                        if (Modal.ReferencesList != null)
                        {
                            int Rcount = 0;
                            clsDataBaseHelper.ExecuteNonQuery("update REC_Applications_Det set isdeleted=1 , deletedat=getdate() where doctype='References' and REC_AppID=" + PostResult.ID);
                            foreach (var item in Modal.ReferencesList)
                            {
                                Rcount++;
                                if (!string.IsNullOrEmpty(item.Name))
                                {
                                    Common_SPU.fnSetREC_ApplyJob_Det(0, PostResult.ID, "References", "", "", "", item.Name, item.Email, item.Relationship, item.Phone, "", "", "", "", "", "", "", "", "", "", "", "", Rcount, "", "", "");
                                }
                            }

                        }

                        if (Modal.QuestionsList != null)
                        {
                            int Qescount = 0;
                            clsDataBaseHelper.ExecuteNonQuery("update REC_Applications_Det set isdeleted=1 , deletedat=getdate() where doctype='Questions' and REC_AppID=" + PostResult.ID);
                            foreach (var item in Modal.QuestionsList)
                            {
                                Qescount++;
                                if (!string.IsNullOrEmpty(item.Question))
                                {
                                    item.Position = (Qescount == 4 ? item.PositionC3Past : item.Position);
                                    item.Location = (Qescount == 4 ? item.LocationC3Past : item.Location);
                                    item.AppliedDate = (Qescount == 5 ? item.AppliedDateC3Past : item.AppliedDate);
                                    Common_SPU.fnSetREC_ApplyJob_Det(0, PostResult.ID, "Questions", "", "", "", item.Name, "", item.Relationship, "", item.Question, item.QuestionOption, item.Relationship, item.NoticePeriod, item.Position, item.AppliedDate, item.outcome, item.Period, item.Location, item.DOL, item.ReasonL, item.Specify, Qescount, "", "", "");
                                }
                            }

                        }

                        if (Modal.EducationList != null)
                        {
                            int Ecount = 0;
                            clsDataBaseHelper.ExecuteNonQuery("update REC_Applications_Det set isdeleted=1 , deletedat=getdate() where doctype='Education' and REC_AppID=" + PostResult.ID);
                            foreach (var item in Modal.EducationList)
                            {
                                Ecount++;
                                if (!string.IsNullOrEmpty(item.Course))
                                {
                                    Common_SPU.fnSetREC_ApplyJob_Det(0, PostResult.ID, "Education", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", item.Location, "", "", "", Ecount, item.Course, item.University, item.year);
                                }
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    PostResult.SuccessMessage = ex.Message;
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Career/Home/Thanks";
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        #region Direct Hiring Process 


        public ActionResult DirectApplication(string Code)
        {
            ViewBag.Code = Code;
            JobApplication.Apply Modal = new JobApplication.Apply();
            List<Questions> questions = new List<Questions>
    {
        new Questions { Question = "Are you related to any of C3's current staff member?", QuestionOption = "ddlStaffmember" },
        new Questions { Question = "If required, are you willing to relocate?", QuestionOption = "ddlRelocate" },
        new Questions { Question = "Notice Period Required", QuestionOption = "ddlNoticePeriod" },
        new Questions { Question = "Were you ever associated with C3 in the past?", QuestionOption = "ddlAssociated" },
        new Questions { Question = "Have you applied with C3 before?", QuestionOption = "ddlApplied" },
        new Questions { Question = "Have you ever been charged with an offence (except for minor traffic challans)?", QuestionOption = "ddlOffencer" }
    };

            Modal = App.GetApplyJob(Code);
            Modal.QuestionsList = questions;

            string[] array = Code.Split('-');
            string RECReqID = array[1].ToString();
            ViewBag.REC_ReqID = RECReqID;

            if (!string.IsNullOrEmpty(RECReqID))
            {
                var vacancy = Rec.GetVacancy_Final(Convert.ToInt64(RECReqID));

            }

            return View(Modal);
        }

        [HttpPost]
        public ActionResult DirectApplication(JobApplication.Apply Modal, string Code, long REC_ReqID)
        {
            ViewBag.Code = Code;
            ViewBag.REC_ReqID = REC_ReqID;
            CommandResult PostResult = new CommandResult { SuccessMessage = "Application Can't Update" };
            if (ModelState.IsValid)
            {
                try
                {
                    if (Modal.Upload != null)
                    {
                        var rvFile = clsApplicationSetting.ValidateFile(Modal.Upload);
                        if (rvFile.IsValid)
                        {
                            Modal.CVAttachID = Common_SPU.fnSetAttachments(Modal.CVAttachID, rvFile.FileName, rvFile.FileExt, "");

                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.CVAttachID + rvFile.FileExt)))
                            {
                                System.IO.File.Delete(Server.MapPath("~/Attachments/" + Modal.CVAttachID + rvFile.FileExt));
                            }

                            Modal.Upload.SaveAs(Server.MapPath("~/Attachments/" + Modal.CVAttachID + rvFile.FileExt));
                        }
                        else
                        {
                            PostResult.Status = rvFile.IsValid;
                            PostResult.SuccessMessage = rvFile.Message;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);
                        }
                    }

                    string thematicID = Modal.ThematicAreaID != null ? string.Join(",", Modal.ThematicAreaID) : "";
                    string skills = Modal.SkillsID != null ? string.Join(",", Modal.SkillsID) : "";

                    PostResult = Common_SPU.fnSetREC_ApplyJobDirect(
                        Modal.Code, Modal.REC_ReqID, Modal.Name, Modal.DOB, Modal.Gender, Modal.Nationality, Modal.Mobile, Modal.EmailID,
                        Modal.Address, Modal.TotalExperience, thematicID, Modal.Resposibilities, Modal.ChangeReason,
                        Modal.BreakReason, Modal.SpecialSkills, Modal.CVAttachID, Modal.CurrentSalary ?? 0,
                        Modal.ExpectedSalary ?? 0
                    );
                    if (PostResult.Status)
                    {
                        int Qcount = 0;
                        if (Modal.ProQualList != null)
                        {
                            clsDataBaseHelper.ExecuteNonQuery("update REC_Applications_Det set isdeleted=1 , deletedat=getdate() where doctype='Professional' and REC_AppID=" + PostResult.ID);
                            foreach (var item in Modal.ProQualList)
                            {
                                Qcount++;
                                if (!string.IsNullOrEmpty(item.Employer))
                                {
                                    Common_SPU.fnSetREC_ApplyJob_Det(0, PostResult.ID, "Professional", item.Employer, item.Post, item.CTC.ToString(), "", "", "", "", "", "", "", "", "", "", "", item.Period, item.Location, "", "", "", Qcount, "", "", "");
                                }
                            }

                        }

                        if (Modal.ReferencesList != null)
                        {
                            int Rcount = 0;
                            clsDataBaseHelper.ExecuteNonQuery("update REC_Applications_Det set isdeleted=1 , deletedat=getdate() where doctype='References' and REC_AppID=" + PostResult.ID);
                            foreach (var item in Modal.ReferencesList)
                            {
                                Rcount++;
                                if (!string.IsNullOrEmpty(item.Name))
                                {
                                    Common_SPU.fnSetREC_ApplyJob_Det(0, PostResult.ID, "References", "", "", "", item.Name, item.Email, item.Relationship, item.Phone, "", "", "", "", "", "", "", "", "", "", "", "", Rcount, "", "", "");
                                }
                            }

                        }

                        if (Modal.QuestionsList != null)
                        {
                            int Qescount = 0;
                            clsDataBaseHelper.ExecuteNonQuery("update REC_Applications_Det set isdeleted=1 , deletedat=getdate() where doctype='Questions' and REC_AppID=" + PostResult.ID);
                            foreach (var item in Modal.QuestionsList)
                            {
                                Qescount++;
                                if (!string.IsNullOrEmpty(item.Question))
                                {
                                    item.Position = (Qescount == 4 ? item.PositionC3Past : item.Position);
                                    item.Location = (Qescount == 4 ? item.LocationC3Past : item.Location);
                                    item.AppliedDate = (Qescount == 5 ? item.AppliedDateC3Past : item.AppliedDate);
                                    Common_SPU.fnSetREC_ApplyJob_Det(0, PostResult.ID, "Questions", "", "", "", item.Name, "", item.Relationship, "", item.Question, item.QuestionOption, item.Relationship, item.NoticePeriod, item.Position, item.AppliedDate, item.outcome, item.Period, item.Location, item.DOL, item.ReasonL, item.Specify, Qescount, "", "", "");
                                }
                            }

                        }

                        if (Modal.EducationList != null)
                        {
                            int Ecount = 0;
                            clsDataBaseHelper.ExecuteNonQuery("update REC_Applications_Det set isdeleted=1 , deletedat=getdate() where doctype='Education' and REC_AppID=" + PostResult.ID);
                            foreach (var item in Modal.EducationList)
                            {
                                Ecount++;
                                if (!string.IsNullOrEmpty(item.Course))
                                {
                                    Common_SPU.fnSetREC_ApplyJob_Det(0, PostResult.ID, "Education", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", item.Location, "", "", "", Ecount, item.Course, item.University, item.year);
                                }
                            }

                        }

                    }

                }
                catch (Exception ex)
                {
                    PostResult.SuccessMessage = ex.Message;
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                }
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Career/Home/Thanks";
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}