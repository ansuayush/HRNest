using Mitr.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mitr.Models
{
   
    public class SubCategory
    {
        public long SubCategoryID { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long CategoryID { set; get; }
        public string CategoryName { set; get; }

        [Required(ErrorMessage = "Hey! You missed this field")]
        public string SubCategoryName { set; get; }

        [Required(ErrorMessage = "Hey! You missed this field")]
        public string Description { set; get; }
        public bool AnonymousEmployee { get; set; }
        public string Location { get; set; }
        public string Assignee { get; set; }
        public string Level1 { get; set; }
        public string DeviceType { get; set; }
        public string IPAddress { get; set; }
        public long Createdby { get; set; }
        public long Modifiedby { get; set; }
        public long Deletedby { get; set; }
        public string CreateDat { get; set; }
        public string ModifieDat { get; set; }
        public string DeleteDat { get; set; }
        public int IsDeleted { get; set; }
        public bool IsActive { get; set; }
        
    }
    public class SubCategoryAssigneeDetails
    {
        public long ID { set; get; }
        public long SubCategoryid { set; get; }
        public long Categoryid { set; get; }
        public string SubCategoryName { set; get; }

        [Required(ErrorMessage = "Hey! You missed this field")]
        public long LocationID { set; get; }
        public string Location { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long AssigneeId { set; get; }
        public string Assignee { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long[] Level1Id { set; get; }
        public long[] Level2Id { set; get; }
        public long[] Level3Id { set; get; }
        public string field_value { get; set; }
        public string Level1 { set; get; }
        public string Level2 { set; get; }
        public string Level3 { set; get; }
        public bool Level1EnableEmail { set; get; }
        public bool Level2EnableEmail { get; set; }
        public bool Level3EnableEmail { get; set; }
        public string DeviceType { get; set; }

        public long Createdby { get; set; }
        public long Modifiedby { get; set; }
        public long Deletedby { get; set; }
        public string CreateDat { get; set; }
        public string ModifieDat { get; set; }
        public string DeleteDat { get; set; }
        public int IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public List<DropdownModel> WorkLocationList { get; set; }
        public List<DropdownModel> EmployeeList { get; set; }
        public List<DropdownModel> Level1List { get; set; }
        public List<DropdownModel> Level2List { get; set; }
        public List<DropdownModel> Level3List { get; set; }
        public List<SubCategoryAssigneeDetails> SubCategoryAssigneeList { get; set; }
        public List<SubCategorySLAPolicy> SubCategorySLAPolicy { get; set; }
    }
    public class SubCategorySLAPolicy
    {
        public long ID { set; get; }
        public long? Categoryid { set; get; }
        public long? SubCategoryid { set; get; }
        public string Priority { set; get; }
        public bool ActionDefault { set; get; }
        public bool ActionFreezed { set; get; }
        [Range(1, 1000)]
        public string FirstResponse { get; set; }
        [Range(1, 1000)]
        public string FollowUpEvery { get; set; }
        [Range(1, 1000)]
        public string EscalationLevel1 { set; get; }
        [Range(1, 1000)]
        public string EscalationLevel2 { set; get; }
        [Range(1, 1000)]
        public string EscalationLevel3 { set; get; }
        [Range(1, 1000)]
        public string TicketAutoCloseAfter { set; get; }

        public string DeviceType { get; set; }
        public long Createdby { get; set; }
        public long Modifiedby { get; set; }
        public long Deletedby { get; set; }
        public string CreateDat { get; set; }
        public string ModifieDat { get; set; }
        public string DeleteDat { get; set; }
        public int IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
    public class ExternalMember
    {
        public ExternalMember()
        {
            Pin = 0;
            DOL = string.Empty;
            Address1 = string.Empty;
            Address2 = string.Empty;
            Address_Mobile = 0;
        }

        public long ID { get; set; }
        //[Required(ErrorMessage = "External Code Can't Blank")]
        public string ExternalCode { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public string External_Member { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public string DOJ { get; set; }
        public string DOL { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        //[Range(0,15)]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        [EmailAddress]
        public string EmailId { get; set; }
        public string Address1 { get; set; }
        public int? Pin { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public string Address2 { get; set; }
        [Phone]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public int? Address_Mobile { get; set; }
        public string DeviceType { get; set; }
        public long Createdby { get; set; }
        public long Modifiedby { get; set; }
        public long Deletedby { get; set; }
        public string CreateDat { get; set; }
        public string ModifieDat { get; set; }
        public string DeleteDat { get; set; }
        public int IsDeleted { get; set; }
        public bool IsActive { get; set; }

        //public List<Country> CountryList { get; set; }
        public List<DropdownModel> StateList { get; set; }
        public List<DropdownModel> CityList { get; set; }

    }
    public class UserGrievance
    {
        public long ID { get; set; }
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long Categoryid { get; set; }
        public string TicketNo { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long Subcatid { get; set; }
        public string SubCategoryName { get; set; }
        public long Locationid { get; set; }
        public long? SubcatAssingid { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long Priorityid { get; set; }
        public long SlaPriorityid { get; set; }
        public string Priority { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public string Description { get; set; }
        public bool IsAnonymous { get; set; }
        public long? Attachmentid1 { get; set; }
        public long? Attachmentid2 { get; set; }
        public string AttachmentPath1 { get; set; }
        public string AttachmentPath2 { get; set; }
        public HttpPostedFileBase UploadFile1 { get; set; }
        public HttpPostedFileBase UploadFile2 { get; set; }
        public string PrimaryAssignee { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public long? Statusid { get; set; }
        public long? ActionId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public long Createdby { get; set; }
        public string CreatedbyName { get; set; }

        public string LoginPage { get; set; }

        public string ModifiedbyName { get; set; }
        public long Modifiedby { get; set; }
        public long Deletedby { get; set; }
        public string Createdat { get; set; }
        public string Modifiedat { get; set; }
        public string Deletedat { get; set; }
        public int Isdeleted { get; set; }
        public string Mobile { get; set; }
        public long UserId { get; set; }
        public List<DropdownModel> CategoryList { get; set; }
        public List<DropdownModel> SubCategoryList { get; set; }
        public List<DropdownModel> PriorityList { get; set; }
        public List<DropdownModel> SLAPriorityList { get; set; }
        public List<UserGrievanceAccident> UserGrievanceAccidentList { get; set; }

    }
    public class UserGrievanceAccident
    {
        public long ID { get; set; }
        public long Grievanceid { get; set; }
        public long Categoryid { get; set; }
        public long SubCategoryid { get; set; }
        public long SubcatAssingid { get; set; }
        public long Locationid { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public string Choosename { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long? Escalateid { get; set; }
        public long? EscalateNextlevelid { get; set; }
        public long[] EmployeeId { get; set; }
        public string Employee { get; set; }
        public long Attachmentid { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public string Remarks { get; set; }
        public string Attachment { get; set; }
        public string Extention { get; set; }
        public string EscalateTo { get; set; }
        public string Type { get; set; }
        public long Createdby { get; set; }
        public long Modifiedby { get; set; }
        public long Deletedby { get; set; }
        public string Createdat { get; set; }
        public string Modifiedat { get; set; }
        public string CreatedbyName { get; set; }
        public string ModifiedbyName { get; set; }
        public string Deletedat { get; set; }
        public long Isdeleted { get; set; }
        public long? ResolvedCategoryid { get; set; }
        public long? ResolvedSubCategoryid { get; set; }
        public string Mobile { get; set; }
        public long UserId { get; set; }
        public long EmpId { get; set; }
        public string ToCCMailId { get; set; }
        public List<DropdownModel> EscalateList { get; set; }
        public List<DropdownModel> EmployeeList { get; set; }
        public List<DropdownModel> ChooseList { get; set; }
        public List<DropdownModel> CategoryList { get; set; }
        public List<DropdownModel> SubCategoryList { get; set; }
        public List<DropdownModel> EscalateNextlevelsList { get; set; }

    }
    public class SubcategoryGRAssigneeDetails
    {
        public long Id { get; set; }
        public string CATEGORYNAME { get; set; }
        public string SUBCATEGORYNAME { get; set; }
        public string location_name { get; set; }
        public string Level1id { get; set; }
        public string Level2id { get; set; }
        public string Level3id { get; set; }
        public string Assigneeid { get; set; }
    }
}