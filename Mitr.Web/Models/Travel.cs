using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class TravelDashboard
    {
        public List<MyTravelRequest> MyTravelRequest { get; set; }
        public List<MyTravelRequest> TravelRequestForApproval { get; set; }
    }
    public class Travel
    {
        public int Approved { get; set; }
        
    }
    public class MyTravelRequest
    {
        public long ID { set; get; }
        public string TravelDetails { get; set; }
        public string TravellerName { get; set; }
        public string ReqNo { get; set; }
        public string RequestDate { get; set; }
        public string TravelType { get; set; }
        public int Approved { set; get; }
        public int submited { set; get; }
        public int IsTicketBookedCompleted { get; set; }
        public string PurposeOfVisit { get; set; }
        public string Status { get; set; }
        public string Stage { get; set; }
        public string createdat { set; get; }
        public string modifiedat { set; get; }
        public int createdby { get; set; }
        public int modifiedby { get; set; }
        public string IPAddress { set; get; }
        public int DoucmentCount { get; set; }
        public string RequestType { get; set; }
        public string RequestTypeChar { get; set; }
        public string ReturnDate { get; set; }
        public string DepartureDate { get; set; }
        public int CountTravelDocID { get; set; }
        public int TravelLineDetailsStatus { get; set; }
        public int isbooked { set; get; }
        public string reason { get; set; }
        public string ActionType { get; set; }
        public int Expectional { set; get; }
        public int RFC { set; get; }
        public long HodId { set; get; }
        public int CountTravelExpenseData { get; set; }
        public long TerApproved { get; set; }
        public long ExpectApproved { get; set; }
        public long PrintCount { get; set; }
        public long TerSubmited { get; set; }
        public long EdId { set; get; }
        public List<MyTravelRequest> myTravelRequests { get; set; }


    }

    public class TravelDetail
    {
        public long TravelDetailID { get; set; }
        public long travelreq_id { get; set; }
        public string req_no { get; set; }
        public string req_date { get; set; }
        public decimal billAmount { get; set; }
        public string billdate { get; set; }
        public string billNo { get; set; }
        public string refundable { get; set; }
        public int sno { get; set; }
        public string Travel_date { get; set; }

        public string TravelMode { get; set; }
        public int Travel_modeID { get; set; }
        public string TravelSource { get; set; }
        public int ticketsourceID { get; set; }
        public string OfficePersonel { get; set; }
        public int officePersonelID { get; set; }

        public string TicketBooking { get; set; }
        public int ticketbookingID { get; set; }
        public string hotelbooking { get; set; }
        public int hotelbookingID { get; set; }
        public decimal Amount { get; set; }
        public string ticketdetail { get; set; }
        public string justification { get; set; }

        public string HotalName { get; set; }
        public string hotel_no { get; set; }

        public string creditNoteNo { get; set; }
        public string hotel_just { get; set; }
        public string fromCity { get; set; }
        public int fromCityID { get; set; }
        public string toCity { get; set; }
        public int ToCityID { get; set; }
        public string ClassOfCity { get; set; }
        public decimal perdiem_rate { get; set; }
        
        public string completed { get; set; }
        public string ModePayment { get; set; }
        public int modePaymentID { get; set; }
        public long attachmentIdTAgent { get; set; }
        public long attachmentId { get; set; }
        public long attachmentIdHotel { get; set; }
        public string TDTicketBooked { get; set; }
        public string bill_remarks { get; set; }
    }

    public class AdvanceTravelRequest
    {
        public long AdvanceID { get; set; }
        public long TravelRequestID { get; set; }
        public string req_no { get; set; }
        public string req_date { get; set; }
        public string traveller_name { get; set; }
        public decimal localTravel_amt { get; set; }
        public decimal Loading_amt { get; set; }
        public decimal OtherExpend_amt { get; set; }
        public decimal total_amount { get; set; }

        public decimal totaladvanceroundoff { get; set; }
        public string pay_mode { get; set; }
        public string OtherSection { get; set; }
        public string OtherSection1 { get; set; }
        public decimal OtherAmount { get; set; }
        public decimal OtherAmount1 { get; set; }
        public int approved { get; set; }
        public string account_no { get; set; }
        public string bank_name { get; set; }
        public string neft_code { get; set; }
        public string branch_name { get; set; }
        public string email_cc { get; set; }
        public string otherRemark { get; set; }
        public int mailsent { get; set; }
        public decimal advance_amt { get; set; }
        public string reason { get; set; }
        public int submited { get; set; }
        public int imailHod { get; set; }
        public decimal PerDiem_Rate { get; set; }
        public decimal Paidamount { get; set; }
        public string PaidDate { get; set; }
        public string AdvanceApprovedby { get; set; }

    }
    public class ViewTravelRequest
    {
        public long TravelRequestID { get; set; }
        public long Approved { get; set; }
        public string reason { get; set; }
        public string ActionType { get; set; }
        public TravelRequest TravelRequest { get; set; }
        public List<ProjectList_Travel> ProjectList_Travel { get; set; }
        public List<TravelDetail> TravelRequestDetail { get; set; }
        public AdvanceTravelRequest AdvanceTravelRequest { get; set; }

    }

    public class TravelDeskList
    {
        public long TravelRequestID { get; set; }
        public string TravellerName { get; set; }
        public string TravelDetails { get; set; }
        public string ReqNo { get; set; }
        public string RequestDate { get; set; }
        public string PurposeOfVisit { get; set; }
        public string Status { get; set; }
        public int isbooked { get; set; }
        public int IsTicketRequired { get; set; }
        public int IsHotelRequired { get; set; }
        public string BookBy { get; set; }
    }
    public class TravelMode
    {
        public long Road { get; set; }
        public long Train { get; set; }
        public long Air { get; set; }
        public long Sea { get; set; }
        public long Hotel { get; set; }
    }
    public class TravelDesk
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long LocationCount { get; set; }
        public int AirTicketBooking { get; set; }
        public long AirTicketCount { get; set; }
        public bool IsActive { set; get; }
        public List<Travellocation> travellocations { get; set; }
    }
    public class EmployeeList
    {
        public long Id { get; set; }
        public long FinancePerson { get; set; }
        public string Name { get; set; }
    }
    public class Travellocation
    {
        public long Id { get; set; }
        public long LocationId { get; set; }
        public string LoctionName { get; set; }
        public string EmpName { get; set; }
        public long EmployeeId { get; set; }
        public int AirTicketBooking { get; set; }
        public string Checkbox { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string IPAddress { get; set; }
        public long LocationCount { get; set; }
        public long AirTicketCount { get; set; }
        public bool IsActive { set; get; }

    }
    public class TravellPerDiem
    {
      
        public string City { get; set; }
        public double PerDiemRate { get; set; }
        public string ClassCity { get; set; }
     

    }
}