using Mitr.BLL;
using Mitr.CommonLib;
using Mitr.Interface;
using Mitr.Model;
using Mitr.Model.ModuleInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Areas.ModuleInfoManagment.Controllers
{
    public class ModuleInfoManagmentController : Controller
    {
        // GET: ModuleInfoManagment/ModuleInfoManagment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ModuleMaster()
        {
            return View();
        }
        public ActionResult TopicMaster()
        {
         
            return View();
        }

        public ActionResult PageMaster()
        {
           
            return View();
        }

        public ActionResult HelpCart()
        {
            List<HelpCardModel> objHelpCardModelList = new List<HelpCardModel>();
            ICommon _objICommon = new CommonBussinessBLL();
            HelpCardModel objComplianceMaster = new HelpCardModel();
            CommonMethods objCommonMethods = new CommonMethods();
            string stringTOXml = objCommonMethods.GetXMLFromObject(objComplianceMaster);
            string errorMessage = string.Empty;
            int roleId = 0;
            int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            var data = _objICommon.GetRecords(stringTOXml, Constants.GetHelpCart, roleId, userid, "GET", out errorMessage);
            if (data.data.Tables.Count > 0)
            {
                var table = data.data.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    HelpCardModel objHelpCardModel = new HelpCardModel
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        ModuleName = row["ModuleName"].ToString(),
                        TopicCount = Convert.ToInt32(row["TopicCount"])
                    };
                    objHelpCardModelList.Add(objHelpCardModel);
                }
            }
             return View(objHelpCardModelList);
        }

        public ActionResult HelpCartTopic(int ModuleId)
        {
            List<HelpCardTopicModel> objHelpCardModelList = new List<HelpCardTopicModel>();
            ICommon _objICommon = new CommonBussinessBLL();
            HelpCardTopicModel objComplianceMaster = new HelpCardTopicModel { ModuleId = ModuleId };
            CommonMethods objCommonMethods = new CommonMethods();
            string stringTOXml = objCommonMethods.GetXMLFromObject(objComplianceMaster);
            string errorMessage = string.Empty;
            int roleId = 0;
            int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            var data = _objICommon.GetRecords(stringTOXml, Constants.GetTopicHelpCart, roleId, userid, "GET", out errorMessage);
            if (data.data.Tables.Count > 0)
            {
                var table = data.data.Tables[0];
                if (table.Rows.Count > 0)
                {
                    ViewBag.ModuleName = table.Rows[0]["ModuleName"].ToString();
                    foreach (DataRow row in table.Rows)
                    {
                        HelpCardTopicModel objHelpCardModel = new HelpCardTopicModel
                        {
                            ID = Convert.ToInt32(row["ID"]),
                            TopicName = row["TopicName"].ToString(),
                            PageCount = Convert.ToInt32(row["PageCount"])
                          
                        };
                        objHelpCardModelList.Add(objHelpCardModel);
                    }
                }
            }
            ViewBag.ModuleId = ModuleId;
            return View(objHelpCardModelList);
        }

        public ActionResult HelpCartPage(int Topic_id, int? ID)
        {
            int Pageid = 0;
            int Moduleid = 0;
            List<HelpCardPAGEModel> objModuleinfoPageModelList = new List<HelpCardPAGEModel>();
            List<HelpCardTopicModel> objHelpCardTopicList = new List<HelpCardTopicModel>();
            ICommon _objICommon = new CommonBussinessBLL();
            ModuleinfoPageModel objComplianceMaster = new ModuleinfoPageModel { Topic_id = Topic_id, ID = ID.GetValueOrDefault() };
            CommonMethods objCommonMethods = new CommonMethods();
            string stringTOXml = objCommonMethods.GetXMLFromObject(objComplianceMaster);
            string errorMessage = string.Empty;
            int roleId = 0;
            int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));

            var data = _objICommon.GetRecords(stringTOXml, Constants.GetAllPageHelpCart, roleId, userid, "GET", out errorMessage);
            if (data.data.Tables.Count > 0)
            {
                var table = data.data.Tables[0];
                if (table.Rows.Count > 0)
                {
                    ViewBag.PageName = table.Rows[0]["PageName"].ToString();
                    ViewBag.TopicName = table.Rows[0]["TopicName"].ToString();
                    ViewBag.ModuleName = table.Rows[0]["ModuleName"].ToString();
                    Pageid = Convert.ToInt32(table.Rows[0]["ID"].ToString());
                    Moduleid = Convert.ToInt32(table.Rows[0]["Module_id"].ToString());
                    foreach (DataRow row in table.Rows)
                    {
                        HelpCardPAGEModel objModuleinfoPageModel = new HelpCardPAGEModel
                        {
                            PageName = row["PageName"].ToString(),
                            Video_link = row["Video_link"].ToString(),
                            Description = row["Description"].ToString(),
                            ModuleName = row["ModuleName"].ToString(),
                            TopicName = row["TopicName"].ToString(),
                            Topic_id = Convert.ToInt32(row["Topic_id"]),
                            ID = Convert.ToInt32(row["ID"]),
                            Module_id = Convert.ToInt32(row["Module_id"])
                        };
                        objModuleinfoPageModelList.Add(objModuleinfoPageModel);
                    }
                }
            }
            ViewBag.Moduleid = Moduleid;
            ViewBag.Topic_id = Topic_id;
            var pages = getallPage(Topic_id);
            ViewBag.Pages = pages;
            if(Topic_id>0 && ID==null)
            ViewBag.Isactive = "Yes";
            else ViewBag.Isactive = "No";
            return View(objModuleinfoPageModelList);
        }

        public List<ModuleinfoPageModel> getallPage(int Topic_id)
        {
            List<ModuleinfoPageModel> objHelpCardModelList = new List<ModuleinfoPageModel>();
            ICommon _objICommon = new CommonBussinessBLL();
            ModuleinfoPageModel objComplianceMaster = new ModuleinfoPageModel { Topic_id = Topic_id };
            CommonMethods objCommonMethods = new CommonMethods();
            string stringTOXml = objCommonMethods.GetXMLFromObject(objComplianceMaster);
            string errorMessage = string.Empty;
            int roleId = 0;
            int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
            var data = _objICommon.GetRecords(stringTOXml, Constants.GetPageHelpCart, roleId, userid, "GET", out errorMessage);

            if (data.data.Tables.Count > 0)
            {
                var table = data.data.Tables[0];
                if (table.Rows.Count > 0)
                {
                    ViewBag.TopicName = table.Rows[0]["TopicName"].ToString();
                    foreach (DataRow row in table.Rows)
                    {
                        ModuleinfoPageModel objHelpCardModel = new ModuleinfoPageModel
                        {
                            ID = Convert.ToInt32(row["ID"]),
                            PageName = row["PageName"].ToString(),
                            Topic_id = Convert.ToInt32(row["Topic_id"]),
                            Module_id = Convert.ToInt32(row["Module_id"]),
                            Video_link = row["Video_link"].ToString(),
                            Description = row["Description"].ToString(),
                            
                        };
                        objHelpCardModelList.Add(objHelpCardModel);
                    }
                }
            }

            return objHelpCardModelList;
        }

        public ActionResult SaveOfficePolicy()
        {

            return View();
        }

       // public ActionResult OfficePolicy()
        //{
        //    List<OfficePolicyModel> officePolicyList = new List<OfficePolicyModel>();
        //    ICommon _objICommon = new CommonBussinessBLL();
        //    HelpCardModel objComplianceMaster = new HelpCardModel();
        //    CommonMethods objCommonMethods = new CommonMethods();
        //    string stringTOXml = objCommonMethods.GetXMLFromObject(objComplianceMaster);
        //    string errorMessage = string.Empty;
        //    int roleId = 0;
        //    int userid = Convert.ToInt32(clsApplicationSetting.GetSessionValue("LoginID"));
        //    var data = _objICommon.GetRecords(stringTOXml, Constants.OfficePolicy, roleId, userid, "GET", out errorMessage);
        //    if (data.data.Tables.Count > 0)
        //    {
        //        var table = data.data.Tables[0];
        //        foreach (DataRow row in table.Rows)
        //        {
        //            var officePolicy = new OfficePolicyModel
        //            {
        //                ID = Convert.ToInt32(row["ID"]),
        //                DocumentName = row["DocumentName"].ToString(),
        //                FileUrl = row["FileUrl"].ToString(),
        //                ActualFileName = row["ActualFileName"].ToString()
        //            };
        //            officePolicyList.Add(officePolicy);
        //        }
        //    }
        //    return View(officePolicyList);
        //}




    }
}
