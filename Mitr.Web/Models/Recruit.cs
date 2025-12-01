using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Recruit
    {
        public class Tabs
        {
            public long EMPID { get; set; }
            public int Approve { get; set; }
        }


        public class EMP
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }

        public class JOB
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }
        public class Pillar
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }
        public class Location
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }
        public class PendingRECRequests
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }

        public class Initiate
        {
            public long ProjectID { get; set; }
            public long ManagerID { get; set; }
            public string Managername { get; set; }
            public string ManagerCode { get; set; }
            public string ProjectName { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public long ThemAreaID { get; set; }
            public string ThematicArea { get; set; }
            [Required(ErrorMessage = "Location  Can't Blank")]
            public long LocationID { get; set; }
            public string LocatioNName { get; set; }
            [Required(ErrorMessage = "Job  Title  Can't Blank")]
            public long JobID { get; set; }
            public string Title { get; set; }
            public long REC_ReqID { get; set; }
            public long ProjectDetailID { get; set; }
            [Required(ErrorMessage = "Sub Title Can't Blank")]
            public string Job_SubTitle { get; set; }
            [Required(ErrorMessage = "Due Date Can't Blank")]
            public string DueDate { get; set; }
            public string JobDescription { get; set; }
            public string Qualification { get; set; }

            [Required(ErrorMessage = " Skills Can't Blank")]
            public long[] SkillsID { get; set; }
            [Required(ErrorMessage = " Skills Can't Blank")]
            public string Skills { get; set; }

            public string Experience { get; set; }
            public decimal Time_Per { get; set; }
        }

        public class Request
        {
            public class Fill
            {
                public long ProjectID { get; set; }
                public long ManagerID { get; set; }
                public string Managername { get; set; }
                public string ManagerCode { get; set; }
                public string ProjectName { get; set; }
                public string StartDate { get; set; }
                public string EndDate { get; set; }
                public long ThemAreaID { get; set; }
                public string ThematicArea { get; set; }
                public long LocationID { get; set; }
                public string LocatioNName { get; set; }
                public long JobID { get; set; }
                public string JobTitle { get; set; }
                public string REC_Type { get; set; }

                public string Staff_Cat { get; set; }
                public long REC_ReqID { get; set; }
                public long ProjectDetailID { get; set; }
                [Required(ErrorMessage = "Sub Title Can't Blank")]
                public string Job_SubTitle { get; set; }
                [Required(ErrorMessage = "Due Date Can't Blank")]
                public string DueDate { get; set; }
                public string JobDescription { get; set; }
                public string Qualification { get; set; }
                [Required(ErrorMessage = "Skills Can't Blank")]
                public long[] SkillsID { get; set; }
                public string Skills { get; set; }
                [Required(ErrorMessage = "Experience Can't Blank")]
                public string Experience { get; set; }
                public int Time_Per { get; set; }

                public long[] Project_TagID { get; set; }
                public string Project_Tag { get; set; }


                public string Recommenders { get; set; }
                public long[] RecommendersID { get; set; }
                public long? FinalApproverID { get; set; }
                public int isdeleted { get; set; }
                public bool IsBtnVisible { get; set; }
                public long AmendID { get; set; }
                public string AmendRemarks { get; set; }
                public string AmendDate { get; set; }

                public List<Recruit.EMP> EmpList { get; set; }
                public List<Recruit.JOB> JOBList { get; set; }
                public List<Recruit.Pillar> PillarList { get; set; }
                public List<Recruit.Location> LocationList { get; set; }
                public List<PendingRECRequests> PendingRECList { get; set; }


            }

            public class View
            {
                public string REC_Code { get; set; }
                public long ProjectID { get; set; }
                public long ManagerID { get; set; }
                public string Managername { get; set; }
                public string ManagerCode { get; set; }
                public string ProjectName { get; set; }
                public string StartDate { get; set; }
                public string EndDate { get; set; }
                public long ThemAreaID { get; set; }
                public string ThematicArea { get; set; }
                public long LocationID { get; set; }
                public string LocatioNName { get; set; }
                public long JobID { get; set; }
                public string JobTitle { get; set; }
                public string REC_Type { get; set; }
                public string Staff_Cat { get; set; }
                public long REC_ReqID { get; set; }
                public long ProjectDetailID { get; set; }
                public string Job_SubTitle { get; set; }
                public string DueDate { get; set; }
                public string JobDescription { get; set; }
                public string Qualification { get; set; }
                public string SkillsName { get; set; }
                public string Experience { get; set; }
                public int Time_Per { get; set; }
                public int Approve { get; set; }
                public long[] Project_TagID { get; set; }
                public string Project_Tag { get; set; }
                public long AmendID { get; set; }
                public string AmendRemarks { get; set; }
                public string AmendDate { get; set; }
            }
        }
        public class InternalStaff
        {
            public long EmpID { get; set; }
            public string EmpName { get; set; }
            public string EMPCode { get; set; }
            public string JobTitle { get; set; }
            public string Pillar { get; set; }
            public string Relocation { get; set; }
            public string JobTitleByHR { get; set; }
            public string PillarByHR { get; set; }
            public string RelocationByHR { get; set; }
            public string Chck { get; set; }
        }

        public class Recom_Preference
        {
            public long REC_IStaffID { get; set; }
            public long REC_ReqID { get; set; }
            public long EmpID { get; set; }
            public string EmpName { get; set; }
            public string EMPCode { get; set; }
            public string JobTitle { get; set; }
            public string Pillar { get; set; }
            public string Relocation { get; set; }
            public string JobTitleByHR { get; set; }
            public string PillarByHR { get; set; }
            public string RelocationByHR { get; set; }
            public string Job { get; set; }
            public string Current_Job { get; set; }
            public string TH_Areas { get; set; }
            public string Current_TH_Areas { get; set; }

            public string Location { get; set; }
            public string Current_Location { get; set; }
            [Required(ErrorMessage = "Comment Can't Blank")]
            public string comment { get; set; }

            public int _Preference { get; set; }
        }

        public class Final_Preference
        {
            [Required(ErrorMessage = "Please select atleast One Candidate or None")]
            public string ApproveCandidate { get; set; }

            public string Remarks { get; set; }
            public List<Staff> StaffList { get; set; }
            public bool IsBtnVisible { get; set; }

            public class Staff
            {
                public long REC_ReqID { get; set; }
                public long EmpID { get; set; }
                public string EmpName { get; set; }
                public string EMPCode { get; set; }

                public long ApproverID { get; set; }
                public string ApproverName { get; set; }
                public string ApproverCode { get; set; }

                public string JobTitle { get; set; }
                public string Pillar { get; set; }
                public string Relocation { get; set; }
                public string JobTitleByHR { get; set; }
                public string PillarByHR { get; set; }
                public string RelocationByHR { get; set; }
                public string comment { get; set; }
                public int Preference { get; set; }
                public int Selected { get; set; }
                public string FinalComment { get; set; }
            }

        }
    }

    public class EXRecruit
    {
        public class Tabs
        {
            public long EMPID { get; set; }
            public int Approve { get; set; }
        }
        public class View
        {
            public string REC_Code { get; set; }
            public long ProjectID { get; set; }
            public long ManagerID { get; set; }
            public string Managername { get; set; }
            public string ManagerCode { get; set; }
            public string ProjectName { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public long ThemAreaID { get; set; }
            public string ThematicArea { get; set; }
            public long LocationID { get; set; }
            public string LocatioNName { get; set; }
            public long JobID { get; set; }
            public string JobTitle { get; set; }
            public string REC_Type { get; set; }
            public string Staff_Cat { get; set; }
            public long REC_ReqID { get; set; }
            public long ProjectDetailID { get; set; }
            public string Job_SubTitle { get; set; }
            public string DueDate { get; set; }
            public string JobDescription { get; set; }
            public string Qualification { get; set; }
            public string SkillsName { get; set; }
            public string Experience { get; set; }
            public int Time_Per { get; set; }
            public int Approve { get; set; }
            public long[] Project_TagID { get; set; }
            public string Project_Tag { get; set; }
            public long AmendID { get; set; }
            public string AmendRemarks { get; set; }
            public string AmendDate { get; set; }
            public List<PendingRECRequests> PendingList { get; set; }
        }
        public class PendingRECRequests
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }
        public class Vacancy
        {
            public class HR
            {
                public long REC_EVacancyID { get; set; }
                public long REC_ReqID { get; set; }
                public string REC_Code { get; set; }
                [Required(ErrorMessage = "Description Can't Blank")]
                public string HRVacancyDes { get; set; }
                public string HRAttach { get; set; }
                public long AttachmentID { get; set; }

                public HttpPostedFileBase Upload { get; set; }
                public bool btnVisible { get; set; }
                public int isdeleted { get; set; }
                public string Approved { get; set; }
            }
            public class Comm
            {
                public long REC_EVacancyID { get; set; }
                public long REC_ReqID { get; set; }
                public string REC_Code { get; set; }
                public string HRAttachID { get; set; }
                [Required(ErrorMessage = "Description Can't Blank")]
                public string CommVacancyDes { get; set; }

                public long CommHRAttachID { get; set; }

                public HttpPostedFileBase Upload { get; set; }
                public bool btnVisible { get; set; }
                public int Approved { get; set; }

            }

            public class Final
            {
                public long REC_EVacancyID { get; set; }
                public long REC_ReqID { get; set; }
                public string REC_Code { get; set; }
                public string CommVacancyDes { get; set; }
                public long CommHRAttachID { get; set; }
                [Required(ErrorMessage = "Start Date Can't Blank")]
                public string StartDate { get; set; }
                [Required(ErrorMessage = "End Date Can't Blank")]
                public string EndDate { get; set; }
                public string Web_Announce { get; set; }
                public string Internal_Announce { get; set; }
                public string Other_Announce { get; set; }
                public bool btnVisible { get; set; }
                public int Approved { get; set; }

            }
        }


        public class Final_ConfirmedCV
        {
            [Required(ErrorMessage = "Please select atleast One Candidate")]
            public string ApproveCandidate { get; set; }
            [Required(ErrorMessage = "Reason Can't Blank")]
            public string Remarks { get; set; }
            public string Reason { get; set; }
            public bool IsBtnVisible { get; set; }
            public List<Application> ApplicationList { get; set; }
            public class Application
            {
                public long REC_ShortID { get; set; }
                public long REC_AppID { get; set; }
                public string Name { get; set; }
                public string Mobile { get; set; }
                public string EmailID { get; set; }
                public long CVAttachID { get; set; }
                public string CVPath { get; set; }
                public long ApproverID { get; set; }
                public string ApproverName { get; set; }
                public string ApproverCode { get; set; }
                public string Comment { get; set; }
                public int Preference { get; set; }
                public int Selected { get; set; }
                public int Approved { get; set; }
                public string FinalComment { get; set; }
            }
        }

    }

    public class Interview
    {
        public class Round
        {

            public long REC_InterviewSetID { get; set; }
            [Required(ErrorMessage = "Round Name Can't Blank")]
            public string RoundName { get; set; }
            public string RoundDesc { get; set; }
            public int Priority { set; get; }
            public int srno { set; get; }
            public bool IsNegotiationRound { get; set; }
            public List<Member> MemberList { get; set; }
            public List<Slot> SlotList { get; set; }

        }
        public class Member
        {
            public long? REC_InterviewSetID { get; set; }
            [Required(ErrorMessage = "Type Can't Blank")]
            public string RoundMemberType { get; set; }
            public long EMPID { get; set; }
            [Required(ErrorMessage = "Name Can't Blank")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Email Can't Blank")]
            [EmailAddress]
            public string Email { get; set; }
            public int srno { set; get; }
            public int Priority { set; get; }
        }
        public class Slot
        {
            public long? REC_InterviewSetID { get; set; }
            [Required(ErrorMessage = "Slot Date Can't Blank")]
            public string SlotDate { get; set; }
            [Required(ErrorMessage = "MAX CV Can't Blank")]
            public string MAXCV { get; set; }
            [Required(ErrorMessage = "From Time Can't Blank")]
            public string FromTime { get; set; }
            [Required(ErrorMessage = "To Time Can't Blank")]
            public string ToTime { get; set; }
        }
    }
    public class InterviewSelection

    {
        public class Round
        {
            public long REC_InterviewSetID { get; set; }
            public long REC_ReqID { get; set; }
            public string RoundName { get; set; }
            public string RoundDesc { get; set; }
            public int Priority { set; get; }
            public int srno { set; get; }
            public int Rejected { set; get; }

            public string Reason { get; set; }
            public bool IsNegotiationRound { get; set; }
            public List<Application> ApplicationList { get; set; }

        }
        public class Application
        {
            public long REC_InterviewSetID { get; set; }
            public long REC_ReqID { get; set; }
            public long REC_AppID { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string Mobile { get; set; }
            public string EmailID { get; set; }
            public string ApplyDate { get; set; }
            public int Rejected { set; get; }
            public string Nationality { get; set; }

            public string NegotiationSalary { get; set; }
            public string CurrentSalary { get; set; }
            public string ExpectedSalary { get; set; }
            public string TotalExperience { get; set; }
            public long CVAttachID { get; set; }
            public string CVPath { get; set; }
            public string Reason { get; set; }
        }


        public class Reject
        {
            [Required(ErrorMessage = "Reason Can't Blank")]
            public string Reason { get; set; }
            public string ChkMove { get; set; }
            public string REC_ReqID { get; set; }
            public string REC_AppID { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string Mobile { get; set; }
            public string EmailID { get; set; }

            public int Rejected { set; get; }
        }
        public class MoveToRound
        {
            public long REC_InterviewSetID { get; set; }
            public long REC_ReqID { get; set; }
            public long REC_AppID { get; set; }
            public string Name { get; set; }
            public string ApplyDate { get; set; }
            public string CurrentSalary { get; set; }
            public string ExpectedSalary { get; set; }
            public bool IsNegotiationRound { get; set; }
            [Required(ErrorMessage = "Score Can't Blank")]
            public string Score { get; set; }
            public long RoundID { get; set; }
            [Required(ErrorMessage = "Slot Can't Blank")]
            public long SlotID { get; set; }
            public int Rejected { set; get; }
            public string Reason { get; set; }
            public List<ddList> RoundList { get; set; }
            public QulifiedRound QulifiedRound { get; set; }
            public List<QulifiedRound> QulifiedRoundList { get; set; }
            public List<ddList> SlotList { get; set; }
            public List<Attachments> AttachmentsList { get; set; }
            [Display(Name = "Make sure that you have reviewed the assessment")]
            [Range(typeof(bool), "true", "true", ErrorMessage = "Please Check ?")]
            public bool chkConsider { get; set; }
            public string RoundName { get; set; }
            public string PanelName { get; set; }
            public string AttachmentPath { get; set; }
            public string Description { get; set; }
            public List<MoveToRound> listMoveRound { get; set; }
            public long REC_InterviewID { get; set; }


        }

        public class Approve
        {
            public long REC_InterviewSetID { get; set; }
            public long REC_ReqID { get; set; }
            public long REC_AppID { get; set; }
            public string Name { get; set; }
            public string CurrentSalary { get; set; }
            public string ExpectedSalary { get; set; }
            public bool IsNegotiationRound { get; set; }
            [Required(ErrorMessage = "Salary offered Can't Blank")]
            public decimal? NegotiationSalary { get; set; }
            [Required(ErrorMessage = "Joining Date Can't Blank")]
            public string ExpectedJDate { get; set; }
            [Required(ErrorMessage = "Round Can't Blank")]
            public long RoundID { get; set; }
            [Required(ErrorMessage = "Slot Can't Blank")]
            public long SlotID { get; set; }
            public string ApplyDate { get; set; }
            [Required(ErrorMessage = "Score Can't Blank")]
            public string Score { get; set; }

            public string Reason { get; set; }
            [Display(Name = "Make sure that you have reviewed the assessment")]
            [Range(typeof(bool), "true", "true", ErrorMessage = "Please Check ?")]
            public bool chkConsider { get; set; }


            public List<Attachments> AttachmentsList { get; set; }
            public List<TagRECRequests> TagRECRequestsList { get; set; }
        }
        public class ddList
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }
        public class QulifiedRound
        {
            public int ID { get; set; }
            public int RowNum { get; set; }
            public int RoundID { get; set; }
            public string RoundName { get; set; }
            public int REC_InterviewSetID { get; set; }
            public int REC_ReqID { get; set; }
            public int REC_InterviewID { get; set; }
            public string Score { get; set; }
            public string Remarks { get; set; }
            public string PanelMember { get; set; }
            public string SlotDate { get; set; }
            public int REC_AppID { get; set; }
        }
        public class Attachments
        {
            public long ID { get; set; }
            public long CountRow { get; set; }
            public HttpPostedFileBase Upload { get; set; }
            public string Description { get; set; }
            public string AttachmentPath { get; set; }
            public string FileName { get; set; }
            public string FileExt { get; set; }
        }

        public class TagRECRequests
        {
            public long REC_ReqID { get; set; }
            public string REC_Code { get; set; }
            public string JobTitle { get; set; }
            public string Job_SubTitle { get; set; }
            public string JobCode { get; set; }

            public string DueDate { get; set; }
            public string JobDescription { get; set; }
            public string Qualification { get; set; }
            public string Skills { get; set; }
            public string Experience { get; set; }
            public string Staff_Cat { get; set; }
            public int Time_Per { get; set; }

            public string proj_name { get; set; }
            public string projref_no { get; set; }
            public string ManagerName { get; set; }
            public string locationName { get; set; }
        }
    }
}
