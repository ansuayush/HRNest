using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class MasterAll
    {
        public class List
        {
            public long ID { get; set; }
            public string table_name { get; set; }
            public string field_name { get; set; }
            public string field_name1 { get; set; }
            public string field_name2 { get; set; }
            public string field_value { get; set; }
            public long group_id { get; set; }
            public string group_Name { get; set; }
            public string group_Value { get; set; }
            public int srno { get; set; }
            public int Priority { get; set; }
            public string createdby { get; set; }
            public string createdat { get; set; }
            public string modifiedby { get; set; }
            public string modifiedat { get; set; }
            public bool IsActive { get; set; }
            public string IPAddress { get; set; }
            public bool Selection { get; set; }
        }

        public class Add
        {
            public long? ID { get; set; }
            public string table_name { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string field_name { get; set; }
            public string field_name1 { get; set; }
            public string field_name2 { get; set; }
            public string field_value { get; set; }
          //  [Required(ErrorMessage = "Hey! You missed this field.")]
            public long? group_id { get; set; }
            public int? srno { get; set; }
            
            public int? Priority { get; set; }
            public bool IsActive { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }
    }
}