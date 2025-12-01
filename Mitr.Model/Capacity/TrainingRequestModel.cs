using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Capacity
{
    public class TrainingRequestModel : BaseModel
    {
        //public int TrainingTypeId { get; set; }
        public int id { get; set; }
        public string ReqNo { get; set; }
        public DateTime ReqDate { get; set; }
        public string ReqBy { get; set; }
        public string SkillId { get; set; }
        public string ReqDesc { get; set; }
        public string RequestedFor { get; set; }
        public string EmployeeId { get; set; }
        public int TrainingType { get; set; }
        public int TrainingName { get; set; }
        public string TrainingNameOther { get; set; }
        public string Reqstatus { get; set; }
        public int IsActive { get; set; }


    }
}
