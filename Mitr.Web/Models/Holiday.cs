using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Holiday
    {
        public long HolidayID { set; get; }
        [Required(ErrorMessage = "Name Can't Blank")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Date Can't Blank")]
        public string Date { set; get; }
        public string ShowDate { set; get; }
        public string Remarks { set; get; }
        public long FYId { get; set; }
        public string ColorName { set; get; }
        public string ColorCode { set; get; }
        public int [] LocationID { get; set; }
        public string LinkedLocationID { get; set; }
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