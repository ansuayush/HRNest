using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMaster
{
    public class PrincipalModal: IPrincipalHelper
    {

        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public List<Principal> GetPrincipalList()
        {
            string SQL = "";
            List<Principal> List = new List<Principal>();
            Principal obj = new Principal();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPrincipalList();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Principal();
                    obj.ID = Convert.ToInt64(item["ID"]);
                    obj.proj_name = item["proj_name"].ToString();
                    obj.ProjectId = Convert.ToInt64(item["ProjectId"]);
                    obj.costcenter_name = item["costcenter_name"].ToString();
                    obj.Approved = Convert.ToInt64(item["Approved"]);
                    obj.travel_id = Convert.ToInt64(item["travel_id"]);
                    obj.traveller_name = item["traveller_name"].ToString();
                    obj.TravelDetails = item["TravelDetails"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetProjectLinePrincipalList. The query was executed :", ex.ToString(), SQL, "PrincipalModal", "PrincipalModal", "");
            }
            return List;

        }
    }
}