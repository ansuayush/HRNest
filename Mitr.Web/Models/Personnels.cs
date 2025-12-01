using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Personnels
    {

        public class ReimbursementBooking
        {
            public class FYList
            {
                public int RowNum { get; set; }
                public long FYId { get; set; }
                public long EmpId { get; set; }
                public string EmployeeCode { get; set; }
                public string EmployeeName { get; set; }
                public string FinancialYear { get; set; }
                public long CFYId { get; set; }
            }
            public class ReimbursementHead
            {
                public long FYId { get; set; }
                public long EmpId { get; set; }
                public string EmployeeCode { get; set; }
                public string EmployeeName { get; set; }
                public string Location { get; set; }
                public string Designation { get; set; }
                public long CFYId { get; set; }
                public List<ClaimHead> claimhead { get; set; }
            }

            public class ClaimHead
            {
                public int RowNum { get; set; }
                public long FYId { get; set; }
                public long EmpId { get; set; }
                public long HeadId { get; set; }
                public string HeadName { get; set; }
                public float Entitlement { get; set; }
                public float UnderProcess { get; set; }
                public float PaidAmount { get; set; }
                public float BalanceAmt { get; set; }
                public List<ClaimVoucher> claimvoucher { get; set; }
            }

            public class ClaimVoucher
            {
                public long FYId { get; set; }
                public long EmpId { get; set; }
                public long ClaimId { get; set; }
                public long HeadId { get; set; }
                public string EmployeeCode { get; set; }
                public string EmployeeName { get; set; }
                public string Location { get; set; }
                public string Designation { get; set; }
                public float BalanceAmt { get; set; }
                public string ClaimType { get; set; }
                public string RequestNo { get; set; }
                public string RequestDate { get; set; }
                public float TotalAmount { get; set; }
                public float PaidAmount { get; set; }
                public string PaidDate { get; set; }
                public string Status { get; set; }
                public int IsDeleted { get; set; }
                public int Approved { get; set; }
                public string Reason { get; set; }
                public string ProjectCode { get; set; }
                public string AccountNo { get; set; }
                public string BankName { get; set; }
                public string NeftCode { get; set; }
                public string Branch { get; set; }
                public long CFYId { get; set; }
                public List<ClaimInsuranceEntry> insuranceentry { get; set; }
                public List<ClaimMedicalEntry> medicalentry { get; set; }
                public List<ClaimOtherEntry> otherentry { get; set; }
            }

            public class ClaimInsuranceEntry
            {
                public int RowNum { get; set; }
                public long Id { get; set; }
                public long ClaimId { get; set; }
                public string InsuranceCompany { get; set; }
                public string PolicyNo { get; set; }
                public string StartDate { get; set; }
                public string EndDate { get; set; }
                public string InsuredPerson { get; set; }
                public float PremiumAmount { get; set; }
                public string ReceiptNo { get; set; }
                public string ReceiptDate { get; set; }
                public string Reason { get; set; }
            }

            public class ClaimMedicalEntry
            {
                public int RowNum { get; set; }
                public long Id { get; set; }
                public string BillNo { get; set; }
                public string BillDate { get; set; }
                public float BillAmt { get; set; }
                public string PatientName { get; set; }
                public string PatientRelation { get; set; }
                public string Remark { get; set; }
            }

            public class ClaimOtherEntry
            {
                public int RowNum { get; set; }
                public long Id { get; set; }
                public long Srno { get; set; }
                public string BillNo { get; set; }
                public string BillDate { get; set; }
                public float BillAmt { get; set; }
                public string DetailsBill { get; set; }
                public string Particulars { get; set; }
                public string AttachmentNo { get; set; }
            }
        }

        public class FinYearList
        {
            public long ID { get; set; }
            public string Year { get; set; }
        }

        public class ClaimEmployees
        {
            public long EmpId { get; set; }
            public string EmpName { get; set; }
        }

        public class ProjectList
        {
            public long Id { get; set; }
            public string ProjectCode { get; set; }
            public string ProjectName { get; set; }
        }

        public class FinancePaymentEntry
        {
            public long FYId { get; set; }
            public long EmpId { get; set; }
            public int Approve { get; set; }
            public List<FinYearList> finyear { get; set; }
            public List<ClaimEmployees> claimemployees { get; set; }
        }

        public class FinancePaymentList
        {
            public long FYId { get; set; }
            public long EmpId { get; set; }
            public string EmpCode { get; set; }
            public string EmpName { get; set; }
            public long ClaimId { get; set; }
            public string RequestNo { get; set; }
            public string RequestDate { get; set; }
            public long HeadId { get; set; }
            public string HeadName { get; set; }
            public decimal ClaimAmt { get; set; }
            public decimal BalanceAmt { get; set; }
            public int Approved { get; set; }
            public string Reason { get; set; }
        }

        public class FinanceViewVoucher
        {
            public long FYId { get; set; }
            public long EmpId { get; set; }
            public string EmployeeCode { get; set; }
            public string EmployeeName { get; set; }
            public string Location { get; set; }
            public string Designation { get; set; }
            public long ClaimId { get; set; }
            public long HeadId { get; set; }
            public string HeadName { get; set; }
            public string ClaimType { get; set; }
            public string RequestNo { get; set; }
            public string RequestDate { get; set; }
            public decimal Entitlement { get; set; }
            public decimal UnderProcess { get; set; }
            public decimal UptoPaid { get; set; }
            public decimal BalanceAmt { get; set; }
            public decimal TotalAmount { get; set; }
            public int Approved { get; set; }
            public string Action { get; set; }

            public string PaidDate { get; set; }
            public decimal PaidAmount { get; set; }
            public int ProjectId { get; set; }
            public string AccountNo { get; set; }
            public string BankName { get; set; }
            public string NeftCode { get; set; }
            public string Branch { get; set; }
            public string Reason { get; set; }
            public List<ProjectList> projectcode { get; set; }
            public List<ReimbursementBooking.ClaimInsuranceEntry> insuranceentry { get; set; }
            public List<ReimbursementBooking.ClaimMedicalEntry> medicalentry { get; set; }
            public List<ReimbursementBooking.ClaimOtherEntry> otherentry { get; set; }
        }
    }


    public class SpecialAllowance
    {
        public long Empid { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string WorkLocation { get; set; }
        public string EmploymentType { get; set; }
        public string MedicalReimbursementEntitlementAmount { get; set; }
        public string PaidAmount { get; set; }
        public string Balance { get; set; }
        public string Dateofjoining { get; set; }
        public string DateofResignation { get; set; }
        public string HourlyRate { get; set; }
        public string Structure { get; set; }
        public string Bonus { get; set; }
    }
    public class ConsolidatedActivityLog
    {
        public long Empid { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Dateofjoining { get; set; }
        public string WorkLocation { get; set; }
        public string ProjectName { get; set; }
        public string Activity { get; set; }
        public string Description { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string FinancialYear { get; set; }
        

        public List<ConsolidatedActivityLog> Emplist { get; set; }
    }


    
}