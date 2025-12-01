using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Areas.Procurement.Controllers
{
    public class ProcurementController : BaseController
    {
        // GET: Procurement/Procurement
        public ActionResult Index()
        {
            return View();
        }
        
      public ActionResult AuthorisedSignatoryMaster()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();

        }
        public ActionResult ProcurementActionRequest()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();

        }
        public ActionResult QuotationEntry()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();

        }
        public ActionResult RFPCreationScreen()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
          
            return View();

        }
        public ActionResult ModuleAdmin()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();

        }
        public ActionResult RFPProcess()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();

        }

        public ActionResult PaymentApprovalAcknowledgement()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();

        }
        
        public ActionResult RFPEntry(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;
            return View();
             
        }
        public ActionResult RFPProcessPending(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;
            return View();

        }
        public ActionResult ModuleAdminPendingProcessed(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;
            return View();

        }
        public ActionResult RPFProcesslive(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;
            return View();

        }
        public ActionResult QuotationEntryPending(string id )
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;
            
            return View();

        }
        public ActionResult QuotationEntryPendingRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult QuotationEntryPendingConsultant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;
            
            return View();

        }
        public ActionResult QuotationEntryPendingConsultantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult QuotationEntryPendingSubgrant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;
            
            return View();

        }
        public ActionResult QuotationEntryPendingSubgrantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }

        public ActionResult FinancePaymentApprovalPending(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;
            return View();

        }
        

        public ActionResult VenderType()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        public ActionResult RelationshipMaster()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        public ActionResult GoodAndServicesCategory()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        public ActionResult PurposeMaster()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        public ActionResult ApprovalAuthority()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        public ActionResult ProcureCommittee()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        public ActionResult VendorRegistration(int ? id, int ? isApproved)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.IsProcurementApprover = clsApplicationSetting.GetSessionValue("IsProcurementApprover");
            
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            ViewBag.IsApproved = isApproved != null ? isApproved.ToString() : "0";
            return View();
        }
        public ActionResult VendorRegistrationList()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }
        public ActionResult VendorRegistrationApprovalRequest()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }

        public ActionResult VendorRegistrationApproval(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            return View();
        }
        public ActionResult VendorRegistrationFormngoApproved(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            return View();
        }
        public ActionResult VendorRegistrationFromngoImpanel(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            return View();
        }
        public ActionResult VendorRegistrationLegacy(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            return View();
        }
        public ActionResult VendorRegistrationApprovedByApprover(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            return View();
        }
        public ActionResult VendorRegistrationRejectedByApprover(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            return View();
        }

        public ActionResult ProcurementUserRequest()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");           
            string isHOD= clsApplicationSetting.GetSessionValue("IsED");
            string IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            string IsModuleAdmin = clsApplicationSetting.GetSessionValue("IsModuleAdmin");
            


            if (isHOD == "Y" || IsPM=="1" || IsModuleAdmin=="Y")
            {
                return RedirectToAction("ProcurementApproval");
            }
            else
            {
                return View();
            }
           
        }
        public ActionResult ProcurementApprovalRequest()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }

        public ActionResult ProcurementCommitteeScreen()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }

        public ActionResult ProcurementCommitteeQuotation(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult ProcurementCommitteeQuotationRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult ProcurementCommitteeQuotationConsultant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult ProcurementCommitteeQuotationConsultantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult ProcurementCommitteeQuotationSubgrant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult ProcurementCommitteeQuotationSubgrantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }

        public ActionResult ModuleAdminQuotation(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult ModuleAdminQuotationRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult ModuleAdminQuotationConsultant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult ModuleAdminQuotationConsultantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult ModuleAdminQuotationSubgrant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult ModuleAdminQuotationSubgrantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult AuthorisedSignatoryApproval()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");            

            return View();

        }

        public ActionResult AuthorisedSignatoryQuotation(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult AuthorisedSignatoryQuotationRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult AuthorisedSignatoryQuotationConsultant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult AuthorisedSignatoryQuotationConsultantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult AuthorisedSignatoryQuotationSubgrant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult AuthorisedSignatoryQuotationSubgrantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }


        public ActionResult UserModuleAdminQuotation(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult UserModuleAdminQuotationRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult UserModuleAdminQuotationConsultant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult UserModuleAdminQuotationConsultantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult UserModuleAdminQuotationSubgrant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult UserModuleAdminQuotationSubgrantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }

        public ActionResult FinanceApproval()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();

        }
        public ActionResult FinanceQuotation(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult FinanceQuotationRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult FinanceQuotationConsultant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult FinanceQuotationConsultantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult FinanceQuotationSubgrant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult FinanceQuotationSubgrantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult TermCondition()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();

        }

        public ActionResult VendorSearch()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");

            return View();

        }
        public ActionResult VendorRegistrationSearch(int? id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.VendorRegId = id != null ? id.ToString() : "0";
            return View();
        }
        public ActionResult ProcurementApproval()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            return View();
        }

        public ActionResult UserAmendmentQuotation(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult UserAmendmentQuotationRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult UserAmendmentQuotationConsultant(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }
        public ActionResult UserAmendmentQuotationConsultantRFP(string id)
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
            ViewBag.RequestId = id;

            return View();

        }

        public ActionResult Report()
        {
            ViewBag.UserId = clsApplicationSetting.GetSessionValue("LoginID");
            ViewBag.UserName = clsApplicationSetting.GetSessionValue("UserName");
            ViewBag.Grade = clsApplicationSetting.GetSessionValue("Grade");
            ViewBag.IsPM = clsApplicationSetting.GetSessionValue("IsPM");
            ViewBag.IsED = clsApplicationSetting.GetSessionValue("IsED");
           

            return View();

        }

    }
}