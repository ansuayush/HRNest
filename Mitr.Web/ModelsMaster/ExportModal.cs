using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mitr.ModelsMaster
{
    public class ExportModal: IExportHelper
    {
        IActivityHelper Activity;
        IPMSHelper PMS;
        public ExportModal()
        {
            Activity = new ActivityModal();
            PMS = new PMSModal();
        }

        public void GetActiveStatusLog_Export(ActivityReport Modal)
        {
            try
            {
                DateTime tdtDate;
                DateTime.TryParse(Modal.Date, out tdtDate);
                DateTime StartDate = Convert.ToDateTime(tdtDate).AddMonths(1).AddDays(-1);
                DateTime EndDate = Convert.ToDateTime(StartDate).AddDays(1).AddMonths(-1).AddDays(-1);
                List<ConsolidateActivityStatusList> ModalView = new List<ConsolidateActivityStatusList>();
                ModalView = Activity.GetConsolidateActivityStatusList(StartDate, EndDate,"");
                var grid = new GridView();
                grid.DataSource = ModalView;
                grid.DataBind();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void TimeSheet_RPT(string sRepType, string sRepName, long EMPID, DateTime Date,string Emptype)
        {
            string strpath = "", sFileName = "";
            try
            {
                ReportDocument rd = new ReportDocument();
                ReportDocument rdTitles = new ReportDocument();
                string constring = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
                SqlConnection _sqlCon = new SqlConnection(constring);
                _sqlCon.Open();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandText = "spu_RepTimeSheet";
                _cmd.Parameters.AddWithValue("@emp_id", EMPID);
                _cmd.Parameters.AddWithValue("@month", Date.Month);
                _cmd.Parameters.AddWithValue("@year", Date.Year);
                _cmd.Parameters.AddWithValue("@Emptype", Emptype);
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Connection = _sqlCon;
                SqlDataAdapter _sda = new SqlDataAdapter(_cmd);
                System.Data.DataSet _dataSet = new System.Data.DataSet();
                _sda.Fill(_dataSet);

                string _reportPath = string.Empty;
                _reportPath = HttpContext.Current.Server.MapPath(@"/CrystalReport/TimesheetReport.rpt");
                sFileName = "TimeSheet_" + Date.ToString("MMMMyyyy");
                _sqlCon.Close();
                rd.Load(_reportPath);
                rd.SetDataSource(_dataSet.Tables[0]);
                rd.DataDefinition.FormulaFields["HeaderName"].Text = "'Time Sheet for the month of " + Date.ToString("MMMM yyyy") + "'";
                rd.DataDefinition.FormulaFields["tsmonth"].Text = "'" + Date.Month.ToString() + "'";
                strpath = HttpContext.Current.Server.MapPath("/Attachments/PDF/" + sFileName + ".pdf");
                if (System.IO.File.Exists(strpath))
                {
                    System.IO.File.Delete(strpath);
                }
                string savelocation = strpath;
                rd.ExportToDisk(ExportFormatType.PortableDocFormat, savelocation);
                rd.Close();
                rd.Dispose();
                rdTitles.Close();
                rdTitles.Dispose();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "Application/pdf";
                HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + sFileName + ".pdf");
                //Write the file directly to the HTTP content output stream.
                HttpContext.Current.Response.WriteFile(savelocation);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void LeaveAvailedReport_RPT(string sRepType, string sRepName, long EMPID, DateTime Date)
        {
            string strpath = "", sFileName = "";
            try
            {
                ReportDocument rd = new ReportDocument();
                ReportDocument rdTitles = new ReportDocument();
                string constring = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
                SqlConnection _sqlCon = new SqlConnection(constring);
                _sqlCon.Open();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandText = "spu_RepApprovedLeave";
                _cmd.Parameters.AddWithValue("@emp_id", EMPID);
                _cmd.Parameters.AddWithValue("@hod_id", EMPID);
                _cmd.Parameters.AddWithValue("@month", Date.Month);
                _cmd.Parameters.AddWithValue("@year", Date.Year);
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Connection = _sqlCon;
                SqlDataAdapter _sda = new SqlDataAdapter(_cmd);
                System.Data.DataSet _dataSet = new System.Data.DataSet();
                _sda.Fill(_dataSet);

                string _reportPath = string.Empty;
                _reportPath = HttpContext.Current.Server.MapPath(@"/CrystalReport/LeaveReport.rpt");
                sFileName = "Leave_" + Date.ToString("MMMMyyyy");
                _sqlCon.Close();
                rd.Load(_reportPath);
                rd.SetDataSource(_dataSet.Tables[0]);
                rd.DataDefinition.FormulaFields["HeaderName"].Text = "'Leave availed during the month of " + Date.ToString("MMMM yyyy") + "'";
                
                strpath = HttpContext.Current.Server.MapPath("/Attachments/PDF/" + sFileName + ".pdf");
                if (System.IO.File.Exists(strpath))
                {
                    System.IO.File.Delete(strpath);
                }
                string savelocation = strpath;
                rd.ExportToDisk(ExportFormatType.PortableDocFormat, savelocation);
                rd.Close();
                rd.Dispose();
                rdTitles.Close();
                rdTitles.Dispose();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "Application/pdf";
                HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + sFileName + ".pdf");
                //Write the file directly to the HTTP content output stream.
                HttpContext.Current.Response.WriteFile(savelocation);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetLeaveAvailedLapsed_Export(  DateTime Date)
        {
            try
            {
                int iMonth = Date.Month;
                int iYear = Date.Year;
                //DateTime dtFDate, dtTdate;
                //DateTime.TryParse("1/" + "4" + "/" + (iMonth <= 3?iYear - 1: iYear), out dtFDate);
                //DateTime.TryParse("31/" + "3" + "/" + (iMonth <= 3 ? iYear + 1 : iYear), out dtTdate);
                var grid = new GridView();
                grid.DataSource = Common_SPU.fnRepAnualLeaveStmt(Date, Date);
                grid.DataBind();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=LeaveAvailedLapsed.xls");
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetAnnualPackageDetails_Export(DateTime Date)
        {
            try
            {
                var grid = new GridView();
                grid.DataSource = CommonSpecial.prcSalaryDetails(Date);
                grid.DataBind();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=AnnualPackageDetails.xls");
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetAppraisal_RPT(long EMPID,long Fyid)
        {
            string strpath = "", sFileName = "";
            try
            {
                ReportDocument rd = new ReportDocument();
                ReportDocument rdTitles = new ReportDocument();
                string constring = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
                SqlConnection _sqlCon = new SqlConnection(constring);
                _sqlCon.Open();
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandText = "spu_GetPMS_AppraisalRpt";
                _cmd.Parameters.AddWithValue("@LoginID", EMPID);
                _cmd.Parameters.AddWithValue("@Empid", EMPID);
                _cmd.Parameters.AddWithValue("@FYid", Fyid);
                _cmd.Parameters.AddWithValue("@Doctype", "PA");
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Connection = _sqlCon;
                SqlDataAdapter _sda = new SqlDataAdapter(_cmd);
                System.Data.DataSet _dataSet = new System.Data.DataSet();
                _sda.Fill(_dataSet);

                SqlCommand _cmdAQ = new SqlCommand();
                _cmdAQ.CommandText = "spu_GetPMS_AdditionalQuestionsRpt";
                _cmdAQ.Parameters.AddWithValue("@LoginID", EMPID);
                _cmdAQ.Parameters.AddWithValue("@Empid", EMPID);
                _cmdAQ.Parameters.AddWithValue("@FYid", Fyid);
                _cmdAQ.Parameters.AddWithValue("@Doctype", "Questions");
                _cmdAQ.CommandType = System.Data.CommandType.StoredProcedure;
                _cmdAQ.Connection = _sqlCon;
                _sda = new SqlDataAdapter(_cmdAQ);
                System.Data.DataSet _dataSetAQ = new System.Data.DataSet();
                _sda.Fill(_dataSetAQ);

                SqlCommand _cmd1 = new SqlCommand();
                _cmd1.CommandText = "spu_GetPMS_AppraisalKPARpt";
                _cmd1.Parameters.AddWithValue("@LoginID", EMPID);
                _cmd1.Parameters.AddWithValue("@Empid", EMPID);
                _cmd1.Parameters.AddWithValue("@FYid", Fyid);
                _cmd1.Parameters.AddWithValue("@Doctype", "KPA");
                _cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd1.Connection = _sqlCon;
                _sda = new SqlDataAdapter(_cmd1);
                System.Data.DataSet _dataSetkpa = new System.Data.DataSet();
                _sda.Fill(_dataSetkpa);

                SqlCommand _cmd2 = new SqlCommand();
                _cmd2.CommandText = "spu_GetPMS_AppraisalTrainingRpt";
                _cmd2.Parameters.AddWithValue("@LoginID", EMPID);
                _cmd2.Parameters.AddWithValue("@Empid", EMPID);
                _cmd2.Parameters.AddWithValue("@FYid", Fyid);
                _cmd2.Parameters.AddWithValue("@Doctype", "Training");
                _cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd2.Connection = _sqlCon;
                _sda = new SqlDataAdapter(_cmd2);
                System.Data.DataSet _dataSetTraining = new System.Data.DataSet();
                _sda.Fill(_dataSetTraining);


                SqlCommand _cmd3 = new SqlCommand();
                _cmd3.CommandText = "spu_GetPMS_AppraisalQuestionsRpt";
                _cmd3.Parameters.AddWithValue("@LoginID", EMPID);
                _cmd3.Parameters.AddWithValue("@Empid", EMPID);
                _cmd3.Parameters.AddWithValue("@FYid", Fyid);
                _cmd3.Parameters.AddWithValue("@Doctype", "Questions");
                _cmd3.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd3.Connection = _sqlCon;
                _sda = new SqlDataAdapter(_cmd3);
                System.Data.DataSet _dataSetQuestions = new System.Data.DataSet();
                _sda.Fill(_dataSetQuestions);

                SqlCommand _cmd4 = new SqlCommand();
                _cmd4.CommandText = "spu_GetPMS_AppraisalRatingRpt";
                _cmd4.Parameters.AddWithValue("@LoginID", EMPID);
                _cmd4.Parameters.AddWithValue("@Empid", EMPID);
                _cmd4.Parameters.AddWithValue("@FYid", Fyid);
                _cmd4.Parameters.AddWithValue("@Doctype", "Rating");
                _cmd4.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd4.Connection = _sqlCon;
                _sda = new SqlDataAdapter(_cmd4);
                System.Data.DataSet _dataSetRating = new System.Data.DataSet();
                _sda.Fill(_dataSetRating);


                string _reportPath = string.Empty;
                _reportPath = HttpContext.Current.Server.MapPath(@"/CrystalReport/PerformanceAppraisal.rpt");
                //sFileName = "Appraisal_Report_" + _dataSet.Tables[0].Rows[0]["EMPNAME"] +"_" + _dataSet.Tables[0].Rows[0]["EMPCode"];
                //sFileName = "Appraisal_Report";
                sFileName = "Appraisal_Report_" + _dataSet.Tables[0].Rows[0]["EMPCode"];
                _sqlCon.Close();
                rd.Load(_reportPath);
                rd.SetDataSource(_dataSet.Tables[0]);
                rd.Subreports["AdditionalQuestions"].SetDataSource(_dataSetAQ.Tables[0]);
                rd.Subreports["PA_KPA"].SetDataSource(_dataSetkpa.Tables[0]);
                rd.Subreports["PA_Training"].SetDataSource(_dataSetTraining.Tables[0]);
                rd.Subreports["PA_Questions"].SetDataSource(_dataSetQuestions.Tables[0]);
                rd.Subreports["PA_Rating"].SetDataSource(_dataSetRating.Tables[0]);
                rd.Subreports["PR_Training"].SetDataSource(_dataSetTraining.Tables[0]);
                strpath = HttpContext.Current.Server.MapPath("/Attachments/PDF/" + sFileName + ".pdf");
                if (System.IO.File.Exists(strpath))
                {
                    System.IO.File.Delete(strpath);
                }
                string savelocation = strpath;
                rd.ExportToDisk(ExportFormatType.PortableDocFormat, savelocation);
                rd.Close();
                rd.Dispose();
                rdTitles.Close();
                rdTitles.Dispose();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "Application/pdf";
                HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + sFileName + ".pdf");
                //Write the file directly to the HTTP content output stream.
                HttpContext.Current.Response.WriteFile(savelocation);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return sFileName;
        }

    }
}