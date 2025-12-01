using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class CostCenter
    {
        public long CostCenterID { get; set; }
        [Required(ErrorMessage = "Code Name Can't Blank")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Name Can't Blank")]
        public string Name { get; set; }
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

    public class SubLineItem
    {
        public long SubLineItemID { set; get; }
        [Required(ErrorMessage = "Code Name Can't Blank")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Can't Blank")]
        public string Name { get; set; }
        public long CostCenterID { get; set; }
        public string CostCenterName { get; set; }
      
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