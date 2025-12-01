using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Onboarding
{
    public class OnboardingProcess : BaseModel
    {
        public int Id { get; set; }
        public int IsGrid { get; set; }
        public int InputData { get; set; }
        public OnboardingConsultant OnboardingConsultantData { get; set; }
        public Offer OfferModel { get; set; }
        public JoiningKit JoiningKitModel { get; set; }
        public PreRegistration PreRegistrationModel { get; set; }
        public Registration RegistrationModel { get; set; }
        public List<PreRequisites> PreRequisitesModel { get; set; }
        public List<UploadingDocument> UploadingDocumentModel { get; set; }
        public List<OrientationSchedule> OrientationSchedulesModel { get; set; }
        public OrientationSchedule OrientationReschedulesModel { get; set; }
        public List<Attachments> AttachmentsModel { get; set; }
        public List<AirlineDetails> AirlineDetailsModel { get; set; }
        public OnboardingUsers OnboardingUsers { get; set; }
    }

    public class OnboardingConsultant
    {
        public int CandidateId { get; set; }

    }
    public class Offer
    {
        public int CandidateId { get; set; }
        public int JobId { get; set; }
        public string EmployeeName { get; set; }
        public string Phone { get; set; }
        public DateTime? IssuesDOFLetter { get; set; }
        public string EmailID { get; set; }
        public string TypeofEmployeeCategory { get; set; }
        public string TermoftheEmployee { get; set; }
        public int AnnualSalaryBenefit { get; set; }
        public string EmploymentStatus { get; set; }
        public string EmploymentCategory { get; set; }
        public bool DocumentVerified { get; set; }
        public bool ReferenceChecked { get; set; }
        public string AttachmentActualName { get; set; }
        public string AttachmentNewName { get; set; }
        public string AttachmentUrl { get; set; }
        public DateTime? OfferAcceptedDate { get; set; }
        public DateTime? ExpectedDateOfJoining { get; set; }
        public DateTime? EndDateOfContract { get; set; }
        public string IPAddress { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string ApprovedBy { get; set; }
        public int IsAccept { get; set; }
        public string Reason { get; set; }
        public bool RejectedByC3 { get; set; }
        public bool RejectedByCandidates { get; set; }
        public bool DoNotMoveToTalentPool { get; set; }
        public DateTime? ExpectedLastDateofProbation { get; set; }
        public string OfferLetterBody { get; set; }

    }
    public class JoiningKit
    {
        public int JKitID { get; set; }
        public int CandidateId { get; set; }
        public string EmployeeName { get; set; }
        public string EmailID { get; set; }
        public DateTime? IssuesDOFLetter { get; set; }
        public DateTime? OfferAcceptedDate { get; set; }
        public DateTime? ExpectedDOJ { get; set; }
        public DateTime? EndDateofContract { get; set; }
        public string PsychometricTest { get; set; }
        public bool RejectedByC3 { get; set; }
        public bool RejectedByCandidates { get; set; }
        public bool DoNotMoveToTalentPool { get; set; }
        public string Reason { get; set; }
        public DateTime? SendOn { get; set; }
        public int IsAccept { get; set; }
        //public DateTime? JoiningKitSendOn { get; set; }
    }
    public class PreRegistration
    {
        public int CandidateId { get; set; }
        public string EmployeeName { get; set; }
        public string EmailID { get; set; }
        public string Gender { get; set; }
        public DateTime? DateofBirth { get; set; }
        public bool MitrUser { get; set; }
        public string OfficialEmailID { get; set; }
        public DateTime? ExpectedDateofJoining { get; set; }
        public string Designation { get; set; }
        public string PrimarySupervisor { get; set; }
        public string SecondarySupervisor { get; set; }
        public string ExceptionalApprover { get; set; }
        public string HRPointPerson { get; set; }
        public string WorkLocation { get; set; }
        public string MetroNonMetro { get; set; }
        public string ThematicArea { get; set; }
        public string OfferLetter { get; set; }
        public string CV { get; set; }
        public bool DocumentVerified { get; set; }
        public bool ReferenceChecked { get; set; }
        public int UploadingDocumentID { get; set; }
        public int PreRequisitesID { get; set; }
        public string AttachmentActualName { get; set; }
        public string AttachmentNewName { get; set; }
        public string AttachmentUrl { get; set; }
        public int Master_Emp_ID { get; set; }
    }
    public class Registration
    {
        public int RegID { get; set; }
        public int CandidateId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string MarriedNoOfChildren { get; set; }
        public string SpousePartner { get; set; }
        public string SpousesName { get; set; }
        public string PartnersName { get; set; }
        public DateTime? AnniversaryDate { get; set; }
        public string DivorcedNoOfChildren { get; set; }
        public string Country { get; set; }
        public string DetailOfWorPermitVisa { get; set; }
        public DateTime? ValidityDate { get; set; }
        public string AnyOtherDetails { get; set; }
        public string LAAddressLine1 { get; set; }
        public string LAAddressLine2 { get; set; }
        public string LAPinCode { get; set; }
        public string LACountry { get; set; }
        public string LAState { get; set; }
        public string LACity { get; set; }
        public string LAMobile { get; set; }
        public string PAAddressLine1 { get; set; }
        public string PAAddressLine2 { get; set; }
        public string PAPinCode { get; set; }
        public string PACountry { get; set; }
        public string PAState { get; set; }
        public string PACity { get; set; }
        public string PAPhoneNumber { get; set; }
        public string PAAlternativePhoneNumber { get; set; }
        public string PALandlineNo { get; set; }
        public string PAPersonalEmailID { get; set; }
        public string SpecialAbility { get; set; }
        public string MDFormC3 { get; set; }
        public string MDConditionC3 { get; set; }
        public string MDHospitalPhysicianName { get; set; }
        public string MDPhoneNumber { get; set; }
        public string MDAlternativeNumber { get; set; }
        public string BloodGroup { get; set; }
        public string MDEmergencyContactName { get; set; }
        public string MDEmergencyContactRelationship { get; set; }
        public bool LIFlagPan { get; set; }
        public string LIPan { get; set; }
        public string LINameOnPAN { get; set; }
        public string LIRemark { get; set; }
        public string BranchAddress { get; set; }
        public bool LIFlagAadhar { get; set; }
        public string LIAadharNo { get; set; }
        public string LINameOnAadharCard { get; set; }
        public bool LIFlagVoterID { get; set; }
        public string LIVoterIDNo { get; set; }
        public bool LIFlagPassport { get; set; }
        public string LIPassportNo { get; set; }
        public string LINameOnPassport { get; set; }
        public string LIPlaceOfIssue { get; set; }
        public string LIPassportExpiryDate { get; set; }
        public bool LIFlagDIN { get; set; }
        public string LIDIN { get; set; }
        public string LINameOnDIN { get; set; }
        public string LEAmtOfIncome { get; set; }
        public bool LIFlagUAN { get; set; }
        public string LIUAN { get; set; }
        public string LINameOnUAN { get; set; }
        public bool LIFlagPIOOCI { get; set; }
        public string LIPIOOCI { get; set; }
        public string LINameOnPIOOCI { get; set; }
        public bool LIFlagDrivingLicense { get; set; }
        public string LIDrivingLicenseNo { get; set; }
        public bool LIFlagPTAD { get; set; }
        public string LIAmount { get; set; }
        public string ResidentialStatus { get; set; }
        public string BenefitsNameOfTheBank { get; set; }
        public string BenefitsTypeOfBankAccount { get; set; }
        public string BenefitsNameOnAccount { get; set; }
        public string BenefitsBankAccountNo { get; set; }
        public string BenefitsBranchAddress { get; set; }
        public string BenefitsIFSCode { get; set; }
        public string BenefitsSWIFTCode { get; set; }
        public string BenefitsOtherDetails { get; set; }
        public string ReimbursementNameOfTheBank { get; set; }
        public string ReimbursementTypeOfBankAccount { get; set; }
        public string ReimbursementNameOnAccount { get; set; }
        public string ReimbursementBankAccountNo { get; set; }
        public string ReimbursementBranchAddress { get; set; }
        public string ReimbursementIFSCode { get; set; }
        public string ReimbursementSWIFTCode { get; set; }
        public string ReimbursementOtherDetails { get; set; }
        public string LEDEmployerName { get; set; }
        public DateTime? LEDDateOfJoining { get; set; }
        public DateTime? LEDDateOfLeaving { get; set; }
        public string LEDTotalWorkExperience { get; set; }
        public decimal LEDLastAnnualCTC { get; set; }
        public string LEDDesignation { get; set; }
        public string LEDLocation { get; set; }
        public string LEDTermofEmployment { get; set; }
        public string LEDInCaseOfGap { get; set; }
        public decimal LEDAmountOfIncome { get; set; }
        public decimal LEDAmountOfTDSDeducted { get; set; }
        public string SeatPreference { get; set; }
        public string MealPreference { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedAt { get; set; }
        public string DeletedAt { get; set; }
        public string Isdeletad { get; set; }
        public string IPAddress { get; set; }
        public string SSN { get; set; }
        public bool CurrentFinancialYear { get; set; }
    }

    public class AirlineDetails
    {
        public int AirlineID { get; set; }
        public int CandidateId { get; set; }
        public string AirlineName { get; set; }
        public string FrequentFlyerNumber { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string IPAddress { get; set; }
    }
    public class PreRequisites
    {
        public int CandidateId { get; set; }
        public string PreRequisitesType { get; set; }
        public string Remark { get; set; }
        public string Action { get; set; }
    }
    public class UploadingDocument
    {
        public int CandidateId { get; set; }
        public string DocumentType { get; set; }
        public string Attachment { get; set; }
        public string AttachmentActualName { get; set; }
        public string AttachmentNewName { get; set; }
        public string AttachmentUrl { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
    }
    public class OrientationSchedule
    {
        public int OriID { get; set; }
        public int CandidateId { get; set; }
        public string NameOfMember { get; set; }
        public int EmpId { get; set; }
        public string OriDate { get; set; }
        public string Time { get; set; }
        public string Place { get; set; }
        public string Purpose { get; set; }
        public string Mode { get; set; }
        public string Action { get; set; }
        public string Feedback { get; set; }
        //public string CreatedBy { get; set; }
        //public string ModifiedBy { get; set; }
        //public string DeletedBy { get; set; }
        //public string CreatedAt { get; set; }
        //public string ModifiedAt { get; set; }
        //public string DeletedAt { get; set; }
        //public string Isdeletad { get; set; }
        //public string IPAddress { get; set; }
    }
    public class Users : BaseModel
    {
        public int Id { get; set; }
        public int IsGrid { get; set; }
        public int InputData { get; set; }
        public int CandidateId { get; set; }
        public string UserType { get; set; }
        public string UserLoginId { get; set; }
        public string UserName { get; set; }
        public string UserEmailId { get; set; }
        public string UserPassword { get; set; }
        public int UserRole { get; set; }
        public bool IsMitrUser { get; set; }
        public int LoginID { get; set; }
        public string IPAddress { get; set; }
    }
    public class Attachments
    {
        public int SRNo { get; set; }
        public int CandidateId { get; set; }
        public int AttachmentID { get; set; }
        public string NatureofDocuments { get; set; }
        public string AttachmentActualName { get; set; }
        public string AttachmentNewName { get; set; }
        public string AttachmentUrl { get; set; }
        public string Remark { get; set; }

    }
    public class OnboardingUsers
    {
        public int Id { get; set; }
        public int UserManID { get; set; }
        public int CandidateId { get; set; }
        public int Status { get; set; }
        //public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }

   
}
