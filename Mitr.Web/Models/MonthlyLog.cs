
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Mitr.Models.AllEnum;
namespace Mitr.Models
{
    public class MonthlyLog
    {
        public bool IsSubmitted { get; set; }
        public List<DailyLeaveLog> DailyLeaveLogList { get; set; }
        public List<DailyLog> DailyLogList { get; set; }
        public double LeaveAvailed { get; set; }
        public double LeavePendingForApproval { get; set; }
        public double CompensatoryHours { get; set; }
        public double CompensatoryHoursApproval { get; set; }
        public string CCMail { get; set; }
    }
    public class DailyLeaveLog
    {
        public int DailyLeaveLogID { get; set; }
        public string LeaveName { get; set; }
        public Dictionary<int, string> DaysKeyValue { get; set; }
        public int Total { get; set; }
    }
    public class CompensatoryOffApproval
    {
        public long EMPID { get; set; }
        public string Date { get; set; }
        public string Approve { get; set; }

        public List<MiscEmployee> CompensatoryEmployee { get; set; }
    }
    public class CompensatoryOffList
    {
        public long CompensatoryOffID { get; set; }
        public long EMPID { get; set; }
        public string Emp_Code { get; set; }
        public string Emp_name { get; set; }
        public string proj_name { get; set; }
        public string description { get; set; }
        public string Date { get; set; }
        public decimal hours { get; set; }
        public decimal HRS { get; set; }
        public decimal Approve_hours { get; set; }
        public long Approved { get; set; }
        public string CheckSelected { get; set; }
        public string Reason { get; set; }
    }

    public class RequestCompensatoryOffList
    {
        public long CompensatoryOffID { get; set; }
        public string EmpName { get; set; }
        public long EMPID { get; set; }
        public string proj_name { get; set; }
        public string description { get; set; }
        public string Date { get; set; }
        public decimal hours { get; set; }
        //[Range(0, 8, ErrorMessage = "Applied hours can't greater than 8 hours.")]
        // public decimal Applied_hours { get; set; } code comment by shailendra Applied_hours only number
        public long Applied_hours { get; set; }
        public long Approved { get; set; }
        public string CheckSelected { get; set; }
    }
    public class OvertimeList
    {
        public long OvertimeID { get; set; }
        public long EMPID { get; set; }
        public string Emp_Code { get; set; }
        public string Emp_name { get; set; }
        public string proj_name { get; set; }
        public string description { get; set; }
        public string Date { get; set; }
        public decimal hours { get; set; }
        public decimal Approve_hours { get; set; }
        public long Approved { get; set; }
        public string CheckSelected { get; set; }
    }


    public class ActiveLog
    {
        public string ActivityName { get; set; }
        public int ActiveLogID { get; set; }
        public int emp_id { get; set; }
        public int month { get; set; }
        public int proj_id { get; set; }
        public long ActivityID { get; set; }
        public string projref_no { get; set; }
        public string proj_name { get; set; }
        public string description { get; set; }
        public int srno { get; set; }
        public double Total { get; set; }
        public double TotalHours { get; set; }
        public double LeaveAvailed { get; set; }
        public double CompenOff { get; set; }
        public int year { get; set; }
        public int srno1 { get; set; }
        public Dictionary<int, double> DaysKeyValue { get; set; }
    }

    public class MyTeamActiveLog
    {
        public string Status { get; set; }
        public long ActiveID { get; set; }
        public long EMPID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string EMPCode { get; set; }
        public string EMPName { get; set; }
        public string ApprovedRemarks { get; set; }
        public string MonthName { get; set; }
        public long HouredWorks { get; set; }
        public long TotalActivityHours { get; set; }
        public long UnApprovedComOff { get; set; }
        public long UnApprovedOT { get; set; }
        public long PendingLeaveHours { get; set; }
        public string CO_OT { get; set; }
        public string CheckSelected { get; set; }
        public int IsLock { get; set; }
    }
    public class MonthlyLogNonMitr
    {
        public int month { get; set; }
        public int year { get; set; }
        public List<MonthlyLogNonMitrList> LstMonthlyLog { get; set; }
        public List<HolidayDailyLogNonMitr> LstHoliday { get; set; }
        public List<MonthlyLogHoursSummary> LstHoursSummary { get; set; }
    }
    public class MonthlyLogHoursSummary
    {
        public int Empid { get; set; }
        public int WorkingHours { get; set; }
        public int MWHrs { get; set; }
        public int TLAHrs { get; set; }
        public int TLPHrs { get; set; }
        public int TCAHrs { get; set; }
        public int TCHrs { get; set; }
        public string EMailCC { get; set; }
        public string EmpType { get; set; }
        public int IsCompOff { get; set; }
    }
    public class MonthlyLogNonMitrList
    {
        public int isSelect { get; set; }
        public int DailyLogID { get; set; }
        public int emp_id { get; set; }
        public DateTime Doj { get; set; }
        public DateTime LastWorkingDay { get; set; }
        public int month { get; set; }
        public int proj_id { get; set; }
        public long ActivityID { get; set; }
        public string Doctype { get; set; }
        public string EmpName { get; set; }
        public string proj_name { get; set; }
        public string description { get; set; }
        public string Activity { get; set; }
        public string FixHoliday { get; set; }
        public string ResubmitRemarks { get; set; }
        public int srno { get; set; }
        public double Day1 { get; set; }
        public double Day2 { get; set; }
        public double Day3 { get; set; }
        public double Day4 { get; set; }
        public double Day5 { get; set; }
        public double Day6 { get; set; }
        public double Day7 { get; set; }
        public double Day8 { get; set; }
        public double Day9 { get; set; }
        public double Day10 { get; set; }
        public double Day11 { get; set; }
        public double Day12 { get; set; }
        public double Day13 { get; set; }
        public double Day14 { get; set; }
        public double Day15 { get; set; }
        public double Day16 { get; set; }
        public double Day17 { get; set; }
        public double Day18 { get; set; }
        public double Day19 { get; set; }
        public double Day20 { get; set; }
        public double Day21 { get; set; }
        public double Day22 { get; set; }
        public double Day23 { get; set; }
        public double Day24 { get; set; }
        public double Day25 { get; set; }
        public double Day26 { get; set; }
        public double Day27 { get; set; }
        public double Day28 { get; set; }
        public double Day29 { get; set; }
        public double Day30 { get; set; }
        public double Day31 { get; set; }
        public int Total { get; set; }
        public int year { get; set; }
        public int srno1 { get; set; }


    }

