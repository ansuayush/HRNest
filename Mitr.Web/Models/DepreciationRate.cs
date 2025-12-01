using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class DepreciationRate
    {
        public long DEPID { set; get; }
        [Required(ErrorMessage = "Code Name Can't Blank")]
        public string MainCode { get; set; }
      
        public double DepRate { set; get; }
        [Required(ErrorMessage = "Description Can't Blank")]
        public string Description { get; set; }

        public string Method { get; set; }
        public string chkMultiple { get; set; }
        public int Multiple { set; get; }
        public double DepRateDouble { set; get; }
        public double DepRateTriple { set; get; }
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