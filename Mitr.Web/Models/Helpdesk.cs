using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Helpdesk
    {
        public class TicketConfigration
        {
            public long TicketCongID { get; set; }
            [Required(ErrorMessage = "Start Time Can't Blank")]
            public string StartTime { get; set; }
            [Required(ErrorMessage = "End Time Can't Blank")]
            public string EndTime { get; set; }
         
            public string[] ApplicableDays { get; set; }
            [Required(ErrorMessage = "Holiday Calendar Applicable Can't Blank")]
            public int IsHolidayCalApplicable { get; set; }  
        }
        public class LocationGroup
        {
            public long LocationGroupID { get; set; }
            [Required(ErrorMessage = "Group Name Can't Blank")]
            public string GroupName { set; get; }
            public string Description { set; get; }
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
            [Required(ErrorMessage = "Location Can't Blank")]
            public int[] LocationIDs { get; set; }
        }

        public class SubCategory
        {
            public long SubCategoryID { get; set; }
            [Required(ErrorMessage = "Category Can't Blank")]
            public long CategoryID { get; set; }
           
            public string CategoryName { get; set; }
            [Required(ErrorMessage = "Sub Category Can't Blank")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Related Can't Blank")]
            public string RelatedTo { get; set; }
            public string Description { get; set; }
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

        public class TicketSetting_Assignment
        {
            public long SubSettingID { get; set; }
            public long SubCategoryID { get; set; }
            [Required(ErrorMessage = "Location Can't Blank")]
            public long LocationGroupID { get; set; }
            public string LocationGroupName { set; get; }
            [Required(ErrorMessage = "Primary Assignee Can't Blank")]
            public int PrimaryAssignee { set; get; }
            [Required(ErrorMessage = "Supervisor Can't Blank")]
            public int Supervisor { set; get; }
            [Required(ErrorMessage = "Escalation Can't Blank")]
            public int Escalation { set; get; }
            public string PrimaryAssigneeName { set; get; }
            public string SupervisorName { set; get; }
            public string EscalationName { set; get; }
            public string Doctype { set; get; }
            public bool IsActive { get; set; }
        }
        public class TicketSetting_SMA
        {
            public long SubSettingID { get; set; }
            public long SubCategoryID { get; set; }
            public string PolicyPriority { set; get; }
            [Required(ErrorMessage = "First Response Can't Blank")]
            public int ResponseTime { set; get; }
            [Required(ErrorMessage = "FollowUp Can't Blank")]
            public int FollowUpTime { set; get; }
            [Required(ErrorMessage = "Escalation Can't Blank")]
            public int EscalationTime { set; get; }
            public int Reopen { set; get; }
            public string Doctype { set; get; }
            public bool IsActive { get; set; }
        }
        

        public class Ticket
        {
            public long TicketID { get; set; }
            public string TicketNo { get; set; }
            public long SubCategoryID { get; set; }
           
            public long CategoryID { get; set; }

            public string CategoryName { get; set; }
           
            public string SubCategoryName { get; set; }
           
            public string RelatedTo { get; set; }
            public long LocationGroupID { get; set; }
            
            public string LocationGroupName { set; get; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string ApplicableDays { get; set; }
            public int IsHolidayCalApplicable { get; set; }
            public int PrimaryAssignee { set; get; }
            public int Supervisor { set; get; }
            public int Escalation { set; get; }
            public string PrimaryAssigneeName { set; get; }
            public string SupervisorName { set; get; }
            public string EscalationName { set; get; }
            public string PolicyPriority { set; get; }
            public int ResponseTime { set; get; }
            public int FollowUpTime { set; get; }
            public int EscalationTime { set; get; }
            public int Reopen { set; get; }
            
            public long AttachmentID { get; set; }
            public string AttachmentName { get; set; }
            public string ContentType { get; set; }
            public int StatusID { set; get; }
            public string StatusName { get; set; }
            public string DisplayName { get; set; }
            public string StatusColor { get; set; }
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
            public string Message { get; set; }
            public string createdbyName { get; set; }
        }

        public class CreateTicket
        {
            [Required(ErrorMessage = "Catgeory Can't Blank")]
            public long CategoryID { get; set; }
            [Required(ErrorMessage = "Sub Catgeory Can't Blank")]
            public long SubCatgeoryID { get; set; }
            public List<MasterAll> CategoryList { get; set; }
            public List<SubCategory> SubCategoryList { get; set; }
            public List<TicketSetting_SMA> PriorityList { get; set; }
             public long LocationGroupID { get; set; }
            public string LocationGroup { get; set; }
            [Required(ErrorMessage = "Priority Can't Blank")]
            public long SelectedPriorityID { get; set; }
            public HttpPostedFileBase UploadFile { get; set; }
            [Required(ErrorMessage = "Message Can't Blank")]
            public string Message { get; set; }
            public long AttachmentID { get; set; }
        }

        public class TicketOtherDet
        {
            public long AssignedTo { get; set; }
            public string AssignedToName { get; set; }
            public List<Helpdesk.TicketSetting_SMA> TicketPriorityList { get; set; }
        }

        public class Ticket_Status
        {
            public long TicketStatusID { get; set; }
            [Required(ErrorMessage = "Type Can't Blank")]
            public string Type { get; set; }
            [Required(ErrorMessage = "Status Name Can't Blank")]
            public string StatusName { get; set; }
            [Required(ErrorMessage = "Display Name Can't Blank")]
            public string DisplayName { get; set; }
              [Required(ErrorMessage = "Status Color Can't Blank")]
            public string StatusColor { get; set; }
            public bool IsActive { set; get; }
            public bool Readonly { set; get; }
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
        public class TicketNotes
        {
            public long TicketNotesID { get; set; }
            public long TicketID { get; set; }
            [Required(ErrorMessage = "Status Can't Blank")]
            public long StatusID { get; set; }
            public string StatusName { get; set; }
            public string DisplayName { get; set; }
            public string StatusColor { get; set; }
            public string NextDate { get; set; }

            [Required(ErrorMessage = "Remarks Can't Blank")]
            public string Remarks { get; set; }
            public int createdby { get; set; }
            public string createdat { get; set; }
            public int modifiedby { get; set; }
            public string modifiedat { get; set; }
            public int deletedby { get; set; }
            public string deletedat { get; set; }
            public int isdeleted { get; set; }
            public string IPAddress { get; set; }
            public string createdbyName { get; set; }
            public long? ForwardTo { get; set; }
        }

        public class TicketDetails
        {
            public List<TicketNotes> TicketNotesList { get; set; }
            public Ticket Ticket { get; set; }
            public List<TicketDeferred>  TicketDeferred { get; set; }

        }
        public class TicketDeferred
        {
            public long DeferredID { get; set; }
            public string DeferredName { get; set; }
            public string DeferredNotesID { get; set; }
        }


    }
}