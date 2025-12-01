using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
   interface IMPRHelper
    {
        List<MPR.Section.SecList> GetMPRSecList(long MPRSID, string IsActive = "0,1");
        MPR.Section.SecAdd GetMPRSec(long MPRSID);

        List<MPR.SubSection.SubSecList> GetMPRSubSecList(long MPRSubSID, string IsActive = "0,1");
        MPR.SubSection.SubSecAdd GetMPRSubSec(long MPRSubSID);

        List<MPR.Indicator.IndicatorList> GetMPRIndicatorList(long IndicatorID, string IsActive = "0,1");
        MPR.Indicator.IndicatorAdd GetMPRIndicator(long IndicatorID);
        MPR.CreateMPR GetCreateMPR(long MPRID);
        List<MPR.MPRList> GetMPRList();
        List<MPR.Targets.TargetsList> GetMPRTargetsList(long FinID, string TargetType);
        List<MPR.FinYear> GetFinYearList();
        MPR.Targets.addTargetSetting AddTargetSetting(long finId, long ProjectID);
        List<SMPR.List> GetSMPRList(string Approve, int Month,int Year,string ListType="");
        List<SMPR.SMPRDet> GetSMPRDetList(long SMPRID);
        SMPR.SMPREntry GetSMPREntry(long SMPRID);
        SMPR.SMPRApproval GetSMPRApproval(long SMPRID);
        List<SMPR.SMPRComments> CommentsList(long SMPRID, long SectionID);
        SMPR.LevelApprover GetLevelApprover(long SMPRID);
        SMPR.LevelApprover2 GetLevel2Approver(long SMPRID);
        MPRDashboard MPRDashboard();
        List<MPRDashboard.List> MPRDashboardList(long StateID, long ProjectID, int Month, int Year);
        List<LockUnlock.List> LockUnlockList(DateTime dtDate);
        List<LockUnlock.HistoryList> SMPRLockHistoryList(long SMPRID);
        MPRSetting MPRSettingList(long MPRSettingID);
        MPRReports.Header MPRReportHeader();
        MPRReports.Header MPRNewReportHeader();
        List<MPRReports.SubSection> GetMPR_Reports_SubHeader(string MPRSID);
        List<MPRReports.List> GetMPRReports(GetMPRResponse modal);
        List<MPRReports.AchievementList> GetStateProjectAchievementReports(GetMPRResponse modal);
        List<MPRReports.AchievementList> GetNewStateProjectAchievementReports(GetMPRResponse modal, int from);
        List<MPRReports.MPRReportExcel> GetMPRReportExcel(GetMPRResponse modal);
        List<MPRDashboard.List> MPRDashboardListNew(long StateID, long ProjectID, int Month, int Year, string DocType);

        //List<MPRDashboard.ListDashboard> MPRDashboardListCount(long StateID, long ProjectID, int Month, int Year, string DocType);
        MPRDashboard.ListDashboard MPRDashboardListCountNew(long StateID, long ProjectID, int Month, int Year, string DocType);
    }
}