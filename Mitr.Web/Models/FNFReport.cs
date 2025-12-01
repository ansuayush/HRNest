using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class FNFReport
    {
        public long Id { get; set; }
        public long FId { get; set; }
        public string emp_name { get; set; }
        public string emp_code { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string DateOfJoining { get; set; }
        public string LastWorkingday { get; set; }
        public string DateofResignation { get; set; }
        public string Status { get; set; }
        public string SalaryStructure { get; set; }
        public string EmployeeCategory { get; set; }
        public string EmployeeSubCategory { get; set; }
        public string HourlyRate { get; set; }
        public long AnnualLeaveBalance { get; set; }
        public long CLSLBalance { get; set; }
        public string NoticePeriodRecovery { get; set; }
        public string NoticePeriodWaived { get; set; }
        public long PaidColspan { get; set; }
        public long PaybleColspan { get; set; }
        public string CompanyAddress { get; set; }
        public List<FNFReport> fNFReportcslist { get; set; }

    }
    public class FinList
    {
        public long ID { get; set; }
        public string year { get; set; }
    }
}