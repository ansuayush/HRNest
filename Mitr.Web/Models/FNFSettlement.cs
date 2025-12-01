using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class FNFSettlement
    {
        [Required]
        public int EMPID { get; set; }
        public List<FNFEmployeeList> EmployeeList { get; set; }
    }
    public class FNFEmployeeList
    {
        public string emp_name { get; set; }
        public int EMPID { get; set; }
    }

    public class FNFEmployeeDetails
    {
        public int EMPID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Designation { get; set; }
        public string LocationName { get; set; }
        public string LeaveType { get; set; }
        public decimal HourlyRate { get; set; }

        public int FinID { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public DateTime doc_date { get; set; }
        public DateTime DateofJoining { get; set; }
        public DateTime DateofLeaving { get; set; }
        public string AnnualLeaveBalance { get; set; }
        public string CLSBalance { get; set; }
        public int NoticePayable { get; set; }
        public int NoticeRecovery { get; set; }

        public decimal BasicDue { get; set; }
        public decimal BasicDrawn { get; set; }
        public decimal BasicPayable { get; set; }

        public decimal HouseRentDue { get; set; }
        public decimal HouseRentDrawn { get; set; }
        public decimal HouseRentPayable { get; set; }

        public decimal TransportDue { get; set; }
        public decimal TransportDrawn { get; set; }
        public decimal TransportPayable { get; set; }

        public decimal LICDue { get; set; }
        public decimal LICDrawn { get; set; }
        public decimal LICPayable { get; set; }

        public decimal EPFDue { get; set; }
        public decimal EPFDrawn { get; set; }
        public decimal EPFPayable { get; set; }

        public decimal MedicalDue { get; set; }
        public decimal MedicalDrawn { get; set; }
        public decimal MedicalPayable { get; set; }

        public decimal BonusDue { get; set; }
        public decimal BonusDrawn { get; set; }
        public decimal BonusPayable { get; set; }

        public decimal NPALDue { get; set; }
        public decimal NPALDrawn { get; set; }
        public decimal NPALPayable { get; set; }
        public decimal GratuityDue { get; set; }
        public decimal GratuityDrawn { get; set; }
        public decimal GratuityPayable { get; set; }

        public decimal PFContribtionDue { get; set; }
        public decimal PFContribtionDrawn { get; set; }
        public decimal PFContribtionPayable { get; set; }

        public decimal TDSDue { get; set; }
        public decimal TDSDrawn { get; set; }
        public decimal TDSPayable { get; set; }


        public decimal AdvancesDue { get; set; }
        public decimal AdvancesDrawn { get; set; }
        public decimal AdvancesPayable { get; set; }

        public decimal ALRecoverDue { get; set; }
        public decimal ALRecoverDrawn { get; set; }
        public decimal ALRecoverPayable { get; set; }

        public decimal OtherdeductionDue { get; set; }
        public decimal OtherdeductionDrawn { get; set; }
        public decimal OtherdeductionPayable { get; set; }


        public decimal GrandGrossSalaryDue { get; set; }
        public decimal GrandGrossSalarDrawn { get; set; }
        public decimal GrandGrossSalaryPayable { get; set; }

        public decimal GrandTotalSalaryBenefitsDue { get; set; }
        public decimal GrandTotalSalaryBenefitsDrawn { get; set; }
        public decimal GrandTotalSalaryBenefitsPayable { get; set; }

        public decimal GrandNetDue { get; set; }
        public decimal GrandNetDrawn { get; set; }
        public decimal GrandNetPayable { get; set; }
    }

}