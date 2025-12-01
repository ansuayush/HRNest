using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Models
{
    public class BloodGroup
    {
        public int ID { get; set; }
        public string Blood_Group_Name { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? DeletedBy { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int? Priority { get; set; }
        public bool IsActive { get; set; }
        public string IPAddress { get; set; }
        public List<DropDownList> BloodGroupList { get; set; }

    }
}
