using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Mitr.Models.AllEnum;

namespace Mitr.Models
{
    public class EmployeeMasterUpdate
    {
        public long AccountID { get; set; }
        public string Doctype { get; set; }
        public long EMPID { get; set; }
        public long BankID { get; set; }
        public string BankName { get; set; }
        public AccountType AccountType { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string IFSCCode { get; set; }
        public string SwiftCode { get; set; }
        public string OtherDetails { get; set; }
        public int? Priority { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        public string gender { get; set; }
        public string marital_status { get; set; }
        public string SpouseName { get; set; }
        public string PartnerName { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public long children { get; set; }
        public string lane1 { get; set; }
        public string zip_code { get; set; }
        public long CountryID { get; set; }
        public long StateId { get; set; }
        public long CityID { get; set; }
        public string lane2 { get; set; }

        public string AnyMedicalCondition { get; set; }
        public string PhysicianName { get; set; }
        public string PhysicianNumber { get; set; }
        public string PhysicianAlternate_No { get; set; }
        public string emergContact_no { get; set; }
        public string emergContact_Name { get; set; }
        public string emergContact_Relation { get; set; }
        public BloodGroup BloodGroup { get; set; }
       public YesNo? SpecialAbility { get; set; }
        public string Course { get; set; }
        public string University { get; set; }
        public string Location { get; set; }
        public string Year { get; set; }
        public string AirlineName { get; set; }
        public string PIO { get; set; }
        public string PIOName { get; set; }
        public string FlyerNumber { get; set; }
        public string OldPFNo { get; set; }
        public decimal Ammount { get; set; }
        public string Remarks { get; set; }
        public HttpPostedFileBase UploadAttachment { get; set; }
        public long AttachmentID { get; set; }
        public string VoterId { get; set; }
        public string PassportNo { get; set; }
        public string PassportName { get; set; }
        public string PassportPlaceOfissue { get; set; }
        public string PassportExpDate { get; set; }
        public string DinNo { get; set; }
        public string DinName { get; set; }

        public string DlNo { get; set; }
        public string DlName { get; set; }
        public string IssueDate { get; set; }
        public string DlExpDate { get; set; }
        public string DlPlaceOfissue { get; set; }
        public string DlRemarks { get; set; }
        public long SeatPreferencesID { get; set; }
        public long MealPreferenceID { get; set; }
        public string src { get; set; }
        public string Mobile { get; set; }



    }
}