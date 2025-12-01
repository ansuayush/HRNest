using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMaster
{
    public class TicketModal: ITicketHelper
    {
        public List<Location> GetTicketLocationList(long LocationGroupID)
        {
            string SQL = "";
            List<Location> List = new List<Location>();
            Location obj = new Location();
            try
            {
                SQL = @"SELECT ID,Location_name FROM master_Location where isdeleted=0 
                        and ID not in (select LocationID FROM  LocationGroup_Map 
                        where LocationGroupID="+ LocationGroupID + ") order by  location_name,Priority";

                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Location();
                    obj.LocationID = Convert.ToInt32(item["ID"]);
                    obj.Name = item["location_name"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTicketLocationList. The query was executed :", ex.ToString(), SQL, "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public Helpdesk.TicketConfigration GetTicketConfigration()
        {
            Helpdesk.TicketConfigration obj = new Helpdesk.TicketConfigration();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTicketConfigration(0);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj.TicketCongID = Convert.ToInt64(item["TicketCongID"]);
                    obj.StartTime = Convert.ToDateTime(item["StartTime"]).ToString("HH:mm:ss");
                    obj.EndTime = Convert.ToDateTime(item["EndTime"]).ToString("HH:mm:ss");
                    obj.ApplicableDays = (string.IsNullOrEmpty(item["ApplicableDays"].ToString())?null:item["ApplicableDays"].ToString().Split(','));
                    obj.IsHolidayCalApplicable = Convert.ToInt32(item["IsHolidayCalApplicable"]);  
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTicketConfigration. The query was executed :", ex.ToString(), "fnGetTicketConfigration", "MasterModal", "MasterModal", "");
            }
            return obj;

        }


        public List<Helpdesk.SubCategory> GetTicket_SubCategoryList(long SubCategoryID)
        {
            List<Helpdesk.SubCategory> List = new List<Helpdesk.SubCategory>();
            Helpdesk.SubCategory obj = new Helpdesk.SubCategory();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTicket_SubCategory(SubCategoryID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Helpdesk.SubCategory();
                    obj.CategoryID = Convert.ToInt64(item["CategoryID"]);
                    obj.SubCategoryID = Convert.ToInt64(item["SubCategoryID"]);
                    obj.RelatedTo = item["RelatedTo"].ToString();
                    obj.CategoryName = item["CategoryName"].ToString();
                    obj.Name = item["Name"].ToString();
                    obj.Description = item["description"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLocationGroupList. The query was executed :", ex.ToString(), "fnGetLocationGroup", "MasterModal", "MasterModal", "");
            }
            return List;

        }

      

        public List<Helpdesk.TicketSetting_Assignment> GetTicket_AssignmentList(long SubSettingID, long SubCategoryID, string Doctype)
        {
            List<Helpdesk.TicketSetting_Assignment> List = new List<Helpdesk.TicketSetting_Assignment>();
            Helpdesk.TicketSetting_Assignment obj = new Helpdesk.TicketSetting_Assignment();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTicket_Setting(SubSettingID, SubCategoryID, Doctype);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Helpdesk.TicketSetting_Assignment();
                    obj.SubSettingID = Convert.ToInt64(item["SubSettingID"]);
                    obj.SubCategoryID = Convert.ToInt64(item["SubCategoryID"]);
                    obj.LocationGroupID = Convert.ToInt64(item["LocationGroupID"]);
                    obj.Doctype = item["Doctype"].ToString();
                    obj.PrimaryAssignee = Convert.ToInt32(item["PrimaryAssignee"]);
                    obj.Supervisor = Convert.ToInt32(item["Supervisor"]);
                    obj.Escalation = Convert.ToInt32(item["Escalation"]);

                    obj.LocationGroupName = item["LocationGroupName"].ToString();
                    obj.PrimaryAssigneeName = item["PrimaryAssigneeName"].ToString();
                    obj.SupervisorName = item["SupervisorName"].ToString();
                    obj.EscalationName = item["EscalationName"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLocationGroupList. The query was executed :", ex.ToString(), "fnGetLocationGroup", "MasterModal", "MasterModal", "");
            }
            return List;

        }
        public List<Helpdesk.TicketSetting_SMA> GetTicket_SMAList(long SubSettingID, long SubCategoryID, string Doctype)
        {
            List<Helpdesk.TicketSetting_SMA> List = new List<Helpdesk.TicketSetting_SMA>();
            Helpdesk.TicketSetting_SMA obj = new Helpdesk.TicketSetting_SMA();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTicket_Setting(SubSettingID, SubCategoryID, Doctype);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Helpdesk.TicketSetting_SMA();
                    obj.SubSettingID = Convert.ToInt64(item["SubSettingID"]);
                    obj.SubCategoryID = Convert.ToInt64(item["SubCategoryID"]);
                    obj.Doctype = item["Doctype"].ToString();
                    obj.ResponseTime = Convert.ToInt32(item["ResponseTime"]);
                    obj.FollowUpTime = Convert.ToInt32(item["FollowUpTime"]);
                    obj.EscalationTime = Convert.ToInt32(item["EscalationTime"]);
                    obj.PolicyPriority = item["PolicyPriority"].ToString();
                    obj.Reopen = Convert.ToInt32(item["Reopen"]);
                    obj.IsActive = Convert.ToBoolean(item["IsActive"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLocationGroupList. The query was executed :", ex.ToString(), "fnGetLocationGroup", "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<Helpdesk.Ticket> GetTicketList(long TicketID)
        {
            List<Helpdesk.Ticket> List = new List<Helpdesk.Ticket>();
            Helpdesk.Ticket obj = new Helpdesk.Ticket();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTicket(TicketID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Helpdesk.Ticket();
                    obj.TicketID = Convert.ToInt64(item["TicketID"]);
                    obj.TicketNo = item["TicketNo"].ToString();
                    obj.CategoryName = item["CategoryName"].ToString();
                    obj.SubCategoryName = item["SubCategoryName"].ToString();
                    obj.RelatedTo = item["RelatedTo"].ToString();
                    obj.TicketNo = item["TicketNo"].ToString();
                    obj.SubCategoryID = Convert.ToInt64(item["SubCategoryID"]);
                    obj.LocationGroupID = Convert.ToInt64(item["LocationGroupID"]);
                    obj.LocationGroupName = item["LocationGroupName"].ToString();
                    obj.PrimaryAssignee = Convert.ToInt32(item["PrimaryAssignee"]);
                    obj.Supervisor = Convert.ToInt32(item["Supervisor"]);
                    obj.Escalation = Convert.ToInt32(item["Escalation"]);
                    obj.PrimaryAssigneeName = item["PrimaryAssigneeName"].ToString();
                    obj.SupervisorName = item["SupervisorName"].ToString();
                    obj.EscalationName = item["EscalationName"].ToString();
                    obj.ResponseTime = Convert.ToInt32(item["ResponseTime"]);
                    obj.FollowUpTime = Convert.ToInt32(item["FollowUpTime"]);
                    obj.EscalationTime = Convert.ToInt32(item["EscalationTime"]);
                    obj.PolicyPriority = item["PolicyPriority"].ToString();
                    obj.Message = item["Message"].ToString();
                    obj.Reopen = Convert.ToInt32(item["Reopen"]);
                    
                  

                    obj.StatusID = Convert.ToInt32(item["StatusID"]);
                    obj.StatusName = item["StatusName"].ToString();
                    obj.DisplayName = item["DisplayName"].ToString();
                    obj.StatusColor = item["StatusColor"].ToString();

                    obj.AttachmentID = Convert.ToInt64(item["AttachmentID"]);
                    obj.AttachmentName = item["AttachmentName"].ToString();
                    obj.ContentType = item["ContentType"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.createdbyName = item["createdbyName"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLocationGroupList. The query was executed :", ex.ToString(), "fnGetLocationGroup", "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public Helpdesk.LocationGroup GetMyLocationGroup()
        {
            Helpdesk.LocationGroup Modal = new Helpdesk.LocationGroup();
            string SQL = "";
            long LocationID = 0;
            try
            {
                long.TryParse(clsApplicationSetting.GetSessionValue("LocationID"), out LocationID);
                SQL = @"select LocationGroupID,GroupName from LocationGroup 
                        where LocationGroupID in (select LocationGroupID from LocationGroup_Map where LocationID="+ LocationID + ")";
                foreach (DataRow item in clsDataBaseHelper.ExecuteDataSet(SQL).Tables[0].Rows)
                {
                    Modal.LocationGroupID = Convert.ToInt64(item["LocationGroupID"]);
                    Modal.GroupName = item["GroupName"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMyLocationGroup. The query was executed :", ex.ToString(), "fnGetLocationGroup", "MasterModal", "MasterModal", "");
            }
            return Modal;
        }



        public List<Helpdesk.Ticket_Status> GetTicketStatusList(long TicketStatusID, string Type, string IsActive)
        {
            List<Helpdesk.Ticket_Status> List = new List<Helpdesk.Ticket_Status>();
            Helpdesk.Ticket_Status obj = new Helpdesk.Ticket_Status();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTicketStatus(TicketStatusID, Type, IsActive);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Helpdesk.Ticket_Status();
                    obj.TicketStatusID = Convert.ToInt64(item["TicketstatusID"]);
                    obj.Type = item["Type"].ToString();
                    obj.StatusName = item["StatusName"].ToString();
                    obj.DisplayName = item["DisplayName"].ToString();
                    obj.StatusColor = item["StatusColor"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.Readonly = Convert.ToBoolean(item["Readonly"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTicketStatusList. The query was executed :", ex.ToString(), "fnGetLocationGroup", "MasterModal", "MasterModal", "");
            }
            return List;

        }
        private List<Helpdesk.TicketNotes> GetTicketNotes(DataTable dt)
        {
            List<Helpdesk.TicketNotes> List = new List<Helpdesk.TicketNotes>();
            Helpdesk.TicketNotes obj = new Helpdesk.TicketNotes();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new Helpdesk.TicketNotes();
                    obj.TicketNotesID = Convert.ToInt64(item["TicketNotesID"]);
                    obj.TicketID = Convert.ToInt64(item["TicketID"]);
                    obj.NextDate = (Convert.ToDateTime(item["NextDate"]).Year>1900?Convert.ToDateTime(item["NextDate"]).ToString("dd-MMM-yy hh:mm:ss tt"):"");
                    obj.StatusID = Convert.ToInt32(item["StatusID"]);
                    obj.StatusName = item["StatusName"].ToString();
                    obj.DisplayName = item["DisplayName"].ToString();
                    obj.StatusColor = item["StatusColor"].ToString();
                    obj.Remarks = item["Remarks"].ToString();
                    obj.createdbyName = item["createdbyName"].ToString();
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTicketNotes. The query was executed :", ex.ToString(), "GetTicketNotes", "MasterModal", "MasterModal", "");
            }
            return List;
        }

        private Helpdesk.Ticket GetTicket(DataTable dt)
        {
            Helpdesk.Ticket obj = new Helpdesk.Ticket();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj.TicketID = Convert.ToInt64(item["TicketID"]);
                    obj.TicketNo = item["TicketNo"].ToString();
                    obj.CategoryName = item["CategoryName"].ToString();
                    obj.SubCategoryName = item["SubCategoryName"].ToString();
                    obj.RelatedTo = item["RelatedTo"].ToString();
                    obj.TicketNo = item["TicketNo"].ToString();
                    obj.SubCategoryID = Convert.ToInt64(item["SubCategoryID"]);
                    obj.LocationGroupID = Convert.ToInt64(item["LocationGroupID"]);
                    obj.LocationGroupName = item["LocationGroupName"].ToString();
                    obj.PrimaryAssignee = Convert.ToInt32(item["PrimaryAssignee"]);
                    obj.Supervisor = Convert.ToInt32(item["Supervisor"]);
                    obj.Escalation = Convert.ToInt32(item["Escalation"]);
                    obj.PrimaryAssigneeName = item["PrimaryAssigneeName"].ToString();
                    obj.SupervisorName = item["SupervisorName"].ToString();
                    obj.EscalationName = item["EscalationName"].ToString();
                    obj.ResponseTime = Convert.ToInt32(item["ResponseTime"]);
                    obj.FollowUpTime = Convert.ToInt32(item["FollowUpTime"]);
                    obj.EscalationTime = Convert.ToInt32(item["EscalationTime"]);
                    obj.PolicyPriority = item["PolicyPriority"].ToString();
                    obj.Message = item["Message"].ToString();
                    obj.Reopen = Convert.ToInt32(item["Reopen"]);
                    
                    
                    obj.StatusID = Convert.ToInt32(item["StatusID"]);
                    obj.StatusName = item["StatusName"].ToString();
                    obj.DisplayName = item["DisplayName"].ToString();
                    obj.StatusColor = item["StatusColor"].ToString();
                    obj.AttachmentID = Convert.ToInt64(item["AttachmentID"]);
                    obj.AttachmentName = item["AttachmentName"].ToString();
                    obj.ContentType = item["ContentType"].ToString();
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.createdbyName = item["createdbyName"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTicket. The query was executed :", ex.ToString(), "fnGetLocationGroup", "MasterModal", "MasterModal", "");
            }
            return obj;

        }
        private List<Helpdesk.TicketDeferred> GetTicketDeferred(DataTable dt)
        {
            List<Helpdesk.TicketDeferred> List = new List<Helpdesk.TicketDeferred>();
            Helpdesk.TicketDeferred obj;
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    obj = new Helpdesk.TicketDeferred();
                    obj.DeferredID = Convert.ToInt64(item["DeferredID"]);
                    obj.DeferredName = item["DeferredName"].ToString();
                    obj.DeferredNotesID = item["DeferredNotesID"].ToString().TrimEnd(',');
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTicketDeferred. The query was executed :", ex.ToString(), "GetTicketDeferred", "MasterModal", "MasterModal", "");
            }
            return List;
        }

        public Helpdesk.TicketDetails GetTicketDetails(long TicketID, long TicketNotesID)
        {
            Helpdesk.TicketDetails mainobj = new Helpdesk.TicketDetails();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTicketNotes(TicketID, TicketNotesID);
                // get 3 Table 1 is notes and other is Ticket and last is TicketDeferred
                mainobj.TicketNotesList = GetTicketNotes(TempModuleDataSet.Tables[0]);
                mainobj.Ticket = GetTicket(TempModuleDataSet.Tables[1]);
                mainobj.TicketDeferred = GetTicketDeferred(TempModuleDataSet.Tables[2]);
                
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTicketNotesDetails. The query was executed :", ex.ToString(), "fnGetLocationGroup", "MasterModal", "MasterModal", "");
            }
            return mainobj;
        }

    }
}