using Dapper;
using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Mitr.ModelsMaster
{
    public class ActivityModal : IActivityHelper
    {
        IMasterHelper Master;
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public ActivityModal()
        {
            Master = new MasterModal();
        }
        public DailyLogCompleteModal DailyLogCompleteModal(DateTime Date)
        {
            long EmpID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EmpID);
            DailyLogCompleteModal obj = new DailyLogCompleteModal();
            obj.ShowRecall = true;
            string SQL = "select id from active_log where emp_id = " + EmpID + " and isdeleted = 0 and month = " + Date.Month + " and year = " + Date.Year + "";
            if (clsDataBaseHelper.CheckRecord(SQL) > 0)
            {
                obj.IsSubmitted = true;

                if (clsDataBaseHelper.CheckRecord("select ID from active_approve where emp_id=" + EmpID + " and month=" + Date.Month + " and year=" + Date.Year + " and isdeleted=0") > 0)
                {
                    obj.ShowRecall = false;
                }
            }
            SQL = @"select isnull(sum(cast(hrs as int)),0) as hrs from compensatory_off where isdeleted=0 and mailsent=0 and compensatory_off.isdeleted=0 
                and compensatory_off.mailsent=0 and month(compensatory_off.date)=" + Date.Month + " and year(compensatory_off.date)=" + Date.Year + "" +
             " and compensatory_off.emp_id=" + clsApplicationSetting.GetSessionValue("EMPID") + "  and hrs>0";
            if (Convert.ToDecimal(clsDataBaseHelper.ExecuteSingleResult(SQL)) > 0)
            {
                obj.IsShowRequestForCompOFF = true;
            }
            SQL = @"select isnull(remarks,'') from active_approve where emp_id=" + EmpID + " and isdeleted=1 and month=" + Date.Month + " and year=" + Date.Year + " order by id desc";
            obj.ResubmitRemarks = clsDataBaseHelper.ExecuteSingleResult(SQL);
            obj.ProjectMappedList = GetSelectedProjectsList("Map");
            obj.DailyLogList = GetDailyLogList(Date.ToString(), true);

            GetResponse getDropDown = new GetResponse();
            getDropDown.Doctype = "projectactivity";
            obj.ActivityMasterList = ClsCommon.GetDropDownList(getDropDown);


            if (obj.DailyLogList.Count > 0)
            {
                obj.LeaveWithAttachmentPending = obj.DailyLogList.Select(x => x.LeaveWithAttachmentPending).FirstOrDefault();
                obj.LeaveWithAttachmentPendingDates = obj.DailyLogList.Select(x => x.LeaveWithAttachmentPendingDates).FirstOrDefault();
            }
            SQL = @"select count(id) from compensatory_off
	                where compensatory_off.isdeleted=0 and compensatory_off.mailsent=0 and compensatory_off.hrs>0 and compensatory_off.hours=0 and compensatory_off.approved=0
                    and month(compensatory_off.date)=" + Date.Month + " and year(compensatory_off.date)=" + Date.Year + " and compensatory_off.emp_id=" + EmpID;
            obj.IsHaveComOff = Convert.ToInt32(clsDataBaseHelper.ExecuteSingleResult(SQL));

            SQL = @"select isnull((select top 1 WorkingHours from SS_EmployeeSalary where Empid=" + EmpID + " and isdeleted=0 and EffectiveDate<=CURRENT_TIMESTAMP order by id desc),0)";

            obj.WorkingHours = Convert.ToInt32(clsDataBaseHelper.ExecuteSingleResult(SQL));
            SQL = @"select  format(master_emp.doj,'yyyy-MM-dd') as doj,format(case when master_emp.lastworking_day<='1900-01-01' then '2100-12-31' else master_emp.lastworking_day end,'yyyy-MM-dd') as lastworking_day from master_emp where id=" + EmpID + "";
            DataSet dsDoj = clsDataBaseHelper.ExecuteDataSet(SQL);
            if (dsDoj.Tables[0].Rows.Count > 0)
            {
                obj.Doj = Convert.ToDateTime(dsDoj.Tables[0].Rows[0]["doj"].ToString());
                obj.LastWorkingDay = Convert.ToDateTime(dsDoj.Tables[0].Rows[0]["lastworking_day"].ToString());
            }
            return obj;
        }
        public List<ProjectMappedList> GetSelectedProjectsList(string Doctype, string searchText = "")
        {
            List<ProjectMappedList> List = new List<ProjectMappedList>();
            ProjectMappedList obj = new ProjectMappedList();

            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetProjectEMP_SelfMapping(Doctype, searchText);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ProjectMappedList();
                    obj.ID = Convert.ToInt32(item["ID"].ToString());
                    obj.ProjectRegID = Convert.ToInt32(item["ProRegID"].ToString());
                    obj.ProjectName = item["ProjectName"].ToString();
                    obj.Description = item["description"].ToString();
                    obj.Doctype = item["Doctype"].ToString();
                    obj.EMPID = Convert.ToInt32(item["EMPID"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetSelectedProjectsList. The query was executed :", ex.ToString(), "fnGetDailyActiveLog", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public List<ActivityProjectList> GetActivityProjectsList(DateTime MyDate)
        {
            string SQL = "";
            List<ActivityProjectList> List = new List<ActivityProjectList>();
            ActivityProjectList obj = new ActivityProjectList();

            try
            {
                // Commented by ASHWATI 18-04-2022
                //SQL = @"select project_registration.ID,project_registration.doc_no + '-' + project_registration.proj_name as ProjectName 
                //from project_registration where  isdeleted =0 and inactive=1 and isactive=1 and 
                //id not in(SELECT proj_id FROM project_regdet WHERE isdeleted=0 and activity='Salaries and Benefits'
                //  and sub_sub_activity_descrip=(select emp_code + ' ('+ emp_name +')' from master_emp where id=" + clsApplicationSetting.GetSessionValue("EMPID") + ")) " +
                //  " and id not in (select ProRegID from ProjectEMP_SelfMapping where isdeleted=0) " +
                //  " order by project_registration.projref_no";
                //SQL = @"select project_registration.ID,project_registration.doc_no + '-' + project_registration.proj_name as ProjectName
                //from project_registration where isdeleted = 0 and inactive = 1 and isactive = 1   

                //order by left(project_registration.doc_no,2),substring(project_registration.doc_no,3,3)";
                //and end_date>'" + MyDate.ToString("yyyy-MM-dd") + "' 
                SQL = @"select project_registration.ID,project_registration.doc_no + '-' + project_registration.proj_name as ProjectName
                from project_registration where isdeleted = 0 and inactive = 1 and isactive = 1 and  project_registration.end_date>=DATEFROMPARTS(Year(getdate()),Month(Getdate() ),1 )   order by left(project_registration.doc_no,2),substring(project_registration.doc_no,3,3)";


                //order by left(project_registration.doc_no,2),substring(project_registration.doc_no,3,3)"; 
                //and end_date>'" + MyDate.ToString("yyyy-MM-dd") + "' 
                //SQL = @"select project_registration.ID,project_registration.doc_no + '-' + project_registration.proj_name as ProjectName
                //from project_registration where isdeleted = 0 and inactive = 1 and isactive = 1 and  project_registration.end_date>='" + MyDate.ToString("yyyy-MM-dd") + "'  order by left(project_registration.doc_no,2),substring(project_registration.doc_no,3,3)";

                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ActivityProjectList();
                    obj.ID = Convert.ToInt32(item["ID"].ToString());
                    obj.ProjectName = item["ProjectName"].ToString();
                    List.Add(obj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetActivityProjectsList. The query was executed :", ex.ToString(), "fnGetDailyActiveLog", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public List<DailyLog> GetDailyLogList(string Date, bool WantNewLine = false)
        {
            List<DailyLog> List = new List<DailyLog>();
            DailyLog obj = new DailyLog();

            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetDailyLog(Convert.ToDateTime(Date).Month, Convert.ToDateTime(Date).Year);
                if (TempModuleDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {

                        obj = new DailyLog();
                        obj.LeaveWithAttachmentPending = Convert.ToInt32(item["LeaveWithAttachmentPending"].ToString());
                        obj.LeaveWithAttachmentPendingDates = item["LeaveWithAttachmentPendingDates"].ToString();
                        obj.DailyLogID = Convert.ToInt32(item["ID"].ToString());

                        obj.proj_id = Convert.ToInt32(item["proj_id"].ToString());
                        obj.ActivityID = Convert.ToInt64(item["ActivityID"].ToString());
                        obj.description = item["description"].ToString();
                        //obj.Doj = Convert.ToDateTime(item["doj"].ToString());
                        //obj.LastWorkingDay = Convert.ToDateTime(item["lastworking_day"].ToString());
                        obj.emp_id = Convert.ToInt32(item["emp_id"].ToString());
                        obj.year = Convert.ToInt32(item["year"].ToString());
                        obj.srno = Convert.ToInt32(item["srno"].ToString());
                        obj.month = Convert.ToInt32(item["month"].ToString());
                        obj.Total = Convert.ToInt32((!string.IsNullOrEmpty(item["Total"].ToString()) ? item["Total"] : "0"));
                        obj.Day1 = (string.IsNullOrEmpty(item["Day1"].ToString()) ? 0 : Convert.ToDouble(item["Day1"].ToString()));
                        obj.Day2 = (string.IsNullOrEmpty(item["Day2"].ToString()) ? 0 : Convert.ToDouble(item["Day2"].ToString()));
                        obj.Day3 = (string.IsNullOrEmpty(item["Day3"].ToString()) ? 0 : Convert.ToDouble(item["Day3"].ToString()));
                        obj.Day4 = (string.IsNullOrEmpty(item["Day4"].ToString()) ? 0 : Convert.ToDouble(item["Day4"].ToString()));
                        obj.Day5 = (string.IsNullOrEmpty(item["Day5"].ToString()) ? 0 : Convert.ToDouble(item["Day5"].ToString()));
                        obj.Day6 = (string.IsNullOrEmpty(item["Day6"].ToString()) ? 0 : Convert.ToDouble(item["Day6"].ToString()));
                        obj.Day7 = (string.IsNullOrEmpty(item["Day7"].ToString()) ? 0 : Convert.ToDouble(item["Day7"].ToString()));
                        obj.Day8 = (string.IsNullOrEmpty(item["Day8"].ToString()) ? 0 : Convert.ToDouble(item["Day8"].ToString()));
                        obj.Day9 = (string.IsNullOrEmpty(item["Day9"].ToString()) ? 0 : Convert.ToDouble(item["Day9"].ToString()));
                        obj.Day10 = (string.IsNullOrEmpty(item["Day10"].ToString()) ? 0 : Convert.ToDouble(item["Day10"].ToString()));
                        obj.Day11 = (string.IsNullOrEmpty(item["Day11"].ToString()) ? 0 : Convert.ToDouble(item["Day11"].ToString()));
                        obj.Day12 = (string.IsNullOrEmpty(item["Day12"].ToString()) ? 0 : Convert.ToDouble(item["Day12"].ToString()));
                        obj.Day13 = (string.IsNullOrEmpty(item["Day13"].ToString()) ? 0 : Convert.ToDouble(item["Day13"].ToString()));
                        obj.Day14 = (string.IsNullOrEmpty(item["Day14"].ToString()) ? 0 : Convert.ToDouble(item["Day14"].ToString()));
                        obj.Day15 = (string.IsNullOrEmpty(item["Day15"].ToString()) ? 0 : Convert.ToDouble(item["Day15"].ToString()));
                        obj.Day16 = (string.IsNullOrEmpty(item["Day16"].ToString()) ? 0 : Convert.ToDouble(item["Day16"].ToString()));
                        obj.Day17 = (string.IsNullOrEmpty(item["Day17"].ToString()) ? 0 : Convert.ToDouble(item["Day17"].ToString()));
                        obj.Day18 = (string.IsNullOrEmpty(item["Day18"].ToString()) ? 0 : Convert.ToDouble(item["Day18"].ToString()));
                        obj.Day19 = (string.IsNullOrEmpty(item["Day19"].ToString()) ? 0 : Convert.ToDouble(item["Day19"].ToString()));
                        obj.Day20 = (string.IsNullOrEmpty(item["Day20"].ToString()) ? 0 : Convert.ToDouble(item["Day20"].ToString()));
                        obj.Day21 = (string.IsNullOrEmpty(item["Day21"].ToString()) ? 0 : Convert.ToDouble(item["Day21"].ToString()));
                        obj.Day22 = (string.IsNullOrEmpty(item["Day22"].ToString()) ? 0 : Convert.ToDouble(item["Day22"].ToString()));
                        obj.Day23 = (string.IsNullOrEmpty(item["Day23"].ToString()) ? 0 : Convert.ToDouble(item["Day23"].ToString()));
                        obj.Day24 = (string.IsNullOrEmpty(item["Day24"].ToString()) ? 0 : Convert.ToDouble(item["Day24"].ToString()));
                        obj.Day25 = (string.IsNullOrEmpty(item["Day25"].ToString()) ? 0 : Convert.ToDouble(item["Day25"].ToString()));
                        obj.Day26 = (string.IsNullOrEmpty(item["Day26"].ToString()) ? 0 : Convert.ToDouble(item["Day26"].ToString()));
                        obj.Day27 = (string.IsNullOrEmpty(item["Day27"].ToString()) ? 0 : Convert.ToDouble(item["Day27"].ToString()));
                        obj.Day28 = (string.IsNullOrEmpty(item["Day28"].ToString()) ? 0 : Convert.ToDouble(item["Day28"].ToString()));
                        obj.Day29 = (string.IsNullOrEmpty(item["Day29"].ToString()) ? 0 : Convert.ToDouble(item["Day29"].ToString()));
                        obj.Day30 = (string.IsNullOrEmpty(item["Day30"].ToString()) ? 0 : Convert.ToDouble(item["Day30"].ToString()));
                        obj.Day31 = (string.IsNullOrEmpty(item["Day31"].ToString()) ? 0 : Convert.ToDouble(item["Day31"].ToString()));
                        obj.TravelrequestID = Convert.ToInt64(item["TravelrequestID"].ToString());
                        List.Add(obj);
                    }
                }
                else
                {
                    if (WantNewLine)
                    {
                        obj = new DailyLog();
                        List.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDailyLogList. The query was executed :", ex.ToString(), "fnGetDailyLog", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }


        public List<TravelDailyLog> GetTravelDailyLog(DateTime Date)
        {
            string SQL = "";
            // string FixHolidays = clsApplicationSetting.GetConfigValue("FixHolidays");
            List<TravelDailyLog> List = new List<TravelDailyLog>();
            TravelDailyLog obj = new TravelDailyLog();
            DateTime EndDate = Date.AddMonths(1).AddDays(-1);
            string LocationID = clsApplicationSetting.GetSessionValue("LocationID");
           
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTravelDailyLog(Convert.ToDateTime(Date).Month, Convert.ToDateTime(Date).Year);
                if (TempModuleDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {
                        obj = new TravelDailyLog();
                        obj.TravelReqID= Convert.ToInt64(item["TravelReqID"].ToString());
                        obj.Date = Convert.ToDateTime(item["TravelDate"]).ToString("dd/MM/yyyy");
                        List.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTravelDailyLog. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }
            return List;

        }



        public List<HolidayDailyLog> GetHolidayDailyLog(DateTime Date, string EMPID = "")
        {
            string SQL = "";
            // string FixHolidays = clsApplicationSetting.GetConfigValue("FixHolidays");
            List<HolidayDailyLog> List = new List<HolidayDailyLog>();
            HolidayDailyLog obj = new HolidayDailyLog();
            DateTime EndDate = Date.AddMonths(1).AddDays(-1);
            string LocationID = clsApplicationSetting.GetSessionValue("LocationID");
            string MyEMPID = clsApplicationSetting.GetSessionValue("EMPID");
            try
            {
                if (!string.IsNullOrEmpty(EMPID))
                {
                    MyEMPID = EMPID;
                    //LocationID = clsDataBaseHelper.ExecuteSingleResult("select address.location_id from master_emp inner join address on master_emp.address_id=address.id and master_emp.id=" + EMPID + " and master_emp.isdeleted=0");
                    LocationID = clsDataBaseHelper.ExecuteSingleResult("select master_emp.WorkLocationID from master_emp where master_emp.id=" + EMPID + " and master_emp.isdeleted=0");
                }

                SQL = @"select holiday_date,color_code,Holiday_name,Remarks from HOLIDAY  where isdeleted =0 and holiday.id 
                        in(select holiday_id from map_holiday_loc where isdeleted=0 and location_id=" + LocationID + ") AND  HOLIDAY_DATE " +
                      " between '" + Convert.ToDateTime(Date).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(EndDate).ToString("dd/MMM/yyyy") + "' ORDER BY HOLIDAY_DATE";

                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new HolidayDailyLog();

                    obj.Date = Convert.ToDateTime(item["holiday_date"]).ToString("dd/MM/yyyy");
                    obj.Color = "#e72d35";
                    obj.HolidayType = "Holiday";
                    obj.Description = item["Holiday_name"] + " " + item["Remarks"];
                    obj.ClassName = "holiday";
                    List.Add(obj);
                }

                SQL = @"select leave_log.emp_id,approved,master_leave.leave_name,leavedet_log.date  from leavedet_log inner join leave_log on leavedet_log.leavelog_id =leave_log.ID
                        inner join master_leave on master_leave.id=leave_log.leave_id
                        where leavedet_log.isdeleted =0 and approved in(0,3,1,4)  
                         and emp_id = " + MyEMPID + " and leavedet_log.DATE  between '" + Convert.ToDateTime(Date).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(EndDate).ToString("dd/MMM/yyyy") + "' ORDER BY leavedet_log.date";

                DataSet TempModuleDataSet1 = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet1.Tables[0].Rows)
                {
                    obj = new HolidayDailyLog();

                    obj.Date = Convert.ToDateTime(item["date"]).ToString("dd/MM/yyyy");
                    if (Convert.ToInt32(item["approved"].ToString()) == 0 || Convert.ToInt32(item["approved"].ToString()) == 3)
                    {
                        obj.HolidayType = item["leave_name"].ToString() + " Pending";
                        obj.Name = obj.HolidayType;
                        obj.Color = "#1d46dc";
                        obj.Description = "Leave pending for Approval";
                        obj.ClassName = "pendingleave";

                    }
                    else if (Convert.ToInt32(item["approved"].ToString()) == 1 || Convert.ToInt32(item["approved"].ToString()) == 4)
                    {
                        obj.HolidayType = item["leave_name"].ToString() + " Approved";
                        obj.Name = obj.HolidayType;
                        obj.Color = "#1a9e0a";
                        obj.Description = "Leave Approved";
                        obj.ClassName = "approvedleave";

                    }
                    //else
                    //{
                    //    obj.HolidayType = item["leave_name"].ToString() + " Cancelled";
                    //    obj.Name = obj.HolidayType;
                    //    obj.Color = "#e01e1a";
                    //    obj.Description = "Leave Cancelled";
                    //    obj.ClassName = "Cancelledleave";


                    //}
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetHolidayDailyLog. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }
            return List;

        }

        public AddActivityProjectList AddProjectList(DateTime MyDate)
        {
            AddActivityProjectList obj = new AddActivityProjectList();
            obj.ProjectMappedList = GetSelectedProjectsList("Map");
            obj.ActivityProjectList = GetActivityProjectsList(MyDate);
            return obj;
        }

        // Monthly Log
        public List<DailyLeaveLog> GetDailyLeaveLogList(string Date)
        {
            List<DailyLeaveLog> List = new List<DailyLeaveLog>();
            DailyLeaveLog obj = new DailyLeaveLog();

            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetLeaveLogDetails(Convert.ToDateTime(Date).Month, Convert.ToDateTime(Date).Year);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj = new DailyLeaveLog();

                    obj.DailyLeaveLogID = Convert.ToInt32(item["ID"].ToString());
                    obj.LeaveName = item["Leave_Name"].ToString();

                    obj.Total = Convert.ToInt32((!string.IsNullOrEmpty(item["Total"].ToString()) ? item["Total"] : "0"));
                    Dictionary<int, string> Dictonayobj = new Dictionary<int, string>();
                    for (int i = 1; i <= 31; i++)
                    {
                        Dictonayobj.Add(i, (!string.IsNullOrEmpty(item["day" + i + ""].ToString()) ? item["day" + i + ""].ToString() : "0"));
                    }
                    obj.DaysKeyValue = Dictonayobj;

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDailyLeaveLogList. The query was executed :", ex.ToString(), "fnGetDailyLog", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public List<DailyLog> GetViewDailyLogList(string Date)
        {
            List<DailyLog> List = new List<DailyLog>();
            DailyLog obj = new DailyLog();

            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetDailyActiveLog(Convert.ToDateTime(Date).Month, Convert.ToDateTime(Date).Year);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new DailyLog();
                    obj.proj_id = Convert.ToInt32(item["proj_id"].ToString());
                    obj.proj_name = item["proj_name"].ToString();
                    obj.description = item["description"].ToString();
                    obj.Total = Convert.ToInt32((!string.IsNullOrEmpty(item["Total"].ToString()) ? item["Total"] : "0"));
                    obj.ActivityID = Convert.ToInt32(item["ActivityID"].ToString());

                    Dictionary<int, double> Dictonayobj = new Dictionary<int, double>();

                    for (int i = 1; i <= 31; i++)
                    {

                        double MyValue = 0;
                        double.TryParse(item["day" + i + ""].ToString(), out MyValue);
                        Dictonayobj.Add(i, MyValue);
                    }

                    obj.DaysKeyValue = Dictonayobj;


                    //obj.Day1 = Convert.ToDouble(item["Day1"].ToString());
                    //obj.Day2 = Convert.ToDouble(item["Day2"].ToString());
                    //obj.Day3 = Convert.ToDouble(item["Day3"].ToString());
                    //obj.Day4 = Convert.ToDouble(item["Day4"].ToString());
                    //obj.Day5 = Convert.ToDouble(item["Day5"].ToString());
                    //obj.Day6 = Convert.ToDouble(item["Day6"].ToString());
                    //obj.Day7 = Convert.ToDouble(item["Day7"].ToString());
                    //obj.Day8 = Convert.ToDouble(item["Day8"].ToString());
                    //obj.Day9 = Convert.ToDouble(item["Day9"].ToString());
                    //obj.Day10 = Convert.ToDouble(item["Day10"].ToString());
                    //obj.Day11 = Convert.ToDouble(item["Day11"].ToString());
                    //obj.Day12 = Convert.ToDouble(item["Day12"].ToString());
                    //obj.Day13 = Convert.ToDouble(item["Day13"].ToString());
                    //obj.Day14 = Convert.ToDouble(item["Day14"].ToString());
                    //obj.Day15 = Convert.ToDouble(item["Day15"].ToString());
                    //obj.Day16 = Convert.ToDouble(item["Day16"].ToString());
                    //obj.Day17 = Convert.ToDouble(item["Day17"].ToString());
                    //obj.Day18 = Convert.ToDouble(item["Day18"].ToString());
                    //obj.Day19 = Convert.ToDouble(item["Day19"].ToString());
                    //obj.Day20 = Convert.ToDouble(item["Day20"].ToString());
                    //obj.Day21 = Convert.ToDouble(item["Day21"].ToString());
                    //obj.Day22 = Convert.ToDouble(item["Day22"].ToString());
                    //obj.Day23 = Convert.ToDouble(item["Day23"].ToString());
                    //obj.Day24 = Convert.ToDouble(item["Day24"].ToString());
                    //obj.Day25 = Convert.ToDouble(item["Day25"].ToString());
                    //obj.Day26 = Convert.ToDouble(item["Day26"].ToString());
                    //obj.Day27 = Convert.ToDouble(item["Day27"].ToString());
                    //obj.Day28 = Convert.ToDouble(item["Day28"].ToString());
                    //obj.Day29 = Convert.ToDouble(item["Day29"].ToString());
                    //obj.Day30 = Convert.ToDouble(item["Day30"].ToString());
                    //obj.Day31 = Convert.ToDouble(item["Day31"].ToString());

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetViewDailyLogList. The query was executed :", ex.ToString(), "fnGetDailyActiveLog", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public MonthlyLog GetMonthlyLogData(string SelectedDate)
        {
            string SQL = "";
            MonthlyLog Obj = new MonthlyLog();

            List<DailyLeaveLog> List = new List<DailyLeaveLog>();

            DataSet TempDataSet = new DataSet();
            string Overtime = "N";
            try
            {
                if (clsDataBaseHelper.fnGetOther_FieldName("master_emp", "co_ot", "id", clsApplicationSetting.GetSessionValue("EMPID"), " and isdeleted=0") == "Overtime")
                {
                    Overtime = "Y";
                }

                SQL = @"select leave_log.emp_id,leavedet_log.leave_id,convert(varchar(11),leavedet_log.date,103) as Date,
leavedet_log.hours from leave_log inner join leavedet_log on leave_log.ID=leavedet_log.leavelog_id 
where leave_log.isdeleted =0 and leavedet_log.isdeleted =0 and Leave_log.approved in(0,1,3,4) 
and leave_log.emp_id= " + clsApplicationSetting.GetSessionValue("EMPID") + " and month(leavedet_log.date)= " + Convert.ToDateTime(SelectedDate).Month + " and year(leavedet_log.date)=" + Convert.ToDateTime(SelectedDate).Year + " ORDER BY leave_id,DATE";
                TempDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);


                foreach (DailyLeaveLog item in GetDailyLeaveLogList(SelectedDate))
                {
                    int Total = 0;
                    foreach (DataRow abc in TempDataSet.Tables[0].Rows)
                    {

                        for (int i = 1; i <= 31; i++)
                        {
                            if (item.DailyLeaveLogID == Convert.ToInt32(abc["leave_id"]) && i == Convert.ToInt32(Convert.ToDateTime(abc["Date"]).ToString("dd")))
                            {
                                item.DaysKeyValue[i] = (!string.IsNullOrEmpty(abc["hours"].ToString()) ? abc["hours"].ToString() : "0");

                            }
                        }
                    }
                    for (int i = 1; i <= 31; i++)
                    {
                        Total += Convert.ToInt32(item.DaysKeyValue[i]);
                    }
                    item.Total = Total;

                    List.Add(item);
                }

                Obj.DailyLogList = GetViewDailyLogList(SelectedDate);
                Obj.DailyLeaveLogList = List;

                SQL = @"select isnull(sum(cast(leavedet_log.hours  as int)),0) as TotalLeaveHours from leave_log inner join 
                        leavedet_log  on leave_log.id=leavedet_log.leavelog_id where leave_log.isdeleted=0 and Approved in(1,4) and
                        leave_log.emp_id=" + clsApplicationSetting.GetSessionValue("EMPID") + " and month(leavedet_log.date)=" + Convert.ToDateTime(SelectedDate).Month + " and year(leavedet_log.date)=" + Convert.ToDateTime(SelectedDate).Year + "";
                Obj.LeaveAvailed = Convert.ToDouble(clsDataBaseHelper.ExecuteSingleResult(SQL));

                SQL = @"select isnull(sum(cast(leavedet_log.hours  as int)),0) as TotalLeaveHours from leave_log inner join 
                        leavedet_log  on leave_log.id=leavedet_log.leavelog_id where leave_log.isdeleted=0 and Approved in(0,3) and
                        leave_log.emp_id=" + clsApplicationSetting.GetSessionValue("EMPID") + " and month(leavedet_log.date)=" + Convert.ToDateTime(SelectedDate).Month + " and year(leavedet_log.date)=" + Convert.ToDateTime(SelectedDate).Year + "";
                Obj.LeavePendingForApproval = Convert.ToDouble(clsDataBaseHelper.ExecuteSingleResult(SQL));

                if (Overtime == "Y")
                {
                    SQL = @"select isnull(sum(cast(approve_hours as int )),0) as OTAppHours,isnull(sum(cast(hours as int )),0) as OTHrs 
                        from overtime_log where isdeleted=0 and month=" + Convert.ToDateTime(SelectedDate).Month + " and year=" + Convert.ToDateTime(SelectedDate).Year + " and emp_id=" + clsApplicationSetting.GetSessionValue("EMPID") + "";
                }
                else
                {
                    SQL = @"select isnull(sum(cast(approve_hours as int )),0) as OTAppHours,isnull(sum(cast(hours as int )),0) as OTHrs 
                        from compensatory_off where isdeleted=0 and month(date)=" + Convert.ToDateTime(SelectedDate).Month + " and year(date)=" + Convert.ToDateTime(SelectedDate).Year + " and emp_id=" + clsApplicationSetting.GetSessionValue("EMPID") + "";

                }

                //Ravi
                foreach (DataRow item in clsDataBaseHelper.ExecuteDataSet(SQL).Tables[0].Rows)
                {
                    Obj.CompensatoryHours = Convert.ToDouble(item["OTHrs"]);
                    Obj.CompensatoryHoursApproval = Convert.ToDouble(item["OTAppHours"]);
                }
                SQL = "select id from active_log where emp_id = " + clsApplicationSetting.GetSessionValue("EMPID") + " and isdeleted = 0 and month = " + Convert.ToDateTime(SelectedDate).Month + " and year = " + Convert.ToDateTime(SelectedDate).Year + "";
                if (clsDataBaseHelper.CheckRecord(SQL) > 0)
                {
                    Obj.IsSubmitted = true;
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetActiveLogData. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }
            return Obj;
        }




        public List<CompensatoryOffList> GetCompensatoryOffList(long EMPID, int iMonth, int iYear, string approved)
        {
            List<CompensatoryOffList> List = new List<CompensatoryOffList>();
            CompensatoryOffList obj = new CompensatoryOffList();

            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetCompensatoryOff(EMPID, iMonth, iYear, approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new CompensatoryOffList();
                    obj.CompensatoryOffID = Convert.ToInt64(item["ID"].ToString());
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.EMPID = Convert.ToInt32(item["EMP_ID"].ToString());
                    obj.Emp_Code = item["Emp_Code"].ToString();
                    obj.Emp_name = item["Emp_name"].ToString();
                    obj.proj_name = item["proj_name"].ToString();
                    obj.Reason = item["Reason"].ToString();
                    obj.description = item["description"].ToString();
                    obj.Date = item["Date"].ToString();
                    obj.HRS = (string.IsNullOrEmpty(item["HRS"].ToString()) ? 0 : Convert.ToDecimal(item["HRS"].ToString()));
                    obj.hours = (string.IsNullOrEmpty(item["hours"].ToString()) ? 0 : Convert.ToDecimal(item["hours"].ToString()));
                    obj.Approve_hours = (string.IsNullOrEmpty(item["Approve_hours"].ToString()) ? 0 : Convert.ToDecimal(item["Approve_hours"].ToString()));
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetCompensatoryOffList. The query was executed :", ex.ToString(), "fnGetCompensatoryOff", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public List<RequestCompensatoryOffList> GetCompensatoryOffList(int iMonth, int iYear)
        {
            List<RequestCompensatoryOffList> List = new List<RequestCompensatoryOffList>();
            RequestCompensatoryOffList obj = new RequestCompensatoryOffList();

            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetRequestForCompOff(iMonth, iYear);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new RequestCompensatoryOffList();
                    obj.CompensatoryOffID = Convert.ToInt64(item["ID"].ToString());
                    obj.proj_name = item["proj_name"].ToString();
                    obj.description = item["description"].ToString();
                    obj.Date = item["Date"].ToString();
                    obj.hours = (string.IsNullOrEmpty(item["hours"].ToString()) ? 0 : Convert.ToDecimal(item["hours"].ToString()));
                    // obj.Applied_hours = (string.IsNullOrEmpty(item["Applied_hours"].ToString()) ? 0 : Convert.ToDecimal(item["Applied_hours"].ToString())); code comment by shailendra
                    obj.Applied_hours = (string.IsNullOrEmpty(item["Applied_hours"].ToString()) ? 0 : Convert.ToInt64(item["Applied_hours"].ToString()));
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetCompensatoryOffList. The query was executed :", ex.ToString(), "fnGetCompensatoryOff", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }
        public List<RequestCompensatoryOffList> GetCompensatoryOffNonMITRList(int iMonth, int iYear, string Empids)
        {
            List<RequestCompensatoryOffList> List = new List<RequestCompensatoryOffList>();
            RequestCompensatoryOffList obj = new RequestCompensatoryOffList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetRequestForCompOffNonMITR(iMonth, iYear, Empids);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new RequestCompensatoryOffList();
                    obj.CompensatoryOffID = Convert.ToInt64(item["ID"].ToString());
                    obj.EMPID = Convert.ToInt64(item["emp_id"].ToString());
                    obj.EmpName = item["EmpName"].ToString();
                    obj.proj_name = item["proj_name"].ToString();
                    obj.description = item["description"].ToString();
                    obj.Date = item["Date"].ToString();
                    obj.hours = (string.IsNullOrEmpty(item["hours"].ToString()) ? 0 : Convert.ToDecimal(item["hours"].ToString()));
                    //  obj.Applied_hours = (string.IsNullOrEmpty(item["Applied_hours"].ToString()) ? 0 : Convert.ToDecimal(item["Applied_hours"].ToString())); code comment by shailendra
                    obj.Applied_hours = (string.IsNullOrEmpty(item["Applied_hours"].ToString()) ? 0 : Convert.ToInt64(item["Applied_hours"].ToString()));
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetCompensatoryOffNonMITRList. The query was executed :", ex.ToString(), "GetCompensatoryOffNonMITRList", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public List<OvertimeList> GetOvertimeList(long EMPID, int iMonth, int iYear, string approved)
        {
            List<OvertimeList> List = new List<OvertimeList>();
            OvertimeList obj = new OvertimeList();

            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetOvertime(EMPID, iMonth, iYear, approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new OvertimeList();
                    obj.OvertimeID = Convert.ToInt64(item["ID"].ToString());
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.EMPID = Convert.ToInt32(item["EMP_ID"].ToString());
                    obj.Emp_Code = item["Emp_Code"].ToString();
                    obj.Emp_name = item["Emp_name"].ToString();
                    obj.proj_name = item["proj_name"].ToString();
                    obj.description = item["description"].ToString();
                    obj.Date = item["Date"].ToString();
                    obj.hours = (string.IsNullOrEmpty(item["hours"].ToString()) ? 0 : Convert.ToDecimal(item["hours"].ToString()));
                    obj.Approve_hours = (string.IsNullOrEmpty(item["Approve_hours"].ToString()) ? 0 : Convert.ToDecimal(item["Approve_hours"].ToString()));
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetOvertimeList. The query was executed :", ex.ToString(), "fnGetCompensatoryOff", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }



        public List<ActiveLog> GetActiveLogList(long EMPID, DateTime Date)
        {
            List<ActiveLog> List = new List<ActiveLog>();
            ActiveLog obj = new ActiveLog();

            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetActiveLog(EMPID, Convert.ToDateTime(Date).Month, Convert.ToDateTime(Date).Year);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ActiveLog();
                    obj.proj_id = Convert.ToInt32(item["proj_id"].ToString());
                    obj.projref_no = item["projref_no"].ToString();
                    obj.proj_name = item["proj_name"].ToString();
                    obj.ActivityName = item["ActivityName"].ToString();
                    obj.ActivityID = Convert.ToInt32(item["ActivityID"].ToString());
                    obj.description = item["description"].ToString();
                    obj.Total = Convert.ToInt32((!string.IsNullOrEmpty(item["Total"].ToString()) ? item["Total"] : "0"));
                    obj.TotalHours = Convert.ToInt32((!string.IsNullOrEmpty(item["Total_Hours"].ToString()) ? item["Total"] : "0"));

                    Dictionary<int, double> Dictonayobj = new Dictionary<int, double>();

                    for (int i = 1; i <= 31; i++)
                    {

                        double MyValue = 0;
                        double.TryParse(item["day" + i + ""].ToString(), out MyValue);
                        Dictonayobj.Add(i, MyValue);
                    }

                    obj.DaysKeyValue = Dictonayobj;
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetActiveLogList. The query was executed :", ex.ToString(), "GetActiveLogList", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public List<FNFEmployeeList> GetFNFEmployeeList()
        {
            List<FNFEmployeeList> List = new List<FNFEmployeeList>();
            DataSet TempModuleDataSet = new DataSet();
            FNFEmployeeList Obj = new FNFEmployeeList();
            string SQL = "";

            try
            {
                SQL = @"select * from master_emp where isdeleted=0 and  dor>'31/Dec/1900' and Isactive=1 order by dor desc ";
                TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Obj = new FNFEmployeeList();
                    Obj.EMPID = Convert.ToInt32(item["ID"]);
                    Obj.emp_name = item["emp_name"].ToString();
                    List.Add(Obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFNFEmployeeList. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }
            return List;

        }

        public FNFEmployeeDetails GetFNFSettlementRpt(long EMPID)
        {
            string SQL = "";
            int FinID = 0;
            decimal dHourRate = 0, dSalAmt = 0, dTotSalAmt = 0; int iTotHours = 0;
            DateTime dtMonth = DateTime.Now;
            FNFEmployeeDetails obj = new FNFEmployeeDetails();
            try
            {
                SQL = @"SELECT address.location_id,master_emp.design_id,master_emp.DOR,master_emp.np_payable,master_emp.np_recovery,master_emp.emp_status,master_emp.emp_code,
                        master_emp.uan_no,master_emp.newpf_no,isnull(master_design.design_name,'') as design_name,MASTER_EMP.DOJ,MASTER_EMP.PAN_NO,MASTER_EMP.EMP_NAME,isnull(address.lane1,'') as address,
                        isnull(master_location.location_name,'') as Location_name FROM MASTER_EMP LEFT JOIN master_design ON MASTER_EMP.design_id=master_design.ID left join address 
                        on master_emp.address_id=address.id left join master_location on address.location_id= master_location.id WHERE MASTER_EMP.ISDELETED=0 AND MASTER_EMP.ID=" + EMPID;
                DataSet TempDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempDataSet.Tables[0].Rows)
                {
                    obj.Name = item["emp_name"].ToString();
                    obj.Code = item["emp_code"].ToString();
                    obj.DateofJoining = Convert.ToDateTime(item["DOJ"]);
                    obj.DateofLeaving = Convert.ToDateTime(item["DOR"]);
                    obj.Designation = item["design_name"].ToString();
                    obj.LocationName = item["Location_name"].ToString();
                    obj.NoticePayable = Convert.ToInt32(item["np_payable"]);
                    obj.NoticeRecovery = Convert.ToInt32(item["np_recovery"]);
                    obj.Code = item["emp_code"].ToString();
                }

                SQL = @"select * from FinYear where isdeleted=0 and  from_date<='" + obj.DateofLeaving.ToString("dd/MMM/yyyy") + "' and to_date >='" + obj.DateofLeaving.ToString("dd/MMM/yyyy") + "'";
                DataSet TempDataSet1 = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempDataSet1.Tables[0].Rows)
                {
                    obj.FinID = Convert.ToInt32(item["id"].ToString());
                    FinID = obj.FinID;
                    obj.from_date = Convert.ToDateTime(item["from_date"]);
                    obj.to_date = Convert.ToDateTime(item["to_date"]);
                }

                SQL = @"select * from emp_salary where isdeleted=0 and emp_id=" + EMPID + " and doc_date between '" + obj.from_date.ToString("dd/MMM/yyyy") + "' and '" + obj.to_date.ToString("dd/MMM/yyyy") + "' order by doc_date";
                DataSet TempDataSet2 = clsDataBaseHelper.ExecuteDataSet(SQL);
                if (TempDataSet2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in TempDataSet2.Tables[0].Rows)
                    {
                        obj.LICDue = Convert.ToDecimal(item["lic_amt"]);
                        obj.doc_date = Convert.ToDateTime(item["doc_date"]);
                    }
                }
                else
                {
                    SQL = @"select * from emp_salary where isdeleted=0 and id in(select max(id) from emp_salary where isdeleted=0 and emp_id=" + EMPID + ")";
                    TempDataSet2 = clsDataBaseHelper.ExecuteDataSet(SQL);
                    foreach (DataRow item in TempDataSet2.Tables[0].Rows)
                    {
                        obj.LICDue = Convert.ToDecimal(item["lic_amt"]);
                        obj.doc_date = Convert.ToDateTime(item["doc_date"]);
                    }
                }


                DataSet TempLeave = Common_SPU.fnGetLeaveStatus(EMPID, dtMonth.Month.ToString(), dtMonth.Year.ToString());
                foreach (DataRow item in TempLeave.Tables[0].Rows)
                {
                    obj.LeaveType = item["leave_type"].ToString();
                    if (obj.LeaveType == "Annual Leave(AL)")
                    {
                        obj.AnnualLeaveBalance = item["balance"].ToString();
                    }
                    if (obj.LeaveType == @"CL\SL")
                    {
                        obj.CLSBalance = item["balance"].ToString();
                    }
                }

                //if(obj.from_date <= obj.DateofJoining)
                //{
                //    obj.from_date = obj.DateofJoining;
                //}
                if (obj.DateofJoining > obj.from_date)
                {
                    dtMonth = obj.DateofJoining;
                }
                iTotHours = CommonSpecial.fnGetTotalMonthHours(Convert.ToInt32(EMPID), dtMonth, obj.DateofJoining, obj.DateofLeaving);
                decimal.TryParse(clsDataBaseHelper.ExecuteSingleResult("select hourly_rate from emp_salary where emp_id=" + EMPID + " and isdeleted=0 and id in(select max(id) from emp_salary where isdeleted=0 and emp_id=" + EMPID + " and doc_date<='" + dtMonth.ToString("dd/MMM/yyyy") + "')"), out dHourRate);

                bool bRegin = false;
                DateTime dtResignDate = obj.DateofLeaving, dtTDate = obj.to_date;
                if (dtResignDate.Year > 1900)
                {
                    bRegin = true;
                }
                for (int i = 1; i <= 12; i++)
                {
                    dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                    dTotSalAmt += dSalAmt;
                    dtMonth = dtMonth.AddMonths(1);
                    if ((dtMonth > dtTDate || bRegin == true) && dtMonth > dtResignDate)
                    {
                        break;
                    }
                    iTotHours = CommonSpecial.fnGetTotalMonthHours(Convert.ToInt32(EMPID), dtMonth, obj.DateofJoining, obj.DateofLeaving);
                    decimal.TryParse(clsDataBaseHelper.ExecuteSingleResult("select hourly_rate from emp_salary where emp_id=" + EMPID + " and isdeleted=0 and id in(select max(id) from emp_salary where isdeleted=0 and emp_id=" + EMPID + " and doc_date<='" + dtMonth.ToString("dd/MMM/yyyy") + "')"), out dHourRate);

                }


                double dSalary = Convert.ToDouble(dTotSalAmt);
                double dBasicAmt = Math.Round(dSalary * 0.6, MidpointRounding.AwayFromZero);
                double dHRAAmt = Math.Round(dSalary * 0.3, MidpointRounding.AwayFromZero);
                double dCEAAmt = Math.Round(dSalary * 0.1, MidpointRounding.AwayFromZero);
                double dBasic = Math.Round(dSalary * 0.6, MidpointRounding.AwayFromZero);
                double dHRA = Math.Round(dSalary * 0.3, MidpointRounding.AwayFromZero);
                double dCLA = Math.Round(dSalary * 0.1, MidpointRounding.AwayFromZero);
                double dTotSal = dBasic + dHRA + dCLA;
                double dEPFAmt = Math.Round(dBasic * 0.12, MidpointRounding.AwayFromZero);
                double dMedical = Math.Round(dSalary * 0.05, MidpointRounding.AwayFromZero);
                double dBonus = Math.Round(dSalary * 0.0833, MidpointRounding.AwayFromZero);
                obj.HourlyRate = Math.Round(dHourRate, 2, MidpointRounding.AwayFromZero);


                obj.BasicDue = Convert.ToDecimal(dBasicAmt);
                obj.BasicDrawn = Convert.ToDecimal(dBasicAmt);
                obj.BasicPayable = 0;

                obj.HouseRentDue = Convert.ToDecimal(dHRAAmt);
                obj.HouseRentDrawn = Convert.ToDecimal(dHRAAmt);
                obj.HouseRentPayable = 0;

                obj.TransportDue = Convert.ToDecimal(dCEAAmt);
                obj.TransportDrawn = Convert.ToDecimal(dCEAAmt);
                obj.TransportPayable = 0;



                obj.LICDue = obj.LICDue;
                obj.LICDrawn = obj.LICDue;
                obj.LICPayable = 0;

                obj.EPFDue = Convert.ToDecimal(dEPFAmt);
                obj.EPFDrawn = 0;
                obj.EPFPayable = Convert.ToDecimal(dEPFAmt);




                obj.BonusDue = Convert.ToDecimal(dBonus);
                obj.BonusDrawn = Convert.ToDecimal(dBonus);
                obj.BonusPayable = 0;

                obj.MedicalDue = Convert.ToDecimal(dMedical);
                double dMedicalLimit = 15000;
                if (FinID >= 17)
                {
                    dMedicalLimit = 25000;
                }
                if (dMedical > dMedicalLimit)
                {
                    obj.MedicalPayable = Convert.ToDecimal(dMedical - dMedicalLimit);
                    obj.MedicalDrawn = Convert.ToDecimal(dMedicalLimit);
                }
                else
                {
                    obj.MedicalPayable = Convert.ToDecimal(dMedical);
                    obj.MedicalDrawn = 0;
                }



                #region Fill TDS Details
                decimal[] dEOthAmt = new decimal[13];
                decimal[] dPFAmt = new decimal[13];
                decimal[] dTDSAmt = new decimal[13];
                decimal[] dAdvAmt = new decimal[13];
                decimal[] dDOthAmt = new decimal[13];
                decimal[] dTotDedAmt = new decimal[13];
                decimal dPaidMedAmt = 0, dPaidLICAmt = 0, dPaidBonusAmt = 0;
                //for (int i = 1; i <= 12; i++)
                //{
                //    dEOthAmt[i] = 0;
                //    dPFAmt[i] = 0;
                //    dTDSAmt[i] = 0;
                //    dAdvAmt[i] = 0;
                //    dDOthAmt[i] = 0;
                //    dTotDedAmt[i] = 0;
                //}
                if (FinID >= 16)
                {
                    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString" + (FinID - 15)];
                    if (!string.IsNullOrEmpty(ConnectionString))
                    {
                        SQL = @"SELECT ATTS.MONTH,EDPS.E_D,EDPS.EDPS,ATTS.ID,ATTS.EDPS_ID,ATTS.PAMOUNT,ATTS.ON_AMT,ATTS.AMOUNT,CASE WHEN ATTS.MANUALYN = 1 THEN 'Y' ELSE '' END AS MANUALYN 
                            FROM ATTS,EDPS,EMP WHERE ATTS.EDPS_ID =EDPS.ID AND ATTS.EMP_ID=EMP.EMP_ID AND EMP.EMP_CODE = '" + obj.Code + "' AND ATTS.MONTH BETWEEN '" + obj.from_date.ToString("yyyy-MM-dd") + "' AND '" + obj.to_date.ToString("yyyy-MM-dd") + "' " +
                             "AND ATTS.MONTH IN(SELECT DATEFROMPARTS(YEAR,MONTH,1) from mitr.dbo.timesheet_log where isdeleted=0 and approved=1 and emp_id=" + EMPID + ") ORDER BY ATTS.MONTH,EDPS.E_D DESC,EDPS.EDPS";
                        DataSet TempDataSetTDS = clsDataBaseHelper.ExecuteDataSetByOtherConnection(SQL, ConnectionString);
                        foreach (DataRow item in TempDataSetTDS.Tables[0].Rows)
                        {
                            if (item["EDPS"].ToString() == "PF")
                            {
                                dPFAmt[Convert.ToDateTime(item["MONTH"].ToString()).Month] = Convert.ToDecimal(item["PAMOUNT"].ToString());
                            }
                            else if (item["EDPS"].ToString() == "TDS")
                            {
                                dTDSAmt[Convert.ToDateTime(item["MONTH"].ToString()).Month] = Convert.ToDecimal(item["PAMOUNT"].ToString());
                            }
                            else if (item["EDPS"].ToString() == "Less Advance")
                            {
                                dAdvAmt[Convert.ToDateTime(item["MONTH"].ToString()).Month] = Convert.ToDecimal(item["PAMOUNT"].ToString());
                            }
                            else if (item["E_D"].ToString() == "D")
                            {
                                dDOthAmt[Convert.ToDateTime(item["MONTH"].ToString()).Month] = Convert.ToDecimal(item["PAMOUNT"].ToString());
                            }
                            else if (item["EDPS"].ToString() == "Medical Reimbursement")
                            {
                                dPaidMedAmt = dPaidMedAmt + Convert.ToDecimal(item["PAMOUNT"].ToString());
                            }
                            else if (item["EDPS"].ToString() == "LIC")
                            {
                                dPaidLICAmt = dPaidLICAmt + Convert.ToDecimal(item["PAMOUNT"].ToString());
                            }
                            else if (item["EDPS"].ToString() == "Bonus")
                            {
                                dPaidBonusAmt = dPaidBonusAmt + Convert.ToDecimal(item["PAMOUNT"].ToString());
                            }
                        }
                    }
                }
                decimal dTotMonthAmt = 0;
                for (int i = 1; i <= 12; i++)
                {
                    dTotMonthAmt = dTotMonthAmt + dPFAmt[i];
                    dTotDedAmt[i] = dTotDedAmt[i] + dPFAmt[i];
                }
                obj.LICDrawn = dPaidLICAmt;
                obj.MedicalDrawn = dPaidMedAmt;
                obj.BonusDrawn = dPaidBonusAmt;
                obj.LICPayable = obj.LICDue - obj.LICDrawn;
                obj.MedicalPayable = obj.MedicalDue - obj.MedicalDrawn;
                obj.BonusPayable = obj.BonusDue - obj.BonusDrawn;
                obj.PFContribtionDue = dTotMonthAmt;
                obj.PFContribtionDrawn = dTotMonthAmt;
                obj.EPFDrawn = obj.PFContribtionDue;
                obj.EPFPayable = obj.EPFDue - obj.PFContribtionDue;
                obj.PFContribtionPayable = 0;
                dTotMonthAmt = 0;
                for (int i = 1; i <= 12; i++)
                {
                    dTotMonthAmt = dTotMonthAmt + dTDSAmt[i];
                    dTotDedAmt[i] = dTotDedAmt[i] + dTDSAmt[i];
                }
                obj.TDSDue = dTotMonthAmt;
                obj.TDSDrawn = dTotMonthAmt;
                obj.TDSPayable = 0;
                dTotMonthAmt = 0;
                for (int i = 1; i <= 12; i++)
                {
                    dTotMonthAmt = dTotMonthAmt + dTotDedAmt[i];
                }
                obj.OtherdeductionDue = 0;
                dTotMonthAmt = 0;
                for (int i = 1; i <= 12; i++)
                {
                    dTotMonthAmt = dTotMonthAmt + dAdvAmt[i];
                    dTotDedAmt[i] = dTotDedAmt[i] + dAdvAmt[i];
                }
                obj.AdvancesDue = dTotMonthAmt;
                #endregion

                #region Fill AnualLeave Details
                // Fill AnualLeave Details
                if (FinID >= 16)
                {
                    string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString" + (FinID - 15)];
                    if (!string.IsNullOrEmpty(ConnectionString))
                    {
                        SQL = @"SELECT ISNULL(SUM(PAMOUNT),0) AS PAMOUNT FROM ATTS WHERE EDPS_ID IN(21) AND EMP_ID IN(SELECT EMP_ID FROM EMP WHERE EMP_CODE ='" + obj.Code + "'";
                        DataSet TempDataOther = clsDataBaseHelper.ExecuteDataSetByOtherConnection(SQL, ConnectionString);
                        foreach (DataRow item in TempDataOther.Tables[0].Rows)
                        {
                            obj.GratuityDue = Convert.ToDecimal(item["PAMOUNT"].ToString());
                            obj.GratuityDrawn = Convert.ToDecimal(item["PAMOUNT"].ToString());
                            obj.GratuityPayable = 0;

                        }


                        SQL = @"SELECT * FROM ATTS WHERE EDPS_ID IN(22,23) AND EMP_ID IN(SELECT EMP_ID FROM EMP WHERE EMP_CODE = '" + obj.Code + "') AND MONTH='" + Convert.ToDateTime("1/" + dtResignDate.Month + "/" + dtResignDate.Year + "").ToString("yyyy-MM-dd") + "'";
                        TempDataOther = clsDataBaseHelper.ExecuteDataSetByOtherConnection(SQL, ConnectionString);
                        if (TempDataOther.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow item in TempDataOther.Tables[0].Rows)
                            {
                                if (item["EDPS_ID"].ToString() == "22")
                                {
                                    obj.NPALDue = Convert.ToDecimal(item["PAMOUNT"].ToString());
                                    obj.NPALDrawn = Convert.ToDecimal(item["PAMOUNT"].ToString());
                                    obj.NPALPayable = 0;

                                }
                                else
                                {
                                    obj.ALRecoverDue = Convert.ToDecimal(item["PAMOUNT"].ToString());
                                    obj.ALRecoverDrawn = Convert.ToDecimal(item["PAMOUNT"].ToString());
                                    obj.ALRecoverPayable = 0;
                                }

                            }
                        }
                        else
                        {
                            long iALBal = Common_SPU.fnGetLeaveBalanceByLeaveID(EMPID, "4", dtResignDate.Month, dtResignDate.Year);
                            long iCLBal = Common_SPU.fnGetLeaveBalanceByLeaveID(EMPID, "1", dtResignDate.Month, dtResignDate.Year);
                            long iTotBal = iALBal + iCLBal;
                            long iNPPayable = obj.NoticePayable;
                            long iNPRecovery = obj.NoticeRecovery;
                            long iPayHrs = (iNPPayable - iNPRecovery + iTotBal > 0 ? iNPPayable - iNPRecovery + iTotBal : 0);
                            long iRecHrs = (iNPPayable - iNPRecovery + iTotBal < 0 ? iNPPayable - iNPRecovery + iTotBal : 0) * -1;
                            decimal dHourlyAmt = dHourRate;
                            decimal dPayable = dHourlyAmt * iPayHrs;
                            decimal dRecovery = dHourlyAmt * iRecHrs;
                            SQL = @"select total_value-round((ph_value*
*ot_hours),0) from timesheet_log where emp_id" + EMPID + " and isdeleted=0 and month=" + dtResignDate.Month + " and year=" + dtResignDate.Year + "";
                            decimal.TryParse(clsDataBaseHelper.ExecuteSingleResult(SQL), out dTotSalAmt);
                            if (dPayable > 0)
                            {
                                double dExempted = 0, dAvgSal = 0, dCashEqu = 0, dAmtChrgTax = 300000;
                                SQL = @"SELECT ISNULL(AVG(PAMOUNT),0) AS AVG_AMT FROM ATTS WHERE EDPS_ID=1 AND EMP_ID IN(SELECT EMP_ID FROM EMP WHERE EMP_CODE = '" + obj.Code + "') AND MONTH<'" + Convert.ToDateTime("1/" + dtResignDate.Month + "/" + dtResignDate.Year + "").ToString("yyyy-MM-dd") + "'";
                                DataSet Temp00 = clsDataBaseHelper.ExecuteDataSetByOtherConnection(SQL, ConnectionString);
                                if (Temp00.Tables[0].Rows.Count > 0)
                                {
                                    double.TryParse(Temp00.Tables[0].Rows[0]["AVG_AMT"].ToString(), out dAvgSal);
                                }
                                if (dAvgSal == 0)
                                {
                                    dAvgSal = Convert.ToDouble(dTotSalAmt) * 0.6;
                                }
                                if (iTotBal > 0 && dAvgSal > 0)
                                {
                                    dCashEqu = (dAvgSal / 22) * (iTotBal / 8);
                                }
                                dExempted = Math.Min(dAvgSal, dAmtChrgTax);
                                dExempted = Math.Min(dExempted, dCashEqu);
                                dExempted = Math.Min(dExempted, Convert.ToDouble(dPayable));
                                obj.NPALDue = Convert.ToDecimal(dExempted);
                                obj.NPALDrawn = 0;
                                obj.NPALPayable = Convert.ToDecimal(dExempted);

                            }
                            if (dRecovery > 0)
                            {
                                obj.ALRecoverDue = Convert.ToDecimal(dRecovery);
                                obj.ALRecoverDrawn = 0;
                                obj.ALRecoverPayable = Convert.ToDecimal(dRecovery);
                            }
                        }
                    }
                }
                #endregion

                #region Calculating Total
                // Calculating Total
                obj.GrandGrossSalaryDue = obj.BasicDue + obj.HouseRentDue + obj.TransportDue;
                obj.GrandGrossSalarDrawn = obj.BasicDrawn + obj.HouseRentDrawn + obj.TransportDrawn;
                obj.GrandGrossSalaryPayable = obj.BasicPayable + obj.HouseRentPayable + obj.TransportPayable;

                obj.GrandTotalSalaryBenefitsDue = obj.GrandGrossSalaryDue + obj.LICDue + obj.EPFDue + obj.MedicalDue + obj.BonusDue;
                obj.GrandTotalSalaryBenefitsDrawn = obj.GrandGrossSalarDrawn + obj.LICDrawn + obj.EPFDrawn + obj.MedicalDrawn + obj.BonusDrawn;
                obj.GrandTotalSalaryBenefitsPayable = obj.GrandGrossSalaryPayable + obj.LICPayable + obj.EPFPayable + obj.MedicalPayable + obj.BonusPayable;

                obj.GrandNetDue = (obj.GrandTotalSalaryBenefitsDue + obj.NPALDue + obj.GratuityDue) - (obj.PFContribtionDue + obj.TDSDue + obj.AdvancesDue + obj.ALRecoverDue + obj.OtherdeductionDue);
                obj.GrandNetDrawn = (obj.GrandTotalSalaryBenefitsDrawn + obj.NPALDrawn + obj.GratuityDrawn) - (obj.PFContribtionDrawn + obj.TDSDrawn + obj.AdvancesDrawn + obj.ALRecoverDrawn + obj.OtherdeductionDrawn);
                obj.GrandNetPayable = (obj.GrandTotalSalaryBenefitsPayable + obj.NPALPayable + obj.GratuityPayable) - (obj.PFContribtionPayable + obj.TDSPayable + obj.AdvancesPayable + obj.ALRecoverPayable + obj.OtherdeductionPayable);
                #endregion



            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFNFSettlementRpt. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }


            return obj;
        }

        public List<ConsolidateActivityStatusList> GetConsolidateActivityStatusList(DateTime StartDate, DateTime EndDate, string EmpType = "")
        {
            List<ConsolidateActivityStatusList> List = new List<ConsolidateActivityStatusList>();
            ConsolidateActivityStatusList obj = new ConsolidateActivityStatusList();

            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetConsolidateActivityStatus(StartDate, EndDate, EmpType);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ConsolidateActivityStatusList();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.Emp_Code = item["Emp_Code"].ToString();
                    obj.Emp_name = item["Emp_name"].ToString();
                    obj.DAL = item["DAL"].ToString();
                    obj.MAL = item["MAL"].ToString();
                    obj.AMAL = item["AMAL"].ToString();
                    obj.Supervisor = item["Supervisor"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetConsolidateActivityStatusList. The query was executed :", ex.ToString(), "GetConsolidateActivityStatusList", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }


        public TimeSheetEmployeeDetails GetTimeSheetEmployeeDetails(string Date, long EMPID, string Type)
        {
            TimeSheetEmployeeDetails obj = new TimeSheetEmployeeDetails();
            string SQL = "";
            try
            {
                //string FixHolidays = clsApplicationSetting.GetConfigValue("FixHolidays");
                string FixHolidays = ClsCommon.GetFixHolidaysByEmpid(EMPID.ToString());
                DateTime MyDate = Convert.ToDateTime(Date);
                DateTime dtSMFDate = MyDate;
                DateTime dtSMEDate = dtSMFDate.AddMonths(1).AddDays(-1);
                int iWDays = dtSMEDate.Day, iOffDays = 0;
                int iTotDays = dtSMEDate.Day;
                DateTime dtdate;
                bool bJMonth = false, bRMonth = false;
                int dNPLhrs;
                //obj.EmployeeTimeSheet = CommonSpecial.prcProjectHours(MyDate, EMPID);
                obj.EmployeeTimeSheet = Common_SPU.fnGetActivityLog(EMPID, MyDate.Month, MyDate.Year, Type);
                obj.TimeSheetName = "Time Sheet for the Month of " + MyDate.ToString("MMM") + " " + MyDate.Year;

                SQL = @" select emp_code,Emp_name,salary,doj,dor,
                (select a.emp_name from master_emp as a where a.id=master_emp.hod_name and a.isdeleted=0) as hod_name,
                (select top 1 al.modifiedat from active_log as al where al.emp_id=master_emp.id and al.isdeleted=0 and al.month=" + MyDate.Month + " and al.Year=" + MyDate.Year + ")as DateofSubmision, " +
                " (select top 1 ap.modifiedat from active_approve as ap where ap.emp_id=master_emp.id and ap.isdeleted=0 and ap.month=" + MyDate.Month + " and ap.Year=" + MyDate.Year + ")as DateApproval" +
                " ,(select top 1 COMPANY_NAME from COMPANY) as CompanyName,(select top 1 Address from COMPANY) as CompanyAddress" +
                " ,isnull(MD.design_name,'') as Designation,isnull(L.location_Name, '') as Location" +
                " from master_emp left join master_design as MD on master_emp.design_id =MD.ID " +
                " left join master_location as L on L.ID = master_emp.WorkLocationID where master_emp.isdeleted =0 and master_emp.id=" + EMPID;
                DataSet TempData = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempData.Tables[0].Rows)
                {
                    obj.Code = item["emp_code"].ToString();
                    obj.Name = item["Emp_name"].ToString();
                    obj.HourlyRate = Convert.ToDecimal(item["salary"].ToString());
                    obj.SupervisorName = item["hod_name"].ToString();
                    obj.DateofApproval = (Convert.ToDateTime(item["DateApproval"]).Year > 1800 ? Convert.ToDateTime(item["DateApproval"]).ToString("dd-MMM-yyyy") : "-");
                    obj.DateofSubmission = (Convert.ToDateTime(item["DateofSubmision"]).Year > 1800 ? Convert.ToDateTime(item["DateofSubmision"]).ToString("dd-MMM-yyyy") : "-");
                    obj.DOR = (Convert.ToDateTime(item["DOR"]).Year > 1800 ? Convert.ToDateTime(item["DOR"]).ToString("dd-MMM-yyyy") : "-");
                    obj.DOJ = (Convert.ToDateTime(item["DOJ"]).Year > 1800 ? Convert.ToDateTime(item["DOJ"]).ToString("dd-MMM-yyyy") : "-");
                    obj.CompanyAddress = item["CompanyAddress"].ToString();
                    obj.CompanyName = item["CompanyName"].ToString();
                    obj.Designation = item["Designation"].ToString();
                    obj.Location = item["Location"].ToString();
                    obj.FixHolidays = FixHolidays;
                }
                if (dtSMEDate < Convert.ToDateTime(obj.DOJ))
                {

                }
                else if (dtSMEDate.Month == Convert.ToDateTime(obj.DOJ).Month && dtSMEDate.Year == Convert.ToDateTime(obj.DOJ).Year)
                {
                    iWDays = iWDays - (Convert.ToDateTime(obj.DOJ).Day - 1);
                    bJMonth = true;
                }
                else if (dtSMEDate.Month == Convert.ToDateTime(obj.DOR).Month && dtSMEDate.Year == Convert.ToDateTime(obj.DOR).Year)
                {
                    iWDays = Convert.ToDateTime(obj.DOR).Day;
                    bRMonth = true;
                }
                for (int i = 1; i <= iTotDays; i++)
                {
                    dtdate = Convert.ToDateTime(MyDate.Year + "-" + MyDate.Month + "-" + i.ToString("D2"));

                    if (FixHolidays.Contains(dtdate.ToString("dddd")))
                    {
                        if (bJMonth && dtdate.Day < Convert.ToDateTime(obj.DOJ).Day)
                        {

                        }
                        else if (bRMonth && dtdate.Day > Convert.ToDateTime(obj.DOR).Day)
                        {

                        }
                        else
                        {
                            iOffDays = iOffDays + 1;
                        }

                    }
                }
                iWDays = iWDays - iOffDays;
                SQL = @"  select isnull(SUM(cast(leavedet_log.hours as int)),0) from 
                  leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id
                  and leave_log.emp_id=" + EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date " +
                  " between '" + dtSMFDate.ToString("dd/MMM/yyyy") + "' and '" + dtSMEDate.ToString("dd/MMM/yyyy") + "' and master_leave.shortname<>'AL' and leave_type=2";

                int.TryParse(clsDataBaseHelper.ExecuteSingleResult(SQL), out dNPLhrs);
                obj.PaidHours = (iWDays * 8) - dNPLhrs;
                obj.LWPHours = dNPLhrs;
                obj.MonthHours = (iWDays * 8);

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTimeSheetEmployeeDetails. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }
            return obj;
        }

        public ProcessActivityTimeSheet GetProcessActivityTimeSheet(DateTime SelectedDate, long EMPID)
        {
            int iCount = 0, iUnLock = 0, iLock = 0;
            DateTime MyDate = Convert.ToDateTime(SelectedDate);
            ProcessActivityTimeSheet obj = new ProcessActivityTimeSheet();
            if (EMPID == 0)
            {

                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and year=" + MyDate.Year + " and emp_id not in (37,38,39,40)"), out iCount);
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and approved=0 and year=" + MyDate.Year + " and emp_id not in (37,38,39,40)"), out iUnLock);
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and approved=1 and year=" + MyDate.Year + " and emp_id not in (37,38,39,40)"), out iLock);

            }
            else
            {
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and year=" + MyDate.Year + " and emp_id=" + EMPID), out iCount);
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and approved=0 and year=" + MyDate.Year + " and emp_id =" + EMPID), out iUnLock);
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and approved=1 and year=" + MyDate.Year + " and emp_id =" + EMPID), out iLock);
            }
            obj.iCount = iCount;
            obj.iLock = iLock;
            obj.iUnLock = iUnLock;
            obj.TimeSheetDataSet = GetTimeSheetTable(SelectedDate, EMPID);
            return obj;

        }
        public DataSet GetTimeSheetTable(DateTime SelectedDate, long EMPID)
        {

            DataSet MyDataSet = new DataSet();
            string SQL = "";
            try
            {
                DateTime MyDate = Convert.ToDateTime(SelectedDate);
                DateTime dtFDate = Convert.ToDateTime(DateTime.Now.Year + "-" + MyDate.Month + "-01");
                DateTime dtEDate = dtFDate.AddMonths(1).AddDays(-1);
                MyDataSet = Common_SPU.fnGetProcessTimeSheetLog(MyDate.ToString("dd/MMM/yyyy"), EMPID.ToString());
                foreach (DataRow dtRow in MyDataSet.Tables[0].Rows)
                {
                    SQL = @"select * from timesheet_log where emp_id=" + dtRow["EMPID"] + " and isdeleted=0 and month=" + MyDate.Month + " and year=" + MyDate.Year + "";
                    DataSet TimeSheetDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                    if (TimeSheetDataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in TimeSheetDataSet.Tables[0].Rows)
                        {
                            dtRow["Modify"] = item["Approved"];
                            dtRow["Per Hour Rate"] = item["ph_value"];
                            dtRow["Adj. Hours"] = item["adjust_hours"];
                            dtRow["OT"] = item["ot_hours"];
                            dtRow["AL"] = item["al_hours"];
                            dtRow["Paid Hours"] = item["paid_hours"];
                            dtRow["LWP"] = item["npl_hours"];
                            dtRow["Gross Salary"] = item["total_value"];
                            dtRow["Month Hours"] = item["total_hours"];

                            // Filling Work and Travel Hour
                            SQL = @"select * from timesheetdet_log where isdeleted=0 and TimeSheetlog_id=" + item["ID"] + " order by proj_id";
                            DataSet TimeSheetDataSetLog = clsDataBaseHelper.ExecuteDataSet(SQL);
                            foreach (DataRow Logitem in TimeSheetDataSetLog.Tables[0].Rows)
                            {
                                foreach (DataColumn dc in MyDataSet.Tables[0].Columns)
                                {
                                    if (dc.ColumnName.Contains('#'))
                                    {
                                        if (Convert.ToInt32(dc.ColumnName.Split('#')[1]) == Convert.ToInt32(Logitem["proj_id"]))
                                        {
                                            switch (dc.ColumnName.Split('#')[2])
                                            {
                                                case "WH":
                                                    dtRow[dc.ColumnName] = Convert.ToInt32(Logitem["work_hours"].ToString()) + Convert.ToInt32(Logitem["adj_hours"].ToString());
                                                    break;
                                                case "TH":
                                                    dtRow[dc.ColumnName] = Logitem["travel_hours"];
                                                    break;
                                                case "Value":
                                                    dtRow[dc.ColumnName] = Convert.ToInt32(Logitem["work_value"].ToString()) + Convert.ToInt32(Logitem["travel_value"].ToString());
                                                    break;
                                                default:
                                                    break;
                                            }

                                        }
                                    }

                                }
                            }
                        }

                    }
                    else
                    {
                        // when Timehsset is not filled
                        int iTotDays = dtEDate.Day, iOffDays = 0, lLoactionID = 0;
                        double dSalary = 0;
                        DateTime dtdate;
                        int iWDays = iTotDays;
                        string dtJoiningDate = clsDataBaseHelper.ExecuteSingleResult("select doj from master_emp where id=" + dtRow["EMPID"]);
                        DateTime dtSMFDate = Convert.ToDateTime(DateTime.Now.Year + "-" + MyDate.Month + "-01");
                        DateTime dtSMEDate = dtSMFDate.AddMonths(1).AddDays(-1);
                        int iNoOfDays = dtSMEDate.Day;
                        bool bJMonth = false, bRMonth = false;
                        string dtResignDate = clsDataBaseHelper.ExecuteSingleResult("select dor from master_emp where id=" + dtRow["EMPID"]);
                        if (dtSMEDate < Convert.ToDateTime(dtJoiningDate))
                        {

                        }
                        else if (dtSMEDate.Month == Convert.ToDateTime(dtJoiningDate).Month && dtSMEDate.Year == Convert.ToDateTime(dtJoiningDate).Year)
                        {
                            iWDays = iWDays - (Convert.ToDateTime(dtJoiningDate).Day - 1);
                            bJMonth = true;
                        }
                        else if (dtSMEDate.Month == Convert.ToDateTime(dtResignDate).Month && dtSMEDate.Year == Convert.ToDateTime(dtResignDate).Year)
                        {
                            iWDays = Convert.ToDateTime(dtResignDate).Day;
                            bRMonth = true;
                        }
                        for (int i = 1; i <= iTotDays; i++)
                        {
                            dtdate = Convert.ToDateTime(i + "/" + MyDate.Month + "/" + MyDate.Year);
                            string sDay = dtdate.ToString("ddd");
                            if (sDay == "Sun" | sDay == "Sat")
                            {
                                if (bJMonth == true & dtdate.Day < Convert.ToDateTime(dtJoiningDate).Day)
                                {
                                }
                                else if (bRMonth == true & dtdate.Day > Convert.ToDateTime(dtResignDate).Day)
                                {
                                }
                                else
                                    iOffDays = iOffDays + 1;
                            }
                        }
                        iWDays = iWDays - iOffDays;
                        double dTotLeaveHrs = 0, dOTHrs = 0, dALHrs = 0, dPLhrs = 0, dNPLhrs = 0, dHDHrs = 0, dHLAdjHrs = 0, dPaidHrs = 0, dAdjPercent = 0, dTotHrs = 0, dTotalHrs = 0, dTotValue = 0, dTHours = 0;
                        int dValue = 0;
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select salary from master_emp where id=" + dtRow["EMPID"]), out dSalary);
                        int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select address.location_id from master_emp inner join address on master_emp.address_id=address.id where master_emp.isdeleted=0 and master_emp.id =" + dtRow["EMPID"]), out lLoactionID);
                        DataSet dsHoliday = clsDataBaseHelper.ExecuteDataSet("select convert(varchar(11),HOLIDAY_DATE,103) as holiday_date,color_code,color_name from HOLIDAY where isdeleted =0 and holiday.id in(select holiday_id from map_holiday_loc where isdeleted=0 and location_id=" + lLoactionID + ") AND  HOLIDAY_DATE between '" + (bJMonth ? Convert.ToDateTime(dtJoiningDate).ToString("dd/MMM/yyyy") : dtFDate.ToString("dd/MMM/yyyy")) + "' and '" + (bRMonth ? Convert.ToDateTime(dtResignDate).ToString("dd/MMM/yyyy") : dtEDate.ToString("dd/MMM/yyyy")) + "' ORDER BY HOLIDAY_DATE");
                        int iHoliday = dsHoliday.Tables[0].Rows.Count;
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id where leave_log.emp_id=" + dtRow["EMPID"] + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "'"), out dTotLeaveHrs);

                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(sum(approve_hours),0) from overtime_log where emp_id=" + dtRow["EMPID"] + " and isdeleted=0 and approved=1 and month=" + MyDate.Month + "  and year=" + MyDate.Year + ""), out dOTHrs);
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id where leave_log.emp_id=" + dtRow["EMPID"] + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "' and master_leave.shortname='AL'"), out dALHrs);
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id where leave_log.emp_id=" + dtRow["EMPID"] + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "' and master_leave.shortname<>'AL' and leave_type=1"), out dPLhrs);
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id where leave_log.emp_id=" + dtRow["EMPID"] + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "' and master_leave.shortname<>'AL' and leave_type=2"), out dNPLhrs);
                        dHDHrs = iHoliday * 8;
                        dHLAdjHrs = dPLhrs + dHDHrs;
                        dPaidHrs = (iWDays * 8) - (dALHrs + dNPLhrs);
                        dAdjPercent = dHLAdjHrs / (dPaidHrs - (dPLhrs + dHDHrs));
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(sum(cast(day1 as int)+cast(day2 as int)+cast(day3 as int)+cast(day4 as int)+cast(day5 as int)+cast(day6 as int)+cast(day7 as int)+cast(day8 as int)+cast(day9 as int)+cast(day10 as int)+cast(day11 as int)+cast(day12 as int)+cast(day13 as int)+cast(day14 as int)+cast(day15 as int)+cast(day16 as int)+cast(day17 as int)+cast(day18 as int)+cast(day19 as int)+cast(day20 as int)+cast(day21 as int)+cast(day22 as int)+cast(day23 as int)+cast(day24 as int)+cast(day25 as int)+cast(day26 as int)+cast(day27 as int)+cast(day28 as int)+cast(day29 as int)+cast(day30 as int)+cast(day31 as int)),0) from active_log where emp_id=" + dtRow["EMPID"] + " and isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + ""), out dTHours);

                        foreach (DataColumn dc in MyDataSet.Tables[0].Columns)
                        {
                            if (dc.ColumnName.Contains('#'))
                            {
                                double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select cast(day1 as int)+cast(day2 as int)+cast(day3 as int)+cast(day4 as int)+cast(day5 as int)+cast(day6 as int)+cast(day7 as int)+cast(day8 as int)+cast(day9 as int)+cast(day10 as int)+cast(day11 as int)+cast(day12 as int)+cast(day13 as int)+cast(day14 as int)+cast(day15 as int)+cast(day16 as int)+cast(day17 as int)+cast(day18 as int)+cast(day19 as int)+cast(day20 as int)+cast(day21 as int)+cast(day22 as int)+cast(day23 as int)+cast(day24 as int)+cast(day25 as int)+cast(day26 as int)+cast(day27 as int)+cast(day28 as int)+cast(day29 as int)+cast(day30 as int)+cast(day31 as int) from active_log where emp_id=" + dtRow["EMPID"] + " and isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + " and proj_id=" + dc.ColumnName.Split('#')[1] + ""), out dTotHrs);
                                if (dTotHrs > 0)
                                {
                                    dTotalHrs = dTotalHrs + dTotHrs;
                                    if (dc.ColumnName.Split('#')[2] == "TH")
                                    {
                                        dtRow[dc.ColumnName] = 0;
                                    }
                                    SQL = @"select distinct min(travelrequest_det.travel_date) as fromdate,max(travelrequest_det.travel_date) as todate 
                                            from travelrequest_det inner join travel_request on travelrequest_det.travelreq_id = travel_request.id
                                            where travel_request.isdeleted = 0 and travelrequest_det.isdeleted = 0 and travel_request.approved in(1, 4) and travel_request.emp_id = " + dtRow["EMPID"] + "" +
                                            " and month(travelrequest_det.travel_date)= " + MyDate.Month + " and year(travelrequest_det.travel_date)= " + MyDate.Year + " group by travelrequest_det.travelreq_id order by min(travelrequest_det.travel_date)";
                                    DataSet TempTravelData = clsDataBaseHelper.ExecuteDataSet(SQL);
                                    int TravelHours = 0;
                                    if (TempTravelData.Tables[0].Rows.Count > 0)
                                    {
                                        DateTime dtFTDate, dtTTDate;
                                        DataSet dsLog = clsDataBaseHelper.ExecuteDataSet("select * from active_log where isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + " and emp_id=" + dtRow["EMPID"] + " and proj_id=" + dc.ColumnName.Split('#')[1] + "");
                                        int iCount = 0;
                                        foreach (DataRow Item in TempTravelData.Tables[0].Rows)
                                        {
                                            iCount++;
                                            dtFTDate = Convert.ToDateTime(Item["fromdate"]);
                                            dtTTDate = Convert.ToDateTime(Item["todate"]);
                                            if (iCount > 1)
                                            {
                                                if (dtTTDate >= dtFTDate)
                                                {
                                                    dtFTDate = dtTTDate.AddDays(1);
                                                }

                                            }
                                            if (dtTTDate > dtSMEDate)
                                            {
                                                dtTTDate = dtSMEDate;
                                            }

                                            if (dsLog.Tables[0].Rows.Count > 0)
                                            {
                                                for (int iTmp = dtFTDate.Day; iTmp < dtTTDate.Day; iTmp++)
                                                {
                                                    if (dc.ColumnName.Split('#')[2] == "TH")
                                                    {
                                                        TravelHours = Convert.ToInt32(dtRow[dc.ColumnName]) + Convert.ToInt32(dsLog.Tables[0].Rows[0]["day" + iTmp]);
                                                    }
                                                }
                                            }

                                        }

                                    }
                                    switch (dc.ColumnName.Split('#')[2])
                                    {
                                        case "WH":
                                            dtRow[dc.ColumnName] = Convert.ToInt32(dTotHrs) - TravelHours;
                                            break;
                                        case "TH":
                                            dtRow[dc.ColumnName] = TravelHours;
                                            break;
                                        case "Value":
                                            dValue = Convert.ToInt32(Math.Round(dTotHrs * dSalary, 0, MidpointRounding.AwayFromZero));
                                            if (dTHours == dTotalHrs)
                                            {
                                                if ((Convert.ToInt32(dTotValue) + dValue) != Math.Round(dTotalHrs * dSalary, 0, MidpointRounding.AwayFromZero))
                                                {
                                                    dValue = dValue + Convert.ToInt32((Math.Round(dTotalHrs * dSalary, 0, MidpointRounding.AwayFromZero) - (dTotValue + dValue)));
                                                }
                                            }
                                            dtRow[dc.ColumnName] = dValue;
                                            dTotValue = dTotValue + dValue;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                        }
                        dtRow["Per Hour Rate"] = dSalary;
                        dtRow["Adj. Hours"] = dTHours;
                        dtRow["OT"] = dOTHrs;
                        dtRow["AL"] = dALHrs;
                        dtRow["Paid Hours"] = dTHours + dALHrs + dPLhrs + dHDHrs;
                        dtRow["LWP"] = dNPLhrs;
                        dtRow["Gross Salary"] = dTotValue + Math.Round(dOTHrs * 1.5 * dSalary, 0, MidpointRounding.AwayFromZero);
                        dtRow["Month Hours"] = iWDays * 8;
                        dtRow["Holiday"] = dHDHrs;
                        dtRow["Paid Leave"] = dPLhrs;
                        dtRow["Diff. Hours"] = (dTHours + dALHrs + dPLhrs + dHDHrs + dNPLhrs) - (iWDays * 8);

                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTimeSheetTable. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }
            return MyDataSet;

        }

        private ProcessTimeSheet TempGetProcessTimeSheet(DateTime SelectedDate, string EMPID, string Emptype)
        {
            ProcessTimeSheet result = new ProcessTimeSheet();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@Date", dbType: DbType.Date, value: SelectedDate, direction: ParameterDirection.Input);
                    param.Add("@EmpId", dbType: DbType.String, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("@Emptype", dbType: DbType.String, value: Emptype, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("Spu_GetProcessTimeSheetList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<ProcessTimeSheet>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new ProcessTimeSheet();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EmployeeDetails = reader.Read<ProcessEmployeeTimeSheet>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AllProjects = reader.Read<ProcessProjectTimeSheet>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AllProjectsWithEmp = reader.Read<ProcessProjectTimeSheet>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during TempGetProcessTimeSheet. The query was executed :", ex.ToString(), "Spu_GetProcessTimeSheetList()", "ActivityModal", "ActivityModal", "");

            }
            return result;
        }

        // New Activity process
        private ProcessTimeSheet TempGetProcessTimeSheetOLD(DateTime SelectedDate, string EMPID, string Emptype)
        {
            int iCount = 0, iUnLock = 0, iLock = 0;
            DateTime MyDate = Convert.ToDateTime(SelectedDate);
            ProcessTimeSheet obj = new ProcessTimeSheet();
            string sqlEmpType = string.Empty;
            if (Emptype == "0")
            {
                sqlEmpType = " and user_id>0";
            }
            else if (Emptype == "1")
            {
                sqlEmpType = " and user_id=0";
            }
            if (EMPID == "0")
            {
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and year=" + MyDate.Year + " and emp_id not in (37,38,39,40)"), out iCount);
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and approved=0 and year=" + MyDate.Year + " and emp_id not in (37,38,39,40)"), out iUnLock);
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and approved=1 and year=" + MyDate.Year + " and emp_id not in (37,38,39,40)"), out iLock);
            }
            else if (EMPID.Contains(","))
            {
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and year=" + MyDate.Year + " and emp_id not in (37,38,39,40) and emp_id in (" + EMPID + ") "), out iCount);
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and approved=0 and year=" + MyDate.Year + " and emp_id not in (37,38,39,40) and emp_id in (" + EMPID + ") "), out iUnLock);
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and approved=1 and year=" + MyDate.Year + " and emp_id not in (37,38,39,40) and emp_id in (" + EMPID + ") "), out iLock);
            }
            else
            {
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and year=" + MyDate.Year + " and emp_id=" + EMPID), out iCount);
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and approved=0 and year=" + MyDate.Year + " and emp_id =" + EMPID), out iUnLock);
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(count(id),0) from timesheet_log where month=" + MyDate.Month + " and isdeleted=0 and approved=1 and year=" + MyDate.Year + " and emp_id =" + EMPID), out iLock);
            }
            obj.iCount = iCount;
            obj.iLock = iLock;
            obj.iUnLock = iUnLock;
            obj.SelectedDate = SelectedDate;
            obj.TempEMPID = EMPID;
            obj.EmployeeDetails = TempProcessEmployeeTimeSheet(SelectedDate, EMPID, Emptype);
            return obj;

        }

        private List<ProcessEmployeeTimeSheet> TempProcessEmployeeTimeSheet(DateTime SelectedDate, string EMPID, string Emptype)
        {
            string sqlEmpType = "";
            if (Emptype == "0")
            {
                sqlEmpType = " and master_emp.user_id>0";
            }
            else if (Emptype == "1")
            {
                sqlEmpType = " and master_emp.user_id=0";
            }
            List<ProcessEmployeeTimeSheet> List = new List<ProcessEmployeeTimeSheet>();
            ProcessEmployeeTimeSheet obj = new ProcessEmployeeTimeSheet();
            string SQL = "";
            try
            {
                if (EMPID == "0")
                {
                    SQL = @"select id,ROW_NUMBER() OVER (ORDER BY EMP_NAME) AS SRNO,emp_code,emp_name from master_emp where isdeleted=0 
                and id in(select emp_id from active_log where isdeleted=2 and month=" + SelectedDate.Month + " and year=" + SelectedDate.Year + ") " +
                " and master_emp.id not in (37,38,39,40) " + sqlEmpType + " order by emp_name";
                }
                else if (EMPID.Contains(","))
                {
                    SQL = @"select id,ROW_NUMBER() OVER (ORDER BY EMP_NAME) AS SRNO,emp_code,emp_name from master_emp where isdeleted=0 
                and id in(select emp_id from active_log where isdeleted=2 and month=" + SelectedDate.Month + " and year=" + SelectedDate.Year + ") " +
                " and master_emp.id not in (37,38,39,40) and master_emp.id in (" + EMPID + ") order by emp_name";
                }
                else
                {
                    SQL = @" select id,ROW_NUMBER() OVER (ORDER BY EMP_NAME) AS SRNO,emp_code,emp_name from master_emp where isdeleted=0 
                and id in(select emp_id from active_log where isdeleted=2 and month=" + SelectedDate.Month + " and year=" + SelectedDate.Year + " " +
                " and emp_id=" + EMPID + ") order by emp_name";
                }

                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                List<ProcessProjectTimeSheet> ProjectDetails = new List<ProcessProjectTimeSheet>();
                ProjectDetails = TempProcessProjectTimeSheet(SelectedDate, 0);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ProcessEmployeeTimeSheet();
                    obj.EMPID = Convert.ToInt64(item["id"].ToString());
                    obj.EMPCode = item["emp_code"].ToString();
                    obj.EMPName = item["emp_name"].ToString();
                    obj.ProjectDetails = ProjectDetails;//TempProcessProjectTimeSheet(SelectedDate, obj.EMPID);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetConsolidateActivityStatusList. The query was executed :", ex.ToString(), "GetConsolidateActivityStatusList", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        private List<ProcessProjectTimeSheet> TempProcessProjectTimeSheet(DateTime SelectedDate, long EMPID)
        {
            List<ProcessProjectTimeSheet> List = new List<ProcessProjectTimeSheet>();
            ProcessProjectTimeSheet obj = new ProcessProjectTimeSheet();

            try
            {
                string SQL = @"select ID,doc_no +'-'+ proj_name as projref_no  from project_registration where project_registration.isdeleted=0 and id not in(select id from project_registration
            where inactive=1 and inactivedate<='" + SelectedDate.ToString("dd-MMM-yyyy") + "' and id not in(select proj_id from daily_log where isdeleted=0 and [month]=" + SelectedDate.Month + " and [year]=" + SelectedDate.Year + ")) order by projref_no";

                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ProcessProjectTimeSheet();
                    obj.ProjectID = Convert.ToInt64(item["ID"].ToString());
                    obj.ProjectName = item["projref_no"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during ProcessProjectTimeSheet. The query was executed :", ex.ToString(), "GetConsolidateActivityStatusList", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public List<ConsolidateEMPList> GetConsolidateEMPList(int month, int year)
        {
            List<ConsolidateEMPList> List = new List<ConsolidateEMPList>();
            ConsolidateEMPList obj = new ConsolidateEMPList();
            string SQL = "";
            try
            {
                SQL = @"select id,emp_name + ' (' + emp_code + ')' as emp_name,user_id from master_emp where isdeleted=0 and id in(select emp_id from active_approve where isdeleted=0 and
                month= " + month + " and year=" + year + ") order by emp_name,id";
                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ConsolidateEMPList();
                    obj.EMPID = Convert.ToInt64(item["id"].ToString());

                    obj.EMPName = item["Emp_name"].ToString();
                    obj.user_id = Convert.ToInt32(item["user_id"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetConsolidateEMPList. The query was executed :", ex.ToString(), "GetConsolidateEMPList", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }
        public ProcessTimeSheet FinalGetProcessTimeSheet(DateTime SelectedDate, string EMPID, string Emptype)
        {

            ProcessTimeSheet obj = new ProcessTimeSheet();
            try
            {
                ProcessTimeSheet TempModal = new ProcessTimeSheet();
                TempModal = TempGetProcessTimeSheet(SelectedDate, EMPID, Emptype);

                obj.iCount = TempModal.iCount;
                obj.iLock = TempModal.iLock;
                obj.iUnLock = TempModal.iUnLock;
                obj.SelectedDate = TempModal.SelectedDate;
                obj.TempEMPID = TempModal.TempEMPID;
                obj.EmployeeDetails = TempModal.EmployeeDetails;

                // Now get TimeSheet Values 

                string SQL = "";
                foreach (var item in obj.EmployeeDetails)
                {
                    item.ProjectDetails = TempModal.AllProjects;
                    item.ProjectDetails = TempModal.AllProjectsWithEmp.Where(n => n.Empid == item.EMPID).ToList();
                    SQL = @"select * from timesheet_log where emp_id=" + item.EMPID + " and isdeleted=0 and month=" + SelectedDate.Month + " and   year=" + SelectedDate.Year + "";
                    DataSet TimeSheetDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                    if (TimeSheetDataSet.Tables[0].Rows.Count > 0)
                    {

                        decimal ot_hours, adjust_hours, al_hours, pl_hours, hd_hours, paid_hours, ph_value, total_value, npl_hours, total_hours, difference_hours;
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["adjust_hours"].ToString(), out adjust_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["al_hours"].ToString(), out al_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["pl_hours"].ToString(), out pl_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["hd_hours"].ToString(), out hd_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["paid_hours"].ToString(), out paid_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["ph_value"].ToString(), out ph_value);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["total_value"].ToString(), out total_value);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["npl_hours"].ToString(), out npl_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["total_hours"].ToString(), out total_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["difference_hours"].ToString(), out difference_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["ot_hours"].ToString(), out ot_hours);

                        item.IsDataComingFormTimeSheet = true;
                        item.Checkbox = "Yes";
                        item.Modify = Convert.ToInt32(TimeSheetDataSet.Tables[0].Rows[0]["Approved"]);
                        item.PerHourRate = ph_value;
                        item.AdjHourRate = adjust_hours;//
                        item.OT = ot_hours;
                        item.PaidHours = Convert.ToDecimal(item.TotalHours) - npl_hours;
                        //item.PaidHours = paid_hours;
                        item.GrossSalary = total_value;
                        item.MonthHours = Convert.ToDecimal(item.TotalHours); //total_hours;

                        // these value wil use later for calculation
                        // item.hdnAdjHourRate = adjust_hours;
                        item.hdnAdjHourRate = Convert.ToDecimal(item.TotalHours) - al_hours - pl_hours - hd_hours - npl_hours;
                        item.hdnLWP = npl_hours;
                        item.hdnAL = al_hours;//
                        item.hdnHolidayHours = hd_hours;
                        item.hdnPaidLeave = pl_hours;
                        item.AL = al_hours;
                        item.LWP = npl_hours;
                        // Filling Work and Travel Hour
                        decimal DiffRanceHours = 0;
                        foreach (var projectitem in item.ProjectDetails)
                        {
                            SQL = @"select * from timesheetdet_log where isdeleted=0 and TimeSheetlog_id=" + TimeSheetDataSet.Tables[0].Rows[0]["ID"].ToString() + " and proj_ID=" + projectitem.ProjectID + " order by proj_id";
                            DataSet TimeSheetDataSetLog = clsDataBaseHelper.ExecuteDataSet(SQL);
                            decimal work_hours, work_value, travel_hours, travel_value, adj_hours;
                            if (TimeSheetDataSetLog.Tables[0].Rows.Count > 0)
                            {
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["work_hours"].ToString(), out work_hours);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["work_value"].ToString(), out work_value);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["travel_hours"].ToString(), out travel_hours);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["travel_value"].ToString(), out travel_value);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["adj_hours"].ToString(), out adj_hours);


                                DiffRanceHours += projectitem.TH + projectitem.WH;
                            }
                        }
                        // item.DiffHourRate = (item.MonthHours - DiffRanceHours);
                        item.DiffHourRate = difference_hours;


                    }
                    else
                    {
                        // when Timehsset is not filled
                        DateTime MyDate = Convert.ToDateTime(SelectedDate);
                        DateTime dtFDate = Convert.ToDateTime(MyDate.Year + "-" + MyDate.Month + "-01");
                        DateTime dtEDate = dtFDate.AddMonths(1).AddDays(-1);
                        int iTotDays = dtEDate.Day, lLoactionID = 0;
                        double dSalary = 0;
                        //DateTime dtdate;
                        int iWDays = item.iWDays;

                        string dtJoiningDate = item.doj; //clsDataBaseHelper.ExecuteSingleResult("select doj from master_emp where id=" + item.EMPID);
                        DateTime dtSMFDate = Convert.ToDateTime(MyDate.Year + "-" + MyDate.Month + "-01");
                        DateTime dtSMEDate = dtSMFDate.AddMonths(1).AddDays(-1);
                        int iNoOfDays = dtSMEDate.Day;
                        string FixHolidays = ClsCommon.GetFixHolidaysByEmpid(item.EMPID.ToString());
                        bool bJMonth = false, bRMonth = false;
                        string dtResignDate = item.dor; //clsDataBaseHelper.ExecuteSingleResult("select dor from master_emp where id=" + item.EMPID);

                        if (dtSMEDate < Convert.ToDateTime(dtJoiningDate))
                        {

                        }
                        else if (dtSMEDate.Month == Convert.ToDateTime(dtJoiningDate).Month && dtSMEDate.Year == Convert.ToDateTime(dtJoiningDate).Year)
                        {
                            bJMonth = true;
                        }
                        else if (dtSMEDate.Month == Convert.ToDateTime(dtResignDate).Month && dtSMEDate.Year == Convert.ToDateTime(dtResignDate).Year)
                        {
                            bRMonth = true;
                        }

                        double dTotLeaveHrs = 0, dTotLeaveHrsPaid = 0, dOTHrs = 0, dALHrs = 0, dPLhrs = 0, dNPLhrs = 0, dHDHrs = 0, dHLAdjHrs = 0, dPaidHrs = 0, dAdjPercent = 0, dTotHrs = 0, dTotalHrs = 0, dTotValue = 0, dTHours = 0;
                        double dValue = 0; int TotalHours = 0;
                        double.TryParse(item.dSalary, out dSalary);
                        int.TryParse(item.lLoactionID, out lLoactionID);
                        DataSet dsHoliday = clsDataBaseHelper.ExecuteDataSet("select convert(varchar(11),HOLIDAY_DATE,103) as holiday_date,color_code,color_name from HOLIDAY where isdeleted =0 and holiday.id in(select holiday_id from map_holiday_loc where isdeleted=0 and location_id=" + lLoactionID + ") AND  HOLIDAY_DATE between '" + (bJMonth ? Convert.ToDateTime(dtJoiningDate).ToString("dd/MMM/yyyy") : dtFDate.ToString("dd/MMM/yyyy")) + "' and '" + (bRMonth ? Convert.ToDateTime(dtResignDate).ToString("dd/MMM/yyyy") : dtEDate.ToString("dd/MMM/yyyy")) + "'" + (item.WorkingDays.Contains("5") ? " and format(HOLIDAY_DATE,'ddd')<>'Sat'" : "") + " and charindex(DATENAME(dw,HOLIDAY_DATE),'" + @FixHolidays + "')=0 ORDER BY HOLIDAY_DATE");
                        int iHoliday = dsHoliday.Tables[0].Rows.Count;

                        double.TryParse(item.dTotLeaveHrs, out dTotLeaveHrs);
                        double.TryParse(item.dTotLeaveHrsPaid, out dTotLeaveHrsPaid);

                        double.TryParse(item.dOTHrs, out dOTHrs);
                        double.TryParse(item.dALHrs, out dALHrs);
                        double.TryParse(item.dPLhrs, out dPLhrs);
                        double.TryParse(item.dNPLhrs, out dNPLhrs);
                        int.TryParse(item.TotalHours, out TotalHours);

                        dHDHrs = iHoliday * 8;
                        dHLAdjHrs = dPLhrs + dHDHrs;
                        dPaidHrs = (iWDays * 8) - (dALHrs + dNPLhrs);
                        dAdjPercent = dHLAdjHrs / (dPaidHrs - (dPLhrs + dHDHrs));
                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(sum(cast(day1 as int)+cast(day2 as int)+cast(day3 as int)+cast(day4 as int)+cast(day5 as int)+cast(day6 as int)+cast(day7 as int)+cast(day8 as int)+cast(day9 as int)+cast(day10 as int)+cast(day11 as int)+cast(day12 as int)+cast(day13 as int)+cast(day14 as int)+cast(day15 as int)+cast(day16 as int)+cast(day17 as int)+cast(day18 as int)+cast(day19 as int)+cast(day20 as int)+cast(day21 as int)+cast(day22 as int)+cast(day23 as int)+cast(day24 as int)+cast(day25 as int)+cast(day26 as int)+cast(day27 as int)+cast(day28 as int)+cast(day29 as int)+cast(day30 as int)+cast(day31 as int)),0) from active_log where emp_id=" + item.EMPID + " and isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + ""), out dTHours);
                        double.TryParse(item.dTHours, out dTHours);

                        // Filling Work and Travel Hour

                        foreach (var projectitem in item.ProjectDetails)
                        {
                            double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select sum(cast(day1 as int)+cast(day2 as int)+cast(day3 as int)+cast(day4 as int)+cast(day5 as int)+cast(day6 as int)+cast(day7 as int)+cast(day8 as int)+cast(day9 as int)+cast(day10 as int)+cast(day11 as int)+cast(day12 as int)+cast(day13 as int)+cast(day14 as int)+cast(day15 as int)+cast(day16 as int)+cast(day17 as int)+cast(day18 as int)+cast(day19 as int)+cast(day20 as int)+cast(day21 as int)+cast(day22 as int)+cast(day23 as int)+cast(day24 as int)+cast(day25 as int)+cast(day26 as int)+cast(day27 as int)+cast(day28 as int)+cast(day29 as int)+cast(day30 as int)+cast(day31 as int)) from active_log where emp_id=" + item.EMPID + " and isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + " and proj_id=" + projectitem.ProjectID + ""), out dTotHrs);
                            if (dTotHrs > 0)
                            {
                                dTotalHrs = dTotalHrs + dTotHrs;
                                //int TravelHours = 0;                               
                                dValue = Math.Round(dTotHrs * dSalary, 0, MidpointRounding.AwayFromZero);
                                if (dTHours == dTotalHrs)
                                {
                                    if ((Math.Round(dTotValue + dValue, 0, MidpointRounding.AwayFromZero)) != Math.Round(dTotalHrs * dSalary, 0, MidpointRounding.AwayFromZero))
                                    {
                                        //dValue = dValue + Convert.ToInt32((Math.Round(dTotalHrs * dSalary, 0, MidpointRounding.AwayFromZero) - (dTotValue + dValue)));
                                        dValue = Math.Round(dValue + (dTotalHrs * dSalary) - (dTotValue + dValue), 0, MidpointRounding.AwayFromZero);
                                    }
                                }

                                //projectitem.Value = dValue;
                                dTotValue = dTotValue + dValue;
                            }
                            else if (TotalHours == Convert.ToInt32(dHDHrs) + Convert.ToInt32(dNPLhrs))
                            {
                                //dTotLeaveHrsPaid = dTotLeaveHrsPaid - dHDHrs;
                                dNPLhrs = dNPLhrs + dHDHrs;
                                dHDHrs = 0;
                            }
                        }
                        item.PerHourRate = Convert.ToDecimal(dSalary);
                        //item.AdjHourRate = Convert.ToDecimal(dTHours);
                        item.AdjHourRate = TotalHours - Convert.ToInt32(dALHrs) - Convert.ToInt32(dPLhrs) - Convert.ToInt32(dHDHrs) - Convert.ToInt32(dNPLhrs);
                        item.OT = Convert.ToDecimal(dOTHrs);
                        item.AL = Convert.ToDecimal(dALHrs);
                        //item.PaidHours = Convert.ToDecimal(dTHours + dALHrs + dPLhrs + dHDHrs);
                        item.PaidHours = Convert.ToDecimal(TotalHours - dNPLhrs);
                        //item.GrossSalary = Convert.ToDecimal(dTotValue + Math.Round( Convert.ToDouble(item.AL) * dSalary, 0, MidpointRounding.AwayFromZero) + Math.Round(dOTHrs * 2 * dSalary, 0, MidpointRounding.AwayFromZero));
                        item.GrossSalary = Convert.ToDecimal(Math.Round(dTotValue + (Convert.ToDouble(item.AL) * dSalary), 0, MidpointRounding.AwayFromZero) + Math.Round(dOTHrs * 2 * dSalary, 0, MidpointRounding.AwayFromZero));
                        //item.MonthHours = Convert.ToDecimal(iWDays * 8);
                        item.MonthHours = Convert.ToDecimal(TotalHours);
                        item.LWP = Convert.ToDecimal(dNPLhrs);
                        item.Holiday = Convert.ToDecimal(dHDHrs);
                        item.PaidLeave = Convert.ToDecimal(dTotLeaveHrsPaid);


                        // these value wil use later for calculation
                        item.hdnAdjHourRate = item.AdjHourRate;
                        item.hdnLWP = item.LWP;
                        item.hdnAL = item.AL;
                        item.hdnHolidayHours = item.Holiday;
                        item.hdnPaidLeave = item.PaidLeave;


                    }

                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during FinalGetProcessTimeSheet. The query was executed :", ex.ToString(), "TempGetProcessTimeSheet", "ActivityModal", "ActivityModal", "");
            }
            return obj;

        }
        public ProcessTimeSheet FinalGetProcessTimeSheet01092022(DateTime SelectedDate, string EMPID, string Emptype)
        {

            ProcessTimeSheet obj = new ProcessTimeSheet();
            try
            {
                ProcessTimeSheet TempModal = new ProcessTimeSheet();
                TempModal = TempGetProcessTimeSheet(SelectedDate, EMPID, Emptype);

                obj.iCount = TempModal.iCount;
                obj.iLock = TempModal.iLock;
                obj.iUnLock = TempModal.iUnLock;
                obj.SelectedDate = TempModal.SelectedDate;
                obj.TempEMPID = TempModal.TempEMPID;
                obj.EmployeeDetails = TempModal.EmployeeDetails;

                // Now get TimeSheet Values 

                string SQL = "";
                foreach (var item in obj.EmployeeDetails)
                {
                    item.ProjectDetails = TempModal.AllProjects;
                    item.ProjectDetails = TempModal.AllProjectsWithEmp.Where(n => n.Empid == item.EMPID).ToList();
                    SQL = @"select * from timesheet_log where emp_id=" + item.EMPID + " and isdeleted=0 and month=" + SelectedDate.Month + " and   year=" + SelectedDate.Year + "";
                    DataSet TimeSheetDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                    if (TimeSheetDataSet.Tables[0].Rows.Count > 0)
                    {

                        decimal ot_hours, adjust_hours, al_hours, pl_hours, hd_hours, paid_hours, ph_value, total_value, npl_hours, total_hours, difference_hours;
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["adjust_hours"].ToString(), out adjust_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["al_hours"].ToString(), out al_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["pl_hours"].ToString(), out pl_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["hd_hours"].ToString(), out hd_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["paid_hours"].ToString(), out paid_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["ph_value"].ToString(), out ph_value);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["total_value"].ToString(), out total_value);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["npl_hours"].ToString(), out npl_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["total_hours"].ToString(), out total_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["difference_hours"].ToString(), out difference_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["ot_hours"].ToString(), out ot_hours);

                        item.IsDataComingFormTimeSheet = true;
                        item.Checkbox = "Yes";
                        item.Modify = Convert.ToInt32(TimeSheetDataSet.Tables[0].Rows[0]["Approved"]);
                        item.PerHourRate = ph_value;
                        item.AdjHourRate = adjust_hours;//
                        item.OT = ot_hours;
                        item.PaidHours = paid_hours;
                        item.GrossSalary = total_value;
                        item.MonthHours = total_hours;

                        // these value wil use later for calculation
                        item.hdnAdjHourRate = adjust_hours;
                        item.hdnLWP = npl_hours;
                        item.hdnAL = al_hours;//
                        item.hdnHolidayHours = hd_hours;
                        item.hdnPaidLeave = pl_hours;
                        item.AL = al_hours;

                        // Filling Work and Travel Hour
                        decimal DiffRanceHours = 0;
                        foreach (var projectitem in item.ProjectDetails)
                        {
                            SQL = @"select * from timesheetdet_log where isdeleted=0 and TimeSheetlog_id=" + TimeSheetDataSet.Tables[0].Rows[0]["ID"].ToString() + " and proj_ID=" + projectitem.ProjectID + " order by proj_id";
                            DataSet TimeSheetDataSetLog = clsDataBaseHelper.ExecuteDataSet(SQL);
                            decimal work_hours, work_value, travel_hours, travel_value, adj_hours;
                            if (TimeSheetDataSetLog.Tables[0].Rows.Count > 0)
                            {
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["work_hours"].ToString(), out work_hours);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["work_value"].ToString(), out work_value);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["travel_hours"].ToString(), out travel_hours);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["travel_value"].ToString(), out travel_value);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["adj_hours"].ToString(), out adj_hours);

                                //projectitem.TH = travel_hours;
                                //projectitem.WH = work_hours + adj_hours;
                                //projectitem.TravelValue = travel_value;
                                //projectitem.WorkValue = work_value;
                                //projectitem.hdnPrAdjsutHours = adj_hours;

                                //projectitem.Value = travel_value + work_value;

                                DiffRanceHours += projectitem.TH + projectitem.WH;
                            }
                        }
                        // item.DiffHourRate = (item.MonthHours - DiffRanceHours);
                        item.DiffHourRate = difference_hours;


                    }
                    else
                    {
                        // when Timehsset is not filled
                        DateTime MyDate = Convert.ToDateTime(SelectedDate);
                        DateTime dtFDate = Convert.ToDateTime(MyDate.Year + "-" + MyDate.Month + "-01");
                        DateTime dtEDate = dtFDate.AddMonths(1).AddDays(-1);
                        int iTotDays = dtEDate.Day, iOffDays = 0, lLoactionID = 0;
                        double dSalary = 0;
                        DateTime dtdate;
                        int iWDays = iTotDays;

                        string dtJoiningDate = item.doj; //clsDataBaseHelper.ExecuteSingleResult("select doj from master_emp where id=" + item.EMPID);
                        DateTime dtSMFDate = Convert.ToDateTime(MyDate.Year + "-" + MyDate.Month + "-01");
                        DateTime dtSMEDate = dtSMFDate.AddMonths(1).AddDays(-1);
                        int iNoOfDays = dtSMEDate.Day;
                        string FixHolidays = ClsCommon.GetFixHolidaysByEmpid(item.EMPID.ToString());
                        bool bJMonth = false, bRMonth = false;
                        string dtResignDate = item.dor; //clsDataBaseHelper.ExecuteSingleResult("select dor from master_emp where id=" + item.EMPID);
                        if (dtSMEDate < Convert.ToDateTime(dtJoiningDate))
                        {

                        }
                        else if (dtSMEDate.Month == Convert.ToDateTime(dtJoiningDate).Month && dtSMEDate.Year == Convert.ToDateTime(dtJoiningDate).Year)
                        {
                            iWDays = iWDays - (Convert.ToDateTime(dtJoiningDate).Day - 1);
                            bJMonth = true;
                        }
                        else if (dtSMEDate.Month == Convert.ToDateTime(dtResignDate).Month && dtSMEDate.Year == Convert.ToDateTime(dtResignDate).Year)
                        {
                            iWDays = Convert.ToDateTime(dtResignDate).Day;
                            bRMonth = true;
                        }
                        for (int i = 1; i <= iTotDays; i++)
                        {
                            dtdate = Convert.ToDateTime(i + "/" + MyDate.Month + "/" + MyDate.Year);
                            string sDay = dtdate.ToString("ddd");
                            if (sDay == "Sun" | (sDay == "Sat" && FixHolidays.Contains("Sat")))
                            {
                                if (bJMonth == true & dtdate.Day < Convert.ToDateTime(dtJoiningDate).Day)
                                {
                                }
                                else if (bRMonth == true & dtdate.Day > Convert.ToDateTime(dtResignDate).Day)
                                {
                                }
                                else
                                    iOffDays = iOffDays + 1;
                            }
                        }
                        iWDays = iWDays - iOffDays;
                        double dTotLeaveHrs = 0, dTotLeaveHrsPaid = 0, dOTHrs = 0, dALHrs = 0, dPLhrs = 0, dNPLhrs = 0, dHDHrs = 0, dHLAdjHrs = 0, dPaidHrs = 0, dAdjPercent = 0, dTotHrs = 0, dTotalHrs = 0, dTotValue = 0, dTHours = 0;
                        double dValue = 0;
                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select salary from master_emp where id=" + item.EMPID), out dSalary);
                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select top 1 HourlyRate from SS_EmployeeSalary where empid="+ item.EMPID.ToString() + " and isdeleted=0 and EffectiveDate <='"+ dtFDate.ToString("yyyy-MMM-dd") + "' order  by effectivedate desc"), out dSalary);
                        //int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select worklocationid from master_emp  where master_emp.isdeleted=0 and master_emp.id =" + item.EMPID), out lLoactionID);
                        double.TryParse(item.dSalary, out dSalary);
                        int.TryParse(item.lLoactionID, out lLoactionID);
                        DataSet dsHoliday = clsDataBaseHelper.ExecuteDataSet("select convert(varchar(11),HOLIDAY_DATE,103) as holiday_date,color_code,color_name from HOLIDAY where isdeleted =0 and holiday.id in(select holiday_id from map_holiday_loc where isdeleted=0 and location_id=" + lLoactionID + ") AND  HOLIDAY_DATE between '" + (bJMonth ? Convert.ToDateTime(dtJoiningDate).ToString("dd/MMM/yyyy") : dtFDate.ToString("dd/MMM/yyyy")) + "' and '" + (bRMonth ? Convert.ToDateTime(dtResignDate).ToString("dd/MMM/yyyy") : dtEDate.ToString("dd/MMM/yyyy")) + "'" + (item.WorkingDays.Contains("5") ? " and format(HOLIDAY_DATE,'ddd')<>'Sat'" : "") + " ORDER BY HOLIDAY_DATE");
                        int iHoliday = dsHoliday.Tables[0].Rows.Count;
                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id where leave_log.emp_id=" + item.EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "'"), out dTotLeaveHrs);
                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id  where leave_log.leave_id <>4 and leave_log.emp_id=" + item.EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4)  and leave_type=1 and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "'"), out dTotLeaveHrsPaid);

                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(sum(approve_hours),0) from overtime_log where emp_id=" + item.EMPID + " and isdeleted=0 and approved=1 and month=" + MyDate.Month + "  and year=" + MyDate.Year + ""), out dOTHrs);
                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id where leave_log.emp_id=" + item.EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "' and master_leave.shortname='AL'"), out dALHrs);
                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id where leave_log.emp_id=" + item.EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "' and master_leave.shortname<>'AL' and leave_type=1"), out dPLhrs);
                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id where leave_log.emp_id=" + item.EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "' and master_leave.shortname<>'AL' and leave_type=2"), out dNPLhrs);

                        double.TryParse(item.dTotLeaveHrs, out dTotLeaveHrs);
                        double.TryParse(item.dTotLeaveHrsPaid, out dTotLeaveHrsPaid);

                        double.TryParse(item.dOTHrs, out dOTHrs);
                        double.TryParse(item.dALHrs, out dALHrs);
                        double.TryParse(item.dPLhrs, out dPLhrs);
                        double.TryParse(item.dNPLhrs, out dNPLhrs);

                        dHDHrs = iHoliday * 8;
                        dHLAdjHrs = dPLhrs + dHDHrs;
                        dPaidHrs = (iWDays * 8) - (dALHrs + dNPLhrs);
                        dAdjPercent = dHLAdjHrs / (dPaidHrs - (dPLhrs + dHDHrs));
                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(sum(cast(day1 as int)+cast(day2 as int)+cast(day3 as int)+cast(day4 as int)+cast(day5 as int)+cast(day6 as int)+cast(day7 as int)+cast(day8 as int)+cast(day9 as int)+cast(day10 as int)+cast(day11 as int)+cast(day12 as int)+cast(day13 as int)+cast(day14 as int)+cast(day15 as int)+cast(day16 as int)+cast(day17 as int)+cast(day18 as int)+cast(day19 as int)+cast(day20 as int)+cast(day21 as int)+cast(day22 as int)+cast(day23 as int)+cast(day24 as int)+cast(day25 as int)+cast(day26 as int)+cast(day27 as int)+cast(day28 as int)+cast(day29 as int)+cast(day30 as int)+cast(day31 as int)),0) from active_log where emp_id=" + item.EMPID + " and isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + ""), out dTHours);
                        double.TryParse(item.dTHours, out dTHours);

                        // Filling Work and Travel Hour

                        foreach (var projectitem in item.ProjectDetails)
                        {

                            double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select sum(cast(day1 as int)+cast(day2 as int)+cast(day3 as int)+cast(day4 as int)+cast(day5 as int)+cast(day6 as int)+cast(day7 as int)+cast(day8 as int)+cast(day9 as int)+cast(day10 as int)+cast(day11 as int)+cast(day12 as int)+cast(day13 as int)+cast(day14 as int)+cast(day15 as int)+cast(day16 as int)+cast(day17 as int)+cast(day18 as int)+cast(day19 as int)+cast(day20 as int)+cast(day21 as int)+cast(day22 as int)+cast(day23 as int)+cast(day24 as int)+cast(day25 as int)+cast(day26 as int)+cast(day27 as int)+cast(day28 as int)+cast(day29 as int)+cast(day30 as int)+cast(day31 as int)) from active_log where emp_id=" + item.EMPID + " and isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + " and proj_id=" + projectitem.ProjectID + ""), out dTotHrs);


                            if (dTotHrs > 0)
                            {
                                dTotalHrs = dTotalHrs + dTotHrs;
                                //int TravelHours = 0;
                                //SQL = @"select distinct min(travelrequest_det.travel_date) as fromdate,max(travelrequest_det.travel_date) as todate 
                                //            from travelrequest_det inner join travel_request on travelrequest_det.travelreq_id = travel_request.id
                                //            where travel_request.isdeleted = 0 and travelrequest_det.isdeleted = 0 and travel_request.approved in(1, 4) and travel_request.emp_id = " + item.EMPID + "" +
                                //        " and month(travelrequest_det.travel_date)= " + MyDate.Month + " and year(travelrequest_det.travel_date)= " + MyDate.Year + " group by travelrequest_det.travelreq_id order by min(travelrequest_det.travel_date)";
                                //DataSet TempTravelData = clsDataBaseHelper.ExecuteDataSet(SQL);

                                //if (TempTravelData.Tables[0].Rows.Count > 0)
                                //{
                                //    DateTime dtFTDate, dtTTDate;
                                //    DateTime.TryParse("", out dtTTDate);

                                //    DataSet dsLog = clsDataBaseHelper.ExecuteDataSet("select * from active_log where isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + " and emp_id=" + item.EMPID + " and proj_id=" + projectitem.ProjectID + "");
                                //    int iCount = 0;
                                //    foreach (DataRow Item in TempTravelData.Tables[0].Rows)
                                //    {
                                //        iCount++;
                                //        dtFTDate = Convert.ToDateTime(Item["fromdate"]);

                                //        if (iCount > 1)
                                //        {
                                //            if (dtTTDate >= dtFTDate)
                                //            {
                                //                dtFTDate = dtTTDate.AddDays(1);
                                //            }

                                //        }
                                //        dtTTDate = Convert.ToDateTime(Item["todate"]);
                                //        if (dtTTDate > dtSMEDate)
                                //        {
                                //            dtTTDate = dtSMEDate;
                                //        }

                                //        if (dsLog.Tables[0].Rows.Count > 0)
                                //        {
                                //            for (int iTmp = dtFTDate.Day; iTmp <= dtTTDate.Day; iTmp++)
                                //            {
                                //                int myvalu = 0;
                                //                int.TryParse(dsLog.Tables[0].Rows[0]["day" + iTmp].ToString(), out myvalu);
                                //                TravelHours = TravelHours + myvalu;
                                //            }
                                //        }

                                //    }

                                //}                                
                                //projectitem.WH = Convert.ToInt32(dTotHrs) - TravelHours;
                                //projectitem.TH = TravelHours;
                                // dValue = Convert.ToDecimal(Math.Round(dTotHrs * dSalary, 0, MidpointRounding.AwayFromZero));
                                dValue = Math.Round(dTotHrs * dSalary, 0, MidpointRounding.AwayFromZero);
                                if (dTHours == dTotalHrs)
                                {
                                    if ((Math.Round(dTotValue + dValue, 0, MidpointRounding.AwayFromZero)) != Math.Round(dTotalHrs * dSalary, 0, MidpointRounding.AwayFromZero))
                                    {
                                        //dValue = dValue + Convert.ToInt32((Math.Round(dTotalHrs * dSalary, 0, MidpointRounding.AwayFromZero) - (dTotValue + dValue)));
                                        dValue = Math.Round(dValue + (dTotalHrs * dSalary) - (dTotValue + dValue), 0, MidpointRounding.AwayFromZero);
                                    }
                                }

                                //projectitem.Value = dValue;
                                dTotValue = dTotValue + dValue;
                            }
                        }
                        item.PerHourRate = Convert.ToDecimal(dSalary);
                        item.AdjHourRate = Convert.ToDecimal(dTHours);
                        item.OT = Convert.ToDecimal(dOTHrs);
                        item.AL = Convert.ToDecimal(dALHrs);
                        item.PaidHours = Convert.ToDecimal(dTHours + dALHrs + dPLhrs + dHDHrs);
                        //item.GrossSalary = Convert.ToDecimal(dTotValue + Math.Round( Convert.ToDouble(item.AL) * dSalary, 0, MidpointRounding.AwayFromZero) + Math.Round(dOTHrs * 2 * dSalary, 0, MidpointRounding.AwayFromZero));
                        item.GrossSalary = Convert.ToDecimal(Math.Round(dTotValue + (Convert.ToDouble(item.AL) * dSalary), 0, MidpointRounding.AwayFromZero) + Math.Round(dOTHrs * 2 * dSalary, 0, MidpointRounding.AwayFromZero));
                        item.MonthHours = Convert.ToDecimal(iWDays * 8);
                        item.LWP = Convert.ToDecimal(dNPLhrs);
                        item.Holiday = Convert.ToDecimal(dHDHrs);
                        item.PaidLeave = Convert.ToDecimal(dTotLeaveHrsPaid);


                        // these value wil use later for calculation
                        item.hdnAdjHourRate = item.AdjHourRate;
                        item.hdnLWP = item.LWP;
                        item.hdnAL = item.AL;
                        item.hdnHolidayHours = item.Holiday;
                        item.hdnPaidLeave = item.PaidLeave;


                    }

                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during FinalGetProcessTimeSheet. The query was executed :", ex.ToString(), "TempGetProcessTimeSheet", "ActivityModal", "ActivityModal", "");
            }
            return obj;

        }


        public ProcessTimeSheet FinalGetProcessTimeSheetOLD(DateTime SelectedDate, string EMPID, string Emptype)
        {

            ProcessTimeSheet obj = new ProcessTimeSheet();
            try
            {
                ProcessTimeSheet TempModal = new ProcessTimeSheet();
                TempModal = TempGetProcessTimeSheetOLD(SelectedDate, EMPID, Emptype);

                obj.iCount = TempModal.iCount;
                obj.iLock = TempModal.iLock;
                obj.iUnLock = TempModal.iUnLock;
                obj.SelectedDate = TempModal.SelectedDate;
                obj.TempEMPID = TempModal.TempEMPID;
                obj.EmployeeDetails = TempModal.EmployeeDetails;

                // Now get TimeSheet Values 

                string SQL = "";
                foreach (var item in obj.EmployeeDetails)
                {
                    item.ProjectDetails = TempModal.AllProjects;
                    SQL = @"select * from timesheet_log where emp_id=" + item.EMPID + " and isdeleted=0 and month=" + SelectedDate.Month + " and   year=" + SelectedDate.Year + "";
                    DataSet TimeSheetDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                    if (TimeSheetDataSet.Tables[0].Rows.Count > 0)
                    {

                        decimal ot_hours, adjust_hours, al_hours, pl_hours, hd_hours, paid_hours, ph_value, total_value, npl_hours, total_hours, difference_hours;
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["adjust_hours"].ToString(), out adjust_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["al_hours"].ToString(), out al_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["pl_hours"].ToString(), out pl_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["hd_hours"].ToString(), out hd_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["paid_hours"].ToString(), out paid_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["ph_value"].ToString(), out ph_value);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["total_value"].ToString(), out total_value);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["npl_hours"].ToString(), out npl_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["total_hours"].ToString(), out total_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["difference_hours"].ToString(), out difference_hours);
                        decimal.TryParse(TimeSheetDataSet.Tables[0].Rows[0]["ot_hours"].ToString(), out ot_hours);

                        item.IsDataComingFormTimeSheet = true;
                        item.Checkbox = "Yes";
                        item.Modify = Convert.ToInt32(TimeSheetDataSet.Tables[0].Rows[0]["Approved"]);
                        item.PerHourRate = ph_value;
                        item.AdjHourRate = adjust_hours;//
                        item.OT = ot_hours;
                        item.PaidHours = paid_hours;
                        item.GrossSalary = total_value;
                        item.MonthHours = total_hours;

                        // these value wil use later for calculation
                        item.hdnAdjHourRate = adjust_hours;
                        item.hdnLWP = npl_hours;
                        item.hdnAL = al_hours;//
                        item.hdnHolidayHours = hd_hours;
                        item.hdnPaidLeave = pl_hours;
                        item.AL = al_hours;

                        // Filling Work and Travel Hour
                        decimal DiffRanceHours = 0;
                        foreach (var projectitem in item.ProjectDetails)
                        {
                            SQL = @"select * from timesheetdet_log where isdeleted=0 and TimeSheetlog_id=" + TimeSheetDataSet.Tables[0].Rows[0]["ID"].ToString() + " and proj_ID=" + projectitem.ProjectID + " order by proj_id";
                            DataSet TimeSheetDataSetLog = clsDataBaseHelper.ExecuteDataSet(SQL);
                            decimal work_hours, work_value, travel_hours, travel_value, adj_hours;
                            if (TimeSheetDataSetLog.Tables[0].Rows.Count > 0)
                            {
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["work_hours"].ToString(), out work_hours);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["work_value"].ToString(), out work_value);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["travel_hours"].ToString(), out travel_hours);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["travel_value"].ToString(), out travel_value);
                                decimal.TryParse(TimeSheetDataSetLog.Tables[0].Rows[0]["adj_hours"].ToString(), out adj_hours);

                                projectitem.TH = travel_hours;
                                projectitem.WH = work_hours + adj_hours;
                                projectitem.TravelValue = travel_value;
                                projectitem.WorkValue = work_value;
                                projectitem.hdnPrAdjsutHours = adj_hours;

                                projectitem.Value = travel_value + work_value;

                                DiffRanceHours += projectitem.TH + projectitem.WH;
                            }
                        }
                        // item.DiffHourRate = (item.MonthHours - DiffRanceHours);
                        item.DiffHourRate = difference_hours;


                    }
                    else
                    {
                        // when Timehsset is not filled
                        DateTime MyDate = Convert.ToDateTime(SelectedDate);
                        DateTime dtFDate = Convert.ToDateTime(MyDate.Year + "-" + MyDate.Month + "-01");
                        DateTime dtEDate = dtFDate.AddMonths(1).AddDays(-1);
                        int iTotDays = dtEDate.Day, iOffDays = 0, lLoactionID = 0;
                        double dSalary = 0;
                        DateTime dtdate;
                        int iWDays = iTotDays;
                        string dtJoiningDate = clsDataBaseHelper.ExecuteSingleResult("select doj from master_emp where id=" + item.EMPID);
                        DateTime dtSMFDate = Convert.ToDateTime(MyDate.Year + "-" + MyDate.Month + "-01");
                        DateTime dtSMEDate = dtSMFDate.AddMonths(1).AddDays(-1);
                        int iNoOfDays = dtSMEDate.Day;
                        string FixHolidays = ClsCommon.GetFixHolidaysByEmpid(item.EMPID.ToString());
                        bool bJMonth = false, bRMonth = false;
                        string dtResignDate = clsDataBaseHelper.ExecuteSingleResult("select dor from master_emp where id=" + item.EMPID);
                        if (dtSMEDate < Convert.ToDateTime(dtJoiningDate))
                        {

                        }
                        else if (dtSMEDate.Month == Convert.ToDateTime(dtJoiningDate).Month && dtSMEDate.Year == Convert.ToDateTime(dtJoiningDate).Year)
                        {
                            iWDays = iWDays - (Convert.ToDateTime(dtJoiningDate).Day - 1);
                            bJMonth = true;
                        }
                        else if (dtSMEDate.Month == Convert.ToDateTime(dtResignDate).Month && dtSMEDate.Year == Convert.ToDateTime(dtResignDate).Year)
                        {
                            iWDays = Convert.ToDateTime(dtResignDate).Day;
                            bRMonth = true;
                        }
                        for (int i = 1; i <= iTotDays; i++)
                        {
                            dtdate = Convert.ToDateTime(i + "/" + MyDate.Month + "/" + MyDate.Year);
                            string sDay = dtdate.ToString("ddd");
                            if (sDay == "Sun" | (sDay == "Sat" && FixHolidays.Contains("Sat")))
                            {
                                if (bJMonth == true & dtdate.Day < Convert.ToDateTime(dtJoiningDate).Day)
                                {
                                }
                                else if (bRMonth == true & dtdate.Day > Convert.ToDateTime(dtResignDate).Day)
                                {
                                }
                                else
                                    iOffDays = iOffDays + 1;
                            }
                        }
                        iWDays = iWDays - iOffDays;
                        double dTotLeaveHrs = 0, dTotLeaveHrsPaid = 0, dOTHrs = 0, dALHrs = 0, dPLhrs = 0, dNPLhrs = 0, dHDHrs = 0, dHLAdjHrs = 0, dPaidHrs = 0, dAdjPercent = 0, dTotHrs = 0, dTotalHrs = 0, dTotValue = 0, dTHours = 0;
                        int dValue = 0;
                        //double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select salary from master_emp where id=" + item.EMPID), out dSalary);
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select top 1 HourlyRate from SS_EmployeeSalary where empid=" + item.EMPID.ToString() + " and isdeleted=0 and EffectiveDate <='" + dtFDate.ToString("yyyy-MMM-dd") + "' order  by effectivedate desc"), out dSalary);
                        int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select worklocationid from master_emp  where master_emp.isdeleted=0 and master_emp.id =" + item.EMPID), out lLoactionID);
                        //double.TryParse(item.dSalary, out dSalary);
                        //int.TryParse(item.lLoactionID, out lLoactionID);
                        DataSet dsHoliday = clsDataBaseHelper.ExecuteDataSet("select convert(varchar(11),HOLIDAY_DATE,103) as holiday_date,color_code,color_name from HOLIDAY where isdeleted =0 and holiday.id in(select holiday_id from map_holiday_loc where isdeleted=0 and location_id=" + lLoactionID + ") AND  HOLIDAY_DATE between '" + (bJMonth ? Convert.ToDateTime(dtJoiningDate).ToString("dd/MMM/yyyy") : dtFDate.ToString("dd/MMM/yyyy")) + "' and '" + (bRMonth ? Convert.ToDateTime(dtResignDate).ToString("dd/MMM/yyyy") : dtEDate.ToString("dd/MMM/yyyy")) + "' ORDER BY HOLIDAY_DATE");
                        int iHoliday = dsHoliday.Tables[0].Rows.Count;
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id where leave_log.emp_id=" + item.EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "'"), out dTotLeaveHrs);
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id  where leave_log.leave_id <>4 and leave_log.emp_id=" + item.EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4)  and leave_type=1 and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "'"), out dTotLeaveHrsPaid);

                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(sum(approve_hours),0) from overtime_log where emp_id=" + item.EMPID + " and isdeleted=0 and approved=1 and month=" + MyDate.Month + "  and year=" + MyDate.Year + ""), out dOTHrs);
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id where leave_log.emp_id=" + item.EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "' and master_leave.shortname='AL'"), out dALHrs);
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id where leave_log.emp_id=" + item.EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "' and master_leave.shortname<>'AL' and leave_type=1"), out dPLhrs);
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(SUM(cast(leavedet_log.hours as int)),0) from leave_log inner join leavedet_log on leavedet_log.leavelog_id=leave_log.id inner join master_leave on leavedet_log.leave_id=master_leave.id where leave_log.emp_id=" + item.EMPID + " and  leave_log.isdeleted =0 and leavedet_log.isdeleted=0 and Leave_log.approved in(1,4) and leavedet_log.date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "' and master_leave.shortname<>'AL' and leave_type=2"), out dNPLhrs);

                        //double.TryParse(item.dTotLeaveHrs, out dTotLeaveHrs);
                        //double.TryParse(item.dTotLeaveHrsPaid, out dTotLeaveHrsPaid);

                        //double.TryParse(item.dOTHrs, out dOTHrs);
                        //double.TryParse(item.dALHrs, out dALHrs);
                        //double.TryParse(item.dPLhrs, out dPLhrs);
                        //double.TryParse(item.dNPLhrs, out dNPLhrs);

                        dHDHrs = iHoliday * 8;
                        dHLAdjHrs = dPLhrs + dHDHrs;
                        dPaidHrs = (iWDays * 8) - (dALHrs + dNPLhrs);
                        dAdjPercent = dHLAdjHrs / (dPaidHrs - (dPLhrs + dHDHrs));
                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select isnull(sum(cast(day1 as int)+cast(day2 as int)+cast(day3 as int)+cast(day4 as int)+cast(day5 as int)+cast(day6 as int)+cast(day7 as int)+cast(day8 as int)+cast(day9 as int)+cast(day10 as int)+cast(day11 as int)+cast(day12 as int)+cast(day13 as int)+cast(day14 as int)+cast(day15 as int)+cast(day16 as int)+cast(day17 as int)+cast(day18 as int)+cast(day19 as int)+cast(day20 as int)+cast(day21 as int)+cast(day22 as int)+cast(day23 as int)+cast(day24 as int)+cast(day25 as int)+cast(day26 as int)+cast(day27 as int)+cast(day28 as int)+cast(day29 as int)+cast(day30 as int)+cast(day31 as int)),0) from active_log where emp_id=" + item.EMPID + " and isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + ""), out dTHours);
                        //double.TryParse(item.dTHours, out dTHours);

                        // Filling Work and Travel Hour

                        foreach (var projectitem in item.ProjectDetails)
                        {

                            double.TryParse(clsDataBaseHelper.ExecuteSingleResult("select sum(cast(day1 as int)+cast(day2 as int)+cast(day3 as int)+cast(day4 as int)+cast(day5 as int)+cast(day6 as int)+cast(day7 as int)+cast(day8 as int)+cast(day9 as int)+cast(day10 as int)+cast(day11 as int)+cast(day12 as int)+cast(day13 as int)+cast(day14 as int)+cast(day15 as int)+cast(day16 as int)+cast(day17 as int)+cast(day18 as int)+cast(day19 as int)+cast(day20 as int)+cast(day21 as int)+cast(day22 as int)+cast(day23 as int)+cast(day24 as int)+cast(day25 as int)+cast(day26 as int)+cast(day27 as int)+cast(day28 as int)+cast(day29 as int)+cast(day30 as int)+cast(day31 as int)) from active_log where emp_id=" + item.EMPID + " and isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + " and proj_id=" + projectitem.ProjectID + ""), out dTotHrs);

                            if (dTotHrs > 0)
                            {
                                dTotalHrs = dTotalHrs + dTotHrs;
                                int TravelHours = 0;
                                SQL = @"select distinct min(travelrequest_det.travel_date) as fromdate,max(travelrequest_det.travel_date) as todate 
                                            from travelrequest_det inner join travel_request on travelrequest_det.travelreq_id = travel_request.id
                                            where travel_request.isdeleted = 0 and travelrequest_det.isdeleted = 0 and travel_request.approved in(1, 4) and travel_request.emp_id = " + item.EMPID + "" +
                                        " and month(travelrequest_det.travel_date)= " + MyDate.Month + " and year(travelrequest_det.travel_date)= " + MyDate.Year + " group by travelrequest_det.travelreq_id order by min(travelrequest_det.travel_date)";
                                DataSet TempTravelData = clsDataBaseHelper.ExecuteDataSet(SQL);

                                if (TempTravelData.Tables[0].Rows.Count > 0)
                                {
                                    DateTime dtFTDate, dtTTDate;
                                    DateTime.TryParse("", out dtTTDate);

                                    DataSet dsLog = clsDataBaseHelper.ExecuteDataSet("select * from active_log where isdeleted=2 and month=" + MyDate.Month + " and year=" + MyDate.Year + " and emp_id=" + item.EMPID + " and proj_id=" + projectitem.ProjectID + "");
                                    int iCount = 0;
                                    foreach (DataRow Item in TempTravelData.Tables[0].Rows)
                                    {
                                        iCount++;
                                        dtFTDate = Convert.ToDateTime(Item["fromdate"]);

                                        if (iCount > 1)
                                        {
                                            if (dtTTDate >= dtFTDate)
                                            {
                                                dtFTDate = dtTTDate.AddDays(1);
                                            }

                                        }
                                        dtTTDate = Convert.ToDateTime(Item["todate"]);
                                        if (dtTTDate > dtSMEDate)
                                        {
                                            dtTTDate = dtSMEDate;
                                        }

                                        if (dsLog.Tables[0].Rows.Count > 0)
                                        {
                                            for (int iTmp = dtFTDate.Day; iTmp <= dtTTDate.Day; iTmp++)
                                            {
                                                int myvalu = 0;
                                                int.TryParse(dsLog.Tables[0].Rows[0]["day" + iTmp].ToString(), out myvalu);
                                                TravelHours = TravelHours + myvalu;
                                            }
                                        }

                                    }

                                }
                                projectitem.WH = Convert.ToInt32(dTotHrs) - TravelHours;
                                projectitem.TH = TravelHours;
                                dValue = Convert.ToInt32(Math.Round(dTotHrs * dSalary, 0, MidpointRounding.AwayFromZero));
                                if (dTHours == dTotalHrs)
                                {
                                    if ((Convert.ToInt32(dTotValue) + dValue) != Math.Round(dTotalHrs * dSalary, 0, MidpointRounding.AwayFromZero))
                                    {
                                        dValue = dValue + Convert.ToInt32((Math.Round(dTotalHrs * dSalary, 0, MidpointRounding.AwayFromZero) - (dTotValue + dValue)));
                                    }
                                }

                                projectitem.Value = dValue;
                                dTotValue = dTotValue + dValue;
                            }
                        }
                        item.PerHourRate = Convert.ToDecimal(dSalary);
                        item.AdjHourRate = Convert.ToDecimal(dTHours);
                        item.OT = Convert.ToDecimal(dOTHrs);
                        item.AL = Convert.ToDecimal(dALHrs);
                        item.PaidHours = Convert.ToDecimal(dTHours + dALHrs + dPLhrs + dHDHrs);
                        item.GrossSalary = Convert.ToDecimal(dTotValue + Math.Round(Convert.ToDouble(item.AL) * dSalary, 0, MidpointRounding.AwayFromZero) + Math.Round(dOTHrs * 2 * dSalary, 0, MidpointRounding.AwayFromZero));
                        item.MonthHours = Convert.ToDecimal(iWDays * 8);
                        item.LWP = Convert.ToDecimal(dNPLhrs);
                        item.Holiday = Convert.ToDecimal(dHDHrs);
                        item.PaidLeave = Convert.ToDecimal(dTotLeaveHrsPaid);


                        // these value wil use later for calculation
                        item.hdnAdjHourRate = item.AdjHourRate;
                        item.hdnLWP = item.LWP;
                        item.hdnAL = item.AL;
                        item.hdnHolidayHours = item.Holiday;
                        item.hdnPaidLeave = item.PaidLeave;


                    }

                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during FinalGetProcessTimeSheet. The query was executed :", ex.ToString(), "TempGetProcessTimeSheet", "ActivityModal", "ActivityModal", "");
            }
            return obj;

        }

        //public ProcessTimeSheet FinalizeOnTimeSheet(ProcessTimeSheet PostModal)
        //{
        //    try
        //    {
        //        ProcessTimeSheet FreshModal = new ProcessTimeSheet();
        //        FreshModal = FinalGetProcessTimeSheet(PostModal.SelectedDate, PostModal.TempEMPID);

        //        PostModal.iCount = FreshModal.iCount;
        //        PostModal.iLock = FreshModal.iLock;
        //        PostModal.iUnLock = FreshModal.iUnLock;
        //        PostModal.SelectedDate = FreshModal.SelectedDate;
        //        PostModal.TempEMPID = FreshModal.TempEMPID;

        //        for (int i = 0; i < PostModal.EmployeeDetails.Count; i++)
        //        {
        //            PostModal.EmployeeDetails[i].IsDataComingFormTimeSheet = FreshModal.EmployeeDetails[i].IsDataComingFormTimeSheet;

        //            // Checking from where data is coming 
        //            if (FreshModal.EmployeeDetails[i].IsDataComingFormTimeSheet)
        //            {

        //                PostModal.EmployeeDetails[i].EMPID = FreshModal.EmployeeDetails[i].EMPID;
        //                PostModal.EmployeeDetails[i].EMPCode = FreshModal.EmployeeDetails[i].EMPCode;
        //                PostModal.EmployeeDetails[i].EMPName = FreshModal.EmployeeDetails[i].EMPName;
        //                PostModal.EmployeeDetails[i].PerHourRate = FreshModal.EmployeeDetails[i].PerHourRate;
        //                PostModal.EmployeeDetails[i].PaidHours = FreshModal.EmployeeDetails[i].PaidHours;
        //                PostModal.EmployeeDetails[i].MonthHours = FreshModal.EmployeeDetails[i].MonthHours;
        //                PostModal.EmployeeDetails[i].OT = FreshModal.EmployeeDetails[i].OT;

        //                decimal DiffrenceHours = 0, TotalWHHours = 0, TotalTHHours = 0, TotalValue = 0;

        //                if (PostModal.EmployeeDetails[i].Checkbox == null)
        //                {
        //                    // Calucation of Distubuting Hours into Project
        //                    decimal TotalMonthHour = 0, TotalLeaveHours = 0, TotalAdjustHours = 0, PerHoursAdjustmentValue = 0;
        //                    TotalMonthHour = FreshModal.EmployeeDetails[i].MonthHours;
        //                    TotalLeaveHours = (FreshModal.EmployeeDetails[i].hdnAL + FreshModal.EmployeeDetails[i].hdnHolidayHours + FreshModal.EmployeeDetails[i].hdnPaidLeave + FreshModal.EmployeeDetails[i].hdnLWP);
        //                    TotalAdjustHours = TotalMonthHour - TotalLeaveHours;
        //                    PostModal.EmployeeDetails[i].AdjHourRate = TotalAdjustHours;

        //                    // setting Default fresh Values 
        //                    for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //                    {
        //                        PostModal.EmployeeDetails[i].ProjectDetails[row].ProjectID = FreshModal.EmployeeDetails[i].ProjectDetails[row].ProjectID;
        //                        PostModal.EmployeeDetails[i].ProjectDetails[row].ProjectName = FreshModal.EmployeeDetails[i].ProjectDetails[row].ProjectName;

        //                        PostModal.EmployeeDetails[i].ProjectDetails[row].WH = (PostModal.EmployeeDetails[i].ProjectDetails[row].WH - FreshModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours);
        //                        PostModal.EmployeeDetails[i].ProjectDetails[row].TH = FreshModal.EmployeeDetails[i].ProjectDetails[row].TH;

        //                        PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (FreshModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
        //                        PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (FreshModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);

        //                        TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
        //                        TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //                        PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);
        //                    }

        //                    DiffrenceHours = (TotalTHHours + TotalWHHours) - PostModal.EmployeeDetails[i].AdjHourRate;
        //                    PostModal.EmployeeDetails[i].DiffHourRate = DiffrenceHours;
        //                    PostModal.EmployeeDetails[i].AL = 0;
        //                    PostModal.EmployeeDetails[i].LWP = 0;
        //                    PostModal.EmployeeDetails[i].Holiday = 0;
        //                    PostModal.EmployeeDetails[i].PaidLeave = 0;

        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        PostModal.EmployeeDetails[i].hdnAdjHourRate = FreshModal.EmployeeDetails[i].hdnAdjHourRate;
        //                        PostModal.EmployeeDetails[i].hdnAL = FreshModal.EmployeeDetails[i].hdnAL;
        //                        PostModal.EmployeeDetails[i].hdnLWP = FreshModal.EmployeeDetails[i].hdnLWP;
        //                        PostModal.EmployeeDetails[i].hdnHolidayHours = FreshModal.EmployeeDetails[i].hdnHolidayHours;
        //                        PostModal.EmployeeDetails[i].hdnPaidLeave = FreshModal.EmployeeDetails[i].hdnPaidLeave;
        //                        PostModal.EmployeeDetails[i].MonthHours = FreshModal.EmployeeDetails[i].MonthHours;
        //                        try
        //                        {
        //                            for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //                            {
        //                                PostModal.EmployeeDetails[i].ProjectDetails[row].ProjectID = FreshModal.EmployeeDetails[i].ProjectDetails[row].ProjectID;
        //                                PostModal.EmployeeDetails[i].ProjectDetails[row].ProjectName = FreshModal.EmployeeDetails[i].ProjectDetails[row].ProjectName;
        //                                PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //                                TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
        //                                TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //                                PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(FreshModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);
        //                                PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = FreshModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours;

        //                                PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (FreshModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
        //                                PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (FreshModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);
        //                            }
        //                            DiffrenceHours = (TotalTHHours + TotalWHHours) - FreshModal.EmployeeDetails[i].AdjHourRate;
        //                            PostModal.EmployeeDetails[i].DiffHourRate = DiffrenceHours;
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            PostModal.isContainError = true;
        //                        }

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        PostModal.isContainError = true;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                PostModal.EmployeeDetails[i].EMPID = FreshModal.EmployeeDetails[i].EMPID;
        //                PostModal.EmployeeDetails[i].EMPCode = FreshModal.EmployeeDetails[i].EMPCode;
        //                PostModal.EmployeeDetails[i].EMPName = FreshModal.EmployeeDetails[i].EMPName;
        //                PostModal.EmployeeDetails[i].PerHourRate = FreshModal.EmployeeDetails[i].PerHourRate;
        //                PostModal.EmployeeDetails[i].PaidHours = FreshModal.EmployeeDetails[i].PaidHours;
        //                PostModal.EmployeeDetails[i].GrossSalary = FreshModal.EmployeeDetails[i].GrossSalary;
        //                PostModal.EmployeeDetails[i].MonthHours = FreshModal.EmployeeDetails[i].MonthHours;
        //                PostModal.EmployeeDetails[i].OT = FreshModal.EmployeeDetails[i].OT;

        //                decimal DiffrenceHours = 0, TotalWHHours = 0, TotalTHHours = 0, TotalValue = 0;

        //                if (PostModal.EmployeeDetails[i].Checkbox == null)
        //                {

        //                    PostModal.EmployeeDetails[i].AdjHourRate = PostModal.EmployeeDetails[i].hdnAdjHourRate;
        //                    PostModal.EmployeeDetails[i].AL = PostModal.EmployeeDetails[i].hdnAL;
        //                    PostModal.EmployeeDetails[i].LWP = PostModal.EmployeeDetails[i].hdnLWP;
        //                    PostModal.EmployeeDetails[i].Holiday = PostModal.EmployeeDetails[i].hdnHolidayHours;
        //                    PostModal.EmployeeDetails[i].PaidLeave = PostModal.EmployeeDetails[i].hdnPaidLeave;

        //                    double OTPer = 1.5;
        //                    decimal OTPerCal = (PostModal.EmployeeDetails[i].PerHourRate * Convert.ToDecimal(OTPer) * PostModal.EmployeeDetails[i].OT);
        //                    decimal TotalOTCal = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].hdnAdjHourRate) + OTPerCal;
        //                    PostModal.EmployeeDetails[i].GrossSalary = TotalOTCal;


        //                    try
        //                    {
        //                        for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //                        {
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].ProjectID = FreshModal.EmployeeDetails[i].ProjectDetails[row].ProjectID;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].ProjectName = FreshModal.EmployeeDetails[i].ProjectDetails[row].ProjectName;


        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH - PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours;
        //                            TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
        //                            TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(FreshModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = 0;

        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (FreshModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (FreshModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);
        //                        }
        //                        DiffrenceHours = (TotalTHHours + TotalWHHours) - FreshModal.EmployeeDetails[i].AdjHourRate;
        //                        PostModal.EmployeeDetails[i].DiffHourRate = DiffrenceHours;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        PostModal.isContainError = true;
        //                    }
        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        // setting Default fresh Values 
        //                        for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //                        {
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].ProjectID = FreshModal.EmployeeDetails[i].ProjectDetails[row].ProjectID;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].ProjectName = FreshModal.EmployeeDetails[i].ProjectDetails[row].ProjectName;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = FreshModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours;
        //                            if (FreshModal.EmployeeDetails[i].ProjectDetails[row].WH != 0)
        //                            {
        //                                PostModal.EmployeeDetails[i].ProjectDetails[row].WH = FreshModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //                            }
        //                            else
        //                            {
        //                                PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //                            }

        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].TH = FreshModal.EmployeeDetails[i].ProjectDetails[row].TH;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].Value = FreshModal.EmployeeDetails[i].ProjectDetails[row].Value;

        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (FreshModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (FreshModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);
        //                        }

        //                        // Calucation of Distubuting Hours into Project
        //                        decimal TotalMonthHour = 0, TotalLeaveHours = 0, TotalAdjustHours = 0, PerHoursAdjustmentValue = 0;
        //                        TotalMonthHour = FreshModal.EmployeeDetails[i].MonthHours;
        //                        TotalLeaveHours = (FreshModal.EmployeeDetails[i].AL + FreshModal.EmployeeDetails[i].Holiday + FreshModal.EmployeeDetails[i].PaidLeave + FreshModal.EmployeeDetails[i].LWP);
        //                        TotalAdjustHours = TotalMonthHour - TotalLeaveHours;
        //                        PostModal.EmployeeDetails[i].AdjHourRate = TotalAdjustHours + TotalLeaveHours;

        //                        double OTPer = 1.5;
        //                        decimal OTPerCal = (PostModal.EmployeeDetails[i].PerHourRate * Convert.ToDecimal(OTPer) * PostModal.EmployeeDetails[i].OT);
        //                        decimal TotalOTCal= (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].AdjHourRate) + OTPerCal;
        //                        PostModal.EmployeeDetails[i].GrossSalary = TotalOTCal;


        //                        decimal.TryParse((TotalLeaveHours / TotalAdjustHours).ToString(), out PerHoursAdjustmentValue);
        //                        decimal minHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderBy(x => x.WH).Select(x => x.WH).FirstOrDefault();
        //                        decimal MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();

        //                        for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //                        {
        //                            //minHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderBy(x => x.WH).Select(x => x.WH).FirstOrDefault();
        //                            //MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();

        //                            if (TotalLeaveHours > 0)
        //                            {
        //                                if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH != 0)
        //                                {
        //                                    int CalculationHour = Convert.ToInt32(Math.Round(PostModal.EmployeeDetails[i].ProjectDetails[row].WH * PerHoursAdjustmentValue, 0));
        //                                    PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH + CalculationHour;
        //                                    TotalLeaveHours = TotalLeaveHours - CalculationHour;
        //                                    PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours + CalculationHour;

        //                                    PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (FreshModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);

        //                                }
        //                            }

        //                            TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
        //                            TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);

        //                        }

        //                        // adding remaining Hours
        //                        if (TotalLeaveHours != 0)
        //                        {
        //                            MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();
        //                            for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //                            {
        //                                if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH == MaxHours)
        //                                {
        //                                    PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH + TotalLeaveHours;
        //                                    PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours + TotalLeaveHours;
        //                                    TotalWHHours += TotalLeaveHours;

        //                                    TotalLeaveHours = 0;
        //                                    PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);
        //                                    PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (FreshModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);

        //                                    break;
        //                                }
        //                            }
        //                        }
        //                        DiffrenceHours = (TotalTHHours + TotalWHHours) - PostModal.EmployeeDetails[i].AdjHourRate;
        //                        PostModal.EmployeeDetails[i].DiffHourRate = DiffrenceHours;

        //                        PostModal.EmployeeDetails[i].AL = 0;
        //                        PostModal.EmployeeDetails[i].LWP = 0;
        //                        PostModal.EmployeeDetails[i].Holiday = 0;
        //                        PostModal.EmployeeDetails[i].PaidLeave = 0;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        PostModal.isContainError = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        PostModal.isContainError = true;
        //        ClsCommon.LogError("Error during CalculationOnTimeSheet. The query was executed :", ex.ToString(), "CalculationOnTimeSheet", "ActivityModal", "ActivityModal", "");
        //    }
        //    return PostModal;

        //}


        public ProcessTimeSheet CalculationOnTimeSheet(ProcessTimeSheet PostModal)
        {
            try
            {
                for (int i = 0; i < PostModal.EmployeeDetails.Count; i++)
                {
                    try
                    {
                        decimal DiffrenceHours = 0, TotalWHHours = 0, TotalTHHours = 0;
                        decimal TotalLeaveHours = 0, TotalAdjustHours = 0;

                        if (PostModal.EmployeeDetails[i].Checkbox == null)
                        {
                            PostModal.EmployeeDetails[i].AL = PostModal.EmployeeDetails[i].hdnAL;
                            PostModal.EmployeeDetails[i].Holiday = PostModal.EmployeeDetails[i].hdnHolidayHours;
                            PostModal.EmployeeDetails[i].PaidLeave = PostModal.EmployeeDetails[i].hdnPaidLeave;
                            PostModal.EmployeeDetails[i].LWP = PostModal.EmployeeDetails[i].hdnLWP;
                        }
                        else
                        {
                            TotalLeaveHours = (PostModal.EmployeeDetails[i].AL + PostModal.EmployeeDetails[i].Holiday + PostModal.EmployeeDetails[i].PaidLeave + PostModal.EmployeeDetails[i].hdnLWP);
                            TotalAdjustHours = PostModal.EmployeeDetails[i].MonthHours - TotalLeaveHours;
                            PostModal.EmployeeDetails[i].AdjHourRate = TotalAdjustHours;
                        }
                        TotalLeaveHours = (PostModal.EmployeeDetails[i].AL + PostModal.EmployeeDetails[i].Holiday + PostModal.EmployeeDetails[i].PaidLeave + PostModal.EmployeeDetails[i].hdnLWP);
                        TotalAdjustHours = PostModal.EmployeeDetails[i].MonthHours - TotalLeaveHours;
                        PostModal.EmployeeDetails[i].AdjHourRate = TotalAdjustHours;

                        double OTPer = 1.5;
                        decimal OTPerCal = (PostModal.EmployeeDetails[i].PerHourRate * Convert.ToDecimal(OTPer) * PostModal.EmployeeDetails[i].OT);
                        decimal TotalOTCal = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].AdjHourRate) + OTPerCal;
                        PostModal.EmployeeDetails[i].GrossSalary = TotalOTCal;

                        for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
                        {
                            TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
                            TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
                            PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);
                            PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
                            PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);
                        }
                        DiffrenceHours = (TotalTHHours + TotalWHHours) - PostModal.EmployeeDetails[i].AdjHourRate;
                        PostModal.EmployeeDetails[i].DiffHourRate = DiffrenceHours;
                    }
                    catch (Exception ex)
                    {
                        PostModal.isContainError = true;
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                PostModal.isContainError = true;
                ClsCommon.LogError("Error during CalculationOnTimeSheet. The query was executed :", ex.ToString(), "CalculationOnTimeSheet", "ActivityModal", "ActivityModal", "");
            }
            return PostModal;

        }

        //public ProcessTimeSheet FinalizeOnTimeSheet(ProcessTimeSheet PostModal)
        //{
        //    try
        //    {
        //        for (int i = 0; i < PostModal.EmployeeDetails.Count; i++)
        //        {
        //            int hdnEmpId = 0;
        //            decimal lblPHValue = 0, lblAdjHours=0, lblWHrs=0, lblOT=0, lblAL=0, lblPL=0, lblHoliday=0, lblPL1=0, lblHoliday1=0, dAdjHrs=0, TotalLeaveHours=0;
        //            lblAdjHours = PostModal.EmployeeDetails[i].hdnAdjHourRate;
        //            lblWHrs= PostModal.EmployeeDetails[i].hdnAdjHourRate;
        //            lblOT = PostModal.EmployeeDetails[i].OT;
        //            lblAL = PostModal.EmployeeDetails[i].hdnAL;
        //            lblPL = PostModal.EmployeeDetails[i].hdnPaidLeave;
        //            lblHoliday = PostModal.EmployeeDetails[i].hdnHolidayHours;
        //            lblPL1 = PostModal.EmployeeDetails[i].hdnPaidLeave;
        //            lblHoliday1 = PostModal.EmployeeDetails[i].hdnHolidayHours;                   
        //            dAdjHrs  = PostModal.EmployeeDetails[i].hdnAdjHourRate;
        //            if (PostModal.EmployeeDetails[i].Checkbox == null)
        //            {
        //                lblPL = lblPL1;
        //                lblHoliday = lblHoliday1;
        //                lblAdjHours = lblWHrs;
        //                TotalLeaveHours = (PostModal.EmployeeDetails[i].hdnAL + PostModal.EmployeeDetails[i].hdnHolidayHours + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnLWP);
        //            }
        //            else
        //            {
        //                lblPL = 0;
        //                lblHoliday = 0;
        //                lblAdjHours = lblWHrs + lblPL1 + lblHoliday1;
        //                TotalLeaveHours = (PostModal.EmployeeDetails[i].AL + PostModal.EmployeeDetails[i].Holiday + PostModal.EmployeeDetails[i].PaidLeave + PostModal.EmployeeDetails[i].LWP);
        //            }
        //            decimal dHLHrs = lblPL1 + lblHoliday1;
        //            decimal dAdjPercent = dHLHrs / dAdjHrs;
        //            decimal dDBAdjHrs = PostModal.EmployeeDetails[i].ProjectDetails.Sum(c => c.hdnPrAdjsutHours); //PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours PostModal.EmployeeDetails[i].hdnPrAdjsutHours;
        //            decimal dDBDiffHrs = 0;
        //            if (dDBAdjHrs > 0)
        //            {
        //                dDBDiffHrs = dDBAdjHrs - dHLHrs;
        //            }

        //            for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //            {
        //                if (TotalLeaveHours > 0)
        //                {
        //                    if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH != 0)
        //                    {
        //                        int CalculationHour = Convert.ToInt32(Math.Round(PostModal.EmployeeDetails[i].ProjectDetails[row].WH * PerHoursAdjustmentValue, 0));
        //                        PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH + CalculationHour;
        //                        TotalLeaveHours = TotalLeaveHours - CalculationHour;
        //                        PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours + CalculationHour;

        //                    }
        //                }
        //                TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
        //                TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //                PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
        //                PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);
        //                PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);


        //            }

        //            //for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //            //{
        //            //    decimal hdnAdjHrs As HiddenField = DirectCast(gvActiveLog.Items(row).FindControl("hdnAdjHrs" & iCol & ""), HiddenField)
        //            //Dim txtProjWHr As TextBox = DirectCast(gvActiveLog.Items(row).FindControl("txtProjWHr" & iCol & ""), TextBox)
        //            //Dim txtProjTHr As TextBox = DirectCast(gvActiveLog.Items(row).FindControl("txtProjTHr" & iCol & ""), TextBox)
        //            //Dim txtProjValue As TextBox = DirectCast(gvActiveLog.Items(row).FindControl("txtVal" & iCol & ""), TextBox)

        //            //    if (TotalLeaveHours > 0)
        //            //    {
        //            //        if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH != 0)
        //            //        {
        //            //            int CalculationHour = Convert.ToInt32(Math.Round(PostModal.EmployeeDetails[i].ProjectDetails[row].WH * PerHoursAdjustmentValue, 0));
        //            //            PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH + CalculationHour;
        //            //            TotalLeaveHours = TotalLeaveHours - CalculationHour;
        //            //            PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours + CalculationHour;

        //            //        }
        //            //    }
        //            //    TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
        //            //    TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //            //    PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
        //            //    PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);
        //            //    PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);


        //            //}
        //        }

        //            for (int i = 0; i < PostModal.EmployeeDetails.Count; i++)
        //            {
        //            if (PostModal.EmployeeDetails[i].Checkbox == null)
        //            {
        //                // Calucation of Distubuting Hours into Project
        //                decimal TotalMonthHour = 0, TotalLeaveHours = 0, TotalAdjustHours = 0, PerHoursAdjustmentValue = 0;
        //                TotalMonthHour = PostModal.EmployeeDetails[i].MonthHours;
        //                TotalLeaveHours = (PostModal.EmployeeDetails[i].hdnAL + PostModal.EmployeeDetails[i].hdnHolidayHours + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnLWP);

        //                TotalAdjustHours = PostModal.EmployeeDetails[i].MonthHours - TotalLeaveHours;
        //                PostModal.EmployeeDetails[i].AdjHourRate = TotalAdjustHours;

        //                double OTPer = 1.5;
        //                decimal OTPerCal = (PostModal.EmployeeDetails[i].PerHourRate * Convert.ToDecimal(OTPer) * PostModal.EmployeeDetails[i].OT);
        //                decimal TotalOTCal = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].AdjHourRate) + OTPerCal;
        //                PostModal.EmployeeDetails[i].GrossSalary = TotalOTCal;

        //                decimal.TryParse((TotalLeaveHours / TotalAdjustHours).ToString(), out PerHoursAdjustmentValue);
        //                decimal minHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderBy(x => x.WH).Select(x => x.WH).FirstOrDefault();
        //                decimal MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();

        //                decimal TotalTHHours = 0, TotalWHHours = 0, DiffrenceHours = 0;
        //                for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //                {
        //                    if (TotalLeaveHours > 0)
        //                    {
        //                        if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH != 0)
        //                        {
        //                            int CalculationHour = Convert.ToInt32(Math.Round(PostModal.EmployeeDetails[i].ProjectDetails[row].WH * PerHoursAdjustmentValue, 0));
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH - CalculationHour;
        //                            TotalLeaveHours = TotalLeaveHours - CalculationHour;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours - CalculationHour;
        //                        }
        //                    }

        //                    TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
        //                    TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //                    PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
        //                    PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);
        //                    PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);

        //                }
        //                // adding remaining Hours
        //                if (TotalLeaveHours != 0)
        //                {
        //                    MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();
        //                    for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //                    {
        //                        if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH == MaxHours)
        //                        {
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH - TotalLeaveHours;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours - TotalLeaveHours;
        //                            TotalWHHours -= TotalLeaveHours;
        //                            TotalLeaveHours = 0;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);

        //                            break;
        //                        }
        //                    }
        //                }
        //                DiffrenceHours = (TotalTHHours + TotalWHHours) - PostModal.EmployeeDetails[i].AdjHourRate;
        //                PostModal.EmployeeDetails[i].DiffHourRate = DiffrenceHours;

        //                //PostModal.EmployeeDetails[i].AL = PostModal.EmployeeDetails[i].hdnAL;
        //                //PostModal.EmployeeDetails[i].LWP = PostModal.EmployeeDetails[i].hdnLWP;
        //                PostModal.EmployeeDetails[i].Holiday = PostModal.EmployeeDetails[i].hdnHolidayHours;
        //                PostModal.EmployeeDetails[i].PaidLeave = PostModal.EmployeeDetails[i].hdnPaidLeave;

        //            }
        //            else
        //            {
        //                // Calucation of Distubuting Hours into Project
        //                decimal TotalMonthHour = 0, TotalLeaveHours = 0, TotalAdjustHours = 0, PerHoursAdjustmentValue = 0;
        //                TotalMonthHour = PostModal.EmployeeDetails[i].MonthHours;
        //                TotalLeaveHours = (PostModal.EmployeeDetails[i].AL + PostModal.EmployeeDetails[i].Holiday + PostModal.EmployeeDetails[i].PaidLeave + PostModal.EmployeeDetails[i].LWP);
        //                TotalAdjustHours = PostModal.EmployeeDetails[i].MonthHours - TotalLeaveHours;
        //                //PostModal.EmployeeDetails[i].AdjHourRate = TotalAdjustHours + TotalLeaveHours;
        //                PostModal.EmployeeDetails[i].AdjHourRate = PostModal.EmployeeDetails[i].hdnAdjHourRate + PostModal.EmployeeDetails[i].PaidLeave + PostModal.EmployeeDetails[i].hdnHolidayHours;

        //                double OTPer = 2;
        //                decimal OTPerCal = (PostModal.EmployeeDetails[i].PerHourRate * Convert.ToDecimal(OTPer) * PostModal.EmployeeDetails[i].OT);
        //                decimal TotalOTCal = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].AdjHourRate) + OTPerCal;
        //                PostModal.EmployeeDetails[i].GrossSalary = TotalOTCal;


        //                decimal.TryParse((TotalLeaveHours / TotalAdjustHours).ToString(), out PerHoursAdjustmentValue);
        //                decimal minHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderBy(x => x.WH).Select(x => x.WH).FirstOrDefault();
        //                decimal MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();

        //                decimal TotalTHHours = 0, TotalWHHours = 0, DiffrenceHours = 0;
        //                for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //                {
        //                    if (TotalLeaveHours > 0)
        //                    {
        //                        if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH != 0)
        //                        {
        //                            int CalculationHour = Convert.ToInt32(Math.Round(PostModal.EmployeeDetails[i].ProjectDetails[row].WH * PerHoursAdjustmentValue, 0));
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH + CalculationHour;
        //                            TotalLeaveHours = TotalLeaveHours - CalculationHour;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours + CalculationHour;

        //                        }
        //                    }
        //                    TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
        //                    TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
        //                    PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
        //                    PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);
        //                    PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);


        //                }
        //                // adding remaining Hours
        //                if (TotalLeaveHours != 0)
        //                {
        //                    MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();
        //                    for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
        //                    {
        //                        if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH == MaxHours)
        //                        {
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH + TotalLeaveHours;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours + TotalLeaveHours;
        //                            TotalWHHours += TotalLeaveHours;
        //                            TotalLeaveHours = 0;
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);
        //                            PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);

        //                            break;
        //                        }
        //                    }
        //                }


        //                DiffrenceHours = (TotalTHHours + TotalWHHours) - PostModal.EmployeeDetails[i].AdjHourRate;
        //                PostModal.EmployeeDetails[i].DiffHourRate = DiffrenceHours;

        //                //PostModal.EmployeeDetails[i].AL = 0;
        //                // PostModal.EmployeeDetails[i].LWP = 0;
        //                PostModal.EmployeeDetails[i].Holiday = 0;
        //                PostModal.EmployeeDetails[i].PaidLeave = 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        PostModal.isContainError = true;
        //        ClsCommon.LogError("Error during CalculationOnTimeSheet. The query was executed :", ex.ToString(), "CalculationOnTimeSheet", "ActivityModal", "ActivityModal", "");
        //    }
        //    return PostModal;

        //}

        public ProcessTimeSheet FinalizeOnTimeSheet(ProcessTimeSheet PostModal)
        {
            try
            {
                for (int i = 0; i < PostModal.EmployeeDetails.Count; i++)
                {
                    try
                    {
                        if (PostModal.EmployeeDetails[i].Checkbox == null)
                        {

                            // Calucation of Distubuting Hours into Project
                            decimal TotalMonthHour = 0, TotalLeaveHours = 0, TotalAdjustHours = 0, PerHoursAdjustmentValue = 0, GrossSalary = 0;
                            TotalMonthHour = PostModal.EmployeeDetails[i].MonthHours;
                            // TotalLeaveHours = (PostModal.EmployeeDetails[i].hdnAL + PostModal.EmployeeDetails[i].hdnHolidayHours + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnLWP);
                            TotalLeaveHours = (PostModal.EmployeeDetails[i].hdnHolidayHours + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnLWP);
                            //if (PostModal.EmployeeDetails[i].IsDataComingFormTimeSheet)
                            //{
                            //    //TotalAdjustHours = PostModal.EmployeeDetails[i].MonthHours - TotalLeaveHours;
                            //    TotalAdjustHours = PostModal.EmployeeDetails[i].ProjectDetails.Sum(n => n.WH) + PostModal.EmployeeDetails[i].ProjectDetails.Sum(n => n.TH) - PostModal.EmployeeDetails[i].ProjectDetails.Sum(n => n.hdnPrAdjsutHours);
                            //}
                            //else
                            //{
                            //    TotalAdjustHours = PostModal.EmployeeDetails[i].hdnAdjHourRate;
                            //}
                            //// TotalAdjustHours = PostModal.EmployeeDetails[i].hdnAdjHourRate - PostModal.EmployeeDetails[i].hdnPaidLeave - PostModal.EmployeeDetails[i].hdnHolidayHours;


                            TotalAdjustHours = PostModal.EmployeeDetails[i].hdnAdjHourRate;
                            PostModal.EmployeeDetails[i].AdjHourRate = TotalAdjustHours;
                            //PostModal.EmployeeDetails[i].AdjHourRate = PostModal.EmployeeDetails[i].hdnAdjHourRate + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnHolidayHours;

                            double OTPer = 2;


                            decimal OTPerCal = Math.Round((PostModal.EmployeeDetails[i].PerHourRate * Convert.ToDecimal(OTPer) * PostModal.EmployeeDetails[i].OT), MidpointRounding.AwayFromZero);
                            // decimal TotalOTCal = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].AdjHourRate) + OTPerCal;
                            PostModal.EmployeeDetails[i].NetSalary = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].AdjHourRate + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnHolidayHours), 0, MidpointRounding.AwayFromZero);
                            //GrossSalary = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].AdjHourRate),MidpointRounding.AwayFromZero)+ OTPerCal + Math.Round(PostModal.EmployeeDetails[i].AL, MidpointRounding.AwayFromZero) ;
                            GrossSalary = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].AdjHourRate + PostModal.EmployeeDetails[i].AL + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnHolidayHours), MidpointRounding.AwayFromZero) + OTPerCal;
                            PostModal.EmployeeDetails[i].GrossSalary = GrossSalary;

                            decimal.TryParse((TotalAdjustHours > 0 ? (TotalLeaveHours / TotalAdjustHours) : 0).ToString(), out PerHoursAdjustmentValue);
                            decimal minHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderBy(x => x.WH).Select(x => x.WH).FirstOrDefault();
                            decimal MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();

                            decimal TotalTHHours = 0, TotalWHHours = 0, DiffrenceHours = 0;
                            for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
                            {
                                if (TotalLeaveHours > 0)
                                {
                                    if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH != 0)
                                    {
                                        int CalculationHour = Convert.ToInt32(Math.Round(PostModal.EmployeeDetails[i].ProjectDetails[row].WH * PerHoursAdjustmentValue, 0));
                                        // PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH - CalculationHour;
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH - PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours;
                                        //TotalLeaveHours = TotalLeaveHours - CalculationHour;
                                        TotalLeaveHours = TotalLeaveHours - PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours;
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = 0;
                                    }
                                }

                                TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
                                TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
                                PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
                                PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);
                                PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);

                            }
                            // adding remaining Hours
                            if (TotalLeaveHours != 0)
                            {
                                MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();
                                for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
                                {
                                    if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH == MaxHours)
                                    {
                                        //PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH - TotalLeaveHours;
                                        //PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours - TotalLeaveHours;
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH - PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours;
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = 0;
                                        TotalWHHours -= TotalLeaveHours;
                                        TotalLeaveHours = 0;
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);

                                        break;
                                    }
                                }
                            }
                            // DiffrenceHours = (TotalTHHours + TotalWHHours) - PostModal.EmployeeDetails[i].AdjHourRate;
                            DiffrenceHours = (PostModal.EmployeeDetails[i].ProjectDetails.Sum(n => n.WH) + PostModal.EmployeeDetails[i].ProjectDetails.Sum(n => n.TH)) - PostModal.EmployeeDetails[i].AdjHourRate;
                            PostModal.EmployeeDetails[i].DiffHourRate = DiffrenceHours;
                            // PostModal.EmployeeDetails[i].DiffHourRate = PostModal.EmployeeDetails[i].AdjHourRate + PostModal.EmployeeDetails[i].hdnAL + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnHolidayHours + PostModal.EmployeeDetails[i].hdnLWP - PostModal.EmployeeDetails[i].MonthHours;

                            //PostModal.EmployeeDetails[i].AL = PostModal.EmployeeDetails[i].hdnAL;
                            //PostModal.EmployeeDetails[i].LWP = PostModal.EmployeeDetails[i].hdnLWP;
                            PostModal.EmployeeDetails[i].Holiday = PostModal.EmployeeDetails[i].hdnHolidayHours;
                            PostModal.EmployeeDetails[i].PaidLeave = PostModal.EmployeeDetails[i].hdnPaidLeave;

                        }
                        else
                        {
                            // Calucation of Distubuting Hours into Project
                            decimal TotalMonthHour = 0, TotalLeaveHours = 0, TotalAdjustHours = 0, PerHoursAdjustmentValue = 0, GrossSalary = 0;
                            TotalMonthHour = PostModal.EmployeeDetails[i].MonthHours;
                            // TotalLeaveHours = (PostModal.EmployeeDetails[i].AL + PostModal.EmployeeDetails[i].Holiday + PostModal.EmployeeDetails[i].PaidLeave + PostModal.EmployeeDetails[i].LWP);
                            TotalLeaveHours = (PostModal.EmployeeDetails[i].Holiday + PostModal.EmployeeDetails[i].PaidLeave);
                            //TotalAdjustHours = PostModal.EmployeeDetails[i].MonthHours - TotalLeaveHours;
                            TotalAdjustHours = PostModal.EmployeeDetails[i].hdnAdjHourRate;
                            ////PostModal.EmployeeDetails[i].AdjHourRate = TotalAdjustHours + TotalLeaveHours;
                            //if (PostModal.EmployeeDetails[i].IsDataComingFormTimeSheet)
                            //{
                            //    PostModal.EmployeeDetails[i].AdjHourRate = PostModal.EmployeeDetails[i].ProjectDetails.Sum(n => n.WH) + PostModal.EmployeeDetails[i].ProjectDetails.Sum(n => n.TH) - PostModal.EmployeeDetails[i].ProjectDetails.Sum(n => n.hdnPrAdjsutHours) + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnHolidayHours;
                            //}
                            //else
                            //{
                            //    PostModal.EmployeeDetails[i].AdjHourRate = PostModal.EmployeeDetails[i].hdnAdjHourRate + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnHolidayHours;
                            //}
                            PostModal.EmployeeDetails[i].AdjHourRate = PostModal.EmployeeDetails[i].hdnAdjHourRate + PostModal.EmployeeDetails[i].hdnPaidLeave + PostModal.EmployeeDetails[i].hdnHolidayHours;

                            double OTPer = 2;
                            decimal OTPerCal = Math.Round((PostModal.EmployeeDetails[i].PerHourRate * Convert.ToDecimal(OTPer) * PostModal.EmployeeDetails[i].OT), MidpointRounding.AwayFromZero);
                            //decimal TotalOTCal = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].AdjHourRate) + OTPerCal;
                            //GrossSalary = Math.Round( PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].AdjHourRate),MidpointRounding.AwayFromZero) + OTPerCal + Math.Round((PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].AL),MidpointRounding.AwayFromZero);
                            GrossSalary = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].AdjHourRate + PostModal.EmployeeDetails[i].AL), MidpointRounding.AwayFromZero) + OTPerCal;
                            PostModal.EmployeeDetails[i].GrossSalary = GrossSalary;
                            PostModal.EmployeeDetails[i].NetSalary = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].AdjHourRate), 0, MidpointRounding.AwayFromZero);
                            decimal.TryParse((TotalAdjustHours > 0 ? (TotalLeaveHours / TotalAdjustHours) : 0).ToString(), out PerHoursAdjustmentValue);
                            decimal minHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderBy(x => x.WH).Select(x => x.WH).FirstOrDefault();
                            decimal MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();

                            decimal TotalTHHours = 0, TotalWHHours = 0, DiffrenceHours = 0;
                            for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
                            {
                                if (TotalLeaveHours > 0)
                                {
                                    if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH != 0)
                                    {
                                        int CalculationHour = Convert.ToInt32(Math.Round(PostModal.EmployeeDetails[i].ProjectDetails[row].WH * PerHoursAdjustmentValue, 0));
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH + CalculationHour;
                                        TotalLeaveHours = TotalLeaveHours - CalculationHour;
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours + CalculationHour;

                                    }
                                }
                                TotalTHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].TH;
                                TotalWHHours += PostModal.EmployeeDetails[i].ProjectDetails[row].WH;
                                PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);
                                PostModal.EmployeeDetails[i].ProjectDetails[row].TravelValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].TH);
                                PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);


                            }
                            // adding remaining Hours
                            if (TotalLeaveHours != 0)
                            {
                                MaxHours = PostModal.EmployeeDetails[i].ProjectDetails.Where(x => x.WH != 0).OrderByDescending(x => x.WH).Select(x => x.WH).FirstOrDefault();
                                for (int row = 0; row < PostModal.EmployeeDetails[i].ProjectDetails.Count; row++)
                                {
                                    if (PostModal.EmployeeDetails[i].ProjectDetails[row].WH == MaxHours)
                                    {
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].WH = PostModal.EmployeeDetails[i].ProjectDetails[row].WH + TotalLeaveHours;
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours = PostModal.EmployeeDetails[i].ProjectDetails[row].hdnPrAdjsutHours + TotalLeaveHours;
                                        TotalWHHours += TotalLeaveHours;
                                        TotalLeaveHours = 0;
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].Value = Math.Round(PostModal.EmployeeDetails[i].PerHourRate * (PostModal.EmployeeDetails[i].ProjectDetails[row].TH + PostModal.EmployeeDetails[i].ProjectDetails[row].WH), 0);
                                        PostModal.EmployeeDetails[i].ProjectDetails[row].WorkValue = (PostModal.EmployeeDetails[i].PerHourRate * PostModal.EmployeeDetails[i].ProjectDetails[row].WH);

                                        break;
                                    }
                                }
                            }

                            DiffrenceHours = (TotalTHHours + TotalWHHours) - PostModal.EmployeeDetails[i].AdjHourRate;

                            PostModal.EmployeeDetails[i].DiffHourRate = DiffrenceHours;

                            //PostModal.EmployeeDetails[i].AL = 0;
                            // PostModal.EmployeeDetails[i].LWP = 0;
                            PostModal.EmployeeDetails[i].Holiday = 0;
                            PostModal.EmployeeDetails[i].PaidLeave = 0;

                        }
                    }
                    catch (Exception ex)
                    {
                        ClsCommon.LogError("Error during FinalizeOnTimeSheet on Employeeid: " + PostModal.EmployeeDetails[i].EMPID + ". The query was executed :", ex.ToString(), "FinalizeOnTimeSheet", "ActivityModal", "ActivityModal", "");
                    }
                }
            }
            catch (Exception ex)
            {
                PostModal.isContainError = true;
                ClsCommon.LogError("Error during CalculationOnTimeSheet. The query was executed :", ex.ToString(), "CalculationOnTimeSheet", "ActivityModal", "ActivityModal", "");
            }
            return PostModal;

        }
        public bool IsMontlyLogSubmited(long EMPID, DateTime Dt)
        {
            bool IsSubmitted = false;
            string SQL = "";
            SQL = "select id from active_log where emp_id = " + EMPID + " and isdeleted = 0 and month = " + Dt.Month + " and year = " + Dt.Year + "";
            if (clsDataBaseHelper.CheckRecord(SQL) > 0)
            {
                IsSubmitted = true;
            }
            return IsSubmitted;
        }
        public List<MyTeamActiveLog> GetMyTeamActiveLogList(string Approved, int Month, int Year)
        {
            List<MyTeamActiveLog> List = new List<MyTeamActiveLog>();
            MyTeamActiveLog obj = new MyTeamActiveLog();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetActivityLogApproval(Approved, Month, Year);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new MyTeamActiveLog();
                    obj.ActiveID = Convert.ToInt64(item["ID"].ToString());
                    obj.Status = item["Status"].ToString();
                    obj.TotalActivityHours = Convert.ToInt64(item["TotalActivityHours"].ToString());
                    obj.UnApprovedComOff = Convert.ToInt64(item["UnApprovedComOff"].ToString());
                    obj.UnApprovedOT = Convert.ToInt64(item["UnApprovedOT"].ToString());
                    obj.PendingLeaveHours = Convert.ToInt64(item["PendingLeaveHours"].ToString());
                    obj.EMPID = Convert.ToInt64(item["EMP_ID"].ToString());
                    obj.Year = Convert.ToInt32(item["Year"].ToString());
                    obj.Month = Convert.ToInt32(item["Month"].ToString());
                    obj.MonthName = item["MonthName"].ToString();

                    obj.CO_OT = item["CO_OT"].ToString();
                    obj.EMPName = item["EMP_Name"].ToString();
                    obj.EMPCode = item["EMP_Code"].ToString();
                    obj.IsLock = Convert.ToInt32(item["IsLock"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMyTeamActiveLogList. The query was executed :", ex.ToString(), "fnGetActivityLogApproval", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }
        public bool GetDailyLogSetting(DateTime MyDate)
        {
            bool show = false;
            long EmpID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EmpID);

            string SQL = "select id from active_log where emp_id = " + EmpID + " and isdeleted = 0 and month = " + MyDate.Month + " and year = " + MyDate.Year + "";
            if (clsDataBaseHelper.CheckRecord(SQL) > 0)
            {
                show = true;
                if (clsDataBaseHelper.CheckRecord("select ID from active_approve where emp_id=" + EmpID + " and month=" + MyDate.Month + " and year=" + MyDate.Year + " and isdeleted=0") > 0)
                {
                    show = false;
                }
            }


            return show;
        }

        public List<LeaveSummary> GetLeaveSummary(long EMPID, int Month, int Year)
        {
            List<LeaveSummary> List = new List<LeaveSummary>();
            LeaveSummary obj = new LeaveSummary();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetLeaveSummary(EMPID, Month, Year);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new LeaveSummary();
                    obj.LeaveLogID = Convert.ToInt64(item["ID"].ToString());
                    obj.LeaveID = Convert.ToInt64(item["Leave_ID"].ToString());
                    obj.Doc_No = item["Doc_No"].ToString();
                    obj.Doc_Date = Convert.ToDateTime(item["Doc_Date"]).ToString("dd MMM yyyy");
                    obj.Date = Convert.ToDateTime(item["Date"]).ToString("dd MMM yyyy");
                    obj.Hours = item["Hours"].ToString();
                    obj.EMPName = item["EMP_Name"].ToString();
                    obj.EMPCode = item["EMP_Code"].ToString();
                    obj.LeaveName = item["Leave_Name"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.RFC = Convert.ToInt32(item["RFC"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveSummary. The query was executed :", ex.ToString(), "fnGetLeaveSummary", "ActivityModal", "ActivityModal", "");
            }
            return List;

        }
        public double GetEmpWorkingHours()
        {
            double show = 0;
            long EmpID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EmpID);

            string SQL = "select isnull((select top 1 WorkingHours from SS_EmployeeSalary where Empid=" + EmpID + " and isdeleted=0 and EffectiveDate<=CURRENT_TIMESTAMP order by id desc),0)";
            show = Convert.ToDouble(clsDataBaseHelper.ExecuteSingleResult(SQL));
            return show;
        }
        public DailyLogNonMitr GetDailyLogNonMitr(string Date, string Empids)
        {
            DailyLogNonMitr obj = new DailyLogNonMitr();
            DataSet TempModuleDataSet = Common_SPU.fnGetDailyLogNonMITR(Convert.ToDateTime(Date).Month, Convert.ToDateTime(Date).Year, Empids);
            obj.LeaveWithAttachmentPending = 0;
            obj.LeaveWithAttachmentPendingDates = "";
            obj.month = Convert.ToDateTime(Date).Month;
            obj.year = Convert.ToDateTime(Date).Year;
            obj.LstDailyLog = GetDailyLogNonMitrList(TempModuleDataSet);
            obj.LstHoliday = GetHolidayLeaveDailyLog(Empids, Date);
            //obj.LstLeaves = GetLeavesDailyLogNonMitr(TempModuleDataSet);
            return obj;

        }

        public List<DailyLogNonMitrList> GetDailyLogNonMitrList(DataSet TempModuleDataSet)
        {
            List<DailyLogNonMitrList> List = new List<DailyLogNonMitrList>();
            DailyLogNonMitrList obj = new DailyLogNonMitrList();

            try
            {

                //DataSet TempModuleDataSet = Common_SPU.fnGetDailyLogNonMITR(Convert.ToDateTime(Date).Month, Convert.ToDateTime(Date).Year,Empids);
                if (TempModuleDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {

                        obj = new DailyLogNonMitrList();
                        obj.LeaveWithAttachmentPending = Convert.ToInt32(item["LeaveWithAttachmentPending"].ToString());
                        obj.LeaveWithAttachmentPendingDates = item["LeaveWithAttachmentPendingDates"].ToString();
                        obj.DailyLogID = Convert.ToInt32(item["ID"].ToString());

                        obj.proj_id = Convert.ToInt32(item["proj_id"].ToString());
                        obj.ActivityID = Convert.ToInt64(item["ActivityID"].ToString());
                        obj.description = item["description"].ToString();
                        obj.EmpName = item["Emp"].ToString();
                        obj.FixHoliday = item["FixHoliday"].ToString();
                        obj.Doj = Convert.ToDateTime(item["doj"].ToString());
                        obj.LastWorkingDay = Convert.ToDateTime(item["lastworking_day"].ToString());
                        obj.emp_id = Convert.ToInt32(item["emp_id"].ToString());
                        obj.WorkingHours = Convert.ToInt32(item["WorkingHours"].ToString());
                        obj.year = Convert.ToInt32(item["year"].ToString());
                        obj.srno = Convert.ToInt32(item["srno"].ToString());
                        obj.month = Convert.ToInt32(item["month"].ToString());
                        obj.Total = Convert.ToInt32((!string.IsNullOrEmpty(item["Total"].ToString()) ? item["Total"] : "0"));
                        obj.Day1 = (string.IsNullOrEmpty(item["Day1"].ToString()) ? 0 : Convert.ToDouble(item["Day1"].ToString()));
                        obj.Day2 = (string.IsNullOrEmpty(item["Day2"].ToString()) ? 0 : Convert.ToDouble(item["Day2"].ToString()));
                        obj.Day3 = (string.IsNullOrEmpty(item["Day3"].ToString()) ? 0 : Convert.ToDouble(item["Day3"].ToString()));
                        obj.Day4 = (string.IsNullOrEmpty(item["Day4"].ToString()) ? 0 : Convert.ToDouble(item["Day4"].ToString()));
                        obj.Day5 = (string.IsNullOrEmpty(item["Day5"].ToString()) ? 0 : Convert.ToDouble(item["Day5"].ToString()));
                        obj.Day6 = (string.IsNullOrEmpty(item["Day6"].ToString()) ? 0 : Convert.ToDouble(item["Day6"].ToString()));
                        obj.Day7 = (string.IsNullOrEmpty(item["Day7"].ToString()) ? 0 : Convert.ToDouble(item["Day7"].ToString()));
                        obj.Day8 = (string.IsNullOrEmpty(item["Day8"].ToString()) ? 0 : Convert.ToDouble(item["Day8"].ToString()));
                        obj.Day9 = (string.IsNullOrEmpty(item["Day9"].ToString()) ? 0 : Convert.ToDouble(item["Day9"].ToString()));
                        obj.Day10 = (string.IsNullOrEmpty(item["Day10"].ToString()) ? 0 : Convert.ToDouble(item["Day10"].ToString()));
                        obj.Day11 = (string.IsNullOrEmpty(item["Day11"].ToString()) ? 0 : Convert.ToDouble(item["Day11"].ToString()));
                        obj.Day12 = (string.IsNullOrEmpty(item["Day12"].ToString()) ? 0 : Convert.ToDouble(item["Day12"].ToString()));
                        obj.Day13 = (string.IsNullOrEmpty(item["Day13"].ToString()) ? 0 : Convert.ToDouble(item["Day13"].ToString()));
                        obj.Day14 = (string.IsNullOrEmpty(item["Day14"].ToString()) ? 0 : Convert.ToDouble(item["Day14"].ToString()));
                        obj.Day15 = (string.IsNullOrEmpty(item["Day15"].ToString()) ? 0 : Convert.ToDouble(item["Day15"].ToString()));
                        obj.Day16 = (string.IsNullOrEmpty(item["Day16"].ToString()) ? 0 : Convert.ToDouble(item["Day16"].ToString()));
                        obj.Day17 = (string.IsNullOrEmpty(item["Day17"].ToString()) ? 0 : Convert.ToDouble(item["Day17"].ToString()));
                        obj.Day18 = (string.IsNullOrEmpty(item["Day18"].ToString()) ? 0 : Convert.ToDouble(item["Day18"].ToString()));
                        obj.Day19 = (string.IsNullOrEmpty(item["Day19"].ToString()) ? 0 : Convert.ToDouble(item["Day19"].ToString()));
                        obj.Day20 = (string.IsNullOrEmpty(item["Day20"].ToString()) ? 0 : Convert.ToDouble(item["Day20"].ToString()));
                        obj.Day21 = (string.IsNullOrEmpty(item["Day21"].ToString()) ? 0 : Convert.ToDouble(item["Day21"].ToString()));
                        obj.Day22 = (string.IsNullOrEmpty(item["Day22"].ToString()) ? 0 : Convert.ToDouble(item["Day22"].ToString()));
                        obj.Day23 = (string.IsNullOrEmpty(item["Day23"].ToString()) ? 0 : Convert.ToDouble(item["Day23"].ToString()));
                        obj.Day24 = (string.IsNullOrEmpty(item["Day24"].ToString()) ? 0 : Convert.ToDouble(item["Day24"].ToString()));
                        obj.Day25 = (string.IsNullOrEmpty(item["Day25"].ToString()) ? 0 : Convert.ToDouble(item["Day25"].ToString()));
                        obj.Day26 = (string.IsNullOrEmpty(item["Day26"].ToString()) ? 0 : Convert.ToDouble(item["Day26"].ToString()));
                        obj.Day27 = (string.IsNullOrEmpty(item["Day27"].ToString()) ? 0 : Convert.ToDouble(item["Day27"].ToString()));
                        obj.Day28 = (string.IsNullOrEmpty(item["Day28"].ToString()) ? 0 : Convert.ToDouble(item["Day28"].ToString()));
                        obj.Day29 = (string.IsNullOrEmpty(item["Day29"].ToString()) ? 0 : Convert.ToDouble(item["Day29"].ToString()));
                        obj.Day30 = (string.IsNullOrEmpty(item["Day30"].ToString()) ? 0 : Convert.ToDouble(item["Day30"].ToString()));
                        obj.Day31 = (string.IsNullOrEmpty(item["Day31"].ToString()) ? 0 : Convert.ToDouble(item["Day31"].ToString()));

                        List.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDailyLogNonMitrList. The query was executed :", ex.ToString(), "GetDailyLogNonMitrList", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }
        public List<HolidayDailyLog> GetHolidayDailyLogNonMitr(DataSet TempModuleDataSet)
        {
            string SQL = "";
            List<HolidayDailyLog> List = new List<HolidayDailyLog>();
            HolidayDailyLog obj = new HolidayDailyLog();
            try
            {
                foreach (DataRow item in TempModuleDataSet.Tables[1].Rows)
                {
                    obj = new HolidayDailyLog();

                    obj.Date = Convert.ToDateTime(item["holiday_date"]).ToString("dd/MM/yyyy");
                    obj.Color = "#e72d35";
                    obj.HolidayType = "Holiday";
                    obj.Description = item["Holiday_name"] + " " + item["Remarks"];
                    obj.ClassName = "holiday";
                    List.Add(obj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetHolidayDailyLog. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }
            return List;

        }
        public List<HolidayDailyLog> GetLeavesDailyLogNonMitr(DataSet TempModuleDataSet1)
        {
            string SQL = "";
            List<HolidayDailyLog> List = new List<HolidayDailyLog>();
            HolidayDailyLog obj = new HolidayDailyLog();
            try
            {
                foreach (DataRow item in TempModuleDataSet1.Tables[2].Rows)
                {
                    obj = new HolidayDailyLog();

                    obj.Date = Convert.ToDateTime(item["date"]).ToString("dd/MM/yyyy");
                    if (Convert.ToInt32(item["approved"].ToString()) == 0 || Convert.ToInt32(item["approved"].ToString()) == 3)
                    {
                        obj.HolidayType = item["leave_name"].ToString() + " Pending";
                        obj.Name = obj.HolidayType;
                        obj.Color = "#1d46dc";
                        obj.Description = "Leave pending for Approval";
                        obj.ClassName = "pendingleave";

                    }
                    else if (Convert.ToInt32(item["approved"].ToString()) == 1 || Convert.ToInt32(item["approved"].ToString()) == 4)
                    {
                        obj.HolidayType = item["leave_name"].ToString() + " Approved";
                        obj.Name = obj.HolidayType;
                        obj.Color = "#1a9e0a";
                        obj.Description = "Leave Approved";
                        obj.ClassName = "approvedleave";

                    }
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetHolidayDailyLog. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }
            return List;

        }
        public List<HolidayDailyLogNonMitr> GetHolidayLeaveDailyLog(string Empids, string LeaveMonth)
        {
            List<HolidayDailyLogNonMitr> List = new List<HolidayDailyLogNonMitr>();
            HolidayDailyLogNonMitr obj = new HolidayDailyLogNonMitr();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetHolidayLeavebyEmpids(Empids, LeaveMonth);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new HolidayDailyLogNonMitr();
                    obj.HolidayType = item["Doctype"].ToString();
                    obj.Name = item["leave_name"].ToString();
                    obj.Color = item["Color"].ToString();
                    obj.Description = item["leave_name"].ToString();
                    obj.ClassName = item["Class"].ToString();
                    obj.Empid = Convert.ToInt64(item["emp_id"].ToString());
                    obj.Date = Convert.ToDateTime(item["hdate"]).ToString("dd/MM/yyyy");
                    obj.DayNo = Convert.ToDateTime(item["hdate"]).ToString("dd");
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetHolidayLeaveDailyLog. The query was executed :", ex.ToString(), "GetHolidayLeaveDailyLog", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }
        public MonthlyLogNonMitr GetMonthlyLogNonMitr(string Date, string Empids, string Type = "")
        {
            MonthlyLogNonMitr obj = new MonthlyLogNonMitr();
            DataSet TempModuleDataSet = Common_SPU.fnGetActivityLogNonMITR(Convert.ToDateTime(Date).Month, Convert.ToDateTime(Date).Year, Empids, Type);
            obj.month = Convert.ToDateTime(Date).Month;
            obj.year = Convert.ToDateTime(Date).Year;
            obj.LstMonthlyLog = GetMonthlyLogNonMitrList(TempModuleDataSet);
            obj.LstHoursSummary = GetMonthlyLogHourSumm(TempModuleDataSet);
            obj.LstHoliday = GetHolidayLeaveDailyLog(Empids, Date);
            return obj;
        }
        public List<MonthlyLogHoursSummary> GetMonthlyLogHourSumm(DataSet TempModuleDataSet)
        {
            List<MonthlyLogHoursSummary> List = new List<MonthlyLogHoursSummary>();
            MonthlyLogHoursSummary obj = new MonthlyLogHoursSummary();
            try
            {
                foreach (DataRow item in TempModuleDataSet.Tables[1].Rows)
                {
                    obj = new MonthlyLogHoursSummary();
                    obj.Empid = Convert.ToInt32(item["Empid"].ToString());
                    obj.WorkingHours = Convert.ToInt32(item["WorkingHours"].ToString());
                    obj.MWHrs = Convert.ToInt32(item["MWHrs"].ToString());
                    obj.TLAHrs = Convert.ToInt32(item["TLAHrs"].ToString());
                    obj.TLPHrs = Convert.ToInt32(item["TLPHrs"].ToString());
                    obj.TCAHrs = Convert.ToInt32(item["TCAHrs"].ToString());
                    obj.TCHrs = Convert.ToInt32(item["TCHrs"].ToString());
                    obj.EMailCC = string.IsNullOrEmpty(item["EMailCC"].ToString()) ? "" : item["EMailCC"].ToString();
                    obj.EmpType = string.IsNullOrEmpty(item["EmpType"].ToString()) ? "" : item["EmpType"].ToString();
                    obj.IsCompOff = Convert.ToInt32(item["IsCompOFF"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMonthlyLogHourSumm. The query was executed :", ex.ToString(), "GetMonthlyLogHourSumm", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }
        public List<MonthlyLogNonMitrList> GetMonthlyLogNonMitrList(DataSet TempModuleDataSet)
        {
            List<MonthlyLogNonMitrList> List = new List<MonthlyLogNonMitrList>();
            MonthlyLogNonMitrList obj = new MonthlyLogNonMitrList();

            try
            {
                if (TempModuleDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {

                        obj = new MonthlyLogNonMitrList();
                        obj.DailyLogID = Convert.ToInt32(item["tid"].ToString());
                        obj.proj_id = Convert.ToInt32(item["proj_id"].ToString());
                        obj.ActivityID = Convert.ToInt64(item["ActivityID"].ToString());
                        obj.description = item["Description"].ToString();
                        obj.EmpName = Convert.ToString(!string.IsNullOrEmpty(item["EmpName"].ToString()) ? item["EmpName"] : "");
                        obj.FixHoliday = Convert.ToString(!string.IsNullOrEmpty(item["FixHoliday"].ToString()) ? item["FixHoliday"] : "");
                        obj.year = Convert.ToInt32((!string.IsNullOrEmpty(item["year"].ToString()) ? item["year"] : "0"));
                        obj.month = Convert.ToInt32((!string.IsNullOrEmpty(item["month"].ToString()) ? item["month"] : "0"));
                        obj.Doctype = item["doc_type"].ToString();
                        obj.Doj = Convert.ToDateTime((!string.IsNullOrEmpty(item["doj"].ToString()) ? item["doj"] : "1900-01-01"));
                        obj.LastWorkingDay = Convert.ToDateTime(item["lastworking_day"].ToString());
                        obj.proj_name = item["proj_name"].ToString();
                        obj.Activity = item["Activity"].ToString();
                        obj.ResubmitRemarks = item["ResubmitRemarks"].ToString();
                        obj.emp_id = Convert.ToInt32((!string.IsNullOrEmpty(item["emp_id"].ToString()) ? item["emp_id"] : "0"));
                        obj.srno = Convert.ToInt32((!string.IsNullOrEmpty(item["srno"].ToString()) ? item["srno"] : "0"));
                        obj.Total = Convert.ToInt32((!string.IsNullOrEmpty(item["total"].ToString()) ? item["total"] : "0"));
                        obj.Day1 = (string.IsNullOrEmpty(item["Day1"].ToString()) ? 0 : Convert.ToDouble(item["Day1"].ToString()));
                        obj.Day2 = (string.IsNullOrEmpty(item["Day2"].ToString()) ? 0 : Convert.ToDouble(item["Day2"].ToString()));
                        obj.Day3 = (string.IsNullOrEmpty(item["Day3"].ToString()) ? 0 : Convert.ToDouble(item["Day3"].ToString()));
                        obj.Day4 = (string.IsNullOrEmpty(item["Day4"].ToString()) ? 0 : Convert.ToDouble(item["Day4"].ToString()));
                        obj.Day5 = (string.IsNullOrEmpty(item["Day5"].ToString()) ? 0 : Convert.ToDouble(item["Day5"].ToString()));
                        obj.Day6 = (string.IsNullOrEmpty(item["Day6"].ToString()) ? 0 : Convert.ToDouble(item["Day6"].ToString()));
                        obj.Day7 = (string.IsNullOrEmpty(item["Day7"].ToString()) ? 0 : Convert.ToDouble(item["Day7"].ToString()));
                        obj.Day8 = (string.IsNullOrEmpty(item["Day8"].ToString()) ? 0 : Convert.ToDouble(item["Day8"].ToString()));
                        obj.Day9 = (string.IsNullOrEmpty(item["Day9"].ToString()) ? 0 : Convert.ToDouble(item["Day9"].ToString()));
                        obj.Day10 = (string.IsNullOrEmpty(item["Day10"].ToString()) ? 0 : Convert.ToDouble(item["Day10"].ToString()));
                        obj.Day11 = (string.IsNullOrEmpty(item["Day11"].ToString()) ? 0 : Convert.ToDouble(item["Day11"].ToString()));
                        obj.Day12 = (string.IsNullOrEmpty(item["Day12"].ToString()) ? 0 : Convert.ToDouble(item["Day12"].ToString()));
                        obj.Day13 = (string.IsNullOrEmpty(item["Day13"].ToString()) ? 0 : Convert.ToDouble(item["Day13"].ToString()));
                        obj.Day14 = (string.IsNullOrEmpty(item["Day14"].ToString()) ? 0 : Convert.ToDouble(item["Day14"].ToString()));
                        obj.Day15 = (string.IsNullOrEmpty(item["Day15"].ToString()) ? 0 : Convert.ToDouble(item["Day15"].ToString()));
                        obj.Day16 = (string.IsNullOrEmpty(item["Day16"].ToString()) ? 0 : Convert.ToDouble(item["Day16"].ToString()));
                        obj.Day17 = (string.IsNullOrEmpty(item["Day17"].ToString()) ? 0 : Convert.ToDouble(item["Day17"].ToString()));
                        obj.Day18 = (string.IsNullOrEmpty(item["Day18"].ToString()) ? 0 : Convert.ToDouble(item["Day18"].ToString()));
                        obj.Day19 = (string.IsNullOrEmpty(item["Day19"].ToString()) ? 0 : Convert.ToDouble(item["Day19"].ToString()));
                        obj.Day20 = (string.IsNullOrEmpty(item["Day20"].ToString()) ? 0 : Convert.ToDouble(item["Day20"].ToString()));
                        obj.Day21 = (string.IsNullOrEmpty(item["Day21"].ToString()) ? 0 : Convert.ToDouble(item["Day21"].ToString()));
                        obj.Day22 = (string.IsNullOrEmpty(item["Day22"].ToString()) ? 0 : Convert.ToDouble(item["Day22"].ToString()));
                        obj.Day23 = (string.IsNullOrEmpty(item["Day23"].ToString()) ? 0 : Convert.ToDouble(item["Day23"].ToString()));
                        obj.Day24 = (string.IsNullOrEmpty(item["Day24"].ToString()) ? 0 : Convert.ToDouble(item["Day24"].ToString()));
                        obj.Day25 = (string.IsNullOrEmpty(item["Day25"].ToString()) ? 0 : Convert.ToDouble(item["Day25"].ToString()));
                        obj.Day26 = (string.IsNullOrEmpty(item["Day26"].ToString()) ? 0 : Convert.ToDouble(item["Day26"].ToString()));
                        obj.Day27 = (string.IsNullOrEmpty(item["Day27"].ToString()) ? 0 : Convert.ToDouble(item["Day27"].ToString()));
                        obj.Day28 = (string.IsNullOrEmpty(item["Day28"].ToString()) ? 0 : Convert.ToDouble(item["Day28"].ToString()));
                        obj.Day29 = (string.IsNullOrEmpty(item["Day29"].ToString()) ? 0 : Convert.ToDouble(item["Day29"].ToString()));
                        obj.Day30 = (string.IsNullOrEmpty(item["Day30"].ToString()) ? 0 : Convert.ToDouble(item["Day30"].ToString()));
                        obj.Day31 = (string.IsNullOrEmpty(item["Day31"].ToString()) ? 0 : Convert.ToDouble(item["Day31"].ToString()));

                        List.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDailyLogNonMitrList. The query was executed :", ex.ToString(), "GetDailyLogNonMitrList", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }
        public List<MyTeamActiveLogListNonMitr> GetMyTeamActiveLogListNonMitr(string Approved, int Month, int Year)
        {
            List<MyTeamActiveLogListNonMitr> List = new List<MyTeamActiveLogListNonMitr>();
            MyTeamActiveLogListNonMitr obj = new MyTeamActiveLogListNonMitr();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetActivityLogApprovalNonMITR(Approved, Month, Year);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new MyTeamActiveLogListNonMitr();
                    obj.Status = item["Status"].ToString();
                    obj.EMPID = Convert.ToInt64(item["EMP_ID"].ToString());
                    obj.Year = Convert.ToInt32(item["Year"].ToString());
                    obj.Month = Convert.ToInt32(item["Month"].ToString());
                    obj.MonthName = item["MonthName"].ToString();
                    obj.TeamCount = Convert.ToInt32(item["TeamCount"].ToString());
                    obj.UnApprovedOT = Convert.ToInt32(item["UnApprovedOT"].ToString());
                    obj.UnApprovedComOff = Convert.ToInt32(item["UnApprovedComOff"].ToString());
                    obj.EMPName = item["emp_name"].ToString();
                    obj.EMPCode = item["emp_code"].ToString();
                    obj.Teamids = item["Teamids"].ToString();

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMyTeamActiveLogListNonMitr. The query was executed :", ex.ToString(), "fnGetActivityLogApproval", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }
        public List<CompensatoryOffList> GetCompensatoryOffListNonMitr(string EMPIDS, int iMonth, int iYear, string approved)
        {
            List<CompensatoryOffList> List = new List<CompensatoryOffList>();
            CompensatoryOffList obj = new CompensatoryOffList();

            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetCompensatoryOffNonMitr(EMPIDS, iMonth, iYear, approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new CompensatoryOffList();
                    obj.CompensatoryOffID = Convert.ToInt64(item["ID"].ToString());
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.EMPID = Convert.ToInt32(item["EMP_ID"].ToString());
                    obj.Emp_Code = item["Emp_Code"].ToString();
                    obj.Emp_name = item["Emp_name"].ToString();
                    obj.proj_name = item["proj_name"].ToString();
                    obj.Reason = item["Reason"].ToString();
                    obj.description = item["description"].ToString();
                    obj.Date = item["Date"].ToString();
                    obj.HRS = (string.IsNullOrEmpty(item["HRS"].ToString()) ? 0 : Convert.ToDecimal(item["HRS"].ToString()));
                    obj.hours = (string.IsNullOrEmpty(item["hours"].ToString()) ? 0 : Convert.ToDecimal(item["hours"].ToString()));
                    obj.Approve_hours = (string.IsNullOrEmpty(item["Approve_hours"].ToString()) ? 0 : Convert.ToDecimal(item["Approve_hours"].ToString()));
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetCompensatoryOffListNonMitr. The query was executed :", ex.ToString(), "GetCompensatoryOffListNonMitr", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }
        public ConsolidateReportEntry GetConsolidateSalaryAllocationEntry(string EmpType, string Date)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            ConsolidateReportEntry result = new ConsolidateReportEntry();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EmpType", dbType: DbType.String, value: EmpType, direction: ParameterDirection.Input);
                    param.Add("@MonthDate", dbType: DbType.DateTime, value: Convert.ToDateTime(Date), direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.String, value: LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetConsolidatedSalaryAllocationtList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<ConsolidateReportEntry>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new ConsolidateReportEntry();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Emplist = reader.Read<ConsolidateEntryEmp>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Component = reader.Read<ConsolidateEntryComponent>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetConsolidateSalaryAllocationEntry. The query was executed :", ex.ToString(), "GetConsolidateSalaryAllocationEntry", "ActivityModal", "ActivityModal", "");
            }
            return result;
        }
        public PostResponse fnSetConsolidatedSalaryAllocationEntry(int Month, int Year, long Empid, string ComponentValuesFCRA, string ComponentValuesNFCRA)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetConsolidatedSalaryAllocation", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Month", SqlDbType.Int).Value = Month;
                        command.Parameters.Add("@Year", SqlDbType.Int).Value = Year;
                        command.Parameters.Add("@Empid", SqlDbType.BigInt).Value = Empid;
                        command.Parameters.Add("@SqlQryFCRA", SqlDbType.VarChar).Value = ComponentValuesFCRA;
                        command.Parameters.Add("@SqlQryNFCRA", SqlDbType.VarChar).Value = ComponentValuesNFCRA;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = LoginID;
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
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }
        public ConsolidateSalaryReport GetConsolidatedSalaryReport(string EmpType, string FromDate, string ToDate)
        {
            ConsolidateSalaryReport result = new ConsolidateSalaryReport();
            List<ConsolidateSalaryEmployee> Employeelist = new List<ConsolidateSalaryEmployee>();
            List<ConsolidateSalaryComponent> ComponentList = new List<ConsolidateSalaryComponent>();
            try
            {
                DataSet grievancesModuleDataSet = Common_SPU.fnGetConsolidatedSalaryReport(EmpType, FromDate, ToDate);

                try
                {
                    foreach (DataRow item in grievancesModuleDataSet.Tables[0].Rows)
                    {
                        ConsolidateSalaryEmployee obj = new ConsolidateSalaryEmployee();
                        obj.Empid = Convert.ToInt64(item["Empid"]);
                        obj.emp_name = Convert.ToString(item["emp_name"]);
                        obj.emp_code = Convert.ToString(item["emp_code"]);
                        obj.EmploymentTerm = Convert.ToString(item["EmploymentTerm"]);
                        obj.Location = Convert.ToString(item["Location"]);
                        obj.HourlyRate = Convert.ToString(item["HourlyRate"]);
                        obj.Salary = Convert.ToString(item["Salary"]);
                        obj.ProjectType = Convert.ToString(item["ProjectType"]);
                        obj.ALAmount = Convert.ToString(item["ALAmount"]);
                        obj.PFAmountC3 = Convert.ToString(item["PFAmountC3"]);
                        obj.FixedAmount = Convert.ToString(item["FixedAmount"]);
                        obj.ALPaid = Convert.ToString(item["ALPaid"]);
                        obj.OTAmount = Convert.ToString(item["OTAmount"]);
                        obj.Salary_FCRA = Convert.ToString(item["Salary_FCRA"]);
                        obj.AL_FCRA = Convert.ToString(item["AL_FCRA"]);
                        obj.PF_FCRA = Convert.ToString(item["PF_FCRA"]);
                        obj.PF_FCRA_Entry = Convert.ToString(item["PF_FCRA_Entry"]);
                        obj.FB_FCRA = Convert.ToString(item["FB_FCRA"]);
                        obj.OT_FCRA = Convert.ToString(item["OT_FCRA"]);
                        obj.ALPaid_FCRA = Convert.ToString(item["ALPaid_FCRA"]);

                        obj.Salary_NONFCRA = Convert.ToString(item["Salary_NONFCRA"]);
                        obj.AL_NONFCRA = Convert.ToString(item["AL_NONFCRA"]);
                        obj.PF_NONFCRA = Convert.ToString(item["PF_NONFCRA"]);
                        obj.PF_NONFCRA_Entry = Convert.ToString(item["PF_NONFCRA_Entry"]);
                        obj.FB_NONFCRA = Convert.ToString(item["FB_NONFCRA"]);
                        obj.OT_NONFCRA = Convert.ToString(item["OT_NONFCRA"]);
                        obj.ALPaid_NONFCRA = Convert.ToString(item["ALPaid_NONFCRA"]);

                        Employeelist.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                try
                {
                    foreach (DataRow item in grievancesModuleDataSet.Tables[1].Rows)
                    {
                        ConsolidateSalaryComponent obj = new ConsolidateSalaryComponent();
                        obj.Empid = Convert.ToInt64(item["Empid"]);
                        obj.Componentid = Convert.ToInt64(item["Componentid"]);
                        obj.Component = Convert.ToString(item["Component"]);
                        obj.Amt = float.Parse(Convert.ToString(item["Amt"]));
                        obj.ProjectType = Convert.ToString(item["ProjectType"]);
                        obj.AmtFCRA = float.Parse(Convert.ToString(item["AmtFCRA"]));
                        obj.AmtNFCRA = float.Parse(Convert.ToString(item["AmtNFCRA"]));
                        obj.Doctype = Convert.ToString(item["Doctype"]);


                        ComponentList.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                result.Emplist = Employeelist;
                result.Component = ComponentList;
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetConsolidateSalaryAllocationEntry. The query was executed :", ex.ToString(), "GetConsolidateSalaryAllocationEntry", "ActivityModal", "ActivityModal", "");
            }
            return result;
        }

        public ConsolidateSalaryReport GetALAccrual_TakeReport(string Empid, string EmpType, string FromDate, string ToDate)
        {
            ConsolidateSalaryReport result = new ConsolidateSalaryReport();
            List<ConsolidateSalaryEmployee> Employeelist = new List<ConsolidateSalaryEmployee>();
            List<ConsolidateSalaryComponent> ComponentList = new List<ConsolidateSalaryComponent>();
            try
            {
                DataSet grievancesModuleDataSet = Common_SPU.fnGetGetALAccrual_TakeReport(Empid, EmpType, FromDate, ToDate);

                try
                {
                    foreach (DataRow item in grievancesModuleDataSet.Tables[0].Rows)
                    {
                        ConsolidateSalaryEmployee obj = new ConsolidateSalaryEmployee();
                        obj.FromDate = Convert.ToString(item["FromDate"]);
                        obj.ToDate = Convert.ToString(item["ToDate"]);
                        obj.Type = Convert.ToString(item["Type"]);
                        obj.Empid = Convert.ToInt64(item["Empid"]);
                        obj.emp_code = Convert.ToString(item["emp_code"]);
                        obj.emp_name = Convert.ToString(item["emp_name"]);
                        obj.EmploymentTerm = Convert.ToString(item["EmploymentTerm"]);
                        obj.Subcategory = Convert.ToString(item["Subcategory"]);
                        obj.HourlyRate = Convert.ToString(item["HourlyRate"]);
                        obj.AnnualLeaveAccuralHrs = Convert.ToString(item["AnnualLeaveAccuralHrs"]);
                        obj.ALAmount = Convert.ToString(item["AnnualLeaveAccuralAmount"]);
                        obj.AnnualLeaveTakenHrs = Convert.ToString(item["AnnualLeaveTakenHrs"]);
                        obj.ALPaid = Convert.ToString(item["AnnualLeaveTakenAmount"]);
                        Employeelist.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //try
                //{
                //    foreach (DataRow item in grievancesModuleDataSet.Tables[1].Rows)
                //    {
                //        ConsolidateSalaryComponent obj = new ConsolidateSalaryComponent();
                //        obj.Empid = Convert.ToInt64(item["Empid"]);
                //        obj.Componentid = Convert.ToInt64(item["Componentid"]);
                //        obj.Component = Convert.ToString(item["Component"]);
                //        obj.Amt = float.Parse(Convert.ToString(item["Amt"]));
                //        obj.ProjectType = Convert.ToString(item["ProjectType"]);
                //        obj.AmtFCRA = float.Parse(Convert.ToString(item["AmtFCRA"]));
                //        obj.AmtNFCRA = float.Parse(Convert.ToString(item["AmtNFCRA"]));
                //        obj.Doctype = Convert.ToString(item["Doctype"]);


                //        ComponentList.Add(obj);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                result.Emplist = Employeelist;
                result.Component = ComponentList;
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetConsolidateSalaryAllocationEntry. The query was executed :", ex.ToString(), "GetConsolidateSalaryAllocationEntry", "ActivityModal", "ActivityModal", "");
            }
            return result;
        }
        public List<LeaveReport> GetEmployeeLeaveReportList(GetResponse modal)
        {
            List<LeaveReport> result = new List<LeaveReport>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@empid", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@month", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@year", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLeaveStatusByEMPID", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<LeaveReport>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeLeaveReportList. The query was executed :", ex.ToString(), "spu_GetLeaveStatusByEMPID()", "ActivityModal", "ActivityModal", "");

            }
            return result;
        }


    }
}