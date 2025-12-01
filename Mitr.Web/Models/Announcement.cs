using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Announcement
    {
        public int AnnouncementID { get; set; }
        [Required(ErrorMessage = "Heading Name Can't be blank")]
        public string HeadingName { set; get; }
        public string Description { set; get; }
        [Required(ErrorMessage = "Start Can't be blank")]
        public string StarDate { set; get; }
        [Required(ErrorMessage = "Expiry Can't be blank")]
        public string ExpiryDate { set; get; }
      
        public long  AttachmentID { get; set; }
        public string ContentType { set; get; }
        public string FileName { set; get; }
        public HttpPostedFileBase UploadAttachment { get; set; }
        public int UserID { get; set; }
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
        public string LinkedLocationID { get; set; }
        public int[] LocationID { get; set; }
    }
}