using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMaster
{
    public class BoardModal: IBoardHelper
    {
        
        public List<BoardMembersDet> GetBoardName()
        {
            string SQL = "";
            List<BoardMembersDet> List = new List<BoardMembersDet>();
            BoardMembersDet obj = new BoardMembersDet();
            try
            {
                SQL = @"select  distinct member_name,memship_no from  board where isdeleted=0 order by member_name";
                DataSet TempModuleDataSet = clsDataBaseHelper.ExecuteDataSet(SQL);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new BoardMembersDet();
                    obj.memship_no = item["memship_no"].ToString();
                    obj.member_name = item["member_name"].ToString();
                    List.Add(obj);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetBoardName. The query was executed :", ex.ToString(), SQL, "BoardModal", "BoardModal", "");
            }
            return List;

        }

        public SingleMemberDetails GetSingleMemberDetails(string memshipno)
        {
            string SQL = "";

            SingleMemberDetails obj = new SingleMemberDetails();
            try
            {
                DataSet TempModuleDataSet = Common_SPU.fnGetSingleMemberDetails(memshipno);
                foreach (DataRow item in TempModuleDataSet.Tables[0].Rows)
                {
                    obj = new SingleMemberDetails();
                    obj.membership_type = item["membership_type"].ToString();
                    obj.memship_no = item["memship_no"].ToString();
                    obj.member_name = item["member_name"].ToString();
                    obj.father_name = item["father_name"].ToString();

                    obj.panno = item["panno"].ToString();
                    obj.doj = item["doj"].ToString();
                    obj.address = item["address"].ToString().Replace("#", " ,").TrimEnd(',');
                    obj.email = item["email"].ToString().Replace("#", " ,").TrimEnd(',');
                    obj.mobile = item["mobile"].ToString().Replace("#", " ,").TrimEnd(',');
                    obj.ward_accessed = item["ward_accessed"].ToString().Replace("#", " ,").TrimEnd(',');
                    obj.source_income = item["source_income"].ToString().Replace("#", " ,").TrimEnd(',');
                    obj.currentdet_emp = item["currentdet_emp"].ToString().Replace("#", " ,").TrimEnd(',');

                    obj.title = item["title"].ToString().Replace("#", " ,").TrimEnd(',');
                    obj.dolgg = item["dolgg"].ToString().Replace("#", " ,").TrimEnd(',');
                    obj.mtitle_id = item["mtitle_id"].ToString();

                    if (item["renewal_date"] != null)
                    {
                        if (item["renewal_date"].ToString().Contains("#"))
                        {
                            foreach (string dfdf in item["renewal_date"].ToString().Split('#'))
                            {
                                if (!string.IsNullOrEmpty(dfdf))
                                {
                                    obj.renewal_date += (Convert.ToDateTime(dfdf).Year != 1899 ? Convert.ToDateTime(dfdf).ToString("dd-MMM-yyyy") + " ," : "");
                                }

                            }
                            obj.renewal_date = obj.renewal_date.TrimEnd(',');
                        }
                        else
                        {
                            obj.renewal_date = (Convert.ToDateTime(item["renewal_date"]).Year != 1899 ? Convert.ToDateTime(item["renewal_date"]).ToString("dd-MMM-yyyy") : "");
                        }

                    }

                    if (item["leaving_date"] != null)
                    {
                        if (item["leaving_date"].ToString().Contains("#"))
                        {
                            foreach (string dfdf in item["leaving_date"].ToString().Split('#'))
                            {
                                if (!string.IsNullOrEmpty(dfdf))
                                {
                                    obj.leaving_date += (Convert.ToDateTime(dfdf).Year != 1899 ? Convert.ToDateTime(dfdf).ToString("dd-MMM-yyyy") + " ," : "");
                                }

                            }
                            obj.leaving_date = obj.leaving_date.TrimEnd(',');
                        }
                        else
                        {
                            obj.leaving_date = (Convert.ToDateTime(item["leaving_date"]).Year != 1899 ? Convert.ToDateTime(item["leaving_date"]).ToString("dd-MMM-yyyy") : "");
                        }

                    }

                    if (item["doj"] != null)
                    {
                        if (item["doj"].ToString().Contains("#"))
                        {
                            foreach (string dfdf in item["doj"].ToString().Split('#'))
                            {
                                if (!string.IsNullOrEmpty(dfdf))
                                {
                                    obj.meeting_date += (Convert.ToDateTime(dfdf).Year != 1899 ? Convert.ToDateTime(dfdf).ToString("dd-MMM-yyyy") + " ," : "");
                                }

                            }
                            obj.doj = obj.doj.TrimEnd(',');
                        }
                        else
                        {
                            obj.doj = (Convert.ToDateTime(item["doj"]).Year != 1899 ? Convert.ToDateTime(item["doj"]).ToString("dd-MMM-yyyy") : "");
                        }

                    }

                    if (item["meeting_date"] != null)
                    {
                        if (item["meeting_date"].ToString().Contains("#"))
                        {
                            foreach (string dfdf in item["meeting_date"].ToString().Split('#'))
                            {
                                if (!string.IsNullOrEmpty(dfdf))
                                {
                                    obj.meeting_date += Convert.ToDateTime(dfdf).ToString("dd-MMM-yyyy") + " ,";
                                }

                            }
                            obj.meeting_date = obj.meeting_date.TrimEnd(',');
                        }
                        else
                        {
                            obj.meeting_date = Convert.ToDateTime(item["meeting_date"]).ToString("dd-MMM-yyyy");
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during GetBoardName. The query was executed :", ex.ToString(), SQL, "BoardModal", "BoardModal", "");
            }
            return obj;

        }

    }
}