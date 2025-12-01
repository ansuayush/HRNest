using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mitr.Models
{
    public class DLibrary
    {
        public class TagList
        {
            public int ID { get; set; }
            public int Thematic_id { get; set; }
            public string Tag { get; set; }
            public string createdby { get; set; }
            public string createdat { get; set; }
            public string modifiedby { get; set; }
            public string modifiedat { get; set; }
            public string thematicarea_code { get; set; }
            public bool IsActive { get; set; }
        }
        public class TagAdd
        {
            public int ID { get; set; }
            public int Thematic_id { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string Tag { get; set; }
            public bool IsActive { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public string thematicarea_code { get; set; }
            public string createdby { get; set; }
            public string createdat { get; set; }
            public string modifiedby { get; set; }
            public string modifiedat { get; set; }
        }
        public class ContentForm
        {
            public int Id { get; set; }
            public string Req_No { get; set; }
            public string Req_Date { get; set; }
            public int Req_By { get; set; }
            public string Req_Name { get; set; }
            public int CategoryId { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public int Subcategoryid { get; set; }
            public string Project_CodeId { get; set; }
            public string PlaceId { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string Author_CoordinatorId { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string Tag_Id { get; set; }
            public string Thematic { get; set; }
            public string Founded_By { get; set; }
            public string ProjectName { get; set; }
            public string Upload_Date { get; set; }
            public string Report_No { get; set; }

            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string Title { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string Sub_Title { get; set; }
            public string Abstract { get; set; }
            public string Remarks { get; set; }
            public string Document_Category { get; set; }
            public string Published { get; set; }
            public string Project_Lead { get; set; }
            public string Document_ID { get; set; }
            public string Upload_Month { get; set; }
            public string Proposal_No { get; set; }
            public string Accepted { get; set; }
            public string Source { get; set; }
            public string Contract_No { get; set; }
            public string Party_Name { get; set; }
            public string Effective_Date { get; set; }
            public string Expiry_Date { get; set; }
            public string Copyright { get; set; }
            public GenerateRequest GenerateReq_No { get; set; }
            public List<DropDownList> SubCategory { get; set; }
            public List<DropDownList> Project_Code { get; set; }
            public List<DropDownList> Place { get; set; }
            public List<DropDownList> Author { get; set; }
            public List<DropDownList> Tag { get; set; }
            public List<DropDownList> Category { get; set; }
            public List<DLAttachment> DLAttachmentList { get; set; }
        }
        public class DLAttachment
        {
            public int Id { get; set; }
            public string DocumentUpload_By { get; set; }
            public string AttachType { get; set; }
            public string AttachmentURL { get; set; }
            public HttpPostedFileBase Upload { get; set; }
        }
        public class ProjectList
        {
            public string ProjectName { get; set; }
            public string ThematicName { get; set; }
            public string donor_name { get; set; }
            public string emp_name { get; set; }
        }
        public class GenerateRequest
        {
            public string userid { get; set; }
        }
    }
}