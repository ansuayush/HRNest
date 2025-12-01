using Mitr.Model;
using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface ISalaryStructureHelper
    {
        List<SalaryStructure.Grade.List> GetGradeList(GetResponse modal);
        SalaryStructure.Grade.Add GetGrade(GetResponse modal);
        PostResponse fnSetGrade(SalaryStructure.Grade.Add model);
        List<SalaryStructure.EarningStructureList> GetEarningStructureList(int Id, string Category, string Doctype);
        PostResponse fnSetEarningStructure(SalaryStructure.EarningStructureList model);
        List<SalaryStructure.EarningStructure> GetEarningStructureMaster(string Category);
        List<SalaryStructure.BenefitStructureList> GetBenefitStructureList(GetResponse modal);
        SalaryStructure.BenefitStructureAdd GetBenefitStructureAdd(GetResponse modal);
        PostResponse fnSetBenefitStructure(SalaryStructure.BenefitStructureAdd model);
        List<SalaryStructure.EmployeeSalary.List> GetEmployeeSalaryList(GetResponse modal);
        SalaryStructure.EmployeeSalary.Add GetEmployeeSalaryAdd(GetResponse modal);
        List<SalaryStructure.EmployeeSalary.StructureList> GetEmployeeSalaryStructureList(SalaryStructure.GetStructureListResponse modal);
        PostResponse fnSetEmployeeSalary(SalaryStructure.EmployeeSalary.Add model);
        SalaryStructure.EmployeeSalary.Detail GetEmployeeSalaryDetail(GetResponse modal);
        SalaryStructure.DeductionEntry GetDeductionEntry(string EmpType, string Date);
        PostResponse fnSetDeductionEntry(int Month, int Year, long Empid, string ComponentValues);
        SalaryStructure.OtherEarningEntry GetOtherEarningEntry(string EmpType, string Date);
        PostResponse fnSetOtherEarningEntry(int Month, int Year, long Empid, string ComponentValues);
        SalaryStructure.EmployeeSalary.Add GetEmploymentHistory(GetResponse modal);
        List<SalaryStructure.EmployeeSalary.StructureList> GetEmploymentHistoryStructure_List(SalaryStructure.GetStructureListResponse modal);
        List<SalaryStructure.OtherBenefitPayment> GetOtherBenefitPaymentList(long FinyearID, long empID, string PaidDate);
        List<SalaryStructure.OtherBenefitPayment.OtherBenefitPaymentComponent> GetOtherBenefitPaymentComponentList(long FinyearID, long empID, string PaidDate);
    }
}