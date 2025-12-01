using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class CompanyUpload
    {
        public long CompanyUploadID { get; set; }
        public long Sno { get; set; }

        public string Description { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "From Date Can't be blank")]
        public string FromDate { get; set; }
        [Required(ErrorMessage = "To Date Can't be blank")]
        public string ToDate { get; set; }
        public string Status { get; set; }
        public long AttachmentID { get; set; }
        public string FileName { get; set; }
        public string URL { get; set; }
        public bool IsActive { set; get; }
        public int Priority { set; get; }
        public int createdby { get; set; }
        public string createdat { get; set; }
        public int modifiedby { get; set; }
        public string modifiedat { get; set; }
        public int deletedby { get; set; }
        public string deletedat { get; set; }
        public int isdeleted { get; set; }
        public string IPAddress { get; set; }

        public List<DropDownList> CategoryList { get; set; }
        public HttpPostedFileBase UploadAttachement { get; set; }
    }
}