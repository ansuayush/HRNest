using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Attachments
    {
        public long AttachmentID { get; set; }
        public long TableID { get; set; }
        public string TableName { set; get; }
        public string FileName { set; get; }
        public string ViewFileName { set; get; }
        public string ContentType { set; get; }
        public string Description { set; get; }
        //[Required(ErrorMessage = "File Can't Blank")]
        public HttpPostedFileBase UploadFile { get; set; }
        public string FileType { get; set; }
        public string[] AttachmentType { get; set; }
        public bool VisibleDelete{get;set;}
        public bool VisibleAdd { get; set; }


    }


    
    
}