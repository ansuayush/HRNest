using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static Mitr.Models.AllEnum;

namespace Mitr.Models
{
    public class ConsolidateActivityLog
    {
        public string EMPID { get; set; }
        public MitrNonMitr? MitrUser { get; set; }
        public string Date { get; set; }
        public List<ConsolidateEMPList> EMPList { get; set; }
    }
    public class ConsolidateEMPList
    {
        public long EMPID { get; set; }
        public string EMPName { get; set; }
        public int user_id { get; set; }
    }

    public class TimeSheetEmployeeDetails
    {
        public string TimeSheetName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SupervisorName { get; set; }
        public decimal PaidHours { get; set; }
        public string DateofSubmission { get; set; }
        public decimal LWPHours { get; set; }
        public decimal HourlyRate { get; set; }
        public string DateofApproval { get; set; }
        public decimal MonthHours { get; set; }
        public string DOJ { get; set; }
        public string DOR { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string FixHolidays { get; set; }
        public DataSet EmployeeTimeSheet { get; set; }
    }

    public class ProcessActivityTimeSheet
    {
        public int iCount { get; set; }
        public int iUnLock { get; set; }
        public int iLock { get; set; }

        public DataSet TimeSheetDataSet { get; set; }
    }


    public class ProcessTimeSheet
    {

        public bool isContainError { get; set; }
        public DateTime SelectedDate { get; set; }
        public string TempEMPID { get; set; }
        public int iCount { get; set; }
        public int iUnLock { get; set; }
        public int iLock { get; set; }
        public List<ProcessEmployeeTimeSheet> EmployeeDetails { get; set; }
        public List<ProcessProjectTimeSheet> AllProjects { get; set; }
        public List<ProcessProjectTimeSheet> AllProjectsWithEmp { get; set; }
    }
    public class ProcessEmployeeTimeSheet
    {
        public bool IsDataComingFormTimeSheet { get; set; }
        public int Modify { get; set; }
        public string Checkbox { get; set; }
        public long EMPID { get; set; }
        public string EMPCode { get; set; }
        public string EMPName { get; set; }
        public decimal PerHourRate { get; set; }
        public decimal AdjHourRate { get; set; }

        public decimal hdnAdjHourRate { get; set; }
        [Range(0, 0, ErrorMessage = "Difference Hours Must be 0")]

        public decimal DiffHourRate { get; set; }
        public decimal OT { get; set; }
        public decimal AL { get; set; }
        public decimal PaidLeave { get; set; }
        public decimal LWP { get; set; }
        public decimal Holiday { get; set; }
        public decimal hdnLWP { get; set; }
        public decimal hdnAL { get; set; }
        public decimal hdnPaidLeave { get; set; }
        public decimal hdnHolidayHours { get; set; }
        public decimal PaidHours { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }

        public decimal MonthHours { get; set; }
        public string doj { get; set; }
        public string dor { get; set; }
        public string dSalary { get; set; }
        public string lLoactionID { get; set; }
        public string dTotLeaveHrs { get; set; }
        public string dTotLeaveHrsPaid { get; set; }
        public string dOTHrs { get; set; }
        public string dALHrs { get; set; }
        public string dPLhrs { get; set; }
        public string dNPLhrs { get; set; }
        public string dTHours { get; set; }
        public int iWDays { get; set; }
        public string WorkingDays { get; set; }

        public string TotalHours { get; set; }
        public List<ProcessProjectTimeSheet> ProjectDetails { get; set; }

    }
    public class SalaryDump
    {
        public string EMPID { get; set; }
        public string hdnAdjHourRate { get; set; }
        public string OT { get; set; }
        public string AL { get; set; }
        public string hdnAL { get; set; }
        public string hdnPaidLeave { get; set; }
        public string hdnHolidayHours { get; set; }
        public string PaidHours { get; set; }
        public string PerHourRate { get; set; }
        public string GrossSalary { get; set; }
        public string hdnLWP { get; set; }
        public string MonthHours { get; set; }



    }

   

