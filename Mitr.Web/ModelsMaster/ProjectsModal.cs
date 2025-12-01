using Dapper;
using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMaster
{
    public class ProjectsModal : IProjectsHelper
    {
        public List<Project.List> GetProjectRegistrationList(GetResponse modal)
        {
            List<Project.List> result = new List<Project.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Approve", dbType: DbType.Int32, value: modal.Approve, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProjectRegistrationList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Project.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProjRegistrationList. The query was executed :", ex.ToString(), "spu_GetProjRegistrationList()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }

        public Project.ProjectDetails GetProjectDetails(GetResponse modal)
        {
            Project.ProjectDetails result = new Project.ProjectDetails();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProjectRegistration", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Project.ProjectDetails>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new Project.ProjectDetails();
                            result.doc_date = DateTime.Now.ToString("dd-MMM-yyyy");
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EMPList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ThematicareaList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.FundTypeList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.FundingTypeList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ProgramList = reader.Read<DropDownList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProjectDetails. The query was executed :", ex.ToString(), "spu_GetProjectRegistration()", "ProjectModal", "ProjectModal", "");

            }
            return result;
        }


        public PostResponse SetProjectDetails(Project.ProjectDetails modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProjectDetails", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@ManagerID", SqlDbType.Int).Value = modal.ManagerID ?? 0;
                        command.Parameters.Add("@projref_no", SqlDbType.VarChar).Value = modal.projref_no ?? "";
                        command.Parameters.Add("@proj_name", SqlDbType.VarChar).Value = modal.proj_name ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = modal.Description ?? "";
                        command.Parameters.Add("@start_date", SqlDbType.VarChar).Value = modal.start_date ?? "";
                        command.Parameters.Add("@end_date", SqlDbType.VarChar).Value = modal.end_date ?? "";
                        command.Parameters.Add("@ThemArea_ID", SqlDbType.Int).Value = modal.ThemArea_ID ?? 0;
                        command.Parameters.Add("@FundTypeID", SqlDbType.Int).Value = modal.FundTypeID ?? 0;
                        command.Parameters.Add("@FundingTypeID", SqlDbType.Int).Value = modal.FundingTypeID ?? 0;
                        command.Parameters.Add("@ProgramIDs", SqlDbType.VarChar).Value = modal.ProgramIDs ?? "";
                        command.Parameters.Add("@TeamMemberID", SqlDbType.Int).Value = modal.TeamMemberID ?? 0;
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


        public Project.ClientsDetails GetProjectClientsDetails(GetResponse modal)
        {
            Project.ClientsDetails result = new Project.ClientsDetails();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProjectRegistration", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Project.ClientsDetails>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new Project.ClientsDetails();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DonorList = reader.Read<DropDownList>().ToList();
                        }

                        if (!reader.IsConsumed)
                        {
                            result.AgreementTypeList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ContactPersonList = reader.Read<Project.ContactPerson>().ToList();
                            if (result.ContactPersonList == null || result.ContactPersonList.Count == 0)
                            {
                                List<Project.ContactPerson> CNList = new List<Project.ContactPerson>();
                                CNList.Add(new Project.ContactPerson());
                                result.ContactPersonList = CNList;
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.ConsortiumProjectsList = reader.Read<DropDownList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetClientsDetails. The query was executed :", ex.ToString(), "spu_GetProjectRegistration()", "ProjectModal", "ProjectModal", "");

            }
            return result;
        }


        public PostResponse SetProjectClientsDetails(Project.ClientsDetails modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProjectClientsDetails", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@donor_id", SqlDbType.Int).Value = modal.donor_id;
                        command.Parameters.Add("@PrincipalDonorID", SqlDbType.VarChar).Value = modal.PrincipalDonorID ?? 0;
                        command.Parameters.Add("@ConsortiumProjectIDs", SqlDbType.VarChar).Value = modal.ConsortiumProjectIDs ?? "";
                        command.Parameters.Add("@AgreementID", SqlDbType.Int).Value = modal.AgreementID ?? 0;
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
        public PostResponse SetProject_ContactPerson(Project.ContactPerson modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProject_ContactPerson", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@DonorDetailsID", SqlDbType.Int).Value = modal.DonorDetailsID ?? 0;
                        command.Parameters.Add("@donor_id", SqlDbType.Int).Value = modal.donor_id ?? 0;
                        command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = modal.ProjectID ?? 0;
                        command.Parameters.Add("@person_name", SqlDbType.VarChar).Value = modal.person_name ?? "";
                        command.Parameters.Add("@designation", SqlDbType.VarChar).Value = modal.designation ?? "";
                        command.Parameters.Add("@location", SqlDbType.VarChar).Value = modal.location ?? "";
                        command.Parameters.Add("@phone_no", SqlDbType.VarChar).Value = modal.phone_no ?? "";
                        command.Parameters.Add("@email", SqlDbType.VarChar).Value = modal.email ?? "";
                        command.Parameters.Add("@Purpose", SqlDbType.VarChar).Value = modal.Purpose ?? "";
                        command.Parameters.Add("@Isdefault", SqlDbType.Int).Value = modal.Isdefault ?? 0;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = 1;
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


        public Project.BudgetDetails GetProjectBudgetDetails(GetResponse modal)
        {
            Project.BudgetDetails result = new Project.BudgetDetails();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProjectRegistration", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Project.BudgetDetails>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new Project.BudgetDetails();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CurrencyList = reader.Read<DropDownList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProjectBudgetDetails. The query was executed :", ex.ToString(), "spu_GetProjectRegistration()", "ProjectModal", "ProjectModal", "");

            }
            return result;
        }

        public PostResponse SetProjectBudgetDetails(Project.BudgetDetails modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProjectBudgetDetails", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@currency_id", SqlDbType.Int).Value = modal.currency_id ?? 0;
                        command.Parameters.Add("@ex_rate", SqlDbType.Decimal).Value = modal.ex_rate ?? 0;
                        command.Parameters.Add("@amount", SqlDbType.Decimal).Value = modal.amount ?? 0;
                        command.Parameters.Add("@amount_inr", SqlDbType.Decimal).Value = modal.amount_inr ?? 0;
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

        public Project.SpecialConditions GetProjectSpecialConditions(GetResponse modal)
        {
            Project.SpecialConditions result = new Project.SpecialConditions();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProjectRegistration", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Project.SpecialConditions>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new Project.SpecialConditions();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.conditionList = reader.Read<Project.SpecialConditions.Condition.List>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProjectSpecialConditions. The query was executed :", ex.ToString(), "spu_GetProjectRegistration()", "ProjectModal", "ProjectModal", "");

            }
            return result;
        }

        public Project.DonorsReport GetProjectDonorsReport(GetResponse modal)
        {
            Project.DonorsReport result = new Project.DonorsReport();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProjectRegistration", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Project.DonorsReport>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new Project.DonorsReport();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.reportList = reader.Read<Project.DonorsReport.ReportList>().ToList();
                            if (result.reportList == null || result.reportList.Count == 0)
                            {
                                List<Project.DonorsReport.ReportList> List = new List<Project.DonorsReport.ReportList>();
                                List.Add(new Project.DonorsReport.ReportList());
                                result.reportList = List;
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EMPList = reader.Read<DropDownList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProjectDonorsReport. The query was executed :", ex.ToString(), "spu_GetProjectRegistration()", "ProjectModal", "ProjectModal", "");

            }
            return result;
        }

        public Project.Attachment GetProjectAttachment(GetResponse modal)
        {
            Project.Attachment result = new Project.Attachment();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProjectRegistration", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Project.Attachment>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new Project.Attachment();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.documentList = reader.Read<Project.Attachment.Document.List>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProjectAttachment. The query was executed :", ex.ToString(), "spu_GetProjectRegistration()", "ProjectModal", "ProjectModal", "");

            }
            return result;
        }


        public PostResponse SetProject_SpecialConditions(Project.SpecialConditions.Condition.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProject_SpecialConditions", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = modal.ProjectID;
                        command.Parameters.Add("@Condition", SqlDbType.VarChar).Value = modal.Condition ?? "";
                        command.Parameters.Add("@AllocatedID", SqlDbType.Int).Value = modal.AllocatedID ?? 0;
                        command.Parameters.Add("@AutoClosure", SqlDbType.Int).Value = (string.IsNullOrEmpty(modal.Chk) ? 0 : 1);
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
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

        public PostResponse SetProject_DonorsReport(Project.DonorsReport.ReportList modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProject_DonorsReport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = modal.ProjectID;
                        command.Parameters.Add("@ReportName", SqlDbType.VarChar).Value = modal.ReportName ?? "";
                        command.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = modal.FromDate ?? "";
                        command.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = modal.ToDate ?? "";
                        command.Parameters.Add("@SubmissionDate", SqlDbType.VarChar).Value = modal.SubmissionDate ?? "";
                        command.Parameters.Add("@AllocatedTo", SqlDbType.Int).Value = modal.AllocatedTo ?? 0;
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = modal.AttachmentID ?? 0;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
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


        public PostResponse SetProject_Attachment(Project.Attachment.Document.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProject_Attachments", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = modal.ID ?? 0;
                        command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = modal.ProjectID;
                        command.Parameters.Add("@DocumentType", SqlDbType.VarChar).Value = modal.DocumentType ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = modal.Description ?? "";
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = modal.AttachmentID ?? 0;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
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


        public List<Project.DonorDetails> GetProject_PendingContactPerson(GetResponse modal)
        {
            List<Project.DonorDetails> result = new List<Project.DonorDetails>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@projectID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Donor_Id", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetProject_PendingContactPerson", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Project.DonorDetails>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProject_PendingContactPerson. The query was executed :", ex.ToString(), "spu_GetProject_PendingContactPerson", "ProjectsModal", "ProjectsModal", "");
            }
            return result;
        }


        public PostResponse SetProjectMakeItLive(GetResponse modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetProjectMakeItLive", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = modal.ID;
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

    }
}