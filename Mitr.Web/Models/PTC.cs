using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class PTC
    {
        public long Approved { get; set; }
        public string Type { get; set; }
        public long NEMPID { get; set; }
        public long EMPID { get; set; }
        public long TypeId { get; set; }
        public class Hierarchy
        {
            public int RowNum { get; set; }
            public long Id { get; set; }
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
            public long Approved { get; set; }
            public string Status { get; set; }
            public string LocationName { get; set; }
            public string doj { get; set; }
            public string Probation_EndDate { get; set; }

            public class Update
            {
                public long Id { get; set; }
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
        public class CreationofObjective
        {
            public long Id { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DepartmentsName { get; set; }
            public string Designation { get; set; }
            public long Approved { get; set; }
            public string Status { get; set; }
        }

        public class ProbationObjectives
        {
            public long Id { get; set; }
            public long Approved { get; set; }
            public long KPId { get; set; }
            public long UMId { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string DepartmentsName { get; set; }
            public string Designation { get; set; }
            public string ProbationDate { get; set; }
            public string Commentors { get; set; }
            public string AppraiserName { get; set; }
            public string ConfirmerName { get; set; }
            public string UserComment { get; set; }
            public string ResubmitComment { get; set; }
            public string Status { get; set; }
            public List<Objectivesperiod> objectivesperiods { get; set; }
            public List<KPAList> kPALists { get; set; }
            public List<UMList> uMLists { get; set; }
            public List<ResubmitList> resubmitLists { get; set; }
        }
        public class ResubmitList
        {
            public long ID { get; set; }
            public string createdat { get; set; }
            public string CommentBy { get; set; }
            public string Comment { get; set; }
        }

        public class Objectivesperiod
        {
            public long Id { get; set; }
           
            public long KPAID { get; set; }
            public long PBID { get; set; }
            public string KPA { get; set; }
          
            public string ProbationObjective { get; set; }
         
            public long UOMId { get; set; }
            public string UOM { get; set; }
            public string UOMType { get; set; }
            public string Remarks { get; set; }
          
            public long Weights { get; set; }
            public long Target { get; set; }
            public long EMPID { get; set; }
            public List<KPAList> kPALists { get; set; }
            public List<UMList> uMLists { get; set; }
        }
        public class ObjectivesperiodNew
        {
            public long Id { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public long KPAID { get; set; }
            public long PBID { get; set; }
            public string KPA { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public string ProbationObjective { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public long UOMId { get; set; }
            public string UOM { get; set; }
            public string UOMType { get; set; }
            public string Remarks { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public long Weights { get; set; }
            public long Target { get; set; }
            public long EMPID { get; set; }
            public List<KPAList> kPALists { get; set; }
            public List<UMList> uMLists { get; set; }
        }

        public class KPAList
        {
            public long ID { get; set; }
            public string KPAName { get; set; }
        }
        public class UMList
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }

        public class Appraisal
        {
            public class SelfAppraisal
            {
                public long ID { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public long Confirmer { get; set; }
                public string ConfirmerName { get; set; }
                public string AppraiserName { get; set; }
                public long HODID { get; set; }
                public string HODName { get; set; }
                public string Designation { get; set; }
                public string DepartmentsName { get; set; }
                public int Approved { get; set; }
                public string Status { get; set; }
                public string ProbationDate { get; set; }
                public string UserComment { get; set; }
                public string HodComment { get; set; }
                public string LocationName { get; set; }
                public string ResubmitComment { get; set; }
                public string UserFinalComment { get; set; }
                public string HodFinalComment { get; set; }
         
                public List<FeedbackQuestions> feedbackQuestions { get; set; }
                public List<Attachments> AttachmentsL { get; set; }
                public List<Training> TrainingL { get; set; }
                public List<KPA> KPAL { get; set; }
                public List<TrainingType> TrainingType { get; set; }
                public List<Type> typeslist { get; set; }
                public List<Type> Ctypeslist { get; set; }
                public AppraisalReview appraisalReview { get; set; }
                public AppraisalReview ConfirmerReview { get; set; }
                public long SystemScore { get; set; }
                public string Comment { get; set; }
                public string IsAgree { get; set; }
                public long IsShow { get; set; }

                public string startDate { get; set; }

            }

            public class AppraisalReview
            {
                public long AppraisalID { get; set; }
                public string AppraisalType { get; set; }
                public long ModificationTypeId { get; set; }
                public string ModificationType { get; set; }
                public long? TypeId { get; set; }
                public string TypeName { get; set; }
                public string Reason { get; set; }
                public string ProbationEndDate { get; set; }
                public long FinalScore { get; set; }
                public long SystemScore { get; set; }
              

            }

            public class Type
            {
                public long ID { get; set; }
                public string Name { get; set; }
            }
            public class TrainingType
            {
                public long ID { get; set; }
                public string Type { get; set; }
            }
      
            public class Training
            {
                public long ID { get; set; }
                public long APID { get; set; }
                public int? TrainingTypeID { get; set; }
                public string TrainingType { get; set; }
                public string TrainingRemarks { get; set; }
                public string ReasonforDeclining { get; set; }
                public int Isdeleted { get; set; }
            }
            public class Attachments
            {
                public long AttachmentID { get; set; }
                public long APID { get; set; }
                public string FileName { get; set; }
                public string Descrip { get; set; }
                public string URL { get; set; }
                public HttpPostedFileBase UploadFile { get; set; }
            }
            public class FeedbackQuestions
            {
                public long ID { get; set; }
                public long QID { get; set; }
                public long APID { get; set; }
                public long EMPID { get; set; }
                public string Question { get; set; }
                public string UserAnswer { get; set; }
                public string SupervisiorAnswer { get; set; }
            }

            public class KPA
            {
           
                public long ID { get; set; }
                public long APID { get; set; }
                public long KPAID { get; set; }
                public string KPAName { get; set; }
                public string ProbationObjective { get; set; }
                public long UOMId { get; set; }
                public string UOM { get; set; }
                public string UOMType { get; set; }
                public string Remarks { get; set; }
                public string Weights { get; set; }
                public long Target { get; set; }
                public long TargetAchievement { get; set; }
                public string Self_Achievement { get; set; }
                public string Self_Comment { get; set; }
                public string HOD_Comment { get; set; }
                public string HOD_Score { get; set; }
                public string KPA_AutoRating { get; set; }
                public string KPA_IncType { get; set; }




            }
            public class TeamList
            {
                public long Id { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public string EMPCode { get; set; }
                public string DepartmentsName { get; set; }
                public string Designation { get; set; }
                public long Approved { get; set; }
                public string Status { get; set; }
                public string LocationName { get; set; }
                
            }
            public class CMCAppraisal
            {
                public long ID { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public long Confirmer { get; set; }
                public string ConfirmerName { get; set; }
                public string AppraiserName { get; set; }
                public long HODID { get; set; }
                public string HODName { get; set; }
                public string Designation { get; set; }
                public string DepartmentsName { get; set; }
                public int Approved { get; set; }
                public string Status { get; set; }
                public string ProbationDate { get; set; }
                public string UserComment { get; set; }
                public string HodComment { get; set; }
                public string LocationName { get; set; }
                public string ResubmitComment { get; set; }
                public string UserFinalComment { get; set; }
                public string HodFinalComment { get; set; }
                public List<FeedbackQuestions> feedbackQuestions { get; set; }
                public List<KPA> KPAL { get; set; }
        
                public List<Type> typeslist { get; set; }
                public List<Type> Ctypeslist { get; set; }
                public List<Type> CMCtypeslist { get; set; }
                public AppraisalReview appraisalReview { get; set; }
                public AppraisalReview ConfirmerReview { get; set; }
                public AppraisalReview CMCReview { get; set; }
                public string Comment { get; set; }
                public string IsAgree { get; set; }
                public string CMCComment { get; set; }
                public string CMCIsAgree { get; set; }

            }
            public class HRAppraisal
            {
                public long ID { get; set; }
                public long EMPID { get; set; }
                public string EMPName { get; set; }
                public long Confirmer { get; set; }
                public string ConfirmerName { get; set; }
                public string AppraiserName { get; set; }
                public long HODID { get; set; }
                public string HODName { get; set; }
                public string Designation { get; set; }
                public string DepartmentsName { get; set; }
                public int Approved { get; set; }
                public string Status { get; set; }
                public string ProbationDate { get; set; }
                public string UserComment { get; set; }
                public string HodComment { get; set; }
                public string LocationName { get; set; }
                public string ResubmitComment { get; set; }
                public string UserFinalComment { get; set; }
                public string HodFinalComment { get; set; }
                public List<FeedbackQuestions> feedbackQuestions { get; set; }
                public List<KPA> KPAL { get; set; }

                public List<Type> typeslist { get; set; }
                public List<Type> Ctypeslist { get; set; }
                public List<Type> CMCtypeslist { get; set; }
                public AppraisalReview appraisalReview { get; set; }
                public AppraisalReview ConfirmerReview { get; set; }
                public AppraisalReview CMCReview { get; set; }
                public string Comment { get; set; }
                public string IsAgree { get; set; }
                public string CMCComment { get; set; }
                public string CMCIsAgree { get; set; }
                public string HRComment { get; set; }

            }

        }


    }
}