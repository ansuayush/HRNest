using Mitr.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.CommonClass
{
    public class CommonSpecial
    {

        public static int fnGetTotalMonthHours(int EMPID, DateTime dtMonth, DateTime DateofJoining, DateTime DateofLeaving)
        {
            int RetValue = 0;
            try
            {
                DateTime dtJoiningDate = DateofJoining;
                DateTime dtSMFDate = Convert.ToDateTime("1/" + dtMonth.Month + "/" + dtMonth.Year);
                DateTime dtSMEDate = dtSMFDate.AddMonths(1).AddDays(-1);
                int iWDays = dtSMEDate.Day;
                int iTotDays = dtSMEDate.Day;
                DateTime dtdate;
                int iOffDays = 0;
                bool bJMonth = false;
                bool bRMonth = false;
                DateTime dtResignDate = DateofLeaving;
                if (dtSMEDate < dtJoiningDate)
                {
                }
                else if (dtSMEDate.Month == dtJoiningDate.Month & dtSMEDate.Year == dtJoiningDate.Year)
                {
                    iWDays = iWDays - (dtJoiningDate.Day - 1);
                    bJMonth = true;
                }
                else if (dtSMEDate.Month == dtResignDate.Month & dtSMEDate.Year == dtResignDate.Year)
                {
                    iWDays = dtResignDate.Day;
                    bRMonth = true;
                }
                for (int i = 1; i <= iTotDays; i++)
                {
                    dtdate = Convert.ToDateTime(i + "/" + dtMonth.Month + "/" + dtMonth.Year);
                    string sDay = dtdate.ToString("ddd");
                    if (sDay == "Sun" | sDay == "Sat")
                    {
                        if (bJMonth == true & dtdate.Day < dtJoiningDate.Day)
                        {
                        }
                        else if (bRMonth == true & dtdate.Day > dtResignDate.Day)
                        {
                        }
                        else
                            iOffDays = iOffDays + 1;
                    }
                }
                iWDays = iWDays - iOffDays;
                int inpl = 0;
                int.TryParse(clsDataBaseHelper.ExecuteSingleResult("select npl_hours from timesheet_log where emp_id=" + EMPID + " and isdeleted=0 and month=" + dtMonth.Month + " and year=" + dtMonth.Year + ""), out inpl);

                RetValue = (iWDays * 8) - inpl;
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetTotalMonthHours. The query was executed :", ex.ToString(), "", "CommonSpecial", "CommonSpecial", "");
            }
            return RetValue;
        }


        public static double fnGetBonusAmt(int lEmpID, int FinancialYearID)
        {
            double dBonus = 0;
            try
            {

                DataSet dsFin = clsDataBaseHelper.ExecuteDataSet("select * from finyear where isdeleted=0 and id=" + FinancialYearID + "");
                if (dsFin.Tables[0].Rows.Count > 0)
                {
                    DateTime dtFDate, dtTDate, dtJoiningDate, dtResignDate;
                    DateTime.TryParse(dsFin.Tables[0].Rows[0]["from_date"].ToString(), out dtFDate);
                    DateTime.TryParse(dsFin.Tables[0].Rows[0]["to_date"].ToString(), out dtTDate);

                    DateTime.TryParse(clsDataBaseHelper.ExecuteSingleResult("select doj from master_emp where id=" + lEmpID), out dtJoiningDate);
                    DateTime.TryParse(clsDataBaseHelper.ExecuteSingleResult("select dor from master_emp where id=" + lEmpID), out dtResignDate);


                    DateTime dtEffDate;
                    double dLICAmt = 0;
                    DataSet ds = clsDataBaseHelper.ExecuteDataSet("select * from emp_salary where isdeleted=0 and emp_id=" + lEmpID + " and doc_date between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtTDate.ToString("dd/MMM/yyyy") + "' order by doc_date");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[0]["doc_date"].ToString(), out dtEffDate);
                        double.TryParse(ds.Tables[0].Rows[0]["lic_amt"].ToString(), out dLICAmt);

                    }
                    else
                    {
                        ds = clsDataBaseHelper.ExecuteDataSet("select * from emp_salary where isdeleted=0 and id in(select max(id) from emp_salary where isdeleted=0 and emp_id=" + lEmpID + ")");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DateTime.TryParse(ds.Tables[0].Rows[0]["doc_date"].ToString(), out dtEffDate);
                            double.TryParse(ds.Tables[0].Rows[0]["lic_amt"].ToString(), out dLICAmt);

                        }
                    }
                    if (dtFDate <= dtJoiningDate)
                        dtFDate = dtJoiningDate;
                    double dTotSalAmt = 0;
                    double dSalAmt = 0;
                    DataTable dt = new DataTable();
                    DataRow dr = null/* TODO Change to default(_) if this is not a reference type */;
                    dt.Columns.Add(new DataColumn("Month", typeof(string)));
                    dt.Columns.Add(new DataColumn("total_hours", typeof(double)));
                    dt.Columns.Add(new DataColumn("hourly_rate", typeof(double)));
                    dt.Columns.Add(new DataColumn("TotalAmt", typeof(double)));

                    bool bRegin = false;
                    if (dtResignDate.Year > 1900)
                        bRegin = true;
                    DateTime dtMonth = dtFDate;
                    if (dtJoiningDate > dtMonth)
                        dtMonth = dtJoiningDate;
                    int iTotHours = fnGetTotalMonthHours(lEmpID, dtMonth, dtJoiningDate, dtResignDate);
                    double dHourRate = 0;
                    string SQL = "select hourly_rate from emp_salary where emp_id=" + lEmpID + "  and isdeleted=0" +
                        " and id in(select max(id) from emp_salary where isdeleted=0 and emp_id=" + lEmpID + " and doc_date<='" + dtMonth.ToString("dd/MMM/yyyy") + "')";
                    double.TryParse(clsDataBaseHelper.ExecuteSingleResult(SQL), out dHourRate);

                    for (int iCount = 1; iCount <= 12; iCount++)
                    {
                        dr = dt.NewRow();
                        dr["Month"] = dtMonth.ToString("MMM/yyyy");
                        dr["total_hours"] = iTotHours;
                        dr["hourly_rate"] = dHourRate;
                        dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        dr["Totalamt"] = dSalAmt; // Math.Round(dHourRate * iTotHours, 2)
                        dTotSalAmt = dTotSalAmt + dSalAmt;
                        dt.Rows.Add(dr);
                        dtMonth = dtMonth.AddMonths(1);
                        if (dtMonth > dtTDate | bRegin == true & dtMonth > dtResignDate)
                            break;
                        iTotHours = fnGetTotalMonthHours(lEmpID, dtMonth, dtJoiningDate, dtResignDate);
                        SQL = "select hourly_rate  from emp_salary where emp_id=" + lEmpID + "" +
                            "and isdeleted=0 and id in(select max(id) from emp_salary where isdeleted=0 and emp_id=" + lEmpID + " and doc_date<='" + dtMonth.ToString("dd/MMM/yyyy") + "')";

                        double.TryParse(clsDataBaseHelper.ExecuteSingleResult(SQL), out dHourRate);
                    }
                    double dSalary = dTotSalAmt;
                    dBonus = Math.Round(dSalary * 0.0833, MidpointRounding.AwayFromZero);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetTotalMonthHours. The query was executed :", ex.ToString(), "", "CommonSpecial", "CommonSpecial", "");
            }
            return dBonus;
        }


        public static List<MiscEmployee> GetEmployeeCompensatoryOFF()
        {
            List<MiscEmployee> List = new List<MiscEmployee>();
            DataSet TempModuleDataSet = new DataSet();
            MiscEmployee Obj = new MiscEmployee();
            string SQL = "";

            try
            {


                SQL = @"SELECT id,emp_name + ' (' + emp_code + ')' AS empName 
                                          FROM master_emp WHERE ISDELETED=0 and co_ot<>'Overtime' 
                                        and cast(hod_name as int)=" + clsApplicationSetting.GetSessionValue("EMPID") + " ";

                TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Obj = new MiscEmployee();
                    Obj.EMPID = Convert.ToInt32(item["ID"]);
                    Obj.EMPName = item["empName"].ToString();
                    List.Add(Obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeCompensatoryOFF. The query was executed :", ex.ToString(), SQL, "CommonSpecial", "CommonSpecial", "");
            }
            return List;

        }



        public static List<MiscEmployee> GetEmployeeOvertime()
        {
            List<MiscEmployee> List = new List<MiscEmployee>();
            DataSet TempModuleDataSet = new DataSet();
            MiscEmployee Obj = new MiscEmployee();
            string SQL = "";

            try
            {
                SQL = @"SELECT id,emp_name + ' (' + emp_code + ')' AS empName FROM master_emp WHERE ISDELETED=0 and co_ot='Overtime' 
                        and cast(hod_name as int)=" + clsApplicationSetting.GetSessionValue("EMPID") + " ";
                TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Obj = new MiscEmployee();
                    Obj.EMPID = Convert.ToInt32(item["ID"]);
                    Obj.EMPName = item["empName"].ToString();
                    List.Add(Obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeOvertime. The query was executed :", ex.ToString(), SQL, "CommonSpecial", "CommonSpecial", "");
            }
            return List;

        }
        public static List<MiscUserman> GetUsermanList(string WantCurrentUserInList = "N")
        {
            List<MiscUserman> List = new List<MiscUserman>();
            DataSet TempModuleDataSet = new DataSet();
            MiscUserman Obj = new MiscUserman();
            string SQL = "";
            try
            {
                TempModuleDataSet = Common_SPU.fnGetUserman(0, WantCurrentUserInList);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Obj = new MiscUserman();
                    Obj.LoginID = Convert.ToInt32(item["ID"]);
                    Obj.EMPName = item["empname"].ToString();
                    Obj.EMPCode = item["EMPCode"].ToString();
                    Obj.EMPID = Convert.ToInt32(item["EMPID"]);
                    Obj.EMPNameCode = item["EMPNameCode"].ToString();
                    List.Add(Obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetUsermanList. The query was executed :", ex.ToString(), SQL, "CommonSpecial", "CommonSpecial", "");
            }
            return List;


        }

        public static List<MiscEmployee> GetAllEmployeeList()
        {
            List<MiscEmployee> List = new List<MiscEmployee>();
            DataSet TempModuleDataSet = new DataSet();
            MiscEmployee Obj = new MiscEmployee();
            string SQL = "";

            try
            {
                SQL = @"select id,emp_name,emp_code,email  from master_emp where isdeleted=0";
                TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Obj = new MiscEmployee();
                    Obj.EMPID = Convert.ToInt32(item["ID"]);
                    Obj.EMPName = item["emp_name"].ToString();
                    Obj.EMPCode = item["emp_code"].ToString();
                    Obj.Email = item["email"].ToString();
                    List.Add(Obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeOvertime. The query was executed :", ex.ToString(), SQL, "CommonSpecial", "CommonSpecial", "");
            }
            return List;

        }

        public static List<MiscEmployee> GetAllEmployeeOfficeList()
        {
            List<MiscEmployee> List = new List<MiscEmployee>();
            DataSet TempModuleDataSet = new DataSet();
            MiscEmployee Obj = new MiscEmployee();
            string SQL = "";

            try
            {
                SQL = @"select id,emp_name,emp_code,email  from master_emp where isdeleted=0 and Year(MASTER_EMP.lastworking_day)=1900 and user_id>0";
                TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Obj = new MiscEmployee();
                    Obj.EMPID = Convert.ToInt32(item["ID"]);
                    Obj.EMPName = item["emp_name"].ToString();
                    Obj.EMPCode = item["emp_code"].ToString();
                    Obj.Email = item["email"].ToString();
                    Obj.EMP = Obj.EMPName + "  (" + Obj.EMPCode + ")";
                    List.Add(Obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeOvertime. The query was executed :", ex.ToString(), SQL, "CommonSpecial", "CommonSpecial", "");
            }
            return List;

        }


        public static List<MiscEmployee> GetAllEmployeeOnbehalf()
        {
            List<MiscEmployee> List = new List<MiscEmployee>();
            DataSet TempModuleDataSet = new DataSet();
            MiscEmployee Obj = new MiscEmployee();
            string SQL = "";

            try
            {
              //  SQL = @"select id,emp_name,emp_code,email  from master_emp where isdeleted=0 and Year(MASTER_EMP.lastworking_day)=1900";
                TempModuleDataSet = Common_SPU.fnGetOnbehalfEMP();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Obj = new MiscEmployee();
                    Obj.EMPID = Convert.ToInt32(item["ID"]);
                    Obj.EMPName = item["emp_name"].ToString();
                    Obj.EMPCode = item["emp_code"].ToString();
                    Obj.Email = item["email"].ToString();
                    List.Add(Obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeOvertime. The query was executed :", ex.ToString(), SQL, "CommonSpecial", "CommonSpecial", "");
            }
            return List;

        }
        public static List<MiscLocation> GetAllLocationList()
        {
            List<MiscLocation> List = new List<MiscLocation>();
            DataSet TempModuleDataSet = new DataSet();
            MiscLocation Obj = new MiscLocation();
            string SQL = "";

            try
            {
                SQL = @"select id,Location_Name  from  master_location where isdeleted=0 and isactive=1";
                TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Obj = new MiscLocation();
                    Obj.LocationID = Convert.ToInt32(item["ID"]);
                    Obj.LocationName = item["Location_Name"].ToString();
                    List.Add(Obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAllLocationList. The query was executed :", ex.ToString(), SQL, "CommonSpecial", "CommonSpecial", "");
            }
            return List;

        }
        public static void prcUpdateCompOff(DateTime SelectedDate, long EMPID)
        {
            DateTime dtDate;
            string sDay = "";
            int iSrno = 0;
            bool bHoliday = false;
            //string sDate = "";
            double dHours = 0;
            DateTime dtCODate;
            double dWHours = 0;
            double dOTHours = 0;
            string CTOT = "";
            string Locationid = "0";
            double WorkingHour = 0;
            try
            {
                double.TryParse(clsApplicationSetting.GetConfigValue("WorkingHours"), out WorkingHour);
                //string FixHolidays = clsApplicationSetting.GetConfigValue("FixHolidays");
                string FixHolidays = ClsCommon.GetFixHolidaysByEmpid(EMPID.ToString());
                DateTime dtFDate = new DateTime(Convert.ToDateTime(SelectedDate).Year, Convert.ToDateTime(SelectedDate).Month, 1);
                DateTime dtEDate = dtFDate.AddMonths(1).AddDays(-1);

                CTOT = clsDataBaseHelper.ExecuteSingleResult("select CO_OT from Master_EMP where isdeleted=0 and ID=" + EMPID);
                Locationid = clsDataBaseHelper.ExecuteSingleResult("select WorkLocationID from Master_EMP where isdeleted=0 and ID=" + EMPID);
                if (CTOT == "Overtime")
                {
                    DataSet dsDAL = clsDataBaseHelper.ExecuteDataSet("select max(Description) as Description,max(proj_id) as proj_id,max(srno) as srno,sum(cast(day1 as int)) as day1,sum(cast(day2 as int)) as day2,sum(cast(day3 as int)) as day3,sum(cast(day4 as int)) as day4,sum(cast(day5 as int)) as day5,sum(cast(day6 as int)) as day6,sum(cast(day7 as int)) as day7,sum(cast(day8 as int)) as day8,sum(cast(day9 as int)) as day9,sum(cast(day10 as int)) as day10,sum(cast(day11 as int)) as day11,sum(cast(day12 as int)) as day12,sum(cast(day13 as int)) as day13,sum(cast(day14 as int)) as day14,sum(cast(day15 as int)) as day15,sum(cast(day16 as int)) as day16,sum(cast(day17 as int)) as day17,sum(cast(day18 as int)) as day18,sum(cast(day19 as int)) as day19,sum(cast(day20 as int)) as day20,sum(cast(day21 as int)) as day21,sum(cast(day22 as int)) as day22,sum(cast(day23 as int)) as day23,sum(cast(day24 as int)) as day24,sum(cast(day25 as int)) as day25,sum(cast(day26 as int)) as day26,sum(cast(day27 as int)) as day27,sum(cast(day28 as int)) as day28,sum(cast(day29 as int)) as day29,sum(cast(day30 as int)) as day30,sum(cast(day31 as int)) as day31 from daily_log " +
                        "where isdeleted=0 and emp_id=" + EMPID + " and month=" + SelectedDate.Month + " and year=" + SelectedDate.Year + "");
                    for (int i = 1; i <= dtEDate.Day; i++)
                    {
                        dtDate = new DateTime(Convert.ToDateTime(SelectedDate).Year, Convert.ToDateTime(SelectedDate).Month, i);

                        sDay = dtDate.ToString("dddd");
                        bHoliday = false;
                        if (FixHolidays.Contains(sDay))
                        {
                        }
                        else if (clsDataBaseHelper.CheckRecord("select ID from holiday where holiday_date='" + dtDate.ToString("dd/MMM/yyyy") + "' and isdeleted=0 and holiday.id in (select holiday_id from map_holiday_loc where isdeleted=0 and location_id=" + Locationid + ")") > 0)
                            bHoliday = true;
                        else
                            bHoliday = false;
                        double.TryParse(dsDAL.Tables[0].Rows[0]["day" + i].ToString(), out dWHours);
                        dWHours = dWHours + Convert.ToInt32(clsDataBaseHelper.fnGetOther_FieldName("leavedet_log a inner join leave_log b on a.leavelog_id=b.id", "isnull(SUM(CAST(a.hours AS INT)),0)", "emp_id", EMPID.ToString(), " and a.isdeleted=0 and b.isdeleted=0 and b.approved in(0,1,3,4) and a.date='" + dtDate.ToString("yyyy-MM-dd") + "'"));
                        if (FixHolidays.Contains(sDay) | bHoliday == true | dWHours > WorkingHour)
                        {
                            for (int iCount = 0; iCount <= dsDAL.Tables[0].Rows.Count - 1; iCount++)
                            {
                                int dayValue = 0;
                                int.TryParse(dsDAL.Tables[0].Rows[iCount]["srno"].ToString(), out iSrno);
                                int.TryParse(dsDAL.Tables[0].Rows[iCount]["day" + i].ToString(), out dayValue);
                                dayValue = dayValue + Convert.ToInt32(clsDataBaseHelper.fnGetOther_FieldName("leavedet_log a inner join leave_log b on a.leavelog_id=b.id", "isnull(SUM(CAST(a.hours AS INT)),0)", "emp_id", EMPID.ToString(), " and a.isdeleted=0 and b.isdeleted=0 and b.approved in(0,1,3,4) and a.date='" + dtDate.ToString("yyyy-MM-dd") + "'"));
                                if (dayValue > 0)
                                {
                                    if (FixHolidays.Contains(sDay) || bHoliday == true)
                                        dOTHours = dayValue;
                                    else
                                        dOTHours = dayValue - WorkingHour;
                                    Common_SPU.fnSetOvertime(iSrno, EMPID, SelectedDate.Month, SelectedDate.Year, dtDate.ToString("dd/MMM/yyyy"), dOTHours.ToString(), dsDAL.Tables[0].Rows[iCount]["Description"].ToString(), dsDAL.Tables[0].Rows[iCount]["proj_id"].ToString());
                                }
                                else
                                    clsDataBaseHelper.ExecuteNonQuery("update overtime_log set isdeleted=1 where isdeleted=0 and emp_id=" + EMPID + " and month=" + SelectedDate.Month + " and year=" + SelectedDate.Year + " and date='" + dtDate.ToString("dd/MMM/yyyy") + "'");
                            }
                        }
                        else
                            clsDataBaseHelper.ExecuteNonQuery("update overtime_log set isdeleted=1 where isdeleted=0 and emp_id=" + EMPID + " and month=" + SelectedDate.Month + " and year=" + SelectedDate.Year + " and date='" + dtDate.ToString("dd/MMM/yyyy") + "'");
                    }
                }
                else
                {
                    DataSet dsCOMail;
                    DataSet dsDAL = clsDataBaseHelper.ExecuteDataSet("select * from daily_log where isdeleted=0 and emp_id=" + EMPID + " and month=" + SelectedDate.Month + " and year=" + SelectedDate.Year + " order by srno");
                    for (int i = 1; i <= dtEDate.Day; i++)
                    {
                        try
                        {
                            dtDate = new DateTime(Convert.ToDateTime(SelectedDate).Year, Convert.ToDateTime(SelectedDate).Month, i);
                            sDay = dtDate.ToString("dddd");
                            bHoliday = false;
                            if (FixHolidays.Contains(sDay))
                            {
                            }
                            else if (clsDataBaseHelper.CheckRecord("select ID from holiday where holiday_date='" + dtDate.ToString("dd/MMM/yyyy") + "' and isdeleted=0 and holiday.id in(select holiday_id from map_holiday_loc where isdeleted=0 and location_id=" + Locationid + ")") > 0)
                                bHoliday = true;
                            else
                                bHoliday = false;

                            if (FixHolidays.Contains(sDay) | bHoliday == true)
                            {

                                for (int iCount = 0; iCount <= dsDAL.Tables[0].Rows.Count - 1; iCount++)
                                {
                                    int dayValue = 0;
                                    int.TryParse(dsDAL.Tables[0].Rows[iCount]["srno"].ToString(), out iSrno);
                                    int.TryParse(dsDAL.Tables[0].Rows[iCount]["day" + i].ToString(), out dayValue);

                                    if (dayValue > 0)
                                        Common_SPU.fnSetCompensatoryOff(iSrno, EMPID, SelectedDate.Month, dtDate.ToString("dd/MMM/yyyy"), dayValue.ToString(), dsDAL.Tables[0].Rows[iCount]["Description"].ToString(), dsDAL.Tables[0].Rows[iCount]["proj_id"].ToString());
                                    else
                                    {
                                        dsCOMail = clsDataBaseHelper.ExecuteDataSet("select * from compensatory_off where isdeleted=0 and mailsent=1 and emp_id=" + EMPID + " and month=" + SelectedDate.Month + " and date='" + dtDate.ToString("dd/MMM/yyyy") + "' and srno=" + iSrno);
                                        if (dsCOMail.Tables[0].Rows.Count > 0)
                                        {
                                            for (int iRow = 0; iRow <= dsCOMail.Tables[0].Rows.Count - 1; iRow++)
                                            {
                                                double aaaahours = 0;
                                                DateTime.TryParse(dsCOMail.Tables[0].Rows[iRow]["date"].ToString(), out dtCODate);
                                                double.TryParse(dsCOMail.Tables[0].Rows[iRow]["hours"].ToString(), out aaaahours);
                                                //sDate = sDate + Interaction.IIf(sDate == "", "", ", ") + Strings.Format(dtCODate, "dd/MM/yyyy");
                                                dHours = dHours + aaaahours;
                                            }
                                        }
                                        clsDataBaseHelper.ExecuteNonQuery("update compensatory_off set isdeleted=1 where isdeleted=0 and emp_id=" + EMPID + " and month=" + SelectedDate.Month + " and date='" + dtDate.ToString("dd/MMM/yyyy") + "' and srno=" + iSrno);
                                    }
                                }
                                dsCOMail = clsDataBaseHelper.ExecuteDataSet("select * from compensatory_off where isdeleted=0 and mailsent=1 and emp_id=" + EMPID + " and month=" + SelectedDate.Month + " and date='" + dtDate.ToString("dd/MMM/yyyy") + "' and srno>" + iSrno);
                                if (dsCOMail.Tables[0].Rows.Count > 0)
                                {
                                    for (int iRow = 0; iRow <= dsCOMail.Tables[0].Rows.Count - 1; iRow++)
                                    {
                                        double aaaahours = 0;
                                        DateTime.TryParse(dsCOMail.Tables[0].Rows[iRow]["date"].ToString(), out dtCODate);
                                        double.TryParse(dsCOMail.Tables[0].Rows[iRow]["hours"].ToString(), out aaaahours);
                                        //sDate = sDate + Interaction.IIf(sDate == "", "", ", ") + Strings.Format(dtCODate, "dd/MM/yyyy");
                                        dHours = dHours + aaaahours;
                                    }
                                }
                                clsDataBaseHelper.ExecuteNonQuery("update compensatory_off set isdeleted=1 where isdeleted=0 and emp_id=" + EMPID + " and month=" + SelectedDate.Month + " and date='" + dtDate.ToString("dd/MMM/yyyy") + "' and srno>" + iSrno);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ClsCommon.LogError("Error during prcUpdateCompOff. The query was executed :", ex.ToString(), "", "CommonSpecial", "CommonSpecial", "");
            }
        }


        // Not in Use
        public static DataTable prcProjectHours(DateTime SelectedDate, long EMPID)
        {
            //bool bHoliday = false;
            //string sDate = "";
            double WorkingHour = 0;
            DataTable dt = new DataTable();
            try
            {
                double[] dDaysHrs = new double[32];
                for (int iRow = 0; iRow <= 31; iRow++)
                    dDaysHrs[iRow] = 0;

                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("Column1", typeof(string)));
                for (int iRow = 1; iRow <= 31; iRow++)
                    dt.Columns.Add(new DataColumn("day" + iRow, typeof(string)));
                dt.Columns.Add(new DataColumn("total", typeof(string)));
                int iSerialNo = 0;
                int isDeleted = 2;
                if (clsDataBaseHelper.CheckRecord("select count(id) from timesheet_log where emp_id=" + EMPID + " and isdeleted=0 and month=" + SelectedDate.Month + " and year=" + SelectedDate.Year + "") > 0)
                {
                    isDeleted = 3;
                }
                double.TryParse(clsApplicationSetting.GetConfigValue("WorkingHours"), out WorkingHour);
                string FixHolidays = clsApplicationSetting.GetConfigValue("FixHolidays");
                DateTime dtDate;
                DateTime dtFDate = new DateTime(Convert.ToDateTime(SelectedDate).Year, Convert.ToDateTime(SelectedDate).Month, 1);
                DateTime dtEDate = dtFDate.AddMonths(1).AddDays(-1);
                DataSet dsActiveLog = clsDataBaseHelper.ExecuteDataSet("select active_log.*,project_registration.projref_no  from active_log inner join project_registration on active_log.proj_id =project_registration.ID where active_log.isdeleted = " + isDeleted + " and active_log.Month =" + SelectedDate.Month + " and active_log.year =" + SelectedDate.Year + " and active_log.emp_id =" + EMPID + " order by active_log.srno ");
                if (dsActiveLog.Tables[0].Rows.Count > 0)
                {
                    for (int iCount = 0; iCount <= dsActiveLog.Tables[0].Rows.Count - 1; iCount++)
                    {
                        dr = dt.NewRow();
                        iSerialNo = iSerialNo + 1;
                        dr["rownumber"] = iSerialNo;
                        dr["Column1"] = dsActiveLog.Tables[0].Rows[iCount]["projref_no"];
                        for (int iRow = 1; iRow <= dtEDate.Day; iRow++)
                        {
                            dtDate = new DateTime(Convert.ToDateTime(SelectedDate).Year, Convert.ToDateTime(SelectedDate).Month, iRow);

                            string sDay = dtDate.ToString("dddd");
                            double dayValue = 0;
                            double.TryParse(dsActiveLog.Tables[0].Rows[iCount]["day" + iRow].ToString(), out dayValue);
                            if (FixHolidays.Contains(sDay))
                            {
                                dr["day" + iRow] = "S";
                            }
                            else
                            {
                                dr["day" + iRow] = dayValue;
                            }
                        }
                        double total = 0;
                        double.TryParse(dsActiveLog.Tables[0].Rows[iCount]["total"].ToString(), out total);
                        dr["total"] = total;
                        dDaysHrs[0] = dDaysHrs[0] + total;
                        for (int iRow = 1; iRow <= dtEDate.Day; iRow++)
                        {
                            double ddd = 0;
                            double.TryParse(dsActiveLog.Tables[0].Rows[iCount]["day" + iRow].ToString(), out ddd);
                            dDaysHrs[iRow] = dDaysHrs[iRow] + ddd;
                        }
                        dt.Rows.Add(dr);
                    }
                }
                DataSet dsLeaveDet = clsDataBaseHelper.ExecuteDataSet("select id,leave_name from master_leave where isdeleted=0 and leave_type=1 and id in(select leavedet_log.leave_id from leave_log inner join leavedet_log on leave_log.ID=leavedet_log.leavelog_id where leavedet_log.isdeleted=0 and leave_log.isdeleted=0 and leave_log.approved in(0,1,3,4) and leave_log.emp_id=" + EMPID + " and month(leavedet_log.date)=" + SelectedDate.Month + " and year(leavedet_log.date)=" + SelectedDate.Year + ")");
                if (dsLeaveDet.Tables[0].Rows.Count > 0)
                {
                    for (int iCount = 0; iCount <= dsLeaveDet.Tables[0].Rows.Count - 1; iCount++)
                    {
                        dr = dt.NewRow();
                        iSerialNo = iSerialNo + 1;
                        dr["rownumber"] = iSerialNo;
                        dr["Column1"] = dsLeaveDet.Tables[0].Rows[iCount]["leave_name"];
                        for (int iRow = 1; iRow <= dtEDate.Day; iRow++)
                        {
                            dtDate = new DateTime(Convert.ToDateTime(SelectedDate).Year, Convert.ToDateTime(SelectedDate).Month, iRow);

                            string sDay = dtDate.ToString("dddd");

                            if (FixHolidays.Contains(sDay))
                            {
                                dr["day" + iRow] = "S";
                            }
                            else
                            {
                                dr["day" + iRow] = "";
                            }
                        }
                        double dTLHrs = 0;
                        DateTime dtLeaveDate;
                        DataSet dsLeave = clsDataBaseHelper.ExecuteDataSet("select leave_log.emp_id,leavedet_log.leave_id,convert(varchar(11),leavedet_log.date,103) as Date,leavedet_log.hours  from leave_log inner join leavedet_log on leave_log.ID=leavedet_log.leavelog_id where leave_log.isdeleted =0 and leavedet_log.isdeleted =0 and Leave_log.approved in(1,4) and leave_log.emp_id=" + EMPID + " and month(leavedet_log.date)= " + SelectedDate.Month + " and YEAR(leavedet_log.date)=" + SelectedDate.Year + " and leavedet_log.leave_id=" + dsLeaveDet.Tables[0].Rows[iCount]["id"] + " order by leavedet_log.DATE");
                        if (dsLeave.Tables[0].Rows.Count > 0)
                        {
                            for (var j = 0; j <= dsLeave.Tables[0].Rows.Count - 1; j++)
                            {
                                double ddd = 0;
                                double.TryParse(dsActiveLog.Tables[0].Rows[iCount]["hours"].ToString(), out ddd);

                                dtLeaveDate = Convert.ToDateTime(dsLeave.Tables[0].Rows[j]["date"]);
                                dr["day" + dtLeaveDate.Day] = ddd;
                                dTLHrs = dTLHrs + ddd;
                                dDaysHrs[dtLeaveDate.Day] = dDaysHrs[dtLeaveDate.Day] + ddd;
                            }
                        }
                        dDaysHrs[0] = dDaysHrs[0] + dTLHrs;
                        dr["total"] = dTLHrs;
                        dt.Rows.Add(dr);
                    }
                }
                DateTime dtHDate;
                long lLoactionID = 0;
                long.TryParse(clsDataBaseHelper.fnGetOther_FieldName("master_emp inner join address on master_emp.address_id=address.id", "address.location_id", "master_emp.id", EMPID.ToString(), " and master_emp.isdeleted=0"), out lLoactionID);
                DataSet dsHoliday = clsDataBaseHelper.ExecuteDataSet("select convert(varchar(11),HOLIDAY_DATE,103) as holiday_date,color_code,color_name from HOLIDAY where isdeleted =0 and holiday.id in(select holiday_id from map_holiday_loc where isdeleted=0 and location_id=" + lLoactionID + ") AND  HOLIDAY_DATE between '" + dtFDate.ToString("dd/MMM/yyyy") + "' and '" + dtEDate.ToString("dd/MMM/yyyy") + "' ORDER BY HOLIDAY_DATE");
                if (dsHoliday.Tables[0].Rows.Count > 0)
                {
                    dr = dt.NewRow();
                    iSerialNo = iSerialNo + 1;
                    dr["rownumber"] = iSerialNo;
                    dr["Column1"] = "Holiday";
                    for (int iRow = 1; iRow <= dtEDate.Day; iRow++)
                    {
                        dtDate = new DateTime(Convert.ToDateTime(SelectedDate).Year, Convert.ToDateTime(SelectedDate).Month, iRow);
                        string sDay = dtDate.ToString("dddd");
                        if (FixHolidays.Contains(sDay))
                        {
                            dr["day" + iRow] = "S";
                        }
                        else
                        {
                            dr["day" + iRow] = "";
                        }
                    }
                    double dTHDHrs = 0;
                    if (dsHoliday.Tables[0].Rows.Count > 0)
                    {
                        for (var j = 0; j <= dsHoliday.Tables[0].Rows.Count - 1; j++)
                        {

                            dtHDate = Convert.ToDateTime(dsHoliday.Tables[0].Rows[j]["holiday_date"]);
                            dr["day" + dtHDate.Day] = WorkingHour;
                            dTHDHrs = dTHDHrs + WorkingHour;
                            dDaysHrs[dtHDate.Day] = dDaysHrs[dtHDate.Day] + WorkingHour;
                        }
                    }
                    dDaysHrs[0] = dDaysHrs[0] + dTHDHrs;
                    dr["total"] = dTHDHrs;
                    dt.Rows.Add(dr);
                }
                dr = dt.NewRow();
                dr["rownumber"] = "";
                dr["Column1"] = "Paid Hours";
                for (int iRow = 1; iRow <= dtEDate.Day; iRow++)
                {
                    dtDate = new DateTime(Convert.ToDateTime(SelectedDate).Year, Convert.ToDateTime(SelectedDate).Month, iRow);
                    string sDay = dtDate.ToString("dddd");
                    if (FixHolidays.Contains(sDay))
                    {
                        dr["day" + iRow] = "S";
                    }
                    else
                    {
                        dr["day" + iRow] = dDaysHrs[iRow];
                    }
                }
                dr["total"] = dDaysHrs[0];
                dt.Rows.Add(dr);
                iSerialNo = 0;
                string sCOOT = clsDataBaseHelper.fnGetOther_FieldName("master_emp", "co_ot", "id", EMPID.ToString(), " and isdeleted=0");
                if (sCOOT == "Overtime")
                {
                    DataSet dsOTHrs = clsDataBaseHelper.ExecuteDataSet("select overtime_log.emp_id,convert(varchar(11),overtime_log.date,103) as Date,overtime_log.approve_hours as hours from overtime_log where overtime_log.isdeleted =0 and overtime_log.approved=1 and overtime_log.emp_id=" + EMPID + " and overtime_log.month= " + SelectedDate.Month + " and overtime_log.year=" + SelectedDate.Year + " order by overtime_log.date");
                    if (dsOTHrs.Tables[0].Rows.Count > 0)
                    {
                        dr = dt.NewRow();
                        dr["rownumber"] = "";
                        dr["Column1"] = "OT Hours";

                        for (int iRow = 1; iRow <= dtEDate.Day; iRow++)
                        {
                            dtDate = new DateTime(Convert.ToDateTime(SelectedDate).Year, Convert.ToDateTime(SelectedDate).Month, iRow);
                            string sDay = dtDate.ToString("dddd");
                            if (FixHolidays.Contains(sDay))
                            {
                                dr["day" + iRow] = "S";
                            }
                            else
                            {
                                dr["day" + iRow] = "";
                            }
                        }
                        double dTOTHrs = 0;
                        DateTime dtOTDate;
                        for (var j = 0; j <= dsOTHrs.Tables[0].Rows.Count - 1; j++)
                        {
                            double ddd = 0;
                            double.TryParse(dsOTHrs.Tables[0].Rows[j]["hours"].ToString(), out ddd);
                            dtOTDate = Convert.ToDateTime(dsOTHrs.Tables[0].Rows[j]["date"]);
                            dr["day" + dtOTDate.Day] = ddd;
                            dTOTHrs = dTOTHrs + ddd;
                        }
                        dr["total"] = dTOTHrs;
                        dt.Rows.Add(dr);
                    }
                }
                iSerialNo = 0;
                dsLeaveDet = clsDataBaseHelper.ExecuteDataSet("select id,leave_name from master_leave where isdeleted=0 and leave_type=2");
                if (dsLeaveDet.Tables[0].Rows.Count > 0)
                {
                    for (int iCount = 0; iCount <= dsLeaveDet.Tables[0].Rows.Count - 1; iCount++)
                    {
                        dr = dt.NewRow();
                        iSerialNo = iSerialNo + 1;
                        dr["rownumber"] = iSerialNo;
                        dr["Column1"] = dsLeaveDet.Tables[0].Rows[iCount]["leave_name"];
                        for (int iRow = 1; iRow <= dtEDate.Day; iRow++)
                        {
                            dtDate = new DateTime(Convert.ToDateTime(SelectedDate).Year, Convert.ToDateTime(SelectedDate).Month, iRow);
                            string sDay = dtDate.ToString("dddd");
                            if (FixHolidays.Contains(sDay))
                            {
                                dr["day" + iRow] = "S";
                            }
                            else
                            {
                                dr["day" + iRow] = "";
                            }
                        }
                        double dTLHrs = 0;
                        DateTime dtLeaveDate;
                        DataSet dsLeave = clsDataBaseHelper.ExecuteDataSet("select leave_log.emp_id,leavedet_log.leave_id,convert(varchar(11),leavedet_log.date,103) as Date,leavedet_log.hours  from leave_log inner join leavedet_log on leave_log.ID=leavedet_log.leavelog_id where leave_log.isdeleted =0 and leavedet_log.isdeleted =0 and Leave_log.approved in(1,4) and leave_log.emp_id=" + EMPID + " and month(leavedet_log.date)= " + SelectedDate.Month + " and year(leavedet_log.date)=" + SelectedDate.Year + " and leavedet_log.leave_id=" + dsLeaveDet.Tables[0].Rows[iCount]["id"] + " order by leavedet_log.DATE");
                        if (dsLeave.Tables[0].Rows.Count > 0)
                        {
                            for (var j = 0; j <= dsLeave.Tables[0].Rows.Count - 1; j++)
                            {
                                double ddd = 0;
                                double.TryParse(dsLeave.Tables[0].Rows[j]["hours"].ToString(), out ddd);

                                dtLeaveDate = Convert.ToDateTime(dsLeave.Tables[0].Rows[j]["date"]);
                                dr["day" + dtLeaveDate.Day] = ddd;
                                dTLHrs = dTLHrs + ddd;

                                double ddd2 = 0;
                                double.TryParse(dsActiveLog.Tables[0].Rows[j]["day" + dtLeaveDate.Day].ToString(), out ddd2);

                                dDaysHrs[dtLeaveDate.Day] = dDaysHrs[dtLeaveDate.Day] + ddd2;
                            }
                        }
                        dDaysHrs[0] = dDaysHrs[0] + dTLHrs;
                        dr["total"] = dTLHrs;
                        dt.Rows.Add(dr);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public static ReimEligibility prcCalculateReimEligibility(long lEmpID, DateTime dtMonthName, DateTime dtJDate, DateTime dtRDate)
        {
            ReimEligibility obj = new ReimEligibility();
            try
            {

                double[] dMonthAmt = new double[12];
                double[] dBasicAmt = new double[12];
                double[] dHRAAmt = new double[12];
                double[] dTransAmt = new double[12];
                double[] dPFAmt = new double[12];

                for (int iCount = 0; iCount < 12; iCount++)
                {
                    dMonthAmt[iCount] = 0;
                    dBasicAmt[iCount] = 0;
                    dHRAAmt[iCount] = 0;
                    dTransAmt[iCount] = 0;
                    dPFAmt[iCount] = 0;
                }
                DataSet dsFin = clsDataBaseHelper.ExecuteDataSet("select * from finyear where isdeleted=0 and from_date<='" + dtMonthName.ToString("dd/MMM/yyyy") + "' and to_date>='" + dtMonthName.ToString("dd/MMM/yyyy") + "'");
                if (dsFin.Tables[0].Rows.Count > 0)
                {
                    DateTime dtFDate, dtTDate;
                    DateTime.TryParse(dsFin.Tables[0].Rows[0]["from_date"].ToString(), out dtFDate);
                    DateTime.TryParse(dsFin.Tables[0].Rows[0]["to_date"].ToString(), out dtTDate);
                    double dTotSalAmt = 0;
                    if (dtFDate <= dtJDate)
                        dtFDate = dtJDate;
                    double dSalAmt = 0;
                    bool bRegin = false;
                    if (dtRDate.Year > 1900)
                        bRegin = true;
                    DateTime dtMonth = dtFDate;
                    if (dtJDate > dtMonth)
                        dtMonth = dtJDate;
                    int iTotHours = fnGetTotalMonthHours(Convert.ToInt32(lEmpID), dtMonth, dtJDate, dtRDate);
                    double dLICAmt, dHourRate;
                    double.TryParse(clsDataBaseHelper.fnGetOther_FieldName("emp_salary", "lic_amt", "emp_id", lEmpID.ToString(), " and isdeleted=0 and id in(select max(id) from emp_salary where isdeleted=0 and emp_id=" + lEmpID + " and doc_date<='" + dtMonth.ToString("dd/MMM/yyyy") + "')"), out dLICAmt);
                    double.TryParse(clsDataBaseHelper.fnGetOther_FieldName("emp_salary", "hourly_rate", "emp_id", lEmpID.ToString(), " and isdeleted=0 and id in(select max(id) from emp_salary where isdeleted=0 and emp_id=" + lEmpID + " and doc_date<='" + dtMonth.ToString("dd/MMM/yyyy") + "')"), out dHourRate);
                    for (int iCount = 1; iCount < 12; iCount++)
                    {
                        dSalAmt = 0;
                        if (dtMonthName.Month == 4)
                        {
                            if (dtMonth.Month != 4)
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 5)
                        {
                            if (dtMonth.Month == 4 | dtMonth.Month == 5)
                            {
                            }
                            else
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 6)
                        {
                            if (dtMonth.Month == 4 | dtMonth.Month == 5 | dtMonth.Month == 6)
                            {
                            }
                            else
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 7)
                        {
                            if (dtMonth.Month == 4 | dtMonth.Month == 5 | dtMonth.Month == 6 | dtMonth.Month == 7)
                            {
                            }
                            else
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 8)
                        {
                            if (dtMonth.Month == 4 | dtMonth.Month == 5 | dtMonth.Month == 6 | dtMonth.Month == 7 | dtMonth.Month == 8)
                            {
                            }
                            else
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 9)
                        {
                            if (dtMonth.Month == 4 | dtMonth.Month == 5 | dtMonth.Month == 6 | dtMonth.Month == 7 | dtMonth.Month == 8 | dtMonth.Month == 9)
                            {
                            }
                            else
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 10)
                        {
                            if (dtMonth.Month == 11 | dtMonth.Month == 12 | dtMonth.Month == 1 | dtMonth.Month == 2 | dtMonth.Month == 3)
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 11)
                        {
                            if (dtMonth.Month == 12 | dtMonth.Month == 1 | dtMonth.Month == 2 | dtMonth.Month == 3)
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 12)
                        {
                            if (dtMonth.Month == 1 | dtMonth.Month == 2 | dtMonth.Month == 3)
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 1)
                        {
                            if (dtMonth.Month == 2 | dtMonth.Month == 3)
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 2)
                        {
                            if (dtMonth.Month == 3)
                                dSalAmt = Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);
                        }
                        else if (dtMonthName.Month == 3)
                        {
                        }
                        dMonthAmt[iCount] = dSalAmt;
                        dBasicAmt[iCount] = dSalAmt * 0.6;
                        dHRAAmt[iCount] = dSalAmt * 0.3;
                        dTransAmt[iCount] = dSalAmt * 0.1;
                        dPFAmt[iCount] = dBasicAmt[iCount] * 0.12;

                        dTotSalAmt = dTotSalAmt + Math.Round(dHourRate * iTotHours, 0, MidpointRounding.AwayFromZero);

                        dtMonth = dtMonth.AddMonths(1);
                        DateTime.TryParse("1/" + dtMonth.Month + "/" + dtMonth.Year, out dtMonth);
                        if (dtMonth > dtTDate | bRegin == true & dtMonth > dtRDate)
                            break;
                        iTotHours = fnGetTotalMonthHours(Convert.ToInt32(lEmpID), dtMonth, dtJDate, dtRDate);
                        double.TryParse(clsDataBaseHelper.fnGetOther_FieldName("emp_salary", "hourly_rate", "emp_id", lEmpID.ToString(), " and isdeleted=0 and id in(select max(id) from emp_salary where isdeleted=0 and emp_id=" + lEmpID + " and doc_date<='" + dtMonth.ToString("dd/MMM/yyyy") + "')"), out dHourRate);
                    }

                    obj.dBasic = Math.Round(dTotSalAmt * 0.6, MidpointRounding.AwayFromZero);
                    obj.dHRA = Math.Round(dTotSalAmt * 0.3, MidpointRounding.AwayFromZero);
                    obj.dTransport = Math.Round(dTotSalAmt * 0.1, MidpointRounding.AwayFromZero);
                    obj.dLic = dLICAmt;
                    obj.dEPF = Math.Round(obj.dBasic * 0.12, MidpointRounding.AwayFromZero);

                    obj.dMedcalAmt = Math.Round(dTotSalAmt * 0.05, 0, MidpointRounding.AwayFromZero);
                    obj.dBonusAmt = Math.Round(dTotSalAmt * 0.0833, MidpointRounding.AwayFromZero);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        public static DataTable prcSalaryDetails(DateTime dtMonthName)
        {
            DataTable dt = new DataTable();
            try
            {

                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("Employee Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Employee Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Basic Pay", typeof(string)));
                dt.Columns.Add(new DataColumn("House Rent Allowance", typeof(string)));
                dt.Columns.Add(new DataColumn("Transport/ CEA / LTA etc", typeof(string)));
                dt.Columns.Add(new DataColumn("LIC", typeof(string)));
                dt.Columns.Add(new DataColumn("EPF (CEDPA's contribution)", typeof(string)));
                dt.Columns.Add(new DataColumn("Medical Reimbursements", typeof(string)));
                dt.Columns.Add(new DataColumn("Bonus", typeof(string)));
                dt.Columns.Add(new DataColumn("Gross Salary", typeof(string)));
                int iSerialNo = 0;
                DataSet dsEmp = clsDataBaseHelper.ExecuteDataSet("select master_emp.id,master_emp.emp_name,master_emp.emp_code,master_emp.doj,master_emp.dor from master_emp where master_emp.isdeleted = 0 and master_emp.id not in (37,38,39,40,53) order by master_emp.emp_name");
                if (dsEmp.Tables[0].Rows.Count > 0)
                {
                    for (int iCount = 0; iCount <= dsEmp.Tables[0].Rows.Count; iCount++)
                    {
                        dr = dt.NewRow();
                        iSerialNo = iSerialNo + 1;
                        dr["RowNumber"] = iSerialNo;
                        dr["Employee Code"] = dsEmp.Tables[0].Rows[iCount]["emp_code"];
                        dr["Employee Name"] = dsEmp.Tables[0].Rows[iCount]["emp_name"];

                        long EMPID = 0;
                        DateTime DOJ, DOR;
                        long.TryParse(dsEmp.Tables[0].Rows[iCount]["id"].ToString(), out EMPID);
                        DateTime.TryParse(dsEmp.Tables[0].Rows[iCount]["DOJ"].ToString(), out DOJ);
                        DateTime.TryParse(dsEmp.Tables[0].Rows[iCount]["DOR"].ToString(), out DOR);

                        ReimEligibility obj = new ReimEligibility();
                        obj = prcCalculateReimEligibility(EMPID, dtMonthName, DOJ, DOR);
                        dr["Basic Pay"] = obj.dBasic;
                        dr["House Rent Allowance"] = obj.dHRA;
                        dr["Transport/ CEA / LTA etc"] = obj.dTransport;
                        dr["LIC"] = obj.dLic;
                        dr["EPF (CEDPA's contribution)"] = obj.dEPF;
                        dr["Medical Reimbursements"] = obj.dMedcalAmt;
                        dr["Bonus"] = obj.dBonusAmt;
                        dr["Gross Salary"] = obj.dBasic + obj.dHRA + obj.dTransport + obj.dEPF + obj.dLic + obj.dMedcalAmt + obj.dBonusAmt;
                        dt.Rows.Add(dr);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static void AdjustLeaveType(long lEmpID, DateTime dtDate)
        {
            string SQL = "";
            long TodatBalance = 0;
            ArrayList ArStr = new ArrayList();
            try
            {
                // checking 4 Annual Leave(AL) balance
                TodatBalance = Common_SPU.fnGetLeaveBalanceByLeaveID(lEmpID, "4", dtDate.Month, dtDate.Year);

                SQL = @"select distinct leave_log.ID  from leavedet_log 
                        inner join leave_log on leavedet_log.leavelog_id=leave_log.id 
                        where leave_log.isdeleted=0 and leavedet_log.isdeleted=0 and AttachmentRequired=1 and leavedet_log.leave_ID=1
                        and Month(leavedet_log.date)=" + dtDate.Month + " and Year(leavedet_log.date)=" + dtDate.Year + " and EMP_ID=" + lEmpID;

                DataSet ds = clsDataBaseHelper.ExecuteDataSet(SQL);
                int TotalCount = ds.Tables[0].Rows.Count;
                if (TotalCount < TodatBalance)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        Common_SPU.fnSetMakeCopyLeaveRequest(Convert.ToInt64(item["id"]), 4);
                    }
                }
                else
                {
                    long remainingBalce = TodatBalance;
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        if (remainingBalce != 0)
                        {
                            Common_SPU.fnSetMakeCopyLeaveRequest(Convert.ToInt64(item["id"]), 4);
                        }
                        else
                        {
                            Common_SPU.fnSetMakeCopyLeaveRequest(Convert.ToInt64(item["id"]), 3);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during AdjustLeaveType. The query was executed :", ex.ToString(), "spu_GetLeaveStatus", "LeaveModal", "LeaveModal", "");
            }
        }

        public static bool IsActivityLogFilled(long EMPID, long LeaveLogID)
        {
            bool Yes = false;
            DateTime StartDate, dtALSubmitDate;
            string SQL = "select start_Date from Leave_log where ID=" + LeaveLogID + " and isdeleted=0";
            DateTime.TryParse(clsDataBaseHelper.ExecuteSingleResult(SQL), out StartDate);

            DateTime.TryParse(clsDataBaseHelper.fnGetOther_FieldName("master_emp left join active_log on master_emp.id=active_log.emp_id and active_log.isdeleted=0", "isnull(max(dateadd(day,-1,dateadd(MM,1,datefromparts(year,month,1)))),dateadd(day,-1,doj))", "master_emp.id", EMPID.ToString(), " and master_emp.isdeleted=0 group by master_emp.id,master_emp.doj"), out dtALSubmitDate);
            if (StartDate.Date <= dtALSubmitDate.Date)
            {
                Yes = true;
            }
            return Yes;
        }

        public static List<DateTime> GetLeaveList(List<DateTime> dates)
        {

            List<DateTime> _dateTimes = new List<DateTime>();

            try
            {
               
                dates.Sort();
                //this will hold the resulted groups
                var groups = new List<List<DateTime>>();
                // the group for the first element
                var group1 = new List<DateTime>() { dates[0] };
                groups.Add(group1);

                DateTime lastDate = Convert.ToDateTime(dates[0].ToString("dd/MMM/yyyy"));
                for (int i = 1; i < dates.Count; i++)
                {
                    DateTime currDate = Convert.ToDateTime(dates[i].ToString("dd/MMM/yyyy"));
                    TimeSpan timeDiff = currDate - lastDate;
                    //should we create a new group?
                    bool isNewGroup = timeDiff.Days > 1;
                    if (isNewGroup)
                    {
                        groups.Add(new List<DateTime>());
                    }
                    groups.Last().Add(currDate);
                    lastDate = currDate;
                }
              
                int count = 0;
                for (int i = 0; i < groups.Count; i++)
                {
                    var item = groups[i].Count >= 3;
                    if (item == true)
                    {

                        break;
                    }
                    count++;
                }
                _dateTimes = groups[count];




            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveList. The query was executed :", ex.ToString(), "", "CommonSpecial", "CommonSpecial", "");
            }

          
            return _dateTimes;

        }
    }

}