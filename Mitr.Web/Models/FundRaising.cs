using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mitr.Models
{
    public class FundRaising
    {
        public class Tabs
        {
            public string Approve { get; set; }
        }
        public class Prospect
        {
            public class List
            {
                public long Id { get; set; }
                public string RowNum { get; set; }
                public string ProspectName { get; set; }
                public string ProspectType { get; set; }
                public string Category { get; set; }
                public long Catid { get; set; }
                public string Company { get; set; }
                public string Country { get; set; }
                public long Countryid { get; set; }
                public string KFA { get; set; }
                public string State { get; set; }
                public long Stateid { get; set; }
                public string Budget { get; set; }
                public string C3Fit { get; set; }
                public long C3Fitid { get; set; }
                public string C3FitScore { get; set; }
                public string Responsible { get; set; }
                public long Responsibleid { get; set; }
                public string Accountable { get; set; }
                public long Accountableid { get; set; }
                public string InformedTo { get; set; }
                public long InformedToid { get; set; }
                public string Consented { get; set; }
                public long Consentedid { get; set; }
                public string Website { get; set; }
                public string OtherSupport { get; set; }
                public string Comment { get; set; }
                public string WebLink { get; set; }

                public string ContactPerson { get; set; }
                public string Designation { get; set; }
                public string ContactNo { get; set; }

            }
            public class Add
            {
                public long Id { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string ProspectName { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string ProspectType { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long Catid { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string Company { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long Countryid { get; set; }
                public string KFA { get; set; }
                public long Stateid { get; set; }
                public string Stateids { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string Budget { get; set; }

                public string C3Fit { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long C3Fitid { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string C3FitScore { get; set; }
                public string Responsible { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long Responsibleid { get; set; }
                public string Accountable { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long Accountableid { get; set; }
                public string InformedTo { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long InformedToid { get; set; }
                public string Consented { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long Consentedid { get; set; }
                public string Website { get; set; }
                public string OtherSupport { get; set; }
                public string Comment { get; set; }
                public string WebLink { get; set; }
                public List<DropDownList> lstCategory { get; set; }
                public List<DropDownList> lstCountry { get; set; }
                public List<DropDownList> lstKFA { get; set; }
                public List<DropDownList> lstState { get; set; }
                public List<DropDownList> lstC3Fit { get; set; }
                public List<DropDownList> lstEmp { get; set; }
                public List<ContactDetails> lstContactDetails { get; set; }
            }
            public class ContactDetails
            {
                public long Id { get; set; }
                public long ProspectId { get; set; }
                public string ContactPerson { get; set; }
                public string Designation { get; set; }
                public string ContactNo { get; set; }
                public string EmailId { get; set; }
                public string LinkedInId { get; set; }
                public int SecratoryDetails { get; set; }
                public string SecratoryName { get; set; }
                public string SecratoryPhone { get; set; }
                public string SecratoryEmail { get; set; }
                public string SecratoryOtherInfo { get; set; }
                public string Address { get; set; }
                public int IsUHNI { get; set; }
                public long AttachmentID { get; set; }
                public string ContentType { get; set; }
                public string FileName { get; set; }
                public HttpPostedFileBase UploadCV { get; set; }
            }
        }
        public class Lead
        {
            public class List
            {
                public long Id { get; set; }
                public string RowNum { get; set; }
                public string DocNo { get; set; }
                public string DocDate { get; set; }
                public string NickName { get; set; }
                public string Revenue { get; set; }
                public string ProspectName { get; set; }
                public string ContactPerson { get; set; }
                public string Reason { get; set; }
                public string ChequeFor { get; set; }
                public string ChequeTo { get; set; }
                public string ChequeIssueDate { get; set; }
                public string ChequeNameof { get; set; }
                public string ChequeAmount { get; set; }
                public string ChequeRemark { get; set; }
                public string StageLevel { get; set; }
                public string C3Fit { get; set; }
                public string Responsible { get; set; }
            }
            public class Add
            {
                public long Id { get; set; }
                public string DocNo { get; set; }
                public string DocDate { get; set; }
                public long StageLevelid { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string NickName { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                [Range(0, 9999999999999999.99, ErrorMessage = "Must be Numeric")]
                public string Revenue { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long Prospectid { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long ProspectContactid { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long ProspectContactOtherId { get; set; }
                public int IsConsider { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string Stateids { get; set; }
                public string Reason { get; set; }
                public string ChequeFor { get; set; }
                public string ChequeTo { get; set; }
                public string ChequeIssueDate { get; set; }
                public string ChequeNameof { get; set; }
                [Range(0, 9999999999999999.99, ErrorMessage = "Must be Numeric")]
                public string ChequeAmount { get; set; }
                public string ChequeRemark { get; set; }
                public string StageName { get; set; }
                public string LatestAction { get; set; }
                public string ActionTaken { get; set; }
                public List<DropDownList> lstProspect { get; set; }
                public List<DropDownList> lstProspectContact { get; set; }
                public List<DropDownList> lstState { get; set; }
                public List<DropDownList> lstStageLevel { get; set; }
                public List<Refferals> lstRefferals { get; set; }
                public List<DropDownList> lstStage { get; set; }
                public List<DropDownList> lstActivity { get; set; }
                public List<LeadActivity> lstLeadActivity { get; set; }
            }
            public class Refferals
            {
                public long Id { get; set; }
                public long LeadId { get; set; }
                public string ReferralName { get; set; }
                public string ReferredDate { get; set; }
                public string ReferredBy { get; set; }
                public string ContactNo { get; set; }
            }
            public class LeadActivity
            {
                public long Id { get; set; }
                public long LeadId { get; set; }
                public string ActivityDate { get; set; }
                public long Stageid { get; set; }
                public long Activityid { get; set; }
                public string Remarks { get; set; }
                public string NextActionDate { get; set; }
            }

        }
        public class RefferalsTask
        {
            public long Id { get; set; }
            public string DocNo { get; set; }
            public string DocDate { get; set; }
            public string NickName { get; set; }
            public string ReferralName { get; set; }
            public string ReferredDate { get; set; }
            public string Referredby { get; set; }
            public string ContactNo { get; set; }
            public string ReferralStatus { get; set; }
        }
        public class Reports
        {
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Doctype { get; set; }
            public long Id { get; set; }
        }
        public class LeadDashboard
        {
            public long Id { get; set; }
            public List<StageCount> listSatgeCount { get; set; }
            public List<NatureCount> listNatureCount { get; set; }
            public List<LeadList> listLead { get; set; }
        }
        public class StageCount
        {
            public long Id { get; set; }
            public string FieldName { get; set; }
            public string Value { get; set; }

        }
        public class NatureCount
        {
            public long Id { get; set; }
            public string ToFieldName { get; set; }
            public decimal TotalPrice { get; set; }
            public decimal Count { get; set; }
        }
        public class LeadList
        {
            public long Id { get; set; }
            public long RowNum { get; set; }
            public long DocNo { get; set; }
            public string DocDate { get; set; }
            public string NickName { get; set; }
            public decimal Revenue { get; set; }
            public string Reason { get; set; }
            public string StageLevel { get; set; }
            public string ProspectName { get; set; }
            public string ContactPerson { get; set; }
            public string C3Fit { get; set; }
            public string Responsible { get; set; }
        }
    }
}
