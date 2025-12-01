using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Procurement
{
    public class RFPEntryModel:BaseModel
    {
        
        public int ProcessType { get; set; }
        public int Procure_Request_Id { get; set; }
        public DateTime LastdateofRFP { get; set; }
        public string AgreedscopeofworkNewFileName { get; set; }
        public string AgreedscopeofworkActualFileName { get; set; }
        public string AgreedscopeofworkActualFileUrl { get; set; }
        public DateTime ? EstimatedStartDate { get; set; }
        public DateTime ? EstimatedEndDate { get; set; }
        public string PaymentTerms { get; set; }
        public int IsResubmit { get; set; }
        public string IPAddress { get; set; }
        public int Status { get; set; }
        
        public List<RFPPublishedDetails> RFPPublishedDetailsList { get; set; }
        public List<RFPPaymentTermsEntry> RFPPaymentTermsEntryList { get; set; }

        public List<RFPLive> RFPLiveList { get; set; }
        public List<UploadSignedDocument> UploadSignedDocumentList { get; set; }

        public List<DetailsofDeliverable> DetailsofDeliverableList { get; set; }
        public List<DetailsofDeliverable> DetailsofDeliverableListFixed { get; set; }
        
        public QuotationMessageData QuotationMessage { get; set; }
    }
    public class DetailsofDeliverable
    {

        public int Id { get; set; }         
        public int InvoiceNo { get; set; }
        public DateTime ? InvoiceDate { get; set; }
        public int InvoiceAmount { get; set; }
        public string InvoiceAttachmentActualName { get; set; }
        public string InvoiceAttachmentNewName { get; set; }
        public string InvoiceAttachmentURL { get; set; }
        public string InvoiceAttachmentActualNameD { get; set; }
        public string InvoiceAttachmentNewNameD { get; set; }
        public string InvoiceAttachmentURLD { get; set; }
        public string PaidAmount { get; set; }
        public DateTime ? PaidDate { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }
        public string TDS { get; set; }
        public int IsSubmit { get; set; }
        
    }
   

    public class QuotationMessageData
    {
       
        public int Id { get; set; }
        public int Procure_Request_Id { get; set; }
        public int ParentId { get; set; }
        public string Message { get; set; }
        public string ReciverId { get; set; }
    }
    public class RFPPaymentTermsEntry
    {
        
        public int Id { get; set; }
        public int Procure_Request_Id { get; set; }
      
        public string PaymentTerms { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
    }
    public class RFPLive
    {

        public int Id { get; set; }
        public int Procure_Request_Id { get; set; }
        public string Source { get; set; }
        public string AttachmentActualName { get; set; }
        public string AttachmentNewName { get; set; }
        public string AttachmentPath { get; set; }
        public string Remarks { get; set; }
         
    }
    public class RFPPublishedDetails
    {

        public int Id { get; set; }
      
        
        public string Paymentinfavour { get; set; }

        public string RecipientBankDetails { get; set; }
        public int Amount { get; set; }
        public DateTime ? BankDate { get; set; }
        public string UTRNo { get; set; }
         public string Remarks { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }        
		public Int64 AccountNo { get; set; }



    }

    public class UploadSignedDocument
    {

        public int Id { get; set; }
        public int Procure_Request_Id { get; set; }        
        public string UploadSignedActualName { get; set; }
        public string UploadSignedNewName { get; set; }
        public string UploadSignedUrl { get; set; }
        public string Remarks { get; set; }

    }
    
    public class ProcurementVendorRating
    {
        public int Id { get; set; }
        public int ContractType { get; set; }
        public int IsSubmit { get; set; }        

    }
}
