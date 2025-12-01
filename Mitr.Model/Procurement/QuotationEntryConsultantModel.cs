using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Procurement
{
    public class QuotationEntryConsultantModel
    {
		public int Id { get; set; }
        public int Procure_Request_Id { get; set; }
        public DateTime EstimatedStartDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public string ContractType { get; set; }
        public string ScopeofworkNewFileName { get; set; }
        public string ScopeofworkActualFileName { get; set; }
        public string ScopeofworkActualFileUrl { get; set; }
        
        public string BudgetAttachmentNewFileName { get; set; }
        public string BudgetAttachmentActualFileName { get; set; }
        public string BudgetAttachmentActualFileUrl { get; set; }

        public string SpecialConditions { get; set; }
        public bool IsNonRegistratorVendor { get; set; }
        public string Justification { get; set; }
        public int Status { get; set; }
        public string ApprovalRemark { get; set; }
        public string IPAddress { get; set; }
        public string Reimbursable { get; set; }
        public string UnitType { get; set; }
        public int ModuleStatus { get; set; }
        public bool IsModuleAdminSubmit { get; set; }
        public int POC { get; set; }
        public List<QuotationEntryNatureofAttachmentConsultant> QuotationEntryNatureofAttachmentList { get; set; }

        public List<QuotationEntryDetailConsultant> QuotationEntryDetailList { get; set; }

        public List<QuotationEntryDetailFixedConsultant> QuotationEntryDetailFixedConsultantList { get; set; }

        public List<QuotationEntryDetailsReimbursableConsultant> QuotationEntryDetailsReimbursableConsultantList { get; set; }
        public List<UploadSignedDocument> UploadSignedDocumentList { get; set; }
        public decimal AmendReqNo { get; set; }
        public string NatureofAmendment { get; set; }
        public string AmendReason { get; set; }
        public string TotalRequiredAmount { get; set; }

        public List<AmendmentProcurementData> AmendmentProcurementDataList { get; set; }

    }

    public class QuotationEntryNatureofAttachmentConsultant
    {
        public string NatureofAttachmentActualName { get; set; }
        public string NatureofAttachmentNewName { get; set; }
        public string NatureofAttachmentUrl { get; set; }
        public string Remarks { get; set; }
       
    }
    public class QuotationEntryDetailConsultant
    {
        
        public int Id { get; set; }
        public int VendorId { get; set; }

        public string Rating { get; set; }
        public bool Empanelled { get; set; }
        public string UnitType { get; set; }
        public int Units { get; set; }
        public int FixedFee { get; set; }
        public int ReimbursableAmount { get; set; }

        
        public int UnitRate { get; set; }

        public int Amount { get; set; }
        public int Tax { get; set; }
        public int GSTEtc { get; set; }
        public int Freight_TPT { get; set; }
        public int TotalValue { get; set; }
        public string AttachQuotationActualName { get; set; }
        public string AttachQuotationNewName { get; set; }
        public string AttachQuotationUrl { get; set; }
        public bool IsRecommend { get; set; }
        public string Remark { get; set; }
        
    }

    public class QuotationEntryDetailFixedConsultant
    {
        public string PaymentTerms { get; set; }
        public int Amount { get; set; }
        public DateTime  ? DueOn { get; set; }
        public int PaymentId { get; set; }

    }

    public class QuotationEntryDetailsReimbursableConsultant
    {
        public string PaymentTerms { get; set; }
        public int Amount { get; set; }
        public DateTime? DueOn { get; set; }
        public int PaymentId { get; set; }

    }


}
