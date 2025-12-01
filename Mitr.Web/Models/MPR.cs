using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class MPR
    {
        public class Section
        {
            public class SecAdd
            {
                public long MPRSID { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string SecName { get; set; }
                
                public string SecDesc { get; set; }
                public bool IsActive { set; get; }
                public int Priority { set; get; }
            }
            public class SecList
            {
                public long RowNum { get; set; }
                public long MPRSID { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string SecName { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string SecDesc { get; set; }

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

        public class SubSection
        {
            public class SubSecAdd
            {
                public long MPRSubSID { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string SubSecName { get; set; }

                [Required(ErrorMessage = "Hey! You missed this field")]
                public long MPRSID { get; set; }
                public string SecName { get; set; }

                public int NoOfCol { get; set; }

                [Required(ErrorMessage = "Hey! You missed this field")]
                public string ColText1 { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string ColDataType1 { get; set; }
                public string ColSuffix1 { get; set; }

                public string ColText2 { get; set; }
                public string ColDataType2 { get; set; }
                public string ColSuffix2 { get; set; }

                public string ColText3 { get; set; }
                public string ColDataType3 { get; set; }
                public string ColSuffix3 { get; set; }

                public bool IsActive { set; get; }
                public int Priority { set; get; }
            }
            public class SubSecList
            {
                public long RowNum { get; set; }
                public long MPRSubSID { get; set; }
                public string SubSecName { get; set; }

                public long MPRSID { get; set; }
                public string SecName { get; set; }

                public int NoOfCol { get; set; }

                public string ColText1 { get; set; }
                public string ColDataType1 { get; set; }
                public string ColSuffix1 { get; set; }

                public string ColText2 { get; set; }
                public string ColDataType2 { get; set; }
                public string ColSuffix2 { get; set; }

                public string ColText3 { get; set; }
                public string ColDataType3 { get; set; }
                public string ColSuffix3 { get; set; }

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
        public class Indicator
        {
            public class IndicatorAdd
            {
                public long IndicatorID { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public long MPRSubSecID { get; set; }
                public string SubSecName { get; set; }
                public string SecName { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string IndicatorName { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string AnswerIs { get; set; }
                public int NoOfCol { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string ColText1 { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string ColDataType1 { get; set; }
                public string ColSuffix1 { get; set; }
                public string ColText2 { get; set; }
                public string ColDataType2 { get; set; }
                public string ColSuffix2 { get; set; }
                public string ColText3 { get; set; }
                public string ColDataType3 { get; set; }
                public string ColSuffix3 { get; set; }
                public string IsOrganisational { get; set; }
                public bool IsActive { set; get; }
                public int Priority { set; get; }
            }
            public class IndicatorList
            {
                public long RowNum { get; set; }
                public long IndicatorID { get; set; }
                public long MPRSubSecID { get; set; }
                public string SubSecName { get; set; }
                public long MPRSID { get; set; }
                public string SecName { get; set; }
                public string IndicatorName { get; set; }
                public string AnswerIs { get; set; }
                public int NoOfCol { get; set; }
                public string ColText1 { get; set; }
                public string ColDataType1 { get; set; }
                public string ColSuffix1 { get; set; }
                public string ColText2 { get; set; }
                public string ColDataType2 { get; set; }
                public string ColSuffix2 { get; set; }
                public string ColText3 { get; set; }
                public string ColDataType3 { get; set; }
                public string ColSuffix3 { get; set; }
                public string IsOrganisational { get; set; }
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

        public class Targets
        {
            public class TargetsList
            {
                public long RowNum { get; set; }
                public long FinID { get; set; }
                public long ProjectID { get; set; }
                public string Year { get; set; }
                public string Proj_Name { get; set; }
                public string Projref_No { get; set; }
                public long MPRTargetID { get; set; }

            }
            public class TargetAction
            {
                public long FinID { get; set; }
                public long Approve { get; set; }
            }
            public class addTargetSetting
            {
                public string Year { get; set; }
                public string ProjectName { get; set; }
                public string Quarter { get; set; }
                public List<TargetSetting> TargetSetting { get; set; }
                public bool IsButtonShow { get; set; }

            }
            public class TargetSetting
            {
                public int RowNum { get; set; }
                public long IndicatorID { get; set; }
                public string IndicatorName { get; set; }
                public long MPRSubSecID { get; set; }
                public string SubSecName { get; set; }
                public long MPRSID { get; set; }
                public string SecName { get; set; }
                public string AnswerIs { get; set; }
                public int NoOfCol { get; set; }

                public string ColText1 { get; set; }
                public string ColDataType1 { get; set; }
                public string ColSuffix1 { get; set; }
                public string ColVal1 { get; set; }

                public string ColText2 { get; set; }
                public string ColDataType2 { get; set; }
                public string ColSuffix2 { get; set; }
                public string ColVal2 { get; set; }

                public string ColText3 { get; set; }
                public string ColDataType3 { get; set; }
                public string ColSuffix3 { get; set; }
                public string ColVal3 { get; set; }
            }
        }

        public class CreateMPR
        {
            public long MPRID { get; set; }
            public int SNo { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public int Version { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public string MPRName { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public string MPRCode { get; set; }
            public string MPRDate { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public string InitiateDate { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public long ProjectID { get; set; }
            public string ProjectName { get; set; }

            [Required(ErrorMessage = "Hey! You missed this field")]
            public long StateID { get; set; }
            public string StateName { get; set; }

            [Required(ErrorMessage = "Hey! You missed this field")]
            public long ApproverLevel1 { get; set; }
            public string ApproverLevel1Name { get; set; }
            public string ApproverLevel1Code { get; set; }

            [Required(ErrorMessage = "Hey! You missed this field")]
            public long ApproverLevel2 { get; set; }
            public string ApproverLevel2Name { get; set; }
            public string ApproverLevel2Code { get; set; }

            public int Priority { get; set; }
            public bool IsActive { get; set; }
            public List<CreateMPRDetails> CreateMPRDetails { get; set; }
            public List<StateList> StateList { get; set; }
            public List<UserList> UserList { get; set; }
            public List<ProjectList> ProjectList { get; set; }

        }
        public class MPRList
        {
            public int RowNum { get; set; }
            public long MPRID { get; set; }
            public string MPRCode { get; set; }
            public string MPRName { get; set; }
            public string Project { get; set; }
            public string State { get; set; }
            public string Version { get; set; }
            public bool IsActive { get; set; }
            public int Priority { get; set; }

        }
        public class CreateMPRDetails
        {
            public long MPRDetID { get; set; }
            public long MPRID { get; set; }
            public long IndicatorID { get; set; }
            public long MPRSID { get; set; }
            public string SecName { get; set; }
            public long MPRSubSecID { get; set; }
            public string SubSecName { get; set; }
            public string IndicatorName { get; set; }
            public string DocType { get; set; }
            public long? Executor1ID { get; set; }
            public long? Executor2ID { get; set; }
            public long? ApproverID { get; set; }

            public string Executor1Name { get; set; }
            public string Executor2Name { get; set; }
            public string ApproverName { get; set; }

            public int Priority { get; set; }
            public bool IsActive { get; set; }
            public string ChkIsActive { get; set; }
            public int NoOfCol { get; set; }
        }

        

        public class StateList
        {
            public long StateID { get; set; }
            public string StateName { get; set; }
        }
        public class UserList
        {
            public long LoginID { get; set; }
            public string UserName { get; set; }
        }
        public class ProjectList
        {
            public long ProjectID { get; set; }
            public string ProjectName { get; set; }
        }

        public class FinYear
        {
            public long FInID { get; set; }
            public string Year { get; set; }
        }

        

        

    }

    public class SMPR
    {
        public class Action
        {
            public string Month { get; set; }
            public string Submitted { get; set; }
        }
        public class List
        {
            public int RowNum { get; set; }
            public long SMPRID { get; set; }
            public long MPRID { get; set; }
            public string MPRCode { get; set; }
            public string MPRName { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public int Submitted { get; set; }
            public string DueOn { get; set; }
            
            public string MonthName { get; set; }
            public int Version { get; set; }

            public int LevelApproved { get; set; }

            public string InitiateDate { get; set; }
            public long ProjectID { get; set; }
            public string ProjectName { get; set; }
            public long StateID { get; set; }
            public string StateName { get; set; }
           
        }

        public class SMPREntry
        {
            public string ProjectName { get; set; }
            public string StateName { get; set; }
            public int Version { get; set; }
            public string MonthName { get; set; }
            public int SMPRSubmitted { get; set; }
            public int LevelApproved { get; set; }
            public string Level1Remarks { get; set; }
            public string Level2Remarks { get; set; }
            public bool IsButtonShow { get; set; }
            public int Lock { get; set; }
            public string DueOn { get; set; }
            public List<Section> SMPRSection { get; set; }
            public List<SubSection> SMPRSubSectionList { get; set; }
            public List<SMPRDetEntry> SMPRDetList { get; set; }
            public List<SMPRComments> SMPRCommentsList { get; set; }
        }

        public class SMPRApproval
        {
            public string ProjectName { get; set; }
            public string StateName { get; set; }
            public int Version { get; set; }
            public string MonthName { get; set; }
            public int SMPRSubmitted { get; set; }
            public int Lock { get; set; }
            public int LevelApproved { get; set; }
            public string Level1Remarks { get; set; }
            public string Level2Remarks { get; set; }
            public string DueOn { get; set; }
            public bool IsButtonShow { get; set; }

            public List<Section> SMPRSection { get; set; }
            public List<SubSection> SMPRSubSectionList { get; set; }
            public List<SMPRDet> SMPRDetList { get; set; }
            public List<ExecuterDetails> ExecuterDetailsList { get; set; }
            public string ApprovalRemarks { get; set; }
            public List<SMPRComments> SMPRCommentsList { get; set; }
        }
        public class ExecuterDetails
        {
            public long ExecutorID { get; set; }
            public string ExecutorName { get; set; }
            public string ExecutorEmail{ get; set; }
            public string Comment { get; set; }
            public string Chk { get; set; }
            public int Submitted { get; set; }
        }


        public class Section
        {
            public long SecID { get; set; }
            public string SecName { get; set; }
           
        }
        public class SubSection
        {
            public long SecID { get; set; }
            public string SecName { get; set; }
            public long SubSecID { get; set; }
            public string SubSecName { get; set; }
            public string OverallReason { get; set; }
        }

        public class SMPRDet
        {
           
            public int RowNum { get; set; }
            public long SMPRDetID { get; set; }
            public long SMPRID { get; set; }
            public string MonthName { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public int LevelApproved { get; set; }
        
            public int Submitted { get; set; }
            public int Approved { get; set; }
            public int SMPRSubmitted { get; set; }
            public int SMPRApproved { get; set; }
            public long MPRID { get; set; }
            public long ProjectID { get; set; }
            public string ProjectName { get; set; }
            public long StateID { get; set; }
            public string StateName { get; set; }
     
            public int Version { get; set; }
            public long MPRDetID { get; set; }
            public long MPRIndicatorID { get; set; }

            public string IndicatorName { get; set; }
            public long MPRSubSecID { get; set; }
            public long MPRSID { get; set; }
           

            public string SecName { get; set; }
            public string SubSecName { get; set; }
            public string AnswerIs { get; set; }
            public string IsOrganisational { get; set; }
            public int NoOfCol { get; set; }

            public string ColText1 { get; set; }
            public string ColDataType1 { get; set; }
            public string ColSuffix1 { get; set; }

            public string ColText2 { get; set; }
            public string ColDataType2 { get; set; }
            public string ColSuffix2 { get; set; }

            public string ColText3 { get; set; }
            public string ColDataType3 { get; set; }
            public string ColSuffix3 { get; set; }

           public string DueOn { get; set; }
            public string ExecuterVal1 { get; set; }
           
            public string ExecuterVal2 { get; set; }
         
            public string ExecuterVal3 { get; set; }


            public long Executor1ID { get; set; }
            public string Executor1Name { get; set; }
            public string Executor1Email { get; set; }

            public long Executor2ID { get; set; }
            public string Executor2Name { get; set; }
            public string Executor2Email { get; set; }

            public long ApproverID { get; set; }
            public string ApproverName { get; set; }
            public string ApproverEmail { get; set; }

           public long ApproverLevel1 { get; set; }
            public string ApproverLevel1Name { get; set; }
            public string ApproverLevel1Email { get; set; }

            public long ApproverLevel2 { get; set; }
            public string ApproverLevel2Name { get; set; }
            public string ApproverLevel2Email { get; set; }

            public int SectionPriority { get; set; }
            public int SubSectionPriority { get; set; }
            public int indicatorPriority { get; set; }
            public int Lock { get; set; }

            public string FilledBy { get; set; }
            public string Filleddat { get; set; }
            public string FilledIP { get; set; }


        }

        public class SMPRDetEntry
        {

            public int RowNum { get; set; }
            public long SMPRDetID { get; set; }
            public long SMPRID { get; set; }
            public string MonthName { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
        
            public int Submitted { get; set; }
            public int LevelApproved { get; set; }
            public int SMPRSubmitted { get; set; }
            public int SMPRApproved { get; set; }
            public long MPRID { get; set; }
            public long ProjectID { get; set; }
            public string ProjectName { get; set; }
            public long StateID { get; set; }
            public string StateName { get; set; }
            
                 public int Approved { get; set; }
            public int Version { get; set; }
            public long MPRDetID { get; set; }
            public long MPRIndicatorID { get; set; }

            public string IndicatorName { get; set; }
            public long MPRSubSecID { get; set; }
            public long MPRSID { get; set; }


            public string SecName { get; set; }
            public string SubSecName { get; set; }
            public string AnswerIs { get; set; }
            public string IsOrganisational { get; set; }
            public int NoOfCol { get; set; }

            public string ColText1 { get; set; }
            public string ColDataType1 { get; set; }
            public string ColSuffix1 { get; set; }

            public string ColText2 { get; set; }
            public string ColDataType2 { get; set; }
            public string ColSuffix2 { get; set; }
            public string DueOn { get; set; }
            public string ColText3 { get; set; }
            public string ColDataType3 { get; set; }
            public string ColSuffix3 { get; set; }

            //[Required(ErrorMessage = "Hey! You missed this field")]
            public string ExecuterVal1 { get; set; }
          
            public string ExecuterVal2 { get; set; }
          
            public string ExecuterVal3 { get; set; }


            public long Executor1ID { get; set; }
            public string Executor1Name { get; set; }
            public string Executor1Email { get; set; }

            public long Executor2ID { get; set; }
            public string Executor2Name { get; set; }
            public string Executor2Email { get; set; }

            public long ApproverID { get; set; }
            public string ApproverName { get; set; }
            public string ApproverEmail { get; set; }

            public long ApproverLevel1 { get; set; }
            public string ApproverLevel1Name { get; set; }
            public string ApproverLevel1Email { get; set; }

            public long ApproverLevel2 { get; set; }
            public string ApproverLevel2Name { get; set; }
            public string ApproverLevel2Email { get; set; }

            public int SectionPriority { get; set; }
            public int SubSectionPriority { get; set; }
            public int indicatorPriority { get; set; }
            public int Lock { get; set; }

            public string FilledBy { get; set; }
            public string Filleddat { get; set; }
            public string FilledIP { get; set; }

            


        }

        public class LevelApprover
        {
            public string ProjectName { get; set; }
            public string StateName { get; set; }
            public int Version { get; set; }
            public string MonthName { get; set; }
            public int SMPRSubmitted { get; set; }
            public int Approved { get; set; }
            public string Level1Remarks { get; set; }
            public string Level2Remarks { get; set; }
            public bool IsButtonShow { get; set; }
            public int Lock { get; set; }
            public string DueOn { get; set; }
            public List<Section> SMPRSection { get; set; }
            public List<SubSection> SMPRSubSectionList { get; set; }
            public List<SMPRDet> SMPRDetList { get; set; }
            public List<ExecuterDetails> ExecuterDetailsList { get; set; }
            public List<SMPRComments> SMPRCommentsList { get; set; }
        }
        public class LevelApprover2
        {
            public string ProjectName { get; set; }
            public string StateName { get; set; }
            public int Version { get; set; }
            public string MonthName { get; set; }
            public int SMPRSubmitted { get; set; }
            public int Approved { get; set; }
            public string Level1Remarks { get; set; }
            public string Level2Remarks { get; set; }
            public bool IsButtonShow { get; set; }
            public int Lock { get; set; }
            public string DueOn { get; set; }
            public List<Section> SMPRSection { get; set; }
            public List<SubSection> SMPRSubSectionList { get; set; }
            public List<SMPRDet> SMPRDetList { get; set; }
            public List<ExecuterDetails> ExecuterDetailsList { get; set; }
            public List<SMPRComments> SMPRCommentsList { get; set; }
        }

        public class SMPRComments
        {
            public long SCID { get; set; }
            public long SMPRID { get; set; }
            public string Comment { get; set; }
            public string Doctype { get; set; }
            public long SectionID { get; set; }
            public long createdby { get; set; }
            public string CreatedByName { get; set; }
            public string CreatedByEmail { get; set; }
            public string createddat { get; set; }
            public string IPAddress { get; set; }
        }

        public class SMPRLock
        {
            public long SMPRID { get; set; }
            public int Lock { get; set; }
            public string dtdate { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public string LockRemarks { get; set; }
        }

    }
  


    public class MPRDashboard
    {
        public string Month { get; set; }
        public long ProjectID { get; set; }
        public long StateID { get; set; }
        public List<MPR.StateList> StateList { get; set; }
        public List<MPR.ProjectList> ProjectList { get; set; }
        

        public class List
        {
            public int RowNum { get; set; }
            public string DueOn { get; set; }
            public long SMPRID { get; set; }
            public long MPRID { get; set; }
            public string MPRCode { get; set; }
            public string MPRName { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public string MonthName { get; set; }
            public int Version { get; set; }
            public string InitiateDate { get; set; }
            public long ProjectID { get; set; }
            public string ProjectName { get; set; }
            public long StateID { get; set; }
            public string StateName { get; set; }
            public int Submitted { get; set; }
            public int Approved { get; set; }
            
            public int TotalExec { get; set; }
            public int ExecCompleted { get; set; }
            public int AppTotal { get; set; }
            public int AppCompleted { get; set; }
            public int Lock { get; set; }
            public int ApproverLevel2 { get; set; }
            public int ApproverLevel1 { get; set; }
            public int Executor1ID { get; set; }
            public int Executor2ID { get; set; }
            public int ApproverID { get; set; }
        }

        public class ListDashboard
        {

            public long PendingAtExcutor { get; set; }
            public long PendingSectionApproval { get; set; }
            public long Pendinglevel1 { get; set; }
            public long Pendinglevel2 { get; set; }
            public long Completed { get; set; }

        }
    }

    public class LockUnlock
    {
        public string Month { get; set; }

        public class List
        {
            public int RowNum { get; set; }
            public long SMPRID { get; set; }
            public long MPRID { get; set; }
            public string MPRCode { get; set; }
            public string MPRName { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public string MonthName { get; set; }
            public int Version { get; set; }
            public string InitiateDate { get; set; }
            public long ProjectID { get; set; }
            public string ProjectName { get; set; }
            public long StateID { get; set; }
            public string StateName { get; set; }
            public int Approved { get; set; }
         
            public int Lock { get; set; }
            public string LastUpdatedOn { get; set; }
            public int UpdatedCount { get; set; }
            public string Status { get; set; }
            public string LastLockedDate { get; set; }
            public string DueOn { get; set; }

        }
        public class HistoryList
        {
            public long SMPRLHID { get; set; }
            public string Date { get; set; }
            public string Remarks { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }

    public class MPRSetting
    {
        public long MPRSettingID { get; set; }

        [Range(1, 28, ErrorMessage = "Hey, you have entered an invalid Date.")]
        public int MPRDueDate { get; set; }
    }

    public class MPRReports
    {
        public class Header
        {
            [Required(ErrorMessage = "Hey! You missed this field")]
            public string ReportName { get; set; }
            public long?[] ProjectID { get; set; }
            public long?[] StateID { get; set; }
            public long?[] SectionID { get; set; }
            public long?[] MPRSubSID { get; set; }            
            public string StartDate { get; set; }            
            public string EndDate { get; set; }
            public string Year { get; set; }
            public List<State> StateList { get; set; }
            public List<Project> ProjectList { get; set; }
            public List<Section> SectionList { get; set; }
            public List<SubSection> SubSectionList { get; set; }
            public List<MPRData> MPRDataList { get; set; }
            public long ?[] MPRID { get; set; }
            public string MPRName { get; set; }
            public bool ? IsInActiveRecords { get; set; }
        }
        public class MPRData
        {
            public long MPRID { get; set; }
            public string MPRName { get; set; }

        }
        public class State
        {
            public long StateID { get; set; }
            public string StateName { get; set; }

        }
        public class Project
        {
            public long ProjectID { get; set; }
            public string ProjectName { get; set; }
        }
        public class Section
        {
            public long SectionID { get; set; }
            public string SectionName { get; set; }
        }
        public class SubSection
        {
            public long MPRSubSID { get; set; }
            public string SubSecName { get; set; }
        }


        public class List
        {
            public string ProjectName { get; set; }
            public string Doctype { get; set; }
            public string IndicatorName { get; set; }
            public long MPRSID { get; set; }
            public string SecName { get; set; }
            public long MPRSubSecID { get; set; }
            public string SubSecName { get; set; }
            public int NoOfCol { get; set; }
            public string ColText1 { get; set; }
            public string ColText2 { get; set; }
            public string ColText3 { get; set; }
            public string ColVal1 { get; set; }
            public string ColVal2 { get; set; }
            public string ColVal3 { get; set; }
            public string ColDataType1 { get; set; }
            public string ColDataType2 { get; set; }
            public string ColDataType3 { get; set; }
        }
        public class AchievementList
        {
            public string RowNum { get; set; }
            public string SMPRID { get; set; }
            public string Month { get; set; }
            public string Year { get; set; }
            public string Lock { get; set; }
            public string MPRID { get; set; }
            public string ProjectID { get; set; }
            public string ProjectName { get; set; }
            public string StateID { get; set; }
            public string StateName { get; set; }
            public string Version { get; set; }
            public string DueOn { get; set; }
            public string MPRDetID { get; set; }
            public string MPRIndicatorID { get; set; }
            public string IndicatorName { get; set; }
            public string MPRSID { get; set; }
            public string SecName { get; set; }
            public string MPRSubSecID { get; set; }
            public string SubSecName { get; set; }
            public string NoOfCol { get; set; }
            public string ColText1 { get; set; }
            public string ColDataType1 { get; set; }
            public string ColSuffix1 { get; set; }
            public string ColText2 { get; set; }
            public string ColDataType2 { get; set; }
            public string ColSuffix2 { get; set; }
            public string ColText3 { get; set; }
            public string ColDataType3 { get; set; }
            public string ColSuffix3 { get; set; }
            public string ExecuterVal1 { get; set; }
            public string ExecuterVal2 { get; set; }
            public string ExecuterVal3 { get; set; }
            public string FromData { get; set; }

        }
        public class AchievementListHeader
        {
            public string StateID { get; set; }
            public string StateName { get; set; }
            public string ProjectID { get; set; }
            public string ProjectName { get; set; }
            public string NoofCol { get; set; }
        }

        public class MPRReportExcel
        {

            public string MPRName { get; set; }
            public string ProjectName { get; set; }
            public string StateName { get; set; }
            public string MonthName { get; set; }
            public string Executor1 { get; set; }
            public string Executor2 { get; set; }
            public string LastDateExecutor { get; set; }
            public string SectionApproval { get; set; }
            public string LastDateSectionApproval { get; set; }
            public string Level1 { get; set; }
            public string LastDateLevel1 { get; set; }
            public string Level2 { get; set; }
            public string LastDateLevel2 { get; set; }
            public string Status { get; set; }
            public string MPRStatus { get; set; }
            public string TAT { get; set; }
            
        }

    }
}