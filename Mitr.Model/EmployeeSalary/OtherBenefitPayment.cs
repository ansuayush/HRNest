using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model
{
    public class OtherBenefitPayment
    {
        public long FYID { get; set; }
        public int ComponentID { get; set; }
        public string PaidDate { get; set; }

        public int ID { get; set; }
        public long EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string Category { get; set; }
        public string fromDate { get; set; }
        public string Todate { get; set; }

        
        public decimal PaidAmount { get; set; }
        public List<OtherBenefitPayment> OtherBenefitPaymentList { get; set; }
        public List<OtherBenefitPaymentComponent> OtherBenefitPaymentComponentList { get; set; }
        public List<OtherBenefitPayment> FinancialYearList { get; set; }
        public List<DropdownModel> EmployeeList { get; set; }
        public class FinList
        {
            public long ID { get; set; }
            public string year { get; set; }
        }



        public class OtherBenefitPaymentComponent
        {
            public int ID { get; set; }
            public long EmpID { get; set; }
            public int ComponentID { get; set; }
            public string Component { get; set; }
            public string PaidDate { get; set; }
            public string Entitlement { get; set; }
            public decimal Paid { get; set; }
            public decimal PaidAmount { get; set; }
            public decimal Balance { get; set; }            
            public string DeviceType { get; set; }
            public long Createdby { get; set; }
            public long Modifiedby { get; set; }
            public long Deletedby { get; set; }
            public string CreateDat { get; set; }
            public string ModifieDat { get; set; }
            public string DeleteDat { get; set; }
            public int IsDeleted { get; set; }
            public bool IsActive { get; set; }
           
        }

    }
}
