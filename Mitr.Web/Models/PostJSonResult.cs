using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Mitr.Models.SalaryStructure;

namespace Mitr.Models
{


    public class PostResponse
    {
        public string ViewAsString { get; set; }
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string SuccessMessage { get; set; }
        public string RedirectURL { get; set; }
        public long ID { get; set; }
        public long OtherID { get; set; }
        public string AdditionalMessage { get; set; }
    
        public OtherEarningEntry OtherEarningEntry { get; set; }
        public DeductionEntry DeductionEntry { get; set; }
        public OtherBenefitPayment otherBenefitPayment { get; set; }

    }

    public class FileResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public int FileLength { get; set; }
        public string ReadAbleFileSize { get; set; }
        public string FileExt { get; set; }
        public string FileType { get; set; }
        public System.IO.Stream InputStream { get; set; }
        public string FileBase64String { get; set; }
    }
    public class CommandResult
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string SuccessMessage { get; set; }
        public long ID { get; set; }
        public string AdditionalMessage { get; set; }
        public string RedirectURL { get; set; }
    }

    public class GetResponse
    {
        public long ID { get; set; }
        public long AdditionalID { get; set; }
        public long AdditionalID1 { get; set; }
        public int Approve { get; set; }
        public string Doctype { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Reason { get; set; }


    }
    public class ListTabs
    {
        public int Approve { get; set; }
        public int FYID { get; set; }
        public int SetFYID { get; set; }

    }
   
    public class GetUpdateColumnResponse
    {
        public long ID { get; set; }
        public string Value { get; set; }
        public string Doctype { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }

    }
    public class GetRecordExitsResponse
    {
        public long ID { get; set; }
        public string Value { get; set; }
        public string Doctype { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }

    }
    public class GetMasterResponse
    {
        public long ID { get; set; }
        public long GroupID { get; set; }
        public string TableName { get; set; }
        public string IsActive { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }

    }

    public class GetMPRResponse
    {
        public string StartDate { get; set; }
        public string Enddate { get; set; }
        public string ProjectIDs { get; set; }
        public string SectionIDs { get; set; }
        public string SubSectionIDs { get; set; }
        public string StateIDs { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public string MPRIDs { get; set; }
        public bool ? IsInActiveRecords { get; set; }
        
    }

    public class GetGenerateValues
    {
        public string AllIDs { get; set; }
        public string Doctype { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
    }

    public class GetValidateToken
    {

        public string Token { get; set; }
        public string Doctype { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }

    }

}