using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Vendor
    {
        public long VendorID { set; get; }
        [Required(ErrorMessage = "Vendor Code Can't be blank")]
        public string VendorCode { get; set; }
        [Required(ErrorMessage = "Vendor Name Can't be blank")]
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Representative { set; get; }
        public string ContactNo { get; set; }
        public string PAN { set; get; }
        public string LSTNo { set; get; }
        public string CSTNo { get; set; }

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
}