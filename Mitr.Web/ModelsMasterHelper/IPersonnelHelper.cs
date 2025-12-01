using Mitr.Model;
using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface IPersonnelHelper
    {
        DataSet GetLeaveType();
        LeaveAvailedReport GetLeaveAvailedReport(string LeaveType);
        List<TempFinYearList> GetFinYearList();
        SalarySlipActivationEntry GetSalarySlipActivationEntry(int Month, int Year, string EmpType, string Doctype);
        PostResponse fnSetSalarySlipActivationEntry(int Month, int Year, long Empid, int IsActive, string Doctype);
        List<Personnels.ReimbursementBooking.FYList> GetReimbursementBooking(long Id);
        Personnels.ReimbursementBooking.ReimbursementHead GetReimbursementHead(long FYId,long HeadId=0,long ClaimId=0);
        Personnels.ReimbursementBooking.ClaimVoucher GetClaimVoucher(long FYId,long HeadId,long ClaimId);
        PostResponse fnSetClaimVoucher(long lId, long lEDPSID, string sClaimName, long lFinanId, string sReqNo, string dtReqDate, double dTotalAmt, int IsDeleted);
        bool fnSetClaimVoucherLIC(long lReimbDetID, long lsrno, string sInsurCompany, string sPolicyNo, string dtStartDate, string dtEndDate, string sInsurePerson, double dPremAmt, string sReceiptNo, string dtReciptdate, string sReason);
        bool fnSetClaimVoucherMed(long lReimbDetID, long lsrno, string sBillNo, string dtBillDate, double dBillAmt, string sPatientName, string sRelEmp, string sRemark);
        bool fnDelClaimVoucherSrno(long lReimbDetID, long lsrno);
        List<Personnels.FinYearList> GetFinYearList(long Id);
        List<Personnels.ClaimEmployees> GetClaimEmployees(long EmpId, long FYId);
        List<Personnels.FinancePaymentList> GetFinancePaymentList(long EmpId, long FYId, int Approve);
        Personnels.FinanceViewVoucher GetFinanceViewVoucher(long FYId, long EmpId, long HeadId, long ClaimId);
        bool fnSetClaimVoucherStatus(long lReimbDetID, int Approve,string Remark);
        PostResponse fnSetFinancePayment(long ClaimId, long EmpId, long FinId, string ClaimType, string PaidDate, decimal PaidAmt, long ProjectId, string Reason, string AccountNo, string BankName, string NeftCode, string BranchName);
        List<SpecialAllowance> GetSpecialAllowanceList(long EMPID);
        ConsolidatedActivityLog GetConsolidatedActivityLogReport(long EMPID, string FromDate, string ToDate);
        FNFReport GetFandFDetails(long EmpId,long FYId);
        List<BonusPaymentEntry> GetBonusPaymentEntryList(long EmpID, string Component);
        PostResponse SetComponentDetails(BonusPaymentEntry model);
        PostResponse fnSetBonusPaymentMobileEntry(BonusPaymentEntry model);
        PostResponse fnSetSpecialAllowancePaymentEntry(BonusPaymentEntry model);
        List<FinList> GetFinYearListData();


    }
}