using Mitr.CommonClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace Mitr.Models
{
    public class TravelExpense
    {
        public long TravelExpenseID { get; set; }
        public string req_date { get; set; }
        public string purpofvisit { get; set; }
        public long emp_id { get; set; }
        public long TRPN { get; set; }
        public string ReqNo { get; set; }
        public string NewReqNo { get; set; }
        public string TravellerName { get; set; }
        public string credit_details { get; set; }
        public string pay_mode { get; set; }
        public double amount { get; set; }
        public string remark { get; set; }
        public string summary { get; set; }
        public double travel_fare { get; set; }
        public double per_Diem { get; set; }
        public double transporation { get; set; }
        public double other_expense { get; set; }
        public double total { get; set; }
        public double advance_received { get; set; }
        public double anyOther_Credit { get; set; }
        public double net_receivable { get; set; }
        public string remark1 { get; set; }
        public int approved { get; set; }
        public int HodId { get; set; }
        public string trip_remarks { get; set; }
        public string rdbOther_status { get; set; }
        public string hod_remarks { get; set; }
        public int submited { get; set; }
        public int ExpenseApproved { get; set; }
        public double Paidamount { get; set; }
        public string Expensedistrubute_status { get; set; }
        public string HodName { get; set; }
        public double ExpenseAmount { get; set; }
        public string ExpensePaidDate { get; set; }
        public string TerApproveDate { get; set; }
        public string TerSubmitionDate { get; set; }
        public double StaffId { get; set; }
        public string emp_code { get; set; }
        public string TrApproveDate { get; set; }
        public string RejectTer { get; set; }
        public string ResubmitTer { get; set; }
        public string Terrsubmit { get; set; }
    }

    public class ProjectList
    {
        public long ProjectID { get; set; }
        public string Proj_name { get; set; }
        public long proReqDet_ID { get; set; }
        public string costcenter_Name { get; set; }
        public string LineItem { get; set; }
        public string LineItemCode { get; set; }
    }
    public class ATRAVELFARE
    {
        public long DetailsID { get; set; }
        public long TRPN { get; set; }
        public string Date { get; set; }
        public int FromCityID { get; set; }
        public int ToCityID { get; set; }

        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string TravelMode { get; set; }
        public string TravelSource { get; set; }
        public string ticket_bordcast { get; set; }
        public string ClassOfCity { get; set; }
        public double PerDiem_Amount { get; set; }
        public double Currentperdiem_rate { get; set; }
        public double CurrentPerDiemRate { get; set; }
        public int ProjectID { get; set; }
        public int Isdefault { get; set; }

        // add new properties by shailendra
        public double ToCurrentperdiem_rate { get; set; }
        public double FromCurrentperdiem_rate { get; set; }
        public string ToClassOfCity { get; set; }
        public string FromClassOfCity { get; set; }
        public List<ProjectList> ProjectList { get; set; }
    }

    public class BPERDIEM
    {
        public long DetailsID { get; set; }
        public long TRPN { get; set; }
        public string Date { get; set; }
        public string CityName { get; set; }
        public string ClassofCity { get; set; }
        public string FreeMealName { get; set; }
        public long FreeMealID { get; set; }
        public double PerDiemRate { get; set; }
        public double Amount { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public long LeaveYes { get; set; }
        public long HolidayYes { get; set; }
        public string Status { get; set; }
        public DateTime CheckDate { get; set; }

    }
    public class CTRANSPORTATION
    {
        public long DetailsID { get; set; }
        public long TRPN { get; set; }
        public string Date { get; set; }
        public string ModeOfTransport { get; set; }
        public string Details { get; set; }
        public string AttachmentNo { get; set; }
        public string TravelKM { get; set; }
        public double RateofPerKM { get; set; }
        public double Amount { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public long? TransportId { get; set; }
        public string ModeofTrnas { get; set; }
    }
    public class DOTHEREXPENDITURE
    {
        public long DetailsID { get; set; }
        public long TRPN { get; set; }
        public string Date { get; set; }
        public string DetailsofExpenditure { get; set; }
        public string AttachmentNo { get; set; }
        public double Amount { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
    }
    public class EXPENSESUMMARY
    {
        public long ID { get; set; }
        public double Travelfare { get; set; }
        public double Perdiem { get; set; }
        public double Transportion { get; set; }
        public double Otherexpesnse { get; set; }
        public long ProjectID { get; set; }
        public long TravelexpenseId { get; set; }
        public long TravelId { get; set; }
    }
    public class TripReport
    {
        public long DetailsID { get; set; }
        public string Date { get; set; }
        public string Justification { get; set; }
    }

    public class CityList
    {
        public long CityID { get; set; }
        public string CityName { get; set; }
        public string Category { get; set; }
    }


    public class ViewTravelExpenseCompleteRequest
    {
        public long TravelRequestID { get; set; }
        public long Approved { get; set; }
        public string reason { get; set; }
        public string ActionType { get; set; }
        public TravelExpense TravelExpense { get; set; }
        public List<ProjectList> ProjectList { get; set; }
        public List<ATRAVELFARE> ATRAVELFARE { get; set; }
        public List<BPERDIEM> BPERDIEM { get; set; }
        public List<CTRANSPORTATION> CTRANSPORTATION { get; set; }
        public List<DOTHEREXPENDITURE> DOTHEREXPENDITURE { get; set; }
        public List<TripReport> TripReport { get; set; }
        public List<EXPENSESUMMARY> EXPENSESUMMARY { get; set; }
    }

    public class TravelExpenseCompleteRequest
    {
        public TravelExpense TravelExpense { get; set; }
        public List<ProjectList> ProjectList { get; set; }
        public List<ATRAVELFARE> ATRAVELFARE { get; set; }
        public List<BPERDIEM> BPERDIEM { get; set; }
        public List<CTRANSPORTATION> CTRANSPORTATION { get; set; }
        public List<DOTHEREXPENDITURE> DOTHEREXPENDITURE { get; set; }
        public List<TripReport> TripReport { get; set; }
        public List<FreeMealExp> FreemealList { get; set; }
        public List<EXPENSESUMMARY> EXPENSESUMMARY { get; set; }
        public List<PerKM> PerKMList { get; set; }
    }

   
    public class FreeMealExp
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public double Percantage { get; set; }
        public long isDefault { get; set; }
    }
    public static class FunctionImplementTravelExpense
    {
        public static List<FreeMealExp> FreeMealList()
        {
            List<FreeMealExp> ProjectList = new List<FreeMealExp>();
            FreeMealExp ProjectListobj = new FreeMealExp();
            try
            {

                DataSet tempDate = Common_SPU.fnGetFreeMeal(0);
                foreach (DataRow item in tempDate.Tables[0].Select("IsActive=1"))
                {
                    ProjectListobj = new FreeMealExp();
                    ProjectListobj.ID = Convert.ToInt32(item["ID"]);
                    if (item["Freemeal"].ToString() == "No")
                    {
                        ProjectListobj.Name = item["Freemeal"].ToString() + " (" + 100 + " %)";
                    }
                    else
                    {
                        ProjectListobj.Name = item["Freemeal"].ToString() + " (" + item["Freemeal_percen"].ToString() + " %)";
                    }
                       
                    ProjectListobj.isDefault = Convert.ToInt32(item["isDefault"]);
                    ProjectListobj.Percantage = Convert.ToDouble(item["Freemeal_percen"]);
                    ProjectList.Add(ProjectListobj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during ProjectList. The query was executed :", ex.ToString(), "Problem in Table 3", "TravelModal", "MasterModal", "");
            }
            return ProjectList;
        }
        public static List<PerKM> PerKMList()
        {
            List<PerKM> ProjectList = new List<PerKM>();
            PerKM ProjectListobj = new PerKM();
            try
            {

                DataSet tempDate = Common_SPU.fnGetPerKmList(0);
                foreach (DataRow item in tempDate.Tables[0].Rows)
                {
                    ProjectListobj = new PerKM();
                    ProjectListobj.Id = Convert.ToInt32(item["ID"]);
                    ProjectListobj.VehicleType = item["vehicle_type"].ToString();
                    ProjectListobj.KMRate = Convert.ToInt64(item["km_rate"]);
                    ProjectList.Add(ProjectListobj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during ProjectList. The query was executed :", ex.ToString(), "Problem in Table 3", "TravelModal", "MasterModal", "");
            }
            return ProjectList;
        }
        public static TravelExpense TravelExpensse(DataTable mytable)
        {
            TravelExpense obj = new TravelExpense();
            try
            {
                foreach (DataRow item in mytable.Rows)
                {
                    obj.TravelExpenseID = Convert.ToInt64(item["ID"]);
                    obj.purpofvisit = item["purpofvisit"].ToString();
                    obj.req_date = Convert.ToDateTime(item["req_date"]).ToString("dd-MMM-yyyy");
                    obj.emp_id = Convert.ToInt64(item["emp_id"]);
                    obj.ReqNo = item["Req_No"].ToString();
                    obj.TRPN = Convert.ToInt64(item["TRPN_no"]);
                    obj.NewReqNo = item["reqno"].ToString();
                    obj.TravellerName = item["Traveler_Name"].ToString();
                    obj.pay_mode = item["pay_mode"].ToString();
                    obj.amount = Convert.ToDouble(item["amount"]);
                    obj.remark = item["remark"].ToString();
                    obj.summary = item["summary"].ToString();
                    obj.travel_fare = Convert.ToDouble(item["travel_fare"]);
                    obj.per_Diem = Convert.ToDouble(item["per_Diem"]);
                    obj.transporation = Convert.ToDouble(item["transporation"]);
                    obj.other_expense = Convert.ToDouble(item["other_expense"]);
                    obj.total = Convert.ToDouble(item["total"]);
                    obj.advance_received = Convert.ToDouble(item["advance_received"]);
                    obj.net_receivable = Convert.ToDouble(item["net_receivable"]);
                    obj.anyOther_Credit = Convert.ToDouble(item["anyOther_Credit"]);
                    obj.remark1 = item["remark1"].ToString();
                    obj.credit_details = item["credit_details"].ToString();
                    obj.approved = Convert.ToInt32(item["approved"]);
                    obj.HodId = Convert.ToInt32(item["HOdEMPID"]);
                    obj.ExpenseApproved= Convert.ToInt32(item["ExpenseApproved"]);
                    obj.trip_remarks = item["trip_remarks"].ToString();
                    obj.rdbOther_status = item["rdbOther_status"].ToString();
                    obj.hod_remarks = item["hod_remarks"].ToString();
                    //obj.submited = Convert.ToInt32(item["submited"]);
                    obj.submited = Convert.ToInt32(item["Tersubmited"]);
                    obj.advance_received = Convert.ToDouble(item["totaladvanceroundoff"]);
                    obj.Paidamount = Convert.ToDouble(item["Paidamount"]);
                    obj.Expensedistrubute_status = item["ExpensedistrubuteStatus"].ToString();
                    obj.ExpenseAmount = Convert.ToDouble(item["ExpenseAmount"]);
                    obj.HodName = item["HodName"].ToString();
                    obj.ExpensePaidDate = item["ExpensePaidDate"].ToString();
                    obj.TerApproveDate = item["TerApproveDate"].ToString();
                    obj.TerSubmitionDate = item["TerSubmitionDate"].ToString();
                    obj.StaffId = Convert.ToInt64(item["StaffId"]);
                    obj.emp_code = item["emp_code"].ToString();
                    obj.TrApproveDate = Convert.ToDateTime(item["app_date"]).ToString("dd/MM/yyyy");
                    obj.ResubmitTer = item["reason2"].ToString();
                    obj.Terrsubmit= item["Terrsubmit"].ToString();

                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during TravelExpensse. The query was executed :", ex.ToString(), "Problem in Table 1", "TravelModal", "MasterModal", "");
            }
            return obj;
        }

        public static List<ProjectList> ProjectList(DataTable mytable2)
        {
            List<ProjectList> ProjectList = new List<ProjectList>();
            ProjectList ProjectListobj = new ProjectList();
            try
            {
                foreach (DataRow item in mytable2.Rows)
                {
                    ProjectListobj = new ProjectList();
                    ProjectListobj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                    ProjectListobj.Proj_name = item["Proj_name"].ToString();
                    ProjectListobj.proReqDet_ID = Convert.ToInt32(item["projRegDet_ID"]);
                    ProjectListobj.costcenter_Name = item["costcenter_Name"].ToString();
                    ProjectListobj.LineItem = item["NameLineItem"].ToString();
                    ProjectListobj.LineItemCode = item["Code"].ToString();
                    ProjectList.Add(ProjectListobj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during ProjectList. The query was executed :", ex.ToString(), "Problem in Table 3", "TravelModal", "MasterModal", "");
            }
            return ProjectList;
        }

        public static List<ATRAVELFARE> ATRAVELFARE(DataTable mytable3,long TRPN)
        {
            List<ATRAVELFARE> ProjectList = new List<ATRAVELFARE>();
            ATRAVELFARE ProjectListobj = new ATRAVELFARE();
            try
            {

                foreach (DataRow item in mytable3.Rows)
                {
                    ProjectListobj = new ATRAVELFARE();
                    ProjectListobj.TRPN = TRPN;
                    ProjectListobj.DetailsID = Convert.ToInt32(item["ID"]);
                    ProjectListobj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                    //  ProjectListobj.Date = Convert.ToDateTime(item["Date"]).ToString("dd-MMM-yyyy");
                    ProjectListobj.Date = Convert.ToDateTime(item["Date"]).ToString("yyyy-MM-dd");
                    ProjectListobj.ToCityID = Convert.ToInt32(item["ToCity_ID"]);
                    ProjectListobj.FromCityID = Convert.ToInt32(item["FromCity_ID"]);
                    ProjectListobj.ClassOfCity = item["ClassCity"].ToString();
                    ProjectListobj.FromCity = item["From_City"].ToString();
                    ProjectListobj.ToCity = item["To_City"].ToString();
                    ProjectListobj.TravelMode = item["Mode_Travel"].ToString();
                    ProjectListobj.TravelSource = item["Travel_Source"].ToString();
                    ProjectListobj.ticket_bordcast = item["ticket_bordcast"].ToString();
                    ProjectListobj.PerDiem_Amount = Convert.ToInt32(item["PerDiem_Amount"]);
                    ProjectListobj.Currentperdiem_rate = Convert.ToInt32(item["Currentperdiem_rate"]);
                    ProjectListobj.Isdefault = Convert.ToInt32(item["Isdefault"]);
                    ProjectListobj.ToCurrentperdiem_rate = Convert.ToInt32(item["ToCurrentperdiem_rate"]);
                    ProjectListobj.FromCurrentperdiem_rate = Convert.ToInt32(item["FromCurrentperdiem_rate"]);
                    ProjectListobj.ToClassOfCity = item["ToClassCity"].ToString();
                    ProjectListobj.FromClassOfCity = item["FromClassCity"].ToString();
                    ProjectList.Add(ProjectListobj);
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during ATRAVELFARE. The query was executed :", ex.ToString(), "Problem in Table 3", "TravelModal", "MasterModal", "");
            }
            return ProjectList;
        }



        public static List<CTRANSPORTATION> CTRANSPORTATIONList(DataTable mytable6)
        {
            List<CTRANSPORTATION> ProjectList = new List<CTRANSPORTATION>();
            CTRANSPORTATION ProjectListobj = new CTRANSPORTATION();
            try
            {
                if (mytable6.Rows.Count > 0)
                {
                    foreach (DataRow item in mytable6.Rows)
                    {
                        ProjectListobj = new CTRANSPORTATION();
                        ProjectListobj.DetailsID = Convert.ToInt64(item["ID"]);
                        ProjectListobj.Date = Convert.ToDateTime(item["Date"]).ToString("yyyy-MM-dd");
                        ProjectListobj.Amount = Convert.ToDouble(item["Transport_Amount"]);
                        ProjectListobj.ModeOfTransport = item["Mode_Transport"].ToString();
                        ProjectListobj.Details = item["Transport_details"].ToString();
                        ProjectListobj.AttachmentNo = item["Transport_Attachno"].ToString();
                        ProjectListobj.TravelKM = item["Travel_KM"].ToString();
                        ProjectListobj.RateofPerKM = Convert.ToDouble(item["km"]);
                        ProjectListobj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                        ProjectListobj.ProjectName = item["ProjectName"].ToString();
                        ProjectListobj.TransportId= Convert.ToInt64(item["mode_transportId"]);
                        ProjectListobj.ModeofTrnas = item["ModeofTrnas"].ToString();
                        ProjectList.Add(ProjectListobj);
                    }
                }
                else
                {
                    ProjectListobj = new CTRANSPORTATION();
                    ProjectList.Add(ProjectListobj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during CTRANSPORTATIONList. The query was executed :", ex.ToString(), "Problem in Table 6", "TravelModal", "MasterModal", "");
            }
            return ProjectList;
        }

        public static List<DOTHEREXPENDITURE> DOTHEREXPENDITUREList(DataTable mytable7)
        {
            List<DOTHEREXPENDITURE> DOTHEREXPENDITUREList = new List<DOTHEREXPENDITURE>();
            DOTHEREXPENDITURE ProjectListobj = new DOTHEREXPENDITURE();
            try
            {
                if (mytable7.Rows.Count > 0)
                {
                    foreach (DataRow item in mytable7.Rows)
                    {
                        ProjectListobj = new DOTHEREXPENDITURE();
                        ProjectListobj.DetailsID = Convert.ToInt64(item["ID"]);
                        ProjectListobj.Amount = Convert.ToDouble(item["Expend_Amount"]);
                        ProjectListobj.Date = Convert.ToDateTime(item["Date"]).ToString("yyyy-MM-dd");
                        ProjectListobj.DetailsofExpenditure = item["Expend_details"].ToString();
                        ProjectListobj.AttachmentNo = item["Expend_Attachno"].ToString();
                        ProjectListobj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                        ProjectListobj.ProjectName = item["ProjectName"].ToString();
                        DOTHEREXPENDITUREList.Add(ProjectListobj);
                    }
                }
                else
                {
                    ProjectListobj = new DOTHEREXPENDITURE();
                    DOTHEREXPENDITUREList.Add(ProjectListobj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during DOTHEREXPENDITUREList. The query was executed :", ex.ToString(), "Problem in Table 7", "TravelModal", "MasterModal", "");
            }
            return DOTHEREXPENDITUREList;
        }


        public static List<TripReport> TripReportList(DataTable mytable7)
        {
            List<TripReport> DOTHEREXPENDITUREList = new List<TripReport>();
            TripReport ProjectListobj = new TripReport();
            try
            {
                if (mytable7.Rows.Count > 0)
                {
                    foreach (DataRow item in mytable7.Rows)
                    {
                        ProjectListobj = new TripReport();
                        ProjectListobj.DetailsID = Convert.ToInt64(item["ID"]);
                        ProjectListobj.Date = Convert.ToDateTime(item["Date"]).ToString("dd-MMM-yyyy");
                        ProjectListobj.Justification = item["Justification"].ToString();
                        DOTHEREXPENDITUREList.Add(ProjectListobj);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during TripReportList. The query was executed :", ex.ToString(), "Problem in Table 7", "TravelModal", "MasterModal", "");
            }
            return DOTHEREXPENDITUREList;
        }

        public static List<BPERDIEM> BPERDIEMList(DataTable mytable3)
        {
            List<BPERDIEM> BPERDIEMListList = new List<BPERDIEM>();
            BPERDIEM ProjectListobj = new BPERDIEM();
            try
            {
                if (mytable3.Rows.Count > 0)
                {
                    foreach (DataRow item in mytable3.Rows)
                    {
                        ProjectListobj = new BPERDIEM();
                        ProjectListobj.DetailsID = Convert.ToInt64(item["ID"]);
                        ProjectListobj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                        ProjectListobj.Date = Convert.ToDateTime(item["Date"]).ToString("dd-MM-yyyy");
                        ProjectListobj.CityName = item["From_City"].ToString();
                        ProjectListobj.ClassofCity = item["To_City"].ToString();
                        ProjectListobj.FreeMealID = Convert.ToInt32(item["meal_ID"]);
                        ProjectListobj.FreeMealName = item["FreeMeal_Dedtn"].ToString();
                        ProjectListobj.PerDiemRate = Convert.ToDouble(item["PerDiem_Rate"]);
                        ProjectListobj.Amount = Convert.ToDouble(item["PerDiem_Amount"]);
                        ProjectListobj.ProjectName = item["ProjectName"].ToString();
                        BPERDIEMListList.Add(ProjectListobj);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during BPERDIEMList. The query was executed :", ex.ToString(), "Problem in Table 3", "TravelModal", "MasterModal", "");
            }
            return BPERDIEMListList;
        }
        public static List<EXPENSESUMMARY> EXPENSESUMMARY(DataTable mytable8)
        {
            List<EXPENSESUMMARY> EXPENSESUMMARY = new List<EXPENSESUMMARY>();
            EXPENSESUMMARY ProjectListobj = new EXPENSESUMMARY();
            try
            {
                if (mytable8.Rows.Count > 0)
                {
                    foreach (DataRow item in mytable8.Rows)
                    {
                        ProjectListobj = new EXPENSESUMMARY();
                        ProjectListobj.ID = Convert.ToInt64(item["ID"]);
                        ProjectListobj.Travelfare = Convert.ToDouble(item["travel_fare"]);
                        ProjectListobj.Perdiem = Convert.ToDouble(item["per_Diem"]);
                        ProjectListobj.Transportion = Convert.ToDouble(item["transporation"]);
                        ProjectListobj.Otherexpesnse = Convert.ToDouble(item["other_expense"]);
                        ProjectListobj.TravelexpenseId = Convert.ToInt32(item["travelexpense_id"]);
                        ProjectListobj.TravelId = Convert.ToInt32(item["travel_id"]);
                        ProjectListobj.ProjectID = Convert.ToInt32(item["ProjectID"]);
                        EXPENSESUMMARY.Add(ProjectListobj);
                    }
                }
                else
                {
                    ProjectListobj = new EXPENSESUMMARY();
                    EXPENSESUMMARY.Add(ProjectListobj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during DOTHEREXPENDITUREList. The query was executed :", ex.ToString(), "Problem in Table 7", "TravelModal", "MasterModal", "");
            }
            return EXPENSESUMMARY;
        }
    }

}