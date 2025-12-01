using Dapper;
using Mitr.CommonClass;
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
    public class BudgetModel : IBudgetHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public PostResponse SetTraining(BudgetMaster.AddTrainingWorkshopSeminarTypes model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTrainingWorkshopSeminar", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@Module", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.Module);
                        command.Parameters.Add("@TrainingName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.TrainingName);
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public PostResponse SetTrainingDetails(BudgetMaster.AddTrainingWorkshopSeminarDeatils model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTrainingWorkshopSeminarDet", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@TrainingWorkshopid", SqlDbType.Int).Value = model.TrainingWorkshopid;
                        command.Parameters.Add("@item", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.Item);
                        command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.Unit);
                        command.Parameters.Add("@Rate", SqlDbType.Float).Value = ClsCommon.EnsureString(model.Rate);
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public BudgetMaster.AddTrainingWorkshopSeminarTypes SetTrainingWorkshop(GetResponse modal)
        {
            BudgetMaster.AddTrainingWorkshopSeminarTypes result = new BudgetMaster.AddTrainingWorkshopSeminarTypes();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    //param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTrainingWorkshopSeminar", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<BudgetMaster.AddTrainingWorkshopSeminarTypes>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new BudgetMaster.AddTrainingWorkshopSeminarTypes();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ListTrainingDetails = reader.Read<BudgetMaster.AddTrainingWorkshopSeminarDeatils>().ToList();
                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetTrainingWorkshopSeminar()", "BudgetMasterModal", "BudgetMasterModal", "");

            }
            return result;
        }
        //public long FnSetTravelBudget(long id, long TravelYear, string TravelType, decimal AirFare, decimal PerDiem, decimal Hotel, decimal LocalTravel)
        //{
        //    long success = 0;
        //    success = Common_SPU.fnSetTravelBudget(id, TravelYear, TravelType, AirFare, PerDiem, Hotel, LocalTravel);
        //    return success;
        //}
        public List<BudgetMaster.TravelBudgetList> GetTravelBudgetList(long TravelId)
        {
            string SQL = "";
            List<BudgetMaster.TravelBudgetList> List = new List<BudgetMaster.TravelBudgetList>();
            BudgetMaster.TravelBudgetList obj = new BudgetMaster.TravelBudgetList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTravelBudgetMaster(TravelId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.TravelBudgetList();
                    obj.Id = Convert.ToInt64(item["Id"]);
                    obj.TravelType = item["TravelType"].ToString();
                    obj.AirFare = Convert.ToDecimal(item["AirFare"]);
                    obj.PerDiem = Convert.ToDecimal(item["PerDiem"]);
                    obj.Hotel = Convert.ToDecimal(item["Hotel"]);
                    obj.LocalTravel = Convert.ToDecimal(item["LocalTravel"]);
                    obj.Total = Convert.ToDecimal(item["total"]);
                    obj.TravelYear = Convert.ToInt64(item["Finyearid"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTravelBudgetList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }
        public List<BudgetMaster.TravelBudgetList> GetTravelBudgetListYear(long TravelId)
        {
            string SQL = "";
            List<BudgetMaster.TravelBudgetList> List = new List<BudgetMaster.TravelBudgetList>();
            BudgetMaster.TravelBudgetList obj = new BudgetMaster.TravelBudgetList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTravelBudgetMasterYear(TravelId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.TravelBudgetList();
                    obj.Id = Convert.ToInt64(item["ID"]);
                    obj.TravelType = item["year"].ToString();

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTravelBudgetListYear. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<BudgetMaster.TrainingWorkshopSeminarTypes> GetTrainingWorkTypeList(long WorkId)
        {
            string SQL = "";
            List<BudgetMaster.TrainingWorkshopSeminarTypes> List = new List<BudgetMaster.TrainingWorkshopSeminarTypes>();
            BudgetMaster.TrainingWorkshopSeminarTypes obj = new BudgetMaster.TrainingWorkshopSeminarTypes();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnTrainingWorkTypeList(WorkId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.TrainingWorkshopSeminarTypes();
                    obj.Id = Convert.ToInt64(item["ID"]);
                    obj.Module = item["Module"].ToString();
                    obj.TrainingName = item["TrainingName"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTrainingWorkTypeList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }
        public List<BudgetMaster.InflationRate> GetInflationRateList(long InflationId)
        {
            string SQL = "";
            List<BudgetMaster.InflationRate> List = new List<BudgetMaster.InflationRate>();
            BudgetMaster.InflationRate obj = new BudgetMaster.InflationRate();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetInflationRateMaster(InflationId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.InflationRate();
                    obj.Id = Convert.ToInt64(item["finyearid"]);
                    obj.Year = item["Year"].ToString();

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetInflationRateList. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;

        }
        public List<BudgetMaster.AddInflationRateDeatils> GetInflationRateYearWiseList(long Id, long YearId)
        {
            string SQL = "";
            List<BudgetMaster.AddInflationRateDeatils> List = new List<BudgetMaster.AddInflationRateDeatils>();
            BudgetMaster.AddInflationRateDeatils obj = new BudgetMaster.AddInflationRateDeatils();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetInflationRatewiseListr(Id, YearId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.AddInflationRateDeatils();
                    obj.Id = Convert.ToInt64(item["Id"]);
                    obj.Ledger = item["Ledger"].ToString();
                    obj.LedgerId = Convert.ToInt64(item["Ledgerid"]);
                    obj.InflationRate = Convert.ToDecimal(item["Rate"]);
                    obj.Year = Convert.ToInt64(item["finyearid"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetInflationRateList. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;

        }
        public PostResponse SetInflationDetails(BudgetMaster.AddInflationRateDeatils model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetInflationRate", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@Finyearid", SqlDbType.Int).Value = model.Year;
                        command.Parameters.Add("@Ledgerid", SqlDbType.Int).Value = model.LedgerId;
                        command.Parameters.Add("@Rate", SqlDbType.VarChar).Value = model.InflationRate;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public List<BudgetMaster.AddIndirectCostRate> GetindirectCostRate(long Id, long YearId)
        {
            string SQL = "";
            List<BudgetMaster.AddIndirectCostRate> List = new List<BudgetMaster.AddIndirectCostRate>();
            BudgetMaster.AddIndirectCostRate obj = new BudgetMaster.AddIndirectCostRate();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetIndirectCostRateList(Id, YearId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.AddIndirectCostRate();
                    obj.Id = Convert.ToInt64(item["Id"]);
                    obj.Ledger = item["Ledger"].ToString();
                    obj.LedgerId = Convert.ToInt64(item["Ledgerid"]);
                    obj.IndirectCostRate = Convert.ToDecimal(item["Rate"]);
                    obj.Year = Convert.ToInt64(item["finyearid"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetIndirectCostRate. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;

        }


        public PostResponse SetIndirectcostRate(BudgetMaster.AddIndirectCostRate model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetIndirectCostRate", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@Finyearid", SqlDbType.Int).Value = model.Year;
                        command.Parameters.Add("@Ledgerid", SqlDbType.Int).Value = model.LedgerId;
                        command.Parameters.Add("@Rate", SqlDbType.VarChar).Value = model.IndirectCostRate;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public List<BudgetMaster.IndirectCostRate> GetIndirectCostDetails(long IndirectId)
        {
            string SQL = "";
            List<BudgetMaster.IndirectCostRate> List = new List<BudgetMaster.IndirectCostRate>();
            BudgetMaster.IndirectCostRate obj = new BudgetMaster.IndirectCostRate();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetIndirectRateMasterList(IndirectId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.IndirectCostRate();
                    obj.Id = Convert.ToInt64(item["finyearid"]);
                    obj.SetYear = item["year"].ToString();

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetIndirectCostRateList. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;

        }

        public List<BudgetMaster.EmployeeList> GetBudgetSettingEmpList()
        {
            string SQL = "";
            List<BudgetMaster.EmployeeList> List = new List<BudgetMaster.EmployeeList>();
            BudgetMaster.EmployeeList obj = new BudgetMaster.EmployeeList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetBudgetEmployeeList();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.EmployeeList();
                    obj.Id = Convert.ToInt64(item["ID"]);
                    obj.Name = item["Name"].ToString();

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetEmployeeBudgetSetting. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;

        }
        public PostResponse SetbudgetSettingEmployee(BudgetMaster.BudgetAuthoritySetting model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudgetSettings", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@financePerson", SqlDbType.Int).Value = model.FinancePerson;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public List<BudgetMaster.EmployeeList> GetBudgetSettingMainEmpList(long EmployeeId)
        {
            string SQL = "";
            List<BudgetMaster.EmployeeList> List = new List<BudgetMaster.EmployeeList>();
            BudgetMaster.EmployeeList obj = new BudgetMaster.EmployeeList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetBudgetMainList(EmployeeId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.EmployeeList();
                    obj.Id = Convert.ToInt64(item["Id"]);
                    obj.FinancePerson = Convert.ToInt64(item["FinancePerson"]);
                    obj.Name = item["EMP_Name"].ToString();

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetEmployeeSettingsList. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;

        }


        public List<BudgetMaster.AddFringeBenefitRate> GetFringeBenefitRate(long Id, long YearId)
        {
            string SQL = "";
            List<BudgetMaster.AddFringeBenefitRate> List = new List<BudgetMaster.AddFringeBenefitRate>();
            BudgetMaster.AddFringeBenefitRate obj = new BudgetMaster.AddFringeBenefitRate();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetFringeBenefitRateList(Id, YearId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.AddFringeBenefitRate();
                    obj.Id = Convert.ToInt64(item["Id"]);
                    obj.Category = item["Category"].ToString();
                    obj.Year = Convert.ToInt64(item["finyearid"]);
                    obj.Rate = Convert.ToDecimal(item["Rate"]);

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetFringeBenefit_Rate. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;

        }
        public PostResponse SetFringeBenefitRate(BudgetMaster.AddFringeBenefitRate model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFringeBenefitRate", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@Finyearid", SqlDbType.Int).Value = model.Year;
                        command.Parameters.Add("@Category", SqlDbType.VarChar).Value = model.Category;
                        command.Parameters.Add("@Rate", SqlDbType.Float).Value = model.Rate;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public List<BudgetMaster.FringeBenefitRate> GetFringeBenefitRateDetails(long YearId)
        {
            string SQL = "";
            List<BudgetMaster.FringeBenefitRate> List = new List<BudgetMaster.FringeBenefitRate>();
            BudgetMaster.FringeBenefitRate obj = new BudgetMaster.FringeBenefitRate();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetFringeBenefitRateMasterList(YearId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.FringeBenefitRate();
                    obj.Id = Convert.ToInt64(item["finyearid"]);
                    obj.SetYear = item["year"].ToString();

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetIndirectCostRateList. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;

        }
        public PostResponse SetBudgetCreate(BudgetMaster.Budget model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudget", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@BudgetType", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.BudgetType);
                        command.Parameters.Add("@ThematicAreaId", SqlDbType.Int).Value = ClsCommon.EnsureNumber(model.ThematicAreaId);
                        command.Parameters.Add("@LocationId", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.LocationId);
                        command.Parameters.Add("@Projectid", SqlDbType.Int).Value = model.Projectid;
                        command.Parameters.Add("@Purpose ", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.Purpose);
                        command.Parameters.Add("@StartDate ", SqlDbType.DateTime).Value = model.StartDate;
                        command.Parameters.Add("@EndDate ", SqlDbType.DateTime).Value = model.EndDate;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public PostResponse SetBudgetOutCome(BudgetMaster.OutcomeDetails model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudgetOutCome", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id ", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@Budgetid", SqlDbType.Int).Value = model.BudgetId;
                        command.Parameters.Add("@OutCome", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.Outcome);
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.isdeleted;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public PostResponse SetBudgetActivity(BudgetMaster.Activitydetails model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudgetActivity", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id ", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@Budgetid", SqlDbType.Int).Value = model.BudgetId;
                        command.Parameters.Add("@Activity", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.Activity);
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.isdeleted;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public BudgetMaster.SetbudgetsubActivity SetbudgetSubActivity(long Id, long BudgetId, long ActivityId)
        {
            BudgetMaster.SetbudgetsubActivity result = new BudgetMaster.SetbudgetsubActivity();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: Id, direction: ParameterDirection.Input);
                    param.Add("@Budgetid", dbType: DbType.Int32, value: BudgetId, direction: ParameterDirection.Input);
                    param.Add("@Activityid", dbType: DbType.Int32, value: ActivityId, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBudgetSubActivity", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<BudgetMaster.SetbudgetsubActivity>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new BudgetMaster.SetbudgetsubActivity();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listActivity = reader.Read<BudgetMaster.BudgetActivity>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listoutcome = reader.Read<DropDownList>().ToList();
                        }
                        List<BudgetMaster.SubActivity> lstSubActivity = new List<BudgetMaster.SubActivity>();                        
                        if (!reader.IsConsumed)
                        {   
                                lstSubActivity = reader.Read<BudgetMaster.SubActivity>().ToList();                         
                        }                       
                            foreach (var item in result.listActivity)
                            {  
                                item.listSubActivity = lstSubActivity.Where(n => n.Activityid == item.Id).ToList();
                            }       
                        }
                    //if (result.listActivity.Count > 0)
                    //{
                    //    foreach (var item in result.listActivity)
                    //    {
                    //        item.listSubActivity = GetBudgetSubActivity(item.Id);
                    //    }
                    //}
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetTrainingWorkshopSeminar()", "BudgetMasterModal", "BudgetMasterModal", "");

            }
            return result;
        }
        //public List<BudgetMaster.SubActivity> GetBudgetSubActivity(long Activityid)
        //{
        //    string SQL = "";
        //    List<BudgetMaster.SubActivity> List = new List<BudgetMaster.SubActivity>();
        //    BudgetMaster.SubActivity obj = new BudgetMaster.SubActivity();
        //    try
        //    {
        //        DataSet TempModuleDataSet = Common_SPU.fnGetBudgetMainList(EmployeeId);
        //        foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
        //        {
        //            obj = new BudgetMaster.EmployeeList();
        //            obj.Id = Convert.ToInt64(item["Id"]);
        //            obj.FinancePerson = Convert.ToInt64(item["FinancePerson"]);
        //            obj.Name = item["EMP_Name"].ToString();

        //            List.Add(obj);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ClsCommon.LogError("Error during spu_GetEmployeeSettingsList. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
        //    }
        //    return List;

        //}
        public PostResponse SetBudgetSubActivityList(BudgetMaster.SubActivity model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudgetSubActivity", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id ", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@Activityid", SqlDbType.Int).Value = model.Activityid;
                        command.Parameters.Add("@Outcomeid", SqlDbType.Int).Value = model.Outcomeid;
                        command.Parameters.Add("@SubActivity", SqlDbType.VarChar).Value = model.Subactivity;
                        command.Parameters.Add("@BudgetRequired", SqlDbType.Int).Value = model.BudgetRequired;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.isdeleted;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public BudgetMaster.SetTentativePeriodofBudget GetBudgetTentativePeriod(long Id, long BudgetId)
        {
            BudgetMaster.SetTentativePeriodofBudget result = new BudgetMaster.SetTentativePeriodofBudget();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: Id, direction: ParameterDirection.Input);
                    param.Add("@Budgetid", dbType: DbType.Int32, value: BudgetId, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBudgetTentativePeriod", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<BudgetMaster.SetTentativePeriodofBudget>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new BudgetMaster.SetTentativePeriodofBudget();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ListinflationDetails = reader.Read<BudgetMaster.InflationDetails>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ListindirectDetails = reader.Read<BudgetMaster.IndirectDetails>().ToList();
                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetTrainingWorkshopSeminar()", "BudgetMasterModal", "BudgetMasterModal", "");

            }
            return result;
        }
        public PostResponse SetBudgetTentativePeriod(BudgetMaster.SetTentativePeriodofBudget model)      
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudgetTentativePeriod", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id ", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@BudgetId", SqlDbType.Int).Value = model.BudgetId;
                        command.Parameters.Add("@ReasonInflationRate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.ReasonInflationRate);
                        command.Parameters.Add("@FringeRateCore", SqlDbType.Float).Value = model.FringeRateCore;
                        command.Parameters.Add("@FringeRateTerm", SqlDbType.Float).Value = model.FringeRateTerm;
                        command.Parameters.Add("@FringeShownAs", SqlDbType.VarChar).Value = model.FringeShownAs;
                        command.Parameters.Add("@IndirectCost", SqlDbType.VarChar).Value = model.IndirectCost;
                        command.Parameters.Add("@ReasonIndirectRate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(model.ReasonIndirectRate);
                        command.Parameters.Add("@ReasonFringeRate", SqlDbType.VarChar).Value =ClsCommon.EnsureString(model.ReasonFringeRate);
                        command.Parameters.Add("@OtherCurrencyid", SqlDbType.Int).Value = model.OtherCurrencyid;
                        command.Parameters.Add("@OtherCurrencyRate ", SqlDbType.Float).Value = model.OtherCurrencyRate;
                        command.Parameters.Add("@OutcomeWiseBudget", SqlDbType.Int).Value = model.OutcomeWiseBudget;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.isdeleted;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public PostResponse SetBudgetInflationRate(BudgetMaster.InflationDetails model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudgetInflationRate", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id ", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@BudgetId", SqlDbType.Int).Value = model.BudgetId;
                        command.Parameters.Add("@Ledgerid", SqlDbType.Int).Value = model.Ledgerid;
                        command.Parameters.Add("@Rate", SqlDbType.Float).Value = model.Rate;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public PostResponse SetBudgetIndirectnRate(BudgetMaster.IndirectDetails model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudgetIndirectRate", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id ", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@BudgetId", SqlDbType.Int).Value = model.BudgetId;
                        command.Parameters.Add("@Ledgerid", SqlDbType.Int).Value = model.Ledgerid;
                        command.Parameters.Add("@Rate", SqlDbType.Float).Value = model.Rate;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public List<BudgetMaster.GetBudgetRPF> GetRFPBudgetProjectList(long ProjectId)
        {
            string SQL = "";
            List<BudgetMaster.GetBudgetRPF> List = new List<BudgetMaster.GetBudgetRPF>();
            BudgetMaster.GetBudgetRPF obj = new BudgetMaster.GetBudgetRPF();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetRFPBudgetProjectList(ProjectId);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.GetBudgetRPF();
                    obj.Id = Convert.ToInt64(item["ID"]);
                    obj.doc_no =ClsCommon.EnsureString (item["doc_no"].ToString());
                    obj.doc_date = Convert.ToDateTime(item["doc_date"]);
                    obj.start_date = item["start_date"].ToString();
                    obj.end_date = item["end_date"].ToString();
                    obj.proj_name = ClsCommon.EnsureString(Convert.ToString(item["proj_name"]));
                    obj.donor_id = Convert.ToInt64(item["donor_id"]);
                    obj.donor_Name = ClsCommon.EnsureString(item["donor_Name"].ToString());
                    obj.projref_no = ClsCommon.EnsureString(item["projref_no"].ToString());
                    obj.Status =ClsCommon.EnsureString (item["Status"].ToString());
                    obj.RowNo = Convert.ToInt64(item["RowNum"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetRFPBudgetProjectList. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;


        }

        public BudgetMaster.SetBudget GetBudgetSublineActivity(long Id, string BudgetType,long Projectid,long Ledgerid)
        {
            BudgetMaster.SetBudget result = new BudgetMaster.SetBudget();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: Id, direction: ParameterDirection.Input);
                    param.Add("@BudgetType", dbType: DbType.String, value: BudgetType, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    param.Add("@ProjectId", dbType: DbType.Int32, value: Projectid, direction: ParameterDirection.Input);
                     param.Add("@ledgerid", dbType: DbType.Int32, value: Ledgerid, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBudgetSublineActivity", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<BudgetMaster.SetBudget>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new BudgetMaster.SetBudget();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listBudgetSublineActivity = reader.Read<BudgetMaster.BudgetSublineActivity>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listProjectBudget = reader.Read<BudgetMaster.ProjectBudget>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listProjectBudgetYear = reader.Read<BudgetMaster.ProjectBudgetYear>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listProjectBudgetLedger = reader.Read<BudgetMaster.ProjectBudgetLedger>().ToList();
                            BudgetMaster.ProjectBudgetLedger objmodel = new BudgetMaster.ProjectBudgetLedger();
                            objmodel.ID = 1001;
                            objmodel.Name = "All";
                            result.listProjectBudgetLedger.Add(objmodel);
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listProjectBudgetEmployee = reader.Read<BudgetMaster.ProjectBudgetEmployee>().ToList();
                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetTrainingWorkshopSeminar()", "BudgetMasterModal", "BudgetMasterModal", "");

            }
            return result;
        }
        public BudgetMaster.SetBudget GetBudgetSublineActivityAll(string BudgetType, long Projectid, long Ledgerid)
        {
            BudgetMaster.SetBudget result = new BudgetMaster.SetBudget();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                 //   param.Add("@id", dbType: DbType.Int32, value: Id, direction: ParameterDirection.Input);
                    param.Add("@BudgetType", dbType: DbType.String, value: BudgetType, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    param.Add("@ProjectId", dbType: DbType.Int32, value: Projectid, direction: ParameterDirection.Input);
                    param.Add("@ledgerid", dbType: DbType.Int32, value: Ledgerid, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBudgetSublineActivityAll", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<BudgetMaster.SetBudget>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new BudgetMaster.SetBudget();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listBudgetSublineActivity = reader.Read<BudgetMaster.BudgetSublineActivity>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listProjectBudget = reader.Read<BudgetMaster.ProjectBudget>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listProjectBudgetYear = reader.Read<BudgetMaster.ProjectBudgetYear>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listProjectBudgetLedger = reader.Read<BudgetMaster.ProjectBudgetLedger>().ToList();
                            //BudgetMaster.ProjectBudgetLedger objmodel = new BudgetMaster.ProjectBudgetLedger();
                            //objmodel.ID = 1001;
                            //objmodel.Name = "All";
                          //  result.listProjectBudgetLedger.Add(objmodel);
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listProjectBudgetEmployee = reader.Read<BudgetMaster.ProjectBudgetEmployee>().ToList();
                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetTrainingWorkshopSeminar()", "BudgetMasterModal", "BudgetMasterModal", "");

            }
            return result;
        }
        public PostResponse SetBudgetSublineActivity(BudgetMaster.SetBudget model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudgetSublineActivity", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id ", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@BudgetId", SqlDbType.Int).Value = model.Budgetid;
                        command.Parameters.Add("@Ledgerid", SqlDbType.Int).Value = 0;
                        // command.Parameters.Add("@Ledgerid", SqlDbType.Int).Value = model.Ledgerid;
                        command.Parameters.Add("@Projectid", SqlDbType.Int).Value = model.Projectid;
                        command.Parameters.Add("@Finyearid", SqlDbType.Int).Value = model.Finyearid;
                      
                        command.Parameters.Add("@DelegationAuthority", SqlDbType.Int).Value = model.DelegationAuthority;
                        command.Parameters.Add("@EntryType", SqlDbType.VarChar).Value = model.EntryType;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public PostResponse SetBudgetSublineActivityDet(BudgetMaster.BudgetSublineActivity model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudgetSublineActivityDet", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@BudgetSublineid", SqlDbType.Int).Value = model.BudgetSublineid;
                        command.Parameters.Add("@Activityid", SqlDbType.Int).Value = model.Activityid;
                        command.Parameters.Add("@ActivityCode", SqlDbType.VarChar).Value = model.ActivityCode;
                        command.Parameters.Add("@SubActivityid", SqlDbType.Int).Value = model.SubActivityid;
                        command.Parameters.Add("@SubActivityCode", SqlDbType.VarChar).Value = model.SubActivityCode;
                        command.Parameters.Add("@Categoryid", SqlDbType.Int).Value = model.Categoryid;
                        command.Parameters.Add("@Amount", SqlDbType.VarChar).Value = model.Amount;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@Ledgerid", SqlDbType.VarChar).Value = model.Ledgerid;
                        command.Parameters.Add("@Code", SqlDbType.VarChar).Value = model.Code;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public List<BudgetMaster.SublineActivityList> GetSublineActivityList()
        {
            string SQL = "";
            List<BudgetMaster.SublineActivityList> List = new List<BudgetMaster.SublineActivityList>();
            BudgetMaster.SublineActivityList obj = new BudgetMaster.SublineActivityList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetSublineActivityList();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.SublineActivityList();
                   // obj.Id = Convert.ToInt64(item["Id"]);
                    obj.ProjectId = Convert.ToInt64(item["ProjectId"]);
                    obj.doc_no = ClsCommon.EnsureString(item["doc_no"].ToString());
                    // obj.LedgerName = Convert.ToString(item["LedgerName"]);
                    //ClsCommon.EnsureString(item["EndDate"].ToString());
                    obj.DelegationAuthority = ClsCommon.EnsureString(item["DelegationAuthority"].ToString());
                    obj.StartDate = Convert.ToString(item["StartDate"]);
                    obj.EndDate = Convert.ToString(item["EndDate"]);
                    obj.TotalAmount = Convert.ToDecimal(item["total"]);
                    obj.BudgetId = Convert.ToInt64(item["Id"]);
                    obj.SublineId = ClsCommon.EnsureNumber(item["SublineId"].ToString());
                    obj.ProjectName = ClsCommon.EnsureString(item["proj_name"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetRFPBudgetProjectList. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;


        }
        public List<BudgetMaster.SublineActivityList> GetSublineActivityDraftList()
        {
            string SQL = "";
            List<BudgetMaster.SublineActivityList> List = new List<BudgetMaster.SublineActivityList>();
            BudgetMaster.SublineActivityList obj = new BudgetMaster.SublineActivityList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetSublineActivitydraftList();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.SublineActivityList();
                    // obj.Id = Convert.ToInt64(item["Id"]);
                    obj.ProjectId = Convert.ToInt64(item["ProjectId"]);
                    obj.doc_no = ClsCommon.EnsureString(item["doc_no"].ToString());
                    //  obj.LedgerName = Convert.ToString(item["LedgerName"]);
                    //ClsCommon.EnsureString(item["EndDate"].ToString());
                    obj.DelegationAuthority = ClsCommon.EnsureString(item["DelegationAuthority"].ToString());
                    obj.StartDate = Convert.ToString(item["StartDate"]);
                    obj.EndDate = Convert.ToString(item["EndDate"]);
                    obj.TotalAmount = Convert.ToDecimal(item["total"]);
                    obj.BudgetId = Convert.ToInt64(item["Id"]);
                    obj.SublineId = ClsCommon.EnsureNumber(item["SublineId"].ToString());
                    obj.ProjectName = ClsCommon.EnsureString(item["proj_name"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetRFPBudgetProjectList. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;


        }
        public BudgetMaster.OutcomesActivities GetbudgetOutcome(long Id, long BudgetId, long ActivityId)
        {
            BudgetMaster.OutcomesActivities result = new BudgetMaster.OutcomesActivities();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: Id, direction: ParameterDirection.Input);
                    param.Add("@Budgetid", dbType: DbType.Int32, value: BudgetId, direction: ParameterDirection.Input);
                    param.Add("@Activityid", dbType: DbType.Int32, value: ActivityId, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBudgetSubActivity", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<BudgetMaster.OutcomesActivities>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new BudgetMaster.OutcomesActivities();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listactivity = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.listoutcome = reader.Read<DropDownList>().ToList();
                        }
                        List<BudgetMaster.OutcomeDetails> listoutcome = new List<BudgetMaster.OutcomeDetails>();
                        for(int i=0;i<result.listoutcome.Count;i++)
                        {
                            BudgetMaster.OutcomeDetails objoutcome = new BudgetMaster.OutcomeDetails();
                            objoutcome.Id = result.listoutcome[i].ID;
                            objoutcome.Outcome = result.listoutcome[i].Name;
                            listoutcome.Add(objoutcome);
                        }
                        result.ListOutcomeDetails = listoutcome;

                        List<BudgetMaster.Activitydetails> listactivity = new List<BudgetMaster.Activitydetails>();
                        for (int j = 0; j < result.listactivity.Count;j++)
                        {
                            BudgetMaster.Activitydetails objacte = new BudgetMaster.Activitydetails();
                            objacte.Id = result.listactivity[j].ID;
                            objacte.Activity = result.listactivity[j].Name;
                            listactivity.Add(objacte);
                        }
                        result.ListActivitydetails = listactivity;
                    }
                   
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetBudgetSubActivity()", "BudgetMasterModal", "BudgetMasterModal", "");

            }
            return result;
        }
        public List<BudgetMaster.StandaloneBudget> GetStandaloneBudgetList()
        {
            string SQL = "";
            List<BudgetMaster.StandaloneBudget> List = new List<BudgetMaster.StandaloneBudget>();
            BudgetMaster.StandaloneBudget obj = new BudgetMaster.StandaloneBudget();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetStandAloneList();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BudgetMaster.StandaloneBudget();
                    obj.Id = Convert.ToInt64(item["Id"]);
                    obj.StartDate = ClsCommon.EnsureString(item["StartDate"].ToString());
                    obj.EndDate = ClsCommon.EnsureString(item["EndDate"].ToString());
                    obj.ThematicAreaName = Convert.ToString(item["ThematicAreaName"]);
                    obj.Purposeobjective = ClsCommon.EnsureString(item["Purpose"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetRFPBudgetProjectList. The query was executed :", ex.ToString(), SQL, "BudgetModal", "BudgetModal", "");
            }
            return List;


        }

        public  PostResponse SetTravelBudgetDetails(BudgetMaster.AddTravelBudget model)
        {
            PostResponse result = new PostResponse();
            string SQL = "";
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTravelBudget", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@Finyearid", SqlDbType.Int).Value = model.TravelYear;
                        command.Parameters.Add("@TravelType", SqlDbType.VarChar).Value = model.TravelType;
                        command.Parameters.Add("@AirFare", SqlDbType.Float).Value = model.AirFare;
                        command.Parameters.Add("@PerDiem", SqlDbType.Float).Value = model.PerDiem;
                        command.Parameters.Add("@Hotel", SqlDbType.Float).Value = model.Hotel;
                        command.Parameters.Add("@LocalTravel", SqlDbType.Float).Value = model.LocalTravel;
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

        public PostResponse SetBudgetSublineActivityImport(string xmlData)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBudgetSublineActivityImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.Add("@xmlData", SqlDbType.VarChar).Value = xmlData;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                       
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
     

    }
}