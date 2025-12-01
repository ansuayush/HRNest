using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Procurement
{
    public class QuotationEntrySubgrantModel
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
        public int ModuleStatus { get; set; }
        public bool IsModuleAdminSubmit { get; set; }
        public List<QuotationEntryNatureofAttachmentSubgrant> QuotationEntryNatureofAttachmentList { get; set; }

        public List<QuotationEntryDetailSubgrant> QuotationEntryDetailList { get; set; }

        public List<QuotationEntryDetailsofDeliverableSubgrant> QuotationEntryDetailsofDeliverableList { get; set; }
        public List<UploadSignedDocument> UploadSignedDocumentList { get; set; }


    }

    public class QuotationEntryNatureofAttachmentSubgrant
    {
        public string NatureofAttachmentActualName { get; set; }
        public string NatureofAttachmentNewName { get; set; }
        public string NatureofAttachmentUrl { get; set; }
        public string Remarks { get; set; }
       
    }
    public class QuotationEntryDetailSubgrant
    {
        
        public int Id { get; set; }
        public int VendorId { get; set; }

        public string Rating { get; set; }
        public bool Empanelled { get; set; }
        

        public int SubgrantAmount { get; set; }
       
        public string AttachQuotationActualName { get; set; }
        public string AttachQuotationNewName { get; set; }
        public string AttachQuotationUrl { get; set; }
        public string AttachProposalActualName { get; set; }
        public string AttachProposalNewName { get; set; }
        public string AttachProposalUrl { get; set; }

        public bool IsRecommend { get; set; }
        public string Remark { get; set; }
        


    }

    public class QuotationEntryDetailsofDeliverableSubgrant
    {
        public string PaymentTerms { get; set; }
        public int Amount { get; set; }
        public DateTime  ? DueOn { get; set; }
         
    }

    
}
