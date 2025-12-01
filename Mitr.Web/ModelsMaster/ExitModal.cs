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
    public class ExitModal: IExitHelper
    {
       
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();


        public List<Exit.EMPList> GetEMPList_ByLocation(int LocationID)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<Exit.EMPList> result = new List<Exit.EMPList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@LocationID", dbType: DbType.Int32, value: LocationID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_ByLocation", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.EMPList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEMPList_ByLocation. The query was executed :", ex.ToString(), "spu_GetEMP_ByLocation()", "ExitModal", "ExitModal", "");

            }
            return result;
        }

        public List<Exit.Request.List> GetExitRequestList(int Approved)
        {

           
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            
            List<Exit.Request.List> result = new List<Exit.Request.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_ReqList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.Request.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExitRequestList. The query was executed :", ex.ToString(), "spu_GetExit_RequestList()", "ExitModal", "ExitModal", "");

            }
            return result;
        }

        public Exit.Request.Add GetExitRequest(long Exit_ID)
        {


            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            Exit.Request.Add result = new Exit.Request.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Exit_ID", dbType: DbType.Int64, value: Exit_ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_Req", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.Request.Add>().FirstOrDefault();
                        if(result==null)
                        {
                            result = new Exit.Request.Add();
                        }
                        if(!reader.IsConsumed)
                        {
                            result.ResignationReasonList = reader.Read<Exit.DDValues>().ToList();
                        }
                    }
                    
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExitRequest. The query was executed :", ex.ToString(), "spu_GetExit_RequestList()", "ExitModal", "ExitModal", "");

            }
            return result;
        }


        public List<Exit.Req_Received.List> GetExit_ReqRecList(int Approved)
        {


            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<Exit.Req_Received.List> result = new List<Exit.Req_Received.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_ReqRecList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.Req_Received.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExit_ReqRecList. The query was executed :", ex.ToString(), "spu_GetExit_ReqRecList()", "ExitModal", "ExitModal", "");

            }
            return result;
        }


        public Exit.Request_View GetExit_Req_View(long Exit_ID)
        {


            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            Exit.Request_View result = new Exit.Request_View();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Exit_ID", dbType: DbType.Int64, value: Exit_ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_Req_View", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.Request_View>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExit_Req_View. The query was executed :", ex.ToString(), "spu_GetExit_Req_View()", "ExitModal", "ExitModal", "");

            }
            return result;
        }


        public Exit.Req_Process.Forward GetHR_ExitForward(long Exit_ID)
        {
           
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            Exit.Req_Process.Forward result = new Exit.Req_Process.Forward();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Exit_ID", dbType: DbType.Int64, value: Exit_ID, direction: ParameterDirection.Input);
                    param.Add("@ActionType", dbType: DbType.String, value: "forward", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_HRProcess_Req", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result.LevelList = reader.Read<Exit.Req_Process.Forward.Level>().ToList();
                        if (!reader.IsConsumed)
                        {
                            result.EmpList = reader.Read<Exit.DDValues>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetHR_ExitForward. The query was executed :", ex.ToString(), "spu_GetExit_Req_View()", "ExitModal", "ExitModal", "");

            }
            return result;

        }


        public Exit.Req_Process.Approve GetHR_ExitApproveAndProcess(long Exit_ID)
        {

            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            Exit.Req_Process.Approve result = new Exit.Req_Process.Approve();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Exit_ID", dbType: DbType.Int64, value: Exit_ID, direction: ParameterDirection.Input);
                    param.Add("@ActionType", dbType: DbType.String, value: "approve_process", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_HRProcess_Req", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.Req_Process.Approve>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.DepartmentList = reader.Read<Exit.Req_Process.Approve.Department>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EmpList = reader.Read<Exit.DDValues>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LocationList = reader.Read<Exit.DDValues>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.NotificationEMPList = reader.Read<Exit.DDValues>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetHR_ExitForward. The query was executed :", ex.ToString(), "spu_GetExit_Req_View()", "ExitModal", "ExitModal", "");

            }
            return result;

        }



        public List<Exit.Req_Approved.List> GetExit_Req_ApprovedList(int Approved)
        {


            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<Exit.Req_Approved.List> result = new List<Exit.Req_Approved.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_Req_ApprovedList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.Req_Approved.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExit_Req_ApprovedList. The query was executed :", ex.ToString(), "spu_GetExit_ReqRecList()", "ExitModal", "ExitModal", "");

            }
            return result;
        }



        public PostResponse SetExit_Handover_Persons(Exit.Req_Process.Approve.Department modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetExit_Handover_Persons", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Exit_ID", SqlDbType.Int).Value = modal.Exit_ID;
                        command.Parameters.Add("@LocationID", SqlDbType.Int).Value = modal.LocationID;
                        command.Parameters.Add("@Department", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Dept);
                        command.Parameters.Add("@HODID", SqlDbType.Int).Value = modal.HODID;
                        command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Remarks);
                        command.Parameters.Add("@Notify_CC", SqlDbType.VarChar).Value =(modal.Notify_CC==null?"":ClsCommon.EnsureString(string.Join(",", modal.Notify_CC)));
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
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


        public PostResponse SetExit_Tasks(Exit.Exit_Task modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetExit_Tasks", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Exit_TaskID", SqlDbType.Int).Value = modal.Exit_TaskID;
                        command.Parameters.Add("@Exit_ID", SqlDbType.Int).Value = modal.Exit_ID;
                        command.Parameters.Add("@Task", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Task);
                        command.Parameters.Add("@Assignee_ID", SqlDbType.VarChar).Value = ClsCommon.EnsureString(string.Join(",",modal.Assignee_ID));
                        command.Parameters.Add("@Priority", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Priority);
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
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


        public PostResponse SetExit_Approved(long Exit_ID,string Approved_Remarks,int DeactiveSurvey,DateTime RelievingDate)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetExit_Approved", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Exit_ID", SqlDbType.Int).Value = Exit_ID;
                        command.Parameters.Add("@Approved_Remarks", SqlDbType.VarChar).Value = ClsCommon.EnsureString(Approved_Remarks);
                        command.Parameters.Add("@DeactiveSurvey", SqlDbType.Int).Value = DeactiveSurvey;
                        command.Parameters.Add("@RelievingDate", SqlDbType.DateTime).Value = RelievingDate;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
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




        public List<Exit.LevelApprovals.List> GetExit_ApproversList(int Approved)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            List<Exit.LevelApprovals.List> result = new List<Exit.LevelApprovals.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_ApproversList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.LevelApprovals.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExit_ApproversList. The query was executed :", ex.ToString(), "spu_GetExit_ReqRecList()", "ExitModal", "ExitModal", "");

            }
            return result;
        }


        public Exit.LevelApprovals.Add GetExit_ApproversAdd(long Exit_APP_ID)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");
            Exit.LevelApprovals.Add result = new Exit.LevelApprovals.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Exit_APP_ID", dbType: DbType.Int32, value: Exit_APP_ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_Approvers", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.LevelApprovals.Add>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExit_ApproversAdd. The query was executed :", ex.ToString(), "spu_GetExit_Approvers()", "ExitModal", "ExitModal", "");

            }
            return result;
        }

        public PostResponse SetExit_Approvers_Action(Exit.LevelApprovals.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetExit_Approvers_Action", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Exit_APP_ID", SqlDbType.Int).Value = modal.Exit_APP_ID;
                        command.Parameters.Add("@NA_Reason", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.NA_Reason);
                        command.Parameters.Add("@Suggested_RDate", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Suggested_RDate);
                        command.Parameters.Add("@Comment", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Comment);
                        command.Parameters.Add("@Approved", SqlDbType.Int).Value = modal.Approved;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
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


        public PostResponse SetExit_Handover_Persons_Default(long Exit_ID)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetExit_Handover_Persons_Default", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Exit_ID", SqlDbType.Int).Value = Exit_ID;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
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



        public Exit.StartExitProcess GetExit_StarExitProcess(long Exit_ID)
        {

            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            Exit.StartExitProcess result = new Exit.StartExitProcess();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Exit_ID", dbType: DbType.Int64, value: Exit_ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_StarExitProcess", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.StartExitProcess>().FirstOrDefault();
                        if(result==null)
                        {
                            result = new Exit.StartExitProcess();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RequestDetails = reader.Read<Exit.Request_View>().FirstOrDefault();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LevelApprovers_Details = reader.Read<Exit.LevelApprovers_Details>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.HandOver_Person = reader.Read<Exit.HandOver_Person>().ToList();
                        
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EmpList = reader.Read<Exit.DDValues>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetHR_ExitForward. The query was executed :", ex.ToString(), "spu_GetExit_Req_View()", "ExitModal", "ExitModal", "");

            }
            return result;

        }
      
        public PostResponse SetExit_StartProcess(Exit.StartExitProcess modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetExit_StartProcess", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Exit_ID", SqlDbType.Int).Value = modal.Exit_ID;
                        command.Parameters.Add("@NotifyIDs", SqlDbType.VarChar).Value =(modal.Notify_CC!=null ?string.Join(",", modal.Notify_CC):"");
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
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


        public List<Exit.NOC_Request.List> GetExit_NOC_List(long  Approved)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<Exit.NOC_Request.List> result = new List<Exit.NOC_Request.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_NOC_List", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.NOC_Request.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExit_NOC_List. The query was executed :", ex.ToString(), "spu_GetExit_NOC_List()", "ExitModal", "ExitModal", "");

            }
            return result;
        }


        public List<Exit.NOC_Dashboard.List> GetExit_NOC_Dashboard_List(long Approved)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<Exit.NOC_Dashboard.List> result = new List<Exit.NOC_Dashboard.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_NOC_Dashboard_List", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.NOC_Dashboard.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExit_NOC_Dashboard_List. The query was executed :", ex.ToString(), "spu_GetExit_NOC_Dashboard_List()", "ExitModal", "ExitModal", "");

            }
            return result;
        }


        public List<Exit.Confirmation.List> GetExit_Confirmation_List(long Approved)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<Exit.Confirmation.List> result = new List<Exit.Confirmation.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_Confirmation_List", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.Confirmation.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExit_Confirmation_List. The query was executed :", ex.ToString(), "spu_GetExit_Confirmation_List()", "ExitModal", "ExitModal", "");

            }
            return result;
        }

        public List<Exit.LevelApprovers_Details> GetExit_ApproversSuggationsList(long Exit_ID)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<Exit.LevelApprovers_Details> result = new List<Exit.LevelApprovers_Details>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@Exit_ID", dbType: DbType.Int32, value: Exit_ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExit_ApproversSuggationsList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Exit.LevelApprovers_Details>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExit_ApproversSuggationsList. The query was executed :", ex.ToString(), "spu_GetExit_ApproversSuggationsList()", "ExitModal", "ExitModal", "");

            }
            return result;
        }

    }
}