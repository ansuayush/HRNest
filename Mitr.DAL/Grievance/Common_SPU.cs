using Mitr.Model;
using Mitr.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Mitr.DAL
{
    public class Common_SPU
    {


        #region GetStoreProcedure

        public static DataSet fnGetEmployeesData()
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmployeesData", oparam);
        }
        //#region REC
        //public static DataSet fnGetREC_TalentPoolList(long REC_ReqID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_TalentPoolList", oparam);
        //}

        //public static DataSet fnGetREC_EApplication_InterviewP(long REC_AppID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@REC_AppID", REC_AppID);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EApplication_InterviewP", oparam);
        //}


        //public static DataSet fnGetREC_EInterviewCandidate(long REC_ReqID, long REC_InterviewSetID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[3];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    oparam[1] = new SqlParameter("@REC_InterviewSetID", REC_InterviewSetID);
        //    oparam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EInterviewCandidate", oparam);
        //}
        //public static DataSet fnGetREC_ESelectionProcess_ViewList(long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@Approved", Approved);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ESelectionProcess_ViewList", oparam);
        //}

        //public static DataSet fnGetREC_ESelectionProcessList(long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@Approved", Approved);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ESelectionProcessList", oparam);
        //}


        //public static DataSet fnGetREC_EInterviewSetting(long REC_ReqID, long REC_InterviewSetID, string DocType)
        //{
        //    SqlParameter[] oparam = new SqlParameter[3];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    oparam[1] = new SqlParameter("@REC_InterviewSetID", REC_InterviewSetID);
        //    oparam[2] = new SqlParameter("@DocType", DocType);
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EInterviewSetting", oparam);
        //}

        //public static DataSet fnGetREC_EInterviewSettingList(long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@Approved", Approved);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EInterviewSettingList", oparam);
        //}

        //public static DataSet fnGetREC_EConfirmedCV(long REC_ReqID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EConfirmedCV", oparam);
        //}

        //public static DataSet fnGetREC_EConfirmedCVList(long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@Approved", Approved);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EConfirmedCVList", oparam);
        //}

        //public static DataSet fnGetREC_ApplicationView(long REC_AppID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[3];
        //    oparam[0] = new SqlParameter("@REC_AppID", REC_AppID);
        //    oparam[1] = new SqlParameter("@REC_ReqID", 0);
        //    oparam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ApplicationView", oparam);
        //}



        //public static DataSet fnGetREC_EShortListedApplicationList(long REC_ReqID, long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[3];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    oparam[1] = new SqlParameter("@Approved", Approved);
        //    oparam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EShortListedApplicationList", oparam);
        //}


        //public static DataSet fnGetREC_EShortlistedCVList(long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@Approved", Approved);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EShortlistedCVList", oparam);
        //}

        //public static DataSet fnGetREC_EScreeningApplicationList(long REC_ReqID, string Applied, string Worked, long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[5];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    oparam[1] = new SqlParameter("@Applied", ClsCommon.EnsureString(Applied));
        //    oparam[2] = new SqlParameter("@Worked", ClsCommon.EnsureString(Worked));
        //    oparam[3] = new SqlParameter("@Approved", Approved);
        //    oparam[4] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EScreeningApplicationList", oparam);
        //}

        //public static DataSet fnGetREC_EVacancyResponseList(long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@Approved", Approved);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EVacancyResponseList", oparam);
        //}

        //public static DataSet fnGetREC_ApplyJob(string Code)
        //{
        //    SqlParameter[] oparam = new SqlParameter[1];
        //    oparam[0] = new SqlParameter("@Code", ClsCommon.EnsureString(Code));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ApplyJob", oparam);
        //}

        //public static DataSet fnGetREC_EVacancyAnno_COMMList(long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@Approved", Approved);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EVacancyAnno_COMMList", oparam);
        //}
        //public static DataSet fnGetREC_EVacancyAnnoList(long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@Approved", Approved);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EVacancyAnnoList", oparam);
        //}
        //public static DataSet spu_GetREC_ERequestsList(int iApproved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[1];
        //    oparam[0] = new SqlParameter("@Approved", iApproved);
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ERequestsList", oparam);
        //}
        //public static DataSet fnGetREC_EVacancyAnno(long REC_ReqID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EVacancyAnno", oparam);
        //}


        //public static DataSet fnGetREC_IFinal(long REC_ReqID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IFinal", oparam);
        //}


        //public static DataSet fnGetREC_IFinalList(long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@Approved", Approved);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IFinalList", oparam);
        //}

        //public static DataSet fnGetREC_IPreference(long REC_ReqID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IPreference", oparam);
        //}


        //public static DataSet fnGetREC_IPreferenceList(long Approved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@Approved", Approved);
        //    oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IPreferenceList", oparam);
        //}

        //public static DataSet fnGetREC_IStaff(long REC_ReqID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[1];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IStaff", oparam);
        //}

        //public static DataSet fnGetREC_IAvailStaff(long REC_ReqID, long PillarID, long LocationID, long JobID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[4];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    oparam[1] = new SqlParameter("@PillarID", PillarID);
        //    oparam[2] = new SqlParameter("@LocationID", LocationID);
        //    oparam[3] = new SqlParameter("@JobID", JobID);
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IAvailStaff", oparam);
        //}
        //public static DataSet fnGetREC_RequestsView(long REC_ReqID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[1];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_RequestsView", oparam);
        //}


        //public static DataSet fnGetREC_Requests(long REC_ReqID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[1];
        //    oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_Requests", oparam);
        //}


        //public static DataSet fnGetREC_RequestsList(int iApproved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[1];
        //    oparam[0] = new SqlParameter("@Approved", iApproved);
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_RequestsList", oparam);
        //}

        //public static DataSet fnGetREC_Initiate(long ProjectDetailID)
        //{
        //    SqlParameter[] oparam = new SqlParameter[1];
        //    oparam[0] = new SqlParameter("@ProjectDetailID", ProjectDetailID);
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_Initiate", oparam);
        //}

        //public static DataSet fnGetREC_ViewBudget(long projregdet_id)
        //{
        //    SqlParameter[] oparam = new SqlParameter[1];
        //    oparam[0] = new SqlParameter("@projregdet_id", projregdet_id);
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ViewBudget", oparam);
        //}

        //public static DataSet fnGetREC_Pendancy(int iApproved)
        //{
        //    SqlParameter[] oparam = new SqlParameter[2];
        //    oparam[0] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
        //    oparam[1] = new SqlParameter("@Approved", iApproved);
        //    return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_Pendancy", oparam);
        //}
        //#endregion

        public static DataSet fnGetPMS_Appraisal(long PMS_AID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@PMS_AID", PMS_AID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_Appraisal", oparam);
        }

        public static DataSet fnGetMPRReport_SP_Achv(DateTime StartDate, DateTime Enddate, string Projects, string States, string Sections)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@StartDate", StartDate);
            oparam[1] = new SqlParameter("@Enddate", Enddate);
            oparam[2] = new SqlParameter("@Projects", ClsCommon.EnsureString(Projects));
            oparam[3] = new SqlParameter("@States", ClsCommon.EnsureString(States));
            oparam[4] = new SqlParameter("@Sections", ClsCommon.EnsureString(Sections));
            oparam[5] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRReport_SP_Achv", oparam);
        }


        public static DataSet fnGetMPRReport_PS_Achv(DateTime StartDate, DateTime Enddate, string Projects, string States, string Sections)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@StartDate", StartDate);
            oparam[1] = new SqlParameter("@Enddate", Enddate);
            oparam[2] = new SqlParameter("@Projects", ClsCommon.EnsureString(Projects));
            oparam[3] = new SqlParameter("@States", ClsCommon.EnsureString(States));
            oparam[4] = new SqlParameter("@Sections", ClsCommon.EnsureString(Sections));
            oparam[5] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRReport_PS_Achv", oparam);
        }


        public static DataSet fnGetMPRReport_Ach_Vs_OrgTrg(DateTime StartDate, DateTime Enddate, string Sections)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@StartDate", StartDate);
            oparam[1] = new SqlParameter("@Enddate", Enddate);
            oparam[2] = new SqlParameter("@Sections", ClsCommon.EnsureString(Sections));
            oparam[3] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRReport_Ach_Vs_OrgTrg", oparam);
        }

        public static DataSet fnGetMPRReport_Ach_Vs_Trg(DateTime StartDate, DateTime Enddate, string Projects, string Sections)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@StartDate", StartDate);
            oparam[1] = new SqlParameter("@Enddate", Enddate);
            oparam[2] = new SqlParameter("@Projects", ClsCommon.EnsureString(Projects));
            oparam[3] = new SqlParameter("@Sections", ClsCommon.EnsureString(Sections));
            oparam[4] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRReport_Ach_Vs_Trg", oparam);
        }

        public static DataSet fnGetPMS_CMCAppraisal(long PMS_AID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@PMS_AID", PMS_AID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_CMCAppraisal", oparam);
        }

        public static DataSet fnGetPMS_GroupAppraisal(long PMS_AID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@PMS_AID", PMS_AID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_GroupAppraisal", oparam);
        }

        public static DataSet fnGetPMS_TeamAppraisal(long PMS_AID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@PMS_AID", PMS_AID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_TeamAppraisal", oparam);
        }



        public static DataSet fnGetPMS_TeamAppraisal_List(long FYID, string Doctype = "Appraisal")
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[2] = new SqlParameter("@Doctype", Doctype);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_TeamAppraisal_List", oparam);
        }

        public static DataSet fnGetPMS_GroupAppraisal_List(long FYID, string Doctype = "")
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[2] = new SqlParameter("@Doctype", Doctype);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_GroupAppraisal_List", oparam);
        }
        public static DataSet fnGetPMS_CMCAppraisal_List(long FYID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_CMCAppraisal_List", oparam);
        }
        public static DataSet fnGetPMS_CommentRequest(long FYID, long EMPID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_CommentRequest", oparam);
        }

        public static DataSet fnGetPMS_SelfAppraisal(long FYID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_SelfAppraisal", oparam);
        }

        public static DataSet fnGetPMS_EMPGoalSheet(long PMS_GSID, string OperationType)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@PMS_GSID", PMS_GSID);
            oparam[1] = new SqlParameter("@OperationType", ClsCommon.EnsureString(OperationType));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_EMPGoalSheet", oparam);
        }

        public static DataSet fnGetPMS_GoalSheet_Det(long GoalSheetID, long PMS_GSDID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@GoalSheetID", GoalSheetID);
            oparam[1] = new SqlParameter("@PMS_GSDID", PMS_GSDID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_GoalSheet_Det", oparam);
        }

        public static DataSet fnGetGoalSheet(long FYID, long EMPID, string OperationType, int Approved)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@OperationType", ClsCommon.EnsureString(OperationType));
            oparam[3] = new SqlParameter("@Approved", Approved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_GoalSheet", oparam);
        }


        public static DataSet fnGetPMS_Essential(long PMS_EID, string OperationType, long FYID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@PMS_EID", PMS_EID);
            oparam[1] = new SqlParameter("@FYID", FYID);
            oparam[2] = new SqlParameter("@OperationType", ClsCommon.EnsureString(OperationType));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_Essential", oparam);
        }


        public static DataSet fnGetPMS_KPA(long KPAID, long FYID, string OperationType, string IsActive)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@KPAID", KPAID);
            oparam[1] = new SqlParameter("@FYID", FYID);
            oparam[2] = new SqlParameter("@OperationType", ClsCommon.EnsureString(OperationType));
            oparam[3] = new SqlParameter("@IsActive", ClsCommon.EnsureString(IsActive));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_KPA", oparam);
        }

        public static DataSet fnGetFinancialYear(long ID, string IsActive)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@IsActive", IsActive);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetFinancialYear", oparam);
        }

        public static DataSet fnGetPMSUOM(long PMSUOMID, long FYID, string IsActive)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@PMSUOMID", PMSUOMID);
            oparam[1] = new SqlParameter("@IsActive", IsActive);
            oparam[2] = new SqlParameter("@FYID", FYID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_UOM", oparam);
        }

        public static DataSet fnGetMPRSetting(long MPRSettingID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@MPRSettingID", MPRSettingID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRSetting", oparam);
        }
        public static DataSet fnGetSMPRLockHistory(long SMPRID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@SMPRID", SMPRID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSMPRLockHistory", oparam);
        }

        public static DataSet fnGetLockunlockSMPR(DateTime dtDate)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@dtDate", dtDate);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLockunlockSMPR", oparam);
        }


        public static DataSet fnGetSMPRStatus(long SMPRID, string Operation)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@SMPRID", SMPRID);
            oparam[1] = new SqlParameter("@Operation", Operation);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSMPRStatus", oparam);
        }


        public static DataSet fnGetMPRDashboardList(long StateID, long ProjectID, int Month, int Year)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@StateID", StateID);
            oparam[1] = new SqlParameter("@ProjectID", ProjectID);
            oparam[2] = new SqlParameter("@Month", Month);
            oparam[3] = new SqlParameter("@Year", Year);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRDashboardList", oparam);
        }

        public static DataSet fnGetMPRDashboardHeader()
        {
            return clsDataBaseHelper.ExecuteDataSet("exec spu_GetMPRDashboardHeader");
        }


        public static DataSet fnGetMPR_Reports_Header()
        {
            return clsDataBaseHelper.ExecuteDataSet("exec spu_GetMPR_Reports_Header");
        }

        public static DataSet fnGetSMPRComments(long SMPRID, long SectionID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@SMPRID", SMPRID);
            oparam[1] = new SqlParameter("@SectionID", SectionID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSMPRComments", oparam);
        }


        public static DataSet fnGetSMPRdet(long SMPRID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@SMPRID", SMPRID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("Spu_GetSMPRdet", oparam);
        }

        public static DataSet fnGetSMPRList(string Submitted, int month, int year, string ListType)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[1] = new SqlParameter("@Submitted", Submitted);
            oparam[2] = new SqlParameter("@month", month);
            oparam[3] = new SqlParameter("@Year", year);
            oparam[4] = new SqlParameter("@ListType", ListType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSMPRList", oparam);
        }

        public static DataSet fnGetMPRTargetDet(long ProjectID, long FinID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@finId", FinID);
            oparam[1] = new SqlParameter("@ProjectID", ProjectID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRTargetDet", oparam);
        }

        public static DataSet fnGetMPRTargetList(string TargetType, long FinID)
        {
            SqlParameter[] oparam = new SqlParameter[2];

            oparam[0] = new SqlParameter("@TargetType", TargetType);
            oparam[1] = new SqlParameter("@FinID", FinID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRTargetList", oparam);
        }

        public static DataSet fnGetMPRIndicator(long IndicatorID, string IsActive = "0,1")
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@IndicatorID", IndicatorID);
            oparam[1] = new SqlParameter("@IsActive", IsActive);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRIndicator", oparam);
        }

        public static DataSet fnGetMPRSubSec(long MPRSubSID, string IsActive = "0,1")
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@MPRSubSID", MPRSubSID);
            oparam[1] = new SqlParameter("@IsActive", IsActive);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRSubSec", oparam);
        }

        public static DataSet fnGetMPRSec(long MPRSID, string IsActive = "0,1")
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@MPRSID", MPRSID);
            oparam[1] = new SqlParameter("@IsActive", IsActive);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRSec", oparam);
        }

        public static DataSet fnValidateCLSLLeave(DateTime StartDate, DateTime EndDate)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@StartDate", StartDate);
            oparam[2] = new SqlParameter("@EndDate", EndDate);
            return clsDataBaseHelper.ExecuteDataSet("spu_ValidateCLSLLeave", oparam);
        }
        public static DataSet fnGetTicketNotes(long TicketID, long TicketNotesID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@TicketID", TicketID);
            oparam[1] = new SqlParameter("@TicketNotesID", TicketNotesID);
            oparam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTicketNotes", oparam);
        }
        public static DataSet fnGetTicketStatus(long TicketStatusID, string Type, string IsActive)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@TicketStatusID", TicketStatusID);
            oparam[1] = new SqlParameter("@Type", Type);
            oparam[2] = new SqlParameter("@IsActive", IsActive);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTicketStatus", oparam);
        }

        public static DataSet fnGetTicket(long TicketID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@TicketID", TicketID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTicket", oparam);
        }

        public static DataSet fnGetMPR(long MPRID, string IsActive, string MPRType)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@MPRID", MPRID);
            oparam[1] = new SqlParameter("@IsActive", IsActive);
            oparam[2] = new SqlParameter("@MPRType", MPRType);
            oparam[3] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPR", oparam);
        }



        public static DataSet fnGetUserman(long ID, string WantCurrentUserInList = "N")
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[2] = new SqlParameter("@WantCurrentUserInList", WantCurrentUserInList);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetUserman", oparam);
        }

        public static DataSet fnGetTicketConfigration(long TicketCongID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@TicketCongID", TicketCongID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTicketConfigration", oparam);
        }

        public static DataSet fnGetTicket_Setting(long SubSettingID, long SubCategoryID, string Doctype)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@SubSettingID", SubSettingID);
            oparam[1] = new SqlParameter("@SubCategoryID", SubCategoryID);
            oparam[2] = new SqlParameter("@Doctype", Doctype);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTicket_Setting", oparam);
        }

        public static DataSet fnGetTicket_SubCategory(long SubCategoryID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@SubCategoryID", SubCategoryID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTicket_SubCategory", oparam);
        }

        public static DataSet fnGetLocationGroup(long LocationGroupID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@LocationGroupID", LocationGroupID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLocationGroup", oparam);
        }

        public static DataSet fnGetJobPost(long JobPostID, string For)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@JobPostID", JobPostID);
            oparam[1] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@For", For);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetJobPost", oparam);
        }

        public static DataSet fnGetRecruitmentRequestExternal(long RecruitmentRequestID, string For)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@RecruitmentRequestID", RecruitmentRequestID);
            oparam[1] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@For", For);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetRecruitmentRequest_External", oparam);
        }
        public static DataSet fnGetRecruitmentRequestInternal(long RecruitmentRequestID, string For)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@RecruitmentRequestID", RecruitmentRequestID);
            oparam[1] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@For", For);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetRecruitmentRequest_Internal", oparam);
        }

        public static DataSet fnGetRecruitmentInternal_Finalized(long RecruitmentRequestID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@RecruitmentRequestID", RecruitmentRequestID);
            return clsDataBaseHelper.ExecuteDataSet("Spu_GetRecruitmentInternal_Finalized", oparam);
        }
        public static DataSet fnGetRecruitmentPreferenceRaw(long RecruitmentRequestID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@RecruitmentRequestID", RecruitmentRequestID);
            oparam[1] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetRecruitmentPreferenceRaw", oparam);
        }


        public static DataSet fnGetSelectedRecruitmentCandidate_InternalList(long InternalCandidateID, long RecruitmentRequestID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@InternalCandidateID", InternalCandidateID);
            oparam[1] = new SqlParameter("@RecruitmentRequestID", RecruitmentRequestID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSelectedRecruitmentCandidate_InternalList", oparam);
        }
        public static DataSet fnGetInitiateRecruitment(long ProjectDetailID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ProjectDetailID", ProjectDetailID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetInitiateRecruitment", oparam);
        }

        public static DataSet fnGetInternalStaffAvailabilityList(long ProjectDetailID, long LocationID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@ProjectDetailID", ProjectDetailID);
            oparam[1] = new SqlParameter("@LocationID", LocationID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetInternalStaffAvailabilityList", oparam);
        }
        public static DataSet fnGetLeadDashboard()
        {

            return clsDataBaseHelper.ExecuteDataSet("spu_Getleaddashbaordlist");
        }
        public static int fnDelTravelReq(string lTravelReqId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@id", lTravelReqId);
            oparam[1] = new SqlParameter("@USERID", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteSp("spu_DelTravelReq", oparam);
            return 1;

        }
        public static DataSet fnGetJob(long JobID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ID", JobID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetJob", oparam);
        }
        public static DataSet fnGetCompanyConfig(long id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCompanyConfiguration", oparam);
        }
        public static long fnSetCompanyConfig(long id, string CompanyName, string CompanyAdress)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@id", id);
            oparam[1] = new SqlParameter("@CompanyName", CompanyName);
            oparam[2] = new SqlParameter("@Address", CompanyAdress);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetCompanyConfiguration", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;


        }
        public static DataSet fnGetJobDetail(long JobID, long JobDetailID, string DocType)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@JobID", JobID);
            oparam[1] = new SqlParameter("@JobDetailID", JobDetailID);
            oparam[2] = new SqlParameter("@DocType", DocType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetJobDetail", oparam);
        }



        public static DataSet fnGetAttachmentList(long ID, string TableID, string Tablename)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@TableID", TableID);
            oparam[2] = new SqlParameter("@Tablename", Tablename);
            oparam[3] = new SqlParameter("@loginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetAttachment", oparam);
        }
        public static DataSet fnGetFreeMeal(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ID", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetFreeMeal", oparam);
        }
        public static DataSet fnGetCompleteTravelRequest_Expense(long TravelRequestID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCompleteTravelRequest_Expense", oparam);
        }
        public static DataSet fnGetTravelDeskList_Dashboard(long locationid, long Travelmode)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@locationid", locationid);
            oparam[1] = new SqlParameter("@Userid", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[2] = new SqlParameter("@Travelmode", Travelmode);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelDeskList_Dashboard", oparam);

        }
        public static DataSet fnGetTravelDeskList(long iIsBooked, long locationid)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@IsBooked", iIsBooked);
            oparam[1] = new SqlParameter("@locationid", locationid);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelDeskList", oparam);

        }

        public static DataSet fnGetTravelRequest_ForDesk(long TravelRequestID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            oparam[1] = new SqlParameter("@userid", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelRequest_ForDesk", oparam);
        }
        public static DataSet fnGetTravelDocuments(long TravelRequestID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelDocuments", oparam);
        }

        public static DataSet fnGetHoursSummary(long EMPID, int Month, int Year)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@EMPID", EMPID);
            oparam[1] = new SqlParameter("@Month", Month);
            oparam[2] = new SqlParameter("@Year", Year);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetHoursSummary", oparam);
        }

        public static DataSet fnGetMasterProcurementCommittee(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMasterProcurementCommittee", oparam);
        }


        public static DataSet fnGetDepreciationRate(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetDepreciationRate", oparam);
        }


        public static DataSet fnGetVendorMaster(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetVendorMaster", oparam);
        }

        public static DataSet fnGetSubLineItem(long lSubLineItemId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", lSubLineItemId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSublineItem", oparam);
        }

        public static DataSet fnGetCostCenter(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ID", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCostCenter", oparam);
        }
        public static DataSet fnGetDonor(long lDonorId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", lDonorId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetDonor", oparam);
        }
        public static DataSet fnGetDonorDetails(long IDonorDetailsID, long lDonorId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@id", IDonorDetailsID);
            oparam[1] = new SqlParameter("@Donor_Id", lDonorId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetDonorDetails", oparam);
        }

        public static DataSet fnGetViewTravelRequest(long TravelRequestID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCompleteTravelRequest", oparam);
        }

        public static DataSet fnGetdiem(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_Getdiem", oparam);

        }

        public static DataSet fnGetDesign(long lDesignId, string IsActive = "0,1")
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@id", lDesignId);
            oparam[1] = new SqlParameter("@IsActive", IsActive);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetDesign", oparam);

        }
        public static DataSet fnGetLeave(long lLeaveId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Id", lLeaveId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeave", oparam);

        }

        public static DataSet fnGetFinYear(long ID, string IsActive = "0,1")
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@id", ID);
            oparam[1] = new SqlParameter("@IsActive", IsActive);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetFinYear", oparam);
        }
        public static DataSet fnGetMembershipPeriod(long id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMembershipPeriod", oparam);
        }

        public static DataSet fnGetCompanyUploadList(long id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCompanyUploadList", oparam);

        }

        public static DataSet fnGetVotingList(long QuestionID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@QuestionID", QuestionID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetVotingList", oparam);

        }

        public static DataSet fnGetQuestion(long lHolId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Id", lHolId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetQuestion", oparam);

        }
        public static DataSet fnGetHoliday(long lHolId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Id", lHolId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetHoliday", oparam);

        }
        public static DataSet fnGetCountry(string CountryId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", CountryId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCountry", oparam);
        }

        public static DataSet fnGetState(long StateId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", StateId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetState", oparam);
        }
        public static DataSet fnGetcity(long Cityid)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", Cityid);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCity", oparam);
        }
        public static DataSet fnGetTravelReq(long lTravelReqID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@id", lTravelReqID);
            oparam[1] = new SqlParameter("@userid", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelReq", oparam);

        }
        public static DataSet fnGetTravelReq_Det(long lTravelReqID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@travelReqId", lTravelReqID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelReq_Det", oparam);

        }
        public static DataSet fnGetProjectList_Travel(long TravelRequestID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetProjectList_Travel", oparam);
        }

        public static DataSet fnGetProjectDetail_Dropdown(long ProjectID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ProjectID", ProjectID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetProjectDetail_Dropdown", oparam);
        }
        public static DataSet fnGetLocation(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLocation", oparam);
        }
        public static DataSet fnGetAnnouncement(long lAnnounceId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", lAnnounceId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetAnnouncement", oparam);

        }

        public static DataSet fnGetsubgrant(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ID", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_Getsubgrant", oparam);
        }
        public static DataSet fnGetsubgrantdet(long ID, string Doctype, long SubgrantID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@Doctype", ClsCommon.EnsureString(Doctype));
            oparam[2] = new SqlParameter("@SubgrantID", SubgrantID);
            return clsDataBaseHelper.ExecuteDataSet("spu_Getsubgrantdet", oparam);
        }
        public static DataSet fnGetThematicArea(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ID", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetThematicArea", oparam);
        }



        public static long fnGetMaxID(string colomnname)
        {
            long retunrID = 0;
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@colomnname", colomnname);
            long.TryParse(clsDataBaseHelper.ExecuteDataSet("spu_GetMaxID", oparam).Tables[0].Rows[0]["RET_ID"].ToString(), out retunrID);
            return retunrID;

        }

        public static DataSet fnGetConsultant(long lConsulId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", lConsulId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetConsultant", oparam);

        }

        public static DataSet fnGetCompensatoryOff_Ess(string StartDate, string EndDate)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[1] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@StartDate", StartDate);
            oparam[3] = new SqlParameter("@EndDate", EndDate);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCompensatoryOff_Ess", oparam);

        }

        public static DataSet fnGetEmpAttachment_Ess(long EMPID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@EMPID", EMPID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmpAttachment_Ess", oparam);

        }
        public static DataSet fnGetMyTravelRequest_Dashboard(long Type)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@userid", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[1] = new SqlParameter("@Type", Type);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMyTravelRequest_Dashboard", oparam);

        }

        public static DataSet fnGetTravelRequestForApproval_Dashboard(long Type)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@Type", Type);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelRequestForApproval_Dashboard", oparam);

        }

        public static DataSet fnGetLeaveAvailedReport(string LeaveType)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@LeaveType", LeaveType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveAvailedReport", oparam);

        }
        public static DataSet ESSMenuDetails()
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@COMPANY_CODE", clsApplicationSetting.GetConfigValue("CompanyCode"));
            oparam[1] = new SqlParameter("@EMP_CODE", clsApplicationSetting.GetSessionValue("EMPCode"));
            return clsDataBaseHelper.ExecuteDataSet("MENULIST", oparam);

        }
        public static DataSet BindGridComputation(string sDocType)
        {
            SqlParameter[] oParam = new SqlParameter[3];
            oParam[0] = new SqlParameter("@COMPANY_CODE", clsApplicationSetting.GetConfigValue("CompanyCode"));
            oParam[1] = new SqlParameter("@EMP_ID", clsApplicationSetting.GetSessionValue("EMPID"));
            oParam[2] = new SqlParameter("@DOC_TYPE", sDocType);
            return clsDataBaseHelper.ExecuteDataSet("COMP", oParam);
        }
        public static DataSet fnRepAnualLeaveStmt(DateTime dtFDate, DateTime dtTDate)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@dtFDate", dtFDate);
            oparam[1] = new SqlParameter("@dtTDate", dtTDate);

            return clsDataBaseHelper.ExecuteDataSet("spu_RepAnualLeaveStmt", oparam);
        }

        public static DataSet fnGetWorkingDays(string fdate, string tdate, string ApplySandwichPolicy = "N")
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@fdate", fdate);
            oparam[2] = new SqlParameter("@tdate", tdate);
            oparam[3] = new SqlParameter("@ApplySandwichPolicy", ApplySandwichPolicy);

            return clsDataBaseHelper.ExecuteDataSet("spu_GetWorkingDays", oparam);
        }

        public static bool fnDelLeaveLog(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@USERID", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteDataSet("spu_DelLeaveLog", oparam);
            return true;
        }
        public static DataSet fnGetLeaveLogSenior(long lEmpId, int iApproved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@emp_Id", lEmpId);
            oparam[1] = new SqlParameter("@approved", iApproved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveLogSenior", oparam);
        }

        public static DataSet fnGetEmp(long lEmpId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", lEmpId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmpList", oparam);

        }
        public static DataSet fnGetEmpCompleteDetail(long lEmpId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", lEmpId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmpCompleteDetail", oparam);

        }

        public static DataSet fnGetLeavedet_Log(long Ileavelog_id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@leavelog_id", Ileavelog_id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeavedet_Log", oparam);

        }
        public static DataSet fnGetLeaveLog(long lEmpId, int iApproved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@empId", lEmpId);
            oparam[1] = new SqlParameter("@approved", iApproved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveLog", oparam);

        }
        public static DataSet fnGetLeaveLog_Dashboard(long lEmpId)
        {
            int AttachmentRequired = 0;
            if (!clsApplicationSetting.IsSessionExpired("MyLeaveAttachment"))
            {
                AttachmentRequired = 1;
            }
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@empId", lEmpId);
            oparam[1] = new SqlParameter("@AttachmentRequired", AttachmentRequired);

            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveLog_Dashboard", oparam);

        }
        public static DataSet fnGetLeaveLogSenior_Dashboard(long lEmpId)
        {
            int AttachmentRequired = 0;
            if (!clsApplicationSetting.IsSessionExpired("MyTeamLeaveAttachment"))
            {
                AttachmentRequired = 1;
            }
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@emp_Id", lEmpId);
            oparam[1] = new SqlParameter("@AttachmentRequired", AttachmentRequired);

            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveLogSenior_Dashboard", oparam);

        }
        public static DataSet fnGetLeaveStatus(long lEmpId, int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@empId", lEmpId);
            oparam[1] = new SqlParameter("@Month", iMonth);
            oparam[2] = new SqlParameter("@Year", iYear);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveStatus", oparam);
        }

        public static DataSet fnGetConsolidateActivityLog(string EMPID, int Month, int Year, string EmpType)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@EMPID", EMPID);
            oparam[1] = new SqlParameter("@Month", Month);
            oparam[2] = new SqlParameter("@Year", Year);
            oparam[3] = new SqlParameter("@Emptype", EmpType);
            return clsDataBaseHelper.ExecuteDataSet("Spu_GetConsolidateActivityLog", oparam);

        }
        public static DataSet fnGetProcessTimeSheetLog(string SelectedDate, string TempEMPID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@SelectedDate", SelectedDate);
            oparam[1] = new SqlParameter("@TempEMPID", TempEMPID);
            return clsDataBaseHelper.ExecuteDataSet("Spu_GetProcessTimeSheetLog", oparam);

        }

        public static DataSet fnGetConsolidateActivityStatus(DateTime StartDate, DateTime EndDate, string EmpType)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@StartDate", StartDate);
            oparam[1] = new SqlParameter("@EndDate", EndDate);
            oparam[2] = new SqlParameter("@Emptype", EmpType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetConsolidateActivityStatus", oparam);
        }

        public static DataSet fnGetBoardMeeting(string Approved)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Approved", Approved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetBoardMeeting", oparam);
        }
        public static DataSet fnGetSingleMemberDetails(string memshipno)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@memshipno", memshipno);
            return clsDataBaseHelper.ExecuteDataSet("spu_getSingleMemberDetails", oparam);
        }
        public static DataSet fnGetMembershipRegistered(string DateAsOn, string Type)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@DateAsOn", DateAsOn);
            oparam[1] = new SqlParameter("@Type", Type);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMembershipRegistered", oparam);
        }

        public static DataSet fnGetBoardMembershipRegistration(string approved)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Approve", approved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetBoardMembershipRegistration", oparam);
        }
        public static DataSet fnGetLeaveStatus(long lEmpId, string iMonth, string iYear)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@empId", lEmpId);
            oparam[1] = new SqlParameter("@Month", iMonth);
            oparam[2] = new SqlParameter("@Year", iYear);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveStatus", oparam);
        }

        public static DataSet fnGetCountry(long CountryId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", CountryId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCountry", oparam);
        }

        public static DataSet fnGetPushMailItems_Attachment(long MailItemID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@MailItemID", MailItemID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPushMailItems_Attachment", oparam);
        }


        public static DataSet fnGetPushMailItems_Pending()
        {
            return clsDataBaseHelper.ExecuteDataSet("exec spu_GetPushMailItems_Pending");
        }
        public static DataSet fnGetPushNotification(string ListType)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[1] = new SqlParameter("@ListType", ClsCommon.EnsureString(ListType));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPushNotification", oparam);

        }

        public static DataSet fnGetConfigSetting(long ConfigID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ConfigID", ConfigID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetConfigSetting", oparam);
        }
        public static DataSet fnGetLogin(string UserID, string Password, string SessionID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@UserID", UserID);
            oparam[1] = new SqlParameter("@Password", Password);
            oparam[2] = new SqlParameter("@SessionID", SessionID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLogin", oparam);
        }


        public static DataSet fnGetErrorLog()
        {
            return clsDataBaseHelper.ExecuteDataSet("exec spu_GetErrorLog");

        }
        public static DataSet fnGetEmailTemplate(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmailTemplate", oparam);
        }
        public static DataSet fnGetRoles(long lRoleid)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", lRoleid);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetRoles", oparam);
        }
        public static DataSet fnGetLoginUsers(long UserID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", UserID);
            return clsDataBaseHelper.ExecuteDataSet("spu_Get_User", oparam);
        }
        public static DataSet fnGetProjectEMP_SelfMapping(string DocType, string searchText)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@empid", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@DocType", ClsCommon.EnsureString(DocType));
            oparam[2] = new SqlParameter("@searchText", ClsCommon.EnsureString(searchText));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetProjectEMP_SelfMapping", oparam);
        }

        public static DataSet fnGetDailyLog(int month, int year)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@emp_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@month", month);
            oparam[2] = new SqlParameter("@year", year);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetDailyLog", oparam);
        }

        public static DataSet fnGetLeaveLogDetails(int month, int year)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@emp_Id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@month", month);
            oparam[2] = new SqlParameter("@year", year);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveLogDetails", oparam);

        }
        public static DataSet fnGetDailyActiveLog(int month, int year)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@emp_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@month", month);
            oparam[2] = new SqlParameter("@year", year);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetDailyActiveLog", oparam);
        }
        public static DataSet fnGetPendingActiveApprove(int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@emp_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@Month", iMonth);
            oparam[2] = new SqlParameter("@year", iYear);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPendingActiveApprove", oparam);

        }
        public static DataSet fnGetApproveActive(int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@emp_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@month", iMonth);
            oparam[2] = new SqlParameter("@year", iYear);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetApproveActive", oparam);

        }
        public static DataSet fnGetResubmitActiveLog(int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@emp_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@Month", iMonth);
            oparam[2] = new SqlParameter("@year", iYear);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetResubmitActive", oparam);

        }

        public static DataSet fnGetActivityLogApproval(string Approved, int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@emp_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@Month", iMonth);
            oparam[3] = new SqlParameter("@year", iYear);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetActivityLogApproval", oparam);

        }

        public static DataSet fnGetRequestForCompOff(int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@Month", iMonth);
            oparam[2] = new SqlParameter("@year", iYear);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetRequestForCompOff", oparam);

        }

        public static DataSet fnGetCompensatoryOff(long EMPID, int iMonth, int iYear, string approved)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@hod_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@emp_id", EMPID);
            oparam[2] = new SqlParameter("@Month", iMonth);
            oparam[3] = new SqlParameter("@year", iYear);
            oparam[4] = new SqlParameter("@approved", approved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCompensatoryOff", oparam);

        }
        public static DataSet fnGetOvertime(long lEmpID, int iMonth, int iYear, string lApproved)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@hod_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@emp_id", lEmpID);
            oparam[2] = new SqlParameter("@month", iMonth);
            oparam[3] = new SqlParameter("@year", iYear);
            oparam[4] = new SqlParameter("@approved", lApproved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetOvertime", oparam);

        }


        public static DataSet fnGetActiveLog(long EMPID, int Month, int Year)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@emp_id", EMPID);
            oparam[1] = new SqlParameter("@Month", Month);
            oparam[2] = new SqlParameter("@Year", Year);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetActiveLog", oparam);

        }
        public static long fnGetLeaveBalanceByLeaveID(long lEmpID, string lLeaveID, int iMonth, int iYear)
        {
            long Balance = 0;
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@empId", lEmpID);
            oparam[1] = new SqlParameter("@LeaveID", lLeaveID);
            oparam[2] = new SqlParameter("@Month", iMonth);
            oparam[3] = new SqlParameter("@Year", iYear);

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveBalance", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Balance = Convert.ToInt64(ds.Tables[0].Rows[0]["Balance"]);

            }
            return Balance;
        }

        public static DataSet fnLeaveAttachmentRequiredCount()
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@emp_id", clsApplicationSetting.GetSessionValue("EMPID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveAttachmentRequiredCount", oparam);
        }


        public static string fnIsLeaveRequestForEDApproval(long leavelog_id)
        {
            string retun = "";
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@leavelog_id", leavelog_id);
            oparam[1] = new SqlParameter("@LoginEMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_IsLeaveRequestForEDApproval", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                retun = ds.Tables[0].Rows[0]["IsValidForEDApproval"].ToString();

            }
            return retun;
        }
        public static DataSet fnGetActivityLog(long EMPID, int Month, int Year, string type = "")
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@emp_id", EMPID);
            oparam[1] = new SqlParameter("@Month", Month);
            oparam[2] = new SqlParameter("@Year", Year);
            oparam[3] = new SqlParameter("@type", type);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetActivityLog", oparam);
        }

        public static DataSet fnGetPendingLeaveLog_Month(long EMPID, int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@emp_id", EMPID);
            oparam[1] = new SqlParameter("@Month", iMonth);
            oparam[2] = new SqlParameter("@year", iYear);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPendingLeaveLog_Month", oparam);

        }

        public static DataSet fnGetLeaveSummary(long EMPID, int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@empid", EMPID);
            oparam[1] = new SqlParameter("@Month", iMonth);
            oparam[2] = new SqlParameter("@year", iYear);
            return clsDataBaseHelper.ExecuteDataSet("Spu_GetLeaveSummary", oparam);
        }
        public static DataSet fnGetLeaveEntryNonMITR(long Hodid, string LeaveMonth, string Empids, string Leaveids)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Hodid", Hodid);
            oparam[1] = new SqlParameter("@Date", LeaveMonth);
            oparam[2] = new SqlParameter("@Empids", ClsCommon.EnsureString(Empids));
            oparam[3] = new SqlParameter("@Leaveids", ClsCommon.EnsureString(Leaveids));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveEntryNonMITR", oparam);
        }
        public static DataSet fnGetDailyLogNonMITR(int month, int year, string Empids)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Hodid", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@month", month);
            oparam[2] = new SqlParameter("@year", year);
            oparam[3] = new SqlParameter("@Empids", ClsCommon.EnsureString(Empids));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetDailyLogNonMitr", oparam);
        }
        public static DataSet fnGetActivityLogNonMITR(int Month, int Year, string Empids, string type = "")
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@Hodid", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@Month", Month);
            oparam[2] = new SqlParameter("@Year", Year);
            oparam[3] = new SqlParameter("@Empids", ClsCommon.EnsureString(Empids));
            oparam[4] = new SqlParameter("@type", type);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetActivityLogNonMitr", oparam);
        }
        public static DataSet fnGetHolidayLeavebyEmpids(string Empids, string LeaveMonth)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@Hodid", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@Empids", ClsCommon.EnsureString(Empids));
            oparam[2] = new SqlParameter("@HolidayMonth", LeaveMonth);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetHolidayLeavesEmpids", oparam);
        }
        public static DataSet fnGetRequestForCompOffNonMITR(int iMonth, int iYear, string Empids)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Hodid", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@Month", iMonth);
            oparam[2] = new SqlParameter("@year", iYear);
            oparam[3] = new SqlParameter("@Empids", ClsCommon.EnsureString(Empids));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetRequestForCompOffNonMitr", oparam);

        }
        public static DataSet fnGetActivityLogApprovalNonMITR(string Approved, int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@emp_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@Month", iMonth);
            oparam[3] = new SqlParameter("@year", iYear);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetActivityLogApprovalNonMITR", oparam);

        }
        public static DataSet fnGetCompensatoryOffNonMitr(string EMPIDS, int iMonth, int iYear, string approved)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@empids", EMPIDS);
            oparam[1] = new SqlParameter("@Month", iMonth);
            oparam[2] = new SqlParameter("@year", iYear);
            oparam[3] = new SqlParameter("@approved", approved);

            return clsDataBaseHelper.ExecuteDataSet("spu_GetCompensatoryOffNonMitr", oparam);

        }
        public static DataSet FnGetProjectWiseSalaryRpt(string EMPIDS, DateTime Dt, string Doctype, string EmpType)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Date", Dt);
            oparam[1] = new SqlParameter("@Empid", EMPIDS);
            oparam[2] = new SqlParameter("@Doctype", Doctype);
            oparam[3] = new SqlParameter("@Emptype", EmpType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetProjectWiseSalaryAllocationRPT", oparam);

        }
        public static DataSet fnGetTravelBudgetMaster(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Finyearid", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelBudget", oparam);
        }
        public static DataSet fnGetTravelBudgetMasterYear(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Finyearid", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelBudgetList", oparam);
        }
        public static DataSet fnTrainingWorkTypeList(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTrainingWorkshopSeminarList", oparam);
        }
        public static DataSet fnGetInflationRateMaster(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetInflationRateList", oparam);
        }
        public static DataSet fnGetInflationRatewiseListr(long ID, long YearId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@id", ID);
            oparam[1] = new SqlParameter("@Finyearid", YearId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetInflationRate", oparam);
        }
        public static DataSet fnGetIndirectCostRateList(long ID, long YearId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@id", ID);
            oparam[1] = new SqlParameter("@Finyearid", YearId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetIndirectCostRate", oparam);
        }
        public static DataSet fnGetFringeBenefitRateList(long ID, long YearId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@id", ID);
            oparam[1] = new SqlParameter("@Finyearid", YearId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetFringeBenefit_Rate", oparam);
        }
        public static DataSet fnGetIndirectRateMasterList(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetIndirectCostRateList", oparam);
        }
        public static DataSet fnGetFringeBenefitRateMasterList(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetFringeBenefitRateList", oparam);
        }
        public static DataSet fnGetBudgetEmployeeList()
        {
            SqlParameter[] oparam = new SqlParameter[0];
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmployeeBudgetSetting", oparam);
        }
        public static DataSet fnGetBudgetMainList(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmployeeBudgetSettingList", oparam);
        }
        public static DataSet fnGetTravelEmployeeBankDeatils(long EmpId, string doctype)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@EMPID", EmpId);
            oparam[1] = new SqlParameter("@Doctype", doctype);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEMP_Account", oparam);
        }
        public static DataSet fnGetRFPBudgetProjectList(long ProjectId)
        {
            SqlParameter[] oParam = new SqlParameter[2];

            oParam[0] = new SqlParameter("@ProjectId", ProjectId);
            if (clsApplicationSetting.GetSessionValue("RolesName").ToString() == "C3 Admin")
            {
                oParam[1] = new SqlParameter("@EmpId", "0");
            }
            else
            {
                oParam[1] = new SqlParameter("@EmpId", clsApplicationSetting.GetSessionValue("EMPID"));
            }

            return clsDataBaseHelper.ExecuteDataSet("spu_GetRFPBudgetProjectList", oParam);
        }
        public static DataSet GetEss(string sDocType)
        {
            SqlParameter[] oParam = new SqlParameter[3];
            oParam[0] = new SqlParameter("@COMPANY_CODE", clsApplicationSetting.GetConfigValue("CompanyCode"));
            oParam[1] = new SqlParameter("@EMP_ID", clsApplicationSetting.GetSessionValue("EMPID"));
            oParam[2] = new SqlParameter("@DOC_TYPE", sDocType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetESS", oParam);
        }
        public static DataSet GetSalarySlip(DateTime dt, string Emptype, string Doctype)
        {
            SqlParameter[] oParam = new SqlParameter[5];
            oParam[0] = new SqlParameter("@Month", dt.Month);
            oParam[1] = new SqlParameter("@Year", dt.Year);
            oParam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oParam[3] = new SqlParameter("@EmpType", Emptype);
            oParam[4] = new SqlParameter("@Doctype", Doctype);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSalarySlipRpt", oParam);
        }
        public static DataSet GetFullAndFinalRpt(long Empid)
        {
            SqlParameter[] oParam = new SqlParameter[2];
            oParam[0] = new SqlParameter("@Empid", Empid);
            oParam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetF&FRpt", oParam);
        }
        //public static PostResponse fnGetValidateToken(GetValidateToken Modal)
        //{
        //    PostResponse result = new PostResponse();
        //    using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
        //    {
        //        try
        //        {
        //            con.Open();
        //            using (SqlCommand command = new SqlCommand("spu_GetValidateToken", con))
        //            {
        //                SqlDataAdapter da = new SqlDataAdapter();
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@Token", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Token);
        //                command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
        //                command.CommandTimeout = 0;
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        result.ID = Convert.ToInt64(reader["RET_ID"]);
        //                        result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
        //                        result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
        //                        if (result.StatusCode > 0)
        //                        {
        //                            result.Status = true;
        //                        }
        //                    }
        //                }

        //            }
        //            con.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            con.Close();
        //            ClsCommon.LogError("Error during fnGetCheckRecordExist. The query was executed :", ex.ToString(), "spu_GetCheckRecordExist", "Common_SPU", "Common_SPU", "");
        //            result.StatusCode = -1;
        //            result.SuccessMessage = ex.Message.ToString();
        //        }
        //    }
        //    return result;

        //}
        public static MasterAll.List GetMasterAllData(long ID, string TableName)
        {
            MasterAll.List result = new MasterAll.List();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetMasterAll", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.BigInt).Value = ID;
                        command.Parameters.Add("@tablename", SqlDbType.VarChar).Value = TableName;
                        command.Parameters.Add("@groupid", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = "";
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["id"]);
                                result.field_name = reader["field_name"].ToString();
                                result.field_value = reader["field_value"].ToString();
                                result.group_id = Convert.ToInt64(reader["group_id"]);
                                result.group_Name = reader["parentfieldname"].ToString();
                                result.group_Value = reader["parentfieldvalue"].ToString();

                            }
                        }


                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    ClsCommon.LogError("Error during GetMasterAllList. The query was executed :", ex.ToString(), "spu_GetMasterAll", "MasterModal", "MasterModal", "");
                }
            }

            return result;
        }
        public static DataSet fnGetMasterAll(int ID, string tablename, int Groupid, string IsActive)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@tablename", tablename);
            oparam[2] = new SqlParameter("@groupid", Groupid);
            oparam[3] = new SqlParameter("@IsActive", IsActive);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMasterAll", oparam);

        }
        public static DataSet fnGetReport_FR(string StartDate, string Enddate, long Id, string Doctype)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@StartDate", ClsCommon.EnsureString(StartDate));
            oparam[1] = new SqlParameter("@EndDate", ClsCommon.EnsureString(Enddate));
            oparam[2] = new SqlParameter("@Doctype", Doctype);
            oparam[3] = new SqlParameter("@id", Id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetFR_AllReports", oparam);

        }
        public static DataSet fnGetFRProspectListExport(long Id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", Id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetFR_ProspectListExport", oparam);

        }
        public static DataSet fnGetReport_ActivityLog(string StartDate, string Enddate, string UserType, long Id, string Doctype)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@StartDate", ClsCommon.EnsureString(StartDate));
            oparam[1] = new SqlParameter("@EndDate", ClsCommon.EnsureString(Enddate));
            oparam[2] = new SqlParameter("@Type", UserType);
            oparam[3] = new SqlParameter("@Doctype", Doctype);
            oparam[4] = new SqlParameter("@id", Id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetActivity_AllReports", oparam);
        }
        //public static PostResponse fnGetUpdateColumnResponse(GetUpdateColumnResponse Modal)
        //{
        //    PostResponse result = new PostResponse();

        //    using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
        //    {
        //        try
        //        {
        //            con.Open();
        //            using (SqlCommand command = new SqlCommand("spu_SetUpdateColumn_Common", con))
        //            {
        //                SqlDataAdapter da = new SqlDataAdapter();
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID;
        //                command.Parameters.Add("@Value", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Value);
        //                command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
        //                command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
        //                command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
        //                command.CommandTimeout = 0;
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        result.ID = Convert.ToInt64(reader["RET_ID"]);
        //                        result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
        //                        result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
        //                        if (result.StatusCode > 0)
        //                        {
        //                            result.Status = true;
        //                        }
        //                    }
        //                }

        //            }
        //            con.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            con.Close();
        //            ClsCommon.LogError("Error during fnGetUpdateColumnResponse. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common", "BoardModal", "BoardModal", "");


        //            result.StatusCode = -1;
        //            result.SuccessMessage = ex.Message.ToString();
        //        }
        //    }
        //    return result;

        //}

        //public static PostResponse fnGetCheckRecordExist(GetRecordExitsResponse Modal)
        //{
        //    PostResponse result = new PostResponse();
        //    using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
        //    {
        //        try
        //        {
        //            con.Open();
        //            using (SqlCommand command = new SqlCommand("spu_GetCheckRecordExist", con))
        //            {
        //                SqlDataAdapter da = new SqlDataAdapter();
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID;
        //                command.Parameters.Add("@Value", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Value);
        //                command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
        //                command.CommandTimeout = 0;
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        result.ID = Convert.ToInt64(reader["RET_ID"]);
        //                        result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
        //                        result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
        //                        if (result.StatusCode > 0)
        //                        {
        //                            result.Status = true;
        //                        }
        //                    }
        //                }

        //            }
        //            con.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            con.Close();
        //            ClsCommon.LogError("Error during fnGetCheckRecordExist. The query was executed :", ex.ToString(), "spu_GetCheckRecordExist", "Common_SPU", "Common_SPU", "");
        //            result.StatusCode = -1;
        //            result.SuccessMessage = ex.Message.ToString();
        //        }
        //    }
        //    return result;

        //}
        //public static PostResponse fnGetGenerateValues(GetGenerateValues Modal)
        //{
        //    PostResponse result = new PostResponse();
        //    using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
        //    {
        //        try
        //        {
        //            con.Open();
        //            using (SqlCommand command = new SqlCommand("spu_GetGenerateValues", con))
        //            {
        //                SqlDataAdapter da = new SqlDataAdapter();
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@AllIDs", SqlDbType.VarChar).Value = Modal.AllIDs ?? "";
        //                command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = Modal.Doctype ?? "";
        //                command.Parameters.Add("@LoginID", SqlDbType.Int).Value = Modal.LoginID;
        //                command.CommandTimeout = 0;
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        result.ID = Convert.ToInt64(reader["RET_ID"]);
        //                        result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
        //                        result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
        //                        if (result.StatusCode > 0)
        //                        {
        //                            result.Status = true;
        //                        }
        //                    }
        //                }

        //            }
        //            con.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            con.Close();
        //            ClsCommon.LogError("Error during fnGetGenerateValues. The query was executed :", ex.ToString(), "spu_GetGenerateValues", "Common_SPU", "Common_SPU", "");
        //            result.StatusCode = -1;
        //            result.SuccessMessage = ex.Message.ToString();
        //        }
        //    }
        //    return result;

        //}
        public static DataSet fnGetSublineActivityList()
        {
            SqlParameter[] oParam = new SqlParameter[1];
            if (clsApplicationSetting.GetSessionValue("RolesName").ToString() == "C3 Admin")
            {
                oParam[0] = new SqlParameter("@EmpId", "0");
            }
            else
            {
                oParam[0] = new SqlParameter("@EmpId", clsApplicationSetting.GetSessionValue("EMPID"));
            }
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSublineActivityList", oParam);
        }
        public static DataSet fnGetSublineActivitydraftList()
        {
            SqlParameter[] oParam = new SqlParameter[1];
            if (clsApplicationSetting.GetSessionValue("RolesName").ToString() == "C3 Admin")
            {
                oParam[0] = new SqlParameter("@EmpId", "0");
            }
            else
            {
                oParam[0] = new SqlParameter("@EmpId", clsApplicationSetting.GetSessionValue("EMPID"));
            }

            return clsDataBaseHelper.ExecuteDataSet("spu_GetSublineActivityDraftList", oParam);
        }
        public static DataSet fnGetStandAloneList()
        {
            return clsDataBaseHelper.ExecuteDataSet("spu_GetStandaloneBudget");
        }
        public static DataSet fnGetTravelLocationList()
        {
            return clsDataBaseHelper.ExecuteDataSet("spu_getTravelDeskLocation");
        }
        public static DataSet GetFNFDetail(DateTime dt, string Emptype, string Doctype)
        {
            SqlParameter[] oParam = new SqlParameter[5];
            oParam[0] = new SqlParameter("@Month", dt.Month);
            oParam[1] = new SqlParameter("@Year", dt.Year);
            oParam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oParam[3] = new SqlParameter("@EmpType", Emptype);
            oParam[4] = new SqlParameter("@Doctype", Doctype);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetF&FDetail", oParam);
        }
        public static DataSet FnGetPFRpt(DateTime dt, string Emptype, string Doctype)
        {
            SqlParameter[] oParam = new SqlParameter[5];
            oParam[0] = new SqlParameter("@Month", dt.Month);
            oParam[1] = new SqlParameter("@Year", dt.Year);
            oParam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oParam[3] = new SqlParameter("@EmpType", Emptype);
            oParam[4] = new SqlParameter("@Doctype", Doctype);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetReportPF", oParam);

        }
        public static DataSet FnGetPTAXRpt(DateTime dt, string Emptype, string Doctype)
        {
            SqlParameter[] oParam = new SqlParameter[5];
            oParam[0] = new SqlParameter("@Month", dt.Month);
            oParam[1] = new SqlParameter("@Year", dt.Year);
            oParam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oParam[3] = new SqlParameter("@EmpType", Emptype);
            oParam[4] = new SqlParameter("@Doctype", Doctype);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetReportPTAX", oParam);

        }
        public static DataSet fnGetTravelRequestAdvanceFinance()
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetFinancePayment_Staff", oparam);

        }
        public static DataSet fnGetViewTravelAdvancedRequest(long TravelRequestID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCompleteAdvancedTravelRequest", oparam);
        }
        public static DataSet fnGetTravelEmployeeList()
        {
            SqlParameter[] oparam = new SqlParameter[0];
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmployeeTravellist", oparam);
        }
        public static DataSet fnGetTravellocationlist(long EmployeeId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@empid ", EmployeeId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelDeskLocationMap", oparam);
        }

        //fnGetCategory
        #endregion

        
        public static long fnSetAttachments(long lAttachment_Id, string sFilename, string sContenttype, string sDescrip, string TableID = "0", string TableName = "")
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long fnSetAttachments = 0;
            long tableid = Convert.ToInt64(TableID);
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@id", lAttachment_Id);
            oparam[1] = new SqlParameter("@filename", ClsCommon.EnsureString(sFilename));
            oparam[2] = new SqlParameter("@content_type", ClsCommon.EnsureString(sContenttype));
            oparam[3] = new SqlParameter("@Descrip", ClsCommon.EnsureString(sDescrip));
            oparam[4] = new SqlParameter("@table_id", tableid);
            oparam[5] = new SqlParameter("@TableName", ClsCommon.EnsureString(TableName));
            oparam[6] = new SqlParameter("@createdby", LoginID);
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_Setattachment", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetAttachments = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetAttachments = 0;
            return fnSetAttachments;

        }
        public static long fnSetAttachmentsforMobile(long lAttachment_Id, string sFilename, string sContenttype, long UserId, string sDescrip,  string TableID = "0", string TableName = "")
        {
           // long LoginID = 0;
          //  long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long fnSetAttachments = 0;
            long tableid = Convert.ToInt64(TableID);
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@id", lAttachment_Id);
            oparam[1] = new SqlParameter("@filename", ClsCommon.EnsureString(sFilename));
            oparam[2] = new SqlParameter("@content_type", ClsCommon.EnsureString(sContenttype));
            oparam[3] = new SqlParameter("@Descrip", ClsCommon.EnsureString(sDescrip));
            oparam[4] = new SqlParameter("@table_id", tableid);
            oparam[5] = new SqlParameter("@TableName", ClsCommon.EnsureString(TableName));
            oparam[6] = new SqlParameter("@createdby", UserId);
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_Setattachment", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetAttachments = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetAttachments = 0;
            return fnSetAttachments;

        }


        public static long fnSetErrorLog(string ErrDescription, string SystemException, string ActiveFunction, string ActiveForm, string ActiveModule)
        {
            long retID = 0;
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@ErrDescription", ClsCommon.EnsureString(ErrDescription));
            oparam[1] = new SqlParameter("@SystemException", ClsCommon.EnsureString(SystemException));
            oparam[2] = new SqlParameter("@ActiveFunction", ClsCommon.EnsureString(ActiveFunction));
            oparam[3] = new SqlParameter("@ActiveForm", ClsCommon.EnsureString(ActiveForm));
            oparam[4] = new SqlParameter("@ActiveModule", ClsCommon.EnsureString(ActiveModule));
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetErrorLog", oparam);
            if (ds.Tables[0].Rows.Count > 0)
                retID = Convert.ToInt32(ds.Tables[0].Rows[0]["RET_ID"]);
            else
                retID = 0;
            return retID;


        }






        #region "Grievance"

        public static DataSet fnGetSubcategoryGRAssigneeDetails()
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSubcategoryGRAssigneeDetails", oparam);
        }
        public static DataSet fnGetSubCategoryList(long lSubcategoryId, int isdeleted)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@id", lSubcategoryId);
            oparam[1] = new SqlParameter("@isdeleted", isdeleted);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSubcategoryGR", oparam);
        }
        public static DataSet fnGetExternalMemberList(long lExternalId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", lExternalId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetExternalMemberGR", oparam);
        }
        public static DataSet fnGetSubCategoryAssigneeList(long id, long Categoryid, long SubCategoryid)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@ID", id);
            oparam[1] = new SqlParameter("@Categoryid", Categoryid);
            oparam[2] = new SqlParameter("@SubCategoryid", SubCategoryid);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSubCategoryAssigneeListGR", oparam);
        }
        public static DataSet fnGetSubCategorySLAPolicyList(long id, long SubCategoryid)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@ID", id);
            oparam[1] = new SqlParameter("@SubCategoryid", SubCategoryid);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetSubCategorySLAListGR", oparam);
        }
        public static DataSet fnGetUserGrievanceList(UserGrievance userGrievance)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@ID", userGrievance.ID);
            oparam[1] = new SqlParameter("@catid", userGrievance.Categoryid);
            oparam[2] = new SqlParameter("@subcatid", userGrievance.Subcatid);
            oparam[3] = new SqlParameter("@locationid", userGrievance.Locationid);
            oparam[4] = new SqlParameter("@loginid", userGrievance.Createdby);
            oparam[5] = new SqlParameter("@loginpage", userGrievance.LoginPage);

            return clsDataBaseHelper.ExecuteDataSet("spu_GetUserGrievanceListGR", oparam);
        }
        public static DataSet fnGetUserGrievanceAccidentList(long id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ID", id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetUserGrievanceAccidentListGR", oparam);
        }
        public static DataSet fnCreateMail_Grievance(long Id, string Action)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@id", Id);
                oparam[1] = new SqlParameter("@RequestStatus", Action);
                oparam[2] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
                ds = clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_Grievance", oparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public static DataSet fnCreateMail_GrievanceForMobile(long Id, string Action,long UserId)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@id", Id);
                oparam[1] = new SqlParameter("@RequestStatus", Action);
                oparam[2] = new SqlParameter("@UserId", UserId);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_Grievance", oparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public static DataSet fnCreateMail_Grievance_Action(long Id, string Action, string empID,string TOIds)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] oparam = new SqlParameter[5];
                oparam[0] = new SqlParameter("@id", Id);
                oparam[1] = new SqlParameter("@Action", Action);
                oparam[2] = new SqlParameter("@empID", empID);
                oparam[3] = new SqlParameter("@loginempID", clsApplicationSetting.GetSessionValue("EMPID"));
                oparam[4] = new SqlParameter("@TOIds", TOIds);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_Grievance_Note", oparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public static DataSet fnCreateMail_Grievance_ActionForMobile(long Id, string Action, string empID, long emp,string TOIds)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] oparam = new SqlParameter[5];
                oparam[0] = new SqlParameter("@id", Id);
                oparam[1] = new SqlParameter("@Action", Action);
                oparam[2] = new SqlParameter("@empID", empID);
                oparam[3] = new SqlParameter("@loginempID", emp);
                oparam[4] = new SqlParameter("@TOIds", TOIds);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_Grievance_Note", oparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        #region "Salary module"
        public static DataSet GetBonusPaymentList(long finyearId, string Component,string PaidDate)
       {
            DataSet ds = null;
            try
            {
                SqlParameter[] oparam = new SqlParameter[4];
                oparam[0] = new SqlParameter("@fyId", finyearId);
                oparam[1] = new SqlParameter("@Component", Component);
                oparam[2] = new SqlParameter("@PaidDate", PaidDate);
                oparam[3] = new SqlParameter("@empID", clsApplicationSetting.GetSessionValue("EmpID"));
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetBonusPaymentList", oparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public static DataSet GetOtherBenefitPaymentList(long finyearId, long empID, string PaidDate)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] oparam = new SqlParameter[4];
                oparam[0] = new SqlParameter("@fyId", finyearId);
                oparam[1] = new SqlParameter("@empID", empID);
                oparam[2] = new SqlParameter("@PaidDate", PaidDate);
                oparam[3] = new SqlParameter("@userID", clsApplicationSetting.GetSessionValue("UserID"));
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetOtherBenefitPaymentList", oparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public static DataSet GetOtherBenefitPaymentComponentList(long finyearId, long Componentid, string PaidDate)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] oparam = new SqlParameter[4];
                oparam[0] = new SqlParameter("@fyId", finyearId);
                oparam[1] = new SqlParameter("@empID", Componentid);
                oparam[2] = new SqlParameter("@PaidDate", PaidDate);
                oparam[3] = new SqlParameter("@userID", clsApplicationSetting.GetSessionValue("UserID"));
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetOtherBenefitPaymentComponent", oparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public static DataSet GetSpecialAllowanceList(long finyearId, string Component, string PaidDate)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] oparam = new SqlParameter[4];
                oparam[0] = new SqlParameter("@fyId", finyearId);
                oparam[1] = new SqlParameter("@Component", Component);
                oparam[2] = new SqlParameter("@PaidDate", PaidDate);
                oparam[3] = new SqlParameter("@empID", clsApplicationSetting.GetSessionValue("EmpID"));
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetSpecialAllowanceList", oparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }




        public static DataSet fnGetProjectEMP_SelfMappingBonus()
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@empid", clsApplicationSetting.GetSessionValue("EMPID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetProjectEMPBonus", oparam);
        }

        #endregion

    }
}
