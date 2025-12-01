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
    public class ExitEmployeeModal: IExitEmployee
    {
        public string DateFormat = "dd-MMM-yyyy", DateFormatC = "dd-MMM-yy hh:mm:ss tt", DateFormatE = "yyyy-MM-dd";
        public ExitEmployeeModal()
        {
            var d = clsApplicationSetting.GetConfigValue("DateFormat");
            var dC = clsApplicationSetting.GetConfigValue("DateFormatC");
            DateFormat = (!string.IsNullOrEmpty(d) ? d : DateFormat);
            DateFormatC = (!string.IsNullOrEmpty(dC) ? dC : DateFormatC);
        }
        public PostResponse fnsetAddSignatoryMaster(EmployeeExit.SignatoryMaster model)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_setExitSignatoryMaster", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@EMPId", SqlDbType.Int).Value = model.EMPId;
                        command.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = model.StartDate;
                        command.Parameters.Add("@Attachmentid", SqlDbType.Int).Value = model.Attachmentid;
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
        public List<EmployeeExit.SignatoryMaster> GetSignatoryMaster(long ID)
        {
            string SQL = "";
            List<EmployeeExit.SignatoryMaster> List = new List<EmployeeExit.SignatoryMaster>();
            EmployeeExit.SignatoryMaster obj = new EmployeeExit.SignatoryMaster();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetsignatoryList(ID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new EmployeeExit.SignatoryMaster();
                    obj.Id = Convert.ToInt32(item["Id"]);
                    obj.StartDate = item["StartDate"].ToString();
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPId = Convert.ToInt64(item["EMPId"]);
                    obj.Attachmentid = Convert.ToInt64(item["Attachmentid"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString(DateFormatC);
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString(DateFormatC);
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.AttachmentPath = item["Attachment"].ToString();
                    obj.filename = item["filename"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetJobList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public EmployeeExit.ResignationRequest GetResignation(long lId, long lEmpid, string sDoctype)
        {

            EmployeeExit.ResignationRequest EItem = new EmployeeExit.ResignationRequest();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.FnGetResignation(lId, lEmpid, sDoctype);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    EItem.lId = Convert.ToInt64(item["id"].ToString());
                    EItem.lEmpid = Convert.ToInt64(item["empid"].ToString());
                    EItem.sRequestno = item["requestno"].ToString();
                    EItem.sRequestdate = Convert.ToDateTime(item["requestdate"].ToString()).ToString("dd/MM/yyyy");
                    EItem.lReasonid = Convert.ToInt64(item["reasonid"].ToString());
                    EItem.sComment = item["comment"].ToString();
                    EItem.iNoticePeriod = Convert.ToInt32(item["noticeperiod"].ToString());
                    EItem.iNoticePeriodServe = Convert.ToInt32(item["noticeperiodserve"].ToString());
                    EItem.sReleivingdate = Convert.ToDateTime(item["relievingdate"]).ToString("dd/MM/yyyy");
                    EItem.sReleivingdateM = (item["relievingdateM"] != null ? item["relievingdateM"].ToString() : "") != "" ? Convert.ToDateTime(item["relievingdateM"].ToString()).ToString("dd-MM-yyyy") : "";

                    EItem.sReleivingdateHR = (item["relievingdateHR"] != null ? item["relievingdateHR"].ToString() : "") != "" ? Convert.ToDateTime(item["relievingdateHR"].ToString()).ToString("dd-MM-yyyy") : "";
                    EItem.iRelievingDay = Convert.ToInt32(item["relievingday"].ToString());
                    EItem.sReasonNoticePeriod = item["reasonNP"].ToString();
                    EItem.iStatusflag = Convert.ToInt32(item["statusflag"].ToString());
                    EItem.iDealernoc = Convert.ToInt32(item["dealernoc"].ToString());
                    EItem.sStatus = item["status"].ToString();
                    EItem.sStatusDNOC = item["statusDNOC"].ToString();
                    EItem.sDealerNOCReq = item["DealerNOCReq"].ToString();
                    // EItem.lstNocList = GetNocList("0", "NOC", "0", sDoctype);
                    EItem.iHistoryCount = Convert.ToInt32(item["historycount"].ToString());
                    EItem.sLatestStatus = item["LatestStatus"].ToString();
                    EItem.sCommentM = item["commentM"].ToString();
                    EItem.sCommentR = item["commentR"].ToString();
                    EItem.sReasonNPM = item["reasonNPM"].ToString();
                    EItem.sEmpcodeL1 = item["EmpcodeL1"].ToString();
                    EItem.sEmpcodeL2 = item["EmpcodeL2"].ToString();
                    EItem.sEmpcodeL3 = item["EmpcodeL3"].ToString();
                    EItem.sEmpcodeL4 = item["EmpcodeL4"].ToString();
                    EItem.sEmpNameL1 = item["EmpNameL1"].ToString();
                    EItem.sEmpNameL2 = item["EmpNameL2"].ToString();
                    EItem.sEmpNameL3 = item["EmpNameL3"].ToString();
                    EItem.sEmpNameL4 = item["EmpNameL4"].ToString();
                    EItem.sCommentL3 = item["CommentL3"].ToString();
                    EItem.sCommentL4 = item["CommentL4"].ToString();
                    EItem.sCommentHR = item["CommentHR"].ToString();
                    EItem.DeginationName = item["Desig_name"].ToString();
                    EItem.DeptName = item["dept_name"].ToString();
                    EItem.LocationName = item["Location"].ToString();
                    // EItem.lstAttachmentsFF = Master.GetMasterAttachments(0, EItem.lId, "EXITFF");
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetResignation. The query was executed :", ex.ToString(), "GetResignation()", "ExitManagementModal", "ExitManagementModal", "");
            }
            return EItem;
        }
        public List<EmployeeExit.HRList> GetExit_HRList(long Approved)
        {
            List<EmployeeExit.HRList> List = new List<EmployeeExit.HRList>();
            EmployeeExit.HRList obj = new EmployeeExit.HRList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetExit_HRlist(Approved);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new EmployeeExit.HRList();
                    obj.RowNum = Convert.ToInt32(item["RowNum"].ToString());
                    obj.EMPID = Convert.ToInt64(item["empid"].ToString());
                    obj.EMPName = item["EMPName"].ToString();
                    obj.EMPCode = item["EMPCode"].ToString();
                    obj.LocationName = item["LocationName"].ToString();
                    obj.DegName = item["DesignationName"].ToString();
                    obj.DeptName = item["Department"].ToString();
                    obj.ID = Convert.ToInt64(item["id"].ToString());
                    obj.LocationName = item["LocationName"].ToString();
                    obj.Dayleft = Convert.ToInt64(item["Daysleft"].ToString());
                    obj.Reldate = item["relievingdate"].ToString();
                    obj.ReqDate = item["requestdate"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetPTC_HRList. The query was executed :", ex.ToString(), "GetPTC_HRList", "EXitModal", "ExitModal", "");
            }
            return List;
        }



    }
}