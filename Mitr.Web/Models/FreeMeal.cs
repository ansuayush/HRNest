using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class FreeMeal
    {
        public long FreeMealID { set; get; }
        [Required(ErrorMessage = "Meal Name Can't Blank")]
        public string FreeMealName { get; set; }
        
        public double Percentage { get; set; }
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