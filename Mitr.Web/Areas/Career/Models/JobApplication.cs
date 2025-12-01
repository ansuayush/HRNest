using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Areas.Career.Models
{
    public class JobApplication
    {
        public class Apply
        {
            public string Code { get; set; }
            public bool Status { get; set; }
            public string StatusMessage { get; set; }
            public long REC_AppID { get; set; }
            public long REC_ReqID { get; set; }
            public long REC_EVacancyID { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string DOB { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Gender { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Nationality { get; set; }
            [MaxLength(10)]
            [StringLength(10, ErrorMessage = "Name cannot be longer than 10 characters.")]
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Mobile { get; set; }
            [EmailAddress]
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string EmailID { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Address { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string TotalExperience { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public long[] ThematicAreaID { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Resposibilities { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string ChangeReason { get; set; }
            //[Required(ErrorMessage = "Hey! You Missed This Field")]
            public string BreakReason { get; set; }
            //[Required(ErrorMessage = "Skill Can't Blank")]
            public long[] SkillsID { get; set; }

             [Required(ErrorMessage = "Hey! You Missed This Field")]
            public decimal? CurrentSalary { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public decimal? ExpectedSalary { get; set; }


            public long CVAttachID { get; set; }
            [Required(ErrorMessage = "please Attach your CV")]
            public HttpPostedFileBase Upload { get; set; }
            public List<Skill> SkillList { get; set; }
            public List<Thematic> ThematicList { get; set; }

            public string Staff_Cat { get; set; }
            public string Job_SubTitle { get; set; }
            public string JobTitle { get; set; }
            public string JobCode { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Location { get; set; }
            public string ProjectName { get; set; }
            public string ProjectNo { get; set; }
            public List<ProfessionalQual> ProQualList { get; set; }
            public List<References> ReferencesList { get; set; }
            public List<Questions> QuestionsList { get; set; }
            public List<Education> EducationList { get; set; }
            [Display(Name = "I Agree")]
            [Range(typeof(bool), "true", "true", ErrorMessage = "Check on I Agree")]
            public bool chkAgree { get; set; }

            public string CurrentStaffMemberName { get; set; }
            public string CurrentStaffMemberRelation { get; set; }
            public string NoticePeriodRequired { get; set; }
            public string AppliedNameofthePosition { get; set; }
            public string AppliedWhenApplied { get; set; }
            public string AppliedOutcome { get; set; }
            public string AssociatedPosition { get; set; }
            public string AssociatedPeriod { get; set; }
            public string AssociatedLocation { get; set; }
            public string AssociatedDateOfLeaving { get; set; }
            public string AssociatedReasonforLeaving { get; set; }
            public string OffenceSpecify { get; set; }
            public string WillingtoRelocate { get; set; }
            public string SpecialSkills { get; set; }
        }

        public class Thematic
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }
        public class Skill
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }
        public class ProfessionalQual
        {
            public long? REC_AppDetID { get; set; }

            public string Employer { get; set; }

            public string Post { get; set; }

            public int? CTC { get; set; }

            public string Location { get; set; }

            public string Period { get; set; }
        }
        public class References
        {
            public long? REC_AppDetID { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Name { get; set; }
            [EmailAddress]
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Relationship { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Phone { get; set; }

        }
        public class Questions
        {
            public long? REC_AppDetID { get; set; }
            public string Question { get; set; }
            public string Relationship { get; set; }
            public string Name { get; set; }
            public string NoticePeriod { get; set; }
            public string PositionC3Past { get; set; }
            public string Position { get; set; }
            public string AppliedDate { get; set; }
            public string AppliedDateC3Past { get; set; }
            public string outcome { get; set; }            
            public string Period { get; set; }
            public string Location { get; set; }
            public string LocationC3Past { get; set; }
            public string DOL { get; set; }
            public string ReasonL { get; set; }
            public string Specify { get; set; }
            public string QuestionOption { get; set; }
        }

        public class Education
        {
            public long? REC_AppDetID { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Course { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string University { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string year { get; set; }
            [Required(ErrorMessage = "Hey! You Missed This Field")]
            public string Location { get; set; }

        }
    }

}