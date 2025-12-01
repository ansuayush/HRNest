using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.ModelsMasterHelper
{
    interface IBudgetHelper
    {
      
        PostResponse SetTraining(BudgetMaster.AddTrainingWorkshopSeminarTypes model);
        PostResponse SetTrainingDetails(BudgetMaster.AddTrainingWorkshopSeminarDeatils model);
        BudgetMaster.AddTrainingWorkshopSeminarTypes SetTrainingWorkshop(GetResponse modal);
        PostResponse SetTravelBudgetDetails(BudgetMaster.AddTravelBudget model);
        //long FnSetTravelBudget(long id, long TravelYear, string TravelType, decimal AirFare, decimal PerDiem, decimal Hotel, decimal LocalTravel);
        List<BudgetMaster.TravelBudgetList> GetTravelBudgetList(long TravelId);
        List<BudgetMaster.TravelBudgetList> GetTravelBudgetListYear(long TravelId);
        List<BudgetMaster.TrainingWorkshopSeminarTypes> GetTrainingWorkTypeList(long WorkId);
        List<BudgetMaster.InflationRate> GetInflationRateList(long InflationId);
        List<BudgetMaster.AddInflationRateDeatils> GetInflationRateYearWiseList(long Id, long YearId);
        PostResponse SetInflationDetails(BudgetMaster.AddInflationRateDeatils model);
        List<BudgetMaster.AddIndirectCostRate> GetindirectCostRate(long Id, long YearId);
        PostResponse SetIndirectcostRate(BudgetMaster.AddIndirectCostRate model);
        List<BudgetMaster.IndirectCostRate> GetIndirectCostDetails(long IndirectId);
        List<BudgetMaster.EmployeeList> GetBudgetSettingEmpList();

        PostResponse SetbudgetSettingEmployee(BudgetMaster.BudgetAuthoritySetting model);
        List<BudgetMaster.EmployeeList> GetBudgetSettingMainEmpList(long EmployeeId);
        List<BudgetMaster.AddFringeBenefitRate> GetFringeBenefitRate(long Id, long YearId);
        PostResponse SetFringeBenefitRate(BudgetMaster.AddFringeBenefitRate model);
        List<BudgetMaster.FringeBenefitRate> GetFringeBenefitRateDetails(long YearId);
        PostResponse SetBudgetCreate(BudgetMaster.Budget model);
        PostResponse SetBudgetOutCome(BudgetMaster.OutcomeDetails model);
        PostResponse SetBudgetActivity(BudgetMaster.Activitydetails model);
        BudgetMaster.SetbudgetsubActivity SetbudgetSubActivity(long Id, long BudgetId, long ActivityId);
        PostResponse SetBudgetSubActivityList(BudgetMaster.SubActivity model);
        BudgetMaster.SetTentativePeriodofBudget GetBudgetTentativePeriod(long Id, long BudgetId);
        PostResponse SetBudgetTentativePeriod(BudgetMaster.SetTentativePeriodofBudget model);
        PostResponse SetBudgetInflationRate(BudgetMaster.InflationDetails model);
        PostResponse SetBudgetIndirectnRate(BudgetMaster.IndirectDetails model);
        List<BudgetMaster.GetBudgetRPF> GetRFPBudgetProjectList(long ProjectId);
        BudgetMaster.SetBudget GetBudgetSublineActivity(long Id, string BudgetType,long Projectid, long Ledgerid);
        BudgetMaster.SetBudget GetBudgetSublineActivityAll( string BudgetType, long Projectid, long Ledgerid);
        PostResponse SetBudgetSublineActivity(BudgetMaster.SetBudget model);
        PostResponse SetBudgetSublineActivityDet(BudgetMaster.BudgetSublineActivity model);
        List<BudgetMaster.SublineActivityList> GetSublineActivityList();
        BudgetMaster.OutcomesActivities GetbudgetOutcome(long Id, long BudgetId, long ActivityId);
        List<BudgetMaster.StandaloneBudget> GetStandaloneBudgetList();
        List<BudgetMaster.SublineActivityList> GetSublineActivityDraftList();
        PostResponse SetBudgetSublineActivityImport(string xmlData);
    }
}
