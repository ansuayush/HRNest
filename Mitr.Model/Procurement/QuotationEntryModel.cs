using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Procurement
{
    public class QuotationEntryModel
    {
		public int Id { get; set; }
        public int Procure_Request_Id { get; set; }
        public DateTime EstimatedStartDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public string ContractType { get; set; }
        public string ScopeofworkNewFileName { get; set; }
        public string ScopeofworkActualFileName { get; set; }
        public string ScopeofworkActualFileUrl { get; set; }
        public string SpecialConditions { get; set; }
        public bool IsNonRegistratorVendor { get; set; }
        public string Justification { get; set; }
        public string UnitType { get; set; }        
        public int Status { get; set; }
        public string ApprovalRemark { get; set; }
        public string IPAddress { get; set; }
        public string ItemDescription { get; set; }
        public int ModuleStatus { get; set; }
        public bool IsModuleAdminSubmit { get; set; }
        public int ? QuotationNo { get; set; }
        public string Shiping { get; set; }
        public string DeliveryAddress { get; set; }
        public string AmendmentReason { get; set; }
        public int POC { get; set; }
         
        public DateTime ? QuotationDate { get; set; }
        public DateTime? QuotationAgreementDate { get; set; }

        public DateTime? QuotationSignDate { get; set; }

        public List<QuotationEntryNatureofAttachment> QuotationEntryNatureofAttachmentList { get; set; }

        public List<QuotationEntryDetail> QuotationEntryDetailList { get; set; }

        public List<QuotationEntryDetailsofDeliverable> QuotationEntryDetailsofDeliverableList { get; set; }

        public List<QuotationRateContractMultiple> QuotationRateContractMultipleList { get; set; }

        public List<QuotationRateContractUnitMultiple> QuotationRateContractUnitMultipleList { get; set; }
        public List<UploadSignedDocument> UploadSignedDocumentList { get; set; }
        public decimal AmendReqNo { get; set; }
        public string NatureofAmendment { get; set; }
        public string AmendReason { get; set; }
        public string TotalRequiredAmount { get; set; }
        
        public List<AmendmentProcurementData> AmendmentProcurementDataList { get; set; }

    }
 
    public class QuotationEntryNatureofAttachment
    {
        public string NatureofAttachmentActualName { get; set; }
        public string NatureofAttachmentNewName { get; set; }
        public string NatureofAttachmentUrl { get; set; }
        public string Remarks { get; set; }
       
    }
    public class QuotationEntryDetail
    {
        
        public int Id { get; set; }
        public int VendorId { get; set; }

        public string Rating { get; set; }
        public bool Empanelled { get; set; }
        public string UnitType { get; set; }
        public int Units { get; set; }

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

    public class QuotationEntryDetailsofDeliverable
    {
        public string PaymentTerms { get; set; }
        public int Amount { get; set; }
        public DateTime  ? DueOn { get; set; }
        public bool ? IsApproved { get; set; }
        public int Id { get; set; }
        public int PaymentId { get; set; }
        


    }

    public class QuotationRateContractMultiple
    {
        public string Description { get; set; }

        public int UnitRate { get; set; }

        public int Tax { get; set; }

        public int Tax_GST { get; set; }
        public int QuoteEntryId { get; set; }


    }
    public class QuotationRateContractUnitMultiple
    {
        public string Description { get; set; }

      
        public int QuoteEntryId { get; set; }

        public int Unit { get; set; }

        public int UnitRate { get; set; }

        public int Amount { get; set; }

        public int Tax { get; set; }
              public int Tax_GST { get; set; }
        public int Frieght { get; set; }


    }

      
}
