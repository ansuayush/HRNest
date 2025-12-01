using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mitr.Model
{
    public class UserMan
    {
        public class List
        {
            public int RowNum { get; set; }
            public long ID { get; set; }
            public string user_name { get; set; }
            public string password { get; set; }
            public string first_name { get; set; }
            public string middle_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string full_name { get; set; }
            public string UserType { get; set; }
            public string RolesName { get; set; }
            public bool IsActive { get; set; }
            public string createdat { get; set; }
            public string modifiedat { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long? ID { set; get; }
            public string UserType { get; set; }
            [Required(ErrorMessage = "User name Can't Blank")]
            public string user_name { get; set; }

            [Required(ErrorMessage = "password Can't Blank")]
            public string password { get; set; }
            [Required(ErrorMessage = "Name Can't Blank")]
            public string Name { get; set; }

            [EmailAddress]
            public string email { get; set; }
            public string Description { get; set; }

            [Required(ErrorMessage = "Role Can't Blank")]
            public string RoleIDs { get; set; }
            public bool IsActive { set; get; }
            public int? Priority { set; get; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropdownModel> UserTypeList { get; set; }
            public List<DropdownModel> RoleList { get; set; }

            public List<DropdownModel> CandidiateList { get; set; }
        }
    }
}
