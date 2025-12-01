using Mitr.Model;
using Mitr.Model.EmployeeSalary;
using System.Collections.Generic;

namespace Mitr.Interface
{
    public interface IEmployeeSalary
    {
        List<Bonus.FinList> GetFinYearList();
        List<Bonus> GetBonusPaymentList(long FinyearID, string Component, string PaidDate);

        string GetBonusPaymentDocNo();
        List<DropdownModel> GetSelectedProjectsList(string Doctype, string searchText = "");
        List<BonusPaymentEntry> GetBonusPaymentEntryList(long EmpID, string Component);
        PostResponseModel fnSetBonusPaymentEntry(BonusPaymentEntry model);
        List<BonusPaymentEntry> AddMultipleProjectPaaymentAmountList();

        List<OtherBenefitPayment> GetOtherBenefitPaymentList(long FinyearID, long empID, string PaidDate);
        List<OtherBenefitPayment.OtherBenefitPaymentComponent> GetOtherBenefitPaymentComponentList(long FinyearID, long empID, string PaidDate);
        PostResponseModel fnSetOtherBenefitPayment(OtherBenefitPayment.OtherBenefitPaymentComponent model);

        List<SpecialAllowance.FinList> GetFinancialYearList();

        List<SpecialAllowance> GetSpecialAllowanceList(long fYID, string component, string paidDate);
        string GetSpecialAllowanceDocNo();
        List<SpecialAllowanceEntry> GetSpecialAllowanceEntryList(long EmpID, string Component);
        PostResponseModel fnSetSpecialAllowancePaymentEntry(SpecialAllowanceEntry model);
        List<SpecialAllowanceEntry> AddMultipleProjectPaymentAmountList();

        PostResponseModel fnSetSpecialAllowanceCalculate(long FYID);
        List<DropdownModel> GetSelectedProjectsBonusList();
    }
}
