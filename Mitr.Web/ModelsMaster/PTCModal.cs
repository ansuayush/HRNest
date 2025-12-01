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
    public class PTCModal : IPTCHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public string DateFormat = "dd-MMM-yyyy", DateFormatC = "dd-MMM-yy hh:mm:ss tt", DateFormatE = "yyyy-MM-dd";
        public PTCModal()
        {
            var d = clsApplicationSetting.GetConfigValue("DateFormat");
            var dC = clsApplicationSetting.GetConfigValue("DateFormatC");
            DateFormat = (!string.IsNullOrEmpty(d) ? d : DateFormat);
            DateFormatC = (!string.IsNullOrEmpty(dC) ? dC : DateFormatC);
        }

        public List<PTC.Hierarchy> GetPTC_HierarchyList(long Approved)
        {
            List<PTC.Hierarchy> List = new List<PTC.Hierarchy>();
            PTC.Hierarchy obj = new PTC.Hierarchy();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPTC_Essential(Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PTC.Hierarchy();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.AppraiserName = item["AppraiserName"].ToString();
                    obj.Designation = item["DesignationName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.ConfirmerName = item["Confirmer"].ToString();
                    obj.Approved = Convert.ToInt64(item["Approved"].ToString());
                    obj.Id = Convert.ToInt64(item["ID"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_HierarchyList. The query was executed :", ex.ToString(), "fnGetPTC_Essential", "PTCModal", "PTCModal", "");
            }
            return List;
        }

        public PTC.Hierarchy.Update GetPTC_HierarchyUpdate(long ID, long Approved, long EMPID)
        {
            PTC.Hierarchy.Update result = new PTC.Hierarchy.Update();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    param.Add("@EID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPTC_Master", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PTC.Hierarchy.Update>().FirstOrDefault();
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
                ClsCommon.LogError("Error during GetPTC_HierarchyUpdate. The query was executed :", ex.ToString(), "spu_GetPTC_Master()", "PTCModal", "PTCModal", "");

            }
            return result;
        }




        public List<PTC.CreationofObjective> GetPTC_CreationofObjectiveMyList(long Approved)
        {
            List<PTC.CreationofObjective> List = new List<PTC.CreationofObjective>();
            PTC.CreationofObjective obj = new PTC.CreationofObjective();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPTC_ObjectiveMylist(Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PTC.CreationofObjective();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.Designation = item["DesignationName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.Approved = Convert.ToInt64(item["Approved"].ToString());
                    obj.Id = Convert.ToInt64(item["ID"].ToString());
                    obj.Status = item["Status"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_CreationofObjectiveMyList. The query was executed :", ex.ToString(), "fnGetPTC_ObjectiveMylist", "PTCModal", "PTCModal", "");
            }
            return List;
        }
        public List<PTC.CreationofObjective> GetPTC_CreationofObjective(long Approved)
        {
            List<PTC.CreationofObjective> List = new List<PTC.CreationofObjective>();
            PTC.CreationofObjective obj = new PTC.CreationofObjective();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPTC_Objective(Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PTC.CreationofObjective();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.Designation = item["DesignationName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.Approved = Convert.ToInt64(item["Approved"].ToString());
                    obj.Id = Convert.ToInt64(item["ID"].ToString());
                    obj.Status = item["Status"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_CreationofObjective. The query was executed :", ex.ToString(), "fnGetPTC_Objective", "PTCModal", "PTCModal", "");
            }
            return List;
        }

        public PTC.ProbationObjectives GetPTC_ProbationObjectives(long ID, long Approved, long EMPID)
        {
            PTC.ProbationObjectives result = new PTC.ProbationObjectives();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    param.Add("@Approved", dbType: DbType.Int32, value: Approved, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPTC_ ProbationObjective", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PTC.ProbationObjectives>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {

                            result.kPALists = reader.Read<PTC.KPAList>().ToList();

                        }
                        result.uMLists = reader.Read<PTC.UMList>().ToList();
                        result.objectivesperiods = reader.Read<PTC.Objectivesperiod>().ToList();
                        result.resubmitLists = reader.Read<PTC.ResubmitList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_ProbationObjectives. The query was executed :", ex.ToString(), "spu_GetPTC_ ProbationObjective()", "PTCModal", "PTCModal", "");

            }
            return result;
        }

        public PTC.ObjectivesperiodNew GetPTC_ObjectiveUpdate(long ID)
        {
            PTC.ObjectivesperiodNew result = new PTC.ObjectivesperiodNew();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPTC_ ProbationObjectivePeriod", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PTC.ObjectivesperiodNew>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {

                            result.kPALists = reader.Read<PTC.KPAList>().ToList();

                        }
                        result.uMLists = reader.Read<PTC.UMList>().ToList();

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_ObjectiveUpdate. The query was executed :", ex.ToString(), "spu_GetPTC_ ProbationObjectivePeriod()", "PTCModal", "PTCModal", "");

            }
            return result;
        }


        public PTC.Appraisal.SelfAppraisal GetPTC_SelfAppraisal(long ID)
        {
            PTC.Appraisal.SelfAppraisal result = new PTC.Appraisal.SelfAppraisal();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("EMPID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPTC_ MyAppraisal", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PTC.Appraisal.SelfAppraisal>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {

                            result.KPAL = reader.Read<PTC.Appraisal.KPA>().ToList();


                        }
                        result.feedbackQuestions = reader.Read<PTC.Appraisal.FeedbackQuestions>().ToList();
                        result.AttachmentsL = reader.Read<PTC.Appraisal.Attachments>().ToList();
                        result.TrainingL = reader.Read<PTC.Appraisal.Training>().ToList();
                        result.TrainingType = reader.Read<PTC.Appraisal.TrainingType>().ToList();
                        result.typeslist = reader.Read<PTC.Appraisal.Type>().ToList();
                        if (!reader.IsConsumed)
                        {
                            result.appraisalReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                            if (result.appraisalReview == null)
                            {
                                result.appraisalReview = new PTC.Appraisal.AppraisalReview();
                            }
                        }
                        //if (result.appraisalReview == null)
                        //{
                        //    result.appraisalReview = new PTC.Appraisal.AppraisalReview();
                        //}
                        //else
                        //{
                        //    result.appraisalReview= reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                        //}

                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_SelfAppraisal. The query was executed :", ex.ToString(), "spu_GetPTC_ MyAppraisal", "PTCModal", "PTCModal", "");

            }
            return result;
        }
        public List<PTC.Appraisal.TeamList> GetPTC_TeamList(long Approved)
        {
            List<PTC.Appraisal.TeamList> List = new List<PTC.Appraisal.TeamList>();
            PTC.Appraisal.TeamList obj = new PTC.Appraisal.TeamList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPTC_TeamList(Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PTC.Appraisal.TeamList();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Designation = item["DesignationName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.Approved = Convert.ToInt64(item["Approved"].ToString());
                    obj.Id = Convert.ToInt64(item["ID"].ToString());
                    obj.Status = item["Status"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_TeamList. The query was executed :", ex.ToString(), "fnGetPTC_TeamList", "PTCModal", "PTCModal", "");
            }
            return List;
        }
        public List<PTC.Appraisal.TeamList> GetPTC_ConfimerList(long Approved)
        {
            List<PTC.Appraisal.TeamList> List = new List<PTC.Appraisal.TeamList>();
            PTC.Appraisal.TeamList obj = new PTC.Appraisal.TeamList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPTC_Confimerlist(Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PTC.Appraisal.TeamList();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Designation = item["DesignationName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.Approved = Convert.ToInt64(item["Approved"].ToString());
                    obj.Id = Convert.ToInt64(item["ID"].ToString());
                    obj.Status = item["Status"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_TeamList. The query was executed :", ex.ToString(), "fnGetPTC_TeamList", "PTCModal", "PTCModal", "");
            }
            return List;
        }
        public PTC.Appraisal.SelfAppraisal GetPTC_ConfimerAppraisal(long ID, long EMPID)
        {
            PTC.Appraisal.SelfAppraisal result = new PTC.Appraisal.SelfAppraisal();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPTC_ ConfimerAppraisal", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PTC.Appraisal.SelfAppraisal>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {

                            result.KPAL = reader.Read<PTC.Appraisal.KPA>().ToList();


                        }
                        result.feedbackQuestions = reader.Read<PTC.Appraisal.FeedbackQuestions>().ToList();
                        result.AttachmentsL = reader.Read<PTC.Appraisal.Attachments>().ToList();
                        result.TrainingL = reader.Read<PTC.Appraisal.Training>().ToList();
                        result.TrainingType = reader.Read<PTC.Appraisal.TrainingType>().ToList();
                        result.typeslist = reader.Read<PTC.Appraisal.Type>().ToList();
                        result.Ctypeslist = reader.Read<PTC.Appraisal.Type>().ToList();
                        if (ID > 0)
                        {
                            if (!reader.IsConsumed)

                            {
                                result.appraisalReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                                result.ConfirmerReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                            }
                        }
                        else
                        {
                            if (!reader.IsConsumed)
                            {
                                result.appraisalReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                                if (result.appraisalReview == null)
                                {
                                    result.appraisalReview = new PTC.Appraisal.AppraisalReview();
                                }
                            }
                            if (!reader.IsConsumed)
                            {
                                result.ConfirmerReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                                if (result.ConfirmerReview == null)
                                {
                                    result.ConfirmerReview = new PTC.Appraisal.AppraisalReview();
                                }
                            }
                        }



                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_ConfimerAppraisal. The query was executed :", ex.ToString(), "spu_GetPTC_ ConfimerAppraisal", "PTCModal", "PTCModal", "");

            }
            return result;
        }
        public List<PTC.Appraisal.TeamList> GetPTC_CMCList(long Approved)
        {
            List<PTC.Appraisal.TeamList> List = new List<PTC.Appraisal.TeamList>();
            PTC.Appraisal.TeamList obj = new PTC.Appraisal.TeamList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPTC_CMClist(Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PTC.Appraisal.TeamList();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Designation = item["DesignationName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.Approved = Convert.ToInt64(item["Approved"].ToString());
                    obj.Id = Convert.ToInt64(item["ID"].ToString());
                    obj.Status = item["Status"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetPTC_CMClist. The query was executed :", ex.ToString(), "fnGetPTC_CMClist", "PTCModal", "PTCModal", "");
            }
            return List;
        }

        public PTC.Appraisal.CMCAppraisal GetPTC_CMCAppraisal(long ID, long EMPID)
        {
            PTC.Appraisal.CMCAppraisal result = new PTC.Appraisal.CMCAppraisal();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPTC_ CMCAppraisal", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PTC.Appraisal.CMCAppraisal>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {

                            result.KPAL = reader.Read<PTC.Appraisal.KPA>().ToList();


                        }
                        result.feedbackQuestions = reader.Read<PTC.Appraisal.FeedbackQuestions>().ToList();
                        result.typeslist = reader.Read<PTC.Appraisal.Type>().ToList();
                        result.Ctypeslist = reader.Read<PTC.Appraisal.Type>().ToList();
                        result.CMCtypeslist = reader.Read<PTC.Appraisal.Type>().ToList();
                        if (ID == 0)
                        {
                            if (!reader.IsConsumed)
                            {
                                result.appraisalReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                                if (result.appraisalReview == null)
                                {
                                    result.appraisalReview = new PTC.Appraisal.AppraisalReview();
                                }
                            }
                            if (!reader.IsConsumed)
                            {
                                result.ConfirmerReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                                if (result.ConfirmerReview == null)
                                {
                                    result.ConfirmerReview = new PTC.Appraisal.AppraisalReview();
                                }
                            }
                            if (!reader.IsConsumed)
                            {
                                result.CMCReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                                if (result.CMCReview == null)
                                {
                                    result.CMCReview = new PTC.Appraisal.AppraisalReview();
                                }
                            }
                        }
                        else
                        {
                            if (!reader.IsConsumed)
                            {
                                result.appraisalReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                                result.ConfirmerReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                                result.CMCReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();

                            }

                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_CMCAppraisal. The query was executed :", ex.ToString(), "spu_GetPTC_ CMCAppraisal", "PTCModal", "PTCModal", "");

            }
            return result;
        }


        public List<PTC.Appraisal.TeamList> GetPTC_HRList(long Approved)
        {
            List<PTC.Appraisal.TeamList> List = new List<PTC.Appraisal.TeamList>();
            PTC.Appraisal.TeamList obj = new PTC.Appraisal.TeamList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetPTC_HRlist(Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new PTC.Appraisal.TeamList();
                    obj.EMPID = Convert.ToInt64(item["EMPID"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Designation = item["DesignationName"].ToString();
                    obj.DepartmentsName = item["Department"].ToString();
                    obj.Approved = Convert.ToInt64(item["Approved"].ToString());
                    obj.Id = Convert.ToInt64(item["ID"].ToString());
                    obj.Status = item["Status"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetPTC_HRlist. The query was executed :", ex.ToString(), "fnGetPTC_HRlist", "PTCModal", "PTCModal", "");
            }
            return List;
        }

        public PTC.Appraisal.HRAppraisal GetPTC_HRAppraisal(long ID, long EMPID)
        {
            PTC.Appraisal.HRAppraisal result = new PTC.Appraisal.HRAppraisal();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", dbType: DbType.Int32, value: ID, direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPTC_ HRAppraisal", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PTC.Appraisal.HRAppraisal>().FirstOrDefault();
                        if (!reader.IsConsumed)
                        {

                            result.KPAL = reader.Read<PTC.Appraisal.KPA>().ToList();


                        }
                        result.feedbackQuestions = reader.Read<PTC.Appraisal.FeedbackQuestions>().ToList();
                        result.typeslist = reader.Read<PTC.Appraisal.Type>().ToList();
                        result.Ctypeslist = reader.Read<PTC.Appraisal.Type>().ToList();
                        result.CMCtypeslist = reader.Read<PTC.Appraisal.Type>().ToList();
                        if (!reader.IsConsumed)
                        {
                            result.appraisalReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                            result.ConfirmerReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();
                            result.CMCReview = reader.Read<PTC.Appraisal.AppraisalReview>().FirstOrDefault();

                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_HRAppraisal. The query was executed :", ex.ToString(), "spu_GetPTC_ CMCAppraisal", "PTCModal", "PTCModal", "");

            }
            return result;
        }
        public List<PTC.Hierarchy> GetPTC_Report(string  Type, long EMPID)
        {
          

            List<PTC.Hierarchy> result = new List<PTC.Hierarchy>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@Type", dbType: DbType.String, value: Type, direction: ParameterDirection.Input);
                    param.Add("@EMPId", dbType: DbType.Int32, value: EMPID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetPTCReport", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<PTC.Hierarchy>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_Report. The query was executed :", ex.ToString(), "spu_GetPTCReport", "PTCModal", "PTCModal", "");

            }
            return result;
        }

    }
}