    public class MyTeamActiveLogListNonMitr
    {
        public string Status { get; set; }
        public long EMPID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int TeamCount { get; set; }
        public string Teamids { get; set; }
        public string EMPCode { get; set; }
        public string EMPName { get; set; }
        public string MonthName { get; set; }
        public int UnApprovedOT { get; set; }
        public int UnApprovedComOff { get; set; }

    }
    public class ActivityReports
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public MitrNonMitr? MitrUser { get; set; }
        public string Doctype { get; set; }
        public long Id { get; set; }
    }

    public class ConsolidateReportEntry
    {
        public string Emptype { get; set; }
        public string Month { get; set; }
        public long StatusType { get; set; }
        public List<ConsolidateEntryEmp> Emplist { get; set; }
        public List<ConsolidateEntryComponent> Component { get; set; }
    }
    public class ConsolidateEntryEmp
    {
        public int RowNum { get; set; }
        public long id { get; set; }
        public string Checkbox { get; set; }
        public long Empid { get; set; }
        public string emp_name { get; set; }
        public string emp_code { get; set; }
        public string EmploymentTerm { get; set; }
        public string Location { get; set; }
        public string HourlyRate { get; set; }
        public string Salary { get; set; }
        public string ProjectType { get; set; }
        public string ALAmount { get; set; }
        public string PFAmountC3 { get; set; }
        public string FixedAmount { get; set; }
        public string ALPaid { get; set; }
        public string OTAmount { get; set; }
        public string Salary_FCRA { get; set; }
        public string AL_FCRA { get; set; }
        public string PF_FCRA { get; set; }
        public string PF_FCRA_Entry { get; set; }
        public string FB_FCRA { get; set; }
        public string OT_FCRA { get; set; }
        public string ALPaid_FCRA { get; set; }
        public string Salary_NONFCRA { get; set; }
        public string AL_NONFCRA { get; set; }
        public string PF_NONFCRA { get; set; }
        public string PF_NONFCRA_Entry { get; set; }
        public string FB_NONFCRA { get; set; }
        public string OT_NONFCRA { get; set; }
        public string ALPaid_NONFCRA { get; set; }
        public string FCRA { get; set; }
        public string NFCRA { get; set; }



    }
    public class ConsolidateEntryComponent
    {
        public long Empid { get; set; }
        public long Componentid { get; set; }
        public string Component { get; set; }
        public float Amt { get; set; }
        public float AmtFCRA { get; set; }
        public float AmtNFCRA { get; set; }
        public string Doctype { get; set; }

    }

    public class ConsolidateSalaryReport
    {
        public string Emptype { get; set; }
        public string Month { get; set; }
        public List<ConsolidateSalaryEmployee> Emplist { get; set; }
        public List<ConsolidateSalaryComponent> Component { get; set; }
    }
    public class ConsolidateSalaryEmployee : ALAccrual_Take
    {
        public int RowNum { get; set; }
        public long id { get; set; }
        public string Checkbox { get; set; }
        public long Empid { get; set; }
        public string emp_name { get; set; }
        public string emp_code { get; set; }
        public string EmploymentTerm { get; set; }
        public string Location { get; set; }
        public string HourlyRate { get; set; }
        public string Salary { get; set; }
        public string ProjectType { get; set; }
        public string ALAmount { get; set; }
        public string PFAmountC3 { get; set; }
        public string FixedAmount { get; set; }
        public string ALPaid { get; set; }
        public string OTAmount { get; set; }
        public string Salary_FCRA { get; set; }
        public string AL_FCRA { get; set; }
        public string PF_FCRA { get; set; }
        public string PF_FCRA_Entry { get; set; }
        public string FB_FCRA { get; set; }
        public string OT_FCRA { get; set; }
        public string ALPaid_FCRA { get; set; }
        public string Salary_NONFCRA { get; set; }
        public string AL_NONFCRA { get; set; }
        public string PF_NONFCRA { get; set; }
        public string PF_NONFCRA_Entry { get; set; }
        public string FB_NONFCRA { get; set; }
        public string OT_NONFCRA { get; set; }
        public string ALPaid_NONFCRA { get; set; }



    }
    public class ConsolidateSalaryComponent
    {
        public long Empid { get; set; }
        public long Componentid { get; set; }
        public string Component { get; set; }
        public string ProjectType { get; set; }
        public float Amt { get; set; }
        public float AmtFCRA { get; set; }
        public float AmtNFCRA { get; set; }
        public string Doctype { get; set; }

    }


    public class ALAccrual_Take
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }

        public string Subcategory { get; set; }
        public string AnnualLeaveAccuralHrs { get; set; }
        public string AnnualLeaveTakenHrs { get; set; }


    }
}