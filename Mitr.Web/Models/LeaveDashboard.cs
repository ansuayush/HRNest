using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class SeniorLeaveDashboard
    {
        public List<LeaveBalanceDetails> LeaveBalanceDetails { get; set; }
        public List<ApplyLeaveList> ApplyLeaveList { get; set; }

        public List<ApplyLeaveList> RequestLeaveList { get; set; }
    }
    public class LeaveDashboard
    {
        public List<LeaveBalanceDetails> LeaveBalanceDetails { get; set; }
        public List<ApplyLeaveList> ApplyLeaveList { get; set; }
    }
    public class LeaveDetails
    {
        
        public List<LeaveLogDetail_Tran> LeaveDetails_Tran { get; set; }
       public  List<Attachments> LeaveAttachment { get; set; }
    }
    public class ApplyLeaveList
    {
        public long EMPID { get; set; }
        public string EMPName { get; set; }
        public string EMPCode { get; set; }
        public long LeaveLogID { get; set; }
        public string DocNo { get; set; }
        public string RequestDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string EmergenContactName { get; set; }
        public string EmergenContactno { get; set; }
        public string Remarks { get; set; }
        public int LeaveHours { get; set; }
        public string hoursDayWise { get; set; }
        public int Approved { get; set; }
        public string Reason { get; set; }
        public int LeaveID { get; set; }
        public string LeaveName { get; set; }
        public string Status { get; set; }
        public int RFC { get; set; }
        public int RFCStatus { get; set; }
        public string RFCRemarks { get; set; }
        public string IsDailyLogApproved { get; set; }
        public int AttachmentRequired { get; set; }

        public string HODRemarks { get; set; }
        public string EDRemarks { get; set; }
        public bool isED { get; set; }
        public long HOD_ID { get; set; }

    }

    public class LeaveBalanceDetails
    {
        public int LeaveBalanceDetailsID { get; set; }
        public string LeaveType { get; set; }
        public int Opening { get; set; }
        public int MonthlyAccured { get; set; }
        public int Availed { get; set; }
        public int Balance { get; set; }
        public int PendingLeave { get; set; }
        public string BInGraph { get; set; }
        
        public string BInHours { get; set; }
    }

    public class LeaveLogDetail_Tran
    {
        public int ID { set; get; }
        public int LeaveLogID { set; get; }

        public string DocNo { get; set; }
        public string DocDate { get; set; }
        public string ExpectedDeliveryDate { get; set; }

        public int LeaveID { get; set; }
        public string LeaveType { get; set; }
        public string Date { get; set; }
        public decimal Hours { get; set; }
        public string hoursDayWise { get; set; }
        public string TotalhoursDayWise { get; set; }
       
        public string emergenContact_no { get; set; }
        public string EmergenContactName { get; set; }
        public string EmergenContactRelation { get; set; }
        public long EMPID { get; set; }
        public string EMPCode { get; set; }
        public string EMPName { get; set; }
        public long AttachmentID { get; set; }
        public string EMPStatus { get; set; }
        public string ContentType { get; set; }
        public int Approved { get; set; }
        public int RFC { get; set; }
        public int RFCStatus { get; set; }
        public string RFCRemarks { get; set; }
        public string Reason { get; set; }
        public string HODRemarks { get; set; }
        public string EDRemarks { get; set; }
    }


    public class LeaveType
    {
        public int ID { get; set; }
        public string LeaveName { get; set; }
    }
    public class Leave
    {
       
        public int? LeaveTypeID { get; set; }
        public List<LeaveType> LeaveTypeList { get; set; }
       
        public string StartDate { get; set; }
     
        public string EndDate { get; set; }

        public string Remarks { get; set; }
        public string ExpectedDeliveryDate { get; set; }

        [Required(ErrorMessage = "Hey! You missed this field")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Hey, you have entered an invalid contact number.")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Allowed only Numbers")]
        public string EmergencyContactNo { get; set; }


        [Required(ErrorMessage = "Hey! You missed this field")]
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelation { get; set; }
        public LeaveEmp LeaveEmp { get; set; }
        public List<LeaveTran> LeaveTranList { get; set; }
        public List<LeaveAttachment> LeaveAttachmentList { get; set; }
    }

    public class LeaveAttachment
    {
        public string AttachmentType { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }

    }
    public class LeaveTran
    {
        [Required(ErrorMessage = "Hey! You missed this field")]
        public DateTime LeaveDate { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public string LeaveType { get; set; }
        public decimal LeaveHours { get; set; }
    }
    public class LeaveEmp
    {
        public string EMPName { get; set; }
        public string EMPCode { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNo { get; set; }
        public string EmergencyContactRelation { get; set; }
        public int HODID { get; set; }
        public string HODName { get; set; }

        public string EMail { get; set; }
        public string HODEMail { get; set; }
        public long LeaveLogID { get; set; }
        public long HolidayID { get; set; }
        public string Status { get; set; }

    }

    public class ActionOnLeave
    {
        public string IsValidForEDApproval { get; set; }
        public long EMPID { get; set; }
        public long LeaveLogID { get; set; }

        public string Reason { get; set; }
        public string HODRemarks { get; set; }
        public string EDRemarks { get; set; }
        
        public int Approved { get; set; }
        public int RFC { get; set; }
        public int RFCStatus { get; set; }
        public string RFCRemarks { get; set; }
        public bool isED { get; set; }
    }

    public class LeaveAvailedReport
    {
        public LeaveEmp EmployeeList { get; set; }
        public DataSet LeaveDetails { get; set; }
    }

    public class LeaveMaster
    {
        public long LeaveID { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public string LeaveName { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public int LeaveType { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public int DueType { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public int NOOfLeave { set; get; }
        public string ColorApplied { set; get; }

        public string ColorcodeApplied { set; get; }

        public string ColorAppproved { set; get; }
        public string ColorcodeApproved { set; get; }
        public string CFLeave { set; get; }
        public string CFLimit { set; get; }
        public int Leave_forword { get; set; }

        public string chkFDLeave { get; set; }
        public string chkApprovalRequired { get; set; }
        public string chkShowleavebalance { get; set; }

        public string ApplicableFor { get; set; }
        public int COLeave { get; set; }
        public int ShownLeave { get; set; }
        public int status { get; set; }
        public bool IsActive { set; get; }
        public bool ShowInSummary { get; set; }
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

    public class LeaveAttachmentCount
    {
        public long MyLeaveCount { get; set; }
        public long TeamLeaveCount { get; set; }
    }
    public class LeaveNonMitrMonth
    {
        public long  Empid { get; set; }
        public string Month { get; set; }
        public string Empids { get; set; }
        public string LeaveTypeids { get; set; }
        public List<DropDownList> EmpList { get; set; }
        public List<DropDownList> LeaveList { get; set; }

    }
    public class LeaveNonMITRAdd
    {
        public int Id { get; set; }
        public int isSelect { get; set; }
        public int? LeaveTypeID { get; set; }
        public int? Empid { get; set; }
        public int IsAttachment { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime LastWorkingDay { get; set; }
        public string EmpName { get; set; }
        public string Opening { get; set; }
        public string Accrued { get; set; }
        public string Availed { get; set; }
        public string Balance { get; set; }
        public string LeaveName { get; set; }
        public string FixHoliday { get; set; }       
        public DateTime LeaveDate { get; set; }
        public decimal LeaveHours { get; set; }             
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
        public string Prevbalance { get; set; }
        public List<LeaveAttachment> LeaveAttachmentList { get; set; }
        public Dictionary<int, double> DaysKeyValue { get; set; }     
    }
}