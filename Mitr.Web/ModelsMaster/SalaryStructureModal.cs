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
using System.Web;

namespace Mitr.ModelsMaster
{
    public class SalaryStructureModal:ISalaryStructureHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public List<SalaryStructure.Grade.List> GetGradeList(GetResponse modal)
        {
            List<SalaryStructure.Grade.List> result = new List<SalaryStructure.Grade.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);                    
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSS_GradeMaster", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.Grade.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetGradeList. The query was executed :", ex.ToString(), "GetGradeList", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }

        public SalaryStructure.Grade.Add GetGrade(GetResponse modal)
        {
            SalaryStructure.Grade.Add result = new SalaryStructure.Grade.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);                   
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSS_GradeMaster", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.Grade.Add>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetGrade. The query was executed :", ex.ToString(), "spu_GetSS_GradeMaster", "MasterModal", "MasterModal", "");
            }
            return result;
        }


        public PostResponse fnSetGrade(SalaryStructure.Grade.Add model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSS_GradeMaster", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = model.id ?? 0;
                        command.Parameters.Add("@Grade", SqlDbType.VarChar).Value = model.Grade ?? "";
                        command.Parameters.Add("@Role", SqlDbType.VarChar).Value = model.Role ?? "";
                        command.Parameters.Add("@JobTitleid", SqlDbType.VarChar).Value = model.JobTitleid ?? "";
                        command.Parameters.Add("@Remark", SqlDbType.VarChar).Value = model.Remark ?? "";                       
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
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
        public List<SalaryStructure.EarningStructure> GetEarningStructureMaster(string Category)
        {
            List<SalaryStructure.EarningStructure> result = new List<SalaryStructure.EarningStructure>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();                    
                    param.Add("@Category", dbType: DbType.String, value: Category, direction: ParameterDirection.Input);                    
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSS_EarningStructureMaster", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.EarningStructure>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEarningStructureMaster. The query was executed :", ex.ToString(), "spu_GetSS_EarningStructureMaster", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }
        public List<SalaryStructure.EarningStructureList> GetEarningStructureList(int Id,string Category,string Doctype)
        {
            List<SalaryStructure.EarningStructureList> result = new List<SalaryStructure.EarningStructureList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: Id, direction: ParameterDirection.Input);
                    param.Add("@Category", dbType: DbType.String, value: Category, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Doctype, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSS_EarningStructure", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.EarningStructureList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEarningStructureList. The query was executed :", ex.ToString(), "spu_GetSS_EarningStructure", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }
        public PostResponse fnSetEarningStructure(SalaryStructure.EarningStructureList model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSS_EarningStructure", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = model.id ?? 0;
                        command.Parameters.Add("@Category", SqlDbType.VarChar).Value = model.Category ?? "";
                        command.Parameters.Add("@Componentid", SqlDbType.Int).Value = model.Componentid ;
                        command.Parameters.Add("@Percentage", SqlDbType.VarChar).Value = model.Percentage ?? "0";
                        command.Parameters.Add("@StartMonth", SqlDbType.VarChar).Value = model.StartMonth ?? "";
                        command.Parameters.Add("@EndMonth", SqlDbType.VarChar).Value = model.EndMonth ?? "";
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
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
        public List<SalaryStructure.BenefitStructureList> GetBenefitStructureList(GetResponse modal)
        {
            List<SalaryStructure.BenefitStructureList> result = new List<SalaryStructure.BenefitStructureList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSS_BenefitStructureList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.BenefitStructureList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetBenefitStructureList. The query was executed :", ex.ToString(), "spu_GetSS_BenefitStructureList", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }
        public SalaryStructure.BenefitStructureAdd GetBenefitStructureAdd(GetResponse modal)
        {
            SalaryStructure.BenefitStructureAdd result = new SalaryStructure.BenefitStructureAdd();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Structureid", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSS_BenefitStructure", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.BenefitStructureAdd>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new SalaryStructure.BenefitStructureAdd();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LstDet = reader.Read<SalaryStructure.BenefitStructureDet>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetBenefitStructureAdd. The query was executed :", ex.ToString(), "spu_GetSS_BenefitStructure", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }
        public PostResponse fnSetBenefitStructure(SalaryStructure.BenefitStructureAdd model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSS_BenefitStructure", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = model.id ?? 0;
                        command.Parameters.Add("@Structure", SqlDbType.VarChar).Value = model.Structure ?? "";
                        command.Parameters.Add("@Category", SqlDbType.VarChar).Value = model.Category;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress ?? "";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
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
                                    foreach (var item in model.LstDet)
                                    {
                                        item.LoginID = model.LoginID;
                                        item.Structureid = Convert.ToInt32(result.ID);
                                        fnSetBenefitStructureDet(item);
                                    }
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
        public PostResponse fnSetBenefitStructureDet(SalaryStructure.BenefitStructureDet model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSS_BenefitStructureDet", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = model.id ?? 0;
                        command.Parameters.Add("@Structureid", SqlDbType.Int).Value = model.Structureid ?? 0;
                        command.Parameters.Add("@Componentid", SqlDbType.Int).Value = model.Benefitid;
                        command.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = model.isactive ?? 0;
                        command.Parameters.Add("@CalculationType", SqlDbType.VarChar).Value = model.CalculationType ;
                        command.Parameters.Add("@Amount", SqlDbType.VarChar).Value = model.Amount ?? 0;
                        command.Parameters.Add("@Formulaid", SqlDbType.VarChar).Value = model.Formulaid ?? 0;                        
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
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
        public List<SalaryStructure.EmployeeSalary.List> GetEmployeeSalaryList(GetResponse modal)
        {
            List<SalaryStructure.EmployeeSalary.List> result = new List<SalaryStructure.EmployeeSalary.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Approve", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeSalaryList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.EmployeeSalary.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeSalaryList. The query was executed :", ex.ToString(), "spu_GetSS_GradeMaster", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }
        public SalaryStructure.EmployeeSalary.Add GetEmployeeSalaryAdd(GetResponse modal)
        {
            SalaryStructure.EmployeeSalary.Add result = new SalaryStructure.EmployeeSalary.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Empid", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@pageid", dbType: DbType.Int32, value: modal.AdditionalID1, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeSalary", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.EmployeeSalary.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new SalaryStructure.EmployeeSalary.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstGrade = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstSubcategory = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstStructure = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstAttachment = reader.Read<Attachments>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstEmp = reader.Read<DropDownList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeSalaryAdd. The query was executed :", ex.ToString(), "spu_GetEmployeeSalary", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }
        public List<SalaryStructure.EmployeeSalary.StructureList> GetEmployeeSalaryStructureList(SalaryStructure.GetStructureListResponse modal)
        {
            List<SalaryStructure.EmployeeSalary.StructureList> result = new List<SalaryStructure.EmployeeSalary.StructureList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int64, value: modal.id, direction: ParameterDirection.Input);
                    param.Add("@EmployeeSalaryid", dbType: DbType.Int64, value: modal.EmployeeSalaryid, direction: ParameterDirection.Input);
                    param.Add("@Empid", dbType: DbType.Int64, value: modal.Empid, direction: ParameterDirection.Input);
                    param.Add("@AnnualSalary", dbType: DbType.String, value: modal.AnnualSalary, direction: ParameterDirection.Input);
                    param.Add("@Structureid", dbType: DbType.Int64, value: modal.Structureid, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSS_EmployeeSalaryStructure", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.EmployeeSalary.StructureList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeSalaryStructureList. The query was executed :", ex.ToString(), "spu_GetSS_EmployeeSalaryStructure", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }
        public PostResponse fnSetEmployeeSalary(SalaryStructure.EmployeeSalary.Add model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSS_EmployeeSalary", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.BigInt).Value = model.id;
                        command.Parameters.Add("@Empid", SqlDbType.BigInt).Value = model.Empid;
                        command.Parameters.Add("@Gradeid", SqlDbType.BigInt).Value = model.Gradeid;
                        command.Parameters.Add("@Subcategoryid", SqlDbType.BigInt).Value = model.Subcategoryid;
                        command.Parameters.Add("@WorkingHours", SqlDbType.Decimal).Value = model.WorkingHours;
                        command.Parameters.Add("@Structureid", SqlDbType.BigInt).Value = model.Structureid;
                        command.Parameters.Add("@AnnualSalary", SqlDbType.Decimal).Value = model.AnnualSalary;
                        command.Parameters.Add("@EffectiveDate", SqlDbType.VarChar).Value = model.EffectiveDate;
                        command.Parameters.Add("@IsDraft", SqlDbType.Int).Value = model.IsDraft;
                        command.Parameters.Add("@CTC", SqlDbType.Decimal).Value = model.CTC;
                        command.Parameters.Add("@TotalHours", SqlDbType.Int).Value = model.TotalHours;
                        command.Parameters.Add("@HourlyRate", SqlDbType.VarChar).Value = model.HourlyRate;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
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
                                    string deleteids = "0";
                                    PostResponse result1 = new PostResponse();
                                    foreach (var item in model.lstSalryStructure)
                                    {
                                        item.LoginID = model.LoginID;
                                        item.EmployeeSalaryid = Convert.ToInt32(result.ID);
                                        result1= fnSetEmployeeSalaryStructure(item);
                                        if (result1.Status)
                                        {
                                            deleteids = deleteids + "," + result1.ID.ToString();
                                        }                                        
                                    }
                                    ClsCommon.fnSetDataString("update SS_EmployeeSalaryStructure set isdeleted=1,deletedat=CURRENT_TIMESTAMP,deletedby="+ model.LoginID.ToString() +" where id not in ("+ deleteids + ") and EmployeeSalaryid="+ result.ID.ToString() + " and isdeleted=0");
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
        public PostResponse fnSetEmployeeSalaryStructure(SalaryStructure.EmployeeSalary.StructureList model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSS_EmployeeSalaryStructure", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.BigInt).Value = model.id;
                        command.Parameters.Add("@EmployeeSalaryid", SqlDbType.BigInt).Value = model.EmployeeSalaryid;
                        command.Parameters.Add("@Componentid", SqlDbType.BigInt).Value = model.Componentid;
                        command.Parameters.Add("@StructureAmt", SqlDbType.BigInt).Value = 0;
                        command.Parameters.Add("@Amt", SqlDbType.Decimal).Value = model.Amount;                        
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
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
        public SalaryStructure.EmployeeSalary.Detail GetEmployeeSalaryDetail(GetResponse modal)
        {
            SalaryStructure.EmployeeSalary.Detail result = new SalaryStructure.EmployeeSalary.Detail();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);                   
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeSalaryDetail", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.EmployeeSalary.Detail>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new SalaryStructure.EmployeeSalary.Detail();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ListStructure = reader.Read<SalaryStructure.EmployeeSalary.StructureList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ListStructureMonthly = reader.Read<SalaryStructure.EmployeeSalary.StructureListMonthly>().ToList();
                        }                       
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeSalaryDetail. The query was executed :", ex.ToString(), "spu_GetEmployeeSalaryDetail", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }
        public SalaryStructure.DeductionEntry GetDeductionEntry(string EmpType,string Date)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            SalaryStructure.DeductionEntry  result = new SalaryStructure.DeductionEntry();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EmpType", dbType: DbType.String, value: EmpType, direction: ParameterDirection.Input);
                    param.Add("@MonthDate", dbType: DbType.DateTime, value: Convert.ToDateTime(Date), direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.String, value: LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeDeductionEntryList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.DeductionEntry>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new SalaryStructure.DeductionEntry();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Emplist = reader.Read<SalaryStructure.DeductionEntryEmp>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Component = reader.Read<SalaryStructure.DeductionEntryComponent>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDeductionEntry. The query was executed :", ex.ToString(), "spu_GetEmployeeDeductionEntryList", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            //Dictionary<long, float> Component;
            //for (int i = 0; i < result.Emplist.Count; i++)
            //{
            //    Component = new Dictionary<long, float>();
            //    foreach (var item in result.Component.Where(n => n.Empid == result.Emplist[i].Empid).ToList())
            //    {
            //        Component.Add(item.Id, item.Amt);
            //    }
            //    result.Emplist[i].ComponentAmt = Component;              
            //}            
            return result;
        }

        //public List<SalaryStructure.DeductionEntry> GetEmployeeDeductionList(SalaryStructure.Tabs modal)
        //{
        //   List<SalaryStructure.DeductionEntry> result = new List<SalaryStructure.DeductionEntry>();
        //    try
        //    {
        //        string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
        //        using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
        //        {
        //            var param = new DynamicParameters();
        //            param.Add("@EmpType", dbType: DbType.String, value: modal.Approve, direction: ParameterDirection.Input);
        //            param.Add("@MonthDate", dbType: DbType.String, value: modal.Date, direction: ParameterDirection.Input);
        //            param.Add("@LoginID", dbType: DbType.String, value: LoginID, direction: ParameterDirection.Input);
        //            DBContext.Open();
        //            using (var reader = DBContext.QueryMultiple("spu_GetEmployeeDeductionEntryList", param: param, commandType: CommandType.StoredProcedure))
        //            {
        //                result = reader.Read<SalaryStructure.DeductionEntry>().ToList();
        //                if (result == null)
        //                {
        //                    result = new List<SalaryStructure.DeductionEntry>();
        //                }
        //                if (!reader.IsConsumed)
        //                {
        //                    q = reader.Read<SalaryStructure.DeductionEntryComponent>().ToList();
        //                }                       
        //            }
        //            DBContext.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ClsCommon.LogError("Error during GetEmployeeSalaryDetail. The query was executed :", ex.ToString(), "spu_GetEmployeeSalaryDetail", "SalaryStructureModal", "SalaryStructureModal", "");
        //    }
        //    return result;
        //}
        public PostResponse fnSetDeductionEntry(int Month,int Year,long Empid,string ComponentValues)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSS_DeductionEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Month", SqlDbType.Int).Value = Month;
                        command.Parameters.Add("@Year", SqlDbType.Int).Value = Year;
                        command.Parameters.Add("@Empid", SqlDbType.BigInt).Value = Empid;
                        command.Parameters.Add("@SqlQry", SqlDbType.VarChar).Value = ComponentValues;                        
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
        public SalaryStructure.OtherEarningEntry GetOtherEarningEntry(string EmpType, string Date)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            SalaryStructure.OtherEarningEntry result = new SalaryStructure.OtherEarningEntry();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@EmpType", dbType: DbType.String, value: EmpType, direction: ParameterDirection.Input);
                    param.Add("@MonthDate", dbType: DbType.DateTime, value: Convert.ToDateTime(Date), direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.String, value: LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeOtherEarningEntryList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.OtherEarningEntry>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new SalaryStructure.OtherEarningEntry();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Emplist = reader.Read<SalaryStructure.OtherEarningEntryEmp>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Component = reader.Read<SalaryStructure.OtherEarningEntryComponent>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetOtherEarningEntry. The query was executed :", ex.ToString(), "spu_GetEmployeeOtherEarningEntryList", "SalaryStructureModal", "SalaryStructureModal", "");
            }                    
            return result;
        }
        public PostResponse fnSetOtherEarningEntry(int Month, int Year, long Empid, string ComponentValues)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSS_OtherEarningEntry", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Month", SqlDbType.Int).Value = Month;
                        command.Parameters.Add("@Year", SqlDbType.Int).Value = Year;
                        command.Parameters.Add("@Empid", SqlDbType.BigInt).Value = Empid;
                        command.Parameters.Add("@SqlQry", SqlDbType.VarChar).Value = ComponentValues;
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

        public SalaryStructure.EmployeeSalary.Add GetEmploymentHistory(GetResponse modal)
        {
            SalaryStructure.EmployeeSalary.Add result = new SalaryStructure.EmployeeSalary.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Empid", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmploymentHistory", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.EmployeeSalary.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new SalaryStructure.EmployeeSalary.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstGrade = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstSubcategory = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstStructure = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstAttachment = reader.Read<Attachments>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstEmp = reader.Read<DropDownList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeSalaryAdd. The query was executed :", ex.ToString(), "spu_GetEmployeeSalary", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }
        public List<SalaryStructure.EmployeeSalary.StructureList> GetEmploymentHistoryStructure_List(SalaryStructure.GetStructureListResponse modal)
        {
            List<SalaryStructure.EmployeeSalary.StructureList> result = new List<SalaryStructure.EmployeeSalary.StructureList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int64, value: modal.id, direction: ParameterDirection.Input);
                    param.Add("@Empid", dbType: DbType.Int64, value: modal.Empid, direction: ParameterDirection.Input);
                    param.Add("@FromDate", dbType: DbType.DateTime, value: modal.FromDate, direction: ParameterDirection.Input);
                    param.Add("@ToDate", dbType: DbType.DateTime, value: modal.ToDate, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmploymentHistoryStructureList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SalaryStructure.EmployeeSalary.StructureList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetSS_EmployeeSalaryStructureList. The query was executed :", ex.ToString(), "spu_GetSS_EmployeeSalaryStructureList", "SalaryStructureModal", "SalaryStructureModal", "");
            }
            return result;
        }


        public List<SalaryStructure.OtherBenefitPayment> GetOtherBenefitPaymentList(long FinyearID, long Componentid, string PaidDate)
        {
            List<SalaryStructure.OtherBenefitPayment> List = new List<SalaryStructure.OtherBenefitPayment>();
            try
            {
                DataSet SalaryModuleDataSet = Common_SPU.GetOtherBenefitPaymentList(FinyearID, Componentid, PaidDate);
                foreach (DataRow item in SalaryModuleDataSet.Tables[0].Rows)
                {
                    SalaryStructure.OtherBenefitPayment obj = new SalaryStructure.OtherBenefitPayment();
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
        public List<SalaryStructure.OtherBenefitPayment.OtherBenefitPaymentComponent> GetOtherBenefitPaymentComponentList(long FinyearID, long Componentid, string PaidDate)
        {
            List<SalaryStructure.OtherBenefitPayment.OtherBenefitPaymentComponent> List = new List<SalaryStructure.OtherBenefitPayment.OtherBenefitPaymentComponent>();
            try
            {
                DataSet SalaryModuleDataSet = Common_SPU.GetOtherBenefitPaymentComponentList(FinyearID, Componentid, PaidDate);
                foreach (DataRow item in SalaryModuleDataSet.Tables[0].Rows)
                {
                    SalaryStructure.OtherBenefitPayment.OtherBenefitPaymentComponent obj = new SalaryStructure.OtherBenefitPayment.OtherBenefitPaymentComponent();
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

    }
}