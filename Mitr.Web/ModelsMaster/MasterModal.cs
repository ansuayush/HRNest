
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
    public class MasterModal : IMasterHelper
    {
        public string DateFormat = "dd-MMM-yyyy", DateFormatC = "dd-MMM-yy hh:mm:ss tt", DateFormatE = "yyyy-MM-dd";
        public MasterModal()
        {
            var d = clsApplicationSetting.GetConfigValue("DateFormat");
            var dC = clsApplicationSetting.GetConfigValue("DateFormatC");
            DateFormat = (!string.IsNullOrEmpty(d) ? d : DateFormat);
            DateFormatC = (!string.IsNullOrEmpty(dC) ? dC : DateFormatC);
        }

        public List<Department.List> GetDepartmentList(GetMasterResponse modal)
        {
            List<Department.List> result = new List<Department.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@IsActive", dbType: DbType.String, value: modal.IsActive);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDepartment", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Department.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDepartmentList. The query was executed :", ex.ToString(), "spu_GetDepartment", "MasterModal", "MasterModal", "");
            }
            return result;
        }

        public Department.Add GetDepartment(GetMasterResponse modal)
        {
            Department.Add result = new Department.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@IsActive", dbType: DbType.String, value: modal.IsActive ?? "");
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDepartment", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Department.Add>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDepartment. The query was executed :", ex.ToString(), "spu_GetDepartment", "MasterModal", "MasterModal", "");
            }
            return result;
        }


        public PostResponse fnSetDepartment(Department.Add model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDepartment", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID ?? 0;
                        command.Parameters.Add("@DepartmentName", SqlDbType.VarChar).Value = model.DepartmentName ?? "";
                        command.Parameters.Add("@DepartmentCode", SqlDbType.VarChar).Value = model.DepartmentCode ?? "";
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = model.Description ?? "";
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;

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

        public List<PerKM> GetPerKmlist(long Id)
        {
            List<PerKM> List = new List<PerKM>();
            PerKM obj = new PerKM();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPerKmList(Id);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PerKM();
                    obj.Id = Convert.ToInt64(item["ID"]);
                    obj.FromKm = Convert.ToInt32(item["from_km"]);
                    obj.ToKm = Convert.ToInt32(item["to_km"]);
                    obj.KM = Convert.ToInt64(item["km"]);
                    obj.KMRate = Convert.ToInt64(item["km_rate"]);
                    obj.VehicleType = item["vehicle_type"].ToString();
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetStateList. The query was executed :", ex.ToString(), "spu_GetState", "MasterModal", "MasterModal", "");
            }
            return List;

        }
        public PostResponse fnsetAddPerKm(PerKM model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetKm", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.Id ;
                        command.Parameters.Add("@km", SqlDbType.Int).Value = model.KM ;
                        command.Parameters.Add("@km_rate", SqlDbType.Int).Value = model.KMRate;
                        command.Parameters.Add("@from_km", SqlDbType.Int).Value = model.FromKm ;
                        command.Parameters.Add("@to_km", SqlDbType.VarChar).Value = model.ToKm ;
                        command.Parameters.Add("@vehicle_type", SqlDbType.VarChar).Value = model.VehicleType;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;

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
        public List<MasterAll.List> GetMasterAllList(GetMasterResponse modal)
        {
            List<MasterAll.List> result = new List<MasterAll.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@tablename", dbType: DbType.String, value: modal.TableName ?? "");
                    param.Add("@groupid", dbType: DbType.Int32, value: modal.GroupID);
                    param.Add("@IsActive", dbType: DbType.String, value: modal.IsActive);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMasterAll", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<MasterAll.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMasterAllList. The query was executed :", ex.ToString(), "spu_GetMasterAll", "MasterModal", "MasterModal", "");
            }
            return result;
        }

        public MasterAll.Add GetMasterAll(GetMasterResponse modal)
        {
            MasterAll.Add result = new MasterAll.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@tablename", dbType: DbType.String, value: modal.TableName ?? "");
                    param.Add("@groupid", dbType: DbType.Int32, value: modal.GroupID);
                    param.Add("@IsActive", dbType: DbType.String, value: modal.IsActive??"");
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMasterAll", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<MasterAll.Add>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMasterAll. The query was executed :", ex.ToString(), "spu_GetMasterAll", "MasterModal", "MasterModal", "");
            }
            return result;
        }
        public PostResponse fnSetMasterAll(MasterAll.Add model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMasterAll", con))
                    {
                        //code change by hotel by list 16/09/2023 // 
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID??0;
                        command.Parameters.Add("@table_name", SqlDbType.VarChar).Value = model.table_name ?? "";
                        command.Parameters.Add("@field_name", SqlDbType.VarChar).Value = model.field_name ?? "";
                        command.Parameters.Add("@field_value", SqlDbType.VarChar).Value = model.field_value ?? "";
                        command.Parameters.Add("@field_name1", SqlDbType.VarChar).Value = model.field_name1 ?? "";
                        command.Parameters.Add("@field_name2", SqlDbType.VarChar).Value = model.field_name2 ?? "";
                        command.Parameters.Add("@group_id", SqlDbType.Int).Value = model.group_id??0;
                        command.Parameters.Add("@srno", SqlDbType.Int).Value = model.srno ?? 0;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;

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

       

        public List<Country> GetCountryList(long CountryID)
        {
            List<Country> List = new List<Country>();
            Country obj = new Country();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetCountry(CountryID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Country();
                    obj.CountryID = Convert.ToInt64(item["ID"]);
                    obj.CountryName = item["Country_Name"].ToString();
                    obj.CountryMinutes = Convert.ToInt32(item["Country_Minutes"]);
                    obj.CountryHours = Convert.ToInt32(item["Country_Hours"]);
                    obj.Description = item["description"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.Priority = Convert.ToInt32(item["Priority"]);

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetCountryList. The query was executed :", ex.ToString(), "spu_GetCountry", "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<City> GetCityList(long CityID)
        {
            List<City> List = new List<City>();
            City obj = new City();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetcity(CityID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new City();
                    obj.CityID = Convert.ToInt64(item["ID"]);
                    obj.CityName = item["City_Name"].ToString();
                    obj.StateID = Convert.ToInt64(item["State_ID"]);
                    obj.StateName = item["State_Name"].ToString();
                    obj.Category = item["Category"].ToString();
                    obj.Description = item["description"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetCityList. The query was executed :", ex.ToString(), "spu_GetCity", "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<State> GetStateList(long StateID)
        {
            List<State> List = new List<State>();
            State obj = new State();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetState(StateID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new State();
                    obj.StateID = Convert.ToInt64(item["ID"]);
                    obj.StateName = item["State_Name"].ToString();
                    obj.CountryID = Convert.ToInt32(item["Country_ID"]);
                    obj.CountryName = item["Country_Name"].ToString();
                    obj.Description = item["description"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetStateList. The query was executed :", ex.ToString(), "spu_GetState", "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<Consultant> GetConsultantList(long ConsultantID)
        {
            List<Consultant> List = new List<Consultant>();
            Consultant obj = new Consultant();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetConsultant(ConsultantID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Consultant();
                    obj.ConsultantID = Convert.ToInt64(item["ID"]);
                    obj.ConsultantName = item["Consultant_Name"].ToString();
                    obj.Code = item["Consultant_Code"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.LinkedThematicArea = item["LinkedThematicArea"].ToString().Trim(',');
                    obj.PanName = item["Name_onPanCard"].ToString();
                    obj.FatherName = item["Father_Name"].ToString();
                    obj.LocalAddress = item["Local_Address"].ToString();
                    obj.PerAddress = item["perm_Address"].ToString();
                    obj.Location = item["Location"].ToString();
                    obj.PhoneNO = item["Phone_NO"].ToString();
                    obj.PanNo = item["Pan_No"].ToString();
                    obj.PayMode = item["Pay_Mode"].ToString();
                    obj.Design = item["Design"].ToString();
                    obj.AccountNo = item["Account_No"].ToString();
                    obj.BankName = item["Bank_Name"].ToString();
                    obj.IFSCCode = item["neft_code"].ToString();
                    obj.AccountName = item["Account_Name"].ToString();
                    obj.AttachmentID = Convert.ToInt32(item["Attachment_ID"]);

                    obj.ContentType = item["content_type"].ToString();
                    obj.FileName = item["filename"].ToString();

                    obj.ContactPerson = item["Contact_Person"].ToString();
                    obj.Email = item["Email"].ToString();
                    obj.Title = item["Title"].ToString();
                    obj.Design = item["Design"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetConsultantList. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public Consultant GetConsultant(long ConsultantID)
        {
            Consultant Obj = new Consultant();

            if (ConsultantID > 0)
            {
                Obj = GetConsultantList(ConsultantID).FirstOrDefault();
                Obj.ThematicAreaData = Common_SPU.fnGetThematicArea(0);
            }
            else
            {
                Obj.ThematicAreaData = Common_SPU.fnGetThematicArea(0);
                Obj.Code = "CC-00" + (Common_SPU.fnGetMaxID("consultant_id") + 1).ToString();
            }
            return Obj;

        }

        public List<ThematicArea> GetThematicAreaList(long ThematicAreaID)
        {
            string SQL = "";
            List<ThematicArea> List = new List<ThematicArea>();
            ThematicArea obj = new ThematicArea();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetThematicArea(ThematicAreaID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ThematicArea();
                    obj.ThematicAreaID = Convert.ToInt32(item["ID"]);
                    obj.Code = item["thematicarea_code"].ToString();
                    obj.Name = item["thematicareaName"].ToString();
                    obj.Description = item["Description"].ToString();
                    obj.Category = item["category"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetThematicAreaList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<Subgrant> GetSubgrantList(long SubgrantID)
        {
            string SQL = "";
            List<Subgrant> List = new List<Subgrant>();
            Subgrant obj = new Subgrant();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetsubgrant(SubgrantID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Subgrant();
                    obj.SubgrantID = Convert.ToInt32(item["ID"]);
                    obj.Name = item["subgrant_name"].ToString();
                    obj.Code = item["subgrant_code"].ToString();
                    obj.Address = item["address"].ToString();
                    obj.Mobile = item["mobile"].ToString();
                    obj.Location = item["location"].ToString();
                    obj.PanNo = item["panno"].ToString();
                    obj.Fora_No = item["fcra_no"].ToString();
                    obj.ShortName = item["short_name"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetSubgrantList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<SubgrantDetails> GetSubgrantDetailsList(long ID, string Doctype, long SubgrantID)
        {
            string SQL = "";
            List<SubgrantDetails> List = new List<SubgrantDetails>();
            SubgrantDetails obj = new SubgrantDetails();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetsubgrantdet(ID, Doctype, SubgrantID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new SubgrantDetails();
                    obj.SubgrantDetailsID = Convert.ToInt32(item["ID"]);
                    obj.SubgrantID = Convert.ToInt32(item["subgrant_id"]);
                    obj.Name = item["name"].ToString();
                    obj.Designation = item["design"].ToString();
                    obj.DocType = item["doc_type"].ToString();
                    obj.TypeID = Convert.ToInt32(item["type_id"]);
                    obj.AttachmentID = Convert.ToInt32(item["attchment_id"]);
                    obj.filename = item["filename"].ToString();
                    obj.ContentType = item["ContentType"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetSubgrantDetailsList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public SubgrantAdd GetSubgrantAdd(long SubgrantID)
        {
            SubgrantAdd obj = new SubgrantAdd();
            if (SubgrantID == 0)
            {
                Subgrant Subobj = new Subgrant();
                Subobj.Code = "Sub-" + (Common_SPU.fnGetMaxID("master_subgrant_id") + 1).ToString();
                obj.SubgrantList = Subobj;
            }
            else
            {
                obj.SubgrantList = GetSubgrantList(SubgrantID).FirstOrDefault();
                obj.AuthorizedPersonList = GetSubgrantDetailsList(0, "Authorized", SubgrantID);
                obj.AttachmentsList = GetSubgrantDetailsList(0, "Attachment", SubgrantID);
            }
            return obj;

        }

        public List<Announcement> GetAnnouncementList(long AnnouncementID)
        {
            List<Announcement> List = new List<Announcement>();
            Announcement obj = new Announcement();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetAnnouncement(AnnouncementID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Announcement();
                    obj.AnnouncementID = Convert.ToInt32(item["ID"]);
                    obj.HeadingName = item["heading_Name"].ToString();
                    obj.UserID = Convert.ToInt32(item["auser_id"]);
                    obj.Description = item["Description"].ToString();
                    obj.StarDate = Convert.ToDateTime(item["Start_Date"]).ToString(DateFormatE);
                    obj.ExpiryDate = Convert.ToDateTime(item["Expiry_Date"]).ToString(DateFormatE);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.LinkedLocationID = item["LinkedLocationID"].ToString().TrimEnd(',');
                    obj.AttachmentID = Convert.ToInt32(item["Attachment_ID"]);
                    obj.ContentType = item["Content_Type"].ToString();
                    obj.FileName = item["FileName"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAnnouncementList. The query was executed :", ex.ToString(), "spu_GetAnnouncement", "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<Holiday> GetHolidayList(long HolidayID)
        {
            List<Holiday> List = new List<Holiday>();
            Holiday obj = new Holiday();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetHoliday(HolidayID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Holiday();
                    obj.HolidayID = Convert.ToInt32(item["ID"]);
                    obj.Name = item["HOLIDAY_NAME"].ToString();
                    obj.LinkedLocationID = item["LinkedLocationID"].ToString().TrimEnd(',');
                    obj.ColorName = item["color_name"].ToString();
                    obj.ColorCode = item["color_code"].ToString();
                    obj.Date = Convert.ToDateTime(item["HOLIDAY_Date"]).ToString(DateFormatE);
                    obj.ShowDate = item["HOLIDAY_Date"].ToString();
                    obj.Remarks = item["Remarks"].ToString();
                    obj.FYId = Convert.ToInt64(item["FYId"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetHolidayList. The query was executed :", ex.ToString(), "spu_GetHoliday", "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<Question> GetQuestionList(long QuestionID)
        {
            string SQL = "";
            List<Question> List = new List<Question>();
            Question obj = new Question();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetQuestion(QuestionID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Question();
                    obj.QuestionID = Convert.ToInt32(item["ID"]);
                    obj.Quest = item["Question"].ToString();
                    obj.NoOfOption = Convert.ToInt32(item["noOfOption"]);
                    obj.Option1 = item["option1"].ToString();
                    obj.Option2 = item["option2"].ToString();
                    obj.Option3 = item["option3"].ToString();
                    obj.Option4 = item["option4"].ToString();
                    if (Convert.ToDateTime(item["start_date"]).Year != 1900)
                    {
                        obj.StartDate = Convert.ToDateTime(item["start_date"]).ToString(DateFormatE);

                    }
                    if (Convert.ToDateTime(item["end_date"]).Year != 1900)
                    {
                        obj.EndDate = Convert.ToDateTime(item["end_date"]).ToString(DateFormatE);
                    }

                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetQuestionList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }
        
       public List<BloodGroup> GetBloodGroupList(long id)
        {
            string SQL = "";
            List<BloodGroup> List = new List<BloodGroup>();
            BloodGroup obj = new BloodGroup();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetBloodGroupList(id);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BloodGroup();
                    obj.ID = Convert.ToInt32(item["ID"]);                    
                    obj.Blood_Group_Name = item["Blood_Group_Name"].ToString();                    
                    obj.Description = item["Description"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.CreatedAt = Convert.ToDateTime(item["createdat"]).ToString(DateFormatE);
                    obj.ModifiedAt = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatE);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetBloodGroupList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }
        public List<CompanyUpload> GetCompanyUpload(long id)
        {
            string SQL = "";
            List<CompanyUpload> List = new List<CompanyUpload>();
            CompanyUpload obj = new CompanyUpload();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetCompanyUploadList(id);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new CompanyUpload();
                    obj.CompanyUploadID = Convert.ToInt32(item["ID"]);
                    obj.Sno = Convert.ToInt32(item["srno"]);
                    obj.Description = item["descrip"].ToString();
                    obj.CategoryID = Convert.ToInt32(item["Cat_id"]);
                    obj.CategoryName = item["CategoryName"].ToString();
                    obj.FromDate = Convert.ToDateTime(item["From_Date"]).ToString(DateFormatE);
                    obj.ToDate = Convert.ToDateTime(item["To_Date"]).ToString(DateFormatE);
                    obj.Status = item["Status"].ToString();
                    obj.AttachmentID = Convert.ToInt32(item["Attachment_ID"]);
                    obj.FileName = item["FileName"].ToString();
                    obj.URL = item["URL"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetCompanyUpload. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<MembershipPeriod> GetMembershipPeriodList(long MembershipPeriodID)
        {
            string SQL = "";
            List<MembershipPeriod> List = new List<MembershipPeriod>();
            MembershipPeriod obj = new MembershipPeriod();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetMembershipPeriod(MembershipPeriodID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new MembershipPeriod();
                    obj.MembershipPeriodID = Convert.ToInt32(item["ID"]);
                    obj.Year = item["Year"].ToString();
                    obj.FromDate = Convert.ToDateTime(item["from_date"]).ToString(DateFormatE);
                    obj.ToDate = Convert.ToDateTime(item["to_date"]).ToString(DateFormatE);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMembershipPeriodList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<FinYear> GetFinYearList(long FinYearID)
        {
            string SQL = "";
            List<FinYear> List = new List<FinYear>();
            FinYear obj = new FinYear();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetFinYear(FinYearID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new FinYear();
                    obj.FinYearID = Convert.ToInt32(item["ID"]);
                    obj.Year = item["Year"].ToString();
                    if (item["from_date"] != null)
                    {
                        obj.FromDate = Convert.ToDateTime(item["from_date"]).ToString(DateFormatE);

                    }
                    if (item["to_date"] != null)
                    {
                        obj.ToDate = Convert.ToDateTime(item["to_date"]).ToString(DateFormatE);

                    }
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.isdeleted = Convert.ToInt32(item["isdeleted"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFinYearList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<LeaveMaster> GetLeaveMasterList(long leaveID)
        {
            List<LeaveMaster> List = new List<LeaveMaster>();
            LeaveMaster obj = new LeaveMaster();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetLeave(leaveID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new LeaveMaster();
                    obj.LeaveID = Convert.ToInt64(item["ID"]);
                    obj.LeaveName = item["leave_name"].ToString();
                    obj.LeaveType = Convert.ToInt32(item["leavetype"]);
                    obj.DueType = Convert.ToInt32(item["DueType"]);
                    obj.NOOfLeave = Convert.ToInt32(item["no_leave"]);
                    obj.ColorApplied = item["color_applied"].ToString();
                    obj.ColorcodeApplied = item["colorcode_applied"].ToString();
                    obj.ColorAppproved = item["color_appproved"].ToString();
                    obj.ColorcodeApproved = item["colorcode_approved"].ToString();
                    obj.CFLeave = item["CF_Leave"].ToString();
                    obj.CFLimit = item["CF_Limit"].ToString();
                    obj.status = Convert.ToInt32(item["status"]);
                    obj.NOOfLeave = Convert.ToInt32(item["no_leave"]);
                    obj.Leave_forword = Convert.ToInt32(item["Leave_forword"]);
                    obj.ShownLeave = Convert.ToInt32(item["shownleave"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.ShowInSummary = Convert.ToBoolean(item["ShowInSummary"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.ApplicableFor = item["ApplicableFor"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveList. The query was executed :", ex.ToString(), "spu_GetLeave", "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<Location> GetLocationList(long LocationID)
        {
            string SQL = "";
            List<Location> List = new List<Location>();
            Location obj = new Location();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetLocation(LocationID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Location();
                    obj.LocationID = Convert.ToInt32(item["ID"]);
                    obj.Name = item["location_name"].ToString();
                    obj.Description = item["Description"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLocationList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<Designation> GetDesignationList(long DesignationID)
        {
            List<Designation> List = new List<Designation>();
            Designation obj = new Designation();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetDesign(DesignationID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Designation();
                    obj.DesignationID = Convert.ToInt32(item["ID"]);
                    obj.DesignationName = item["design_name"].ToString();
                    obj.Description = item["Description"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDesignationList. The query was executed :", ex.ToString(), "spu_GetDesign", "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<Diem> GetDiemList(long DiemID)
        {
            string SQL = "";
            List<Diem> List = new List<Diem>();
            Diem obj = new Diem();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetdiem(DiemID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Diem();
                    obj.DiemID = Convert.ToInt32(item["ID"]);
                    obj.Status = item["status"].ToString();
                    obj.Category = item["Category"].ToString();
                    obj.PerDiemRate = Convert.ToDouble(item["perdiem_rate"]);
                    obj.HotelRate = Convert.ToDouble(item["hotel_rate"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDiemList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }


    
       
   


        public List<CostCenter> GetCostCenterList(long CostCenterID)
        {
            string SQL = "";
            List<CostCenter> List = new List<CostCenter>();
            CostCenter obj = new CostCenter();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetCostCenter(CostCenterID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new CostCenter();
                    obj.CostCenterID = Convert.ToInt32(item["ID"]);
                    obj.Code = item["costCenter_code"].ToString();
                    obj.Name = item["costcenter_name"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetCostCenterList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<SubLineItem> GetSubLineItemList(long SubLineItemID)
        {
            List<SubLineItem> List = new List<SubLineItem>();
            SubLineItem obj = new SubLineItem();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetSubLineItem(SubLineItemID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new SubLineItem();
                    obj.SubLineItemID = Convert.ToInt32(item["ID"]);
                    obj.Code = item["subLItem_Code"].ToString();
                    obj.CostCenterID = Convert.ToInt32(item["CostCenter_ID"]);
                    obj.CostCenterName = item["CostCenter_Name"].ToString();
                    obj.Name = item["SubLItem_Name"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetSubLineItemList. The query was executed :", ex.ToString(), "spu_GetSublineItem", "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<Vendor> GetVendorList(long VendorID)
        {
            string SQL = "";
            List<Vendor> List = new List<Vendor>();
            Vendor obj = new Vendor();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetVendorMaster(VendorID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Vendor();
                    obj.VendorID = Convert.ToInt32(item["ID"]);
                    obj.VendorCode = item["vendor_code"].ToString();
                    obj.VendorName = item["vendor_name"].ToString();
                    obj.Address = item["address"].ToString();
                    obj.Representative = item["representative"].ToString();
                    obj.ContactNo = item["contact_no"].ToString();
                    obj.LSTNo = item["lst_no"].ToString();
                    obj.CSTNo = item["cst_no"].ToString();
                    obj.PAN = item["pan_no"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetVendorList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<DepreciationRate> GetDepreciationRateList(long DepID)
        {
            string SQL = "";
            List<DepreciationRate> List = new List<DepreciationRate>();
            DepreciationRate obj = new DepreciationRate();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetDepreciationRate(DepID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new DepreciationRate();
                    obj.DEPID = Convert.ToInt32(item["ID"]);
                    obj.MainCode = item["main_code"].ToString();
                    obj.Description = item["descrip"].ToString();
                    obj.Method = item["method"].ToString();
                    obj.DepRate = Convert.ToDouble(item["depre_rate"]);
                    obj.DepRateDouble = Convert.ToDouble(item["depre_rate_double"]);
                    obj.DepRateTriple = Convert.ToDouble(item["depre_rate_triple"]);
                    obj.Multiple = Convert.ToInt32(item["iMultiple"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDepreciationRateList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<ProcurementCommittee> GetProcurementCommitteeList(long ID)
        {
            string SQL = "";
            List<ProcurementCommittee> List = new List<ProcurementCommittee>();
            ProcurementCommittee obj = new ProcurementCommittee();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetMasterProcurementCommittee(ID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ProcurementCommittee();
                    obj.ID = Convert.ToInt32(item["ID"]);
                    obj.EMP_ID = Convert.ToInt32(item["EMP_ID"]);
                    obj.emp_name = item["emp_name"].ToString();
                    obj.Effective_Date = Convert.ToDateTime(item["Effective_Date"]).ToString(DateFormatE);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDepreciationRateList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<FreeMeal> GetFreeMealList(long FreeMealID)
        {
            string SQL = "";
            List<FreeMeal> List = new List<FreeMeal>();
            FreeMeal obj = new FreeMeal();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetFreeMeal(FreeMealID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new FreeMeal();
                    obj.FreeMealID = Convert.ToInt32(item["ID"]);
                    obj.Percentage = Convert.ToDouble(item["freemeal_Percen"]);
                    obj.FreeMealName = item["freemeal"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFreeMealList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<Job> GetJobList(long JobID)
        {
            string SQL = "";
            List<Job> List = new List<Job>();
            Job obj = new Job();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetJob(JobID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Job();
                    obj.JobID = Convert.ToInt32(item["JobID"]);
                    obj.JobCode = item["JobCode"].ToString();
                    obj.Title = item["Title"].ToString();
                    obj.Skills = item["Skills"].ToString();
                    obj.Experience = item["Experience"].ToString();
                    obj.Description = item["Description"].ToString();
                    obj.QualificationDet = item["QualificationDet"].ToString();
                    obj.NoticePeriod = Convert.ToInt32(item["NoticePeriod"]);
                    obj.ProbationPeriod = Convert.ToInt32(item["ProbationPeriod"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetJobList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public ViewJobDetails GetJobDetails(long JobID)
        {
            ViewJobDetails obj = new ViewJobDetails();
            obj.Job = new Job();
            DataSet TempModuleDataSet = Common_SPU.fnGetJob(JobID);
            foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
            {
                obj.Job.JobID = Convert.ToInt32(item["JobID"]);
                obj.Job.JobCode = item["JobCode"].ToString();
                obj.Job.Title = item["Title"].ToString();
                obj.Job.Skills = item["Skills"].ToString();
                obj.Job.Experience = item["Experience"].ToString();
                obj.Job.Description = item["Description"].ToString();
                obj.Job.QualificationDet = item["QualificationDet"].ToString();
                obj.Job.NoticePeriod = Convert.ToInt32(item["NoticePeriod"]);
                obj.Job.ProbationPeriod = Convert.ToInt32(item["ProbationPeriod"]);
            }
            obj.JobRoundList = GetJobRound(JobID,0);
            return obj;
        }
        public List<JobRound> GetJobRound(long JobID,long JobDetailsID)
        {
            List<JobRound> List = new List<JobRound>();
            JobRound obj = new JobRound();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetJobDetail(JobID, JobDetailsID, "Round");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new JobRound();
                    obj.JobDetailsID = Convert.ToInt32(item["JobDetailID"]);
                    obj.srno = Convert.ToInt32(item["Srno"]);
                    obj.RoundName = item["RoundName"].ToString();
                    obj.IsNegotiationRound = Convert.ToBoolean(item["IsNegotiationRound"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.RoundDesc = item["RoundDesc"].ToString();
                    obj.JobMemberList = GetJobMember(JobID, obj.JobDetailsID);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetJobList. The query was executed :", ex.ToString(), "", "MasterModal", "MasterModal", "");
            }
            return List;

        }
        private List<JobMember> GetJobMember(long JobID, long JobDetailsID)
        {
            List<JobMember> List = new List<JobMember>();
            JobMember obj = new JobMember();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetJobDetail(JobID, JobDetailsID, "Member");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new JobMember();
                    obj.JobDetailsID = Convert.ToInt32(item["JobDetailID"]);
                    obj.srno = Convert.ToInt32(item["Srno"]);
                    obj.EMPID = Convert.ToInt32(item["EMPID"]);
                    obj.Email = item["Email"].ToString();
                    obj.Name = item["Name"].ToString();
                    obj.RoundMemberType = item["RoundMemberType"].ToString();
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetJobMember. The query was executed :", ex.ToString(), "", "MasterModal", "MasterModal", "");
            }
            return List;
        }


        public List<Helpdesk.LocationGroup> GetLocationGroupList(long LocationGroupID)
        {
            List<Helpdesk.LocationGroup> List = new List<Helpdesk.LocationGroup>();
            Helpdesk.LocationGroup obj = new Helpdesk.LocationGroup();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetLocationGroup(LocationGroupID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Helpdesk.LocationGroup();
                    obj.LocationGroupID = Convert.ToInt64(item["LocationGroupID"]);
                    obj.GroupName = item["GroupName"].ToString();
                    obj.Description = item["description"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.LocationIDs = (string.IsNullOrEmpty(item["LocationIDs"].ToString())?null:item["LocationIDs"].ToString().TrimEnd(',').Split(',').Select(int.Parse).ToArray());
                    

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLocationGroupList. The query was executed :", ex.ToString(), "fnGetLocationGroup", "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<Donor.List> GetDonorList(GetResponse modal)
        {
            List<Donor.List> result = new List<Donor.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDonorList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Donor.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDonorList. The query was executed :", ex.ToString(), "spu_GetDonor", "MasterModal", "MasterModal", "");
            }
            return result;
        }

        public Donor.Add GetDonor(GetResponse modal)
        {
            Donor.Add result = new Donor.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDonor", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Donor.Add>().FirstOrDefault();
                        if(result==null)
                        {
                            result = new Donor.Add();
                        }
                        if(!reader.IsConsumed)
                        {
                            result.DonorTypeList = reader.Read<DropDownList>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.LocalAddress = reader.Read<DonarAddress>().FirstOrDefault();
                            if(result.LocalAddress == null)
                            {
                                result.LocalAddress = new DonarAddress();
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.PermanentAddress = reader.Read<DonarAddress>().FirstOrDefault();
                            if (result.PermanentAddress == null)
                            {
                                result.PermanentAddress = new DonarAddress();
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DonorDetailsList = reader.Read<Donor.DonorDetails>().ToList();
                            if(result.DonorDetailsList==null || result.DonorDetailsList.Count==0)
                            {
                                List<Donor.DonorDetails> DList = new List<Donor.DonorDetails>();
                                DList.Add(new Donor.DonorDetails());
                                result.DonorDetailsList = DList;
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CountryList = reader.Read<DropDownList>().ToList();
                        }
                        result.StateList = new List<DropDownList>();
                        result.CityList = new List<DropDownList>();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDonor. The query was executed :", ex.ToString(), "spu_GetDonor", "MasterModal", "MasterModal", "");
            }
            return result;
        }

        public Donor.View GetDonorView(GetResponse modal)
        {
            Donor.View result = new Donor.View();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: modal.Doctype??"", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDonor", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<Donor.View>().FirstOrDefault();
                        
                        if (!reader.IsConsumed)
                        {
                            result.LocalAddress = reader.Read<DonarAddress>().FirstOrDefault();
                        
                        }
                        if (!reader.IsConsumed)
                        {
                            result.PermanentAddress = reader.Read<DonarAddress>().FirstOrDefault();
                        
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DonorDetailsList = reader.Read<Donor.DonorDetails>().ToList();
                        
                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDonor. The query was executed :", ex.ToString(), "spu_GetDonor", "MasterModal", "MasterModal", "");
            }
            return result;
        }

        public PostResponse fnSetDonor(Donor.Add model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDonor", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID ?? 0;
                        command.Parameters.Add("@SourceType", SqlDbType.VarChar).Value = model.SourceType.ToString() ?? "";
                        command.Parameters.Add("@DonorTypeID", SqlDbType.Int).Value = model.DonorTypeID;
                        command.Parameters.Add("@donor_name", SqlDbType.VarChar).Value = model.donor_name ?? "";
                        command.Parameters.Add("@address_id", SqlDbType.Int).Value = model.address_id;
                        command.Parameters.Add("@website", SqlDbType.VarChar).Value = model.website ?? "";
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();

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

        public PostResponse fnSetDonorDetail(Donor.DonorDetails model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDonorDetail", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@donor_id", SqlDbType.Int).Value = model.donor_id;
                        command.Parameters.Add("@srno", SqlDbType.Int).Value = model.Priority??0;
                        command.Parameters.Add("@person_name", SqlDbType.VarChar).Value = model.person_name ?? "";
                        command.Parameters.Add("@designation", SqlDbType.VarChar).Value = model.designation??"";
                        command.Parameters.Add("@location", SqlDbType.VarChar).Value = model.location ?? "";
                        command.Parameters.Add("@phone_no", SqlDbType.VarChar).Value = model.phone_no??"";
                        command.Parameters.Add("@mobile_no", SqlDbType.VarChar).Value = "";
                        command.Parameters.Add("@email", SqlDbType.VarChar).Value = model.email ?? "";
                        command.Parameters.Add("@Isdefault", SqlDbType.VarChar).Value = model.Isdefault ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();

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
        public CompanyConfig fnGetCompnayConfiguration(long id)
        {
            CompanyConfig obj = new CompanyConfig();
          
            DataSet TempModuleDataSet = Common_SPU.fnGetCompanyConfig(id);
            foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
            {
               
                obj.CompanyName= item["COMPANY_NAME"].ToString();
                obj.CompanyAdress = item["Address"].ToString();
                obj.CompanyLogo =item["Logo"].ToString();
              
            }
           
            return obj;
        }
        public  long fnUpdateCompanyConfiguration(long Id, string CompanyName, string CompanyAdress)
        {
            long success = 0;
            success = Common_SPU.fnSetCompanyConfig(Id,CompanyName,CompanyAdress);
            return success;


        }
        public PostResponse SetDeclarationMaster(DeclarationMaster model)
        {
            PostResponse result = new PostResponse();
            string SQL = "";
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDeclarationMaster", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@Declarationname", SqlDbType.VarChar).Value = model.Declarationname;
                        command.Parameters.Add("@Frequency", SqlDbType.VarChar).Value = model.Frequency;
                        command.Parameters.Add("@DueDate", SqlDbType.VarChar).Value = model.DueDate;
                        command.Parameters.Add("@AtOnboarding", SqlDbType.VarChar).Value = model.AtOnboarding;
                        command.Parameters.Add("@EmployeeType", SqlDbType.VarChar).Value = model.EmployeeType;
                        command.Parameters.Add("@AttachmentId", SqlDbType.Int).Value = model.AttachmentId;
                        command.Parameters.Add("@Content", SqlDbType.VarChar).Value = model.Content;
                        command.Parameters.Add("@Requiredremarksfield", SqlDbType.Int).Value = model.Requiredremarksfield;
                        command.Parameters.Add("@RequiredinhardCopy", SqlDbType.Int).Value = model.RequiredinhardCopy;
                        command.Parameters.Add("@Remark", SqlDbType.VarChar).Value = model.Remark;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
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

        public DeclarationMaster GetDeclarationMaster()
        {
            DeclarationMaster result = new DeclarationMaster();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@UserId", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("EMPID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDeclarationMasterList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result.listdeclarationMasters = reader.Read<DeclarationMaster>().ToList();
                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetDeclarationMasterList()", "MasterModal", "MasterModal", "");

            }
            return result;
        }
        public DeclarationMaster GetDeclarationMaster(int LoginID,int EMPID)
        {
            DeclarationMaster result = new DeclarationMaster();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@UserId", dbType: DbType.Int32, value: LoginID, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDeclarationMasterList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result.listdeclarationMasters = reader.Read<DeclarationMaster>().ToList();
                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetDeclarationMasterList()", "MasterModal", "MasterModal", "");

            }
            return result;
        }
        public DeclarationMaster GetRecordDeclarationMaster(long Id)
        {
            DeclarationMaster result = new DeclarationMaster();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: Id, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDeclarationMaster", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result = reader.Read<DeclarationMaster>().FirstOrDefault();
                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetDeclarationMaster()", "MasterModal", "MasterModal", "");

            }
            return result;
        }
        public List<MPRReports.SubSection> GetMPR_Reports_SubHeader(string MPRSID)
        {


            List<MPRReports.SubSection> SubSectionList = new List<MPRReports.SubSection>();
            DataSet Tset = Common_SPU.GetMPR_Reports_SubHeader(MPRSID);

            foreach (DataRow item in Tset.Tables[0].Rows)
            {
                var Pobj = new MPRReports.SubSection();
                Pobj.MPRSubSID = Convert.ToInt64(item["MPRSubSID"].ToString());
                Pobj.SubSecName = Convert.ToString(item["SubSecName"].ToString());
                SubSectionList.Add(Pobj);
            }

            return SubSectionList;

        }

        public PostResponse fnsetAddOfficeListing(OfficeListing model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetOffceListing", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@BranchOffce", SqlDbType.VarChar).Value = model.BranchOffice;
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = model.Description;
                        command.Parameters.Add("@LocationIds", SqlDbType.VarChar).Value = model.LOcId;
                        command.Parameters.Add("@Admin", SqlDbType.Int).Value = model.Admin;
                        command.Parameters.Add("@IT", SqlDbType.VarChar).Value = model.IT;
                        command.Parameters.Add("@HR", SqlDbType.VarChar).Value = model.HR;
                        command.Parameters.Add("@Finance", SqlDbType.VarChar).Value = model.Finance;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();

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

        public List<OfficeListing> GetOfficeListing(long ID)
        {
            string SQL = "";
            List<OfficeListing> List = new List<OfficeListing>();
            OfficeListing obj = new OfficeListing();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetOfficeList(ID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new OfficeListing();
                    obj.Id = Convert.ToInt32(item["Id"]);
                    obj.BranchOffice = item["BranchOffice"].ToString();
                    obj.Description = item["Description"].ToString();
                    obj.LOcId = item["WorkLocation"].ToString();
                    obj.LocationIds = item["LocationId"].ToString();
                    obj.Admin = Convert.ToInt64(item["Admin"].ToString());
                    obj.IT = Convert.ToInt64(item["IT"].ToString());
                    obj.HR = Convert.ToInt64(item["HR"]);
                    obj.Finance = Convert.ToInt64(item["Finance"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetJobList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<Location> GetLocationOfficeList(long LocationID)
        {
            string SQL = "";
            List<Location> List = new List<Location>();
            Location obj = new Location();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetofficeLocation(LocationID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Location();
                    obj.LocationID = Convert.ToInt32(item["ID"]);
                    obj.Name = item["location_name"].ToString();
                    obj.Description = item["Description"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLocationList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }
        public List<Location> GetLocationOfficeListnew(long LocationID)
        {
            string SQL = "";
            List<Location> List = new List<Location>();
            Location obj = new Location();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetofficeLocationNew(LocationID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Location();
                    obj.LocationID = Convert.ToInt32(item["ID"]);
                    obj.Name = item["location_name"].ToString();
                    obj.Description = item["Description"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLocationList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }
        public PostResponse fnsetBloodGroupListing(BloodGroup model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBloodGroup", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID;
                        command.Parameters.Add("@Blood_Group_Name", SqlDbType.VarChar).Value = model.Blood_Group_Name;
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = model.Description;                       
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();

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






