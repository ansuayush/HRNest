using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Models
{
    public class EmployeeCTCModel
    {
        public string FinancialYear { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmploymentCategory { get; set; }
        public int WeeklyWorkingDays { get; set; }
        public string SalaryOption { get; set; }
        public decimal HourlyRate { get; set; }
        public string JoiningDate { get; set; }
        public long NoOfHours { get; set; }

        public decimal TaxRegime1TotalPayable { get; set; }
        public decimal TaxRegime2TotalPayable { get; set; }
        public decimal TaxRegime1Balance { get; set; }
        public decimal TaxRegime2Balance { get; set; }
        public int BalanceMonths { get; set; }
        public decimal AvgMonthlyPayable1 { get; set; }
        public decimal AvgMonthlyPayable2 { get; set; }

        public decimal YearlySalary { get; set; }
        public decimal AddBenefits { get; set; }
        public decimal TotalPerYear { get; set; }
        public decimal Basicpay { get; set; }
        public decimal HRA { get; set; }
        public decimal Transport { get; set; }
        public decimal Overtime { get; set; }
        public decimal LIC { get; set; }
        public decimal PFC3 { get; set; }
        public decimal MedicalRemb { get; set; }
        public decimal Bonus { get; set; }
        public decimal Gratutity { get; set; }
        public decimal MobileRemb { get; set; }
        public decimal InternetRemb { get; set; }
        public decimal MedicalAncident { get; set; }
        public decimal TotalSalary { get; set; }


    }
}
