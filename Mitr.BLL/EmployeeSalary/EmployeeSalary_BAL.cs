using Dapper;
using Mitr.DAL;
using Mitr.Interface;
using Mitr.Model;
using Mitr.Model.EmployeeSalary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.BLL
{
    public class EmployeeSalary_BAL : IEmployeeSalary
    {
        public List<Bonus.FinList> GetFinYearList()
        {
            List<Bonus.FinList> List = new List<Bonus.FinList>();
            Bonus.FinList obj = new Bonus.FinList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetFinancialYear(0, "1");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Bonus.FinList();
                    obj.ID = Convert.ToInt32(item["ID"].ToString());
                    obj.year = item["year"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFinYearList. The query was executed :", ex.ToString(), "fnGetFinancialYear", "BonusModal", "BonusModal", "");
            }
            return List;
        }
        
        public List<Bonus> GetBonusPaymentList(long FinyearID, string Component, string PaidDate)
        {
            List<Bonus> List = new List<Bonus>();
            try
            {
                DataSet SalaryModuleDataSet = Common_SPU.GetBonusPaymentList(FinyearID, Component, PaidDate);
                foreach (DataRow item in SalaryModuleDataSet.Tables[0].Rows)
                {
                    Bonus obj = new Bonus();
                    obj.EmpID = Convert.ToInt64(item["EmpID"]);
                    obj.EmpCode = Convert.ToString(item["EmpCode"]);
                    obj.EmpName = Convert.ToString(item["EmpName"]);
                    obj.Designation = Convert.ToString(item["Designation"]);
                    obj.Category = Convert.ToString(item["Category"]);
                    obj.Location = Convert.ToString(item["Location"]);
                    obj.DateofJoining = Convert.ToString(item["DateofJoining"]);
                    obj.Entitlement = Convert.ToString(item["Entitlement"]);
                    obj.Paid = Convert.ToInt32(item["Paid"]);
                    obj.Balance = Convert.ToInt32(item["Balance"]);
                    obj.Component = Convert.ToString(item["Component"]);
                    obj.Status = Convert.ToInt32(item["Status"]);

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                //ClsCommon.LogError("Error during GetSubCategoryList. The query was executed :", ex.ToString(), "spu_GetGrSubcategory", "GrievanceModal", "GrievanceModal", "");
                throw ex;
            }
            return List;
        }
        public string GetBonusPaymentDocNo()
        {
            BonusPaymentEntry result = new BonusPaymentEntry();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBonusPaidDocNo", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<BonusPaymentEntry>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetBonusPaidDocNo. The query was executed :", ex.ToString(), "GetBonusPaymentDocNo", "SalaryModal", "SalaryModal", "");
            }
            return result.DocNo;
        }
        public List<DropdownModel> GetSelectedProjectsList(string Doctype, string searchText = "")
        {
            List<DropdownModel> List = new List<DropdownModel>();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetProjectEMP_SelfMapping(Doctype, searchText);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    DropdownModel obj = new DropdownModel();
                    obj.ID = Convert.ToString(item["ProRegID"]);
                    obj.Name = Convert.ToString(item["ProjectName"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetSelectedProjectsList. The query was executed :", ex.ToString(), "fnGetProjectEMP_SelfMapping", "SalaryModal", "SalaryModal", "");
            }
            return List;
        }
        public List<DropdownModel> GetSelectedProjectsBonusList()
        {
            List<DropdownModel> List = new List<DropdownModel>();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetProjectEMP_SelfMappingBonus();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    DropdownModel obj = new DropdownModel();
                    obj.ID = Convert.ToString(item["ID"]);
                    obj.Name = Convert.ToString(item["ProjectName"]);
                    List.Add(obj);
                    // 
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetSelectedProjectsList. The query was executed :", ex.ToString(), "fnGetProjectEMP_SelfMapping", "SalaryModal", "SalaryModal", "");
            }
            return List;
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

        public PostResponseModel fnSetBonusPaymentEntry(BonusPaymentEntry model)
        {
            PostResponseModel result = new PostResponseModel();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBonusPaymentEntry", con))
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

        public List<BonusPaymentEntry> AddMultipleProjectPaaymentAmountList()
        {
            List<BonusPaymentEntry> result = new List<BonusPaymentEntry>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_AddMultipleProjectPaaymentAmountList", param: param, commandType: CommandType.StoredProcedure))
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


        public List<OtherBenefitPayment> GetOtherBenefitPaymentList(long FinyearID, long Componentid, string PaidDate)
        {
            List<OtherBenefitPayment> List = new List<OtherBenefitPayment>();
            try
            {
                DataSet SalaryModuleDataSet = Common_SPU.GetOtherBenefitPaymentList(FinyearID, Componentid, PaidDate);
                foreach (DataRow item in SalaryModuleDataSet.Tables[0].Rows)
                {
                    OtherBenefitPayment obj = new OtherBenefitPayment();
                    obj.EmpID = Convert.ToInt64(item["EmpID"]);
                    obj.EmpCode = Convert.ToString(item["EmpCode"]);
                    obj.EmpName = Convert.ToString(item["EmpName"]);
                    obj.Category = Convert.ToString(item["Category"]);
                    obj.fromDate = Convert.ToString(item["fromDate"]);
                    obj.Todate = Convert.ToString(item["Todate"]);

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                //ClsCommon.LogError("Error during GetSubCategoryList. The query was executed :", ex.ToString(), "spu_GetGrSubcategory", "GrievanceModal", "GrievanceModal", "");
                throw ex;
            }
            return List;
        }
        public List<OtherBenefitPayment.OtherBenefitPaymentComponent> GetOtherBenefitPaymentComponentList(long FinyearID, long Componentid, string PaidDate)
        {
            List<OtherBenefitPayment.OtherBenefitPaymentComponent> List = new List<OtherBenefitPayment.OtherBenefitPaymentComponent>();
            try
            {
                DataSet SalaryModuleDataSet = Common_SPU.GetOtherBenefitPaymentComponentList(FinyearID, Componentid, PaidDate);
                foreach (DataRow item in SalaryModuleDataSet.Tables[0].Rows)
                {
                    OtherBenefitPayment.OtherBenefitPaymentComponent obj = new OtherBenefitPayment.OtherBenefitPaymentComponent();
                    obj.EmpID = Convert.ToInt64(item["EmpID"]);
                    obj.Entitlement = Convert.ToString(item["Entitlement"]);
                    obj.Paid = Convert.ToInt32(item["Paid"]);
                    obj.Balance = Convert.ToInt32(item["Balance"]);
                    obj.ComponentID = Convert.ToInt32(item["ComponentID"]);
                    obj.Component = Convert.ToString(item["Component"]);
                    obj.PaidAmount = Convert.ToDecimal(item["PaidAmt"]);
                    
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetOtherBenefitPaymentComponentList. The query was executed :", ex.ToString(), "spu_GetGrSubcategory", "GrievanceModal", "GrievanceModal", "");

            }
            return List;
        }

        public PostResponseModel fnSetOtherBenefitPayment(OtherBenefitPayment.OtherBenefitPaymentComponent model)
        {
            PostResponseModel result = new PostResponseModel();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetOtherBenefitPaymentEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@empid", SqlDbType.Int).Value = model.EmpID;
                        command.Parameters.Add("@ComponentID", SqlDbType.Int).Value = model.ComponentID ;
                        command.Parameters.Add("@PaidDate", SqlDbType.VarChar).Value = model.PaidDate ;
                        command.Parameters.Add("@PaidAmt", SqlDbType.Int).Value = model.PaidAmount;
                        command.Parameters.Add("@Type", SqlDbType.VarChar).Value = model.DeviceType ?? "Web";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.Createdby;
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.IsDeleted;

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



        public List<SpecialAllowance.FinList> GetFinancialYearList()
        {
            List<SpecialAllowance.FinList> List = new List<SpecialAllowance.FinList>();
            SpecialAllowance.FinList obj = new SpecialAllowance.FinList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetFinancialYear(0, "1");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new SpecialAllowance.FinList();
                    obj.ID = Convert.ToInt32(item["ID"].ToString());
                    obj.year = item["year"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFinYearList. The query was executed :", ex.ToString(), "fnGetFinancialYear", "SpecialAllowanceModal", "SpecialAllowanceModal", "");
            }
            return List;
        }
        public List<SpecialAllowance> GetSpecialAllowanceList(long FinyearID, string Component, string PaidDate)
       {
            List<SpecialAllowance> List = new List<SpecialAllowance>();
            try
            {
                DataSet SalaryModuleDataSet = Common_SPU.GetSpecialAllowanceList(FinyearID, Component, PaidDate);
                foreach (DataRow item in SalaryModuleDataSet.Tables[0].Rows)
                {
                    SpecialAllowance obj = new SpecialAllowance();
                    obj.EmpID = Convert.ToInt64(item["EmpID"]);
                    obj.EmpCode = Convert.ToString(item["EmpCode"]);
                    obj.EmpName = Convert.ToString(item["EmpName"]);
                    obj.Designation = Convert.ToString(item["Designation"]);
                    obj.Category = Convert.ToString(item["Category"]);
                    obj.Location = Convert.ToString(item["Location"]);
                    obj.DateofJoining = Convert.ToString(item["DateofJoining"]);
                    obj.Entitlement = Convert.ToString(item["Entitlement"]);
                    obj.Paid = Convert.ToInt32(item["Paid"]);
                    obj.Balance = Convert.ToInt32(item["Balance"]);
                    obj.Component = Convert.ToString(item["Component"]);
                    obj.Status = Convert.ToInt32(item["Status"]);


                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                //ClsCommon.LogError("Error during GetSubCategoryList. The query was executed :", ex.ToString(), "spu_GetGrSubcategory", "GrievanceModal", "GrievanceModal", "");
                throw ex;
            }
            return List;
        }
        public string GetSpecialAllowanceDocNo()
        {
            SpecialAllowanceEntry result = new SpecialAllowanceEntry();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSpecialAllowanceDocNo", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SpecialAllowanceEntry>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetBonusPaidDocNo. The query was executed :", ex.ToString(), "GetSpecialAllowanceDocNo", "SalaryModal", "SalaryModal", "");
            }
            return result.DocNo;
        }
        public List<SpecialAllowanceEntry> GetSpecialAllowanceEntryList(long EmpID, string Component)
        {
            List<SpecialAllowanceEntry> result = new List<SpecialAllowanceEntry>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@empID", dbType: DbType.Int64, value: EmpID, direction: ParameterDirection.Input);
                    param.Add("@Component", dbType: DbType.String, value: Component, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSpecialAllowanceEntryList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SpecialAllowanceEntry>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetSpecialAllowanceEntryList. The query was executed :", ex.ToString(), "GetSpecialAllowanceEntryList", "SalaryModal", "SalaryModal", "");
            }
            return result;
        }

        public PostResponseModel fnSetSpecialAllowancePaymentEntry(SpecialAllowanceEntry model)
        {
            PostResponseModel result = new PostResponseModel();
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

        public List<SpecialAllowanceEntry> AddMultipleProjectPaymentAmountList()
        {
            List<SpecialAllowanceEntry> result = new List<SpecialAllowanceEntry>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_AddMultipleProjectPaaymentAmountList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SpecialAllowanceEntry>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetSpecialAllowanceEntryList. The query was executed :", ex.ToString(), "GetSpecialAllowanceEntryList", "SalaryModal", "SalaryModal", "");
            }
            return result;
        }

        public PostResponseModel fnSetSpecialAllowanceCalculate(long fyid)
        {
            PostResponseModel result = new PostResponseModel();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSpecialAllowanceCalculate", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Fyid", SqlDbType.Int).Value = fyid;
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
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;
        }

    }
}
