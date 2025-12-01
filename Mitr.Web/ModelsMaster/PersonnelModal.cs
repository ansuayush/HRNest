using Dapper;
using Mitr.CommonClass;
using Mitr.Model;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Mitr.ModelsMaster
{
    public class PersonnelModal : IPersonnelHelper
    {
        ILeaveHelper Leave;
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public PersonnelModal()
        {
            Leave = new LeaveModal();
        }
        public DataSet GetLeaveType()
        {
            string SQL = "";
            DataSet TempModuleDataSet = new DataSet();
            try
            {
                SQL = @"select master_leave.id,master_leave.leave_name  from master_empdet inner join master_emp on 
                     master_empdet.emp_id  = master_emp.id  inner join master_leave on master_empdet.leave_id = master_leave.id 
                     where  master_emp.isdeleted = 0 And master_empdet.isdeleted = 0 And master_leave.isdeleted = 0 
                     and master_emp.id=" + clsApplicationSetting.GetSessionValue("EMPID") + " order by master_leave.leave_name";

                TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveType. The query was executed :", ex.ToString(), SQL, "PersonnelModal", "PersonnelModal", "");
            }
            return TempModuleDataSet;

        }
        public LeaveAvailedReport GetLeaveAvailedReport(string LeaveType)
        {
            long EMPIDDD = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPIDDD);
            LeaveAvailedReport obj = new LeaveAvailedReport();
            obj.EmployeeList = Leave.GetLeaveEmpDetails(EMPIDDD);
            obj.LeaveDetails = Common_SPU.fnGetLeaveAvailedReport(LeaveType);
            return obj;
        }

        public List<TempFinYearList> GetFinYearList()
        {
            string SQL = "";
            List<TempFinYearList> List = new List<TempFinYearList>();
            TempFinYearList obj = new TempFinYearList();
            try
            {
                SQL = @"select id,Year,from_date,To_Date
                        from FinYear where isdeleted=0 and
                            (from_date>=(select DOJ from master_emp where id=" + clsApplicationSetting.GetSessionValue("EMPID") + ")" +
                           " or from_date<=(select DOJ from master_emp where id= " + clsApplicationSetting.GetSessionValue("EMPID") + ")" +
                           " and to_date>=(select DOJ from master_emp where id=" + clsApplicationSetting.GetSessionValue("EMPID") + "))" +
                           " and from_date<=(select case when year(DOR)=1899 then getdate() else DOR end as aa " +
                           " from master_emp where id=" + clsApplicationSetting.GetSessionValue("EMPID") + ") order by id desc";
                foreach (DataRow item in clsDataBaseHelper.ExecuteDataSet(SQL).Tables[0].Rows)
                {
                    obj = new TempFinYearList();
                    obj.ID = Convert.ToInt32(item["ID"]);
                    obj.year = item["year"].ToString();
                    obj.FromDate = item["from_date"].ToString();
                    obj.Todate = item["To_Date"].ToString();
                    obj.ShowFromDate = Convert.ToDateTime(obj.FromDate).ToString("dd-MMM-yyyy");
                    obj.ShowToDate = Convert.ToDateTime(obj.Todate).ToString("dd-MMM-yyyy");
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFinYearList. The query was executed :", ex.ToString(), SQL, "PersonnelModal", "PersonnelModal", "");
            }

            return List;
        }
        public SalarySlipActivationEntry GetSalarySlipActivationEntry(int Month, int Year, string EmpType, string Doctype)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            SalarySlipActivationEntry result = new SalarySlipActivationEntry();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Month", dbType: DbType.Int32, value: Month, direction: ParameterDirection.Input);
                    param.Add("@Year", dbType: DbType.Int32, value: Year, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.String, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@EmpType", dbType: DbType.String, value: EmpType, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Doctype, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetReportActivationList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalarySlipActivationEntry>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new SalarySlipActivationEntry();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Emplist = reader.Read<SalarySlipActivationEmp>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetSalarySlipActivationEntry. The query was executed :", ex.ToString(), "GetSalarySlipActivationEntry", "PersonnelModal", "PersonnelModal", "");
            }
            return result;
        }
        public PostResponse fnSetSalarySlipActivationEntry(int Month, int Year, long Empid, int IsActive, string Doctype)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetReportActivation", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Month", SqlDbType.Int).Value = Month;
                        command.Parameters.Add("@Year", SqlDbType.Int).Value = Year;
                        command.Parameters.Add("@Empid", SqlDbType.BigInt).Value = Empid;
                        command.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = IsActive;
                        command.Parameters.Add("@Doctype", SqlDbType.VarChar).Value = Doctype;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = LoginID;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }

        public List<Personnels.ReimbursementBooking.FYList> GetReimbursementBooking(long Id)
        {
            string EMPID = clsApplicationSetting.GetSessionValue("EMPID");

            List<Personnels.ReimbursementBooking.FYList> result = new List<Personnels.ReimbursementBooking.FYList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", dbType: DbType.Int64, value: Id, direction: ParameterDirection.Input);
                    param.Add("EmpId", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetReimbursementBooking", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Personnels.ReimbursementBooking.FYList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetReimbursementBooking. The query was executed :", ex.ToString(), "spu_GetReimbursementBooking()", "PersonnelModal", "PersonnelModal", "");

            }
            return result;
        }

        public Personnels.ReimbursementBooking.ReimbursementHead GetReimbursementHead(long FYId, long HeaId = 0, long ClaimId = 0)
        {
            string EMPID = clsApplicationSetting.GetSessionValue("EMPID");

            Personnels.ReimbursementBooking.ReimbursementHead result = new Personnels.ReimbursementBooking.ReimbursementHead();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("FYId", dbType: DbType.Int64, value: FYId, direction: ParameterDirection.Input);
                    param.Add("EmpId", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("HeadId", dbType: DbType.Int64, value: HeaId, direction: ParameterDirection.Input);
                    param.Add("ClaimId", dbType: DbType.Int64, value: ClaimId, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetReimbursementHead", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Personnels.ReimbursementBooking.ReimbursementHead>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Personnels.ReimbursementBooking.ReimbursementHead();
                            result.claimhead = new List<Personnels.ReimbursementBooking.ClaimHead>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.claimhead = reader.Read<Personnels.ReimbursementBooking.ClaimHead>().ToList();
                            if (result.claimhead.Count > 0)
                            {
                                result.claimhead[0].claimvoucher = reader.Read<Personnels.ReimbursementBooking.ClaimVoucher>().ToList();
                                if (result.claimhead[0].claimvoucher.Count > 0)
                                {
                                    result.claimhead[0].claimvoucher[0].insuranceentry = reader.Read<Personnels.ReimbursementBooking.ClaimInsuranceEntry>().ToList();
                                    result.claimhead[0].claimvoucher[0].medicalentry = reader.Read<Personnels.ReimbursementBooking.ClaimMedicalEntry>().ToList();
                                    result.claimhead[0].claimvoucher[0].otherentry = reader.Read<Personnels.ReimbursementBooking.ClaimOtherEntry>().ToList();
                                }
                            }
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetReimbursementHead. The query was executed :", ex.ToString(), "GetReimbursementHead()", "PersonnelModal", "PersonnelModal", "");

            }
            return result;
        }

        public Personnels.ReimbursementBooking.ClaimVoucher GetClaimVoucher(long FYId, long HeadId, long ClaimId)
        {
            string EMPID = clsApplicationSetting.GetSessionValue("EMPID");

            Personnels.ReimbursementBooking.ClaimVoucher result = new Personnels.ReimbursementBooking.ClaimVoucher();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("FYId", dbType: DbType.Int64, value: FYId, direction: ParameterDirection.Input);
                    param.Add("HeadId", dbType: DbType.Int64, value: HeadId, direction: ParameterDirection.Input);
                    param.Add("EmpId", dbType: DbType.Int64, value: EMPID, direction: ParameterDirection.Input);
                    param.Add("ClaimId", dbType: DbType.Int64, value: ClaimId, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetClaimVoucher", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Personnels.ReimbursementBooking.ClaimVoucher>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Personnels.ReimbursementBooking.ClaimVoucher();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.medicalentry = reader.Read<Personnels.ReimbursementBooking.ClaimMedicalEntry>().ToList();
                            result.insuranceentry = reader.Read<Personnels.ReimbursementBooking.ClaimInsuranceEntry>().ToList();
                            result.otherentry = reader.Read<Personnels.ReimbursementBooking.ClaimOtherEntry>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetClaimVoucher. The query was executed :", ex.ToString(), "GetClaimVoucher()", "PersonnelModal", "PersonnelModal", "");

            }
            return result;
        }

        public PostResponse fnSetClaimVoucher(long lId, long lEDPSID, string sClaimName, long lFinanId, string sReqNo, string dtReqDate, double dTotalAmt, int IsDeleted)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            string EMPID = clsApplicationSetting.GetSessionValue("EMPID");
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetReimbursdet", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = lId;
                        command.Parameters.Add("@edps_id", SqlDbType.Int).Value = lEDPSID;
                        command.Parameters.Add("@claim_name", SqlDbType.VarChar).Value = sClaimName;
                        command.Parameters.Add("@finan_id", SqlDbType.Int).Value = lFinanId;
                        command.Parameters.Add("@emp_id", SqlDbType.Int).Value = EMPID;
                        command.Parameters.Add("@req_no", SqlDbType.VarChar).Value = sReqNo;
                        command.Parameters.Add("@req_date", SqlDbType.VarChar).Value = dtReqDate;
                        command.Parameters.Add("@Total_Amount", SqlDbType.Decimal).Value = dTotalAmt;
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = IsDeleted;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = LoginID;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = 1;
                                result.SuccessMessage = "";
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }

        public bool fnSetClaimVoucherLIC(long lReimbDetID, long lsrno, string sInsurCompany, string sPolicyNo, string dtStartDate, string dtEndDate, string sInsurePerson, double dPremAmt, string sReceiptNo, string dtReciptdate, string sReason)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            bool RetValue = false;
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetreimbursdetailsLIC", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@reimbdet_id", SqlDbType.Int).Value = lReimbDetID;
                        command.Parameters.Add("@srno", SqlDbType.Int).Value = lsrno;
                        command.Parameters.Add("@insur_company", SqlDbType.VarChar).Value = sInsurCompany;
                        command.Parameters.Add("@policy_no", SqlDbType.VarChar).Value = sPolicyNo;
                        command.Parameters.Add("@start_date", SqlDbType.VarChar).Value = dtStartDate;
                        command.Parameters.Add("@end_date", SqlDbType.VarChar).Value = dtEndDate;
                        command.Parameters.Add("@insur_person", SqlDbType.VarChar).Value = sInsurePerson;
                        command.Parameters.Add("@prem_amt", SqlDbType.Decimal).Value = dPremAmt;
                        command.Parameters.Add("@receipt_no", SqlDbType.VarChar).Value = sReceiptNo;
                        command.Parameters.Add("@receipt_date", SqlDbType.VarChar).Value = dtReciptdate;
                        command.Parameters.Add("@reason", SqlDbType.VarChar).Value = sReason;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = LoginID;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.RecordsAffected > 0)
                            {
                                RetValue = true;
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw ex;
                }
            }
            return RetValue;
        }

        public bool fnSetClaimVoucherMed(long lReimbDetID, long lsrno, string sBillNo, string dtBillDate, double dBillAmt, string sPatientName, string sRelEmp, string sRemark)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            bool RetValue = false;
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetreimbursdetailsMed", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@reimbdet_id", SqlDbType.Int).Value = lReimbDetID;
                        command.Parameters.Add("@srno", SqlDbType.Int).Value = lsrno;
                        command.Parameters.Add("@bill_no", SqlDbType.VarChar).Value = sBillNo;
                        command.Parameters.Add("@bill_date", SqlDbType.VarChar).Value = dtBillDate;
                        command.Parameters.Add("@bill_amt", SqlDbType.Decimal).Value = dBillAmt;
                        command.Parameters.Add("@name_patient", SqlDbType.VarChar).Value = sPatientName;
                        command.Parameters.Add("@relwithemp", SqlDbType.VarChar).Value = sRelEmp;
                        command.Parameters.Add("@remark", SqlDbType.VarChar).Value = sRemark;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = LoginID;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.RecordsAffected > 0)
                            {
                                RetValue = true;
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close(); throw ex;
                }
            }
            return RetValue;
        }

        public bool fnDelClaimVoucherSrno(long lReimbDetID, long lsrno)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            bool RetValue = false;
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_DelReimbursdetailsSrno", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@reimbdet_id", SqlDbType.Int).Value = lReimbDetID;
                        command.Parameters.Add("@srno", SqlDbType.Int).Value = lsrno;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = LoginID;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.RecordsAffected > 0)
                            {
                                RetValue = true;
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close(); throw ex;
                }
            }
            return RetValue;
        }

        public List<Personnels.FinYearList> GetFinYearList(long Id)
        {
            string IsActive = "0,1";

            List<Personnels.FinYearList> result = new List<Personnels.FinYearList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", dbType: DbType.Int64, value: Id, direction: ParameterDirection.Input);
                    param.Add("IsActive", dbType: DbType.String, value: IsActive, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFinYear", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Personnels.FinYearList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFinYearList. The query was executed :", ex.ToString(), "spu_GetFinYearList()", "PersonnelModal", "PersonnelModal", "");

            }
            return result;
        }

        public List<Personnels.ClaimEmployees> GetClaimEmployees(long EmpId, long FYId)
        {
            List<Personnels.ClaimEmployees> result = new List<Personnels.ClaimEmployees>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("FYId", dbType: DbType.Int64, value: FYId, direction: ParameterDirection.Input);
                    param.Add("EmpId", dbType: DbType.Int64, value: EmpId, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetClaimEmployee", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Personnels.ClaimEmployees>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetClaimEmployees. The query was executed :", ex.ToString(), "spu_GetClaimEmployees()", "PersonnelModal", "PersonnelModal", "");

            }
            return result;
        }

        public List<Personnels.FinancePaymentList> GetFinancePaymentList(long EmpId, long FYId, int Approve)
        {
            List<Personnels.FinancePaymentList> result = new List<Personnels.FinancePaymentList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("FYId", dbType: DbType.Int64, value: FYId, direction: ParameterDirection.Input);
                    param.Add("EmpId", dbType: DbType.Int64, value: EmpId, direction: ParameterDirection.Input);
                    param.Add("Approve", dbType: DbType.Int32, value: Approve, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetClaimPaymentList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Personnels.FinancePaymentList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFinancePaymentList. The query was executed :", ex.ToString(), "spu_GetClaimPaymentList()", "PersonnelModal", "PersonnelModal", "");

            }
            return result;
        }

        public Personnels.FinanceViewVoucher GetFinanceViewVoucher(long FYId, long EmpId, long HeadId, long ClaimId)
        {
            Personnels.FinanceViewVoucher result = new Personnels.FinanceViewVoucher();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("FYId", dbType: DbType.Int64, value: FYId, direction: ParameterDirection.Input);
                    param.Add("EmpId", dbType: DbType.Int64, value: EmpId, direction: ParameterDirection.Input);
                    param.Add("HeadId", dbType: DbType.Int64, value: HeadId, direction: ParameterDirection.Input);
                    param.Add("ClaimId", dbType: DbType.Int64, value: ClaimId, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFinanceViewVoucher", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Personnels.FinanceViewVoucher>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Personnels.FinanceViewVoucher();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.medicalentry = reader.Read<Personnels.ReimbursementBooking.ClaimMedicalEntry>().ToList();
                            result.insuranceentry = reader.Read<Personnels.ReimbursementBooking.ClaimInsuranceEntry>().ToList();
                            result.otherentry = reader.Read<Personnels.ReimbursementBooking.ClaimOtherEntry>().ToList();
                            result.projectcode = reader.Read<Personnels.ProjectList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetClaimVoucher. The query was executed :", ex.ToString(), "GetClaimVoucher()", "PersonnelModal", "PersonnelModal", "");

            }
            return result;
        }

        public bool fnSetClaimVoucherStatus(long lReimbDetID, int Approve, string Remark)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            bool RetValue = false;
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetReimbursementStatus", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@reimbdet_id", SqlDbType.Int).Value = lReimbDetID;
                        command.Parameters.Add("@Approve", SqlDbType.Int).Value = Approve;
                        command.Parameters.Add("@Remark", SqlDbType.VarChar).Value = Remark;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = LoginID;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.RecordsAffected > 0)
                            {
                                RetValue = true;
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close(); throw ex;
                }
            }
            return RetValue;
        }

        public PostResponse fnSetFinancePayment(long ClaimId, long EmpId, long FinId, string ClaimType, string PaidDate, decimal PaidAmt, long ProjectId, string Reason, string AccountNo, string BankName, string NeftCode, string BranchName)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetReimbursFinan", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@reimbdet_id", SqlDbType.Int).Value = ClaimId;
                        command.Parameters.Add("@claim_type", SqlDbType.VarChar).Value = ClaimType;
                        command.Parameters.Add("@hard_copy", SqlDbType.VarChar).Value = "True";
                        command.Parameters.Add("@cheque_no", SqlDbType.VarChar).Value = "";
                        command.Parameters.Add("@paid_date", SqlDbType.VarChar).Value = PaidDate;
                        command.Parameters.Add("@paid_amt", SqlDbType.Decimal).Value = PaidAmt;
                        command.Parameters.Add("@pay_mode", SqlDbType.VarChar).Value = "NEFT/Bank Transfer";
                        command.Parameters.Add("@bank_name", SqlDbType.VarChar).Value = BankName;
                        command.Parameters.Add("@neft_code", SqlDbType.VarChar).Value = NeftCode;
                        command.Parameters.Add("@branch_name", SqlDbType.VarChar).Value = BranchName;
                        command.Parameters.Add("@account_no", SqlDbType.VarChar).Value = AccountNo;
                        command.Parameters.Add("@finan_id", SqlDbType.Int).Value = FinId;
                        command.Parameters.Add("@emp_id", SqlDbType.Int).Value = EmpId;
                        command.Parameters.Add("@projid", SqlDbType.Int).Value = ProjectId;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = LoginID;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = 1;
                                result.SuccessMessage = "";
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }

        public List<SpecialAllowance> GetSpecialAllowanceList(long EMPID)
        {
            List<SpecialAllowance> result = new List<SpecialAllowance>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EmpID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSpecialAllowanceRpt", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SpecialAllowance>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetSpecialAllowanceRpt. The query was executed :", ex.ToString(), "GetSpecialAllowanceList()", "PersonnelModal", "PersonnelModal");
            }
            return result;
        }
        public ConsolidatedActivityLog GetConsolidatedActivityLogReport(long Empid, string FromDate, string ToDate)
        {
            ConsolidatedActivityLog obj = new ConsolidatedActivityLog();
            List<ConsolidatedActivityLog> Employeelist = new List<ConsolidatedActivityLog>();
            try
            {
                DataSet grievancesModuleDataSet = Common_SPU.fnGetConsolidatedActivityLogReport(Empid, FromDate, ToDate);
                foreach (DataRow item in grievancesModuleDataSet.Tables[0].Rows)
                {
                    obj = new ConsolidatedActivityLog();
                    obj.Empid = Convert.ToInt64(item["Empid"]);
                    obj.EmployeeName = Convert.ToString(item["EmployeeName"]);
                    obj.EmployeeCode = Convert.ToString(item["EmployeeCode"]);
                    obj.Dateofjoining = Convert.ToDateTime(item["DOJ"]).ToString("dd/MMM/yyyy");
                    obj.WorkLocation = Convert.ToString(item["WorkLocation"]);
                    obj.Designation = Convert.ToString(item["Designation"]);
                    obj.Month = Convert.ToString(item["Month"]);
                    obj.Year = Convert.ToString(item["Year"]);
                    obj.ProjectName = Convert.ToString(item["ProjectName"]);
                    obj.Activity = Convert.ToString(item["activity"]);
                    obj.Description = Convert.ToString(item["description"]);
                    obj.FinancialYear = Convert.ToString(item["FinancialYear"]);
                    

                    Employeelist.Add(obj);
                }
                obj.Emplist = Employeelist;
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetConsolidatedActivityLogReport. The query was executed :", ex.ToString(), "GetConsolidatedActivityLogReport", "PersonnelModal", "PersonnelModal", "");
            }
            return obj;
        }
        public FNFReport GetFandFDetails(long EmpId,long FYId)
        {
            FNFReport result = new FNFReport();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int32, value: EmpId, direction: ParameterDirection.Input);
                    param.Add("@FYId", dbType: DbType.Int32, value: FYId, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmploymentDetailsFullFinal", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<FNFReport>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new FNFReport();
                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetEmploymentDetailsFullFinal()", "PersonalModal", "PersonalModal", "");

            }
            return result;
        }
        public List<BonusPaymentEntry> GetBonusPaymentEntryList(long EmpID, string Component)
        {
            List<BonusPaymentEntry> result = new List<BonusPaymentEntry>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@empID", dbType: DbType.Int64, value: EmpID, direction: ParameterDirection.Input);
                    param.Add("@Component", dbType: DbType.String, value: Component, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBonusPaymentEntryList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<BonusPaymentEntry>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetBonusPaymentEntryList. The query was executed :", ex.ToString(), "GetBonusPaymentEntryList", "SalaryModal", "SalaryModal", "");
            }
            return result;
        }
        public PostResponse SetComponentDetails(BonusPaymentEntry model)
        {
            PostResponse result = new PostResponse();
            string SQL = "";
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetComonentSave", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ComponentID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = model.EmpID;
                        command.Parameters.Add("@Amount", SqlDbType.Float).Value = model.PaidAmount;
                        command.Parameters.Add("@Month", SqlDbType.Int).Value = model.Month;
                        command.Parameters.Add("@Year", SqlDbType.Int).Value = model.Year;
                        command.Parameters.Add("@DocType", SqlDbType.VarChar).Value = model.DocType;
                        command.Parameters.Add("@PaidDate", SqlDbType.VarChar).Value = model.PaidDate;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    ClsCommon.LogError("Error during spu_SetTravelBudget. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public PostResponse fnSetBonusPaymentMobileEntry(BonusPaymentEntry model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetReimbursFinanMedical", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@empid", SqlDbType.Int).Value = model.EmpID;
                        command.Parameters.Add("@PaidDate", SqlDbType.VarChar).Value = model.PaidDate ?? string.Empty;
                        command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = model.ProjectID;
                        command.Parameters.Add("@PaidAmount", SqlDbType.Int).Value = model.PaidAmount;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.Createdby;
                        command.Parameters.Add("@Type", SqlDbType.VarChar).Value = model.DeviceType ?? "Web";
                        command.Parameters.Add("@Component", SqlDbType.VarChar).Value = model.Component ?? "";

                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }




        public PostResponse fnSetSpecialAllowancePaymentEntry(BonusPaymentEntry model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSpecialAllowanceEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@empid", SqlDbType.Int).Value = model.EmpID;
                        command.Parameters.Add("@DocNo", SqlDbType.VarChar).Value = model.DocNo ?? string.Empty;
                        command.Parameters.Add("@PaidDate", SqlDbType.VarChar).Value = model.PaidDate ?? string.Empty;
                        command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = model.ProjectID;
                        command.Parameters.Add("@PaidAmount", SqlDbType.Int).Value = model.PaidAmount;
                        command.Parameters.Add("@Remark", SqlDbType.VarChar).Value = model.Remark ?? string.Empty;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.Createdby;
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.IsDeleted;
                        command.Parameters.Add("@Type", SqlDbType.VarChar).Value = model.DeviceType ?? "Web";
                        command.Parameters.Add("@Component", SqlDbType.VarChar).Value = model.Component ?? "";

                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }

        public List<FinList> GetFinYearListData()
        {
            List<FinList> List = new List<FinList>();
            FinList obj = new FinList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetFinancialYear(0, "1");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new FinList();
                    obj.ID = Convert.ToInt32(item["ID"].ToString());
                    obj.year = item["year"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFinYearList. The query was executed :", ex.ToString(), "fnGetFinancialYear", "PMSModal", "PMSModal", "");
            }
            return List;
        }
    }
}