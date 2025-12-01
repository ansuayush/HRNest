using Dapper;
using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMaster
{
    public class TravelModal : ITravelHelper
    {
        IMasterHelper Master;
        public TravelModal()
        {
            Master = new MasterModal();
        }

        public TravelDashboard GetTravelDashboard(long Type)
        {
            TravelDashboard obj = new TravelDashboard();
            obj.MyTravelRequest = GetMyTravelRequest_Dashboard(Type);
            obj.TravelRequestForApproval = GetTravelRequestForApproval_Dashboard(Type);
            return obj;
        }
        public List<MyTravelRequest> GetMyTravelRequest_Dashboard(long Type)
        {
            List<MyTravelRequest> List = new List<MyTravelRequest>();
            MyTravelRequest obj = new MyTravelRequest();
            DataSet TempModuleDataSet = new DataSet();

            try
            {
                TempModuleDataSet = Common_SPU.fnGetMyTravelRequest_Dashboard(Type);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new MyTravelRequest();
                    obj.ID = Convert.ToInt32(item["ID"]);
                    obj.TravellerName = item["Traveller_Name"].ToString();
                    obj.TravelDetails = item["TravelDetails"].ToString();
                    obj.TravelType = item["TravelType"].ToString();
                    obj.ReqNo = item["Req_No"].ToString();
                    obj.RequestDate = Convert.ToDateTime(item["Req_Date"]).ToString("dd-MMM-yyyy");
                    obj.PurposeOfVisit = item["PurpOfVisit"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"]);
                    obj.submited = Convert.ToInt32(item["submited"]);
                    obj.IsTicketBookedCompleted = Convert.ToInt32(item["IsTicketBookedCompleted"]);
                    obj.RequestType = item["RequestType"].ToString();
                    obj.RequestTypeChar = (string.IsNullOrEmpty(obj.RequestType) ? "D" : obj.RequestType.Substring(0, 1));
                    obj.Status = item["Status"].ToString();
                    obj.Stage = item["Stage"].ToString();
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.ReturnDate = Convert.ToDateTime(item["ReturnDate"]).ToString("dd-MMM-yyyy");
                    obj.DepartureDate = Convert.ToDateTime(item["DepartureDate"]).ToString("dd-MMM-yyyy");
                    obj.CountTravelDocID = Convert.ToInt32(item["CountTravelDocID"]);
                    obj.isbooked = Convert.ToInt32(item["isbooked"]);
                    obj.RFC = Convert.ToInt32(item["RFC"]);
                    obj.TerApproved = Convert.ToInt64(item["TERApproved"]);
                    obj.CountTravelExpenseData = Convert.ToInt32(item["CountTravelExpenseData"]);
                    obj.PrintCount = Convert.ToInt32(item["PrintCount"]);
                    obj.TerSubmited = Convert.ToInt32(item["TERSubmit"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetMyTravelRequest_Dashboard. The query was executed :", ex.ToString(), "fnGetMyTravelRequest_Dashboard ", "TravelModal", "TravelModal", "");
            }
            return List;

        }
                      
        public List<MyTravelRequest> GetTravelRequestForApproval_Dashboard(long Type)
        {
            List<MyTravelRequest> List = new List<MyTravelRequest>();
            MyTravelRequest obj = new MyTravelRequest();
            DataSet TempModuleDataSet = new DataSet();

            try
            {
                TempModuleDataSet = Common_SPU.fnGetTravelRequestForApproval_Dashboard(Type);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new MyTravelRequest();
                    obj.ID = Convert.ToInt32(item["ID"]);
                    obj.TravellerName = item["Traveller_Name"].ToString();
                    obj.TravelDetails = item["TravelDetails"].ToString();
                    obj.TravelType = item["TravelType"].ToString();
                    obj.ReqNo = item["Req_No"].ToString();
                    obj.RequestDate = Convert.ToDateTime(item["Req_Date"]).ToString("dd-MMM-yyyy");
                    obj.PurposeOfVisit = item["PurpOfVisit"].ToString();
                    obj.Approved = Convert.ToInt32(item["Approved"]);
                    obj.submited = Convert.ToInt32(item["submited"]);
                    obj.IsTicketBookedCompleted = Convert.ToInt32(item["IsTicketBookedCompleted"]);
                    obj.Status = item["Status"].ToString();
                    obj.Stage = item["Stage"].ToString();
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.IPAddress = item["IPAddress"].ToString();
                    obj.ReturnDate = Convert.ToDateTime(item["ReturnDate"]).ToString("dd-MMM-yyyy");
                    obj.DepartureDate = Convert.ToDateTime(item["DepartureDate"]).ToString("dd-MMM-yyyy");
                    obj.TravelLineDetailsStatus = Convert.ToInt32(item["TravelLineDetailsStatus"]);
                    obj.Expectional = Convert.ToInt32(item["Expectional"]);
                    obj.HodId = Convert.ToInt32(item["HodId"]);
                    obj.TerApproved = Convert.ToInt64(item["TERApproved"]);
                    obj.ExpectApproved = Convert.ToInt64(item["ExpectApproved"]);
                    obj.EdId = Convert.ToInt32(item["ed_name"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTravelRequest_Dashboard. The query was executed :", ex.ToString(), "fnGetTravelRequest_Dashboard ", "TravelModal", "TravelModal", "");
            }
            return List;

        }


        public List<TravelRequest> GetTravelRequestList(long lTravelReqID)
        {
            List<TravelRequest> List = new List<TravelRequest>();
            TravelRequest obj = new TravelRequest();
            DataSet TempModuleDataSet = new DataSet();

            try
            {
                TempModuleDataSet = Common_SPU.fnGetTravelReq(lTravelReqID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new TravelRequest();
                    obj.TravelRequestID = Convert.ToInt32(item["ID"]);
                    obj.isTravelAgent = Convert.ToInt32(item["isTravelAgent"]);
                    obj.emp_id = Convert.ToInt32(item["emp_id"]);
                    obj.emp_type = item["emp_type"].ToString();
                    obj.req_no = item["req_no"].ToString();
                    obj.reqno = item["reqno"].ToString();
                    obj.req_date = item["req_date"].ToString();
                    obj.TravelMode = item["TravelMode"].ToString();
                    obj.traveller_name = item["traveller_name"].ToString();
                    obj.req_date = item["req_date"].ToString();
                    obj.req_date = item["req_date"].ToString();
                    obj.generatedby = item["generatedby"].ToString();
                    obj.user_remarks = item["user_remarks"].ToString();
                    obj.purpofvisit = item["purpofvisit"].ToString();
                    obj.emailto = item["emailto"].ToString();
                    obj.emailCc = item["emailCc"].ToString();
                    obj.approved = Convert.ToInt32(item["approved"]);
                    obj.submited = Convert.ToInt32(item["submited"]);
                    obj.submiteddate = Convert.ToDateTime(item["submiteddate"]).ToString("yyyy-MM-dd");
                    obj.DepartureDate = Convert.ToDateTime(item["DepartureDate"]).ToString("yyyy-MM-dd");
                    obj.ReturnDate = Convert.ToDateTime(item["ReturnDate"]).ToString("yyyy-MM-dd");
                    obj.RequestType = item["RequestType"].ToString();
                    obj.TravelType = item["TravelType"].ToString();
                    obj.TripType = item["TripType"].ToString();
                    obj.FromCity = Convert.ToInt32(item["FromCity"]);
                    obj.ToCity = Convert.ToInt32(item["ToCity"]);
                    obj.SponsorAttachID = Convert.ToInt64(item["SponsorAttachID"]);
                    obj.IsAdvanceRequest = Convert.ToInt32(item["IsAdvanceRequest"]);
                    obj.FileName = item["FileName"].ToString();
                    obj.ContentType = item["Content_Type"].ToString();
                    obj.TripSponsorName = item["TripSponsorName"].ToString();
                    obj.IsTicketToBeBooked = item["IsTicketToBeBooked"].ToString();
                    obj.IsHotelToBeBooked = item["IsHotelToBeBooked"].ToString();
                    obj.phoneno = item["phoneno"].ToString();
                    obj.emailid = item["emailid"].ToString();
                    obj.airline = item["airline"].ToString();
                    obj.prefered_seat = item["prefered_seat"].ToString();
                    obj.online_checking = item["online_checking"].ToString();
                    obj.frequentflyer_No = item["frequentflyer_No"].ToString();
                    obj.reason = item["reason"].ToString();
                    obj.isbooked = Convert.ToInt32(item["isbooked"]);
                    obj.requester_email = item["requester_email"].ToString();
                    obj.contact_no = item["contact_no"].ToString();
                    obj.isTravelAgent = Convert.ToInt32(item["isTravelAgent"]);
                    obj.amend_id = Convert.ToInt32(item["amend_id"]);
                    obj.contact_no = item["contact_no"].ToString();
                    obj.amend_remark = item["amend_remark"].ToString();
                    obj.user_remarks = item["user_remarks"].ToString();
                    obj.amendcancel_reason = item["amendcancel_reason"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.createdby = Convert.ToInt32(item["createdby"]);
                    obj.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    obj.createdat = Convert.ToDateTime(item["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    obj.IPAddress = item["IPAddress"].ToString();
                    List.Add(obj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTravelRequestList. The query was executed :", ex.ToString(), "spu_GetTravelReq ", "TravelModal", "TravelModal", "");
            }
            return List;

        }

        public List<ProjectList_Travel> GetProjectList_Travel(long lTravelReqID)
        {
            List<ProjectList_Travel> List = new List<ProjectList_Travel>();
            ProjectList_Travel obj = new ProjectList_Travel();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetProjectList_Travel(lTravelReqID);

                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ProjectList_Travel();
                    obj.MapProjectID = Convert.ToInt32(item["ID"]);
                    obj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                    obj.TravelRequestID = Convert.ToInt32(item["Travel_id"]);
                    obj.SNo = Convert.ToInt32(item["Srno"]);
                    obj.Proj_name = item["Proj_name"].ToString();
                    obj.proReqDet_ID = Convert.ToInt32(item["projRegDet_ID"]);
                    obj.costcenter_Name = item["costcenter_Name"].ToString();
                    List.Add(obj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProjectList_Travel. The query was executed :", ex.ToString(), "fnGetProjectList_Travel ", "TravelModal", "TravelModal", "");
            }
            return List;

        }

        public List<TravellerName> GetTravellerNameDetails()
        {
            string SQL = "";
            List<TravellerName> List = new List<TravellerName>();
            TravellerName obj = new TravellerName();
            try
            {
                SQL = @"select * from v_project";
                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new TravellerName();
                    obj.EmpID = item["emp_id"].ToString();
                    obj.EmpCode = item["emp_code"].ToString();
                    obj.EmpType = item["Emp_Type"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTravellerNameDetails. The query was executed :", ex.ToString(), SQL, "TravelModal", "MasterModal", "");
            }
            return List;

        }
        public List<Projects_Dropdown> GetProjects_Dropdown()
        {
            string SQL = "";
            List<Projects_Dropdown> List = new List<Projects_Dropdown>();
            Projects_Dropdown obj = new Projects_Dropdown();
            try
            {
                //SQL = @"SELECT ROW_NUMBER() OVER (Order By Priority,projref_no) As RowNum, ID,projref_no ,proj_name+''+'('+doc_no+')' as  proj_name
                //        from project_registration where isdeleted =0 and isactive=1 and Inactive=1 and end_date>GETDATE() order by doc_no asc";// and inactive=0 condition remove 1/10/2022
                //DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                DataSet TempModuleDataSet = Common_SPU.fnGetProjectTravel_Dropdown();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Projects_Dropdown();
                    obj.ID = Convert.ToInt64(item["ID"]);
                    obj.ProjectRefNo = item["projref_no"].ToString();
                    obj.ProjectName = item["Proj_Name"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProjects_Dropdown. The query was executed :", ex.ToString(), SQL, "TravelModal", "MasterModal", "");
            }
            return List;

        }
        public List<ProjectDetail_Dropdown> GetProjectDetail_Dropdown(long ProjectID)
        {
            string SQL = "";
            List<ProjectDetail_Dropdown> List = new List<ProjectDetail_Dropdown>();
            ProjectDetail_Dropdown obj = new ProjectDetail_Dropdown();
            try
            {
                //SQL = @"select project_regdet.id,proj_id,master_costcenter.costCenter_code+ ' -'+ project_regdet.activity +' -' + project_regdet.sub_activity + ' -'+ project_regdet.sub_activity_descrip + ' -' +project_regdet.sub_sub_activity +' -'+ project_regdet.sub_sub_activity_descrip as SubActivity  from project_regdet 
                //        inner join  project_registration on project_regdet.proj_id=project_registration.id  
                //        left join master_costcenter  on project_regdet.activity_id= master_costcenter.id   where project_regdet.isdeleted=0 and  master_costcenter.isdeleted=0  and  
                //        project_registration.isdeleted=0 and project_regdet.activity_id<>0 and project_regdet.proj_id <>0 and  project_regdet.link_module ='Travel' and
                //        project_regdet.proj_id=" + ProjectID + "  Order by  project_regdet.id";
                //   DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);



                DataSet TempModuleDataSet = Common_SPU.fnGetProjectDetail_Dropdown(ProjectID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ProjectDetail_Dropdown();
                    obj.ID = Convert.ToInt32(item["SublineId"]);
                    obj.ProjectID = Convert.ToInt32(item["Projectid"]);
                    obj.SubActivity = item["SubActivity"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProjectDetail_Dropdown. The query was executed :", ex.ToString(), SQL, "TravelModal", "MasterModal", "");
            }
            return List;

        }

        public CreateTravelRequest CreateTravelRequest(long TravelRequestID)
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            CreateTravelRequest obj = new CreateTravelRequest();
            obj.CityList = Master.GetCityList(0).Where(x => x.IsActive).ToList();
            obj.TravellerNameList = GetTravellerNameDetails();
            obj.Projects_Dropdown = GetProjects_Dropdown();
            if (TravelRequestID == 0)
            {
                string SQL = "select top 1 ID from travel_request where isdeleted=2 and createdby=" + LoginID + " order by id desc";
                long.TryParse(clsDataBaseHelper.ExecuteSingleResult(SQL), out TravelRequestID);
            }
            if (TravelRequestID > 0)
            {
                obj.TravelRequest = GetTravelRequestList(TravelRequestID).FirstOrDefault();
                obj.ProjectList_Travel = GetProjectList_Travel(TravelRequestID);
                obj.MultipleCityList = GetMultipleCityList(TravelRequestID);

            }
            else
            {

                obj.TravelRequest = new TravelRequest();
                obj.TravelRequest.req_no = (Common_SPU.fnGetMaxID("travel_request_id") + 1).ToString();
                obj.TravelRequest.req_date = DateTime.Now.ToString();
                obj.ProjectList_Travel = new List<ProjectList_Travel>();
                List<MultipleCity> MultipleCityList = new List<MultipleCity>();
                MultipleCityList.Add(new MultipleCity());
                obj.MultipleCityList = MultipleCityList;
            }
            return obj;
        }

        public List<MultipleCity> GetMultipleCityList(long TravelRequestID)
        {
            string SQL = "";
            List<MultipleCity> List = new List<MultipleCity>();
            MultipleCity obj = new MultipleCity();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTravelReq_Det(TravelRequestID);
                if (TempModuleDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                    {
                        obj = new MultipleCity();
                        obj.ID = Convert.ToInt64(item["ID"]);
                        obj.FromCity = Convert.ToInt32(item["FromCityID"]);
                        obj.ToCity = Convert.ToInt32(item["ToCityID"]);
                        obj.DepartureDate = Convert.ToDateTime(item["Travel_Date"]).ToString("yyyy-MM-dd");

                        List.Add(obj);
                    }
                }
                else
                {
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetProjects_Dropdown. The query was executed :", ex.ToString(), SQL, "TravelModal", "MasterModal", "");
            }
            return List;

        }





        public List<ItineraryTravelDetail> GetItineraryTravelDetailList(long TravelRequestID)
        {
            string SQL = "";
            List<ItineraryTravelDetail> List = new List<ItineraryTravelDetail>();
            ItineraryTravelDetail obj = new ItineraryTravelDetail();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTravelReq_Det(TravelRequestID);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new ItineraryTravelDetail();
                    obj.TravelDetailID = Convert.ToInt64(item["ID"]);
                    obj.travelreq_id = Convert.ToInt64(item["travelreq_id"]);
                    obj.req_no = item["req_no"].ToString();
                    obj.req_date = item["req_date"].ToString();
                    obj.Travel_date = Convert.ToDateTime(item["travel_date"]).ToString("yyyy-MM-dd");
                    obj.Travel_modeID = Convert.ToInt32(item["Travel_modeID"]);
                    obj.officePersonelID = Convert.ToInt32(item["officePersonelID"]);
                    obj.ticketbookingID = Convert.ToInt32(item["ticketbookingID"]);
                    obj.hotelbookingID = Convert.ToInt32(item["hotelbookingID"]);
                    obj.ticketdetail = item["ticketdetail"].ToString();
                    obj.justification = item["justification"].ToString();
                    obj.hotel_no = item["hotel_no"].ToString();
                    obj.OtherHotel = item["OtherHotel"].ToString();
                    obj.fromCity = item["fromCity"].ToString();
                    obj.fromCityID = Convert.ToInt32(item["fromCityID"]);
                    obj.toCity = item["toCity"].ToString();
                    obj.ToCityID = Convert.ToInt32(item["ToCityID"]);
                    obj.ToClassCity = item["ToClassCity"].ToString();
                    obj.hotel_no = item["hotel_no"].ToString();
                    obj.FromClassCity = item["FromClassCity"].ToString();
                    obj.Toperdiem_rate = Convert.ToDecimal(item["Toperdiem_rate"]);
                    obj.Fromperdiem_rate = Convert.ToDecimal(item["Fromperdiem_rate"]);
                    obj.Hotel_rate = Convert.ToDecimal(item["Hotel_rate"]);
                   // obj.Status = item["Status"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetItineraryTravelDetailList. The query was executed :", ex.ToString(), SQL, "TravelModal", "MasterModal", "");
            }
            return List;

        }

        public CreateItineraryDetails CreateItineraryDetails(long TravelRequestID)
        {
            CreateItineraryDetails obj = new CreateItineraryDetails();
            obj.TravelDetailList = GetItineraryTravelDetailList(TravelRequestID);
            // fetch Reimbursement bank details by shailendra 02/11/2022
            DataSet Ds = Common_SPU.fnGetTravelEmployeeBankDeatils(Convert.ToInt64(clsApplicationSetting.GetSessionValue("EMPID")), "Reimbursement");
            //DataSet Ds = Common_SPU.fnGetTravelEmployeeBankDeatils(Convert.ToInt64(clsApplicationSetting.GetSessionValue("EMPID")), "Salary");
            // string SQL = @"select * from master_emp where isdeleted=0 and id=" + clsApplicationSetting.GetSessionValue("EMPID");
            // DataSet Ds = new DataSet();
            //  Ds = clsDataBaseHelper.ExecuteDataSet(SQL);
            if (Ds != null)
            {
                foreach (DataRow item in Ds.Tables[0].Rows)
                {
                    //  obj.pay_mode = item["pay_mode"].ToString();
                    obj.account_no = item["AccountNo"].ToString();
                    obj.bank_name = item["BankName"].ToString();
                    obj.branch_name = item["BranchAddress"].ToString();
                    obj.neft_code = item["IFSCCode"].ToString();
                }
            }
            return obj;

        }

        public ViewTravelRequest GetViewTravelRequest(long TravelRequestID)
        {
            TravelRequest TravelRequest = new TravelRequest();
            List<ProjectList_Travel> ProjectList = new List<ProjectList_Travel>();
            List<TravelDetail> TrvaleDetailsList = new List<TravelDetail>();
            AdvanceTravelRequest AdvanceTravelRequest = new AdvanceTravelRequest();

            ViewTravelRequest obj = new ViewTravelRequest();
            DataSet TempDataSet = new DataSet();
            TempDataSet = Common_SPU.fnGetViewTravelRequest(TravelRequestID);


            // Get main Travel Request by Table 0
            if (TempDataSet.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow item in TempDataSet.Tables[0].Rows)
                {
                    TravelRequest.TravelRequestID = Convert.ToInt32(item["ID"]);
                    TravelRequest.isTravelAgent = Convert.ToInt32(item["isTravelAgent"]);
                    TravelRequest.emp_id = Convert.ToInt32(item["emp_id"]);
                    TravelRequest.emp_type = item["emp_type"].ToString();
                    TravelRequest.req_no = item["req_no"].ToString();
                    TravelRequest.req_date = Convert.ToDateTime(item["req_date"]).ToString("dd-MMM-yyyy");
                    TravelRequest.TravelMode = item["TravelMode"].ToString();
                    TravelRequest.traveller_name = item["traveller_name"].ToString();
                    TravelRequest.generatedby = item["generatedby"].ToString();
                    TravelRequest.user_remarks = item["user_remarks"].ToString();
                    TravelRequest.purpofvisit = item["purpofvisit"].ToString();
                    TravelRequest.emailto = item["emailto"].ToString();
                    TravelRequest.emailCc = item["emailCc"].ToString();
                    TravelRequest.approved = Convert.ToInt32(item["approved"]);
                    TravelRequest.submited = Convert.ToInt32(item["submited"]);
                    TravelRequest.submiteddate = Convert.ToDateTime(item["submiteddate"]).ToString("dd-MMM-yyyy");
                    TravelRequest.DepartureDate = Convert.ToDateTime(item["DepartureDate"]).ToString("dd-MMM-yyyy");
                    TravelRequest.ReturnDate = Convert.ToDateTime(item["ReturnDate"]).ToString("dd-MMM-yyyy");
                    TravelRequest.RequestType = item["RequestType"].ToString();
                    TravelRequest.TravelType = item["TravelType"].ToString();
                    TravelRequest.TripType = item["TripType"].ToString();
                    TravelRequest.FromCity = Convert.ToInt32(item["FromCity"]);
                    TravelRequest.ToCity = Convert.ToInt32(item["ToCity"]);
                    TravelRequest.SponsorAttachID = Convert.ToInt64(item["SponsorAttachID"]);
                    TravelRequest.IsAdvanceRequest = Convert.ToInt32(item["IsAdvanceRequest"]);
                    TravelRequest.FileName = item["FileName"].ToString();
                    TravelRequest.ContentType = item["Content_Type"].ToString();
                    TravelRequest.TripSponsorName = item["TripSponsorName"].ToString();
                    TravelRequest.IsTicketToBeBooked = item["IsTicketToBeBooked"].ToString();
                    TravelRequest.IsHotelToBeBooked = item["IsHotelToBeBooked"].ToString();
                    TravelRequest.phoneno = item["phoneno"].ToString();
                    TravelRequest.emailid = item["emailid"].ToString();
                    TravelRequest.airline = item["airline"].ToString();
                    TravelRequest.prefered_seat = item["prefered_seat"].ToString();
                    TravelRequest.online_checking = item["online_checking"].ToString();
                    TravelRequest.frequentflyer_No = item["frequentflyer_No"].ToString();
                    TravelRequest.reason = item["reason"].ToString();
                    TravelRequest.isbooked = Convert.ToInt32(item["isbooked"]);
                    TravelRequest.requester_email = item["requester_email"].ToString();
                    TravelRequest.contact_no = item["contact_no"].ToString();
                    TravelRequest.isTravelAgent = Convert.ToInt32(item["isTravelAgent"]);
                    TravelRequest.amend_id = Convert.ToInt32(item["amend_id"]);
                    TravelRequest.contact_no = item["contact_no"].ToString();
                    TravelRequest.amend_remark = item["amend_remark"].ToString();
                    TravelRequest.user_remarks = item["user_remarks"].ToString();
                    TravelRequest.amendcancel_reason = item["amendcancel_reason"].ToString();
                    TravelRequest.Status = item["Status"].ToString();
                    TravelRequest.createdby = Convert.ToInt32(item["createdby"]);
                    TravelRequest.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    TravelRequest.createdat = Convert.ToDateTime(item["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    TravelRequest.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    TravelRequest.IPAddress = item["IPAddress"].ToString();
                    TravelRequest.Rfcreason = ClsCommon.EnsureString(item["RfcReason"].ToString());
                    TravelRequest.ApprovedBy = item["Approvedby"].ToString();
                    TravelRequest.app_date = Convert.ToDateTime(item["app_date"]).ToString("dd/MM/yyyy");
                    TravelRequest.Expectional = Convert.ToInt32(item["Expectional"]);
                    TravelRequest.ExpectionalApproved = Convert.ToInt32(item["ExpectApproved"]);
                    TravelRequest.HodId = Convert.ToInt32(item["HodId"]);
                    TravelRequest.AmendUserId = Convert.ToInt32(item["AmendUserId"]);
                    TravelRequest.IPAddress = item["IPAddress"].ToString();
                    TravelRequest.ExpectionalBy = item["ExpectApprovedby"].ToString();
                    TravelRequest.ExpectionalDate = Convert.ToDateTime(item["ExpectApproveddate"]).ToString("dd/MM/yyyy");
                    TravelRequest.ReasonAmendment = item["ReasonAmendment"].ToString();
                }
            }
            // Get project List  Request by Table 1
            if (TempDataSet.Tables[1].Rows.Count > 0)
            {

                ProjectList_Travel ProjectListobj = new ProjectList_Travel();
                try
                {
                    foreach (DataRow item in TempDataSet.Tables[1].Rows)
                    {
                        ProjectListobj = new ProjectList_Travel();
                        ProjectListobj.MapProjectID = Convert.ToInt32(item["ID"]);
                        ProjectListobj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                        ProjectListobj.TravelRequestID = Convert.ToInt32(item["Travel_id"]);
                        ProjectListobj.SNo = Convert.ToInt32(item["Srno"]);
                        ProjectListobj.Proj_name = item["Proj_name"].ToString();
                        ProjectListobj.proReqDet_ID = Convert.ToInt32(item["projRegDet_ID"]);
                        ProjectListobj.costcenter_Name = item["costcenter_Name"].ToString();
                        ProjectListobj.Approved = Convert.ToInt32(item["Approved"]);
                        ProjectListobj.ApprovedBy = item["ApprovedBy"].ToString();
                        ProjectListobj.ApprovedDate = Convert.ToDateTime(item["app_date"]).ToString("dd/MM/yyyy");
                        ProjectList.Add(ProjectListobj);
                    }
                }
                catch (Exception ex)
                {
                    ClsCommon.LogError("Error during GetViewTravelRequest. The query was executed :", ex.ToString(), "Problem in Table 1", "TravelModal", "MasterModal", "");
                }

            }

            // Get Travel Details  Request by Table 2
            if (TempDataSet.Tables[2].Rows.Count > 0)
            {

                TravelDetail TravelDetailobj = new TravelDetail();
                try
                {
                    foreach (DataRow item in TempDataSet.Tables[2].Rows)
                    {
                        TravelDetailobj = new TravelDetail();
                        TravelDetailobj.TravelDetailID = Convert.ToInt64(item["ID"]);
                        TravelDetailobj.travelreq_id = Convert.ToInt64(item["travelreq_id"]);
                        TravelDetailobj.req_no = item["req_no"].ToString();
                        TravelDetailobj.req_date = item["req_date"].ToString();
                        TravelDetailobj.Travel_date = Convert.ToDateTime(item["travel_date"]).ToString("dd-MMM-yyyy");
                        TravelDetailobj.Travel_modeID = Convert.ToInt32(item["Travel_modeID"]);
                        TravelDetailobj.officePersonelID = Convert.ToInt32(item["officePersonelID"]);
                        TravelDetailobj.ticketbookingID = Convert.ToInt32(item["ticketbookingID"]);
                        TravelDetailobj.hotelbookingID = Convert.ToInt32(item["hotelbookingID"]);
                        TravelDetailobj.ticketdetail = item["ticketdetail"].ToString();
                        TravelDetailobj.TicketBooking = item["TicketBooking"].ToString();
                        TravelDetailobj.justification = item["justification"].ToString();
                        TravelDetailobj.hotel_no = item["hotel_no"].ToString();
                        TravelDetailobj.fromCity = item["fromCity"].ToString();
                        TravelDetailobj.fromCityID = Convert.ToInt32(item["fromCityID"]);
                        TravelDetailobj.toCity = item["toCity"].ToString();
                        TravelDetailobj.ToCityID = Convert.ToInt32(item["ToCityID"]);
                        TravelDetailobj.TravelMode = item["TravelMode"].ToString();
                        TravelDetailobj.HotalName = item["HotelName"].ToString();
                        TravelDetailobj.ClassOfCity = item["ClassofCity"].ToString();
                        TravelDetailobj.perdiem_rate = Convert.ToDecimal(item["perdiem_rate"]);
                        TravelDetailobj.hotelbooking = item["hotelbooking"].ToString();
                        TravelDetailobj.hotelbookingID = Convert.ToInt32(item["hotelbookingID"]);
                        TrvaleDetailsList.Add(TravelDetailobj);
                    }
                }
                catch (Exception ex)
                {
                    ClsCommon.LogError("Error during GetViewTravelRequest. The query was executed :", ex.ToString(), "Problem in Table 2", "TravelModal", "MasterModal", "");
                }

            }


            // Get main Travel Request by Table 3
            if (TempDataSet.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow item in TempDataSet.Tables[3].Rows)
                {
                    AdvanceTravelRequest.AdvanceID = Convert.ToInt32(item["ID"]);
                    AdvanceTravelRequest.TravelRequestID = Convert.ToInt32(item["TravelRequestID"]);
                    AdvanceTravelRequest.req_no = item["req_no"].ToString();
                    AdvanceTravelRequest.req_date = Convert.ToDateTime(item["req_date"]).ToString("dd-MMM-yyyy");
                    AdvanceTravelRequest.traveller_name = item["traveller_name"].ToString();
                    AdvanceTravelRequest.localTravel_amt = Convert.ToDecimal(item["localTravel_amt"]);
                    AdvanceTravelRequest.Loading_amt = Convert.ToDecimal(item["Loading_amt"]);
                    AdvanceTravelRequest.OtherExpend_amt = Convert.ToDecimal(item["OtherExpend_amt"]);
                    AdvanceTravelRequest.total_amount = Convert.ToDecimal(item["total_amount"]);
                    AdvanceTravelRequest.totaladvanceroundoff = Convert.ToDecimal(item["totaladvanceroundoff"]);
                    AdvanceTravelRequest.pay_mode = item["pay_mode"].ToString();
                    AdvanceTravelRequest.OtherSection = item["OtherSection"].ToString();
                    AdvanceTravelRequest.OtherSection1 = item["OtherSection1"].ToString();
                    AdvanceTravelRequest.OtherAmount = Convert.ToDecimal(item["OtherAmount"]);
                    AdvanceTravelRequest.OtherAmount1 = Convert.ToDecimal(item["OtherAmount1"]);
                    AdvanceTravelRequest.approved = Convert.ToInt32(item["approved"]);
                    AdvanceTravelRequest.account_no = item["account_no"].ToString();

                    AdvanceTravelRequest.bank_name = item["bank_name"].ToString();
                    AdvanceTravelRequest.neft_code = item["neft_code"].ToString();
                    AdvanceTravelRequest.branch_name = item["branch_name"].ToString();
                    AdvanceTravelRequest.otherRemark = item["otherRemark"].ToString();
                    AdvanceTravelRequest.advance_amt = Convert.ToDecimal(item["advance_amt"]);
                    AdvanceTravelRequest.reason = item["reason"].ToString();
                    AdvanceTravelRequest.submited = Convert.ToInt32(item["submited"]);
                    AdvanceTravelRequest.imailHod = Convert.ToInt32(item["imailHod"]);
                    AdvanceTravelRequest.PerDiem_Rate = Convert.ToDecimal(item["PerDiem_Rate"]);

                }
            }

            obj.TravelRequest = TravelRequest;
            obj.ProjectList_Travel = ProjectList;
            obj.TravelRequestDetail = TrvaleDetailsList;
            obj.AdvanceTravelRequest = AdvanceTravelRequest;
            return obj;

        }


        public List<TravelDocuments> GetTravelDocumentsList(long TravelRequestID, long Traveldocid)
        {
            string SQL = "";
            List<TravelDocuments> List = new List<TravelDocuments>();
            TravelDocuments obj = new TravelDocuments();
            try
            {

                DataSet TempModuleDataSet = Common_SPU.fnGetTravelDocuments(TravelRequestID, Traveldocid);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new TravelDocuments();
                    obj.TravelDocID = Convert.ToInt64(item["TravelDocID"]);
                    obj.TravelRequestID = Convert.ToInt64(item["TravelRequestID"]);
                    obj.AttachmentID = Convert.ToInt64(item["AttachmentID"]);
                    obj.DocumentType = item["DocumentType"].ToString();
                    obj.filename = item["filename"].ToString();
                    obj.content_type = item["content_type"].ToString();
                    obj.TransactionDate = Convert.ToDateTime(item["TransactionDate"]).ToString("dd-MM-yyyy");
                    obj.Amount = Convert.ToDecimal(item["Amount"]);
                    obj.isbooked = Convert.ToInt32(item["isbooked"]);
                    obj.approved = Convert.ToInt32(item["approved"]);
                    obj.BookedBy = item["Bookby"].ToString();
                    obj.RefundAmount = Convert.ToDecimal(item["RefundAmount"]);
                    obj.StafId = Convert.ToInt32(item["staffid"]);
                    obj.AgentName = item["AgentName"].ToString();
                    obj.CardandBank = item["CardandBank"].ToString();
                    obj.BillDate = Convert.ToDateTime(item["BillDate"]).ToString("dd-MM-yyyy");
                    obj.BillNo = item["BillNo"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTravelDocumentsList. The query was executed :", ex.ToString(), SQL, "TravelModal", "MasterModal", "");
            }
            return List;

        }


        public List<TravelDeskList> GetTravelDeskList_Dashboard(long TravelMode)
        {
            string SQL = "";
            List<TravelDeskList> List = new List<TravelDeskList>();
            TravelDeskList obj = new TravelDeskList();
            try
            {
                long locationid = 0;
                long.TryParse(clsApplicationSetting.GetSessionValue("LocationID"), out locationid);

                DataSet TempModuleDataSet = Common_SPU.fnGetTravelDeskList_Dashboard(locationid, TravelMode);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new TravelDeskList();
                    obj.TravelDetails = item["TravelDetails"].ToString();
                    obj.TravelRequestID = Convert.ToInt64(item["ID"]);
                    obj.TravelRequestID = Convert.ToInt64(item["ID"]);
                    obj.isbooked = Convert.ToInt32(item["isbooked"]);
                    obj.TravellerName = item["Traveller_Name"].ToString();
                    obj.ReqNo = item["Req_No"].ToString();
                    obj.RequestDate = Convert.ToDateTime(item["Req_Date"]).ToString("dd-MMM-yyyy");
                    obj.PurposeOfVisit = item["PurpOfVisit"].ToString();
                    obj.Status = item["Status"].ToString();
                    obj.IsTicketRequired = Convert.ToInt32(item["IsTicketRequired"]);
                    obj.IsHotelRequired = Convert.ToInt32(item["IsHotelRequired"]);
                    obj.BookBy = item["BookBy"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTravelDeskList_Dashboard. The query was executed :", ex.ToString(), SQL, "TravelModal", "MasterModal", "");
            }
            return List;

        }

        public List<TravelDeskList> GetTravelDeskList(long iIsBooked)
        {
            string SQL = "";
            List<TravelDeskList> List = new List<TravelDeskList>();
            TravelDeskList obj = new TravelDeskList();
            try
            {
                long locationid = 0;
                long.TryParse(clsApplicationSetting.GetSessionValue("LocationID"), out locationid);

                DataSet TempModuleDataSet = Common_SPU.fnGetTravelDeskList(iIsBooked, locationid);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new TravelDeskList();
                    obj.TravelRequestID = Convert.ToInt64(item["ID"]);
                    obj.TravellerName = item["Traveller_Name"].ToString();
                    obj.ReqNo = item["Req_No"].ToString();
                    obj.RequestDate = Convert.ToDateTime(item["Req_Date"]).ToString("yyyy-MM-dd");
                    obj.PurposeOfVisit = item["PurpOfVisit"].ToString();
                    obj.Status = item["Status"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTravelDeskList. The query was executed :", ex.ToString(), SQL, "TravelModal", "MasterModal", "");
            }
            return List;

        }

        private List<TravelDocuments> TravelDocumentByDataTable(DataTable MyTable)
        {
            List<TravelDocuments> TravelDocumentsList = new List<TravelDocuments>();
            TravelDocuments TravelDocumentsobj = new TravelDocuments();
            try
            {
                foreach (DataRow item in MyTable.Rows)
                {
                    TravelDocumentsobj = new TravelDocuments();
                    TravelDocumentsobj.TravelDocID = Convert.ToInt64(item["TravelDocID"]);
                    TravelDocumentsobj.TravelRequestID = Convert.ToInt64(item["TravelRequestID"]);
                    TravelDocumentsobj.AttachmentID = Convert.ToInt64(item["AttachmentID"]);
                    TravelDocumentsobj.DocumentType = item["DocumentType"].ToString();
                    TravelDocumentsobj.filename = item["filename"].ToString();
                    TravelDocumentsobj.content_type = item["content_type"].ToString();
                    TravelDocumentsobj.TransactionDate = Convert.ToDateTime(item["TransactionDate"]).ToString("dd/MM/yyyy");
                    TravelDocumentsobj.Amount = Convert.ToDecimal(item["Amount"]);
                    TravelDocumentsobj.isbooked = Convert.ToInt32(item["isbooked"]);
                    TravelDocumentsobj.approved = Convert.ToInt32(item["approved"]);
                    TravelDocumentsobj.BookedBy = item["Bookby"].ToString();
                    TravelDocumentsList.Add(TravelDocumentsobj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during TravelDocumentByDataTable. The query was executed :", ex.ToString(), "", "TravelModal", "MasterModal", "");
            }
            return TravelDocumentsList;
        }


        private List<RequestDetail_ForDesk> RequestDetail_ByDataTable(DataTable MyTable)
        {
            List<RequestDetail_ForDesk> List = new List<RequestDetail_ForDesk>();
            RequestDetail_ForDesk TravelDetailobj = new RequestDetail_ForDesk();
            try
            {
                foreach (DataRow item in MyTable.Rows)
                {
                    TravelDetailobj = new RequestDetail_ForDesk();
                    TravelDetailobj.TravelDetailID = Convert.ToInt64(item["ID"]);
                    TravelDetailobj.travelreq_id = Convert.ToInt64(item["travelreq_id"]);
                    TravelDetailobj.ReqNo = item["req_no"].ToString();
                    TravelDetailobj.req_date = Convert.ToDateTime(item["req_date"]).ToString("dd-MMM-yyyy");
                    TravelDetailobj.Isbooked = Convert.ToInt32(item["Isbooked"]);
                    TravelDetailobj.approved = Convert.ToInt32(item["approved"]);
                    TravelDetailobj.Travel_date = Convert.ToDateTime(item["travel_date"]).ToString("dd-MMM-yyyy");
                    TravelDetailobj.Travel_modeID = Convert.ToInt32(item["Travel_modeID"]);
                    TravelDetailobj.officePersonelID = Convert.ToInt32(item["officePersonelID"]);
                    TravelDetailobj.ticketbookingID = Convert.ToInt32(item["ticketbookingID"]);
                    TravelDetailobj.hotelbookingID = Convert.ToInt32(item["hotelbookingID"]);
                    TravelDetailobj.ticketdetail = item["ticketdetail"].ToString();
                    TravelDetailobj.TicketBooking = item["TicketBooking"].ToString();
                    TravelDetailobj.justification = item["justification"].ToString();
                    TravelDetailobj.hotel_no = item["hotel_no"].ToString();
                    TravelDetailobj.fromCity = item["fromCity"].ToString();
                    TravelDetailobj.fromCityID = Convert.ToInt32(item["fromCityID"]);
                    TravelDetailobj.toCity = item["toCity"].ToString();
                    TravelDetailobj.ToCityID = Convert.ToInt32(item["ToCityID"]);
                    TravelDetailobj.Travel_mode = item["TravelMode"].ToString();
                    TravelDetailobj.hotel_no = item["hotel_no"].ToString();
                    TravelDetailobj.DepartureDate = (Convert.ToDateTime(item["DepartureDate"]).Year > 1900 ? Convert.ToDateTime(item["DepartureDate"]).ToString("dd-MMM-yyyy") : "");
                    TravelDetailobj.Telephone = item["Phone"].ToString();
                    TravelDetailobj.TripType = item["TripType"].ToString();
                    TravelDetailobj.Generatedby = item["Generatedby"].ToString();
                    TravelDetailobj.EMPName = item["EMP_Name"].ToString();
                    TravelDetailobj.ProjectName = item["ProjectName"].ToString();
                    TravelDetailobj.PassportNo = item["PassportNo"].ToString();
                    TravelDetailobj.PassportExpiryDate = (Convert.ToDateTime(item["PassportExpiryDate"]).Year > 1900 ? Convert.ToDateTime(item["PassportExpiryDate"]).ToString("dd-MMM-yyyy") : "");
                    TravelDetailobj.Mealpreference = item["MealPreferencesName"].ToString();
                    TravelDetailobj.Seatpreference = item["SeatPreferencesName"].ToString();
                    TravelDetailobj.Gender = item["Gender"].ToString();
                    TravelDetailobj.RequestType = item["RequestType"].ToString();
                    TravelDetailobj.HotalName = item["HotelName"].ToString();
                    TravelDetailobj.Dateofbirth = (Convert.ToDateTime(item["DOB"]).Year > 1900 ? Convert.ToDateTime(item["DOB"]).ToString("dd-MMM-yyyy") : "");
                    TravelDetailobj.approvedby = item["approvedby"].ToString();
                    TravelDetailobj.approved_date = Convert.ToDateTime(item["app_date"]).ToString("dd-MMM-yyyy");
                    TravelDetailobj.Itemlinebooked = Convert.ToInt32(item["Itemlinebooked"]);
                    TravelDetailobj.AmendmentType = item["AmendmentType"].ToString();
                    TravelDetailobj.EMPMail = item["email"].ToString();
                    TravelDetailobj.versionno = Convert.ToInt32(item["versionno"]);
                    TravelDetailobj.Flyername = item["Flyername"].ToString();
                    TravelDetailobj.PanNo = item["pan_no"].ToString();
                    TravelDetailobj.AdharNo = item["UdharNo"].ToString();
                    TravelDetailobj.TraveldeskName = item["TraveldeskName"].ToString();
                    TravelDetailobj.EmpWorkLocationID = Convert.ToInt32(item["EmpWorkLocationID"]);
                    TravelDetailobj.TravelDeskEmployeeId = Convert.ToInt32(item["TravelDeskEmployeeId"]);
                    TravelDetailobj.TransferTravelDeskUserId = Convert.ToInt32(item["TransferTravelDeskUserId"]);
                    TravelDetailobj.ID = Convert.ToInt32(item["ID"]);
                    TravelDetailobj.ExpectApproveddate = (Convert.ToDateTime(item["ExpectApproveddate"]).Year > 1899 ? Convert.ToDateTime(item["ExpectApproveddate"]).ToString("dd-MMM-yyyy") : "");
                    TravelDetailobj.ExpName = item["ExpName"].ToString();
                    List.Add(TravelDetailobj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during TravelDocumentByDataTable. The query was executed :", ex.ToString(), "", "TravelModal", "MasterModal", "");
            }
            return List;
        }


        public ViewTravelRequest_ForDesk GetTravelRequest_ForDesk(long TravelRequestID)
        {
            ViewTravelRequest_ForDesk obj = new ViewTravelRequest_ForDesk();
            DataSet tempDataSet = Common_SPU.fnGetTravelRequest_ForDesk(TravelRequestID);

            // Wil Give 2 Tables 1 is Travel Document and 2 is Travel Details
            obj.TravelDocumentsList = TravelDocumentByDataTable(tempDataSet.Tables[0]);
            obj.RequestDetailList = RequestDetail_ByDataTable(tempDataSet.Tables[1]);

            return obj;

        }

        public TravelExpenseCompleteRequest GetTravelExpenseCompleteRequest(long TravelRequestID)
        {
            TravelExpenseCompleteRequest mainObj = new TravelExpenseCompleteRequest();
            DataSet tempDataSet = Common_SPU.fnGetCompleteTravelRequest_Expense(TravelRequestID);
            mainObj.TravelExpense = FunctionImplementTravelExpense.TravelExpensse(tempDataSet.Tables[0]);
            mainObj.ProjectList = FunctionImplementTravelExpense.ProjectList(tempDataSet.Tables[1]);
            mainObj.ATRAVELFARE = FunctionImplementTravelExpense.ATRAVELFARE(tempDataSet.Tables[2], mainObj.TravelExpense.TRPN);
            mainObj.CTRANSPORTATION = FunctionImplementTravelExpense.CTRANSPORTATIONList(tempDataSet.Tables[4]);
            mainObj.DOTHEREXPENDITURE = FunctionImplementTravelExpense.DOTHEREXPENDITUREList(tempDataSet.Tables[5]);
            mainObj.EXPENSESUMMARY = FunctionImplementTravelExpense.EXPENSESUMMARY(tempDataSet.Tables[7]);
            mainObj.FreemealList = FunctionImplementTravelExpense.FreeMealList();
            mainObj.PerKMList = FunctionImplementTravelExpense.PerKMList();
            // above code add shailendra
            List<TripReport> objlisttrip = FunctionImplementTravelExpense.TripReportList(tempDataSet.Tables[6]).OrderBy(x => x.Date).ToList();
            List<BPERDIEM> objlistperdiem = FunctionImplementTravelExpense.BPERDIEMList(tempDataSet.Tables[3]);
            List<TripReport> TripReportList = new List<TripReport>();
            TripReport TObj = new TripReport();
            List<BPERDIEM> BPERDIEMlIST = new List<BPERDIEM>();
            BPERDIEM boBJ = new BPERDIEM();
            if (mainObj.ATRAVELFARE != null)
            {
                DateTime StartDate, EndDate;
                DateTime.TryParse(mainObj.ATRAVELFARE.Select(x => x.Date).FirstOrDefault(), out StartDate);
                DateTime.TryParse(mainObj.ATRAVELFARE.Select(x => x.Date).LastOrDefault(), out EndDate);
                string FromCity = "", ClassOfCity = "";
                double Currentperdiem_rate = 0;
                int count = 0;
                int newcount = 0;
                int Bnewcount = 0;
                DateTime dtCheckDate;
                for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
                {
                    dtCheckDate = date.AddDays(-1);
                    count++;
                    TObj = new TripReport();
                    boBJ = new BPERDIEM();
                    boBJ.Date = date.ToString("dd-MMM-yyyy");
                    TObj.Date = date.ToString("dd-MMM-yyyy");
                    TravellPerDiem obj = new TravellPerDiem();
                    obj = GetTravelPerdiemRate(date.ToString("yyyy/MM/dd"), Convert.ToString(TravelRequestID));
                    FromCity = obj.City;
                    ClassOfCity = obj.ClassCity;
                    Currentperdiem_rate = obj.PerDiemRate;
                    //if (count == 1 && date != dtCheckDate)
                    //{
                    //    if (!string.IsNullOrEmpty(mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToCity).FirstOrDefault()))
                    //    {
                    //        FromCity = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToCity).FirstOrDefault();
                    //    }
                    //    if (!string.IsNullOrEmpty(mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToClassOfCity).FirstOrDefault()))
                    //    {
                    //        ClassOfCity = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToClassOfCity).FirstOrDefault();
                    //    }

                    //    if (mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToCurrentperdiem_rate).FirstOrDefault() != 0)
                    //    {
                    //        Currentperdiem_rate = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToCurrentperdiem_rate).FirstOrDefault();
                    //    }
                    //    newcount++;
                    //}
                    ////else if (count == mainObj.ATRAVELFARE.Count) code comment by shailendra
                    //else if (EndDate == date && date != dtCheckDate)
                    //{
                    //    if (!string.IsNullOrEmpty(mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.FromCity).FirstOrDefault()))
                    //    {
                    //        FromCity = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.FromCity).FirstOrDefault();
                    //    }
                    //    if (!string.IsNullOrEmpty(mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.FromClassOfCity).FirstOrDefault()))
                    //    {
                    //        ClassOfCity = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.FromClassOfCity).FirstOrDefault();
                    //    }

                    //    if (mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.FromCurrentperdiem_rate).FirstOrDefault() != 0)
                    //    {
                    //        Currentperdiem_rate = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.FromCurrentperdiem_rate).FirstOrDefault();
                    //    }
                    //    newcount++;
                    //}
                    //else
                    //{

                    //    if (date != dtCheckDate)
                    //    {
                    //        if (!string.IsNullOrEmpty(mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToCity).FirstOrDefault()))
                    //        {

                    //        }
                    //        else
                    //        {
                    //            if (Convert.ToDateTime(BPERDIEMlIST[Bnewcount - 1].Date)== Convert.ToDateTime(mainObj.ATRAVELFARE[newcount].Date))
                    //            {
                    //                BPERDIEMlIST[Bnewcount - 1].CityName = mainObj.ATRAVELFARE[newcount].ToCity;
                    //                BPERDIEMlIST[Bnewcount - 1].ClassofCity = mainObj.ATRAVELFARE[newcount].ToClassOfCity;
                    //                BPERDIEMlIST[Bnewcount - 1].PerDiemRate = mainObj.ATRAVELFARE[newcount].ToCurrentperdiem_rate;
                    //            }
                    //            else
                    //            {

                    //            }

                    //        }


                    //       // newcount++;


                    //    }

                    //    if (!string.IsNullOrEmpty(mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToCity).FirstOrDefault()))
                    //    {
                    //        FromCity = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToCity).FirstOrDefault();
                    //        newcount++;
                    //    }
                    //    else
                    //    {
                    //        // FromCity = mainObj.ATRAVELFARE[newcount].ToCity;
                    //        FromCity = BPERDIEMlIST[Bnewcount - 1].CityName;
                    //       // newcount++;
                    //    }
                    //    if (!string.IsNullOrEmpty(mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToClassOfCity).FirstOrDefault()))
                    //    {
                    //        ClassOfCity = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToClassOfCity).FirstOrDefault();
                    //    }
                    //    else
                    //    {
                    //        // ClassOfCity = mainObj.ATRAVELFARE[newcount].ToClassOfCity;
                    //        ClassOfCity = BPERDIEMlIST[Bnewcount - 1].ClassofCity;
                    //    }

                    //    if (mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToCurrentperdiem_rate).FirstOrDefault() != 0)
                    //    {
                    //        Currentperdiem_rate = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ToCurrentperdiem_rate).FirstOrDefault();
                    //    }
                    //    else
                    //    {
                    //        // Currentperdiem_rate = mainObj.ATRAVELFARE[newcount].ToCurrentperdiem_rate;
                    //        Currentperdiem_rate = BPERDIEMlIST[Bnewcount - 1].PerDiemRate;
                    //    }
                    //}
                    //if (!string.IsNullOrEmpty(mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.FromCity).FirstOrDefault()))
                    //{
                    //    FromCity = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.FromCity).FirstOrDefault();
                    //}
                    //if (!string.IsNullOrEmpty(mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ClassOfCity).FirstOrDefault()))
                    //{
                    //    ClassOfCity = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.ClassOfCity).FirstOrDefault();
                    //}

                    //if (mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.Currentperdiem_rate).FirstOrDefault() != 0)
                    //{
                    //    Currentperdiem_rate = mainObj.ATRAVELFARE.Where(x => Convert.ToDateTime(x.Date) == date).Select(x => x.Currentperdiem_rate).FirstOrDefault();
                    //}
                    var data = GetLeaveEmpDetailsTravel(Convert.ToDateTime(boBJ.Date).ToString("yyyy-MM-dd"));
                    var checkdata = GetDataCheckEmpDayWiseStatus(Convert.ToDateTime(boBJ.Date).ToString("yyyy-MM-dd"));
                    boBJ.CityName = FromCity;
                    boBJ.ClassofCity = ClassOfCity;
                    boBJ.PerDiemRate = Currentperdiem_rate;
                    if (data.LeaveLogID > 0)
                    {
                        boBJ.LeaveYes = data.LeaveLogID;
                    }
                    boBJ.Status = checkdata.Status;
                    BPERDIEMlIST.Add(boBJ);
                    TripReportList.Add(TObj);
                    Bnewcount++;
                }

            }
            int k = 0;
            if (objlisttrip.Count > 0)
            {
                for (int i = 0; i < TripReportList.Count; i++)
                {

                    for (int j = k; j < objlisttrip.Count; j++)
                    {


                        if (TripReportList[i].Date == objlisttrip[j].Date)
                        {


                            TripReportList[i].Justification = objlisttrip[j].Justification;
                            TripReportList[i].DetailsID = objlisttrip[j].DetailsID;

                            k++;
                            break;
                        }


                    }
                }


            }
            var listFinal = TripReportList.Concat(objlisttrip);
            int m = 0;
            if (objlistperdiem.Count > 0)
            {
                for (int n = 0; n < BPERDIEMlIST.Count; n++)
                {

                    for (int p = m; p < objlistperdiem.Count; p++)
                    {

                        BPERDIEMlIST[n].Amount = objlistperdiem[p].Amount;
                        BPERDIEMlIST[n].PerDiemRate = objlistperdiem[p].PerDiemRate;
                        BPERDIEMlIST[n].FreeMealID = objlistperdiem[p].FreeMealID;
                        BPERDIEMlIST[n].ProjectID = objlistperdiem[p].ProjectID;
                        BPERDIEMlIST[n].ProjectName = objlistperdiem[p].ProjectName;
                        m++;
                        break;
                    }
                }
            }

            mainObj.BPERDIEM = BPERDIEMlIST;

            mainObj.TripReport = TripReportList;
            return mainObj;

        }


        public ViewTravelExpenseCompleteRequest GetViewTravelExpenseCompleteRequest(long TravelRequestID)
        {
            ViewTravelExpenseCompleteRequest mainObj = new ViewTravelExpenseCompleteRequest();
            DataSet tempDataSet = Common_SPU.fnGetCompleteTravelRequest_Expense(TravelRequestID);
            mainObj.TravelExpense = FunctionImplementTravelExpense.TravelExpensse(tempDataSet.Tables[0]);
            mainObj.ProjectList = FunctionImplementTravelExpense.ProjectList(tempDataSet.Tables[1]);
            mainObj.ATRAVELFARE = FunctionImplementTravelExpense.ATRAVELFARE(tempDataSet.Tables[2], mainObj.TravelExpense.TRPN);
            mainObj.BPERDIEM = FunctionImplementTravelExpense.BPERDIEMList(tempDataSet.Tables[3]);
            mainObj.CTRANSPORTATION = FunctionImplementTravelExpense.CTRANSPORTATIONList(tempDataSet.Tables[4]);
            mainObj.DOTHEREXPENDITURE = FunctionImplementTravelExpense.DOTHEREXPENDITUREList(tempDataSet.Tables[5]);
            mainObj.TripReport = FunctionImplementTravelExpense.TripReportList(tempDataSet.Tables[6]);
            return mainObj;
        }
        public TravelMode GetTravelMode()
        {
            TravelMode _list = new TravelMode();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravelMode", param: param, commandType: CommandType.StoredProcedure))
                    {
                        _list = reader.Read<TravelMode>().FirstOrDefault();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetTravelMode. The query was executed :", ex.ToString(), "spu_GetTravelMode()", "TravelModel", "TravelModel", "");

            }
            return _list;
        }
        public List<FinancePayment_Staff> GetTravelRequestAdvanceFinance()
        {
            List<FinancePayment_Staff> List = new List<FinancePayment_Staff>();
            FinancePayment_Staff obj = new FinancePayment_Staff();
            DataSet TempModuleDataSet = new DataSet();

            try
            {
                TempModuleDataSet = Common_SPU.fnGetTravelRequestAdvanceFinance();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new FinancePayment_Staff();
                    obj.Id = Convert.ToInt64(item["ID"]);
                    //  obj.trpn_no = item["trpn_no"].ToString();
                    obj.req_no = item["req_no"].ToString();
                    obj.req_Date = item["Req_Date"].ToString();
                    obj.travller_name = item["traveller_name"].ToString();
                    obj.doc_no = item["doc_no"].ToString();
                    // obj.proj_name = item["proj_name"].ToString();
                    obj.totaladvanceroundoff = Convert.ToDecimal(item["Amount"]);
                    obj.approved = Convert.ToInt64(item["approved"]);
                    obj.Paid_Amount = Convert.ToDecimal(item["Paidamount"]);
                    obj.Paid_Date = item["PaidDate"].ToString();
                    obj.reason = item["reason"].ToString();
                    obj.approvedammount = Convert.ToInt64(item["approvedammount"]);
                    obj.TravelConfirmId = Convert.ToInt64(item["TravelConfirmId"]);
                    obj.Stage = item["Stage"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetFinancePayment_Staff. The query was executed :", ex.ToString(), "fnGetTravelRequestAdvanceFinance ", "TravelModal", "TravelModal", "");
            }
            return List;

        }



        // advanced amount request
        public ViewTravelRequest GetViewTravelAdvancedRequest(long TravelRequestID)
        {
            TravelRequest TravelRequest = new TravelRequest();
            List<ProjectList_Travel> ProjectList = new List<ProjectList_Travel>();
            List<TravelDetail> TrvaleDetailsList = new List<TravelDetail>();
            AdvanceTravelRequest AdvanceTravelRequest = new AdvanceTravelRequest();

            ViewTravelRequest obj = new ViewTravelRequest();
            DataSet TempDataSet = new DataSet();
            TempDataSet = Common_SPU.fnGetViewTravelAdvancedRequest(TravelRequestID);


            // Get main Travel Request by Table 0
            if (TempDataSet.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow item in TempDataSet.Tables[0].Rows)
                {
                    TravelRequest.TravelRequestID = Convert.ToInt32(item["ID"]);
                    TravelRequest.isTravelAgent = Convert.ToInt32(item["isTravelAgent"]);
                    TravelRequest.emp_id = Convert.ToInt32(item["emp_id"]);
                    TravelRequest.emp_type = item["emp_type"].ToString();
                    TravelRequest.req_no = item["req_no"].ToString();
                    // TravelRequest.req_date = Convert.ToDateTime(item["req_date"]).ToString("dd/MMM/yyyy");
                    TravelRequest.req_date = item["req_date"].ToString();
                    TravelRequest.TravelMode = item["TravelMode"].ToString();
                    TravelRequest.traveller_name = item["traveller_name"].ToString();
                    TravelRequest.generatedby = item["generatedby"].ToString();
                    TravelRequest.user_remarks = item["user_remarks"].ToString();
                    TravelRequest.purpofvisit = item["purpofvisit"].ToString();
                    TravelRequest.emailto = item["emailto"].ToString();
                    TravelRequest.emailCc = item["emailCc"].ToString();
                    TravelRequest.approved = Convert.ToInt32(item["approved"]);
                    TravelRequest.submited = Convert.ToInt32(item["submited"]);
                    TravelRequest.submiteddate = Convert.ToDateTime(item["submiteddate"]).ToString("dd-MMM-yyyy");
                    TravelRequest.DepartureDate = Convert.ToDateTime(item["DepartureDate"]).ToString("dd-MMM-yyyy");
                    TravelRequest.ReturnDate = Convert.ToDateTime(item["ReturnDate"]).ToString("dd-MMM-yyyy");
                    TravelRequest.RequestType = item["RequestType"].ToString();
                    TravelRequest.TravelType = item["TravelType"].ToString();
                    TravelRequest.TripType = item["TripType"].ToString();
                    TravelRequest.FromCity = Convert.ToInt32(item["FromCity"]);
                    TravelRequest.ToCity = Convert.ToInt32(item["ToCity"]);
                    TravelRequest.SponsorAttachID = Convert.ToInt64(item["SponsorAttachID"]);
                    TravelRequest.IsAdvanceRequest = Convert.ToInt32(item["IsAdvanceRequest"]);
                    TravelRequest.FileName = item["FileName"].ToString();
                    TravelRequest.ContentType = item["Content_Type"].ToString();
                    TravelRequest.TripSponsorName = item["TripSponsorName"].ToString();
                    TravelRequest.IsTicketToBeBooked = item["IsTicketToBeBooked"].ToString();
                    TravelRequest.IsHotelToBeBooked = item["IsHotelToBeBooked"].ToString();
                    TravelRequest.phoneno = item["phoneno"].ToString();
                    TravelRequest.emailid = item["emailid"].ToString();
                    TravelRequest.airline = item["airline"].ToString();
                    TravelRequest.prefered_seat = item["prefered_seat"].ToString();
                    TravelRequest.online_checking = item["online_checking"].ToString();
                    // TravelRequest.frequentflyer_No = item["frequentflyer_No"].ToString();
                    TravelRequest.reason = item["reason"].ToString();
                    TravelRequest.isbooked = Convert.ToInt32(item["isbooked"]);
                    TravelRequest.requester_email = item["requester_email"].ToString();
                    TravelRequest.contact_no = item["contact_no"].ToString();
                    TravelRequest.isTravelAgent = Convert.ToInt32(item["isTravelAgent"]);
                    TravelRequest.amend_id = Convert.ToInt32(item["amend_id"]);
                    TravelRequest.contact_no = item["contact_no"].ToString();
                    TravelRequest.amend_remark = item["amend_remark"].ToString();
                    TravelRequest.user_remarks = item["user_remarks"].ToString();
                    TravelRequest.amendcancel_reason = item["amendcancel_reason"].ToString();
                    TravelRequest.Status = item["Status"].ToString();
                    TravelRequest.createdby = Convert.ToInt32(item["createdby"]);
                    TravelRequest.modifiedby = Convert.ToInt32(item["modifiedby"]);
                    TravelRequest.createdat = Convert.ToDateTime(item["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    TravelRequest.modifiedat = Convert.ToDateTime(item["modifiedat"]).ToString("dd-MMM-yy hh:mm:ss tt");
                    TravelRequest.IPAddress = item["IPAddress"].ToString();
                    TravelRequest.app_date = item["app_date"].ToString();
                    TravelRequest.ApprovedBy = item["ApprovedBy"].ToString();
                    TravelRequest.doc_no = item["doc_no"].ToString();
                }
            }
            //  Get project List Request by Table 1
            if (TempDataSet.Tables[1].Rows.Count > 0)
            {

                ProjectList_Travel ProjectListobj = new ProjectList_Travel();
                try
                {
                    foreach (DataRow item in TempDataSet.Tables[1].Rows)
                    {
                        ProjectListobj = new ProjectList_Travel();
                        ProjectListobj.MapProjectID = Convert.ToInt32(item["ID"]);
                        ProjectListobj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                        ProjectListobj.TravelRequestID = Convert.ToInt32(item["Travel_id"]);
                        ProjectListobj.SNo = Convert.ToInt32(item["Srno"]);
                        ProjectListobj.Proj_name = item["Proj_name"].ToString();
                        ProjectListobj.proReqDet_ID = Convert.ToInt32(item["projRegDet_ID"]);
                        ProjectListobj.costcenter_Name = item["costcenter_Name"].ToString();
                        ProjectListobj.DocNo = item["DocNo"].ToString();
                        ProjectList.Add(ProjectListobj);
                    }
                }
                catch (Exception ex)
                {
                    ClsCommon.LogError("Error during GetViewTravelRequest. The query was executed :", ex.ToString(), "Problem in Table 1", "TravelModal", "MasterModal", "");
                }

            }

            // Get Travel Details  Request by Table 2
            if (TempDataSet.Tables[2].Rows.Count > 0)
            {

                TravelDetail TravelDetailobj = new TravelDetail();
                try
                {
                    foreach (DataRow item in TempDataSet.Tables[2].Rows)
                    {
                        TravelDetailobj = new TravelDetail();
                        TravelDetailobj.TravelDetailID = Convert.ToInt64(item["ID"]);
                        TravelDetailobj.travelreq_id = Convert.ToInt64(item["travelreq_id"]);
                        TravelDetailobj.req_no = item["req_no"].ToString();
                        TravelDetailobj.req_date = item["req_date"].ToString();
                        TravelDetailobj.Travel_date = Convert.ToDateTime(item["travel_date"]).ToString("dd-MMM-yyyy");
                        TravelDetailobj.Travel_modeID = Convert.ToInt32(item["Travel_modeID"]);
                        TravelDetailobj.officePersonelID = Convert.ToInt32(item["officePersonelID"]);
                        TravelDetailobj.ticketbookingID = Convert.ToInt32(item["ticketbookingID"]);
                        TravelDetailobj.hotelbookingID = Convert.ToInt32(item["hotelbookingID"]);
                        TravelDetailobj.ticketdetail = item["ticketdetail"].ToString();
                        TravelDetailobj.TicketBooking = item["TicketBooking"].ToString();
                        TravelDetailobj.justification = item["justification"].ToString();
                        TravelDetailobj.hotel_no = item["hotel_no"].ToString();
                        TravelDetailobj.fromCity = item["fromCity"].ToString();
                        TravelDetailobj.fromCityID = Convert.ToInt32(item["fromCityID"]);
                        TravelDetailobj.toCity = item["toCity"].ToString();
                        TravelDetailobj.ToCityID = Convert.ToInt32(item["ToCityID"]);
                        TravelDetailobj.TravelMode = item["TravelMode"].ToString();
                        TravelDetailobj.HotalName = item["HotelName"].ToString();
                        TravelDetailobj.ClassOfCity = item["ClassofCity"].ToString();
                        TravelDetailobj.perdiem_rate = Convert.ToDecimal(item["perdiem_rate"]);
                        TravelDetailobj.hotelbooking = item["hotelbooking"].ToString();
                        TravelDetailobj.hotelbookingID = Convert.ToInt32(item["hotelbookingID"]);
                        TrvaleDetailsList.Add(TravelDetailobj);
                    }
                }
                catch (Exception ex)
                {
                    ClsCommon.LogError("Error during GetViewTravelRequest. The query was executed :", ex.ToString(), "Problem in Table 2", "TravelModal", "MasterModal", "");
                }

            }


            // Get main Travel Request by Table 3
            if (TempDataSet.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow item in TempDataSet.Tables[3].Rows)
                {
                    AdvanceTravelRequest.AdvanceID = Convert.ToInt32(item["ID"]);
                    AdvanceTravelRequest.TravelRequestID = Convert.ToInt32(item["TravelRequestID"]);
                    AdvanceTravelRequest.req_no = item["req_no"].ToString();
                    AdvanceTravelRequest.req_date = Convert.ToDateTime(item["req_date"]).ToString("dd-MMM-yyyy");
                    AdvanceTravelRequest.traveller_name = item["traveller_name"].ToString();
                    AdvanceTravelRequest.localTravel_amt = Convert.ToDecimal(item["localTravel_amt"]);
                    AdvanceTravelRequest.Loading_amt = Convert.ToDecimal(item["Loading_amt"]);
                    AdvanceTravelRequest.OtherExpend_amt = Convert.ToDecimal(item["OtherExpend_amt"]);
                    AdvanceTravelRequest.total_amount = Convert.ToDecimal(item["total_amount"]);
                    AdvanceTravelRequest.totaladvanceroundoff = Convert.ToDecimal(item["totaladvanceroundoff"]);
                    AdvanceTravelRequest.pay_mode = item["pay_mode"].ToString();
                    AdvanceTravelRequest.OtherSection = item["OtherSection"].ToString();
                    AdvanceTravelRequest.OtherSection1 = item["OtherSection1"].ToString();
                    AdvanceTravelRequest.OtherAmount = Convert.ToDecimal(item["OtherAmount"]);
                    AdvanceTravelRequest.OtherAmount1 = Convert.ToDecimal(item["OtherAmount1"]);
                    AdvanceTravelRequest.approved = Convert.ToInt32(item["approved"]);
                    AdvanceTravelRequest.account_no = item["account_no"].ToString();
                    AdvanceTravelRequest.bank_name = item["bank_name"].ToString();
                    AdvanceTravelRequest.neft_code = item["neft_code"].ToString();
                    AdvanceTravelRequest.branch_name = item["branch_name"].ToString();
                    AdvanceTravelRequest.otherRemark = item["otherRemark"].ToString();
                    AdvanceTravelRequest.advance_amt = Convert.ToDecimal(item["advance_amt"]);
                    AdvanceTravelRequest.reason = item["reason"].ToString();
                    AdvanceTravelRequest.submited = Convert.ToInt32(item["submited"]);
                    AdvanceTravelRequest.imailHod = Convert.ToInt32(item["imailHod"]);
                    AdvanceTravelRequest.PerDiem_Rate = Convert.ToDecimal(item["PerDiem_Rate"]);
                    AdvanceTravelRequest.Paidamount = Convert.ToDecimal(item["Paidamount"]);
                    AdvanceTravelRequest.PaidDate = Convert.ToDateTime(item["PaidDate"]).ToString("dd/MMM/yyyy");
                    AdvanceTravelRequest.AdvanceApprovedby = item["AdvanceApprovedBy"].ToString();

                }
            }

            obj.TravelRequest = TravelRequest;
            obj.ProjectList_Travel = ProjectList;
            obj.TravelRequestDetail = TrvaleDetailsList;
            obj.AdvanceTravelRequest = AdvanceTravelRequest;
            return obj;

        }



        public List<EmployeeList> GetTravelEmpList()
        {
            string SQL = "";
            List<EmployeeList> List = new List<EmployeeList>();
            EmployeeList obj = new EmployeeList();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTravelEmployeeList();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new EmployeeList();
                    obj.Id = Convert.ToInt64(item["ID"]);
                    obj.Name = item["Name"].ToString();

                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetEmployeeTravellist. The query was executed :", ex.ToString(), SQL, "TravelModal", "TravelModal", "");
            }
            return List;

        }

        public List<Travellocation> GetTravellocationmap(long employeeid)
        {
            string SQL = "";
            List<Travellocation> List = new List<Travellocation>();
            Travellocation obj = new Travellocation();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTravellocationlist(employeeid);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Travellocation();
                    obj.Id = Convert.ToInt64(item["TravelDeskId"]);
                    obj.LocationId = Convert.ToInt64(item["Id"]);
                    obj.LoctionName = item["Location_name"].ToString();
                    obj.EmployeeId = Convert.ToInt64(item["EmployeeId"]);
                    obj.AirTicketBooking = Convert.ToInt32(item["AirTicketBooking"]);
                    obj.LocationCount = Convert.ToInt64(item["LocationCount"]);
                    obj.AirTicketCount = Convert.ToInt64(item["AirTicketCount"]);
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during spu_GetTravelDeskLocationMap. The query was executed :", ex.ToString(), SQL, "TravelModel", "TravelModel", "");
            }
            return List;

        }


        public PostResponse SetTravelLocationMap(Travellocation model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTravelDeskLocation", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@Employeeid", SqlDbType.Int).Value = model.EmployeeId;
                        command.Parameters.Add("@LocationId", SqlDbType.Int).Value = model.LocationId;
                        command.Parameters.Add("@Airticketbooking", SqlDbType.Int).Value = model.AirTicketBooking;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ClsCommon.GetIPAddress();
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }
        public PostResponse DelTravelLocationMap(Travellocation model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_DelTravelDeskLocation", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;
                        command.Parameters.Add("@createdby", SqlDbType.VarChar).Value = clsApplicationSetting.GetSessionValue("LoginID");
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                Result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public List<Travellocation> GetTravelLocationMapList()
        {
            string SQL = "";
            List<Travellocation> List = new List<Travellocation>();
            Travellocation obj = new Travellocation();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTravelLocationList();
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new Travellocation();
                    //   obj.Id = Convert.ToInt64(item["Id"]);
                    obj.EmployeeId = ClsCommon.EnsureNumber(item["EmployeeId"].ToString());
                    obj.EmpName = ClsCommon.EnsureString(item["EmpName"].ToString());
                    obj.LoctionName = ClsCommon.EnsureString(item["LoctionName"].ToString());
                    obj.CreatedDate = ClsCommon.EnsureString(item["createdat"].ToString());
                    obj.ModifiedDate = ClsCommon.EnsureString(item["ModifiedDate"].ToString());
                    obj.IPAddress = ClsCommon.EnsureString(item["IPAddress"].ToString());
                    obj.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during fnGetTravelLocationList. The query was executed :", ex.ToString(), SQL, "TravelModal", "TravelModal", "");
            }
            return List;


        }
        public LeaveEmp GetLeaveEmpDetailsTravel(string Traveldate)
        {
            LeaveEmp obj = new LeaveEmp();
            string SQL = "";
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetEmpTravelDateLeave(Traveldate);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj.HolidayID = Convert.ToInt64(item["Id"]);


                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveEmpDetailsTravel. The query was executed :", ex.ToString(), SQL, "TravelModel", "TravelModel", "");
            }
            return obj;

        }

        public LeaveEmp GetDataCheckEmpDayWiseStatus(string Traveldate)
        {
            LeaveEmp obj = new LeaveEmp();
            string SQL = "";
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetEmpTravelDatewisestatus(Traveldate);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj.Status = Convert.ToString(item["Status"]);


                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLeaveEmpDetailsTravel. The query was executed :", ex.ToString(), SQL, "TravelModel", "TravelModel", "");
            }
            return obj;

        }

        public PostResponse GetPrintTERUser(long EmpId, long TravelRequestId)
        {
            PostResponse result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ClsCommon.connectionstring()))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetTERPrint", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = EmpId;
                        command.Parameters.Add("@TravelRequsetId", SqlDbType.Int).Value = TravelRequestId;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.ID = Convert.ToInt64(reader["RET_ID"]);
                                result.StatusCode = Convert.ToInt32(reader["COMMANDSTATUS"]);
                                result.SuccessMessage = reader["COMMANDMESSAGE"].ToString();
                                if (result.StatusCode > 0)
                                {
                                    result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    result.StatusCode = -1;
                    result.SuccessMessage = ex.Message.ToString();
                }
            }
            return result;

        }


        public TravelDeskToFinaceCard.TravelDeskToFinance GetTravelDeskToFinance()
        {
            TravelDeskToFinaceCard.TravelDeskToFinance result = new TravelDeskToFinaceCard.TravelDeskToFinance();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@UserId", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("EMPID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravelDeskToFinance", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result.listTravelDeskToFinance_Card = reader.Read<TravelDeskToFinaceCard.TravelDeskToFinance_Card>().ToList();
                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetTravelDeskToFinance()", "TravelMasterModal", "TravelMasterModal", "");

            }
            return result;
        }


        public TravelDeskToFinaceCardView GetTravelDeskToFinaceCardView(long Id, long TravelReqId)
        {
            TravelDeskToFinaceCardView result = new TravelDeskToFinaceCardView();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@TravelDocID", dbType: DbType.Int32, value: Id, direction: ParameterDirection.Input);
                    param.Add("@TravelRequestID", dbType: DbType.Int32, value: TravelReqId, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravelRequestFinancePrintDetail", param: param, commandType: CommandType.StoredProcedure))
                    {
                        result = reader.Read<TravelDeskToFinaceCardView>().FirstOrDefault();

                        if (result == null)
                        {
                            result = new TravelDeskToFinaceCardView();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.travelRequestDetailsViewsHis = reader.Read<TravelRequestDetailsView>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.travelRequestDetailsViews = reader.Read<TravelRequestDetailsView>().ToList();
                        }

                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetTravelRequestFinancePrintDetail()", "TravelModal", "TravelModal", "");

            }
            return result;
        }
        public TravelDeskToFinaceCard.TravelDeskToFinance GetTravelDeskToFinanceAgent()
        {
            TravelDeskToFinaceCard.TravelDeskToFinance result = new TravelDeskToFinaceCard.TravelDeskToFinance();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@UserId", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("EMPID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravelDeskToFinanceAgent", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result.listTravelDeskToFinance_Card = reader.Read<TravelDeskToFinaceCard.TravelDeskToFinance_Card>().ToList();
                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetTravelDeskToFinanceAgent()", "TravelMasterModal", "TravelMasterModal", "");

            }
            return result;
        }
        public TravelDeskToFinaceCard.TravelDeskToFinance GetTravelDeskToFinanceAgentPending()
        {
            TravelDeskToFinaceCard.TravelDeskToFinance result = new TravelDeskToFinaceCard.TravelDeskToFinance();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ClsCommon.connectionstring()))
                {
                    var param = new DynamicParameters();
                    param.Add("@UserId", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("LoginID"), direction: ParameterDirection.Input);
                    param.Add("@EMPID", dbType: DbType.Int32, value: clsApplicationSetting.GetSessionValue("EMPID"), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetTravelDeskToFinanceAgentPending", param: param, commandType: CommandType.StoredProcedure))
                    {
                        if (!reader.IsConsumed)
                        {
                            result.listTravelDeskToFinance_Card = reader.Read<TravelDeskToFinaceCard.TravelDeskToFinance_Card>().ToList();
                        }


                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetLead. The query was executed :", ex.ToString(), "spu_GetTravelDeskToFinanceAgent()", "TravelMasterModal", "TravelMasterModal", "");

            }
            return result;
        }
        public TravellPerDiem GetTravelPerdiemRate(string Traveldate, string req_no)
        {
            TravellPerDiem obj = new TravellPerDiem();
            string SQL = "";
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetTravelExpenseRptDetailsLogPerDiem(Traveldate, req_no);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {

                    obj.City = item["City"].ToString();
                    obj.ClassCity = item["ClassCity"].ToString();
                    obj.PerDiemRate = Convert.ToDouble(item["PerDiemRate"]);


                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetTravelPerdiemRate. The query was executed :", ex.ToString(), SQL, "TravelModel", "TravelModel", "");
            }
            return obj;

        }
        public List<MyTravelRequest> GeTravelRequest_Dashboard(long Type)
        {
            List<MyTravelRequest> List = new List<MyTravelRequest>();
            MyTravelRequest obj = new MyTravelRequest();
            DataSet TempModuleDataSet = new DataSet();

            try
            {
                TempModuleDataSet = Common_SPU.fnGetTravelRequest_Dashboard(Type);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new MyTravelRequest();
                    obj.ID = Convert.ToInt32(item["ID"]);
                    obj.TravellerName = item["Traveller_Name"].ToString();
                    obj.ReqNo = item["Req_No"].ToString();
                    obj.RequestDate = Convert.ToDateTime(item["Req_Date"]).ToString("dd-MMM-yyyy");
                    obj.Status = item["Status"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GeTravelRequest_Dashboard. The query was executed :", ex.ToString(), "fnGetMyTravelRequest_Dashboard ", "TravelModal", "TravelModal", "");
            }
            return List;

        }

    }
}