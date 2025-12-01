using Dapper;
using Mitr.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mitr.DAL
{
    public class ClsCommon
    {

        public static string IPAddress { get;set; }
        public static string MultipleLoginID = string.Empty;
        public static string ConnectionString { get; set; }
        public static string connectionstring()
        {
            return ClsCommon.ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        }
        public static bool LogError(string ErrDescription, string SystemException = "", string ActiveFunction = "", string ActiveForm = "", string ActiveModule = "", string LogMessage = "")
        {
            try
            {
                //string SQL = null;
                Common_SPU.fnSetErrorLog(ErrDescription, SystemException, ActiveFunction, ActiveForm, ActiveModule);
                HttpContext.Current.Response.Write(LogMessage.Trim());
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(LogMessage.Trim() + "<br><br> & " + SystemException.Trim());
                return false;
            }

        }

        public static string GetIPAddress()
        {
            return ClsCommon.IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        public static string EnsureString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                str = "";
            }
            return str.Replace("'", "''").Replace("\"", "" + "").Trim();
        }

        public static bool CreateConfigJson()
        {
            bool ret = false;
            try
            {
                DataSet Ds = Common_SPU.fnGetConfigSetting(0);
                var jsonString = ConvertDataTableToJson(Ds.Tables[0]);
                if (jsonString != null)
                {
                    if (System.IO.File.Exists(clsApplicationSetting.GetPhysicalPath("json") + "/Config.json"))
                    {
                        System.IO.File.Delete(clsApplicationSetting.GetPhysicalPath("json") + "/Config.json");
                    }
                    System.IO.File.WriteAllText(clsApplicationSetting.GetPhysicalPath("json") + "/Config.json", jsonString);
                }

            }
            catch (Exception ex)
            {
                LogError("Error during CreateConfigJson. The query was executed :", ex.ToString(), "", "ClsCommon", "ClsCommon", "");
            }
            return ret;
        }
        public static string ConvertDataTableToJson(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
        public static List<ConfigSetting> GetConfigJson()
        {
            List<ConfigSetting> List = new List<ConfigSetting>();
            try
            {
                if (!System.IO.File.Exists(clsApplicationSetting.GetPhysicalPath("json") + "/config.json"))
                {
                    CreateConfigJson();
                }
                string file = clsApplicationSetting.GetPhysicalPath("json") + "/config.json";
                string Json = System.IO.File.ReadAllText(file);
                List = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ConfigSetting>>(Json);
                //JavaScriptSerializer ser = new JavaScriptSerializer();
                //List = ser.Deserialize<List<MenuJson>>(Json);
            }
            catch (Exception ex)
            {
                LogError("Error during GetConfigJson. The query was executed :", ex.ToString(), "", "ClsCommon", "ClsCommon", "");
            }
            return List;
        }
        public static List<DropdownModel> GetDropDownList(GetResponseModel modal)
        {
            List<DropdownModel> result = new List<DropdownModel>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Doctype", dbType: DbType.String, value: EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDropDownList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<DropdownModel>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                LogError("Error during GetDropDownList. The query was executed :", ex.ToString(), "spu_GetDropDownList()", "Common_SPU", "Common_SPU");
            }
            return result;
        }
        public static List<DropdownModel> GR_GetDropDownList(GetResponseModel modal)
        {
            List<DropdownModel> result = new List<DropdownModel>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Doctype", dbType: DbType.String, value: EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    param.Add("@ID", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@AdditionalID", dbType: DbType.Int32, value: modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@AdditionalID1", dbType: DbType.Int32, value: modal.AdditionalID1, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDropDownListGR", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<DropdownModel>().ToList();

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                LogError("Error during GR_GetDropDownList. The query was executed :", ex.ToString(), "spu_GetDropDownListGR()", "Common_SPU", "Common_SPU");

            }
            return result;
        }
        public static List<DropdownModel> GetDropDownListFilterLevelName(GetResponseModel modal)
        {
            List<DropdownModel> result = new List<DropdownModel>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Doctype", dbType: DbType.String, value: EnsureString(modal.Doctype), direction: ParameterDirection.Input);
                    param.Add("@LOGINID", dbType: DbType.String, value: modal.MultipleLoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDropDownListFilterLevelNameGR", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<DropdownModel>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                LogError("Error during GetDropDownListFilterLevelName. The query was executed :", ex.ToString(), "spu_GetDropDownListFilterLevelNameGR()", "Common_SPU", "Common_SPU");


            }
            return result;
        }
    }
}
