using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class DailyLog
    {
      
        public int DailyLogID { get; set; }
        public int LeaveWithAttachmentPending { get; set; }
        public string LeaveWithAttachmentPendingDates { get; set; }
        public int emp_id { get; set; }
        public int month { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public int proj_id { get; set; }

        [Required(ErrorMessage = "Hey! You missed this field")]
        public long ActivityID { get; set; }
        public string proj_name { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public string description { get; set; }        
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
        public Dictionary<int, double> DaysKeyValue { get; set; }
        
        public long TravelrequestID { get; set; }


    }
    public class DailyLogMonth
    {
        public string Month { get; set; }
       

    }
    public class DailyLogCompleteModal
    {
      
        public string ResubmitRemarks { get; set; }
        public bool IsShowRequestForCompOFF { get; set; }
        public bool IsSubmitted { get; set; }
        public string RecallRemarks { get; set; }
        public bool ShowRecall { get; set; }
        public List<ProjectMappedList> ProjectMappedList { get; set; }
        public List<DailyLog> DailyLogList { get; set; }
        public List<DropDownList> ActivityMasterList { get; set; }
        public string TaskStandardDesc { get; set; }
        public DateTime SelectedDate { get; set; }
        public DateTime Doj { get; set; }
        public DateTime LastWorkingDay { get; set; }
        public int LeaveWithAttachmentPending { get; set; }
        public string LeaveWithAttachmentPendingDates { get; set; }
        public int IsHaveComOff { get; set; }
        public int IsLoad { get; set; }
        public decimal WorkingHours { get; set; }

    }
    public class AddActivityProjectList
    {
        public List<ProjectMappedList> ProjectMappedList { get; set; }
        public List<ActivityProjectList> ActivityProjectList { get; set; }
    }
    public class ProjectMappedList
    {
        public long ID { get; set; }
        public long ProjectRegID { get; set; }
        public string ProjectName { get; set; }
        public long EMPID { get; set; }
        public string DocType { get; set; }
        public string Description { get; set; }
        public string Doctype { get; set; }
    }
    public class ActivityProjectList
    {
        public long ID { get; set; }
        public string ProjectName { get; set; }

    }
    public class HolidayDailyLog
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string ClassName { get; set; }
        public string Color { get; set; }
        public string HolidayType { get; set; }
        public string Description { get; set; }
        public string readonlycheck { get; set; }
    }
    public class TravelDailyLog
    {
        public long TravelReqID { get; set; }
        public string Date { get; set; }
        public string ClassName { get; set; }
        public string Color { get; set; }
       
    }
    public class HolidayDailyLogNonMitr
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string DayNo { get; set; }
        public string ClassName { get; set; }
        public string Color { get; set; }
        public string HolidayType { get; set; }
        public string Description { get; set; }
        public long Empid { get; set; }
    }

    public class Calendardata
    {
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string classNames { get; set; }
        public string description { get; set; }
    }
    public class InputDate
    {
        public string Date { get; set; }
        public string Approve { get; set; }

    }

    public class LeaveSummary
    {
        public long LeaveLogID { get; set; }
        public long LeaveID { get; set; }
        public string LeaveName { get; set; }
        public string Doc_No { get; set; }
        public string Doc_Date { get; set; }
        public string Date { get; set; }
        public string Hours { get; set; }
        public string Status { get; set; }
        public string EMPName { get; set; }
        public string EMPCode { get; set; }
        public int Approved { get; set; }
        public int RFC { get; set; }
    }
    public class DailyLogNonMitr
    {
        public int LeaveWithAttachmentPending { get; set; }
        public string LeaveWithAttachmentPendingDates { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public List<DailyLogNonMitrList> LstDailyLog { get; set; }
        public List<HolidayDailyLogNonMitr> LstHoliday { get; set; }
        //public List<HolidayDailyLog> LstLeaves { get; set; }
    }
    public class DailyLogNonMitrList
    {
        public int isSelect { get; set; }
        public int DailyLogID { get; set; }

        public int LeaveWithAttachmentPending { get; set; }
        public string LeaveWithAttachmentPendingDates { get; set; }
        public int emp_id { get; set; }
        public DateTime Doj { get; set; }
        public DateTime LastWorkingDay { get; set; }
        public int WorkingHours { get; set; }
        public int month { get; set; }        
        public int proj_id { get; set; }        
        public long ActivityID { get; set; }
        public string EmpName { get; set; }
        public string proj_name { get; set; }        
        public string description { get; set; }
        public string FixHoliday { get; set; }
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
        public Dictionary<int, double> DaysKeyValue { get; set; }


    }

}