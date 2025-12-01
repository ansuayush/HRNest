using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface IExportHelper
    {
        void GetActiveStatusLog_Export(ActivityReport Modal);
        void TimeSheet_RPT(string sRepType, string sRepName, long EMPID, DateTime Date, string Emptype);
        void LeaveAvailedReport_RPT(string sRepType, string sRepName, long EMPID, DateTime Date);
        void GetLeaveAvailedLapsed_Export(  DateTime Date);
        void GetAnnualPackageDetails_Export(DateTime Date);
        string GetAppraisal_RPT(long EMPID, long Fyid);
    }
}