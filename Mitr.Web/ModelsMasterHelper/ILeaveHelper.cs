using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
     interface ILeaveHelper
    {
        LeaveDashboard GetLeaveDashboard(long EMPID);
        LeaveDetails GetLeaveDetails(long EMPID, long LeaveLogID, DateTime dDate);
        SeniorLeaveDashboard GetSeniorLeaveDashboard(long EMPID);
        List<LeaveBalanceDetails> GetLeaveBalanceList(long IEMPID, int iMonth, int iYear);
        List<HolidayDailyLog> GetHoliTrvAndLeaveList(long EMPID);
        List<Calendardata> GetCalenedarData(long EMPID);
        List<LeaveType> GetLeaveType();
        List<LeaveType> GetLeaveTypebyEmp(string Empid);
        LeaveEmp GetLeaveEmpDetails(long EMPID);
        string ValidateCLSL(List<LeaveTran> Modal);
        string ValidateLeaveRequest(List<LeaveTran> Modal, DateTime ExpectedDeliveryDate);
        LeaveAttachmentCount GetAttachmentRequiredCount();
        List<LeaveNonMITRAdd> GetLeaveNonMITRAddList(LeaveNonMitrMonth Modal);
        List<BudgetMaster.EmployeeList> GetLeaveEmpList(string EMPType);


    }
}