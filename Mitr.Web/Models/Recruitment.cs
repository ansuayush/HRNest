using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Recruitment
    {
       
    }

    public class InternalStaff
    {
        public long InternalCandidateID { get; set; }
        public long RecruitmentRequestID { get; set; }
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

    public class InitiateRecruitment
    {
        public long RecruitmentRequestID { get; set; }
        public long ProjectDetailID { get; set; }
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
        public string Title { get; set; }
        public string JobDescription { get; set; }
        public string Qualification { get; set; }
        public string Skill { get; set; }
        public string Experience { get; set; }

        public string StaffCategory { get; set; }
        public string ChkType { get; set; }
        public string DueDate { get; set; }
        public List <InternalLevel> InternalLevelList { get; set; }
        public List<MiscEmployee> EmployeeList { get; set; }
    }

    public class InternalLevel
    {
        public long RecruitmentLevelID { get; set; }
        public long RecruitmentRequestID { get; set; }
        public string DocType { get; set; }
     
        public long ApprovalEMPID1 { get; set; }
        public long ApprovalEMPID2 { get; set; }

    }

    public class RecruitmentRequest
    {
        public long RecruitmentRequestID { get; set; }
        public long ProjectDetailID { get; set; }
        public long JobID { get; set; }
        public long Location { get; set; }
        public string RecruitmentType { get; set; }
        public string ProjectName { get; set; }
        public long ProjectManagerID { get; set; }
        public string ManagerName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string JobTitle { get; set; }
        public string DueDate { get; set; }
        public string JobDescription { get; set; }
        public string Qualification { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
        public string StaffCategory { get; set; }
        public int ApprovedCandidateID { get; set; }
        public int PreferenceGiven { get; set; }
        public List<RecruitmentPreference> RecruitmentPreferenceList { get; set; }

        public List<RecruitmentFinalize> RecruitmentFinalizeList { get; set; }
    }

    public class RecruitmentPreference
    {
        public long CanLevelMapID { get; set; }
        public long RecruitmentRequestID { get; set; }
        public long EmpID { get; set; }
        public string EmpName { get; set; }
        public string DocType { get; set; }
        public string JobTitle { get; set; }
        public string Pillar { get; set; }
        public string Relocation { get; set; }
        public string JobTitleByHR { get; set; }
        public string PillarByHR { get; set; }
        public string RelocationByHR { get; set; }
        public int Priority { get; set; }
        [Required(ErrorMessage = "Preference Can't Blank")]
        public int Preference { get; set; }
    }

    public class RecruitmentFinalize
    {
        public long CanLevelMapID { get; set; }
        public long RecruitmentRequestID { get; set; }
        public long CandidateEmpID { get; set; }
        public string CandidateEmpName { get; set; }
        public long ApprovalEmpID { get; set; }
        public string ApprovalEmpName { get; set; }
        public string DocType { get; set; }
        public string JobTitle { get; set; }
        public string Pillar { get; set; }
        public string Relocation { get; set; }
        public string JobTitleByHR { get; set; }
        public string PillarByHR { get; set; }
        public string RelocationByHR { get; set; }
        
        public int Preference { get; set; }
        public int Approved { get; set; }
    }

    public class FinalSelection
    {
        [Required(ErrorMessage = "Please select atleast One Candidate")]
        public string ApproveCandidate { get; set; }
        public RecruitmentRequest RecruitmentRequest { get; set; }
    }

    public class RecruitmentRequestExternal
    {
        public long JobPostID { get; set; }
        public long RecruitmentRequestID { get; set; }
        public long ProjectDetailID { get; set; }
        public long JobID { get; set; }
        public long Location { get; set; }
        public string RecruitmentType { get; set; }
        public string ProjectName { get; set; }
        public long ProjectManagerID { get; set; }
        public string ManagerName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string JobTitle { get; set; }
        public string DueDate { get; set; }
        public string JobDescription { get; set; }
        public string Qualification { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
        public string StaffCategory { get; set; }

        public long AttachmentID { get; set; }
        public string Announcement { get; set; }
        [Required(ErrorMessage = "Please select atleast One Type")]
        public string AnnouncementType { get; set; }
        [Required(ErrorMessage = "Start Date Can't be Blank")]
        public string AnnouncementStartDate { get; set; }
        [Required(ErrorMessage = "End Date Can't be Blank")]
        public string AnnouncementEndDate { get; set; }
        public string Link { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
        
    }

}