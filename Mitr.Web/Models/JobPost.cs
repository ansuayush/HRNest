using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class JobPost
    {
        public long JobPostID { get; set; }
        public long RecruitmentRequestID { get; set; }
        
        public long Location { get; set; }
        public string RecruitmentType { get; set; }
        public string ProjectName { get; set; }
        public long ProjectManagerID { get; set; }
        public string ManagerName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string JobTitle { get; set; }
        public string DueDate { get; set; }
        public string JobDescription { get; set; }
        public string Qualification { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
        public string StaffCategory { get; set; }
        public long AttachmentID { get; set; }
        public string AttachmentName { get; set; }
        public string ContentType { get; set; }
        public string Announcement { get; set; }
        public string AnnouncementType { get; set; }
        public string AnnouncementStartDate { get; set; }
        public string AnnouncementEndDate { get; set; }
        public string Link { get; set; }
        public int Approved { get; set; }
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