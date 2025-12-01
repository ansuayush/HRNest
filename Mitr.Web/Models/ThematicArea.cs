using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class ThematicArea
    {
        public int ThematicAreaID { set; get; }
        [Required(ErrorMessage = "Code Can't Blank")]
        public string Code { set; get; }
        public string Name { set; get; }
        public string Description { get; set; }
        public string Category { get; set; }
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