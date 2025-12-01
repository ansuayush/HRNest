using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System.Data.SqlClient;
using System.Data;
using Mitr.CommonClass;
using Dapper;

namespace Mitr.ModelsMaster
{
    public class DLibraryModel : IDLibraryHelper
    {
        public PostResponse SetTag(DLibrary.TagAdd tag)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("spu_SetDL_TagMaster", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.Add("@id", SqlDbType.Int).Value = tag.ID;
                        com.Parameters.Add("@Thematic_id", SqlDbType.Int).Value = tag.Thematic_id;
                        com.Parameters.Add("@Tag", SqlDbType.VarChar).Value = tag.Tag;
                        com.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        com.CommandTimeout = 0;
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Result.ID = Convert.ToInt64(dr["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(dr["COMMANDSTATUS"]);
                                Result.SuccessMessage = dr["COMMANDMESSAGE"].ToString();
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

        public List<DLibrary.TagList> GetTagLists(GetResponse modal)
        {
            List<DLibrary.TagList> result = new List<DLibrary.TagList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDL_TagMaster", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<DLibrary.TagList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetDL_TagMaster. The query was executed :", ex.ToString(), "spu_GetDL_TagMaster()", "DLibraryModel", "DLibraryModel", "");

            }
            return result;
        }

        public DLibrary.TagAdd GetTag(long id)
        {
            DLibrary.TagAdd result = new DLibrary.TagAdd();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: id, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDL_TagMaster", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<DLibrary.TagAdd>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetDL_TagMaster. The query was executed :", ex.ToString(), "spu_GetDL_TagMaster()", "DLibraryModel", "DLibraryModel", "");

            }
            return result;
        }

        public DLibrary.ContentForm GetContentFormDetails(GetResponse modal)
        {
            DLibrary.ContentForm result = new DLibrary.ContentForm();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDL_Content", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<DLibrary.ContentForm>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new DLibrary.ContentForm();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.GenerateReq_No = reader.Read<DLibrary.GenerateRequest>().FirstOrDefault();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Place = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Project_Code = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Category = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Tag = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.Author = reader.Read<DropDownList>().ToList();
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetContentFormDetails. The query was executed :", ex.ToString(), "spu_GetDL_Content()", "DLibraryModel", "DLibraryModel", "");

            }
            return result;
        }

        public List<DropDownList> GetSubCategoryByCategory(string categoryid,string Doctype)
        {
            List<DropDownList> result = new List<DropDownList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value:0, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: ClsCommon.EnsureString(Doctype), direction: ParameterDirection.Input);
                    param.Add("@group_id", dbType: DbType.Int32, value: categoryid, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDL_Content", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result = reader.Read<DropDownList>().ToList();
                        }
                       
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetContentFormDetails. The query was executed :", ex.ToString(), "spu_GetDL_Content()", "DLibraryModel", "DLibraryModel", "");

            }
            return result;
        }

        public DLibrary.ProjectList GetProjectDetails(string Projectid)
        {
            DLibrary.ProjectList _list = new DLibrary.ProjectList();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ProjectId", dbType: DbType.String, value: Projectid, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDL_DataByProjectCode", param: param, commandType: CommandType.StoredProcedure))
                    {
                        _list = reader.Read<DLibrary.ProjectList>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetDL_TagMaster. The query was executed :", ex.ToString(), "spu_GetDL_DataByProjectCode()", "DLibraryModel", "DLibraryModel", "");

            }
            return _list;
        }

        public PostResponse AddContentForm(DLibrary.ContentForm model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("spu_SetDL_Content", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        com.Parameters.Add("@Req_Date", SqlDbType.VarChar).Value = model.Req_Date;
                        com.Parameters.Add("@Req_By", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        com.Parameters.Add("@CategoryId", SqlDbType.Int).Value = model.CategoryId;
                        com.Parameters.Add("@Sub_CategoryId", SqlDbType.Int).Value = model.Subcategoryid;
                        com.Parameters.Add("@Project_CodeId", SqlDbType.VarChar).Value = model.Project_CodeId;
                        com.Parameters.Add("@PlaceId", SqlDbType.VarChar).Value = model.PlaceId;
                        com.Parameters.Add("@Upload_Date", SqlDbType.VarChar).Value = model.Upload_Date;
                        com.Parameters.Add("@Report_No", SqlDbType.VarChar).Value = model.Report_No;
                        com.Parameters.Add("@Author_CoordinatorId", SqlDbType.VarChar).Value = model.Author_CoordinatorId;
                        com.Parameters.Add("@Title", SqlDbType.VarChar).Value = model.Title;
                        com.Parameters.Add("@Sub_Title", SqlDbType.VarChar).Value = model.Sub_Title;
                        com.Parameters.Add("@Tag_Id", SqlDbType.VarChar).Value = model.Tag_Id;
                        com.Parameters.Add("@Abstract_Summary", SqlDbType.VarChar).Value = model.Abstract;
                        com.Parameters.Add("@Remark", SqlDbType.VarChar).Value = model.Remarks;
                        com.Parameters.Add("@Document_Category", SqlDbType.VarChar).Value = model.Document_Category;
                        com.Parameters.Add("@Published", SqlDbType.VarChar).Value = model.Published;
                        com.Parameters.Add("@Document_ID", SqlDbType.VarChar).Value = model.Document_ID;
                        com.Parameters.Add("@Proposal_No", SqlDbType.VarChar).Value = model.Proposal_No;
                        com.Parameters.Add("@Accepted", SqlDbType.VarChar).Value = model.Accepted;
                        com.Parameters.Add("@Copyright", SqlDbType.VarChar).Value = model.Copyright;
                        com.Parameters.Add("@Contract_No", SqlDbType.VarChar).Value = model.Contract_No;
                        com.Parameters.Add("@Party_Name", SqlDbType.VarChar).Value = model.Party_Name;
                        com.Parameters.Add("@Effective_Date", SqlDbType.VarChar).Value = model.Effective_Date;
                        com.Parameters.Add("@Expiry_RenewableDate", SqlDbType.VarChar).Value = model.Expiry_Date;
                        com.Parameters.Add("@Source", SqlDbType.VarChar).Value = model.Source;
                        com.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        com.CommandTimeout = 0;
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Result.ID = Convert.ToInt64(dr["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(dr["COMMANDSTATUS"]);
                                Result.SuccessMessage = dr["COMMANDMESSAGE"].ToString();
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