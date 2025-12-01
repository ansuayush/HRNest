using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class CreateTravelRequest
    {
        public TravelRequest TravelRequest { get; set; }
        public List<TravellerName> TravellerNameList { get; set; }
        public List<ProjectList_Travel> ProjectList_Travel { get; set; }
        public List<Projects_Dropdown> Projects_Dropdown { get; set; }
        public List<ProjectDetail_Dropdown> ProjectDetail_Dropdown { get; set; }
        public List<City> CityList { get; set; }
        public List<MultipleCity> MultipleCityList { get; set; }

    }
    public class TravelRequest
    {
        public string Status { get; set; }
        public long TravelRequestID { set; get; }
        [Required(ErrorMessage = "Request No Can't be blank")]
        public string req_no { get; set; }
        [Required(ErrorMessage = "Request Date Can't be blank")]
        public string req_date { get; set; }
        public string reqno { get; set; }
        public string TravelMode { get; set; }
        public long emp_id { get; set; }
        public string emp_type { get; set; }
        public string traveller_name { get; set; }
        public string generatedby { get; set; }

        [Required(ErrorMessage = "Purpose Of Visit Can't be blank")]
        public string purpofvisit { get; set; }
        public string emailto { get; set; }
        public string emailCc { get; set; }
        public int approved { get; set; }
        public int submited { get; set; }
        public string submiteddate { get; set; }
        public string RequestType { get; set; }
        [Required(ErrorMessage = "Please Select Travel Type ")]
        public string TravelType { get; set; }
        [Required(ErrorMessage = "Please Select Trip Type ")]
        public string TripType { get; set; }

        public int? FromCity { get; set; }

        public int? ToCity { get; set; }

        public string DepartureDate { get; set; }
        public string ReturnDate { get; set; }
        public string TripSponsorName { get; set; }
        public long SponsorAttachID { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string IsTicketToBeBooked { get; set; }
        public string IsHotelToBeBooked { get; set; }
        public int IsAdvanceRequest { get; set; }
        public string phoneno { get; set; }
        public string emailid { get; set; }
        public string airline { get; set; }
        public string prefered_seat { get; set; }
        public string online_checking { get; set; }
        public string frequentflyer_No { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
        public string reason { get; set; }
        public int isbooked { get; set; }
        public string Rfcreason { get; set; }
        public string requester_email { get; set; }
        public string contact_no { get; set; }
        public int isTravelAgent { get; set; }
        public int mailsent { get; set; }
        public long amend_id { get; set; }
        public string amend_remark { get; set; }
        public string user_remarks { get; set; }
        public int amend { get; set; }
        public string amendcancel_reason { get; set; }
        public int imailHod { get; set; }
        public int iMailTravelDesk { get; set; }
        public int iMailTravelFinan { get; set; }
        public int createdby { get; set; }
        public string createdat { get; set; }
        public int modifiedby { get; set; }
        public string modifiedat { get; set; }
        public int deletedby { get; set; }
        public string deletedat { get; set; }
        public int isdeleted { get; set; }
        public string IPAddress { get; set; }
        // add two properties using advanced finance report
        public string app_date { get; set; }
        public string ApprovedBy { get; set; }
        public string doc_no { get; set; }
        public string AmendmentType { get; set; }
        public int Expectional { get; set; }
        public int ExpectionalApproved { set; get; }
        public string ReasonAmendment { get; set; }
        public int HodId { get; set; }
        public int ApprovedStatus { get; set; }
        public long AmendUserId { get; set; }
        public string ExpectionalBy { get; set; }
        public string ExpectionalDate { get; set; }
        public string deptjustification { get; set; }
        public string retjustification { get; set; }

    }

    public class TravellerName
    {
        public string EmpID { set; get; }
        public string EmpCode { get; set; }
        public string EmpType { get; set; }
    }

    public class ProjectList_Travel
    {
        public long MapProjectID { get; set; }
        public long ProjectID { get; set; }
        public string Proj_name { get; set; }
        public long proReqDet_ID { get; set; }
        public string costcenter_Name { get; set; }
        public long TravelRequestID { get; set; }
        public int SNo { get; set; }
        public string DocNo { get; set; }
        public long Approved { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }

    }

    public class Projects_Dropdown
    {
        public long ID { set; get; }
        public string ProjectRefNo { get; set; }
        public string ProjectName { get; set; }
    }
    public class ProjectDetail_Dropdown
    {
        public long ID { set; get; }
        public long ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string SubActivity { get; set; }
    }


    public class TravelRequestDetails
    {
        public long RequestDetailID { set; get; }
        public long TravelRequestID { set; get; }
        public int SRNO { set; get; }
        public string TravelDate { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public int FromCityID { get; set; }
        public int ToCityID { get; set; }
        public string TravelMode { get; set; }
        public int TravelModeID { get; set; }
        public string OfficePersonel { get; set; }
        public int OfficePersonelID { get; set; }
        public string TicketBooking { get; set; }
        public int TicketBookingID { get; set; }
        [Required(ErrorMessage = "Please enter TicketDetails ")]
        public string TicketDetails { get; set; }
        [Required(ErrorMessage = "Please enter Justification ")]
        public string Justification { get; set; }
        public int TicketSource { get; set; }
        public int ModePayment { get; set; }

        public decimal Amount { get; set; }
        public int AttachmentID { set; get; }
        public string Completed { get; set; }
        public int HotelBookingID { get; set; }
        public string HotelBooking { get; set; }
        public string AttachmentIDHotel { get; set; }
        public string HotelJust { get; set; }
        public string Refundable { get; set; }
        public string BillNo { get; set; }
        public string BillDate { get; set; }
        public decimal BillAmount { get; set; }
        public string CreditNoteNO { get; set; }
        public string IsCompleted { get; set; }
        public int TargetAttachment2 { set; get; }
        public string Reason { get; set; }
        public string IsTicketBooked { get; set; }
        public string TDTicketBooked { get; set; }
        public int Srno1 { set; get; }
        public string BillRemarks { get; set; }
    }
    public class MultipleCity
    {
        public long ID { get; set; }
        public int? FromCity { get; set; }

        public int? ToCity { get; set; }

        public string DepartureDate { get; set; }
        public long JustID { get; set; }
        public string justification { get; set; }
    }

    public class CreateItineraryDetails
    {
        public string AdvanceRequired { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal PerdiemAmount { get; set; }
        public decimal HotelAmount { get; set; }
        public decimal TransportationAmount { get; set; }
        public string OtherSection1 { get; set; }
        public string OtherSection2 { get; set; }
        public decimal OtherAmount1 { get; set; }
        public decimal OtherAmount2 { get; set; }

        public string pay_mode { get; set; }
        public string account_no { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string neft_code { get; set; }
        public List<ItineraryTravelDetail> TravelDetailList { get; set; }
    }
    public class ItineraryTravelDetail
    {
        public long TravelDetailID { get; set; }
        public long travelreq_id { get; set; }
        public string req_no { get; set; }
        public string req_date { get; set; }
        public string Travel_date { get; set; }
        public int Travel_modeID { get; set; }
        public int officePersonelID { get; set; }
        public int ticketbookingID { get; set; }
        public int hotelbookingID { get; set; }
        public string ticketdetail { get; set; }
        public string justification { get; set; }
        public string HotalName { get; set; }
        public string hotel_no { get; set; }
        public string OtherHotel { get; set; }
        public string fromCity { get; set; }
        public int fromCityID { get; set; }
        public string toCity { get; set; }
        public int ToCityID { get; set; }
        public string ClassOfCity { get; set; }
        public decimal PerDiem_Rate { get; set; }
        public string ToClassCity { get; set; }
        public string FromClassCity { get; set; }
        public decimal Toperdiem_rate { get; set; }
        public decimal Fromperdiem_rate { get; set; }
        public decimal Hotel_rate { get; set; }
        public string Status { get; set; }

        public string SelfArrangejustification { get; set; }

    }
 
    public class TravelDocuments
    {
        public long TravelDocID { get; set; }
        public long TravelRequestID { get; set; }
        [Required(ErrorMessage = "Docuement Can't Blank")]
        public string DocumentType { get; set; }
        public long AttachmentID { get; set; }
        public string filename { get; set; }
        public string content_type { get; set; }
        [Required(ErrorMessage = "Date Can't Blank")]
        public string TransactionDate { get; set; }


        public decimal Amount { get; set; }
        public decimal RefundAmount { get; set; }

        //[Required(ErrorMessage = "Attachment Can't Blank")]
        public HttpPostedFileBase UploadAttachment { get; set; }
        public string AgentName { get; set; }
        public long? StafId { get; set; }
        public int isbooked { get; set; }
        public int approved { get; set; }
        public string BookedBy { get; set; }
        public string CardandBank { get; set; }
        public string BillNo { get; set; }
        public string BillDate { get; set; }

    }
    public class ViewTravelRequest_ForDesk
    {
        public string  checkbox { get; set; }
        public List<RequestDetail_ForDesk> RequestDetailList { get; set; }
        public List<TravelDocuments> TravelDocumentsList { get; set; }
    }

    public class RequestDetail_ForDesk
    {
        public long travelreq_id { get; set; }
        public string ReqNo { set; get; }
        public string req_date { get; set; }
        public string Generatedby { set; get; }
        public string RequestType { set; get; }
        public string DepartureDate { set; get; }
        public string EMPName { set; get; }
        public string Gender { set; get; }
        public string Telephone { set; get; }
        public string TripType { set; get; }
        public string Dateofbirth { set; get; }
        public string Mealpreference { set; get; }
        public string Seatpreference { set; get; }
        public string PassportNo { set; get; }
        public string PassportExpiryDate { set; get; }
        public string VisaRequired { set; get; }
        public string InsuranceRequired { set; get; }
        public string PanNo { set; get; }
        public string AdharNo { set; get; }

        // Travel details part
        public long TravelDetailID { get; set; }
        public string Travel_date { get; set; }
        public string Travel_mode { get; set; }
        public int Travel_modeID { get; set; }
        public int officePersonelID { get; set; }
        public int ticketbookingID { get; set; }
        public string TicketBooking { get; set; }
        public int hotelbookingID { get; set; }
        public string ticketdetail { get; set; }
        public string justification { get; set; }
        public string HotalName { get; set; }
        public string hotel_no { get; set; }
        public string OtherHotel { get; set; }
        public string fromCity { get; set; }
        public int fromCityID { get; set; }
        public string toCity { get; set; }
        public int ToCityID { get; set; }
        public string ProjectName { get; set; }
        public string approvedby { get; set; }
        public string approved_date { get; set; }
        public int Isbooked { get; set; }
        public int approved { get; set; }
        public int Itemlinebooked { get; set; }
        public string AmendmentType { get; set; }
        public string EMPMail { set; get; }
        public long versionno { get; set; }
        public string Flyername { set; get; }
        public string TraveldeskName { set; get; }
        public long EmpWorkLocationID { get; set; }
        public long TravelDeskEmployeeId { get; set; }
        public long TransferTravelDeskUserId { get; set; }
        public long ID { get; set; }
        public string ExpName { set; get; }
        public string ExpectApproveddate { set; get; }

    }

    public class AdvancePaymentUser
    {
        public long Id { get; set; }
        public List<FinancePayment_Staff> listfinancePayment_Staffs { get; set; }
    }
    public class FinancePayment_Staff
    {
        public long Id { get; set; }
        public long TravelConfirmId { get; set; }
        public string req_no { get; set; }
        public string trpn_no { get; set; }
        public string req_Date { get; set; }
        public string travller_name { get; set; }
        public string doc_no { get; set; }
        public string proj_name { get; set; }
        public decimal totaladvanceroundoff { get; set; }
        public long approved { get; set; }
        public string Paid_Date { get; set; }
        public decimal Paid_Amount { get; set; }
        public string reason { get; set; }
        public long approvedammount { get; set; }
        public string Stage { get; set; }
    }

    public class TravelDeskToFinaceCard
    {
        public class TravelDeskToFinance
        {
         
            public List<TravelDeskToFinance_Card> listTravelDeskToFinance_Card { get; set; }
        }
        public class TravelDeskToFinance_Card
        {
            public long Id { get; set; }
            public long ReqId { get; set; }
            public long TravelDocID { get; set; }
            public string req_no { get; set; }
            public string traveller_name { get; set; }
            public string req_Date { get; set; }
            public string doc_no { get; set; }
            public string TransactionDate { get; set; }
            public string BookBy { get; set; }
            public decimal Amount { get; set; }
            public decimal RefundAmount { get; set; }
            public string TravelDesk { get; set; }
            public string CardandBank { get; set; }
            public string DocumentType { get; set; }
            public decimal FinanceAmount { get; set; }
            public string Financeentrydate { get; set; }
            public long Approved { get; set; }
            public string reason { get; set; }
            public string Billdate { get; set; }
            public string BillNo { get; set; }
            public string UTRNo { get; set; }
            

        }
    }


    public class TravelDeskToFinaceCardView
    {
        public long Id { get; set; }
        public long versionno { get; set; }
        public string req_no { get; set; }
        public string traveller_name { get; set; }
        public string purpofvisit { get; set; }
        public string Req_Date { get; set; }
        public string app_date { get; set; }
        public string TransactionDate { get; set; }
        public string Financeentrydate { get; set; }
        public decimal FinanceAmount { get; set; }
        public string ReasonAmendment { get; set; }
        public string AmendmentType { get; set; }
        public decimal RefundAmount { get; set; }
        public string ProjectCode { get; set; }
        public string HODName { get; set; }
        public string BookBy { get; set; }
        public string CardandBank { get; set; }
        public decimal Amount { get; set; }
        public string DocumentType { get; set; }
        public string AgentName { get; set; }
        public string BillNo { get; set; }
        public string Billdate { get; set; }
        public string UTRNo { get; set; }
        public string reason { get; set; }
        public List<TravelRequestDetailsView> travelRequestDetailsViewsHis { get; set; }
        public List<TravelRequestDetailsView> travelRequestDetailsViews { get; set; }
    }

    public class TravelRequestDetailsView
    {
        public long ID { get; set; }
        public long travelreq_id { get; set; }
        public string req_no { get; set; }
        public string Req_Date { get; set; }
        public string srno { get; set; }
        public string travel_date { get; set; }
        public string travel_Newdate { get; set; }
        public string fromCity { get; set; }
        public string toCity { get; set; }
        public string TravelMode { get; set; }
        public string HotelName { get; set; }
        public string TravelDeskName { get; set; }
        public long isbooked { get; set; }
        public string TicketBooking { get; set; }
        public string ticketdetail { get; set; }
        public string justification { get; set; }
    }


}