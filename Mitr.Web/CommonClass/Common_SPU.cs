using Dapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using static Mitr.Models.MPR;
using static Mitr.Models.MPRReports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Mitr.CommonClass
{
    public class Common_SPU
    {


        #region GetStoreProcedure
        public static DataSet fnGetLeaveEmployeeList(string EMPType)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@EMPType", EMPType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmployeeLeaveReport", oparam);
        }

        public static DataSet fnGetEmployeesYearlyLeaveReport(long EmpId, string EMPType, long FINID, long LeaveId)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@EMPID", EmpId);
            oparam[1] = new SqlParameter("@EMPType", EMPType);
            oparam[2] = new SqlParameter("@FinID", FINID);
            oparam[3] = new SqlParameter("@LeaveId", LeaveId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLeaveReportYearly", oparam);
        }
        public static DataSet fnGetEmployeesFAndFData(long EmpId, long FYid)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@EMPID", EmpId);
            oparam[1] = new SqlParameter("@FYid", FYid);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmploymentHistoryStructureListFinYearly", oparam);
        }

        public static DataSet fnGetEmployeesDeclartionData(long FID, string EmpType)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@FYID", FID);
            oparam[1] = new SqlParameter("@EmpType", EmpType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetDeclartionEmpReport", oparam);
        }
        public static DataSet fnGetEmployeesData()
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmployeesData", oparam);
        }

        public static DataSet fnGetREC_TalentPoolList(long REC_ReqID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_TalentPoolList", oparam);
        }

        public static DataSet fnGetREC_EApplication_InterviewP(long REC_AppID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EApplication_InterviewP", oparam);
        }

        public static DataSet fnGetREC_EApplication_InterviewAttachementlist(long REC_InterviewSetID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@REC_InterviewSetID", REC_InterviewSetID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EApplication_InterviewAttachement", oparam);
        }
        public static DataSet fnGetRECRoundName(long REC_Id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@REC_ReqID ", REC_Id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetRECRound", oparam);
        }

        public static DataSet fnGetREC_EInterviewCandidate(long REC_ReqID, long REC_InterviewSetID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@REC_InterviewSetID", REC_InterviewSetID);
            oparam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EInterviewCandidateDirect", oparam);
        }
        public static DataSet fnGetREC_ESelectionProcess_ViewList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ESelectionProcess_ViewList", oparam);
        }

        public static DataSet fnGetREC_ESelectionProcessList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ESelectionProcessListDirect", oparam);
        }


        public static DataSet fnGetREC_EInterviewSetting(long REC_ReqID, long REC_InterviewSetID, string DocType)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@REC_InterviewSetID", REC_InterviewSetID);
            oparam[2] = new SqlParameter("@DocType", DocType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EInterviewSetting", oparam);
        }

        public static DataSet fnGetREC_EInterviewSettingList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EInterviewSettingListDirect", oparam);
        }

        public static DataSet fnGetREC_EConfirmedCV(long REC_ReqID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EConfirmedCV", oparam);
        }

        public static DataSet fnGetREC_EConfirmedCVList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EConfirmedCVListDirect", oparam);
        }

        public static DataSet fnGetREC_ApplicationView(long REC_AppID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[1] = new SqlParameter("@REC_ReqID", 0);
            oparam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ApplicationView", oparam);
        }



        public static DataSet fnGetREC_EShortListedApplicationList(long REC_ReqID, long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@Approved", Approved);
            oparam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EShortListedApplicationListDirect", oparam);
        }


        public static DataSet fnGetREC_EShortlistedCVList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EShortlistedCVListDirect", oparam);
        }

        public static DataSet fnGetREC_EScreeningApplicationList(long REC_ReqID, string Applied, string Worked, long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@Applied", ClsCommon.EnsureString(Applied));
            oparam[2] = new SqlParameter("@Worked", ClsCommon.EnsureString(Worked));
            oparam[3] = new SqlParameter("@Approved", Approved);
            oparam[4] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EScreeningApplicationListDirect", oparam);
        }

        public static DataSet fnGetREC_EVacancyResponseList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EVacancyResponseList", oparam);
        }

        public static DataSet fnGetREC_ApplyJob(string Code)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Code", ClsCommon.EnsureString(Code));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ApplyJob", oparam);
        }

        public static DataSet fnGetREC_EVacancyAnno_COMMList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EVacancyAnno_COMMList", oparam);
        }
        public static DataSet fnGetREC_EVacancyAnnoList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EVacancyAnnoList", oparam);
        }
        public static DataSet spu_GetREC_ERequestsList(int iApproved)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Approved", iApproved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ERequestsListDirect", oparam);
        }
        public static DataSet fnGetREC_EVacancyAnno(long REC_ReqID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_EVacancyAnno", oparam);
        }


        public static DataSet fnGetREC_IFinal(long REC_ReqID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IFinal", oparam);
        }


        public static DataSet fnGetREC_IFinalList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IFinalList", oparam);
        }

        public static DataSet fnGetREC_IPreference(long REC_ReqID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IPreference", oparam);
        }


        public static DataSet fnGetREC_IPreferenceList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Approved", Approved);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IPreferenceList", oparam);
        }

        public static DataSet fnGetREC_IStaff(long REC_ReqID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IStaff", oparam);
        }

        public static DataSet fnGetREC_IAvailStaff(long REC_ReqID, long PillarID, long LocationID, long JobID)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@PillarID", PillarID);
            oparam[2] = new SqlParameter("@LocationID", LocationID);
            oparam[3] = new SqlParameter("@JobID", JobID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_IAvailStaff", oparam);
        }
        public static DataSet fnGetREC_RequestsView(long REC_ReqID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_RequestsView", oparam);
        }


        public static DataSet fnGetREC_Requests(long REC_ReqID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_Requests", oparam);
        }


        public static DataSet fnGetREC_RequestsList(int iApproved)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Approved", iApproved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_RequestsList", oparam);
        }

        public static DataSet fnGetREC_Initiate(long ProjectDetailID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ProjectDetailID", ProjectDetailID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_Initiate", oparam);
        }

        public static DataSet fnGetREC_ViewBudget(long projregdet_id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@projregdet_id", projregdet_id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_ViewBudget", oparam);
        }

        public static DataSet fnGetREC_Pendancy(int iApproved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[1] = new SqlParameter("@Approved", iApproved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_Pendancy", oparam);
        }


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
        public static DataSet fnGetPMS_GoalSheet_DetNew(long GoalSheetID, long PMS_GSDID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@GoalSheetID", GoalSheetID);
            oparam[1] = new SqlParameter("@PMS_GSDID", PMS_GSDID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_GoalSheet_DetNew", oparam);
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
        public static DataSet fnGetPMS_EssentialPB(long PMS_EID, string OperationType, long FYID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@PMS_EID", PMS_EID);
            oparam[1] = new SqlParameter("@FYID", FYID);
            oparam[2] = new SqlParameter("@OperationType", ClsCommon.EnsureString(OperationType));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPMS_EssentialPB", oparam);
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
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@StateID", StateID);
            oparam[1] = new SqlParameter("@ProjectID", ProjectID);
            oparam[2] = new SqlParameter("@Month", Month);
            oparam[3] = new SqlParameter("@Year", Year);
            oparam[4] = new SqlParameter("@LOGINID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRDashboardList", oparam);
        }
        public static DataSet fnGetMPRDashboardListNew(long StateID, long ProjectID, int Month, int Year, string DocType)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@StateID", StateID);
            oparam[1] = new SqlParameter("@ProjectID", ProjectID);
            oparam[2] = new SqlParameter("@Month", Month);
            oparam[3] = new SqlParameter("@Year", Year);
            oparam[4] = new SqlParameter("@LOGINID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@DocType", DocType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRDashboardList_new", oparam);
        }
        public static DataSet fnGetMPRDashboardListCount(long StateID, long ProjectID, int Month, int Year, string DocType)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@StateID", StateID);
            oparam[1] = new SqlParameter("@ProjectID", ProjectID);
            oparam[2] = new SqlParameter("@Month", Month);
            oparam[3] = new SqlParameter("@Year", Year);
            oparam[4] = new SqlParameter("@LOGINID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@DocType", DocType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRDashboardList_new", oparam);
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
        public static DataSet GetMPR_Reports_SubHeader(string MPRSID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@MPRSID", MPRSID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPR_Reports_SubHeader", oparam);
        }
        public static DataSet GetMPR_ReportsALlMpr(DateTime StartDate, DateTime Enddate)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@FromDate", StartDate);
            oparam[1] = new SqlParameter("@ToDate", Enddate);
            oparam[2] = new SqlParameter("@UserID ", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetMPRNewReport", oparam);
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

        public static DataSet fnGetOnbehalfEMP()
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetOnbehalf", oparam);
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

        public static DataSet fnGetOfficeList(long Id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ID", Id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetOfficeListing", oparam);
        }
        public static DataSet fnGetsignatoryList(long Id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ID", Id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetExitSignatoryMaster", oparam);
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

        public static DataSet fnGetAttachmentListforMobile(long ID, string TableID, string Tablename, string UserId)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@TableID", TableID);
            oparam[2] = new SqlParameter("@Tablename", Tablename);
            oparam[3] = new SqlParameter("@loginID", UserId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetAttachment", oparam);
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
        public static DataSet fnGetTravelDocuments(long TravelRequestID, long Traveldocid)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            oparam[1] = new SqlParameter("@Traveldocid", Traveldocid);
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
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            oparam[1] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
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
        public static DataSet fnGetBloodGroupList(long id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetBloodGroupList", oparam);

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
        public static DataSet fnGetPerKmList(long Id)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@ID", Id);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetKm", oparam);
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

        // amendenment line again open
        public static DataSet fnGetTravelReq_AmendDet(long lTravelReqID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@travelReqId", lTravelReqID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelReq_AmendDet", oparam);

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
        public static DataSet fnGetProjectTravel_Dropdown()
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_getProjectTravelRequest", oparam);
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

        public static DataSet fnGetofficeLocation(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLocationoffice", oparam);
        }
        public static DataSet fnGetofficeLocationNew(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLocationofficeNew", oparam);
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
        public static DataSet fnGetEmpTravelDateLeave(string Traveldate)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@EmpId", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@TravelDate", Traveldate);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCheckLeaveTravel", oparam);

        }

        public static DataSet fnGetEmpTravelDateHoliday(string Traveldate)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@TravelDate", Traveldate);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetCheckHolidayTravel", oparam);

        }


        public static DataSet fnGetEmpTravelDatewisestatus(string Traveldate)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@EmpId", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@TravelDate", Traveldate);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetDataCheckEmpDayWiseStatus", oparam);

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
        public static DataSet fnGetLoginDataforMobile(long UserID, long EmpId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@UserID", UserID);
            oparam[1] = new SqlParameter("@EmpId", EmpId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLoginDataforMobile", oparam);
        }
        public static DataSet fnGetLoginDataforMobilenonmitr(long UserID, long EmpId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@UserID", UserID);
            oparam[1] = new SqlParameter("@EmpId", EmpId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLoginDataforMobileNonmitr", oparam);
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
        public static DataSet fnGetTravelDailyLog(int month, int year)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@emp_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@month", month);
            oparam[2] = new SqlParameter("@year", year);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelDateInActivity", oparam);
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
        public static DataSet FnGetProjectReport( string fromdate, string todate, string EMPType, string Projectid, string Components)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@EmpType", EMPType);
            oparam[1] = new SqlParameter("@FromDate", fromdate);
            oparam[2] = new SqlParameter("@ToDate", todate);
            oparam[3] = new SqlParameter("@Projectid", Projectid);
            oparam[4] = new SqlParameter("@Component", Components);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetStaffsalaryProjectWiseReport", oparam);



        }
        public static DataSet FnGetStaffWiseReport(string fromdate, string todate,  long EMpId, string Components)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@FromDate", fromdate);
            oparam[1] = new SqlParameter("@ToDate", todate);
            oparam[2] = new SqlParameter("@EmployeeId", EMpId);
            oparam[3] = new SqlParameter("@Component", Components);
             return clsDataBaseHelper.ExecuteDataSet("spu_GetComponentEmployeeWiseReport", oparam);
          

        }
        public static DataSet FnGetConsolidatedStaffSalaryReport(string fromdate, string todate, string EMpId, string Components,string EMPType)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@FromDate", fromdate);
            oparam[1] = new SqlParameter("@ToDate", todate);
            oparam[2] = new SqlParameter("@EmpId", EMpId);
            oparam[3] = new SqlParameter("@Component", Components);
            oparam[4] = new SqlParameter("@EmpType", EMPType);
              return clsDataBaseHelper.ExecuteDataSet("spu_GetEmployeeWiseConsolidatedSalaryReport", oparam);


        }
        public static DataSet FnGetProjectWiseReport(string EMPIDS, string fromdate ,string todate, string EMPType, string Projectid, string Components)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@EmpType", EMPType);
            oparam[1] = new SqlParameter("@EmpId", EMPIDS);
            oparam[2] = new SqlParameter("@FromDate", fromdate);
            oparam[3] = new SqlParameter("@ToDate", todate);
            oparam[4] = new SqlParameter("@Projectid", Projectid);
            oparam[5] = new SqlParameter("@Component", Components);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetStaffsalaryandbenefitsreportbyprojectLiveFinal", oparam);
        

        }

        public static DataSet FnGetComponentWiseReport(string fromdate, string todate, string EMpId, string Components, string EMPType)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@FromDate", fromdate);
            oparam[1] = new SqlParameter("@ToDate", todate);
            oparam[2] = new SqlParameter("@EmpId", EMpId);
            oparam[3] = new SqlParameter("@Component", Components);
            oparam[4] = new SqlParameter("@EmpType", EMPType);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetEmployeeWiseComponentReport", oparam);


        }

        public static DataSet FnGetProjectWiseBonusRpt(long EMPIDS, string Dt)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@dtDate", Dt);
            oparam[1] = new SqlParameter("@Empid", EMPIDS);
            return clsDataBaseHelper.ExecuteDataSet("spu_RepBonusDetails", oparam);

        }

        public static DataSet FnGetGetBankBankReport(string EmpType, string Dt)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@MonthDate", Dt);
            oparam[1] = new SqlParameter("@EmpType", EmpType);
            oparam[2] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("EMPID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetBankBankReportListTest", oparam);

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

        public static DataSet fnGetPrincipalList()
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("EMPID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetProjectLinePrincipalList", oparam);
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
        public static PostResponse fnGetValidateToken(GetValidateToken Modal)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetValidateToken", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Token", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Token);
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnGetCheckRecordExist. The query was executed :", ex.ToString(), "spu_GetCheckRecordExist", "Common_SPU", "Common_SPU", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }
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
        public static PostResponse fnGetUpdateColumnResponse(GetUpdateColumnResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUpdateColumn_Common", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID;
                        command.Parameters.Add("@Value", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Value);
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnGetUpdateColumnResponse. The query was executed :", ex.ToString(), "spu_SetUpdateColumn_Common", "BoardModal", "BoardModal", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static PostResponse fnGetCheckRecordExist(GetRecordExitsResponse Modal)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetCheckRecordExist", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID;
                        command.Parameters.Add("@Value", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Value);
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnGetCheckRecordExist. The query was executed :", ex.ToString(), "spu_GetCheckRecordExist", "Common_SPU", "Common_SPU", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }

        public static PostResponse GetCapacityTrainingNameRecordExist(GetRecordExitsResponse Modal)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Capacity_CheckTrainingNameRecordExist", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Value", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Value);
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnGetCheckRecordExist. The query was executed :", ex.ToString(), "spu_GetCheckRecordExist", "Common_SPU", "Common_SPU", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }
        public static PostResponse GetCapacityTrainingTypeRecordExist(GetRecordExitsResponse Modal)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Capacity_CheckTrainingTypeRecordExist", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Value", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Value);
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnGetCheckRecordExist. The query was executed :", ex.ToString(), "spu_GetCheckRecordExist", "Common_SPU", "Common_SPU", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }
        public static PostResponse GetOnboardingCheckRecordExist(GetRecordExitsResponse Modal)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetOnboardingCheckRecordExist", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Value", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Value);
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnGetCheckRecordExist. The query was executed :", ex.ToString(), "spu_GetCheckRecordExist", "Common_SPU", "Common_SPU", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }
        public static PostResponse fnGetGenerateValues(GetGenerateValues Modal)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetGenerateValues", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@AllIDs", SqlDbType.VarChar).Value = Modal.AllIDs ?? "";
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = Modal.Doctype ?? "";
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = Modal.LoginID;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnGetGenerateValues. The query was executed :", ex.ToString(), "spu_GetGenerateValues", "Common_SPU", "Common_SPU", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }
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
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));

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

        public static DataSet fnGetTravelExpenseRptDetailsLogPerDiem(string Traveldate, string req_no)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@TravelRequestID", req_no);
            oparam[1] = new SqlParameter("@TravelDate", Traveldate);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelExpenseRptDetailsLogPerDiem", oparam);

        }

        public static DataSet fnGetTravelRequest_Dashboard(long Type)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@userid", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[1] = new SqlParameter("@Type", Type);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetTravelRequest_Dashboard", oparam);

        }


        public static DataSet FnGetResignation(long lId, long lEmpid, string sDoctype)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@id", lId);
            oparam[1] = new SqlParameter("@empid", lEmpid);
            oparam[2] = new SqlParameter("@doctype", sDoctype);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetResignation", oparam);
        }


        #endregion

        #region SetStoreProcedure
        public static long fnSetTicket_Notes(long TicketNotesID, long TicketID, long StatusID, DateTime NextDate, string Remarks, long ForwardTo, int IsActive)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@TicketNotesID", TicketNotesID);
            oparam[1] = new SqlParameter("@TicketID", TicketID);
            oparam[2] = new SqlParameter("@StatusID", StatusID);
            oparam[3] = new SqlParameter("@NextDate", NextDate);
            oparam[4] = new SqlParameter("@Remarks", ClsCommon.EnsureString(Remarks));
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@ForwardTo", ForwardTo);
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTicket_Notes", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }

        public static long fnSetTicket_Status(long TicketstatusID, string Type, string StatusName, string DisplayName, string StatusColor, int Priority, int IsActive)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@TicketstatusID", @TicketstatusID);
            oparam[1] = new SqlParameter("@Type", ClsCommon.EnsureString(Type));
            oparam[2] = new SqlParameter("@StatusName", ClsCommon.EnsureString(StatusName));
            oparam[3] = new SqlParameter("@DisplayName", ClsCommon.EnsureString(DisplayName));
            oparam[4] = new SqlParameter("@StatusColor", ClsCommon.EnsureString(StatusColor));
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@Priority", Priority);
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTicket_Status", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }

        public static long fnSetCreateTicket(long TicketID, long CategoryID, long SubCategoryID, long LocationGroupID, long SetPolicyPriority, string Message, long AttachmentID, int IsActive)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@TicketID", TicketID);
            oparam[1] = new SqlParameter("@CategoryID", CategoryID);
            oparam[2] = new SqlParameter("@SubCategoryID", SubCategoryID);
            oparam[3] = new SqlParameter("@LocationGroupID", LocationGroupID);
            oparam[4] = new SqlParameter("@SetPolicyPriority", SetPolicyPriority);
            oparam[5] = new SqlParameter("@Message", ClsCommon.EnsureString(Message));
            oparam[6] = new SqlParameter("@AttachmentID", AttachmentID);
            oparam[7] = new SqlParameter("@IsActive", IsActive);
            oparam[8] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[9] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_setCreateTicket", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }

        public static long fnSetTicket_Configration(long TicketCongID, string StartTime, string EndTime, string ApplicableDays, int IsHolidayCalApplicable, int Priority, int IsActive)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@TicketCongID", TicketCongID);
            oparam[1] = new SqlParameter("@StartTime", ClsCommon.EnsureString(StartTime));
            oparam[2] = new SqlParameter("@EndTime", ClsCommon.EnsureString(EndTime));
            oparam[3] = new SqlParameter("@ApplicableDays", ClsCommon.EnsureString(ApplicableDays));
            oparam[4] = new SqlParameter("@IsHolidayCalApplicable", IsHolidayCalApplicable);
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@Priority", Priority);
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTicket_Configration", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }

        public static long fnSetTicketSetting(long SubCategoryID, long LocationGroupID, string Doctype, long PrimaryAssignee, long Supervisor, long Escalation, string PolicyPriority, long ResponseTime, long FollowUpTime, long EscalationTime, long Reopen, int Priority, int IsActive)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[15];
            oparam[0] = new SqlParameter("@SubCategoryID", SubCategoryID);
            oparam[1] = new SqlParameter("@LocationGroupID", LocationGroupID);
            oparam[2] = new SqlParameter("@Doctype", ClsCommon.EnsureString(Doctype));
            oparam[3] = new SqlParameter("@PrimaryAssignee", PrimaryAssignee);
            oparam[4] = new SqlParameter("@Supervisor", Supervisor);
            oparam[5] = new SqlParameter("@Escalation", Escalation);
            oparam[6] = new SqlParameter("@PolicyPriority", ClsCommon.EnsureString(PolicyPriority));
            oparam[7] = new SqlParameter("@ResponseTime", ResponseTime);
            oparam[8] = new SqlParameter("@FollowUpTime", FollowUpTime);
            oparam[9] = new SqlParameter("@EscalationTime", EscalationTime);
            oparam[10] = new SqlParameter("@Reopen", Reopen);
            oparam[11] = new SqlParameter("@IsActive", IsActive);
            oparam[12] = new SqlParameter("@Priority", Priority);
            oparam[13] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[14] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTicketSetting", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }


        public static long fnSetTicket_SubCategory(long SubCategoryID, long CategoryID, string Name, string RelatedTo, string Description, int Priority, int IsActive)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@SubCategoryID", SubCategoryID);
            oparam[1] = new SqlParameter("@CategoryID", CategoryID);
            oparam[2] = new SqlParameter("@Name", ClsCommon.EnsureString(Name));
            oparam[3] = new SqlParameter("@RelatedTo", ClsCommon.EnsureString(RelatedTo));
            oparam[4] = new SqlParameter("@Description", ClsCommon.EnsureString(Description));
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@Priority", Priority);
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTicket_SubCategory", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }


        public static long fnSetLocationGroup(long LocationGroupID, string GroupName, string Description, string LocationIDs, int Priority, int IsActive)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@LocationGroupID", LocationGroupID);
            oparam[1] = new SqlParameter("@GroupName", ClsCommon.EnsureString(GroupName));
            oparam[2] = new SqlParameter("@Description", ClsCommon.EnsureString(Description));
            oparam[3] = new SqlParameter("@IsActive", IsActive);
            oparam[4] = new SqlParameter("@Priority", Priority);
            oparam[5] = new SqlParameter("@LocationIDs", ClsCommon.EnsureString(LocationIDs));
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetLocationGroup", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }

        public static long fnSetJobPost(long RecruitmentRequestID, string JobTitle, string DueDate, string StartDate, string EndDate, string JobDescription, string Qualification, string Skills, string Experience, long LocationID,
            long AttachmentID, string Announcement, string AnnouncementType, string AnnouncementStartDate, string AnnouncementEndDate, string Link, int Approved, int IsActive, int Priority)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[21];
            oparam[0] = new SqlParameter("@RecruitmentRequestID", RecruitmentRequestID);
            oparam[1] = new SqlParameter("@JobTitle", ClsCommon.EnsureString(JobTitle));
            oparam[2] = new SqlParameter("@DueDate", ClsCommon.EnsureString(DueDate));
            oparam[3] = new SqlParameter("@StartDate", ClsCommon.EnsureString(StartDate));
            oparam[4] = new SqlParameter("@EndDate", ClsCommon.EnsureString(EndDate));
            oparam[5] = new SqlParameter("@JobDescription", ClsCommon.EnsureString(JobDescription));
            oparam[6] = new SqlParameter("@Qualification", ClsCommon.EnsureString(Qualification));
            oparam[7] = new SqlParameter("@Skills", ClsCommon.EnsureString(Skills));
            oparam[8] = new SqlParameter("@Experience", ClsCommon.EnsureString(Experience));
            oparam[9] = new SqlParameter("@LocationID", LocationID);
            oparam[10] = new SqlParameter("@AttachmentID", AttachmentID);
            oparam[11] = new SqlParameter("@Announcement", ClsCommon.EnsureString(Announcement));
            oparam[12] = new SqlParameter("@AnnouncementType", ClsCommon.EnsureString(AnnouncementType));
            oparam[13] = new SqlParameter("@AnnouncementStartDate", ClsCommon.EnsureString(AnnouncementStartDate));
            oparam[14] = new SqlParameter("@AnnouncementEndDate", ClsCommon.EnsureString(AnnouncementEndDate));
            oparam[15] = new SqlParameter("@Link", ClsCommon.EnsureString(Link));
            oparam[16] = new SqlParameter("@Approved", Approved);
            oparam[17] = new SqlParameter("@IsActive", IsActive);
            oparam[18] = new SqlParameter("@Priority", Priority);
            oparam[19] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[20] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetJobPost", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }

        public static long fnSetRecruitmentCandidateLevel_Mapping(long CanLevelMapID, long RecruitmentRequestID, long CandidateEmpID, string DocType, int Preference)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@CanLevelMapID", CanLevelMapID);
            oparam[1] = new SqlParameter("@RecruitmentRequestID", RecruitmentRequestID);
            oparam[2] = new SqlParameter("@DocType", ClsCommon.EnsureString(DocType));
            oparam[3] = new SqlParameter("@CandidateEmpID", CandidateEmpID);
            oparam[4] = new SqlParameter("@ApprovalEMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[5] = new SqlParameter("@Preference", Preference);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetRecruitmentCandidateLevel_Mapping", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }


        public static long fnSetRecruitmentInternal_Level(long RecruitmentLevelID, long RecruitmentRequestID, string DocType, long ApprovalEMPID, int Priority, int IsActive)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@RecruitmentLevelID", RecruitmentLevelID);
            oparam[1] = new SqlParameter("@RecruitmentRequestID", RecruitmentRequestID);
            oparam[2] = new SqlParameter("@DocType", ClsCommon.EnsureString(DocType));
            oparam[3] = new SqlParameter("@ApprovalEMPID", ApprovalEMPID);

            oparam[4] = new SqlParameter("@IsActive", IsActive);
            oparam[5] = new SqlParameter("@Priority", Priority);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetRecruitmentInternal_Level", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }

        public static long fnSetRecruitmentCandidate_Internal(long InternalCandidateID, long RecruitmentRequestID, long EMPID, string Relocation, string JobTitle, string Pillar, string RelocationByHR, string JobTitleByHR, string PillarByHR, int Priority, int IsActive)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[13];
            oparam[0] = new SqlParameter("@InternalCandidateID", InternalCandidateID);
            oparam[1] = new SqlParameter("@RecruitmentRequestID", RecruitmentRequestID);
            oparam[2] = new SqlParameter("@EMPID", EMPID);
            oparam[3] = new SqlParameter("@Relocation", ClsCommon.EnsureString(Relocation));
            oparam[4] = new SqlParameter("@JobTitle", ClsCommon.EnsureString(JobTitle));
            oparam[5] = new SqlParameter("@Pillar", ClsCommon.EnsureString(Pillar));
            oparam[6] = new SqlParameter("@RelocationByHR", ClsCommon.EnsureString(RelocationByHR));
            oparam[7] = new SqlParameter("@JobTitleByHR", ClsCommon.EnsureString(JobTitleByHR));
            oparam[8] = new SqlParameter("@PillarByHR", ClsCommon.EnsureString(PillarByHR));
            oparam[9] = new SqlParameter("@IsActive", IsActive);
            oparam[10] = new SqlParameter("@Priority", Priority);
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[12] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetRecruitmentCandidate_Internal", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }

        public static long fnSetCreateInitialRecruitmentRequest(long ProjectDetailID, long JobID, string RecruitmentType, string StaffCategory, long Location, int Priority, int IsActive, int isdeleted)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@ProjectDetailID", ProjectDetailID);
            oparam[1] = new SqlParameter("@JobID", JobID);
            oparam[2] = new SqlParameter("@RecruitmentType", ClsCommon.EnsureString(RecruitmentType));
            oparam[3] = new SqlParameter("@StaffCategory", ClsCommon.EnsureString(StaffCategory));
            oparam[4] = new SqlParameter("@Location", Location);
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@Priority", Priority);
            oparam[7] = new SqlParameter("@isdeleted", isdeleted);
            oparam[8] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[9] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetCreateInitialRecruitmentRequest", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }

        public static long fnSetJobDetails(long JobDetailID, long JobID, int Srno, int IsNegotiationRound, string DocType, long LinkWithJobDetID, string RoundType, string RoundName, string RoundTitle, string RoundDesc, string RoundMemberType, long EMPID, string Name, string Email, int Priority, int IsActive)
        {

            long ID = 0;
            SqlParameter[] oparam = new SqlParameter[18];
            oparam[0] = new SqlParameter("@JobDetailID", JobDetailID);
            oparam[1] = new SqlParameter("@LinkWithJobDetID", LinkWithJobDetID);
            oparam[2] = new SqlParameter("@JobID", JobID);
            oparam[3] = new SqlParameter("@Srno", Srno);
            oparam[4] = new SqlParameter("@DocType", ClsCommon.EnsureString(DocType));
            oparam[5] = new SqlParameter("@RoundType", ClsCommon.EnsureString(RoundType));
            oparam[6] = new SqlParameter("@RoundName", ClsCommon.EnsureString(RoundName));
            oparam[7] = new SqlParameter("@RoundTitle", ClsCommon.EnsureString(RoundTitle));
            oparam[8] = new SqlParameter("@RoundDesc", ClsCommon.EnsureString(RoundDesc));
            oparam[9] = new SqlParameter("@RoundMemberType", ClsCommon.EnsureString(RoundMemberType));
            oparam[10] = new SqlParameter("@EMPID", EMPID);
            oparam[11] = new SqlParameter("@IsNegotiationRound", IsNegotiationRound);
            oparam[12] = new SqlParameter("@Name", ClsCommon.EnsureString(Name));
            oparam[13] = new SqlParameter("@Email", ClsCommon.EnsureString(Email));
            oparam[14] = new SqlParameter("@IsActive", IsActive);
            oparam[15] = new SqlParameter("@Priority", Priority);
            oparam[16] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[17] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetJobDetails", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                ID = 0;
            return ID;

        }

        public static long fnSetJob(long JobID, string JobCode, string Title, string Experience, string Skills, string Description, string QualificationDet, int NoticePeriod, int ProbationPeriod,
            int Priority, int IsActive)
        {

            long ID = 0;
            SqlParameter[] oparam = new SqlParameter[13];
            oparam[0] = new SqlParameter("@JobID", JobID);
            oparam[1] = new SqlParameter("@JobCode", ClsCommon.EnsureString(JobCode));
            oparam[2] = new SqlParameter("@Title", ClsCommon.EnsureString(Title));
            oparam[3] = new SqlParameter("@Experience", ClsCommon.EnsureString(Experience));
            oparam[4] = new SqlParameter("@Description", ClsCommon.EnsureString(Description));
            oparam[5] = new SqlParameter("@QualificationDet", ClsCommon.EnsureString(QualificationDet));
            oparam[6] = new SqlParameter("@Skills", ClsCommon.EnsureString(Skills));
            oparam[7] = new SqlParameter("@NoticePeriod", NoticePeriod);
            oparam[8] = new SqlParameter("@ProbationPeriod", ProbationPeriod);
            oparam[9] = new SqlParameter("@IsActive", IsActive);
            oparam[10] = new SqlParameter("@Priority", Priority);
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[12] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetJob", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                ID = 0;
            return ID;

        }
        public static CommandResult fnSetRecruitmentRoundsEmailConfirmation(string REC_InterviewSetID, string EmailType, string REC_ReqID, string Reason, string RoundName)
        {

            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@Recruitment_Id", REC_InterviewSetID);
            oparam[1] = new SqlParameter("@EmailType", EmailType);
            oparam[2] = new SqlParameter("@ProjectDetailID", REC_ReqID);
            oparam[3] = new SqlParameter("@Reason", Reason);
            oparam[4] = new SqlParameter("@RoundName", RoundName);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_Recruitment_SendEmailConfirmationSender", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static long fnSetTravelExpenseApproval_Action(long TravelExpenseID, string Actionname, string hod_remarks, int approved)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@TravelExpenseID", TravelExpenseID);
            oparam[1] = new SqlParameter("@Actionname", ClsCommon.EnsureString(Actionname));
            oparam[2] = new SqlParameter("@hod_remarks", ClsCommon.EnsureString(hod_remarks));
            oparam[3] = new SqlParameter("@approved", approved);
            oparam[4] = new SqlParameter("@approvedby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteDataSet("spu_SetTravelExpenseApproval_Action", oparam);
            return 1;
        }
        public static long fnSetForwardToFinanceBy_Desk(long TravelRequestID, long TravelDetailID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            oparam[1] = new SqlParameter("@Id", TravelDetailID);
            clsDataBaseHelper.ExecuteDataSet("Set_ForwardToFinanceBy_Desk", oparam);
            return 1;
        }


        public static long fnSetFreeMeal(long lFreeMealId, string sFreeMeal, double dFreeMealPercen, int Priority, int IsActive)
        {
            long fnSetFreeMeal = 0;
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@id", lFreeMealId);
            oparam[1] = new SqlParameter("@FreeMeal", ClsCommon.EnsureString(sFreeMeal));
            oparam[2] = new SqlParameter("@freemeal_percen", dFreeMealPercen);
            oparam[3] = new SqlParameter("@IsActive", IsActive);
            oparam[4] = new SqlParameter("@Priority", Priority);
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetFreeMeal", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFreeMeal = 0;
            return fnSetFreeMeal;

        }

        public static int fnSetTravelRequestsDetailsIntoExpense(long TravelRequestID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            clsDataBaseHelper.ExecuteDataSet("spu_SetTravelRequestsDetailsIntoExpense", oparam);
            return 1;
        }

        public static int fnSetTravelExpenRpt_Log(long lTERId, long lTRPNNo, string lReqNo, string sTravellerName, string sCostCenterName,
            string sCreditDetails, string sPayMode, double dAmount, string sRemark, string sSummary, double dTravelfare, double dPerDiem,
            double dTransporation, double dOtherExpense, double dTotal, double dAdvanceRecevied, double dAnyOtherCredit, double dNetRecivable, string sRemark1, string Command, string rdbOther_status, string Expensedistrubute_status)
        {
            int fnSetTravelExpenRpt_Log = 0;
            SqlParameter[] oparam = new SqlParameter[24];
            oparam[0] = new SqlParameter("@id", lTERId);
            oparam[1] = new SqlParameter("@trpn_no", lTRPNNo);
            oparam[2] = new SqlParameter("@req_no", lReqNo);
            oparam[3] = new SqlParameter("@traveler_name", ClsCommon.EnsureString(sTravellerName));
            oparam[4] = new SqlParameter("@cost_center", ClsCommon.EnsureString(sCostCenterName));
            oparam[5] = new SqlParameter("@credit_details", ClsCommon.EnsureString(sCreditDetails));
            oparam[6] = new SqlParameter("@pay_mode", ClsCommon.EnsureString(sPayMode));
            oparam[7] = new SqlParameter("@amount", dAmount);
            oparam[8] = new SqlParameter("@remark", ClsCommon.EnsureString(sRemark));
            oparam[9] = new SqlParameter("@summary", ClsCommon.EnsureString(sSummary));
            oparam[10] = new SqlParameter("@travel_fare", dTravelfare);
            oparam[11] = new SqlParameter("@per_Diem", dPerDiem);
            oparam[12] = new SqlParameter("@transporation", dTransporation);
            oparam[13] = new SqlParameter("@other_expense", dOtherExpense);
            oparam[14] = new SqlParameter("@total", dTotal);
            oparam[15] = new SqlParameter("@advance_received", dAdvanceRecevied);
            oparam[16] = new SqlParameter("@anyOther_Credit", dAnyOtherCredit);
            oparam[17] = new SqlParameter("@net_receivable", dNetRecivable);

            oparam[18] = new SqlParameter("@SaveType", ClsCommon.EnsureString(Command));
            oparam[19] = new SqlParameter("@rdbOther_status", ClsCommon.EnsureString(rdbOther_status));
            oparam[20] = new SqlParameter("@remark1", ClsCommon.EnsureString(sRemark1));
            oparam[21] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[22] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[23] = new SqlParameter("@ExpensedistrubuteStatus", ClsCommon.EnsureString(Expensedistrubute_status));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTravelExpenRpt", oparam);
            if (ds.Tables[0].Rows.Count > 0)
                fnSetTravelExpenRpt_Log = Convert.ToInt32(ds.Tables[0].Rows[0]["RET_ID"]);
            else
                fnSetTravelExpenRpt_Log = 0;
            return fnSetTravelExpenRpt_Log;


        }

        public static int fnSetTEDetTravelFare(long lTravelExpenseId, string dtDate, string sTravelMode, string sTicketBoardCast,
            double dPerDiemAmont, int iSrNo, string sDocType, string Travel_source, long lFromCityId, long lToCityId, int IsDefault, long ProjectId)
        {
            SqlParameter[] oparam = new SqlParameter[13];
            oparam[0] = new SqlParameter("@travelexpen_id", lTravelExpenseId);
            oparam[1] = new SqlParameter("@Date", dtDate);
            oparam[2] = new SqlParameter("@mode_travel", ClsCommon.EnsureString(sTravelMode));
            oparam[3] = new SqlParameter("@ticket_bordcast", ClsCommon.EnsureString(sTicketBoardCast));
            oparam[4] = new SqlParameter("@PerDiem_Amount", dPerDiemAmont);
            oparam[5] = new SqlParameter("@srno", iSrNo);
            oparam[6] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
            oparam[7] = new SqlParameter("@Travel_source", ClsCommon.EnsureString(Travel_source));
            oparam[8] = new SqlParameter("@fromcity_id", lFromCityId);
            oparam[9] = new SqlParameter("@tocity_id", lToCityId);
            oparam[10] = new SqlParameter("@IsDefault", IsDefault);
            oparam[11] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[12] = new SqlParameter("@ProjectId", ProjectId);
            clsDataBaseHelper.ExecuteDataSet("spu_SetTEDetTravelFare", oparam);
            return 1;

        }

        public static int fnSetTEDetPerDiem(long lTravelExpenseId, string dtDate, string sFormCity, string sToCity,
            double dDeductPercen, double dPerDiemRate, double dPerDiemAmount, int iSrNo, string sDocType, long lMealId, long ProjectId)
        {
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@travelexpen_id", lTravelExpenseId);
            oparam[1] = new SqlParameter("@Date", dtDate);
            oparam[2] = new SqlParameter("@from_city", ClsCommon.EnsureString(sFormCity));
            oparam[3] = new SqlParameter("@to_city", ClsCommon.EnsureString(sToCity));

            oparam[4] = new SqlParameter("@deduction_percen", dDeductPercen);
            oparam[5] = new SqlParameter("@PerDiem_rate", dPerDiemRate);
            oparam[6] = new SqlParameter("@PerDiem_Amount", dPerDiemAmount);
            oparam[7] = new SqlParameter("@srno", iSrNo);
            oparam[8] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
            oparam[9] = new SqlParameter("@meal_id", lMealId);
            oparam[10] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[11] = new SqlParameter("@ProjectId", ProjectId);
            clsDataBaseHelper.ExecuteDataSet("spu_SetTEDetPerDiem", oparam);
            return 1;

        }
        public static int fnSetTETransportion(long lTravelExpenseId, string dtDate, string sModeTransport, string sTransportDetail, string iTransAttachNo, double dTransAmount, int iSrNo, string sDocType, string skm, double dKmrate, long ProjectId, long sModeTransportId)
        {
            SqlParameter[] oparam = new SqlParameter[13];
            oparam[0] = new SqlParameter("@travelexpen_id", lTravelExpenseId);
            oparam[1] = new SqlParameter("@Date", dtDate);
            oparam[2] = new SqlParameter("@mode_transport", ClsCommon.EnsureString(sModeTransport));
            oparam[3] = new SqlParameter("@transport_details", ClsCommon.EnsureString(sTransportDetail));
            oparam[4] = new SqlParameter("@transport_AttachNo", ClsCommon.EnsureString(iTransAttachNo));
            oparam[5] = new SqlParameter("@transport_Amount", dTransAmount);
            oparam[6] = new SqlParameter("@srno", iSrNo);
            oparam[7] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
            //oparam[8] = new SqlParameter("@km", skm);
            //oparam[9] = new SqlParameter("@travel_km", dKmrate);
            oparam[8] = new SqlParameter("@km", dKmrate);
            oparam[9] = new SqlParameter("@travel_km", skm);
            oparam[10] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[11] = new SqlParameter("@ProjectId", ProjectId);
            oparam[12] = new SqlParameter("@ModeTransportId", sModeTransportId);
            clsDataBaseHelper.ExecuteDataSet("spu_SetTEtransporation", oparam);
            return 1;

        }

        //public static int fnSetTETransportion(long lTravelExpenseId, string dtDate, string sModeTransport, string sTransportDetail, string iTransAttachNo, double dTransAmount, int iSrNo, string sDocType, string skm, double dKmrate,long ProjectId, long sModeTransportId)
        //{
        //    SqlParameter[] oparam = new SqlParameter[13];
        //    oparam[0] = new SqlParameter("@travelexpen_id", lTravelExpenseId);
        //    oparam[1] = new SqlParameter("@Date", dtDate);
        //    oparam[2] = new SqlParameter("@mode_transport", ClsCommon.EnsureString(sModeTransport));
        //    oparam[3] = new SqlParameter("@transport_details", ClsCommon.EnsureString(sTransportDetail));
        //    oparam[4] = new SqlParameter("@transport_AttachNo", ClsCommon.EnsureString(iTransAttachNo));
        //    oparam[5] = new SqlParameter("@transport_Amount", dTransAmount);
        //    oparam[6] = new SqlParameter("@srno", iSrNo);
        //    oparam[7] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
        //    oparam[8] = new SqlParameter("@km", skm);
        //    oparam[9] = new SqlParameter("@travel_km", dKmrate);
        //    oparam[10] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
        //    oparam[11] = new SqlParameter("@ProjectId", ProjectId);
        //    oparam[12] = new SqlParameter("@ModeTransportId", sModeTransportId);
        //    clsDataBaseHelper.ExecuteDataSet("spu_SetTEtransporation", oparam);
        //    return 1;

        //}
        public static int fnSetTEOtherExpense(long lTravelExpenseId, string dtDate, string sExpenDetail, string iExpenAttachNo, double dExpenAmount, int iSrNo, string sDocType, string sJust, long ProjectId)
        {
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@travelexpen_id", lTravelExpenseId);
            oparam[1] = new SqlParameter("@Date", dtDate);
            oparam[2] = new SqlParameter("@expend_details", ClsCommon.EnsureString(sExpenDetail));
            oparam[3] = new SqlParameter("@expend_AttachNo", iExpenAttachNo);
            oparam[4] = new SqlParameter("@expend_Amount", dExpenAmount);
            oparam[5] = new SqlParameter("@srno", iSrNo);
            oparam[6] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
            oparam[7] = new SqlParameter("@justification", ClsCommon.EnsureString(sJust));
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[9] = new SqlParameter("@ProjectId", ProjectId);
            clsDataBaseHelper.ExecuteDataSet("spu_SetTEOtherExpense", oparam);
            return 1;

        }


        public static int fnSetTEFillReprt(long lTravelExpenseId, long DetailsId, string dtDate, string sFormCity, string sToCity, int iSrNo, string sDocType, string sRemarks)
        {
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@travelexpen_id", lTravelExpenseId);
            oparam[1] = new SqlParameter("@Date", dtDate);
            oparam[2] = new SqlParameter("@from_city", ClsCommon.EnsureString(sFormCity));
            oparam[3] = new SqlParameter("@to_city", ClsCommon.EnsureString(sToCity));
            oparam[4] = new SqlParameter("@srno", iSrNo);
            oparam[5] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
            oparam[6] = new SqlParameter("@justification", ClsCommon.EnsureString(sRemarks));
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[8] = new SqlParameter("@Id", DetailsId);

            clsDataBaseHelper.ExecuteDataSet("spu_SetTEFillReport", oparam);
            return 1;

        }

        public static int fnSetTravelExpenseSummary(long ID, long travelexpense_id, long project_id, double travel_fare, double per_Diem, double transporation, double other_expense, long travel_id)
        {
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@travelexpense_id", travelexpense_id);
            oparam[2] = new SqlParameter("@project_id", project_id);
            oparam[3] = new SqlParameter("@travel_fare", travel_fare);
            oparam[4] = new SqlParameter("@per_Diem", per_Diem);
            oparam[5] = new SqlParameter("@transporation", transporation);
            oparam[6] = new SqlParameter("@other_expense", other_expense);
            oparam[7] = new SqlParameter("@travel_id", travel_id);
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));

            clsDataBaseHelper.ExecuteDataSet("spu_Settravelexpense_summary", oparam);
            return 1;

        }

        public static int fnDelTravelExpenseRptDet(long lTERID, int iSrno, string sDocType)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@id", lTERID);
            oparam[1] = new SqlParameter("@SRNO", iSrno);
            oparam[2] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
            oparam[3] = new SqlParameter("@USERID", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteDataSet("spu_DelTravelExpenseRptDet", oparam);
            return 1;
        }

        //public static long fnSetTravelDocuments(long TravelRequestID, string DocumentType, long AttachmentID, string TransactionDate, decimal Amount, decimal RefundAmount,string BookBy, string AgentName,long staffid, long TravelDocID)
        //{
        //    long MainID = 0;
        //    SqlParameter[] oparam = new SqlParameter[12];
        //    oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
        //    oparam[1] = new SqlParameter("@DocumentType", ClsCommon.EnsureString(DocumentType));
        //    oparam[2] = new SqlParameter("@AttachmentID", AttachmentID);
        //    oparam[3] = new SqlParameter("@TransactionDate", TransactionDate);
        //    oparam[4] = new SqlParameter("@Amount", Amount);
        //    oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
        //    oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
        //    oparam[7] = new SqlParameter("@RefundAmount", RefundAmount);
        //    oparam[8] = new SqlParameter("@BookBy", BookBy);
        //    oparam[9] = new SqlParameter("@AgentName", AgentName);
        //    oparam[10] = new SqlParameter("@staffid", staffid);
        //    oparam[11] = new SqlParameter("@Traveldocid", TravelDocID);
        //    DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTravelDocuments", oparam);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        MainID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
        //    }
        //    else
        //        MainID = 0;
        //    return MainID;
        //}

        public static long fnSetTravelDocuments(long TravelRequestID, string DocumentType, long AttachmentID, string TransactionDate, decimal Amount, decimal RefundAmount, string BookBy, string AgentName, long staffid, long TravelDocID, string CardandBank, string BillNo, string BillDate)
        {
            long MainID = 0;
            SqlParameter[] oparam = new SqlParameter[15];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            oparam[1] = new SqlParameter("@DocumentType", ClsCommon.EnsureString(DocumentType));
            oparam[2] = new SqlParameter("@AttachmentID", AttachmentID);
            oparam[3] = new SqlParameter("@TransactionDate", TransactionDate);
            oparam[4] = new SqlParameter("@Amount", Amount);
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[7] = new SqlParameter("@RefundAmount", RefundAmount);
            oparam[8] = new SqlParameter("@BookBy", BookBy);
            oparam[9] = new SqlParameter("@AgentName", AgentName);
            oparam[10] = new SqlParameter("@staffid", staffid);
            oparam[11] = new SqlParameter("@Traveldocid", TravelDocID);
            oparam[12] = new SqlParameter("@CardandBank", CardandBank);
            oparam[13] = new SqlParameter("@BillNo", BillNo);
            oparam[14] = new SqlParameter("@BillDate", BillDate);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTravelDocuments", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MainID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                MainID = 0;
            return MainID;
        }

        public static long fnSetProcurCommMst(long lID, long lEmpId, string dtEffectiveDate, int Priority, int IsActive)
        {
            long fnSetProcurCommMst = 0;
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@id", lID);
            oparam[1] = new SqlParameter("@emp_id", lEmpId);
            oparam[2] = new SqlParameter("@effective_date", dtEffectiveDate);
            oparam[3] = new SqlParameter("@Priority", Priority);
            oparam[4] = new SqlParameter("@IsActive", IsActive);
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetProcurCommMst", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetProcurCommMst = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);

            }
            else
                fnSetProcurCommMst = 0;
            return fnSetProcurCommMst;

        }


        public static long fnSetDepreRate(long lId, string sMainCode, string sDescrip, string sMethod, double dDepreRate, long iMultiple, double dDepreRateDouble, double dDepreRateTriple, int Priority, int IsActive)
        {
            long fnSetDepreRate = 0;
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@id", lId);
            oparam[1] = new SqlParameter("@main_code", ClsCommon.EnsureString(sMainCode));
            oparam[2] = new SqlParameter("@descrip", ClsCommon.EnsureString(sDescrip));
            oparam[3] = new SqlParameter("@method", ClsCommon.EnsureString(sMethod));
            oparam[4] = new SqlParameter("@depre_rate", dDepreRate);
            oparam[5] = new SqlParameter("@iMultiple", iMultiple);
            oparam[6] = new SqlParameter("@depre_rate_double", dDepreRateDouble);
            oparam[7] = new SqlParameter("@depre_rate_triple", dDepreRateTriple);
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[9] = new SqlParameter("@Priority", Priority);
            oparam[10] = new SqlParameter("@IsActive", IsActive);
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetDepreRateMst", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetDepreRate = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);

            }
            else
                fnSetDepreRate = 0;
            return fnSetDepreRate;

        }
        public static long fnSetVendorMst(long lVendorId, string sVCode, string sVName, string sAddress, string sRepresentative, string sContactNo, string sLstNo, string sCSTNo, string sPanNo, int Priority, int IsActive)
        {
            long fnSetVendorMst = 0;
            SqlParameter[] oparam = new SqlParameter[13];
            oparam[0] = new SqlParameter("@id", lVendorId);
            oparam[1] = new SqlParameter("@vendor_code", ClsCommon.EnsureString(sVCode));
            oparam[2] = new SqlParameter("@vendor_name", ClsCommon.EnsureString(sVName));
            oparam[3] = new SqlParameter("@address", ClsCommon.EnsureString(sAddress));
            oparam[4] = new SqlParameter("@representative", ClsCommon.EnsureString(sRepresentative));
            oparam[5] = new SqlParameter("@contact_no", ClsCommon.EnsureString(sContactNo));
            oparam[6] = new SqlParameter("@lst_no", ClsCommon.EnsureString(sLstNo));
            oparam[7] = new SqlParameter("@cst_no", ClsCommon.EnsureString(sCSTNo));
            oparam[8] = new SqlParameter("@pan_no", ClsCommon.EnsureString(sPanNo));
            oparam[9] = new SqlParameter("@Priority", Priority);
            oparam[10] = new SqlParameter("@IsActive", IsActive);
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[12] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetVendorMst", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetVendorMst = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetVendorMst = 0;
            return fnSetVendorMst;

        }

        public static long fnSetSubLineItem(long lSubLineItemId, string sSubLineCode, string sSubLineName, long lCostCenterId, int Priority, int IsActive)
        {
            long fnSetSubLineItem = 0;
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@id", lSubLineItemId);
            oparam[1] = new SqlParameter("@subLItem_Code", ClsCommon.EnsureString(sSubLineCode));
            oparam[2] = new SqlParameter("@SubLItem_Name", ClsCommon.EnsureString(sSubLineName));
            oparam[3] = new SqlParameter("@CostCenter_ID", lCostCenterId);
            oparam[4] = new SqlParameter("@Priority", Priority);
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetLineItem", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetSubLineItem = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetSubLineItem = 0;
            return fnSetSubLineItem;
        }

        public static long fnSetCostCenter(long lCostCenterId, string sCostCenterCode, string sCostCenterName, int Priority, int IsActive)
        {
            long fnSetCostCenter = 0;
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@id", lCostCenterId);
            oparam[1] = new SqlParameter("@CostCenter_Code", ClsCommon.EnsureString(sCostCenterCode));
            oparam[2] = new SqlParameter("@costcenter_name", ClsCommon.EnsureString(sCostCenterName));
            oparam[3] = new SqlParameter("@Priority", Priority);
            oparam[4] = new SqlParameter("@IsActive", IsActive);
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetCostCenter", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetCostCenter = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetCostCenter = 0;
            return fnSetCostCenter;
        }

        public static long fnSetDonor(long lDonorId, long lSourceId, string sStatus, string sDonorName, long lAddressId, string sWebsite, int Priority, int IsActive)
        {
            long fnSetDonor = 0;
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@id", lDonorId);
            oparam[1] = new SqlParameter("@source_id", lSourceId);
            oparam[2] = new SqlParameter("@status", ClsCommon.EnsureString(sStatus));
            oparam[3] = new SqlParameter("@donor_name", ClsCommon.EnsureString(sDonorName));
            oparam[4] = new SqlParameter("@address_id", lAddressId);
            oparam[5] = new SqlParameter("@website", ClsCommon.EnsureString(sWebsite));
            oparam[6] = new SqlParameter("@Priority", Priority);
            oparam[7] = new SqlParameter("@IsActive", IsActive);
            oparam[8] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[9] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetDonor", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetDonor = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetDonor = 0;
            return fnSetDonor;
        }

        public static long fnSetDonorDetail(long donor_id, int srno, string person_name, string designation, string location,
            string phone_no, string mobile_no, string email)
        {
            long fnSetDonor = 0;
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@donor_id", donor_id);
            oparam[1] = new SqlParameter("@srno", srno);
            oparam[2] = new SqlParameter("@person_name", ClsCommon.EnsureString(person_name));
            oparam[3] = new SqlParameter("@designation", ClsCommon.EnsureString(designation));
            oparam[4] = new SqlParameter("@location", ClsCommon.EnsureString(location));
            oparam[5] = new SqlParameter("@phone_no", ClsCommon.EnsureString(phone_no));
            oparam[6] = new SqlParameter("@mobile_no", ClsCommon.EnsureString(mobile_no));
            oparam[7] = new SqlParameter("@email", ClsCommon.EnsureString(email));
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetDonorDetail", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetDonor = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetDonor = 0;
            return fnSetDonor;
        }

        public static long fnSetAddress(long Addressid, string lane1, string lane2, string City, string State, string Country, string Zip_code, string Phone, string Cell, string LocationId, string Fax)
        {
            long fnSetAddress = 0;
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@id", Addressid);
            oparam[1] = new SqlParameter("@lane1", ClsCommon.EnsureString(lane1));
            oparam[2] = new SqlParameter("@lane2", ClsCommon.EnsureString(lane2));
            oparam[3] = new SqlParameter("@city_name", ClsCommon.EnsureString(City));
            oparam[4] = new SqlParameter("@State_name", ClsCommon.EnsureString(State));
            oparam[5] = new SqlParameter("@Country_name", ClsCommon.EnsureString(Country));
            oparam[6] = new SqlParameter("@zip_code", ClsCommon.EnsureString(Zip_code));
            oparam[7] = new SqlParameter("@phone_no", ClsCommon.EnsureString(Phone));
            oparam[8] = new SqlParameter("@cell", ClsCommon.EnsureString(Cell));
            oparam[9] = new SqlParameter("@fax", ClsCommon.EnsureString(Fax));
            oparam[10] = new SqlParameter("@Location_ID", LocationId);
            oparam[11] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetAddress", oparam);
            if (ds.Tables[0].Rows.Count > 0)
                fnSetAddress = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            else
                fnSetAddress = 0;
            return fnSetAddress;
        }
        public static int fnSetTravelRequestAction(long lTravelReqId, int Approved, string reason, string ActionType)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@TravelRequestID", lTravelReqId);
            oparam[1] = new SqlParameter("@Approved", Approved);
            oparam[2] = new SqlParameter("@reason", ClsCommon.EnsureString(reason));
            oparam[3] = new SqlParameter("@ActionType", ClsCommon.EnsureString(ActionType));
            oparam[4] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteSp("spu_SetTravelRequestAction", oparam);
            return 1;

        }
        public static int fnSetTravelRequestExpenceAction(long ID, decimal Paidamount, string PaidDate, string ActionType, string reason)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@Id", ID);
            oparam[1] = new SqlParameter("@Paidamount", Convert.ToDecimal(Paidamount));
            oparam[2] = new SqlParameter("@PaidDate", Convert.ToDateTime(PaidDate));
            oparam[3] = new SqlParameter("@ActionType", ClsCommon.EnsureString(ActionType));
            oparam[4] = new SqlParameter("@LoginID", Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID")));
            oparam[5] = new SqlParameter("@reason", ClsCommon.EnsureString(reason));
            clsDataBaseHelper.ExecuteSp("spu_SetExpenseTravelRequestAction", oparam);
            return 1;

        }

        public static int fnSetEMPLockAction(long EMPId, int Lock)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@EMPId", EMPId);
            oparam[1] = new SqlParameter("@Lock", Lock);
            oparam[2] = new SqlParameter("@LoginID", Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID")));
            clsDataBaseHelper.ExecuteSp("spu_SetFullFinalLock", oparam);
            return 1;

        }

        public static int fnSetPMSEditAction(long PMSGSID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@PMSGSID", PMSGSID);
            oparam[1] = new SqlParameter("@LoginID", Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID")));
            clsDataBaseHelper.ExecuteSp("spu_SetPMSEdit", oparam);
            return 1;

        }
        public static int fnSetTravelRequestAdvanceAction(long ID, decimal Paidamount, string PaidDate, string ActionType, string reason)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@Id", ID);
            oparam[1] = new SqlParameter("@Paidamount", Convert.ToDecimal(Paidamount));
            oparam[2] = new SqlParameter("@PaidDate", Convert.ToDateTime(PaidDate));
            oparam[3] = new SqlParameter("@ActionType", ClsCommon.EnsureString(ActionType));
            oparam[4] = new SqlParameter("@LoginID", Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID")));
            oparam[5] = new SqlParameter("@reason", ClsCommon.EnsureString(reason));
            clsDataBaseHelper.ExecuteSp("spu_SetAdvanceTravelRequestAction", oparam);
            return 1;

        }
        public static int fnSetTravelRequestRFCSend(long ID, string reason)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@TravelRequestID", ID);
            oparam[1] = new SqlParameter("@LoginID", Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID")));
            oparam[2] = new SqlParameter("@reason", ClsCommon.EnsureString(reason));
            clsDataBaseHelper.ExecuteSp("spu_SetTravelRequestRFCSubmit", oparam);
            return 1;

        }
        public static int fnSetTravelRequestApprovalManager(long TravelRequestID, long ProjectDetId, string ActionType, string reason)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            oparam[1] = new SqlParameter("ProjectDetId", ProjectDetId);
            oparam[2] = new SqlParameter("@reason", ClsCommon.EnsureString(reason));
            oparam[3] = new SqlParameter("@EmpId", Convert.ToInt64(clsApplicationSetting.GetSessionValue("EMPID")));
            oparam[4] = new SqlParameter("@ActionType", ClsCommon.EnsureString(ActionType));
            clsDataBaseHelper.ExecuteSp("spu_SetPrincipalApprovalManager", oparam);
            return 1;

        }

        public static int fnSetTravelRequestTransferTrevelDesk(long TravelRequestDetID, long TravelDeskId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@TravelRequestDetID", TravelRequestDetID);
            oparam[1] = new SqlParameter("@TravelDeskId", TravelDeskId);

            clsDataBaseHelper.ExecuteSp("spu_SetTrnasferTravelDesk", oparam);
            return 1;

        }
        public static long fnSetDiem(long lDiemId, string sStatus, string sCategory, double dPerDiemRate, double dHotelRate, int Priority, int IsActive)
        {
            long fnSetdiem = 0;
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@id", lDiemId);
            oparam[1] = new SqlParameter("@status", ClsCommon.EnsureString(sStatus));
            oparam[2] = new SqlParameter("@Category", ClsCommon.EnsureString(sCategory));
            oparam[3] = new SqlParameter("@perdiem_rate", dPerDiemRate);
            oparam[4] = new SqlParameter("@hotel_rate", dHotelRate);
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[6] = new SqlParameter("@Priority", Priority);
            oparam[7] = new SqlParameter("@IsActive", IsActive);
            oparam[8] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetDiem", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetdiem = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);

            }
            else
                fnSetdiem = 0;
            return fnSetdiem;
        }


        public static long fnSetState(long lStateId, string sStateName, long lCountryId, string sDescrip, int Priority, int IsActive)
        {
            long fnSetState = 0;
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@id", lStateId);
            oparam[1] = new SqlParameter("@state_name", sStateName);
            oparam[2] = new SqlParameter("@country_id", lCountryId);
            oparam[3] = new SqlParameter("@description", ClsCommon.EnsureString(sDescrip));
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@Priority", Priority);
            oparam[6] = new SqlParameter("@IsActive", IsActive);
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetState", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetState = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);

            }
            else
                fnSetState = 0;
            return fnSetState;
        }
        public static long fnSetcity(long lCityId, string sCityName, string sCategory, long lStateId, string sDescription, int Priority, int IsActive)
        {
            long fnSetCity = 0;
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@id", lCityId);
            oparam[1] = new SqlParameter("@city_name", sCityName);
            oparam[2] = new SqlParameter("@Category", ClsCommon.EnsureString(sCategory));
            oparam[3] = new SqlParameter("@state_Id", lStateId);
            oparam[4] = new SqlParameter("@description", ClsCommon.EnsureString(sDescription));
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[6] = new SqlParameter("@Priority", Priority);
            oparam[7] = new SqlParameter("@IsActive", IsActive);
            oparam[8] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_Setcity", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetCity = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetCity = 0;
            return fnSetCity;
        }

        public static long fnSetDesign(long lDesignId, string sDesignName, string sDescription, int Priority, int IsActive)
        {
            long fnSetDesign = 0;
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@id", lDesignId);
            oparam[1] = new SqlParameter("@Design_Name", ClsCommon.EnsureString(sDesignName));
            oparam[2] = new SqlParameter("@Description", ClsCommon.EnsureString(sDescription));
            oparam[3] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[4] = new SqlParameter("@Priority", Priority);
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetDesign", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetDesign = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetDesign = 0;
            return fnSetDesign;


        }

        public static long fnSetLoaction(long lLocId, string sLocName, string sDescription, int Priority, int IsActive)
        {
            long fnSetHoliday = 0;
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@id", lLocId);
            oparam[1] = new SqlParameter("@Location_Name", ClsCommon.EnsureString(sLocName));
            oparam[2] = new SqlParameter("@Description", ClsCommon.EnsureString(sDescription));
            oparam[3] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[4] = new SqlParameter("@Priority", Priority);
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetLocation", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetHoliday = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);

            }
            else
                fnSetHoliday = 0;
            return fnSetHoliday;

        }

        public static string FnSetOtp()
        {
            string OTP = "";
            SqlParameter[] oparam = new SqlParameter[2];

            oparam[0] = new SqlParameter("@User_ID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[1] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetOtp", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                OTP = ds.Tables[0].Rows[0]["RET_ID"].ToString();
            }
            return OTP;

        }


        public static void FnSetSMSLog(string mobile, string msg, string status, string doctype)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@mobile", mobile);
            oparam[1] = new SqlParameter("@msg", @msg);
            oparam[2] = new SqlParameter("@status", status);
            oparam[3] = new SqlParameter("@doctype", doctype);
            oparam[4] = new SqlParameter("@loginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            clsDataBaseHelper.ExecuteDataSet("spu_SetSMS_Log", oparam);

        }

        public static long fnSetTravelAdvanReq(long TravelRequestID, int IsAdvanceRequest,
            decimal dLocalTravelAmt, decimal dLoadingAmt, decimal dTotalAmt, decimal dAdvanceRoundAmt,
            string OtherSection, decimal OtherAmount, string OtherSection1, decimal OtherAmount1,
            string sPayMode, string sAccountNo, string sBankName, string sNeftCode, string sBranchame)
        {
            long fnSetTravelAdvanReq = 0;
            SqlParameter[] oparam = new SqlParameter[16];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            oparam[1] = new SqlParameter("@IsAdvanceRequest", IsAdvanceRequest);
            oparam[2] = new SqlParameter("@localTravel_amt", dLocalTravelAmt);
            oparam[3] = new SqlParameter("@Loading_amt", dLoadingAmt);
            oparam[4] = new SqlParameter("@total_amount", dTotalAmt);
            oparam[5] = new SqlParameter("@totaladvanceroundoff", dAdvanceRoundAmt);
            oparam[6] = new SqlParameter("@pay_mode", ClsCommon.EnsureString(sPayMode));
            oparam[7] = new SqlParameter("@OtherSection", ClsCommon.EnsureString(OtherSection));
            oparam[8] = new SqlParameter("@OtherAmount", OtherAmount);
            oparam[9] = new SqlParameter("@OtherSection1", ClsCommon.EnsureString(OtherSection1));
            oparam[10] = new SqlParameter("@OtherAmount1", OtherAmount1);
            oparam[11] = new SqlParameter("@account_no", ClsCommon.EnsureString(sAccountNo));
            oparam[12] = new SqlParameter("@bank_name", ClsCommon.EnsureString(sBankName));
            oparam[13] = new SqlParameter("@neft_code", ClsCommon.EnsureString(sNeftCode));
            oparam[14] = new SqlParameter("@branch_name", ClsCommon.EnsureString(sBranchame));
            oparam[15] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTravelAdvanceRequest", oparam);
            if (ds.Tables[0].Rows.Count > 0)
                fnSetTravelAdvanReq = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            else
                fnSetTravelAdvanReq = 0;
            return fnSetTravelAdvanReq;
        }


        // update approve Principal approval
        public static long fnSetTravelPrincipalapproval(long TravelRequestID)
        {
            //long fnSetTravelAdvanReq = 0;
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            oparam[1] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPrincipalApproval", oparam);

            return 1;
        }

        public static long fnSetTravelDeskModeAir(long TravelRequestID)
        {
            //long fnSetTravelAdvanReq = 0;
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@TravelRequestID", TravelRequestID);
            oparam[1] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTravelDeskModeAir", oparam);

            return 1;
        }
        public static long fnSetLeave(long lLeavelId, string ApplicableFor, string sLeaveName, int iLeaveType, int iDueType, int iNoLeave, string sColorApplied, string sColorApproved, string sCFLeave, string sCFLimit, string iStatus, string sColorCodeApply, string sColorCodeApproved, int iLeaveFrwd, int iCOLeave, int iShownLeave, int Priority, int IsActive)
        {
            long fnSetLeave = 0;
            SqlParameter[] oparam = new SqlParameter[20];
            oparam[0] = new SqlParameter("@Id", lLeavelId);
            oparam[1] = new SqlParameter("@leave_Name", ClsCommon.EnsureString(sLeaveName));
            oparam[2] = new SqlParameter("@leave_type", iLeaveType);
            oparam[3] = new SqlParameter("@due_type", iDueType);
            oparam[4] = new SqlParameter("@no_leave", iNoLeave);
            oparam[5] = new SqlParameter("@color_applied", ClsCommon.EnsureString(sColorApplied));
            oparam[6] = new SqlParameter("@color_appproved", ClsCommon.EnsureString(sColorApproved));
            oparam[7] = new SqlParameter("@cf_leave", ClsCommon.EnsureString(sCFLeave));
            oparam[8] = new SqlParameter("@cf_limit", ClsCommon.EnsureString(sCFLimit));
            oparam[9] = new SqlParameter("@status", ClsCommon.EnsureString(iStatus));
            oparam[10] = new SqlParameter("@LeaveFrwd", iLeaveFrwd);
            oparam[11] = new SqlParameter("@COLeave", iCOLeave);
            oparam[12] = new SqlParameter("@colorcode_applied", ClsCommon.EnsureString(sColorCodeApply));
            oparam[13] = new SqlParameter("@colorcode_approved", ClsCommon.EnsureString(sColorCodeApproved));
            oparam[14] = new SqlParameter("@shownleave", iShownLeave);
            oparam[15] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[16] = new SqlParameter("@Priority", Priority);
            oparam[17] = new SqlParameter("@ApplicableFor", ClsCommon.EnsureString(ApplicableFor));
            oparam[18] = new SqlParameter("@IsActive", IsActive);
            oparam[19] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetLeave", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetLeave = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetLeave = 0;
            return fnSetLeave;


        }


        public static int fnSetTravelReq_Det(long lTrvaelReqId, int iSrNo, int iSrNo1, string dtTravelDate, int lFormCity, int lToCity,
            string ClassOfCity, decimal PerDiem_Rate, int lTravelMode, int lOfficePersonal,
            int lTicketBooking, int lHotelBooking, string sTicketDetail, string sJustification, string sHotelNo, string OtherHotel, decimal PerdiemAmountuser, string ClassOfCityUser)
        {
            SqlParameter[] oparam = new SqlParameter[19];
            oparam[0] = new SqlParameter("@travelreq_id", lTrvaelReqId);
            oparam[1] = new SqlParameter("@srno", iSrNo);
            oparam[2] = new SqlParameter("@srno1", iSrNo1);
            oparam[3] = new SqlParameter("@travel_date", dtTravelDate);
            oparam[4] = new SqlParameter("@from_city", lFormCity);
            oparam[5] = new SqlParameter("@to_city", lToCity);
            oparam[6] = new SqlParameter("@ClassOfCity", ClsCommon.EnsureString(ClassOfCity));
            oparam[7] = new SqlParameter("@PerDiem_Rate", PerDiem_Rate);
            oparam[8] = new SqlParameter("@travel_mode", lTravelMode);
            oparam[9] = new SqlParameter("@officePersonel", lOfficePersonal);
            oparam[10] = new SqlParameter("@ticketbooking", lTicketBooking);
            oparam[11] = new SqlParameter("@hotelbooking", lHotelBooking);
            oparam[12] = new SqlParameter("@ticketdetail", ClsCommon.EnsureString(sTicketDetail));
            oparam[13] = new SqlParameter("@justification", ClsCommon.EnsureString(sJustification));
            oparam[14] = new SqlParameter("@hotel_no", ClsCommon.EnsureString(sHotelNo));
            oparam[15] = new SqlParameter("@OtherHotel", ClsCommon.EnsureString(OtherHotel));
            oparam[16] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[17] = new SqlParameter("@UserPerDiem_Rate", PerdiemAmountuser);
            oparam[18] = new SqlParameter("@UserClassOfCity", ClsCommon.EnsureString(ClassOfCityUser));
            clsDataBaseHelper.ExecuteSp("spu_SetTravelReqDet", oparam);
            return 1;

        }
        public static long fnSetFinYear(long lId, string sYear, string dtFromDate, string dtToDate, int Priority, int IsActive)
        {
            long fnSetFinYear = 0;
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@id", lId);
            oparam[1] = new SqlParameter("@year", ClsCommon.EnsureString(sYear));
            oparam[2] = new SqlParameter("@from_date", dtFromDate);
            oparam[3] = new SqlParameter("@to_date", dtToDate);
            oparam[4] = new SqlParameter("@Priority", Priority);
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetFinYear", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFinYear = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFinYear = 0;
            return fnSetFinYear;


        }

        public static long fnSetMemberShipPeriod(long lId, string sYear, string dtFromdate, string dtTodate, int Priority, int IsActive)
        {
            long fnSetFinYear = 0;
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@id", lId);
            oparam[1] = new SqlParameter("@year", ClsCommon.EnsureString(sYear));
            oparam[2] = new SqlParameter("@from_date", dtFromdate);
            oparam[3] = new SqlParameter("@to_date", dtTodate);
            oparam[4] = new SqlParameter("@Priority", Priority);
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMemberPeriod", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetFinYear = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetFinYear = 0;
            return fnSetFinYear;
        }

        public static int fnSetByLaws(long lSrno, string sDescrip, string dtFromDate, string dtTodate, long lAttachmentID, string sDocType, long lCatId, int Priority, int IsActive)
        {
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@srno", lSrno);
            oparam[1] = new SqlParameter("@descrip", ClsCommon.EnsureString(sDescrip));
            oparam[2] = new SqlParameter("@from_date", dtFromDate);
            oparam[3] = new SqlParameter("@to_date", dtTodate);
            oparam[4] = new SqlParameter("@attachment_id", lAttachmentID);
            oparam[5] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
            oparam[6] = new SqlParameter("@user_id", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[7] = new SqlParameter("@cat_Id", lCatId);
            oparam[8] = new SqlParameter("@Priority", Priority);
            oparam[9] = new SqlParameter("@IsActive", IsActive);
            oparam[10] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            clsDataBaseHelper.ExecuteSp("spu_Setbylaws", oparam);
            return 1;

        }
        public static long fnSetQuestionMst(long lId, string sQuestion, long sNoOption, string sOption1, string sOption2, string sOption3, string sOption4, string dtStartDate, string dtEndDate, int Priority, int IsActive)
        {
            long fnSetQuestionMst = 0;
            SqlParameter[] oparam = new SqlParameter[13];
            oparam[0] = new SqlParameter("@id", lId);
            oparam[1] = new SqlParameter("@Question", ClsCommon.EnsureString(sQuestion));
            oparam[2] = new SqlParameter("@noOfOption", sNoOption);
            oparam[3] = new SqlParameter("@option1", ClsCommon.EnsureString(sOption1));
            oparam[4] = new SqlParameter("@option2", ClsCommon.EnsureString(sOption2));
            oparam[5] = new SqlParameter("@option3", ClsCommon.EnsureString(sOption3));
            oparam[6] = new SqlParameter("@option4", ClsCommon.EnsureString(sOption4));
            oparam[7] = new SqlParameter("@start_date", dtStartDate);
            oparam[8] = new SqlParameter("@end_date", dtEndDate);
            oparam[9] = new SqlParameter("@Priority", Priority);
            oparam[10] = new SqlParameter("@IsActive", IsActive);
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[12] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMasterQuestion", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetQuestionMst = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetQuestionMst = 0;
            return fnSetQuestionMst;
        }

        public static long fnSetAllMaster(long lId, string sTableaname, string sFieldname, string sFieldvalue, int lGroup_id, int dSerialno, int Priority, int IsActive)
        {
            long fnSetAllMaster = 0;
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@id", lId);
            oparam[1] = new SqlParameter("@table_name", ClsCommon.EnsureString(sTableaname));
            oparam[2] = new SqlParameter("@field_name", ClsCommon.EnsureString(sFieldname));
            oparam[3] = new SqlParameter("@field_value", ClsCommon.EnsureString(sFieldvalue));
            oparam[4] = new SqlParameter("@group_id", lGroup_id);
            oparam[5] = new SqlParameter("@srno", dSerialno);
            oparam[6] = new SqlParameter("@Priority", Priority);
            oparam[7] = new SqlParameter("@IsActive", IsActive);
            oparam[8] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[9] = new SqlParameter("@user_id", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMasterAll", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {

                fnSetAllMaster = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetAllMaster = 0;
            return fnSetAllMaster;
        }

        public static int fnSetMapHolidayLoc(long lHolidayId, long lLocationId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@holiday_id", lHolidayId);
            oparam[1] = new SqlParameter("@location_id", lLocationId);
            clsDataBaseHelper.ExecuteSp("spu_SetMapHolidayLoc", oparam);
            return 1;
        }
        public static long fnSetHoliday(long lHolId, string sHoliDayName, string dHDate, string sRemarks, string sColorName, string sColorCode, int Priority, int IsActive)
        {
            long fnSetHoliday = 0;
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@Id", lHolId);
            oparam[1] = new SqlParameter("@HOLIDAY_NAME", ClsCommon.EnsureString(sHoliDayName));
            oparam[2] = new SqlParameter("@HOLIDAY_DATE", dHDate);
            oparam[3] = new SqlParameter("@REMARKS", ClsCommon.EnsureString(sRemarks));
            oparam[4] = new SqlParameter("@AUSER_ID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@color_name", ClsCommon.EnsureString(sColorName));
            oparam[6] = new SqlParameter("@color_code", ClsCommon.EnsureString(sColorCode));
            oparam[7] = new SqlParameter("@LOcation_id", "0");
            oparam[8] = new SqlParameter("@Priority", Priority);
            oparam[9] = new SqlParameter("@IsActive", IsActive);
            oparam[10] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[11] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetHoliDay", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetHoliday = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetHoliday = 0;
            return fnSetHoliday;
        }

        public static int fnSetMapProjCostcenter(string sProjName, string sCostCenterName, long lTravelId, int isrno, long lProjDetID)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@proj_name", ClsCommon.EnsureString(sProjName));
            oparam[1] = new SqlParameter("@costcenter_name", sCostCenterName);
            oparam[2] = new SqlParameter("@travel_id", lTravelId);
            oparam[3] = new SqlParameter("@srno", isrno);
            oparam[4] = new SqlParameter("@ProjRegDet_id", lProjDetID);
            clsDataBaseHelper.ExecuteSp("spu_SetMapProjCostCenter", oparam);
            return 1;

        }
        public static long fnSetTravelRequest(long lTravelId, string lReqNo, string dtReqdate, string sTravellerName,
            long lProjId, long lCostCenterId, string sPurposeVisit, string sEmailTo, string sEmailCc, long lEmpId, string sEmpType, string sContactNo,
            string RequestType, string TravelType, string TripType, string TripSponsorName, int IsTicketToBeBooked, int IsHotelToBeBooked,
            long SponsorAttachID, int FromCity, int ToCity, string DepartureDate, string ReturnDate, string UserRemarks, int Submited)
        {
            long fnSetTravelReq = 0;
            SqlParameter[] oparam = new SqlParameter[27];
            oparam[0] = new SqlParameter("@id", lTravelId);
            oparam[1] = new SqlParameter("@req_no", ClsCommon.EnsureString(lReqNo));
            oparam[2] = new SqlParameter("@req_date", Convert.ToDateTime(dtReqdate).ToString("yyyy-MM-dd"));
            oparam[3] = new SqlParameter("@traveller_name", ClsCommon.EnsureString(sTravellerName));
            oparam[4] = new SqlParameter("@generatedby", clsApplicationSetting.GetSessionValue("UserName"));
            oparam[5] = new SqlParameter("@proj_id", lProjId);
            oparam[6] = new SqlParameter("@costcenter_id", lCostCenterId);
            oparam[7] = new SqlParameter("@purpofvisit", ClsCommon.EnsureString(sPurposeVisit));
            oparam[8] = new SqlParameter("@emailto", ClsCommon.EnsureString(sEmailTo));
            oparam[9] = new SqlParameter("@emailCc", ClsCommon.EnsureString(sEmailCc));
            oparam[10] = new SqlParameter("@emp_id", lEmpId);
            oparam[11] = new SqlParameter("@emp_type", ClsCommon.EnsureString(sEmpType));
            oparam[12] = new SqlParameter("@contact_no", ClsCommon.EnsureString(sContactNo));
            oparam[13] = new SqlParameter("@RequestType", ClsCommon.EnsureString(RequestType));
            oparam[14] = new SqlParameter("@TravelType", ClsCommon.EnsureString(TravelType));
            oparam[15] = new SqlParameter("@TripType", ClsCommon.EnsureString(TripType));
            oparam[16] = new SqlParameter("@TripSponsorName", ClsCommon.EnsureString(TripSponsorName));
            oparam[17] = new SqlParameter("@IsTicketToBeBooked", IsTicketToBeBooked);
            oparam[18] = new SqlParameter("@IsHotelToBeBooked", IsHotelToBeBooked);
            oparam[19] = new SqlParameter("@SponsorAttachID", SponsorAttachID);
            oparam[20] = new SqlParameter("@FromCity", FromCity);
            oparam[21] = new SqlParameter("@ToCity", ToCity);
            oparam[22] = new SqlParameter("@DepartureDate", DepartureDate);
            oparam[23] = new SqlParameter("@ReturnDate", ReturnDate);
            oparam[24] = new SqlParameter("@UserRemarks", ClsCommon.EnsureString(UserRemarks));
            oparam[25] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[26] = new SqlParameter("@Submited", Submited);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTravelRequest", oparam);
            if (ds.Tables[0].Rows.Count > 0)
                fnSetTravelReq = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            else
                fnSetTravelReq = 0;
            return fnSetTravelReq;
        }

        public static int fnSetPurposeamendment(long req_no, string purpofvisit, string AmendmentType, string ActionType, int fromcity, int tocity, string fromdepaturedate, string fromreturndate, string ReasonAmendment, int ApprovedStatus)
        {
            SqlParameter[] oparam = new SqlParameter[11];
            oparam[0] = new SqlParameter("@req_no", req_no);
            oparam[1] = new SqlParameter("@purpofvisit", purpofvisit);
            oparam[2] = new SqlParameter("@AmendmentType", AmendmentType);
            oparam[3] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[4] = new SqlParameter("@ActionType", ActionType);
            oparam[5] = new SqlParameter("@fromcity", fromcity);
            oparam[6] = new SqlParameter("@tocity", tocity);
            oparam[7] = new SqlParameter("@fromdepaturedate", fromdepaturedate);
            oparam[8] = new SqlParameter("@fromreturndate", fromreturndate);
            oparam[9] = new SqlParameter("@ReasonAmendment", ReasonAmendment);
            oparam[10] = new SqlParameter("@ApprovedStatus", ApprovedStatus);
            clsDataBaseHelper.ExecuteSp("spu_SetTravelRequestPurposeAmendment", oparam);
            return 1;
        }
        public static int fnSetTravelReqDetPurposeAmendment(long travelreq_id, long travelreqdet_id, long from_city, long to_city, string depaturedate, string ActionType)
        {
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@travelreq_id", travelreq_id);
            oparam[1] = new SqlParameter("@travelreqdet_id", travelreqdet_id);
            oparam[2] = new SqlParameter("@from_city", from_city);
            oparam[3] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[4] = new SqlParameter("@to_city", to_city);
            oparam[5] = new SqlParameter("@depaturedate", depaturedate);
            oparam[6] = new SqlParameter("@ActionType", ActionType);
            clsDataBaseHelper.ExecuteSp("spu_SetTravelReqDetPurposeAmendment", oparam);
            return 1;
        }

        public static int fnSetMapAnnouncementLoc(long lAnnounceId, long lLocationId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@announcement_id", lAnnounceId);
            oparam[1] = new SqlParameter("@location_id", lLocationId);
            clsDataBaseHelper.ExecuteSp("spu_SetMapAnnouncementLoc", oparam);
            return 1;
        }
        public static long fnSetAnouncement(long lAnnounceId, string sHeadingName, string sDescrip, long lAttachmentId, string dtStartDate, string dtExpiryDate, int Priority, int IsActive)
        {
            long fnSetAnouncement = 0;
            SqlParameter[] oparam = new SqlParameter[11];
            oparam[0] = new SqlParameter("@Id", lAnnounceId);
            oparam[1] = new SqlParameter("@heading_name", ClsCommon.EnsureString(sHeadingName));
            oparam[2] = new SqlParameter("@description", ClsCommon.EnsureString(sDescrip));
            oparam[3] = new SqlParameter("@auser_id", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[4] = new SqlParameter("@attachment_id", lAttachmentId);
            oparam[5] = new SqlParameter("@start_date", dtStartDate);
            oparam[6] = new SqlParameter("@Priority", Priority);
            oparam[7] = new SqlParameter("@IsActive", IsActive);
            oparam[8] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[9] = new SqlParameter("@expiry_date", dtExpiryDate);
            oparam[10] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetAnnouncement", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetAnouncement = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetAnouncement = 0;
            return fnSetAnouncement;
        }
        public static long fnSetSubGrantDetAtthr(long lSubgrantID, int lSrNo, string sName, string sDesign, string sDocType)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@subgrant_id", lSubgrantID);
            oparam[1] = new SqlParameter("@srno", lSrNo);
            oparam[2] = new SqlParameter("@Name", ClsCommon.EnsureString(sName));
            oparam[3] = new SqlParameter("@design", ClsCommon.EnsureString(sDesign));
            oparam[4] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteSp("spu_SetSubgtantDetAtthr", oparam);
            return 1;

        }
        public static long fnSetSubGrantDetAtthachment(long lSubgrantID, int lSrNo, string sDescrip, long lTypeID, long lAttachmentID, string sDocType)
        {
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@subgrant_id", lSubgrantID);
            oparam[1] = new SqlParameter("@srno", lSrNo);
            oparam[2] = new SqlParameter("@Name", ClsCommon.EnsureString(sDescrip));
            oparam[3] = new SqlParameter("@type_id", lTypeID);
            oparam[4] = new SqlParameter("@attchment_id", lAttachmentID);
            oparam[5] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteSp("spu_SetSubgtantDetAttach", oparam);
            return 1;
        }
        public static long fnDelSubgrantDet(long lSubgrantID, string iSrno, string sDocType)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@id", lSubgrantID);
            oparam[1] = new SqlParameter("@SRNO", iSrno);
            oparam[2] = new SqlParameter("@USERID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[3] = new SqlParameter("@doc_type", ClsCommon.EnsureString(sDocType));
            clsDataBaseHelper.ExecuteSp("spu_DelSubgrantdet", oparam);
            return 1;

        }
        public static long fnSetSubgrantMst(long lId, string sSubCode, string sSubName, string sAddress, string sMobile, string sLocation, string sPANNo, string sFCRANo, string sShortName, int Priority, int IsActive)
        {
            long fnSetSubgrantMst = 0;
            SqlParameter[] oparam = new SqlParameter[13];
            oparam[0] = new SqlParameter("@ID", lId);
            oparam[1] = new SqlParameter("@subgrant_code", ClsCommon.EnsureString(sSubCode));
            oparam[2] = new SqlParameter("@subgrant_name", ClsCommon.EnsureString(sSubName));
            oparam[3] = new SqlParameter("@address", ClsCommon.EnsureString(sAddress));
            oparam[4] = new SqlParameter("@mobile", ClsCommon.EnsureString(sMobile));
            oparam[5] = new SqlParameter("@location", ClsCommon.EnsureString(sLocation));
            oparam[6] = new SqlParameter("@panno", ClsCommon.EnsureString(sPANNo));
            oparam[7] = new SqlParameter("@fcra_no", ClsCommon.EnsureString(sFCRANo));
            oparam[8] = new SqlParameter("@short_name", ClsCommon.EnsureString(sShortName));
            oparam[9] = new SqlParameter("@Priority", Priority);
            oparam[10] = new SqlParameter("@IsActive", IsActive);
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[12] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetSubgrant", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetSubgrantMst = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetSubgrantMst = 0;
            return fnSetSubgrantMst;
        }

        public static long fnSetThematicArea(long lThemAreaId, string sThemAreaCode, string sDescrip, string Category, int Priority, int IsActive)
        {
            long fnSetThematicArea = 0;
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@id", lThemAreaId);
            oparam[1] = new SqlParameter("@ThematicArea_Code", ClsCommon.EnsureString(sThemAreaCode));
            oparam[2] = new SqlParameter("@Description", ClsCommon.EnsureString(sDescrip));
            oparam[3] = new SqlParameter("@Category", ClsCommon.EnsureString(Category));
            oparam[4] = new SqlParameter("@Priority", Priority);
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetThemArea", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetThematicArea = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetThematicArea = 0;
            return fnSetThematicArea;

        }
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

        public static long SetAttachmentsMappingUpdateFileName(long lAttachment_Id)
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long fnSetAttachments = 0;

            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", lAttachment_Id);

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_Onboarding_UpdateMasterAttachment", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetAttachments = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetAttachments = 0;
            return fnSetAttachments;

        }

        public static int OnboardingEmpCheck(long empid)
        {
            int fnSetAttachments = 0;


            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@EMPID", empid);

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_OnboardingEmpCheck", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetAttachments = Convert.ToInt32(ds.Tables[0].Rows[0]["IsOnboardEmp"]);
            }
            else
                fnSetAttachments = 0;
            return fnSetAttachments;

        }


        public static long SetAttachmentsMapping(long lAttachment_Id, string sFilename, string sContenttype, string sDescrip, string TableID = "0", string TableName = "")
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
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_Onboarding_SetattAchmentMapping", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetAttachments = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetAttachments = 0;
            return fnSetAttachments;

        }

        public static long fnSetAttachmentsforMobile(long lAttachment_Id, string sFilename, string sContenttype, string sDescrip, long UserId, string TableID = "0", string TableName = "")
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long fnSetAttachments = 0;
            if (TableID == "")
            {
                TableID = "0";
            }
            long tableid = Convert.ToInt64(TableID);
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@id", lAttachment_Id);
            oparam[1] = new SqlParameter("@filename", ClsCommon.EnsureString(sFilename));
            oparam[2] = new SqlParameter("@content_type", ClsCommon.EnsureString(sContenttype));
            oparam[3] = new SqlParameter("@Descrip", ClsCommon.EnsureString(sDescrip));
            oparam[4] = new SqlParameter("@table_id", tableid);
            oparam[5] = new SqlParameter("@TableName", ClsCommon.EnsureString(TableName));
            if (LoginID > 0)
            {
                oparam[6] = new SqlParameter("@createdby", LoginID);
            }
            else
            {
                oparam[6] = new SqlParameter("@createdby", UserId);
            }

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
        public static long fnSetCompnayConfiguration(int id, string Company, string Adress)
        {
            long fnSetCompnayConfiguration = 0;
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@id", id);
            oparam[1] = new SqlParameter("@CompanyName", ClsCommon.EnsureString(Company));
            oparam[2] = new SqlParameter("@@Address", ClsCommon.EnsureString(Adress));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetCompanyConfiguration", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetCompnayConfiguration = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetCompnayConfiguration = 0;
            return fnSetCompnayConfiguration;



        }

        public static long fnGetCompnayConfiguration(int id)
        {
            long fnGetCompnayConfiguration = 0;
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@id", id);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_GetCompanyConfiguration", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnGetCompnayConfiguration = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnGetCompnayConfiguration = 0;
            return fnGetCompnayConfiguration;



        }
        public static long fnSetConsultant(long lConsultId, string sConsulName, string sConsCode, string sStatus,
            string sNameOnPanCard, string sfathername, string sLocalAddress, string sPermAddress, string sLocation, string sPhoneNo,
            string sPanNo, string sPayMode, string sDesign, string sAccountNo, string sBankName, string sNeftCode, string sAccountName,
            long lAttachId, string sEmail, string sContactPerson, string sTitle, int Priority, int IsActive)
        {
            long fnSetConsultant = 0;
            SqlParameter[] oparam = new SqlParameter[25];
            oparam[0] = new SqlParameter("@id", lConsultId);
            oparam[1] = new SqlParameter("@consultant_name", ClsCommon.EnsureString(sConsulName));
            oparam[2] = new SqlParameter("@consultant_code", ClsCommon.EnsureString(sConsCode));
            oparam[3] = new SqlParameter("@status", ClsCommon.EnsureString(sStatus));
            oparam[4] = new SqlParameter("@name_onPanCard", ClsCommon.EnsureString(sNameOnPanCard));
            oparam[5] = new SqlParameter("@father_name", ClsCommon.EnsureString(sfathername));
            oparam[6] = new SqlParameter("@local_address", ClsCommon.EnsureString(sLocalAddress));
            oparam[7] = new SqlParameter("@perm_address", ClsCommon.EnsureString(sPermAddress));
            oparam[8] = new SqlParameter("@location", ClsCommon.EnsureString(sLocation));
            oparam[9] = new SqlParameter("@phone_no", ClsCommon.EnsureString(sPhoneNo));
            oparam[10] = new SqlParameter("@pan_no", ClsCommon.EnsureString(sPanNo));
            oparam[11] = new SqlParameter("@pay_mode", ClsCommon.EnsureString(sPayMode));
            oparam[12] = new SqlParameter("@design", ClsCommon.EnsureString(sDesign));
            oparam[13] = new SqlParameter("@account_no", ClsCommon.EnsureString(sAccountNo));
            oparam[14] = new SqlParameter("@bank_name", ClsCommon.EnsureString(sBankName));
            oparam[15] = new SqlParameter("@neft_code", ClsCommon.EnsureString(sNeftCode));
            oparam[16] = new SqlParameter("@account_name", ClsCommon.EnsureString(sAccountName));
            oparam[17] = new SqlParameter("@attachment_id", lAttachId);
            oparam[18] = new SqlParameter("@email", ClsCommon.EnsureString(sEmail));
            oparam[19] = new SqlParameter("@contact_person", ClsCommon.EnsureString(sContactPerson));
            oparam[20] = new SqlParameter("@title", ClsCommon.EnsureString(sTitle));
            oparam[21] = new SqlParameter("@Priority", Priority);
            oparam[22] = new SqlParameter("@IsActive", IsActive);
            oparam[23] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[24] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetConsultant", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetConsultant = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetConsultant = 0;
            return fnSetConsultant;
        }


        public static int fnSetMapThematicArea(string sTBName, string lTAId, string lLinkId)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@table_name", sTBName);
            oparam[1] = new SqlParameter("@ta_id", lTAId);
            oparam[2] = new SqlParameter("@link_id", lLinkId);
            clsDataBaseHelper.ExecuteSp("spu_SetMapThematicArea", oparam);
            return 1;
        }

        public static void CreateMail_ApplyLeave(long LeaveLogID)
        {
            SqlParameter[] oParam = new SqlParameter[1];
            oParam[0] = new SqlParameter("@LeaveLogID", LeaveLogID);
            clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_ApplyLeave", oParam);

        }

        public static long fnSetLeaveLog(long lLeaveLogId, long lEmpId, DateTime dtStartDate, DateTime dtEndDate, DateTime dtExpectedDeliveryDate, long iLeaveId,
            string sEmergenContactNo, string EmergencyContactName, string sRemarks, string dLeaveHours, string sdatesel, long AttachmentRequired, string sEmergenContactRelation)
        {
            long fnSetLeaveLog = 0;
            SqlParameter[] oparam = new SqlParameter[14];
            oparam[0] = new SqlParameter("@id", lLeaveLogId);
            oparam[1] = new SqlParameter("@emp_id", lEmpId);
            oparam[2] = new SqlParameter("@start_date", dtStartDate);
            oparam[3] = new SqlParameter("@end_date", dtEndDate);
            oparam[4] = new SqlParameter("@leave_id", iLeaveId);
            oparam[5] = new SqlParameter("@emergenContactName", ClsCommon.EnsureString(EmergencyContactName));
            oparam[6] = new SqlParameter("@emergenContact_no", ClsCommon.EnsureString(sEmergenContactNo));
            oparam[7] = new SqlParameter("@remarks", ClsCommon.EnsureString(sRemarks));
            oparam[8] = new SqlParameter("@leave_hours", dLeaveHours);
            oparam[9] = new SqlParameter("@datesel", sdatesel);
            oparam[10] = new SqlParameter("@ExpectedDeliveryDate", (dtExpectedDeliveryDate.Year > 1900 ? dtExpectedDeliveryDate.ToString("dd-MMM-yyyy") : ""));
            oparam[11] = new SqlParameter("@AttachmentRequired", AttachmentRequired);
            oparam[12] = new SqlParameter("@emergenContactRelation", ClsCommon.EnsureString(sEmergenContactRelation));
            oparam[13] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetLeaveLog", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetLeaveLog = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetLeaveLog = 0;
            return fnSetLeaveLog;

        }
        public static int fnSetLeaveDet(long lLeaveLogId, DateTime dtDate, string sHours, int lSrNo, int iLeaveId, string sReason)
        {
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@leavelog_id", lLeaveLogId);
            oparam[1] = new SqlParameter("@date", dtDate);
            oparam[2] = new SqlParameter("@hours", sHours);
            oparam[3] = new SqlParameter("@srno", lSrNo);
            oparam[4] = new SqlParameter("@leave_id", iLeaveId);
            oparam[5] = new SqlParameter("@reason", ClsCommon.EnsureString(sReason));
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteSp("spu_SetLeaveDetLog", oparam);
            return 1;

        }
        public static DataSet fnDelLeaveDetLog(long lLeaveLogId, int iSrno)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@id", lLeaveLogId);
            oparam[1] = new SqlParameter("@SRNO", iSrno);
            oparam[2] = new SqlParameter("@USERID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_DelLeavedetLog", oparam);
        }

        public static DataSet fnDelProjectEMP_SelfMapping(long ID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@id", ID);
            return clsDataBaseHelper.ExecuteDataSet("spu_DelProjectEMP_SelfMapping", oparam);
        }

        public static int fnSetCompensatoryOff(long lCompensatoryId, long lEmpId, int iMonth, string dtDate, string sHours, string sDescrip, string lProjId)
        {
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@Id", lCompensatoryId);
            oparam[1] = new SqlParameter("@emp_id", lEmpId);
            oparam[2] = new SqlParameter("@month", iMonth);
            oparam[3] = new SqlParameter("@Date", dtDate);
            oparam[4] = new SqlParameter("@hours", sHours);
            oparam[5] = new SqlParameter("@Description", sDescrip);
            oparam[6] = new SqlParameter("@Proj_id", lProjId);
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteDataSet("spu_SetCompansatory", oparam);
            return 1;

        }

        public static int fnSetOvertime(int iSrno, long EMPID, int iMonth, int iYear, string dtDate, string sHours, string sDescrip, string lProjId)
        {
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@srno", iSrno);
            oparam[1] = new SqlParameter("@emp_id", EMPID);
            oparam[2] = new SqlParameter("@month", iMonth);
            oparam[3] = new SqlParameter("@year", iYear);
            oparam[4] = new SqlParameter("@Date", dtDate);
            oparam[5] = new SqlParameter("@hours", sHours);
            oparam[6] = new SqlParameter("@Description", ClsCommon.EnsureString(sDescrip));
            oparam[7] = new SqlParameter("@Proj_id", lProjId);
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteDataSet("spu_SetOvertime", oparam);
            return 1;
        }

        public static void fnSetCompensatoryOffApproval(long lCompensatoryId, int iApproved, decimal sApprovehours, string Reason)
        {

            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@id", lCompensatoryId);
            oparam[1] = new SqlParameter("@approved", iApproved);
            oparam[2] = new SqlParameter("@approvedby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[3] = new SqlParameter("@approve_hours", sApprovehours);
            oparam[4] = new SqlParameter("@Reason", ClsCommon.EnsureString(Reason));
            clsDataBaseHelper.ExecuteDataSet("spu_setCompenOffApproval", oparam);

        }

        public static void fnSetOvertimeApproval(long lCompensatoryId, int iApproved, decimal sApprovehours)
        {

            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@id", lCompensatoryId);
            oparam[1] = new SqlParameter("@approved", iApproved);
            oparam[2] = new SqlParameter("@approvedby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[3] = new SqlParameter("@approve_hours", sApprovehours);
            clsDataBaseHelper.ExecuteDataSet("spu_SetOvertimeApproval", oparam);

        }

        public static long fnSetCountry(long lCountryId, string sCountryName, string sDescrip, int iHours, int iMinutes, int Priority, int IsActive)
        {
            long fnSetCountry = 0;
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@id", lCountryId);
            oparam[1] = new SqlParameter("@country_name", ClsCommon.EnsureString(sCountryName));
            oparam[2] = new SqlParameter("@description", ClsCommon.EnsureString(sDescrip));
            oparam[3] = new SqlParameter("@country_hours", iHours);
            oparam[4] = new SqlParameter("@country_minutes", iMinutes);
            oparam[5] = new SqlParameter("@Priority", Priority);
            oparam[6] = new SqlParameter("@IsActive", IsActive);
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetCountry", oparam);

            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetCountry = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetCountry = 0;
            return fnSetCountry;


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

        public static long fnSetEmailTemplate(long ID, string GroupName, string TemplateName, string SMSBody, string Repository, string Body, string Subject, string CCMail, string BCCMail)
        {
            long retID = 0;
            SqlParameter[] oparam = new SqlParameter[11];
            oparam[0] = new SqlParameter("@id", ID);
            oparam[1] = new SqlParameter("@TemplateName", ClsCommon.EnsureString(TemplateName));
            oparam[2] = new SqlParameter("@Body", ClsCommon.EnsureStringSingle(Body));
            oparam[3] = new SqlParameter("@Subject", ClsCommon.EnsureString(Subject));
            oparam[4] = new SqlParameter("@CCMail", ClsCommon.EnsureString(CCMail));
            oparam[5] = new SqlParameter("@BCCMail", ClsCommon.EnsureString(BCCMail));
            oparam[6] = new SqlParameter("@SMSBody", ClsCommon.EnsureString(SMSBody));
            oparam[7] = new SqlParameter("@Repository", ClsCommon.EnsureString(Repository));
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[9] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[10] = new SqlParameter("@GroupName", ClsCommon.EnsureString(GroupName));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetEmailTemplate", oparam);
            if (ds.Tables[0].Rows.Count > 0)
                retID = Convert.ToInt32(ds.Tables[0].Rows[0]["RET_ID"]);
            else
                retID = 0;
            return retID;

        }

        public static long fnSetConfigSetting(long ConfigID, string Category, string SubCategory, string ConfigKey, string ConfigValue, string Remarks, string Help, int Priority)
        {
            long retID = 0;
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@ConfigID", ConfigID);
            oparam[1] = new SqlParameter("@Category", ClsCommon.EnsureString(Category));
            oparam[2] = new SqlParameter("@SubCategory", ClsCommon.EnsureString(SubCategory));
            oparam[3] = new SqlParameter("@ConfigKey", ClsCommon.EnsureString(ConfigKey));
            oparam[4] = new SqlParameter("@ConfigValue", ClsCommon.EnsureString(ConfigValue));
            oparam[5] = new SqlParameter("@Remarks", ClsCommon.EnsureString(Remarks));
            oparam[6] = new SqlParameter("@Help", ClsCommon.EnsureString(Help));
            oparam[7] = new SqlParameter("@Priority", Priority);
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[9] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetConfigSetting", oparam);
            if (ds.Tables[0].Rows.Count > 0)
                retID = Convert.ToInt32(ds.Tables[0].Rows[0]["RET_ID"]);
            else
                retID = 0;
            return retID;
        }

        public static long fnSetUserRole(long RoleID, string RoleName, string Description, int Priority, int IsActive)
        {
            long fnSetUserRole = 0;
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@id", RoleID);
            oparam[1] = new SqlParameter("@Role_Name", RoleName);
            oparam[2] = new SqlParameter("@Description", ClsCommon.EnsureString(Description));
            oparam[3] = new SqlParameter("@Priority", Priority);
            oparam[4] = new SqlParameter("@IsActive", IsActive);
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetUserRole", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetUserRole = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }

            return fnSetUserRole;
        }

        // this is unnessary but using is old code so it is mandiatory to set UserID and Role on role_user_map table - said Ravi
        public static long fnSetRole_User_Map(string lMap_Id, string lUser_id, string lRoleid)
        {
            long fnSetRole_User_Map = 0;
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@id", lMap_Id);
            oparam[1] = new SqlParameter("@user_id", lUser_id);
            oparam[2] = new SqlParameter("@role_id", lRoleid);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetRole_User_Map", oparam);
            if (ds.Tables[0].Rows.Count > 0)
                fnSetRole_User_Map = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            else
                fnSetRole_User_Map = 0;
            return fnSetRole_User_Map;
        }
        public static long fnSetUsers(long ID, string MainRoleID, string MapRoleID, string Username, string Password, string Firstname,
            string Middlename, string Lastname, string Email, long Address, string Theme, int Priority, int IsActive)
        {
            long fnSetUsers = 0;
            SqlParameter[] oparam = new SqlParameter[14];
            oparam[0] = new SqlParameter("@id", ID);
            oparam[1] = new SqlParameter("@user_name", ClsCommon.EnsureString(Username));
            oparam[2] = new SqlParameter("@password", clsApplicationSetting.Encrypt(Password));
            oparam[3] = new SqlParameter("@first_name", ClsCommon.EnsureString(Firstname));
            oparam[4] = new SqlParameter("@middle_name", ClsCommon.EnsureString(Middlename));
            oparam[5] = new SqlParameter("@last_name", ClsCommon.EnsureString(Lastname));
            oparam[6] = new SqlParameter("@email", ClsCommon.EnsureString(Email));
            oparam[7] = new SqlParameter("@address", Address);
            oparam[8] = new SqlParameter("@theme", ClsCommon.EnsureString(Theme));
            oparam[9] = new SqlParameter("@RoleID", ClsCommon.EnsureString(MainRoleID));
            oparam[10] = new SqlParameter("@userid", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[11] = new SqlParameter("@Priority", Priority);
            oparam[12] = new SqlParameter("@IsActive", IsActive);
            oparam[13] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetUsers", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetUsers = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                // fnSetRole_User_Map(MapRoleID, fnSetUsers.ToString(), clsApplicationSetting.GetSessionValue("RoleID"));
            }
            else
                fnSetUsers = 0;
            return fnSetUsers;
        }


        public static long fnSetProjectEMP_SelfMapping(long ID, long ProRegID, string DocType, string Description)
        {
            long fnSetAddress = 0;
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@ProRegID", ProRegID);
            oparam[2] = new SqlParameter("@empID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[3] = new SqlParameter("@DocType", ClsCommon.EnsureString(DocType));
            oparam[4] = new SqlParameter("@Description", ClsCommon.EnsureString(Description));
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetProjectEMP_SelfMapping", oparam);
            if (ds.Tables[0].Rows.Count > 0)
                fnSetAddress = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            else
                fnSetAddress = 0;
            return fnSetAddress;
        }
        public static DataSet fnSetUpdateSrNumber(long EMPID, long Month, long Year)

        {

            SqlParameter[] oparam = new SqlParameter[3];

            oparam[0] = new SqlParameter("@EMPID", EMPID);

            oparam[1] = new SqlParameter("@month", Month);

            oparam[2] = new SqlParameter("@year", Year);

            return clsDataBaseHelper.ExecuteDataSet("spu_SetDailyLogUpdateSrNo", oparam);

        }
        public static long fnSetDailyLog(string lDailyLogId, int iMonth, int iYear, string lProjId, string sDescription, double sDay1, double sDay2, double sDay3, double sDay4,
           double sDay5, double sDay6, double sDay7, double sDay8, double sDay9, double sDay10, double sDay11, double sDay12, double sDay13, double sDay14, double sDay15,
           double sDay16, double sDay17, double sDay18, double sDay19, double sDay20, double sDay21, double sDay22, double sDay23, double sDay24, double sDay25, double sDay26, double sDay27,
           double sDay28, double sDay29, double sDay30, double sDay31, double sTotal, int srno, long TravelrequestID, long ActivityID = 0)
        {
            SqlParameter[] oparam = new SqlParameter[42];
            oparam[0] = new SqlParameter("@id", lDailyLogId);
            oparam[1] = new SqlParameter("@emp_id", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@month", iMonth);
            oparam[3] = new SqlParameter("@year", iYear);
            oparam[4] = new SqlParameter("@proj_id", lProjId);
            oparam[5] = new SqlParameter("@description", ClsCommon.EnsureString(sDescription));
            oparam[6] = new SqlParameter("@day1", sDay1);
            oparam[7] = new SqlParameter("@day2", sDay2);
            oparam[8] = new SqlParameter("@day3", sDay3);
            oparam[9] = new SqlParameter("@day4", sDay4);
            oparam[10] = new SqlParameter("@day5", sDay5);
            oparam[11] = new SqlParameter("@day6", sDay6);
            oparam[12] = new SqlParameter("@day7", sDay7);
            oparam[13] = new SqlParameter("@day8", sDay8);
            oparam[14] = new SqlParameter("@day9", sDay9);
            oparam[15] = new SqlParameter("@day10", sDay10);
            oparam[16] = new SqlParameter("@day11", sDay11);
            oparam[17] = new SqlParameter("@day12", sDay12);
            oparam[18] = new SqlParameter("@day13", sDay13);
            oparam[19] = new SqlParameter("@day14", sDay14);
            oparam[20] = new SqlParameter("@day15", sDay15);
            oparam[21] = new SqlParameter("@day16", sDay16);
            oparam[22] = new SqlParameter("@day17", sDay17);
            oparam[23] = new SqlParameter("@day18", sDay18);
            oparam[24] = new SqlParameter("@day19", sDay19);
            oparam[25] = new SqlParameter("@day20", sDay20);
            oparam[26] = new SqlParameter("@day21", sDay21);
            oparam[27] = new SqlParameter("@day22", sDay22);
            oparam[28] = new SqlParameter("@day23", sDay23);
            oparam[29] = new SqlParameter("@day24", sDay24);
            oparam[30] = new SqlParameter("@day25", sDay25);
            oparam[31] = new SqlParameter("@day26", sDay26);
            oparam[32] = new SqlParameter("@day27", sDay27);
            oparam[33] = new SqlParameter("@day28", sDay28);
            oparam[34] = new SqlParameter("@day29", sDay29);
            oparam[35] = new SqlParameter("@day30", sDay30);
            oparam[36] = new SqlParameter("@day31", sDay31);
            oparam[37] = new SqlParameter("@Total", sTotal);
            oparam[38] = new SqlParameter("@srno", srno);
            oparam[39] = new SqlParameter("@ActivityID", ActivityID);
            oparam[40] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[41] = new SqlParameter("@TravelrequestID", TravelrequestID);
            //clsDataBaseHelper.ExecuteDataSet("spu_SetDailyLogWithActivity", oparam);
            clsDataBaseHelper.ExecuteDataSet("spu_SetDailyLogWithActivityNew", oparam);
            return 1;

        }


        public static long fnSetActiveLog(string lActiveLogId, string lempId, string ActivityID, string description, string iMonth, string iYear, string email_cc, string lProjId, string sDay1, string sDay2, string sDay3, string sDay4,
          string sDay5, string sDay6, string sDay7, string sDay8, string sDay9, string sDay10, string sDay11, string sDay12, string sDay13, string sDay14, string sDay15,
          string sDay16, string sDay17, string sDay18, string sDay19, string sDay20, string sDay21, string sDay22, string sDay23, string sDay24, string sDay25, string sDay26, string sDay27,
          string sDay28, string sDay29, string sDay30, string sDay31, string sTotal, int srno, string sTotalhour, string sLeaveAvailed, string sCompenOff)
        {
            SqlParameter[] oparam = new SqlParameter[45];
            oparam[0] = new SqlParameter("@id", lActiveLogId);
            oparam[1] = new SqlParameter("@emp_id", lempId);
            oparam[2] = new SqlParameter("@month", iMonth);
            oparam[3] = new SqlParameter("@Year", iYear);
            oparam[4] = new SqlParameter("@proj_id", lProjId);
            oparam[5] = new SqlParameter("@day1", ClsCommon.EnsureHours(sDay1));
            oparam[6] = new SqlParameter("@day2", ClsCommon.EnsureHours(sDay2));
            oparam[7] = new SqlParameter("@day3", ClsCommon.EnsureHours(sDay3));
            oparam[8] = new SqlParameter("@day4", ClsCommon.EnsureHours(sDay4));
            oparam[9] = new SqlParameter("@day5", ClsCommon.EnsureHours(sDay5));
            oparam[10] = new SqlParameter("@day6", ClsCommon.EnsureHours(sDay6));
            oparam[11] = new SqlParameter("@day7", ClsCommon.EnsureHours(sDay7));
            oparam[12] = new SqlParameter("@day8", ClsCommon.EnsureHours(sDay8));
            oparam[13] = new SqlParameter("@day9", ClsCommon.EnsureHours(sDay9));
            oparam[14] = new SqlParameter("@day10", ClsCommon.EnsureHours(sDay10));
            oparam[15] = new SqlParameter("@day11", ClsCommon.EnsureHours(sDay11));
            oparam[16] = new SqlParameter("@day12", ClsCommon.EnsureHours(sDay12));
            oparam[17] = new SqlParameter("@day13", ClsCommon.EnsureHours(sDay13));
            oparam[18] = new SqlParameter("@day14", ClsCommon.EnsureHours(sDay14));
            oparam[19] = new SqlParameter("@day15", ClsCommon.EnsureHours(sDay15));
            oparam[20] = new SqlParameter("@day16", ClsCommon.EnsureHours(sDay16));
            oparam[21] = new SqlParameter("@day17", ClsCommon.EnsureHours(sDay17));
            oparam[22] = new SqlParameter("@day18", ClsCommon.EnsureHours(sDay18));
            oparam[23] = new SqlParameter("@day19", ClsCommon.EnsureHours(sDay19));
            oparam[24] = new SqlParameter("@day20", ClsCommon.EnsureHours(sDay20));
            oparam[25] = new SqlParameter("@day21", ClsCommon.EnsureHours(sDay21));
            oparam[26] = new SqlParameter("@day22", ClsCommon.EnsureHours(sDay22));
            oparam[27] = new SqlParameter("@day23", ClsCommon.EnsureHours(sDay23));
            oparam[28] = new SqlParameter("@day24", ClsCommon.EnsureHours(sDay24));
            oparam[29] = new SqlParameter("@day25", ClsCommon.EnsureHours(sDay25));
            oparam[30] = new SqlParameter("@day26", ClsCommon.EnsureHours(sDay26));
            oparam[31] = new SqlParameter("@day27", ClsCommon.EnsureHours(sDay27));
            oparam[32] = new SqlParameter("@day28", ClsCommon.EnsureHours(sDay28));
            oparam[33] = new SqlParameter("@day29", ClsCommon.EnsureHours(sDay29));
            oparam[34] = new SqlParameter("@day30", ClsCommon.EnsureHours(sDay30));
            oparam[35] = new SqlParameter("@day31", ClsCommon.EnsureHours(sDay31));
            oparam[36] = new SqlParameter("@Total", ClsCommon.EnsureHours(sTotal));
            oparam[37] = new SqlParameter("@srno", srno);
            oparam[38] = new SqlParameter("@email_cc", ClsCommon.EnsureString(email_cc));
            oparam[39] = new SqlParameter("@total_hours", sTotalhour);
            oparam[40] = new SqlParameter("@leave_availed", sLeaveAvailed);
            oparam[41] = new SqlParameter("@compen_off", sCompenOff);
            oparam[42] = new SqlParameter("@ActivityID", ActivityID);
            oparam[43] = new SqlParameter("@description", ClsCommon.EnsureString(description));
            oparam[44] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteDataSet("spu_SetActiveLog", oparam);
            return 1;

        }



        public static void fnSetActiveApprove(long lActiveApproveId, long lEmpId, int iMonth, int lYear, long lApprovedBy, string sRemark, string Command)
        {

            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@Id", lActiveApproveId);
            oparam[1] = new SqlParameter("@emp_id", lEmpId);
            oparam[2] = new SqlParameter("@month", iMonth);
            oparam[3] = new SqlParameter("@year", lYear);
            oparam[4] = new SqlParameter("@approvedby", lApprovedBy);
            oparam[5] = new SqlParameter("@remarks", ClsCommon.EnsureString(sRemark));
            oparam[6] = new SqlParameter("@Command", ClsCommon.EnsureString(Command));
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteDataSet("spu_SetActive_Approve", oparam);

        }


        public static void fnProcessActiveLog(long lEmpID, int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@empid", lEmpID);
            oparam[1] = new SqlParameter("@month", iMonth);
            oparam[2] = new SqlParameter("@Year", iYear);
            clsDataBaseHelper.ExecuteDataSet("spu_ProcessActiveLog", oparam);

        }

        public static int fnSetTimesheetDet(long lTSLogID, long lProjID, decimal dWHrs, decimal dAdjHrs, decimal dWValue, decimal dTHrs, decimal dTValue)
        {
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@TimeSheetlog_id", lTSLogID);
            oparam[1] = new SqlParameter("@proj_id", lProjID);
            oparam[2] = new SqlParameter("@work_hours", dWHrs);
            oparam[3] = new SqlParameter("@adj_hours", dAdjHrs);
            oparam[4] = new SqlParameter("@work_value", Math.Round(dWValue, 0, MidpointRounding.AwayFromZero));
            oparam[5] = new SqlParameter("@travel_hours", dTHrs);
            oparam[6] = new SqlParameter("@travel_value", Math.Round(dTValue, 0, MidpointRounding.AwayFromZero));
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteDataSet("spu_SetTimesheetDet", oparam);
            return 1;
        }

        public static long fnSetAdjustmentActiveLog(long lEmpID, int iMonth, int iYear)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@empid", lEmpID);
            oparam[1] = new SqlParameter("@month", iMonth);
            oparam[2] = new SqlParameter("@year", iYear);
            clsDataBaseHelper.ExecuteDataSet("spu_SetAdjActiveLog", oparam);
            return 1;
        }
        public static long fnSetTimesheetLog(long lEmpId, decimal dAdjustHrs, decimal dOTHrs, decimal dALHrs, decimal dPLHrs, decimal dHDHrs, decimal dPaidHrs, decimal dPHValue, decimal dTotalValue, decimal dNPLHrs, decimal dTotalHrs, int iMonth, int iYear)
        {
            long fnSetTimesheetLog = 0;
            SqlParameter[] oparam = new SqlParameter[14];
            oparam[0] = new SqlParameter("@Emp_Id", lEmpId);
            oparam[1] = new SqlParameter("@adjust_hours", dAdjustHrs);
            oparam[2] = new SqlParameter("@ot_hours", dOTHrs);
            oparam[3] = new SqlParameter("@al_hours", dALHrs);
            oparam[4] = new SqlParameter("@pl_hours", dPLHrs);
            oparam[5] = new SqlParameter("@hd_hours", dHDHrs);
            oparam[6] = new SqlParameter("@Paid_hours", dPaidHrs);
            oparam[7] = new SqlParameter("@ph_value", dPHValue);
            oparam[8] = new SqlParameter("@total_value", Math.Round(dTotalValue, 0, MidpointRounding.AwayFromZero));
            oparam[9] = new SqlParameter("@npl_hours", dNPLHrs);
            oparam[10] = new SqlParameter("@total_hours", dTotalHrs);
            oparam[11] = new SqlParameter("@Month", iMonth);
            oparam[12] = new SqlParameter("@year", iYear);
            oparam[13] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTimesheet", oparam);
            if (ds.Tables[0].Rows.Count > 0)
                fnSetTimesheetLog = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);

            return fnSetTimesheetLog;
        }



        public static long fnSetLeaveApprovalHOD(long lLeaveLogId, int lApproved, string HODRemarks, string EDRemarks)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@id", lLeaveLogId);
            oparam[1] = new SqlParameter("@Approved", lApproved);
            oparam[2] = new SqlParameter("@HODRemarks", ClsCommon.EnsureString(HODRemarks));
            oparam[3] = new SqlParameter("@EDRemarks", ClsCommon.EnsureString(EDRemarks));
            clsDataBaseHelper.ExecuteSp("spu_setLeaveApproval", oparam);
            return 1;

        }
        public static void fnsetRFC_LeaveRequest(long LeaveLogID, string RFCRemarks)
        {

            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@LeaveLogID", LeaveLogID);
            oparam[1] = new SqlParameter("@RFCRemarks", ClsCommon.EnsureString(RFCRemarks));
            clsDataBaseHelper.ExecuteDataSet("spu_setRFC_LeaveRequest", oparam);
        }
        public static void fnSetRFC_LeaveRequest_Approve(long LeaveLogID)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@LeaveLogID", LeaveLogID);
            oparam[1] = new SqlParameter("@ApprovedByEMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            clsDataBaseHelper.ExecuteDataSet("spu_SetRFC_LeaveRequest_Approve", oparam);
        }


        public static void fnSetRecallAcivityLog(int iMonth, int iYear, string RecallRemarks)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@empid", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@month", iMonth);
            oparam[2] = new SqlParameter("@Year", iYear);
            oparam[3] = new SqlParameter("@RecallRemarks", ClsCommon.EnsureString(RecallRemarks));
            clsDataBaseHelper.ExecuteDataSet("spu_SetRecallActiveLog", oparam);
        }
        public static PostResponse FnSetFrProspectImport(string Token, string ProspectName, string ProspectType, string Sector, string Company, string Country, string KFA, string State, string Budget, string C3Fit, string C3FitScore, string Responsible, string Accountable, string InformedTo, string Consented, string Website, string OtherSupport, string Comment, string WebLink, string ContactPerson, string Designation, string ContactNo, string EmailId, string LinkedInId, string ContactAddress, string SecratoryName, string SecratoryPhone, string SecratoryEmail, string SecratoryOtherInfo, int IsLast)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFR_ProspectImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Token", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Token);
                        command.Parameters.Add("@ProspectName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(ProspectName);
                        command.Parameters.Add("@ProspectType", SqlDbType.VarChar).Value = ClsCommon.EnsureString(ProspectType);
                        command.Parameters.Add("@Sector", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Sector);
                        command.Parameters.Add("@Company", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Company);
                        command.Parameters.Add("@Country", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Country);
                        command.Parameters.Add("@KFA", SqlDbType.VarChar).Value = ClsCommon.EnsureString(KFA);
                        command.Parameters.Add("@State", SqlDbType.VarChar).Value = ClsCommon.EnsureString(State);
                        command.Parameters.Add("@Budget", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Budget);
                        command.Parameters.Add("@C3Fit", SqlDbType.VarChar).Value = ClsCommon.EnsureString(C3Fit);
                        command.Parameters.Add("@C3FitScore", SqlDbType.VarChar).Value = ClsCommon.EnsureString(C3FitScore);
                        command.Parameters.Add("@Responsible", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Responsible);
                        command.Parameters.Add("@Accountable", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Accountable);
                        command.Parameters.Add("@InformedTo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(InformedTo);
                        command.Parameters.Add("@Consented", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Consented);
                        command.Parameters.Add("@Website", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Website);
                        command.Parameters.Add("@OtherSupport", SqlDbType.VarChar).Value = ClsCommon.EnsureString(OtherSupport);
                        command.Parameters.Add("@Comment", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Comment);
                        command.Parameters.Add("@WebLink", SqlDbType.VarChar).Value = ClsCommon.EnsureString(WebLink);
                        command.Parameters.Add("@ContactPerson", SqlDbType.VarChar).Value = ClsCommon.EnsureString(ContactPerson);
                        command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Designation);
                        command.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(ContactNo);
                        command.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = ClsCommon.EnsureString(EmailId);
                        command.Parameters.Add("@LinkedInId", SqlDbType.VarChar).Value = ClsCommon.EnsureString(LinkedInId);
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = ClsCommon.EnsureString(ContactAddress);
                        command.Parameters.Add("@SecratoryName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SecratoryName);
                        command.Parameters.Add("@SecratoryPhone", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SecratoryPhone);
                        command.Parameters.Add("@SecratoryEmail", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SecratoryEmail);
                        command.Parameters.Add("@SecratoryOtherInfo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SecratoryOtherInfo);
                        command.Parameters.Add("@IsLast", SqlDbType.Int).Value = IsLast;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during FnSetFrProspectImport. The query was executed :", ex.ToString(), "spu_SetFR_ProspectImport", "Common", "Common", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }
        public static PostResponse FnSetLeaveOpeningImport(string Token, string EmpName, string Category, string EmpCode, string Designation, string Department, string Location, string Gender, string JoiningDate, string LeaveType, string OpeningBalance, string AllotedHours, string StartDate, int IsLast)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetLeaveOpening_Import", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Token", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Token);
                        command.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(EmpName);
                        command.Parameters.Add("@Category", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Category);
                        command.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(EmpCode);
                        command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Designation);
                        command.Parameters.Add("@Department", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Department);
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Location);
                        command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Gender);
                        command.Parameters.Add("@JoiningDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(JoiningDate);
                        command.Parameters.Add("@LeaveType", SqlDbType.VarChar).Value = ClsCommon.EnsureString(LeaveType);
                        command.Parameters.Add("@OpeningBalance", SqlDbType.VarChar).Value = ClsCommon.EnsureString(OpeningBalance);
                        command.Parameters.Add("@AllotedHours", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AllotedHours);
                        command.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(StartDate);
                        command.Parameters.Add("@IsLast", SqlDbType.Int).Value = IsLast;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during FnSetLeaveOpeningImport. The query was executed :", ex.ToString(), "spu_SetLeaveOpening_Import", "Common", "Common", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }
        public static long fnSetLeaveNonMitr(long id, int Empid, DateTime LeaveDate, int Leaveid, int LeaveHours, string DateSel, int AttachmentRequired, string ExpectedDeliveryDate, double sDay1, double sDay2, double sDay3, double sDay4,
          double sDay5, double sDay6, double sDay7, double sDay8, double sDay9, double sDay10, double sDay11, double sDay12, double sDay13, double sDay14, double sDay15,
          double sDay16, double sDay17, double sDay18, double sDay19, double sDay20, double sDay21, double sDay22, double sDay23, double sDay24, double sDay25, double sDay26, double sDay27,
          double sDay28, double sDay29, double sDay30, double sDay31, int IsAttachment = 0)
        {
            SqlParameter[] oparam = new SqlParameter[41];
            oparam[0] = new SqlParameter("@ID", id);
            oparam[1] = new SqlParameter("@emp_id", Empid);
            oparam[2] = new SqlParameter("@Date", LeaveDate);
            oparam[3] = new SqlParameter("@leave_id", Leaveid);
            oparam[4] = new SqlParameter("@leave_hours", LeaveHours);
            oparam[5] = new SqlParameter("@datesel", DateSel);
            oparam[6] = new SqlParameter("@AttachmentRequired", AttachmentRequired);
            oparam[7] = new SqlParameter("@ExpectedDeliveryDate", ExpectedDeliveryDate);
            oparam[8] = new SqlParameter("@Day1", sDay1);
            oparam[9] = new SqlParameter("@Day2", sDay2);
            oparam[10] = new SqlParameter("@Day3", sDay3);
            oparam[11] = new SqlParameter("@Day4", sDay4);
            oparam[12] = new SqlParameter("@Day5", sDay5);
            oparam[13] = new SqlParameter("@Day6", sDay6);
            oparam[14] = new SqlParameter("@Day7", sDay7);
            oparam[15] = new SqlParameter("@Day8", sDay8);
            oparam[16] = new SqlParameter("@Day9", sDay9);
            oparam[17] = new SqlParameter("@Day10", sDay10);
            oparam[18] = new SqlParameter("@Day11", sDay11);
            oparam[19] = new SqlParameter("@Day12", sDay12);
            oparam[20] = new SqlParameter("@Day13", sDay13);
            oparam[21] = new SqlParameter("@Day14", sDay14);
            oparam[22] = new SqlParameter("@Day15", sDay15);
            oparam[23] = new SqlParameter("@Day16", sDay16);
            oparam[24] = new SqlParameter("@Day17", sDay17);
            oparam[25] = new SqlParameter("@Day18", sDay18);
            oparam[26] = new SqlParameter("@Day19", sDay19);
            oparam[27] = new SqlParameter("@Day20", sDay20);
            oparam[28] = new SqlParameter("@Day21", sDay21);
            oparam[29] = new SqlParameter("@Day22", sDay22);
            oparam[30] = new SqlParameter("@Day23", sDay23);
            oparam[31] = new SqlParameter("@Day24", sDay24);
            oparam[32] = new SqlParameter("@Day25", sDay25);
            oparam[33] = new SqlParameter("@Day26", sDay26);
            oparam[34] = new SqlParameter("@Day27", sDay27);
            oparam[35] = new SqlParameter("@Day28", sDay28);
            oparam[36] = new SqlParameter("@Day29", sDay29);
            oparam[37] = new SqlParameter("@Day30", sDay30);
            oparam[38] = new SqlParameter("@Day31", sDay31);
            oparam[39] = new SqlParameter("@IsAttachment", IsAttachment);
            oparam[40] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteDataSet("spu_SetLeaveLogNoNMITR", oparam);
            return 1;
        }
        public static long fnSetDailyLogNonMitr(string lDailyLogId, long Empid, int iMonth, int iYear, string lProjId, string sDescription, double sDay1, double sDay2, double sDay3, double sDay4,
           double sDay5, double sDay6, double sDay7, double sDay8, double sDay9, double sDay10, double sDay11, double sDay12, double sDay13, double sDay14, double sDay15,
           double sDay16, double sDay17, double sDay18, double sDay19, double sDay20, double sDay21, double sDay22, double sDay23, double sDay24, double sDay25, double sDay26, double sDay27,
           double sDay28, double sDay29, double sDay30, double sDay31, double sTotal, int srno, long ActivityID = 0)
        {
            SqlParameter[] oparam = new SqlParameter[42];
            oparam[0] = new SqlParameter("@id", lDailyLogId);
            oparam[1] = new SqlParameter("@emp_id", Empid);
            oparam[2] = new SqlParameter("@month", iMonth);
            oparam[3] = new SqlParameter("@year", iYear);
            oparam[4] = new SqlParameter("@proj_id", lProjId);
            oparam[5] = new SqlParameter("@description", ClsCommon.EnsureString(sDescription));
            oparam[6] = new SqlParameter("@day1", sDay1);
            oparam[7] = new SqlParameter("@day2", sDay2);
            oparam[8] = new SqlParameter("@day3", sDay3);
            oparam[9] = new SqlParameter("@day4", sDay4);
            oparam[10] = new SqlParameter("@day5", sDay5);
            oparam[11] = new SqlParameter("@day6", sDay6);
            oparam[12] = new SqlParameter("@day7", sDay7);
            oparam[13] = new SqlParameter("@day8", sDay8);
            oparam[14] = new SqlParameter("@day9", sDay9);
            oparam[15] = new SqlParameter("@day10", sDay10);
            oparam[16] = new SqlParameter("@day11", sDay11);
            oparam[17] = new SqlParameter("@day12", sDay12);
            oparam[18] = new SqlParameter("@day13", sDay13);
            oparam[19] = new SqlParameter("@day14", sDay14);
            oparam[20] = new SqlParameter("@day15", sDay15);
            oparam[21] = new SqlParameter("@day16", sDay16);
            oparam[22] = new SqlParameter("@day17", sDay17);
            oparam[23] = new SqlParameter("@day18", sDay18);
            oparam[24] = new SqlParameter("@day19", sDay19);
            oparam[25] = new SqlParameter("@day20", sDay20);
            oparam[26] = new SqlParameter("@day21", sDay21);
            oparam[27] = new SqlParameter("@day22", sDay22);
            oparam[28] = new SqlParameter("@day23", sDay23);
            oparam[29] = new SqlParameter("@day24", sDay24);
            oparam[30] = new SqlParameter("@day25", sDay25);
            oparam[31] = new SqlParameter("@day26", sDay26);
            oparam[32] = new SqlParameter("@day27", sDay27);
            oparam[33] = new SqlParameter("@day28", sDay28);
            oparam[34] = new SqlParameter("@day29", sDay29);
            oparam[35] = new SqlParameter("@day30", sDay30);
            oparam[36] = new SqlParameter("@day31", sDay31);
            oparam[37] = new SqlParameter("@Total", sTotal);
            oparam[38] = new SqlParameter("@srno", srno);
            oparam[39] = new SqlParameter("@ActivityID", ActivityID);
            oparam[40] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[41] = new SqlParameter("@TravelrequestID", 0);
            clsDataBaseHelper.ExecuteDataSet("spu_SetDailyLogWithActivity", oparam);
            return 1;

        }
        public static long FnSetReverseNonMitr(long ID, long lEmpId, int iMonth, int lYear, string Doctype)
        {

            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@emp_id", lEmpId);
            oparam[2] = new SqlParameter("@month", iMonth);
            oparam[3] = new SqlParameter("@year", lYear);
            oparam[4] = new SqlParameter("@Doctype", Doctype);
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            clsDataBaseHelper.ExecuteDataSet("spu_SetReverseNonMitr", oparam);
            return 1;
        }
        public static long FNSaveSalaryComponentAmtFromConsolidate(long Empid, string Date)
        {
            long retid = 0;
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Empid", Empid);
            oparam[1] = new SqlParameter("@Month", Convert.ToDateTime(Date).Month);
            oparam[2] = new SqlParameter("@Year", Convert.ToDateTime(Date).Year);
            oparam[3] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetSS_EmpComponentAmt", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                retid = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                retid = 0;
            return retid;
        }

        public static PostResponse fnDelRecord_Common(GetResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_DelRecord_Common", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID;
                        command.Parameters.Add("@AdditionalID", SqlDbType.Int).Value = Modal.AdditionalID;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnDelRecord_Common. The query was executed :", ex.ToString(), "spu_DelRecord_Common", "Common_SPU", "Common_SPU", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static PostResponse fnDelRecord_CommonTraining(GetResponse Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_DelRecord_CommonTrain", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = Modal.ID;
                        command.Parameters.Add("@AdditionalID", SqlDbType.Int).Value = Modal.AdditionalID;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.Parameters.Add("@Reason", SqlDbType.VarChar).Value = Modal.Reason;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnDelRecord_Common. The query was executed :", ex.ToString(), "spu_DelRecord_Common", "Common_SPU", "Common_SPU", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }
        public static PostResponse SetLoginUsers(UserMan.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    string first_name = "", middle_name = "", last_name = "";
                    if (!string.IsNullOrEmpty(modal.Name))
                    {
                        if (modal.Name.Contains(' '))
                        {
                            var a = modal.Name.Split(' ');
                            if (a.Length == 3)
                            {
                                first_name = a[0];
                                middle_name = a[1];
                                last_name = a[2];
                            }
                            else if (a.Length == 2)
                            {
                                first_name = a[0];
                                last_name = a[1];
                            }
                            else
                            {
                                first_name = a[0];
                            }
                        }
                        else
                        {
                            first_name = modal.Name;
                        }
                    }

                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUsers", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = modal.UserType ?? "";
                        command.Parameters.Add("@user_name", SqlDbType.VarChar).Value = modal.user_name ?? "";
                        command.Parameters.Add("@password", SqlDbType.VarChar).Value = clsApplicationSetting.Encrypt(modal.password) ?? "";
                        command.Parameters.Add("@first_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(first_name);
                        command.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(middle_name);
                        command.Parameters.Add("@last_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(last_name);
                        command.Parameters.Add("@email", SqlDbType.VarChar).Value = modal.email ?? "";
                        command.Parameters.Add("@RoleIDs", SqlDbType.VarChar).Value = modal.RoleIDs ?? "";
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public static PostResponse SetLoginUsersMapping(UserMan.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    string first_name = "", middle_name = "", last_name = "";
                    if (!string.IsNullOrEmpty(modal.Name))
                    {
                        if (modal.Name.Contains(' '))
                        {
                            var a = modal.Name.Split(' ');
                            if (a.Length == 3)
                            {
                                first_name = a[0];
                                middle_name = a[1];
                                last_name = a[2];
                            }
                            else if (a.Length == 2)
                            {
                                first_name = a[0];
                                last_name = a[1];
                            }
                            else
                            {
                                first_name = a[0];
                            }
                        }
                        else
                        {
                            first_name = modal.Name;
                        }
                    }

                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_Onboarding_SetUsersMapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = modal.UserType ?? "";
                        command.Parameters.Add("@user_name", SqlDbType.VarChar).Value = modal.user_name ?? "";
                        command.Parameters.Add("@password", SqlDbType.VarChar).Value = clsApplicationSetting.Encrypt(modal.password) ?? "";
                        command.Parameters.Add("@first_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(first_name);
                        command.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(middle_name);
                        command.Parameters.Add("@last_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(last_name);
                        command.Parameters.Add("@email", SqlDbType.VarChar).Value = modal.email ?? "";
                        command.Parameters.Add("@RoleIDs", SqlDbType.VarChar).Value = modal.RoleIDs ?? "";
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public static PostResponse SetLoginOnboardingUsers(UserMan.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    string first_name = "", middle_name = "", last_name = "";
                    if (!string.IsNullOrEmpty(modal.Name))
                    {
                        if (modal.Name.Contains(' '))
                        {
                            var a = modal.Name.Split(' ');
                            if (a.Length == 3)
                            {
                                first_name = a[0];
                                middle_name = a[1];
                                last_name = a[2];
                            }
                            else if (a.Length == 2)
                            {
                                first_name = a[0];
                                last_name = a[1];
                            }
                            else
                            {
                                first_name = a[0];
                            }
                        }
                        else
                        {
                            first_name = modal.Name;
                        }
                    }

                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetOnboardingUsers", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = modal.UserType ?? "";
                        command.Parameters.Add("@user_name", SqlDbType.VarChar).Value = modal.user_name ?? "";
                        command.Parameters.Add("@password", SqlDbType.VarChar).Value = clsApplicationSetting.Encrypt(modal.password) ?? "";
                        command.Parameters.Add("@first_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(first_name);
                        command.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(middle_name);
                        command.Parameters.Add("@last_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(last_name);
                        command.Parameters.Add("@email", SqlDbType.VarChar).Value = modal.email ?? "";
                        command.Parameters.Add("@RoleIDs", SqlDbType.VarChar).Value = modal.RoleIDs ?? "";
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.Parameters.Add("@IsDeleted", SqlDbType.Int).Value = 0;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public static PostResponse fnSetPasswordChange(ChangePassword Modal)
        {
            PostResponse result = new PostResponse();


            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPasswordChange", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OldPassword", SqlDbType.VarChar).Value = Modal.OldPassword ?? "";
                        command.Parameters.Add("@NewPassword", SqlDbType.VarChar).Value = Modal.NewPassword ?? "";
                        command.Parameters.Add("@Token", SqlDbType.VarChar).Value = Modal.Token ?? "";
                        command.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.ID > 0)
                                {
                                    result.Status = true;
                                    result.SuccessMessage = "Password Changed'";
                                }
                                else
                                {
                                    result.SuccessMessage = "Password not Changed'";
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnCreateMail_ForgotPassword. The query was executed :", ex.ToString(), "spu_CreateMail_ForgotPassword", "BoardModal", "BoardModal", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }
        public static PostResponse fnSetPasswordChangeForMobile(ChangePassword Modal)
        {
            PostResponse result = new PostResponse();


            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetPasswordChangeForMobile", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OldPassword", SqlDbType.VarChar).Value = Modal.OldPassword ?? "";
                        command.Parameters.Add("@NewPassword", SqlDbType.VarChar).Value = Modal.NewPassword ?? "";
                        command.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.ID > 0)
                                {
                                    result.Status = true;
                                    result.SuccessMessage = "Password Changed'";
                                }
                                else
                                {
                                    result.SuccessMessage = "Password not Changed'";
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnCreateMail_ForgotPassword. The query was executed :", ex.ToString(), "spu_CreateMail_ForgotPassword", "BoardModal", "BoardModal", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        //public static PostResponse fnSetPasswordChange(ChangePassword Modal)
        //{
        //    PostResponse result = new PostResponse();


        //    using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
        //    {
        //        try
        //        {
        //            con.Open();
        //            using (SqlCommand command = new SqlCommand("spu_SetPasswordChange", con))
        //            {
        //                SqlDataAdapter da = new SqlDataAdapter();
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@OldPassword", SqlDbType.VarChar).Value = Modal.OldPassword ?? "";
        //                command.Parameters.Add("@NewPassword", SqlDbType.VarChar).Value = Modal.NewPassword ?? "";
        //                command.Parameters.Add("@Token", SqlDbType.VarChar).Value = Modal.Token ?? "";
        //                command.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = Modal.LoginID;
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
        //            ClsCommon.LogError("Error during fnCreateMail_ForgotPassword. The query was executed :", ex.ToString(), "spu_CreateMail_ForgotPassword", "BoardModal", "BoardModal", "");


        //            result.StatusCode = -1;
        //            result.SuccessMessage = ex.Message.ToString();
        //        }
        //    }
        //    return result;

        //}

        public static PostResponse FnSetSubcateImportAssign(string Token, string CategoryName, string SubCategoryName, string Location, string Primary, string Levelone, string Leveltwo, string Levelthree, int IsLast)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSubCateGriAssignImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Token", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Token);
                        command.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(CategoryName);
                        command.Parameters.Add("@SubCategoryName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SubCategoryName);
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Location);
                        command.Parameters.Add("@PA", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Primary);
                        command.Parameters.Add("@LOne", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Levelone);
                        command.Parameters.Add("@LTwo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Leveltwo);
                        command.Parameters.Add("@LThree", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Levelthree);
                        command.Parameters.Add("@IsLast", SqlDbType.Int).Value = IsLast;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during FnSetEmployeeImport. The query was executed :", ex.ToString(), "spu_SetMaster_empImport", "Common", "Common", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static PostResponse FnSetSubcateImport(string Token, string CategoryName, string SubCategoryName, string Description, string Anonymous, int IsLast)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSubCateGriImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Token", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Token);
                        command.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(CategoryName);
                        command.Parameters.Add("@SubCategoryName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SubCategoryName);
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Description);
                        command.Parameters.Add("@Anonymous", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Anonymous);
                        command.Parameters.Add("@IsLast", SqlDbType.Int).Value = IsLast;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during FnSetEmployeeImport. The query was executed :", ex.ToString(), "spu_SetMaster_empImport", "Common", "Common", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public static PostResponse FnSetEmployeeImport(string Token, string emp_code, string emp_name, string email, string MitrUser, string doj, string EmploymentTerm, string Contract_EndDate, string DepartmentID, string thematicarea_IDs, string WorkLocationID, string metro, string design_id, string JobID, string emp_status, string Probation_EndDate, string doc, string hod_name, string SecondaryHODID, string ed_name, string HRID, string AppraiserID, string co_ot, string ResidentialStatus, string SkillsIDs, string NoticePeriod, string dor, string lastworking_day, string BankIDS, string AccountTypeS, string AccountNameS, string AccountNoS, string BranchNameS, string BranchAddressS, string IFSCCodeS,
                            string SwiftCodeS, string OtherDetailsS, string BankIDR, string AccountTypeR, string AccountNameR, string AccountNoR, string BranchNameR, string BranchAddressR, string IFSCCodeR, string SwiftCodeR, string OtherDetailsR, string PsychometricTest, string father_name, string mother_name, string gender, string dob, string marital_status, string SpouseName, string PartnerName, string NomineeName, string NomineeRelation, string children, string Nationality, string lane1P1, string lane2P1, string zip_codeP1, string CountryIDP1, string StateIDP1, string CityIDP1, string lane1P2, string lane2P2, string zip_codeP2, string CountryIDP2, string StateIDP2,
                            string CityIDP2, string SpecialAbility, string AnyMedicalCondition, string PhysicianName, string PhysicianNumber, string PhysicianAlternate_No, string BloodGroup, string emergContact_no, string emergContact_Name, string emergContact_Relation, string CourseE, string UniversityE, string LocationE, string YearE, string CourseP, string UniversityP, string LocationP, string YearP, string aireline_no, string frequentflyer_no, string seat_preference, string MealPreference, string CompanyName, string DOJLE, string DORLE, string TotalExperence, string AnnualCTC, string Designation, string Location, string EmploymentTermLE, string ShareSomething,
                            string IsConsiderIncome, string IncomeAmount, string TDSDeduction, string pan_no, string NameonPAN, string AadharNo, string NameonAadhar, string Voterno, string PassportNo, string NameonPassport, string PlaceofIssue, string PassportExpiryDate, string DirectorIdentificationNumber, string DIN, string NameonDIN, string UAN, string NameonUAN, string OldPF, string OldPFNo, string PIO_OCI, string NameonPIO_OCI, string DrivingLicense, string DLNo, string NameonDL, string IssueDate, string ExpiryDate, string PlaceofIssueID, string MobileP1, string MobileP2, int IsLast)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMaster_empImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Token", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Token);
                        command.Parameters.Add("@emp_code", SqlDbType.VarChar).Value = ClsCommon.EnsureString(emp_code);
                        command.Parameters.Add("@emp_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(emp_name);
                        command.Parameters.Add("@email", SqlDbType.VarChar).Value = ClsCommon.EnsureString(email);
                        command.Parameters.Add("@MitrUser", SqlDbType.VarChar).Value = ClsCommon.EnsureString(MitrUser);
                        command.Parameters.Add("@doj", SqlDbType.VarChar).Value = ClsCommon.EnsureString(doj);
                        command.Parameters.Add("@EmploymentTerm", SqlDbType.VarChar).Value = ClsCommon.EnsureString(EmploymentTerm);
                        command.Parameters.Add("@Contract_EndDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Contract_EndDate);
                        command.Parameters.Add("@DepartmentID", SqlDbType.VarChar).Value = ClsCommon.EnsureString(DepartmentID);
                        command.Parameters.Add("@thematicarea_IDs", SqlDbType.VarChar).Value = ClsCommon.EnsureString(thematicarea_IDs);
                        command.Parameters.Add("@WorkLocationID", SqlDbType.VarChar).Value = ClsCommon.EnsureString(WorkLocationID);
                        command.Parameters.Add("@metro", SqlDbType.VarChar).Value = ClsCommon.EnsureString(metro);
                        command.Parameters.Add("@design_id", SqlDbType.VarChar).Value = ClsCommon.EnsureString(design_id);
                        command.Parameters.Add("@JobID", SqlDbType.VarChar).Value = ClsCommon.EnsureString(JobID);
                        command.Parameters.Add("@emp_status", SqlDbType.VarChar).Value = ClsCommon.EnsureString(emp_status);
                        command.Parameters.Add("@Probation_EndDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Probation_EndDate);
                        command.Parameters.Add("@doc", SqlDbType.VarChar).Value = ClsCommon.EnsureString(doc);
                        command.Parameters.Add("@hod_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(hod_name);
                        command.Parameters.Add("@SecondaryHODID", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SecondaryHODID);
                        command.Parameters.Add("@ed_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(ed_name);
                        command.Parameters.Add("@HRID", SqlDbType.VarChar).Value = ClsCommon.EnsureString(HRID);
                        command.Parameters.Add("@AppraiserID", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AppraiserID);
                        command.Parameters.Add("@co_ot", SqlDbType.VarChar).Value = ClsCommon.EnsureString(co_ot);
                        command.Parameters.Add("@ResidentialStatus", SqlDbType.VarChar).Value = ClsCommon.EnsureString(ResidentialStatus);
                        command.Parameters.Add("@SkillsIDs", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SkillsIDs);
                        command.Parameters.Add("@NoticePeriod", SqlDbType.VarChar).Value = ClsCommon.EnsureString(NoticePeriod);
                        command.Parameters.Add("@dor", SqlDbType.VarChar).Value = ClsCommon.EnsureString(dor);
                        command.Parameters.Add("@lastworking_day", SqlDbType.VarChar).Value = ClsCommon.EnsureString(lastworking_day);
                        command.Parameters.Add("@BankIDS", SqlDbType.VarChar).Value = ClsCommon.EnsureString(BankIDS);
                        command.Parameters.Add("@AccountTypeS", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AccountTypeS);
                        command.Parameters.Add("@AccountNameS", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AccountNameS);
                        command.Parameters.Add("@AccountNoS", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AccountNoS);
                        command.Parameters.Add("@BranchNameS", SqlDbType.VarChar).Value = ClsCommon.EnsureString(BranchNameS);
                        command.Parameters.Add("@BranchAddressS", SqlDbType.VarChar).Value = ClsCommon.EnsureString(BranchAddressS);
                        command.Parameters.Add("@IFSCCodeS", SqlDbType.VarChar).Value = ClsCommon.EnsureString(IFSCCodeS);
                        command.Parameters.Add("@SwiftCodeS", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SwiftCodeS);
                        command.Parameters.Add("@OtherDetailsS", SqlDbType.VarChar).Value = ClsCommon.EnsureString(OtherDetailsS);
                        command.Parameters.Add("@BankIDR", SqlDbType.VarChar).Value = ClsCommon.EnsureString(BankIDR);
                        command.Parameters.Add("@AccountTypeR", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AccountTypeR);
                        command.Parameters.Add("@AccountNameR", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AccountNameR);
                        command.Parameters.Add("@AccountNoR", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AccountNoR);
                        command.Parameters.Add("@BranchNameR", SqlDbType.VarChar).Value = ClsCommon.EnsureString(BranchNameR);
                        command.Parameters.Add("@BranchAddressR", SqlDbType.VarChar).Value = ClsCommon.EnsureString(BranchAddressR);
                        command.Parameters.Add("@IFSCCodeR", SqlDbType.VarChar).Value = ClsCommon.EnsureString(IFSCCodeR);
                        command.Parameters.Add("@SwiftCodeR", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SwiftCodeR);
                        command.Parameters.Add("@OtherDetailsR", SqlDbType.VarChar).Value = ClsCommon.EnsureString(OtherDetailsR);
                        command.Parameters.Add("@PsychometricTest", SqlDbType.VarChar).Value = ClsCommon.EnsureString(PsychometricTest);
                        command.Parameters.Add("@father_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(father_name);
                        command.Parameters.Add("@mother_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(mother_name);
                        command.Parameters.Add("@gender", SqlDbType.VarChar).Value = ClsCommon.EnsureString(gender);
                        command.Parameters.Add("@dob", SqlDbType.VarChar).Value = ClsCommon.EnsureString(dob);
                        command.Parameters.Add("@marital_status", SqlDbType.VarChar).Value = ClsCommon.EnsureString(marital_status);
                        command.Parameters.Add("@SpouseName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SpouseName);
                        command.Parameters.Add("@PartnerName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(PartnerName);
                        command.Parameters.Add("@NomineeName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(NomineeName);
                        command.Parameters.Add("@NomineeRelation", SqlDbType.VarChar).Value = ClsCommon.EnsureString(NomineeRelation);
                        command.Parameters.Add("@children", SqlDbType.VarChar).Value = ClsCommon.EnsureString(children);
                        command.Parameters.Add("@Nationality", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Nationality);
                        command.Parameters.Add("@lane1P1", SqlDbType.VarChar).Value = ClsCommon.EnsureString(lane1P1);
                        command.Parameters.Add("@lane2P1", SqlDbType.VarChar).Value = ClsCommon.EnsureString(lane2P1);
                        command.Parameters.Add("@zip_codeP1", SqlDbType.VarChar).Value = ClsCommon.EnsureString(zip_codeP1);
                        command.Parameters.Add("@CountryIDP1", SqlDbType.VarChar).Value = ClsCommon.EnsureString(CountryIDP1);
                        command.Parameters.Add("@StateIDP1", SqlDbType.VarChar).Value = ClsCommon.EnsureString(StateIDP1);
                        command.Parameters.Add("@CityIDP1", SqlDbType.VarChar).Value = ClsCommon.EnsureString(CityIDP1);
                        command.Parameters.Add("@lane1P2", SqlDbType.VarChar).Value = ClsCommon.EnsureString(lane1P2);
                        command.Parameters.Add("@lane2P2", SqlDbType.VarChar).Value = ClsCommon.EnsureString(lane2P2);
                        command.Parameters.Add("@zip_codeP2", SqlDbType.VarChar).Value = ClsCommon.EnsureString(zip_codeP2);
                        command.Parameters.Add("@CountryIDP2", SqlDbType.VarChar).Value = ClsCommon.EnsureString(CountryIDP2);
                        command.Parameters.Add("@StateIDP2", SqlDbType.VarChar).Value = ClsCommon.EnsureString(StateIDP2);
                        command.Parameters.Add("@CityIDP2", SqlDbType.VarChar).Value = ClsCommon.EnsureString(CityIDP2);
                        command.Parameters.Add("@SpecialAbility", SqlDbType.VarChar).Value = ClsCommon.EnsureString(SpecialAbility);
                        command.Parameters.Add("@AnyMedicalCondition", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AnyMedicalCondition);
                        command.Parameters.Add("@PhysicianName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(PhysicianName);
                        command.Parameters.Add("@PhysicianNumber", SqlDbType.VarChar).Value = ClsCommon.EnsureString(PhysicianNumber);
                        command.Parameters.Add("@PhysicianAlternate_No", SqlDbType.VarChar).Value = ClsCommon.EnsureString(PhysicianAlternate_No);
                        command.Parameters.Add("@BloodGroup", SqlDbType.VarChar).Value = ClsCommon.EnsureString(BloodGroup);
                        command.Parameters.Add("@emergContact_no", SqlDbType.VarChar).Value = ClsCommon.EnsureString(emergContact_no);
                        command.Parameters.Add("@emergContact_Name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(emergContact_Name);
                        command.Parameters.Add("@emergContact_Relation", SqlDbType.VarChar).Value = ClsCommon.EnsureString(emergContact_Relation);
                        command.Parameters.Add("@CourseE", SqlDbType.VarChar).Value = ClsCommon.EnsureString(CourseE);
                        command.Parameters.Add("@UniversityE", SqlDbType.VarChar).Value = ClsCommon.EnsureString(UniversityE);
                        command.Parameters.Add("@LocationE", SqlDbType.VarChar).Value = ClsCommon.EnsureString(LocationE);
                        command.Parameters.Add("@YearE", SqlDbType.VarChar).Value = ClsCommon.EnsureString(YearE);
                        command.Parameters.Add("@CourseP", SqlDbType.VarChar).Value = ClsCommon.EnsureString(CourseP);
                        command.Parameters.Add("@UniversityP", SqlDbType.VarChar).Value = ClsCommon.EnsureString(UniversityP);
                        command.Parameters.Add("@LocationP", SqlDbType.VarChar).Value = ClsCommon.EnsureString(LocationP);
                        command.Parameters.Add("@YearP", SqlDbType.VarChar).Value = ClsCommon.EnsureString(YearP);
                        command.Parameters.Add("@aireline_no", SqlDbType.VarChar).Value = ClsCommon.EnsureString(aireline_no);
                        command.Parameters.Add("@frequentflyer_no", SqlDbType.VarChar).Value = ClsCommon.EnsureString(frequentflyer_no);
                        command.Parameters.Add("@seat_preference", SqlDbType.VarChar).Value = ClsCommon.EnsureString(seat_preference);
                        command.Parameters.Add("@MealPreference", SqlDbType.VarChar).Value = ClsCommon.EnsureString(MealPreference);
                        command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(CompanyName);
                        command.Parameters.Add("@DOJLE", SqlDbType.VarChar).Value = ClsCommon.EnsureString(DOJLE);
                        command.Parameters.Add("@DORLE", SqlDbType.VarChar).Value = ClsCommon.EnsureString(DORLE);
                        command.Parameters.Add("@TotalExperence", SqlDbType.VarChar).Value = ClsCommon.EnsureString(TotalExperence);
                        command.Parameters.Add("@AnnualCTC", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AnnualCTC);
                        command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Designation);
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Location);
                        command.Parameters.Add("@EmploymentTermLE", SqlDbType.VarChar).Value = ClsCommon.EnsureString(EmploymentTermLE);
                        command.Parameters.Add("@ShareSomething", SqlDbType.VarChar).Value = ClsCommon.EnsureString(ShareSomething);
                        command.Parameters.Add("@IsConsiderIncome", SqlDbType.VarChar).Value = ClsCommon.EnsureString(IsConsiderIncome);
                        command.Parameters.Add("@IncomeAmount", SqlDbType.VarChar).Value = ClsCommon.EnsureString(IncomeAmount);
                        command.Parameters.Add("@TDSDeduction", SqlDbType.VarChar).Value = ClsCommon.EnsureString(TDSDeduction);
                        command.Parameters.Add("@pan_no", SqlDbType.VarChar).Value = ClsCommon.EnsureString(pan_no);
                        command.Parameters.Add("@NameonPAN", SqlDbType.VarChar).Value = ClsCommon.EnsureString(NameonPAN);
                        command.Parameters.Add("@AadharNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(AadharNo);
                        command.Parameters.Add("@NameonAadhar", SqlDbType.VarChar).Value = ClsCommon.EnsureString(NameonAadhar);
                        command.Parameters.Add("@Voterno", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Voterno);
                        command.Parameters.Add("@PassportNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(PassportNo);
                        command.Parameters.Add("@NameonPassport", SqlDbType.VarChar).Value = ClsCommon.EnsureString(NameonPassport);
                        command.Parameters.Add("@PlaceofIssue", SqlDbType.VarChar).Value = ClsCommon.EnsureString(PlaceofIssue);
                        command.Parameters.Add("@PassportExpiryDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(PassportExpiryDate);
                        command.Parameters.Add("@DirectorIdentificationNumber", SqlDbType.VarChar).Value = ClsCommon.EnsureString(DirectorIdentificationNumber);
                        command.Parameters.Add("@DIN", SqlDbType.VarChar).Value = ClsCommon.EnsureString(DIN);
                        command.Parameters.Add("@NameonDIN", SqlDbType.VarChar).Value = ClsCommon.EnsureString(NameonDIN);
                        command.Parameters.Add("@UAN", SqlDbType.VarChar).Value = ClsCommon.EnsureString(UAN);
                        command.Parameters.Add("@NameonUAN", SqlDbType.VarChar).Value = ClsCommon.EnsureString(NameonUAN);
                        command.Parameters.Add("@OldPF", SqlDbType.VarChar).Value = ClsCommon.EnsureString(OldPF);
                        command.Parameters.Add("@OldPFNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(OldPFNo);
                        command.Parameters.Add("@PIO_OCI", SqlDbType.VarChar).Value = ClsCommon.EnsureString(PIO_OCI);
                        command.Parameters.Add("@NameonPIO_OCI", SqlDbType.VarChar).Value = ClsCommon.EnsureString(NameonPIO_OCI);
                        command.Parameters.Add("@DrivingLicense", SqlDbType.VarChar).Value = ClsCommon.EnsureString(DrivingLicense);
                        command.Parameters.Add("@DLNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(DLNo);
                        command.Parameters.Add("@NameonDL", SqlDbType.VarChar).Value = ClsCommon.EnsureString(NameonDL);
                        command.Parameters.Add("@IssueDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(IssueDate);
                        command.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(ExpiryDate);
                        command.Parameters.Add("@PlaceofIssueID", SqlDbType.VarChar).Value = ClsCommon.EnsureString(PlaceofIssueID);
                        command.Parameters.Add("@IsLast", SqlDbType.Int).Value = IsLast;
                        command.Parameters.Add("@MobileP1", SqlDbType.VarChar).Value = ClsCommon.EnsureString(MobileP1);
                        command.Parameters.Add("@MobileP2", SqlDbType.VarChar).Value = ClsCommon.EnsureString(MobileP2);
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during FnSetEmployeeImport. The query was executed :", ex.ToString(), "spu_SetMaster_empImport", "Common", "Common", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        //public static long fnSetTravelBudget(long id, long TravelYear, string TravelType, decimal AirFare, decimal PerDiem, decimal Hotel, decimal LocalTravel)
        //{
        //    long fnSetFreeMeal = 0;
        //    SqlParameter[] oparam = new SqlParameter[8];
        //    oparam[0] = new SqlParameter("@Id", id);
        //    oparam[1] = new SqlParameter("@Finyearid", TravelYear);
        //    oparam[2] = new SqlParameter("@TravelType", TravelType);
        //    oparam[3] = new SqlParameter("@AirFare", AirFare);
        //    oparam[4] = new SqlParameter("@PerDiem", PerDiem);
        //    oparam[5] = new SqlParameter("@Hotel", Hotel);
        //    oparam[6] = new SqlParameter("@LocalTravel", LocalTravel);
        //    oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
        //    DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetTravelBudget", oparam);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        fnSetFreeMeal = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
        //    }
        //    else
        //        fnSetFreeMeal = 0;
        //    return fnSetFreeMeal;
        //}



        public static PostResponse fnDelConsolidatedSalaryAllocation(long Empid, int Month, int Year)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_DelConsolidatedSalaryAllocation", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Empid", SqlDbType.Int).Value = Empid;
                        command.Parameters.Add("@month", SqlDbType.Int).Value = Month;
                        command.Parameters.Add("@Year", SqlDbType.VarChar).Value = Year;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnDelConsolidatedSalaryAllocation. The query was executed :", ex.ToString(), "spu_DelConsolidatedSalaryAllocation", "Common_SPU", "Common_SPU", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static int fnSetDeclartionEmpAccept(long ID, string Accept, string Remark, string status, string UserId, string EmpId, string DueDate)
        {
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@DeclarationId", ID);
            oparam[1] = new SqlParameter("@Accept", Convert.ToInt32(Accept));
            oparam[2] = new SqlParameter("@Remark", ClsCommon.EnsureString(Remark));
            if (Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID")) > 0)
            {
                oparam[3] = new SqlParameter("@LoginID", Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID")));
                oparam[4] = new SqlParameter("@EmpId", clsApplicationSetting.GetSessionValue("EMPID"));
            }
            else
            {
                oparam[3] = new SqlParameter("@LoginID", Convert.ToInt64(UserId));
                oparam[4] = new SqlParameter("@EmpId", EmpId);
            }
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@status", ClsCommon.EnsureNumber(status));
            oparam[7] = new SqlParameter("@DueDate", DueDate);
            clsDataBaseHelper.ExecuteSp("spu_SetDeclartionEmployee", oparam);
            return 1;

        }
        public static int SetHRDeclartionEmpAccept(long ID, string Accept, string Remark, string status, string UserId, string EmpId, string DueDate)
        {
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@DeclarationId", ID);
            oparam[1] = new SqlParameter("@Accept", Convert.ToInt32(Accept));
            oparam[2] = new SqlParameter("@Remark", ClsCommon.EnsureString(Remark));
            if (Convert.ToInt64(UserId) > 0)
            {
                oparam[3] = new SqlParameter("@LoginID", Convert.ToInt64(UserId));
                oparam[4] = new SqlParameter("@EmpId", EmpId);
            }
            else
            {
                oparam[3] = new SqlParameter("@LoginID", Convert.ToInt64(UserId));
                oparam[4] = new SqlParameter("@EmpId", EmpId);
            }
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@status", ClsCommon.EnsureNumber(status));
            oparam[7] = new SqlParameter("@DueDate", DueDate);
            clsDataBaseHelper.ExecuteSp("spu_SetDeclartionEmployee", oparam);
            return 1;

        }
        public static int fnSetActiveDeclartion(long ID, string Doctype)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@Id", ID);
            oparam[1] = new SqlParameter("@DocType", Doctype);
            oparam[2] = new SqlParameter("@LoginID", Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID")));
            clsDataBaseHelper.ExecuteSp("spu_SetActiveOrDecativeDeclartion", oparam);
            return 1;

        }
        public static int fnSetTravelFinanceOfficeCardAction(long ID, decimal Paidamount, string PaidDate, string ActionType, string reason, string UatNo)
        {
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@TravelDocId", ID);
            oparam[1] = new SqlParameter("@Paidamount", Convert.ToDecimal(Paidamount));
            oparam[2] = new SqlParameter("@PaidDate", Convert.ToDateTime(PaidDate));
            oparam[3] = new SqlParameter("@ActionType", ClsCommon.EnsureString(ActionType));
            oparam[4] = new SqlParameter("@LoginID", Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID")));
            oparam[5] = new SqlParameter("@reason", ClsCommon.EnsureString(reason));
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@UatNo", ClsCommon.EnsureString(UatNo));
            clsDataBaseHelper.ExecuteSp("spu_SetFinanceCardAction", oparam);
            return 1;

        }


        public static int fnSetUpdateCompOff(int Month, int Year)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Month", Month);
            oparam[1] = new SqlParameter("@Year", Year);
            oparam[2] = new SqlParameter("@EmpID", Convert.ToInt64(clsApplicationSetting.GetSessionValue("EMPID")));
            clsDataBaseHelper.ExecuteSp("spu_SetUpdateCompOff ", oparam);
            return 1;

        }

        #endregion


        #region FireMailStoreProcedure


        public static DataSet fnCreateMail_REC_IApprovers(long REC_ReqID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_REC_IApprovers", oparam);
        }


        public static DataSet fnCreateMail_Leave(long LeaveLogID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@LeaveLogID", LeaveLogID);
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_Leave", oparam);
        }

        public static DataSet fnCreateMail_ActivityLog(long Month, long Year, long EMPID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@Month", Month);
            oparam[1] = new SqlParameter("@Year", Year);
            oparam[2] = new SqlParameter("@EMPID", EMPID);
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_ActivityLog", oparam);
        }

        public static void fnSetPushNotification(string Subject, string MessageContent, string Category, long TableID,
            string Status, string GotoURL, int IsStatusRead, string CreateFor)
        {
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@Subject", ClsCommon.EnsureString(Subject));
            oparam[1] = new SqlParameter("@MessageContent", ClsCommon.EnsureString(MessageContent));
            oparam[2] = new SqlParameter("@Category", ClsCommon.EnsureString(Category));
            oparam[3] = new SqlParameter("@TableID", TableID);
            oparam[4] = new SqlParameter("@Status", ClsCommon.EnsureString(Status));
            oparam[5] = new SqlParameter("@GotoURL", ClsCommon.EnsureString(GotoURL));
            oparam[6] = new SqlParameter("@IsStatusRead", IsStatusRead);
            oparam[7] = new SqlParameter("@CreateFor", ClsCommon.EnsureString(CreateFor));
            clsDataBaseHelper.ExecuteDataSet("spu_SetPushNotification", oparam);
        }
        public static PostResponse fnCreateMail_ForgotPassword(ForgotPassword.Request Modal)
        {
            PostResponse result = new PostResponse();

            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_CreateMail_ForgotPassword", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Username", SqlDbType.VarChar).Value = Modal.Username ?? "";
                        command.Parameters.Add("@Token", SqlDbType.VarChar).Value = Modal.Token ?? "";
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnCreateMail_ForgotPassword. The query was executed :", ex.ToString(), "spu_CreateMail_ForgotPassword", "BoardModal", "BoardModal", "");


                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }
        public static DataSet fnCreateMail_Travel(long TravelId, string Action)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@travelreq_id", TravelId);
            oparam[1] = new SqlParameter("@RequestStatus", Action);
            oparam[2] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_TravelRequest", oparam);
        }

        public static DataSet fnCreateMail_TravelAmendment(long TravelId)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@travelreq_id", TravelId);
            oparam[1] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_TravelAmendment", oparam);
        }

        public static DataSet fnCreateMail_UserUpdateProfile()
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@DocType", "RequestforApproval");
            oparam[1] = new SqlParameter("@EmployeeId", clsApplicationSetting.GetSessionValue("EMPID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_ProfileApproval", oparam);
        }

        public static DataSet fnCreateMail_ReminderMail()
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_TERFillSendNotification", oparam);
        }
        public static DataSet fnCreateMail_TER(long TravelId, string Action)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@travelreq_id", TravelId);
            oparam[1] = new SqlParameter("@RequestStatus", Action);
            oparam[2] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_TERRequest", oparam);
        }

        public static DataSet fnCreateMail_OTP(string OTP)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[1] = new SqlParameter("@OTP", OTP);
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_OTPLogin", oparam);
        }


        #endregion


        #region SetSPV1

        public static CommandResult fnSetPMS_Essential(long PMS_EID, long FYID, string DocType, string Commentors, long Confirmer, string GoalSheetStart, string GoalSheetEnd,
            string AppraisalEntryStart, string AppraisalEntryEnd, string AppraisalReviewStart, string AppraisalReviewEnd, string Question, string QApplyDesignation, string QDesignationIDs, string QuestionFor, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[19];
            oparam[0] = new SqlParameter("@PMS_EID", PMS_EID);
            oparam[1] = new SqlParameter("@DocType", ClsCommon.EnsureString(DocType));
            oparam[2] = new SqlParameter("@Commentors", ClsCommon.EnsureString(Commentors));
            oparam[3] = new SqlParameter("@Confirmer", Confirmer);
            oparam[4] = new SqlParameter("@GoalSheetStart", ClsCommon.EnsureString(GoalSheetStart));
            oparam[5] = new SqlParameter("@GoalSheetEnd", ClsCommon.EnsureString(GoalSheetEnd));
            oparam[6] = new SqlParameter("@AppraisalEntryStart", ClsCommon.EnsureString(AppraisalEntryStart));
            oparam[7] = new SqlParameter("@AppraisalEntryEnd", ClsCommon.EnsureString(AppraisalEntryEnd));
            oparam[8] = new SqlParameter("@AppraisalReviewStart", ClsCommon.EnsureString(AppraisalReviewStart));
            oparam[9] = new SqlParameter("@AppraisalReviewEnd", ClsCommon.EnsureString(AppraisalReviewEnd));
            oparam[10] = new SqlParameter("@Question", ClsCommon.EnsureString(Question));
            oparam[11] = new SqlParameter("@QApplyDesignation", ClsCommon.EnsureString(QApplyDesignation));
            oparam[12] = new SqlParameter("@QDesignationIDs", ClsCommon.EnsureString(QDesignationIDs));
            oparam[13] = new SqlParameter("@QuestionFor", ClsCommon.EnsureString(QuestionFor));
            oparam[14] = new SqlParameter("@Priority", Priority);
            oparam[15] = new SqlParameter("@IsActive", IsActive);
            oparam[16] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[17] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[18] = new SqlParameter("@FYID", FYID);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_Essential", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetPMS_HierarchyApproval(string Ids, int Approved)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Ids", Ids);
            oparam[1] = new SqlParameter("@Approved", Approved);
            oparam[2] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[3] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_HierarchyApproval", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetPMS_GoalSheetApproval(string Ids, int Approved, DateTime GoalSheetStart, DateTime GoalSheetEnd)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@Ids", Ids);
            oparam[1] = new SqlParameter("@Approved", Approved);
            oparam[2] = new SqlParameter("@GoalSheetStart", GoalSheetStart.ToString("dd-MMM-yyyy"));
            oparam[3] = new SqlParameter("@GoalSheetEnd", GoalSheetEnd.ToString("dd-MMM-yyyy"));
            oparam[4] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_GoalSheetApproval", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }


        public static CommandResult fnSetPMS_AppraisalActDate(string Ids, int Approved, DateTime AppraisalEntryStart, DateTime AppraisalEntryEnd, DateTime AppraisalReviewStart, DateTime AppraisalReviewEnd)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@Ids", Ids);
            oparam[1] = new SqlParameter("@Approved", Approved);
            oparam[2] = new SqlParameter("@AppraisalEntryStart", AppraisalEntryStart.ToString("dd-MMM-yyyy"));
            oparam[3] = new SqlParameter("@AppraisalEntryEnd", AppraisalEntryEnd.ToString("dd-MMM-yyyy"));
            oparam[4] = new SqlParameter("@AppraisalReviewStart", AppraisalReviewStart.ToString("dd-MMM-yyyy"));
            oparam[5] = new SqlParameter("@AppraisalReviewEnd", AppraisalReviewEnd.ToString("dd-MMM-yyyy"));
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_AppraisalActDate", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }


        public static CommandResult fnSetPMS_KPAApproval(long FYID, int Approved, string Remarks)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@Approved", Approved);
            oparam[2] = new SqlParameter("@Remarks", ClsCommon.EnsureString(Remarks));
            oparam[3] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_KPAApproval", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetPMS_KPA(long PMS_KPAID, long FYID, string Area, long UOMID, string IncType, string IsMonitoring,
            string IsMandatory, string AutoRating, string IsProbation, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[13];
            oparam[0] = new SqlParameter("@PMS_KPAID", PMS_KPAID);
            oparam[1] = new SqlParameter("@FYID", FYID);
            oparam[2] = new SqlParameter("@Area", ClsCommon.EnsureString(Area));
            oparam[3] = new SqlParameter("@UOMID", UOMID);
            oparam[4] = new SqlParameter("@IncType", ClsCommon.EnsureString(IncType));
            oparam[5] = new SqlParameter("@IsMonitoring", ClsCommon.EnsureString(IsMonitoring));
            oparam[6] = new SqlParameter("@IsMandatory", ClsCommon.EnsureString(IsMandatory));
            oparam[7] = new SqlParameter("@AutoRating", ClsCommon.EnsureString(AutoRating));
            oparam[8] = new SqlParameter("@IsProbation", ClsCommon.EnsureString(IsProbation));
            oparam[9] = new SqlParameter("@Priority", Priority);
            oparam[10] = new SqlParameter("@IsActive", IsActive);
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[12] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_KPA", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetPMSUOM(long PMSUOMID, long FYID, string Name, string Type, string AutoRating, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@PMSUOMID", PMSUOMID);
            oparam[1] = new SqlParameter("@FYID", FYID);
            oparam[2] = new SqlParameter("@Name", ClsCommon.EnsureString(Name));
            oparam[3] = new SqlParameter("@Type", ClsCommon.EnsureString(Type));
            oparam[4] = new SqlParameter("@AutoRating", AutoRating);
            oparam[5] = new SqlParameter("@Priority", Priority);
            oparam[6] = new SqlParameter("@IsActive", IsActive);
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_UOM", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetMPRSetting(long MPRSettingID, int MPRDueDate)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@MPRSettingID", MPRSettingID);
            oparam[1] = new SqlParameter("@MPRDueDate", MPRDueDate);
            oparam[2] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[3] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMPRSetting", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetSMPRLock(long SMPRID, int Lock, DateTime dtdate, string LockRemarks)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@SMPRID", SMPRID);
            oparam[1] = new SqlParameter("@Lock", Lock);
            oparam[2] = new SqlParameter("@dtdate", dtdate);
            oparam[3] = new SqlParameter("@LockRemarks", LockRemarks);
            oparam[4] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[5] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetSMPRLock", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetSMPRComments(long SCID, long SMPRID, string Comment, string Doctype, long SectionID, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@SCID", SCID);
            oparam[1] = new SqlParameter("@SMPRID", SMPRID);
            oparam[2] = new SqlParameter("@Comment", Comment);
            oparam[3] = new SqlParameter("@Doctype", Doctype);
            oparam[4] = new SqlParameter("@SectionID", SectionID);
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetSMPRComments", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnSetMPRSec(long MPRSID, string SecName, string SecDesc, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@MPRSID", MPRSID);
            oparam[1] = new SqlParameter("@SecName", ClsCommon.EnsureString(SecName));
            oparam[2] = new SqlParameter("@SecDesc", ClsCommon.EnsureString(SecDesc));
            oparam[3] = new SqlParameter("@IsActive", IsActive);
            oparam[4] = new SqlParameter("@Priority", Priority);
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMPRSec", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }

        public static CommandResult fnSetMPR_SubSec(long MPRSubSID, string SubSecName, long MPRSID, int NoOfCol, string ColText1, string ColDataType1, string ColSuffix1, string ColText2, string ColDataType2, string ColSuffix2,
                string ColText3, string ColDataType3, string ColSuffix3, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[17];
            oparam[0] = new SqlParameter("@MPRSubSID", MPRSubSID);
            oparam[1] = new SqlParameter("@SubSecName", ClsCommon.EnsureString(SubSecName));
            oparam[2] = new SqlParameter("@MPRSID", MPRSID);
            oparam[3] = new SqlParameter("@NoOfCol", NoOfCol);
            oparam[4] = new SqlParameter("@ColText1", ClsCommon.EnsureString(ColText1));
            oparam[5] = new SqlParameter("@ColDataType1", ClsCommon.EnsureString(ColDataType1));
            oparam[6] = new SqlParameter("@ColSuffix1", ClsCommon.EnsureString(ColSuffix1));
            oparam[7] = new SqlParameter("@ColText2", ClsCommon.EnsureString(ColText2));
            oparam[8] = new SqlParameter("@ColDataType2", ClsCommon.EnsureString(ColDataType2));
            oparam[9] = new SqlParameter("@ColSuffix2", ClsCommon.EnsureString(ColSuffix2));
            oparam[10] = new SqlParameter("@ColText3", ClsCommon.EnsureString(ColText3));
            oparam[11] = new SqlParameter("@ColDataType3", ClsCommon.EnsureString(ColDataType3));
            oparam[12] = new SqlParameter("@ColSuffix3", ClsCommon.EnsureString(ColSuffix3));
            oparam[13] = new SqlParameter("@IsActive", IsActive);
            oparam[14] = new SqlParameter("@Priority", Priority);
            oparam[15] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[16] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMPR_SubSec", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnSetMPR_Indicator(long IndicatorID, long MPRSubSID, string IndicatorName, string AnswerIs, int NoOfCol, string ColText1, string ColDataType1, string ColSuffix1, string ColText2, string ColDataType2, string ColSuffix2,
               string ColText3, string ColDataType3, string ColSuffix3, string IsOrganisational, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[19];
            oparam[0] = new SqlParameter("@IndicatorID", IndicatorID);
            oparam[1] = new SqlParameter("@MPRSubSecID", MPRSubSID);
            oparam[2] = new SqlParameter("@IndicatorName", ClsCommon.EnsureString(IndicatorName));
            oparam[3] = new SqlParameter("@AnswerIs", ClsCommon.EnsureString(AnswerIs));

            oparam[4] = new SqlParameter("@NoOfCol", NoOfCol);
            oparam[5] = new SqlParameter("@ColText1", ClsCommon.EnsureString(ColText1));
            oparam[6] = new SqlParameter("@ColDataType1", ClsCommon.EnsureString(ColDataType1));
            oparam[7] = new SqlParameter("@ColSuffix1", ClsCommon.EnsureString(ColSuffix1));
            oparam[8] = new SqlParameter("@ColText2", ClsCommon.EnsureString(ColText2));
            oparam[9] = new SqlParameter("@ColDataType2", ClsCommon.EnsureString(ColDataType2));
            oparam[10] = new SqlParameter("@ColSuffix2", ClsCommon.EnsureString(ColSuffix2));
            oparam[11] = new SqlParameter("@ColText3", ClsCommon.EnsureString(ColText3));
            oparam[12] = new SqlParameter("@ColDataType3", ClsCommon.EnsureString(ColDataType3));
            oparam[13] = new SqlParameter("@ColSuffix3", ClsCommon.EnsureString(ColSuffix3));
            oparam[14] = new SqlParameter("@IsOrganisational", ClsCommon.EnsureString(IsOrganisational));
            oparam[15] = new SqlParameter("@IsActive", IsActive);
            oparam[16] = new SqlParameter("@Priority", Priority);
            oparam[17] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[18] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMPR_Indicator", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static PostResponse fnSetMPR(long MPRID, string MPRCode, string MPRName, string MPRDate, int Version, string InitiateDate,
            long ProjectID, long StateID, long ApproverLevel1, long ApproverLevel2, int Priority, int IsActive)
        {
            PostResponse Result = new PostResponse();
            SqlParameter[] oparam = new SqlParameter[14];
            oparam[0] = new SqlParameter("@MPRID", MPRID);
            oparam[1] = new SqlParameter("@MPRCode", ClsCommon.EnsureString(MPRCode));
            oparam[2] = new SqlParameter("@MPRName", ClsCommon.EnsureString(MPRName));
            oparam[3] = new SqlParameter("@MPRDate", ClsCommon.EnsureString(MPRDate));
            oparam[4] = new SqlParameter("@Version", Version);
            oparam[5] = new SqlParameter("@InitiateDate", ClsCommon.EnsureString(InitiateDate));
            oparam[6] = new SqlParameter("@ProjectID", ProjectID);
            oparam[7] = new SqlParameter("@StateID", StateID);
            oparam[8] = new SqlParameter("@ApproverLevel1", ApproverLevel1);
            oparam[9] = new SqlParameter("@ApproverLevel2", ApproverLevel2);
            oparam[10] = new SqlParameter("@IsActive", IsActive);
            oparam[11] = new SqlParameter("@Priority", Priority);
            oparam[12] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[13] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMPR", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static PostResponse fnSetMPRDet(long MPRID, int SNo, string DocType, long MPRIndicatorID,
            long Executor1ID, long Executor2ID, long ApproverID, int Priority, int IsActive)
        {
            PostResponse Result = new PostResponse();
            SqlParameter[] oparam = new SqlParameter[11];
            oparam[0] = new SqlParameter("@MPRID", MPRID);
            oparam[1] = new SqlParameter("@SNo", SNo);
            oparam[2] = new SqlParameter("@DocType", ClsCommon.EnsureString(DocType));
            oparam[3] = new SqlParameter("@MPRIndicatorID", MPRIndicatorID);
            oparam[4] = new SqlParameter("@Executor1ID", Executor1ID);
            oparam[5] = new SqlParameter("@Executor2ID", Executor2ID);
            oparam[6] = new SqlParameter("@ApproverID", ApproverID);
            oparam[7] = new SqlParameter("@IsActive", IsActive);
            oparam[8] = new SqlParameter("@Priority", Priority);
            oparam[9] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[10] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMPRDet", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnSetMakeCopyLeaveRequest(long LeavelogID, int LeaveTypeID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@LeavelogID", LeavelogID);
            oparam[1] = new SqlParameter("@LeaveTypeID", LeaveTypeID);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMakeCopyLeaveRequest", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnSetMPRTarget(long FINID, string Quarter, long ProjectID, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@FINID", FINID);
            oparam[1] = new SqlParameter("@Quarter", ClsCommon.EnsureString(Quarter));
            oparam[2] = new SqlParameter("@ProjectID", ProjectID);
            oparam[3] = new SqlParameter("@Priority", Priority);
            oparam[4] = new SqlParameter("@IsActive", IsActive);
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMPRTarget", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }

        public static CommandResult fnSetMPRTargetDet(long MPRTargetID, long IndicatorID, string ColVal1, string ColVal2, string ColVal3, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[9];

            oparam[0] = new SqlParameter("@MPRTargetID", MPRTargetID);
            oparam[1] = new SqlParameter("@IndicatorID", IndicatorID);
            oparam[2] = new SqlParameter("@ColVal1", ClsCommon.EnsureString(ColVal1));
            oparam[3] = new SqlParameter("@ColVal2", ClsCommon.EnsureString(ColVal2));
            oparam[4] = new SqlParameter("@ColVal3", ClsCommon.EnsureString(ColVal3));
            oparam[5] = new SqlParameter("@Priority", Priority);
            oparam[6] = new SqlParameter("@IsActive", IsActive);

            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMPRTargetDet", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }



        public static CommandResult fnSetSMPR_Det(long SMPRDetID, string ExecuterVal1, string ExecuterVal2, string ExecuterVal3, int Submitted)
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);

            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[15];
            oparam[0] = new SqlParameter("@SMPRID", "0");
            oparam[1] = new SqlParameter("@MPRDetID", "0");
            oparam[2] = new SqlParameter("@MPRIndicatorID", "0");
            oparam[3] = new SqlParameter("@Executor1ID", "0");
            oparam[4] = new SqlParameter("@Executor2ID", "0");
            oparam[5] = new SqlParameter("@ApproverID", "0");

            oparam[6] = new SqlParameter("@ExecuterVal1", ClsCommon.EnsureString(ExecuterVal1));
            oparam[7] = new SqlParameter("@ExecuterVal2", ClsCommon.EnsureString(ExecuterVal2));
            oparam[8] = new SqlParameter("@ExecuterVal3", ClsCommon.EnsureString(ExecuterVal3));

            oparam[9] = new SqlParameter("@IsActive", "1");
            oparam[10] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[11] = new SqlParameter("@createdby", LoginID);

            oparam[12] = new SqlParameter("@OperationType", "Update");
            oparam[13] = new SqlParameter("@SMPRDetIDDD", SMPRDetID);
            oparam[14] = new SqlParameter("@Submitted", Submitted);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetSMPR_Det", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnSetSMPRApproval(long SMPRID, long ExecuterID, string ApprovalRemarks, string OperationType)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@SMPRID", SMPRID);
            oparam[1] = new SqlParameter("@ExecuterID", ExecuterID);
            oparam[2] = new SqlParameter("@ApprovalRemarks", ClsCommon.EnsureString(ApprovalRemarks));
            oparam[3] = new SqlParameter("@OperationType", ClsCommon.EnsureString(OperationType));
            oparam[4] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetSMPRApproval", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }
        public static PostResponse fnSetMPRAmend(long MPRID)
        {
            PostResponse Result = new PostResponse();
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@MPRID", MPRID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[2] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetMPRAmend", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }

        public static CommandResult fnAutoSync_EMPwithPMS(long FYID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[2] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_AutoSync_EMPwithPMS", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }

        public static CommandResult fnSetPMS_GoalSheet(long FYID, string Comment, int Priority, int IsActive, string Command)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@Comment", ClsCommon.EnsureString(Comment));
            oparam[3] = new SqlParameter("@Priority", Priority);
            oparam[4] = new SqlParameter("@IsActive", IsActive);
            oparam[5] = new SqlParameter("@Command", Command);
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_GoalSheet", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnSetPMS_GoalSheet_Det(long PMS_GSDID, long FYID, long KPAID, string PIndicator, string Target, string DetRemarks, int Priority, int IsActive, long UOMID, int Weight = 0, string operationType = "")
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[14];
            oparam[0] = new SqlParameter("@PMS_GSDID", PMS_GSDID);
            oparam[1] = new SqlParameter("@FYID", FYID);
            oparam[2] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[3] = new SqlParameter("@KPAID", KPAID);
            oparam[4] = new SqlParameter("@PIndicator", ClsCommon.EnsureString(PIndicator));
            oparam[5] = new SqlParameter("@Target", ClsCommon.EnsureString(Target));
            oparam[6] = new SqlParameter("@DetRemarks", ClsCommon.EnsureString(DetRemarks));
            oparam[7] = new SqlParameter("@Priority", Priority);
            oparam[8] = new SqlParameter("@IsActive", IsActive);
            oparam[9] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[10] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());

            oparam[11] = new SqlParameter("@operationType", operationType);
            oparam[12] = new SqlParameter("@Weight", Weight);
            oparam[13] = new SqlParameter("@UOMID", UOMID);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_GoalSheet_Det", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }




        public static CommandResult fnSetPMS_GSApproval(long PMS_GSID, string Reason, string Doctype, int Approved)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@PMS_GSID", PMS_GSID);
            oparam[1] = new SqlParameter("@Reason", ClsCommon.EnsureString(Reason));
            oparam[2] = new SqlParameter("@Doctype", ClsCommon.EnsureString(Doctype));
            oparam[3] = new SqlParameter("@Approved", Approved);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_GSApproval", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnSetPMS_QA(long FYID, long EMPID, string Doctype, string Question, string Answer, string FinalComment, string FinalRating, int Isdeleted)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[11];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@Doctype", ClsCommon.EnsureString(Doctype));
            oparam[3] = new SqlParameter("@GivenBy", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[4] = new SqlParameter("@Question", string.IsNullOrEmpty(Question) ? ClsCommon.EnsureString(Question) : Question);
            oparam[5] = new SqlParameter("@Answer", string.IsNullOrEmpty(Answer) ? ClsCommon.EnsureString(Answer) : Answer);
            oparam[6] = new SqlParameter("@FinalComment", ClsCommon.EnsureString(FinalComment));
            oparam[7] = new SqlParameter("@FinalRating", ClsCommon.EnsureString(FinalRating));
            oparam[8] = new SqlParameter("@Isdeleted", Isdeleted);
            oparam[9] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[10] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_QA", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }

        public static CommandResult fnSetPMSComments(long PMS_CommentID, long FYID, long EMPID, string Comment, string Doctype, int IsActive, long TableID = 0, string TableName = "")
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@PMS_CommentID", PMS_CommentID);
            oparam[1] = new SqlParameter("@FYID", FYID);
            oparam[2] = new SqlParameter("@EMPID", EMPID);
            oparam[3] = new SqlParameter("@Comment", ClsCommon.EnsureString(Comment));
            oparam[4] = new SqlParameter("@Doctype", ClsCommon.EnsureString(Doctype));
            oparam[5] = new SqlParameter("@IsActive", IsActive);
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[8] = new SqlParameter("@TableID", TableID);
            oparam[9] = new SqlParameter("@TableName", ClsCommon.EnsureString(TableName));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMSComments", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }

        public static PostResponse fnAutoCreateSMPR()
        {
            PostResponse Result = new PostResponse();
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("exec spu_AutoCreateSMPR");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnSetPMS_Appraisal(long FYID, long EMPID, int Priority, int IsActive, string Command)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@FYID", FYID);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@Priority", Priority);
            oparam[3] = new SqlParameter("@IsActive", IsActive);
            oparam[4] = new SqlParameter("@Command", ClsCommon.EnsureString(Command));
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_Appraisal", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }

        public static CommandResult fnSetPMS_Appraisal_det(long PMS_ADID, long PMS_AID, string Doctype, long TrainingTypeID, string TrainingType, string TrainingRemarks, long GoalSheetID, long GoalSheet_DetID,
            long KPAID, string KPA_Area, string KPA_PIndicator, string KPA_Target, string KPA_Weight, string KPA_IncType, string KPA_IsMonitoring, string KPA_IsMandatory, string KPA_AutoRating,
            long UOMID, string UOM_Name, string Self_Achievement, string Self_Comment, string KPA_TargetAchieved)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[24];
            oparam[0] = new SqlParameter("@PMS_ADID", PMS_ADID);
            oparam[1] = new SqlParameter("@PMS_AID", PMS_AID);
            oparam[2] = new SqlParameter("@Doctype", ClsCommon.EnsureString(Doctype));
            oparam[3] = new SqlParameter("@TrainingTypeID", TrainingTypeID);
            oparam[4] = new SqlParameter("@TrainingType", ClsCommon.EnsureString(TrainingType));
            oparam[5] = new SqlParameter("@TrainingRemarks", ClsCommon.EnsureString(TrainingRemarks));
            oparam[6] = new SqlParameter("@GoalSheetID", GoalSheetID);
            oparam[7] = new SqlParameter("@GoalSheet_DetID", GoalSheet_DetID);
            oparam[8] = new SqlParameter("@KPAID", KPAID);
            oparam[9] = new SqlParameter("@KPA_Area", ClsCommon.EnsureString(KPA_Area));
            oparam[10] = new SqlParameter("@KPA_PIndicator", ClsCommon.EnsureString(KPA_PIndicator));
            oparam[11] = new SqlParameter("@KPA_Weight", KPA_Weight);
            oparam[12] = new SqlParameter("@KPA_Target", KPA_Target);
            oparam[13] = new SqlParameter("@KPA_IncType", ClsCommon.EnsureString(KPA_IncType));
            oparam[14] = new SqlParameter("@KPA_IsMonitoring", ClsCommon.EnsureString(KPA_IsMonitoring));
            oparam[15] = new SqlParameter("@KPA_IsMandatory", ClsCommon.EnsureString(KPA_IsMandatory));
            oparam[16] = new SqlParameter("@KPA_AutoRating", ClsCommon.EnsureString(KPA_AutoRating));
            oparam[17] = new SqlParameter("@UOMID", UOMID);
            oparam[18] = new SqlParameter("@UOM_Name", ClsCommon.EnsureString(UOM_Name));
            oparam[19] = new SqlParameter("@Self_Achievement", ClsCommon.EnsureString(Self_Achievement));
            oparam[20] = new SqlParameter("@Self_Comment", ClsCommon.EnsureString(Self_Comment));
            oparam[21] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[22] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[23] = new SqlParameter("@KPA_TargetAchieved", ClsCommon.EnsureString(KPA_TargetAchieved));

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_Appraisal_Det", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }

        public static CommandResult fnSetPMS_Appraisal_Det_Team(long PMS_ADID, long PMS_AID, string Doctype, long TrainingTypeID, string TrainingType, string TrainingRemarks, string HOD_Score, string HOD_Comment)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@PMS_ADID", PMS_ADID);
            oparam[1] = new SqlParameter("@PMS_AID", PMS_AID);
            oparam[2] = new SqlParameter("@Doctype", Doctype);
            oparam[3] = new SqlParameter("@TrainingTypeID", TrainingTypeID);
            oparam[4] = new SqlParameter("@TrainingType", ClsCommon.EnsureString(TrainingType));
            oparam[5] = new SqlParameter("@TrainingRemarks", ClsCommon.EnsureString(TrainingRemarks));
            oparam[6] = new SqlParameter("@HOD_Score", HOD_Score);
            oparam[7] = new SqlParameter("@HOD_Comment", ClsCommon.EnsureString(HOD_Comment));
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[9] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_Appraisal_Det_Team", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnSetPMS_AppraisalApproval(long PMS_AID, int Approved, string Doctype, int Team_Score, string TeamRecommendation, string Group_Comment, int Group_Score, decimal CMC_Increment, int CMC_Score, string CMC_Comment = "")
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@PMS_AID", PMS_AID);
            oparam[1] = new SqlParameter("@Approved", Approved);
            oparam[2] = new SqlParameter("@Doctype", Doctype);
            oparam[3] = new SqlParameter("@Team_Score", Team_Score);
            oparam[4] = new SqlParameter("@TeamRecommendation", ClsCommon.EnsureString(TeamRecommendation));
            oparam[5] = new SqlParameter("@Group_Comment", Group_Comment);
            oparam[6] = new SqlParameter("@Group_Score", Group_Score);
            oparam[7] = new SqlParameter("@CMC_Increment", CMC_Increment);
            oparam[8] = new SqlParameter("@CMC_Score", CMC_Score);
            oparam[9] = new SqlParameter("@CMC_Comment", ClsCommon.EnsureString(CMC_Comment));
            oparam[10] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_AppraisalApproval", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnSetPMS_AppraisalApprovalReSubmit(long PMS_AID, int Approved, string Reason, string Doctype, int Team_Score, string TeamRecommendation, string Group_Comment, int Group_Score, decimal CMC_Increment, int CMC_Score,  string CMC_Comment = "")
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[13];
            oparam[0] = new SqlParameter("@PMS_AID", PMS_AID);
            oparam[1] = new SqlParameter("@Approved", Approved);
            oparam[2] = new SqlParameter("@Doctype", Doctype);
            oparam[3] = new SqlParameter("@Team_Score", Team_Score);
            oparam[4] = new SqlParameter("@TeamRecommendation", ClsCommon.EnsureString(TeamRecommendation));
            oparam[5] = new SqlParameter("@Group_Comment", Group_Comment);
            oparam[6] = new SqlParameter("@Group_Score", Group_Score);
            oparam[7] = new SqlParameter("@CMC_Increment", CMC_Increment);
            oparam[8] = new SqlParameter("@CMC_Score", CMC_Score);
            oparam[9] = new SqlParameter("@CMC_Comment", ClsCommon.EnsureString(CMC_Comment));
            oparam[10] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[12] = new SqlParameter("@Reason", Reason);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPMS_AppraisalApproval", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }
        public static CommandResult fnCreateMail_SMPR_Notify(DateTime date)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@dt", date);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_SMPR_Notify", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }

        public static CommandResult fnSetFinanYear(long ID, string year, string from_date, string to_date)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@ID", ID);
            oparam[1] = new SqlParameter("@year", year);
            oparam[2] = new SqlParameter("@from_date", from_date);
            oparam[3] = new SqlParameter("@to_date", to_date);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetFinanYear", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnCopyPMSToNextYear(long FYID_To)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@FYID_To", FYID_To);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_CopyPMSToNextYear", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }


        public static CommandResult fnDelJobRound(long JobDetailID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@JobDetailID", JobDetailID);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_DelJobRound", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }

        public static CommandResult fnSetREC_Initiate(long REC_ReqID, long ProjectDetailID, string Job_SubTitle, string DueDate, string JobDescription, string Qualification, string Skills, string Experience, long LocationId, long JobId, decimal Time_Per, long ProjectID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[14];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@ProjectDetailID", ProjectDetailID);
            oparam[2] = new SqlParameter("@Job_SubTitle", ClsCommon.EnsureString(Job_SubTitle));
            oparam[3] = new SqlParameter("@DueDate", DueDate);
            oparam[4] = new SqlParameter("@JobDescription", ClsCommon.EnsureString(JobDescription));
            oparam[5] = new SqlParameter("@Qualification", ClsCommon.EnsureString(Qualification));
            oparam[6] = new SqlParameter("@Skills", ClsCommon.EnsureString(Skills));
            oparam[7] = new SqlParameter("@Experience", ClsCommon.EnsureString(Experience));
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[9] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[10] = new SqlParameter("@ProjectID", ProjectID);
            oparam[11] = new SqlParameter("@Job_ID", JobId);
            oparam[12] = new SqlParameter("@Location_ID", LocationId);
            oparam[13] = new SqlParameter("@Time_Per", Time_Per);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_Initiate", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }

        public static CommandResult fnSetREC_IStaf(long REC_ReqID, long EMPID, string Relocation, string JobTitle, string Pillar, string RelocationByHR, string JobTitleByHR, string PillarByHR, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@Relocation", ClsCommon.EnsureString(Relocation));
            oparam[3] = new SqlParameter("@JobTitle", ClsCommon.EnsureString(JobTitle));
            oparam[4] = new SqlParameter("@Pillar", ClsCommon.EnsureString(Pillar));
            oparam[5] = new SqlParameter("@RelocationByHR", ClsCommon.EnsureString(RelocationByHR));
            oparam[6] = new SqlParameter("@JobTitleByHR", ClsCommon.EnsureString(JobTitleByHR));
            oparam[7] = new SqlParameter("@PillarByHR", ClsCommon.EnsureString(PillarByHR));
            oparam[8] = new SqlParameter("@IsActive", IsActive);
            oparam[9] = new SqlParameter("@Priority", Priority);
            oparam[10] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[11] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_IStaff", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }


        public static CommandResult fnSetREC_FillRequest(long REC_ReqID, string Job_SubTitle, string DueDate, string JobDescription, string Qualification, string Skills, string Experience, string Staff_Cat, string REC_Type, string Project_Tag)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@Job_SubTitle", ClsCommon.EnsureString(Job_SubTitle));
            oparam[2] = new SqlParameter("@DueDate", DueDate);
            oparam[3] = new SqlParameter("@JobDescription", ClsCommon.EnsureString(JobDescription));
            oparam[4] = new SqlParameter("@Qualification", ClsCommon.EnsureString(Qualification));
            oparam[5] = new SqlParameter("@Skills", ClsCommon.EnsureString(Skills));
            oparam[6] = new SqlParameter("@Experience", ClsCommon.EnsureString(Experience));
            oparam[7] = new SqlParameter("@Staff_Cat", ClsCommon.EnsureString(Staff_Cat));
            oparam[8] = new SqlParameter("@REC_Type", ClsCommon.EnsureString(REC_Type));
            oparam[9] = new SqlParameter("@Project_Tag", ClsCommon.EnsureString(Project_Tag));
            oparam[10] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_FillRequest", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }


        public static CommandResult fnSetREC_IApprovers(long REC_ReqID, long ApproverID, string DocType, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@ApproverID", ApproverID);
            oparam[2] = new SqlParameter("@DocType", ClsCommon.EnsureString(DocType));
            oparam[3] = new SqlParameter("@IsActive", IsActive);
            oparam[4] = new SqlParameter("@Priority", Priority);
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_IApprovers", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }

        public static CommandResult fnSetREC_IPreferences(long REC_ReqID, long EMPID, string Comment, int Preference)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@Comment", ClsCommon.EnsureString(Comment));
            oparam[3] = new SqlParameter("@Preference", Preference);
            oparam[4] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_IPreferences", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }


        public static CommandResult fnSetREC_IFinalPreferences(long REC_ReqID, string ApproveCandidate, string FinalComment)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@ApproveCandidate", ClsCommon.EnsureString(ApproveCandidate));
            oparam[2] = new SqlParameter("@FinalComment", ClsCommon.EnsureString(FinalComment));
            oparam[3] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_IFinalPreferences", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }


        public static CommandResult fnSetREC_EVacancyAnno_HR(long REC_ReqID, string HRVacancyDes, long HRAttachID, string Command)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@HRVacancyDes", ClsCommon.EnsureString(HRVacancyDes));
            oparam[2] = new SqlParameter("@HRAttachID", HRAttachID);
            oparam[3] = new SqlParameter("@Command", ClsCommon.EnsureString(Command));
            oparam[4] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EVacancyAnno_HR", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }

        public static CommandResult fnSetREC_EVacancyAnno_COMM(long REC_ReqID, string CommVacancyDes, long CommHRAttachID, string Command)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@CommVacancyDes", ClsCommon.EnsureString(CommVacancyDes));
            oparam[2] = new SqlParameter("@CommHRAttachID", CommHRAttachID);
            oparam[3] = new SqlParameter("@Command", ClsCommon.EnsureString(Command));
            oparam[4] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EVacancyAnno_Comm", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }


        public static CommandResult fnSetREC_EVacancyAnno_Final(long REC_ReqID, string StartDate, string EndDate, int Web_Announce, int Internal_Announce, int Other_Announce, string Command)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@StartDate", ClsCommon.EnsureString(StartDate));
            oparam[2] = new SqlParameter("@EndDate", ClsCommon.EnsureString(EndDate));
            oparam[3] = new SqlParameter("@Web_Announce", Web_Announce);
            oparam[4] = new SqlParameter("@Internal_Announce", Internal_Announce);
            oparam[5] = new SqlParameter("@Other_Announce", Other_Announce);
            oparam[6] = new SqlParameter("@Command", ClsCommon.EnsureString(Command));
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EVacancyAnno_Final", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }

        public static CommandResult fnSetREC_ChangeTag(long REC_ReqID, string Project_Tag)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@Project_Tag", ClsCommon.EnsureString(Project_Tag));
            oparam[2] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[3] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_ChangeTag", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }

        public static CommandResult fnSetREC_ApplyJob(string Code, string Name, string DOB, string Gender, string Nationality, string Mobile, string EmailID, string Address,
            string TotalExperience, string ThematicAreaID, string Resposibilities, string ChangeReason, string BreakReason, string Skills, long CVAttachID, decimal CurrentSalary,
            decimal ExpectedSalary)
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[20];
            oparam[0] = new SqlParameter("@Code", ClsCommon.EnsureString(Code));
            oparam[1] = new SqlParameter("@Name", ClsCommon.EnsureString(Name));
            oparam[2] = new SqlParameter("@DOB", ClsCommon.EnsureString(DOB));
            oparam[3] = new SqlParameter("@Gender", ClsCommon.EnsureString(Gender));
            oparam[4] = new SqlParameter("@Nationality", ClsCommon.EnsureString(Nationality));
            oparam[5] = new SqlParameter("@Mobile", ClsCommon.EnsureString(Mobile));
            oparam[6] = new SqlParameter("@EmailID", ClsCommon.EnsureString(EmailID));
            oparam[7] = new SqlParameter("@Address", ClsCommon.EnsureString(Address));
            oparam[8] = new SqlParameter("@TotalExperience", ClsCommon.EnsureString(TotalExperience));
            oparam[9] = new SqlParameter("@ThematicAreaID", ClsCommon.EnsureString(ThematicAreaID));
            oparam[10] = new SqlParameter("@Resposibilities", ClsCommon.EnsureString(Resposibilities));
            oparam[11] = new SqlParameter("@ChangeReason", ClsCommon.EnsureString(ChangeReason));
            oparam[12] = new SqlParameter("@BreakReason", ClsCommon.EnsureString(BreakReason));
            oparam[13] = new SqlParameter("@Skills", ClsCommon.EnsureString(""));//ClsCommon.EnsureString(Skills)
            oparam[14] = new SqlParameter("@CVAttachID", CVAttachID);
            oparam[15] = new SqlParameter("@CurrentSalary", CurrentSalary);
            oparam[16] = new SqlParameter("@ExpectedSalary", ExpectedSalary);
            oparam[17] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[18] = new SqlParameter("@createdby", LoginID);
            oparam[19] = new SqlParameter("@AdditionalSkills", ClsCommon.EnsureString(Skills));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_ApplyJob", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }

        public static CommandResult fnSetREC_ApplyJob_Det(long REC_AppDetID, long REC_AppID, string Doctype, string Employer, string Post, string CTC, string Name, string EmailID,
            string Relationship, string Phone, string Questions, string Answer, string Relation, string NoticePeriod, string Position, string AppliedDate, string outcome, string Period, string Location, string DOL,
            string ReasonL, string Specify, int Sno, string Course, string University, string year)
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[28];
            oparam[0] = new SqlParameter("@REC_AppDetID", REC_AppDetID);
            oparam[1] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[2] = new SqlParameter("@Doctype", ClsCommon.EnsureString(Doctype));
            oparam[3] = new SqlParameter("@Employer", ClsCommon.EnsureString(Employer));
            oparam[4] = new SqlParameter("@Post", ClsCommon.EnsureString(Post));
            oparam[5] = new SqlParameter("@CTC", ClsCommon.EnsureString(CTC));
            oparam[6] = new SqlParameter("@Name", ClsCommon.EnsureString(Name));
            oparam[7] = new SqlParameter("@EmailID", ClsCommon.EnsureString(EmailID));
            oparam[8] = new SqlParameter("@Relationship", ClsCommon.EnsureString(Relationship));
            oparam[9] = new SqlParameter("@Phone", ClsCommon.EnsureString(Phone));
            oparam[10] = new SqlParameter("@Questions", ClsCommon.EnsureString(Questions));
            oparam[11] = new SqlParameter("@Answer", ClsCommon.EnsureString(Answer));
            oparam[12] = new SqlParameter("@Relation", ClsCommon.EnsureString(Relation));
            oparam[13] = new SqlParameter("@NoticePeriod", ClsCommon.EnsureString(NoticePeriod));
            oparam[14] = new SqlParameter("@Position", ClsCommon.EnsureString(Position));
            oparam[15] = new SqlParameter("@AppliedDate", ClsCommon.EnsureString(AppliedDate));
            oparam[16] = new SqlParameter("@outcome", ClsCommon.EnsureString(outcome));
            oparam[17] = new SqlParameter("@Period", ClsCommon.EnsureString(Period));
            oparam[18] = new SqlParameter("@Location", ClsCommon.EnsureString(Location));
            oparam[19] = new SqlParameter("@DOL", ClsCommon.EnsureString(DOL));
            oparam[20] = new SqlParameter("@ReasonL", ClsCommon.EnsureString(ReasonL));
            oparam[21] = new SqlParameter("@Specify", ClsCommon.EnsureString(Specify));
            oparam[22] = new SqlParameter("@Course", ClsCommon.EnsureString(Course));
            oparam[23] = new SqlParameter("@University", ClsCommon.EnsureString(University));
            oparam[24] = new SqlParameter("@year", ClsCommon.EnsureString(year));
            oparam[25] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[26] = new SqlParameter("@createdby", LoginID);
            oparam[27] = new SqlParameter("@Sno", Sno);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_ApplyJob_Det", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }
        public static CommandResult fnSetREC_EScreening(long REC_ReqID, string REC_AppID, int Approved, string Remarks)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[2] = new SqlParameter("@Approved", Approved);
            oparam[3] = new SqlParameter("@Remarks", ClsCommon.EnsureString(Remarks));
            oparam[4] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EScreening", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetREC_EScreening_Approvers(long REC_ReqID, int SrNo, string ApproverID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@SrNo", SrNo);
            oparam[2] = new SqlParameter("@ApproverID", ApproverID);
            oparam[3] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EScreening_Approvers", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetREC_EShortlisted(long REC_ReqID, string REC_AppID, long ApproverID, long Preference, string Comment, int Approved)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[2] = new SqlParameter("@ApproverID", ApproverID);
            oparam[3] = new SqlParameter("@Preference", Preference);
            oparam[4] = new SqlParameter("@Comment", ClsCommon.EnsureString(Comment));
            oparam[5] = new SqlParameter("@Approved", Approved);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EShortlisted", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetREC_EFinalConfirmedCV(long REC_ReqID, string ApproveCandidate, string FinalComment)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@ApproveCandidate", ClsCommon.EnsureString(ApproveCandidate));
            oparam[2] = new SqlParameter("@FinalComment", ClsCommon.EnsureString(FinalComment));
            oparam[3] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EFinalConfirmedCV", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }
        public static CommandResult fnSetREC_EFinalConfirmedStatusCV(long REC_ReqID, string ApproveCandidate, string FinalComment, int Approved)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@ApproveCandidate", ClsCommon.EnsureString(ApproveCandidate));
            oparam[2] = new SqlParameter("@FinalComment", ClsCommon.EnsureString(FinalComment));
            oparam[3] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@Approved", Approved);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EFinalConfirmedStatusCV", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }



        public static CommandResult fnSetREC_EInterviewSetting(long REC_InterviewSetID, long REC_ReqID, int Srno, int IsNegotiationRound, string DocType, long LinkID, string RoundType, string RoundName, string RoundTitle,
            string RoundDesc, string RoundMemberType, long EMPID, string Name, string Email, string SlotDate, string MAXCV, string FromTime, string ToTime, int Priority, int IsActive)
        {
            CommandResult Result = new CommandResult();
            //long ID = 0;
            SqlParameter[] oparam = new SqlParameter[22];
            oparam[0] = new SqlParameter("@REC_InterviewSetID", REC_InterviewSetID);
            oparam[1] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[2] = new SqlParameter("@LinkID", LinkID);
            oparam[3] = new SqlParameter("@Srno", Srno);
            oparam[4] = new SqlParameter("@DocType", ClsCommon.EnsureString(DocType));
            oparam[5] = new SqlParameter("@RoundType", ClsCommon.EnsureString(RoundType));
            oparam[6] = new SqlParameter("@RoundName", ClsCommon.EnsureString(RoundName));
            oparam[7] = new SqlParameter("@RoundTitle", ClsCommon.EnsureString(RoundTitle));
            oparam[8] = new SqlParameter("@RoundDesc", ClsCommon.EnsureString(RoundDesc));
            oparam[9] = new SqlParameter("@RoundMemberType", ClsCommon.EnsureString(RoundMemberType));
            oparam[10] = new SqlParameter("@EMPID", EMPID);
            oparam[11] = new SqlParameter("@IsNegotiationRound", IsNegotiationRound);
            oparam[12] = new SqlParameter("@Name", ClsCommon.EnsureString(Name));
            oparam[13] = new SqlParameter("@Email", ClsCommon.EnsureString(Email));
            oparam[14] = new SqlParameter("@SlotDate", ClsCommon.EnsureString(SlotDate));
            oparam[15] = new SqlParameter("@MAXCV", ClsCommon.EnsureString(MAXCV));
            oparam[16] = new SqlParameter("@FromTime", ClsCommon.EnsureString(FromTime));
            oparam[17] = new SqlParameter("@ToTime", ClsCommon.EnsureString(ToTime));
            oparam[18] = new SqlParameter("@IsActive", IsActive);
            oparam[19] = new SqlParameter("@Priority", Priority);
            oparam[20] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[21] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EInterviewSetting", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }



        public static CommandResult fnSetREC_DelInterviewRound(long REC_InterviewSetID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@REC_InterviewSetID", REC_InterviewSetID);
            oparam[1] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_DelInterviewRound", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }

        public static CommandResult fnSetREC_SyncInterviewRound(long REC_ReqID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_SyncInterviewRound", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }

        public static CommandResult fnSetREC_EInterview(long REC_InterviewSetID, long REC_ReqID, long REC_AppID, long SlotID, string Score, string Remarks, decimal NegotiationSalary, string ExpectedJDate, int Approved, string REC_Tags)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@REC_InterviewSetID", REC_InterviewSetID);
            oparam[1] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[2] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[3] = new SqlParameter("@SlotID", SlotID);
            oparam[4] = new SqlParameter("@Score", Score);
            oparam[5] = new SqlParameter("@Remarks", ClsCommon.EnsureString(Remarks));
            oparam[6] = new SqlParameter("@NegotiationSalary", NegotiationSalary);
            oparam[7] = new SqlParameter("@ExpectedJDate", ClsCommon.EnsureString(ExpectedJDate));
            oparam[8] = new SqlParameter("@Approved", Approved);
            oparam[9] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[10] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[11] = new SqlParameter("@REC_Tags", ClsCommon.EnsureString(REC_Tags));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EInterview", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetREC_EInterviewConfirm(long REC_InterviewSetID, long REC_ReqID, long REC_AppID, long SlotID, string Score, string Remarks, decimal NegotiationSalary, string ExpectedJDate, int Approved, string REC_Tags)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@REC_InterviewSetID", REC_InterviewSetID);
            oparam[1] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[2] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[3] = new SqlParameter("@SlotID", SlotID);
            oparam[4] = new SqlParameter("@Score", Score);
            oparam[5] = new SqlParameter("@Remarks", ClsCommon.EnsureString(Remarks));
            oparam[6] = new SqlParameter("@NegotiationSalary", NegotiationSalary);
            oparam[7] = new SqlParameter("@ExpectedJDate", ClsCommon.EnsureString(ExpectedJDate));
            oparam[8] = new SqlParameter("@Approved", Approved);
            oparam[9] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[10] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[11] = new SqlParameter("@REC_Tags", ClsCommon.EnsureString(REC_Tags));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EInterviewConfirm", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetREC_InterviewRoundHistory(InterviewSelection.QulifiedRound Modal)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@RoundID", Modal.RoundID);
            oparam[1] = new SqlParameter("@RoundName", Modal.RoundName);
            oparam[2] = new SqlParameter("@REC_InterviewSetID", Modal.REC_InterviewSetID);
            oparam[3] = new SqlParameter("@REC_ReqID", Modal.REC_ReqID);
            oparam[4] = new SqlParameter("@REC_InterviewID", Modal.REC_InterviewID);
            oparam[5] = new SqlParameter("@Score", Modal.Score);
            oparam[6] = new SqlParameter("@Remarks", Modal.Remarks);
            oparam[7] = new SqlParameter("@PanelMember", Modal.PanelMember);
            oparam[8] = new SqlParameter("@SlotDate", Modal.SlotDate);
            oparam[9] = new SqlParameter("@REC_AppID", Modal.REC_AppID);

            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_InterviewRoundHistory", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetREC_TalentPoolSelection(long REC_AppID, string Reason)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[1] = new SqlParameter("@Reason", ClsCommon.EnsureString(Reason));
            oparam[2] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[3] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_TalentPoolSelection", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetREC_TalentPool(long REC_AppID, string Reason)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[1] = new SqlParameter("@Reason", ClsCommon.EnsureString(Reason));
            oparam[2] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[3] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_TalentPool", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetREC_ImportFromTalentPool(long REC_ReqID, string REC_TalentID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[1] = new SqlParameter("@REC_TalentID", REC_TalentID);
            oparam[2] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[3] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_ImportFromTalentPool", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static PostResponse SetUser_Task(User_Task.Task.AddTask model)
        {
            PostResponse Result = new PostResponse();
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@TaskID", model.TaskID);
            oparam[1] = new SqlParameter("@Subject", model.Subject);
            oparam[2] = new SqlParameter("@Message", model.Message);
            oparam[3] = new SqlParameter("@Priority", model.Priority);
            oparam[4] = new SqlParameter("@AssigenedToIDs", model.AssigenedToIDs);
            oparam[5] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetUser_Task", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static PostResponse SetUser_Task_Tran(User_Task.Notes.Add model)
        {
            PostResponse Result = new PostResponse();
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@TaskID", model.TaskID);
            oparam[1] = new SqlParameter("@StatusID", model.StatusID);
            oparam[2] = new SqlParameter("@NextDate", ClsCommon.EnsureString(model.NextDate));
            oparam[3] = new SqlParameter("@Remarks", model.Remarks);
            oparam[4] = new SqlParameter("@NewAssigenedToIDs", ClsCommon.EnsureString(model.NewAssigenedToIDs));
            oparam[5] = new SqlParameter("@newDeferredIDs", ClsCommon.EnsureString(model.NewDeferredIDs));
            oparam[6] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[7] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetUser_Task_Tran", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }



        public static PostResponse SetUser_Task_Status(User_Task.StatusList model)
        {
            PostResponse Result = new PostResponse();
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@StatusID", model.StatusID);
            oparam[1] = new SqlParameter("@Type", model.Type);
            oparam[2] = new SqlParameter("@StatusName", ClsCommon.EnsureString(model.StatusName));
            oparam[3] = new SqlParameter("@DisplayName", ClsCommon.EnsureString(model.DisplayName));
            oparam[4] = new SqlParameter("@StatusColor", ClsCommon.EnsureString(model.StatusColor));
            oparam[5] = new SqlParameter("@UseFor", ClsCommon.EnsureString(model.UseFor));
            oparam[6] = new SqlParameter("@Priority", model.Priority ?? 0);
            oparam[7] = new SqlParameter("@IsActive", model.IsActive);
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[9] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetUser_Task_Status", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static PostResponse SetExit_Request(Exit.Request.Add model)
        {
            PostResponse Result = new PostResponse();
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@Exit_ID", model.Exit_ID);
            oparam[1] = new SqlParameter("@EMP_ID", model.EMP_ID);
            oparam[2] = new SqlParameter("@Resignation_Reason", ClsCommon.EnsureString(model.Resignation_Reason));
            oparam[3] = new SqlParameter("@Comment", ClsCommon.EnsureString(model.Comment));
            oparam[4] = new SqlParameter("@IsServeNotice", model.IsServeNotice);
            oparam[5] = new SqlParameter("@RelievingDate", ClsCommon.EnsureString(model.RelievingDate));
            oparam[6] = new SqlParameter("@Actual_RelievingDate", ClsCommon.EnsureString(model.Actual_RelievingDate));
            oparam[7] = new SqlParameter("@ReasonForNotServingNotice", ClsCommon.EnsureString(model.ReasonForNotServingNotice));
            oparam[8] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[9] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetExit_Request", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }



        public static PostResponse SetExit_Retained(long Exit_ID, string RetainedAt_Level, string Retained_Remarks, long Retained_Attachment)
        {
            PostResponse Result = new PostResponse();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@Exit_ID", Exit_ID);
            oparam[1] = new SqlParameter("@RetainedAt_Level", RetainedAt_Level);
            oparam[2] = new SqlParameter("@Retained_Remarks", ClsCommon.EnsureString(Retained_Remarks));
            oparam[3] = new SqlParameter("@Retained_Attachment", Retained_Attachment);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetExit_Retained", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }


        public static PostResponse SetExit_Approvers(long Exit_APP_ID, long Exit_ID, string Doctype, long ApproverID)
        {
            PostResponse Result = new PostResponse();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@Exit_APP_ID", Exit_APP_ID);
            oparam[1] = new SqlParameter("@Exit_ID", Exit_ID);
            oparam[2] = new SqlParameter("@Doctype", Doctype);
            oparam[3] = new SqlParameter("@ApproverID", ApproverID);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetExit_Approvers", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }



        #endregion
        #region SMPR Mails	
        public static DataSet fnCreateMail_SMPR(long id, string Action)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] oparam = new SqlParameter[3];
                oparam[0] = new SqlParameter("@id", id);
                oparam[1] = new SqlParameter("@RequestStatus", Action);
                oparam[2] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
                ds = clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_SMPR", oparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion
        #region PTC 
        public static DataSet fnGetNonMitrList()
        {
            return clsDataBaseHelper.ExecuteDataSet("exec spu_GetNoNMitrList");
        }
        public static DataSet fnGetPTC_Objective(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@ApprovedId", Approved);
            oparam[1] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPTC_CreationofObjective", oparam);
        }
        public static DataSet fnGetPTC_ObjectiveMylist(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@ApprovedId", Approved);
            oparam[1] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPTC_MyGoalList", oparam);
        }
        public static DataSet fnGetPTC_TeamList(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@ApprovedId", Approved);
            oparam[1] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPTC_TeamList", oparam);
        }
        public static DataSet fnGetPTC_Confimerlist(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@ApprovedId", Approved);
            oparam[1] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[2] = new SqlParameter("@CNFEMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPTC_ConfimerTeamList", oparam);
        }
        public static DataSet fnGetPTC_CMClist(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@ApprovedId", Approved);
            oparam[1] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPTC_CMCList", oparam);
        }
        public static DataSet fnGetPTC_HRlist(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@ApprovedId", Approved);
            oparam[1] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPTC_HRList", oparam);
        }
        public static DataSet fnGetPTC_Essential(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@ApprovedId", Approved);
            oparam[1] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetPTC_HierarchyMaster", oparam);
        }
        public static CommandResult fnSetPTC_Master(long Id, long EMPID, long Confirmer)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@Confirmer", Confirmer);
            oparam[3] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_Master", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetPTC_ProbationObjective(long Id, long EMPID, string Comment, long Approved, long draft)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@Comment", ClsCommon.EnsureString(Comment));
            oparam[3] = new SqlParameter("@Approved", Approved);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@draft", draft);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_ProbationObjective", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }


        public static CommandResult fnSetPTC_spu_SetPTC_ProbationObjectivePeriod(long Id, long PBID, long KPID, long UOMID, long Weight, string ProbationObjective, string ProbationRemark, long EMPID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[10];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@PBID", PBID);
            oparam[2] = new SqlParameter("@KPID", KPID);
            oparam[3] = new SqlParameter("@UOMID", UOMID);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@Weight", Weight);
            oparam[6] = new SqlParameter("@ProbationObjective", ClsCommon.EnsureString(ProbationObjective));
            oparam[7] = new SqlParameter("@ProbationRemark", ClsCommon.EnsureString(ProbationRemark));
            oparam[8] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[9] = new SqlParameter("@EMPID", EMPID);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_ProbationObjectivePeriod", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetPTC_spu_SetPTC_ProbationObjectivePeriodUpdate(long Id, long PBID, long EMPID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@PBID", PBID);
            oparam[2] = new SqlParameter("@EMPID", EMPID);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_ProbationObjectivePeriodupdateNew", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetPTC_ProbationObjectiveUser(long Id, long EMPID, string UserComment, long Approved, long draft)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@UserComment", ClsCommon.EnsureString(UserComment));
            oparam[3] = new SqlParameter("@Approved", Approved);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@draft", draft);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_ProbationObjectiveUpdate", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }


        public static CommandResult fnSetPTC_spu_SetPTC_ProbationObjectivePeriodUser(long Id, long PBID, long Target)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@PBID", PBID);
            oparam[2] = new SqlParameter("@Target", Target);
            oparam[3] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[4] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_ProbationObjectivePeriodupdate", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetPTC_ProbationObjectiveResubmit(long Id, string Comment)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@Comment", Comment);
            oparam[2] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[3] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_ProbationObjectiveResubmit", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult fnSetPTC_SelfAppraisal(long Id, string UserRemark, long Approved, long draft, string SupervisiorRemark, string ResubmitComment)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@UserRemark", ClsCommon.EnsureString(UserRemark));
            oparam[3] = new SqlParameter("@Approved", Approved);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@draft", draft);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@SupervisiorRemark", ClsCommon.EnsureString(SupervisiorRemark));
            oparam[8] = new SqlParameter("@ResubmitComment", ClsCommon.EnsureString(ResubmitComment));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_PTC_SelfAppraisal", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetPTC_FinalAppraisal(long Id, string HodFinalComment, long Approved)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@HodFinalComment", ClsCommon.EnsureString(HodFinalComment));
            oparam[3] = new SqlParameter("@Approved", Approved);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_FinalTeamAppraisal", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetPTC_ConfimerAppraisal(long Id, string Comment, long Approved, string Isagree, long EMPID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@ConfimerRemark", ClsCommon.EnsureString(Comment));
            oparam[3] = new SqlParameter("@Approved", Approved);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@Isagree", Isagree);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_ConfimerAppraisal", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetPTC_CMCAppraisal(long Id, string Comment, long Approved, string Isagree, long EMPID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@ConfimerRemark", ClsCommon.EnsureString(Comment));
            oparam[3] = new SqlParameter("@Approved", Approved);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@Isagree", Isagree);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_CMCAppraisal", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetPTC_HRAppraisal(long Id, string Comment, long Approved, long EMPID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@EMPID", EMPID);
            oparam[2] = new SqlParameter("@ConfimerRemark", ClsCommon.EnsureString(Comment));
            oparam[3] = new SqlParameter("@Approved", Approved);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_HRAppraisal", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetPTC_FinalSubmitAppraisalReview(long Id, long AppraisalID, string AppraisalType, long FinalScore, long SystemScore, long ModificationTypeID, string ModificationType, long ModificationChangeID, string TypeName, string Reason, string ProbationEndDate, long EMPID)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[14];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@AppraisalTypeID", AppraisalID);
            oparam[2] = new SqlParameter("@AppraisalType", ClsCommon.EnsureString(AppraisalType));
            oparam[3] = new SqlParameter("@FinalScore", FinalScore);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@SystemScore", SystemScore);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@ModificationTypeID", ModificationTypeID);
            oparam[8] = new SqlParameter("@ModificationType", ClsCommon.EnsureString(ModificationType));
            oparam[9] = new SqlParameter("@ModificationChangeID", ModificationChangeID);
            oparam[10] = new SqlParameter("@Reason", ClsCommon.EnsureString(Reason));
            oparam[11] = new SqlParameter("@Submitted", ClsCommon.EnsureString(TypeName));
            oparam[12] = new SqlParameter("@ProbationPeriodEndDate", ClsCommon.EnsureString(ProbationEndDate));
            oparam[13] = new SqlParameter("@EMPID", EMPID);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_AppraisalReview", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetPTC_SubmitFinalAppraisal(long Id, string UserFinalComment, long Approved)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[2] = new SqlParameter("@UserFinalComment", ClsCommon.EnsureString(UserFinalComment));
            oparam[3] = new SqlParameter("@Approved", Approved);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_FinalSelfAppraisal", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetPTC_SelfAppraisalKPA(long Id, long APID, long KPID, string ProbationObjective, long UOMID, long Weight, long TargetAchievement, string Achievement, long Target, string UserRemark, string SupervisorComment, long AppraiserScore)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[14];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@APID", APID);
            oparam[2] = new SqlParameter("@KPID", KPID);
            oparam[3] = new SqlParameter("@ProbationObjective", ProbationObjective);
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@UOMID", UOMID);
            oparam[6] = new SqlParameter("@Weight", Weight);
            oparam[7] = new SqlParameter("@TargetAchievement", TargetAchievement);
            oparam[8] = new SqlParameter("@Achievement", Achievement);
            oparam[9] = new SqlParameter("@Target", Target);
            oparam[10] = new SqlParameter("@UserRemark", UserRemark);
            oparam[11] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[12] = new SqlParameter("@SupervisorComment", ClsCommon.EnsureString(SupervisorComment));
            oparam[13] = new SqlParameter("@AppraiserScore", AppraiserScore);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_SelfAppraisalKPA", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }


        public static CommandResult fnSetPTC_AppraisalQuestion(long Id, long APID, string Question, string UserAnswer, long QID, string SupervisiorAnswer)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@APID", APID);
            oparam[2] = new SqlParameter("@Question", ClsCommon.EnsureString(Question));
            oparam[3] = new SqlParameter("@UserAnswer", ClsCommon.EnsureString(UserAnswer));
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@QID", QID);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@SupervisiorAnswer", ClsCommon.EnsureString(SupervisiorAnswer));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_AppraisalQuestion", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult fnSetPTC_AppraisalTraining(long Id, long APID, long TrainingTypeID, string TrainingRemarks, string ReasonDeclining)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@APID", APID);
            oparam[2] = new SqlParameter("@TrainingTypeID", TrainingTypeID);
            oparam[3] = new SqlParameter("@TrainingRemarks", ClsCommon.EnsureString(TrainingRemarks));
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[6] = new SqlParameter("@ReasonDeclining", ClsCommon.EnsureString(ReasonDeclining));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetPTC_AppraisalTraining", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        #endregion



        #region Exit
        public static DataSet fnGetExit_HRlist(long Approved)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Approved", Approved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetExitHRList", oparam);
        }

        public static CommandResult FnSetResignation(string lId, long lEmpid, long lReasonid, string sComment, int iNoticePeriodServe, DateTime dRelievingDate, int iRelievingDay, string sReasonNP, string sDoctype, long OBEmpid)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[11];
            oparam[0] = new SqlParameter("@id", lId);
            oparam[1] = new SqlParameter("@empid", lEmpid);
            oparam[2] = new SqlParameter("@reasonid", lReasonid);
            oparam[3] = new SqlParameter("@comment", sComment);
            oparam[4] = new SqlParameter("@noticeperiodserve", iNoticePeriodServe);
            oparam[5] = new SqlParameter("@relievingdate", dRelievingDate);
            oparam[6] = new SqlParameter("@relievingday", iRelievingDay);
            oparam[7] = new SqlParameter("@reasonNP", sReasonNP);
            oparam[8] = new SqlParameter("@doctype", sDoctype);
            oparam[9] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[10] = new SqlParameter("@OBEmpid", OBEmpid);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_ExitSetResignation", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }


        #endregion

        public static DataSet fnGetConsolidatedSalaryReport(string EmpType, string FromDate, string ToDate)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@EmpType", EmpType);
            oparam[1] = new SqlParameter("@FromDate", FromDate);
            oparam[2] = new SqlParameter("@ToDate", ToDate);
            oparam[3] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_GetConsolidatedSalaryAllocationtListYearly_new", oparam);
        }
        public static DataSet fnGetConsolidatedActivityLogReport(long Empid, string FromDate, string ToDate)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@Empid", Empid);
            oparam[1] = new SqlParameter("@FromDate", FromDate);
            oparam[2] = new SqlParameter("@ToDate", ToDate);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetConsolidatedActivityLogReport", oparam);
        }
        public static DataSet fnGetGetALAccrual_TakeReport(string Empid, string EmpType, string FromDate, string ToDate)
        {
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@EmpType", EmpType);
            oparam[1] = new SqlParameter("@FromDate", FromDate);
            oparam[2] = new SqlParameter("@ToDate", ToDate);
            oparam[3] = new SqlParameter("@Empid", Empid);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetGetALAccrual_TakeReport", oparam);
        }
        public static PostResponse SetLoginNonmitrUsers(UserMan.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    string first_name = "", middle_name = "", last_name = "";
                    if (!string.IsNullOrEmpty(modal.Name))
                    {
                        if (modal.Name.Contains(' '))
                        {
                            var a = modal.Name.Split(' ');
                            if (a.Length == 3)
                            {
                                first_name = a[0];
                                middle_name = a[1];
                                last_name = a[2];
                            }
                            else if (a.Length == 2)
                            {
                                first_name = a[0];
                                last_name = a[1];
                            }
                            else
                            {
                                first_name = a[0];
                            }
                        }
                        else
                        {
                            first_name = modal.Name;
                        }
                    }

                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetNonmitrUsers", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = modal.UserType ?? "";
                        command.Parameters.Add("@user_name", SqlDbType.VarChar).Value = modal.user_name ?? "";
                        command.Parameters.Add("@password", SqlDbType.VarChar).Value = clsApplicationSetting.Encrypt(modal.password) ?? "";
                        command.Parameters.Add("@first_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(first_name);
                        command.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(middle_name);
                        command.Parameters.Add("@last_name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(last_name);
                        command.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = modal.EMPID;
                        command.Parameters.Add("@email", SqlDbType.VarChar).Value = modal.email ?? "";
                        command.Parameters.Add("@RoleIDs", SqlDbType.VarChar).Value = modal.RoleIDs ?? "";
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public static int fnSetBankDetailsSetAction(long EmpId, string DocType)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@EMPID", EmpId);
            oparam[1] = new SqlParameter("@Doctype", DocType);
            clsDataBaseHelper.ExecuteSp("spu_SetEMP_Bankdetails", oparam);
            return 1;
        }
        public static int fnSetPersonalDetailsSetAction(long EmpId, string Column, string DocType, string ColumnUpdate, long UID)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@EMPID", EmpId);
            oparam[1] = new SqlParameter("@Column", Column);
            oparam[2] = new SqlParameter("@Doctype", DocType);
            oparam[4] = new SqlParameter("@ColumnUpdate", ColumnUpdate);
            oparam[5] = new SqlParameter("@UID", UID);
            clsDataBaseHelper.ExecuteSp("spu_SetEMP_PersonaldetailsUpdate", oparam);
            return 1;
        }
        public static int fnSetPersonalDetailsRejectAction(long EmpId, string Column, string DocType, string ColumnUpdate, long UID)
        {
            SqlParameter[] oparam = new SqlParameter[6];
            oparam[0] = new SqlParameter("@EMPID", EmpId);
            oparam[1] = new SqlParameter("@Column", Column);
            oparam[2] = new SqlParameter("@Doctype", DocType);
            oparam[4] = new SqlParameter("@ColumnUpdate", ColumnUpdate);
            oparam[5] = new SqlParameter("@UID", UID);
            clsDataBaseHelper.ExecuteSp("spu_SetEMP_PersonaldetailsReject", oparam);
            return 1;
        }
        public static int fnSetPersonalTravelPreferenceSetAction(long EmpId, string AirlineName, string DocType, string FlyerNo, long AirId)
        {
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@EMPID", EmpId);
            oparam[1] = new SqlParameter("@AirlineName", AirlineName);
            oparam[2] = new SqlParameter("@Doctype", DocType);
            oparam[4] = new SqlParameter("@FlyerNo", FlyerNo);
            oparam[5] = new SqlParameter("@AirId", AirId);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            clsDataBaseHelper.ExecuteSp("spu_SetEMP_FlyerdetailsUpdate", oparam);
            return 1;
        }
        public static int fnRejectSetPersonalTravelPreferenceSetAction(long EmpId, string AirlineName, string DocType, string FlyerNo, long AirId)
        {
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@EMPID", EmpId);
            oparam[1] = new SqlParameter("@AirlineName", AirlineName);
            oparam[2] = new SqlParameter("@Doctype", DocType);
            oparam[4] = new SqlParameter("@FlyerNo", FlyerNo);
            oparam[5] = new SqlParameter("@AirId", AirId);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            clsDataBaseHelper.ExecuteSp("spu_SetEMP_FlyerdetailsUpdateReject", oparam);
            return 1;
        }
        public static int fnSetPersonalQualificationSetAction(long EmpId, string Course, string DocType, string University, string Location, string Year, long QID)
        {
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@EMPID", EmpId);
            oparam[1] = new SqlParameter("@Course", Course);
            oparam[2] = new SqlParameter("@Doctype", DocType);
            oparam[4] = new SqlParameter("@University", University);
            oparam[5] = new SqlParameter("@Location", Location);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@Year", Year);
            oparam[8] = new SqlParameter("@QID", QID);
            clsDataBaseHelper.ExecuteSp("spu_SetEMP_QualificationUpdate", oparam);
            return 1;
        }
        public static int fnRejectSetPersonalQualificationSetAction(long EmpId, string Course, string DocType, string University, string Location, string Year, long QID)
        {
            SqlParameter[] oparam = new SqlParameter[9];
            oparam[0] = new SqlParameter("@EMPID", EmpId);
            oparam[1] = new SqlParameter("@Course", Course);
            oparam[2] = new SqlParameter("@Doctype", DocType);
            oparam[4] = new SqlParameter("@University", University);
            oparam[5] = new SqlParameter("@Location", Location);
            oparam[6] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[7] = new SqlParameter("@Year", Year);
            oparam[8] = new SqlParameter("@QID", QID);
            clsDataBaseHelper.ExecuteSp("spu_SetEMP_QualificationUpdateReject", oparam);
            return 1;
        }
        public static int fnApprovedetPersonaDocSetAction(long AttachmentID, long MUID, long EMPID, string DocType)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@AttachmentID", AttachmentID);
            oparam[1] = new SqlParameter("@MUID", MUID);
            oparam[2] = new SqlParameter("@EMPID", EMPID);
            oparam[3] = new SqlParameter("@Doctype", DocType);
            oparam[4] = new SqlParameter("@Status", 1);
            clsDataBaseHelper.ExecuteSp("spu_GetEMP_AttachementUpdateSetDetails", oparam);
            return 1;
        }
        public static int fnRejectetPersonaDocSetAction(long AttachmentID, long MUID, long EMPID, string DocType)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@AttachmentID", AttachmentID);
            oparam[1] = new SqlParameter("@MUID", MUID);
            oparam[2] = new SqlParameter("@EMPID", EMPID);
            oparam[3] = new SqlParameter("@Doctype", DocType);
            oparam[4] = new SqlParameter("@Status", 2);
            clsDataBaseHelper.ExecuteSp("spu_GetEMP_AttachementUpdateSetDetails", oparam);
            return 1;
        }
        public static DataSet fnCreateMail_DailyActivityCompOff(string Date, string Hours, long CompensatoryOffID, string Command, string Reason)
        {
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@Date", Date);
            oparam[1] = new SqlParameter("@Hours", Hours);
            oparam[2] = new SqlParameter("@EMPID", clsApplicationSetting.GetSessionValue("EMPID"));
            oparam[3] = new SqlParameter("@CompensatoryOffID", CompensatoryOffID);
            oparam[4] = new SqlParameter("@Type", Command);
            oparam[5] = new SqlParameter("@Reason", Reason);
            oparam[6] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_DailyActivityCompOff", oparam);
        }
        public static DataSet fnCreateMailHOD_DailyActivityCompOff(string Date, string Hours, long CompensatoryOffID, string Command, string Reason, long EmpId)
        {
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@Date", Date);
            oparam[1] = new SqlParameter("@Hours", Hours);
            oparam[2] = new SqlParameter("@EMPID", EmpId);
            oparam[3] = new SqlParameter("@CompensatoryOffID", CompensatoryOffID);
            oparam[4] = new SqlParameter("@Type", Command);
            oparam[5] = new SqlParameter("@Reason", Reason);
            oparam[6] = new SqlParameter("@UserId", clsApplicationSetting.GetSessionValue("LoginID"));
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_DailyActivityCompOff", oparam);
        }
        public static DataSet fnGetMobileLogin(string UserID, string Password, string SessionID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@UserID", UserID);
            oparam[1] = new SqlParameter("@Password", Password);
            oparam[2] = new SqlParameter("@SessionID", SessionID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLoginNonmitr", oparam);
        }
        public static DataSet fnGetRollId(long UserId)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@UserId", UserId);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetRoleID", oparam);
        }
        public static CommandResult fnSetUser_Registration(long Id, string user_name, string email, string Mobile, string Password)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@user_name", ClsCommon.EnsureString(user_name));
            oparam[2] = new SqlParameter("@email", email);
            oparam[3] = new SqlParameter("@Mobile_no", Mobile);
            oparam[4] = new SqlParameter("@Password", Password);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetUSERRegistration", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;

        }
        public static DataSet fnCreateMail_Termbaseactivitylog(string date, string Command, string remarks, long EmpidNew)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@date", date);
            oparam[1] = new SqlParameter("@Hodid", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[2] = new SqlParameter("@Command", Command);
            oparam[3] = new SqlParameter("@remarks", remarks);
            oparam[4] = new SqlParameter("@EmpidNew", EmpidNew);
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_Termbaseactivitylog", oparam);
        }

        public static DataSet fnGetMobileLoginGlobal(string UserID, string Password, string SessionID)
        {
            SqlParameter[] oparam = new SqlParameter[3];
            oparam[0] = new SqlParameter("@UserID", UserID);
            oparam[1] = new SqlParameter("@Password", Password);
            oparam[2] = new SqlParameter("@SessionID", SessionID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLoginGlobalUser", oparam);
        }

        public static DataSet fnGetMobileLoginIOS(long UserID)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@UserID", UserID);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLoginDataforIOSMobile", oparam);
        }

        public static long fnSetGlobalUser(long Id, string EmpName, string Mobile, string Email, string Address, string zipcode, string DOB, string Companyname)
        {
            long fnSetGlobalUser = 0;
            SqlParameter[] oparam = new SqlParameter[8];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@EmployeeName", ClsCommon.EnsureString(EmpName));
            oparam[2] = new SqlParameter("@EMail", ClsCommon.EnsureString(Email));
            oparam[3] = new SqlParameter("@Address", Address);
            oparam[4] = new SqlParameter("@ZipCode", zipcode);
            oparam[5] = new SqlParameter("@DOB", DOB);
            oparam[6] = new SqlParameter("@Mobile", Mobile);
            oparam[7] = new SqlParameter("@CompanyName", Companyname);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetUSERRegistrationGlobal", oparam);

            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetGlobalUser = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
                fnSetGlobalUser = 0;
            return fnSetGlobalUser;
        }
        public static int UpdateOnboardUserStatus(int CandidateId)
        {
            int fnSetGlobalUser = 0;
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@CandidateId", CandidateId);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_Onboarding_UpdateUserStatus", oparam);

            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetGlobalUser = Convert.ToInt32(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
            {
                fnSetGlobalUser = 0;
            }
            return fnSetGlobalUser;
        }
        public static DataSet GetCandidateMasterID(int Candidateid)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Candidateid", Candidateid);
            return clsDataBaseHelper.ExecuteDataSet("spu_Onboarding_GetCandidateMasterid", oparam);
        }

        public static int UpdateUserStatus(int Id, int UserManID)
        {
            int fnSetGlobalUser = 0;
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Id", Id);
            oparam[1] = new SqlParameter("@UserManID", UserManID);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_UpdateUserStatus", oparam);

            if (ds.Tables[0].Rows.Count > 0)
            {
                fnSetGlobalUser = Convert.ToInt32(ds.Tables[0].Rows[0]["RET_ID"]);
            }
            else
            {
                fnSetGlobalUser = 0;
            }
            return fnSetGlobalUser;
        }
        public static CommandResult UpdateVideoReviewPreRegistration(string CandidateId, string VideoReviewed)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@CandidateId", CandidateId);
            oparam[1] = new SqlParameter("@VideoReviewed", VideoReviewed);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_UpdateVideoReview", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RETID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static CommandResult UpdateReviewCommentsPreRegistration(string CandidateId, string ReviewComments)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@CandidateId", CandidateId);
            oparam[1] = new SqlParameter("@ReviewComments", ReviewComments);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_UpdateReviewComments", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RETID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }

        public static DataSet GetExitEmployeeDetails(int Approved, string Exit_HP_ID, string Exit_ID)
        {
            DataSet ds = null;
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            try
            {
                SqlParameter[] oparam = new SqlParameter[4];
                oparam[0] = new SqlParameter("@LoginID", LoginID);
                oparam[1] = new SqlParameter("@Approved", Approved);
                oparam[2] = new SqlParameter("@ExitHPID", Exit_HP_ID);
                oparam[3] = new SqlParameter("@ExitID", Exit_ID);
                ds = clsDataBaseHelper.ExecuteDataSet("spu_GetExitEmployeeDetails", oparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }
        public static DataSet fnGetMobileLoginGlobalSendDetails(string UserID, string Token)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@UserID", UserID);
            oparam[1] = new SqlParameter("@Token", Token);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetLoginGlobalUserSendDetails", oparam);
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
        public static DataSet GetONB_UserProcessStatus(int Candidateid)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Candidateid", Candidateid);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetUserProcessStatus", oparam);
        }
        public static DataSet GetOnboardingEmpStatus(int Candidateid)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Candidateid", Candidateid);
            return clsDataBaseHelper.ExecuteDataSet("spu_OnboardingEmpStatus", oparam);
        }
        public static DataSet GetOnboardingCurrentMasterEmpStatus(int Candidateid, int Status)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@Candidateid", Candidateid);
            oparam[1] = new SqlParameter("@Status", Status);
            return clsDataBaseHelper.ExecuteDataSet("spu_OnboardingCurrentMasterEmpStatus", oparam);
        }
        public static CommandResult OnboardingUpdateEmpStatus(int CandidateId)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@CandidateId", CandidateId);
            oparam[1] = new SqlParameter("@UserID", clsApplicationSetting.GetSessionValue("LoginID"));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_OnboardingUpdateEmpStatus", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RETID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult OnboardingUpdateEmpMasterStatus(int CandidateId, string MasterEmployeeStatus)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@CandidateId", CandidateId);
            oparam[1] = new SqlParameter("@MasterEmployeeStatus", MasterEmployeeStatus);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_OnboardingUpdateEmpMasterStatus", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RETID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static CommandResult RegistrationCompletedStatus(int CandidateId, int Status, string StatusText, int Createdby)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[4];
            oparam[0] = new SqlParameter("@CandidateId", CandidateId);
            oparam[1] = new SqlParameter("@Status", Status);
            oparam[2] = new SqlParameter("@StatusText", StatusText);
            oparam[3] = new SqlParameter("@Createdby", Createdby);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_RegistrationCompletedStatus", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RETID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        public static PostResponse fnGetCheckRecordLeaveExist(GetRecordExitsResponse Modal)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetCheckRecordLeaveExist", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = Modal.ID;
                        command.Parameters.Add("@lastworking_day", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Modal.Doctype);
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during fnGetCheckRecordLeaveExist. The query was executed :", ex.ToString(), "spu_GetCheckRecordLeaveExist", "Common_SPU", "Common_SPU", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }

        public static CommandResult fnSetREC_EFinalConfirmedStatusCVOrDrop(long REC_AppID, long REC_ReqID, string ApproveCandidate, string FinalComment, int Approved)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[7];
            oparam[0] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[1] = new SqlParameter("@ApproveCandidate", ClsCommon.EnsureString(ApproveCandidate));
            oparam[2] = new SqlParameter("@FinalComment", ClsCommon.EnsureString(FinalComment));
            oparam[3] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[4] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[5] = new SqlParameter("@Approved", Approved);
            oparam[6] = new SqlParameter("@REC_ReqID", REC_ReqID);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EFinalConfirmedStatusCVOrDrop", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }

        #region  Direct Recruitment Process
        public static DataSet fnGetREC_PendancyDirect(int iApproved)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@LoginID", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[1] = new SqlParameter("@Approved", iApproved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_PendancyDirect", oparam);
        }

        public static DataSet fnGetREC_RequestsListDirect(int iApproved)
        {
            SqlParameter[] oparam = new SqlParameter[1];
            oparam[0] = new SqlParameter("@Approved", iApproved);
            return clsDataBaseHelper.ExecuteDataSet("spu_GetREC_RequestsListDirect", oparam);
        }
        public static CommandResult fnSetREC_ApplyJobDirect(string Code, long REC_ReqID, string Name, string DOB, string Gender, string Nationality, string Mobile, string EmailID, string Address,
           string TotalExperience, string ThematicAreaID, string Resposibilities, string ChangeReason, string BreakReason, string Skills, long CVAttachID, decimal CurrentSalary,
           decimal ExpectedSalary)
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[21];
            oparam[0] = new SqlParameter("@Code", ClsCommon.EnsureString(Code));
            oparam[1] = new SqlParameter("@Name", ClsCommon.EnsureString(Name));
            oparam[2] = new SqlParameter("@DOB", ClsCommon.EnsureString(DOB));
            oparam[3] = new SqlParameter("@Gender", ClsCommon.EnsureString(Gender));
            oparam[4] = new SqlParameter("@Nationality", ClsCommon.EnsureString(Nationality));
            oparam[5] = new SqlParameter("@Mobile", ClsCommon.EnsureString(Mobile));
            oparam[6] = new SqlParameter("@EmailID", ClsCommon.EnsureString(EmailID));
            oparam[7] = new SqlParameter("@Address", ClsCommon.EnsureString(Address));
            oparam[8] = new SqlParameter("@TotalExperience", ClsCommon.EnsureString(TotalExperience));
            oparam[9] = new SqlParameter("@ThematicAreaID", ClsCommon.EnsureString(ThematicAreaID));
            oparam[10] = new SqlParameter("@Resposibilities", ClsCommon.EnsureString(Resposibilities));
            oparam[11] = new SqlParameter("@ChangeReason", ClsCommon.EnsureString(ChangeReason));
            oparam[12] = new SqlParameter("@BreakReason", ClsCommon.EnsureString(BreakReason));
            oparam[13] = new SqlParameter("@Skills", ClsCommon.EnsureString(""));//ClsCommon.EnsureString(Skills)
            oparam[14] = new SqlParameter("@CVAttachID", CVAttachID);
            oparam[15] = new SqlParameter("@CurrentSalary", CurrentSalary);
            oparam[16] = new SqlParameter("@ExpectedSalary", ExpectedSalary);
            oparam[17] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[18] = new SqlParameter("@createdby", LoginID);
            oparam[19] = new SqlParameter("@AdditionalSkills", ClsCommon.EnsureString(Skills));
            oparam[20] = new SqlParameter("@REC_ReqID", REC_ReqID);
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_ApplyJobDirect", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;


        }
        public static CommandResult fnSetREC_EInterviewDirect(long REC_InterviewSetID, long REC_ReqID, long REC_AppID, long SlotID, string Score, string Reason, decimal NegotiationSalary, string ExpectedJDate, int Approved, string REC_Tags)
        {
            CommandResult Result = new CommandResult();
            SqlParameter[] oparam = new SqlParameter[12];
            oparam[0] = new SqlParameter("@REC_InterviewSetID", REC_InterviewSetID);
            oparam[1] = new SqlParameter("@REC_ReqID", REC_ReqID);
            oparam[2] = new SqlParameter("@REC_AppID", REC_AppID);
            oparam[3] = new SqlParameter("@SlotID", SlotID);
            oparam[4] = new SqlParameter("@Score", Score);
            oparam[5] = new SqlParameter("@Reason", ClsCommon.EnsureString(Reason));
            oparam[6] = new SqlParameter("@NegotiationSalary", NegotiationSalary);
            oparam[7] = new SqlParameter("@ExpectedJDate", ClsCommon.EnsureString(ExpectedJDate));
            oparam[8] = new SqlParameter("@Approved", Approved);
            oparam[9] = new SqlParameter("@createdby", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[10] = new SqlParameter("@IPAddress", ClsCommon.GetIPAddress());
            oparam[11] = new SqlParameter("@REC_Tags", ClsCommon.EnsureString(REC_Tags));
            DataSet ds = clsDataBaseHelper.ExecuteDataSet("spu_SetREC_EInterviewDirect", oparam);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Result.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["RET_ID"]);
                Result.StatusCode = (int)ds.Tables[0].Rows[0]["COMMANDSTATUS"];
                Result.SuccessMessage = ds.Tables[0].Rows[0]["COMMANDMESSAGE"].ToString();
                if (Result.StatusCode > 0)
                {
                    Result.Status = true;
                }
            }
            return Result;
        }
        #endregion


        public static DataSet FnGetAnualTaxReport(long empid, long Fyid)
        {
            SqlParameter[] oparam = new SqlParameter[2];
            oparam[0] = new SqlParameter("@empId", empid);
            oparam[1] = new SqlParameter("@financial_year_id", Fyid);
            return clsDataBaseHelper.ExecuteDataSet("spu_GenerateEmployeeCTCBreakup", oparam);


        }

        public static DataSet fnCreateMail_TermbaseactivitylogForresubmit(string date, string Command, string remarks, string empIdCsv)
        {
            SqlParameter[] oparam = new SqlParameter[5];
            oparam[0] = new SqlParameter("@date", date);
            oparam[1] = new SqlParameter("@Hodid", clsApplicationSetting.GetSessionValue("LoginID"));
            oparam[2] = new SqlParameter("@Command", Command);
            oparam[3] = new SqlParameter("@remarks", remarks);
            oparam[4] = new SqlParameter("@EmpidNew", empIdCsv);
            return clsDataBaseHelper.ExecuteDataSet("spu_CreateMail_NonMitrResubmitMailSupervisior", oparam);
        }


    }
}

