using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public class PMSModal : IPMSHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public string DateFormat = "dd-MMM-yyyy", DateFormatC = "dd-MMM-yy hh:mm:ss tt", DateFormatE = "yyyy-MM-dd";
        public PMSModal()
        {
            var d = clsApplicationSetting.GetConfigValue("DateFormat");
            var dC = clsApplicationSetting.GetConfigValue("DateFormatC");
            DateFormat = (!string.IsNullOrEmpty(d) ? d : DateFormat);
            DateFormatC = (!string.IsNullOrEmpty(dC) ? dC : DateFormatC);
        }
        public List<PMS.CalenderYear.List> GetCalenderYearList()
        {
            List<PMS.CalenderYear.List> List = new List<PMS.CalenderYear.List>();
            PMS.CalenderYear.List obj = new PMS.CalenderYear.List();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetFinancialYear(0, "0,1");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.CalenderYear.List();
                    obj.ID = Convert.ToInt32(item["ID"].ToString());
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.year = item["year"].ToString();
                    obj.from_date = Convert.ToDateTime(item["from_date"]).ToString(DateFormat);
                    obj.to_date = Convert.ToDateTime(item["to_date"]).ToString(DateFormat);
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
                ClsCommon.LogError("Error during GetFinYearList. The query was executed :", ex.ToString(), "fnGetFinancialYear", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public PMS.CalenderYear.Add GetCalenderYear(long ID)
        {

            PMS.CalenderYear.Add obj = new PMS.CalenderYear.Add();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetFinancialYear(ID,"0,1");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj = new PMS.CalenderYear.Add();
                    obj.ID = Convert.ToInt32(item["ID"].ToString());
                    obj.year = item["year"].ToString();
                    obj.from_date = Convert.ToDateTime(item["from_date"]).ToString(DateFormatE);
                    obj.to_date = Convert.ToDateTime(item["to_date"]).ToString(DateFormatE);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetCalenderYear. The query was executed :", ex.ToString(), "fnGetPMSUOM", "PMSModal", "PMSModal", "");
            }
            return obj;
        }
        public List<PMS.UOM.List> GetPMSUOMList(long PMSUOMID, long FYID, string IsActive = "0,1")
        {
            List<PMS.UOM.List> List = new List<PMS.UOM.List>();
            PMS.UOM.List obj = new PMS.UOM.List();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMSUOM(PMSUOMID, FYID, IsActive);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.UOM.List();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.PMSUOMID = Convert.ToInt64(item["PMSUOMID"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.AutoRating = item["AutoRating"].ToString();
                    obj.Name = item["Name"].ToString();
                    obj.Type = item["Type"].ToString();
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
                ClsCommon.LogError("Error during GetPMSUOM. The query was executed :", ex.ToString(), "fnGetPMSUOM", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public List<PMS.FinList> GetFinYearList()
        {
            List<PMS.FinList> List = new List<PMS.FinList>();
            PMS.FinList obj = new PMS.FinList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetFinancialYear(0, "1");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.FinList();
                    obj.ID = Convert.ToInt32(item["ID"].ToString());
                    obj.year = item["year"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFinYearList. The query was executed :", ex.ToString(), "fnGetFinancialYear", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public PMS.UOM.Add GetPMSUOM(long PMSUOMID)
        {

            PMS.UOM.Add obj = new PMS.UOM.Add();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMSUOM(PMSUOMID, 0, "0");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj.PMSUOMID = Convert.ToInt64(item["PMSUOMID"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.Name = item["Name"].ToString();
                    obj.AutoRating = item["AutoRating"].ToString();
                    obj.Type = item["Type"].ToString();
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMSUOM. The query was executed :", ex.ToString(), "fnGetPMSUOM", "PMSModal", "PMSModal", "");
            }
            return obj;
        }
        public List<PMS.KPA.List> GetPMS_KPAList(long KPAID, long FYID, string OperationType, string IsActive = "0,1")
        {
            List<PMS.KPA.List> List = new List<PMS.KPA.List>();
            PMS.KPA.List obj = new PMS.KPA.List();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_KPA(KPAID, FYID, OperationType, IsActive);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.KPA.List();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.PMS_KPAID = Convert.ToInt64(item["PMS_KPAID"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.AutoRating = item["AutoRating"].ToString();
                    obj.Area = item["Area"].ToString();
                    obj.UOMID = Convert.ToInt32(item["UOMID"]);
                    obj.UOMName = item["UOMName"].ToString();
                    obj.UOMType = item["UOMType"].ToString();
                    obj.IncType = item["IncType"].ToString();
                    obj.IsMonitoring = item["IsMonitoring"].ToString();
                    obj.IsMandatory = item["IsMandatory"].ToString();
                    obj.AutoRating = item["AutoRating"].ToString();
                    obj.IsProbation = item["IsProbation"].ToString();
                    obj.Remarks = item["Remarks"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"]);
                    obj.isdeleted = Convert.ToInt32(item["isdeleted"]);
                    obj.ApprovedStatus = item["ApprovedStatus"].ToString();
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.IsActive = item["Status"].ToString();
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
                ClsCommon.LogError("Error during GetPMSUOM. The query was executed :", ex.ToString(), "fnGetPMSUOM", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public List<PMS.KPA.KPASummary> GetPMS_KPASummaryList()
        {
            List<PMS.KPA.KPASummary> List = new List<PMS.KPA.KPASummary>();
            PMS.KPA.KPASummary obj = new PMS.KPA.KPASummary();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_KPA(0, 0, "Summary", "0,1");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.KPA.KPASummary();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.TotalKPA = Convert.ToInt64(item["TotalKPA"].ToString());
                    obj.TActiveKPA = Convert.ToInt64(item["TActiveKPA"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_KPASummaryList. The query was executed :", ex.ToString(), "fnGetPMS_KPA", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public PMS.KPA.Add GetPMS_KPA(long PMSUOMID, long FYID)
        {

            PMS.KPA.Add obj = new PMS.KPA.Add();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_KPA(PMSUOMID, FYID, "", "0,1");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj.PMS_KPAID = Convert.ToInt64(item["PMS_KPAID"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.AutoRating = item["AutoRating"].ToString();
                    obj.Area = item["Area"].ToString();
                    obj.UOMID = Convert.ToInt32(item["UOMID"]);
                    obj.UOMName = item["UOMName"].ToString();
                    obj.IncType = item["IncType"].ToString();
                    obj.IsMonitoring = item["IsMonitoring"].ToString();
                    obj.IsMandatory = item["IsMandatory"].ToString();
                    obj.AutoRating = item["AutoRating"].ToString();
                    obj.IsProbation = item["IsProbation"].ToString();
                    obj.Remarks = item["Remarks"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"]);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_KPA. The query was executed :", ex.ToString(), "fnGetPMSUOM", "PMSModal", "PMSModal", "");
            }
            return obj;
        }
        public List<PMS.Hierarchy> GetPMS_HierarchyListPB(long FYID)
        {
            List<PMS.Hierarchy> List = new List<PMS.Hierarchy>();
            PMS.Hierarchy obj = new PMS.Hierarchy();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_EssentialPB(0, "Hierarchy", FYID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.Hierarchy();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.PMS_EID = Convert.ToInt32(item["PMS_EID"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.AppraiserName = item["AppraiserName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.Confirmer = Convert.ToInt64(item["Confirmer"].ToString());
                    obj.ConfirmerName = item["ConfirmerName"].ToString();
                    obj.Commentors = item["Commentors"].ToString();
                    obj.CommentorsName = item["CommentorsName"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_KPASummaryList. The query was executed :", ex.ToString(), "fnGetPMS_KPA", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public List<PMS.Hierarchy> GetPMS_HierarchyList(long FYID)
        {
            List<PMS.Hierarchy> List = new List<PMS.Hierarchy>();
            PMS.Hierarchy obj = new PMS.Hierarchy();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_Essential(0, "Hierarchy", FYID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.Hierarchy();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.PMS_EID = Convert.ToInt32(item["PMS_EID"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.AppraiserName = item["AppraiserName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.Confirmer = Convert.ToInt64(item["Confirmer"].ToString());
                    obj.ConfirmerName = item["ConfirmerName"].ToString();
                    obj.Commentors = item["Commentors"].ToString();
                    obj.CommentorsName = item["CommentorsName"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_KPASummaryList. The query was executed :", ex.ToString(), "fnGetPMS_KPA", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        private List<PMS.Hierarchy.EMPList> GetPMS_HierarchyEMPList(DataTable Dt)
        {
            List<PMS.Hierarchy.EMPList> List = new List<PMS.Hierarchy.EMPList>();
            PMS.Hierarchy.EMPList obj = new PMS.Hierarchy.EMPList();
            try
            {

                foreach (DataRow item in Dt.Rows)
                {
                    obj = new PMS.Hierarchy.EMPList();
                    obj.EMPID = Convert.ToInt64(item["ID"].ToString());
                    obj.EMPName = item["emp_name"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_HierarchyEMPList. The query was executed :", ex.ToString(), "fnGetPMS_KPA", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public PMS.Hierarchy.Update GetPMS_HierarchyUpdate(long PMS_EID)
        {
            PMS.Hierarchy.Update result = new PMS.Hierarchy.Update();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@PMS_EID", dbType: DbType.Int32, value: PMS_EID, direction: ParameterDirection.Input);
                    param.Add("@FYID", dbType: DbType.Int32, value: 0, direction: ParameterDirection.Input);
                    param.Add("@OperationType", dbType: DbType.String, value: "Hierarchy", direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPMS_Essential", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PMS.Hierarchy.Update>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {
                            result.EmployeeList = reader.Read<DropDownList>().ToList();

                        }
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_HierarchyUpdate. The query was executed :", ex.ToString(), "spu_GetEmp()", "EmployeeModal", "EmployeeModal", "");

            }
            return result;
        }
        public List<PMS.GoalSheetAct> GetPMS_GoalSheetActList(long FYID)
        {
            List<PMS.GoalSheetAct> List = new List<PMS.GoalSheetAct>();
            PMS.GoalSheetAct obj = new PMS.GoalSheetAct();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_Essential(0, "GoalSheet", FYID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.GoalSheetAct();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.PMS_EID = Convert.ToInt32(item["PMS_EID"].ToString());

                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());

                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.AppraiserName = item["HODName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.Designation = item["DesignationName"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.GoalSheetStart = (Convert.ToDateTime(item["GoalSheetStart"]).Year > 1900 ? Convert.ToDateTime(item["GoalSheetStart"]).ToString(DateFormat) : "");

                    obj.GoalSheetEnd = (Convert.ToDateTime(item["GoalSheetEnd"]).Year > 1900 ? Convert.ToDateTime(item["GoalSheetEnd"]).ToString(DateFormat) : "");

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_KPASummaryList. The query was executed :", ex.ToString(), "fnGetPMS_KPA", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public List<PMS.OSQuestion.List> GetPMS_QuestionList(long FYID)
        {
            List<PMS.OSQuestion.List> List = new List<PMS.OSQuestion.List>();
            PMS.OSQuestion.List obj = new PMS.OSQuestion.List();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_Essential(0, "Question", FYID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.OSQuestion.List();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.PMS_EID = Convert.ToInt32(item["PMS_EID"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.Question = item["Question"].ToString();
                    obj.QApplyDesignation = item["QApplyDesignation"].ToString();
                    obj.QDesignationIDs = item["QDesignationIDs"].ToString();
                    obj.DesignationNames = item["DesignationNames"].ToString().TrimEnd(',');
                    obj.QuestionFor = item["QuestionFor"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.Priority = Convert.ToInt32(item["Priority"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_QuestionList. The query was executed :", ex.ToString(), "fnGetPMS_KPA", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public PMS.OSQuestion.Add GetPMS_Question(long PMS_EID)
        {
            PMS.OSQuestion.Add obj = new PMS.OSQuestion.Add();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_Essential(PMS_EID, "Question", 0);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.OSQuestion.Add();
                    obj.PMS_EID = Convert.ToInt32(item["PMS_EID"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.Question = item["Question"].ToString();
                    obj.QApplyDesignation = item["QApplyDesignation"].ToString();
                    obj.QDesignationIDs = item["QDesignationIDs"].ToString();
                    obj.QuestionFor = item["QuestionFor"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_Question. The query was executed :", ex.ToString(), "fnGetPMS_Essential", "PMSModal", "PMSModal", "");
            }
            return obj;
        }
        public List<PMS.DesignationList> GetDesignationList(long ID, string IsActive)
        {
            List<PMS.DesignationList> List = new List<PMS.DesignationList>();
            PMS.DesignationList obj = new PMS.DesignationList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetDesign(ID, "1");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.DesignationList();
                    obj.ID = Convert.ToInt32(item["ID"].ToString());
                    obj.Design = item["Design_name"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetDesignationList. The query was executed :", ex.ToString(), "fnGetFinancialYear", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public List<PMS.AppraisalAct> GetPMS_AppraisalActList(long FYID)
        {
            List<PMS.AppraisalAct> List = new List<PMS.AppraisalAct>();
            PMS.AppraisalAct obj = new PMS.AppraisalAct();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_Essential(0, "Appraisal", FYID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.AppraisalAct();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.PMS_EID = Convert.ToInt32(item["PMS_EID"].ToString());
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.AppraiserName = item["AppraiserName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.Designation = item["DesignationName"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.AppraisalEntryStart = (Convert.ToDateTime(item["AppraisalEntryStart"]).Year > 1900 ? Convert.ToDateTime(item["AppraisalEntryStart"]).ToString(DateFormat) : "");
                    obj.AppraisalEntryEnd = (Convert.ToDateTime(item["AppraisalEntryEnd"]).Year > 1900 ? Convert.ToDateTime(item["AppraisalEntryEnd"]).ToString(DateFormat) : "");

                    obj.AppraisalReviewStart = (Convert.ToDateTime(item["AppraisalReviewStart"]).Year > 1900 ? Convert.ToDateTime(item["AppraisalReviewStart"]).ToString(DateFormat) : "");
                    obj.AppraisalReviewEnd = (Convert.ToDateTime(item["AppraisalReviewEnd"]).Year > 1900 ? Convert.ToDateTime(item["AppraisalReviewEnd"]).ToString(DateFormat) : "");

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_AppraisalActList. The query was executed :", ex.ToString(), "fnGetPMS_Essential", "PMSModal", "PMSModal", "");
            }
            return List;
        }
        public PMS.GoalSheet.MySheet GetMyGoalSheet(long FYID, int Approved)
        {

            PMS.GoalSheet.MySheet obj = new PMS.GoalSheet.MySheet();
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);

            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetGoalSheet(FYID, EMPID, "MyGoalSheet", Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj.FYID = Convert.ToInt32(item["FYID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.Confirmer = Convert.ToInt64(item["Confirmer"].ToString());
                    obj.ConfirmerName = item["ConfirmerName"].ToString();
                    obj.HODID = Convert.ToInt64(item["HODID"].ToString());
                    obj.HODName = item["HODName"].ToString();
                    obj.DesignationName = item["DesignationName"].ToString();
                    obj.Isdeleted = Convert.ToInt32(item["Isdeleted"].ToString());
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.Status = item["Status"].ToString();
                    obj.GoalSheetStart = (Convert.ToDateTime(item["GoalSheetStart"]).Year > 1900 ? Convert.ToDateTime(item["GoalSheetStart"]).ToString(DateFormat) : "");
                    obj.GoalSheetEnd = (Convert.ToDateTime(item["GoalSheetEnd"]).Year > 1900 ? Convert.ToDateTime(item["GoalSheetEnd"]).ToString(DateFormat) : "");
                    obj.Comment = item["Comment"].ToString();


                }
                obj.GoalSheetDet = GetMyGoalSheet_Det(TempModuleDataSet.Tables[1]);
                obj.CommentList = GetCommentList(TempModuleDataSet.Tables[2]);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMyGoalSheet. The query was executed :", ex.ToString(), "fnGetGoalSheet", "PMSModal", "PMSModal", "");
            }
            return obj;

        }
        private List<PMS.GoalSheet.GoalSheetDet> GetMyGoalSheet_Det(DataTable dt)
        {
            List<PMS.GoalSheet.GoalSheetDet> List = new List<PMS.GoalSheet.GoalSheetDet>();
            PMS.GoalSheet.GoalSheetDet obj = new PMS.GoalSheet.GoalSheetDet();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.GoalSheet.GoalSheetDet();
                    obj.PMS_GSDID = Convert.ToInt32(item["PMS_GSDID"].ToString());
                    obj.GoalSheetID = Convert.ToInt64(item["GoalSheetID"].ToString());
                    obj.KPAID = Convert.ToInt64(item["KPAID"].ToString());
                    obj.Area = item["Area"].ToString();
                    obj.UOMID = Convert.ToInt64(item["UOMID"].ToString());
                    obj.UOMName = item["UOMName"].ToString();
                    obj.UOMType = item["UOMType"].ToString();
                    obj.PIndicator = item["PIndicator"].ToString();
                    obj.DetRemarks = item["DetRemarks"].ToString();
                    obj.Target = item["Target"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMyGoalSheet_Det. The query was executed :", ex.ToString(), "fnGetGoalSheet", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        public PMS.GoalSheet.Add GetPMS_GoalSheet_Det(long PMS_GSDID)
        {
            PMS.GoalSheet.Add obj = new PMS.GoalSheet.Add();
            try
            {
                // code comment by shailendra 04/04/2024
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_GoalSheet_DetNew(0, PMS_GSDID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj.PMS_GSDID = Convert.ToInt32(item["PMS_GSDID"].ToString());
                    obj.GoalSheetID = Convert.ToInt64(item["GoalSheetID"].ToString());
                    obj.KPAID = Convert.ToInt64(item["KPAID"].ToString());
                    obj.Area = item["Area"].ToString();
                    obj.UOMID = Convert.ToInt64(item["UOMID"].ToString());
                    obj.UOMName = item["UOMName"].ToString();
                    obj.UOMType = item["UOMType"].ToString();
                    obj.PIndicator = item["PIndicator"].ToString();
                    obj.DetRemarks = item["DetRemarks"].ToString();
                    obj.Target = item["Target"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPMS_Question. The query was executed :", ex.ToString(), "fnGetPMS_Essential", "PMSModal", "PMSModal", "");
            }
            return obj;
        }
        public List<PMS.GoalSheet.TeamGoalSheet> GetTeamGoalSheet(long FYID, int Approved)
        {
            List<PMS.GoalSheet.TeamGoalSheet> list = new List<PMS.GoalSheet.TeamGoalSheet>();
            PMS.GoalSheet.TeamGoalSheet obj = new PMS.GoalSheet.TeamGoalSheet();
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetGoalSheet(FYID, EMPID, "TeamGoalSheet", Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.GoalSheet.TeamGoalSheet();
                    obj.PMS_GSID = Convert.ToInt32(item["PMS_GSID"].ToString());
                    obj.FYID = Convert.ToInt32(item["FYID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.Confirmer = Convert.ToInt64(item["Confirmer"].ToString());
                    obj.ConfirmerName = item["ConfirmerName"].ToString();
                    obj.HODID = Convert.ToInt64(item["HODID"].ToString());
                    obj.HODName = item["HODName"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Department = item["Department"].ToString();
                    obj.DesignationName = item["DesignationName"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.Status = item["Status"].ToString();
                    obj.GoalSheetStart = (Convert.ToDateTime(item["GoalSheetStart"]).Year > 1900 ? Convert.ToDateTime(item["GoalSheetStart"]).ToString(DateFormat) : "");
                    obj.GoalSheetEnd = (Convert.ToDateTime(item["GoalSheetEnd"]).Year > 1900 ? Convert.ToDateTime(item["GoalSheetEnd"]).ToString(DateFormat) : "");
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetGroupGoalSheet. The query was executed :", ex.ToString(), "fnGetGoalSheet", "PMSModal", "PMSModal", "");
            }
            return list;

        }
        public PMS.GoalSheet.EMPGoalSheet GetEMPGoalSheet(long PMS_GSID, string OperationType)
        {

            PMS.GoalSheet.EMPGoalSheet obj = new PMS.GoalSheet.EMPGoalSheet();

            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_EMPGoalSheet(PMS_GSID, OperationType);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj.PMS_GSID = Convert.ToInt32(item["PMS_GSID"].ToString());
                    obj.FYID = Convert.ToInt32(item["FYID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Department = item["Department"].ToString();
                    obj.EMPName = item["EMPName"].ToString();
                    obj.Confirmer = Convert.ToInt64(item["Confirmer"].ToString());
                    obj.ConfirmerName = item["ConfirmerName"].ToString();
                    obj.HODID = Convert.ToInt64(item["HODID"].ToString());
                    obj.HODName = item["HODName"].ToString();
                    obj.DesignationName = item["DesignationName"].ToString();
                    obj.Isdeleted = Convert.ToInt32(item["Isdeleted"].ToString());
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.Status = item["Status"].ToString();
                    obj.GoalSheetStart = (Convert.ToDateTime(item["GoalSheetStart"]).Year > 1900 ? Convert.ToDateTime(item["GoalSheetStart"]).ToString(DateFormat) : "");
                    obj.GoalSheetEnd = (Convert.ToDateTime(item["GoalSheetEnd"]).Year > 1900 ? Convert.ToDateTime(item["GoalSheetEnd"]).ToString(DateFormat) : "");
                    obj.Comment = item["Comment"].ToString();
                    obj.IsEdit = Convert.ToInt32(item["IsEdit"].ToString());


                }
                obj.GoalSheetDet = GetEMPGoalSheet_det(TempModuleDataSet.Tables[1]);
                obj.CommentList = GetCommentList(TempModuleDataSet.Tables[2]);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMyGoalSheet. The query was executed :", ex.ToString(), "fnGetGoalSheet", "PMSModal", "PMSModal", "");
            }
            return obj;

        }
        private List<PMS.GoalSheet.EMPGoalSheetDet> GetEMPGoalSheet_det(DataTable dt)
        {
            List<PMS.GoalSheet.EMPGoalSheetDet> List = new List<PMS.GoalSheet.EMPGoalSheetDet>();
            PMS.GoalSheet.EMPGoalSheetDet obj = new PMS.GoalSheet.EMPGoalSheetDet();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.GoalSheet.EMPGoalSheetDet();
                    obj.PMS_GSDID = Convert.ToInt32(item["PMS_GSDID"].ToString());
                    obj.GoalSheetID = Convert.ToInt64(item["GoalSheetID"].ToString());
                    obj.KPAID = Convert.ToInt64(item["KPAID"].ToString());
                    obj.Area = item["Area"].ToString();
                    obj.UOMID = Convert.ToInt64(item["UOMID"].ToString());
                    obj.UOMName = item["UOMName"].ToString();
                    obj.UOMType = item["UOMType"].ToString();
                    obj.PIndicator = item["PIndicator"].ToString();
                    obj.DetRemarks = item["DetRemarks"].ToString();
                    obj.Target = item["Target"].ToString();
                    obj.Weight = Convert.ToInt32(item["Weight"].ToString());
                    obj.RPIndicator= item["PIndicator"].ToString();
                    obj.RTarget= item["Target"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEMPGoalSheet_det. The query was executed :", ex.ToString(), "fnGetGoalSheet", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.GoalSheet.CommentList> GetCommentList(DataTable dt)
        {
            List<PMS.GoalSheet.CommentList> List = new List<PMS.GoalSheet.CommentList>();
            PMS.GoalSheet.CommentList obj = new PMS.GoalSheet.CommentList();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.GoalSheet.CommentList();
                    obj.PMS_CommentID = Convert.ToInt32(item["PMS_CommentID"].ToString());
                    obj.createdby = Convert.ToInt64(item["createdby"].ToString());
                    obj.Comment = item["Comment"].ToString();
                    obj.Doctype = item["Doctype"].ToString();
                    obj.Name = item["Name"].ToString();
                    obj.Email = item["Email"].ToString();
                    obj.createdat = (Convert.ToDateTime(item["createdat"]).Year > 1900 ? Convert.ToDateTime(item["createdat"]).ToString(DateFormat) : "");
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetCommentList. The query was executed :", ex.ToString(), "fnGetGoalSheet", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        public List<PMS.GroupGoalSheet.List> GetGroupGoalSheet(long FYID, int Approved)
        {
            List<PMS.GroupGoalSheet.List> list = new List<PMS.GroupGoalSheet.List>();
            PMS.GroupGoalSheet.List obj = new PMS.GroupGoalSheet.List();
            long EMPID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetGoalSheet(FYID, EMPID, "GroupGoalSheet", Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.GroupGoalSheet.List();
                    obj.PMS_GSID = Convert.ToInt32(item["PMS_GSID"].ToString());
                    obj.FYID = Convert.ToInt32(item["FYID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.Confirmer = Convert.ToInt64(item["Confirmer"].ToString());
                    obj.ConfirmerName = item["ConfirmerName"].ToString();
                    obj.HODID = Convert.ToInt64(item["HODID"].ToString());
                    obj.HODName = item["HODName"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Department = item["Department"].ToString();
                    obj.DesignationName = item["DesignationName"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.Status = item["Status"].ToString();
                    obj.GoalSheetStart = (Convert.ToDateTime(item["GoalSheetStart"]).Year > 1900 ? Convert.ToDateTime(item["GoalSheetStart"]).ToString(DateFormat) : "");
                    obj.GoalSheetEnd = (Convert.ToDateTime(item["GoalSheetEnd"]).Year > 1900 ? Convert.ToDateTime(item["GoalSheetEnd"]).ToString(DateFormat) : "");
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetGroupGoalSheet. The query was executed :", ex.ToString(), "fnGetGoalSheet", "PMSModal", "PMSModal", "");
            }
            return list;

        }
        public List<PMS.Feedback.List> GetFeedbackList(long FYID)
        {
            List<PMS.Feedback.List> list = new List<PMS.Feedback.List>();
            PMS.Feedback.List obj = new PMS.Feedback.List();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_CommentRequest(FYID, 0);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.Feedback.List();
                    obj.PMS_EID = Convert.ToInt32(item["PMS_EID"].ToString());
                    obj.FYID = Convert.ToInt32(item["FYID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.EMPID = Convert.ToInt32(item["EMPID"].ToString());
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.EMPName = item["EMPName"].ToString();
                    obj.DocType = item["DocType"].ToString();
                    obj.HODID = Convert.ToInt64(item["HODID"].ToString());
                    obj.HODName = item["HODName"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Department = item["Department"].ToString();
                    obj.DesignationName = item["DesignationName"].ToString();
                    obj.Completed = Convert.ToInt32(item["completed"].ToString());
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFeedbackList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return list;

        }
        public PMS.Feedback.Add GetFeedback(long FYID, long EMPID)
        {
            PMS.Feedback.Add obj = new PMS.Feedback.Add();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_CommentRequest(FYID, EMPID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj.PMS_EID = Convert.ToInt32(item["PMS_EID"].ToString());
                    obj.FYID = Convert.ToInt32(item["FYID"].ToString());
                    obj.FYName = item["FYName"].ToString();
                    obj.EMPID = Convert.ToInt32(item["EMPID"].ToString());
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.EMPName = item["EMPName"].ToString();
                    obj.DocType = item["DocType"].ToString();
                    obj.HODID = Convert.ToInt64(item["HODID"].ToString());
                    obj.HODName = item["HODName"].ToString();
                    obj.HODCode = item["HODCode"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Department = item["Department"].ToString();
                    obj.DesignationName = item["DesignationName"].ToString();
                }

                obj.QuestionsList = GetQuestionList(TempModuleDataSet.Tables[1]);
                if (obj.QuestionsList.Count > 0)
                {
                    obj.FinalComment = obj.QuestionsList.Select(x => x.FinalComment).FirstOrDefault();
                    obj.OverAllRating = obj.QuestionsList.Select(x => x.OverAllRating).FirstOrDefault();
                    obj.IsBtnVisible = (obj.QuestionsList.Any(x => x.isdeleted == 0) ? false : true);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFeedback. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return obj;

        }
        private List<PMS.Feedback.Questions> GetQuestionList(DataTable dt)
        {
            List<PMS.Feedback.Questions> List = new List<PMS.Feedback.Questions>();
            PMS.Feedback.Questions obj = new PMS.Feedback.Questions();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.Feedback.Questions();
                    obj.PMS_QAID = Convert.ToInt64(item["PMS_QAID"].ToString());
                    obj.Q = item["Question"].ToString();
                    obj.A = item["Answer"].ToString();
                    obj.FinalComment = item["FinalComment"].ToString();
                    obj.OverAllRating = item["FinalRating"].ToString();
                    obj.isdeleted = Convert.ToInt32(item["isdeleted"].ToString());
                    obj.GivenBy = Convert.ToInt32(item["GivenBy"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetQuestionList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        public PMS.SelfAppraisal GetSelfAppraisal(long FYID)
        {
            PMS.SelfAppraisal obj = new PMS.SelfAppraisal();
            DataSet DST = Common_SPU.fnGetPMS_SelfAppraisal(FYID);
            //  0 Get Appraisal
            //  1 Get All KPA
            //  2 Get Question
            //  3 Get Attachment
            //  4 Get Training
            //  5 Get Comment
            obj.Status = Convert.ToBoolean(DST.Tables[0].Rows[0]["Status"].ToString());
            obj.StatusMessage = DST.Tables[0].Rows[0]["StatusMessage"].ToString();
            if (obj.Status)
            {
                obj.isdeleted = Convert.ToInt32(DST.Tables[0].Rows[0]["isdeleted"].ToString());
                obj.Approved = Convert.ToInt32(DST.Tables[0].Rows[0]["Approved"].ToString());

                obj.StatusMessage = DST.Tables[0].Rows[0]["StatusMessage"].ToString();

                obj.PMS_AID = Convert.ToInt64(DST.Tables[0].Rows[0]["PMS_AID"].ToString());
                obj.FYID = Convert.ToInt64(DST.Tables[0].Rows[0]["FYID"].ToString());
                obj.EMPID = Convert.ToInt64(DST.Tables[0].Rows[0]["EMPID"].ToString());
                obj.EMPCode = DST.Tables[0].Rows[0]["EMPCode"].ToString();
                obj.EMPName = DST.Tables[0].Rows[0]["EMPName"].ToString();
              //  obj.Reason = DST.Tables[0].Rows[0]["Reason"]?.ToString() ?? "Reason";
              if(obj.Approved==2)
                {
                    obj.Reason = DST.Tables[0].Rows[0]["Reason"]?.ToString();
                }

                if (obj.isdeleted != 0)
                {
                    obj.IsBtnVisible = true;
                }
                else if (obj.Approved == 1 || obj.Approved == 2)
                {
                    obj.IsBtnVisible = true;
                }


                obj.KPAL = GetAppraisalKPAList(DST.Tables[1]);
                obj.QAL = GetAppraisalQAList(DST.Tables[2]);
                obj.AttachmentsL = GetAppraisalAttachmentList(DST.Tables[3]);
                obj.TrainingL = GetAppraisalTrainingList(DST.Tables[4]);
                obj.CommentsL = GetAppraisalCommentList(DST.Tables[5]);
                obj.TrainingType = GetTrainingType(DST.Tables[6]);
                if (obj.CommentsL.Count == 0)
                {

                    PMS.SelfAppraisal.Comments C1 = new PMS.SelfAppraisal.Comments();
                    C1.Doctype = "Self Comment 1";

                    obj.CommentsL.Add(C1);
                }
                if (obj.Approved == 1)
                {
                    PMS.SelfAppraisal.Comments C2 = new PMS.SelfAppraisal.Comments();
                    C2.Doctype = "Self Comment 2";

                    obj.CommentsL.Add(C2);
                }
                if (obj.TrainingL.Count < 5)
                {
                    obj.TrainingL.Add(new PMS.SelfAppraisal.Training());
                    //obj.TrainingL[obj.TrainingL.Count-1].Isdeleted = 2;
                }

                if (obj.AttachmentsL.Count == 0)
                {
                    PMS.SelfAppraisal.Attachments Aobj = new PMS.SelfAppraisal.Attachments();
                    Aobj.FileName = "Self Appraisal Document";
                    obj.AttachmentsL.Add(Aobj);
                }

            }
            return obj;
        }
        private List<PMS.SelfAppraisal.KPA> GetAppraisalKPAList(DataTable dt)
        {
            List<PMS.SelfAppraisal.KPA> List = new List<PMS.SelfAppraisal.KPA>();
            PMS.SelfAppraisal.KPA obj = new PMS.SelfAppraisal.KPA();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.SelfAppraisal.KPA();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.PMS_ADID = Convert.ToInt64(item["PMS_ADID"].ToString());
                    obj.PMS_AID = Convert.ToInt64(item["PMS_AID"].ToString());
                    obj.KPAID = Convert.ToInt64(item["KPAID"].ToString());

                    obj.GoalSheetID = Convert.ToInt64(item["GoalSheetID"].ToString());
                    obj.GoalSheet_DetID = Convert.ToInt64(item["GoalSheet_DetID"].ToString());

                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.Doctype = item["Doctype"].ToString();
                    obj.KPA_Area = item["KPA_Area"].ToString();
                    obj.KPA_PIndicator = item["KPA_PIndicator"].ToString();
                    obj.KPA_IsMonitoring = item["KPA_IsMonitoring"].ToString();
                    obj.KPA_IsMandatory = item["KPA_IsMandatory"].ToString();
                    obj.kPA_Target = item["kPA_Target"].ToString();
                    obj.KPA_IncType = item["KPA_IncType"].ToString();
                    obj.KPA_AutoRating = item["KPA_AutoRating"].ToString();
                    obj.KPA_Weight = item["KPA_Weight"].ToString();
                    obj.KPA_TargetAchieved =Convert.ToString(item["KPA_TargetAchieved"]);
                    
                    obj.UOMID = Convert.ToInt32(item["UOMID"].ToString());
                    obj.UOM_Name = Convert.ToString(item["UOM_Name"]);
                    //obj.UOM_Type = Convert.ToString(item["UOM_Type"]);                    
                    obj.Self_Achievement = Convert.ToString(item["Self_Achievement"]);
                    obj.Self_Comment = Convert.ToString(item["Self_Comment"]);
                    obj.HOD_Score = Convert.ToString(item["HOD_Score"]);
                    obj.HOD_Comment = Convert.ToString(item["HOD_Comment"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalTrainingList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.SelfAppraisal.Training> GetAppraisalTrainingList(DataTable dt)
        {
            List<PMS.SelfAppraisal.Training> List = new List<PMS.SelfAppraisal.Training>();
            PMS.SelfAppraisal.Training obj = new PMS.SelfAppraisal.Training();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.SelfAppraisal.Training();
                    obj.PMS_ADID = Convert.ToInt64(item["PMS_ADID"].ToString());
                    obj.TrainingTypeID = Convert.ToInt32(item["TrainingTypeID"].ToString());
                    obj.TrainingType = item["TrainingType"].ToString();
                    obj.TrainingRemarks = item["TrainingRemarks"].ToString();
                    obj.Isdeleted = Convert.ToInt32(item["isdeleted"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalTrainingList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.SelfAppraisal.QA> GetAppraisalQAList(DataTable dt)
        {
            List<PMS.SelfAppraisal.QA> List = new List<PMS.SelfAppraisal.QA>();
            PMS.SelfAppraisal.QA obj = new PMS.SelfAppraisal.QA();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.SelfAppraisal.QA();
                    obj.PMS_QAID = Convert.ToInt64(item["PMS_QAID"].ToString());
                    obj.Question = item["Question"].ToString();
                    obj.Answer = item["Answer"].ToString();
                    obj.QuestionFor = item["QuestionFor"].ToString();
                    obj.FinalComment = item["FinalComment"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalQAList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.SelfAppraisal.Comments> GetAppraisalCommentList(DataTable dt)
        {
            List<PMS.SelfAppraisal.Comments> List = new List<PMS.SelfAppraisal.Comments>();
            PMS.SelfAppraisal.Comments obj = new PMS.SelfAppraisal.Comments();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.SelfAppraisal.Comments();
                    obj.PMS_CommentID = Convert.ToInt64(item["PMS_CommentID"].ToString());
                    obj.Comment = item["Comment"].ToString();
                    obj.Doctype = item["Doctype"].ToString();
                    obj.TableName = item["TableName"].ToString();
                    obj.Name = item["Name"].ToString();
                    obj.Email = item["Email"].ToString();
                    obj.createdat = (Convert.ToDateTime(item["createdat"]).Year > 1900 ? Convert.ToDateTime(item["createdat"]).ToString(DateFormat) : "");
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalCommentList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.SelfAppraisal.Attachments> GetAppraisalAttachmentList(DataTable dt)
        {
            List<PMS.SelfAppraisal.Attachments> List = new List<PMS.SelfAppraisal.Attachments>();
            PMS.SelfAppraisal.Attachments obj = new PMS.SelfAppraisal.Attachments();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.SelfAppraisal.Attachments();
                    obj.AttachmentID = Convert.ToInt64(item["AttachmentID"].ToString());
                    obj.FileName = item["FileName"].ToString();
                    obj.Descrip = item["Descrip"].ToString();
                    obj.URL = item["URL"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalAttachmentList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.TrainingType> GetTrainingType(DataTable dt)
        {
            List<PMS.TrainingType> List = new List<PMS.TrainingType>();
            PMS.TrainingType obj = new PMS.TrainingType();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.TrainingType();
                    obj.ID = Convert.ToInt64(item["CBTID"].ToString());
                    obj.Type = item["Type"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTrainingType. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        public List<PMS.TeamAppraisal.List> GetTeamAppraisalList(long FYID, string Doctype)
        {
            List<PMS.TeamAppraisal.List> list = new List<PMS.TeamAppraisal.List>();
            PMS.TeamAppraisal.List obj = new PMS.TeamAppraisal.List();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_TeamAppraisal_List(FYID, Doctype);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.TeamAppraisal.List();
                    obj.PMS_AID = Convert.ToInt32(item["PMS_AID"].ToString());
                    obj.FYID = Convert.ToInt32(item["FYID"].ToString());
                    obj.EMPID = Convert.ToInt32(item["EMPID"].ToString());
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.EMPName = item["EMPName"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Department = item["Department"].ToString();
                    obj.DesignationName = item["DesignationName"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.SubmitedDate = (Convert.ToDateTime(item["SubmitedDate"]).Year > 1900 ? Convert.ToDateTime(item["SubmitedDate"]).ToString(DateFormat) : "");


                    obj.EMPName = item["EMPName"].ToString();
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFeedbackList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return list;

        }
        public PMS.TeamAppraisal.Add GetTeamAppraisal(long PMS_AID)
        {
            PMS.TeamAppraisal.Add obj = new PMS.TeamAppraisal.Add();
            DataSet DST = Common_SPU.fnGetPMS_TeamAppraisal(PMS_AID);
            //  0 Get Appraisal
            //  1 Get All KPA
            //  2 Get Question
            //  3 Get Attachment
            //  4 Get Training
            //  5 Get Comment
            obj.Status = Convert.ToBoolean(DST.Tables[0].Rows[0]["Status"].ToString());
            obj.StatusMessage = DST.Tables[0].Rows[0]["StatusMessage"].ToString();
            if (obj.Status)
            {

                obj.Approved = Convert.ToInt32(DST.Tables[0].Rows[0]["Approved"].ToString());
                obj.StatusMessage = DST.Tables[0].Rows[0]["StatusMessage"].ToString();
                obj.PMS_AID = Convert.ToInt64(DST.Tables[0].Rows[0]["PMS_AID"].ToString());
                obj.FYID = Convert.ToInt64(DST.Tables[0].Rows[0]["FYID"].ToString());
                obj.EMPID = Convert.ToInt64(DST.Tables[0].Rows[0]["EMPID"].ToString());
                obj.EMPCode = DST.Tables[0].Rows[0]["EMPCode"].ToString();
                obj.EMPName = DST.Tables[0].Rows[0]["EMPName"].ToString();
                obj.ApproveStatus = DST.Tables[0].Rows[0]["ApproveStatus"].ToString();
                obj.LocationName = DST.Tables[0].Rows[0]["LocationName"].ToString();
                obj.Department = DST.Tables[0].Rows[0]["Department"].ToString();
                obj.DesignationName = DST.Tables[0].Rows[0]["DesignationName"].ToString();
                obj.FYName = DST.Tables[0].Rows[0]["FYName"].ToString();
                obj.Team_Score = Convert.ToInt32(DST.Tables[0].Rows[0]["Team_Score"].ToString());
                obj.SystemScore = Convert.ToDouble(DST.Tables[0].Rows[0]["SystemScore"].ToString());
                obj.TeamRecommendation = DST.Tables[0].Rows[0]["TeamRecommendation"].ToString();

                if (obj.Approved == 0 || obj.Approved == 3)
                {
                    obj.IsBtnVisible = true;
                }
                obj.KPAL = GetTeamKPAList(DST.Tables[1]);
                obj.QAL = GetTeamQAList(DST.Tables[2]);
                obj.AttachmentsL = GetTeamAttachmentList(DST.Tables[3]);
                obj.TrainingL = GetTeamTrainingList(DST.Tables[4]);
                obj.CommentsL = GetTeamCommentList(DST.Tables[5]);
                obj.TrainingType = GetTrainingType(DST.Tables[6]);
                if (obj.TrainingL.Where(x => x.isdeleted == 0).Count() < 5)
                {
                    PMS.TeamAppraisal.Training aaobj = new PMS.TeamAppraisal.Training();
                    aaobj.Doctype = "New";
                    obj.TrainingL.Add(aaobj);
                }

                List<PMS.TeamAppraisal.TeamComment> CobjList = new List<PMS.TeamAppraisal.TeamComment>();
                if (obj.Approved == 0)
                {
                    PMS.TeamAppraisal.TeamComment C1 = new PMS.TeamAppraisal.TeamComment();
                    C1.Doctype = "Team Comment 1";
                    if (obj.CommentsL.Any(x => x.Doctype == "Team" && x.TableName == C1.Doctype))
                    {
                        C1.PMS_CommentID = obj.CommentsL.Where(x => x.Doctype == "Team" && x.TableName == C1.Doctype).Select(x => x.PMS_CommentID).LastOrDefault();
                        C1.Comment = obj.CommentsL.Where(x => x.Doctype == "Team" && x.TableName == C1.Doctype).Select(x => x.Comment).LastOrDefault();
                    }
                    CobjList.Add(C1);
                }
                else if (obj.Approved == 3)
                {
                    PMS.TeamAppraisal.TeamComment C2 = new PMS.TeamAppraisal.TeamComment();
                    C2.PMS_CommentID = obj.CommentsL.Where(n => n.Doctype == "Team").ToList()[0].PMS_CommentID;
                    C2.Comment = obj.CommentsL.Where(n => n.Doctype == "Team").ToList()[0].Comment;
                    C2.Doctype = "Team Comment 1";
                    CobjList.Add(C2);
                    C2 = new PMS.TeamAppraisal.TeamComment();
                    C2.Doctype = "Team Comment 2";
                    if (obj.CommentsL.Any(x => x.Doctype == "Team" && x.TableName == C2.Doctype))
                    {
                        C2.PMS_CommentID = obj.CommentsL.Where(x => x.Doctype == "Team" && x.TableName == C2.Doctype).Select(x => x.PMS_CommentID).LastOrDefault();
                        C2.Comment = obj.CommentsL.Where(x => x.Doctype == "Team" && x.TableName == C2.Doctype).Select(x => x.Comment).LastOrDefault();
                    }
                    CobjList.Add(C2);
                }
                obj.TeamCommentL = CobjList;
            }
            return obj;
        }
        private List<PMS.TeamAppraisal.KPA> GetTeamKPAList(DataTable dt)
        {
            List<PMS.TeamAppraisal.KPA> List = new List<PMS.TeamAppraisal.KPA>();
            PMS.TeamAppraisal.KPA obj = new PMS.TeamAppraisal.KPA();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.TeamAppraisal.KPA();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.PMS_ADID = Convert.ToInt64(item["PMS_ADID"].ToString());
                    obj.PMS_AID = Convert.ToInt64(item["PMS_AID"].ToString());
                    obj.KPAID = Convert.ToInt64(item["KPAID"].ToString());

                    obj.GoalSheetID = Convert.ToInt64(item["GoalSheetID"].ToString());
                    obj.GoalSheet_DetID = Convert.ToInt64(item["GoalSheet_DetID"].ToString());

                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.Doctype = item["Doctype"].ToString();
                    obj.KPA_Area = item["KPA_Area"].ToString();
                    obj.KPA_PIndicator = item["KPA_PIndicator"].ToString();
                    obj.KPA_IsMonitoring = item["KPA_IsMonitoring"].ToString();
                    obj.KPA_IsMandatory = item["KPA_IsMandatory"].ToString();
                    obj.kPA_Target = item["kPA_Target"].ToString();
                    obj.KPA_IncType = item["KPA_IncType"].ToString();
                    obj.KPA_AutoRating = item["KPA_AutoRating"].ToString();
                    obj.KPA_Weight = item["KPA_Weight"].ToString();

                    obj.UOMID = Convert.ToInt32(item["UOMID"].ToString());
                    obj.UOM_Name = item["UOM_Name"].ToString();
                    obj.Self_Achievement = item["Self_Achievement"].ToString();
                    obj.Self_Comment = item["Self_Comment"].ToString();
                    obj.HOD_Comment = item["HOD_Comment"].ToString();
                    obj.HOD_Score = item["HOD_Score"].ToString();
                    obj.KPA_TargetAchieved = Convert.ToString(item["KPA_TargetAchieved"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalTrainingList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.TeamAppraisal.Training> GetTeamTrainingList(DataTable dt)
        {
            List<PMS.TeamAppraisal.Training> List = new List<PMS.TeamAppraisal.Training>();
            PMS.TeamAppraisal.Training obj = new PMS.TeamAppraisal.Training();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.TeamAppraisal.Training();
                    obj.PMS_ADID = Convert.ToInt64(item["PMS_ADID"].ToString());
                    obj.TrainingTypeID = Convert.ToInt32(item["TrainingTypeID"].ToString());
                    obj.TrainingType = item["TrainingType"].ToString();
                    obj.TrainingRemarks = item["TrainingRemarks"].ToString();
                    obj.RejectReason = item["HOD_Comment"].ToString();
                    obj.isdeleted = Convert.ToInt32(item["isdeleted"].ToString());
                    obj.Doctype = item["Doctype"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalTrainingList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.TeamAppraisal.QA> GetTeamQAList(DataTable dt)
        {


            List<PMS.TeamAppraisal.QA> TempList = new List<PMS.TeamAppraisal.QA>();
            PMS.TeamAppraisal.QA Tempobj = new PMS.TeamAppraisal.QA();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    Tempobj = new PMS.TeamAppraisal.QA();
                    Tempobj.PMS_QAID = Convert.ToInt64(item["PMS_QAID"].ToString());
                    Tempobj.Question = item["Question"].ToString();
                    Tempobj.Answer = item["Answer"].ToString();

                    Tempobj.Doctype = item["Doctype"].ToString();
                    Tempobj.QuestionFor = item["QuestionFor"].ToString();
                    Tempobj.FinalComment = item["FinalComment"].ToString();
                    TempList.Add(Tempobj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalQAList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return TempList;

        }
        private List<PMS.TeamAppraisal.Comments> GetTeamCommentList(DataTable dt)
        {
            List<PMS.TeamAppraisal.Comments> List = new List<PMS.TeamAppraisal.Comments>();
            PMS.TeamAppraisal.Comments obj = new PMS.TeamAppraisal.Comments();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.TeamAppraisal.Comments();
                    obj.Doctype = item["Doctype"].ToString();
                    obj.TableName = item["TableName"].ToString();
                    obj.PMS_CommentID = Convert.ToInt64(item["PMS_CommentID"].ToString());
                    obj.Comment = item["Comment"].ToString();
                    obj.Name = item["Name"].ToString();

                    obj.Email = item["Email"].ToString();
                    obj.createdat = (Convert.ToDateTime(item["createdat"]).Year > 1900 ? Convert.ToDateTime(item["createdat"]).ToString(DateFormat) : "");
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalCommentList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.TeamAppraisal.Attachments> GetTeamAttachmentList(DataTable dt)
        {
            List<PMS.TeamAppraisal.Attachments> List = new List<PMS.TeamAppraisal.Attachments>();
            PMS.TeamAppraisal.Attachments obj = new PMS.TeamAppraisal.Attachments();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.TeamAppraisal.Attachments();
                    obj.AttachmentID = Convert.ToInt64(item["AttachmentID"].ToString());
                    obj.FileName = item["FileName"].ToString();
                    obj.Descrip = item["Descrip"].ToString();
                    obj.URL = item["URL"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalAttachmentList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        public List<PMS.GroupAppraisal.List> GetGroupAppraisalList(long FYID, string Doctype)
        {
            List<PMS.GroupAppraisal.List> list = new List<PMS.GroupAppraisal.List>();
            PMS.GroupAppraisal.List obj = new PMS.GroupAppraisal.List();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_GroupAppraisal_List(FYID, Doctype);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PMS.GroupAppraisal.List();
                    obj.PMS_AID = Convert.ToInt32(item["PMS_AID"].ToString());
                    obj.FYID = Convert.ToInt32(item["FYID"].ToString());
                    obj.EMPID = Convert.ToInt32(item["EMPID"].ToString());
                    obj.FeedbackReceived = Convert.ToInt32(item["FeedbackReceived"].ToString());
                    obj.TotalFeedback = Convert.ToInt32(item["TotalFeedback"].ToString());
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.EMPName = item["EMPName"].ToString();
                    obj.HODCode = item["HODCode"].ToString();
                    obj.HODName = item["HODName"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Department = item["Department"].ToString();
                    obj.DesignationName = item["DesignationName"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    obj.SubmitedDate = (Convert.ToDateTime(item["SubmitedDate"]).Year > 1900 ? Convert.ToDateTime(item["SubmitedDate"]).ToString(DateFormat) : "");
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFeedbackList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return list;

        }
        public List<PMS.CMCAppraisal.List> GetCMCAppraisalList(long FYID)
        {
            List<PMS.CMCAppraisal.List> list = new List<PMS.CMCAppraisal.List>();
            PMS.CMCAppraisal.List obj = new PMS.CMCAppraisal.List();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPMS_CMCAppraisal_List(FYID);
                if (TempModuleDataSet != null)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {
                        obj = new PMS.CMCAppraisal.List();
                        obj.PMS_AID = Convert.ToInt32(item["PMS_AID"].ToString());
                        obj.FYID = Convert.ToInt32(item["FYID"].ToString());
                        obj.EMPID = Convert.ToInt32(item["EMPID"].ToString());
                        obj.EMPCode = item["EMPCode"].ToString();
                        obj.EMPName = item["EMPName"].ToString();
                        obj.ConfirmerName = item["ConfirmerName"].ToString();
                        obj.HODCode = item["HODCode"].ToString();
                        obj.HODName = item["HODName"].ToString();
                        obj.Status = item["Status"].ToString();
                        obj.LocationName = item["LocationName"].ToString();
                        obj.Department = item["Department"].ToString();
                        obj.DesignationName = item["DesignationName"].ToString();
                        obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                        obj.SubmitedDate = (Convert.ToDateTime(item["SubmitedDate"]).Year > 1900 ? Convert.ToDateTime(item["SubmitedDate"]).ToString(DateFormat) : "");
                        list.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetFeedbackList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return list;

        }
        public PMS.GroupAppraisal.Add GetGroupAppraisal(long PMS_AID)
        {
            PMS.GroupAppraisal.Add obj = new PMS.GroupAppraisal.Add();
            DataSet DST = Common_SPU.fnGetPMS_GroupAppraisal(PMS_AID);
            //  0 Get Appraisal
            //  1 Get All KPA
            //  2 Get All Question
            //  3 Get Attachment
            //  4 Get Training
            //  5 Get Comment
            obj.Status = Convert.ToBoolean(DST.Tables[0].Rows[0]["Status"].ToString());
            obj.StatusMessage = DST.Tables[0].Rows[0]["StatusMessage"].ToString();
            if (obj.Status)
            {
                obj.Approved = Convert.ToInt32(DST.Tables[0].Rows[0]["Approved"].ToString());
                obj.ApproveStatus = DST.Tables[0].Rows[0]["ApproveStatus"].ToString();
                obj.StatusMessage = DST.Tables[0].Rows[0]["StatusMessage"].ToString();
                obj.PMS_AID = Convert.ToInt64(DST.Tables[0].Rows[0]["PMS_AID"].ToString());
                obj.FYID = Convert.ToInt64(DST.Tables[0].Rows[0]["FYID"].ToString());
                obj.EMPID = Convert.ToInt64(DST.Tables[0].Rows[0]["EMPID"].ToString());
                obj.EMPCode = DST.Tables[0].Rows[0]["EMPCode"].ToString();
                obj.EMPName = DST.Tables[0].Rows[0]["EMPName"].ToString();
                obj.LocationName = DST.Tables[0].Rows[0]["LocationName"].ToString();
                obj.Department = DST.Tables[0].Rows[0]["Department"].ToString();
                obj.DesignationName = DST.Tables[0].Rows[0]["DesignationName"].ToString();
                obj.FYName = DST.Tables[0].Rows[0]["FYName"].ToString();
                obj.Group_Score = Convert.ToInt32(DST.Tables[0].Rows[0]["Group_Score"].ToString());
                obj.Group_Comment = DST.Tables[0].Rows[0]["Group_Comment"].ToString();
                obj.Team_Score = Convert.ToInt32(DST.Tables[0].Rows[0]["Team_Score"].ToString());
                obj.TeamRecommendation = DST.Tables[0].Rows[0]["TeamRecommendation"].ToString();
                obj.AppraiserName = DST.Tables[0].Rows[0]["AppraiserName"].ToString();
                obj.CommentorsName = DST.Tables[0].Rows[0]["CommentorsName"].ToString();
                obj.SystemScore = Convert.ToDouble(DST.Tables[0].Rows[0]["SystemScore"].ToString());
                obj.KPAL = GetGroupKPAList(DST.Tables[1]);
                obj.QAL = GetGroupQAList(DST.Tables[2]);
                obj.AttachmentsL = GetGroupAttachmentList(DST.Tables[3]);
                obj.TrainingL = GetGroupTrainingList(DST.Tables[4]);
                obj.CommentsL = GetGroupCommentList(DST.Tables[5]);


                if (obj.Approved == 4)
                {
                    obj.IsBtnVisible = true;
                }


            }
            return obj;
        }
        private List<PMS.GroupAppraisal.KPA> GetGroupKPAList(DataTable dt)
        {
            List<PMS.GroupAppraisal.KPA> List = new List<PMS.GroupAppraisal.KPA>();
            PMS.GroupAppraisal.KPA obj = new PMS.GroupAppraisal.KPA();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.GroupAppraisal.KPA();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.PMS_ADID = Convert.ToInt64(item["PMS_ADID"].ToString());
                    obj.PMS_AID = Convert.ToInt64(item["PMS_AID"].ToString());
                    obj.KPAID = Convert.ToInt64(item["KPAID"].ToString());

                    obj.GoalSheetID = Convert.ToInt64(item["GoalSheetID"].ToString());
                    obj.GoalSheet_DetID = Convert.ToInt64(item["GoalSheet_DetID"].ToString());

                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.Doctype = item["Doctype"].ToString();
                    obj.KPA_Area = item["KPA_Area"].ToString();
                    obj.KPA_PIndicator = item["KPA_PIndicator"].ToString();
                    obj.KPA_IsMonitoring = item["KPA_IsMonitoring"].ToString();
                    obj.KPA_IsMandatory = item["KPA_IsMandatory"].ToString();
                    obj.kPA_Target = item["kPA_Target"].ToString();
                    obj.KPA_IncType = item["KPA_IncType"].ToString();
                    obj.KPA_AutoRating = item["KPA_AutoRating"].ToString();
                    obj.KPA_Weight = item["KPA_Weight"].ToString();

                    obj.UOMID = Convert.ToInt32(item["UOMID"].ToString());
                    obj.UOM_Name = item["UOM_Name"].ToString();
                    obj.Self_Achievement = item["Self_Achievement"].ToString();
                    obj.Self_Comment = item["Self_Comment"].ToString();
                    obj.HOD_Comment = item["HOD_Comment"].ToString();
                    obj.HOD_Score = item["HOD_Score"].ToString();
                    obj.KPA_TargetAchieved = Convert.ToString(item["KPA_TargetAchieved"]);

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalTrainingList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.GroupAppraisal.Training> GetGroupTrainingList(DataTable dt)
        {
            List<PMS.GroupAppraisal.Training> List = new List<PMS.GroupAppraisal.Training>();
            PMS.GroupAppraisal.Training obj = new PMS.GroupAppraisal.Training();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.GroupAppraisal.Training();
                    obj.PMS_ADID = Convert.ToInt64(item["PMS_ADID"].ToString());
                    obj.TrainingTypeID = Convert.ToInt32(item["TrainingTypeID"].ToString());
                    obj.TrainingType = item["TrainingType"].ToString();
                    obj.TrainingRemarks = item["TrainingRemarks"].ToString();
                    obj.isdeleted = Convert.ToInt32(item["isdeleted"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalTrainingList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.GroupAppraisal.QA> GetGroupQAList(DataTable dt)
        {


            List<PMS.GroupAppraisal.QA> TempList = new List<PMS.GroupAppraisal.QA>();
            PMS.GroupAppraisal.QA Tempobj = new PMS.GroupAppraisal.QA();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    Tempobj = new PMS.GroupAppraisal.QA();
                    Tempobj.PMS_QAID = Convert.ToInt64(item["PMS_QAID"].ToString());
                    Tempobj.Question = item["Question"].ToString();
                    Tempobj.Answer = item["Answer"].ToString();
                    Tempobj.FinalComment = item["FinalComment"].ToString();
                    Tempobj.GivenBy = Convert.ToInt64(item["GivenBy"].ToString());
                    Tempobj.EMPName = item["EMPName"].ToString();
                    Tempobj.EMPCode = item["EMPCode"].ToString();
                    Tempobj.Department = item["Department"].ToString();
                    Tempobj.DesignationName = item["DesignationName"].ToString();
                    Tempobj.LocationName = item["LocationName"].ToString();
                    Tempobj.Doctype = item["Doctype"].ToString().ToLower();
                    Tempobj.FinalRating = item["FinalRating"].ToString().ToLower();
                    Tempobj.QuestionFor = item["QuestionFor"].ToString();
                    Tempobj.EMPdetails = Tempobj.EMPName + "(" + Tempobj.EMPCode + ") ," + Tempobj.DesignationName;
                    TempList.Add(Tempobj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalQAList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return TempList;

        }
        private List<PMS.GroupAppraisal.Comments> GetGroupCommentList(DataTable dt)
        {
            List<PMS.GroupAppraisal.Comments> List = new List<PMS.GroupAppraisal.Comments>();
            PMS.GroupAppraisal.Comments obj = new PMS.GroupAppraisal.Comments();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.GroupAppraisal.Comments();
                    obj.Doctype = item["Doctype"].ToString();

                    obj.Doctype = item["Doctype"].ToString();
                    obj.TableName = item["TableName"].ToString();

                    obj.PMS_CommentID = Convert.ToInt64(item["PMS_CommentID"].ToString());
                    obj.Comment = item["Comment"].ToString();
                    obj.Name = item["Name"].ToString();

                    obj.Email = item["Email"].ToString();
                    obj.createdat = (Convert.ToDateTime(item["createdat"]).Year > 1900 ? Convert.ToDateTime(item["createdat"]).ToString(DateFormat) : "");
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalCommentList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.GroupAppraisal.Attachments> GetGroupAttachmentList(DataTable dt)
        {
            List<PMS.GroupAppraisal.Attachments> List = new List<PMS.GroupAppraisal.Attachments>();
            PMS.GroupAppraisal.Attachments obj = new PMS.GroupAppraisal.Attachments();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.GroupAppraisal.Attachments();
                    obj.AttachmentID = Convert.ToInt64(item["AttachmentID"].ToString());
                    obj.FileName = item["FileName"].ToString();
                    obj.Descrip = item["Descrip"].ToString();
                    obj.URL = item["URL"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalAttachmentList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        public PMS.CMCAppraisal.Add GetCMCAppraisal(long PMS_AID)
        {
            PMS.CMCAppraisal.Add obj = new PMS.CMCAppraisal.Add();
            DataSet DST = Common_SPU.fnGetPMS_CMCAppraisal(PMS_AID);
            //  0 Get Appraisal
            //  1 Get All KPA
            //  2 Get All Question
            //  3 Get Attachment
            //  4 Get Training
            //  5 Get Comment
            obj.Status = Convert.ToBoolean(DST.Tables[0].Rows[0]["Status"].ToString());
            obj.StatusMessage = DST.Tables[0].Rows[0]["StatusMessage"].ToString();
            if (obj.Status)
            {
                obj.Approved = Convert.ToInt32(DST.Tables[0].Rows[0]["Approved"].ToString());
                obj.ApproveStatus = DST.Tables[0].Rows[0]["ApproveStatus"].ToString();
                obj.StatusMessage = DST.Tables[0].Rows[0]["StatusMessage"].ToString();
                obj.PMS_AID = Convert.ToInt64(DST.Tables[0].Rows[0]["PMS_AID"].ToString());
                obj.FYID = Convert.ToInt64(DST.Tables[0].Rows[0]["FYID"].ToString());
                obj.EMPID = Convert.ToInt64(DST.Tables[0].Rows[0]["EMPID"].ToString());
                obj.EMPCode = DST.Tables[0].Rows[0]["EMPCode"].ToString();
                obj.EMPName = DST.Tables[0].Rows[0]["EMPName"].ToString();
                obj.LocationName = DST.Tables[0].Rows[0]["LocationName"].ToString();
                obj.Department = DST.Tables[0].Rows[0]["Department"].ToString();
                obj.DesignationName = DST.Tables[0].Rows[0]["DesignationName"].ToString();
                obj.FYName = DST.Tables[0].Rows[0]["FYName"].ToString();
                obj.AppraiserName = DST.Tables[0].Rows[0]["AppraiserName"].ToString();
                obj.CommentorsName = DST.Tables[0].Rows[0]["CommentorsName"].ToString();
                obj.Team_Score = Convert.ToInt32(DST.Tables[0].Rows[0]["Team_Score"].ToString());
                obj.TeamRecommendation = DST.Tables[0].Rows[0]["TeamRecommendation"].ToString();

                obj.Group_Score = Convert.ToInt32(DST.Tables[0].Rows[0]["Group_Score"].ToString());
                obj.Group_Comment = DST.Tables[0].Rows[0]["Group_Comment"].ToString();

                obj.CMC_Increment = Convert.ToDecimal(DST.Tables[0].Rows[0]["CMC_Increment"].ToString());
                obj.CMC_Score = Convert.ToInt32(DST.Tables[0].Rows[0]["CMC_Score"].ToString());
                obj.CMC_Comment = DST.Tables[0].Rows[0]["CMC_Comment"].ToString();
                obj.SystemScore = Convert.ToDouble(DST.Tables[0].Rows[0]["SystemScore"].ToString());

                obj.KPAL = GetCMCKPAList(DST.Tables[1]);
                obj.QAL = GetCMCQAList(DST.Tables[2]);
                obj.AttachmentsL = GetCMCAttachmentList(DST.Tables[3]);
                obj.TrainingL = GetCMCTrainingList(DST.Tables[4]);
                obj.CommentsL = GetCMCCommentList(DST.Tables[5]);


                if (obj.Approved == 5)
                {
                    obj.IsBtnVisible = true;
                }


            }
            return obj;
        }
        private List<PMS.CMCAppraisal.KPA> GetCMCKPAList(DataTable dt)
        {
            List<PMS.CMCAppraisal.KPA> List = new List<PMS.CMCAppraisal.KPA>();
            PMS.CMCAppraisal.KPA obj = new PMS.CMCAppraisal.KPA();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.CMCAppraisal.KPA();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.PMS_ADID = Convert.ToInt64(item["PMS_ADID"].ToString());
                    obj.PMS_AID = Convert.ToInt64(item["PMS_AID"].ToString());
                    obj.KPAID = Convert.ToInt64(item["KPAID"].ToString());

                    obj.GoalSheetID = Convert.ToInt64(item["GoalSheetID"].ToString());
                    obj.GoalSheet_DetID = Convert.ToInt64(item["GoalSheet_DetID"].ToString());

                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.Doctype = item["Doctype"].ToString();
                    obj.KPA_Area = item["KPA_Area"].ToString();
                    obj.KPA_PIndicator = item["KPA_PIndicator"].ToString();
                    obj.KPA_IsMonitoring = item["KPA_IsMonitoring"].ToString();
                    obj.KPA_IsMandatory = item["KPA_IsMandatory"].ToString();
                    obj.kPA_Target = item["kPA_Target"].ToString();
                    obj.KPA_IncType = item["KPA_IncType"].ToString();
                    obj.KPA_AutoRating = item["KPA_AutoRating"].ToString();
                    obj.KPA_Weight = item["KPA_Weight"].ToString();

                    obj.UOMID = Convert.ToInt32(item["UOMID"].ToString());
                    obj.UOM_Name = item["UOM_Name"].ToString();
                    obj.Self_Achievement = item["Self_Achievement"].ToString();
                    obj.Self_Comment = item["Self_Comment"].ToString();
                    obj.HOD_Comment = item["HOD_Comment"].ToString();
                    obj.HOD_Score = item["HOD_Score"].ToString();
                    obj.KPA_TargetAchieved = item["KPA_TargetAchieved"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalTrainingList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.CMCAppraisal.Training> GetCMCTrainingList(DataTable dt)
        {
            List<PMS.CMCAppraisal.Training> List = new List<PMS.CMCAppraisal.Training>();
            PMS.CMCAppraisal.Training obj = new PMS.CMCAppraisal.Training();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.CMCAppraisal.Training();
                    obj.PMS_ADID = Convert.ToInt64(item["PMS_ADID"].ToString());
                    obj.TrainingTypeID = Convert.ToInt32(item["TrainingTypeID"].ToString());
                    obj.TrainingType = item["TrainingType"].ToString();
                    obj.TrainingRemarks = item["TrainingRemarks"].ToString();
                    obj.isdeleted = Convert.ToInt32(item["isdeleted"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalTrainingList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.CMCAppraisal.QA> GetCMCQAList(DataTable dt)
        {


            List<PMS.CMCAppraisal.QA> TempList = new List<PMS.CMCAppraisal.QA>();
            PMS.CMCAppraisal.QA Tempobj = new PMS.CMCAppraisal.QA();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    Tempobj = new PMS.CMCAppraisal.QA();
                    Tempobj.PMS_QAID = Convert.ToInt64(item["PMS_QAID"].ToString());
                    Tempobj.Question = item["Question"].ToString();
                    Tempobj.Answer = item["Answer"].ToString();
                    Tempobj.FinalComment = item["FinalComment"].ToString();
                    Tempobj.GivenBy = Convert.ToInt64(item["GivenBy"].ToString());
                    Tempobj.EMPName = item["EMPName"].ToString();
                    Tempobj.EMPCode = item["EMPCode"].ToString();
                    Tempobj.Department = item["Department"].ToString();
                    Tempobj.DesignationName = item["DesignationName"].ToString();
                    Tempobj.LocationName = item["LocationName"].ToString();
                    Tempobj.Doctype = item["Doctype"].ToString().ToLower();
                    Tempobj.FinalRating = item["FinalRating"].ToString().ToLower();
                    Tempobj.QuestionFor = item["QuestionFor"].ToString().ToLower();
                    Tempobj.EMPdetails = Tempobj.EMPName + "(" + Tempobj.EMPCode + ") ," + Tempobj.DesignationName;
                    TempList.Add(Tempobj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalQAList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return TempList;

        }
        private List<PMS.CMCAppraisal.Comments> GetCMCCommentList(DataTable dt)
        {
            List<PMS.CMCAppraisal.Comments> List = new List<PMS.CMCAppraisal.Comments>();
            PMS.CMCAppraisal.Comments obj = new PMS.CMCAppraisal.Comments();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.CMCAppraisal.Comments();
                    obj.Doctype = item["Doctype"].ToString();

                    obj.Doctype = item["Doctype"].ToString();
                    obj.TableName = item["TableName"].ToString();

                    obj.PMS_CommentID = Convert.ToInt64(item["PMS_CommentID"].ToString());
                    obj.Comment = item["Comment"].ToString();
                    obj.Name = item["Name"].ToString();

                    obj.Email = item["Email"].ToString();
                    obj.createdat = (Convert.ToDateTime(item["createdat"]).Year > 1900 ? Convert.ToDateTime(item["createdat"]).ToString(DateFormat) : "");
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalCommentList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.CMCAppraisal.Attachments> GetCMCAttachmentList(DataTable dt)
        {
            List<PMS.CMCAppraisal.Attachments> List = new List<PMS.CMCAppraisal.Attachments>();
            PMS.CMCAppraisal.Attachments obj = new PMS.CMCAppraisal.Attachments();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.CMCAppraisal.Attachments();
                    obj.AttachmentID = Convert.ToInt64(item["AttachmentID"].ToString());
                    obj.FileName = item["FileName"].ToString();
                    obj.Descrip = item["Descrip"].ToString();
                    obj.URL = item["URL"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalAttachmentList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        // View Appraisal
        public PMS.Appraisal.Add GetAppraisal_View(long PMS_AID)
        {
            PMS.Appraisal.Add obj = new PMS.Appraisal.Add();
            DataSet DST = Common_SPU.fnGetPMS_Appraisal(PMS_AID);
            //  0 Get Appraisal
            //  1 Get All KPA
            //  2 Get All Question
            //  3 Get Attachment
            //  4 Get Training
            //  5 Get Comment
            obj.Status = Convert.ToBoolean(DST.Tables[0].Rows[0]["Status"].ToString());
            obj.StatusMessage = DST.Tables[0].Rows[0]["StatusMessage"].ToString();
            if (obj.Status)
            {
                obj.Approved = Convert.ToInt32(DST.Tables[0].Rows[0]["Approved"].ToString());
                obj.ApproveStatus = DST.Tables[0].Rows[0]["ApproveStatus"].ToString();
                obj.StatusMessage = DST.Tables[0].Rows[0]["StatusMessage"].ToString();
                obj.PMS_AID = Convert.ToInt64(DST.Tables[0].Rows[0]["PMS_AID"].ToString());
                obj.FYID = Convert.ToInt64(DST.Tables[0].Rows[0]["FYID"].ToString());
                obj.EMPID = Convert.ToInt64(DST.Tables[0].Rows[0]["EMPID"].ToString());
                obj.EMPCode = DST.Tables[0].Rows[0]["EMPCode"].ToString();
                obj.EMPName = DST.Tables[0].Rows[0]["EMPName"].ToString();
                obj.LocationName = DST.Tables[0].Rows[0]["LocationName"].ToString();
                obj.Department = DST.Tables[0].Rows[0]["Department"].ToString();
                obj.DesignationName = DST.Tables[0].Rows[0]["DesignationName"].ToString();
                obj.FYName = DST.Tables[0].Rows[0]["FYName"].ToString();

                obj.Team_Score = Convert.ToInt32(DST.Tables[0].Rows[0]["Team_Score"].ToString());
                obj.TeamRecommendation = DST.Tables[0].Rows[0]["TeamRecommendation"].ToString();

                obj.Group_Score = Convert.ToInt32(DST.Tables[0].Rows[0]["Group_Score"].ToString());
                obj.Group_Comment = DST.Tables[0].Rows[0]["Group_Comment"].ToString();

                obj.CMC_Increment = Convert.ToDecimal(DST.Tables[0].Rows[0]["CMC_Increment"].ToString());
                obj.CMC_Score = Convert.ToInt32(DST.Tables[0].Rows[0]["CMC_Score"].ToString());
                obj.CMC_Comment = DST.Tables[0].Rows[0]["CMC_Comment"].ToString();

                obj.KPAL = GetViewKPAList(DST.Tables[1]);
                obj.QAL = GetViewQAList(DST.Tables[2]);
                obj.AttachmentsL = GetViewAttachmentList(DST.Tables[3]);
                obj.TrainingL = GetViewTrainingList(DST.Tables[4]);
                obj.CommentsL = GetViewCommentList(DST.Tables[5]);
                if (obj.Approved == 5)
                {
                    obj.IsBtnVisible = true;
                }


            }
            return obj;
        }
        private List<PMS.Appraisal.KPA> GetViewKPAList(DataTable dt)
        {
            List<PMS.Appraisal.KPA> List = new List<PMS.Appraisal.KPA>();
            PMS.Appraisal.KPA obj = new PMS.Appraisal.KPA();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.Appraisal.KPA();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.PMS_ADID = Convert.ToInt64(item["PMS_ADID"].ToString());
                    obj.PMS_AID = Convert.ToInt64(item["PMS_AID"].ToString());
                    obj.KPAID = Convert.ToInt64(item["KPAID"].ToString());

                    obj.GoalSheetID = Convert.ToInt64(item["GoalSheetID"].ToString());
                    obj.GoalSheet_DetID = Convert.ToInt64(item["GoalSheet_DetID"].ToString());

                    obj.FYID = Convert.ToInt64(item["FYID"].ToString());
                    obj.Doctype = item["Doctype"].ToString();
                    obj.KPA_Area = item["KPA_Area"].ToString();
                    obj.KPA_PIndicator = item["KPA_PIndicator"].ToString();
                    obj.KPA_IsMonitoring = item["KPA_IsMonitoring"].ToString();
                    obj.KPA_IsMandatory = item["KPA_IsMandatory"].ToString();
                    obj.kPA_Target = item["kPA_Target"].ToString();
                    obj.KPA_IncType = item["KPA_IncType"].ToString();
                    obj.KPA_AutoRating = item["KPA_AutoRating"].ToString();
                    obj.KPA_Weight = item["KPA_Weight"].ToString();

                    obj.UOMID = Convert.ToInt32(item["UOMID"].ToString());
                    obj.UOM_Name = item["UOM_Name"].ToString();
                    obj.Self_Achievement = item["Self_Achievement"].ToString();
                    obj.Self_Comment = item["Self_Comment"].ToString();
                    obj.HOD_Comment = item["HOD_Comment"].ToString();
                    obj.HOD_Score = item["HOD_Score"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalTrainingList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.Appraisal.Training> GetViewTrainingList(DataTable dt)
        {
            List<PMS.Appraisal.Training> List = new List<PMS.Appraisal.Training>();
            PMS.Appraisal.Training obj = new PMS.Appraisal.Training();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.Appraisal.Training();
                    obj.PMS_ADID = Convert.ToInt64(item["PMS_ADID"].ToString());
                    obj.TrainingTypeID = Convert.ToInt32(item["TrainingTypeID"].ToString());
                    obj.TrainingType = item["TrainingType"].ToString();
                    obj.TrainingRemarks = item["TrainingRemarks"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalTrainingList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.Appraisal.QA> GetViewQAList(DataTable dt)
        {


            List<PMS.Appraisal.QA> TempList = new List<PMS.Appraisal.QA>();
            PMS.Appraisal.QA Tempobj = new PMS.Appraisal.QA();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    Tempobj = new PMS.Appraisal.QA();
                    Tempobj.PMS_QAID = Convert.ToInt64(item["PMS_QAID"].ToString());
                    Tempobj.Question = item["Question"].ToString();
                    Tempobj.Answer = item["Answer"].ToString();
                    Tempobj.FinalComment = item["FinalComment"].ToString();
                    Tempobj.GivenBy = Convert.ToInt64(item["GivenBy"].ToString());
                    Tempobj.EMPName = item["EMPName"].ToString();
                    Tempobj.EMPCode = item["EMPCode"].ToString();
                    Tempobj.Department = item["Department"].ToString();
                    Tempobj.DesignationName = item["DesignationName"].ToString();
                    Tempobj.LocationName = item["LocationName"].ToString();
                    Tempobj.Doctype = item["Doctype"].ToString().ToLower();
                    Tempobj.EMPdetails = Tempobj.EMPName + "(" + Tempobj.EMPCode + ") ," + Tempobj.DesignationName;
                    TempList.Add(Tempobj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalQAList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return TempList;

        }
        private List<PMS.Appraisal.Comments> GetViewCommentList(DataTable dt)
        {
            List<PMS.Appraisal.Comments> List = new List<PMS.Appraisal.Comments>();
            PMS.Appraisal.Comments obj = new PMS.Appraisal.Comments();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.Appraisal.Comments();
                    obj.Doctype = item["Doctype"].ToString();

                    obj.Doctype = item["Doctype"].ToString();
                    obj.TableName = item["TableName"].ToString();

                    obj.PMS_CommentID = Convert.ToInt64(item["PMS_CommentID"].ToString());
                    obj.Comment = item["Comment"].ToString();
                    obj.Name = item["Name"].ToString();

                    obj.Email = item["Email"].ToString();
                    obj.createdat = (Convert.ToDateTime(item["createdat"]).Year > 1900 ? Convert.ToDateTime(item["createdat"]).ToString(DateFormat) : "");
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalCommentList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        private List<PMS.Appraisal.Attachments> GetViewAttachmentList(DataTable dt)
        {
            List<PMS.Appraisal.Attachments> List = new List<PMS.Appraisal.Attachments>();
            PMS.Appraisal.Attachments obj = new PMS.Appraisal.Attachments();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new PMS.Appraisal.Attachments();
                    obj.AttachmentID = Convert.ToInt64(item["AttachmentID"].ToString());
                    obj.FileName = item["FileName"].ToString();
                    obj.Descrip = item["Descrip"].ToString();
                    obj.URL = item["URL"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetAppraisalAttachmentList. The query was executed :", ex.ToString(), "fnGetPMS_CommentRequest", "PMSModal", "PMSModal", "");
            }
            return List;

        }
        public List<PMS.TrainingTypeMaster.List> GetTrainingTypeList(long ID)
        {
            string LoginID = clsApplicationSetting.GetSessionValue("LoginID");

            List<PMS.TrainingTypeMaster.List> result = new List<PMS.TrainingTypeMaster.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int64, value: ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTrainingTypeList", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PMS.TrainingTypeMaster.List>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTrainingTypeList. The query was executed :", ex.ToString(), "spu_GetTrainingTypeList()", "PMSModal", "PMSModal", "");

            }
            return result;
        }
        public PMS.TrainingTypeMaster.Add GetTrainingType(GetResponse modal)
        {
            PMS.TrainingTypeMaster.Add result = new PMS.TrainingTypeMaster.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@id", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTrainingType", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PMS.TrainingTypeMaster.Add>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new PMS.TrainingTypeMaster.Add();
                        }

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTrainingType. The query was executed :", ex.ToString(), "spu_GetFR_Prospect()", "PMSModal", "PMSModal", "");

            }
            return result;
        }
        public PostResponse SetTrainingType(PMS.TrainingTypeMaster.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTrainingType", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = modal.Id;
                        command.Parameters.Add("@TrainingName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.TrainingName);
                        command.Parameters.Add("@TrainingDesc", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.TrainingDesc);
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
        public string GetAppraisal_RPT(long EMPID, long Fyid)
        {
            string strpath = "", sFileName = "";
            try
            {
                ReportDocument rd = new ReportDocument();
                ReportDocument rdTitles = new ReportDocument();
                string constring = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
                SqlConnection _sqlCon = new SqlConnection(constring);
                _sqlCon.Open();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandText = "spu_GetPMS_AppraisalRpt";
                _cmd.Parameters.AddWithValue("@LoginID", EMPID);
                _cmd.Parameters.AddWithValue("@Empid", EMPID);
                _cmd.Parameters.AddWithValue("@FYid", Fyid);
                _cmd.Parameters.AddWithValue("@Doctype", "PA");
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Connection = _sqlCon;
                SqlDataAdapter _sda = new SqlDataAdapter(_cmd);
                System.Data.DataSet _dataSet = new System.Data.DataSet();
                _sda.Fill(_dataSet);

                SqlCommand _cmd1 = new SqlCommand();
                _cmd1.CommandText = "spu_GetPMS_AppraisalKPARpt";
                _cmd1.Parameters.AddWithValue("@LoginID", EMPID);
                _cmd1.Parameters.AddWithValue("@Empid", EMPID);
                _cmd1.Parameters.AddWithValue("@FYid", Fyid);
                _cmd1.Parameters.AddWithValue("@Doctype", "KPA");
                _cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd1.Connection = _sqlCon;
                _sda = new SqlDataAdapter(_cmd1);
                System.Data.DataSet _dataSetkpa = new System.Data.DataSet();
                _sda.Fill(_dataSetkpa);

                SqlCommand _cmd2 = new SqlCommand();
                _cmd2.CommandText = "spu_GetPMS_AppraisalTrainingRpt";
                _cmd2.Parameters.AddWithValue("@LoginID", EMPID);
                _cmd2.Parameters.AddWithValue("@Empid", EMPID);
                _cmd2.Parameters.AddWithValue("@FYid", Fyid);
                _cmd2.Parameters.AddWithValue("@Doctype", "Training");
                _cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd2.Connection = _sqlCon;
                _sda = new SqlDataAdapter(_cmd2);
                System.Data.DataSet _dataSetTraining = new System.Data.DataSet();
                _sda.Fill(_dataSetTraining);


                SqlCommand _cmd3 = new SqlCommand();
                _cmd3.CommandText = "spu_GetPMS_AppraisalQuestionsRpt";
                _cmd3.Parameters.AddWithValue("@LoginID", EMPID);
                _cmd3.Parameters.AddWithValue("@Empid", EMPID);
                _cmd3.Parameters.AddWithValue("@FYid", Fyid);
                _cmd3.Parameters.AddWithValue("@Doctype", "Questions");
                _cmd3.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd3.Connection = _sqlCon;
                _sda = new SqlDataAdapter(_cmd3);
                System.Data.DataSet _dataSetQuestions = new System.Data.DataSet();
                _sda.Fill(_dataSetQuestions);

                SqlCommand _cmd4 = new SqlCommand();
                _cmd4.CommandText = "spu_GetPMS_AppraisalRatingRpt";
                _cmd4.Parameters.AddWithValue("@LoginID", EMPID);
                _cmd4.Parameters.AddWithValue("@Empid", EMPID);
                _cmd4.Parameters.AddWithValue("@FYid", Fyid);
                _cmd4.Parameters.AddWithValue("@Doctype", "Rating");
                _cmd4.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd4.Connection = _sqlCon;
                _sda = new SqlDataAdapter(_cmd4);
                System.Data.DataSet _dataSetRating = new System.Data.DataSet();
                _sda.Fill(_dataSetRating);


                string _reportPath = string.Empty;
                _reportPath = HttpContext.Current.Server.MapPath(@"/CrystalReport/PerformanceAppraisal.rpt");
                //sFileName = "Appraisal_Report_" + _dataSet.Tables[0].Rows[0]["EMPNAME"] +"_" + _dataSet.Tables[0].Rows[0]["EMPCode"];
                sFileName = "Appraisal_Report_" + _dataSet.Tables[0].Rows[0]["EMPCode"];
                //sFileName = "Appraisal_Report";
                _sqlCon.Close();
                rd.Load(_reportPath);
                rd.SetDataSource(_dataSet.Tables[0]);
                rd.Subreports["PA_KPA"].SetDataSource(_dataSetkpa.Tables[0]);
                rd.Subreports["PA_Training"].SetDataSource(_dataSetTraining.Tables[0]);
                rd.Subreports["PA_Questions"].SetDataSource(_dataSetQuestions.Tables[0]);
                rd.Subreports["PA_Rating"].SetDataSource(_dataSetRating.Tables[0]);
                rd.Subreports["PR_Training"].SetDataSource(_dataSetTraining.Tables[0]);
                strpath = HttpContext.Current.Server.MapPath("/Attachments/PDF/" + sFileName + ".pdf");
                if (System.IO.File.Exists(strpath))
                {
                    System.IO.File.Delete(strpath);
                }
                string savelocation = strpath;
                rd.ExportToDisk(ExportFormatType.PortableDocFormat, savelocation);
                rd.Close();
                rd.Dispose();
                rdTitles.Close();
                rdTitles.Dispose();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "Application/pdf";
                HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + sFileName + ".pdf");
                //Write the file directly to the HTTP content output stream.
                HttpContext.Current.Response.WriteFile(savelocation);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }

            catch (Exception ex)
            {

            }
            return sFileName;
        }
        public List<PMS.PMSStatus> GetPMSStatusList(long EMPID, long Fyid)
        {
            List<PMS.PMSStatus> result = new List<PMS.PMSStatus>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Fyid", dbType: DbType.Int32, value: Fyid, direction: ParameterDirection.Input);
                    param.Add("@EmpID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);                    
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPMSGoalSheetRpt", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PMS.PMSStatus>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetPMSGoalSheetRpt. The query was executed :", ex.ToString(), "GetPMSStatusList()", "PMSModaal", "PMSModaal");
            }
            return result;
        }

        public List<PMS.PMSStatus> GetAppraisalReportList(long EMPID, long Fyid)
        {
            List<PMS.PMSStatus> result = new List<PMS.PMSStatus>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Fyid", dbType: DbType.Int32, value: Fyid, direction: ParameterDirection.Input);
                    param.Add("@EmpID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPMS_AppraisalRptAllEmp", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PMS.PMSStatus>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetPMS_AppraisalRptAllEmp. The query was executed :", ex.ToString(), "GetAppraisalReportList()", "PMSModaal", "PMSModaal");
            }
            return result;
        }

        public List<PMS.PMSStatus> GetAdditionalReviewerReportList(long EMPID, long Fyid)
        {
            List<PMS.PMSStatus> result = new List<PMS.PMSStatus>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Fyid", dbType: DbType.Int32, value: Fyid, direction: ParameterDirection.Input);
                    param.Add("@EmpID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPMSAdditionalReportAllEmp", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PMS.PMSStatus>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetPMSAdditionalReportAllEmp. The query was executed :", ex.ToString(), "GetAdditionalReviewerReportList()", "PMSModaal", "PMSModaal");
            }
            return result;
        }
        public List<PMS.TrainingSkills> GetEmployeeTrainingReportList(GetResponse modal)
        {
            List<PMS.TrainingSkills> result = new List<PMS.TrainingSkills>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Empid", dbType: DbType.Int32, value: modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@FYid ", dbType: DbType.Int32, value: modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPMSAppraisalTrainingSkillsRpt", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PMS.TrainingSkills>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetEmployeeTrainingReportList. The query was executed :", ex.ToString(), "spu_GetPMSAppraisalTrainingSkillsRpt()", "PMSModal", "PMSModal", "");

            }
            return result;
        }

    }
}
