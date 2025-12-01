using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Subgrant
    {
        public long SubgrantID { set; get; }
        [Required(ErrorMessage = "Code Can't Blank")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Can't Blank")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address Can't Blank")]
        public string Address { set; get; }
        [Required(ErrorMessage = "Mobile Can't Blank")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Location Can't Blank")]
        public string Location { get; set; }
        [Required(ErrorMessage = "PanNo Can't Blank")]
        public string PanNo { get; set; }
        [Required(ErrorMessage = "FCRA Can't Blank")]
        public string Fora_No { get; set; }
        public string ShortName { get; set; }
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
    }
    public class SubgrantDetails
    {
        public long SubgrantDetailsID { set; get; }
        public long SubgrantID { set; get; }
        public long AttachmentID { set; get; }
        public string filename { get; set; }
        public string ContentType { get; set; }
        public int TypeID { set; get; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string DocType { get; set; }
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
        public HttpPostedFileBase UploadFile { get; set; }
    }

    public class SubgrantAdd
    {
        public Subgrant SubgrantList { get; set; }
        public List<SubgrantDetails> AuthorizedPersonList { get; set; }
        public List<SubgrantDetails> AttachmentsList { get; set; }

    }
}