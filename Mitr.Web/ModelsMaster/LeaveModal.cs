using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMaster
{
    public class LeaveModal : ILeaveHelper
    {
        public LeaveDashboard GetLeaveDashboard(long EMPID)
        {
            LeaveDashboard obj = new LeaveDashboard();

            obj.ApplyLeaveList = GetApplyLeaveDashboardList(EMPID);
            obj.LeaveBalanceDetails = GetLeaveBalanceList(EMPID, DateTime.Now.Month, DateTime.Now.Year);
            return obj;
        }

        public LeaveDetails GetLeaveDetails(long EMPID, long LeaveLogID, DateTime dDate)
        {
            LeaveDetails obj = new LeaveDetails();

            obj.LeaveDetails_Tran = GetLeaveLog_Tran(LeaveLogID);
            //obj.LeaveBalanceDetails = GetLeaveBalanceList(EMPID, dDate.Month, dDate.Year);
            obj.LeaveAttachment = ClsCommon.GetAttachmentList(0, LeaveLogID.ToString(), "ApplyLeave");
            return obj;
        }




        public List<ApplyLeaveList> GetApplyLeaveDashboardList(long EMPID)
        {
            List<ApplyLeaveList> List = new List<ApplyLeaveList>();
            ApplyLeaveList obj = new ApplyLeaveList();
            string SQL = "";
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetLeaveLog_Dashboard(EMPID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ApplyLeaveList();
                    obj.EMPID = Convert.ToInt64(item["Emp_ID"]);
                    obj.LeaveLogID = Convert.ToInt64(item["ID"]);
                    obj.DocNo = item["Doc_No"].ToString();
                    obj.RequestDate = Convert.ToDateTime(item["Request_date"]).ToString("dd MMM yyyy");
                    obj.StartDate = Convert.ToDateTime(item["start_date"]).ToString("dd MMM");
                    obj.EndDate = Convert.ToDateTime(item["End_Date"]).ToString("dd MMM yyyy");
                    obj.LeaveHours = Convert.ToInt32(item["Leave_Hours"]);
                    obj.hoursDayWise = item["hoursDayWise"].ToString();
                    obj.Remarks = item["Remarks"].ToString();
                    obj.EmergenContactName = item["EmergenContactName"].ToString();
                    obj.EmergenContactno = item["EmergenContact_no"].ToString();
                    obj.LeaveID = Convert.ToInt32(item["Leave_ID"]);
                    obj.Approved = Convert.ToInt32(item["Approved"]);
                    obj.LeaveName = item["Leave_Name"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.AttachmentRequired = Convert.ToInt32(item["AttachmentRequired"]);
                    obj.RFC = Convert.ToInt32(item["RFC"]);
                    obj.RFCStatus = Convert.ToInt32(item["RFCStatus"]);
                    obj.RFCRemarks = item["RFCRemarks"].ToString();
                    obj.IsDailyLogApproved = item["IsDailyLogApproved"].ToString();
                    obj.isED = item["isED"].ToString() == "Y" ? true : false;
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetApplyLeaveList. The query was executed :", ex.ToString(), SQL, "LeaveModal", "LeaveModal", "");
            }
            return List;

        }

        public List<LeaveBalanceDetails> GetLeaveBalanceList(long IEMPID, int iMonth, int iYear)
        {
            List<LeaveBalanceDetails> List = new List<LeaveBalanceDetails>();
            LeaveBalanceDetails obj = new LeaveBalanceDetails();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetLeaveStatus(IEMPID, iMonth, iYear);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new LeaveBalanceDetails();
                    obj.LeaveBalanceDetailsID = Convert.ToInt32(item["ID"]);
                    obj.LeaveType = item["Leave_Type"].ToString();
                    obj.Opening = Convert.ToInt32(item["Opening"]);
                    obj.MonthlyAccured = Convert.ToInt32(item["Monthly_accrred"]);
                    obj.Availed = Convert.ToInt32(item["Availed"]);
                    obj.Balance = Convert.ToInt32(item["Balance"]);
                    obj.PendingLeave = Convert.ToInt32(item["PendingLeave"]);
                    obj.BInGraph = item["BInGraph"].ToString();
                   
                    obj.BInHours = item["BInHours"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveBalanceList. The query was executed :", ex.ToString(), "spu_GetLeaveStatus", "LeaveModal", "LeaveModal", "");
            }
            return List;

        }



        public List<LeaveLogDetail_Tran> GetLeaveLog_Tran(long LeaveLogID)
        {
            List<LeaveLogDetail_Tran> List = new List<LeaveLogDetail_Tran>();
            LeaveLogDetail_Tran obj = new LeaveLogDetail_Tran();
            string SQL = "";
            //Double TotalHours = 0;
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetLeavedet_Log(LeaveLogID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new LeaveLogDetail_Tran();
                    obj.Approved = Convert.ToInt32(item["Approved"]);
                    obj.ID = Convert.ToInt32(item["ID"]);
                    obj.LeaveLogID = Convert.ToInt32(item["LeaveLog_ID"]);
                    obj.LeaveID = Convert.ToInt32(item["Leave_ID"]);
                    obj.DocNo = item["Doc_No"].ToString();
                    obj.DocDate = Convert.ToDateTime(item["Doc_Date"]).ToString("dd MMM yyyy");
                    obj.ExpectedDeliveryDate = (Convert.ToDateTime(item["ExpectedDeliveryDate"]).Year > 1900 ? Convert.ToDateTime(item["ExpectedDeliveryDate"]).ToString("dd-MMM-yyyy") : "");
                    obj.Date = Convert.ToDateTime(item["Date"]).ToString("dd MMM yyyy");
                    obj.LeaveType = item["LeaveType"].ToString();
                    obj.Hours = Convert.ToDecimal(item["Hours"]);
                    obj.emergenContact_no = item["emergenContact_no"].ToString();
                    obj.EmergenContactName = item["EmergenContactName"].ToString();
                    obj.EmergenContactRelation = item["emergenContactRelation"].ToString();                    
                    obj.EMPID = Convert.ToInt32(item["EMP_ID"]);
                    obj.AttachmentID = Convert.ToInt32(item["attachment_id"]);
                    obj.EMPCode = item["EMP_Code"].ToString();
                    obj.EMPName = item["EMP_Name"].ToString();
                    obj.EMPStatus = item["EMP_Status"].ToString();
                    obj.ContentType = item["Content_Type"].ToString();
                    obj.RFC = Convert.ToInt32(item["RFC"]);
                    obj.RFCStatus = Convert.ToInt32(item["RFCStatus"]);
                    obj.RFCRemarks = item["RFCRemarks"].ToString();
                    obj.hoursDayWise = item["hoursDayWise"].ToString();
                    obj.HODRemarks = item["HODRemarks"].ToString();
                    obj.EDRemarks = item["EDRemarks"].ToString();
                    obj.Reason = item["Reason"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveLog_Tran. The query was executed :", ex.ToString(), SQL, "LeaveModal", "LeaveModal", "");
            }
            return List;

        }


        public SeniorLeaveDashboard GetSeniorLeaveDashboard(long EMPID)
        {
            SeniorLeaveDashboard obj = new SeniorLeaveDashboard();

            obj.ApplyLeaveList = GetApplyLeaveDashboardList(EMPID);
            obj.LeaveBalanceDetails = GetLeaveBalanceList(EMPID, DateTime.Now.Month, DateTime.Now.Year);
            obj.RequestLeaveList = GetLeaveLogSeniorDashboardList(EMPID);
            return obj;
        }
        public List<ApplyLeaveList> GetLeaveLogSeniorDashboardList(long EMPID)
        {
            List<ApplyLeaveList> List = new List<ApplyLeaveList>();
            ApplyLeaveList obj = new ApplyLeaveList();
            string SQL = "";
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetLeaveLogSenior_Dashboard(EMPID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ApplyLeaveList();
                    obj.EMPID = Convert.ToInt64(item["Emp_ID"]);
                    obj.LeaveLogID = Convert.ToInt64(item["ID"]);
                    obj.DocNo = item["Doc_No"].ToString();
                    obj.RequestDate = Convert.ToDateTime(item["Request_date"]).ToString("dd MMM yyyy");
                    obj.StartDate = Convert.ToDateTime(item["start_date"]).ToString("dd MMM");
                    obj.EndDate = Convert.ToDateTime(item["End_Date"]).ToString("dd MMM yyyy");
                    obj.LeaveHours = Convert.ToInt32(item["Leave_Hours"]);
                    obj.hoursDayWise = item["hoursDayWise"].ToString();
                    obj.Remarks = item["Remarks"].ToString();
                    obj.EmergenContactno = item["EmergenContact_no"].ToString();
                    obj.EmergenContactName = item["EmergenContactName"].ToString();
                    obj.LeaveID = Convert.ToInt32(item["Leave_ID"]);
                    obj.Approved = Convert.ToInt32(item["Approved"]);
                    obj.LeaveName = item["Leave_Name"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.AttachmentRequired = Convert.ToInt32(item["AttachmentRequired"]);
                    obj.EMPName = item["Emp_Name"].ToString();
                    obj.EMPCode = item["Emp_Code"].ToString();
                    obj.RFC = Convert.ToInt32(item["RFC"]);
                    obj.RFCStatus = Convert.ToInt32(item["RFCStatus"]);
                    obj.RFCRemarks = item["RFCRemarks"].ToString();

                    obj.HODRemarks = item["HODRemarks"].ToString();
                    obj.EDRemarks = item["EDRemarks"].ToString();
                    obj.isED= item["isED"].ToString()=="Y"?true:false;
                    obj.HOD_ID = Convert.ToInt64(item["HOD_ID"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveLogSeniorDashboardList. The query was executed :", ex.ToString(), SQL, "LeaveModal", "LeaveModal", "");
            }
            return List;

        }

        public List<Calendardata> GetCalenedarData(long EMPID)
        {
            string SQL = "";
        
            List<Calendardata> List = new List<Calendardata>();
            Calendardata obj = new Calendardata();
            long LocationID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LocationID"), out LocationID);
            if (EMPID == 0)
            {
                long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            }
            try
            {
               // long.TryParse(clsDataBaseHelper.ExecuteSingleResult("select address.location_id from master_emp inner join address on master_emp.address_id=address.id and master_emp.id=" + EMPID + " and master_emp.isdeleted=0"), out LocationID);
                long.TryParse(clsDataBaseHelper.ExecuteSingleResult("select WorkLocationID from master_emp where id=" + EMPID + ""), out LocationID);

                SQL = @"select holiday_date,color_code,Holiday_name,Remarks from HOLIDAY  where isdeleted =0 and holiday.id 
                        in(select holiday_id from map_holiday_loc where isdeleted=0 and location_id=" + LocationID + ")  ORDER BY HOLIDAY_DATE desc";

                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Calendardata();
                    obj.title = item["Holiday_name"].ToString();
                    obj.start = Convert.ToDateTime(item["holiday_date"]).ToString("yyyy-MM-dd");
                    obj.end = Convert.ToDateTime(item["holiday_date"]).ToString("yyyy-MM-dd");
                    obj.classNames = "holiday";
                    obj.description = item["Holiday_name"] + " " + item["Remarks"];
                    List.Add(obj);
                }

                SQL = @"select leave_log.emp_id,approved,master_leave.leave_name,master_leave.ShortName,leavedet_log.date  from leavedet_log inner join leave_log on leavedet_log.leavelog_id =leave_log.ID
                        inner join master_leave on master_leave.id=leavedet_log.leave_id
                        where leavedet_log.isdeleted =0 and leave_log.isdeleted=0 and approved in(0,3,1,4)  
                         and emp_id = " + EMPID + "  ORDER BY date desc";

                DataSet TempModuleDataSet1 = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet1.Tables[0].Rows)
                {
                    obj = new Calendardata();

                    obj.start = Convert.ToDateTime(item["date"]).ToString("yyyy-MM-dd");
                    obj.end = Convert.ToDateTime(item["date"]).ToString("yyyy-MM-dd");

                    if (Convert.ToInt32(item["approved"].ToString()) == 0 || Convert.ToInt32(item["approved"].ToString()) == 3)
                    {
                        obj.title = item["ShortName"].ToString() + " Pending";

                        obj.classNames = "pendingleave";
                        obj.description = item["leave_name"].ToString() + " Pending";

                    }
                    else if (Convert.ToInt32(item["approved"].ToString()) == 1 || Convert.ToInt32(item["approved"].ToString()) == 4)
                    {
                        obj.title = item["ShortName"].ToString() + " Approved";

                        obj.description = item["leave_name"].ToString() + " Approved";
                        obj.classNames = "approvedleave";
                    }
                    else
                    {
                        obj.title = item["ShortName"].ToString() + " Cancelled";
                        obj.description = item["leave_name"].ToString() + " Cancelled";
                        obj.classNames = "Cancelledleave";

                    }


                    List.Add(obj);
                }


                // Travel Booking display calender
                SQL = @"select req_no,approved,submited,isbooked,DepartureDate,ReturnDate from travel_request where emp_id = " + EMPID + "  and isdeleted=0 ORDER BY req_no desc";

                DataSet TempModuleDataSet2 = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet2.Tables[0].Rows)
                {

                    int count = 0;
                    string depturedate = null;
                    foreach (DateTime day in EachDay(Convert.ToDateTime(item["DepartureDate"]), Convert.ToDateTime(item["ReturnDate"])))
                    {
                        obj = new Calendardata();
                     
                        if (count == 0)
                        {
                            obj.start = Convert.ToDateTime(item["DepartureDate"]).ToString("yyyy-MM-dd");
                            obj.end = Convert.ToDateTime(item["DepartureDate"]).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            obj.start = depturedate;
                            obj.end = depturedate;
                        }
                     
                        //if (Convert.ToInt32(item["approved"].ToString()) == 0 && Convert.ToInt32(item["submited"].ToString()) == 0 && Convert.ToInt32(item["isbooked"].ToString()) == 0)
                        //{
                        //    obj.title = " TNA"+ item["req_no"].ToString();
                        //    obj.description = "TNA"+item["req_no"].ToString();
                        //    obj.classNames = "approvedtravel";

                        //}
                      if (Convert.ToInt32(item["approved"].ToString()) == 1 && Convert.ToInt32(item["submited"].ToString()) == 0 && (Convert.ToInt32(item["isbooked"].ToString()) == 0|| Convert.ToInt32(item["isbooked"].ToString()) == 1))
                        {
                            obj.title = " TA"+ item["req_no"].ToString();
                            obj.description = " TA"+ item["req_no"].ToString();
                            obj.classNames = "approvedtravel";

                        }
                        if (count == 0)
                        {
                            depturedate = Convert.ToDateTime(item["DepartureDate"]).AddDays(1).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            depturedate = Convert.ToDateTime(depturedate).AddDays(1).ToString("yyyy-MM-dd");
                        }

                        count++;
                       
                        List.Add(obj);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetHoliTrvAndLeaveList. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }
            return List;

        }
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public List<HolidayDailyLog> GetHoliTrvAndLeaveList(long EMPID)
        {
            string SQL = "";

            List<HolidayDailyLog> List = new List<HolidayDailyLog>();
            HolidayDailyLog obj = new HolidayDailyLog();
            long LocationID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LocationID"), out LocationID);
            if (EMPID == 0)
            {
                long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            }
            try
            {
                long.TryParse(clsDataBaseHelper.ExecuteSingleResult("select address.location_id from master_emp inner join address on master_emp.address_id=address.id and master_emp.id=" + EMPID + " and master_emp.isdeleted=0"), out LocationID);

                SQL = @"select holiday_date,color_code,Holiday_name,Remarks from HOLIDAY  where isdeleted =0 and holiday.id 
                        in(select holiday_id from map_holiday_loc where isdeleted=0 and location_id=" + LocationID + ")  ORDER BY HOLIDAY_DATE";

                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new HolidayDailyLog();
                    obj.Name = item["Holiday_name"].ToString();
                    obj.Date = Convert.ToDateTime(item["holiday_date"]).ToString("yyyy/MM/dd");
                    obj.ClassName = "holiday";
                    //obj.Color = "#e72d35";
                    obj.HolidayType = "Holiday";
                    obj.Description = item["Holiday_name"] + " " + item["Remarks"];
                    List.Add(obj);
                }

                SQL = @"select leave_log.emp_id,approved,master_leave.leave_name,master_leave.ShortName,leavedet_log.date  from leavedet_log inner join leave_log on leavedet_log.leavelog_id =leave_log.ID
                        inner join master_leave on master_leave.id=leavedet_log.leave_id
                        where leavedet_log.isdeleted =0 and approved in(0,3,1,4)  
                         and emp_id = " + EMPID + "  ORDER BY date";

                DataSet TempModuleDataSet1 = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet1.Tables[0].Rows)
                {
                    obj = new HolidayDailyLog();

                    obj.Date = Convert.ToDateTime(item["date"]).ToString("yyyy/MM/dd");

                    if (Convert.ToInt32(item["approved"].ToString()) == 0 || Convert.ToInt32(item["approved"].ToString()) == 3)
                    {
                        obj.HolidayType = item["ShortName"].ToString() + " Pending";
                        obj.Name = obj.HolidayType;
                        //obj.Color = "#1d46dc";
                        obj.ClassName = "pendingleave";
                        obj.Description = item["leave_name"].ToString() + " Pending";

                    }
                    else if (Convert.ToInt32(item["approved"].ToString()) == 1 || Convert.ToInt32(item["approved"].ToString()) == 4)
                    {
                        obj.HolidayType = item["ShortName"].ToString() + " Approved";
                        obj.Name = obj.HolidayType;
                        //obj.Color = "#1a9e0a";
                        obj.Description = item["leave_name"].ToString() + " Approved";
                        obj.ClassName = "approvedleave";
                    }
                    else
                    {
                        obj.HolidayType = item["ShortName"].ToString() + " Cancelled";
                        obj.Name = obj.HolidayType;
                        //obj.Color = "#e01e1a";
                        obj.Description = item["leave_name"].ToString() + " Cancelled";
                        obj.ClassName = "Cancelledleave";

                    }


                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetHoliTrvAndLeaveList. The query was executed :", ex.ToString(), SQL, "ActivityModal", "ActivityModal", "");
            }
            return List;

        }

        public List<LeaveType> GetLeaveType()
        {
            List<LeaveType> List = new List<LeaveType>();
            LeaveType obj = new LeaveType();
            string SQL = "", Gender = clsApplicationSetting.GetSessionValue("Gender");
            try
            {
                SQL = @"select id,leave_name from master_leave where isdeleted =0 and isActive=1 
                        and (ApplicableFor='" + Gender + "' or ApplicableFor='ALL')  order by Priority,leave_name ";
                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new LeaveType();
                    obj.ID = Convert.ToInt32(item["ID"]);
                    obj.LeaveName = item["leave_name"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveType. The query was executed :", ex.ToString(), SQL, "LeaveModal", "LeaveModal", "");
            }
            return List;

        }
        public List<LeaveType> GetLeaveTypebyEmp(string Empid)
        {
            List<LeaveType> List = new List<LeaveType>();
            LeaveType obj = new LeaveType();
            string SQL = "", Gender = clsApplicationSetting.GetSessionValue("Gender");
            try
            {
                
                SQL = @"select master_leave.id,leave_name from master_leave
                    inner join master_empdet on master_empdet.leave_id = master_leave.id and master_empdet.isdeleted = 0
                    where master_leave.isdeleted = 0 and master_leave.isActive = 1 and master_empdet.emp_id = "+ Empid + "  and(ApplicableFor = '" + Gender + "' or ApplicableFor = 'ALL') group by master_leave.id,leave_name,master_leave.Priority order by master_leave.Priority,master_leave.leave_name";
                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new LeaveType();
                    obj.ID = Convert.ToInt32(item["ID"]);
                    obj.LeaveName = item["leave_name"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveType. The query was executed :", ex.ToString(), SQL, "LeaveModal", "LeaveModal", "");
            }
            return List;

        }
        public LeaveEmp GetLeaveEmpDetails(long EMPID)
        {
            LeaveEmp obj = new LeaveEmp();
            string SQL = "";
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetEmpCompleteDetail(EMPID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj.EMail = item["email"].ToString();
                    obj.EMPName = item["emp_name"].ToString();
                    obj.EMPCode = item["emp_code"].ToString();
                    obj.HODName = item["HOD1"].ToString();
                    obj.EmergencyContactNo = item["emergContact_no"].ToString();
                    obj.EmergencyContactName = item["emergContact_Name"].ToString();
                    obj.EmergencyContactRelation = item["emergContact_Relation"].ToString();
                    obj.HODID = Convert.ToInt32(item["hod_name"]);

                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveEmpDetails. The query was executed :", ex.ToString(), SQL, "LeaveModal", "LeaveModal", "");
            }
            return obj;

        }

        public string ValidateCLSL(List<LeaveTran> Modal)
        {
            string Message = "", SQL = "";
            long EMPIDDD = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);
            //string FixHolidays = clsApplicationSetting.GetConfigValue("FixHolidays"); code comment by shailendra

            string NSQL = @"select top 1 case when master_all.field_name1='6 Days' then 'Sunday' else 'Sunday,Saturday' end from SS_EmployeeSalary inner join master_all on SS_EmployeeSalary.Subcategoryid=master_all.id where SS_EmployeeSalary.Empid=" + EMPIDDD + " and  SS_EmployeeSalary.isdeleted=0  order by SS_EmployeeSalary.id desc";
            string FixHolidays = clsDataBaseHelper.ExecuteSingleResult(NSQL);

            int AllowedCLSL = 0;
            int.TryParse(clsApplicationSetting.GetConfigValue("AllowedCLSL"), out AllowedCLSL);

            string _SQL = @"select top 1 SS_EmployeeSalary.workingHours from SS_EmployeeSalary left join master_all as tblSubcategory on tblSubcategory.id=SS_EmployeeSalary.subcategoryid and tblSubcategory.table_name='SS_Subcategory' where empid=" + EMPIDDD + " and   SS_EmployeeSalary.isdeleted=0 order by SS_EmployeeSalary.id desc";

            int WorkHours = Convert.ToInt32(clsDataBaseHelper.ExecuteSingleResult(_SQL));

            if (Modal.Any(x => x.LeaveType == "1"))
            {
                int CLSLCount = 0;
                DateTime StartDate, EndDate;
                DateTime.TryParse(Modal.OrderBy(x => x.LeaveDate).Select(x => x.LeaveDate).FirstOrDefault().ToString(), out StartDate);
                DateTime.TryParse(Modal.OrderByDescending(x => x.LeaveDate).Select(x => x.LeaveDate).FirstOrDefault().ToString(), out EndDate);

                // Checking last Maternity Taken within this year
                DateTime LastMaternitTaken;
                SQL = @"select leavedet_log.date from leavedet_log 
                        inner join leave_log on leavedet_log.leavelog_id=leave_log.id where 
                        leave_log.emp_id=" + EMPIDDD + "  and leave_log.isdeleted=0 and leavedet_log.isdeleted=0 and leave_log.approved in(0,1,3,4) " +
                        " and leavedet_log.leave_id=5 and year(leavedet_log.date)=" + StartDate.Year + " order by leavedet_log.date desc";
                DateTime.TryParse(clsDataBaseHelper.ExecuteSingleResult(SQL), out LastMaternitTaken);

                if (LastMaternitTaken.Date.Year > 1900)
                {
                    DataSet ValidaDateDS = Common_SPU.fnGetWorkingDays(LastMaternitTaken.AddDays(1).ToString("dd/MM/yyyy"), EndDate.ToString("dd/MM/yyyy"));
                    for (int i = 0; i < ValidaDateDS.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToDateTime(ValidaDateDS.Tables[0].Rows[i]["date"]).Date == StartDate.Date)
                        {
                            Message = "After Maternity Leave";
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(Message))
                {

                    DataSet tempDateSet = Common_SPU.fnValidateCLSLLeave(StartDate, EndDate);
                    bool isFutureDate = false;
                    if (StartDate.Date > DateTime.Now.Date || EndDate.Date > DateTime.Now.Date)
                    {
                        isFutureDate = true;
                    }
                    for (int i = 0; i < tempDateSet.Tables[0].Rows.Count; i++)
                    {
                        CLSLCount = CLSLCount + Convert.ToInt32(tempDateSet.Tables[0].Rows[i]["hours"]);
                    }
                    //int.TryParse(tempDateSet.Tables[0].Rows.Count.ToString(), out CLSLCount);
                    foreach (var item in Modal)
                    {
                        if (item.LeaveType=="1")
                        {
                            CLSLCount = CLSLCount + Convert.ToInt32(item.LeaveHours);
                        }
                    }
                    //CLSLCount = CLSLCount + Modal.Where(x => x.LeaveType == "1").Count();
                    if (CLSLCount > AllowedCLSL && EndDate.Date < DateTime.Now.Date)
                    {
                        Message = "Past CL/SL";
                        ClsCommon.LogError("Error during GetLeaveList. The query was executed :", Message.ToString(), "", "CommonSpecial", "CommonSpecial", "");
                    }
                    else if (CLSLCount > AllowedCLSL && !isFutureDate)
                    {
                        Message = "Medical Certificate";
                    }
                    else if (CLSLCount > AllowedCLSL)
                    {
                        Message = "Pass";
                    }
                }
            }

            //if (PreviousCLSLCount+Modal.Where(x => x.LeaveType == "1").Count() > 3 && Convert.ToDateTime(Modal.Select(x => x.LeaveDate).LastOrDefault()).Date < DateTime.Now.Date)
            //{
            //    Message = "Past CL/SL";
            //}
            //else if (PreviousCLSLCount+Modal.Where(x => x.LeaveType == "1").Count() > 3 && !isFutureDate)
            //{
            //    Message = "Medical Certificate";
            //}
            //else if (PreviousCLSLCount + Modal.Where(x => x.LeaveType == "1").Count() > 3)
            //{
            //    //Message = "CL/SL already taken "+ Message.TrimEnd(',') + " now you have to attached Medical & Fitness Certificate it is mandatory";
            //    Message = "Pass";
            //}
            return Message;

        }

        public string ValidateLeaveRequest(List<LeaveTran> Modal, DateTime ExpectedDeliveryDate)
        {
            string Message = "";
            string SQL = "";
            string TotalPendingAppliedLeave = "0";
            decimal TodatBalance = 0;
            long EMPIDDD = 0;
            long PaternityLeaveAllowed = 0, MaternityLeaveAllowed = 0, AllowedMaternityBeforeDelivery = 0;
            long PaternityLeaveRequestd = 0, MaternityLeaveRequestd = 0;
            DateTime JoiningDate, DORDate, dtALSubmitDate, LastWorkingDayDate;
            bool PaternityTaken = false, MaternitTaken = false, ProcessValid = true;
            try
            {
                long.TryParse(clsApplicationSetting.GetConfigValue("PaternityLeaveAllowed"), out PaternityLeaveAllowed);
                long.TryParse(clsApplicationSetting.GetConfigValue("MaternityLeaveAllowed"), out MaternityLeaveAllowed);
                long.TryParse(clsApplicationSetting.GetConfigValue("AllowedMaternityBeforeDelivery"), out AllowedMaternityBeforeDelivery);
                long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);

                DateTime StartDate, EndDate;
                DateTime.TryParse(Modal.OrderBy(x => x.LeaveDate).Select(x => x.LeaveDate).FirstOrDefault().ToString(), out StartDate);
                DateTime.TryParse(Modal.OrderByDescending(x => x.LeaveDate).Select(x => x.LeaveDate).FirstOrDefault().ToString(), out EndDate);
                if (ExpectedDeliveryDate.Year > 1900 && Modal.Any(x => x.LeaveType == "5"))
                {
                    DateTime NewDate;
                    DateTime.TryParse(ExpectedDeliveryDate.Date.AddDays(-AllowedMaternityBeforeDelivery).ToString(), out NewDate);
                    if (Modal.Any(x => x.LeaveDate.Date < NewDate.Date))
                    {
                        Message = "You Can't apply for Maternity Leave before " + ExpectedDeliveryDate.Date.AddDays(-AllowedMaternityBeforeDelivery).ToString("dd-MMM-yyyy");
                        ProcessValid = false;
                    }
                }

                if (ProcessValid)
                {
                    SQL = @"select isnull(doj,'') from master_emp where id=" + EMPIDDD + "  and isdeleted=0";
                    DateTime.TryParse(clsDataBaseHelper.ExecuteSingleResultScalar(SQL), out JoiningDate);

                    SQL = @"select isnull(DOR,'') from master_emp where id=" + EMPIDDD + "  and isdeleted=0";
                    DateTime.TryParse(clsDataBaseHelper.ExecuteSingleResultScalar(SQL), out DORDate);

                    SQL = @"select isnull(lastworking_day,'') from master_emp where id=" + EMPIDDD + "  and isdeleted=0";
                    DateTime.TryParse(clsDataBaseHelper.ExecuteSingleResultScalar(SQL), out LastWorkingDayDate);

                    //Checking Activity log
                    DateTime.TryParse(clsDataBaseHelper.fnGetOther_FieldName("master_emp left join active_log on master_emp.id=active_log.emp_id and active_log.isdeleted=0", "isnull(max(dateadd(day,-1,dateadd(MM,1,datefromparts(year,month,1)))),dateadd(day,-1,doj))", "master_emp.id", EMPIDDD.ToString(), " and master_emp.isdeleted=0 group by master_emp.id,master_emp.doj"), out dtALSubmitDate);

                    // Checking Maternity Taken within this year
                    SQL = @"select leavedet_log.id from leavedet_log 
                        inner join leave_log on leavedet_log.leavelog_id=leave_log.id where 
                        leave_log.emp_id=" + EMPIDDD + "  and leave_log.isdeleted=0 and leavedet_log.isdeleted=0 and leave_log.approved in(0,1,3,4) " +
                            " and leavedet_log.leave_id=5 and year(leavedet_log.date)=" + StartDate.Year;
                    bool.TryParse((clsDataBaseHelper.CheckRecord(SQL) == 0 ? "False" : "True").ToString(), out MaternitTaken);



                    // Checking Paternity Leave Taken within this year
                    SQL = @"select leavedet_log.id from leavedet_log 
                        inner join leave_log on leavedet_log.leavelog_id=leave_log.id where 
                        leave_log.emp_id=" + EMPIDDD + "  and leave_log.isdeleted=0 and leavedet_log.isdeleted=0 and leave_log.approved in(0,1,3,4) " +
                            " and leavedet_log.leave_id=7 and year(leavedet_log.date)=" + StartDate.Year;

                    bool.TryParse((clsDataBaseHelper.CheckRecord(SQL) == 0 ? "False" : "True").ToString(), out PaternityTaken);

                    if (StartDate.Date < JoiningDate)
                    {
                        Message = "Leave date can not be less than date of joining.";
                    }
                    //else if (DORDate.Year > 1900 && StartDate.Date > DORDate) // Commented on 30-06-2022 by Aswati
                    //{
                    //    Message = "Leave date can not be less than date of resignation.";
                    //}
                    else if (LastWorkingDayDate.Year > 1900 && (StartDate.Date > LastWorkingDayDate || EndDate.Date > LastWorkingDayDate)) // Added on 30-06-2022 by Aswati
                    {
                        Message = "Leave date can not be less than Last Working Day.";
                    }
                    
                    else if (StartDate.Date <= dtALSubmitDate.Date)
                    {
                        Message = "The activity log already submited for this month, leave can not be apply for this date.";
                    }
                    else if (MaternitTaken && Modal.Where(x => x.LeaveType == "5").Count() > 0)
                    {
                        Message = "Maternity Leave already applied for this year.";
                    }
                    else if (PaternityTaken && Modal.Where(x => x.LeaveType == "7").Count() > 0)
                    {
                        Message = "Paternity Leave already applied for this year.";
                    }
                    else
                    {
                        var newTotalList = Modal.GroupBy(x => x.LeaveType)
                     .Select(x => new
                     {
                         LeaveType = x.Key,
                         Hours = x.Sum(y => y.LeaveHours),
                         StartDate = x.Select(ex => ex.LeaveDate).FirstOrDefault(),
                         EndDate = x.Select(ex => ex.LeaveDate).LastOrDefault()
                     })
                     .ToList();
                        // Checking Leave Balance
                        foreach (var item in newTotalList)
                        {
                            if (Convert.ToInt32(item.LeaveType) > 4 | Convert.ToInt32(item.LeaveType) == 2)
                            {
                                // checking by Start Date
                                TodatBalance = Common_SPU.fnGetLeaveBalanceByLeaveID(EMPIDDD, item.LeaveType, DateTime.Now.Month, DateTime.Now.Year);

                                SQL = @"select isnull(sum(cast(hours as int)),0)as a from leavedet_log inner join leave_log on leavedet_log.leavelog_id=leave_log.id where " +
                                 " leave_log.emp_id=" + EMPIDDD + " and leave_log.isdeleted=0 and leavedet_log.isdeleted=0 and leave_log.approved in(0,3)" +
                                 " and leavedet_log.leave_id=" + item.LeaveType + " and month(leavedet_log.date)=" + Convert.ToDateTime(item.StartDate).Month + " and year(leavedet_log.date)=" + Convert.ToDateTime(item.StartDate).Year + "";
                                TotalPendingAppliedLeave = clsDataBaseHelper.ExecuteSingleResultScalar(SQL);

                                decimal pendingLeave = 0;
                                decimal.TryParse(TotalPendingAppliedLeave, out pendingLeave);
                                //LeaveType = clsDataBaseHelper.ExecuteSingleResult("select leave_Name from master_leave where id=" + item.LeaveType + "");
                                if ((TodatBalance - pendingLeave) - item.Hours < 0)
                                {
                                    Message = "You Can't apply for leave because this leave request will cause the leave balance negative";

                                }
                                //if (Convert.ToDateTime(item.StartDate) != Convert.ToDateTime(item.EndDate))
                                //{
                                //    // cheking By end Date
                                //    TodatBalance = Common_SPU.fnGetLeaveBalanceByLeaveID(EMPIDDD, item.LeaveType, Convert.ToDateTime(item.EndDate).Month, Convert.ToDateTime(item.EndDate).Year);
                                //    SQL = @"select isnull(sum(cast(hours as int)),0)as a from leavedet_log inner join leave_log on leavedet_log.leavelog_id=leave_log.id where " +
                                //     " leave_log.emp_id=" + EMPIDDD + " and leave_log.isdeleted=0 and leavedet_log.isdeleted=0 and leave_log.approved in(0,3)" +
                                //     " and leavedet_log.leave_id=" + item.LeaveType;// + " and month(leavedet_log.date)=" + Convert.ToDateTime(item.EndDate).Month + " and year(leavedet_log.date)=" + Convert.ToDateTime(item.EndDate).Year + "";
                                //    TotalPendingAppliedLeave = clsDataBaseHelper.ExecuteSingleResultScalar(SQL);

                                //    pendingLeave = 0;
                                //    decimal.TryParse(TotalPendingAppliedLeave, out pendingLeave);

                                //    LeaveType = clsDataBaseHelper.ExecuteSingleResult("select leave_Name from master_leave where id=" + item.LeaveType + "");
                                //    if ((TodatBalance - pendingLeave) - item.Hours < 0)
                                //    {
                                //        Message = "You Can't apply for leave because this leave request will cause the leave balance negative";
                                //    }
                                //}
                            }
                        }

                        // Now Checking Leave Date Already  Applied
                        if (string.IsNullOrEmpty(Message))
                        {

                            foreach (var item in Modal)
                            {
                                SQL = @"select leavedet_log.id from leave_log inner join leavedet_log on leave_log.id=leavedet_log.leavelog_id
                            where leavedet_log.date='" + Convert.ToDateTime(item.LeaveDate).ToString("dd-MMM-yyyy") + "' and leavedet_log.isdeleted=0 and leave_log.isdeleted=0 and leave_log.approved in(0,1,3,4)" +
                                    " and leave_log.emp_id=" + EMPIDDD + "  ";
                                if (clsDataBaseHelper.CheckRecord(SQL) > 0)
                                {
                                    Message += Convert.ToDateTime(item.LeaveDate).ToString("dd-MMM-yyyy") + ",";
                                }

                                if (Convert.ToInt32(item.LeaveType) == 7)
                                {
                                    PaternityLeaveRequestd++;
                                }
                                if (Convert.ToInt32(item.LeaveType) == 5)
                                {
                                    MaternityLeaveRequestd++;
                                }
                            }
                            if (!string.IsNullOrEmpty(Message))
                            {
                                Message = "Leave has been already applied on " + Message.TrimEnd(',');
                            }
                            else if (PaternityLeaveAllowed != 0 && PaternityLeaveRequestd > PaternityLeaveAllowed)
                            {
                                Message = "You can't apply more than " + PaternityLeaveAllowed + " Paternity Leave ";
                            }
                            else if (MaternityLeaveAllowed != 0 && MaternityLeaveRequestd > MaternityLeaveAllowed)
                            {
                                Message = "You can't apply more than " + MaternityLeaveAllowed + " Maternity Leave ";
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during ValidateAddLeaveRequest. The query was executed :", ex.ToString(), SQL, "LeaveModal", "LeaveModal", "");
            }
            return Message;
        }


        public LeaveAttachmentCount GetAttachmentRequiredCount()
        {
            //long Tr = 0;
            LeaveAttachmentCount Obj = new LeaveAttachmentCount();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnLeaveAttachmentRequiredCount();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    Obj.MyLeaveCount = Convert.ToInt64(item["MyLeaveCount"]);
                    Obj.TeamLeaveCount = Convert.ToInt64(item["TeamLeaveCount"]);

                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnLeaveAttachmentRequiredCount. The query was executed :", ex.ToString(), "", "LeaveModal", "LeaveModal", "");
            }
            return Obj;

        }


        public List<LeaveNonMITRAdd> GetLeaveNonMITRAddList(LeaveNonMitrMonth Modal)
        {
            List<LeaveNonMITRAdd> List = new List<LeaveNonMITRAdd>();
            LeaveNonMITRAdd obj = new LeaveNonMITRAdd();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetLeaveEntryNonMITR(Modal.Empid,Modal.Month,Modal.Empids,Modal.LeaveTypeids);
                if (TempModuleDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {
                        obj = new LeaveNonMITRAdd();
                        obj.Id = Convert.ToInt32(item["LeaveLogid"].ToString());
                        obj.Empid = Convert.ToInt32(item["id"].ToString());
                        obj.LeaveTypeID = Convert.ToInt32(item["Leaveid"].ToString());
                        obj.LeaveDate = Convert.ToDateTime(item["LeaveDate"].ToString());
                        obj.EmpName = item["Emp"].ToString();
                        obj.LeaveName = item["leave_name"].ToString();
                        obj.FixHoliday = item["FixHoliday"].ToString();
                        obj.DOJ =Convert.ToDateTime(item["doj"].ToString());
                        obj.LastWorkingDay = Convert.ToDateTime(item["lastworking_day"].ToString());
                        obj.LeaveHours = 0;
                        obj.IsAttachment = Convert.ToInt32(item["IsAttachment"].ToString());
                        obj.Opening = item["Opening"].ToString();
                        obj.Accrued = item["Accrued"].ToString();
                        obj.Availed = item["Availed"].ToString();
                        obj.Balance = item["Balance"].ToString();
                        obj.Prevbalance = item["Prevbalance"].ToString();
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
                ClsCommon.LogError("Error during GetLeaveNonMITRAddList. The query was executed :", ex.ToString(), "spu_GetLeaveEntryNonMITR", "LeaveModal", "LeaveModal", "");
            }
            return List;
        }

        public List<BudgetMaster.EmployeeList> GetLeaveEmpList(string EMPType)
        {
            string SQL = "";
            List<BudgetMaster.EmployeeList> List = new List<BudgetMaster.EmployeeList>();
            BudgetMaster.EmployeeList obj = new BudgetMaster.EmployeeList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetLeaveEmployeeList(EMPType);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.EmployeeList();
                    obj.Id = Convert.ToInt64(item["ID"]);
                    obj.Name = item["Name"].ToString();

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetEmployeeBudgetSetting. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;
        }
    }
   
}

//string COunt = TodatBalance.ToString();
//if (pendingLeave > 0)
//{
//    Message = "You Can't apply for leave because this leave request will cause the leave balance negative";
//}
//else
//{
//    Message = "You Can't apply for leave because this leave request will cause the leave balance negative";
//}
//long.TryParse(Modal.Sum(x => x.LeaveHours).ToString(), out TotalLeaveHours);
//if (TodatBalance<= TotalLeaveHours)
//{

//    Message = "Total Leave Balance is " + TodatBalance + " and You Are Applying For " + TotalLeaveHours + " .";
//}
//else
//{
//    foreach (LeaveTran item in Modal)
//    {
//        TodatBalance = Common_SPU.fnGetLeaveBalanceByLeaveID(clsApplicationSetting.GetSessionValue("MainEMPID"), item.LeaveType,Convert.ToDateTime(item.LeaveDate).Month, Convert.ToDateTime(item.LeaveDate).Year);

//        SQL = @"select isnull(sum(cast(hours as int)),0)as a from leavedet_log inner join leave_log on leavedet_log.leavelog_id=leave_log.id where " +
//            " leave_log.Company_Code='" + clsApplicationSetting.GetSessionValue("CompanyCode") + "' and leave_log.emp_id=" + EMPID + " and leave_log.isdeleted=0 and leavedet_log.isdeleted=0 and leave_log.approved in(0,3) and leavedet_log.leave_id=" + item.LeaveType + " and month(leavedet_log.date)=" + Convert.ToDateTime(item.LeaveDate).Month + " and year(leavedet_log.date)=" + Convert.ToDateTime(item.LeaveDate).Year + "";
//        TotalPendingAppliedLeave = clsDataBaseHelper.ExecuteSingleResultScalar(SQL);


//        //SQL = @"select doj from emp where id=" + EmpID + "  and Company_Code='"+clsApplicationSetting.GetSessionValue("CompanyCode")+"'";
//        //JoiningDate = clsDataBaseHelper.ExecuteSingleResultScalar(SQL);


//        //if (Convert.ToDateTime(Date) < Convert.ToDateTime(JoiningDate))
//        //{
//        //    Message = "Leave date can not be less than date of joining.";
//        LeaveType = clsDataBaseHelper.ExecuteSingleResult("select leave_Name from master_leave where Company_Code='" + clsApplicationSetting.GetSessionValue("CompanyCode") + "' and id=" + LeaveType + "");
//        if ((TodatBalance - Convert.ToInt32(TotalPendingAppliedLeave)) - item.LeaveHours < 0)
//        {
//            string COunt = TodatBalance.ToString();
//            Message = "On " + Convert.ToDateTime(item.LeaveDate).ToString("dd-MMM-yyyy") + " Do not Have " + LeaveType + "  Leave Balance Current Balance is " + COunt + " .";
//        }


//    }
//}