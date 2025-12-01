using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class DeclarationMaster
    {
        public long Id { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string Declarationname { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string Frequency { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string DueDate { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string AtOnboarding { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string EmployeeType { get; set; }
        public long AttachmentId { get; set; }
        public string Content { get; set; }
        public string Remark { get; set; }
        public string IPAddress { get; set; }
        public int IsActive { get; set; }
        public int Requiredremarksfield { get; set; }
        public int RequiredinhardCopy { get; set; }
        public string Attachement { get; set; }
        public string file_Name { get; set; }
        public string Status { get; set; }
        public HttpPostedFileBase UploadAttachment { get; set; }
        public List<DeclarationMaster> listdeclarationMasters { get; set; }
    }
    public class DeclarationReport
    {
        public long FYId { get; set; }
        public long EId { get; set; }
        public string EmpType { get; set; }
     
    }
}