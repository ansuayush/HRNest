using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class SalaryReport
    {
         public string EmployeeCode { get; set; }
        public string PaymentType { get; set; }
        public string PaymentDate { get; set; }
        public string BeneficaryName { get; set; }
        public string BeneficaryAccountNo { get; set; }
        public string BeneficaryBankCode { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public string EmailId { get; set; }
        public string DebitAccountNumber { get; set; }
        public string Type { get; set; }
        
    }
}