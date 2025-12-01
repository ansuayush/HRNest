using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using static Mitr.Models.AllEnum;
using Mitr.Model;

namespace Mitr.Models
{
    public class SalaryStructure
    {
        public class Tabs
        {
            public string Approve { get; set; }
            public string Date { get; set; }
            public MitrNonMitr? MitrUser { get; set; }
        }
        public class GetStructureListResponse
        {
            public long id { get; set; }
            public long EmployeeSalaryid { get; set; }
            public long Empid { get; set; }
            public string AnnualSalary { get; set; }
            public long Structureid { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
        }
        public class Grade
        {
            public class List
            {
                public long id { get; set; }
                public string Grade { get; set; }
                public string Role { get; set; }
                public string Designationid { get; set; }
                public string JobTitleid { get; set; }
                public string Remark { get; set; }
                public string Designation { get; set; }
                public string JobTitle { get; set; }
                public string createdat { get; set; }
                public string modifiedat { get; set; }
                public bool IsActive { get; set; }
            }

            public class Add
            {
                public long? id { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string Grade { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string Role { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string JobTitleid { get; set; }
                public string Remark { get; set; }
                public long LoginID { get; set; }
            }
        }
        public class EarningStructure
        {
            public string Category { get; set; }
            public string Configured { get; set; }
        }
        public class EarningStructureList
        {
            public long? id { get; set; }
            public string Category { get; set; }
            public int Componentid { get; set; }
            public string Component { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string Percentage { get; set; }
            public string StartMonth { get; set; }
            public string EndMonth { get; set; }
            public string IPAddress { get; set; }
            public long LoginID { get; set; }
            public string Doctype { get; set; }
        }
        public class BenefitStructureList
        {
            public string RowNum { get; set; }
            public int id { get; set; }
            public string Structure { get; set; }
            public string Category { get; set; }
            public string IPAddress { get; set; }
        }
        public class BenefitStructureAdd
        {
            public int? id { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string Structure { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public EmploymentTerm? Category { get; set; }
            public string IPAddress { get; set; }
            public int LoginID { get; set; }
            public List<BenefitStructureDet> LstDet { get; set; }
        }
        public class BenefitStructureDet
        {
            public int? id { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public int? Benefitid { get; set; }
            public int? Structureid { get; set; }
            public string Benefit { get; set; }
            public int? isactive { get; set; }
            public SS_CalculationType? CalculationType { get; set; }
            public decimal? Amount { get; set; }
            public int? Formulaid { get; set; }
            public int LoginID { get; set; }

        }
        public class EmployeeSalary
        {
            public class List
            {
                public int RowNum { get; set; }
                public long id { get; set; }
                public long Empid { get; set; }
                public string emp_name { get; set; }
                public string emp_code { get; set; }
                public string Designation { get; set; }
                public string Department { get; set; }
                public string Location { get; set; }
                public string Salary { get; set; }
                public string DateOfJoining { get; set; }
                public string EmploymentTerm { get; set; }
                public string Subcategory { get; set; }
                public string WorkingHours { get; set; }
                public string Step { get; set; }
                public string HourlyRate { get; set; }
                public string Structure { get; set; }
                public string CTC { get; set; }
                public string AnnualSalary { get; set; }

            }
            public class Add
            {
                public long id { get; set; }
                public long Nextid { get; set; }
                public long Previousid { get; set; }
                public long TotalRecordsCount { get; set; }
                public long CurrentRecord { get; set; }
                public long Empid { get; set; }
                public string emp_name { get; set; }
                public string emp_code { get; set; }
                public string Designation { get; set; }
                public string Department { get; set; }
                public string Location { get; set; }
                public string Salary { get; set; }
                public string DateOfJoining { get; set; }
                public string EmploymentTerm { get; set; }
                public decimal WorkingHours { get; set; }
                public string Step { get; set; }
                public string HourlyRate { get; set; }
                public string Structure { get; set; }
                public decimal AnnualSalary { get; set; }
                public string CTC { get; set; }
                public int TotalHours { get; set; }
                public string Role { get; set; }
                public long Gradeid { get; set; }
                public int IsDraft { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long Subcategoryid { get; set; }
                public string Subcategory { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field.")]
                public long Structureid { get; set; }

                [Required(ErrorMessage = "Hey! You missed this field.")]
                public string EffectiveDate { get; set; }
                public string OldEffectiveDate { get; set; }
                public string Days { get; set; }
                public int LoginID { get; set; }
                public string FromDate { get; set; }
                public string ToDate { get; set; }

                public List<DropDownList> lstGrade { get; set; }
                public List<DropDownList> lstSubcategory { get; set; }
                public List<DropDownList> lstStructure { get; set; }
                public List<StructureList> lstSalryStructure { get; set; }
                public List<Attachments> lstAttachment { get; set; }
                public List<DropDownList> lstEmp { get; set; }
            }

            public class StructureList
            {
                public long id { get; set; }
                public long EmployeeSalaryid { get; set; }
                public long Componentid { get; set; }
                public string Component { get; set; }
                public string CalculationType { get; set; }
                public string Percentage { get; set; }
                public string Amount { get; set; }
                public string Doctype { get; set; }
                public string Taxable { get; set; }
                public int LoginID { get; set; }
                public long Empid { get; set; }
                public decimal AnnualSalary { get; set; }
                public string HourlyRate { get; set; }
                public string EffectiveDate { get; set; }


            }
            public class Detail
            {
                public int RowNum { get; set; }
                public long id { get; set; }
                public long Empid { get; set; }
                public string emp_name { get; set; }
                public string emp_code { get; set; }
                public string Designation { get; set; }
                public string Department { get; set; }
                public string Location { get; set; }
                public string DateOfJoining { get; set; }
                public string WorkingHours { get; set; }
                public long Structureid { get; set; }
                public string EffectiveDate { get; set; }
                public string AnnualSalary { get; set; }
                public string TotalBenefit { get; set; }
                public string CTC { get; set; }
                public string WorkingHoursTotal { get; set; }
                public string HourlyRate { get; set; }

                public List<StructureList> ListStructure { get; set; }
                public List<StructureListMonthly> ListStructureMonthly { get; set; }

            }
            public class StructureListMonthly
            {
                public string Month { get; set; }
                public string TotalHours { get; set; }
                public string HourlyRate { get; set; }
                public string TotalAmount { get; set; }
            }
        }

        public class DeductionEntry
        {
            public string Emptype { get; set; }
            public string Month { get; set; }
            public List<DeductionEntryEmp> Emplist { get; set; }
            public List<DeductionEntryComponent> Component { get; set; }
        }
        public class DeductionEntryEmp
        {
            public int RowNum { get; set; }
            public long id { get; set; }
            public string Checkbox { get; set; }
            public long Empid { get; set; }
            public string emp_name { get; set; }
            public string emp_code { get; set; }
            public string Designation { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
            public string Salary { get; set; }
            public string DateOfJoining { get; set; }
            public string EmploymentTerm { get; set; }
            public string Subcategory { get; set; }
            public string WorkingHours { get; set; }
            public string Step { get; set; }
            public string HourlyRate { get; set; }
            public string Structure { get; set; }
            public string CTC { get; set; }
            public string AnnualSalary { get; set; }
            public Dictionary<long, float> ComponentAmt { get; set; }
            //public List<DeductionEntryComponent> LstComponent { get; set; }
        }
        public class DeductionEntryComponent
        {
            public long Id { get; set; }
            public long Empid { get; set; }
            public string Component { get; set; }
            public float Amt { get; set; }

        }
        public class OtherEarningEntry
        {
            public string Emptype { get; set; }
            public string Month { get; set; }
            public List<OtherEarningEntryEmp> Emplist { get; set; }
            public List<OtherEarningEntryComponent> Component { get; set; }
        }
        public class OtherEarningEntryEmp
        {
            public int RowNum { get; set; }
            public long id { get; set; }
            public string Checkbox { get; set; }
            public long Empid { get; set; }
            public string emp_name { get; set; }
            public string emp_code { get; set; }
            public string Designation { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
            public string Salary { get; set; }
            public string DateOfJoining { get; set; }
            public string EmploymentTerm { get; set; }
            public string Subcategory { get; set; }
            public string WorkingHours { get; set; }
            public string Step { get; set; }
            public string HourlyRate { get; set; }
            public string Structure { get; set; }
            public string CTC { get; set; }
            public string AnnualSalary { get; set; }
            public Dictionary<long, float> ComponentAmt { get; set; }
        }
        public class OtherEarningEntryComponent
        {
            public long Id { get; set; }
            public long Empid { get; set; }
            public string Component { get; set; }
            public float Amt { get; set; }

        }


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
}