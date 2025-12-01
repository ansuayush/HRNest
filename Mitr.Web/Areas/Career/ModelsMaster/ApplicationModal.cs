using Mitr.Areas.Career.Models;
using Mitr.Areas.Career.ModelsMasterHelper;
using Mitr.CommonClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.Areas.Career.ModelsMaster
{
    public class ApplicationModal: IApplicationHelper
    {
        public string DateFormat = "dd-MMM-yyyy", DateFormatC = "dd-MMM-yy hh:mm:ss tt", DateFormatE = "yyyy-MM-dd";
        public ApplicationModal()
        {
            var d = clsApplicationSetting.GetConfigValue("DateFormat");
            var dC = clsApplicationSetting.GetConfigValue("DateFormatC");
            DateFormat = (!string.IsNullOrEmpty(d) ? d : DateFormat);
            DateFormatC = (!string.IsNullOrEmpty(dC) ? dC : DateFormatC);
        }

        public JobApplication.Apply GetApplyJob(string Code)
        {
            JobApplication.Apply Modal = new JobApplication.Apply();
            DataSet TempModuleDataSet = Common_SPU.fnGetREC_ApplyJob(Code);
            Modal.Status = Convert.ToBoolean(TempModuleDataSet.Tables[0].Rows[0]["Status"]);
            Modal.StatusMessage = TempModuleDataSet.Tables[0].Rows[0]["StatusMessage"].ToString();
            if(TempModuleDataSet.Tables[1] !=null)
            {
                foreach (DataRow item in TempModuleDataSet.Tables[1].Rows)
                {
                    Modal.REC_EVacancyID = Convert.ToInt64(item["REC_EVacancyID"]);
                    Modal.REC_ReqID = Convert.ToInt64(item["REC_ReqID"]);
                    Modal.StartDate = Convert.ToDateTime(item["StartDate"]).ToString(DateFormat);
                    Modal.EndDate = Convert.ToDateTime(item["EndDate"]).ToString(DateFormat);
                    Modal.Staff_Cat = item["Staff_Cat"].ToString();
                    Modal.Job_SubTitle = item["Job_SubTitle"].ToString();
                    Modal.JobTitle = item["JobTitle"].ToString();
                    Modal.JobCode = item["JobCode"].ToString();
                    Modal.ProjectNo = item["projref_no"].ToString();
                    Modal.ProjectName = item["proj_name"].ToString();
                    Modal.Location = item["locationName"].ToString();
                }
            }
            Modal.ThematicList = GetThematicList(TempModuleDataSet.Tables[2]);
            Modal.SkillList = GetSkillList(TempModuleDataSet.Tables[3]);

            List<JobApplication.ProfessionalQual> JobAppList = new List<JobApplication.ProfessionalQual>();
            JobAppList.Add(new JobApplication.ProfessionalQual());
            Modal.ProQualList = JobAppList;

            List<JobApplication.References> ReferencesList = new List<JobApplication.References>();
            ReferencesList.Add(new JobApplication.References());
            ReferencesList.Add(new JobApplication.References());
            Modal.ReferencesList = ReferencesList;

            List<JobApplication.Education> EducationList = new List<JobApplication.Education>();
            EducationList.Add(new JobApplication.Education());
            Modal.EducationList = EducationList;


            Modal.QuestionsList = GetQuestionsList(TempModuleDataSet.Tables[4]);
            return Modal;

        }
        private List<JobApplication.Skill> GetSkillList(DataTable dt)
        {
            List<JobApplication.Skill> List = new List<JobApplication.Skill>();
            JobApplication.Skill Obj = new JobApplication.Skill();
            if (dt != null)
            {

                foreach (DataRow item in dt.Rows)
                {
                    Obj = new JobApplication.Skill();
                    Obj.ID = Convert.ToInt32(item["ID"]);
                    Obj.Name = item["Name"].ToString();
                    List.Add(Obj);
                }
            }

            return List;

        }
        private List<JobApplication.Thematic> GetThematicList(DataTable dt)
        {
            List<JobApplication.Thematic> List = new List<JobApplication.Thematic>();
            JobApplication.Thematic Obj = new JobApplication.Thematic();
            if (dt != null)
            {

                foreach (DataRow item in dt.Rows)
                {
                    Obj = new JobApplication.Thematic();
                    Obj.ID = Convert.ToInt32(item["ID"]);
                    Obj.Name = item["Name"].ToString();
                    List.Add(Obj);
                }
            }

            return List;

        }
        private List<JobApplication.Questions> GetQuestionsList(DataTable dt)
        {
            List<JobApplication.Questions> List = new List<JobApplication.Questions>();
            JobApplication.Questions Obj = new JobApplication.Questions();
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Obj = new JobApplication.Questions();
                    Obj.Question = item["Question"].ToString();
                    List.Add(Obj);
                }
            }
            return List;

        }
    }
}