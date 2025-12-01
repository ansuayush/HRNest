using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface IActivityHelper
    {
        
        DailyLogCompleteModal DailyLogCompleteModal(DateTime Date);
        List<HolidayDailyLog> GetHolidayDailyLog(DateTime Date, string EMPID = "");
        List<TravelDailyLog> GetTravelDailyLog(DateTime Date);
        List<ProjectMappedList> GetSelectedProjectsList(string Doctype, string searchText);
        AddActivityProjectList AddProjectList(DateTime MyDate);

        MonthlyLog GetMonthlyLogData(string SelectedDate);
        List<DailyLog> GetDailyLogList(string Date, bool WantNewLine = false);
        List<CompensatoryOffList> GetCompensatoryOffList(long EMPID, int iMonth, int iYear, string approved);
        List<RequestCompensatoryOffList> GetCompensatoryOffList(int iMonth, int iYear);
        List<OvertimeList> GetOvertimeList(long EMPID, int iMonth, int iYear, string approved);
        List<ActiveLog> GetActiveLogList(long EMPID, DateTime Date);
        List<FNFEmployeeList> GetFNFEmployeeList();
        FNFEmployeeDetails GetFNFSettlementRpt(long EMPID);
        List<ConsolidateActivityStatusList> GetConsolidateActivityStatusList(DateTime StartDate, DateTime EndDate, string EmpType);
        TimeSheetEmployeeDetails GetTimeSheetEmployeeDetails(string Date, long EMPID,string Type);
        
        ProcessActivityTimeSheet GetProcessActivityTimeSheet(DateTime SelectedDate, long EMPID);

        ProcessTimeSheet FinalGetProcessTimeSheet(DateTime SelectedDate, string EMPID, string Emptype);
        ProcessTimeSheet FinalGetProcessTimeSheetOLD(DateTime SelectedDate, string EMPID, string Emptype);
        ProcessTimeSheet CalculationOnTimeSheet(ProcessTimeSheet PostModal);
        //ProcessTimeSheet FinalizeOnTimeSheet(ProcessTimeSheet PostModal);
        List<ConsolidateEMPList> GetConsolidateEMPList(int month, int year);
        ProcessTimeSheet FinalizeOnTimeSheet(ProcessTimeSheet PostModal);
        bool IsMontlyLogSubmited(long EMPID, DateTime Dt);
        List<MyTeamActiveLog> GetMyTeamActiveLogList(string Approved, int Month, int Year);
        bool GetDailyLogSetting(DateTime MyDate);
        double GetEmpWorkingHours();
        List<LeaveSummary> GetLeaveSummary(long EMPID, int Month, int Year);
        List<DailyLogNonMitrList> GetDailyLogNonMitrList(DataSet ds);
        List<ActivityProjectList> GetActivityProjectsList(DateTime MyDate);
        DailyLogNonMitr GetDailyLogNonMitr(string Date, string Empids);
        List<HolidayDailyLogNonMitr> GetHolidayLeaveDailyLog(string Empids, string LeaveMonth);
        MonthlyLogNonMitr GetMonthlyLogNonMitr(string Date, string Empids,string Type="");
        List<RequestCompensatoryOffList> GetCompensatoryOffNonMITRList(int iMonth, int iYear, string Empids);
        List<MyTeamActiveLogListNonMitr> GetMyTeamActiveLogListNonMitr(string Approved, int Month, int Year);
        List<CompensatoryOffList> GetCompensatoryOffListNonMitr(string EMPIDS, int iMonth, int iYear, string approved);
        ConsolidateReportEntry GetConsolidateSalaryAllocationEntry(string EmpType, string Date);
        PostResponse fnSetConsolidatedSalaryAllocationEntry(int Month, int Year, long Empid, string ComponentValuesFCRA, string ComponentValuesNFCRA);
        ConsolidateSalaryReport GetConsolidatedSalaryReport(string EmpType, string FromDate, string ToDate);
        ConsolidateSalaryReport GetALAccrual_TakeReport(string Empid, string EmpType, string FromDate, string ToDate);
        List<LeaveReport> GetEmployeeLeaveReportList(GetResponse modal);
    }
}