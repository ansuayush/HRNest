using Mitr.CommonClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Job
    {
        public long JobID { get; set; }
        [Required(ErrorMessage = "Job Code Can't Blank")]
        public string JobCode { set; get; }
        public string Experience { set; get; }

        [Required(ErrorMessage = "Title Can't Blank")]
        public string Title { set; get; }

        [Required(ErrorMessage = "Hey! You missed this field.")]
        public long[] SkillsIds { set; get; }

        public string Skills { set; get; }

        public string QualificationDet { set; get; }
        public string Description { set; get; }
        public int NoticePeriod { set; get; }
        public int ProbationPeriod { set; get; }
        public bool IsActive { set; get; }
        public int Priority { set; get; }
        public int createdby { get; set; }
        public string createdat { get; set; }
        public int modifiedby { get; set; }
        public string modifiedat { get; set; }
        public string IPAddress { get; set; }
    }


    public class ViewJobDetails
    {
       public Job Job { get; set; }
        public List<JobRound> JobRoundList { get; set; }
        
    }
    public class    JobRound
    {

        public long JobDetailsID { get; set; }
        [Required(ErrorMessage = "Round Name Can't Blank")]
        public string RoundName { get; set; }

       
        public string RoundDesc { get; set; }
        public int Priority { set; get; }
        public int srno { set; get; }
        public List<JobMember> JobMemberList { get; set; }
        public bool IsNegotiationRound { get; set; }
        
    }
    public class JobMember
    {
        public long? JobDetailsID { get; set; }
        [Required(ErrorMessage = "Type Can't Blank")]
        public string RoundMemberType { get; set; }
        public long EMPID { get; set; }
        [Required(ErrorMessage = "Name Can't Blank")]
        public string Name { get; set; }
       
        [Required(ErrorMessage = "Email Can't Blank")]
        [EmailAddress]
        public string Email { get; set; }
        public int srno { set; get; }
        public int Priority { set; get; }
    }


   
    
}