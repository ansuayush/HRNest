using CrystalDecisions.CrystalReports.ViewerObjectModel;
using Mitr.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Exit
    {

        public class Tabs
        {
            public int Approve { get; set; }
        }
        public class DDValues
        {
            public long ID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
        public class Attachment
        {
            public long ID { get; set; }
            public string FileName { get; set; }
            public string Decription { get; set; }

            public HttpPostedFileBase Upload { get; set; }
        }

        public class Exit_Task
        {
            public long Exit_TaskID { get; set; }
            public long Exit_ID { get; set; }
            public string Task { get; set; }
            public long[] Assignee_ID { get; set; }
            public string Priority { get; set; }
        }
        public class EMPList
        {
            public long EMP_ID { get; set; }
            public string EMP_Name { get; set; }
            public string Email { get; set; }
        }

        public class Request_View
        {
            public long Exit_ID { get; set; }
            public string NoticePeriod { get; set; }
            public string Req_No { get; set; }
            public string Req_Date { get; set; }
            public long EMP_ID { get; set; }
            public string EMP_Name { get; set; }
            public string EMP_Code { get; set; }
            public string Resignation_Reason { get; set; }
            public string Comment { get; set; }
            public int IsServeNotice { get; set; }
            public string RelievingDate { get; set; }
            public string Actual_RelievingDate { get; set; }
            public string ReasonForNotServingNotice { get; set; }
            public string DesignationName { get; set; }
            public string Department { get; set; }
            public string CreatedByName { get; set; }

            public int Approved { get; set; }
            public bool IsRetained { get; set; }
            public string RetainedBy { get; set; }
            public string RetainedAt_Level { get; set; }
            public string Retained_Remarks { get; set; }
            public long Retained_Attachment { get; set; }
            public string Retained_AttachmentPath { get; set; }


        }
        public class Request
        {
            public class List
            {
                public int RowNum { get; set; }
                public long Exit_ID { get; set; }
                public long EMP_ID { get; set; }
                public string Req_No { get; set; }
                public string Req_Date { get; set; }
                public string RelievingDate { get; set; }
                public string Resignation_Reason { get; set; }
                public string Comment { get; set; }
                public string IsServeNotice { get; set; }

                public string ReasonForNotServingNotice { get; set; }
                public string Status { get; set; }

                public string Retained_Remarks { get; set; }
                public string RetainedAt_Level { get; set; }
                public long Retained_Attachment { get; set; }
                public string Retained_AttachmentPath { get; set; }
            }
            public class Add
            {
                public int NoticePeriod { get; set; }
                public int NewNoticePeriod { get; set; }
                public long Exit_ID { get; set; }
                public string Req_No { get; set; }
                public string Req_Date { get; set; }
                public long EMP_ID { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string Resignation_Reason { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string Comment { get; set; }

                public int IsServeNotice { get; set; }
                public string Status { get; set; }
                public int isdeleted { get; set; }
                public int Approved { get; set; }
                public string RelievingDate { get; set; }
                public string Actual_RelievingDate { get; set; }
                public string ReasonForNotServingNotice { get; set; }
                public List<DDValues> ResignationReasonList { get; set; }
                public int HistoryCount { get; set; }

            
            }
        }

        public class Req_Received
        {
            public class List
            {
                public int RowNum { get; set; }
                public long Exit_ID { get; set; }
                public long EMP_ID { get; set; }
                public string emp_code { get; set; }
                public string emp_name { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string Req_No { get; set; }
                public string Req_Date { get; set; }
                public string RelievingDate { get; set; }
                public string Resignation_Reason { get; set; }
                public string Comment { get; set; }
                public string IsServeNotice { get; set; }
                public int DaysLeft { get; set; }
                public string ReasonForNotServingNotice { get; set; }
                public string Status { get; set; }
                public string RetainedByName { get; set; }
                public string RetainedAt_Level { get; set; }
                public string Retained_Remarks { get; set; }
            }

            //public class Proccess
            //{
            //    public long Exit_ID { get; set; }
            //    public long EMP_ID { get; set; }
            //    public string EMP_code { get; set; }
            //    public string EMP_name { get; set; }
            //    public string Department { get; set; }
            //    public string DesignationName { get; set; }
            //    public string Req_No { get; set; }
            //    public string Req_Date { get; set; }
            //    public string RelievingDate { get; set; }
            //    public string Resignation_Reason { get; set; }
            //    public string Comment { get; set; }
            //    public string ReasonForNotServingNotice { get; set; }

            //    public string NoticePeriod { get; set; }
            //    public int IsServeNotice { get; set; }
            //    public string Status { get; set; }
            //}

        }

        public class Req_Process
        {
            public class Resolution
            {
                public HttpPostedFileBase UploadFile { get; set; }

                [Required(ErrorMessage = "Hey! You missed this field")]
                public string Retained_Remarks { get; set; }
                public string RetainedAt_Level { get; set; }
            }
            public class Forward
            {
                public class Level
                {
                    public long? ID { get; set; }
                    public string LevelType { get; set; }
                }
                public List<Level> LevelList { get; set; }
                public List<DDValues> EmpList { get; set; }
            }
            public class Approve
            {
                public string DeactiveSurvey { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string RelievingDate { get; set; }

                [Required(ErrorMessage = "Hey! You missed this field")]
                public string Approved_Remarks { get; set; }
                public List<Department> DepartmentList { get; set; }
                public class Department
                {
                    [Required(ErrorMessage = "Hey! You missed this field")]
                    public string Dept { get; set; }
                    [Required(ErrorMessage = "Hey! You missed this field")]
                    public long LocationID { get; set; }
                    [Required(ErrorMessage = "Hey! You missed this field")]
                    public long HODID { get; set; }
                    [Required(ErrorMessage = "Hey! You missed this field")]
                    public string Remarks { get; set; }
                    public long[] Notify_CC { get; set; }
                    public long Exit_ID { get; set; }
                    public int Priority { get; set; }
                }
                public List<DDValues> LocationList { get; set; }
                public List<DDValues> EmpList { get; set; }

                public List<DDValues> NotificationEMPList { get; set; }
                public List<Exit_Task> Exit_TaskList { get; set; }
                public List<Attachment> AttachmentList { get; set; }
            }


        }


        public class Req_Approved
        {
            public class List
            {
                public int RowNum { get; set; }
                public long Exit_ID { get; set; }
                public long EMP_ID { get; set; }
                public string emp_code { get; set; }
                public string emp_name { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string location_name { get; set; }
                public string Req_No { get; set; }
                public string Req_Date { get; set; }
                public string RelievingDate { get; set; }

            }

        }


        public class LevelApprovals
        {
            public class List
            {
                public long Exit_APP_ID { get; set; }
                public int RowNum { get; set; }
                public long Exit_ID { get; set; }
                public long EMP_ID { get; set; }
                public string emp_code { get; set; }
                public string emp_name { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string location_name { get; set; }
                public string Req_No { get; set; }
                public string Req_Date { get; set; }
                public string RelievingDate { get; set; }
            }

            public class Add
            {
                public long Exit_APP_ID { get; set; }
                public long Exit_ID { get; set; }
                public string Doctype { get; set; }
                public int Approved { get; set; }

                public string NA_Reason { get; set; }

                public string Suggested_RDate { get; set; }

                [Required(ErrorMessage = "Hey! You missed this field")]
                public string Comment { get; set; }
            }
        }


        public class StartExitProcess
        {
            public long Exit_SP_ID { get; set; }
            public long Exit_ID { get; set; }
            public string NotifyIDs { get; set; }

            public long[] Notify_CC { get; set; }

            public Request_View RequestDetails { get; set; }
            public List<LevelApprovers_Details> LevelApprovers_Details { get; set; }
            public List<HandOver_Person> HandOver_Person { get; set; }

            public List<DDValues> EmpList { get; set; }
        }

        public class LevelApprovers_Details
        {
            public long Exit_APP_ID { get; set; }
            public string Doctype { get; set; }
            public long ApproverID { get; set; }
            public string ApproverName { get; set; }
            public string NA_Reason { get; set; }
            public string Suggested_RDate { get; set; }
            public string Comment { get; set; }
            public int Approved { get; set; }
        }
        public class HandOver_Person
        {
            public long Exit_HP_ID { get; set; }
            public long LocationID { get; set; }
            public string location_name { get; set; }

            public string Department { get; set; }
            public string Remarks { get; set; }
            public string Notify_CC { get; set; }
            public long HODID { get; set; }
            public string HODName { get; set; }
        }





        public class NOC_Request
        {
            public class List
            {
                public long Exit_HP_ID { get; set; }
                public string HandOver_Dept { get; set; }
                public int RowNum { get; set; }
                public long Exit_ID { get; set; }
                public long EMP_ID { get; set; }
                public string emp_code { get; set; }
                public string emp_name { get; set; }
                public string EMP_Department { get; set; }
                public string DesignationName { get; set; }
                public string location_name { get; set; }
                public string Req_No { get; set; }
                public string Req_Date { get; set; }
                public string RelievingDate { get; set; }
                public string DaysLeft { get; set; }
            }

        }

        public class NOC_Dashboard
        {
            public class List
            {
                public int RowNum { get; set; }
                public long Exit_ID { get; set; }
                public long EMP_ID { get; set; }
                public string emp_code { get; set; }
                public string emp_name { get; set; }
                public string EMP_Department { get; set; }
                public string DesignationName { get; set; }
                public string location_name { get; set; }
                public string Req_No { get; set; }
                public string Req_Date { get; set; }
                public string RelievingDate { get; set; }
                public string Admin { get; set; }
                public string IT { get; set; }
                public string Finance { get; set; }
                public string HR { get; set; }
                public string Task { get; set; }
            }
        }


        public class Confirmation
        {
            public class List
            {
                public int RowNum { get; set; }
                public long Exit_ID { get; set; }
                public long EMP_ID { get; set; }
                public string emp_code { get; set; }
                public string emp_name { get; set; }
                public string EMP_Department { get; set; }
                public string DesignationName { get; set; }
                public string location_name { get; set; }
                public string Req_No { get; set; }
                public string Req_Date { get; set; }
                public string RelievingDate { get; set; }
                public int DaysLeft { get; set; }
            }
        }

       public List<AssetsDetails> assetsDetails { get; set; }
        public int Id { get; set; }
        public int IsGrid { get; set; }
        public int InputData { get; set; }
        public string UserGrade { get; set; }
    }
    public class AssetsDetails : BaseModel
    {

        public string AssetID { get; set; }
        public string DateOfIssue { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int RowNum { get; set; }
        public string Exit_HP_ID { get; set; }
        public string Exit_ID { get; set; }

    }
}