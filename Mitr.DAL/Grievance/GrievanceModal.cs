
using Dapper;
using Mitr.Model;
using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Mitr.DAL
{
    public class GrievanceModal : IGrievancesHelper
    {

        public string DateFormat = "dd-MMM-yyyy", DateFormatC = "dd-MMM-yy hh:mm:ss tt", DateFormatE = "yyyy-MM-dd";
        public GrievanceModal()
        {
            var d = clsApplicationSetting.GetConfigValue("DateFormat");
            var dC = clsApplicationSetting.GetConfigValue("DateFormatC");
            DateFormat = (!string.IsNullOrEmpty(d) ? d : DateFormat);
            DateFormatC = (!string.IsNullOrEmpty(dC) ? dC : DateFormatC);
        }

        public List<SubCategory> GetSubCategoryList(long subCategoryID, int isDeleted)
        {
            List<SubCategory> List = new List<SubCategory>();
            try
            {
                DataSet grievancesModuleDataSet = Common_SPU.fnGetSubCategoryList(subCategoryID, isDeleted);
                foreach (DataRow item in grievancesModuleDataSet.Tables[0].Rows)
                {
                    SubCategory obj = new SubCategory();
                    obj.SubCategoryID = Convert.ToInt64(item["ID"]);
                    obj.SubCategoryName = Convert.ToString(item["SubCategoryName"]);
                    obj.CategoryID = Convert.ToInt64(item["CategoryID"]);
                    obj.CategoryName = Convert.ToString(item["CategoryName"]);
                    obj.Description = Convert.ToString(item["Description"]);
                    obj.Location = Convert.ToString(item["Location"]);
                    obj.Assignee = Convert.ToString(item["PrimaryAssignee"]);
                    obj.Level1 = Convert.ToString(item["Level1"]);
                    obj.DeviceType = Convert.ToString(item["Type"]);
                    obj.Createdby = Convert.ToInt32(item["createdby"]);
                    obj.Modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Deletedby = Convert.ToInt32(item["deletedby"]);
                    obj.CreateDat = Convert.ToDateTime(item["createdat"]).ToString(DateFormat);
                    obj.ModifieDat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormat);
                    obj.DeleteDat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormat);
                    obj.IPAddress = Convert.ToString(item["IPAddress"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"]);
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
        public SubCategory GetSubCategoryDetail(SubCategory modal)
        {
            SubCategory result = new SubCategory();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.SubCategoryID, direction: ParameterDirection.Input);
                    param.Add("@isdeleted", dbType: DbType.Int32, value: modal.IsDeleted);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetSubcategoryGr", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<SubCategory>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMasterAll. The query was executed :", ex.ToString(), "spu_GetGrSubcategory", "GrievancesModal", "GrievancesModal", "");
            }
            return result;
        }
        public List<ExternalMember> GetExternalMemberList(long externalyID)
        {
            List<ExternalMember> List = new List<ExternalMember>();
            try
            {
                DataSet grievancesModuleDataSet = Common_SPU.fnGetExternalMemberList(externalyID);
                foreach (DataRow item in grievancesModuleDataSet.Tables[0].Rows)
                {
                    ExternalMember obj = new ExternalMember();
                    obj.ID = Convert.ToInt64(item["ID"]);
                    obj.ExternalCode = Convert.ToString(item["ExternalCode"]);
                    obj.External_Member = Convert.ToString(item["External_Member"]);
                    obj.DOJ = Convert.ToString(item["DOJ"]) == "1900-01-01" ? string.Empty : Convert.ToDateTime(item["DOJ"]).ToString(DateFormat);
                    obj.DOL = Convert.ToString(item["DOL"]) == "1900-01-01" ? string.Empty : Convert.ToDateTime(item["DOL"]).ToString(DateFormat);
                    obj.Mobile = Convert.ToString(item["Mobile"]);
                    obj.EmailId = Convert.ToString(item["EmailId"]);
                    obj.CreateDat = Convert.ToDateTime(item["createdat"]).ToString(DateFormat);
                    obj.ModifieDat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormat);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExternalMemberList. The query was executed :", ex.ToString(), "spu_GetExternalMember\r\n", "GrievanceModal", "GrievanceModal", "");
            }
            return List;
        }
        public ExternalMember GetExternalMemberDetail(ExternalMember modal)
        {
            ExternalMember result = new ExternalMember();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExternalMemberGr", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<ExternalMember>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExternalMemberDetail. The query was executed :", ex.ToString(), "spu_GetExternalMember", "GrievancesModal", "GrievancesModal", "");
            }
            return result;
        }
        public PostResponseModel fnSetSubCategory(SubCategory model)
        {
            PostResponseModel result = new PostResponseModel();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSubCategoryGR", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.SubCategoryID;
                        command.Parameters.Add("@CategoryID", SqlDbType.Int).Value = model.CategoryID;
                        command.Parameters.Add("@SubCategoryName", SqlDbType.VarChar).Value = model.SubCategoryName ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = model.Description ?? "";
                        command.Parameters.Add("@Anonymous", SqlDbType.Bit).Value = model.AnonymousEmployee;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.Createdby;
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.IsDeleted;
                        command.Parameters.Add("@Type", SqlDbType.VarChar).Value = model.DeviceType ?? "Web";
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
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
        public PostResponseModel fnSetExternalMember(ExternalMember model)
        {
            PostResponseModel result = new PostResponseModel();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetExternalMemberGR", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@ExternalCode", SqlDbType.VarChar).Value = model.ExternalCode;
                        command.Parameters.Add("@ExternalMember", SqlDbType.VarChar).Value = model.External_Member ?? "";
                        command.Parameters.Add("@DOJ", SqlDbType.VarChar).Value = model.DOJ ?? "";
                        command.Parameters.Add("@DOL", SqlDbType.VarChar).Value = model.DOL ?? "";
                        command.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = model.Mobile ?? "";
                        command.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = model.EmailId ?? "";
                        command.Parameters.Add("@AddressLine1", SqlDbType.VarChar).Value = model.Address1 ?? "";
                        command.Parameters.Add("@AddressLine2", SqlDbType.VarChar).Value = model.Address2 ?? "";
                        command.Parameters.Add("@PINCode", SqlDbType.Int).Value = model.Pin ?? 0;
                        command.Parameters.Add("@Countryid", SqlDbType.Int).Value = model.CountryId ?? 0;
                        command.Parameters.Add("@Stateid", SqlDbType.Int).Value = model.StateId ?? 0;
                        command.Parameters.Add("@Cityid", SqlDbType.Int).Value = model.CityId ?? 0;
                        command.Parameters.Add("@AddressMobile", SqlDbType.Int).Value = model.Address_Mobile ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.Createdby;
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.IsDeleted;
                        command.Parameters.Add("@Type", SqlDbType.VarChar).Value = model.DeviceType ?? "Web";
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
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
        public string GetExternalCode()
        {
            ExternalMember result = new ExternalMember();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetExternalCode", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<ExternalMember>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExternalCode. The query was executed :", ex.ToString(), "spu_GetExternalMember", "GrievancesModal", "GrievancesModal", "");
            }
            return result.ExternalCode;
        }
        public List<SubCategoryAssigneeDetails> GetSubCategoryAssigneeList(long id, long Categoryid, long subCategoryid)
        {
            List<SubCategoryAssigneeDetails> List = new List<SubCategoryAssigneeDetails>();
            try
            {
                DataSet grievancesModuleDataSet = Common_SPU.fnGetSubCategoryAssigneeList(id, Categoryid, subCategoryid);
                foreach (DataRow item in grievancesModuleDataSet.Tables[0].Rows)
                {
                    SubCategoryAssigneeDetails obj = new SubCategoryAssigneeDetails();
                    obj.ID = Convert.ToInt64(item["ID"]);
                    obj.SubCategoryid = Convert.ToInt64(item["SubCategoryid"]);
                    obj.Categoryid = Convert.ToInt64(item["Categoryid"]);
                    obj.SubCategoryName = Convert.ToString(item["SubCategoryName"]);
                    obj.Location = Convert.ToString(item["Location"]);
                    obj.AssigneeId = Convert.ToInt64(item["AssigneeId"]);
                    obj.Assignee = Convert.ToString(item["Assignee"]);
                    obj.Level1 = Convert.ToString(item["Level1"]);
                    obj.Level2 = Convert.ToString(item["Level2"]);
                    obj.Level3 = Convert.ToString(item["Level3"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExternalMemberList. The query was executed :", ex.ToString(), "spu_GetExternalMember\r\n", "GrievanceModal", "GrievanceModal", "");
            }
            return List;
        }
        public PostResponseModel fnSetSubCategoryAssignee(SubCategoryAssigneeDetails model)
        {
            PostResponseModel result = new PostResponseModel();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSubCategoryAssigneeDetailsGR", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@Categoryid", SqlDbType.Int).Value = model.Categoryid;
                        command.Parameters.Add("@SubCategoryid", SqlDbType.Int).Value = model.SubCategoryid;
                        command.Parameters.Add("@Locationid", SqlDbType.Int).Value = model.LocationID;
                        command.Parameters.Add("@Assigneeid", SqlDbType.Int).Value = model.AssigneeId;
                        command.Parameters.Add("@Level1id", SqlDbType.VarChar).Value = model.Level1;
                        command.Parameters.Add("@Level1EnableEmail", SqlDbType.Bit).Value = model.Level1EnableEmail;
                        command.Parameters.Add("@Level2id", SqlDbType.VarChar).Value = model.Level2 ?? string.Empty;
                        command.Parameters.Add("@Level2EnableEmail", SqlDbType.Bit).Value = model.Level2EnableEmail;
                        command.Parameters.Add("@Level3id", SqlDbType.VarChar).Value = model.Level3 ?? string.Empty;
                        command.Parameters.Add("@Level3EnableEmail", SqlDbType.Bit).Value = model.Level3EnableEmail;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.Createdby;
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.IsDeleted;
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = model.SubCategoryid;
                                result.OtherID = model.Categoryid;
                                //result.ID = Convert.ToInt64(reader["RET_ID"]);
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
        public PostResponseModel fnDeleteSubCategoryAssignee(SubCategoryAssigneeDetails model)
        {
            PostResponseModel result = new PostResponseModel();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDeleteSubCategoryAssigneeDetailsGR", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.Createdby;
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.IsDeleted;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = model.SubCategoryid;
                                result.OtherID = model.Categoryid;
                                //result.ID = Convert.ToInt64(reader["RET_ID"]);
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
        public List<SubCategorySLAPolicy> GetSubCategorySLAPolicyList(long id, long subCategoryid)
        {
            List<SubCategorySLAPolicy> List = new List<SubCategorySLAPolicy>();
            try
            {
                DataSet grievancesModuleDataSet = Common_SPU.fnGetSubCategorySLAPolicyList(id, subCategoryid);
                foreach (DataRow item in grievancesModuleDataSet.Tables[0].Rows)
                {
                    SubCategorySLAPolicy obj = new SubCategorySLAPolicy();
                    obj.ID = Convert.ToInt64(item["ID"]);
                    obj.Categoryid = (string.IsNullOrEmpty(Convert.ToString(item["Categoryid"]))) ? 0 : Convert.ToInt64(item["Categoryid"]);
                    obj.SubCategoryid = (string.IsNullOrEmpty(Convert.ToString(item["SubCategoryid"]))) ? 0 : Convert.ToInt64(item["SubCategoryid"]);
                    obj.Priority = (string.IsNullOrEmpty(Convert.ToString(item["PriorityName"]))) ? string.Empty : Convert.ToString(item["PriorityName"]);
                    obj.ActionDefault = (string.IsNullOrEmpty(Convert.ToString(item["ActionDefault"]))) ? false : Convert.ToBoolean(item["ActionDefault"]);
                    obj.ActionFreezed = (string.IsNullOrEmpty(Convert.ToString(item["ActionFreezed"]))) ? false : Convert.ToBoolean(item["ActionFreezed"]);
                    obj.FirstResponse = (string.IsNullOrEmpty(Convert.ToString(item["FirstResponse"]))) ? string.Empty : (Convert.ToString(item["FirstResponse"]) == "0") ? string.Empty : Convert.ToString(item["FirstResponse"]);
                    obj.FollowUpEvery = (string.IsNullOrEmpty(Convert.ToString(item["FollowUpEvery"]))) ? string.Empty : (Convert.ToString(item["FollowUpEvery"]) == "0") ? string.Empty : Convert.ToString(item["FollowUpEvery"]);
                    obj.EscalationLevel1 = (string.IsNullOrEmpty(Convert.ToString(item["EscalationLevel1"]))) ? string.Empty : (Convert.ToString(item["EscalationLevel1"]) == "0") ? string.Empty : Convert.ToString(item["EscalationLevel1"]);
                    obj.EscalationLevel2 = (string.IsNullOrEmpty(Convert.ToString(item["EscalationLevel2"]))) ? string.Empty : (Convert.ToString(item["EscalationLevel2"]) == "0") ? string.Empty : Convert.ToString(item["EscalationLevel2"]);
                    obj.EscalationLevel3 = (string.IsNullOrEmpty(Convert.ToString(item["EscalationLevel3"]))) ? string.Empty : (Convert.ToString(item["EscalationLevel3"]) == "0") ? string.Empty : Convert.ToString(item["EscalationLevel3"]);
                    obj.TicketAutoCloseAfter = (string.IsNullOrEmpty(Convert.ToString(item["TicketAutoCloseAfter"]))) ? string.Empty : (Convert.ToString(item["TicketAutoCloseAfter"]) == "0") ? string.Empty : Convert.ToString(item["TicketAutoCloseAfter"]);

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExternalMemberList. The query was executed :", ex.ToString(), "spu_GetExternalMember\r\n", "GrievanceModal", "GrievanceModal", "");
            }
            return List;
        }
        public PostResponseModel fnSetSubCategorySLAPolicy(SubCategorySLAPolicy model)
        {
            PostResponseModel result = new PostResponseModel();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetSubCategorySLAPolicyGR", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@Categoryid", SqlDbType.Int).Value = model.Categoryid;
                        command.Parameters.Add("@SubCategoryid", SqlDbType.Int).Value = model.SubCategoryid;
                        //command.Parameters.Add("@Priority", SqlDbType.VarChar).Value = model.Priority;
                        command.Parameters.Add("@ActionDefault", SqlDbType.Bit).Value = model.ActionDefault;
                        command.Parameters.Add("@ActionFreezed", SqlDbType.Bit).Value = model.ActionFreezed;
                        command.Parameters.Add("@FirstResponse", SqlDbType.Int).Value = model.FirstResponse ?? string.Empty;
                        command.Parameters.Add("@FollowUpEvery", SqlDbType.Int).Value = model.FollowUpEvery ?? string.Empty;
                        command.Parameters.Add("@EscalationLevel1", SqlDbType.Int).Value = model.EscalationLevel1 ?? string.Empty;
                        command.Parameters.Add("@EscalationLevel2", SqlDbType.Int).Value = model.EscalationLevel2 ?? string.Empty;
                        command.Parameters.Add("@EscalationLevel3", SqlDbType.Int).Value = model.EscalationLevel3 ?? string.Empty;
                        command.Parameters.Add("@TicketAutoCloseAfter", SqlDbType.Int).Value = model.TicketAutoCloseAfter ?? string.Empty;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.Createdby;
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.IsDeleted;
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = model.SubCategoryid ?? 0;
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
        public List<UserGrievance> GetUserGrievanceList(UserGrievance modal)
        {
            List<UserGrievance> List = new List<UserGrievance>();
            try
            {
                DataSet grievancesModuleDataSet = Common_SPU.fnGetUserGrievanceList(modal);
                foreach (DataRow item in grievancesModuleDataSet.Tables[0].Rows)
                {
                    UserGrievance obj = new UserGrievance();
                    obj.ID = Convert.ToInt64(item["ID"]);
                    obj.TicketNo = Convert.ToString(item["TicketNo"]);
                    obj.Createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormat);
                    //obj.Categoryid = Convert.ToInt64(item["Categoryid"]);
                    obj.CategoryName = Convert.ToString(item["CategoryName"]);
                    obj.SubcatAssingid = string.IsNullOrEmpty(Convert.ToString(item["SubcatAssingid"])) ? 0 : Convert.ToInt64(item["SubcatAssingid"]);
                    obj.SubCategoryName = Convert.ToString(item["SubCategoryName"]);
                    obj.PrimaryAssignee = Convert.ToString(item["PrimaryAssignee"]);
                    obj.Level1 = Convert.ToString(item["Level1"]);
                    obj.Level2 = Convert.ToString(item["Level2"]);
                    obj.Level3 = Convert.ToString(item["Level3"]);
                    obj.Modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.CreatedbyName = Convert.ToString(item["CreatedbyName"]);
                    obj.ModifiedbyName = Convert.ToString(item["modifiedbyName"]);
                    obj.Modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormat);
                    obj.IsAnonymous = Convert.ToBoolean(item["IsAnonymous"]);

                    obj.Attachmentid1 = Convert.ToInt64(item["Attachmentid1"]);
                    obj.Attachmentid2 = Convert.ToInt64(item["Attachmentid2"]);
                    obj.AttachmentPath1 = Convert.ToString(item["AttachmentPath1"]);
                    obj.AttachmentPath2 = Convert.ToString(item["AttachmentPath2"]);



                    //obj.AnonymousValue = !string.IsNullOrEmpty(Convert.ToString(item["IsAnonymous"]))?"Yes":"No";
                    obj.Statusid = Convert.ToInt64(item["Statusid"]);
                    obj.Status = Convert.ToString(item["Status"]);
                    obj.ActionId = Convert.ToInt64(item["actionid"]);

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExternalMemberList. The query was executed :", ex.ToString(), "spu_GetExternalMember\r\n", "GrievanceModal", "GrievanceModal", "");
            }
            return List;
        }

        public PostResponseModel fnSetUserGrievance(UserGrievance model)
        {
            PostResponseModel result = new PostResponseModel();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUserGrievanceGr", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@Categoryid", SqlDbType.Int).Value = model.Categoryid;
                        command.Parameters.Add("@SubCategoryid", SqlDbType.Int).Value = model.Subcatid;
                        command.Parameters.Add("@Locationid", SqlDbType.Int).Value = model.Locationid;
                        command.Parameters.Add("@IsAnonymous", SqlDbType.Bit).Value = model.IsAnonymous;
                        command.Parameters.Add("@Attachment1", SqlDbType.BigInt).Value = model.Attachmentid1 ?? 0;
                        command.Parameters.Add("@Attachment2", SqlDbType.BigInt).Value = model.Attachmentid2 ?? 0;
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = model.Description ?? string.Empty;
                        command.Parameters.Add("@Status", SqlDbType.Int).Value = model.Statusid ?? 0;
                        //command.Parameters.Add("@Status", SqlDbType.VarChar).Value = model.Statusid;
                        command.Parameters.Add("@Type", SqlDbType.VarChar).Value = model.Type ?? string.Empty;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.Createdby;
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.Isdeleted;
                        command.Parameters.Add("@Priority", SqlDbType.VarChar).Value = model.Priorityid;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //result.ID = model.SubCategoryid;
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
        public UserGrievance GetUserGrievanceDetails(UserGrievance userGrievance)
        {
            UserGrievance result = new UserGrievance();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: userGrievance.ID, direction: ParameterDirection.Input);
                    param.Add("@catid", dbType: DbType.Int32, value: userGrievance.Categoryid, direction: ParameterDirection.Input);
                    param.Add("@subcatid", dbType: DbType.Int32, value: userGrievance.Subcatid, direction: ParameterDirection.Input);
                    param.Add("@locationid", dbType: DbType.Int32, value: userGrievance.Locationid, direction: ParameterDirection.Input);
                    param.Add("@loginid", dbType: DbType.Int32, value: userGrievance.Createdby, direction: ParameterDirection.Input);
                    param.Add("@loginpage", dbType: DbType.String, value: userGrievance.LoginPage, direction: ParameterDirection.Input);

                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetUserGrievanceListGR", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<UserGrievance>().FirstOrDefault();
                        if (result == null)
                            result = new UserGrievance();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {

                ClsCommon.LogError("Error during GetExternalMemberDetail. The query was executed :", ex.ToString(), "spu_GetExternalMember", "GrievancesModal", "GrievancesModal", "");
            }
            return result;
        }


        public PostResponseModel fnSetUserGrievanceAccident(UserGrievanceAccident model)
        {
            PostResponseModel result = new PostResponseModel();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUserGrievanceAccidentGr", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@Grievanceid", SqlDbType.Int).Value = model.Grievanceid;
                        command.Parameters.Add("@Categoryid", SqlDbType.Int).Value = model.Categoryid;
                        command.Parameters.Add("@SubCategoryid", SqlDbType.Int).Value = model.SubCategoryid;
                        command.Parameters.Add("@Locationid", SqlDbType.Int).Value = model.Locationid;
                        command.Parameters.Add("@SubcatAssingid", SqlDbType.Int).Value = model.SubcatAssingid;
                        command.Parameters.Add("@Choosename", SqlDbType.VarChar).Value = model.Choosename;
                        command.Parameters.Add("@EscalateTo", SqlDbType.Int).Value = model.Escalateid ?? 0;
                        command.Parameters.Add("@Employeeid", SqlDbType.VarChar).Value = model.Employee ?? string.Empty;
                        command.Parameters.Add("@Attachment", SqlDbType.Int).Value = model.Attachmentid;
                        command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = model.Remarks ?? string.Empty;
                        command.Parameters.Add("@Type", SqlDbType.VarChar).Value = model.Type ?? string.Empty;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.Createdby;
                        command.Parameters.Add("@isdeleted", SqlDbType.Int).Value = model.Isdeleted;

                        command.Parameters.Add("@ResolvedCategoryid", SqlDbType.Int).Value = model.ResolvedCategoryid ?? 0;
                        command.Parameters.Add("@ResolvedSubCategoryid", SqlDbType.Int).Value = model.ResolvedSubCategoryid ?? 0;


                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //result.ID = model.SubCategoryid;
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                result.OtherID = Convert.ToInt64(reader["CHOOSEID"]);

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
        public List<UserGrievanceAccident> GetUserGrievanceAccidentList(long id)
        {
            List<UserGrievanceAccident> List = new List<UserGrievanceAccident>();
            try
            {
                DataSet grievancesModuleDataSet = Common_SPU.fnGetUserGrievanceAccidentList(id);
                foreach (DataRow item in grievancesModuleDataSet.Tables[0].Rows)
                {
                    UserGrievanceAccident obj = new UserGrievanceAccident();
                    obj.ID = Convert.ToInt64(item["ID"]);
                    obj.Choosename = Convert.ToString(item["Choosename"]);
                    obj.Remarks = Convert.ToString(item["Remarks"]);
                    obj.Attachmentid = Convert.ToInt64(item["Attachmentid"]);
                    obj.Attachment = Convert.ToString(item["Attachment"]);
                    obj.Escalateid = Convert.ToInt32(item["Escalateid"]);
                    obj.EscalateTo = Convert.ToString(item["EscalateTo"]);
                    obj.Employee = Convert.ToString(item["Employee"]);
                    //obj.ModifiedbyName = Convert.ToString(item["modifiedbyName"]);
                    obj.Createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormat);
                    obj.Modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormat);
                    obj.CreatedbyName = Convert.ToString(item["CreatedbyName"]);
                    obj.ModifiedbyName = Convert.ToString(item["modifiedbyName"]);


                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetExternalMemberList. The query was executed :", ex.ToString(), "spu_GetExternalMember\r\n", "GrievanceModal", "GrievanceModal", "");
            }
            return List;
        }

        public List<SubcategoryGRAssigneeDetails> GetSubcategoryGRAssigneeDetails()
        {
            List<SubcategoryGRAssigneeDetails> List = new List<SubcategoryGRAssigneeDetails>();
            try
            {
                DataSet grievancesModuleDataSet = Common_SPU.fnGetSubcategoryGRAssigneeDetails();
                foreach (DataRow item in grievancesModuleDataSet.Tables[0].Rows)
                {
                    SubcategoryGRAssigneeDetails obj = new SubcategoryGRAssigneeDetails();
                    obj.Id = Convert.ToInt64(item["id"]);
                    obj.CATEGORYNAME = Convert.ToString(item["CATEGORYNAME"]);
                    obj.SUBCATEGORYNAME = Convert.ToString(item["SUBCATEGORYNAME"]);
                    obj.location_name = Convert.ToString(item["location_name"]);
                    obj.Level1id = Convert.ToString(item["Level1id"]);
                    obj.Level2id = Convert.ToString(item["Level2id"]);
                    obj.Level3id = Convert.ToString(item["Level3id"]);
                    obj.Assigneeid = Convert.ToString(item["Assigneeid"]);
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
    }
}