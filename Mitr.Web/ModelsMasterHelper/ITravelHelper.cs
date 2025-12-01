using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface ITravelHelper
    {
        TravelDashboard GetTravelDashboard(long Type);
        List<MyTravelRequest> GetMyTravelRequest_Dashboard(long Type);
        List<MyTravelRequest> GetTravelRequestForApproval_Dashboard(long Type);
        List<TravelRequest> GetTravelRequestList(long lTravelReqID);
        List<TravellerName> GetTravellerNameDetails();
        List<ProjectDetail_Dropdown> GetProjectDetail_Dropdown(long ProjectID);
        CreateTravelRequest CreateTravelRequest(long TravelRequestID);
        
        CreateItineraryDetails CreateItineraryDetails(long TravelRequestID);
        ViewTravelRequest GetViewTravelRequest(long TravelRequestID);
        List<TravelDocuments> GetTravelDocumentsList(long TravelRequestID,long Traveldocid);
        List<TravelDeskList> GetTravelDeskList_Dashboard(long TravelMode);
        List<TravelDeskList> GetTravelDeskList(long iIsBooked);
        ViewTravelRequest_ForDesk GetTravelRequest_ForDesk(long TravelRequestID);
        TravelExpenseCompleteRequest GetTravelExpenseCompleteRequest(long TravelRequestID);
        ViewTravelExpenseCompleteRequest GetViewTravelExpenseCompleteRequest(long TravelRequestID);
        TravelMode GetTravelMode();
        List<FinancePayment_Staff> GetTravelRequestAdvanceFinance();
        ViewTravelRequest GetViewTravelAdvancedRequest(long TravelRequestID);
        List<EmployeeList> GetTravelEmpList();
        List<Travellocation> GetTravellocationmap(long employeeid);
        PostResponse SetTravelLocationMap(Travellocation model);
        PostResponse DelTravelLocationMap(Travellocation model);
        List<Travellocation> GetTravelLocationMapList();
        LeaveEmp GetLeaveEmpDetailsTravel( string Traveldate);
        PostResponse GetPrintTERUser(long EmpId,long TravelRequestId);
        TravelDeskToFinaceCard.TravelDeskToFinance GetTravelDeskToFinance();
        TravelDeskToFinaceCardView GetTravelDeskToFinaceCardView(long Id, long TravelReqId);
        TravelDeskToFinaceCard.TravelDeskToFinance GetTravelDeskToFinanceAgent();
        TravelDeskToFinaceCard.TravelDeskToFinance GetTravelDeskToFinanceAgentPending();
        TravellPerDiem GetTravelPerdiemRate(string Traveldate,string req_no);
        List<MyTravelRequest> GeTravelRequest_Dashboard(long Type);
    }
}