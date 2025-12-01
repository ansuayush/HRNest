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
    public class FundRaisingModal : IFundRaisingHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public List<FundRaising.Prospect.List> GetProspectList(long ID)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<FundRaising.Prospect.List> result = new List<FundRaising.Prospect.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int64, value: ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFR_ProspectList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<FundRaising.Prospect.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProspectList. The query was executed :", ex.ToString(), "spu_GetFR_ProspectList()", "FundRaisingModal", "FundRaisingModal", "");

            }
            return result;
        }
        public FundRaising.Prospect.Add GetFundRaisingDetail(GetResponse modal)
        {
            FundRaising.Prospect.Add result = new FundRaising.Prospect.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFR_Prospect", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<FundRaising.Prospect.Add>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new FundRaising.Prospect.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstCategory = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstKFA = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstCountry = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstState = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstC3Fit = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstEmp = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstContactDetails = reader.Read<FundRaising.Prospect.ContactDetails>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFundRaisingDetail. The query was executed :", ex.ToString(), "spu_GetFR_Prospect()", "FundRaisingModal", "FundRaisingModal", "");

            }
            return result;
        }
        public PostResponse SetProspect(FundRaising.Prospect.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFR_Prospect", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = modal.Id;
                        command.Parameters.Add("@ProspectName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ProspectName);
                        command.Parameters.Add("@ProspectType", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ProspectType);
                        command.Parameters.Add("@Catid", SqlDbType.Int).Value = modal.Catid;
                        command.Parameters.Add("@Company", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Company);
                        command.Parameters.Add("@Countryid", SqlDbType.Int).Value = modal.Countryid;
                        command.Parameters.Add("@KFA", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.KFA);
                        command.Parameters.Add("@Stateid", SqlDbType.Int).Value = modal.Stateid;
                        command.Parameters.Add("@Budget", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Budget);
                        command.Parameters.Add("@C3Fitid", SqlDbType.Int).Value = modal.C3Fitid;
                        command.Parameters.Add("@C3FitScore", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.C3FitScore.ToString());
                        command.Parameters.Add("@Responsibleid", SqlDbType.Int).Value = modal.Responsibleid;
                        command.Parameters.Add("@Accountableid", SqlDbType.Int).Value = modal.Accountableid;
                        command.Parameters.Add("@InformedToid", SqlDbType.Int).Value = modal.InformedToid;
                        command.Parameters.Add("@Consentedid", SqlDbType.Int).Value = modal.Consentedid;
                        command.Parameters.Add("@Website", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Website);
                        command.Parameters.Add("@OtherSupport", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.OtherSupport);
                        command.Parameters.Add("@Comment", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Comment);
                        command.Parameters.Add("@WebLink", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.WebLink);
                        command.Parameters.Add("@Stateids", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Stateids);
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

        public PostResponse SetProspectContact(FundRaising.Prospect.ContactDetails modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFR_ProspectContact", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = modal.Id;
                        command.Parameters.Add("@ContactPerson", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ContactPerson);
                        command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Designation);
                        command.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ContactNo);
                        command.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.EmailId);
                        command.Parameters.Add("@LinkedInId", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.LinkedInId);
                        command.Parameters.Add("@SecratoryDetails", SqlDbType.Int).Value = modal.SecratoryDetails;
                        command.Parameters.Add("@SecratoryName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SecratoryName);
                        command.Parameters.Add("@SecratoryPhone", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SecratoryPhone);
                        command.Parameters.Add("@SecratoryEmail", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SecratoryEmail);
                        command.Parameters.Add("@SecratoryOtherInfo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.SecratoryOtherInfo);
                        command.Parameters.Add("@ProspectId", SqlDbType.VarChar).Value = modal.ProspectId;
                        command.Parameters.Add("@IsUHNI", SqlDbType.Int).Value = modal.IsUHNI;
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = modal.Address;
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
        public List<FundRaising.Lead.List> GetLeadList(long ID, string Status, long StageLevelId)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<FundRaising.Lead.List> result = new List<FundRaising.Lead.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int64, value: ID, direction: ParameterDirection.Input);
                    param.Add("@Status", dbType: DbType.String, value: Status, direction: ParameterDirection.Input);
                    param.Add("@StageLevelid", dbType: DbType.Int64, value: StageLevelId, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFR_LeadList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<FundRaising.Lead.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeadList. The query was executed :", ex.ToString(), "spu_GetFR_LeadList()", "FundRaisingModal", "FundRaisingModal", "");

            }
            return result;
        }
        public FundRaising.Lead.Add GetLead(GetResponse modal)
        {
            FundRaising.Lead.Add result = new FundRaising.Lead.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFR_Lead", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<FundRaising.Lead.Add>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new FundRaising.Lead.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstProspect = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstProspectContact = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstState = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstStageLevel = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstRefferals = reader.Read<FundRaising.Lead.Refferals>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstStage = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstActivity = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.lstLeadActivity = reader.Read<FundRaising.Lead.LeadActivity>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetFR_Lead()", "FundRaisingModal", "FundRaisingModal", "");

            }
            return result;
        }
        public PostResponse SetLead(FundRaising.Lead.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFR_Lead", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = modal.Id;
                        command.Parameters.Add("@DocNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DocNo);
                        command.Parameters.Add("@DocDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DocDate);
                        command.Parameters.Add("@StageLevelid", SqlDbType.Int).Value = modal.StageLevelid;
                        command.Parameters.Add("@NickName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.NickName);
                        command.Parameters.Add("@Revenue", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Revenue);
                        command.Parameters.Add("@Propectid", SqlDbType.Int).Value = modal.Prospectid;
                        command.Parameters.Add("@ProspectContactid", SqlDbType.Int).Value = modal.ProspectContactid;
                        command.Parameters.Add("@IsConsider", SqlDbType.Int).Value = modal.IsConsider;
                        command.Parameters.Add("@Stateids", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Stateids);
                        command.Parameters.Add("@Reason", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Reason);
                        command.Parameters.Add("@ChequeFor", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ChequeFor);
                        command.Parameters.Add("@ChequeTo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ChequeTo);
                        command.Parameters.Add("@ChequeIssueDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ChequeIssueDate);
                        command.Parameters.Add("@ChequeNameof", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ChequeNameof);
                        command.Parameters.Add("@ChequeAmount", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ChequeAmount);
                        command.Parameters.Add("@ChequeRemark", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ChequeRemark);
                        command.Parameters.Add("@ProspectContactOtherId", SqlDbType.Int).Value = modal.ProspectContactOtherId;
                        command.Parameters.Add("@LatestAction", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.LatestAction);
                        command.Parameters.Add("@ActionTaken", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ActionTaken);
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
        public PostResponse SetLeadActivity(FundRaising.Lead.LeadActivity modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFR_LeadActivity", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = modal.Id;
                        command.Parameters.Add("@LeadId", SqlDbType.Int).Value = modal.LeadId;
                        command.Parameters.Add("@ActivityDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ActivityDate);
                        command.Parameters.Add("@Stageid", SqlDbType.Int).Value = modal.Stageid;
                        command.Parameters.Add("@Activityid", SqlDbType.Int).Value = modal.Activityid;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Remarks);
                        command.Parameters.Add("@NextActionDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.NextActionDate);
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
        public PostResponse SetLeadReferrals(FundRaising.Lead.Refferals modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetFR_LeadReferrals", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = modal.Id;
                        command.Parameters.Add("@LeadId", SqlDbType.Int).Value = modal.LeadId;
                        command.Parameters.Add("@ReferralName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ReferralName);
                        command.Parameters.Add("@ReferredDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ReferredDate);
                        command.Parameters.Add("@Referredby", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ReferredBy);
                        command.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.ContactNo);
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
        public List<FundRaising.RefferalsTask> GetReferralTaskList(long ID, string Status)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<FundRaising.RefferalsTask> result = new List<FundRaising.RefferalsTask>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int64, value: ID, direction: ParameterDirection.Input);
                    param.Add("@Status", dbType: DbType.String, value: Status, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetFR_LeadReferralsList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<FundRaising.RefferalsTask>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetReferralTaskList. The query was executed :", ex.ToString(), "spu_GetFR_LeadReferralsList()", "FundRaisingModal", "FundRaisingModal", "");

            }
            return result;
        }
        public FundRaising.LeadDashboard GetLeadDashboardList()
        {
            string SQL = "";
          FundRaising.LeadDashboard listdashboard = new FundRaising.LeadDashboard();
     
            try
            {
                List<FundRaising.StageCount> List = new List<FundRaising.StageCount>();
                List<FundRaising.NatureCount> Listnature = new List<FundRaising.NatureCount>();
                List<FundRaising.LeadList> Listlead = new List<FundRaising.LeadList>();
                DataSet TempModuleDataSet = Common_SPU.fnGetLeadDashboard();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    FundRaising.StageCount obj = new FundRaising.StageCount();
                    obj.FieldName = item["FieldName"].ToString();
                    obj.Value = item["Value"].ToString();
                    List.Add(obj);
                }
                foreach (DataRow item in TempModuleDataSet.Tables[1].Rows)
                {
                    FundRaising.NatureCount objnature = new FundRaising.NatureCount();
                    objnature.ToFieldName = item["ToFieldName"].ToString();
                    objnature.TotalPrice = Convert.ToDecimal(item["TotalPrice"]);
                    objnature.Count = Convert.ToDecimal(item["Count"]);
                    Listnature.Add(objnature);
                }
                foreach (DataRow item in TempModuleDataSet.Tables[2].Rows)
                {
                    FundRaising.LeadList objlead = new FundRaising.LeadList();
                    objlead.Id = Convert.ToInt64(item["Id"]);
                    objlead.RowNum = Convert.ToInt64(item["RowNum"]);
                    objlead.DocNo = Convert.ToInt64(item["DocNo"]);
                    objlead.DocDate = Convert.ToString(item["DocDate"]);
                    objlead.NickName = Convert.ToString(item["NickName"]);
                    objlead.Revenue = Convert.ToDecimal(item["Revenue"]);
                    objlead.Reason = Convert.ToString(item["Reason"]);
                    objlead.StageLevel = Convert.ToString(item["StageLevel"]);
                    objlead.ProspectName = Convert.ToString(item["ProspectName"]);
                    objlead.ContactPerson = Convert.ToString(item["ContactPerson"]);
                    objlead.C3Fit = Convert.ToString(item["C3Fit"]);
                    objlead.Responsible = Convert.ToString(item["Responsible"]);
                    Listlead.Add(objlead);
                }
                listdashboard.listLead = Listlead;
                listdashboard.listNatureCount = Listnature;
                listdashboard.listSatgeCount = List;
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_Getleaddashbaordlist. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return listdashboard;

        }
        



    }
}