    public class ProcessProjectTimeSheet
    {
        public long ProjectID { get; set; }
        public string ProjectName { get; set; }
        public decimal WH { get; set; }
        public decimal TH { get; set; }
        public decimal Value { get; set; }
        public decimal TravelValue { get; set; }
        public decimal WorkValue { get; set; }
        public decimal hdnPrAdjsutHours { get; set; }
        public long Empid { get; set; }
        public long TimeSheetid { get; set; }

    }

    public class ActivityReport
    {
        public string RadioButton { get; set; }
        public string Date { get; set; }
        public MitrNonMitr? MitrUser { get; set; }
    }
    public class ConsolidateReport
    {
        public string Date { get; set; }
        public string ToDate { get; set; }
        public long? Empid { get; set; }
        public MitrNonMitr? MitrUser { get; set; }
        public List<DropDownList> EmployeeList { get; set; }
    }
    public class LeaveReport
    {
        public long id { get; set; }
        public long Empid { get; set; }
        public long FinId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DOJ { get; set; }
        public string DOL { get; set; }
        public string Leave_type { get; set; }
        public long Opening { get; set; }
        public long Monthly_accrred { get; set; }
        public long availed { get; set; }
        public long PendingLeave { get; set; }
        public long balance { get; set; }
        public string BInGraph { get; set; }
        public string BInHours { get; set; }
        public string Date { get; set; }
        public long leaveType { get; set; }
        public List<LeaveReport> leaveReports { get; set; }
        public MitrNonMitr? MitrUser { get; set; }
        public List<DropDownList> FinYear { get; set; }

    }
    public class ProcessProjectTimeSheetDetails
    {
        public long TimeSheetlog_id { get; set; }
        public long proj_id { get; set; }
        public decimal work_hours { get; set; }
        public decimal adj_hours { get; set; }
        public decimal travel_value { get; set; }
        public decimal travel_hour { get; set; }
        public decimal work_value { get; set; }
        public long createdby { get; set; }


    }
    public class CustomizedReport
    {
        public string Empid { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public long proj_id { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string MitrUser { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string Earnings { get; set; }
        public long Deductions { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string fromDate { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string toDate { get; set; }
        public string pproj_id { get; set; }
        public List<DropDownList> EarningsList { get; set; }
        public List<DropDownList> EmpList { get; set; }
        public List<DropDownList> ProjectList { get; set; }


    }
    public class CustomizedReportStaffwise
    {
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string Empid { get; set; }
 
        public long proj_id { get; set; }
      
        public string MitrUser { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string Earnings { get; set; }
        public long Deductions { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string fromDate { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string toDate { get; set; }
        public string pproj_id { get; set; }
        public List<DropDownList> EarningsList { get; set; }
        public List<DropDownList> EmpList { get; set; }
        public List<DropDownList> ProjectList { get; set; }


    }


    public class CustomizedReportProject
    {
        public string Empid { get; set; }
   
        public long proj_id { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string MitrUser { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string Earnings { get; set; }
        public long Deductions { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string fromDate { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string toDate { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string pproj_id { get; set; }
        public List<DropDownList> EarningsList { get; set; }
        public List<DropDownList> EmpList { get; set; }
        public List<DropDownList> ProjectList { get; set; }


    }
    public class ConsolidatedStaffSalaryReport
    {
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string Empid { get; set; }
        public string MitrUser { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string Earnings { get; set; }
        public long Deductions { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string fromDate { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string toDate { get; set; }
        public List<DropDownList> EarningsList { get; set; }
        public List<DropDownList> EmpList { get; set; }
        public List<DropDownList> ProjectList { get; set; }


    }

    public class ComponentwiseReport
    {
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string Empid { get; set; }
     
        public string MitrUser { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string Earnings { get; set; }
        
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string fromDate { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string toDate { get; set; }
        public List<DropDownList> EarningsList { get; set; }
        public List<DropDownList> EmpList { get; set; }
  


    }
    public class DropDownListEMP
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string ExtraValue { get; set; }
    }



}