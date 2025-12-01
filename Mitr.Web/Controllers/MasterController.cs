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
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class MasterController : Controller
    {
        IMasterHelper Master;
        IBudgetHelper Budget;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IPMSHelper PMS;
        public MasterController()
        {

            getResponse = new GetResponse();
            Master = new MasterModal();
            Budget = new BudgetModel();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsCommon.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            PMS = new PMSModal();
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
        public ActionResult CountryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Country> Modal = new List<Country>();
            Modal = Master.GetCountryList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CountryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CountryID = GetQueryString[2];
            Country Modal = new Country();
            long ID = 0;
            long.TryParse(ViewBag.CountryID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetCountryList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CountryAdd(string src, Country Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CountryID = GetQueryString[2];
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Country is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {

                    SaveID = Common_SPU.fnSetCountry(Modal.CountryID, Modal.CountryName, Modal.Description, Modal.CountryHours, Modal.CountryMinutes, Modal.Priority, 1);
                    status = true;
                    Msg = "Country Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ConsultantList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Consultant> Modal = new List<Consultant>();
            Modal = Master.GetConsultantList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult ConsultantAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ConsultantID = GetQueryString[2];
            Consultant Modal = new Consultant();
            long ConsultantID = 0;
            long.TryParse(ViewBag.ConsultantID, out ConsultantID);
            Modal = Master.GetConsultant(ConsultantID);
            return View(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult ConsultantAdd(Consultant Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ConsultantID = GetQueryString[2];
            long ConsultantID = 0;
            long.TryParse(ViewBag.ConsultantID, out ConsultantID);
            long SaveID = 0, AttachID = 0;
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Consultant is not Saved";
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(Command))
                    {
                        if (Modal.UploadCV != null)
                        {
                            var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadCV);
                            if (RvFile.IsValid)
                            {
                                Modal.AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentID, RvFile.FileName, RvFile.FileExt, "");
                                if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt)))
                                {
                                    System.IO.File.Delete(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt));
                                }
                                Modal.UploadCV.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt));
                            }
                            else
                            {
                                status = false;
                                TempData["Success"] = "N";
                                TempData["SuccessMsg"] = RvFile.Message;
                                return RedirectToAction("ConsultantAdd", new { src = src });
                            }
                        }
                        SaveID = Common_SPU.fnSetConsultant(ConsultantID, Modal.ConsultantName, Modal.Code, Modal.Status, Modal.PanName,
                            Modal.FatherName, Modal.LocalAddress, Modal.PerAddress, Modal.Location, Modal.PhoneNO, Modal.PanNo,
                            Modal.PayMode, Modal.Design, Modal.AccountNo, Modal.BankName, Modal.IFSCCode, Modal.AccountName,
                            Modal.AttachmentID, Modal.Email, Modal.ContactPerson, Modal.Title, Modal.Priority, 1);
                        if (SaveID > 0)
                        {
                            clsDataBaseHelper.ExecuteNonQuery("UPDATE map_thematic_Area SET ISDELETED=1 WHERE TABLE_NAME='CONSULTANT' AND LINK_ID=" + SaveID + " ");
                            if (Modal.ThematicAreaID != null)
                            {
                                foreach (var item in Modal.ThematicAreaID)
                                {
                                    Common_SPU.fnSetMapThematicArea("CONSULTANT", item.ToString(), SaveID.ToString());
                                }
                            }
                            status = true;
                            Msg = "Consultant Added Successfully";
                        }
                    }
                }
                else
                {
                    Modal.ThematicAreaData = Common_SPU.fnGetThematicArea(0);
                    return View(Modal);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during ConsultantAdd. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterController", "MasterController", "");
            }

            if (status)
            {
                TempData["Success"] = "Y";
                TempData["SuccessMsg"] = Msg;
            }

            return RedirectToAction("ConsultantList", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Master/ConsultantList") });

        }

        public ActionResult ThematicAreaList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<ThematicArea> Modal = new List<ThematicArea>();
            Modal = Master.GetThematicAreaList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _ThematicAreaAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ThematicAreaID = GetQueryString[2];
            ThematicArea Modal = new ThematicArea();
            long ID = 0;
            long.TryParse(ViewBag.ThematicAreaID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetThematicAreaList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _ThematicAreaAdd(string src, ThematicArea Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ThematicAreaID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ThematicAreaID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "ThematicArea is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {

                    SaveID = Common_SPU.fnSetThematicArea(ID, Modal.Code, Modal.Description, Modal.Category, Modal.Priority, 1);
                    status = true;
                    Msg = "ThematicArea Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult SubgrantList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Subgrant> Modal = new List<Subgrant>();
            Modal = Master.GetSubgrantList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult SubgrantAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubgrantID = GetQueryString[2];
            SubgrantAdd Modal = new SubgrantAdd();
            long ID = 0;
            long.TryParse(ViewBag.SubgrantID, out ID);

            Modal = Master.GetSubgrantAdd(ID);

            return View(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult SubgrantAdd(SubgrantAdd Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubgrantID = GetQueryString[2];
            long SubgrantID = 0;
            long.TryParse(ViewBag.SubgrantID, out SubgrantID);
            long SaveID = 0, AttachID = 0;
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Subgrant is not Saved";
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(Command))
                    {
                        SaveID = Common_SPU.fnSetSubgrantMst(SubgrantID, Modal.SubgrantList.Code, Modal.SubgrantList.Name,
                            Modal.SubgrantList.Address, Modal.SubgrantList.Mobile, Modal.SubgrantList.Location, Modal.SubgrantList.PanNo,
                            Modal.SubgrantList.Fora_No, Modal.SubgrantList.ShortName, Modal.SubgrantList.Priority, 1);
                        if (SaveID > 0)
                        {
                            if (Modal.AuthorizedPersonList != null)
                            {
                                for (int i = 0; i < Modal.AuthorizedPersonList.Count; i++)
                                {
                                    Common_SPU.fnSetSubGrantDetAtthr(SaveID, (i + 1), Modal.AuthorizedPersonList[i].Name,
                                        Modal.AuthorizedPersonList[i].Designation, "Authorized");
                                }
                            }

                            if (Modal.AttachmentsList != null)
                            {
                                for (int i = 0; i < Modal.AttachmentsList.Count; i++)
                                {
                                    if (Modal.AttachmentsList[i].UploadFile != null)
                                    {
                                        var RvFile = clsApplicationSetting.ValidateFile(Modal.AttachmentsList[i].UploadFile);
                                        if (RvFile.IsValid)
                                        {
                                            Modal.AttachmentsList[i].AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentsList[i].AttachmentID, RvFile.FileName, RvFile.FileExt, "");
                                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentsList[i].AttachmentID + RvFile.FileExt)))
                                            {
                                                System.IO.File.Delete("~/Attachments/" + Modal.AttachmentsList[i].AttachmentID + RvFile.FileExt);
                                            }
                                            Modal.AttachmentsList[i].UploadFile.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentsList[i].AttachmentID + RvFile.FileExt));
                                        }
                                        else
                                        {
                                            status = false;
                                            TempData["Success"] = "N";
                                            TempData["SuccessMsg"] = RvFile.Message;
                                            return RedirectToAction("SubgrantAdd", new { src = src });
                                        }
                                    }

                                    Common_SPU.fnSetSubGrantDetAtthachment(SaveID, (i + 1), Modal.AttachmentsList[i].Name,
                                        Modal.AttachmentsList[i].TypeID, Modal.AttachmentsList[i].AttachmentID, "Attachment");
                                }
                            }
                            status = true;
                            Msg = "Subgrant Added Successfully";
                        }
                    }
                }
                else
                {
                    return View(Modal);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during SubgrantAdd. The query was executed :", ex.ToString(), "fnSetSubgrantMst", "MasterController", "MasterController", "");
            }

            if (status)
            {
                TempData["Success"] = "Y";
                TempData["SuccessMsg"] = Msg;
            }


            return RedirectToAction("SubgrantList", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Master/SubgrantList") });

        }

        public ActionResult AnnouncementList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Announcement> Modal = new List<Announcement>();
            Modal = Master.GetAnnouncementList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult AnnouncementAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.AnnouncementID = GetQueryString[2];
            ViewBag.LocationList = Common_SPU.fnGetLocation(0);
            Announcement Modal = new Announcement();
            long ID = 0;
            long.TryParse(ViewBag.AnnouncementID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetAnnouncementList(ID).FirstOrDefault();
            }
            return View(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult AnnouncementAdd(Announcement Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.AnnouncementID = GetQueryString[2];
            long AnnouncementID = 0;
            long.TryParse(ViewBag.AnnouncementID, out AnnouncementID);
            long SaveID = 0, AttachID = 0;
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Announcement is not Saved";
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(Command))
                    {
                        if (Modal.UploadAttachment != null)
                        {
                            var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadAttachment);
                            if (RvFile.IsValid)
                            {
                                Modal.AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentID, RvFile.FileName, RvFile.FileExt, "");
                                if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt)))
                                {
                                    System.IO.File.Delete("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt);
                                }
                                Modal.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt));
                            }
                            else
                            {
                                status = false;
                                TempData["Success"] = "N";
                                TempData["SuccessMsg"] = RvFile.Message;
                                return RedirectToAction("AnnouncementAdd", new { src = src });
                            }
                        }
                        SaveID = Common_SPU.fnSetAnouncement(AnnouncementID, Modal.HeadingName, Modal.Description, Modal.AttachmentID, Modal.StarDate, Modal.ExpiryDate, Modal.Priority, 1);
                        if (SaveID > 0)
                        {
                            clsDataBaseHelper.ExecuteNonQuery("UPDATE map_announcement_loc SET ISDELETED=1 WHERE Announcement_ID=" + SaveID + " ");
                            if (Modal.LocationID != null)
                            {
                                foreach (var item in Modal.LocationID)
                                {
                                    Common_SPU.fnSetMapAnnouncementLoc(SaveID, item);
                                }
                            }
                            status = true;
                            Msg = "Announcement Added Successfully";
                        }
                    }
                }
                else
                {
                    ViewBag.LocationList = Common_SPU.fnGetLocation(0);
                    return View(Modal);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during AnnouncementAdd. The query was executed :", ex.ToString(), "fnSetAnouncement", "MasterController", "MasterController", "");
            }

            if (status)
            {
                TempData["Success"] = "Y";
                TempData["SuccessMsg"] = Msg;
            }

            return RedirectToAction("AnnouncementList", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Master/AnnouncementList") });

        }

        public ActionResult HolidayList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Personnels.FinYearList Modal = new Personnels.FinYearList();
            ViewBag.FYList = Master.GetFinYearList(0);
            long FYId = 23;
            Modal.ID = FYId;
            string Yr = DateTime.Now.Year.ToString();
            foreach (var item in ViewBag.FYList)
            {
                if (item.Year.Contains(Yr))
                {
                    Modal.ID = item.FinYearID;
                    Modal.Year = item.Year;
                    break;
                }
            }
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _HolidayList(Personnels.FinYearList FYMod, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Holiday> Modal = new List<Holiday>();
            Modal = Master.GetHolidayList(0).Where(x => x.FYId == FYMod.ID).OrderByDescending(x => Convert.ToDateTime(x.Date)).ToList();
            return PartialView(Modal);
        }

        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _HolidayAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HolidayID = GetQueryString[2];
            Holiday Modal = new Holiday();
            ViewBag.LocationList = Common_SPU.fnGetLocation(0);
            long ID = 0;
            long.TryParse(ViewBag.HolidayID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetHolidayList(ID).FirstOrDefault();
            }

            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _HolidayAdd(string src, Holiday Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.HolidayID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.HolidayID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Holiday is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    SaveID = Common_SPU.fnSetHoliday(ID, Modal.Name, Modal.Date, Modal.Remarks, Modal.ColorName.Split('^')[1], Modal.ColorName.Split('^')[0], Modal.Priority, 1);

                    if (SaveID > 0)
                    {
                        clsDataBaseHelper.ExecuteNonQuery("UPDATE map_holiday_loc SET ISDELETED=1 WHERE holiday_id=" + SaveID + "");
                        if (Modal.LocationID != null)
                        {
                            foreach (var item in Modal.LocationID)
                            {
                                Common_SPU.fnSetMapHolidayLoc(SaveID, item);
                            }
                        }
                    }
                    status = true;
                    Msg = "Holiday Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }




        public ActionResult QuestionList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Question> Modal = new List<Question>();
            Modal = Master.GetQuestionList(0);
            return View(Modal);
        }

        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _QuestionAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.QuestionID = GetQueryString[2];
            Question Modal = new Question();
            long ID = 0;
            long.TryParse(ViewBag.QuestionID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetQuestionList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _QuestionAdd(string src, Question Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.QuestionID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.QuestionID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Question is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    if (Modal.NoOfOption == 2)
                    {
                        Modal.Option3 = "";
                        Modal.Option4 = "";
                    }
                    else if (Modal.NoOfOption == 3)
                    {
                        Modal.Option4 = "";
                    }
                    SaveID = Common_SPU.fnSetQuestionMst(ID, Modal.Quest, Modal.NoOfOption, Modal.Option1, Modal.Option2, Modal.Option3, Modal.Option4, Modal.StartDate, Modal.EndDate, Modal.Priority, 1);
                    status = true;
                    Msg = "Question Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult VotingScoreBoardList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MiscQuestion Modal = new MiscQuestion();
            Modal.QuestionList = Master.GetQuestionList(0).Where(x => x.IsActive).ToList();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _VotingScoreBoardList(string src, Question Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.QuestionID = Modal.QuestionID;
            DataSet rModal = new DataSet();
            ViewBag.QuestionDetails = Master.GetQuestionList(0).Where(x => x.QuestionID == Modal.QuestionID).FirstOrDefault();
            rModal = Common_SPU.fnGetVotingList(Modal.QuestionID);
            return PartialView(rModal);
        }

        public ActionResult CompanyUploadList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<CompanyUpload> Modal = new List<CompanyUpload>();
            Modal = Master.GetCompanyUpload(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CompanyUploadAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CompanyUploadID = GetQueryString[2];
            CompanyUpload Modal = new CompanyUpload();
            long ID = 0;
            long.TryParse(ViewBag.CompanyUploadID, out ID);

            if (ID > 0)
            {
                Modal = Master.GetCompanyUpload(ID).FirstOrDefault();
            }
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "Comp_Category";
            Modal.CategoryList = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CompanyUploadAdd(string src, CompanyUpload Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CompanyUploadID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.CompanyUploadID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Company Upload is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    if (Modal.UploadAttachement != null)
                    {
                        var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadAttachement);
                        if (RvFile.IsValid)
                        {
                            Modal.AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentID, RvFile.FileName, RvFile.FileExt, "");
                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt);
                            }
                            Modal.UploadAttachement.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt));
                        }
                        else
                        {
                            status = false;
                            PostResult.Status = false;
                            PostResult.SuccessMessage = RvFile.Message;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);
                        }
                    }

                    SaveID = Common_SPU.fnSetByLaws(ID, Modal.Description, Modal.FromDate, Modal.ToDate, Modal.AttachmentID, "Company Upload", Modal.CategoryID, Modal.Priority, 1);
                    status = true;
                    Msg = "Company Upload Updated Successfully";

                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MemberShipPeriodList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MembershipPeriod> Modal = new List<MembershipPeriod>();
            Modal = Master.GetMembershipPeriodList(0);
            return View(Modal);
        }

        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _MemberShipPeriodAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MembershipPeriodID = GetQueryString[2];
            MembershipPeriod Modal = new MembershipPeriod();
            long ID = 0;
            long.TryParse(ViewBag.MembershipPeriodID, out ID);

            if (ID > 0)
            {
                Modal = Master.GetMembershipPeriodList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _MemberShipPeriodAdd(string src, MembershipPeriod Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MembershipPeriodID = GetQueryString[2];
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Membership Period is not Saved";
            long ID = 0;
            long.TryParse(ViewBag.MembershipPeriodID, out ID);
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    SaveID = Common_SPU.fnSetMemberShipPeriod(ID, Modal.Year, Modal.FromDate, Modal.ToDate, Modal.Priority, 1);
                    status = true;
                    Msg = "Membership Period Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }






        public ActionResult FinYearList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<FinYear> Modal = new List<FinYear>();
            Modal = Master.GetFinYearList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _FinYearAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FinYearID = GetQueryString[2];

            FinYear Modal = new FinYear();
            long ID = 0;
            long.TryParse(ViewBag.FinYearID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetFinYearList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _FinYearAdd(string src, FinYear Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FinYearID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.FinYearID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Financial Year is not Saved";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    SaveID = Common_SPU.fnSetFinYear(ID, Modal.Year, Modal.FromDate, Modal.ToDate, Modal.Priority, 1);
                    status = true;
                    Msg = "Financial Year Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult LeaveList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<LeaveMaster> Modal = new List<LeaveMaster>();
            Modal = Master.GetLeaveMasterList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _LeaveAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LeaveID = GetQueryString[2];
            LeaveMaster Modal = new LeaveMaster();
            long ID = 0;
            long.TryParse(ViewBag.LeaveID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetLeaveMasterList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _LeaveAdd(string src, LeaveMaster Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LeaveID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.LeaveID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Leave is not Saved";
            PostResult.SuccessMessage = "Leave is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    string ColorApplied = "", ColorcodeApplied = "", ColorAppproved = "", ColorcodeApproved = "";
                    if (!string.IsNullOrEmpty(Modal.ColorApplied))
                    {
                        ColorApplied = Modal.ColorApplied.Split('^')[1];
                        ColorcodeApplied = Modal.ColorApplied.Split('^')[0];
                    }
                    if (!string.IsNullOrEmpty(Modal.ColorAppproved))
                    {
                        ColorAppproved = Modal.ColorAppproved.Split('^')[1];
                        ColorcodeApproved = Modal.ColorAppproved.Split('^')[0];
                    }

                    SaveID = Common_SPU.fnSetLeave(ID, Modal.ApplicableFor, Modal.LeaveName, Modal.LeaveType, Modal.DueType, Modal.NOOfLeave,
                    ColorApplied, ColorAppproved, Modal.CFLeave, Modal.CFLimit, "1", ColorcodeApplied, ColorcodeApproved,
                    (Modal.chkFDLeave == null ? 0 : 1), (Modal.chkApprovalRequired == null ? 0 : 1),
                    (Modal.chkShowleavebalance == null ? 0 : 1),
                    Modal.Priority, 1);
                    status = true;
                    Msg = "Leave Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult LocationList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Location> Modal = new List<Location>();
            Modal = Master.GetLocationList(0);
            return View(Modal);

        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _LocationAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LocationID = GetQueryString[2];

            Location Modal = new Location();
            long ID = 0;
            long.TryParse(ViewBag.LocationID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetLocationList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _LocationAdd(string src, Location Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LocationID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.LocationID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Location is not Saved";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    SaveID = Common_SPU.fnSetLoaction(ID, Modal.Name, Modal.Description, Modal.Priority, 1);
                    status = true;
                    Msg = "Location Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DesignationList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Designation> Modal = new List<Designation>();
            Modal = Master.GetDesignationList(0);
            return View(Modal);

        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _DesignationAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DesignationID = GetQueryString[2];
            Designation Modal = new Designation();
            long ID = 0;
            long.TryParse(ViewBag.DesignationID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetDesignationList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _DesignationAdd(string src, Designation Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DesignationID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.DesignationID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Designation is not Saved";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    SaveID = Common_SPU.fnSetDesign(ID, Modal.DesignationName, Modal.Description, Modal.Priority, 1);
                    status = true;
                    Msg = "Designation Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }






        public ActionResult DepartmentList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Department.List> Modal = new List<Department.List>();

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.IsActive = "0,1";
            Modal = Master.GetDepartmentList(getMasterResponse);
            return View(Modal);

        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _DepartmentAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DesignationID = GetQueryString[2];
            Department.Add Modal = new Department.Add();
            long ID = 0;
            long.TryParse(ViewBag.DesignationID, out ID);
            if (ID > 0)
            {
                GetMasterResponse getMasterResponse = new GetMasterResponse();
                getMasterResponse.LoginID = LoginID;
                getMasterResponse.IPAddress = IPAddress;
                getMasterResponse.ID = ID;
                Modal = Master.GetDepartment(getMasterResponse);
            }
            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _DepartmentAdd(string src, Department.Add Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DepartmentID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.DepartmentID, out ID);
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    Modal.ID = ID;
                    Modal.LoginID = LoginID;
                    Modal.IPAddress = IPAddress;
                    PostResult = Master.fnSetDepartment(Modal);
                }

            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Master/DepartmentList?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Master/DepartmentList");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult CityList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<City> Modal = new List<City>();
            Modal = Master.GetCityList(0);

            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CityAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CityID = GetQueryString[2];
            City Modal = new City();
            long ID = 0;
            long.TryParse(ViewBag.CityID, out ID);
            ViewBag.StateList = Master.GetStateList(0).Where(x => x.IsActive).ToList();
            if (ID > 0)
            {
                Modal = Master.GetCityList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CityAdd(string src, City Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CountryID = GetQueryString[2];
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "City is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {

                    SaveID = Common_SPU.fnSetcity(Modal.CityID, Modal.CityName, Modal.Category, Modal.StateID, Modal.Description, Modal.Priority, 1);
                    status = true;
                    Msg = "City Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult StateList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<State> Modal = new List<State>();
            Modal = Master.GetStateList(0);

            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _StateAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.StateID = GetQueryString[2];
            State Modal = new State();
            long ID = 0;
            long.TryParse(ViewBag.StateID, out ID);
            ViewBag.CountryList = Master.GetCountryList(0).Where(x => x.IsActive).ToList();
            if (ID > 0)
            {
                Modal = Master.GetStateList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _StateAdd(string src, State Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.StateID = GetQueryString[2];
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "State is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {

                    SaveID = Common_SPU.fnSetState(Modal.StateID, Modal.StateName, Modal.CountryID, Modal.Description, Modal.Priority, 1);
                    status = true;
                    Msg = "State Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DiemList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Diem> Modal = new List<Diem>();
            Modal = Master.GetDiemList(0);

            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _DiemAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DiemID = GetQueryString[2];
            Diem Modal = new Diem();
            long ID = 0;
            long.TryParse(ViewBag.DiemID, out ID);

            if (ID > 0)
            {
                Modal = Master.GetDiemList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _DiemAdd(string src, Diem Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DiemID = GetQueryString[2];
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Diem is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {

                    SaveID = Common_SPU.fnSetDiem(Modal.DiemID, Modal.Status, Modal.Category, Modal.PerDiemRate, Modal.HotelRate, Modal.Priority, 1);
                    status = true;
                    Msg = "Diem Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }






        public ActionResult CostCenterList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<CostCenter> Modal = new List<CostCenter>();
            Modal = Master.GetCostCenterList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CostCenterAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CostCenterID = GetQueryString[2];
            CostCenter Modal = new CostCenter();
            long ID = 0;
            long.TryParse(ViewBag.CostCenterID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetCostCenterList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CostCenterAdd(string src, CostCenter Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CostCenterID = GetQueryString[2];
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Cost Center is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {

                    SaveID = Common_SPU.fnSetCostCenter(Modal.CostCenterID, Modal.Code, Modal.Name, Modal.Priority, 1);
                    status = true;
                    Msg = "Cost Center Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult SubLineItemList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<SubLineItem> Modal = new List<SubLineItem>();
            Modal = Master.GetSubLineItemList(0);
            return View(Modal);
        }
        public ActionResult _SubLineItemAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubLineItemID = GetQueryString[2];
            ViewBag.CostCenterList = Master.GetCostCenterList(0).Where(x => x.IsActive);
            SubLineItem Modal = new SubLineItem();
            long ID = 0;
            long.TryParse(ViewBag.SubLineItemID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetSubLineItemList(ID).FirstOrDefault();
            }
            return PartialView(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _SubLineItemAdd(string src, SubLineItem Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubLineItemID = GetQueryString[2];
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "SubLine Item is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {

                    SaveID = Common_SPU.fnSetSubLineItem(Modal.SubLineItemID, Modal.Code, Modal.Name, Modal.CostCenterID, Modal.Priority, 1);
                    status = true;
                    Msg = "SubLine Item Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }







        public ActionResult VendorList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Vendor> Modal = new List<Vendor>();
            Modal = Master.GetVendorList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _VendorAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.VendorID = GetQueryString[2];
            Vendor Modal = new Vendor();
            long ID = 0;
            long.TryParse(ViewBag.VendorID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetVendorList(ID).FirstOrDefault();
            }
            else
            {
                Modal.VendorCode = (Common_SPU.fnGetMaxID("vendor_id") + 1).ToString();
            }
            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _VendorAdd(string src, Vendor Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.VendorID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.VendorID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Vendor is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    SaveID = Common_SPU.fnSetVendorMst(ID, Modal.VendorCode, Modal.VendorName, Modal.Address, Modal.Representative, Modal.ContactNo, Modal.LSTNo, Modal.CSTNo, Modal.PAN, Modal.Priority, 1);
                    status = true;
                    Msg = "Vendor Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DepreciationRateList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<DepreciationRate> Modal = new List<DepreciationRate>();
            Modal = Master.GetDepreciationRateList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _DepreciationRateAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DeptID = GetQueryString[2];
            DepreciationRate Modal = new DepreciationRate();
            long ID = 0;
            long.TryParse(ViewBag.DeptID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetDepreciationRateList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _DepreciationRateAdd(string src, DepreciationRate Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DeptID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.DeptID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Depreciation Rate is not Saved";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    int Multiple = 1;
                    if (string.IsNullOrEmpty(Modal.chkMultiple))
                    {
                        Multiple = 0;
                        Modal.DepRateDouble = 0;
                        Modal.DepRateTriple = 0;
                    }
                    SaveID = Common_SPU.fnSetDepreRate(ID, Modal.MainCode, Modal.Description, Modal.Method, Modal.DepRate,
                       Multiple, Modal.DepRateDouble, Modal.DepRateTriple,
                        Modal.Priority, 1);
                    status = true;
                    Msg = "Depreciation Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ProcurementCommitteeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<ProcurementCommittee> Modal = new List<ProcurementCommittee>();
            Modal = Master.GetProcurementCommitteeList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _ProcurementCommitteeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProcID = GetQueryString[2];
            ProcurementCommittee Modal = new ProcurementCommittee();
            ViewBag.EmpList = CommonSpecial.GetAllEmployeeList();
            long ID = 0;
            long.TryParse(ViewBag.ProcID, out ID);
            if (ID > 0)
            {
                Modal = Master.GetProcurementCommitteeList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _ProcurementCommitteeAdd(string src, ProcurementCommittee Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProcID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ProcID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Procurement Committee is not Saved";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    long EMPID = Modal.EMP_ID ?? 0;
                    SaveID = Common_SPU.fnSetProcurCommMst(ID, EMPID, Modal.Effective_Date, Modal.Priority, 1);
                    status = true;
                    Msg = "Procurement Committee Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }




        public ActionResult FreemealList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<FreeMeal> Modal = new List<FreeMeal>();
            Modal = Master.GetFreeMealList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _FreeMealAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FreeMealID = GetQueryString[2];
            FreeMeal Modal = new FreeMeal();
            long ID = 0;
            long.TryParse(ViewBag.FreeMealID, out ID);
            ViewBag.CityList = Master.GetCityList(0).Where(x => x.IsActive).ToList();
            if (ID > 0)
            {
                Modal = Master.GetFreeMealList(ID).FirstOrDefault();
            }
            return PartialView(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _FreeMealAdd(string src, FreeMeal Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FreeMealID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.FreeMealID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "FreeMeal is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    SaveID = Common_SPU.fnSetFreeMeal(ID, Modal.FreeMealName, Modal.Percentage, Modal.Priority, 1);
                    status = true;
                    Msg = "FreeMeal Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult _AddAttachment(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string tableID = GetQueryString[2];
            string tableName = GetQueryString[3];
            string FileType = GetQueryString[4];
            Attachments Modal = new Attachments();
            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult _AddAttachment(Attachments Modal, string src, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string tableID = GetQueryString[2];
            string tableName = GetQueryString[3];
            string FileType = GetQueryString[4];
            try
            {
                if (ModelState.IsValid)
                {
                    if (Modal.UploadFile != null)
                    {
                        var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadFile);
                        if (RvFile.IsValid)
                        {
                            Modal.AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentID, RvFile.FileName, RvFile.FileExt, Modal.Description, tableID, tableName);
                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt);
                            }
                            Modal.UploadFile.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt));
                            PostResult.Status = true;
                            PostResult.SuccessMessage = "Attachment Saved Successfully";


                            List<Attachments> ListModal = new List<Attachments>();
                            ListModal = ClsCommon.GetAttachmentList(0, tableID, tableName);
                            PostResult.ViewAsString = RenderRazorViewToString("_ViewAllAttachments", ListModal);


                        }
                        else
                        {
                            PostResult.SuccessMessage = RvFile.Message;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);

                        }
                    }
                }
                else
                {
                    PostResult.SuccessMessage = "there is some problem, Please try again..";
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during AddTravelDocuments. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterController", "MasterController", "");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult _ViewAllAttachments(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string tableID = GetQueryString[2];
            string tableName = GetQueryString[3];
            if (GetQueryString.Length == 4)
            {
                ViewBag.DeleteDisabled = "Yes";
            }
            List<Attachments> Modal = new List<Attachments>();
            Modal = ClsCommon.GetAttachmentList(0, tableID, tableName);
            return PartialView(Modal);
        }

        public ActionResult JobList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Job> List = new List<Job>();
            List = Master.GetJobList(0);
            return View(List);
        }

        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _JobAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.JobID = GetQueryString[2];
            //ViewBag.Skills = Master.GetMasterAllList(0, "skills", 0, "1");
            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "skills";
            ViewBag.Skills = ClsCommon.GetDropDownList(getDropDown);


            Job Modal = new Job();
            long JobID = 0;
            long.TryParse(ViewBag.JobID, out JobID);
            if (JobID > 0)
            {
                Modal = Master.GetJobList(JobID).FirstOrDefault();
            }
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        [ValidateInput(false)]
        public ActionResult _JobAdd(string src, Job Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.JobID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.JobID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Job is not Saved";
            if (Modal.SkillsIds == null)
            {
                ModelState.AddModelError("ID", "Enter Skills");
                PostResult.SuccessMessage = "Enter Skills";
            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    string Skills = string.Join(",", Modal.SkillsIds);
                    SaveID = Common_SPU.fnSetJob(ID, Modal.JobCode, Modal.Title, Modal.Experience, Skills, Modal.Description, Modal.QualificationDet, Modal.NoticePeriod, Modal.ProbationPeriod, Modal.Priority, 1);
                    status = true;
                    if (Modal.JobID > 0)
                    {
                        Msg = "Job Updated Successfully";
                    }
                    else
                    {
                        Msg = "Job Saved Successfully";
                    }

                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult JobRound(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.JobID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.JobID, out ID);
            ViewJobDetails Modal = new ViewJobDetails();
            Modal = Master.GetJobDetails(ID);
            return View(Modal);
        }

        public ActionResult _AddJobRound(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.JobID = GetQueryString[2];
            ViewBag.JobDetailsID = GetQueryString[3];
            long JobID = 0, JobDetailsID = 0;
            long.TryParse(ViewBag.JobID, out JobID);
            long.TryParse(ViewBag.JobDetailsID, out JobDetailsID);
            JobRound Modal = new JobRound();
            if (JobDetailsID > 0)
            {
                Modal = Master.GetJobRound(JobID, JobDetailsID).FirstOrDefault();
            }
            ViewBag.EmployeeList = CommonSpecial.GetAllEmployeeList();
            return PartialView(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AddJobRound(string src, JobRound Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.JobID = GetQueryString[2];
            long JobID = 0;
            long.TryParse(ViewBag.JobID, out JobID);

            ViewBag.JobDetailID = GetQueryString[3];
            long JobDetailID = 0;
            long.TryParse(ViewBag.JobDetailID, out JobDetailID);

            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Round and details is not Saved";
            PostResult.SuccessMessage = "Round and details is not Saved";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    int IsNegotiationRound = (Modal.IsNegotiationRound ? 1 : 0);


                    SaveID = Common_SPU.fnSetJobDetails(JobDetailID, JobID, 1, IsNegotiationRound, "Round", 0, "", Modal.RoundName, "", Modal.RoundDesc, "", 0, "", "", Modal.Priority, 1);
                    if (SaveID > 0 && Modal.JobMemberList != null)
                    {
                        int Count = 0;
                        for (int i = 0; i < Modal.JobMemberList.Count; i++)
                        {
                            Count++;
                            Common_SPU.fnSetJobDetails(Modal.JobMemberList[i].JobDetailsID ?? 0, JobID, Count, 0, "Member", SaveID, "", "", "", "", Modal.JobMemberList[i].RoundMemberType, Modal.JobMemberList[i].EMPID, Modal.JobMemberList[i].Name, Modal.JobMemberList[i].Email, Modal.Priority, 1);
                        }
                        clsDataBaseHelper.ExecuteNonQuery("update JobDetails set isdeleted=1,deletedat=getdate() where Srno>" + Count + " and  LinkWithJobDetID=" + SaveID + "");
                    }

                    status = true;
                    Msg = "Round and Details Updated Successfully";
                }
                if (status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }




        //Add Master Attachment

        [HttpPost]
        public ActionResult _AddMasterAttachment(Attachments Modal, string src, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableID = GetQueryString[2];
            ViewBag.TableName = GetQueryString[3];
            ViewBag.ViewFileName = GetQueryString[4];
            ViewBag.VisibleDelete = GetQueryString[5];
            ViewBag.FileType = GetQueryString[6];
            try
            {
                if (ModelState.IsValid)
                {
                    if (Modal.UploadFile != null)
                    {
                        var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadFile);
                        if (RvFile.IsValid)
                        {
                            Modal.Description = (string.IsNullOrEmpty(Modal.Description) ? Modal.FileName : Modal.Description);
                            Modal.AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentID, Modal.FileName, RvFile.FileExt, Modal.Description, ViewBag.TableID, ViewBag.TableName);
                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt);
                            }
                            Modal.UploadFile.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt));
                            PostResult.Status = true;
                            PostResult.SuccessMessage = "Attachment Saved Successfully";


                            List<Attachments> ListModal = new List<Attachments>();
                            ListModal = ClsCommon.GetAttachmentList(0, ViewBag.TableID, ViewBag.TableName);
                            PostResult.ViewAsString = RenderRazorViewToString("_ViewMasterAttachments", ListModal);
                        }
                        else
                        {
                            PostResult.SuccessMessage = RvFile.Message;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);

                        }
                    }
                }
                else
                {
                    PostResult.SuccessMessage = "there is some problem, Please try again..";
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during _AddMasterAttachment. The query was executed :", ex.ToString(), "fnSetAttachments", "MasterController", "MasterController", "");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult _ViewMasterAttachments(string src)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableID = GetQueryString[2];
            ViewBag.TableName = GetQueryString[3];
            ViewBag.ViewFileName = GetQueryString[4];
            ViewBag.VisibleDelete = GetQueryString[5];
            List<Attachments> RVModal = new List<Attachments>();
            RVModal = ClsCommon.GetAttachmentList(0, ViewBag.TableID, ViewBag.TableName);
            return PartialView(RVModal);
        }

        public ActionResult _MasterAttachmentController(string src, long TableID, string TableName, string ViewFileName, string FileType, string[] AttachmentType, bool VisibleDelete, bool VisibleAdd)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Attachments Modal = new Attachments();
            Modal.TableID = TableID;
            Modal.TableName = TableName;
            Modal.FileType = FileType;
            Modal.ViewFileName = ViewFileName;
            Modal.AttachmentType = AttachmentType;
            Modal.VisibleDelete = VisibleDelete;
            Modal.VisibleAdd = VisibleAdd;
            return PartialView(Modal);
        }

        public ActionResult DonorList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Donor.List> Modal = new List<Donor.List>();
            Modal = Master.GetDonorList(getResponse);
            return View(Modal);
        }

        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AddDonor(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            Donor.Add Modal = new Donor.Add();
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            getResponse.ID = ID;
            Modal = Master.GetDonor(getResponse);
            return PartialView(Modal);

        }

        public ActionResult _ViewDonor(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            Donor.View Modal = new Donor.View();
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            getResponse.ID = ID;
            getResponse.Doctype = "View";
            Modal = Master.GetDonorView(getResponse);
            return PartialView(Modal);

        }


        [HttpPost]
        public ActionResult _AddDonor(string src, Donor.Add Modal, string Command)
        {

            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            PostResult.SuccessMessage = "Action Can't Update";
            if (ModelState.IsValid)
            {
                Modal.ID = ID;
                PostResult = Master.fnSetDonor(Modal);
                if (PostResult.Status)
                {
                    EmployeeModal employee = new EmployeeModal();
                    if (Modal.LocalAddress != null && !string.IsNullOrEmpty(Modal.LocalAddress.lane1))
                    {

                        Modal.LocalAddress.Doctype = "Local";
                        Modal.LocalAddress.TableName = "Donor";
                        Modal.LocalAddress.TableID = PostResult.ID;

                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(Modal.LocalAddress);
                        Address localAddressCopy = Newtonsoft.Json.JsonConvert.DeserializeObject<Address>(json);
                        employee.SetAddress(localAddressCopy);
                    }
                    if (Modal.PermanentAddress != null && !string.IsNullOrEmpty(Modal.PermanentAddress.lane1))
                    {
                        Modal.PermanentAddress.Doctype = "Permanent";
                        Modal.PermanentAddress.TableName = "Donor";
                        Modal.PermanentAddress.TableID = PostResult.ID;

                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(Modal.PermanentAddress);
                        Address permanentAddressCopy = Newtonsoft.Json.JsonConvert.DeserializeObject<Address>(json);
                        employee.SetAddress(permanentAddressCopy);
                    }
                    if (Modal.DonorDetailsList != null)
                    {
                        int PCount = 0;
                        foreach (var item in Modal.DonorDetailsList)
                        {
                            PCount++;
                            item.Priority = PCount;
                            item.donor_id = PostResult.ID;
                            Master.fnSetDonorDetail(item);
                        }
                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        #region All Masters

        public ActionResult UserTypeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "UserType";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _UserTypeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "UserType";
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


        public ActionResult BankList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "BankList";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _BankAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "BankList";
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

        public ActionResult CompanyUploadCatList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Comp_Category";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _CompanyUploadCatMstAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "Comp_Category";
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

        public ActionResult RoomList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "room_master";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _RoomAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "room_master";
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
            getDropDown.Doctype = "Location";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);

        }
        public ActionResult FoodCategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "food";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _FoodCategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "food";
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
        public ActionResult CarList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "car_master";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _CarAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "car_master";
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
            getDropDown.Doctype = "Driver";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);

            return PartialView(Modal);

        }

        public ActionResult RelationWithEMPList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "relation";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _RelationWithEMPAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "relation";
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

        public ActionResult EmpCategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "emp_category";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _EmpCategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "emp_category";
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
        public ActionResult LevelCode1List(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "level_code1";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _LevelCode1Add(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "level_code1";
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
        public ActionResult LevelCode2List(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "level_code2";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _LevelCode2Add(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "level_code2";
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


        public ActionResult LevelCode3List(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "level_code3";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _LevelCode3Add(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "level_code3";
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


        public ActionResult AirlineList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "master_airline";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AirlineAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "master_airline";
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

        public ActionResult HotelList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "master_hotel";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _HotelAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "master_hotel";
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
            getDropDown.Doctype = "City";
            ViewBag.List = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);

        }

        public ActionResult CurrencyList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Currency_master";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _CurrencyAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "Currency_master";
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
        public ActionResult ProjectFinanceDesription(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "proj_financial";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _ProjectFinanceDesriptionAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "proj_financial";
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

        public ActionResult ModeOfSubmissionList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "mode_submission";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _ModeOfSubmissionAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "mode_submission";
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

        public ActionResult ProjectActivityList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "ProjectActivity";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _ProjectActivityAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "ProjectActivity";
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

        public ActionResult SkillList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Skills";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _SkillAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "Skills";
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


        public ActionResult ExitReasonsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Exit_Reason";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _ExitReasonsAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "Exit_Reason";
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


        public ActionResult TelDirCategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Area";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _TelDirCategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "main_category";
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



        public ActionResult TelDirLinkageAList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "linkagesa";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _TelDirLinkageAAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "linkagesa";
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


        public ActionResult TelDirLinkageBList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "linkagesb";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }

        public ActionResult _TelDirLinkageBAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "linkagesb";
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



        public ActionResult FundTypeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "FundType";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);

        }
        public ActionResult _AddFundType(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "FundType";
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


        public ActionResult FundingTypeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "FundingType";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);

        }
        public ActionResult _AddFundingType(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "FundingType";
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



        public ActionResult ProgramList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "Program";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);

        }
        public ActionResult _AddProgram(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "Program";
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


        public ActionResult DonorTypeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "DonorType";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);

        }
        public ActionResult _AddDonorType(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "DonorType";
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



        public ActionResult AgreementTypeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "AgreementType";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);

        }
        public ActionResult _AddAgreementType(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "AgreementType";
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



        public ActionResult MealPreferenceList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "MealPreferences";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);

        }
        public ActionResult _AddMealPreference(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "MealPreferences";
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


        public ActionResult SeatPreferenceList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "SeatPreferences";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);

        }
        public ActionResult _AddSeatPreference(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "SeatPreferences";
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

        #endregion


        public ActionResult CompanyConfiguration(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            CompanyConfig model = new CompanyConfig();
            model = Master.fnGetCompnayConfiguration(model.Id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult CompanyConfiguration(string src, string Command, CompanyConfig Modal)
        {

            long success = 0;
            bool status = false;
            string Msg = "";
            string fileName = Modal.FileUpload.FileName;
            string[] str2 = fileName.Split('.');
            PostResponse PostResult = new PostResponse();
            if (ModelState.IsValid)
            {
                TempData["Success"] = "N";
                TempData["SuccessMsg"] = "Company  is not Updated";
                if (Command == "submit")
                {
                    if (Modal.FileUpload != null)
                    {

                        var RvFile = clsApplicationSetting.ValidateFile(Modal.FileUpload);
                        if (RvFile.IsValid)
                        {

                            // string extension = System.IO.Path.GetExtension(Modal.FileUpload.FileName);

                            Modal.FileUpload.SaveAs(Server.MapPath("~/assets/design/images/" + Modal.FileUpload.FileName));
                           //Modal.FileUpload.SaveAs(Server.MapPath("~/assets/design/images/" + "c3.png"));



                        }
                        else
                        {
                            status = false;
                            TempData["Success"] = "N";
                            TempData["SuccessMsg"] = RvFile.Message;
                            return RedirectToAction("CompanyConfiguration", new { src = src });
                        }

                    }
                    success = Master.fnUpdateCompanyConfiguration(Modal.Id, Modal.CompanyName, Modal.CompanyAdress);
                    if (success > 0)
                    {
                        status = true;
                        Msg = "Company Updated Successfully";
                    }
                    if (status == true)
                    {
                        TempData["Success"] = "Y";
                        TempData["SuccessMsg"] = Msg;
                        // return View(Modal);

                        return RedirectToAction("CompanyConfiguration", new { src = src });
                    }
                }

            }
            else
            {
                return View(Modal);

            }

            return View(Modal);
        }

        // Master PerKM

        public ActionResult PerKM(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            long ID = 0;

            PerKM Modal = new PerKM();
            Modal.objkmlist = Master.GetPerKmlist(0);
            return View(Modal);
        }

        public ActionResult _AddPerKM(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];

            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            PerKM Modal = new PerKM();
            if (ID > 0)
            {
                Modal = Master.GetPerKmlist(ID).FirstOrDefault();
            }


            return PartialView(Modal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AddPerKM(string src, PerKM Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Per KM  Can't Save";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    Modal.LoginID = LoginID;
                    Modal.IPAddress = IPAddress;
                    if (Modal.Id == null)
                    {
                        Modal.Id = 0;

                    }
                    PostResult = Master.fnsetAddPerKm(Modal);
                    if (PostResult.Status == true)
                    {
                        PostResult.SuccessMessage = "Action save Data";
                        PostResult.ID = Convert.ToInt64(Modal.Id);
                        ModelState.Clear();
                        return Json(PostResult, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        // Declaration Master

        public ActionResult DeclarationMaster(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            long ID = 0;

            DeclarationMaster Modal = new DeclarationMaster();
            Modal = Master.GetDeclarationMaster();
            return View(Modal);
        }

        public ActionResult _AddDeclarationMaster(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            BudgetMaster.EmployeeList employeeList = new BudgetMaster.EmployeeList();
            employeeList.Id = 0;
            employeeList.Name = "All";
            List<BudgetMaster.EmployeeList> list= Budget.GetBudgetSettingEmpList();
            list.Insert(0,employeeList);
            ViewBag.emplist = list;
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            DeclarationMaster Modal = new DeclarationMaster();
            if (ID > 0)
            {
                Modal = Master.GetRecordDeclarationMaster(ID);
                Modal.DueDate = Convert.ToDateTime(Modal.DueDate).ToString("yyyy-MM-dd");
            }
            return PartialView(Modal);
        }


        //[HttpPost]
        //public ActionResult _AddDeclarationMaster(string src, DeclarationMaster Modal)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    ViewBag.ID = GetQueryString[2];
        //    ViewBag.emplist = Budget.GetBudgetSettingEmpList();
        //    long ID = 0;
        //    long.TryParse(ViewBag.ID, out ID);
        //    return PartialView(Modal);
        //}

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult _AddDeclarationMaster(string src, DeclarationMaster Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            ViewBag.emplist = Budget.GetBudgetSettingEmpList();
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            PostResult.SuccessMessage = "there is some problem, Please try again...";

            if (Modal.Id > 0)
            {
                if (Modal.AttachmentId > 0)
                {
                    if (Modal.RequiredinhardCopy == 0)
                    {
                        ModelState.AddModelError("UploadAttachment", "Please select checkbox");
                        PostResult.SuccessMessage = "Please select checkbox";
                    }
                }
                
            }
            else
            {
                if (Modal.UploadAttachment != null)
                {
                    if (Modal.RequiredinhardCopy == 0)
                    {
                        ModelState.AddModelError("UploadAttachment", "Please select checkbox");
                        PostResult.SuccessMessage = "Please select checkbox";
                    }

                }
                if (Modal.RequiredinhardCopy == 1)
                {
                    if (Modal.AttachmentId == 0)
                    {
                        if (Modal.UploadAttachment == null)
                        {
                            ModelState.AddModelError("UploadAttachment", "Please upload Attachement");
                            PostResult.SuccessMessage = "Please upload Attachement";
                        }
                    }

                }
            }
           
            //if (Modal.Requiredremarksfield == 1)
            //{
            //    if (Modal.Content == null)
            //    {
            //        ModelState.AddModelError("Content", "Please write Content");
            //        PostResult.SuccessMessage = "Please write Content";
            //    }
            //}
            try
            {
                if (ModelState.IsValid)
                {
                    if (Modal.UploadAttachment != null)
                    {
                        var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadAttachment);
                        if (RvFile.IsValid)
                        {
                            Modal.AttachmentId = 0;
                            Modal.AttachmentId = Common_SPU.fnSetAttachments(Modal.AttachmentId, RvFile.FileName, RvFile.FileExt, "");
                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentId + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + Modal.AttachmentId + RvFile.FileExt);
                            }
                            Modal.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentId + RvFile.FileExt));
                        }
                        else
                        {
                            PostResult.SuccessMessage = RvFile.Message;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);

                        }
                    }
                    if (Modal.Content == null)
                    {
                        Modal.Content = "";
                    }
                    if (Modal.Remark == null)
                    {
                        Modal.Remark = "";
                    }
                    string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Master/DeclarationMaster*" + PostResult.ID);
                    PostResult = Master.SetDeclarationMaster(Modal);
                    if (PostResult.Status == true)
                    {
                        PostResult.SuccessMessage = PostResult.SuccessMessage;
                        PostResult.ID = Modal.Id;
                        PostResult.AdditionalMessage = Url;
                        ModelState.Clear();
                        return Json(PostResult, JsonRequestBehavior.AllowGet);


                    }

                }
                else
                {
                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during _AddDeclarationMaster. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterController", "MasterController", "");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult EmployeeDeclarationReport(string src)
        {
            DeclarationReport modal = new DeclarationReport();
           
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PMS.FinList> FList = new List<PMS.FinList>();
            FList = PMS.GetFinYearList();
            ViewBag.FinYearList = FList;
            return View(modal);
        }
        public ActionResult _ReportDeclaration(string src)
        {
            ViewBag.src = src;
            DeclarationReport modal = new DeclarationReport();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            modal.FYId = Convert.ToInt64(GetQueryString[2]);
            modal.EmpType = Convert.ToString(GetQueryString[3]);
            return PartialView(modal);
        }
        public ActionResult EmployeesExcelToExportReport(int? FId, string Type)
        {
            try
            {

                DataSet ds = Common_SPU.fnGetEmployeesDeclartionData(Convert.ToInt64(FId), Type);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                WriteExcelEmployeesData(dt, "xls");
            }
            catch (Exception ex)
            {
                //"EmployeeList", Modal
            }
            return View();
        }

        public void WriteExcelEmployeesData(DataTable dt, string extension)
        {
            try
            {
                IWorkbook workbook;

                if (extension == "xlsx")
                {
                    workbook = new XSSFWorkbook();
                }
                else if (extension == "xls")
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    throw new Exception("This format is not supported");
                }

                ISheet sheet1 = workbook.CreateSheet("Sheet 1");
                //make a header row
                IRow row1 = sheet1.CreateRow(0);

                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Columns[j].ToString());

                    ICellStyle style = workbook.CreateCellStyle();

                    //Set border style 
                    //style.BorderBottom = BorderStyle.Thick;
                    //style.BottomBorderColor = HSSFColor.Black.Index;

                    //Set font style
                    IFont font = workbook.CreateFont();
                    //font.Color = HSSFColor.White.Index;
                    font.FontName = "Arial";
                    font.FontHeight = 200;
                    font.IsBold = true;
                    style.SetFont(font);

                    //Set background color
                    //style.FillForegroundColor = IndexedColors.DarkBlue.Index;
                    //style.FillPattern = FillPattern.SolidForeground;
                    style.Alignment = HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Center;

                    //Apply the style
                    cell.CellStyle = style;
                }

                //loops through data
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        string columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(dt.Rows[i][columnName].ToString());
                    }
                }

                using (var exportData = new MemoryStream())
                {
                    //Response.ClearContent();
                    Response.Clear();
                    //Response.Flush();
                    //Response.Buffer = true;
                    workbook.Write(exportData,false  );
                      string attach = string.Format("attachment;filename={0}", "EmployeesDeclaration.xls");
                    if (extension == "xlsx") //xlsx file format
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("Content-Disposition", attach);
                        Response.BinaryWrite(exportData.ToArray());
                    }
                    else if (extension == "xls")  //xls file format
                    {
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", attach);
                        Response.BinaryWrite(exportData.GetBuffer());
                    }
                    Response.End();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public ActionResult Officelisting(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<OfficeListing> Modal = new List<OfficeListing>();
            Modal = Master.GetOfficeListing(0);
            return View(Modal);
        }
        public ActionResult _AddOfficeListing(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            OfficeListing officeListing = new OfficeListing();
            ViewBag.EmpList = CommonSpecial.GetAllEmployeeOfficeList();
            ViewBag.LocationList = Master.GetLocationOfficeListnew(0);
            if (ID > 0)
            {
                officeListing = Master.GetOfficeListing(ID).FirstOrDefault();
                ViewBag.LocationList = Master.GetLocationOfficeList(0);
            }
         
            return PartialView(officeListing);

        }
        [HttpPost]
        public ActionResult _AddOfficeListing(string src, OfficeListing Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Action Can't Save";

            if (Modal.LocationIds == null)
            {
               
                    ModelState.AddModelError("BranchOffice", "Hey! You missed this field");
                    PostResult.SuccessMessage = "Hey! You missed this field";
                
            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    string LOcId = string.Join(",", Modal.LocationIds);
                    Modal.LOcId = LOcId;
                    PostResult = Master.fnsetAddOfficeListing(Modal);
                    if (PostResult.Status == true)
                    {
                       // PostResult.SuccessMessage = "Action save Data";
                        PostResult.ID = Convert.ToInt64(Modal.Id);
                        ModelState.Clear();
                        return Json(PostResult, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult BloodGroupList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<BloodGroup> Modal = new List<BloodGroup>();
            Modal = Master.GetBloodGroupList(0);
            return View(Modal);
        }
       
        public ActionResult _BloodGroupAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CompanyUploadID = GetQueryString[2];
            BloodGroup Modal = new BloodGroup();
            long ID = 0;
            long.TryParse(ViewBag.CompanyUploadID, out ID);

            if (ID > 0)
            {
                Modal = Master.GetBloodGroupList(ID).FirstOrDefault();
            }
            //GetResponse getDropDown = new GetResponse();
            //getDropDown.Doctype = "Comp_Category";
            //Modal.BloodGroupList = ClsCommon.GetDropDownList(getDropDown);
            return PartialView(Modal);

        }
        [HttpPost]
        public ActionResult _BloodGroupAdd(string src,BloodGroup Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Action Can't Save";

            if (Modal.Blood_Group_Name == null)
            {

                ModelState.AddModelError("Blood GroupName", "Hey! You missed this field");
                PostResult.SuccessMessage = "Hey! You missed this field";

            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    Modal.Blood_Group_Name = Modal.Blood_Group_Name;
                    Modal.Description = Modal.Description;
                    PostResult = Master.fnsetBloodGroupListing(Modal);
                    if (PostResult.Status == true)
                    {
                        // PostResult.SuccessMessage = "Action save Data";
                        PostResult.ID = Convert.ToInt64(Modal.ID);
                        ModelState.Clear();
                        return Json(PostResult, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
    }
}