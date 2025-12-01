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
    public class ProjectsController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IProjectsHelper Project;
        public ProjectsController()
        {
            Project = new ProjectsModal();
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

        public ActionResult ProjectRegistrationList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ListTabs Modal = new ListTabs();
            Modal.Approve = 0;
            return View(Modal);
        }
        public ActionResult _ProjectRegistrationList(string src, ListTabs Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Project.List> result = new List<Project.List>();
            getResponse.Approve = Modal.Approve;
            result = Project.GetProjectRegistrationList(getResponse);
            return PartialView(result);
        }

        public ActionResult ProjectDetails(string src)
        {
            ViewBag.TabIndex = 1;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Project.ProjectDetails result = new Project.ProjectDetails();
            getResponse.ID = ID;
            getResponse.Doctype = "ProjectDetails";
            result = Project.GetProjectDetails(getResponse);
            result = Project.GetProjectDetails(getResponse);
            return View(result);
        }
        [HttpPost]
        public ActionResult ProjectDetails(string src, Project.ProjectDetails Modal, string Command)
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
            if (ModelState.IsValid)
            {
                Modal.ID = ID;
                PostResult = Project.SetProjectDetails(Modal);

            }
            if(Command=="Live")
            {
                getResponse.ID = ID;
                PostResult = Project.SetProjectMakeItLive(getResponse);
                PostResult.RedirectURL = "/Projects/ProjectDetails?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Projects/ProjectDetails*" + ID);
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Projects/ClientsDetails?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Projects/ClientsDetails*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ClientsDetails(string src)
        {
            ViewBag.TabIndex = 2;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Project.ClientsDetails result = new Project.ClientsDetails();
            getResponse.ID = ID;
            getResponse.Doctype = "ClientsDetails";
            result = Project.GetProjectClientsDetails(getResponse);
            return View(result);
        }

        [HttpPost]
        public ActionResult ClientsDetails(string src, Project.ClientsDetails Modal, string Command)
        {
            ViewBag.TabIndex = 2;
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
                PostResult = Project.SetProjectClientsDetails(Modal);

                if (PostResult.Status)
                {
                    if (Modal.ContactPersonList != null)
                    {
                        foreach (var item in Modal.ContactPersonList)
                        {
                            item.ProjectID = PostResult.ID;
                            item.donor_id = Modal.donor_id;
                            var current = Project.SetProject_ContactPerson(item);
                            if (!current.Status)
                            {
                                PostResult.Status = false;
                                PostResult.SuccessMessage = current.SuccessMessage;
                                return Json(PostResult, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                }
            }
            if (Command == "Live")
            {
                getResponse.ID = ID;
                PostResult = Project.SetProjectMakeItLive(getResponse);
                PostResult.RedirectURL = "/Projects/ClientsDetails?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Projects/ClientsDetails*" + ID);
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Projects/BudgetDetails?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Projects/BudgetDetails*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult BudgetDetails(string src)
        {
            ViewBag.TabIndex = 3;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Project.BudgetDetails result = new Project.BudgetDetails();
            getResponse.ID = ID;
            getResponse.Doctype = "BudgetDetails";
            result = Project.GetProjectBudgetDetails(getResponse);
            return View(result);
        }
        [HttpPost]
        public ActionResult BudgetDetails(string src, Project.BudgetDetails Modal, string Command)
        {
            ViewBag.TabIndex = 3;
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
                PostResult = Project.SetProjectBudgetDetails(Modal);
            }
            if (Command == "Live")
            {
                getResponse.ID = ID;
                PostResult = Project.SetProjectMakeItLive(getResponse);
                PostResult.RedirectURL = "/Projects/BudgetDetails?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Projects/BudgetDetails*" + ID);
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Projects/SpecialConditions?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Projects/SpecialConditions*" + PostResult.ID);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SpecialConditions(string src)
        {
            ViewBag.TabIndex = 4;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Project.SpecialConditions result = new Project.SpecialConditions();
            getResponse.ID = ID;
            getResponse.Doctype = "SpecialConditions";
            result = Project.GetProjectSpecialConditions(getResponse);
            return View(result);
        }
        public ActionResult _AddSpecialConditions(string src)
        {
            ViewBag.TabIndex = 4;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Project.SpecialConditions.Condition.Add result = new Project.SpecialConditions.Condition.Add();
            getResponse.Doctype = "EMP";
            //result.EMPList = ClsCommon.GetDropDownList(getResponse);
            result.EMPList = new List<DropDownList>();
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult _AddSpecialConditions(string src, Project.SpecialConditions.Condition.Add Modal, string Command)
        {
            ViewBag.TabIndex = 4;
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
                Modal.ProjectID = ID;
                PostResult = Project.SetProject_SpecialConditions(Modal);
            }
            if (PostResult.Status)
            {
                Project.SpecialConditions result = new Project.SpecialConditions();
                getResponse.ID = ID;
                getResponse.Doctype = "SpecialConditions";
                result = Project.GetProjectSpecialConditions(getResponse);
                PostResult.ViewAsString = RenderRazorViewToString("_ListSpecialConditions", result.conditionList);
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DonorsReport(string src)
        {
            ViewBag.TabIndex = 5;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Project.DonorsReport result = new Project.DonorsReport();
            getResponse.ID = ID;
            getResponse.Doctype = "DonorsReport";
            result = Project.GetProjectDonorsReport(getResponse);
            return View(result);
        }
        [HttpPost]
        public ActionResult DonorsReport(string src, Project.DonorsReport Modal, string Command)
        {
            ViewBag.TabIndex = 5;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            PostResult.SuccessMessage = "Action Can't Update";
            string _Path = clsApplicationSetting.GetPhysicalPath();
            if (ModelState.IsValid)
            {
                if (Modal.reportList != null)
                {
                    int count = 0;
                    foreach (var item in Modal.reportList)
                    {
                        count++;
                        item.ProjectID = ID;
                        item.Priority = count;
                        if (item.upload != null)
                        {
                            var RvFile = clsApplicationSetting.ValidateFile(item.upload);
                            if (RvFile.IsValid)
                            {
                                item.AttachmentID = Common_SPU.fnSetAttachments(0, RvFile.FileName, RvFile.FileExt, "");
                                item.upload.SaveAs(Path.Combine(_Path, item.AttachmentID + RvFile.FileExt));
                            }
                            else
                            {
                                PostResult.Status = RvFile.IsValid;
                                PostResult.SuccessMessage = RvFile.Message;
                                return Json(PostResult, JsonRequestBehavior.AllowGet);
                            }
                        }
                        PostResult = Project.SetProject_DonorsReport(item);
                    }
                }

            }
            if (Command == "Live")
            {
                getResponse.ID = ID;
                PostResult = Project.SetProjectMakeItLive(getResponse);
                PostResult.RedirectURL = "/Projects/DonorsReport?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Projects/DonorsReport*" + ID);
                return Json(PostResult, JsonRequestBehavior.AllowGet);
            }
            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Projects/Attachment?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Projects/Attachment*" + ID);
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Attachment(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Project.Attachment result = new Project.Attachment();
            getResponse.ID = ID;
            getResponse.Doctype = "Attachments";
            result = Project.GetProjectAttachment(getResponse);
            return View(result);
        }

        public ActionResult _AddProjectAttachment(string src)
        {
            ViewBag.TabIndex = 6;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            Project.Attachment.Document.Add result = new Project.Attachment.Document.Add();
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult _AddProjectAttachment(string src, Project.Attachment.Document.Add Modal, string Command)
        {
            ViewBag.TabIndex = 6;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            PostResult.SuccessMessage = "Action Can't Update";
            string _Path = clsApplicationSetting.GetPhysicalPath();
            if (ModelState.IsValid)
            {
                Modal.ProjectID = ID;
                if (Modal.upload != null)
                {
                    var RvFile = clsApplicationSetting.ValidateFile(Modal.upload);
                    if (RvFile.IsValid)
                    {
                        Modal.AttachmentID = Common_SPU.fnSetAttachments(0, RvFile.FileName, RvFile.FileExt, "");
                        Modal.upload.SaveAs(Path.Combine(_Path, Modal.AttachmentID + RvFile.FileExt));
                    }
                    else
                    {
                        PostResult.Status = RvFile.IsValid;
                        PostResult.SuccessMessage = RvFile.Message;
                        return Json(PostResult, JsonRequestBehavior.AllowGet);
                    }
                }

                PostResult = Project.SetProject_Attachment(Modal);
            }
            if (PostResult.Status)
            {
                Project.Attachment result = new Project.Attachment();
                getResponse.ID = ID;
                getResponse.Doctype = "Attachments";
                result = Project.GetProjectAttachment(getResponse);
                PostResult.ViewAsString = RenderRazorViewToString("_ListProjectAttachments", result.documentList);
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        public ActionResult _PendingDonorContactList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProjectID = GetQueryString[2];
            ViewBag.Donor_Id = GetQueryString[3];
            int Donor_Id = 0, ProjectID = 0;
            int.TryParse(ViewBag.Donor_Id, out Donor_Id);
            int.TryParse(ViewBag.ProjectID, out ProjectID);
            List<Project.DonorDetails> result = new List<Project.DonorDetails>();
            getResponse.ID = ProjectID;
            getResponse.AdditionalID = Donor_Id;
            result = Project.GetProject_PendingContactPerson(getResponse);
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult _PendingDonorContactList(string src, List<Project.DonorDetails> Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ProjectID = GetQueryString[2];
            ViewBag.Donor_Id = GetQueryString[3];
            int Donor_Id = 0, ProjectID = 0;
            int.TryParse(ViewBag.Donor_Id, out Donor_Id);
            int.TryParse(ViewBag.ProjectID, out ProjectID);
            PostResult.SuccessMessage = "Action Can't Update";
            if (ModelState.IsValid)
            {

                if (Modal != null)
                {
                    int Count = 0;
                    foreach (var item in Modal.Where(x => !string.IsNullOrEmpty(x.Chk)).ToList())
                    {
                        Project.ContactPerson it = new Project.ContactPerson();
                        it.ProjectID = ProjectID;
                        it.designation = item.designation;
                        it.DonorDetailsID = item.ID;
                        it.donor_id = item.donor_id;
                        it.person_name = item.person_name;
                        it.location = item.location;
                        it.phone_no = item.phone_no;
                        it.email = item.email;
                        it.Priority = Count;
                        PostResult = Project.SetProject_ContactPerson(it);

                    }
                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SetProjectMakeItLive(string src)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            getResponse.ID = ID;
            PostResult.SuccessMessage = "Action Can't Update";
            PostResult = Project.SetProjectMakeItLive(getResponse);
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }
    }
}