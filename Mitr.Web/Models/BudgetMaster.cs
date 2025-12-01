using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class BudgetMaster
    {

        public class TravelBudgetMasterList
        {
            public long Id { get; set; }
            public DateTime BudgetYear { get; set; }
        }
        public class AddTravelBudget
        {
            public long Id { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public long TravelYear { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public string TravelType { get; set; }
            [Range(0, int.MaxValue, ErrorMessage = "Hey! You missed this field")]
            public decimal AirFare { get; set; }
            [Range(0, int.MaxValue, ErrorMessage = "Hey! You missed this field")]
            public decimal PerDiem { get; set; }
            [Range(0, int.MaxValue, ErrorMessage = "Hey! You missed this field")]
            public decimal Hotel { get; set; }
            [Range(0, int.MaxValue, ErrorMessage = "Hey! You missed this field")]
            public decimal LocalTravel { get; set; }
            public decimal Total { get; set; }
            public List<TravelBudgetList> TravelBudgetList { get; set; }
        }
        public class TravelBudgetList
        {
            public long Id { get; set; }
            public string TravelType { get; set; }
            public decimal AirFare { get; set; }
            public decimal PerDiem { get; set; }
            public decimal Hotel { get; set; }
            public decimal LocalTravel { get; set; }
            public decimal Total { get; set; }
            public long TravelYear { get; set; }
        }
        public class TrainingWorkshopSeminarTypes
        {
            public long Id { get; set; }
            public string Module { get; set; }
            public string TrainingName { get; set; }
        }
        public class AddTrainingWorkshopSeminarTypes
        {
            public long Id { get; set; }
            [Required(ErrorMessage = "Hey ! You missed this field")]
            public string Module { get; set; }
            [Required(ErrorMessage = "Hey ! You missed this field")]
            public string TrainingName { get; set; }
            public List<AddTrainingWorkshopSeminarDeatils> ListTrainingDetails { get; set; }
        }
        public class AddTrainingWorkshopSeminarDeatils
        {

            public long Id { get; set; }
            public long TrainingWorkshopid { get; set; }
            public string Item { get; set; }
            public string Unit { get; set; }
            public string Rate { get; set; }
        }
        public class InflationRate
        {
            public long Id { get; set; }
            public string Year { get; set; }
        }
        public class AddInflationRate
        {
            public long Id { get; set; }

            [Required(ErrorMessage = "Year Name Can't Blank")]
            public long Year { get; set; }
            public List<AddInflationRateDeatils> InflationList { get; set; }
            public List<InflationRate> InfMainList { get; set; }
        }
        public class AddInflationRateDeatils
        {
            public long Id { get; set; }
            public long LedgerId { get; set; }
            public string Ledger { get; set; }
            public long Year { get; set; }
            public decimal InflationRate { get; set; }

        }
        public class IndirectCostRate
        {
            public long Id { get; set; }
            [Required(ErrorMessage = "Year Name Can't Blank")]
            public long Year { get; set; }
            public string SetYear { get; set; }
            public List<AddIndirectCostRate> listIndirectCostRate { get; set; }
        }
        public class AddIndirectCostRate
        {
            public long Id { get; set; }
            public long Year { get; set; }
            public long LedgerId { get; set; }
            public string Ledger { get; set; }
            public decimal IndirectCostRate { get; set; }

        }
        public class FringeBenefitRate
        {
            public long Id { get; set; }
            [Required(ErrorMessage = "Year Name Can't Blank")]
            public long Year { get; set; }
            public string SetYear { get; set; }
            public List<AddFringeBenefitRate> listAddFringeBenefitRate { get; set; }
        }
        public class AddFringeBenefitRate
        {
            public long Id { get; set; }
            public long Year { get; set; }
            public string Category { get; set; }

            public decimal Rate { get; set; }
        }
        public class BudgetAuthoritySetting
        {
            public long Id { get; set; }
            public long FinancePerson { get; set; }
            public List<EmployeeList> EMPList { get; set; }
        }
        public class EmployeeList
        {
            public long Id { get; set; }
            public long FinancePerson { get; set; }
            public string Name { get; set; }
        }

        //Create Budget class 
        public class Budget
        {
            public long Id { get; set; }

            public string BudgetType { get; set; }
            public string ThematicAreaId { get; set; }
            public string LocationId { get; set; }
            public long Projectid { get; set; }
            public string Purpose { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string DonarName { get; set; }
            public List<GetBudgetRPF> listGetBudgetRpf { get; set; }
            public List<SublineActivityList> listSublineActivityList { get; set; }
            public List<SublineActivityList> listSublineActivitydraftList { get; set; }
            public List<StandaloneBudget> listAloneBudget { get; set; }
        }
        public class StandaloneBudget
        {
            public long Id { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string ThematicAreaName { get; set; }
            public string BudgetName { get; set; }
            public long ThematicAreaId { get; set; }
            public string Geography { get; set; }
            public string Purposeobjective { get; set; }
        }
        public class OutcomesActivities
        {
            public long Id { get; set; }
            public long ProjectId { get; set; }
            public long BudgetId { get; set; }
            public string ProjectName { get; set; }
            public string DonarName { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public long isdeleted { get; set; }
            public List<OutcomeDetails> ListOutcomeDetails { get; set; }
            public List<Activitydetails> ListActivitydetails { get; set; }
            public List<DropDownList> listoutcome { get; set; }
            public List<DropDownList> listactivity { get; set; }
            public BudgetDetails GetBudget { get; set; }
        }
        public class OutcomeDetails
        {
            public long Id { get; set; }
            public long BudgetId { get; set; }
            public string Outcome { get; set; }
            public long isdeleted { get; set; }
        }
        public class BudgetDetails
        {
            public string Doc { get; set; }
            public string Donar_Name { get; set; }
            public string Project_Name { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            
        }
        public class Activitydetails
        {
            public long Id { get; set; }
            public long BudgetId { get; set; }
            public string Activity { get; set; }
            public long isdeleted { get; set; }
        }
        public class SetbudgetsubActivity
        {
            public long BudgetId { get; set; }
            public long Projectid { get; set; }
            public long isdeleted { get; set; }
            public List<BudgetActivity> listActivity { get; set; }
            public List<DropDownList> listoutcome { get; set; }

        }
        public class BudgetActivity
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public List<SubActivity> listSubActivity { get; set; }
        }

        public class SubActivity
        {
            public long Id { get; set; }
            public long Activityid { get; set; }
            public string Activity { get; set; }
            public string Subactivity { get; set; }
            public long isdeleted { get; set; }
            public long BudgetRequired { get; set; }
            public long Outcomeid { get; set; }
        }
        public class SetTentativePeriodofBudget
        {
            public long Id { get; set; }
            public long BudgetId { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public long ProjectId { get; set; }
            public string ReasonInflationRate { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public decimal FringeRateCore { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public decimal FringeRateTerm { get; set; }
            //[Required(ErrorMessage = "Hey! You missed this field")]
            public string ReasonFringeRate { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public string FringeShownAs { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public string IndirectCost { get; set; }
            //[Required(ErrorMessage = "Hey! You missed this field")]
            public string ReasonIndirectRate { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public long OtherCurrencyid { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
           
            public decimal OtherCurrencyRate { get; set; }
            public int? OutcomeWiseBudget { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public long isdeleted { get; set; }
            public List<InflationDetails> ListinflationDetails { get; set; }
            public List<IndirectDetails> ListindirectDetails { get; set; }
      


        }
        public class InflationDetails
        {
            public long Id { get; set; }
            public long BudgetId { get; set; }
            public long Ledgerid { get; set; }
            public string Ledger { get; set; }
            public decimal Rate { get; set; }
        }
        public class IndirectDetails
        {
            public long Id { get; set; }
            public long BudgetId { get; set; }
            public long Ledgerid { get; set; }
            public string Ledger { get; set; }
            public decimal Rate { get; set; }
        }
        public class SetBudgetEmployeeSallary
        {
            public long Id { get; set; }
            public long BudgetId { get; set; }
            public long EmployeeId { get; set; }
            public string EmoloyeeName { get; set; }

            public string EmpCode { get; set; }
            public string DepartmentName { get; set; }
            public string LocationName { get; set; }
            public string SubCategory { get; set; }
            public string WorkingDays { get; set; }
            public string Email { get; set; }
            public string EmployeeTerm { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public List<ListMonthYear> listMonthYears { get; set; }
        }
        public class ListMonthYear
        {
            public string Month { get; set; }
            public int Year { get; set; }
        }
        public class SetBudget
        {
            public long Id { get; set; }
            public long Budgetid { get; set; }
            public long Projectid { get; set; }
            public long? Finyearid { get; set; }
            public long Ledgerid { get; set; }
            public long DelegationAuthority { get; set; }
            public string EntryType { get; set; }
            public string LedgerName { get; set; }
            public List<ProjectBudget> listProjectBudget { get; set; }
            public List<ProjectBudgetYear> listProjectBudgetYear { get; set; }
            public List<ProjectBudgetLedger> listProjectBudgetLedger { get; set; }
            public List<ProjectBudgetEmployee> listProjectBudgetEmployee { get; set; }
            public List<BudgetSublineActivity> listBudgetSublineActivity { get; set; }
            public List<DropDownList> ActivityList { get; set; }
            public List<DropDownList> SubActivityList { get; set; }
            public List<DropDownList> listLedger { get; set; }
            public List<DropDownList> listemplist { get; set; }




        }

        public class ProjectBudget
        {
            public long RowNum { get; set; }
            public long ID { get; set; }
            public string Name { get; set; }
        }
        public class ProjectBudgetYear
        {
            public long RowNum { get; set; }
            public long ID { get; set; }
            public string Name { get; set; }
        }
        public class ProjectBudgetLedger
        {
            public long RowNum { get; set; }
            public long ID { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Type { get; set; }
        }
        public class ProjectBudgetEmployee
        {
            public long RowNum { get; set; }
            public long ID { get; set; }
            public string Name { get; set; }
        }
        public class BudgetSublineActivity
        {
            public long ?Id { get; set; }
            // public long ProjectId { get; set; }
            public long BudgetSublineid { get; set; }
            public long Activityid { get; set; }
            public string ActivityCode { get; set; }
            public long SubActivityid { get; set; }
            public string SubActivityCode { get; set; }
            public long? Categoryid { get; set; }
            public string Category { get; set; }
            public string Code { get; set; }
            public decimal Amount { get; set; }
            public long Ledgerid { get; set; }
        }
        public class GetBudgetRPF
        {
            public long Id { get; set; }
            public string doc_no { get; set; }
            public DateTime doc_date { get; set; }
            public string projref_no { get; set; }
            public string proj_name { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
            public string Status { get; set; }
            public long donor_id { get; set; }
            public string donor_Name { get; set; }
            public long RowNo { get; set; }
         

        }
        public class SublineActivityList
        {
            public long Id { get; set; }
            public long ProjectId { get; set; }
            public string doc_no { get; set; }
            public string LedgerName { get; set; }
            public string DelegationAuthority { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public decimal TotalAmount { get; set; }
            public long BudgetId { get; set; }
            public long SublineId { get; set; }
            public string ProjectName { get; set; }
        }


    }
}