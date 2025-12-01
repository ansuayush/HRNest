using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class TicketController : Controller
    {
        ITicketHelper Ticket;
        IMasterHelper Master;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;

        public TicketController()
        {
            Ticket = new TicketModal();
            Master = new MasterModal();

            getResponse = new GetResponse();
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
        public ActionResult LocationGroupList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Helpdesk.LocationGroup> List = new List<Helpdesk.LocationGroup>();
            List = Master.GetLocationGroupList(0);
            return View(List);
        }

        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _LocationGroupAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LocationGroupID = GetQueryString[2];
            Helpdesk.LocationGroup List = new Helpdesk.LocationGroup();
            long LocationGroupID = 0;
            long.TryParse(ViewBag.LocationGroupID, out LocationGroupID);
            ViewBag.LocationList = Master.GetLocationList(0).Where(x => x.IsActive);
            if (LocationGroupID > 0)
            {
                List = Master.GetLocationGroupList(LocationGroupID).FirstOrDefault();
            }
            return PartialView(List);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _LocationGroupAdd(string src, Helpdesk.LocationGroup Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.LocationGroupID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.LocationGroupID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Group is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    string LocationIDs = String.Join(",", Modal.LocationIDs);
                    SaveID = Common_SPU.fnSetLocationGroup(ID, Modal.GroupName, Modal.Description, LocationIDs, Modal.Priority, 1);
                    status = true;
                    Msg = "Group Updated Successfully";
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


        public ActionResult TicketCategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            GetMasterResponse getMasterResponse = new GetMasterResponse();
            getMasterResponse.LoginID = LoginID;
            getMasterResponse.IPAddress = IPAddress;
            getMasterResponse.TableName = "TicketCategory";
            getMasterResponse.IsActive = "0,1";
            List<MasterAll.List> Modal = new List<MasterAll.List>();
            Modal = Master.GetMasterAllList(getMasterResponse);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _TicketCategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            string TableName = "TicketCategory";
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
       
        public ActionResult SubCategory(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Helpdesk.SubCategory> Modal = new List<Helpdesk.SubCategory>();
            Modal = Ticket.GetTicket_SubCategoryList(0);
            return View(Modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _SubCategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubCategoryID = GetQueryString[2];
            Helpdesk.SubCategory Modal = new Helpdesk.SubCategory();
            long ID = 0;
            long.TryParse(ViewBag.SubCategoryID, out ID);

          
            if (ID > 0)
            {
                Modal = Ticket.GetTicket_SubCategoryList(ID).FirstOrDefault();
            }


            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "TicketCategory";
            ViewBag.CategoryList = ClsCommon.GetDropDownList(getDropDown);

            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _SubCategoryAdd(string src, Helpdesk.SubCategory Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubCategoryID = GetQueryString[2];
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Sub Category is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {

                    SaveID = Common_SPU.fnSetTicket_SubCategory(Modal.SubCategoryID,Modal.CategoryID,Modal.Name,Modal.RelatedTo,Modal.Description, Modal.Priority, 1);
                    status = true;
                    Msg = "Sub Category Updated Successfully";
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

        public ActionResult _TicketSettingPopUp(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubCategoryID = GetQueryString[2];
            long SubCategoryID = 0;
            long.TryParse(ViewBag.SubCategoryID, out SubCategoryID);
            return PartialView();
        }
        public ActionResult _AddTicketSetting_Assignment(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubCategoryID = GetQueryString[2];
            long SubCategoryID = 0;
            long.TryParse(ViewBag.SubCategoryID, out SubCategoryID);
            Helpdesk.TicketSetting_Assignment Modal = new Helpdesk.TicketSetting_Assignment();
            ViewBag.LocationGroup = Master.GetLocationGroupList(0).Where(x=>x.IsActive);
            ViewBag.EmployeeList = CommonSpecial.GetUsermanList();
            return PartialView(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AddTicketSetting_Assignment(string src, Helpdesk.TicketSetting_Assignment Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubCategoryID = GetQueryString[2];
            long SubCategoryID = 0;
            long.TryParse(ViewBag.SubCategoryID, out SubCategoryID);
            PostResult.SuccessMessage = "Assignment Details can't updated";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    SaveID = Common_SPU.fnSetTicketSetting(SubCategoryID, Modal.LocationGroupID,"Assignment",Modal.PrimaryAssignee,Modal.Supervisor,Modal.Escalation,"",0,0,0,0, 0, 1);
                    PostResult.Status = true;
                    PostResult.SuccessMessage = "Assignment Details updated Successfully";

                    List<Helpdesk.TicketSetting_Assignment> ListModal = new List<Helpdesk.TicketSetting_Assignment>();
                    ListModal = Ticket.GetTicket_AssignmentList(0, SubCategoryID, "Assignment");
                    PostResult.ViewAsString = RenderRazorViewToString("_TicketSetting_AssignmentList", ListModal);

                    
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _TicketSetting_AssignmentList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubCategoryID = GetQueryString[2];
            long SubCategoryID = 0;
            long.TryParse(ViewBag.SubCategoryID, out SubCategoryID);
            List<Helpdesk.TicketSetting_Assignment> ListModal = new List<Helpdesk.TicketSetting_Assignment>();
            ListModal = Ticket.GetTicket_AssignmentList(0, SubCategoryID, "Assignment"); 
            return PartialView(ListModal);
        }

        public ActionResult _AddTicketSetting_SLA(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubCategoryID = GetQueryString[2];
            long SubCategoryID = 0;
            long.TryParse(ViewBag.SubCategoryID, out SubCategoryID);
            List<Helpdesk.TicketSetting_SMA> SMAModal = new List<Helpdesk.TicketSetting_SMA>();
            SMAModal= Ticket.GetTicket_SMAList(0, SubCategoryID, "SLA");
            if (SMAModal.Count == 0)
            {
                string Prio = "High,Medium,Low";
                foreach (var a in Prio.Split(','))
                {
                    Helpdesk.TicketSetting_SMA mst = new Helpdesk.TicketSetting_SMA();
                    mst.PolicyPriority = a;
                    SMAModal.Add(mst);
                }
            }
            return PartialView(SMAModal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AddTicketSetting_SLA(string src, List<Helpdesk.TicketSetting_SMA> Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SubCategoryID = GetQueryString[2];
            long SubCategoryID = 0;
            long.TryParse(ViewBag.SubCategoryID, out SubCategoryID);
            PostResult.SuccessMessage = "SLA Setting not Updated";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    for (int i = 0; i < Modal.Count; i++)
                    {
                        SaveID = Common_SPU.fnSetTicketSetting(SubCategoryID, 0, "SLA",0, 0,0,Modal[i].PolicyPriority, Modal[i].ResponseTime, Modal[i].FollowUpTime, Modal[i].EscalationTime, Modal[i].Reopen, (i+1), 1);
                    }
                    if (SaveID > 0)
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = "SLA Setting Updated Successfully";
                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Configuration(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Helpdesk.TicketConfigration Modal = new Helpdesk.TicketConfigration();
            Modal = Ticket.GetTicketConfigration();
            return View(Modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult Configuration(string src, Helpdesk.TicketConfigration Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Configuration Setting not Updated";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    var date = new DateTime();
                    var Starttime = TimeSpan.Parse(Modal.StartTime);
                    var Startcombo = date + Starttime;

                    var Endtime = TimeSpan.Parse(Modal.EndTime);
                    var Endcombo = date + Endtime;
                    var selectedDays = string.Join(",", Modal.ApplicableDays);

                    if (!string.IsNullOrEmpty(selectedDays))
                    {
                        SaveID = Common_SPU.fnSetTicket_Configration(Modal.TicketCongID, Modal.StartTime, Modal.EndTime, selectedDays, Modal.IsHolidayCalApplicable, 0, 1);
                        if (SaveID > 0)
                        {
                            PostResult.Status = true;
                            PostResult.SuccessMessage = "Configuration Setting Updated Successfully";
                        }
                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult LocationGroup()
        {
            return View();
        }
        public ActionResult Myticket(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Helpdesk.Ticket> Modal = new List<Helpdesk.Ticket>();
            Modal = Ticket.GetTicketList(0);
            return View(Modal);
        }
        public ActionResult _CreateTicket(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Helpdesk.CreateTicket Modal = new Helpdesk.CreateTicket();

            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "TicketCategory";
            ViewBag.CategoryList = ClsCommon.GetDropDownList(getDropDown);

            Modal.SubCategoryList = new List<Helpdesk.SubCategory>();
            Modal.PriorityList = new List<Helpdesk.TicketSetting_SMA>();
            var LocationModal = Ticket.GetMyLocationGroup();
            Modal.LocationGroup = LocationModal.GroupName;
            Modal.LocationGroupID = LocationModal.LocationGroupID;
            return PartialView(Modal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CreateTicket(string src, Helpdesk.CreateTicket Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Ticket is not created";
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    if (Modal.UploadFile != null)
                    {
                        var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadFile);
                        if (RvFile.IsValid)
                        {
                            Modal.AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentID, RvFile.FileName, RvFile.FileExt, "");
                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt);
                            }
                            Modal.UploadFile.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt));
                        }
                        else
                        {
                            PostResult.SuccessMessage = RvFile.Message;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);
                        }
                    }
                    SaveID = Common_SPU.fnSetCreateTicket(0, Modal.CategoryID, Modal.SubCatgeoryID, Modal.LocationGroupID, Modal.SelectedPriorityID, Modal.Message, Modal.AttachmentID, 1);
                    if (SaveID > 0)
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = "Ticket created Successfully";
                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TicketStatusList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Helpdesk.Ticket_Status> List = new List<Helpdesk.Ticket_Status>();
            List = Ticket.GetTicketStatusList(0, "", "0,1");
            return View(List);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _TicketStatusAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TicketStatusID = GetQueryString[2];
            Helpdesk.Ticket_Status Modal = new Helpdesk.Ticket_Status();
            long ID = 0;
            long.TryParse(ViewBag.TicketStatusID, out ID);
            if (ID > 0)
            {
                Modal = Ticket.GetTicketStatusList(ID, "", "0,1").FirstOrDefault();
            }
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _TicketStatusAdd(string src, Helpdesk.Ticket_Status Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TicketStatusID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.TicketStatusID, out ID);
            bool status = false;
            string Msg = "";
            TempData["Success"] = "N";
            TempData["SuccessMsg"] = "Status is not Saved";

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {

                    SaveID = Common_SPU.fnSetTicket_Status(ID,Modal.Type,Modal.StatusName,Modal.DisplayName,Modal.StatusColor,Modal.Priority,1);
                    status = true;
                    Msg = "Status Updated Successfully";
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




        public ActionResult TicketNotes(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TicketID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.TicketID, out ID);
            Helpdesk.TicketDetails modal = new Helpdesk.TicketDetails();
            if (ID > 0)
            {
                modal = Ticket.GetTicketDetails(ID, 0);
            }
            return View(modal);
        }

        public ActionResult _AddTicketNotes(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TicketID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.TicketID, out ID);
            Helpdesk.TicketNotes modal = new Helpdesk.TicketNotes();
            ViewBag.TicketStatus= Ticket.GetTicketStatusList(0, "", "1");
            ViewBag.EmployeeList = CommonSpecial.GetUsermanList();
            return PartialView(modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AddTicketNotes(string src, Helpdesk.TicketNotes Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();

            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TicketID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.TicketID, out ID);
            PostResult.SuccessMessage = "Notes Can't  Update";
            if(Modal.StatusID==4 && Modal.ForwardTo==null)
            {
                PostResult.SuccessMessage = "Forward To can't be blank";
                ModelState.AddModelError("ForwardTo", "Forward To can't be blank");
            }
            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    DateTime dt;
                    DateTime.TryParse(Modal.NextDate, out dt);
                    DateTime.TryParse((string.IsNullOrEmpty(Modal.NextDate) ? "01/01/1900" : Modal.NextDate), out dt);
                    SaveID = Common_SPU.fnSetTicket_Notes(Modal.TicketNotesID, ID,Modal.StatusID, dt, Modal.Remarks,(Modal.ForwardTo??0),1);
                    PostResult.Status = true;
                    PostResult.SuccessMessage = "Notes Updated Successfully";
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
    }
}