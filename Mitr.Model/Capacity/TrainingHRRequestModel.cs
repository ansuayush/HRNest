using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Capacity
{
    public class TrainingHRRequestModel : BaseModel
    {
        public int Id { get; set; }
        public string RequId { get; set; }
        public int IsGrid { get; set; }
        public int InputData { get; set; }
        public HRRequest hrRequest { get; set; }
        public string ReasonForRejection { get; set; }
        public List<RequestHRAttachment> requestHRAttachment { get; set; }
        public List<RequestHRMentors> requestHRMentors { get; set; }
        public List<TrainingHRAttendees> trainingHRAttendees { get; set; }
        public List<TrainingHRRequestCalendar> trainingHRRequestCalendar { get; set; }
    }
  
    public class HRRequest
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
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public bool SupervisorAssessment { get; set; }
        public DateTime? DateOfAssessment { get; set; }
        public string InstructionsForTeam { get; set; }
        public string InstructionsForSupervisors { get; set; }
        public int MentorType { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool isdeleted { get; set; }
        public bool IsActive { get; set; }
        public string IPAddress { get; set; }
        public string HrOrUser { get; set; }
        public int Status { get; set; }
        public string RequestDescription { get; set; }
        public int RequestedFor { get; set; }
        public string EmployeeID { get; set; }
    }
    public class RequestHRAttachment
    {
        public int AttachmentID { get; set; }
        public int ReqID { get; set; }
        public int AttachmentType { get; set; }
        public string AttachmentActualName { get; set; }
        public string AttachmentNewName { get; set; }
        public string AttachmentUrl { get; set; }
        public string Remark { get; set; }
        public bool isdeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    }
    public class RequestHRMentors
    {
        public int MentorsID { get; set; }
        public int ReqID { get; set; }
        public int MentorType { get; set; }
        public string MentorTypeName { get; set; }
        public string MentorName { get; set; }
        public string Email { get; set; }
        public bool isdeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string IPAddress { get; set; }
        public int EmpID { get; set; }
    }
    public class TrainingHRAttendees
    {
        public int AttID { get; set; }
        public int ReqID { get; set; }
        public int UserTrainingID { get; set; }
        public string ReqNo { get; set; }
        public string AttendeesNames { get; set; }
        public int AttendeesType { get; set; }
        public string RequestSource { get; set; }
        public string RequestName { get; set; }
        public bool isdeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string IPAddress { get; set; }
        public int AttendEmployeeID { get; set; }
    }
    public class TrainingHRRequestCalendar
    {
        public int CalendarID { get; set; }
        public int ReqID { get; set; }
        public string TrainingType { get; set; }
        public string TrainingName { get; set; }
        public int TrainingMode { get; set; }
        public string Location { get; set; }
        public bool isdeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string IPAddress { get; set; }
        public int TrainingTypeID { get; set; }
    }

    public class SuperisorRequestModel : BaseModel
    {
        public int Id { get; set; }
        public int EMPId { get; set; }
        public int IsGrid { get; set; }
        public int InputData { get; set; }
        public TrainingRequestDetails trainingRequestDetails { get; set; }
        public List<TrainingUserList> trainingUserLists { get; set; }
        
    }
    public class TrainingUserList
    {
        public int UserTrainingID { get; set; }
        public int ReqID { get; set; }
        public string ReqNo { get; set; }
        public DateTime ReqDate { get; set; }
        public int RequestedByID { get; set; }
        public string RequestedByName { get; set; }
        public string Source { get; set; }
        public string TypeOfTraining { get; set; }
        public string NameOfTraining { get; set; }
        public string TrainingDescription { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public bool SupervisorAssessment { get; set; }
        public DateTime? DateOfAssessment { get; set; }
        public string InstructionsForTeam { get; set; }
        public string InstructionsForSupervisors { get; set; }
        public int MentorType { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool isdeleted { get; set; }
        public bool IsActive { get; set; }
        public string IPAddress { get; set; }
        public string HrOrUser { get; set; }
        public int Status { get; set; }
        public string RequestDescription { get; set; }
        public int RequestedFor { get; set; }
        public string EmployeeID { get; set; }
        public string SpervisorComments { get; set; }
        public string Skills { get; set; }
        public string HRFeedback { get; set; }
        public int Id { get; set; }
        public int IsGrid { get; set; }
        public int InputData { get; set; }
        public string UserGrade { get; set; }
        public string ClubbedID { get; set; }
        public string HRCommentOnAssessment { get; set; }
        public int FinalAttendance { get; set; }

    }
    public class TrainingRequestDetails {
        public string NameOfTraining { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string MentorNames { get; set; }
        public string InstructionsForSupervisors { get; set; }
    }
}
