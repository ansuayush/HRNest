using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Question
    {
        public long QuestionID { set; get; }
        public long NoOfOption { set; get; }
        public string Quest { get; set; }

        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
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

    public class MiscQuestion
    {
        public List<Question> QuestionList { get; set; }
        public long QuestionID { get; set; }
    }
    
}