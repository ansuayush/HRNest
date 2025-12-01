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
    public class MPRModal : IMPRHelper
    {
        public string DateFormat = "dd-MMM-yyyy", DateFormatC = "dd-MMM-yy hh:mm:ss tt",DateFormatE = "yyyy-MM-dd";
        public MPRModal()
        {
            var d = clsApplicationSetting.GetConfigValue("DateFormat");
            var dC = clsApplicationSetting.GetConfigValue("DateFormatC");
            DateFormat = (!string.IsNullOrEmpty(d) ? d : DateFormat);
            DateFormatC = (!string.IsNullOrEmpty(dC) ? dC : DateFormatC);
        }

        public List<MPR.Section.SecList> GetMPRSecList(long MPRSID, string IsActive = "0,1")
        {
            List<MPR.Section.SecList> List = new List<MPR.Section.SecList>();
            MPR.Section.SecList obj = new MPR.Section.SecList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetMPRSec(MPRSID, IsActive);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new MPR.Section.SecList();
                    obj.RowNum = Convert.ToInt64(item["RowNum"].ToString());
                    obj.MPRSID = Convert.ToInt64(item["MPRSID"].ToString());
                    obj.SecName = item["SecName"].ToString();
                    obj.SecDesc = item["SecDesc"].ToString();
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMPRSecList. The query was executed :", ex.ToString(), "fnGetMPRSec", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public MPR.Section.SecAdd GetMPRSec(long MPRSID)
        {
            MPR.Section.SecAdd obj = new MPR.Section.SecAdd();

            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetMPRSec(MPRSID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj.MPRSID = Convert.ToInt64(item["MPRSID"].ToString());
                    obj.SecName = item["SecName"].ToString();
                    obj.SecDesc = item["SecDesc"].ToString();
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMPRSec. The query was executed :", ex.ToString(), "fnGetMPRSec", "ActivityModal", "ActivityModal", "");
            }
            return obj;
        }


        public List<MPR.SubSection.SubSecList> GetMPRSubSecList(long MPRSubSID, string IsActive = "0,1")
        {
            List<MPR.SubSection.SubSecList> List = new List<MPR.SubSection.SubSecList>();
            MPR.SubSection.SubSecList obj = new MPR.SubSection.SubSecList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetMPRSubSec(MPRSubSID, IsActive);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new MPR.SubSection.SubSecList();
                    obj.RowNum = Convert.ToInt64(item["RowNum"].ToString());
                    obj.MPRSubSID = Convert.ToInt64(item["MPRSubSID"].ToString());
                    obj.SubSecName = item["SubSecName"].ToString();

                    obj.MPRSID = Convert.ToInt64(item["MPRSID"].ToString());
                    obj.SecName = item["SecName"].ToString();
                    obj.NoOfCol = Convert.ToInt32(item["NoOfCol"]);

                    obj.ColText1 = item["ColText1"].ToString();
                    obj.ColDataType1 = item["ColDataType1"].ToString();
                    obj.ColSuffix1 = item["ColSuffix1"].ToString();

                    obj.ColText2 = item["ColText2"].ToString();
                    obj.ColDataType2 = item["ColDataType2"].ToString();
                    obj.ColSuffix2 = item["ColSuffix2"].ToString();

                    obj.ColText3 = item["ColText3"].ToString();
                    obj.ColDataType3 = item["ColDataType3"].ToString();
                    obj.ColSuffix3 = item["ColSuffix3"].ToString();

                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMPRSecList. The query was executed :", ex.ToString(), "fnGetMPRSec", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public MPR.SubSection.SubSecAdd GetMPRSubSec(long MPRSubSID)
        {
            MPR.SubSection.SubSecAdd obj = new MPR.SubSection.SubSecAdd();

            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetMPRSubSec(MPRSubSID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj.MPRSubSID = Convert.ToInt64(item["MPRSubSID"].ToString());
                    obj.SubSecName = item["SubSecName"].ToString();
                    obj.SecName = item["SecName"].ToString();

                    obj.MPRSID = Convert.ToInt64(item["MPRSID"].ToString());
                    obj.NoOfCol = Convert.ToInt32(item["NoOfCol"]);

                    obj.ColText1 = item["ColText1"].ToString();
                    obj.ColDataType1 = item["ColDataType1"].ToString();
                    obj.ColSuffix1 = item["ColSuffix1"].ToString();

                    obj.ColText2 = item["ColText2"].ToString();
                    obj.ColDataType2 = item["ColDataType2"].ToString();
                    obj.ColSuffix2 = item["ColSuffix2"].ToString();

                    obj.ColText3 = item["ColText3"].ToString();
                    obj.ColDataType3 = item["ColDataType3"].ToString();
                    obj.ColSuffix3 = item["ColSuffix3"].ToString();

                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMPRSubSec. The query was executed :", ex.ToString(), "GetMPRSubSec", "ActivityModal", "ActivityModal", "");
            }
            return obj;
        }

        public List<MPR.Indicator.IndicatorList> GetMPRIndicatorList(long IndicatorID, string IsActive = "0,1")
        {
            List<MPR.Indicator.IndicatorList> List = new List<MPR.Indicator.IndicatorList>();
            MPR.Indicator.IndicatorList obj = new MPR.Indicator.IndicatorList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetMPRIndicator(IndicatorID, IsActive);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new MPR.Indicator.IndicatorList();
                    obj.RowNum = Convert.ToInt64(item["RowNum"].ToString());
                    obj.IndicatorID = Convert.ToInt64(item["IndicatorID"].ToString());
                    obj.MPRSubSecID = Convert.ToInt64(item["MPRSubSecID"].ToString());
                    obj.SubSecName = item["SubSecName"].ToString();
                    obj.IndicatorName = item["IndicatorName"].ToString();
                    obj.SecName = item["SecName"].ToString();
                    obj.AnswerIs = item["AnswerIs"].ToString();
                    obj.NoOfCol = Convert.ToInt32(item["NoOfCol"]);
                    obj.ColText1 = item["ColText1"].ToString();
                    obj.ColDataType1 = item["ColDataType1"].ToString();
                    obj.ColSuffix1 = item["ColSuffix1"].ToString();
                    obj.ColText2 = item["ColText2"].ToString();
                    obj.ColDataType2 = item["ColDataType2"].ToString();
                    obj.ColSuffix2 = item["ColSuffix2"].ToString();
                    obj.ColText3 = item["ColText3"].ToString();
                    obj.ColDataType3 = item["ColDataType3"].ToString();
                    obj.ColSuffix3 = item["ColSuffix3"].ToString();
                    obj.IsOrganisational = item["IsOrganisational"].ToString();
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMPRIndicatorList. The query was executed :", ex.ToString(), "fnGetMPRSec", "ActivityModal", "ActivityModal", "");
            }
            return List;
        }

        public MPR.Indicator.IndicatorAdd GetMPRIndicator(long IndicatorID)
        {
            MPR.Indicator.IndicatorAdd obj = new MPR.Indicator.IndicatorAdd();

            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetMPRIndicator(IndicatorID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj.IndicatorID = Convert.ToInt64(item["IndicatorID"].ToString());
                    obj.MPRSubSecID = Convert.ToInt64(item["MPRSubSecID"].ToString());
                    obj.IndicatorName = item["IndicatorName"].ToString();

                    obj.SubSecName = item["SubSecName"].ToString();
                    obj.SecName = item["SecName"].ToString();
                    obj.AnswerIs = item["AnswerIs"].ToString();
                    obj.NoOfCol = Convert.ToInt32(item["NoOfCol"]);
                    obj.ColText1 = item["ColText1"].ToString();
                    obj.ColDataType1 = item["ColDataType1"].ToString();
                    obj.ColSuffix1 = item["ColSuffix1"].ToString();
                    obj.ColText2 = item["ColText2"].ToString();
                    obj.ColDataType2 = item["ColDataType2"].ToString();
                    obj.ColSuffix2 = item["ColSuffix2"].ToString();
                    obj.ColText3 = item["ColText3"].ToString();
                    obj.ColDataType3 = item["ColDataType3"].ToString();
                    obj.ColSuffix3 = item["ColSuffix3"].ToString();
                    obj.IsOrganisational = item["IsOrganisational"].ToString();
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMPRIndicator. The query was executed :", ex.ToString(), "GetMPRIndicator", "ActivityModal", "ActivityModal", "");
            }
            return obj;
        }

        private List<MPR.StateList> GetStateList(DataTable dt)
        {
            List<MPR.StateList> List = new List<MPR.StateList>();
            MPR.StateList obj = new MPR.StateList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new MPR.StateList();
                    obj.StateID = Convert.ToInt32(item["ID"]);
                    obj.StateName = item["State_Name"].ToString();
                    List.Add(obj);
                }
            }
            return List;

        }
        private List<MPR.UserList> GetUserList(DataTable dt)
        {
            List<MPR.UserList> List = new List<MPR.UserList>();
            MPR.UserList obj = new MPR.UserList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new MPR.UserList();
                    obj.LoginID = Convert.ToInt32(item["ID"]);
                    obj.UserName = item["EMPNameCode"].ToString().Replace("( )", "");
                    List.Add(obj);
                }
            }
            return List;

        }

        private List<MPR.CreateMPRDetails> GetCreateMPRDetails(DataTable dt)
        {
            List<MPR.CreateMPRDetails> List = new List<MPR.CreateMPRDetails>();
            MPR.CreateMPRDetails obj = new MPR.CreateMPRDetails();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new MPR.CreateMPRDetails();
                    obj.IndicatorID = Convert.ToInt64(item["IndicatorID"]);
                    obj.IndicatorName = item["IndicatorName"].ToString();
                    obj.MPRSubSecID = Convert.ToInt64(item["MPRSubSecID"]);
                    obj.MPRSID = Convert.ToInt64(item["MPRSID"]);
                    obj.SubSecName = item["SubSecName"].ToString();
                    obj.SecName = item["SecName"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"]);

                    obj.Executor1ID = Convert.ToInt64(item["Executor1ID"]);
                    obj.Executor2ID = Convert.ToInt64(item["Executor2ID"]);
                    obj.ApproverID = Convert.ToInt64(item["ApproverID"]);

                    obj.Executor1Name = item["Executor1Name"].ToString();
                    obj.Executor2Name = item["Executor2Name"].ToString();
                    obj.ApproverName = item["ApproverName"].ToString();

                    List.Add(obj);
                }
            }
            return List;
        }
        private List<MPR.ProjectList> GetProjectList(DataTable dt)
        {
            List<MPR.ProjectList> List = new List<MPR.ProjectList>();
            MPR.ProjectList obj = new MPR.ProjectList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new MPR.ProjectList();
                    obj.ProjectID = Convert.ToInt32(item["ID"]);
                    obj.ProjectName = item["ProjectName"].ToString();
                    List.Add(obj);
                }
            }
            return List;

        }

        public MPR.CreateMPR GetCreateMPR(long MPRID)
        {
            MPR.CreateMPR obj = new MPR.CreateMPR();
            DataSet Tset = Common_SPU.fnGetMPR(MPRID, "0,1", "create");
            DataTable T0 = Tset.Tables[0];

            obj.MPRID = Convert.ToInt64(T0.Rows[0]["MPRID"]);
            obj.MPRCode = T0.Rows[0]["MPRCode"].ToString();
            obj.Version = Convert.ToInt32(T0.Rows[0]["Version"]);
            obj.MPRDate = DateTime.Now.Date.ToString(DateFormat);
            if (obj.MPRID != 0)
            {
                obj.MPRCode = T0.Rows[0]["MPRCode"].ToString();
                obj.MPRName = T0.Rows[0]["MPRName"].ToString();
                obj.Version = Convert.ToInt32(T0.Rows[0]["Version"]);
                obj.MPRDate = Convert.ToDateTime(T0.Rows[0]["MPRDate"]).ToString(DateFormat);
                obj.ProjectID = Convert.ToInt64(T0.Rows[0]["ProjectID"]);
                obj.StateID = Convert.ToInt64(T0.Rows[0]["StateID"]);
                obj.ApproverLevel1 = Convert.ToInt64(T0.Rows[0]["ApproverLevel1"]);
                obj.ApproverLevel2 = Convert.ToInt64(T0.Rows[0]["ApproverLevel2"]);
                obj.InitiateDate = (Convert.ToDateTime(T0.Rows[0]["InitiateDate"]).Year > 1900 ? Convert.ToDateTime(T0.Rows[0]["InitiateDate"]).ToString(DateFormatE) : "");

            }

            obj.CreateMPRDetails = GetCreateMPRDetails(Tset.Tables[1]);
            obj.UserList = GetUserList(Tset.Tables[2]);
            obj.ProjectList = GetProjectList(Tset.Tables[3]);
            obj.StateList = GetStateList(Tset.Tables[4]);
            return obj;
        }

        public List<MPR.MPRList> GetMPRList()
        {
            List<MPR.MPRList> List = new List<MPR.MPRList>();
            MPR.MPRList obj = new MPR.MPRList();
            DataSet Tset = Common_SPU.fnGetMPR(0, "0,1", "");
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new MPR.MPRList();
                    obj.RowNum = Convert.ToInt32(item["RowNum"]);
                    obj.MPRID = Convert.ToInt64(item["MPRID"]);
                    obj.MPRCode = item["MPRCode"].ToString();
                    obj.MPRName = item["MPRName"].ToString();
                    obj.State = item["StateName"].ToString();
                    obj.Project = item["ProjectName"].ToString();
                    obj.Version = item["Version"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    List.Add(obj);
                }
            }
            return List;
        }

        public List<MPR.Targets.TargetsList> GetMPRTargetsList(long FinID, string TargetType)
        {
            List<MPR.Targets.TargetsList> List = new List<MPR.Targets.TargetsList>();
            MPR.Targets.TargetsList obj = new MPR.Targets.TargetsList();
            DataSet Tset = Common_SPU.fnGetMPRTargetList(TargetType, FinID);
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new MPR.Targets.TargetsList();
                    obj.RowNum = Convert.ToInt64(item["RowNum"]);
                    obj.FinID = Convert.ToInt64(item["FinID"]);
                    obj.ProjectID = Convert.ToInt64(item["ProjectID"]);
                    obj.Year = item["Year"].ToString();
                    obj.Proj_Name = item["Proj_Name"].ToString();
                    obj.Projref_No = item["Projref_No"].ToString();
                    obj.MPRTargetID = Convert.ToInt64(item["MPRTargetID"]);
                    List.Add(obj);
                }
            }
            return List;
        }

        public List<MPR.FinYear> GetFinYearList()
        {
            List<MPR.FinYear> List = new List<MPR.FinYear>();
            MPR.FinYear obj = new MPR.FinYear();
            DataSet Tset = Common_SPU.fnGetFinYear(0, "1");
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new MPR.FinYear();
                    obj.FInID = Convert.ToInt64(item["ID"]);
                    obj.Year = item["Year"].ToString();
                    List.Add(obj);
                }
            }
            return List;
        }

      
        public MPR.Targets.addTargetSetting AddTargetSetting(long finId, long ProjectID)
        {
            MPR.Targets.addTargetSetting obj = new MPR.Targets.addTargetSetting();
            DataSet Tset = Common_SPU.fnGetMPRTargetDet(ProjectID, finId);
            if (Tset.Tables[0].Rows.Count > 0)
            {
                obj.ProjectName = Tset.Tables[0].Rows[0]["proj_name"].ToString();
                obj.Year = Tset.Tables[0].Rows[0]["year"].ToString();
                obj.Quarter = Tset.Tables[0].Rows[0]["Quarter"].ToString();
            }
            obj.TargetSetting = GetTargetSetting(Tset.Tables[1]);
            if (string.IsNullOrEmpty(obj.Quarter))
            {
                obj.IsButtonShow = true;
            }
            else
            {
                int Quarter = 0;
                int.TryParse(obj.Quarter, out Quarter);

                if (clsApplicationSetting.GetCurrentQuarter() == Quarter)
                {
                    obj.IsButtonShow = true;
                }
            }
            return obj;

        }
        private List<MPR.Targets.TargetSetting> GetTargetSetting(DataTable Dt)
        {
            List<MPR.Targets.TargetSetting> List = new List<MPR.Targets.TargetSetting>();
            MPR.Targets.TargetSetting obj = new MPR.Targets.TargetSetting();
            foreach (DataRow item in Dt.Rows)
            {
                obj = new MPR.Targets.TargetSetting();
                obj.RowNum = Convert.ToInt32(item["RowNum"]);
                obj.IndicatorID = Convert.ToInt64(item["IndicatorID"]);
                obj.IndicatorName = item["IndicatorName"].ToString();
                obj.MPRSubSecID = Convert.ToInt64(item["MPRSubSecID"]);
                obj.MPRSID = Convert.ToInt64(item["MPRSID"]);
                obj.SubSecName = item["SubSecName"].ToString();
                obj.SecName = item["SecName"].ToString();

                obj.NoOfCol = Convert.ToInt32(item["NoOfCol"]);
                obj.ColText1 = item["ColText1"].ToString();
                obj.ColDataType1 = item["ColDataType1"].ToString();
                obj.ColSuffix1 = item["ColSuffix1"].ToString();
                obj.ColVal1 = item["ColVal1"].ToString();

                obj.ColText2 = item["ColText2"].ToString();
                obj.ColDataType2 = item["ColDataType2"].ToString();
                obj.ColSuffix2 = item["ColSuffix2"].ToString();
                obj.ColVal2 = item["ColVal2"].ToString();

                obj.ColText3 = item["ColText3"].ToString();
                obj.ColDataType3 = item["ColDataType3"].ToString();
                obj.ColSuffix3 = item["ColSuffix3"].ToString();
                obj.ColVal3 = item["ColVal3"].ToString();

                List.Add(obj);
            }

            return List;
        }


        public List<SMPR.List> GetSMPRList(string Approve, int Month, int Year, string ListType = "")
        {
            List<SMPR.List> List = new List<SMPR.List>();
            SMPR.List obj = new SMPR.List();
            DataSet Tset = Common_SPU.fnGetSMPRList(Approve, Month, Year, ListType);
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new SMPR.List();
                    obj.SMPRID = Convert.ToInt64(item["SMPRID"]);
                    obj.MPRID = Convert.ToInt64(item["MPRID"]);
                    obj.MPRCode = item["MPRCode"].ToString();
                    obj.RowNum = Convert.ToInt32(item["RowNum"]);
                    obj.MPRName = item["MPRName"].ToString();
                    obj.Version = Convert.ToInt32(item["Version"]);
                    obj.InitiateDate = item["InitiateDate"].ToString();
                    obj.Month = Convert.ToInt32(item["Month"]);
                    obj.Year = Convert.ToInt32(item["Year"]);
                    obj.MonthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(obj.Month);
                    obj.ProjectName = item["ProjectName"].ToString();
                    obj.StateName = item["StateName"].ToString();
                    obj.LevelApproved = Convert.ToInt32(item["Approved"]);
                    obj.Submitted = Convert.ToInt32(item["Submitted"]);
                    obj.DueOn = Convert.ToDateTime(item["DueOn"]).ToString(DateFormat);
                    List.Add(obj);
                }
            }
            return List;
        }

        public List<SMPR.SMPRDet> GetSMPRDetList(long SMPRID)
        {


            List<SMPR.SMPRDet> List = new List<SMPR.SMPRDet>();
            SMPR.SMPRDet obj = new SMPR.SMPRDet();
            DataSet Tset = Common_SPU.fnGetSMPRdet(SMPRID);
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new SMPR.SMPRDet();
                    obj.RowNum = Convert.ToInt32(item["RowNum"]);
                    obj.SMPRDetID = Convert.ToInt64(item["SMPRDetID"]);
                    obj.SMPRID = Convert.ToInt64(item["SMPRID"]);
                    obj.Month = Convert.ToInt32(item["Month"]);
                    obj.Year = Convert.ToInt32(item["Year"]);
                    obj.MonthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(obj.Month);
                    obj.Lock = Convert.ToInt32(item["Lock"]);
                    obj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                    obj.ProjectName = item["ProjectName"].ToString();
                    obj.StateID = Convert.ToInt32(item["StateID"]);
                    obj.StateName = item["StateName"].ToString();
                    obj.Version = Convert.ToInt32(item["Version"]);
                    obj.MPRDetID = Convert.ToInt64(item["MPRDetID"]);
                    obj.MPRIndicatorID = Convert.ToInt64(item["MPRIndicatorID"]);
                    obj.IndicatorName = item["IndicatorName"].ToString();
                    obj.MPRSubSecID = Convert.ToInt64(item["MPRSubSecID"]);
                    obj.MPRSID = Convert.ToInt64(item["MPRSID"]);
                    obj.SecName = item["SecName"].ToString();
                    obj.SubSecName = item["SubSecName"].ToString();
                    obj.AnswerIs = item["AnswerIs"].ToString();
                    obj.IsOrganisational = item["IsOrganisational"].ToString();
                    obj.NoOfCol = Convert.ToInt32(item["NoOfCol"]);
                    obj.ColText1 = item["ColText1"].ToString();
                    obj.ColDataType1 = item["ColDataType1"].ToString();
                    obj.ColSuffix1 = item["ColSuffix1"].ToString();
                    obj.ColText2 = item["ColText2"].ToString();
                    obj.ColDataType2 = item["ColDataType2"].ToString();
                    obj.ColSuffix2 = item["ColSuffix2"].ToString();
                    obj.ColText3 = item["ColText3"].ToString();
                    obj.ColDataType3 = item["ColDataType3"].ToString();
                    obj.ColSuffix3 = item["ColSuffix3"].ToString();
                    obj.DueOn = Convert.ToDateTime(item["DueOn"]).ToString(DateFormat);
                    obj.ExecuterVal1 = item["ExecuterVal1"].ToString();
                    obj.ExecuterVal2 = item["ExecuterVal2"].ToString();
                    obj.ExecuterVal3 = item["ExecuterVal3"].ToString();
                    obj.LevelApproved = Convert.ToInt32(item["LevelApproved"]);

                    obj.Submitted = Convert.ToInt32(item["Submitted"]);
                    obj.Approved = Convert.ToInt32(item["Approved"]);
                    obj.SMPRSubmitted = Convert.ToInt32(item["SMPRSubmitted"]);
                    
                    obj.Executor1ID = Convert.ToInt64(item["Executor1ID"]);
                    obj.Executor1Name = item["Executor1Name"].ToString();
                    obj.Executor1Email = item["Executor1Email"].ToString();

                    obj.Executor2ID = Convert.ToInt64(item["Executor2ID"]);
                    obj.Executor2Name = item["Executor2Name"].ToString();
                    obj.Executor2Email = item["Executor2Email"].ToString();

                    obj.ApproverID = Convert.ToInt64(item["ApproverID"]);
                    obj.ApproverName = item["ApproverName"].ToString();
                    obj.ApproverEmail = item["ApproverEmail"].ToString();

                    obj.ApproverLevel1 = Convert.ToInt64(item["ApproverLevel1"]);
                    obj.ApproverLevel1Name = item["ApproverLevel1Name"].ToString();
                    obj.ApproverLevel1Email = item["ApproverLevel1Email"].ToString();

                    obj.ApproverLevel2 = Convert.ToInt64(item["ApproverLevel2"]);
                    obj.ApproverLevel2Name = item["ApproverLeve12Name"].ToString();
                    obj.ApproverLevel2Email = item["ApproverLevel2Email"].ToString();

                    obj.SectionPriority = Convert.ToInt32(item["SectionPriority"]);
                    obj.SubSectionPriority = Convert.ToInt32(item["SubSectionPriority"]);
                    obj.indicatorPriority = Convert.ToInt32(item["indicatorPriority"]);

                    obj.FilledBy = item["FilledBy"].ToString();
                    obj.Filleddat = (Convert.ToDateTime(item["Filleddat"]).Year > 1900 ? Convert.ToDateTime(item["Filleddat"]).ToString(DateFormatC) : "");
                    obj.FilledIP = item["FilledIP"].ToString();

                    List.Add(obj);
                }
            }

            return List;
        }
        private List<SMPR.SMPRDetEntry> GetEntrySMPRDetList(long SMPRID)
        {
            List<SMPR.SMPRDetEntry> List = new List<SMPR.SMPRDetEntry>();
            SMPR.SMPRDetEntry obj = new SMPR.SMPRDetEntry();
            DataSet Tset = Common_SPU.fnGetSMPRdet(SMPRID);
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new SMPR.SMPRDetEntry();
                    
                    obj.RowNum = Convert.ToInt32(item["RowNum"]);
                    obj.SMPRDetID = Convert.ToInt64(item["SMPRDetID"]);
                    obj.SMPRID = Convert.ToInt64(item["SMPRID"]);
                    obj.Month = Convert.ToInt32(item["Month"]);
                    obj.Year = Convert.ToInt32(item["Year"]);
                    obj.MonthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(obj.Month);
                    obj.Lock = Convert.ToInt32(item["Lock"]);
                    obj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                    obj.ProjectName = item["ProjectName"].ToString();
                    obj.DueOn = Convert.ToDateTime(item["DueOn"]).ToString(DateFormat);
                    obj.StateID = Convert.ToInt32(item["StateID"]);
                    obj.StateName = item["StateName"].ToString();
                    obj.Version = Convert.ToInt32(item["Version"]);
                    obj.MPRDetID = Convert.ToInt64(item["MPRDetID"]);
                    obj.MPRIndicatorID = Convert.ToInt64(item["MPRIndicatorID"]);
                    obj.IndicatorName = item["IndicatorName"].ToString();
                    obj.MPRSubSecID = Convert.ToInt64(item["MPRSubSecID"]);
                    obj.MPRSID = Convert.ToInt64(item["MPRSID"]);
                    obj.SecName = item["SecName"].ToString();
                    obj.SubSecName = item["SubSecName"].ToString();
                    obj.AnswerIs = item["AnswerIs"].ToString();
                    obj.IsOrganisational = item["IsOrganisational"].ToString();
                    obj.NoOfCol = Convert.ToInt32(item["NoOfCol"]);
                    obj.ColText1 = item["ColText1"].ToString();
                    obj.ColDataType1 = item["ColDataType1"].ToString();
                    obj.ColSuffix1 = item["ColSuffix1"].ToString();
                    obj.ColText2 = item["ColText2"].ToString();
                    obj.ColDataType2 = item["ColDataType2"].ToString();
                    obj.ColSuffix2 = item["ColSuffix2"].ToString();
                    obj.ColText3 = item["ColText3"].ToString();
                    obj.ColDataType3 = item["ColDataType3"].ToString();
                    obj.ColSuffix3 = item["ColSuffix3"].ToString();

                    obj.ExecuterVal1 = item["ExecuterVal1"].ToString();
                    obj.ExecuterVal2 = item["ExecuterVal2"].ToString();
                    obj.ExecuterVal3 = item["ExecuterVal3"].ToString();
                    obj.LevelApproved = Convert.ToInt32(item["LevelApproved"]);
                    obj.Submitted = Convert.ToInt32(item["Submitted"]);
                    obj.Approved = Convert.ToInt32(item["Approved"]);
                    obj.SMPRSubmitted = Convert.ToInt32(item["SMPRSubmitted"]);

                    obj.Executor1ID = Convert.ToInt64(item["Executor1ID"]);
                    obj.Executor1Name = item["Executor1Name"].ToString();
                    obj.Executor1Email = item["Executor1Email"].ToString();

                    obj.Executor2ID = Convert.ToInt64(item["Executor2ID"]);
                    obj.Executor2Name = item["Executor2Name"].ToString();
                    obj.Executor2Email = item["Executor2Email"].ToString();

                    obj.ApproverID = Convert.ToInt64(item["ApproverID"]);
                    obj.ApproverName = item["ApproverName"].ToString();
                    obj.ApproverEmail = item["ApproverEmail"].ToString();

                    obj.ApproverLevel1 = Convert.ToInt64(item["ApproverLevel1"]);
                    obj.ApproverLevel1Name = item["ApproverLevel1Name"].ToString();
                    obj.ApproverLevel1Email = item["ApproverLevel1Email"].ToString();

                    obj.ApproverLevel2 = Convert.ToInt64(item["ApproverLevel2"]);
                    obj.ApproverLevel2Name = item["ApproverLeve12Name"].ToString();
                    obj.ApproverLevel2Email = item["ApproverLevel2Email"].ToString();

                    obj.SectionPriority = Convert.ToInt32(item["SectionPriority"]);
                    obj.SubSectionPriority = Convert.ToInt32(item["SubSectionPriority"]);
                    obj.indicatorPriority = Convert.ToInt32(item["indicatorPriority"]);

                    obj.FilledBy = item["FilledBy"].ToString();
                    obj.Filleddat = (Convert.ToDateTime(item["Filleddat"]).Year > 1900 ? Convert.ToDateTime(item["Filleddat"]).ToString(DateFormatC) : "");
                    obj.FilledIP = item["FilledIP"].ToString();

                    List.Add(obj);
                }
            }

            return List;
        }

        public SMPR.SMPREntry GetSMPREntry(long SMPRID)
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            SMPR.SMPREntry obj = new SMPR.SMPREntry();
            List<SMPR.SMPRDetEntry> List = new List<SMPR.SMPRDetEntry>();
            List<SMPR.Section> Slist = new List<SMPR.Section>();
            List<SMPR.SubSection> SSlist = new List<SMPR.SubSection>();

            List = GetEntrySMPRDetList(SMPRID);

            if (List.Count > 0)
            {

                var Section = List.GroupBy(x => x.MPRSID)
                            .Select(x => new
                            {
                                SecID = x.Key,
                                SecName = x.Select(ex => ex.SecName).FirstOrDefault(),
                                SectionPriority = x.Select(ex => ex.SectionPriority).FirstOrDefault(),
                            })
                            .OrderBy(x => x.SectionPriority).ToList();

                var SubSection = List.GroupBy(x => x.MPRSubSecID)
                         .Select(x => new
                         {
                             SubSecID = x.Key,
                             SecID = x.Select(ex => ex.MPRSID).FirstOrDefault(),
                             SecName = x.Select(ex => ex.SecName).FirstOrDefault(),
                             SubSecName = x.Select(ex => ex.SubSecName).FirstOrDefault(),
                             SubSectionPriority = x.Select(ex => ex.SubSectionPriority).FirstOrDefault(),

                         })
                         .OrderBy(x => x.SubSectionPriority).ToList();
                obj.DueOn = List.Select(x => x.DueOn).FirstOrDefault();
                obj.ProjectName = List.Select(x => x.ProjectName).FirstOrDefault();
                obj.StateName = List.Select(x => x.StateName).FirstOrDefault();
                obj.MonthName = List.Select(x => x.MonthName).FirstOrDefault();
                obj.Version = List.Select(x => x.Version).FirstOrDefault();
                obj.SMPRSubmitted = List.Select(x => x.SMPRSubmitted).FirstOrDefault();
                obj.LevelApproved = List.Select(x => x.LevelApproved).FirstOrDefault();

                obj.Lock = List.Select(x => x.Lock).FirstOrDefault();
                obj.IsButtonShow = false;

                if (List.Any(x => x.Submitted == 0) || List.Any(x => x.Submitted == 2) && obj.Lock == 0)
                {
                    obj.IsButtonShow = true;
                }



                //List<SMPR.Section> Slist = new List<SMPR.Section>();
                //List<SMPR.SubSection> SSlist = new List<SMPR.SubSection>();

                SMPR.Section Sobj = new SMPR.Section();
                foreach (var item in Section)
                {
                    Sobj = new SMPR.Section();
                    Sobj.SecID = item.SecID;
                    Sobj.SecName = item.SecName;
                    Slist.Add(Sobj);
                }

                //List<SMPR.SubSection> SSlist = new List<SMPR.SubSection>();
                SMPR.SubSection SSobj = new SMPR.SubSection();
                foreach (var item in SubSection)
                {
                    SSobj = new SMPR.SubSection();
                    SSobj.SecID = item.SecID;
                    SSobj.SecName = item.SecName;
                    SSobj.SubSecID = item.SubSecID;
                    SSobj.SubSecName = item.SubSecName;

                    SSlist.Add(SSobj);
                }
            }
                obj.SMPRSection = Slist;
                obj.SMPRSubSectionList = SSlist;
                obj.SMPRDetList = List;

            

            obj.SMPRCommentsList = CommentsList(SMPRID, 0);
            return obj;
        }


        public SMPR.SMPRApproval GetSMPRApproval(long SMPRID)
        {
            SMPR.SMPRApproval obj = new SMPR.SMPRApproval();
            List<SMPR.SMPRDet> List = new List<SMPR.SMPRDet>();
            List = GetSMPRDetList(SMPRID);

            if (List.Count > 0)
            {

                var Section = List.GroupBy(x => x.MPRSID)
                           .Select(x => new
                           {
                               SecID = x.Key,
                               SecName = x.Select(ex => ex.SecName).FirstOrDefault(),
                               SectionPriority = x.Select(ex => ex.SectionPriority).FirstOrDefault(),
                           })
                           .OrderBy(x => x.SectionPriority).ToList();

                var SubSection = List.GroupBy(x => x.MPRSubSecID)
                         .Select(x => new
                         {
                             SubSecID = x.Key,
                             SecID = x.Select(ex => ex.MPRSID).FirstOrDefault(),
                             SecName = x.Select(ex => ex.SecName).FirstOrDefault(),
                             SubSecName = x.Select(ex => ex.SubSecName).FirstOrDefault(),
                             SubSectionPriority = x.Select(ex => ex.SubSectionPriority).FirstOrDefault(),

                         })
                         .OrderBy(x => x.SubSectionPriority).ToList();

                obj.DueOn = List.Select(x => x.DueOn).FirstOrDefault();
                obj.ProjectName = List.Select(x => x.ProjectName).FirstOrDefault();
                obj.StateName = List.Select(x => x.StateName).FirstOrDefault();
                obj.MonthName = List.Select(x => x.MonthName).FirstOrDefault();
                obj.Version = List.Select(x => x.Version).FirstOrDefault();
                obj.SMPRSubmitted = List.Select(x => x.SMPRSubmitted).FirstOrDefault();

                obj.LevelApproved = List.Select(x => x.LevelApproved).FirstOrDefault();
                
                obj.Lock = List.Select(x => x.Lock).FirstOrDefault();
                obj.IsButtonShow = false;
                if (List.Any(x => x.Approved==0) || List.Any(x => x.Approved == 4) && obj.Lock == 0)
                {
                    obj.IsButtonShow = true;
                }

               
                List<SMPR.Section> Slist = new List<SMPR.Section>();
                SMPR.Section Sobj = new SMPR.Section();
                foreach (var item in Section)
                {
                    Sobj = new SMPR.Section();
                    Sobj.SecID = item.SecID;
                    Sobj.SecName = item.SecName;
                    Slist.Add(Sobj);
                }

                List<SMPR.SubSection> SSlist = new List<SMPR.SubSection>();
                SMPR.SubSection SSobj = new SMPR.SubSection();
                foreach (var item in SubSection)
                {
                    SSobj = new SMPR.SubSection();
                    SSobj.SecID = item.SecID;
                    SSobj.SecName = item.SecName;
                    SSobj.SubSecID = item.SubSecID;
                    SSobj.SubSecName = item.SubSecName;
                    SSlist.Add(SSobj);
                }
                obj.SMPRSection = Slist;
                obj.SMPRSubSectionList = SSlist;

                obj.SMPRDetList = List;

                List<SMPR.ExecuterDetails> ExecuterList = new List<SMPR.ExecuterDetails>();
                SMPR.ExecuterDetails EObj = new SMPR.ExecuterDetails();
                var Executer1 = List.GroupBy(x => x.Executor1ID)
                        .Select(x => new
                        {
                            Executor1ID = x.Key,
                            Executor1Name = x.Select(ex => ex.Executor1Name).FirstOrDefault(),
                            Executor1Email = x.Select(ex => ex.Executor1Email).FirstOrDefault(),
                            Submitted = x.Select(ex => ex.Submitted).FirstOrDefault(),
                        }).ToList();

                var Executer2 = List.GroupBy(x => x.Executor2ID)
                        .Select(x => new
                        {
                            Executor2ID = x.Key,
                            Executor2Name = x.Select(ex => ex.Executor2Name).FirstOrDefault(),
                            Executor2Email = x.Select(ex => ex.Executor2Email).FirstOrDefault(),
                            Submitted = x.Select(ex => ex.Submitted).FirstOrDefault(),
                        }).ToList();

                foreach (var item in Executer1)
                {
                    EObj = new SMPR.ExecuterDetails();
                    EObj.ExecutorID = item.Executor1ID;
                    EObj.ExecutorName = item.Executor1Name;
                    EObj.ExecutorEmail = item.Executor1Email;
                    EObj.Submitted = item.Submitted;
                    ExecuterList.Add(EObj);

                }
                foreach (var item in Executer2)
                {
                    EObj = new SMPR.ExecuterDetails();
                    EObj.ExecutorID = item.Executor2ID;
                    EObj.ExecutorName = item.Executor2Name;
                    EObj.ExecutorEmail = item.Executor2Email;
                    EObj.Submitted = item.Submitted;
                    ExecuterList.Add(EObj);

                }
                obj.ExecuterDetailsList = ExecuterList;

            }
            obj.SMPRCommentsList = CommentsList(SMPRID, 0);
            return obj;
        }


        public SMPR.LevelApprover GetLevelApprover(long SMPRID)
        {
            SMPR.LevelApprover obj = new SMPR.LevelApprover();
            List<SMPR.SMPRDet> List = new List<SMPR.SMPRDet>();
            List = GetSMPRDetList(SMPRID);
            if (List.Count > 0)
            {

                var Section = List.GroupBy(x => x.MPRSID)
                           .Select(x => new
                           {
                               SecID = x.Key,
                               SecName = x.Select(ex => ex.SecName).FirstOrDefault(),
                               SectionPriority = x.Select(ex => ex.SectionPriority).FirstOrDefault(),
                           })
                           .OrderBy(x => x.SectionPriority).ToList();

                var SubSection = List.GroupBy(x => x.MPRSubSecID)
                         .Select(x => new
                         {
                             SubSecID = x.Key,
                             SecID = x.Select(ex => ex.MPRSID).FirstOrDefault(),
                             SecName = x.Select(ex => ex.SecName).FirstOrDefault(),
                             SubSecName = x.Select(ex => ex.SubSecName).FirstOrDefault(),
                             SubSectionPriority = x.Select(ex => ex.SubSectionPriority).FirstOrDefault(),

                         })
                         .OrderBy(x => x.SubSectionPriority).ToList();

                obj.DueOn = List.Select(x => x.DueOn).FirstOrDefault();
                obj.ProjectName = List.Select(x => x.ProjectName).FirstOrDefault();
                obj.StateName = List.Select(x => x.StateName).FirstOrDefault();
                obj.MonthName = List.Select(x => x.MonthName).FirstOrDefault();
                obj.Version = List.Select(x => x.Version).FirstOrDefault();
                obj.SMPRSubmitted = List.Select(x => x.SMPRSubmitted).FirstOrDefault();

                obj.Approved = List.Select(x => x.LevelApproved).FirstOrDefault();
              
                obj.Lock = List.Select(x => x.Lock).FirstOrDefault();
                obj.IsButtonShow = false;
                if (obj.Approved==1 || obj.Approved == 6  && obj.Lock == 0)
                {
                    obj.IsButtonShow = true;
                }
                List<SMPR.Section> Slist = new List<SMPR.Section>();
                SMPR.Section Sobj = new SMPR.Section();
                foreach (var item in Section)
                {
                    Sobj = new SMPR.Section();
                    Sobj.SecID = item.SecID;
                    Sobj.SecName = item.SecName;
                    Slist.Add(Sobj);
                }

                List<SMPR.SubSection> SSlist = new List<SMPR.SubSection>();
                SMPR.SubSection SSobj = new SMPR.SubSection();
                foreach (var item in SubSection)
                {
                    SSobj = new SMPR.SubSection();
                    SSobj.SecID = item.SecID;
                    SSobj.SecName = item.SecName;
                    SSobj.SubSecID = item.SubSecID;
                    SSobj.SubSecName = item.SubSecName;
                    SSlist.Add(SSobj);
                }
                obj.SMPRSection = Slist;
                obj.SMPRSubSectionList = SSlist;

                obj.SMPRDetList = List;

                List<SMPR.ExecuterDetails> ExecuterList = new List<SMPR.ExecuterDetails>();
                SMPR.ExecuterDetails EObj = new SMPR.ExecuterDetails();
                var SectionApprover = List.GroupBy(x => x.ApproverID)
                        .Select(x => new
                        {
                            ApproverID = x.Key,
                            ApproverName = x.Select(ex => ex.ApproverName).FirstOrDefault(),
                            ApproverEmail = x.Select(ex => ex.ApproverEmail).FirstOrDefault(),
                            Submitted = x.Select(ex => ex.Submitted).FirstOrDefault(),

                        }).ToList();

                foreach (var item in SectionApprover)
                {
                    EObj = new SMPR.ExecuterDetails();
                    EObj.ExecutorID = item.ApproverID;
                    EObj.ExecutorName = item.ApproverName;
                    EObj.ExecutorEmail = item.ApproverEmail;
                    EObj.Submitted = item.Submitted;
                    ExecuterList.Add(EObj);

                }

                obj.ExecuterDetailsList = ExecuterList;
                obj.SMPRCommentsList = CommentsList(SMPRID, 0);

            }
            return obj;
        }

        public SMPR.LevelApprover2 GetLevel2Approver(long SMPRID)
        {
            SMPR.LevelApprover2 obj = new SMPR.LevelApprover2();
            List<SMPR.SMPRDet> List = new List<SMPR.SMPRDet>();
            List = GetSMPRDetList(SMPRID);
            if (List.Count > 0)
            {

                var Section = List.GroupBy(x => x.MPRSID)
                           .Select(x => new
                           {
                               SecID = x.Key,
                               SecName = x.Select(ex => ex.SecName).FirstOrDefault(),
                               SectionPriority = x.Select(ex => ex.SectionPriority).FirstOrDefault(),
                           })
                           .OrderBy(x => x.SectionPriority).ToList();

                var SubSection = List.GroupBy(x => x.MPRSubSecID)
                         .Select(x => new
                         {
                             SubSecID = x.Key,
                             SecID = x.Select(ex => ex.MPRSID).FirstOrDefault(),
                             SecName = x.Select(ex => ex.SecName).FirstOrDefault(),
                             SubSecName = x.Select(ex => ex.SubSecName).FirstOrDefault(),
                             SubSectionPriority = x.Select(ex => ex.SubSectionPriority).FirstOrDefault(),

                         })
                         .OrderBy(x => x.SubSectionPriority).ToList();

                obj.DueOn = List.Select(x => x.DueOn).FirstOrDefault();
                obj.ProjectName = List.Select(x => x.ProjectName).FirstOrDefault();
                obj.StateName = List.Select(x => x.StateName).FirstOrDefault();
                obj.MonthName = List.Select(x => x.MonthName).FirstOrDefault();
                obj.Version = List.Select(x => x.Version).FirstOrDefault();
                obj.SMPRSubmitted = List.Select(x => x.SMPRSubmitted).FirstOrDefault();

                obj.Approved = List.Select(x => x.LevelApproved).FirstOrDefault();
           
                obj.Lock = List.Select(x => x.Lock).FirstOrDefault();
                obj.IsButtonShow = false;
                if (obj.Approved ==3  && obj.Lock==0)
                {
                    obj.IsButtonShow = true;
                }
               
                List<SMPR.Section> Slist = new List<SMPR.Section>();
                SMPR.Section Sobj = new SMPR.Section();
                foreach (var item in Section)
                {
                    Sobj = new SMPR.Section();
                    Sobj.SecID = item.SecID;
                    Sobj.SecName = item.SecName;
                    Slist.Add(Sobj);
                }

                List<SMPR.SubSection> SSlist = new List<SMPR.SubSection>();
                SMPR.SubSection SSobj = new SMPR.SubSection();
                foreach (var item in SubSection)
                {
                    SSobj = new SMPR.SubSection();
                    SSobj.SecID = item.SecID;
                    SSobj.SecName = item.SecName;
                    SSobj.SubSecID = item.SubSecID;
                    SSobj.SubSecName = item.SubSecName;
                    SSlist.Add(SSobj);
                }
                obj.SMPRSection = Slist;
                obj.SMPRSubSectionList = SSlist;

                obj.SMPRDetList = List;

                List<SMPR.ExecuterDetails> ExecuterList = new List<SMPR.ExecuterDetails>();
                SMPR.ExecuterDetails EObj = new SMPR.ExecuterDetails();
                var SectionApprover = List.GroupBy(x => x.LevelApproved)
                        .Select(x => new
                        {
                            ApproverID = x.Key,
                            ApproverName = x.Select(ex => ex.ApproverLevel1Name).FirstOrDefault(),
                            ApproverEmail = x.Select(ex => ex.ApproverLevel1Email).FirstOrDefault(),
                            Submitted = x.Select(ex => ex.Submitted).FirstOrDefault()

                        }).ToList();

                foreach (var item in SectionApprover)
                {
                    EObj = new SMPR.ExecuterDetails();
                    EObj.ExecutorID = item.ApproverID;
                    EObj.ExecutorName = item.ApproverName;
                    EObj.ExecutorEmail = item.ApproverEmail;
                    EObj.Submitted = item.Submitted;
                    ExecuterList.Add(EObj);

                }

                obj.ExecuterDetailsList = ExecuterList;
                obj.SMPRCommentsList = CommentsList(SMPRID, 0);

            }
            return obj;
        }

        public List<SMPR.SMPRComments> CommentsList(long SMPRID, long SectionID)
        {
            List<SMPR.SMPRComments> List = new List<SMPR.SMPRComments>();
            SMPR.SMPRComments obj = new SMPR.SMPRComments();
            DataSet Tset = Common_SPU.fnGetSMPRComments(SMPRID, SectionID);
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new SMPR.SMPRComments();
                    obj.SCID = Convert.ToInt64(item["SCID"]);
                    obj.SMPRID = Convert.ToInt64(item["SMPRID"]);
                    obj.Comment = item["Comment"].ToString();
                    obj.Doctype = item["Doctype"].ToString();
                    obj.SectionID = Convert.ToInt64(item["SectionID"]);
                    obj.createdby = Convert.ToInt64(item["createdby"]);
                    obj.CreatedByName = item["Name"].ToString();
                    obj.CreatedByEmail = item["Email"].ToString();
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.createddat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    List.Add(obj);
                }
            }
            return List;

        }

        public MPRDashboard MPRDashboard()
        {

            MPRDashboard obj = new MPRDashboard();

            DataSet Tset = Common_SPU.fnGetMPRDashboardHeader();

            obj.ProjectList = GetProjectList(Tset.Tables[0]);
            obj.StateList = GetStateList(Tset.Tables[1]);


            return obj;

        }

        public List<MPRDashboard.List> MPRDashboardList(long StateID, long ProjectID, int Month, int Year)
        {
            List<MPRDashboard.List> List = new List<MPRDashboard.List>();
            MPRDashboard.List obj = new MPRDashboard.List();
            DataSet Tset = Common_SPU.fnGetMPRDashboardList(StateID, ProjectID, Month, Year);
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new MPRDashboard.List();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.SMPRID = Convert.ToInt64(item["SMPRID"].ToString());
                    obj.MPRID = Convert.ToInt64(item["MPRID"].ToString());
                    obj.MPRCode = item["MPRCode"].ToString();
                    obj.DueOn = Convert.ToDateTime(item["DueOn"]).ToString(DateFormat);
                    obj.MPRName = item["MPRName"].ToString();
                    obj.InitiateDate = Convert.ToDateTime(item["InitiateDate"]).ToString(DateFormat);
                    obj.Month = Convert.ToInt32(item["Month"].ToString());
                    obj.Year = Convert.ToInt32(item["Year"].ToString());
                    obj.ProjectID = Convert.ToInt64(item["ProjectID"].ToString());
                    obj.StateID = Convert.ToInt64(item["StateID"].ToString());
                    obj.ProjectName = item["ProjectName"].ToString();
                    obj.StateName = item["StateName"].ToString();
                    obj.Lock = Convert.ToInt32(item["Lock"].ToString());
                    obj.Submitted = Convert.ToInt32(item["Submitted"].ToString());
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());

                    obj.TotalExec = Convert.ToInt32(item["TotalExec"].ToString());
                    obj.ExecCompleted = Convert.ToInt32(item["ExecCompleted"].ToString());
                    obj.AppTotal = Convert.ToInt32(item["AppTotal"].ToString());
                    obj.AppCompleted = Convert.ToInt32(item["AppCompleted"].ToString());
                    //obj.ApproverLevel2 = Convert.ToInt32(item["ApproverLevel2"].ToString());
                    //obj.ApproverLevel1 = Convert.ToInt32(item["ApproverLevel1"].ToString());
                    //obj.Executor1ID = Convert.ToInt32(item["Executor1ID"].ToString());
                    //obj.Executor2ID = Convert.ToInt32(item["Executor2ID"].ToString());
                    //obj.ApproverID = Convert.ToInt32(item["ApproverID"].ToString());
                    List.Add(obj);
                }
            }
            return List;

        }
        public List<MPRDashboard.List> MPRDashboardListNew(long StateID, long ProjectID, int Month, int Year, string DocType)
        {
            List<MPRDashboard.List> List = new List<MPRDashboard.List>();
            MPRDashboard.List obj = new MPRDashboard.List();
            DataSet Tset = Common_SPU.fnGetMPRDashboardListNew(StateID, ProjectID, Month, Year,  DocType);
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new MPRDashboard.List();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.SMPRID = Convert.ToInt64(item["SMPRID"].ToString());
                    obj.MPRID = Convert.ToInt64(item["MPRID"].ToString());
                   // obj.MPRCode = item["MPRCode"].ToString();
                    obj.DueOn = Convert.ToDateTime(item["DueOn"]).ToString(DateFormat);
                    obj.MPRName = item["MPRName"].ToString();
                    obj.InitiateDate = Convert.ToDateTime(item["InitiateDate"]).ToString(DateFormat);
                   // obj.Month = Convert.ToInt32(item["Month"].ToString());
                   // obj.Year = Convert.ToInt32(item["Year"].ToString());
                    obj.ProjectID = Convert.ToInt64(item["ProjID"].ToString());
                    obj.StateID = Convert.ToInt64(item["StateID"].ToString());
                    obj.ProjectName = item["ProjectName"].ToString();
                    obj.StateName = item["StateName"].ToString();
                    obj.Lock = Convert.ToInt32(item["lock"].ToString());
                    //  obj.Submitted = Convert.ToInt32(item["Submitted"].ToString());
                    //  obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.TotalExec = Convert.ToInt32(item["TotalExec"].ToString());
                    obj.ExecCompleted = Convert.ToInt32(item["ExecCompleted"].ToString());
                    obj.AppTotal = Convert.ToInt32(item["AppTotal"].ToString());
                    obj.AppCompleted = Convert.ToInt32(item["AppCompleted"].ToString());
                    List.Add(obj);
                }
            }
            return List;

        }

        //public List<MPRDashboard.ListDashboard> MPRDashboardListCount(long StateID, long ProjectID, int Month, int Year,string DocType)
        //{
        //    List<MPRDashboard.ListDashboard> List = new List<MPRDashboard.ListDashboard>();
        //    MPRDashboard.ListDashboard obj = new MPRDashboard.ListDashboard();
        //    DataSet Tset = Common_SPU.fnGetMPRDashboardListCount(StateID, ProjectID, Month, Year, DocType);
        //    if (Tset.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow item in Tset.Tables[0].Rows)
        //        {
        //            obj = new MPRDashboard.ListDashboard();
        //            obj.PendingExec = Convert.ToInt32(item["PendingExecutor"].ToString());
        //            obj.PendingSection = Convert.ToInt32(item["PendingSectionApprova"].ToString());
        //            obj.PendingLevelOne = Convert.ToInt32(item["Pendinglevel1"].ToString());
        //            obj.PendingLevelTwo = Convert.ToInt32(item["Pendinglevel2"].ToString());
        //            obj.Complted = Convert.ToInt32(item["Completed"].ToString());
        //            List.Add(obj);
        //        }
        //    }
        //    return List;

        //}


        public MPRDashboard.ListDashboard MPRDashboardListCountNew(long StateID, long ProjectID, int Month, int Year, string DocType)
        {
            MPRDashboard.ListDashboard result = new MPRDashboard.ListDashboard();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@STATEID", dbType: DbType.Int32, value: StateID, direction: ParameterDirection.Input);
                    param.Add("@PROJECTID", dbType: DbType.Int32, value: ProjectID, direction: ParameterDirection.Input);
                    param.Add("@Month", dbType: DbType.Int32, value: Month, direction: ParameterDirection.Input);
                    param.Add("@Year", dbType: DbType.Int32, value: Year, direction: ParameterDirection.Input);
                    param.Add("@DocType", dbType: DbType.String, value: DocType, direction: ParameterDirection.Input);
                    param.Add("@LOGINID ", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMPRDashboardList_New", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<MPRDashboard.ListDashboard>().FirstOrDefault();
                        
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during MPRDashboardListCountNew. The query was executed :", ex.ToString(), "spu_GetMPRDashboardList_New()", "MPR", "MPRModal", "");

            }
            return result;
        }

        public List<LockUnlock.List> LockUnlockList(DateTime dtDate)
        {
            List<LockUnlock.List> List = new List<LockUnlock.List>();
            LockUnlock.List obj = new LockUnlock.List();
            DataSet Tset = Common_SPU.fnGetLockunlockSMPR(dtDate);
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new LockUnlock.List();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.SMPRID = Convert.ToInt64(item["SMPRID"].ToString());
                    obj.MPRID = Convert.ToInt64(item["MPRID"].ToString());
                    obj.MPRCode = item["MPRCode"].ToString();
                    obj.MPRName = item["MPRName"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.DueOn = Convert.ToDateTime(item["DueOn"]).ToString(DateFormat);
                    obj.InitiateDate = Convert.ToDateTime(item["InitiateDate"]).ToString(DateFormat);
                    obj.Month = Convert.ToInt32(item["Month"].ToString());
                    obj.Year = Convert.ToInt32(item["Year"].ToString());
                    obj.ProjectID = Convert.ToInt64(item["ProjectID"].ToString());
                    obj.StateID = Convert.ToInt64(item["StateID"].ToString());
                    obj.ProjectName = item["ProjectName"].ToString();
                    obj.StateName = item["StateName"].ToString();
                    obj.Lock = Convert.ToInt32(item["Lock"].ToString());
                    obj.Version = Convert.ToInt32(item["Version"].ToString());
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    
                    obj.UpdatedCount = Convert.ToInt32(item["UpdatedCount"].ToString());
                    obj.LastUpdatedOn = (Convert.ToDateTime(item["LastUpdatedOn"]).Year > 1900 ? Convert.ToDateTime(item["LastUpdatedOn"]).ToString(DateFormatC) : "");
                    obj.LastLockedDate = item["LastLockedDate"].ToString();
                    List.Add(obj);
                }
            }
            return List;

        }

        public List<LockUnlock.HistoryList> SMPRLockHistoryList(long SMPRID)
        {
            List<LockUnlock.HistoryList> List = new List<LockUnlock.HistoryList>();
            LockUnlock.HistoryList obj = new LockUnlock.HistoryList();
            DataSet Tset = Common_SPU.fnGetSMPRLockHistory(SMPRID);
            if (Tset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    obj = new LockUnlock.HistoryList();
                    obj.SMPRLHID = Convert.ToInt32(item["SMPRLHID"].ToString());
                    obj.Name = item["Name"].ToString();
                    obj.Email = item["Email"].ToString();
                    obj.Remarks = item["Remarks"].ToString();
                    obj.Date = (Convert.ToDateTime(item["createdat"]).Year > 1900 ? Convert.ToDateTime(item["createdat"]).ToString(DateFormatC) : "");
                    List.Add(obj);
                }
            }
            return List;

        }
        public MPRSetting MPRSettingList(long MPRSettingID)
        {
            MPRSetting List = new MPRSetting();
            DataSet Tset = Common_SPU.fnGetMPRSetting(MPRSettingID);

            foreach (DataRow item in Tset.Tables[0].Rows)
            {
                List.MPRSettingID = Convert.ToInt64(item["MPRSettingID"].ToString());
                List.MPRDueDate = Convert.ToInt32(item["MPRDueDate"].ToString());
            }

            return List;

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


        public MPRReports.Header MPRReportHeader()
        {

            MPRReports.Header obj = new MPRReports.Header();

            List<MPRReports.State> StateList = new List<MPRReports.State>();
         
            List<MPRReports.Project> ProjectList = new List<MPRReports.Project>();
            List<MPRReports.Section> SectionList = new List<MPRReports.Section>();

            List<MPRReports.MPRData> MPRList = new List<MPRReports.MPRData>();

            DataSet Tset = Common_SPU.fnGetMPR_Reports_Header();

            if (Tset.Tables.Count > 0)
            {
                // 0 get All Projects
                // 1 get All State
                // 2 get All Section
                MPRReports.Project Pobj;
                MPRReports.Section Secobj;
                MPRReports.State Sobj;
                MPRReports.MPRData Mobj;
                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    Pobj = new MPRReports.Project();
                    Pobj.ProjectID = Convert.ToInt64(item["ID"]);
                    Pobj.ProjectName = item["ProjectName"].ToString();
                    ProjectList.Add(Pobj);
                }
                foreach (DataRow item in Tset.Tables[3].Rows)
                {
                    Mobj = new MPRReports.MPRData();
                    Mobj.MPRID = Convert.ToInt64(item["MPRID"]);
                    Mobj.MPRName = item["MPRName"].ToString();
                    MPRList.Add(Mobj);
                }
                
                foreach (DataRow item in Tset.Tables[1].Rows)
                {
                    Sobj = new MPRReports.State();
                    Sobj.StateID = Convert.ToInt64(item["ID"]);
                    Sobj.StateName = item["state_name"].ToString();
                    StateList.Add(Sobj);
                }
                foreach (DataRow item in Tset.Tables[2].Rows)
                {
                    Secobj = new MPRReports.Section();
                    Secobj.SectionID = Convert.ToInt64(item["MPRSID"]);
                    Secobj.SectionName = item["SecName"].ToString();
                    SectionList.Add(Secobj);
                }
               
            }
            obj.ProjectList = ProjectList;
            obj.StateList = StateList;
            obj.SectionList = SectionList;
            obj.MPRDataList = MPRList;
            return obj;

        }

        public MPRReports.Header MPRNewReportHeader()
        {

            MPRReports.Header obj = new MPRReports.Header();

            List<MPRReports.State> StateList = new List<MPRReports.State>();

            List<MPRReports.Project> ProjectList = new List<MPRReports.Project>();
            List<MPRReports.Section> SectionList = new List<MPRReports.Section>();

            DataSet Tset = Common_SPU.fnGetMPR_Reports_Header();

            if (Tset.Tables.Count > 0)
            {
                // 0 get All Projects
                // 1 get All State
                // 2 get All Section
                MPRReports.Project Pobj;
                MPRReports.Section Secobj;
                MPRReports.State Sobj;

                foreach (DataRow item in Tset.Tables[0].Rows)
                {
                    Pobj = new MPRReports.Project();
                    Pobj.ProjectID = Convert.ToInt64(item["ID"]);
                    Pobj.ProjectName = item["ProjectName"].ToString();
                    ProjectList.Add(Pobj);
                }

                foreach (DataRow item in Tset.Tables[1].Rows)
                {
                    Sobj = new MPRReports.State();
                    Sobj.StateID = Convert.ToInt64(item["ID"]);
                    Sobj.StateName = item["state_name"].ToString();
                    StateList.Add(Sobj);
                }
                foreach (DataRow item in Tset.Tables[2].Rows)
                {
                    Secobj = new MPRReports.Section();
                    Secobj.SectionID = Convert.ToInt64(item["MPRSID"]);
                    Secobj.SectionName = item["SecName"].ToString();
                    SectionList.Add(Secobj);
                }

            }
            obj.ProjectList = ProjectList;
            obj.StateList = StateList;
            obj.SectionList = SectionList;
            return obj;

        }
        public List<MPRReports.List> GetMPRReports(GetMPRResponse modal)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<MPRReports.List> result = new List<MPRReports.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@StartDate", dbType: DbType.String, value: modal.StartDate??"", direction: ParameterDirection.Input);
                    param.Add("@Enddate", dbType: DbType.String, value: modal.Enddate ?? "", direction: ParameterDirection.Input);
                    param.Add("@ProjectIDs", dbType: DbType.String, value: modal.ProjectIDs, direction: ParameterDirection.Input);
                    param.Add("@SectionIDs", dbType: DbType.String, value: modal.SectionIDs, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMPRReports", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<MPRReports.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMPRReports. The query was executed :", ex.ToString(), "GetMPRReports()", "MPRModal", "MPRModal", "");

            }
            return result;
        }
        public List<MPRReports.AchievementList> GetStateProjectAchievementReports(GetMPRResponse modal)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<MPRReports.AchievementList> result = new List<MPRReports.AchievementList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@StartDate", dbType: DbType.String, value: modal.StartDate ?? "", direction: ParameterDirection.Input);
                    param.Add("@Enddate", dbType: DbType.String, value: modal.Enddate ?? "", direction: ParameterDirection.Input);
                    param.Add("@Projects", dbType: DbType.String, value: modal.ProjectIDs, direction: ParameterDirection.Input);
                    param.Add("@States", dbType: DbType.String, value: modal.StateIDs , direction: ParameterDirection.Input);
                    param.Add("@Sections", dbType: DbType.String, value: modal.SectionIDs, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMPRReport_SP_Achv", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<MPRReports.AchievementList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetMPRReport_SP_Achv. The query was executed :", ex.ToString(), "GetStateProjectAchievementReports()", "MPRModal", "MPRModal", "");

            }
            return result;
        }

        public List<MPRReports.AchievementList> GetNewStateProjectAchievementReports(GetMPRResponse modal,int from)
        {
            string spName = "";
            if(from==1)
            {
                spName = "spu_GetMPRNewReport_SP_Achv";
            }
            else
            {
                spName = "spu_GetMPRNewReport_Month_Achv";
            }
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<MPRReports.AchievementList> result = new List<MPRReports.AchievementList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {

                    int StartMonth = 0;
                    int EndMonth = 0;
                    int StartYear = 0;
                    int EndYear = 0;
                    if (!string.IsNullOrEmpty(modal.StartDate))
                    {
                        string[] slStartDate = modal.StartDate.Split('-');
                        StartYear  = Convert.ToInt32(slStartDate[0]);
                        StartMonth = Convert.ToInt32(slStartDate[1]);
                    }

                    if (!string.IsNullOrEmpty(modal.Enddate))
                    {
                        string[] slEnddate = modal.Enddate.Split('-');
                        EndYear  = Convert.ToInt32(slEnddate[0]);
                        EndMonth = Convert.ToInt32(slEnddate[1]);
                    }

                    var param = new DynamicParameters();
                    param.Add("@StartMonth", dbType: DbType.Int32, value: StartMonth, direction: ParameterDirection.Input);
                    param.Add("@EndMonth", dbType: DbType.Int32, value: EndMonth, direction: ParameterDirection.Input);
                    param.Add("@StartYear", dbType: DbType.Int32, value: StartYear, direction: ParameterDirection.Input);
                    param.Add("@EndYear", dbType: DbType.Int32, value: EndYear, direction: ParameterDirection.Input);


                    param.Add("@Projects", dbType: DbType.String, value: modal.ProjectIDs!=string.Empty?modal.ProjectIDs:"", direction: ParameterDirection.Input);
                    param.Add("@States", dbType: DbType.String, value: modal.StateIDs != string.Empty ? modal.StateIDs : "", direction: ParameterDirection.Input);
                    param.Add("@Sections", dbType: DbType.String, value: modal.SectionIDs != string.Empty ? modal.SectionIDs : "", direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: modal.LoginID, direction: ParameterDirection.Input);
                     param.Add("@SubSectionIDs", dbType: DbType.String, value: modal.SubSectionIDs != string.Empty ? modal.SubSectionIDs : "", direction: ParameterDirection.Input);
                    
                    param.Add("@MPRIDs", dbType: DbType.String, value: modal.MPRIDs != string.Empty ? modal.MPRIDs : "", direction: ParameterDirection.Input);
                    if (modal.IsInActiveRecords != null)
                    {
                        param.Add("@IsInActiveRecords", dbType: DbType.Boolean, value: modal.IsInActiveRecords, direction: ParameterDirection.Input);
                    }
                    else
                    {
                        param.Add("@IsInActiveRecords", dbType: DbType.Boolean, value: false, direction: ParameterDirection.Input);
                    }
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple(spName, param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<MPRReports.AchievementList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetMPRReport_SP_Achv. The query was executed :", ex.ToString(), "GetStateProjectAchievementReports()", "MPRModal", "MPRModal", "");

            }
            return result;
        }

        public List<MPRReports.MPRReportExcel> GetMPRReportExcel(GetMPRResponse modal)
        {
           // string spName = "spu_GetMPRNewReport";
            
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<MPRReports.MPRReportExcel> result = new List<MPRReports.MPRReportExcel>();
            try
            {

                // code comment by shailendra 06092024 
                //using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                //{


                //    var param = new DynamicParameters();
                //    param.Add("@FromDate", dbType: DbType.DateTime, value: modal.StartDate, direction: ParameterDirection.Input);
                //    param.Add("@ToDate", dbType: DbType.DateTime, value: modal.Enddate, direction: ParameterDirection.Input);
                //    param.Add("@UserID", dbType: DbType.String, value: LoginID, direction: ParameterDirection.Input);


                //    DBContext.Open();
                //    using (var reader = DBContext.QueryMultiple(spName, param: param, commandType: CommandType.StoredProcedure))
                //    {
                //        result = reader.Read<MPRReports.MPRReportExcel>().ToList();
                //    }
                //    DBContext.Close();
                //}

                DataSet Tset = Common_SPU.GetMPR_ReportsALlMpr(Convert.ToDateTime(modal.StartDate), Convert.ToDateTime(modal.Enddate));
                if (Tset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in Tset.Tables[0].Rows)
                    {
                        var Pobj = new MPRReports.MPRReportExcel();
                        Pobj.MPRName = item["MPRName"].ToString();
                        Pobj.ProjectName = item["ProjectName"].ToString();
                        Pobj.StateName = item["StateName"].ToString();
                        Pobj.MonthName = item["MonthName"].ToString();
                        Pobj.Executor1 = item["Executor1"].ToString();
                        Pobj.Executor2 = item["Executor2"].ToString();
                        Pobj.LastDateExecutor = item["LastDateExecutor"].ToString();
                        Pobj.SectionApproval = item["SectionApproval"].ToString();
                        Pobj.LastDateSectionApproval = item["LastDateSectionApproval"].ToString();
                        Pobj.Level1 = item["Level1"].ToString();
                        Pobj.LastDateLevel1 = item["LastDateLevel1"].ToString();
                        Pobj.Level2 = item["Level2"].ToString();
                        Pobj.LastDateLevel2 = item["LastDateLevel2"].ToString();
                        Pobj.Status = item["status"].ToString();
                        Pobj.MPRStatus = item["MPRStatus"].ToString();
                        Pobj.TAT = item["TAT"].ToString();
                        result.Add(Pobj);
                    }
                }


            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetMPRNewReport. The query was executed :", ex.ToString(), "GetStateProjectAchievementReports()", "MPRModal", "MPRModal", "");

            }
            return result;
        }
    }
}