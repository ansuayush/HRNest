using Mitr.CommonClass;
using Mitr.CommonLib;
using Mitr.Model;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.IO;
using OfficeOpenXml;
using System.Web.Script.Serialization;
using DropDownList = Mitr.Models.DropDownList;
using static Mitr.Models.MPR;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    // Test
    public class ActivityController : Controller
    {
        IActivityHelper Activity;
        IExportHelper Export;
        IBudgetHelper Budget;
        ILeaveHelper Leave;
        ISallaryProcess sallaryProcess;

        long LoginID = 0, EmpID = 0;
        public ActivityController()
        {
            Activity = new ActivityModal();
            Export = new ExportModal();
            Budget = new BudgetModel();
            Leave = new LeaveModal();
            sallaryProcess = new SallaryProcessModal();
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long.TryParse(clsApplicationSetting.GetSessionValue("EmpID"), out EmpID);
        }

        private string GetDateError_Daily(DailyLogCompleteModal Modal, DateTime SelectedDate)
        {
            string ValidateDates = "";
            double Day1 = Modal.DailyLogList.Sum(x => x.Day1);
            double Day2 = Modal.DailyLogList.Sum(x => x.Day2);
            double Day3 = Modal.DailyLogList.Sum(x => x.Day3);
            double Day4 = Modal.DailyLogList.Sum(x => x.Day4);
            double Day5 = Modal.DailyLogList.Sum(x => x.Day5);
            double Day6 = Modal.DailyLogList.Sum(x => x.Day6);
            double Day7 = Modal.DailyLogList.Sum(x => x.Day7);
            double Day8 = Modal.DailyLogList.Sum(x => x.Day8);
            double Day9 = Modal.DailyLogList.Sum(x => x.Day9);
            double Day10 = Modal.DailyLogList.Sum(x => x.Day10);
            double Day11 = Modal.DailyLogList.Sum(x => x.Day11);
            double Day12 = Modal.DailyLogList.Sum(x => x.Day12);
            double Day13 = Modal.DailyLogList.Sum(x => x.Day13);
            double Day14 = Modal.DailyLogList.Sum(x => x.Day14);
            double Day15 = Modal.DailyLogList.Sum(x => x.Day15);
            double Day16 = Modal.DailyLogList.Sum(x => x.Day16);
            double Day17 = Modal.DailyLogList.Sum(x => x.Day17);
            double Day18 = Modal.DailyLogList.Sum(x => x.Day18);
            double Day19 = Modal.DailyLogList.Sum(x => x.Day19);
            double Day20 = Modal.DailyLogList.Sum(x => x.Day20);
            double Day21 = Modal.DailyLogList.Sum(x => x.Day21);
            double Day22 = Modal.DailyLogList.Sum(x => x.Day22);
            double Day23 = Modal.DailyLogList.Sum(x => x.Day23);
            double Day24 = Modal.DailyLogList.Sum(x => x.Day24);
            double Day25 = Modal.DailyLogList.Sum(x => x.Day25);
            double Day26 = Modal.DailyLogList.Sum(x => x.Day26);
            double Day27 = Modal.DailyLogList.Sum(x => x.Day27);
            double Day28 = Modal.DailyLogList.Sum(x => x.Day28);
            double Day29 = Modal.DailyLogList.Sum(x => x.Day29);
            double Day30 = Modal.DailyLogList.Sum(x => x.Day30);
            double Day31 = Modal.DailyLogList.Sum(x => x.Day31);

            DateTime StartDate = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
            DateTime Doj, LastWorkingDay;
            string MonthYear = StartDate.ToString("yyyy-MM");
            string FixHolidays = clsApplicationSetting.GetConfigValue("FixHolidays");
            List<HolidayDailyLog> LeaveList = new List<HolidayDailyLog>();
            LeaveList = Activity.GetHolidayDailyLog(SelectedDate);
            double WorkingHours = Convert.ToDouble(Modal.WorkingHours);
            Doj = Modal.Doj;
            LastWorkingDay = Modal.LastWorkingDay;
            //if (LeaveList.Where(x => x.Date == date.ToString("dd/MM/yyyy") && !FixHolidays.Contains(date.ToString("dddd")))

            for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                bool isvalid = true;
                if (LeaveList.Any(x => x.Date == date.ToString("dd/MM/yyyy")) || FixHolidays.Contains(date.ToString("dddd")))
                {
                    isvalid = false;
                }
                if (isvalid)
                {
                    int DateError = 0;
                    int.TryParse(date.ToString("dd"), out DateError);
                    switch (DateError)
                    {
                        case 1:

                            if ((Day1 < WorkingHours || WorkingHours == 0 || Day1 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "1 ,";
                            }
                            break;
                        case 2:
                            if ((Day2 < WorkingHours || WorkingHours == 0 || Day2 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "2 ,";
                            }
                            break;
                        case 3:
                            if ((Day3 < WorkingHours || WorkingHours == 0 || Day3 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "3 ,";
                            }
                            break;
                        case 4:
                            if ((Day4 < WorkingHours || WorkingHours == 0 || Day4 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "4 ,";
                            }
                            break;
                        case 5:
                            if ((Day5 < WorkingHours || WorkingHours == 0 || Day5 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "5 ,";
                            }
                            break;
                        case 6:
                            if ((Day6 < WorkingHours || WorkingHours == 0 || Day6 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "6 ,";
                            }
                            break;
                        case 7:
                            if ((Day7 < WorkingHours || WorkingHours == 0 || Day7 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "7 ,";
                            }
                            break;
                        case 8:
                            if ((Day8 < WorkingHours || WorkingHours == 0 || Day8 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "8 ,";
                            }
                            break;
                        case 9:
                            if ((Day9 < WorkingHours || WorkingHours == 0 || Day9 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "9 ,";
                            }
                            break;
                        case 10:
                            if ((Day10 < WorkingHours || WorkingHours == 0 || Day10 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "10 ,";
                            }
                            break;
                        case 11:
                            if ((Day11 < WorkingHours || WorkingHours == 0 || Day11 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "11 ,";
                            }
                            break;
                        case 12:
                            if ((Day12 < WorkingHours || WorkingHours == 0 || Day12 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "12 ,";
                            }
                            break;
                        case 13:
                            if ((Day13 < WorkingHours || WorkingHours == 0 || Day13 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "13 ,";
                            }
                            break;
                        case 14:
                            if ((Day14 < WorkingHours || WorkingHours == 0 || Day14 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "14 ,";
                            }
                            break;
                        case 15:
                            if ((Day15 < WorkingHours || WorkingHours == 0 || Day15 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "15 ,";
                            }
                            break;
                        case 16:
                            if ((Day16 < WorkingHours || WorkingHours == 0 || Day16 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "16 ,";
                            }
                            break;
                        case 17:
                            if ((Day17 < WorkingHours || WorkingHours == 0 || Day17 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "17 ,";
                            }
                            break;
                        case 18:
                            if ((Day18 < WorkingHours || WorkingHours == 0 || Day18 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "18 ,";
                            }
                            break;
                        case 19:
                            if ((Day19 < WorkingHours || WorkingHours == 0 || Day19 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "19 ,";
                            }
                            break;
                        case 20:
                            if ((Day20 < WorkingHours || WorkingHours == 0 || Day20 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "20 ,";
                            }
                            break;
                        case 21:
                            if ((Day21 < WorkingHours || WorkingHours == 0 || Day21 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "21 ,";
                            }
                            break;
                        case 22:
                            if ((Day22 < WorkingHours || WorkingHours == 0 || Day22 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "22 ,";
                            }
                            break;
                        case 23:
                            if ((Day23 < WorkingHours || WorkingHours == 0 || Day23 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "23 ,";
                            }
                            break;
                        case 24:
                            if ((Day24 < WorkingHours || WorkingHours == 0 || Day24 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "24 ,";
                            }
                            break;
                        case 25:
                            if ((Day25 < WorkingHours || WorkingHours == 0 || Day25 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "25 ,";
                            }
                            break;
                        case 26:
                            if ((Day26 < WorkingHours || WorkingHours == 0 || Day26 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "26 ,";
                            }
                            break;
                        case 27:
                            if ((Day27 < WorkingHours || WorkingHours == 0 || Day27 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "27 ,";
                            }
                            break;
                        case 28:
                            if ((Day28 < WorkingHours || WorkingHours == 0 || Day28 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "28 ,";
                            }
                            break;
                        case 29:
                            if ((Day29 < WorkingHours || WorkingHours == 0 || Day29 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "29 ,";
                            }
                            break;
                        case 30:
                            if ((Day30 < WorkingHours || WorkingHours == 0 || Day30 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "30 ,";
                            }
                            break;
                        case 31:
                            if ((Day31 < WorkingHours || WorkingHours == 0 || Day31 > 24) && Doj < Convert.ToDateTime(MonthYear + "-" + DateError.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + DateError.ToString()))
                            {
                                ValidateDates += "31 ,";
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return ValidateDates.Trim().TrimEnd(',');
        }
        // New Action for Activity Log By Ravi
        public ActionResult _HoursSummary(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            DateTime MyDate;
            DateTime.TryParse(GetQueryString[2], out MyDate);
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            DataSet Ds = new DataSet();
            Ds = Common_SPU.fnGetHoursSummary(EMPID, MyDate.Month, MyDate.Year);
            ViewBag.ShowRecall = Activity.GetDailyLogSetting(MyDate);
            return PartialView(Ds);
        }
        public ActionResult DailyLog(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DailyLogMonth Modal = new DailyLogMonth();
            DateTime MyDate = DateTime.Now;
            if (GetQueryString.Length > 2)
            {
                DateTime.TryParse(GetQueryString[2], out MyDate);
            }
            Modal.Month = MyDate.ToString("yyyy-MM");
            return View(Modal);
        }
        [HttpPost]

        public ActionResult _DailyLog(DailyLogMonth Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(Modal.Month, out MyDate);

            DailyLogCompleteModal modal = new DailyLogCompleteModal();
            ViewBag.SelectedDate = MyDate;
            modal = Activity.DailyLogCompleteModal(MyDate);
            ViewBag.GetHolidayDailyLog = Activity.GetHolidayDailyLog(MyDate);
            ViewBag.GetTravelDailyLog = Activity.GetTravelDailyLog(MyDate);
            modal.SelectedDate = MyDate;
            return PartialView(modal);
        }
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AddProject(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            ViewBag.SelectedDate = GetQueryString[2];
            AddActivityProjectList Modal = new AddActivityProjectList();
            DateTime MyDate = DateTime.Now;
            if (GetQueryString.Length > 2)
            {
                DateTime.TryParse(GetQueryString[2], out MyDate);
            }
            Modal = Activity.AddProjectList(MyDate);
            return PartialView(Modal);

        }

        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public JsonResult SubmitActiveLog(FormCollection Collection, string src, string Command)
        {
            PostResponse result = new PostResponse();
            result.SuccessMessage = "Monthly Activity Log not Submitted";
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            DateTime Month;
            string DateError = "";
            string HourWorked = "0", LeaveAvailed = "0", LeavePendingForApproval = "0", CompensatoryHours = "0", CompensatoryHoursApproved = "0", txtCC = "";
            DateTime Doj, LastWorkingDay;
            if (Command == "Submit")
            {
                string FixHolidays = clsApplicationSetting.GetConfigValue("FixHolidays");
                Month = Convert.ToDateTime(Collection.GetValue("hdnSelectedDate").AttemptedValue);
                DateTime StartDate = new DateTime(Month.Year, Month.Month, 1);
                DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);

                HourWorked = Collection.GetValue("HourWorked").AttemptedValue;
                LeaveAvailed = Collection.GetValue("LeaveAvailed").AttemptedValue;
                LeavePendingForApproval = Collection.GetValue("LeavePendingForApproval").AttemptedValue;
                CompensatoryHours = Collection.GetValue("CompensatoryHours").AttemptedValue;
                CompensatoryHoursApproved = Collection.GetValue("CompensatoryHoursApproved").AttemptedValue;
                txtCC = Collection.GetValue("txtCC").AttemptedValue;
                Doj = Convert.ToDateTime(Collection.GetValue("txtDoj").AttemptedValue);
                LastWorkingDay = Convert.ToDateTime(Collection.GetValue("txtLastWorkingDay").AttemptedValue);
                double WorkingHours = Activity.GetEmpWorkingHours();
                List<HolidayDailyLog> LeaveList = new List<HolidayDailyLog>();
                LeaveList = Activity.GetHolidayDailyLog(StartDate);

                int count = 0;
                for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
                {
                    count++;
                    double hdnGrandTotal_ = 0;
                    double.TryParse(Collection.GetValue("hdnGrandTotal_" + count).AttemptedValue, out hdnGrandTotal_);
                    if (hdnGrandTotal_ < WorkingHours || WorkingHours == 0 || hdnGrandTotal_ > 24)
                    {
                        bool isHoliday = false;
                        if (LeaveList.Any(x => x.Date == date.ToString("dd/MM/yyyy")))
                        {
                            if (LeaveList.Where(x => x.Date == date.ToString("dd/MM/yyyy")).Select(x => x.HolidayType).FirstOrDefault() == "Holiday")
                            {
                                isHoliday = true;
                            }
                        }
                        if (!FixHolidays.Contains(date.ToString("dddd")) && !isHoliday && Doj < date && LastWorkingDay >= date)
                        {
                            DateError += count + ", ";
                        }
                    }
                }
                if (DateError == "")
                {
                    long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
                    int EntryCount = 0;
                    foreach (DataRow item in Common_SPU.fnGetActivityLog(EMPID, Month.Month, Month.Year).Tables[0].Select("doc_type='A'"))
                    {
                        EntryCount++;
                        Common_SPU.fnSetActiveLog("0", item["emp_id"].ToString(), item["ActivityID"].ToString(), item["description"].ToString(), Month.Month.ToString(), Month.Year.ToString(), txtCC, item["proj_id"].ToString(),
                            item["day1"].ToString(), item["day2"].ToString(), item["day3"].ToString(), item["day4"].ToString(), item["day5"].ToString(), item["day6"].ToString(), item["day7"].ToString(), item["day8"].ToString(), item["day9"].ToString(), item["day10"].ToString(),
                            item["day11"].ToString(), item["day12"].ToString(), item["day13"].ToString(), item["day14"].ToString(), item["day15"].ToString(), item["day16"].ToString(), item["day17"].ToString(), item["day18"].ToString(), item["day19"].ToString(), item["day20"].ToString(),
                            item["day21"].ToString(), item["day22"].ToString(), item["day23"].ToString(), item["day24"].ToString(), item["day25"].ToString(), item["day26"].ToString(), item["day27"].ToString(), item["day28"].ToString(), item["day29"].ToString(), item["day30"].ToString(), item["day31"].ToString(),
                            item["Total"].ToString(),
                            EntryCount, HourWorked, LeaveAvailed, CompensatoryHours);

                        result.Status = true;
                        result.SuccessMessage = "Your monthly activity log submitted successfully";
                        result.RedirectURL = "/Activity/DailyLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/DailyLog*" + Month.ToString("yyyy-MM"));

                    }
                    //if (EntryCount > 0)
                    //{
                    //    string SQL = "UPDATE active_log SET ISDELETED = 1,deletedat=CURRENT_TIMESTAMP WHERE ISDELETED=0 and EMP_ID = " + EMPID.ToString() + " AND month = " + Month.Month + " AND year = " + Month.Year + " AND srno> " + EntryCount + "";
                    //    clsDataBaseHelper.ExecuteNonQuery(SQL);
                    //}                    
                    // fire mail
                    Common_SPU.fnCreateMail_ActivityLog(Month.Month, Month.Year, EMPID);
                }
                else if (DateError != "")
                {
                    result.Status = false;
                    result.SuccessMessage = "Working hours can not be less then 8 hours Or can not be Greater than 24 hours, Please Verify " + DateError;
                }

            }
            TempData["Success"] = (result.Status ? "Y" : "N");
            TempData["SuccessMsg"] = result.SuccessMessage;
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public JsonResult _SaveDailyLog(DailyLogCompleteModal Modal, string src, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int TotalLeaveWithAttachmentPending = 0;
            int.TryParse(GetQueryString[2], out TotalLeaveWithAttachmentPending);
            DateTime SelectedDate = DateTime.Now.Date;
            if (GetQueryString.Length > 3)
            {
                DateTime.TryParse(GetQueryString[3], out SelectedDate);
            }

            long SaveID = 0, EMPID = 0;
            double Total = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            if (Modal.DailyLogList == null && Command != "Recall")
            {
                PostResult.SuccessMessage = "Log Table Can't be blank";
                ModelState.AddModelError("SelectedDate", PostResult.SuccessMessage);

            }
            if (ModelState.IsValid)
            {
                if (Command == "Recall")
                {
                    Common_SPU.fnSetRecallAcivityLog(Modal.SelectedDate.Month, Modal.SelectedDate.Year, Modal.RecallRemarks);
                    PostResult.Status = true;
                    PostResult.SuccessMessage = "Successfully recalled";
                    PostResult.RedirectURL = "/Activity/DailyLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/DailyLog*" + SelectedDate.ToString("yyyy-MM"));
                    PostResult.ID = SaveID;
                }
                else if (Command == "Save")
                {
                    PostResult.SuccessMessage = "Activity log for the month submitted Not Saved";
                    int TotalSRNO = 0;

                    for (int i = 0; i < Modal.DailyLogList.Count; i++)
                    {
                        Total = (Modal.DailyLogList[i].Day1 + Modal.DailyLogList[i].Day2 + Modal.DailyLogList[i].Day3 + Modal.DailyLogList[i].Day4 + Modal.DailyLogList[i].Day5 + Modal.DailyLogList[i].Day6 + Modal.DailyLogList[i].Day7 + Modal.DailyLogList[i].Day8 + Modal.DailyLogList[i].Day9 + Modal.DailyLogList[i].Day10 +
                              Modal.DailyLogList[i].Day11 + Modal.DailyLogList[i].Day12 + Modal.DailyLogList[i].Day13 + Modal.DailyLogList[i].Day14 + Modal.DailyLogList[i].Day15 + Modal.DailyLogList[i].Day16 + Modal.DailyLogList[i].Day17 + Modal.DailyLogList[i].Day18 + Modal.DailyLogList[i].Day19 + Modal.DailyLogList[i].Day20 +
                              Modal.DailyLogList[i].Day21 + Modal.DailyLogList[i].Day22 + Modal.DailyLogList[i].Day23 + Modal.DailyLogList[i].Day24 + Modal.DailyLogList[i].Day25 + Modal.DailyLogList[i].Day26 + Modal.DailyLogList[i].Day27 + Modal.DailyLogList[i].Day28 + Modal.DailyLogList[i].Day29 + Modal.DailyLogList[i].Day30 +
                              Modal.DailyLogList[i].Day31);

                        SaveID = Common_SPU.fnSetDailyLog(Modal.DailyLogList[i].DailyLogID.ToString(), Modal.SelectedDate.Month, Modal.SelectedDate.Year,
                            Modal.DailyLogList[i].proj_id.ToString(), Modal.DailyLogList[i].description,
                              Modal.DailyLogList[i].Day1, Modal.DailyLogList[i].Day2, Modal.DailyLogList[i].Day3, Modal.DailyLogList[i].Day4, Modal.DailyLogList[i].Day5, Modal.DailyLogList[i].Day6, Modal.DailyLogList[i].Day7, Modal.DailyLogList[i].Day8, Modal.DailyLogList[i].Day9, Modal.DailyLogList[i].Day10,
                              Modal.DailyLogList[i].Day11, Modal.DailyLogList[i].Day12, Modal.DailyLogList[i].Day13, Modal.DailyLogList[i].Day14, Modal.DailyLogList[i].Day15, Modal.DailyLogList[i].Day16, Modal.DailyLogList[i].Day17, Modal.DailyLogList[i].Day18, Modal.DailyLogList[i].Day19, Modal.DailyLogList[i].Day20,
                              Modal.DailyLogList[i].Day21, Modal.DailyLogList[i].Day22, Modal.DailyLogList[i].Day23, Modal.DailyLogList[i].Day24, Modal.DailyLogList[i].Day25, Modal.DailyLogList[i].Day26, Modal.DailyLogList[i].Day27, Modal.DailyLogList[i].Day28, Modal.DailyLogList[i].Day29, Modal.DailyLogList[i].Day30,
                              Modal.DailyLogList[i].Day31, Total, i + 1, Modal.DailyLogList[i].TravelrequestID, Modal.DailyLogList[i].ActivityID);

                        TotalSRNO = TotalSRNO + 1;

                        clsDataBaseHelper.ExecuteNonQuery("UPDATE daily_log SET ISDELETED=1 WHERE TravelrequestID=" + 0 + " and  EMP_ID=" + EMPID + " AND month=" + Modal.SelectedDate.Month.ToString() + " and year=" + Modal.SelectedDate.Year.ToString() + " AND srno>" + i + 1);
                        //clsDataBaseHelper.ExecuteNonQuery("UPDATE daily_log SET ISDELETED=1 WHERE EMP_ID=" + EMPID + " AND month=" + Modal.SelectedDate.Month.ToString() + " and year=" + Modal.SelectedDate.Year.ToString() + " AND srno>" + i);


                    }
                    Common_SPU.fnSetUpdateSrNumber(EMPID, Convert.ToInt64(Modal.SelectedDate.Month.ToString()), Convert.ToInt64(Modal.SelectedDate.Year.ToString()));
                    CommonSpecial.prcUpdateCompOff(Modal.SelectedDate, EMPID);
                    //Common_SPU.fnSetUpdateCompOff(Modal.SelectedDate.Month, Modal.SelectedDate.Year);
                    //  clsDataBaseHelper.ExecuteNonQuery("UPDATE daily_log SET ISDELETED=1 WHERE EMP_ID=" + EMPID + " AND month=" + Modal.SelectedDate.Month.ToString() + " and year=" + Modal.SelectedDate.Year.ToString() + " AND srno>" + TotalSRNO );
                    if (SaveID > 0)
                    {
                        if (Modal.IsLoad == 0)
                        {
                            PostResult.Status = true;
                            PostResult.SuccessMessage = "Activity log saved successfully";
                            PostResult.RedirectURL = "/Activity/DailyLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/DailyLog*" + SelectedDate.ToString("yyyy-MM"));
                            PostResult.ID = SaveID;
                        }
                        else
                        {
                            PostResult.Status = true;
                            PostResult.SuccessMessage = "Activity log saved successfully";
                            PostResult.RedirectURL = "/Activity/DailyLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/DailyLog*" + SelectedDate.ToString("yyyy-MM"));
                            PostResult.ID = SaveID;
                            PostResult.OtherID = Modal.IsLoad;
                        }

                    }
                }
                else if (Command == "VerifyAndSave")
                {
                    PostResult.StatusCode = 1;
                    PostResult.SuccessMessage = "Activity log for the month submitted Not Saved";
                    string ValidateDates = GetDateError_Daily(Modal, SelectedDate);
                    int TotalSRNO = 0;
                    if (string.IsNullOrEmpty(ValidateDates))
                    {
                        if (TotalLeaveWithAttachmentPending > 0)
                        {
                            CommonSpecial.AdjustLeaveType(EMPID, Modal.SelectedDate);
                        }
                        for (int i = 0; i < Modal.DailyLogList.Count; i++)
                        {
                            Total = (Modal.DailyLogList[i].Day1 + Modal.DailyLogList[i].Day2 + Modal.DailyLogList[i].Day3 + Modal.DailyLogList[i].Day4 + Modal.DailyLogList[i].Day5 + Modal.DailyLogList[i].Day6 + Modal.DailyLogList[i].Day7 + Modal.DailyLogList[i].Day8 + Modal.DailyLogList[i].Day9 + Modal.DailyLogList[i].Day10 +
                                  Modal.DailyLogList[i].Day11 + Modal.DailyLogList[i].Day12 + Modal.DailyLogList[i].Day13 + Modal.DailyLogList[i].Day14 + Modal.DailyLogList[i].Day15 + Modal.DailyLogList[i].Day16 + Modal.DailyLogList[i].Day17 + Modal.DailyLogList[i].Day18 + Modal.DailyLogList[i].Day19 + Modal.DailyLogList[i].Day20 +
                                  Modal.DailyLogList[i].Day21 + Modal.DailyLogList[i].Day22 + Modal.DailyLogList[i].Day23 + Modal.DailyLogList[i].Day24 + Modal.DailyLogList[i].Day25 + Modal.DailyLogList[i].Day26 + Modal.DailyLogList[i].Day27 + Modal.DailyLogList[i].Day28 + Modal.DailyLogList[i].Day29 + Modal.DailyLogList[i].Day30 +
                                  Modal.DailyLogList[i].Day31);

                            SaveID = Common_SPU.fnSetDailyLog(Modal.DailyLogList[i].DailyLogID.ToString(), Modal.SelectedDate.Month, Modal.SelectedDate.Year,
                                Modal.DailyLogList[i].proj_id.ToString(), Modal.DailyLogList[i].description,
                                  Modal.DailyLogList[i].Day1, Modal.DailyLogList[i].Day2, Modal.DailyLogList[i].Day3, Modal.DailyLogList[i].Day4, Modal.DailyLogList[i].Day5, Modal.DailyLogList[i].Day6, Modal.DailyLogList[i].Day7, Modal.DailyLogList[i].Day8, Modal.DailyLogList[i].Day9, Modal.DailyLogList[i].Day10,
                                  Modal.DailyLogList[i].Day11, Modal.DailyLogList[i].Day12, Modal.DailyLogList[i].Day13, Modal.DailyLogList[i].Day14, Modal.DailyLogList[i].Day15, Modal.DailyLogList[i].Day16, Modal.DailyLogList[i].Day17, Modal.DailyLogList[i].Day18, Modal.DailyLogList[i].Day19, Modal.DailyLogList[i].Day20,
                                  Modal.DailyLogList[i].Day21, Modal.DailyLogList[i].Day22, Modal.DailyLogList[i].Day23, Modal.DailyLogList[i].Day24, Modal.DailyLogList[i].Day25, Modal.DailyLogList[i].Day26, Modal.DailyLogList[i].Day27, Modal.DailyLogList[i].Day28, Modal.DailyLogList[i].Day29, Modal.DailyLogList[i].Day30,
                                  Modal.DailyLogList[i].Day31, Total, i + 1, Modal.DailyLogList[i].TravelrequestID, Modal.DailyLogList[i].ActivityID);
                            TotalSRNO = TotalSRNO + 1;

                            //clsDataBaseHelper.ExecuteNonQuery("UPDATE daily_log SET ISDELETED=1 WHERE EMP_ID=" + EMPID + " AND month=" + Modal.DailyLogList[i].month + " and year=" + Modal.DailyLogList[i].year + " AND srno>" + i);
                            //   CommonSpecial.prcUpdateCompOff(Modal.SelectedDate, EMPID);
                            clsDataBaseHelper.ExecuteNonQuery("UPDATE daily_log SET ISDELETED=1 WHERE TravelrequestID=" + 0 + " and EMP_ID=" + EMPID + " AND month=" + Modal.DailyLogList[i].month + " and year=" + Modal.DailyLogList[i].year + " AND srno>" + i + 1);
                        }
                        Common_SPU.fnSetUpdateSrNumber(EMPID, Convert.ToInt64(Modal.SelectedDate.Month.ToString()), Convert.ToInt64(Modal.SelectedDate.Year.ToString()));
                        CommonSpecial.prcUpdateCompOff(Modal.SelectedDate, EMPID);
                        //Common_SPU.fnSetUpdateCompOff(Modal.SelectedDate.Month, Modal.SelectedDate.Year);
                        //  clsDataBaseHelper.ExecuteNonQuery("UPDATE daily_log SET ISDELETED=1 WHERE EMP_ID=" + EMPID + " AND month=" + Modal.SelectedDate.Month.ToString() + " and year=" + Modal.SelectedDate.Year.ToString() + " AND srno>" + TotalSRNO);
                        if (SaveID > 0)
                        {
                            PostResult.Status = true;
                            PostResult.StatusCode = 0; // add code by shailendra 03092024
                            PostResult.SuccessMessage = "Activity log submitted successfully";
                            PostResult.RedirectURL = "/Activity/MonthlyActiveLog?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/MonthlyActiveLog*" + SelectedDate.ToString("yyyy-MM"));
                            PostResult.ID = SaveID;
                            //TempData["Success"] = "Y";
                            //TempData["SuccessMsg"] = PostResult.SuccessMessage;
                        }
                    }
                    else
                    {

                        PostResult.SuccessMessage = ValidateDates;
                    }

                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _RequestForCompensatoryOff(string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<RequestCompensatoryOffList> ViweModal = new List<RequestCompensatoryOffList>();
            DateTime Date;
            DateTime.TryParse(GetQueryString[2], out Date);

            ViweModal = Activity.GetCompensatoryOffList(Date.Month, Date.Year);
            return PartialView(ViweModal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public JsonResult _ActionRequestForCompensatoryOff(string src, List<RequestCompensatoryOffList> Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            //long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Count = 0;
            bool status = false;
            string Msg = "Comp Off Request is not Saved";
            if (Modal.Any(x => x.Applied_hours > x.hours))
            {
                ModelState.AddModelError("Applied_hours", "");
                PostResult.SuccessMessage = "Hours Applied can not be greater than Hours Worked.";
            }
            if (ModelState.IsValid)
            {
                string CompDate = string.Empty;
                int CompHours = 0;
                if (Command == "Apply")
                {

                    foreach (var item in Modal)
                    {
                        if (!string.IsNullOrEmpty(item.CheckSelected))
                        {
                            Count++;
                            clsDataBaseHelper.ExecuteNonQuery("update compensatory_off set hours='" + item.Applied_hours + "', mailsent=1 where id=" + item.CompensatoryOffID + "");
                            if (string.IsNullOrEmpty(CompDate))
                            {
                                CompDate = item.Date;
                            }
                            else
                            {
                                CompDate = CompDate + "," + item.Date;
                            }
                            if (CompHours == 0)
                            {
                                CompHours = Convert.ToInt32(item.Applied_hours);
                            }
                            else
                            {
                                CompHours = CompHours + Convert.ToInt32(item.Applied_hours);
                            }

                            //Common_SPU.fnCreateMail_DailyActivityCompOff(item.Date, Convert.ToString(item.Applied_hours), item.CompensatoryOffID, Command, "");
                        }
                    }

                    if (Count > 0)
                    {
                        Common_SPU.fnCreateMail_DailyActivityCompOff(CompDate, Convert.ToString(CompHours), 0, Command, "");
                        status = true;
                        Msg = "Comp off request submitted";
                    }

                }
                if (status)
                {
                    //TempData["Success"] = "Y";
                    //TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;

                }
                else
                {
                    PostResult.Status = false;
                    PostResult.SuccessMessage = Msg;
                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        // Monthly Active Log
        public ActionResult MonthlyActiveLog(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now;
            if (GetQueryString.Length > 2)
            {
                DateTime.TryParse(GetQueryString[2], out MyDate);
            }
            DailyLogMonth Modal = new DailyLogMonth();
            Modal.Month = MyDate.ToString("yyyy-MM");
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _MonthlyActiveLog(DailyLogMonth Model, string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(Model.Month, out MyDate);
            ViewBag.SelectedDate = MyDate;
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);

            ViewBag.GetHolidayDailyLog = Activity.GetHolidayDailyLog(MyDate);

            ViewBag.IsMontlyLogSubmited = Activity.IsMontlyLogSubmited(EMPID, MyDate);
            DataSet Ds = new DataSet();
            if (!ViewBag.IsMontlyLogSubmited)
            {
                Ds = Common_SPU.fnGetActivityLog(EMPID, MyDate.Month, MyDate.Year);
            }
            string SQL = @"select isnull(remarks,'') from active_approve where emp_id=" + EMPID + " and isdeleted=1 and month=" + MyDate.Month + " and year=" + MyDate.Year + " order by id desc";
            ViewBag.ResubmitRemarks = clsDataBaseHelper.ExecuteSingleResult(SQL);
            return PartialView(Ds);
        }


        //[HttpPost]
        //public ActionResult _MonthlyActiveLog(DailyLogMonth Model, string src)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    ViewBag.SelectedDate = Convert.ToDateTime(Model.Month);
        //    DateTime firstDayOfMonth = new DateTime(Convert.ToDateTime(ViewBag.SelectedDate).Year, Convert.ToDateTime(ViewBag.SelectedDate).Month, 1);
        //    ViewBag.GetHolidayDailyLog = Activity.GetHolidayDailyLog(firstDayOfMonth);

        //    MonthlyLog Modal = new MonthlyLog();
        //    Modal = Activity.GetMonthlyLogData(firstDayOfMonth.ToString());
        //    return PartialView(Modal);
        //}




        public ActionResult _PendingLeaveDuringTheMonth(string src, MiscGetActiveLog Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet ds = new DataSet();
            DateTime MyDate;
            DateTime.TryParse(Modal.Date, out MyDate);
            ds = Common_SPU.fnGetPendingLeaveLog_Month(Modal.EMPID, MyDate.Month, MyDate.Year);
            return PartialView(ds);
        }

        public ActionResult ActivityLogApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now;
            if (GetQueryString.Length == 3)
            {
                DateTime.TryParse(GetQueryString[2], out MyDate);
            }
            InputDate Modal = new InputDate();
            Modal.Date = MyDate.ToString("yyyy-MM");
            Modal.Approve = "0";
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ActivityLogApproval(string src, InputDate Modal, string Command)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            List<MyTeamActiveLog> List = new List<MyTeamActiveLog>();
            DateTime mydate;
            DateTime.TryParse(Modal.Date, out mydate);
            ViewBag.SelectedDate = mydate;
            List = Activity.GetMyTeamActiveLogList(Modal.Approve, mydate.Month, mydate.Year);

            return PartialView(List);
        }

        // Submit Active Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _SubmitActiveLogApproval(string src, List<MyTeamActiveLog> Modal, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now;
            DateTime Doj = Convert.ToDateTime("1899-12-31");
            DateTime LastWorkingDay = Convert.ToDateTime("2100-12-31");
            string MonthYear = "";
            if (GetQueryString.Length == 3)
            {
                DateTime.TryParse(GetQueryString[2], out MyDate);
            }
            string InValidaDates = "";
            bool Status = false;
            string SuccessMessage = "Activity log Approval is not Saved";
            long lHODID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out lHODID);
            string FixHolidays = clsApplicationSetting.GetConfigValue("FixHolidays");
            if (ModelState.IsValid)
            {
                SeniorLeaveDashboard ModalLeave = new SeniorLeaveDashboard();
                long EMPID = 0;
                long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
                ModalLeave = Leave.GetSeniorLeaveDashboard(EMPID);


                if (Command == "Approved")
                {
                    DateTime StartDate = new DateTime(MyDate.Year, MyDate.Month, 1);
                    DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
                    MonthYear = StartDate.Year.ToString() + "-" + StartDate.Month.ToString();
                    List<HolidayDailyLog> LeaveList = new List<HolidayDailyLog>();
                    //   LeaveList = Activity.GetHolidayDailyLog(StartDate, "");
                    foreach (var item in Modal)
                    {

                        var data = ModalLeave.RequestLeaveList.Where(x => x.EMPID == item.EMPID && x.RFC == 1).FirstOrDefault();
                        if (data != null)
                        {
                            if (data.RFC == 1 && data.Approved == 1)
                            {
                                SuccessMessage = "First Approved RFC Employee Name:" + item.EMPName;
                                break;
                            }
                        }
                        LeaveList = Activity.GetHolidayDailyLog(StartDate, Convert.ToString(item.EMPID));
                        if (!string.IsNullOrEmpty(item.CheckSelected))
                        {
                            DataSet tempData = Common_SPU.fnGetActivityLog(item.EMPID, item.Month, item.Year);
                            if (tempData.Tables[1].Rows.Count > 0)
                            {
                                Doj = Convert.ToDateTime(tempData.Tables[1].Rows[0]["doj"].ToString());
                                LastWorkingDay = Convert.ToDateTime(tempData.Tables[1].Rows[0]["lastworking_day"].ToString());
                            }
                            foreach (DataRow dtRow in tempData.Tables[0].Select("doc_type='G'"))
                            {
                                foreach (DataColumn dc in tempData.Tables[0].Columns)
                                {
                                    if (dc.ColumnName.Contains("day"))
                                    {
                                        for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
                                        {
                                            if (dc.ColumnName == "day" + Convert.ToInt32(date.ToString("dd")))
                                            {
                                                int value = 0;
                                                int.TryParse(dtRow[dc].ToString(), out value);

                                                bool isHoliday = false;
                                                if (LeaveList.Any(x => x.Date == date.ToString("dd/MM/yyyy")))
                                                {
                                                    if (LeaveList.Where(x => x.Date == date.ToString("dd/MM/yyyy")).Select(x => x.HolidayType).FirstOrDefault() == "Holiday")
                                                    {
                                                        isHoliday = true;
                                                    }
                                                }
                                                if (FixHolidays.Contains(date.ToString("dddd")))
                                                {
                                                    isHoliday = true;
                                                }

                                                if (dtRow[dc].ToString() != "S" && value == 0 && !isHoliday && Doj < Convert.ToDateTime(MonthYear + "-" + date.Day.ToString()) && LastWorkingDay >= Convert.ToDateTime(MonthYear + "-" + date.Day.ToString()))
                                                {
                                                    InValidaDates += Convert.ToInt32(date.ToString("dd")) + " ,";

                                                }
                                                break;

                                            }
                                        }
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(InValidaDates))
                            {
                                Common_SPU.fnSetActiveApprove(0, item.EMPID, MyDate.Month, MyDate.Year, lHODID, item.ApprovedRemarks, Command);
                                // fire mail
                                Common_SPU.fnCreateMail_ActivityLog(MyDate.Month, MyDate.Year, item.EMPID);
                                Status = true;
                                SuccessMessage = "Activity log approved successfully";
                            }
                            else
                            {
                                Status = false;
                                SuccessMessage = item.EMPName + "'s Working hours can not be less then 8 hours Or can not be Greater than 24 hours, Please Verify " + InValidaDates.TrimEnd(',');
                                break;
                            }
                        }
                    }

                }
                if (Command == "Resubmit")
                {
                    foreach (var item in Modal)
                    {
                        if (!string.IsNullOrEmpty(item.CheckSelected))
                        {
                            Common_SPU.fnSetActiveApprove(0, item.EMPID, MyDate.Month, MyDate.Year, lHODID, item.ApprovedRemarks, Command);
                            // fire mail
                            Common_SPU.fnCreateMail_ActivityLog(MyDate.Month, MyDate.Year, item.EMPID);
                        }
                        Status = true;
                        SuccessMessage = "Activity log resubmitted successfully";
                    }
                }
                if (Status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = SuccessMessage;
                }
                else
                {
                    TempData["Success"] = "N";
                    TempData["SuccessMsg"] = SuccessMessage;
                }

            }

            return RedirectToAction("ActivityLogApproval", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/ActivityLogApproval*" + MyDate) });

        }


        public ActionResult CompensatoryOffApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            CompensatoryOffApproval Modal = new CompensatoryOffApproval();
            Modal.Approve = "0";
            Modal.Date = DateTime.Now.ToString("yyyy-MM");
            Modal.CompensatoryEmployee = CommonSpecial.GetEmployeeCompensatoryOFF();
            return View(Modal);
        }
        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _CompensatoryOffApproval(CompensatoryOffApproval Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            //DataSet ds = new DataSet();
            List<CompensatoryOffList> ViweModal = new List<CompensatoryOffList>();
            ViweModal = Activity.GetCompensatoryOffList(Modal.EMPID, Convert.ToDateTime(Modal.Date).Month, Convert.ToDateTime(Modal.Date).Year, Modal.Approve);

            //ds = Common_SPU.fnGetCompensatoryOff(Modal.EMPID, Convert.ToDateTime(Modal.Date).Month, Convert.ToDateTime(Modal.Date).Year, Modal.Approve);
            return PartialView(ViweModal);
        }
        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public JsonResult _ApprovalAllOTCOLeave(string src, MiscGetActiveLog Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            bool status = false;
            string Msg = "", LeaveMsg = "";
            if (ModelState.IsValid)
            {
                DateTime MyDate;
                DateTime.TryParse(Modal.Date, out MyDate);
                ViewBag.SelectedDate = MyDate;

                if (Command == "Approved")
                {
                    // Approved all Com Off
                    List<CompensatoryOffList> ComOFFlist = new List<CompensatoryOffList>();
                    ComOFFlist = Activity.GetCompensatoryOffList(Modal.EMPID, Convert.ToDateTime(Modal.Date).Month, Convert.ToDateTime(Modal.Date).Year, "0");
                    if (ComOFFlist.Count > 0)
                    {
                        foreach (var item in ComOFFlist)
                        {
                            Common_SPU.fnSetCompensatoryOffApproval(item.CompensatoryOffID, 1, item.HRS, item.Reason);
                        }
                        status = true;
                        Msg += "Compensatory Off ,";
                    }
                    // Approved all OT
                    List<OvertimeList> OTList = new List<OvertimeList>();
                    OTList = Activity.GetOvertimeList(Modal.EMPID, Convert.ToDateTime(Modal.Date).Month, Convert.ToDateTime(Modal.Date).Year, "0");
                    if (OTList.Count > 0)
                    {
                        foreach (var item in OTList)
                        {
                            Common_SPU.fnSetOvertimeApproval(item.OvertimeID, 1, item.hours);
                        }
                        status = true;
                        Msg += "Overtime ,";
                    }
                    // Approved all Leaves
                    DataSet ds = new DataSet();
                    ds = Common_SPU.fnGetPendingLeaveLog_Month(Modal.EMPID, MyDate.Month, MyDate.Year);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            long leaveLogID = 0;
                            long.TryParse(item["ID"].ToString(), out leaveLogID);
                            int Approve = (clsApplicationSetting.GetSessionValue("IsED") == "Y" ? 4 : 1);
                            LeaveMsg = Common_SPU.fnIsLeaveRequestForEDApproval(leaveLogID);
                            if (LeaveMsg == "ED and HOD is Same")
                            {
                                Approve = 1;
                                LeaveMsg = "";
                            }
                            if (string.IsNullOrEmpty(LeaveMsg) || clsApplicationSetting.GetSessionValue("IsED") == "Y")
                            {
                                Common_SPU.fnSetLeaveApprovalHOD(leaveLogID, Approve, "", "");
                                // Fire Mail
                                Common_SPU.fnCreateMail_Leave(leaveLogID);
                                status = true;
                            }
                        }
                        if (string.IsNullOrEmpty(LeaveMsg))
                        {
                            Msg = Msg + " Leaves Approved Successfully";
                        }
                        else
                        {
                            Msg += Msg.TrimEnd(',') + LeaveMsg;
                        }
                    }
                }
                if (status)
                {
                    Msg = Msg.TrimEnd(',');
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;

                }
                else
                {
                    PostResult.Status = false;
                    PostResult.SuccessMessage = (string.IsNullOrEmpty(Msg) ? "No data found for action please verify the same." : Msg);

                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public JsonResult _ActionOnCompensatoryOff(string src, List<CompensatoryOffList> Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            //long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Count = 0;
            //bool status = false;
            PostResult.SuccessMessage = "Compensatory Off Approval is not Saved";
            if (Command == "Resubmit" && string.IsNullOrEmpty(Modal.Select(x => x.Reason).FirstOrDefault()))
            {
                ModelState.AddModelError("Reason", "");
                PostResult.SuccessMessage = "Reason can't be blank";
            }
            if (ModelState.IsValid)
            {
                if (Command == "Approved")
                {
                    string CompDate = string.Empty;
                    int CompHours = 0;
                    int EMPID = 0;
                    foreach (var item in Modal)
                    {
                        if (!string.IsNullOrEmpty(item.CheckSelected))
                        {
                            Count++;
                            Common_SPU.fnSetCompensatoryOffApproval(item.CompensatoryOffID, 1, item.Approve_hours, item.Reason);

                            if (string.IsNullOrEmpty(CompDate))
                            {
                                CompDate = item.Date;
                            }
                            else
                            {
                                CompDate = CompDate + "," + item.Date;
                            }
                            if (CompHours == 0)
                            {
                                CompHours = Convert.ToInt32(item.hours);
                            }
                            else
                            {
                                CompHours = CompHours + Convert.ToInt32(item.hours);
                            }
                            EMPID = Convert.ToInt32(item.EMPID);



                            // Common_SPU.fnCreateMailHOD_DailyActivityCompOff(item.Date, Convert.ToString(item.hours), item.CompensatoryOffID, Command, "", item.EMPID);
                        }
                    }
                    if (Count > 0)
                    {
                        Common_SPU.fnCreateMailHOD_DailyActivityCompOff(CompDate, Convert.ToString(CompHours), 0, Command, "", EMPID);
                        PostResult.Status = true;
                        PostResult.SuccessMessage = "Compensatory Off Approval Successfully";
                    }

                }
                if (Command == "Resubmit")
                {
                    string CompDate = string.Empty;
                    int CompHours = 0;
                    int EMPID = 0;
                    string reson = string.Empty;
                    foreach (var item in Modal)
                    {

                        if (!string.IsNullOrEmpty(item.CheckSelected))
                        {
                            Count++;

                            if (string.IsNullOrEmpty(CompDate))
                            {
                                CompDate = item.Date;
                            }
                            else
                            {
                                CompDate = CompDate + "," + item.Date;
                            }
                            if (CompHours == 0)
                            {
                                CompHours = Convert.ToInt32(item.hours);
                            }
                            else
                            {
                                CompHours = CompHours + Convert.ToInt32(item.hours);
                            }
                            EMPID = Convert.ToInt32(item.EMPID);
                            reson = item.Reason;
                            Common_SPU.fnSetCompensatoryOffApproval(item.CompensatoryOffID, 2, item.Approve_hours, item.Reason);
                            // Common_SPU.fnCreateMailHOD_DailyActivityCompOff(item.Date, Convert.ToString(item.hours), item.CompensatoryOffID, Command, item.Reason, item.EMPID);
                        }
                    }
                    if (Count > 0)
                    {
                        Common_SPU.fnCreateMailHOD_DailyActivityCompOff(CompDate, Convert.ToString(CompHours), 0, Command, reson, EMPID);
                        PostResult.Status = true;
                        PostResult.SuccessMessage = "Compensatory Off Rejected Successfully";
                    }

                }
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ApproveOT(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            CompensatoryOffApproval Modal = new CompensatoryOffApproval();
            Modal.Approve = "0";
            Modal.Date = DateTime.Now.ToString("yyyy-MM");
            Modal.CompensatoryEmployee = CommonSpecial.GetEmployeeOvertime();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ApproveOT(CompensatoryOffApproval Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;

            List<OvertimeList> ViweModal = new List<OvertimeList>();
            ViweModal = Activity.GetOvertimeList(Modal.EMPID, Convert.ToDateTime(Modal.Date).Month, Convert.ToDateTime(Modal.Date).Year, Modal.Approve);
            return PartialView(ViweModal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public JsonResult _ActionOnOvertime(string src, List<OvertimeList> Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            //long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Count = 0;
            //bool status = false;
            PostResult.SuccessMessage = "OverTime Approval is not Saved";
            if (ModelState.IsValid)
            {
                if (Command == "Approved")
                {
                    foreach (var item in Modal)
                    {
                        if (!string.IsNullOrEmpty(item.CheckSelected))
                        {
                            Count++;
                            Common_SPU.fnSetOvertimeApproval(item.OvertimeID, 1, item.Approve_hours);
                        }
                    }
                    if (Count > 0)
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = "OverTime Approval Successfully";
                    }

                }
                if (Command == "Resubmit")
                {
                    foreach (var item in Modal)
                    {

                        if (!string.IsNullOrEmpty(item.CheckSelected))
                        {
                            Count++;
                            Common_SPU.fnSetOvertimeApproval(item.OvertimeID, 2, item.Approve_hours);
                        }
                    }
                    if (Count > 0)
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = "OverTime Approval Successfully";
                    }

                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _ViewActivityLogwithSummary(string src, MiscGetActiveLog Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(Modal.Date, out MyDate);
            ViewBag.SelectedDate = MyDate;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetActivityLog(Modal.EMPID, MyDate.Month, MyDate.Year);
            return PartialView(ds);
        }

        public ActionResult _ViewActiveLog(string src, MiscGetActiveLog Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(Modal.Date, out MyDate);
            ViewBag.SelectedDate = MyDate;
            List<ActiveLog> ViewModal = new List<ActiveLog>();
            ViewModal = Activity.GetActiveLogList(Modal.EMPID, MyDate);
            return PartialView(ViewModal);
        }


        public ActionResult FNFSettlement(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            FNFSettlement Modal = new FNFSettlement();
            Modal.EmployeeList = Activity.GetFNFEmployeeList();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _FNFSettlement(string src, FNFSettlement MyModal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = MyModal.EMPID;
            DataSet ds = Common_SPU.GetFullAndFinalRpt(MyModal.EMPID);
            //FNFEmployeeDetails Modal = new FNFEmployeeDetails();
            //Modal = Activity.GetFNFSettlementRpt(MyModal.EMPID);
            return PartialView(ds);
        }

        public ActionResult ConsolidateActivityLog(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now.AddMonths(-1);
            ConsolidateActivityLog Modal = new ConsolidateActivityLog();
            Modal.MitrUser = AllEnum.MitrNonMitr.Mitr;
            Modal.Date = MyDate.ToString("yyyy-MM");
            Modal.EMPList = Activity.GetConsolidateEMPList(MyDate.Month, MyDate.Year);
            return View(Modal);
        }

        [ValidateAntiForgeryToken]
        //[WhitespaceFilter]
        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _ConsolidateActivityLog(string src, ConsolidateActivityLog Modal, string Command, FormCollection Collection)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SelectedDate = Modal.Date;
            string Empid = "-1";
            string MitrUser = "";
            try
            {
                if (Collection.AllKeys.Contains("EMPID"))
                {
                    Empid = Collection.GetValue("EMPID").AttemptedValue == null ? "0" : Collection.GetValue("EMPID").AttemptedValue;
                }

            }
            catch { }
            try
            {
                if (Collection.AllKeys.Contains("MitrUser"))
                {
                    MitrUser = Convert.ToString(Collection.GetValue("MitrUser").AttemptedValue == null ? "0" : Collection.GetValue("MitrUser").AttemptedValue);
                }

            }
            catch { }

            DateTime MyDate;
            DateTime.TryParse(Modal.Date, out MyDate);
            DataSet ds;
            try
            {
                ds = Common_SPU.fnGetConsolidateActivityLog(Empid, MyDate.Month, MyDate.Year, MitrUser);
            }
            catch (Exception ex)
            {
                ds = Common_SPU.fnGetConsolidateActivityLog("-2", MyDate.Month, MyDate.Year, MitrUser);
                throw ex;
            }
            return PartialView(ds);
        }


        [AuthorizeFilter(ActionFor = "W")]

        [HttpPost]
        public JsonResult _ConsolidateCollate(ConsolidateActivityLog Modal, string src, string Command, FormCollection Collection)
        {

            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SelectedDate = Modal.Date;
            DateTime MyDate;
            DateTime.TryParse(Modal.Date, out MyDate);
            int MitrUser = -1;
            try
            {
                if (!string.IsNullOrEmpty(Modal.MitrUser.ToString()))
                {
                    MitrUser = Modal.MitrUser.ToString() == "NonMitr" ? 1 : 0;
                }
            }
            catch { }
            try
            {
                if (Modal.EMPID == "" || Modal.EMPID == "0" || Modal.EMPID == null)
                {
                    List<ConsolidateEMPList> lst = new List<ConsolidateEMPList>();
                    if (MitrUser == 0)
                    {
                        lst = Activity.GetConsolidateEMPList(MyDate.Month, MyDate.Year).Where(n => n.user_id > 0).ToList();
                    }
                    else if (MitrUser == 1)
                    {
                        lst = Activity.GetConsolidateEMPList(MyDate.Month, MyDate.Year).Where(n => n.user_id == 0).ToList();
                    }
                    else
                    {
                        lst = Activity.GetConsolidateEMPList(MyDate.Month, MyDate.Year);
                    }
                    foreach (var item in lst)
                    {
                        Common_SPU.fnProcessActiveLog(item.EMPID, MyDate.Month, MyDate.Year);
                    }
                }
                else
                {
                    string[] arr = Modal.EMPID.Split(',');
                    foreach (var item in arr)
                    {
                        Common_SPU.fnProcessActiveLog(Convert.ToInt64(item), MyDate.Month, MyDate.Year);
                    }
                }
                PostResult.Status = true;
                PostResult.SuccessMessage = "Consolidate activity log collate successfully.";

            }
            catch (Exception ex)
            {
                PostResult.SuccessMessage = "collate Not Done";

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }


        // old Action
        //[WhitespaceFilter]
        public ActionResult ProcessConsolidateActivity(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(GetQueryString[2], out MyDate);
            long EMPIDDD = 0;
            long.TryParse(GetQueryString[3], out EMPIDDD);
            ViewBag.SelectedDate = MyDate;
            ViewBag.EMPID = EMPIDDD;
            ProcessActivityTimeSheet Modal = new ProcessActivityTimeSheet();
            Modal = Activity.GetProcessActivityTimeSheet(MyDate, EMPIDDD);
            return View(Modal);
        }




        [HttpPost]

        public ActionResult _ConsolidateActivityCheckStatus(string src, InputDate Modal)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime StartDate = Convert.ToDateTime(Modal.Date + "-01");
            DateTime EndDate = Convert.ToDateTime(StartDate).AddMonths(1).AddDays(-1);
            List<ConsolidateActivityStatusList> ModalView = new List<ConsolidateActivityStatusList>();
            ModalView = Activity.GetConsolidateActivityStatusList(StartDate, EndDate, Modal.Approve);
            return PartialView(ModalView);
        }
        //[WhitespaceFilter]
        [HttpPost]
        public ActionResult _EmployeeTimeSheet(string src, MiscGetTimeSheet Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = Convert.ToDateTime(Modal.Date);
            TimeSheetEmployeeDetails MyModal = new TimeSheetEmployeeDetails();
            MyModal = Activity.GetTimeSheetEmployeeDetails(Modal.Date, Modal.EMPID, Modal.Type);
            return PartialView(MyModal);
        }
        // New Process TimeSheet
        //[WhitespaceFilter]
        public ActionResult ProcessTimeSheet(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(GetQueryString[2], out MyDate);
            string EMPIDDD = GetQueryString[3];
            string EmpType = GetQueryString[4];
            ViewBag.SelectedDate = MyDate;
            ViewBag.EMPID = EMPIDDD;
            ProcessTimeSheet Modal = new ProcessTimeSheet();
            Modal = Activity.FinalGetProcessTimeSheet(MyDate, EMPIDDD, EmpType);

            //Modal = Activity.CalculationOnTimeSheet(Modal);
            return View(Modal);
        }
        public ActionResult ProcessTimeSheetNew(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(GetQueryString[2], out MyDate);
            string EMPIDDD = GetQueryString[3];
            string EmpType = GetQueryString[4];
            ViewBag.SelectedDate = MyDate;
            ViewBag.EMPID = EMPIDDD;
            ProcessTimeSheet Modal = new ProcessTimeSheet();
            Modal = Activity.FinalGetProcessTimeSheet(MyDate, EMPIDDD, EmpType);
            //Modal = Activity.CalculationOnTimeSheet(Modal);
            return View(Modal);
        }

        //[WhitespaceFilter]
        [HttpPost]
        public ActionResult ProcessTimeSheet(ProcessTimeSheet Modal, string src, string Command)
        {
            ViewBag.src = src;
            CommonMethods objCommonMethods = new CommonMethods();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            List<ProcessProjectTimeSheetDetails> objlist = new List<ProcessProjectTimeSheetDetails>();
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate;
            DateTime.TryParse(GetQueryString[2], out MyDate);
            long EMPIDDD = 0;
            long.TryParse(GetQueryString[3], out EMPIDDD);
            ViewBag.SelectedDate = MyDate;
            ViewBag.EMPID = EMPIDDD;
            long SaveID = 0;
            string Msg = "Saved Successfully.";
            if (Command == "Calculate")
            {
                Modal = Activity.CalculationOnTimeSheet(Modal);
                ModelState.Clear();
                return View(Modal);
            }
            else if (Command == "Finalize")
            {
                Modal = Activity.FinalizeOnTimeSheet(Modal);
                ModelState.Clear();
                return View(Modal);
            }
            else if (Command == "Save" || Command == "Lock")
            {
                //  if (PostModal.EmployeeDetails[i].Checkbox == null)
                if (Modal.EmployeeDetails.Any(x => x.DiffHourRate != 0 && x.Checkbox != null))
                {
                    Msg = "Difference hours must be zero.";
                    ModelState.AddModelError("isContainError", Msg);
                }
                if (Modal.EmployeeDetails.Any(x => x.PerHourRate == 0 && x.Checkbox != null))
                {
                    Msg = "Please fill per hour rate in Salary Structure.";
                    ModelState.AddModelError("isContainError", Msg);
                }
                //if (Modal.EmployeeDetails.Any(x => x.AdjHourRate == 0 && x.Checkbox != null))
                //{
                //    Msg = "Working hours can not be zero.";
                //    ModelState.AddModelError("isContainError", Msg);
                //}
                if (ModelState.IsValid)
                {
                    if (Modal.EmployeeDetails != null)
                    {
                        for (int i = 0; i < Modal.EmployeeDetails.Count; i++)
                        {
                            if (Modal.EmployeeDetails[i].Checkbox != null)
                            {
                                if (Modal.EmployeeDetails[i].IsDataComingFormTimeSheet)
                                {
                                    SaveID = Common_SPU.fnSetTimesheetLog(Modal.EmployeeDetails[i].EMPID, Modal.EmployeeDetails[i].hdnAdjHourRate, Modal.EmployeeDetails[i].OT, Modal.EmployeeDetails[i].hdnAL,
                                   Modal.EmployeeDetails[i].hdnPaidLeave, Modal.EmployeeDetails[i].hdnHolidayHours, Modal.EmployeeDetails[i].PaidHours, Modal.EmployeeDetails[i].PerHourRate,
                                   Modal.EmployeeDetails[i].GrossSalary, Modal.EmployeeDetails[i].hdnLWP, Modal.EmployeeDetails[i].MonthHours, Modal.SelectedDate.Month, Modal.SelectedDate.Year);
                                }
                                else
                                {
                                    SaveID = Common_SPU.fnSetTimesheetLog(Modal.EmployeeDetails[i].EMPID, Modal.EmployeeDetails[i].AdjHourRate, Modal.EmployeeDetails[i].OT, Modal.EmployeeDetails[i].hdnAL,
                                    Modal.EmployeeDetails[i].hdnPaidLeave, Modal.EmployeeDetails[i].hdnHolidayHours, Modal.EmployeeDetails[i].PaidHours, Modal.EmployeeDetails[i].PerHourRate,
                                    Modal.EmployeeDetails[i].GrossSalary, Modal.EmployeeDetails[i].hdnLWP, Modal.EmployeeDetails[i].MonthHours, Modal.SelectedDate.Month, Modal.SelectedDate.Year);
                                }
                                if (SaveID > 0)
                                {
                                    int TotalWorkingProjects = Modal.EmployeeDetails[i].ProjectDetails.Where(n => n.WH > 0).Count();
                                    int SavedProjectsCount = 0;
                                    decimal ProjectValues = 0;
                                    decimal CalValue = 0;
                                    decimal ALValue = Math.Round(Modal.EmployeeDetails[i].hdnAL * Modal.EmployeeDetails[i].PerHourRate, 0, MidpointRounding.AwayFromZero);
                                    decimal OTValue = Math.Round(Modal.EmployeeDetails[i].OT * 2 * Modal.EmployeeDetails[i].PerHourRate, 0, MidpointRounding.AwayFromZero);
                                    decimal NetAmt = Modal.EmployeeDetails[i].NetSalary;
                                    decimal AdjustAmt = Modal.EmployeeDetails[i].GrossSalary - (ALValue + OTValue);
                                    // add new code by shailendra
                                    if (Command == "Save")
                                    {
                                        for (int K = 0; K < Modal.EmployeeDetails[i].ProjectDetails.Count; K++)
                                        {
                                            ProcessProjectTimeSheetDetails obj = new ProcessProjectTimeSheetDetails();
                                            if (Modal.EmployeeDetails[i].ProjectDetails[K].WH != 0)
                                            {
                                                CalValue = Math.Round(Modal.EmployeeDetails[i].ProjectDetails[K].WH * Modal.EmployeeDetails[i].PerHourRate, 0, MidpointRounding.AwayFromZero);
                                                ProjectValues = ProjectValues + CalValue;
                                                SavedProjectsCount = SavedProjectsCount + 1;
                                                if (TotalWorkingProjects == SavedProjectsCount && AdjustAmt != ProjectValues)
                                                {
                                                    CalValue = CalValue + (AdjustAmt - ProjectValues);
                                                }
                                                Modal.EmployeeDetails[i].ProjectDetails[K].WorkValue = CalValue;
                                                //Common_SPU.fnSetTimesheetDet(SaveID, Modal.EmployeeDetails[i].ProjectDetails[K].ProjectID,
                                                //    (Modal.EmployeeDetails[i].ProjectDetails[K].WH - Modal.EmployeeDetails[i].ProjectDetails[K].hdnPrAdjsutHours),
                                                //    Modal.EmployeeDetails[i].ProjectDetails[K].hdnPrAdjsutHours,
                                                //    Modal.EmployeeDetails[i].ProjectDetails[K].WorkValue,
                                                //    Modal.EmployeeDetails[i].ProjectDetails[K].TH,
                                                //    Modal.EmployeeDetails[i].ProjectDetails[K].TravelValue);

                                                // add below line by shailendra
                                                obj.TimeSheetlog_id = SaveID;
                                                obj.proj_id = Modal.EmployeeDetails[i].ProjectDetails[K].ProjectID;
                                                obj.work_hours = (Modal.EmployeeDetails[i].ProjectDetails[K].WH - Modal.EmployeeDetails[i].ProjectDetails[K].hdnPrAdjsutHours);

                                                obj.adj_hours = Modal.EmployeeDetails[i].ProjectDetails[K].hdnPrAdjsutHours;
                                                obj.work_value = Math.Round(Modal.EmployeeDetails[i].ProjectDetails[K].WorkValue, 0, MidpointRounding.AwayFromZero);

                                                obj.travel_hour = Modal.EmployeeDetails[i].ProjectDetails[K].TH;
                                                obj.travel_value = Math.Round(Modal.EmployeeDetails[i].ProjectDetails[K].TravelValue, 0, MidpointRounding.AwayFromZero);
                                                obj.createdby = Convert.ToInt64(clsApplicationSetting.GetSessionValue("LoginID"));
                                                objlist.Add(obj);
                                            }
                                            else
                                            {
                                                clsDataBaseHelper.ExecuteNonQuery("update timesheetdet_log set isdeleted=1 where isdeleted=0 and TimeSheetlog_id=" + SaveID + " and proj_id=" + Modal.EmployeeDetails[i].ProjectDetails[K].ProjectID + "");
                                            }
                                        }
                                    }

                                    if (Command == "Lock")
                                    {
                                        clsDataBaseHelper.ExecuteNonQuery("update timesheet_log set approved=1 where isdeleted=0 and id=" + SaveID + "");
                                    }
                                    //   Common_SPU.fnSetAdjustmentActiveLog(Modal.EmployeeDetails[i].EMPID, Modal.SelectedDate.Month, Modal.SelectedDate.Year);
                                }
                            }

                        }
                        if (SaveID > 0)
                        {
                            TempData["Success"] = "Y";
                            TempData["SuccessMsg"] = Msg;
                        }
                        // new code by shailendra
                        if (Command == "Save")
                        {
                            string stringTOXml = objCommonMethods.GetXMLFromObject(objlist);
                            string errorMessage = string.Empty;
                            int roleId = 1;
                            int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
                            var data = sallaryProcess.PerformOperation(stringTOXml, Convert.ToString((int)Enum.Parse(typeof(Constants.ScreenID), Constants.ScreenID.SallaryData.ToString())), roleId, userid, "Save", out errorMessage);
                            string jsondata = new JavaScriptSerializer().Serialize(objlist);
                            string path = Server.MapPath("~/Attachments/Salary/");
                            // Write that JSON to txt file,  
                            System.IO.File.WriteAllText(path + "'" + Modal.SelectedDate.Month + "," + Modal.SelectedDate.Year + "'.json", jsondata);

                        }
                        return RedirectToAction("ProcessTimeSheet", new { src = src });
                    }

                }
                else
                {
                    TempData["Success"] = "N";
                    TempData["SuccessMsg"] = Msg;
                    return View(Modal);
                }
            }

            else if (Command == "GenerateReport")
            {
                return RedirectToAction("ReportsaActivityLog", "Activity", new { src = src, sRepType = "Timesheet", sRepName = "TimesheetReport.rpt", EMPID = Modal.TempEMPID, Date = Modal.SelectedDate });

            }
            if (SaveID > 0)
            {
                TempData["Success"] = "Y";
                TempData["SuccessMsg"] = Msg;
            }
            else
            {
                TempData["Success"] = "N";
                TempData["SuccessMsg"] = "TimeSheet Not Saved";
            }
            if (Modal.isContainError)
            {
                TempData["Success"] = "N";
                TempData["SuccessMsg"] = "mismatched or tampering data found Please Calculate Again";
                return RedirectToAction("ProcessTimeSheet", new { src = src });

            }
            else
            {
                ModelState.Clear();
                return View(Modal);
            }


        }

        public ActionResult ActivityReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now.AddMonths(-1);
            ActivityReport Modal = new ActivityReport();
            Modal.Date = MyDate.ToString("yyyy-MM");
            if (TempData.ContainsKey("MitrUser"))
            {
                if (TempData["MitrUser"].ToString() != "")
                {
                    if (TempData["MitrUser"].ToString() == "Mitr")
                    {
                        Modal.MitrUser = 0;
                    }
                    else
                    {
                        Modal.MitrUser = AllEnum.MitrNonMitr.NonMitr;
                    }
                }

            }
            else
            {
                Modal.MitrUser = 0;
            }

            return View(Modal);

        }

        //public ActionResult ActivityReport(string src)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    DateTime MyDate = DateTime.Now.AddMonths(-1);
        //    ActivityReport Modal = new ActivityReport();
        //    Modal.Date = MyDate.ToString("yyyy-MM");
        //    Modal.MitrUser = 0;
        //    return View(Modal);

        //}
        [HttpPost]
        public ActionResult ActivityReport(ActivityReport Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet ds;
            //string SQL = "";
            ViewBag.dsdata = null;
            TempData["dsdata"] = null;
            if (ModelState.IsValid)
            {
                if (Command == "Download")
                {
                    switch (Modal.RadioButton)
                    {
                        case "1":
                            Export.GetActiveStatusLog_Export(Modal);
                            break;

                        case "2":
                            Export.LeaveAvailedReport_RPT("LeaveReport", "LeaveReport.rpt", 0, Convert.ToDateTime(Modal.Date));
                            break;
                        case "3":
                            ds = Common_SPU.FnGetProjectWiseSalaryRpt("", Convert.ToDateTime(Modal.Date), "SALARYALLOCATION", Modal.MitrUser.ToString());
                            ViewBag.dsdata = ds;
                            TempData["dsdata"] = ds;
                            TempData["Reporttype"] = "SALARYALLOCATION";
                            break;
                        case "4":
                            ds = Common_SPU.FnGetProjectWiseSalaryRpt("", Convert.ToDateTime(Modal.Date), "PFALLOCATION", Modal.MitrUser.ToString());
                            ViewBag.dsdata = ds;
                            TempData["dsdata"] = ds;
                            TempData["Reporttype"] = "PFALLOCATION";
                            break;
                        case "5":
                            Export.TimeSheet_RPT("Timesheet", "TimesheetReport.rpt", 0, Convert.ToDateTime(Modal.Date), Modal.MitrUser.ToString());
                            break;

                        case "6":
                            ds = Common_SPU.FnGetPFRpt(Convert.ToDateTime(Modal.Date), Modal.MitrUser.ToString(), "FNF");
                            ViewBag.dsdata = ds;
                            TempData["dsdata"] = ds;
                            TempData["Reporttype"] = "PF";
                            break;
                        case "7":
                            ds = Common_SPU.FnGetPTAXRpt(Convert.ToDateTime(Modal.Date), Modal.MitrUser.ToString(), "PTAX");
                            ViewBag.dsdata = ds;
                            TempData["dsdata"] = ds;
                            TempData["Reporttype"] = "PTAX";
                            break;
                        case "8":
                            Export.GetAnnualPackageDetails_Export(Convert.ToDateTime(Modal.Date));
                            break;
                        case "9":
                            Export.GetLeaveAvailedLapsed_Export(Convert.ToDateTime(Modal.Date));
                            break;
                        case "10":
                            ds = Common_SPU.FnGetProjectWiseSalaryRpt("", Convert.ToDateTime(Modal.Date), "ALALLOCATION", Modal.MitrUser.ToString());
                            ViewBag.dsdata = ds;
                            TempData["dsdata"] = ds;
                            TempData["Reporttype"] = "ALALLOCATION";
                            break;
                        case "11":
                            ds = Common_SPU.FnGetProjectWiseSalaryRpt("", Convert.ToDateTime(Modal.Date), "PWAFIXEDBENEFIT", Modal.MitrUser.ToString());
                            ViewBag.dsdata = ds;
                            TempData["dsdata"] = ds;
                            TempData["Reporttype"] = "PWAFIXEDBENEFIT";
                            break;

                        case "12":
                            // DateTime StartDate = Convert.ToDateTime(Modal.Date + "-01");
                            string Date = Modal.Date + "-01";

                            ds = Common_SPU.FnGetProjectWiseBonusRpt(0, Date);
                            ViewBag.dsdata = ds;
                            TempData["dsdata"] = ds;
                            TempData["Reporttype"] = "BonusReport";
                            break;
                        case "13":
                            // DateTime StartDate = Convert.ToDateTime(Modal.Date + "-01");
                            string Daten = Modal.Date + "-01";
                            ds = Common_SPU.FnGetGetBankBankReport(Modal.MitrUser.ToString(), Daten);
                            SalaryReport obj = new SalaryReport();
                            List<SalaryReport> Employeelist = new List<SalaryReport>();
                            DataSet grievancesModuleDataSet = Common_SPU.FnGetGetBankBankReport(Modal.MitrUser.ToString(), Daten);
                            foreach (DataRow item in grievancesModuleDataSet.Tables[1].Rows)
                            {
                                obj = new SalaryReport();
                                obj.EmployeeCode = Convert.ToString(item["emp_code"]);
                                obj.PaymentType = "PAY";
                                obj.PaymentDate = Convert.ToString(item["SalaryDate"]);
                                obj.BeneficaryName = Convert.ToString(item["emp_name"]);
                                obj.BeneficaryAccountNo = Convert.ToString(item["AccountNo"]);
                                obj.BeneficaryBankCode = Convert.ToString(item["IFSCCode"]);
                                if (Convert.ToString(item["ProjectType"]) == "FCRA")
                                {
                                    obj.Amount = Convert.ToDecimal(item["NetFCRAAmt"]);
                                }
                                else
                                {
                                    obj.Amount = Convert.ToDecimal(item["NetNFCRAAmt"]);
                                }
                                obj.Narration = Convert.ToString("Salary for the month of " + item["YearName"]);
                                obj.EmailId = Convert.ToString(item["email"]);
                                if (Convert.ToString(item["ProjectType"]) == "FCRA")
                                {
                                    obj.DebitAccountNumber = Convert.ToString("52510891000");
                                }
                                else
                                {
                                    obj.DebitAccountNumber = Convert.ToString("52510891019");
                                }
                                obj.Type = Convert.ToString(item["ProjectType"]);
                                Employeelist.Add(obj);
                            }

                            // Create Excel package
                            ExcelPackage excelPackage = new ExcelPackage();
                            var worksheet = excelPackage.Workbook.Worksheets.Add("Bank Report");
                            worksheet.Cells[1, 5].Value = "Centre for Catalyzing Change";
                            worksheet.Cells[2, 5].Value = "Plot no. 6, Local Shopping Centre  Panchsheel Park, New Delhi 110017";
                            worksheet.Cells[3, 5].Value = "Bank Report";
                            worksheet.Cells[4, 1].Value = "Employee Code";
                            worksheet.Cells[4, 2].Value = "Payment Type";
                            worksheet.Cells[4, 3].Value = "Payment Date";
                            worksheet.Cells[4, 4].Value = "Beneficary Name";
                            worksheet.Cells[4, 5].Value = "Beneficary Account No";
                            worksheet.Cells[4, 6].Value = "Beneficary Bank Code";
                            worksheet.Cells[4, 7].Value = "Amount";
                            worksheet.Cells[4, 8].Value = "Narration";
                            worksheet.Cells[4, 9].Value = "Email Id";
                            worksheet.Cells[4, 10].Value = "Debit Account Number";
                            worksheet.Cells[4, 11].Value = "Type";
                            worksheet.Cells[4, 1].Style.Font.Bold = true;
                            worksheet.Cells[4, 2].Style.Font.Bold = true;
                            worksheet.Cells[4, 3].Style.Font.Bold = true;
                            worksheet.Cells[4, 4].Style.Font.Bold = true;
                            worksheet.Cells[4, 5].Style.Font.Bold = true;
                            worksheet.Cells[4, 6].Style.Font.Bold = true;
                            worksheet.Cells[4, 7].Style.Font.Bold = true;
                            worksheet.Cells[4, 8].Style.Font.Bold = true;
                            worksheet.Cells[4, 9].Style.Font.Bold = true;
                            worksheet.Cells[4, 10].Style.Font.Bold = true;
                            worksheet.Cells[4, 11].Style.Font.Bold = true;

                            worksheet.Cells[1, 5].Style.Font.Bold = true;
                            worksheet.Cells[1, 5].Style.Font.Size = 15;
                            worksheet.Cells[2, 5].Style.Font.Bold = true;
                            worksheet.Cells[2, 5].Style.Font.Size = 15;
                            worksheet.Cells[3, 5].Style.Font.Bold = true;
                            worksheet.Cells[3, 5].Style.Font.Size = 15;


                            // Write data to Excel
                            int row = 5;
                            foreach (var list in Employeelist)
                            {
                                worksheet.Cells[row, 1].Value = list.EmployeeCode;
                                worksheet.Cells[row, 2].Value = list.PaymentType;
                                worksheet.Cells[row, 3].Value = list.PaymentDate;
                                worksheet.Cells[row, 4].Value = list.BeneficaryName;
                                worksheet.Cells[row, 5].Value = list.BeneficaryAccountNo;
                                worksheet.Cells[row, 6].Value = list.BeneficaryBankCode;
                                worksheet.Cells[row, 7].Value = list.Amount;
                                worksheet.Cells[row, 8].Value = list.Narration;
                                worksheet.Cells[row, 9].Value = list.EmailId;
                                worksheet.Cells[row, 10].Value = list.DebitAccountNumber;
                                worksheet.Cells[row, 11].Value = list.Type;
                                worksheet.Cells[row, 5].Style.Numberformat.Format = "@";

                                row++;
                            }

                            worksheet.Column(1).Width = 15;
                            worksheet.Column(2).Width = 15;
                            worksheet.Column(3).Width = 20;
                            worksheet.Column(4).Width = 30;
                            worksheet.Column(5).Width = 22;
                            worksheet.Column(6).Width = 20;
                            worksheet.Column(7).Width = 20;
                            worksheet.Column(8).Width = 30;
                            worksheet.Column(9).Width = 30;
                            worksheet.Column(10).Width = 30;
                            worksheet.Column(11).Width = 10;
                            byte[] fileContents = excelPackage.GetAsByteArray();
                            string fileName = "Bank Report.xlsx";

                            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

                            break;



                        default:
                            return RedirectToAction("ActivityReport", "Activity", new { src = src });
                            break;
                    }
                }
                if (Modal.MitrUser != null)
                    TempData["MitrUser"] = Modal.MitrUser;
                else
                    TempData["MitrUser"] = "";
            }
            return RedirectToAction("ActivityReport", "Activity", new { src = src });



        }


        public ActionResult _LeaveSummary(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now;
            if (GetQueryString.Length > 2)
            {
                DateTime.TryParse(GetQueryString[2], out MyDate);
            }
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            List<LeaveSummary> List = new List<LeaveSummary>();
            List = Activity.GetLeaveSummary(EMPID, MyDate.Month, MyDate.Year);
            return PartialView(List);

        }

        public ActionResult _StandardTaskList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<ProjectMappedList> List = new List<ProjectMappedList>();
            List = Activity.GetSelectedProjectsList("Task", "");
            return PartialView(List);
        }
        public ActionResult ActivityLogApprovalNonMITR(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now;
            if (GetQueryString.Length == 3)
            {
                DateTime.TryParse(GetQueryString[2], out MyDate);
            }
            InputDate Modal = new InputDate();
            Modal.Date = MyDate.ToString("yyyy-MM");
            Modal.Approve = "0";
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ActivityLogApprovalNonMITR(string src, InputDate Modal, string Command)
        {

            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            List<MyTeamActiveLogListNonMitr> List = new List<MyTeamActiveLogListNonMitr>();
            DateTime mydate;
            DateTime.TryParse(Modal.Date, out mydate);
            ViewBag.SelectedDate = mydate;
            List = Activity.GetMyTeamActiveLogListNonMitr(Modal.Approve, mydate.Month, mydate.Year);
            return PartialView(List);
        }
        public ActionResult ActivityLogApprovalEMPNonMITR(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string Teamids = GetQueryString[3];
            ViewBag.Teamids = Teamids;
            ViewBag.Approve = GetQueryString[4];
            DateTime MyDate = DateTime.Now;
            string Type = "APPROVAL_PENDING";
            if (ViewBag.Approve == "1")
            {
                Type = "APPROVAL_APPROVED";
            }
            else if (ViewBag.Approve == "2")
            {
                Type = "APPROVAL_RESUBMIT";
            }
            if (GetQueryString.Length >= 3)
            {
                DateTime.TryParse(GetQueryString[2], out MyDate);
            }
            MonthlyLogNonMitr modal = new MonthlyLogNonMitr();
            ViewBag.SelectedDate = MyDate;
            modal = Activity.GetMonthlyLogNonMitr(MyDate.ToString("yyyy-MM-dd"), Teamids, Type);
            return View(modal);
        }
        [HttpPost]
        public JsonResult ActivityLogApprovalEMPNonMITR(MonthlyLogNonMitr Modal, FormCollection Collection, string src, string Command)
        {
            PostResponse result = new PostResponse();
            result.SuccessMessage = "Activity log Approval is not Saved";
            DateTime Month;
            Month = Convert.ToDateTime(Collection.GetValue("hdnSelectedDate").AttemptedValue);
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Approveby = Convert.ToInt32(clsApplicationSetting.GetSessionValue("EMPID"));
            string Remarks = "";
            IActivityHelper Activity;
            Activity = new ActivityModal();
            long SavedCount = 0;

            if (!Modal.LstMonthlyLog.Any(x => x.isSelect == 1))
            {
                result.SuccessMessage = "Please Make a Selection";
                ModelState.AddModelError("LstMonthlyLog[0].proj_id", result.SuccessMessage);
            }
            if (Command == "Resubmit")
            {
                Remarks = Collection.GetValue("txtRemark").AttemptedValue;
                if (string.IsNullOrEmpty(Remarks))
                {
                    result.SuccessMessage = "Enter Remarks";
                    ModelState.AddModelError("LstMonthlyLog[0].proj_id", result.SuccessMessage);
                }
            }

            if (ModelState.IsValid)
            {
                int EmpidNew = 0, EmpidOld = 0, IsSelect = 0, EntryCount = 0, EmpidNewId = 0;
                List<int> selectedEmpIds = new List<int>();
                int chekEMPId = 0;
                if (Command == "Approve" || Command == "Resubmit")
                {
                    foreach (var item in Modal.LstMonthlyLog.Where(n => n.Doctype == "A"))
                    {
                        EmpidNewId = Convert.ToInt32(item.emp_id);
                        EmpidNew = Convert.ToInt32(item.emp_id);
                        IsSelect = item.isSelect;
                        if (EmpidNew != EmpidOld)
                        {
                            IsSelect = item.isSelect;
                            EntryCount = 0;
                        }
                        if (IsSelect == 1)
                        {
                            chekEMPId = Convert.ToInt32(item.emp_id);
                             Common_SPU.fnSetActiveApprove(0, item.emp_id, item.month, item.year, Approveby, Remarks, Command);
                            selectedEmpIds.Add(Convert.ToInt32(item.emp_id));
                            EntryCount = EntryCount + 1;
                            // fire mail
                            //  Common_SPU.fnCreateMail_ActivityLog(item.month, item.year, item.emp_id);
                            result.Status = true;
                            result.SuccessMessage = "Activity log " + Command + " successfully";
                            SavedCount = SavedCount + 1;
                        }
                        EmpidOld = EmpidNew;
                    }
                    if (Command == "Approve")
                    {
                        Common_SPU.fnCreateMail_Termbaseactivitylog(Convert.ToString(Convert.ToDateTime(Month).ToString("yyyy/MM/dd")), Command, "", chekEMPId);
                    }
                    else
                    {
                        string empIdCsv = string.Join(",", selectedEmpIds);
                        Common_SPU.fnCreateMail_TermbaseactivitylogForresubmit(Convert.ToString(Month), Command, Remarks, empIdCsv);
                    }

                    result.Status = true;
                    //result.SuccessMessage = "Monthly Activity Log Added Successfully";
                    result.RedirectURL = "/Activity/ActivityLogApprovalNonMITR?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/ActivityLogApprovalNonMITR");
                }

            }

            TempData["Success"] = (result.Status ? "Y" : "N");
            TempData["SuccessMsg"] = result.SuccessMessage;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _CompensatoryOffApprovalNonMitr(string src, string EMPID, string Date, string Approve)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Approve;
            List<CompensatoryOffList> ViweModal = new List<CompensatoryOffList>();
            ViweModal = Activity.GetCompensatoryOffListNonMitr(EMPID, Convert.ToDateTime(Date).Month, Convert.ToDateTime(Date).Year, Approve);
            return PartialView(ViweModal);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ActivityLogApprovalEMPNonMITRJson(string EMPIDs, string Date, string Status)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "something went wrong while updating.";
            int Approveby = Convert.ToInt32(clsApplicationSetting.GetSessionValue("EMPID"));
            string[] empid = EMPIDs.Split(',');
            foreach (var item in empid)
            {
                Common_SPU.fnSetActiveApprove(0, Convert.ToInt32(item), Convert.ToDateTime(Date).Month, Convert.ToDateTime(Date).Year, Approveby, "", Status);
            }
            PostResult.Status = true;
            PostResult.SuccessMessage = " updated successfully";
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ActivityLogStatusReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ActivityReports Modal = new ActivityReports();
            Modal.Doctype = "Activity_Log_Status";
            Modal.StartDate = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _Reports(string src)
        {
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            string StartDate = GetQueryString[3] + "-01";
            string Type = GetQueryString[4];
            if (Type != "")
            {
                Type = Type == "0" ? "MITR" : "NON-MITR";
            }
            string Doctype = GetQueryString[2];
            ViewBag.Doctype = Doctype;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetReport_ActivityLog(StartDate, StartDate, Type, 0, Doctype);
            return PartialView(ds);
        }
        public ActionResult ReportaActivityLog(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            ViewBag.Empid = GetQueryString[3];
            ConsolidateActivityLog Modal = new ConsolidateActivityLog();
            return View(Modal);
        }
        public ActionResult ReportsaActivityLog(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            ViewBag.Empid = GetQueryString[3];
            ConsolidateActivityLog Modal = new ConsolidateActivityLog();
            return View();
        }
        public ActionResult SalarySlipRpt(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ActivityReports Modal = new ActivityReports();
            Modal.Doctype = "SalarySlip";
            Modal.StartDate = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _SalarySlipRpt(string src)
        {
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            string StartDate = GetQueryString[3] + "-01";
            string Type = GetQueryString[4];
            if (Type != "")
            {
                Type = Type == "0" ? "MITR" : "NON-MITR";
            }
            string Doctype = GetQueryString[2];
            ViewBag.Doctype = Doctype;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetReport_ActivityLog(StartDate, StartDate, Type, 0, Doctype);
            return PartialView(ds);
        }
        public ActionResult ConsolidateReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now.AddMonths(-1);
            ConsolidateReport Modal = new ConsolidateReport();
            Modal.Date = MyDate.ToString("yyyy-MM");
            Modal.MitrUser = 0;
            return View(Modal);
        }
        public ActionResult _ConsolidateReport(string src, string Emptype, string Date)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Status = Emptype;
            ConsolidateReportEntry modal = new ConsolidateReportEntry();
            modal = Activity.GetConsolidateSalaryAllocationEntry(Emptype, Date);
            return PartialView(modal);
        }
        [HttpPost]
        public ActionResult _ConsolidateReport(string src, ConsolidateReportEntry Modal, string Command, FormCollection Collection)
        {
            ViewBag.TabIndex = 1;
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            DateTime SelectedMonth = Convert.ToDateTime(Convert.ToDateTime(Modal.Month).ToString("dd-MM-yyyy"));
            //string Emptype = Collection.GetValue("Emptype").AttemptedValue;
            int ID = 0;
            int.TryParse(ViewBag.ID, out ID);
            string msg = "Action Can't Update";
            PostResult.SuccessMessage = msg;
            if (Command == "Delete" && !Modal.Emplist.Any(x => x.Checkbox != null))
            {
                msg = "Please Choose Employee.";
                PostResult.SuccessMessage = msg;
                ModelState.AddModelError("Month", msg);
            }

            var ComponentList = Modal.Component.GroupBy(x => x.Componentid)
                .Select(x => new
                {
                    Componentid = x.Select(ex => ex.Componentid).FirstOrDefault(),
                    Component = x.Select(ex => ex.Component).FirstOrDefault(),
                    Doctype = x.Select(ex => ex.Doctype).FirstOrDefault()
                })
                .ToList();
            string Empid = "0"; string Componentid = "0"; string ComponentStr = ""; string Qry = ""; string QryNFCRA = ""; string Amount = "0"; string AmountNFCRA = "0"; string ComponentStrNonFCRA = ""; string NetFCRA = ""; string NetNFCRA = "";
            Qry = "";
            //string ALPaidFCRA = "", ALPaidNFCRA = "",OTFCRA = "", OTNFCRA = "", PFFCRA = "", PFNFCRA = "", ALFCRA = "", ALNFCRA = "";
            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    foreach (var item in Modal.Emplist)
                    {

                        //Qry = ""; NetNFCRA = "";
                        //Empid = item.Empid.ToString();
                        //ComponentStr = Empid + "-NetNFCRA";
                        //Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                        //Qry = Qry + (Qry != "" ? "," : "") + "NetNFCRA" + "#" + Amount;

                        //Qry = ""; NetFCRA = "";
                        //Empid = item.Empid.ToString();
                        //ComponentStr = Empid + "-NetFCRA";
                        //Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                        //Qry = Qry + (Qry != "" ? "," : "") + "NetFCRA" + "#" + Amount;

                        Qry = ""; QryNFCRA = "";

                        // Total save
                        // FCRA
                        Empid = item.Empid.ToString();
                        ComponentStr = Empid + "-NetFCRA";
                        Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                        Qry = Qry + (Qry != "" ? "," : "") + "NetFCRA" + "#" + Amount;
                        // NFCRA
                        Empid = item.Empid.ToString();
                        ComponentStr = Empid + "-NetNFCRA";
                        Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                        Qry = Qry + (Qry != "" ? "," : "") + "NetNFCRA" + "#" + Amount;

                        Empid = item.Empid.ToString();
                        ComponentStr = Empid + "-ALPaidFCRA";
                        Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                        Qry = Qry + (Qry != "" ? "," : "") + "ALPaidFCRA" + "#" + Amount;

                        ComponentStr = Empid + "-OTFCRA";
                        Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                        Qry = Qry + (Qry != "" ? "," : "") + "OTFCRA" + "#" + Amount;

                        ComponentStr = Empid + "-PFFCRA";
                        Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                        Qry = Qry + (Qry != "" ? "," : "") + "PFFCRA" + "#" + Amount;

                        ComponentStr = Empid + "-ALFCRA";
                        Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                        Qry = Qry + (Qry != "" ? "," : "") + "ALFCRA" + "#" + Amount;

                        ComponentStrNonFCRA = Empid + "-ALPaidNFCRA";
                        Amount = Collection.GetValue(ComponentStrNonFCRA).AttemptedValue;
                        QryNFCRA = QryNFCRA + (QryNFCRA != "" ? "," : "") + "ALPaidNFCRA" + "#" + Amount;

                        ComponentStrNonFCRA = Empid + "-OTNFCRA";
                        Amount = Collection.GetValue(ComponentStrNonFCRA).AttemptedValue;
                        QryNFCRA = QryNFCRA + (QryNFCRA != "" ? "," : "") + "OTNFCRA" + "#" + Amount;

                        ComponentStrNonFCRA = Empid + "-PFNFCRA";
                        Amount = Collection.GetValue(ComponentStrNonFCRA).AttemptedValue;
                        QryNFCRA = QryNFCRA + (QryNFCRA != "" ? "," : "") + "PFNFCRA" + "#" + Amount;

                        ComponentStrNonFCRA = Empid + "-ALNFCRA";
                        Amount = Collection.GetValue(ComponentStrNonFCRA).AttemptedValue;
                        QryNFCRA = QryNFCRA + (QryNFCRA != "" ? "," : "") + "ALNFCRA" + "#" + Amount;


                        foreach (var itemCom in ComponentList.Where(n => n.Doctype == "Other Earning"))
                        {
                            Componentid = itemCom.Componentid.ToString();
                            ComponentStr = Empid + "-" + Componentid + "-FCRA";
                            if (Collection.AllKeys.Contains(ComponentStr))
                            {
                                Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                                Qry = Qry + (Qry != "" ? "," : "") + Componentid + "#" + Amount;
                            }
                            ComponentStrNonFCRA = Empid + "-" + Componentid + "-NFCRA";
                            if (Collection.AllKeys.Contains(ComponentStrNonFCRA))
                            {
                                Amount = Collection.GetValue(ComponentStrNonFCRA).AttemptedValue;
                                QryNFCRA = QryNFCRA + (QryNFCRA != "" ? "," : "") + Componentid + "#" + Amount;
                            }

                        }
                        foreach (var itemCom in ComponentList.Where(n => n.Doctype == "Deduction"))
                        {
                            Componentid = itemCom.Componentid.ToString();
                            ComponentStr = Empid + "-" + Componentid + "#FCRA";
                            ComponentStrNonFCRA = Empid + "-" + Componentid + "#NonFCRA";
                            if (Collection.AllKeys.Contains(ComponentStr))
                            {
                                Amount = Collection.GetValue(ComponentStr).AttemptedValue;
                                AmountNFCRA = Collection.GetValue(ComponentStrNonFCRA).AttemptedValue;
                                Qry = Qry + (Qry != "" ? "," : "") + Componentid + "#" + Amount;
                                QryNFCRA = QryNFCRA + (QryNFCRA != "" ? "," : "") + Componentid + "#" + AmountNFCRA;
                            }

                        }
                        PostResult = Activity.fnSetConsolidatedSalaryAllocationEntry(SelectedMonth.Month, Convert.ToDateTime(Modal.Month).Year, Convert.ToInt64(Empid), Qry, QryNFCRA);


                    }
                }
                else if (Command == "Delete")
                {
                    foreach (var item in Modal.Emplist.Where(n => n.Checkbox == "1"))
                    {
                        PostResult = Common_SPU.fnDelConsolidatedSalaryAllocation(item.Empid, Convert.ToInt32(SelectedMonth.Month), Convert.ToInt32(SelectedMonth.Year));
                    }
                }

            }

            if (PostResult.Status)
            {
                PostResult.RedirectURL = "/Activity/ConsolidateReport?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Activity/ConsolidateReport");
            }
            //PostResult.AdditionalMessage = Emptype;
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Consolidated Salary Report
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>

        public ActionResult ConsolidatedSalaryReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now.AddMonths(-1);
            ConsolidateReport Modal = new ConsolidateReport();
            Modal.Date = MyDate.ToString("yyyy-MM");
            Modal.ToDate = MyDate.ToString("yyyy-MM");
            Modal.MitrUser = 0;
            return View(Modal);         
        }
        public ActionResult _ConsolidatedSalaryReport(string src, string Emptype, string FromDate, string ToDate)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Status = Emptype;

            string FDate = Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd");
            int Tyear = Convert.ToDateTime(ToDate).Year;
            int Tmonth = Convert.ToDateTime(ToDate).Month;
            var TDate = new DateTime(Tyear, Tmonth, DateTime.DaysInMonth(Tyear, Tmonth)).ToString("yyyy-MM-dd");

            ConsolidateSalaryReport modal = new ConsolidateSalaryReport();
            modal = Activity.GetConsolidatedSalaryReport(Emptype, FDate, TDate);
            return PartialView(modal);
        }

        public ActionResult ALAccrual_TakeReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime MyDate = DateTime.Now.AddMonths(-1);
            ConsolidateReport Modal = new ConsolidateReport();
            Modal.Date = MyDate.ToString("yyyy-MM");
            Modal.ToDate = MyDate.ToString("yyyy-MM");
            Modal.MitrUser = 0;
            GetResponse employee = new GetResponse();
            employee.Doctype = "GR_Employee";
            employee.LoginID = LoginID;
            Modal.EmployeeList = ClsCommon.GetDropDownList(employee);
            Modal.Empid = EmpID;
            return View(Modal);
        }
        public ActionResult _ALAccrual_TakeReport(string src, string Empid, string Emptype, string FromDate, string ToDate)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Status = Emptype;

            string FDate = Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd");
            int Tyear = Convert.ToDateTime(ToDate).Year;
            int Tmonth = Convert.ToDateTime(ToDate).Month;
            var TDate = new DateTime(Tyear, Tmonth, DateTime.DaysInMonth(Tyear, Tmonth)).ToString("yyyy-MM-dd");
            Empid = (string.IsNullOrEmpty(Empid)) ? Convert.ToString(EmpID) : Empid;
            ConsolidateSalaryReport modal = new ConsolidateSalaryReport();
            modal = Activity.GetALAccrual_TakeReport(Empid, Emptype, FDate, TDate);
            return PartialView(modal);
        }

        public ActionResult LeaveReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            LeaveReport Modal = new LeaveReport();
            ViewBag.emplist = Budget.GetBudgetSettingEmpList();
            return View(Modal);

        }
        public ActionResult _LeaveReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            int EMPID = 0;
            int Month = 0;
            int Year = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            string date = Convert.ToString(GetQueryString[3]);
            char[] spearator = { '-' };
            string[] datemonth = date.Split(spearator);
            Year = Convert.ToInt32(datemonth[0]);
            Month = Convert.ToInt32(datemonth[1]);

            LeaveReport Modal = new LeaveReport();
            GetResponse getResponse = new GetResponse();
            getResponse.LoginID = EMPID;
            getResponse.AdditionalID = Year;
            getResponse.ID = Month;
            Modal.leaveReports = Activity.GetEmployeeLeaveReportList(getResponse);
            return PartialView(Modal);

        }
        public ActionResult LeaveReportYearly(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            LeaveReport Modal = new LeaveReport();
            ViewBag.emplist = Leave.GetLeaveEmpList("All");
            ViewBag.leaveList = Leave.GetLeaveType();
            GetResponse finyear = new GetResponse();
            finyear.Doctype = "FinYearLeaveReport";
            finyear.LoginID = LoginID;
            Modal.FinYear = ClsCommon.GetDropDownList(finyear);
            return View(Modal);

        }

        public ActionResult LeaveReportYearlyReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.FinYear = GetQueryString[3];
            ViewBag.LeaveID = GetQueryString[5];
            int EMPID = 0;
            int FinYear = 0;
            int LeaveID = 0;
            int.TryParse(ViewBag.EMPID, out EMPID);
            int.TryParse(ViewBag.FinYear, out FinYear);
            int.TryParse(ViewBag.LeaveID, out LeaveID);
            string EMPTYPE = Convert.ToString(GetQueryString[4]);
            LeaveReport Modal = new LeaveReport();
            Modal.Empid = EMPID;
            Modal.FinId = FinYear;
            Modal.EmpCode = EMPTYPE;
            Modal.leaveType = LeaveID;
            return View(Modal);

        }

        public ActionResult CustomizedReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            CustomizedReport Modal = new CustomizedReport();
            GetResponse EMP = new GetResponse();
            EMP.Doctype = "Employee";
            EMP.LoginID = LoginID;
            Modal.EmpList = ClsCommon.GetDropDownList(EMP);

            List<DropDownList> dropDownListProject = new List<DropDownList>();
            GetResponse Project = new GetResponse();
            Project.Doctype = "ProjectReport";
            Project.LoginID = LoginID;
            Modal.ProjectList = ClsCommon.GetDropDownList(Project);
            ViewBag.listProject = ClsCommon.GetDropDownList(Project);
            GetResponse Earnings = new GetResponse();
            Earnings.Doctype = "Earnings";
            Earnings.LoginID = LoginID;
            // ViewBag.Earnings = ClsCommon.GetDropDownList(Earnings);
            List<DropDownList> dropDownLists = ClsCommon.GetDropDownList(Earnings);


            DropDownList dropDownList = new DropDownList();
            dropDownList.ID = -1;
            dropDownList.Name = "Salary";
            dropDownList.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownList);
            DropDownList dropDownListnew = new DropDownList();
            dropDownListnew.ID = -2;
            dropDownListnew.Name = "Fixed Benefits";
            dropDownListnew.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownListnew);
            DropDownList dropDownListn = new DropDownList();
            dropDownListn.ID = -3;
            dropDownListn.Name = "Provident Fund C3Contribution";
            dropDownListn.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownListn);
            Modal.EarningsList = dropDownLists;

            DropDownList dropDownListot = new DropDownList();
            dropDownListot.ID = -4;
            dropDownListot.Name = "OT";
            dropDownListot.ExtraValue = "Other Benefit";
            dropDownLists.Add(dropDownListot);
            Modal.EarningsList = dropDownLists;
            GetResponse Deduction = new GetResponse();
            Deduction.LoginID = LoginID;
            Deduction.Doctype = "Deduction";
            ViewBag.Deduction = ClsCommon.GetDropDownList(Deduction);

            return View(Modal);

        }
        [HttpPost]
        public ActionResult CustomizedReport(CustomizedReport Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet ds;
            //string SQL = "";
            ViewBag.dsdata = null;
            TempData["dsdata"] = null;

            if (Command == "Download")
            {
                if (Modal.Empid == "0")
                {
                    Modal.Empid = "All";
                }
                ds = Common_SPU.FnGetProjectWiseReport(Modal.Empid, Modal.fromDate, Modal.toDate, Convert.ToString(Modal.MitrUser), Convert.ToString(Modal.proj_id), Modal.Earnings);
                ViewBag.dsdata = ds;
                TempData["dsdata"] = ds;
                TempData["Reporttype"] = "SALARY";
            }


            return RedirectToAction("CustomizedReport", "Activity", new { src = src });
        }

        public ActionResult CustomizedReportProject(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            CustomizedReportProject Modal = new CustomizedReportProject();
            List<DropDownList> dropDownListProject = new List<DropDownList>();
            GetResponse Project = new GetResponse();
            Project.Doctype = "ProjectReport";
            Project.LoginID = LoginID;
            Modal.ProjectList = ClsCommon.GetDropDownList(Project);
            ViewBag.listProject = ClsCommon.GetDropDownList(Project);
            GetResponse Earnings = new GetResponse();
            Earnings.Doctype = "Earnings";
            Earnings.LoginID = LoginID;
            List<DropDownList> dropDownLists = ClsCommon.GetDropDownList(Earnings);
            DropDownList dropDownList = new DropDownList();
            dropDownList.ID = -1;
            dropDownList.Name = "Salary";
            dropDownList.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownList);
            DropDownList dropDownListnew = new DropDownList();
            dropDownListnew.ID = -2;
            dropDownListnew.Name = "Fixed Benefits";
            dropDownListnew.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownListnew);
            DropDownList dropDownListn = new DropDownList();
            dropDownListn.ID = -3;
            dropDownListn.Name = "Provident Fund C3Contribution";
            dropDownListn.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownListn);
            DropDownList dropDownListot = new DropDownList();
            dropDownListot.ID = -4;
            dropDownListot.Name = "OT";
            dropDownListot.ExtraValue = "Other Benefit";
            dropDownLists.Add(dropDownListot);
            Modal.EarningsList = dropDownLists;
            return View(Modal);

        }

        [HttpPost]
        public ActionResult CustomizedReportProject(CustomizedReportProject Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet ds;
            //string SQL = "";
            ViewBag.dsdata = null;
            TempData["dsdata"] = null;

            if (Command == "Download")
            {
                ds = Common_SPU.FnGetProjectReport(Modal.fromDate, Modal.toDate, Convert.ToString(Modal.MitrUser), Convert.ToString(Modal.pproj_id), Modal.Earnings);
                ViewBag.dsdata = ds;
                TempData["dsdata"] = ds;
                TempData["ReporttypeProject"] = "ProjectWise";
            }

            return RedirectToAction("CustomizedReportProject", "Activity", new { src = src });
        }

        public ActionResult CustomizedReportStaffWise(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            CustomizedReportStaffwise Modal = new CustomizedReportStaffwise();
            GetResponse Earnings = new GetResponse();
            Earnings.Doctype = "Earnings";
            Earnings.LoginID = LoginID;
            List<DropDownList> dropDownLists = ClsCommon.GetDropDownList(Earnings);
            DropDownList dropDownList = new DropDownList();
            dropDownList.ID = -1;
            dropDownList.Name = "Salary";
            dropDownList.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownList);
            DropDownList dropDownListnew = new DropDownList();
            dropDownListnew.ID = -2;
            dropDownListnew.Name = "Fixed Benefits";
            dropDownListnew.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownListnew);
            DropDownList dropDownListn = new DropDownList();
            dropDownListn.ID = -3;
            dropDownListn.Name = "Provident Fund C3Contribution";
            dropDownListn.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownListn);
            DropDownList dropDownListot = new DropDownList();
            dropDownListot.ID = -4;
            dropDownListot.Name = "OT";
            dropDownListot.ExtraValue = "Other Benefit";
            dropDownLists.Add(dropDownListot);
            Modal.EarningsList = dropDownLists;
            GetResponse EMP = new GetResponse();
            EMP.Doctype = "Employee";
            EMP.LoginID = LoginID;
            Modal.EmpList = ClsCommon.GetDropDownList(EMP);
            return View(Modal);

        }

        [HttpPost]
        public ActionResult CustomizedReportStaffWise(CustomizedReportStaffwise Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet ds;
            //string SQL = "";
            ViewBag.dsdata = null;
            TempData["dsdata"] = null;

            if (Command == "Download")
            {
                ds = Common_SPU.FnGetStaffWiseReport(Modal.fromDate, Modal.toDate, Convert.ToInt64(Modal.Empid), Modal.Earnings);
                ViewBag.dsdata = ds;
                TempData["dsdata"] = ds;
                TempData["ReporttypeProject"] = "StaffWise";
            }

            return RedirectToAction("CustomizedReportStaffWise", "Activity", new { src = src });
        }
        public ActionResult ConsolidatedStaffSalaryReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ConsolidatedStaffSalaryReport Modal = new ConsolidatedStaffSalaryReport();
            GetResponse Earnings = new GetResponse();
            Earnings.Doctype = "Earnings";
            Earnings.LoginID = LoginID;
            List<DropDownList> dropDownLists = ClsCommon.GetDropDownList(Earnings);
            DropDownList dropDownList = new DropDownList();
            dropDownList.ID = -1;
            dropDownList.Name = "Salary";
            dropDownList.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownList);
            DropDownList dropDownListnew = new DropDownList();
            dropDownListnew.ID = -2;
            dropDownListnew.Name = "Fixed Benefits";
            dropDownListnew.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownListnew);
            DropDownList dropDownListn = new DropDownList();
            dropDownListn.ID = -3;
            dropDownListn.Name = "Provident Fund C3Contribution";
            dropDownListn.ExtraValue = "Benefit";
            dropDownLists.Add(dropDownListn);
            DropDownList dropDownListot = new DropDownList();
            dropDownListot.ID = -4;
            dropDownListot.Name = "OT";
            dropDownListot.ExtraValue = "Other Benefit";
            dropDownLists.Add(dropDownListot);
            Modal.EarningsList = dropDownLists;
            GetResponse EMP = new GetResponse();
            EMP.Doctype = "Employee";
            EMP.LoginID = LoginID;
            Modal.EmpList = ClsCommon.GetDropDownList(EMP);
            return View(Modal);

        }
        [HttpPost]
        public ActionResult ConsolidatedStaffSalaryReport(ConsolidatedStaffSalaryReport Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet ds;
            //string SQL = "";
            ViewBag.dsdata = null;
            TempData["dsdata"] = null;

            if (Command == "Download")
            {
               
                ds = Common_SPU.FnGetConsolidatedStaffSalaryReport(Modal.fromDate, Modal.toDate, Modal.Empid, Modal.Earnings, Modal.MitrUser);
                ViewBag.dsdata = ds;
                TempData["dsdata"] = ds;
                TempData["ReporttypeProject"] = "ConsolidatedStaff";
            }

            return RedirectToAction("ConsolidatedStaffSalaryReport", "Activity", new { src = src });
        }

        public ActionResult ComponentWiseReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ComponentwiseReport Modal = new ComponentwiseReport();
            GetResponse Earnings = new GetResponse();
            Earnings.Doctype = "EarningsSingle";
            Earnings.LoginID = LoginID;
            List<DropDownList> dropDownLists = ClsCommon.GetDropDownList(Earnings);
            DropDownList dropDownList = new DropDownList();
            dropDownList.ID = -1;
            dropDownList.Name = "Salary";
            dropDownList.ExtraValue = "Salary(Benefit)";
            dropDownLists.Add(dropDownList);
            DropDownList dropDownListnew = new DropDownList();
            dropDownListnew.ID = -2;
            dropDownListnew.Name = "Fixed Benefits";
            dropDownListnew.ExtraValue = "Fixed Benefits(Benefit)";
            dropDownLists.Add(dropDownListnew);
            DropDownList dropDownListn = new DropDownList();
            dropDownListn.ID = -3;
            dropDownListn.Name = "Provident Fund C3Contribution";
            dropDownListn.ExtraValue = "Provident Fund C3Contribution(Benefit)";
            dropDownLists.Add(dropDownListn);
            DropDownList dropDownListot = new DropDownList();
            dropDownListot.ID = -4;
            dropDownListot.Name = "OT";
            dropDownListot.ExtraValue = "OT(Other Benefit)";
            dropDownLists.Add(dropDownListot);
            Modal.EarningsList = dropDownLists;
            GetResponse EMP = new GetResponse();
            EMP.Doctype = "Employee";
            EMP.LoginID = LoginID;
            Modal.EmpList = ClsCommon.GetDropDownList(EMP);
            return View(Modal);

        }
        [HttpPost]
        public ActionResult ComponentWiseReport(ComponentwiseReport Modal, string src, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DataSet ds;
            //string SQL = "";
            ViewBag.dsdata = null;
            TempData["dsdata"] = null;

            if (Command == "Download")
            {

                ds = Common_SPU.FnGetComponentWiseReport(Modal.fromDate, Modal.toDate, Modal.Empid, Modal.Earnings, Modal.MitrUser);
                ViewBag.dsdata = ds;
                TempData["dsdata"] = ds;
                TempData["ReporttypeProject"] = "ComponentwiseReport";
            }

            return RedirectToAction("ComponentWiseReport", "Activity", new { src = src });
        }
        public ActionResult AnnualCTCReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        public ActionResult ActivityProjectReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        public ActionResult AnualCTCTaxReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        public ActionResult DownloadEmployeeCTCExcel(int employeeId, string financialYear, string src)
        {
            // 1. Get template path
            string templatePath = Server.MapPath("~/Attachments/Templates/CTC_Template.xlsx");

            // 2. Output folder
            string outputFolder = Server.MapPath("~/ExportedCTC/");
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // 3. Load employee data
            EmployeeCTCModel empData = GetEmployeeCTCDataFromDB(employeeId, financialYear);
            if (empData == null)
                return new HttpStatusCodeResult(404, "Employee data not found");

            // 4. Generate Excel
            string outputFileName = $"CTC_{empData.EmployeeCode}_{financialYear}.xlsx";
            string outputPath = Path.Combine(outputFolder, outputFileName);
            FillCTCTemplate(templatePath, outputPath, empData);

            // 5. Return file to browser
            return File(outputPath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", outputFileName);
        }
        public EmployeeCTCModel GetEmployeeCTCDataFromDB(int empId, string year)
        {
            EmployeeCTCModel model = new EmployeeCTCModel();

            DataSet ds = Common_SPU.FnGetAnualTaxReport(empId, Convert.ToInt32(year));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                model.FinancialYear = row["Financial Year"]?.ToString();
                model.EmployeeCode = row["Employee Code"]?.ToString();
                model.EmployeeName = row["Employee Name"]?.ToString();
                model.EmploymentCategory = row["Employment Category"]?.ToString();
                model.JoiningDate = row["Date of Joining"]?.ToString();
                model.HourlyRate = row["Hourly Rate"] != DBNull.Value ? Convert.ToDecimal(row["Hourly Rate"]) : 0;
                model.SalaryOption = ""; // If you have this in another column like [Salary Structure], set it
                model.TotalPerYear= row["Total Per Year"] != DBNull.Value ? Convert.ToDecimal(row["Total Per Year"]) : 0;
                model.Basicpay = row["BASIC PAY"] != DBNull.Value ? Convert.ToDecimal(row["BASIC PAY"]) : 0;
                model.HRA = row["HRA"] != DBNull.Value ? Convert.ToDecimal(row["HRA"]) : 0;
                model.Transport = row["TRANSPORT/CEA/LTC"] != DBNull.Value ? Convert.ToDecimal(row["TRANSPORT/CEA/LTC"]) : 0;
                model.HRA = row["HRA"] != DBNull.Value ? Convert.ToDecimal(row["HRA"]) : 0;
                model.LIC = row["LIFE INSURANCE PREMIUM"] != DBNull.Value ? Convert.ToDecimal(row["LIFE INSURANCE PREMIUM"]) : 0;
                model.PFC3 = row["PROVIDENT FUND (C3'S CONTRIBUTION)"] != DBNull.Value ? Convert.ToDecimal(row["PROVIDENT FUND (C3'S CONTRIBUTION)"]) : 0;
                model.MedicalRemb = row["MEDICAL REIMBURSEMENT"] != DBNull.Value ? Convert.ToDecimal(row["MEDICAL REIMBURSEMENT"]) : 0;
                model.Bonus = row["BONUS"] != DBNull.Value ? Convert.ToDecimal(row["BONUS"]) : 0;
                model.Gratutity = row["GRATUITY"] != DBNull.Value ? Convert.ToDecimal(row["GRATUITY"]) : 0;
                model.MobileRemb = row["MEDICAL REIMBURSEMENT"] != DBNull.Value ? Convert.ToDecimal(row["MEDICAL REIMBURSEMENT"]) : 0;
                model.InternetRemb = row["INTERNET REIMBURSEMENT"] != DBNull.Value ? Convert.ToDecimal(row["INTERNET REIMBURSEMENT"]) : 0;
                model.MedicalAncident = row["MEDICAL / ACCIDENT INSURANCE"] != DBNull.Value ? Convert.ToDecimal(row["MEDICAL / ACCIDENT INSURANCE"]) : 0;
                model.NoOfHours = row["Number of Hours"] != DBNull.Value ? Convert.ToInt64(row["Number of Hours"]) : 0;
                model.AddBenefits = row["Add - Benefits"] != DBNull.Value ? Convert.ToDecimal(row["Add - Benefits"]) : 0;
                model.TotalSalary = row["Total Salary"] != DBNull.Value ? Convert.ToDecimal(row["Total Salary"]) : 0;


            }

            return model;
        }

        public void FillCTCTemplate(string templatePath, string outputPath, EmployeeCTCModel model)
        {
            FileInfo template = new FileInfo(templatePath);
            using (ExcelPackage package = new ExcelPackage(template))
            {
                var sheet1 = package.Workbook.Worksheets["Data Entry Field"];
                sheet1.Cells["B1"].Value = model.FinancialYear;
                sheet1.Cells["B2"].Value = model.EmployeeCode;
                sheet1.Cells["B3"].Value = model.EmployeeName;
                sheet1.Cells["B4"].Value = model.EmploymentCategory;
                sheet1.Cells["B7"].Value = model.WeeklyWorkingDays;
                sheet1.Cells["B8"].Value = model.SalaryOption;
                sheet1.Cells["B9"].Value = model.HourlyRate;
                sheet1.Cells["B10"].Value = model.JoiningDate;

                var sheet2 = package.Workbook.Worksheets["CTC Breakup"];
                sheet2.Cells["C4"].Value = model.FinancialYear;
                sheet2.Cells["C6"].Value = model.EmployeeCode;
                sheet2.Cells["C7"].Value = model.EmployeeName;
                sheet2.Cells["C10"].Value = model.TotalSalary;
                sheet2.Cells["C11"].Value = model.AddBenefits;
                sheet2.Cells["C12"].Value = model.TotalPerYear;

                sheet2.Cells["D23"].Value = model.Basicpay;
                sheet2.Cells["D24"].Value = model.HRA;
                sheet2.Cells["D24"].Value = model.Transport;
                sheet2.Cells["D27"].Value = model.TotalSalary;


                package.SaveAs(new FileInfo(outputPath));
            }
        }


    }
}