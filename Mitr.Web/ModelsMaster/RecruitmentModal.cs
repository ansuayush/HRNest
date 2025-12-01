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
    public class RecruitmentModal: IRecruitmentHelper
    {
        public List<InternalStaff> GetInternalStaffList(long ProjectDetailID, long LocationID)
        {
            List<InternalStaff> List = new List<InternalStaff>();
            InternalStaff obj = new InternalStaff();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetInternalStaffAvailabilityList(ProjectDetailID, LocationID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new InternalStaff();
                    obj.EmpID = Convert.ToInt64(item["ID"]);
                    obj.EMPCode = item["EMP_Code"].ToString();
                    obj.EmpName = item["Emp_Name"].ToString();
                    obj.Relocation = item["Relocation"].ToString();
                    obj.Pillar = item["Pillar"].ToString();
                    obj.JobTitle     = item["JobTitle"].ToString();

                    obj.RelocationByHR = item["Relocation"].ToString();
                    obj.PillarByHR = item["Pillar"].ToString();
                    obj.JobTitleByHR = item["JobTitle"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetInternalStaffList. The query was executed :", ex.ToString(), "spu_GetCountry", "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public InitiateRecruitment GetInitiateRecruitment(long ProjectDetailID)
        {
            
            InitiateRecruitment obj = new InitiateRecruitment();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetInitiateRecruitment(ProjectDetailID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj.ProjectDetailID = Convert.ToInt64(item["ID"]);
                    obj.ProjectID = Convert.ToInt64(item["Proj_ID"]);
                    obj.ManagerID = Convert.ToInt64(item["ManagerID"]);
                    obj.ThemAreaID = Convert.ToInt64(item["ThemArea_ID"]);
                    obj.LocationID = Convert.ToInt64(item["LocationID"]);
                    obj.Managername = item["Managername"].ToString();
                    obj.ManagerCode = item["ManagerCode"].ToString();
                    obj.ProjectName = item["Proj_Name"].ToString();
                    obj.JobID = Convert.ToInt64(item["JobID"]);
                    obj.StartDate = Convert.ToDateTime(item["Start_Date"]).ToString("dd-MMM-yyyy");
                    obj.EndDate = Convert.ToDateTime(item["End_Date"]).ToString("dd-MMM-yyyy");
                    obj.Title = item["Title"].ToString();
                    obj.JobDescription = item["JobDescription"].ToString();
                    obj.Qualification = item["Qualification"].ToString();
                    obj.Skill = item["Skill"].ToString();
                    obj.Experience = item["Experience"].ToString();
                    obj.ThematicArea = item["ThematicArea"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetInternalStaffList. The query was executed :", ex.ToString(), "spu_GetCountry", "MasterModal", "MasterModal", "");
            }
            return obj;

        }


        public List<InternalStaff> GetSelectedRecruitmentCandidate_InternalList(long InternalCandidateID, long RecruitmentRequestID)
        {
            List<InternalStaff> List = new List<InternalStaff>();
            InternalStaff obj = new InternalStaff();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetSelectedRecruitmentCandidate_InternalList(InternalCandidateID, RecruitmentRequestID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new InternalStaff();
                    obj.InternalCandidateID = Convert.ToInt64(item["InternalCandidateID"]);
                    obj.RecruitmentRequestID = Convert.ToInt64(item["RecruitmentRequestID"]);
                    obj.EmpID = Convert.ToInt64(item["EMPID"]);
                    obj.EmpName = item["EmpName"].ToString();
                    obj.EMPCode = item["EmpCode"].ToString();
                    obj.Relocation = item["Relocation"].ToString();
                    obj.Pillar = item["Pillar"].ToString();
                    obj.JobTitle = item["JobTitle"].ToString();
                    obj.RelocationByHR = item["RelocationByHR"].ToString();
                    obj.PillarByHR = item["PillarByHR"].ToString();
                    obj.JobTitleByHR = item["JobTitleByHR"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetSelectedRecruitmentCandidate_InternalList. The query was executed :", ex.ToString(), "spu_GetSelectedRecruitmentCandidate_InternalList", "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public List<RecruitmentRequest> GetRecruitmentRequestInternal(long RecruitmentRequestID,string For)
        {
            List<RecruitmentRequest> List = new List<RecruitmentRequest>();
            RecruitmentRequest obj = new RecruitmentRequest();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetRecruitmentRequestInternal(RecruitmentRequestID, For);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new RecruitmentRequest();
                    obj.RecruitmentRequestID = Convert.ToInt64(item["RecruitmentRequestID"]);
                    obj.ProjectDetailID = Convert.ToInt64(item["ProjectDetailID"]);
                    obj.JobID = Convert.ToInt64(item["JobID"]);
                    obj.Location = Convert.ToInt64(item["Location"]);
                    obj.ProjectManagerID = Convert.ToInt64(item["ProjectManagerID"]);
                    obj.ManagerName = item["ManagerName"].ToString();
                    obj.RecruitmentType = item["RecruitmentType"].ToString();
                    obj.ProjectName = item["ProjectName"].ToString();
                    obj.StartDate = item["StartDate"].ToString();
                    obj.EndDate = item["EndDate"].ToString();
                    obj.JobTitle = item["JobTitle"].ToString();
                    obj.DueDate = item["DueDate"].ToString();
                    obj.JobDescription = item["JobDescription"].ToString();
                    obj.Qualification = item["Qualification"].ToString();
                    obj.Skills = item["Skills"].ToString();
                    obj.Experience = item["Experience"].ToString();
                    obj.StaffCategory = item["StaffCategory"].ToString();
                    obj.ApprovedCandidateID = Convert.ToInt32(item["ApprovedCandidateID"]);
                    obj.PreferenceGiven = Convert.ToInt32(item["PreferenceGiven"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetRecruitmentRequest. The query was executed :", ex.ToString(), "spu_GetCountry", "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public RecruitmentRequest GetRecruitmentPreferenceRaw( long RecruitmentRequestID)
        {
            RecruitmentRequest RecruitmentObj = new RecruitmentRequest();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetRecruitmentPreferenceRaw(RecruitmentRequestID);
                List<RecruitmentPreference> PreList = new List<RecruitmentPreference>();
                if (TempModuleDataSet.Tables[0] != null)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {
                        var obj = new RecruitmentPreference();
                        obj.RecruitmentRequestID = Convert.ToInt64(item["RecruitmentRequestID"]);
                        obj.EmpID = Convert.ToInt64(item["EmpID"]);
                        obj.Preference = Convert.ToInt32(item["Preference"]);
                        obj.Priority = Convert.ToInt32(item["Priority"]);
                        obj.DocType = item["DocType"].ToString();
                        obj.EmpName = item["EmpName"].ToString();
                        obj.JobTitle = item["JobTitle"].ToString();
                        obj.Relocation = item["Relocation"].ToString();
                        obj.Pillar = item["Pillar"].ToString();
                        obj.JobTitle = item["JobTitle"].ToString();
                        obj.RelocationByHR = item["RelocationByHR"].ToString();
                        obj.PillarByHR = item["PillarByHR"].ToString();
                        obj.JobTitleByHR = item["JobTitleByHR"].ToString();
                        PreList.Add(obj);
                    }
                }

                if (TempModuleDataSet.Tables[1] !=null)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[1].Rows)
                    {
                        RecruitmentObj.RecruitmentRequestID = Convert.ToInt64(item["RecruitmentRequestID"]);
                        RecruitmentObj.ProjectDetailID = Convert.ToInt64(item["ProjectDetailID"]);
                        RecruitmentObj.JobID = Convert.ToInt64(item["JobID"]);
                        RecruitmentObj.Location = Convert.ToInt64(item["Location"]);
                        RecruitmentObj.ProjectManagerID = Convert.ToInt64(item["ProjectManagerID"]);
                        RecruitmentObj.ManagerName = item["ManagerName"].ToString();
                        RecruitmentObj.RecruitmentType = item["RecruitmentType"].ToString();
                        RecruitmentObj.ProjectName = item["ProjectName"].ToString();
                        RecruitmentObj.StartDate = item["StartDate"].ToString();
                        RecruitmentObj.EndDate = item["EndDate"].ToString();
                        RecruitmentObj.JobTitle = item["JobTitle"].ToString();
                        RecruitmentObj.DueDate = item["DueDate"].ToString();
                        RecruitmentObj.JobDescription = item["JobDescription"].ToString();
                        RecruitmentObj.Qualification = item["Qualification"].ToString();
                        RecruitmentObj.Skills = item["Skills"].ToString();
                        RecruitmentObj.Experience = item["Experience"].ToString();
                        RecruitmentObj.StaffCategory = item["StaffCategory"].ToString();
                        RecruitmentObj.ApprovedCandidateID = Convert.ToInt32(item["ApprovedCandidateID"]);
                        RecruitmentObj.PreferenceGiven = Convert.ToInt32(item["PreferenceGiven"]);
                        
                    }
                }
                RecruitmentObj.RecruitmentPreferenceList = PreList;
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetRecruitmentPreferenceRaw. The query was executed :", ex.ToString(), "fnGetRecruitmentPreferenceRaw", "RecruitmentModal", "RecruitmentModal", "");
            }
            return RecruitmentObj;

        }


        public List<RecruitmentRequestExternal> GetRecruitmentRequestExternal(long RecruitmentRequestID, string For)
        {
            List<RecruitmentRequestExternal> List = new List<RecruitmentRequestExternal>();
            RecruitmentRequestExternal obj = new RecruitmentRequestExternal();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetRecruitmentRequestExternal(RecruitmentRequestID, For);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new RecruitmentRequestExternal();
                    obj.RecruitmentRequestID = Convert.ToInt64(item["RecruitmentRequestID"]);
                    obj.ProjectDetailID = Convert.ToInt64(item["ProjectDetailID"]);
                    obj.JobID = Convert.ToInt64(item["JobID"]);
                    obj.Location = Convert.ToInt64(item["Location"]);
                    obj.ProjectManagerID = Convert.ToInt64(item["ProjectManagerID"]);
                    obj.ManagerName = item["ManagerName"].ToString();
                    obj.RecruitmentType = item["RecruitmentType"].ToString();
                    obj.ProjectName = item["ProjectName"].ToString();
                    obj.StartDate = item["StartDate"].ToString();
                    obj.EndDate = item["EndDate"].ToString();
                    obj.JobTitle = item["JobTitle"].ToString();
                    obj.DueDate = item["DueDate"].ToString();
                    obj.JobDescription = item["JobDescription"].ToString();
                    obj.Qualification = item["Qualification"].ToString();
                    obj.Skills = item["Skills"].ToString();
                    obj.Experience = item["Experience"].ToString();
                    obj.StaffCategory = item["StaffCategory"].ToString();
                    obj.JobPostID = Convert.ToInt64(item["JobPostID"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetRecruitmentRequest. The query was executed :", ex.ToString(), "spu_GetCountry", "MasterModal", "MasterModal", "");
            }
            return List;

        }

        public List<JobPost> GetJobPost(long JobPostID, string For)
        {
            List<JobPost> List = new List<JobPost>();
            JobPost obj = new JobPost();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetJobPost(JobPostID, For);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new JobPost();
                    obj.JobPostID = Convert.ToInt64(item["JobPostID"]);
                    obj.RecruitmentRequestID = Convert.ToInt64(item["RecruitmentRequestID"]);
                    obj.Location = Convert.ToInt64(item["LocationID"]);
                    obj.ProjectManagerID = Convert.ToInt64(item["ProjectManagerID"]);
                    obj.ManagerName = item["ManagerName"].ToString();
                    
                    obj.ProjectName = item["ProjectName"].ToString();
                    obj.StartDate = item["StartDate"].ToString();
                    obj.EndDate = item["EndDate"].ToString();
                    obj.JobTitle = item["JobTitle"].ToString();
                    obj.DueDate = item["DueDate"].ToString();
                    obj.JobDescription = item["JobDescription"].ToString();
                    obj.Qualification = item["Qualification"].ToString();
                    obj.Skills = item["Skills"].ToString();
                    obj.Experience = item["Experience"].ToString();
                    obj.AttachmentID = Convert.ToInt64(item["AttachmentID"]);
                    obj.AttachmentName = item["AttachmentName"].ToString();
                    obj.ContentType = item["ContentType"].ToString();
                    obj.Announcement = item["Announcement"].ToString();
                    obj.AnnouncementType = item["AnnouncementType"].ToString();
                    obj.AnnouncementStartDate = Convert.ToDateTime(item["AnnouncementStartDate"]).ToString("yyyy-MM-dd");
                    obj.AnnouncementEndDate = Convert.ToDateTime(item["AnnouncementEndDate"]).ToString("yyyy-MM-dd");
                    obj.Link = item["Link"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"]);
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
                ClsCommon.LogError("Error during GetRecruitmentRequest. The query was executed :", ex.ToString(), "spu_GetCountry", "MasterModal", "MasterModal", "");
            }
            return List;

        }
    }
}