using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Consultant
    {
        public long ConsultantID { set; get; }
        [Required(ErrorMessage = "Consultant Name Can't be blank")]
        public string ConsultantName { get; set; }
        [Required(ErrorMessage = "Consultant Code Can't be blank")]
        public string Code { set; get; }
        public string Status { get; set; }
        public string PanName { set; get; }
        public string FatherName { get; set; }
        public string LocalAddress { get; set; }
        public string PerAddress { get; set; }
       
        public string LinkedThematicArea { get; set; }
        public string Location { get; set; }
        public string PhoneNO { get; set; }
        public string PanNo { get; set; }
        public string PayMode { get; set; }
        public string Design { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string AccountName { get; set; }
        public long AttachmentID { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
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
        public HttpPostedFileBase UploadCV { get; set; }
        public DataSet ThematicAreaData { get; set; }
        public int[] ThematicAreaID { get; set; }
    }

   
}