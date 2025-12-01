using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class FinYear
    {
        public long FinYearID { get; set; }
        [Required(ErrorMessage = "Year Can't Blank")]
        public string Year { set; get; }
        [Required(ErrorMessage = "From Date Can't Blank")]
        public string FromDate { set; get; }
        [Required(ErrorMessage = "To Date Can't Blank")]
        public string ToDate { set; get; }
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