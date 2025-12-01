using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.CommonLib
{
   public static class Constants
    {
        #region Constant for Digital library
        
        public enum ScreenID
        {
            SubCategory=100,
            Master=101  ,
            DLContent=102,
            DLContentReqNo= 103,
            TagMAster=104,
            CategoryEDODLinkup=105,
            TeamLeadMaster=106,
            ProjectDetails=107,
            SearchContentDetails= 108,
            AttachementSearch=109,
            MyContent=110,
            ContentUpdate= 111,
            MyTeamContent=112,
            Approval=113,
            RejectedDocument=114,
            BulkUpload=115,
            Matrix = 116,
            Dashboard = 117,
            MyRequestApproval = 118,
            SharedContent = 119,
            ContentCount = 120,
            SharedContentReport=121,
            DLDownloadReport=143,
            DLUploadReport = 144,
            DLSharedReport = 145,
            SallaryData=401
        }
        #endregion

        #region Constant for Procurment
        public enum ScreenIDProcurment
        {
            VendorType = 122,
            RelationshipMaster= 123,
            GoodAndServicesCategory=124,
            PurposeMaster  = 125,
            ProcurSaveApprovalAuthority= 126,
            ProcureCommitteeMembers = 127, 
            ProcureBankDetails = 128,
            ProcureMaxNo=129,
            ProcureVendorRegistration= 130,
            ProcureVendorApproval= 131,
            ProcureApproval = 132,
            ProcureProjectDetails=133,
            ManageProcureRegistration = 134,
            ApproveProcureRegistration = 135,
            RFPEntry = 136,
            QuotationEntry=137,
            QuotationConsultant= 138,
            QuotationSubgrant = 139,
            AuthorisedSignatoryMaster = 140,
            Template = 141,
            VendorSearch = 142,
            QuotationPOContractNumber=146,
            CheckVendorRating = 147,
            QuotationEntryAmend = 148,
            QuotationEntryAmendConsultant = 149,
            UpdatePOC = 150

        }
        #endregion

        #region MasterTableTypeConstant
       
        public const string SubCategory = "1"; 
        public const string VenderType = "13";
        public const string RelationshipManager = "14";
        public const string GoodAndServicesCategory = "15";
        public const string PurposeMaster = "16";
        public const string ProcurementBankDetails = "17";
        public const string ComplianceCategory = "100";
        public const string ComplianceSubCategory = "101";
        #endregion

        #region Constant for Employee Salary

        public enum EmployeeSalaryScreenID
        {
            BonusPaymentEntry = 201
        }
        #endregion

        #region Constant for Onboarding
        public enum ScreenIDOnboarding
        {
            OnboardingHRScreen = 300,
            OnboardingStatus = 301,
            OnboardingProcess = 302,
            OnboardingUser= 303,
            OnboardingDeleteListData=304
        }
        #endregion

        #region Constant for Exit
        public enum ScreenIDExit
        {
            ExitAssetsDetail = 505            
        }
        #endregion

        #region Capacity
        public enum ScreenIDCapacity
        {
            TraniningTypeMaster = 500,  
            TrainingCalenderMaster= 501,
            TrainingReqMaxNo = 502,
            TrainingRequest=503,
            GetTrainingDesc=504,
            TrainingRequestNumber = 506,
            TrainingRequestCalender = 507,
            HRTrainingRequest = 508,
            GetAllHRTrainingRequest = 509,
            UserTrainingRequest = 510,
            HRTrainingRequestDetails = 511,
            MasterTainingTypeStatusUpdate = 512,
            TrainingRequestConfirmAndbtnReject = 513,
            TrainingRequestClubbed = 514,
            TrainingRequestDraft = 515,
            GetTrainingRequestDraft = 516,
            HRTrainingRequestDraftDataSave = 517,
            GetTrainingRequestProcess = 518,
            GetHRTrainingRequestProcess=519,
            GetAllTrainingAttendancePending = 520,
            GetAllTrainingAssessmentRequestPending = 521,
            GetAllTrainingRequestViewCompleted = 522,
            GetAllSupervisorAssessmentRequest = 523,
            GetTrainingCalendarByReqID = 524,
            SaveSupervisorComments = 525,
            GetTrainingRequestFeedback = 526,
            GetTrainingRequestFeedBackCompleted = 527,
            GetAllUserTrainingRequestList = 528,
            GetALLHRFeedbackRequest = 529,
            GetHRAssessmentRequest = 530
        }
        #endregion 

        #region Compliance
        public enum ScreenIDCompliance
        {
            ComplianceMasterGet = 700
            
        }       
        public const string ComlianceLegacyData = "Com100";
        public const string ComlianceGetMyTranData = "Com101";
        public const string ComlianceViewMyTranData = "Com102";
        public const string ComlianceViewMyTranApproval = "Com103";
        public const string ComlianceViewToManage = "Com104";
        public const string ComplianceReportManage = "ComplianceReport";
        
        #endregion

        #region Module Infomation
        
        public const string MasterModule = "ModuleInf_100";
        #endregion


        #region Topic Infomation

        public const string MasterTopic = "ModuleInf_101";
        #endregion

        #region Page Infomation

        public const string MasterPage = "ModuleInf_102";
        #endregion

        #region Help Cart GET Infomation

        public const string GetHelpCart = "ModuleInf_103";
        #endregion


        #region Help Cart Topic GET Infomation

        public const string GetTopicHelpCart = "ModuleInf_104";
        #endregion

        #region Help Cart Page GET Infomation

        public const string GetPageHelpCart = "ModuleInf_105";
        #endregion

        #region Help Cart Page GET Infomation

        public const string GetAllPageHelpCart = "ModuleInf_106";
        #endregion

        #region Help Cart Page GET Infomation

        public const string OfficePolicy = "ModuleInf_107";
        #endregion
    }
}
