using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Procurement
{
    public class ProcurementVendorRegistration : BaseModel
    {

        public int Procure_Id { get; set; }
        public int IsApprover { get; set; }
        public int InputData { get; set; }
        public string SublineItem { get; set; }
        public string ProjectLineIds { get; set; }
        public int IsBindLine { get; set; }
        public int Id { get; set; }
        public int Req_No { get; set; }
        public DateTime Req_Date { get; set; }
        public int Req_By { get; set; }

        public int VendorType { get; set; }
        public string OtherVendor { get; set; }
        public string PartnerName { get; set; }
        public string RelationshipwithC3 { get; set; }
        public string SpecifyRelationshipwithC3 { get; set; }

        public string EntityRegistrationNo { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string Act { get; set; }

        public DateTime? ValidUpto { get; set; }

        public bool AreyouregisteredunderFCRA { get; set; }
        public string FCRARegistrationNo { get; set; }
        public DateTime? FCRARegistrationDate { get; set; }
        public DateTime? FCRAValidUpto { get; set; }
        public bool AreyouregisteredunderSection12Aand80G { get; set; }
        public string RegistrationNo12A { get; set; }
        public DateTime? RegistrationDate12A { get; set; }
        public DateTime? ValidUpto12A { get; set; }
        public string RegistrationNo80G { get; set; }
        public DateTime? RegistrationDateDatetime80G { get; set; }
        public DateTime? ValidUpto80G { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string RelatedEntities { get; set; }
        public bool HasthevendorworkedwithC3 { get; set; }

        public string Pleasesharemoredetails { get; set; }
        public string PANNo { get; set; }

        public string NameonPANCard { get; set; }
        public string GST { get; set; }
        public string CategoriesofGoodServices { get; set; }
        public bool IsthisentityegisteredunderMSME { get; set; }
        public string MSMENo { get; set; }
        public string Anyotherdetail { get; set; }
        public string AreaofOperations { get; set; }
        public bool Areyouawareofanyconflictofinterest { get; set; }
        public string Conflictofinterestdetails { get; set; }
        public string Status { get; set; }

        public string IPAddress { get; set; }

        public string Reason { get; set; }

        public int ApproverId { get; set; }
        public string ApprovarAuth { get; set; }
        public int IsGrid { get; set; }
        public string IsNGO { get; set; }
        public string VendorName { get; set; }
        public int IsAttachment { get; set; }
        public int VendorId { get; set; }
        public bool IsDeleted { get; set; }

        public List<VRegistrationAttachments> VRegistrationAttachments { get; set; }

        public List<VRegistrationBankDetails> VRegistrationBankDetails { get; set; }

        public List<VRegistrationAuthorisedSignatories> VRegistrationAuthorisedSignatories { get; set; }

        public List<VRegistrationMSMEOtherDetail> VRegistrationMSMEOtherDetail { get; set; }


    }
    public class VRegistrationAttachments
    {
        public int SRNo { get; set; }
        public string NatureofDocuments { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentActualName { get; set; }
        public string AttachmentNewName { get; set; }
        public string Remarks { get; set; }

    }
    public class VRegistrationBankDetails
    {
        public string BankName { get; set; }
        public string BankId { get; set; }
        public string AcNo { get; set; }

        public string IFSCCode { get; set; }
        public string TypeofAc { get; set; }
        public string Classification { get; set; }

    }

    public class VRegistrationAuthorisedSignatories
    {
        public string Name { get; set; }

        public string Designation { get; set; }


        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Purposes { get; set; }
        public string PurposesId { get; set; }


    }

    public class VRegistrationMSMEOtherDetail
    {
        public string Details { get; set; }
        public string Remarks { get; set; }

    }

    public class ProcurementVendorRegistrationImpanel : BaseModel
    {
        public int Id { get; set; }
        public bool IsEmpaneled { get; set; }
        public DateTime? EmpStartDate { get; set; }
        public DateTime? EmpEndDate { get; set; }
        public DateTime? EmpDateOfMeeting { get; set; }
        public string EmpCommitteMembers { get; set; }
        public string EmpAttachmentPath { get; set; }
        public string EmpAttachmentFileName { get; set; }
        public string EmpRemark { get; set; }
        public int Action { get; set; }
        public string BlacklistReason { get; set; }
        public string DeactivateReason { get; set; }

    }

    public class VendorSearchModel
    {
        public int? VendorId { get; set; }
        public int? VendorType { get; set; }
        public string C3Relation { get; set; }
        public string CategoryGoodsNService { get; set; }
        public string Area { get; set; }
    }
    public class DownloadReportModel
    {

        public int Req_No { get; set; }
        public string Req_Date { get; set; }
        public string Req_By { get; set; }
        public string Reqester_Location { get; set; }
        
        public string Category { get; set; }
        public string Sub_Category { get; set; }
        public string Project_Code { get; set; }
        public string Project_Name { get; set; }
        public string Themetic_Area { get; set; }
        public string Funded_By { get; set; }
        public string Content_Origin { get; set; }
        public string Proposal_No { get; set; }
        public string Report_No { get; set; }
        public string Copyright { get; set; }
        public string Project_Manager { get; set; }
        public string Title { get; set; }
        public string Sub_Title { get; set; }
        public string Tags { get; set; }
        public string Abstract_Summary { get; set; }
        public string Remark { get; set; }
        public string Document_Category { get; set; }
        public string Auth_Name { get; set; }
        public string Accepted { get; set; }
        public string File_Name { get; set; }
        public string File_Type { get; set; }
        public string File_Size { get; set; }
       
        public string Status { get; set; }
        public string Reason { get; set; }

    }

    public class SharedReportModel
    {

        public int Req_No { get; set; }
        public string DateofSharing { get; set; }
        public string Shared_by  { get; set; }
        public string Reqester_Location { get; set; }
        public string Shared_With { get; set; }
        public string Category { get; set; }
        public string Sub_Category { get; set; }
        public string Project_Code { get; set; }
        public string Project_Name { get; set; }
        public string Themetic_Area { get; set; }
        public string Funded_By { get; set; }
        public string Content_Origin { get; set; }
        public string Proposal_No { get; set; }
        public string Report_No { get; set; }
        public string Copyright { get; set; }
        public string Project_Manager { get; set; }
        public string Title { get; set; }
        public string Sub_Title { get; set; }
        public string Tags { get; set; }
        public string Abstract_Summary { get; set; }
        public string Remark { get; set; }
        public string Document_Category { get; set; }
        public string Auth_Name { get; set; }
        public string Accepted { get; set; }
        public string File_Name { get; set; }
        public string File_Type { get; set; }
        public string File_Size { get; set; }
       
        public string Status { get; set; }
    

    }

    public class UploadReportModel
    {

        public int Req_No { get; set; }
        public string Req_Date { get; set; }
        public string Req_By { get; set; }
        public string Reqester_Location { get; set; }
        
        public string Category { get; set; }
        public string Sub_Category { get; set; }
        public string Project_Code { get; set; }
        public string Project_Name { get; set; }
        public string Themetic_Area { get; set; }
        public string Funded_By { get; set; }
        public string Content_Origin { get; set; }
        public string Proposal_No { get; set; }
        public string Report_No { get; set; }
        public string Copyright { get; set; }
        public string Project_Manager { get; set; }
        public string Title { get; set; }
        public string Sub_Title { get; set; }
        public string Tags { get; set; }
        public string Abstract_Summary { get; set; }
        public string Remark { get; set; }
        public string Document_Category { get; set; }
        public string Auth_Name { get; set; }
        public string Accepted { get; set; }
        public string File_Name { get; set; }
        public string File_Type { get; set; }
        public string File_Size { get; set; }
     
        public string Status { get; set; }
        public string If_Resubmitted { get; set; }
        public string Reason { get; set; }

    }
}