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
    public class Recruit_Modal : IRecruit_Helper
    {
        public string DateFormat = "dd-MMM-yyyy", DateFormatC = "dd-MMM-yy hh:mm:ss tt", DateFormatE = "yyyy-MM-dd";
        public Recruit_Modal()
        {
            var d = clsApplicationSetting.GetConfigValue("DateFormat");
            var dC = clsApplicationSetting.GetConfigValue("DateFormatC");
            DateFormat = (!string.IsNullOrEmpty(d) ? d : DateFormat);
            DateFormatC = (!string.IsNullOrEmpty(dC) ? dC : DateFormatC);
        }

        public Recruit.Initiate GetREC_Initiate(long ProjectDetailID)
        {

            Recruit.Initiate obj = new Recruit.Initiate();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_Initiate(ProjectDetailID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj.ProjectDetailID = Convert.ToInt64(item["ID"]);
                    obj.ProjectID = Convert.ToInt64(item["Proj_ID"]);
                    obj.ManagerID = Convert.ToInt64(item["ManagerID"]);
                    obj.ThemAreaID = Convert.ToInt64(item["ThemArea_ID"]);
                    obj.LocationID = Convert.ToInt64(item["LocationID"]);
                    obj.LocatioNName = item["locationName"].ToString();
                    obj.Managername = item["Managername"].ToString();
                    obj.ManagerCode = item["ManagerCode"].ToString();
                    obj.ProjectName = item["Proj_Name"].ToString();
                    obj.JobID = Convert.ToInt64(item["JobID"]);
                    obj.StartDate = Convert.ToDateTime(item["Start_Date"]).ToString("dd/MM/yyyy");
                    obj.EndDate = Convert.ToDateTime(item["End_Date"]).ToString("dd/MM/yyyy");
                    obj.Title = item["JobTitle"].ToString();
                    obj.ThemAreaID = Convert.ToInt64(item["themarea_ID"]);
                    obj.ThematicArea = item["thematicarea"].ToString();
                    obj.Time_Per = Convert.ToInt32(item["Time_Per"]);
                    obj.JobDescription = item["JobDesc"].ToString();
                    obj.Qualification = item["JobQualification"].ToString();
                    obj.Skills = item["Skills"].ToString();
                    obj.Experience = item["Experience"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetInternalStaffList. The query was executed :", ex.ToString(), "spu_GetCountry", "MasterModal", "MasterModal", "");
            }
            return obj;


        }

        public Recruit.Request.Fill GetREC_Request(long REC_ReqID)
        {

            Recruit.Request.Fill obj = new Recruit.Request.Fill();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_Requests(REC_ReqID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj.REC_ReqID = Convert.ToInt64(item["REC_ReqID"]);
                    obj.ProjectDetailID = Convert.ToInt64(item["ProjectDetailID"]);
                    obj.ProjectID = Convert.ToInt64(item["Proj_ID"]);
                    obj.ManagerID = Convert.ToInt64(item["ManagerID"]);
                    obj.ThemAreaID = Convert.ToInt64(item["ThemArea_ID"]);
                    obj.LocationID = Convert.ToInt64(item["LocationID"]);
                    obj.LocatioNName = item["LocatioNName"].ToString();
                    obj.Managername = item["Managername"].ToString();
                    obj.ManagerCode = item["ManagerCode"].ToString();
                    obj.ProjectName = item["Proj_Name"].ToString();
                    obj.Job_SubTitle = item["Job_SubTitle"].ToString();
                    obj.REC_Type = item["REC_Type"].ToString();
                    obj.JobID = Convert.ToInt64(item["JobID"]);
                    obj.StartDate = Convert.ToDateTime(item["Start_Date"]).ToString(DateFormat);
                    obj.EndDate = Convert.ToDateTime(item["End_Date"]).ToString(DateFormat);
                    obj.DueDate = Convert.ToDateTime(item["DueDate"]).ToString(DateFormatE);
                    obj.JobTitle = item["JobTitle"].ToString();
                    obj.ThemAreaID = Convert.ToInt64(item["themarea_ID"]);
                    obj.ThematicArea = item["thematicarea"].ToString();
                    obj.Time_Per = item["Time_Per"].ToString() == "" ? 0 : Convert.ToInt32(item["Time_Per"]);
                    obj.JobDescription = item["JobDescription"].ToString();
                    obj.Qualification = item["Qualification"].ToString();
                    obj.Skills = item["Skills"].ToString();
                    obj.Experience = item["Experience"].ToString();
                    obj.Staff_Cat = item["Staff_Cat"].ToString();
                    obj.Project_Tag = item["Project_Tag"].ToString();
                    obj.isdeleted = Convert.ToInt32(item["isdeleted"]);
                    obj.AmendID = Convert.ToInt32(item["AmendID"]);
                    obj.AmendDate = Convert.ToDateTime(item["AmendDate"]).ToString(DateFormatC);
                    obj.AmendRemarks = item["AmendRemarks"].ToString();
                    obj.Recommenders = item["RecommendersID"].ToString();

                    if (obj.isdeleted == 2)
                    {
                        obj.IsBtnVisible = true;
                    }
                    obj.FinalApproverID = Convert.ToInt64(item["FinalApproverID"]);

                }
                obj.EmpList = GetEMPList(TempModuleDataSet.Tables[1]);
                obj.LocationList = GetLocationList(TempModuleDataSet.Tables[2]);
                obj.JOBList = GetJOBList(TempModuleDataSet.Tables[3]);
                obj.PillarList = GetPillarList(TempModuleDataSet.Tables[4]);
                obj.PendingRECList = GetPendingRECList(TempModuleDataSet.Tables[5]);

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetInternalStaffList. The query was executed :", ex.ToString(), "spu_GetCountry", "MasterModal", "MasterModal", "");
            }
            return obj;


        }
        private List<Recruit.PendingRECRequests> GetPendingRECList(DataTable dt)
        {
            List<Recruit.PendingRECRequests> List = new List<Recruit.PendingRECRequests>();
            Recruit.PendingRECRequests Obj = new Recruit.PendingRECRequests();
            if (dt != null)
            {

                foreach (DataRow item in dt.Rows)
                {
                    Obj = new Recruit.PendingRECRequests();
                    Obj.ID = Convert.ToInt32(item["ID"]);
                    Obj.Name = item["Name"].ToString();
                    List.Add(Obj);
                }
            }

            return List;

        }

        private List<Recruit.JOB> GetJOBList(DataTable dt)
        {
            List<Recruit.JOB> List = new List<Recruit.JOB>();
            Recruit.JOB Obj = new Recruit.JOB();
            if (dt != null)
            {

                foreach (DataRow item in dt.Rows)
                {
                    Obj = new Recruit.JOB();
                    Obj.ID = Convert.ToInt32(item["ID"]);
                    Obj.Name = item["Name"].ToString();
                    List.Add(Obj);
                }
            }
            return List;
        }

        private List<Recruit.Pillar> GetPillarList(DataTable dt)
        {
            List<Recruit.Pillar> List = new List<Recruit.Pillar>();
            Recruit.Pillar Obj = new Recruit.Pillar();
            if (dt != null)
            {

                foreach (DataRow item in dt.Rows)
                {
                    Obj = new Recruit.Pillar();
                    Obj.ID = Convert.ToInt32(item["ID"]);
                    Obj.Name = item["Name"].ToString();
                    List.Add(Obj);
                }
            }
            return List;
        }

        private List<Recruit.Location> GetLocationList(DataTable dt)
        {
            List<Recruit.Location> List = new List<Recruit.Location>();
            Recruit.Location Obj = new Recruit.Location();
            if (dt != null)
            {

                foreach (DataRow item in dt.Rows)
                {
                    Obj = new Recruit.Location();
                    Obj.ID = Convert.ToInt32(item["ID"]);
                    Obj.Name = item["Name"].ToString();
                    List.Add(Obj);
                }
            }
            return List;
        }

        private List<Recruit.EMP> GetEMPList(DataTable dt)
        {
            List<Recruit.EMP> List = new List<Recruit.EMP>();
            Recruit.EMP Obj = new Recruit.EMP();
            if (dt != null)
            {

                foreach (DataRow item in dt.Rows)
                {
                    Obj = new Recruit.EMP();
                    Obj.ID = Convert.ToInt32(item["ID"]);
                    Obj.Name = item["Name"].ToString();
                    List.Add(Obj);
                }
            }

            return List;

        }

        public List<Recruit.InternalStaff> GetIAvailStaff(long REC_ReqID, long PillarID, long LocationID, long JobID)
        {
            List<Recruit.InternalStaff> List = new List<Recruit.InternalStaff>();
            Recruit.InternalStaff obj = new Recruit.InternalStaff();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_IAvailStaff(REC_ReqID, PillarID, LocationID, JobID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Recruit.InternalStaff();
                    obj.EmpID = Convert.ToInt64(item["ID"]);
                    obj.EMPCode = item["EMP_Code"].ToString();
                    obj.EmpName = item["Emp_Name"].ToString();
                    obj.Relocation = item["Relocation"].ToString();
                    obj.Pillar = item["Pillar"].ToString();
                    obj.JobTitle = item["JobTitle"].ToString();

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetInternalStaffList. The query was executed :", ex.ToString(), "spu_GetCountry", "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public Recruit.Request.View GetREC_Request_View(long REC_ReqID)
        {

            Recruit.Request.View obj = new Recruit.Request.View();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_RequestsView(REC_ReqID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj.REC_Code = item["REC_Code"].ToString();
                    obj.REC_ReqID = Convert.ToInt64(item["REC_ReqID"]);
                    obj.ProjectDetailID = Convert.ToInt64(item["ProjectDetailID"]);
                    obj.ProjectID = Convert.ToInt64(item["Proj_ID"]);
                    obj.ManagerID = Convert.ToInt64(item["ManagerID"]);
                    obj.ThemAreaID = Convert.ToInt64(item["ThemArea_ID"]);
                    obj.LocationID = Convert.ToInt64(item["LocationID"]);
                    obj.LocatioNName = item["LocatioNName"].ToString();
                    obj.Managername = item["Managername"].ToString();
                    obj.ManagerCode = item["ManagerCode"].ToString();
                    obj.ProjectName = item["Proj_Name"].ToString();
                    obj.Job_SubTitle = item["Job_SubTitle"].ToString();
                    obj.REC_Type = item["REC_Type"].ToString();
                    obj.JobID = Convert.ToInt64(item["JobID"]);
                    obj.StartDate = Convert.ToDateTime(item["Start_Date"]).ToString(DateFormat);
                    obj.EndDate = Convert.ToDateTime(item["End_Date"]).ToString(DateFormat);
                    obj.DueDate = Convert.ToDateTime(item["DueDate"]).ToString(DateFormat);
                    obj.JobTitle = item["JobTitle"].ToString();
                    obj.ThemAreaID = Convert.ToInt64(item["themarea_ID"]);
                    obj.ThematicArea = item["thematicarea"].ToString();
                    obj.Time_Per = Convert.ToInt32(item["Time_Per"]);
                    obj.JobDescription = item["JobDescription"].ToString();
                    obj.Qualification = item["Qualification"].ToString();
                    obj.SkillsName = item["SkillsName"].ToString().Trim().TrimEnd(',');
                    obj.Experience = item["Experience"].ToString();
                    obj.Staff_Cat = item["Staff_Cat"].ToString();
                    obj.Project_Tag = item["Project_Tag"].ToString();
                    obj.AmendID = Convert.ToInt32(item["AmendID"]);
                    obj.AmendDate = Convert.ToDateTime(item["AmendDate"]).ToString(DateFormatC);
                    obj.AmendRemarks = item["AmendRemarks"].ToString();

                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetREC_Request_View. The query was executed :", ex.ToString(), "fnGetREC_Requests", "Recruit_Modal", "MasterModal", "");
            }
            return obj;


        }

        public List<Recruit.Recom_Preference> GetREC_Recom_Preference(long REC_ReqID)
        {
            List<Recruit.Recom_Preference> List = new List<Recruit.Recom_Preference>();
            Recruit.Recom_Preference obj = new Recruit.Recom_Preference();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_IPreference(REC_ReqID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Recruit.Recom_Preference();
                    obj.EmpID = Convert.ToInt64(item["EMPID"]);
                    obj.EMPCode = item["EMP_Code"].ToString();
                    obj.EmpName = item["Emp_Name"].ToString();
                    obj.Relocation = item["Relocation"].ToString();
                    obj.Pillar = item["Pillar"].ToString();
                    obj.JobTitle = item["JobTitle"].ToString();
                    obj.RelocationByHR = item["Relocation"].ToString();
                    obj.PillarByHR = item["PillarByHR"].ToString();
                    obj.JobTitleByHR = item["JobTitleByHR"].ToString();

                    obj.Location = item["Location"].ToString();
                    obj.Current_Location = item["Current_Location"].ToString();
                    obj.TH_Areas = item["TH_Areas"].ToString();
                    obj.Current_TH_Areas = item["Current_TH_Areas"].ToString();
                    obj.Job = item["Job"].ToString();
                    obj.Current_Job = item["Current_Job"].ToString();

                    obj.comment = item["comment"].ToString();
                    obj._Preference = Convert.ToInt32(item["Preference"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetREC_Preference. The query was executed :", ex.ToString(), "fnGetREC_Preference", "MasterModal", "MasterModal", "");
            }
            return List;

        }


        public Recruit.Final_Preference GetREC_Final_Preference(long REC_ReqID)
        {
            Recruit.Final_Preference obj = new Recruit.Final_Preference();

            List<Recruit.Final_Preference.Staff> SList = new List<Recruit.Final_Preference.Staff>();
            Recruit.Final_Preference.Staff Sobj = new Recruit.Final_Preference.Staff();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_IFinal(REC_ReqID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Sobj = new Recruit.Final_Preference.Staff();
                    Sobj.REC_ReqID = Convert.ToInt64(item["REC_ReqID"]);
                    Sobj.EmpID = Convert.ToInt64(item["EMPID"]);
                    Sobj.EMPCode = item["EMP_Code"].ToString();
                    Sobj.EmpName = item["Emp_Name"].ToString();
                    Sobj.ApproverID = Convert.ToInt64(item["ApproverID"]);
                    Sobj.ApproverName = item["ApproverName"].ToString();
                    Sobj.ApproverCode = item["ApproverCode"].ToString();
                    Sobj.Relocation = item["Relocation"].ToString();
                    Sobj.Pillar = item["Pillar"].ToString();
                    Sobj.JobTitle = item["JobTitle"].ToString();
                    Sobj.RelocationByHR = item["Relocation"].ToString();
                    Sobj.PillarByHR = item["PillarByHR"].ToString();
                    Sobj.JobTitleByHR = item["JobTitleByHR"].ToString();
                    Sobj.comment = item["comment"].ToString();
                    Sobj.Preference = Convert.ToInt32(item["Preference"]);
                    Sobj.Selected = Convert.ToInt32(item["Selected"]);
                    Sobj.FinalComment = item["FinalComment"].ToString();
                    SList.Add(Sobj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetREC_Final_Preference. The query was executed :", ex.ToString(), "fnGetREC_Preference", "MasterModal", "MasterModal", "");
            }
            obj.StaffList = SList;

            obj.Remarks = obj.StaffList.Where(x => !string.IsNullOrEmpty(x.FinalComment)).Select(x => x.FinalComment).FirstOrDefault();
            if (!obj.StaffList.Any(x => x.Selected > 0))
            {
                obj.IsBtnVisible = true;
            }

            if (obj.StaffList.Any(x => x.Preference == 0))
            {
                obj.IsBtnVisible = false;
            }
            return obj;

        }




        public EXRecruit.View GetEREC_Request_View(long REC_ReqID)
        {

            EXRecruit.View obj = new EXRecruit.View();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_RequestsView(REC_ReqID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj.REC_Code = item["REC_Code"].ToString();
                    obj.REC_ReqID = Convert.ToInt64(item["REC_ReqID"]);
                    obj.ProjectDetailID = Convert.ToInt64(item["ProjectDetailID"]);
                    obj.ProjectID = Convert.ToInt64(item["Proj_ID"]);
                    obj.ManagerID = Convert.ToInt64(item["ManagerID"]);
                    obj.ThemAreaID = Convert.ToInt64(item["ThemArea_ID"]);
                    obj.LocationID = Convert.ToInt64(item["LocationID"]);
                    obj.LocatioNName = item["LocatioNName"].ToString();
                    obj.Managername = item["Managername"].ToString();
                    obj.ManagerCode = item["ManagerCode"].ToString();
                    obj.ProjectName = item["Proj_Name"].ToString();
                    obj.Job_SubTitle = item["Job_SubTitle"].ToString();
                    obj.REC_Type = item["REC_Type"].ToString();
                    obj.JobID = Convert.ToInt64(item["JobID"]);
                    obj.StartDate = Convert.ToDateTime(item["Start_Date"]).ToString(DateFormat);
                    obj.EndDate = Convert.ToDateTime(item["End_Date"]).ToString(DateFormat);
                    obj.DueDate = Convert.ToDateTime(item["DueDate"]).ToString(DateFormat);
                    obj.JobTitle = item["JobTitle"].ToString();
                    obj.ThemAreaID = Convert.ToInt64(item["themarea_ID"]);
                    obj.ThematicArea = item["thematicarea"].ToString();
                    obj.Time_Per = Convert.ToInt32(item["Time_Per"]);
                    obj.JobDescription = item["JobDescription"].ToString();
                    obj.Qualification = item["Qualification"].ToString();
                    obj.SkillsName = item["SkillsName"].ToString().Trim().TrimEnd(',');
                    obj.Experience = item["Experience"].ToString();
                    obj.Staff_Cat = item["Staff_Cat"].ToString();
                    obj.Project_Tag = item["Project_Tag"].ToString();
                    obj.AmendID = Convert.ToInt32(item["AmendID"]);
                    obj.AmendDate = Convert.ToDateTime(item["AmendDate"]).ToString(DateFormatC);
                    obj.AmendRemarks = item["AmendRemarks"].ToString();

                }
                obj.PendingList = GetEPendingReqList(TempModuleDataSet.Tables[1]);
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetREC_Request_View. The query was executed :", ex.ToString(), "fnGetREC_Requests", "Recruit_Modal", "MasterModal", "");
            }
            return obj;
        }
        private List<EXRecruit.PendingRECRequests> GetEPendingReqList(DataTable dt)
        {
            List<EXRecruit.PendingRECRequests> List = new List<EXRecruit.PendingRECRequests>();
            EXRecruit.PendingRECRequests Obj = new EXRecruit.PendingRECRequests();
            if (dt != null)
            {

                foreach (DataRow item in dt.Rows)
                {
                    Obj = new EXRecruit.PendingRECRequests();
                    Obj.ID = Convert.ToInt32(item["ID"]);
                    Obj.Name = item["Name"].ToString();
                    List.Add(Obj);
                }
            }

            return List;

        }


        public EXRecruit.Vacancy.HR GetVacancy_HR(long REC_ReqID)
        {

            EXRecruit.Vacancy.HR obj = new EXRecruit.Vacancy.HR();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EVacancyAnno(REC_ReqID);
                if (TempModuleDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {
                        obj.REC_ReqID = Convert.ToInt64(item["REC_ReqID"]);
                        obj.REC_EVacancyID = Convert.ToInt64(item["REC_EVacancyID"]);
                        obj.REC_Code = item["REC_Code"].ToString();
                        obj.HRVacancyDes = item["HRVacancyDes"].ToString();
                        obj.HRAttach = item["HRAttach"].ToString();
                        obj.isdeleted = Convert.ToInt32(item["isdeleted"]);
                        obj.AttachmentID = Convert.ToInt32(item["HRAttachID"]);
                        obj.Approved = item["Approved"].ToString();

                        if (obj.isdeleted == 2)
                        {
                            obj.btnVisible = true;
                        }
                    }
                }
                else
                {
                    obj.btnVisible = true;

                }



            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetREC_Request_View. The query was executed :", ex.ToString(), "fnGetREC_Requests", "Recruit_Modal", "MasterModal", "");
            }
            return obj;
        }


        public EXRecruit.Vacancy.Comm GetVacancy_Comm(long REC_ReqID)
        {

            EXRecruit.Vacancy.Comm obj = new EXRecruit.Vacancy.Comm();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EVacancyAnno(REC_ReqID);
                if (TempModuleDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {
                        obj.REC_ReqID = Convert.ToInt64(item["REC_ReqID"]);
                        obj.REC_EVacancyID = Convert.ToInt64(item["REC_EVacancyID"]);
                        obj.REC_Code = item["REC_Code"].ToString();
                        obj.CommVacancyDes = item["CommVacancyDes"].ToString();
                        obj.CommHRAttachID = Convert.ToInt32(item["CommHRAttachID"]);
                        if (string.IsNullOrEmpty(obj.CommVacancyDes))
                        {
                            obj.CommVacancyDes = item["HRVacancyDes"].ToString();
                        }
                        obj.HRAttachID = item["HRAttachID"].ToString();
                        obj.Approved = Convert.ToInt32(item["Approved"]);

                        if (obj.Approved == 0)
                        {
                            obj.btnVisible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetREC_Request_View. The query was executed :", ex.ToString(), "fnGetREC_Requests", "Recruit_Modal", "MasterModal", "");
            }
            return obj;
        }

        public EXRecruit.Vacancy.Final GetVacancy_Final(long REC_ReqID)
        {

            EXRecruit.Vacancy.Final obj = new EXRecruit.Vacancy.Final();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EVacancyAnno(REC_ReqID);
                if (TempModuleDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {
                        obj.REC_ReqID = Convert.ToInt64(item["REC_ReqID"]);
                        obj.REC_EVacancyID = Convert.ToInt64(item["REC_EVacancyID"]);
                        obj.REC_Code = item["REC_Code"].ToString();
                        obj.CommVacancyDes = item["CommVacancyDes"].ToString();
                        obj.CommHRAttachID = Convert.ToInt32(item["CommHRAttachID"]);
                        obj.StartDate = (Convert.ToDateTime(item["StartDate"]).Year > 1900 ? Convert.ToDateTime(item["StartDate"]).ToString(DateFormatE) : "");
                        obj.EndDate = (Convert.ToDateTime(item["EndDate"]).Year > 1900 ? Convert.ToDateTime(item["EndDate"]).ToString(DateFormatE) : "");
                        obj.Approved = Convert.ToInt32(item["Approved"]);

                        obj.Web_Announce = item["Web_Announce"].ToString();
                        obj.Internal_Announce = item["Internal_Announce"].ToString();
                        obj.Other_Announce = item["Other_Announce"].ToString();
                        if (obj.Approved == 1)
                        {
                            obj.btnVisible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetREC_Request_View. The query was executed :", ex.ToString(), "fnGetREC_Requests", "Recruit_Modal", "MasterModal", "");
            }
            return obj;
        }


        public EXRecruit.Final_ConfirmedCV GetFinishConfirmedCV(long REC_ReqID)
        {
            EXRecruit.Final_ConfirmedCV obj = new EXRecruit.Final_ConfirmedCV();

            List<EXRecruit.Final_ConfirmedCV.Application> SList = new List<EXRecruit.Final_ConfirmedCV.Application>();
            EXRecruit.Final_ConfirmedCV.Application Sobj = new EXRecruit.Final_ConfirmedCV.Application();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EConfirmedCV(REC_ReqID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Sobj = new EXRecruit.Final_ConfirmedCV.Application();
                    Sobj.REC_ShortID = Convert.ToInt64(item["REC_ShortID"]);
                    Sobj.REC_AppID = Convert.ToInt64(item["REC_AppID"]);
                    Sobj.Name = item["Name"].ToString();
                    Sobj.Mobile = item["Mobile"].ToString();
                    Sobj.EmailID = item["EmailID"].ToString();
                    Sobj.CVAttachID = Convert.ToInt64(item["CVAttachID"]);
                    Sobj.ApproverID = Convert.ToInt64(item["ApproverID"]);
                    Sobj.ApproverName = item["ApproverName"].ToString();
                    Sobj.ApproverCode = item["ApproverCode"].ToString();
                    Sobj.CVPath = item["CVPath"].ToString();
                    Sobj.Comment = item["comment"].ToString();
                    Sobj.Preference = Convert.ToInt32(item["Preference"]);
                    Sobj.Selected = Convert.ToInt32(item["Selected"]);
                    Sobj.FinalComment = item["FinalComment"].ToString();
                    Sobj.Approved = Convert.ToInt32(item["Approved"]);
                    SList.Add(Sobj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetREC_EConfirmedCV. The query was executed :", ex.ToString(), "GetFinishConfirmedCV", "RecruitModal", "RecruitModal", "");
            }
            obj.ApplicationList = SList;

            obj.Remarks = obj.ApplicationList.Where(x => !string.IsNullOrEmpty(x.FinalComment)).Select(x => x.FinalComment).FirstOrDefault();
            if (obj.ApplicationList.Any(x => x.Selected == 0))
            {
                obj.IsBtnVisible = true;
            }


            return obj;

        }


        public List<Interview.Round> GetInterviewRound(long REC_ReqID, long REC_InterviewSetID)
        {
            List<Interview.Round> List = new List<Interview.Round>();
            Interview.Round obj = new Interview.Round();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EInterviewSetting(REC_ReqID, REC_InterviewSetID, "Round");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Interview.Round();
                    obj.REC_InterviewSetID = Convert.ToInt32(item["REC_InterviewSetID"]);
                    obj.srno = Convert.ToInt32(item["Srno"]);
                    obj.RoundName = item["RoundName"].ToString();
                    obj.IsNegotiationRound = Convert.ToBoolean(item["IsNegotiationRound"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.RoundDesc = item["RoundDesc"].ToString();

                    obj.MemberList = GetInterviewMember(REC_ReqID, obj.REC_InterviewSetID);
                    obj.SlotList = GetInterviewSlot(REC_ReqID, obj.REC_InterviewSetID);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetJobList. The query was executed :", ex.ToString(), "", "MasterModal", "MasterModal", "");
            }
            return List;

        }
        private List<Interview.Member> GetInterviewMember(long REC_ReqID, long REC_InterviewSetID)
        {
            List<Interview.Member> List = new List<Interview.Member>();
            Interview.Member obj = new Interview.Member();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EInterviewSetting(REC_ReqID, REC_InterviewSetID, "Member");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Interview.Member();
                    obj.REC_InterviewSetID = Convert.ToInt32(item["REC_InterviewSetID"]);
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

        private List<Interview.Slot> GetInterviewSlot(long REC_ReqID, long REC_InterviewSetID)
        {
            List<Interview.Slot> List = new List<Interview.Slot>();
            Interview.Slot obj = new Interview.Slot();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EInterviewSetting(REC_ReqID, REC_InterviewSetID, "Slot");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Interview.Slot();
                    obj.REC_InterviewSetID = Convert.ToInt32(item["REC_InterviewSetID"]);
                    obj.SlotDate = (Convert.ToDateTime(item["SlotDate"]).Year > 1900 ? Convert.ToDateTime(item["SlotDate"]).ToString(DateFormatE) : "");
                    obj.MAXCV = item["MAXCV"].ToString();
                    obj.FromTime = Convert.ToDateTime(item["FromTime"]).ToString("hh:mm");
                    obj.ToTime = Convert.ToDateTime(item["ToTime"]).ToString("hh:mm");
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetJobMember. The query was executed :", ex.ToString(), "", "MasterModal", "MasterModal", "");
            }
            return List;
        }



        public List<InterviewSelection.Round> GetInterviewSelectionRound(long REC_ReqID)
        {
            List<InterviewSelection.Round> List = new List<InterviewSelection.Round>();
            InterviewSelection.Round obj = new InterviewSelection.Round();
            // obj.REC_InterviewSetID = 0;
            obj.srno = 1;
            obj.RoundName = "Confirmed List";
            obj.ApplicationList = GetInterviewCandidate(REC_ReqID, 0);
            List.Add(obj);
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EInterviewSetting(REC_ReqID, 0, "Round");
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new InterviewSelection.Round();
                    obj.REC_InterviewSetID = Convert.ToInt32(item["REC_InterviewSetID"]);
                    obj.srno = Convert.ToInt32(item["Srno"]);
                    obj.RoundName = item["RoundName"].ToString();
                    obj.IsNegotiationRound = Convert.ToBoolean(item["IsNegotiationRound"]);
                    obj.Priority = Convert.ToInt32(item["Priority"]);
                    obj.RoundDesc = item["RoundDesc"].ToString();
                    obj.ApplicationList = GetInterviewCandidate(REC_ReqID, obj.REC_InterviewSetID);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetJobList. The query was executed :", ex.ToString(), "", "MasterModal", "MasterModal", "");
            }
            return List;
        }


        private List<InterviewSelection.Application> GetInterviewCandidate(long REC_ReqID, long REC_InterviewSetID)
        {
            List<InterviewSelection.Application> List = new List<InterviewSelection.Application>();
            InterviewSelection.Application obj = new InterviewSelection.Application();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EInterviewCandidate(REC_ReqID, REC_InterviewSetID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new InterviewSelection.Application();
                    obj.REC_ReqID = Convert.ToInt32(item["REC_ReqID"]);
                    obj.REC_AppID = Convert.ToInt32(item["REC_AppID"]);
                    obj.Rejected = Convert.ToInt32(item["Rejected"]);
                    obj.REC_InterviewSetID = Convert.ToInt32(item["REC_InterviewSetID"]);
                    obj.EmailID = item["EmailID"].ToString();
                    obj.Name = item["Name"].ToString();
                    obj.Gender = item["Gender"].ToString();
                    obj.ApplyDate = item["ApplyDate"].ToString();
                    obj.CVPath = item["CVPath"].ToString();
                    obj.CVAttachID = Convert.ToInt32(item["CVAttachID"]);


                    obj.Nationality = item["Nationality"].ToString();
                    obj.TotalExperience = item["TotalExperience"].ToString();
                    obj.CurrentSalary = item["CurrentSalary"].ToString();
//                    obj.NegotiationSalary = item["NegotiationSalary"].ToString();
                    obj.ExpectedSalary = item["ExpectedSalary"].ToString();
                    obj.Reason = item["Reason"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetInterviewCandidate. The query was executed :", ex.ToString(), "", "MasterModal", "MasterModal", "");
            }
            return List;
        }
        //public List< InterviewSelection.MoveToRound> GetMoveToRound(long REC_AppID)
        //{
        //    List<InterviewSelection.MoveToRound> list = new List<InterviewSelection.MoveToRound>();
        //    //InterviewSelection.MoveToRound Sobj = new InterviewSelection.MoveToRound();
        //    try
        //    {
        //        DataSet TempModuleDataSet = Common_SPU.fnGetREC_EApplication_InterviewP(REC_AppID);
        //        foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
        //        {
        //            InterviewSelection.MoveToRound Sobj = new InterviewSelection.MoveToRound();
        //            Sobj.REC_AppID = Convert.ToInt64(item["REC_AppID"]);
        //            Sobj.Name = item["Name"].ToString();
        //            Sobj.ApplyDate = item["ApplyDate"].ToString();
        //            Sobj.CurrentSalary = item["CurrentSalary"].ToString();
        //            Sobj.ExpectedSalary = item["ExpectedSalary"].ToString();
        //            Sobj.Score = item["Score"].ToString();
        //            Sobj.Reason = item["Remarks"].ToString();
        //            //Sobj.RoundName = item["RoundName"].ToString();
        //            //Sobj.PanelName = item["PanelName"].ToString();
        //            //Sobj.Description = item["Description"].ToString();
        //            //Sobj.REC_InterviewSetID = Convert.ToInt64(item["REC_InterviewSetId"]);
        //            //Sobj.REC_InterviewID = Convert.ToInt64(item["REC_InterviewID"]);
        //            list.Add(Sobj);
        //        }
        //        //Sobj.AttachmentsList = GetInterviewP_Attachment(TempModuleDataSet.Tables[1]);
        //        //Sobj.RoundList = GetInterviewP_Round(TempModuleDataSet.Tables[2]);

        //        //Sobj.SlotList = new List<InterviewSelection.ddList>();
        //    }
        //    catch (Exception ex)
        //    {
        //        ClsCommon.LogError("Error during fnGetREC_EConfirmedCV. The query was executed :", ex.ToString(), "GetFinishConfirmedCV", "RecruitModal", "RecruitModal", "");
        //    }
        //    return list;
        //}
        // code coment by shailendra 21/01/2023
        public InterviewSelection.MoveToRound GetMoveToRound(long REC_AppID)
        {
            InterviewSelection.MoveToRound Sobj = new InterviewSelection.MoveToRound();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EApplication_InterviewP(REC_AppID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Sobj.REC_AppID = Convert.ToInt64(item["REC_AppID"]);
                    Sobj.Name = item["Name"].ToString();
                    Sobj.ApplyDate = item["ApplyDate"].ToString();
                    Sobj.CurrentSalary = item["CurrentSalary"].ToString();
                    Sobj.ExpectedSalary = item["ExpectedSalary"].ToString();
                    //Sobj.Score = item["Score"].ToString();
                    //Sobj.Reason = item["Remarks"].ToString();
                    //Sobj.RoundName = item["RoundName"].ToString();
                    //Sobj.PanelName = item["PanelName"].ToString();
                }
                Sobj.AttachmentsList = GetInterviewP_Attachment(TempModuleDataSet.Tables[1]);
                Sobj.RoundList = GetInterviewP_Round(TempModuleDataSet.Tables[2]);
                Sobj.QulifiedRoundList = GetQulifiedRoundList(TempModuleDataSet.Tables[4]);
                Sobj.SlotList = new List<InterviewSelection.ddList>();
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetREC_EConfirmedCV. The query was executed :", ex.ToString(), "GetFinishConfirmedCV", "RecruitModal", "RecruitModal", "");
            }
            return Sobj;
        }

        public InterviewSelection.Approve GetInterviewApprove(long REC_AppID)
        {
            InterviewSelection.Approve Sobj = new InterviewSelection.Approve();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EApplication_InterviewP(REC_AppID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    Sobj.REC_AppID = Convert.ToInt64(item["REC_AppID"]);
                    Sobj.Name = item["Name"].ToString();
                    Sobj.ApplyDate = item["ApplyDate"].ToString();
                    Sobj.CurrentSalary = item["CurrentSalary"].ToString();
                    Sobj.ExpectedSalary = item["ExpectedSalary"].ToString();
                    Sobj.NegotiationSalary = Convert.ToDecimal(item["NegotiationSalary"]);
                    Sobj.ExpectedJDate = (Convert.ToDateTime(item["ExpectedJDate"]).Year > 1900 ? Convert.ToDateTime(item["ExpectedJDate"]).ToString(DateFormatE) : "");
                    Sobj.Reason = item["Remarks"].ToString();
                    Sobj.Score = item["Score"].ToString();
                }
                Sobj.AttachmentsList = GetInterviewP_Attachment(TempModuleDataSet.Tables[1]);
                Sobj.TagRECRequestsList = GetInterviewP_TagRECRequests(TempModuleDataSet.Tables[3]);


            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetREC_EConfirmedCV. The query was executed :", ex.ToString(), "GetFinishConfirmedCV", "RecruitModal", "RecruitModal", "");
            }
            return Sobj;
        }

        private List<InterviewSelection.ddList> GetInterviewP_Round(DataTable dt)
        {
            List<InterviewSelection.ddList> List = new List<InterviewSelection.ddList>();
            InterviewSelection.ddList Obj = new InterviewSelection.ddList();
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Obj = new InterviewSelection.ddList();
                    Obj.ID = Convert.ToInt32(item["ID"]);
                    Obj.Name = item["Name"].ToString();
                    List.Add(Obj);
                }
            }

            return List;

        }
        private List<InterviewSelection.QulifiedRound> GetQulifiedRoundList(DataTable dt)
        {
            List<InterviewSelection.QulifiedRound> List = new List<InterviewSelection.QulifiedRound>();
            InterviewSelection.QulifiedRound Obj = new InterviewSelection.QulifiedRound();
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Obj = new InterviewSelection.QulifiedRound();
                    Obj.ID = Convert.ToInt32(item["ID"]);
                    Obj.RowNum = Convert.ToInt32(item["RowNum"]);
                    Obj.RoundID = Convert.ToInt32(item["RoundID"].ToString());
                    Obj.RoundName = item["RoundName"].ToString();
                    Obj.REC_InterviewSetID = Convert.ToInt32(item["REC_InterviewSetID"].ToString());
                    Obj.REC_ReqID = Convert.ToInt32(item["REC_ReqID"].ToString());
                    Obj.REC_InterviewID = Convert.ToInt32(item["REC_InterviewID"].ToString());
                    Obj.Score = item["Score"].ToString();
                    Obj.Remarks = item["Remarks"].ToString();
                    Obj.PanelMember = item["PanelMember"].ToString();
                    Obj.SlotDate = item["SlotDate"].ToString();
                    List.Add(Obj);
                }
            }

            return List;

        }
        private List<InterviewSelection.Attachments> GetInterviewP_Attachment(DataTable dt)
        {
            List<InterviewSelection.Attachments> List = new List<InterviewSelection.Attachments>();
            InterviewSelection.Attachments Obj = new InterviewSelection.Attachments();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Obj = new InterviewSelection.Attachments();
                        Obj.ID = Convert.ToInt32(item["ID"]);
                        Obj.Description = item["Descrip"].ToString();
                        Obj.FileName = item["FileName"].ToString();
                        Obj.AttachmentPath = item["AttachmentPath"].ToString();
                        Obj.FileExt = item["content_type"].ToString();
                        List.Add(Obj);
                    }
                }
                else
                {
                    Obj = new InterviewSelection.Attachments();
                    List.Add(Obj);
                }
            }
            else
            {
                Obj = new InterviewSelection.Attachments();
                List.Add(Obj);
            }

            return List;

        }

        private List<InterviewSelection.TagRECRequests> GetInterviewP_TagRECRequests(DataTable dt)
        {
            List<InterviewSelection.TagRECRequests> List = new List<InterviewSelection.TagRECRequests>();
            InterviewSelection.TagRECRequests Obj = new InterviewSelection.TagRECRequests();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Obj = new InterviewSelection.TagRECRequests();
                        Obj.REC_ReqID = Convert.ToInt64(item["REC_ReqID"]);
                        Obj.REC_Code = item["REC_Code"].ToString();

                        Obj.Job_SubTitle = item["Job_SubTitle"].ToString();
                        Obj.DueDate = (Convert.ToDateTime(item["DueDate"]).Year > 1900 ? Convert.ToDateTime(item["DueDate"]).ToString(DateFormatE) : "");


                        Obj.Staff_Cat = item["Staff_Cat"].ToString();
                        Obj.Experience = item["Experience"].ToString();

                        Obj.Time_Per = Convert.ToInt32(item["Time_Per"]);
                        Obj.JobTitle = item["JobTitle"].ToString();
                        Obj.Job_SubTitle = item["Job_SubTitle"].ToString();
                        Obj.JobCode = item["JobCode"].ToString();
                        Obj.projref_no = item["projref_no"].ToString();
                        Obj.ManagerName = item["ManagerName"].ToString();
                        Obj.proj_name = item["proj_name"].ToString();
                        Obj.locationName = item["locationName"].ToString();

                        List.Add(Obj);
                    }
                }

            }

            return List;

        }

        public List<InterviewSelection.ddList> GetInterviewRound(long RecId)
        {
            List<InterviewSelection.ddList> List = new List<InterviewSelection.ddList>();
            InterviewSelection.ddList Obj = new InterviewSelection.ddList();
            DataSet TempModuleDataSet = Common_SPU.fnGetRECRoundName(RecId);

            foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
            {
                Obj = new InterviewSelection.ddList();
                Obj.ID = Convert.ToInt32(item["ID"]);
                Obj.Name = item["Name"].ToString();
                List.Add(Obj);
            }


            return List;

        }

        public List<InterviewSelection.Attachments> GetInterviewRound_Attachment(long REC_AppID)
        {
            List<InterviewSelection.Attachments> List = new List<InterviewSelection.Attachments>();
            InterviewSelection.Attachments Obj = new InterviewSelection.Attachments();
            DataSet TempModuleDataSet = Common_SPU.fnGetREC_EApplication_InterviewP(REC_AppID);
            if (TempModuleDataSet.Tables[1] != null)
            {
                if (TempModuleDataSet.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[1].Rows)
                    {
                        Obj = new InterviewSelection.Attachments();
                        Obj.ID = Convert.ToInt32(item["ID"]);
                        Obj.Description = item["Descrip"].ToString();
                        Obj.FileName = item["FileName"].ToString();
                        Obj.AttachmentPath = item["AttachmentPath"].ToString();
                        Obj.FileExt = item["content_type"].ToString();
                        List.Add(Obj);
                    }
                }
                else
                {
                    Obj = new InterviewSelection.Attachments();
                    List.Add(Obj);
                }
            }
            else
            {
                Obj = new InterviewSelection.Attachments();
                List.Add(Obj);
            }

            return List;

        }
        public List<InterviewSelection.Attachments> GetInterviewRound_AttachementList(long REC_InterviewSetID)
        {
            List<InterviewSelection.Attachments> List = new List<InterviewSelection.Attachments>();
            InterviewSelection.Attachments Obj = new InterviewSelection.Attachments();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetREC_EApplication_InterviewAttachementlist(REC_InterviewSetID);

                if (TempModuleDataSet.Tables[0].Rows.Count > 0)
                {
                    long count = 0;
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {
                        count++;
                        Obj = new InterviewSelection.Attachments();
                        Obj.CountRow = count;
                        Obj.ID = Convert.ToInt32(item["ID"]);
                        Obj.Description = item["Descrip"].ToString();
                        Obj.FileName = item["FileName"].ToString();
                        Obj.AttachmentPath = item["AttachmentPath"].ToString();
                        Obj.FileExt = item["content_type"].ToString();
                        List.Add(Obj);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetREC_EConfirmedCV. The query was executed :", ex.ToString(), "GetInterviewRound_AttachementList", "RecruitModal", "RecruitModal", "");
            }

            return List;

        }
    }
}