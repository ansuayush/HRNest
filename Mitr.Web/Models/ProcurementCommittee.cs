using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class ProcurementCommittee
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Emp Name Can't be blank")]
        public long? EMP_ID { get; set; }
        public string emp_name { get; set; }
        [Required(ErrorMessage = "Effective Date Can't be blank")]
        public string Effective_Date { get; set; }
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