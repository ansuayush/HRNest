using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Project
    {
        public class List
        {
            public int RowNum { get; set; }
            public long ID { get; set; }
            public string doc_no { get; set; }
            public string donor_Name { get; set; }
            public string DonorPOC { get; set; }
            public string ProgramsName { get; set; }
            public string projref_no { get; set; }
            public string proj_name { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
            public string Currency_Name { get; set; }
            public decimal amount { get; set; }
            public string ManagerName { get; set; }
            public string Status { get; set; }
        }

        public class ProjectDetails
        {
            public long? ID { get; set; }


            public string projref_no { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public string proj_name { get; set; }
            
            public string doc_no { get; set; }
            
            public string doc_date { get; set; }

            [Required(ErrorMessage = "Hey, you missed this field!")]
            public string start_date { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public string end_date { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public long? ManagerID { get; set; }

            [Required(ErrorMessage = "Hey, you missed this field!")]
            public long? ThemArea_ID { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public long? FundTypeID { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public long? FundingTypeID { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public string ProgramIDs { get; set; }
            
            public long? TeamMemberID { get; set; }
            
            public int Inactive { get; set; }
            public string Description { get; set; }
            public List<DropDownList> EMPList { get; set; }
            public List<DropDownList> ThematicareaList { get; set; }
            public List<DropDownList> FundTypeList { get; set; }
            public List<DropDownList> FundingTypeList { get; set; }

            public List<DropDownList> ProgramList { get; set; }
        }

        public class ClientsDetails
        {
            public int Inactive { get; set; }
            public long? ID { get; set; }
            public string projref_no { get; set; }
            public string proj_name { get; set; }
            public string doc_no { get; set; }
            public string doc_date { get; set; }

            public string DonorTypeName { get; set; }

            [Required(ErrorMessage = "Hey, you missed this field!")]
            public long? donor_id { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public long? PrincipalDonorID { get; set; }
            public string ConsortiumProjectIDs { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public long? AgreementID { get; set; }
            public List<DropDownList> DonorList { get; set; }
            public List<DropDownList> ConsortiumProjectsList { get; set; }

            public List<DropDownList> AgreementTypeList { get; set; }
            public List<ContactPerson> ContactPersonList { get; set; }

        }

        public class ContactPerson
        {
            public long? ID { get; set; }
            public long? DonorDetailsID { get; set; }
            public long? donor_id { get; set; }
            public long? ProjectID { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public string person_name { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public string designation { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public string location { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public string phone_no { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            [EmailAddress]
            public string email { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public string Purpose { get; set; }
            public int? Isdefault { get; set; }

            public int? Priority { get; set; }
        }

        public class BudgetDetails
        {
            public int Inactive { get; set; }
            public long? ID { get; set; }
            public string projref_no { get; set; }
            public string proj_name { get; set; }
            public string doc_no { get; set; }
            public string doc_date { get; set; }

            [Required(ErrorMessage = "Hey, you missed this field!")]
            public long? currency_id { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public decimal? ex_rate { get; set; }


            [Required(ErrorMessage = "Hey, you missed this field!")]
            public decimal? amount { get; set; }
            [Required(ErrorMessage = "Hey, you missed this field!")]
            public decimal? amount_inr { get; set; }
            public List<DropDownList> CurrencyList { get; set; }
        }

        public class SpecialConditions
        {
            public int Inactive { get; set; }
            public long? ID { get; set; }
            public string projref_no { get; set; }
            public string proj_name { get; set; }
            public string doc_no { get; set; }
            public string doc_date { get; set; }
            public List<Condition.List> conditionList { get; set; }
            public class Condition
            {
                public class Add
                {
                    public long? ID { get; set; }
                    public long ProjectID { get; set; }
                    [Required(ErrorMessage = "Hey, you missed this field!")]
                    public string Condition { get; set; }
                    public long? AllocatedID { get; set; }
                    public string Chk { get; set; }
                    public int? Priority { get; set; }
                    public List<DropDownList> EMPList { get; set; }
                }
                public class List
                {
                    public long? ID { get; set; }
                    public string Condition { get; set; }
                    public string AllocatedName { get; set; }
                    public int AutoClosure { get; set; }
                    public int? Priority { get; set; }
                }
            }
        }
        public class DonorsReport
        {
            public int Inactive { get; set; }
            public long? ID { get; set; }
            public string projref_no { get; set; }
            public string proj_name { get; set; }
            public string doc_no { get; set; }
            public string doc_date { get; set; }
            public List<ReportList> reportList { get; set; }
            public List<DropDownList> EMPList { get; set; }
            public class ReportList
            {
                public long? ID { get; set; }
                public long? ProjectID { get; set; }
                [Required(ErrorMessage = "Hey, you missed this field!")]
                public string ReportName { get; set; }
                [Required(ErrorMessage = "Hey, you missed this field!")]
                public string FromDate { get; set; }
                [Required(ErrorMessage = "Hey, you missed this field!")]
                public string ToDate { get; set; }
                [Required(ErrorMessage = "Hey, you missed this field!")]
                public string SubmissionDate { get; set; }
                public long? AllocatedTo { get; set; }
                public long? AttachmentID { get; set; }
                public string AttachmentPath { get; set; }
                public int? Priority { get; set; }
                public HttpPostedFileBase upload { get; set; }
            }
        }

        public class Attachment
        {
            public int Inactive { get; set; }
            public long? ID { get; set; }
            public string projref_no { get; set; }
            public string proj_name { get; set; }
            public string doc_no { get; set; }
            public string doc_date { get; set; }
            public List<Document.List> documentList { get; set; }
            public class Document
            {
                public class List
                {
                    public long? ID { get; set; }
                    public string DocumentType { get; set; }
                    public string Description { get; set; }
                    public int? AttachmentID { get; set; }
                    public string AttachmentPath { get; set; }
                }
                public class Add
                {
                    public long? ProjectID { get; set; }
                    public long? ID { get; set; }
                    [Required(ErrorMessage = "Hey, you missed this field!")]
                    public string DocumentType { get; set; }
                    public string Description { get; set; }
                    [Required(ErrorMessage = "Hey, you missed this field!")]
                    public HttpPostedFileBase upload { get; set; }
                    public long? AttachmentID { get; set; }
                    public string AttachmentPath { get; set; }
                    public int? Priority { get; set; }

                    
                }
               
            }
        }

        public class DonorDetails
        {
            public int RowNum { get; set; }
            public long ID { get; set; }
            public long donor_id { set; get; }
            public string person_name { get; set; }
            public string designation { get; set; }
            public string location { get; set; }
            public string phone_no { get; set; }
            public string email { get; set; }
            public string Chk { get; set; }
        }
    }
}