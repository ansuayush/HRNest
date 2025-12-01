using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mitr.Models
{
    public class PMS
    {
        public long FYID { get; set; }
        public long Empid { get; set; }

        public class DesignationList
        {
            public long ID { get; set; }
            public string Design { get; set; }
        }
        public class TrainingTypeMaster
        {
            public class List
            {
                public int RowNum { get; set; }
                public long Id { get; set; }
                public string TrainingName { get; set; }
                public string TrainingDesc { get; set; }
            }
            public class Add
            {
                public long Id { get; set; }
                [Required(ErrorMessage = "Training Name Required")]
                public string TrainingName { get; set; }
                [Required(ErrorMessage = "Training Desc Required")]
                public string TrainingDesc { get; set; }
            }
        }


        public class CalenderYear
        {
            public class List
            {
                public long ID { get; set; }
                public int RowNum { get; set; }
                public string year { get; set; }
                public string from_date { get; set; }
                public string to_date { get; set; }
                public bool IsActive { set; get; }
                public int Priority { set; get; }
                public int createdby { get; set; }
                public string createdat { get; set; }
                public int modifiedby { get; set; }
                public string modifiedat { get; set; }
                public int deletedby { get; set; }
                public string deletedat { get; set; }
                public int isdeleted { get; set; }
                public string IPAddress { get; set; }
            }
            public class Add
            {
                public long ID { get; set; }
                [Required(ErrorMessage = "You missed Year")]
                public string year { get; set; }
                [Required(ErrorMessage = "You missed From Date")]
                public string from_date { get; set; }
                [Required(ErrorMessage = "You missed To Date")]
                public string to_date { get; set; }
                public string CopyNextYear { get; set; }
            }
        }

        public class FinList
        {
            public long ID { get; set; }
            public string year { get; set; }
        }
        public class UOM
        {
            public class Add
            {
                public long PMSUOMID { get; set; }
                public long FYID { get; set; }
                [Required(ErrorMessage = "You missed Name")]
                public string Name { get; set; }
                [Required(ErrorMessage = "You missed Type")]
                public string Type { get; set; }
                public string AutoRating { get; set; }
                public string ChkAutoRating { get; set; }
                public int Priority { set; get; }
            }
            public class List
            {
                public int RowNum { get; set; }
                public long PMSUOMID { get; set; }
                public long FYID { get; set; }
                public string FYName { get; set; }
                public string Name { get; set; }
                public string Type { get; set; }
                public string AutoRating { get; set; }
                public bool IsActive { set; get; }
                public int Priority { set; get; }
                public int createdby { get; set; }
                public string createdat { get; set; }
                public int modifiedby { get; set; }
                public string modifiedat { get; set; }
                public int deletedby { get; set; }
                public string deletedat { get; set; }
                public int isdeleted { get; set; }
                public string IPAddress { get; set; }

            }

        }

        public class KPA
        {
            public class Add
            {

                public long PMS_KPAID { get; set; }
                [Required(ErrorMessage = "You missed Area")]
                public string Area { get; set; }
                public long FYID { get; set; }
                public string FYName { get; set; }
                [Required(ErrorMessage = "You missed UOM")]
                public long UOMID { get; set; }
                public string UOMName { get; set; }
                [Required(ErrorMessage = "You missed Monitoring")]
                public string IsMonitoring { get; set; }
                [Required(ErrorMessage = "You missed Mandatory")]
                public string IsMandatory { get; set; }
                public string IsProbation { get; set; }

                public string IncType { get; set; }
                public string AutoRating { get; set; }
                public int Approved { get; set; }
                public string Remarks { get; set; }
                public bool IsActive { set; get; }
                public int Priority { set; get; }
                public string ChkAutoRating { get; set; }
                public string ChkProbation { get; set; }
            }
            public class List
            {
                public int RowNum { get; set; }
                public long PMS_KPAID { get; set; }
                public string Area { get; set; }
                public long FYID { get; set; }
                public string FYName { get; set; }
                public long UOMID { get; set; }
                public string UOMName { get; set; }
                public string UOMType { get; set; }
                public string IsMonitoring { get; set; }
                public string IsMandatory { get; set; }
                public string IsProbation { get; set; }
                public string IncType { get; set; }
                public string AutoRating { get; set; }
                public int Approved { get; set; }
                public string Remarks { get; set; }
                public string ApprovedStatus { get; set; }
                public string IsActive { set; get; }
                public int Priority { set; get; }


                public int createdby { get; set; }
                public string createdat { get; set; }
                public int modifiedby { get; set; }
                public string modifiedat { get; set; }
                public int deletedby { get; set; }
                public string deletedat { get; set; }
                public int isdeleted { get; set; }
                public string IPAddress { get; set; }
            }

            public class KPASummary
            {
                public int RowNum { get; set; }

                public long FYID { get; set; }
                public string FYName { get; set; }
                public long TotalKPA { get; set; }
                public long TActiveKPA { get; set; }
                public string Status { get; set; }
            }
        }
        public class Hierarchy
        {
            public int RowNum { get; set; }
            public long PMS_EID { get; set; }
            public long FYID { get; set; }
            public string FYName { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DepartmentsName { get; set; }
            public int Design_ID { get; set; }
            public string Designation { get; set; }
            public string AppraiserName { get; set; }
            public string Commentors { get; set; }
            public string CommentorsName { get; set; }
            public long Confirmer { get; set; }
            public string ConfirmerName { get; set; }
            public int Approved { get; set; }


            public class Update
            {
                public long PMS_EID { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public long AppraiserID { get; set; }
                public string AppraiserName { get; set; }
                public string Commentors { get; set; }

                [Required(ErrorMessage = "Hey! You missed this field")]
                public long Confirmer { get; set; }
                public string[] SelectedCommentors { get; set; }
                public List<DropDownList> EmployeeList { get; set; }


            }
            public class EMPList
            {
                public long EMPID { get; set; }
                public string EMPName { get; set; }
            }
        }

        public class GoalSheetAct
        {
            public int RowNum { get; set; }
            public long PMS_EID { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DepartmentsName { get; set; }
            public string Designation { get; set; }
            public string LocationName { get; set; }
            public string AppraiserName { get; set; }
            public int Approved { get; set; }

            public string GoalSheetStart { get; set; }
            public string GoalSheetEnd { get; set; }
        }

        public class OSQuestion
        {
            public class List
            {
                public int RowNum { get; set; }
                public long PMS_EID { get; set; }
                public long FYID { get; set; }
                public string FYName { get; set; }
                public string Question { get; set; }
                public string QApplyDesignation { get; set; }
                public string QDesignationIDs { get; set; }
                public string DesignationNames { get; set; }
                public string QuestionFor { get; set; }
                public bool IsActive { get; set; }
                public int Priority { set; get; }
            }
            public class Add
            {
                public long PMS_EID { get; set; }
                public long FYID { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string Question { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string QApplyDesignation { get; set; }
                public string QDesignationIDs { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string QuestionFor { get; set; }

                public string[] SelectedDesig { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string[] ChkValues { get; set; }
                public int Priority { set; get; }
            }
        }


        public class AppraisalAct
        {
            public int RowNum { get; set; }
            public long PMS_EID { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DepartmentsName { get; set; }
            public string Designation { get; set; }
            public string LocationName { get; set; }
            public string AppraiserName { get; set; }
            public int Approved { get; set; }

            public string AppraisalEntryStart { get; set; }
            public string AppraisalEntryEnd { get; set; }
            public string AppraisalReviewStart { get; set; }
            public string AppraisalReviewEnd { get; set; }
        }



        public class GoalSheet
        {
            public class MySheet
            {
                public long FYID { get; set; }
                public string EMPName { get; set; }
                public long Confirmer { get; set; }
                public string ConfirmerName { get; set; }
                public long HODID { get; set; }
                public string HODName { get; set; }
                public string DesignationName { get; set; }
                public int Approved { get; set; }
                public int Isdeleted { get; set; }
                public string Status { get; set; }
                public string GoalSheetStart { get; set; }
                public string GoalSheetEnd { get; set; }

                public string Comment { get; set; }
                public List<GoalSheetDet> GoalSheetDet { get; set; }

                public List<CommentList> CommentList { get; set; }
            }
            public class GoalSheetDet
            {
                public long PMS_GSDID { get; set; }
                public long GoalSheetID { get; set; }
                public long KPAID { get; set; }
                public string Area { get; set; }
                public long UOMID { get; set; }
                public string UOMName { get; set; }
                public string UOMType { get; set; }
                public string PIndicator { get; set; }
                public string Target { get; set; }
                public string DetRemarks { get; set; }

            }

            public class Add
            {
                public long PMS_GSDID { get; set; }
                public long FYID { get; set; }
                public string Area { get; set; }
                public long UOMID { get; set; }
                public string UOMName { get; set; }
                public string UOMType { get; set; }
                public long GoalSheetID { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public long KPAID { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string PIndicator { get; set; }
                [Required(ErrorMessage = "Hey! You missed this field")]
                public string Target { get; set; }
                public string DetRemarks { get; set; }
                public int Priority { set; get; }
            }


            public class TeamGoalSheet
            {
                public long PMS_GSID { get; set; }
                public long FYID { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public long Confirmer { get; set; }
                public string ConfirmerName { get; set; }
                public long HODID { get; set; }
                public string HODName { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public int Approved { get; set; }
                public string Status { get; set; }
                public string GoalSheetStart { get; set; }
                public string GoalSheetEnd { get; set; }
                public string Department { get; set; }
            }


            public class EMPGoalSheet
            {
                public long PMS_GSID { get; set; }
                public long FYID { get; set; }
                public string FYName { get; set; }
                public string LocationName { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public long Confirmer { get; set; }
                public string ConfirmerName { get; set; }
                public long HODID { get; set; }
                public string HODName { get; set; }
                public string DesignationName { get; set; }
                public string Department { get; set; }
                public int Approved { get; set; }
                public int Isdeleted { get; set; }
                public string Status { get; set; }
                public string GoalSheetStart { get; set; }
                public string GoalSheetEnd { get; set; }
                public string Comment { get; set; }
                public string Reason { get; set; }
                public List<EMPGoalSheetDet> GoalSheetDet { get; set; }
                public List<CommentList> CommentList { get; set; }
                public int IsEdit { get; set; }
            }
            public class EMPGoalSheetDet
            {
                public long PMS_GSDID { get; set; }
                public long GoalSheetID { get; set; }
                public long KPAID { get; set; }
                public string Area { get; set; }
                public long UOMID { get; set; }
                public string UOMName { get; set; }
                public string UOMType { get; set; }
                public string PIndicator { get; set; }
                public string Target { get; set; }

                public int Weight { get; set; }
                public string DetRemarks { get; set; }
                public string RPIndicator { get; set; }
                public string RTarget { get; set; }

               

            }
            public class CommentList
            {
                public long PMS_CommentID { get; set; }
                public string Comment { get; set; }
                public string Doctype { get; set; }
                public long createdby { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
                public string createdat { get; set; }
                public string IPAddress { get; set; }
            }
        }

        public class GroupGoalSheet
        {
            public class List
            {
                public long PMS_GSID { get; set; }
                public long FYID { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public long Confirmer { get; set; }
                public string ConfirmerName { get; set; }
                public long HODID { get; set; }
                public string HODName { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public int Approved { get; set; }
                public string Status { get; set; }
                public string GoalSheetStart { get; set; }
                public string GoalSheetEnd { get; set; }
                public string Department { get; set; }
            }
        }


        public class FeedbackQuestions
        {
            public long EMPID { get; set; }
            public string Question { get; set; }
            public string Answer { get; set; }
            public string Doctype { get; set; }
        }

        public class Feedback
        {
            public class List
            {
                public long PMS_EID { get; set; }
                public string DocType { get; set; }
                public long FYID { get; set; }
                public string FYName { get; set; }
                public long EMPID { get; set; }
                public string EMPCode { get; set; }
                public string EMPName { get; set; }
                public long HODID { get; set; }
                public string HODName { get; set; }
                public string HODCode { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public int Completed { get; set; }

            }

            public class Add
            {
                public long PMS_EID { get; set; }
                public string DocType { get; set; }
                public long FYID { get; set; }
                public string FYName { get; set; }
                public long EMPID { get; set; }
                public string EMPCode { get; set; }
                public string EMPName { get; set; }
                public long HODID { get; set; }
                public string HODName { get; set; }
                public string HODCode { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public string OverAllComment { get; set; }
                public List<Questions> QuestionsList { get; set; }
                public string FinalComment { get; set; }
                public string OverAllRating { get; set; }
                public bool IsBtnVisible { get; set; }
            }
            public class Questions
            {
                public long PMS_QAID { get; set; }
                public string Q { get; set; }
                public string A { get; set; }
                public string FinalComment { get; set; }
                public string OverAllRating { get; set; }
                public int isdeleted { get; set; }
                public int GivenBy { get; set; }
            }
        }

        public class SelfAppraisal
        {
            public long PMS_AID { get; set; }
            public long FYID { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public int Approved { get; set; }
            public int isdeleted { get; set; }
            public bool Status { get; set; }
            public string StatusMessage { get; set; }
            public string Reason { get; set; }
            public bool IsBtnVisible { get; set; }


            public List<Training> TrainingL { get; set; }
            public List<KPA> KPAL { get; set; }
            public List<QA> QAL { get; set; }
            public List<QA> QALEmp { get; set; }
            public List<Comments> CommentsL { get; set; }
            public List<Attachments> AttachmentsL { get; set; }
            public List<TrainingType> TrainingType { get; set; }


            public class Training
            {
                public long PMS_ADID { get; set; }
                public int? TrainingTypeID { get; set; }
                public string TrainingType { get; set; }
                public string TrainingRemarks { get; set; }
                public int Isdeleted { get; set; }
            }
            public class KPA
            {
                public int RowNum { get; set; }
                public long FYID { get; set; }
                public long PMS_ADID { get; set; }
                public long PMS_AID { get; set; }
                public string Doctype { get; set; }
                public long KPAID { get; set; }
                public long GoalSheetID { get; set; }
                public long GoalSheet_DetID { get; set; }
                public string KPA_Area { get; set; }
                public string KPA_PIndicator { get; set; }
                public string kPA_Target { get; set; }
                public string KPA_Weight { get; set; }

                [Required(ErrorMessage = "Score can't be blank")]
                [RegularExpression(@"^\d+$", ErrorMessage = "Score must be a valid integer")]
                public string KPA_TargetAchieved { get; set; }                
                public string KPA_IncType { get; set; }
                public string KPA_IsMonitoring { get; set; }
                public string KPA_IsMandatory { get; set; }
                public string KPA_AutoRating { get; set; }
                public long UOMID { get; set; }
                public string UOM_Name { get; set; }
                public string UOM_Type { get; set; }
                
                //[Required(ErrorMessage = "Achievement Can't Blank")]
                public string Self_Achievement { get; set; }
                public string Self_Comment { get; set; }


                public string HOD_Comment { get; set; }
                public string HOD_Score { get; set; }


            }
            public class QA
            {
                public long PMS_QAID { get; set; }
                public string Question { get; set; }

                public string Answer { get; set; }
                public string QuestionFor { get; set; }
                public string FinalComment { get; set; }
            }
            public class Comments
            {
                public long PMS_CommentID { get; set; }
                public string Doctype { get; set; }
                public string TableName { get; set; }
                //[Required(ErrorMessage = "Comment Can't Blank")]
                public string Comment { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
                public string createdat { get; set; }
                public string IPAddress { get; set; }
            }
            public class Attachments
            {
                public long AttachmentID { get; set; }
                public string FileName { get; set; }
                public string Descrip { get; set; }
                public string URL { get; set; }
                public HttpPostedFileBase UploadFile { get; set; }
            }


        }

        public class TrainingType
        {
            public long ID { get; set; }
            public string Type { get; set; }
        }


        public class TeamAppraisal
        {
            public class List
            {
                public long PMS_AID { get; set; }
                public long FYID { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public int Approved { get; set; }
                public string Reason { get; set; }
                public string Status { get; set; }

                public string SubmitedDate { get; set; }

            }
            public class Add
            {
                public long PMS_AID { get; set; }
                public long FYID { get; set; }
                public string FYName { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public string Department { get; set; }

               
                //[Required(ErrorMessage = "Hey, You missed this field!!")]
                public string Reason { get; set; }

                public int Approved { get; set; }
                public string ApproveStatus { get; set; }
                public string TeamRecommendation { get; set; }
                public int Team_Score { get; set; }
                public double SystemScore { get; set; }
                public bool Status { get; set; }
                public string StatusMessage { get; set; }
                public bool IsBtnVisible { get; set; }

                public List<Training> TrainingL { get; set; }
                public List<KPA> KPAL { get; set; }
                public List<QA> QAL { get; set; }
                public List<QA> QALEmp { get; set; }
                public List<Comments> CommentsL { get; set; }
                public List<Attachments> AttachmentsL { get; set; }
                public List<TrainingType> TrainingType { get; set; }
                public List<TeamComment> TeamCommentL { get; set; }


            }

            public class Training
            {
                public long PMS_ADID { get; set; }
                public int? TrainingTypeID { get; set; }
                public string TrainingType { get; set; }
                public string TrainingRemarks { get; set; }
                public string RejectReason { get; set; }
                public int isdeleted { get; set; }
                public string Doctype { get; set; }
            }
            public class KPA
            {
                public int RowNum { get; set; }
                public long FYID { get; set; }
                public long EMPID { get; set; }
                public long PMS_ADID { get; set; }
                public long PMS_AID { get; set; }
                public string Doctype { get; set; }
                public long KPAID { get; set; }
                public long GoalSheetID { get; set; }
                public long GoalSheet_DetID { get; set; }
                public string KPA_Area { get; set; }
                public string KPA_PIndicator { get; set; }
                public string kPA_Target { get; set; }
                public string KPA_TargetAchieved { get; set; }
                public string KPA_Weight { get; set; }
                public string KPA_IncType { get; set; }
                public string KPA_IsMonitoring { get; set; }
                public string KPA_IsMandatory { get; set; }
                public string KPA_AutoRating { get; set; }
                public long UOMID { get; set; }
                public string UOM_Name { get; set; }

                public string Self_Achievement { get; set; }
                public string Self_Comment { get; set; }

                public string HOD_Comment { get; set; }
               
                [Required(ErrorMessage = "Score can't be blank")]
                [RegularExpression(@"^\d+$", ErrorMessage = "Score must be a valid integer")]
                public string HOD_Score { get; set; }


            }
            public class QA
            {
                public long PMS_QAID { get; set; }
                public string Question { get; set; }
                public string Answer { get; set; }
                public string FinalComment { get; set; }
                public string Doctype { get; set; }
                public string QuestionFor { get; set; }
            }
            public class Comments
            {
                public long PMS_CommentID { get; set; }
                public string Comment { get; set; }
                public string TableName { get; set; }
                public string Name { get; set; }
                public string Doctype { get; set; }
                public string Email { get; set; }
                public string createdat { get; set; }
                public string IPAddress { get; set; }
            }
            public class TeamComment
            {
                public long PMS_CommentID { get; set; }
                public string Doctype { get; set; }

                public string Comment { get; set; }
            }
            public class Attachments
            {
                public long AttachmentID { get; set; }
                public string FileName { get; set; }
                public string Descrip { get; set; }
                public string URL { get; set; }
            }
        }

        public class GroupAppraisal
        {
            public class List
            {
                public long PMS_AID { get; set; }
                public long FYID { get; set; }
                public long EMPID { get; set; }
                public int FeedbackReceived { get; set; }
                public int TotalFeedback { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string HODCode { get; set; }
                public string HODName { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public int Approved { get; set; }
                public string Status { get; set; }
                public string SubmitedDate { get; set; }

            }

            public class Add
            {
                public long PMS_AID { get; set; }
                public long FYID { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string HODName { get; set; }
                public string HODCode { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public string Department { get; set; }
                public string ConfirmerName { get; set; }
                public int Approved { get; set; }
                public string ApproveStatus { get; set; }
                public bool Status { get; set; }
                public string StatusMessage { get; set; }
                public string FYName { get; set; }
                public bool IsBtnVisible { get; set; }
                public string Group_Comment { get; set; }
                public int? Group_Score { get; set; }
                public string TeamRecommendation { get; set; }
                public string AppraiserName { get; set; }
                public string CommentorsName { get; set; }
                public int Team_Score { get; set; }
                public double SystemScore { get; set; }

                [Required(ErrorMessage = "Are you agreeing with the appraisal?")]
                [Display(Name = "Are you agreeing with the appraisal")]
                public string chkAgreeWithAppraisal { get; set; }


                [Display(Name = "I have considered the Feedback")]
                [Range(typeof(bool), "true", "true", ErrorMessage = "Have considered the Feedback ?")]
                public bool chkConsiderFeedback { get; set; }

                public List<Training> TrainingL { get; set; }
                public List<KPA> KPAL { get; set; }
                public List<QA> QAL { get; set; }
                public List<QA> QALEmp { get; set; }
                public List<QA> QALTeam { get; set; }
                public List<Comments> CommentsL { get; set; }
                public List<Attachments> AttachmentsL { get; set; }
                public List<TrainingType> TrainingType { get; set; }

                public int No_ApprovedConformers { get; set; }
                public int No_Conformers { get; set; }
            }

            public class Training
            {
                public long PMS_ADID { get; set; }
                public int TrainingTypeID { get; set; }
                public string TrainingType { get; set; }
                public string TrainingRemarks { get; set; }
                public int isdeleted { get; set; }
            }
            public class KPA
            {
                public int RowNum { get; set; }
                public long FYID { get; set; }
                public long PMS_ADID { get; set; }
                public long PMS_AID { get; set; }
                public string Doctype { get; set; }
                public long KPAID { get; set; }
                public long GoalSheetID { get; set; }
                public long GoalSheet_DetID { get; set; }
                public string KPA_Area { get; set; }
                public string KPA_PIndicator { get; set; }
                public string kPA_Target { get; set; }
                public string KPA_Weight { get; set; }
                public string KPA_IncType { get; set; }
                public string KPA_IsMonitoring { get; set; }
                public string KPA_IsMandatory { get; set; }
                public string KPA_AutoRating { get; set; }
                public long UOMID { get; set; }
                public string UOM_Name { get; set; }
                public string Self_Achievement { get; set; }
                public string Self_Comment { get; set; }
                public string HOD_Comment { get; set; }
                public string HOD_Score { get; set; }
                public string KPA_TargetAchieved { get; set; }

            }
            public class QA
            {
                public long PMS_QAID { get; set; }
                public long GivenBy { get; set; }
                public string Question { get; set; }
                public string Answer { get; set; }
                public string FinalComment { get; set; }
                public string Doctype { get; set; }
                public string EMPdetails { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public string FinalRating { get; set; }
                public string QuestionFor { get; set; }
            }
            public class Comments
            {
                public long PMS_CommentID { get; set; }
                public string Doctype { get; set; }
                public string TableName { get; set; }
                public string Comment { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
                public string createdat { get; set; }
                public string IPAddress { get; set; }
            }
            public class Attachments
            {
                public long AttachmentID { get; set; }
                public string FileName { get; set; }
                public string Descrip { get; set; }
                public string URL { get; set; }

            }


        }

        public class CMCAppraisal
        {
            public class List
            {
                public long PMS_AID { get; set; }
                public long FYID { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string HODCode { get; set; }
                public string HODName { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public string ConfirmerName { get; set; }
                public int Approved { get; set; }
                public string Status { get; set; }
                public string SubmitedDate { get; set; }

            }

            public class Add
            {
                public long PMS_AID { get; set; }
                public long FYID { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string HODName { get; set; }
                public string HODCode { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public string Department { get; set; }
                public string ConfirmerName { get; set; }
                public int Approved { get; set; }
                public string ApproveStatus { get; set; }
                public bool Status { get; set; }
                public string StatusMessage { get; set; }
                public string FYName { get; set; }
                public string AppraiserName { get; set; }
                public string CommentorsName { get; set; }
                public bool IsBtnVisible { get; set; }
                public string TeamRecommendation { get; set; }
                public int Team_Score { get; set; }
                public string Group_Comment { get; set; }
                public int Group_Score { get; set; }
                public string CMC_Comment { get; set; }
                public double SystemScore { get; set; }

                [Range(1, 100, ErrorMessage = "Value for Score must be between {1} and {2}.")]
                [Required(ErrorMessage = "Score can't be blank")]
                public int CMC_Score { get; set; }

                [Range(1, 100, ErrorMessage = "Value for Increment  must be between {1} and {2}.")]
                [Required(ErrorMessage = "Increment can't be blank")]
                public decimal CMC_Increment { get; set; }


                [Required(ErrorMessage = "Are you agreeing with the appraisal?")]
                [Display(Name = "Are you agreeing with the appraisal")]
                public string chkAgreeWithAppraisal { get; set; }


                [Display(Name = "I have considered the Feedback")]
                [Range(typeof(bool), "true", "true", ErrorMessage = "Have considered the Feedback ?")]
                public bool chkConsiderFeedback { get; set; }

                public List<Training> TrainingL { get; set; }
                public List<KPA> KPAL { get; set; }
                public List<QA> QAL { get; set; }
                public List<QA> QALEmp { get; set; }
                public List<QA> QALTeam { get; set; }
                public List<Comments> CommentsL { get; set; }
                public List<Attachments> AttachmentsL { get; set; }
                public List<TrainingType> TrainingType { get; set; }


            }

            public class Training
            {
                public long PMS_ADID { get; set; }
                public int TrainingTypeID { get; set; }
                public string TrainingType { get; set; }
                public string TrainingRemarks { get; set; }
                public int isdeleted { get; set; }
            }
            public class KPA
            {
                public int RowNum { get; set; }
                public long FYID { get; set; }
                public long PMS_ADID { get; set; }
                public long PMS_AID { get; set; }
                public string Doctype { get; set; }
                public long KPAID { get; set; }
                public long GoalSheetID { get; set; }
                public long GoalSheet_DetID { get; set; }
                public string KPA_Area { get; set; }
                public string KPA_PIndicator { get; set; }
                public string kPA_Target { get; set; }
                public string KPA_Weight { get; set; }
                public string KPA_IncType { get; set; }
                public string KPA_IsMonitoring { get; set; }
                public string KPA_IsMandatory { get; set; }
                public string KPA_AutoRating { get; set; }
                public long UOMID { get; set; }
                public string UOM_Name { get; set; }
                public string Self_Achievement { get; set; }
                public string Self_Comment { get; set; }
                public string HOD_Comment { get; set; }
                public string HOD_Score { get; set; }
                public string KPA_TargetAchieved { get; set; }


            }
            public class QA
            {
                public long PMS_QAID { get; set; }
                public string Question { get; set; }
                public string Answer { get; set; }
                public string FinalComment { get; set; }
                public long GivenBy { get; set; }
                public string Doctype { get; set; }
                public string EMPdetails { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public string QuestionFor { get; set; }
                public string FinalRating { get; set; }
            }
            public class Comments
            {
                public long PMS_CommentID { get; set; }
                public string Doctype { get; set; }
                public string TableName { get; set; }
                public string Comment { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
                public string createdat { get; set; }
                public string IPAddress { get; set; }
            }
            public class Attachments
            {
                public long AttachmentID { get; set; }
                public string FileName { get; set; }
                public string Descrip { get; set; }
                public string URL { get; set; }

            }
        }


        public class Appraisal
        {
            public class Add
            {
                public long PMS_AID { get; set; }
                public long FYID { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string HODName { get; set; }
                public string HODCode { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
                public string Department { get; set; }
                public string ConfirmerName { get; set; }
                public int Approved { get; set; }
                public string ApproveStatus { get; set; }
                public bool Status { get; set; }
                public string StatusMessage { get; set; }
                public string FYName { get; set; }
                public bool IsBtnVisible { get; set; }
                public string TeamRecommendation { get; set; }
                public int Team_Score { get; set; }
                public string Group_Comment { get; set; }
                public int Group_Score { get; set; }
                public string CMC_Comment { get; set; }
                public int CMC_Score { get; set; }
                public decimal CMC_Increment { get; set; }
                public string chkAgreeWithAppraisal { get; set; }
                public bool chkConsiderFeedback { get; set; }
                public List<Training> TrainingL { get; set; }
                public List<KPA> KPAL { get; set; }
                public List<QA> QAL { get; set; }
                public List<Comments> CommentsL { get; set; }
                public List<Attachments> AttachmentsL { get; set; }
                public List<TrainingType> TrainingType { get; set; }


            }
            public class Training
            {
                public long PMS_ADID { get; set; }
                public int TrainingTypeID { get; set; }
                public string TrainingType { get; set; }
                public string TrainingRemarks { get; set; }
                public int Approved { get; set; }
            }
            public class KPA
            {
                public int RowNum { get; set; }
                public long FYID { get; set; }
                public long PMS_ADID { get; set; }
                public long PMS_AID { get; set; }
                public string Doctype { get; set; }
                public long KPAID { get; set; }
                public long GoalSheetID { get; set; }
                public long GoalSheet_DetID { get; set; }
                public string KPA_Area { get; set; }
                public string KPA_PIndicator { get; set; }
                public string kPA_Target { get; set; }
                public string KPA_Weight { get; set; }
                public string KPA_IncType { get; set; }
                public string KPA_IsMonitoring { get; set; }
                public string KPA_IsMandatory { get; set; }
                public string KPA_AutoRating { get; set; }
                public long UOMID { get; set; }
                public string UOM_Name { get; set; }
                public string Self_Achievement { get; set; }
                public string Self_Comment { get; set; }
                public string HOD_Comment { get; set; }
                public string HOD_Score { get; set; }

            }
            public class QA
            {
                public long PMS_QAID { get; set; }
                public string Question { get; set; }
                public string Answer { get; set; }
                public string FinalComment { get; set; }
                public long GivenBy { get; set; }
                public string Doctype { get; set; }
                public string EMPdetails { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string Department { get; set; }
                public string DesignationName { get; set; }
                public string LocationName { get; set; }
            }
            public class Comments
            {
                public long PMS_CommentID { get; set; }
                public string Doctype { get; set; }
                public string TableName { get; set; }
                public string Comment { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
                public string createdat { get; set; }
                public string IPAddress { get; set; }
            }
            public class Attachments
            {
                public long AttachmentID { get; set; }
                public string FileName { get; set; }
                public string Descrip { get; set; }
                public string URL { get; set; }
            }
            public class TrainingSkills
            {
                public long FYId { get; set; }
                public long EMPId { get; set; }
                public string EmpCode { get; set; }
                public string EmpName { get; set; }
                public string TrainingType { get; set; }
                public string TrainingRemarks { get; set; }
                public List<TrainingSkills> trainingSkills { get; set; }

            }
        }
        public class PMSStatus
        {
            public string EmployeeCode { get; set; }
            public string EmployeeName { get; set; }
            public string WorkLocation { get; set; }
            public string Designation { get; set; }
            public string Department { get; set; }
            public string Supervisor { get; set; }
            public string AppraiserName { get; set; }
            public string ConfirmerName { get; set; }
            public string CommentorsName { get; set; }
            public string Status { get; set; }
            public long FYID { get; set; }
            public long Empid { get; set; }
            public List<DropDownList> EmployeeList { get; set; }
            public List<PMS.FinList> FinYearList { get; set; }
            public List<PMS.PMSStatus> PMSStatusList { get; set; }
           


        }
        public class TrainingSkills
        {
            public long FYId { get; set; }
            public long EMPId { get; set; }
            public string EmpCode { get; set; }
            public string EmpName { get; set; }
            public string TrainingType { get; set; }
            public string TrainingRemarks { get; set; }
            public List<TrainingSkills> trainingSkills { get; set; }

        }

    }
}