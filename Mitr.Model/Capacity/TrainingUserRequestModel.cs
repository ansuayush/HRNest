using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Capacity
{
    public class TrainingUserRequestModel: BaseModel
    {
        public int Id { get; set; }
        public int IsGrid { get; set; }
        public int InputData { get; set; }

        public UserRequest userRequest { get; set; }
        public  List<TrainingHRRequestCalendar> trainingHRRequestCalendar { get; set; }
        public List<TrainingHRAttendees> trainingHRAttendees { get; set; }
    }
    public class UserRequest
    {
        public int ReqID { get; set; }
        public string ReqNo { get; set; }
        public DateTime ReqDate { get; set; }
        public int RequestedByID { get; set; }
        public string RequestedByName { get; set; }
        public string Source { get; set; }
        public string TypeOfTraining { get; set; }
        public string NameOfTraining { get; set; }
        public string TrainingDescription { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool Isdeleted { get; set; }
        public bool IsActive { get; set; }
        public string IPAddress { get; set; }
        public string HrOrUser { get; set; }
        public int Status { get; set; }
        public string RequestDescription { get; set; }
        public int RequestedFor { get; set; }
        public string EmployeeID { get; set; }
        public int SkillToBeEarned { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NameOfTrainingID { get; set; }

    }
}
