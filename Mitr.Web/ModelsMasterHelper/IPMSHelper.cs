using Mitr.Models;
using System.Collections.Generic;

namespace Mitr.ModelsMasterHelper
{
    interface IPMSHelper
    {
        List<PMS.CalenderYear.List> GetCalenderYearList();
        PMS.CalenderYear.Add GetCalenderYear(long ID);
        List<PMS.FinList> GetFinYearList();
        List<PMS.UOM.List> GetPMSUOMList(long PMSUOMID, long FYID, string IsActive = "0,1");
        PMS.UOM.Add GetPMSUOM(long PMSUOMID);
        List<PMS.KPA.List> GetPMS_KPAList(long KPAID, long FYID, string OperationType, string IsActive = "0,1");
        List<PMS.KPA.KPASummary> GetPMS_KPASummaryList();
        PMS.KPA.Add GetPMS_KPA(long PMSUOMID, long FYID);
        List<PMS.Hierarchy> GetPMS_HierarchyList(long FYID);
        PMS.Hierarchy.Update GetPMS_HierarchyUpdate(long PMS_EID);
        List<PMS.GoalSheetAct> GetPMS_GoalSheetActList(long FYID);
        List<PMS.OSQuestion.List> GetPMS_QuestionList(long FYID);
        PMS.OSQuestion.Add GetPMS_Question(long PMS_EID);
        List<PMS.DesignationList> GetDesignationList(long ID, string IsActive);
        List<PMS.AppraisalAct> GetPMS_AppraisalActList(long FYID);
        PMS.GoalSheet.MySheet GetMyGoalSheet(long FYID,  int Approved);
        PMS.GoalSheet.Add GetPMS_GoalSheet_Det(long PMS_GSDID);
        List<PMS.GoalSheet.TeamGoalSheet> GetTeamGoalSheet(long FYID, int Approved);
        PMS.GoalSheet.EMPGoalSheet GetEMPGoalSheet(long PMS_GSID ,string OperationType);
        List<PMS.GroupGoalSheet.List> GetGroupGoalSheet(long FYID, int Approved);
        List<PMS.Feedback.List> GetFeedbackList(long FYID);
        PMS.Feedback.Add GetFeedback(long FYID, long EMPID);
        PMS.SelfAppraisal GetSelfAppraisal(long FYID);
        List<PMS.TeamAppraisal.List> GetTeamAppraisalList(long FYID, string Doctype);
        PMS.TeamAppraisal.Add GetTeamAppraisal(long PMS_AID);
        List<PMS.GroupAppraisal.List> GetGroupAppraisalList(long FYID,string Doctype);
        List<PMS.CMCAppraisal.List> GetCMCAppraisalList(long FYID);
        PMS.GroupAppraisal.Add GetGroupAppraisal(long PMS_AID);
        PMS.CMCAppraisal.Add GetCMCAppraisal(long PMS_AID);
        PMS.Appraisal.Add GetAppraisal_View(long PMS_AID);
        List<PMS.TrainingTypeMaster.List> GetTrainingTypeList(long ID);
        PMS.TrainingTypeMaster.Add GetTrainingType(GetResponse modal);
        PostResponse SetTrainingType(PMS.TrainingTypeMaster.Add modal);
        string GetAppraisal_RPT(long EMPID, long Fyid);
        List<PMS.PMSStatus> GetPMSStatusList(long EMPID, long Fyid);
        List<PMS.PMSStatus> GetAppraisalReportList(long EMPID, long Fyid);
        List<PMS.PMSStatus> GetAdditionalReviewerReportList(long EMPID, long Fyid);
        List<PMS.TrainingSkills> GetEmployeeTrainingReportList(GetResponse modal);
        List<PMS.Hierarchy> GetPMS_HierarchyListPB(long FYID);


    }
}