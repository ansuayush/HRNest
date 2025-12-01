using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mitr.Model
{
    public class Bonus
    {
        public long FYID { get; set; }
        public string Component { get; set; }
        public string PaidDate { get; set; }

        public long EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string Category { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string DateofJoining { get; set; }
        public string Entitlement { get; set; }
        public decimal Paid { get; set; }
        public decimal Balance { get; set; }
        public decimal Previous { get; set; }
        public int Status { get; set; }

        public List<Bonus> EmployeeList { get; set; }
        public List<Bonus> FinancialYearList { get; set; }
        public List<Bonus> ComponentList { get; set; }
        public class FinList
        {
            public long ID { get; set; }
            public string year { get; set; }
        }
    }

    public class BonusPaymentEntry
    {
        public long ID { get; set; }
        public string FinancialYearID { get; set; }
        public string Component { get; set; }
        public long EmpID { get; set; }
        public string DocNo { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public string PaidDate { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public decimal PaidAmount { get; set; }
        public string Remark { get; set; }
        public string DeviceType { get; set; }
        public long Createdby { get; set; }
        public long Modifiedby { get; set; }
        public long Deletedby { get; set; }
        public string CreateDat { get; set; }
        public string ModifieDat { get; set; }
        public string DeleteDat { get; set; }
        public int IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string DocType { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public List<DropdownModel> ProjectList { get; set; }
        public List<BonusPaymentEntry> ProjectPaaymentAmountList { get; set; }
        public List<BonusPaymentEntry> BonusPaymentEntryList { get; set; }

    }
    public class Test
    {
        public long ID { get; set; }

        public string Component { get; set; }
    }

}
