using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Mitr.Controllers
{
    [CheckLoginFilter]

    public class TravelController : Controller
    {
        ITravelHelper Travel;
        IBudgetHelper Budget;
        IProjectsHelper Project;
        ILeaveHelper Leave;
        public TravelController()
        {
            Project = new ProjectsModal();
            Travel = new TravelModal();
            Budget = new BudgetModel();
            Leave = new LeaveModal();
        }

        public ActionResult TravelDashboard(string src)
        {
            //Type 0 = TR amd 1 = TRE
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Type = 0;
            if (GetQueryString.Length == 3)
            {
                int.TryParse(GetQueryString[2], out Type);
            }
            ViewBag.Type = Type;
            TravelDashboard Modal = new TravelDashboard();
            Modal = Travel.GetTravelDashboard(Type);
            return View(Modal);
        }



        public ActionResult CreateRequest(string src)
        {


            ViewBag.src = src;
            ViewBag.TabIndex = 1;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            ViewBag.RequestType = GetQueryString[3];


            long TravelREQID = 0;
            int ApprovedStatus = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelREQID);
            CreateTravelRequest Modal = new CreateTravelRequest();
            Modal = Travel.CreateTravelRequest(TravelREQID);
            if (GetQueryString.Length > 4)
            {
                ViewBag.ApprovedStatus = GetQueryString[4];
                int.TryParse(ViewBag.ApprovedStatus, out ApprovedStatus);
                Modal.TravelRequest.ApprovedStatus = ApprovedStatus;
            }
            if (GetQueryString[3].ToString() == "Amendment")
            {
                // Common_SPU.fnGetTravelReq_AmendDet(TravelREQID);
            }
            ViewBag.TravelREQID = Modal.TravelRequest.TravelRequestID;
            return View(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult CreateRequest(string src, CreateTravelRequest Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            ViewBag.TabIndex = 1;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            ViewBag.RequestType = GetQueryString[3];

            long TravelREQID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelREQID);
            bool status = false;
            string Msg = "";
            long SponsorAttachID = 0;
            string ChkMultDateDepartureDat = "";
            PostResult.SuccessMessage = "Request is not Saved";

            if (Modal.ProjectList_Travel == null)
            {
                ModelState.AddModelError("TravelRequest.traveller_name", "Please add project and line item");
                PostResult.SuccessMessage = "Please add project and line item";
            }


            else if (Modal.ProjectList_Travel.Count > 3)
            {
                ModelState.AddModelError("TravelRequest.traveller_name", "You cannot add more than 3 projects");
                PostResult.SuccessMessage = "You cannot add more than 3 projects";
            }
            if (Modal.TravelRequest.TripType == "RoundTrip")
            {
                DateTime dateTimereturn = DateTime.Parse(Modal.TravelRequest.ReturnDate);
                DateTime dateTimedeptaure = DateTime.Parse(Modal.TravelRequest.DepartureDate);
                int checkDate = DateTime.Compare(dateTimereturn, dateTimedeptaure);
                int FromCity = Modal.TravelRequest.FromCity ?? 0;
                int ToCity = Modal.TravelRequest.ToCity ?? 0;

                if (FromCity == 0 || ToCity == 0)
                {
                    ModelState.AddModelError("TravelRequest.traveller_name", "City Can't be Blank");
                    PostResult.SuccessMessage = "City Can't be Blank ";
                }
                else if (string.IsNullOrEmpty(Modal.TravelRequest.DepartureDate) || string.IsNullOrEmpty(Modal.TravelRequest.ReturnDate))
                {
                    ModelState.AddModelError("TravelRequest.traveller_name", "Date Can't be Blank");
                    PostResult.SuccessMessage = "Date Can't be Blank ";
                }
                //else if (DateTime.Parse(Modal.TravelRequest.DepartureDate, CultureInfo.InvariantCulture) < DateTime.Parse(DateTime.Now.ToString(), CultureInfo.InvariantCulture) || DateTime.Parse(Modal.TravelRequest.DepartureDate.ToString("yyyy-MM-dd"), CultureInfo.InvariantCulture) == DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"), CultureInfo.InvariantCulture))

                //{
                //    ModelState.AddModelError("TravelRequest.traveller_name", "cannot request for Past dates ");
                //    PostResult.SuccessMessage = "cannot request for Past dates ";
                //}


                //else if (Convert.ToDateTime(Modal.TravelRequest.DepartureDate) < DateTime.Today || (Convert.ToDateTime(Modal.TravelRequest.ReturnDate) < DateTime.Today))
                //{
                //    ModelState.AddModelError("TravelRequest.traveller_name", "cannot request for Past dates ");
                //    PostResult.SuccessMessage = "cannot request for Past dates ";
                //}

                //else if ((DateTime.Parse(Modal.TravelRequest.ReturnDate)).Date > (DateTime.Parse(Modal.TravelRequest.DepartureDate)).Date)
                //{
                //    ModelState.AddModelError("TravelRequest.traveller_name", "
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //    Return date always greater then Departure Date ");
                //    PostResult.SuccessMessage = "Return date always greater then Departure Date ";
                //}
                else if (checkDate == -1)
                {
                    ModelState.AddModelError("TravelRequest.traveller_name", "Return date always greater then Departure Date ");
                    PostResult.SuccessMessage = "Return date always greater then Departure Date ";
                }


            }

            else if (Modal.TravelRequest.TripType == "MultiCity")
            {

                //ChkMultDateDepartureDat = (Modal.MultipleCityList.Any(x => string.IsNullOrEmpty(x.DepartureDate))).ToString();
                //DateTime dateTimereturn = DateTime.Parse(ChkMultDateDepartureDat);
                //DateTime dateTimedeptaure = DateTime.Parse(Modal.TravelRequest.DepartureDate);
                //int checkDate = DateTime.Compare(dateTimereturn, dateTimedeptaure);

                if (Modal.MultipleCityList == null || Modal.MultipleCityList.Count < 2)
                {
                    ModelState.AddModelError("TravelRequest.traveller_name", "Please Fill City Details");
                    PostResult.SuccessMessage = "Please Fill City Details For Come back";
                }
                else
                {
                    int FromCity = Modal.MultipleCityList.Select(x => x.FromCity).FirstOrDefault() ?? 0;
                    int ToCity = Modal.MultipleCityList.Select(x => x.ToCity).LastOrDefault() ?? 0;

                    if (FromCity == 0 || ToCity == 0)
                    {
                        ModelState.AddModelError("TravelRequest.traveller_name", "City Can't be Blank");
                        PostResult.SuccessMessage = "City Can't be Blank ";
                    }
                    else if (Modal.MultipleCityList.Any(x => string.IsNullOrEmpty(x.DepartureDate)))
                    {
                        ModelState.AddModelError("TravelRequest.traveller_name", "Date Can't be Blank");
                        PostResult.SuccessMessage = "Date Can't be Blank ";
                    }

                    //else if (Modal.MultipleCityList.Any(x => Convert.ToDateTime(x.DepartureDate) < DateTime.Now))
                    //{
                    //    ModelState.AddModelError("TravelRequest.traveller_name", "cannot request for Past dates ");
                    //    PostResult.SuccessMessage = "cannot request for Past dates ";
                    //}

                    else if (FromCity != ToCity)
                    {
                        ModelState.AddModelError("TravelRequest.traveller_name", "Project List Can't be Blank");
                        PostResult.SuccessMessage = "error: from and to city cannot be the same?";
                    }

                }
                int MultiCityCOunt = 0;
                for (int i = 0; i < Modal.MultipleCityList.Count; i++)
                {
                    MultiCityCOunt++;
                    if (MultiCityCOunt == 1)
                    {
                        ChkMultDateDepartureDat = Modal.MultipleCityList[i].DepartureDate;
                    }

                    if (MultiCityCOunt > 1)
                    {

                        DateTime dateTimedeptaure = DateTime.Parse(ChkMultDateDepartureDat);
                        DateTime dateTimereturn = DateTime.Parse(Modal.MultipleCityList[i].DepartureDate);
                        int checkDate = DateTime.Compare(dateTimereturn, dateTimedeptaure);
                        ChkMultDateDepartureDat = Modal.MultipleCityList[i].DepartureDate;
                        if (checkDate == -1)
                        {
                            ModelState.AddModelError("TravelRequest.traveller_name", "Return date always greater then Departure Date ");
                            PostResult.SuccessMessage = "Return date always greater then Departure Date ";
                        }
                    }

                }
            }

            if (ModelState.IsValid)
            {
                if (Command == "Add")
                {
                    if (Modal.TravelRequest.TravelType == "Sponsored" && Modal.TravelRequest.UploadFile != null)
                    {
                        var RvFile = clsApplicationSetting.ValidateFile(Modal.TravelRequest.UploadFile);
                        if (RvFile.IsValid)
                        {
                            SponsorAttachID = Common_SPU.fnSetAttachments(Modal.TravelRequest.SponsorAttachID, RvFile.FileName, RvFile.FileExt, "");
                            if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + SponsorAttachID + RvFile.FileExt)))
                            {
                                System.IO.File.Delete("~/Attachments/" + SponsorAttachID + RvFile.FileExt);
                            }
                            Modal.TravelRequest.UploadFile.SaveAs(Server.MapPath("~/Attachments/" + SponsorAttachID + RvFile.FileExt));
                        }
                        else
                        {
                            PostResult.SuccessMessage = RvFile.Message;
                            return Json(PostResult, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Modal.TravelRequest != null)
                    {
                        Modal.TravelRequest.RequestType = (ViewBag.RequestType == "D" ? "Domestic" : ViewBag.RequestType == "I" ? "International" : ViewBag.RequestType == "R" ? "Relocation" : "");
                        string EMPTYPE = "", TrvallerName = "", TripSponsorName = "";
                        int EMPID;
                        int FromCity, ToCity;
                        int IsTicketToBeBooked = 0, IsHotelToBeBooked = 0;
                        DateTime DepartureDate, ReturnDate;
                        FromCity = Modal.TravelRequest.FromCity ?? 0;
                        ToCity = Modal.TravelRequest.ToCity ?? 0;
                        DateTime.TryParse(Modal.TravelRequest.DepartureDate, out DepartureDate);
                        DateTime.TryParse(Modal.TravelRequest.ReturnDate, out ReturnDate);
                        int.TryParse(Modal.TravelRequest.traveller_name.Split('#')[0].Substring(1), out EMPID);
                        EMPTYPE = Modal.TravelRequest.traveller_name.Split('#')[1];
                        TrvallerName = Modal.TravelRequest.traveller_name.Split('#')[2];

                        if (Modal.TravelRequest.TravelType == "Sponsored")
                        {
                            IsTicketToBeBooked = (!string.IsNullOrEmpty(Modal.TravelRequest.IsTicketToBeBooked) ? 1 : 0);
                            IsHotelToBeBooked = (!string.IsNullOrEmpty(Modal.TravelRequest.IsHotelToBeBooked) ? 1 : 0);
                            TripSponsorName = Modal.TravelRequest.TripSponsorName;
                            Modal.TravelRequest.SponsorAttachID = SponsorAttachID;
                        }
                        if (Modal.TravelRequest.TripType == "MultiCity" && Modal.MultipleCityList != null)
                        {
                            FromCity = Modal.MultipleCityList.Select(x => x.FromCity).FirstOrDefault() ?? 0;
                            ToCity = Modal.MultipleCityList.Select(x => x.ToCity).LastOrDefault() ?? 0;
                            DateTime.TryParse(Modal.MultipleCityList.Select(x => x.DepartureDate).FirstOrDefault(), out DepartureDate);
                            DateTime.TryParse(Modal.MultipleCityList.Select(x => x.DepartureDate).LastOrDefault(), out ReturnDate);


                        }

                        SaveID = Common_SPU.fnSetTravelRequest(TravelREQID, Modal.TravelRequest.req_no, Modal.TravelRequest.req_date, TrvallerName, 0, 0, Modal.TravelRequest.purpofvisit,
                            "", "", EMPID, EMPTYPE, "", Modal.TravelRequest.RequestType, Modal.TravelRequest.TravelType, Modal.TravelRequest.TripType,
                            TripSponsorName, IsTicketToBeBooked, IsHotelToBeBooked, SponsorAttachID,
                            FromCity, ToCity, DepartureDate.ToString("yyyy-MM-dd"), ReturnDate.ToString("yyyy-MM-dd"), Modal.TravelRequest.user_remarks, Modal.TravelRequest.submited);
                        if (SaveID > 0)
                        {
                            // Set projects
                            int ProjectCount = 0;
                            for (int i = 0; i < Modal.ProjectList_Travel.Count; i++)
                            {
                                ProjectCount++;
                                Common_SPU.fnSetMapProjCostcenter(Modal.ProjectList_Travel[i].Proj_name, Modal.ProjectList_Travel[i].costcenter_Name,
                                    SaveID, ProjectCount, Modal.ProjectList_Travel[i].proReqDet_ID);

                                // add new code LineItem set Isactivate=0 by shailendra 12/11/2022
                                clsDataBaseHelper.ExecuteNonQuery("update BudgetSublineActivityDet set IsActive=0 where Id=" + Modal.ProjectList_Travel[i].proReqDet_ID + "");
                            }
                            clsDataBaseHelper.ExecuteNonQuery("update map_project_costCenter set isdeleted=1 where isdeleted=0 and travel_id=" + SaveID + " and srno>" + ProjectCount + "");


                            // Set Travel Details
                            if (Modal.TravelRequest.TripType == "RoundTrip")
                            {
                                Common_SPU.fnSetTravelReq_Det(SaveID, 1, 0, DepartureDate.ToString("yyyy-MM-dd"), FromCity, ToCity, "", 0, 0, 0, 0, 0, "", "", "", "", 0, "");
                                Common_SPU.fnSetTravelReq_Det(SaveID, 2, 0, ReturnDate.ToString("yyyy-MM-dd"), ToCity, FromCity, "", 0, 0, 0, 0, 0, "", "", "", "", 0, "");

                                clsDataBaseHelper.ExecuteNonQuery("update travelrequest_det set isdeleted=1 where isdeleted=0 and travelreq_id=" + SaveID + " and srno>2");

                            }

                            if (Modal.TravelRequest.TripType == "MultiCity")
                            {
                                if (Modal.MultipleCityList != null)
                                {
                                    int MultiCityCOunt = 0;
                                    for (int i = 0; i < Modal.MultipleCityList.Count; i++)
                                    {
                                        MultiCityCOunt++;
                                        FromCity = Modal.MultipleCityList[i].FromCity ?? 0;
                                        ToCity = Modal.MultipleCityList[i].ToCity ?? 0;
                                        Common_SPU.fnSetTravelReq_Det(SaveID, MultiCityCOunt, 0,
                                            Modal.MultipleCityList[i].DepartureDate, FromCity, ToCity, "", 0, 0, 0, 0, 0, "", "", "", "", 0, "");
                                    }
                                    clsDataBaseHelper.ExecuteNonQuery("update travelrequest_det set isdeleted=1 where isdeleted=0 and travelreq_id=" + SaveID + " and srno>" + MultiCityCOunt);

                                }
                            }

                            PostResult.RedirectURL = Url.Action("CreateItinerary", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Travel/CreateItinerary*" + SaveID + "*" + ViewBag.RequestType) });
                            status = true;
                            Msg = "Request Updated Successfully";
                        }
                        else
                        {

                            status = false;

                        }

                    }


                }
                else if (Command == "Amendment")
                {


                    if (Modal.TravelRequest.AmendmentType == "Purpose")
                    {
                        if (Modal.TravelRequest.ReasonAmendment == null && Modal.TravelRequest.ApprovedStatus > 0)
                        {
                            status = false;
                            // Msg = "Please Enter Reason Amendment ";
                        }
                        else
                        {
                            Common_SPU.fnSetPurposeamendment(Convert.ToInt64(Modal.TravelRequest.req_no), Modal.TravelRequest.purpofvisit, Modal.TravelRequest.AmendmentType, "Purpose", 0, 0, "", "", Modal.TravelRequest.ReasonAmendment, Modal.TravelRequest.ApprovedStatus);
                            PostResult.RedirectURL = Url.Action("TravelDashboard", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Travel/TravelDashboard*") });
                            status = true;
                            Msg = "Request Amendment Successfully";
                        }

                    }
                    else if (Modal.TravelRequest.AmendmentType == "Travel Date")
                    {
                        if (Modal.TravelRequest.ReasonAmendment == null && Modal.TravelRequest.ApprovedStatus > 0)
                        {
                            status = false;
                            // Msg = "Please Enter Reason Amendment ";
                        }
                        else
                        {
                            Common_SPU.fnSetPurposeamendment(Convert.ToInt64(Modal.TravelRequest.req_no), "", Modal.TravelRequest.AmendmentType, "Travel Date", 0, 0, Modal.MultipleCityList.Select(x => x.DepartureDate).FirstOrDefault(), Modal.MultipleCityList.Select(x => x.DepartureDate).LastOrDefault(), Modal.TravelRequest.ReasonAmendment, Modal.TravelRequest.ApprovedStatus);
                            for (int i = 0; i < Modal.MultipleCityList.Count; i++)
                            {
                                Common_SPU.fnSetTravelReqDetPurposeAmendment(Convert.ToInt64(Modal.TravelRequest.req_no), Modal.MultipleCityList[i].ID, 0, 0, Modal.MultipleCityList[i].DepartureDate, "TravelDate");
                            }
                            PostResult.RedirectURL = Url.Action("TravelDashboard", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Travel/TravelDashboard*") });
                            status = true;
                            Msg = "Request Amendment Successfully";
                        }

                    }
                    else if (Modal.TravelRequest.AmendmentType == "Travel City")
                    {
                        if (Modal.TravelRequest.ReasonAmendment == null && Modal.TravelRequest.ApprovedStatus > 0)
                        {
                            status = false;
                            // Msg = "Please Enter Reason Amendment ";
                        }
                        else
                        {
                            Common_SPU.fnSetPurposeamendment(Convert.ToInt64(Modal.TravelRequest.req_no), "", Modal.TravelRequest.AmendmentType, "Travel City", Convert.ToInt32(Modal.MultipleCityList.Select(x => x.FromCity).FirstOrDefault()), Convert.ToInt32(Modal.MultipleCityList.Select(x => x.ToCity).FirstOrDefault()), "", "", Modal.TravelRequest.ReasonAmendment, Modal.TravelRequest.ApprovedStatus);
                            for (int i = 0; i < Modal.MultipleCityList.Count; i++)
                            {
                                Common_SPU.fnSetTravelReqDetPurposeAmendment(Convert.ToInt64(Modal.TravelRequest.req_no), Modal.MultipleCityList[i].ID, Convert.ToInt64(Modal.MultipleCityList[i].FromCity), Convert.ToInt64(Modal.MultipleCityList[i].ToCity), "", "TravelCity");
                            }
                            PostResult.RedirectURL = Url.Action("CreateItinerary", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Travel/CreateItinerary*" + Convert.ToInt64(Modal.TravelRequest.req_no) + "*" + ViewBag.RequestType) });
                            status = true;
                            Msg = "Request Amendment Successfully";
                        }

                    }
                    else if (Modal.TravelRequest.AmendmentType == "All")
                    {
                        if (Modal.TravelRequest.ReasonAmendment == null && Modal.TravelRequest.ApprovedStatus > 0)
                        {
                            status = false;
                            //Msg = "Please Enter Reason Amendment ";
                        }
                        else
                        {

                            Common_SPU.fnSetPurposeamendment(Convert.ToInt64(Modal.TravelRequest.req_no), Modal.TravelRequest.purpofvisit, Modal.TravelRequest.AmendmentType, "All", Convert.ToInt32(Modal.MultipleCityList.Select(x => x.FromCity).FirstOrDefault()), Convert.ToInt32(Modal.MultipleCityList.Select(x => x.ToCity).FirstOrDefault()), Modal.MultipleCityList.Select(x => x.DepartureDate).FirstOrDefault(), Modal.MultipleCityList.Select(x => x.DepartureDate).LastOrDefault(), Modal.TravelRequest.ReasonAmendment, Modal.TravelRequest.ApprovedStatus);
                            for (int i = 0; i < Modal.MultipleCityList.Count; i++)
                            {
                                Common_SPU.fnSetTravelReqDetPurposeAmendment(Convert.ToInt64(Modal.TravelRequest.req_no), Modal.MultipleCityList[i].ID, Convert.ToInt64(Modal.MultipleCityList[i].FromCity), Convert.ToInt64(Modal.MultipleCityList[i].ToCity), Modal.MultipleCityList[i].DepartureDate, "All");
                            }

                            PostResult.RedirectURL = Url.Action("CreateItinerary", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Travel/CreateItinerary*" + Convert.ToInt64(Modal.TravelRequest.req_no) + "*" + ViewBag.RequestType) });
                            status = true;
                            Msg = "Request Amendment Successfully";
                        }

                    }
                    else if (Modal.TravelRequest.ApprovedStatus == 0)
                    {
                        if (Modal.TravelRequest.ReasonAmendment == null && Modal.TravelRequest.ApprovedStatus > 0)
                        {
                            status = false;
                            //Msg = "Please Enter Reason Amendment ";
                        }
                        else
                        {
                            if (Modal.TravelRequest.ApprovedStatus == 0)
                            {
                                Modal.TravelRequest.ReasonAmendment = "";
                            }

                            Common_SPU.fnSetPurposeamendment(Convert.ToInt64(Modal.TravelRequest.req_no), Modal.TravelRequest.purpofvisit, "", "All", Convert.ToInt32(Modal.MultipleCityList.Select(x => x.FromCity).FirstOrDefault()), Convert.ToInt32(Modal.MultipleCityList.Select(x => x.ToCity).FirstOrDefault()), Modal.MultipleCityList.Select(x => x.DepartureDate).FirstOrDefault(), Modal.MultipleCityList.Select(x => x.DepartureDate).LastOrDefault(), Modal.TravelRequest.ReasonAmendment, Modal.TravelRequest.ApprovedStatus);
                            for (int i = 0; i < Modal.MultipleCityList.Count; i++)
                            {
                                Common_SPU.fnSetTravelReqDetPurposeAmendment(Convert.ToInt64(Modal.TravelRequest.req_no), Modal.MultipleCityList[i].ID, Convert.ToInt64(Modal.MultipleCityList[i].FromCity), Convert.ToInt64(Modal.MultipleCityList[i].ToCity), Modal.MultipleCityList[i].DepartureDate, "All");
                            }

                            PostResult.RedirectURL = Url.Action("CreateItinerary", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Travel/CreateItinerary*" + Convert.ToInt64(Modal.TravelRequest.req_no) + "*" + ViewBag.RequestType) });
                            status = true;
                            Msg = "Request Amendment Successfully";
                        }

                    }
                    // amendment mail fire
                    Common_SPU.fnCreateMail_TravelAmendment(TravelREQID);

                }
                if (status)
                {
                    //TempData["Success"] = "Y";
                    //TempData["SuccessMsg"] = Msg;
                    PostResult.Status = true;
                    PostResult.SuccessMessage = Msg;
                }
                else
                {
                    if (Command == "Add")
                    {
                        ModelState.AddModelError("TravelRequest.traveller_name", "TravelRequest.traveller_name");
                        PostResult.SuccessMessage = "This date allready travelling";
                    }
                    else
                    {
                        if (Modal.TravelRequest.AmendmentType == null || Modal.TravelRequest.AmendmentType == "Select")
                        {
                            ModelState.AddModelError("TravelRequest.traveller_name", "Return date always greater then Departure Date ");
                            PostResult.SuccessMessage = "Please choose the appropriate type of amendment   ";
                        }
                        else if (Modal.TravelRequest.ReasonAmendment == null && Modal.TravelRequest.ApprovedStatus > 0)
                        {
                            ModelState.AddModelError("TravelRequest.traveller_name", "Return date always greater then Departure Date ");
                            PostResult.SuccessMessage = "Please share reason this amendment";
                        }


                    }

                }

            }

            ModelState.Clear();
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult CreateItinerary(string src)
        {
            ViewBag.src = src;
            ViewBag.TabIndex = 2;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            ViewBag.RequestType = GetQueryString[3];
            long TravelREQID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelREQID);
            ViewBag.TravelREQID = TravelREQID;
            CreateItineraryDetails Modal = new CreateItineraryDetails();
            Modal = Travel.CreateItineraryDetails(TravelREQID);
            return View(Modal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult CreateItinerary(string src, CreateItineraryDetails Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            ViewBag.TabIndex = 1;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            ViewBag.RequestType = GetQueryString[3];
            long TravelREQID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelREQID);
            bool status = true;
            string Msg = "";
            PostResult.SuccessMessage = "Travel Request not Saved";
            if (Modal.OtherAmount1 > 0)
            {
                if (Modal.OtherSection1 == null)
                {
                    ModelState.AddModelError("TravelRequest.traveller_name", "Please Enter ammount description");
                    PostResult.SuccessMessage = "Please Enter ammount description";
                    status = false;
                }

            }
            if (Modal.OtherAmount2 > 0)
            {
                if (Modal.OtherSection2 == null)
                {
                    ModelState.AddModelError("TravelRequest.traveller_name", "Please Enter ammount description");
                    PostResult.SuccessMessage = "Please Enter ammount description";
                    status = false;
                }

            }

            if (status == true)
            {
                if (ModelState.IsValid)
                {
                    if (Command == "Add")
                    {
                        if (Modal.TravelDetailList != null)
                        {
                            int count = 0, hotelbookingID = 0;
                            string hotel_no = "";
                            string OtherHotel = "";
                            long listTravelCount = Modal.TravelDetailList.Count;
                            double PerdiemAmountuser = 0;
                            string ClassOfCityUser = "";
                            for (int i = 0; i < Modal.TravelDetailList.Count; i++)
                            {
                                count++;
                                if (Modal.TravelDetailList[i].ticketbookingID == 1)
                                {

                                    // New code update a/c to perdiem rate

                                    if (string.IsNullOrEmpty(Modal.TravelDetailList[i].ticketdetail))
                                    {
                                        PostResult.SuccessMessage = "Please share justification for booking of travel tickets not as per office policy/ rules";

                                        return Json(PostResult, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (string.IsNullOrEmpty(Modal.TravelDetailList[i].justification))
                                    {
                                        PostResult.SuccessMessage = "Justification can't blank when you choosed Ticket as per rule as No";

                                        return Json(PostResult, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    Modal.TravelDetailList[i].ticketdetail = "";
                                    Modal.TravelDetailList[i].justification = "";
                                }

                                if (Modal.TravelDetailList[i].hotelbookingID == 1 && string.IsNullOrEmpty(Modal.TravelDetailList[i].OtherHotel))
                                {
                                    PostResult.SuccessMessage = "Please share preferred hotel name ";

                                    return Json(PostResult, JsonRequestBehavior.AllowGet);
                                }


                                if (Modal.TravelDetailList[i].OtherHotel != null)
                                {
                                    OtherHotel = Modal.TravelDetailList[i].OtherHotel;
                                }

                                if (count == 1)
                                {
                                    PerdiemAmountuser = Convert.ToDouble(Modal.TravelDetailList[i].Toperdiem_rate) * 0.75;
                                    ClassOfCityUser = Modal.TravelDetailList[i].ToClassCity;
                                }
                                else if (count == Modal.TravelDetailList.Count)
                                {
                                    PerdiemAmountuser = Convert.ToDouble(Modal.TravelDetailList[i].Fromperdiem_rate) * 0.75;
                                    ClassOfCityUser = Modal.TravelDetailList[i].FromClassCity;
                                }
                                else
                                {
                                    PerdiemAmountuser = Convert.ToDouble(Modal.TravelDetailList[i].Toperdiem_rate);
                                    ClassOfCityUser = Modal.TravelDetailList[i].ToClassCity;
                                }
                                SaveID = Common_SPU.fnSetTravelReq_Det(TravelREQID, count, 0, Modal.TravelDetailList[i].Travel_date,
                                   Modal.TravelDetailList[i].fromCityID, Modal.TravelDetailList[i].ToCityID, Modal.TravelDetailList[i].ToClassCity, Modal.TravelDetailList[i].Toperdiem_rate,
                                   Modal.TravelDetailList[i].Travel_modeID, Modal.TravelDetailList[i].officePersonelID,
                                   Modal.TravelDetailList[i].ticketbookingID, hotelbookingID,
                                   Modal.TravelDetailList[i].ticketdetail, Modal.TravelDetailList[i].justification,
                                  Modal.TravelDetailList[i].hotelbookingID.ToString(), OtherHotel, Convert.ToDecimal(PerdiemAmountuser), ClassOfCityUser);



                                // End New code update a/c to perdiem rate


                                //    if (string.IsNullOrEmpty(Modal.TravelDetailList[i].ticketdetail))
                                //    {
                                //        PostResult.SuccessMessage = "Ticket Details can't blank when you choosed Ticket as per rule as No";

                                //        return Json(PostResult, JsonRequestBehavior.AllowGet);
                                //    }
                                //    else if (string.IsNullOrEmpty(Modal.TravelDetailList[i].justification))
                                //    {
                                //        PostResult.SuccessMessage = "Justification can't blank when you choosed Ticket as per rule as No";

                                //        return Json(PostResult, JsonRequestBehavior.AllowGet);
                                //    }
                                //}
                                //else
                                //{
                                //    Modal.TravelDetailList[i].ticketdetail = "";
                                //    Modal.TravelDetailList[i].justification = "";
                                //}

                                //if (Modal.TravelDetailList[i].hotelbookingID == 1 && string.IsNullOrEmpty(Modal.TravelDetailList[i].OtherHotel))
                                //{
                                //    PostResult.SuccessMessage = "Other Hotel Name can't blank.";

                                //    return Json(PostResult, JsonRequestBehavior.AllowGet);
                                //}
                                ////if ( string.IsNullOrEmpty(Modal.TravelDetailList[i].OtherHotel))
                                ////{
                                ////    PostResult.SuccessMessage = "Other Hotel Name can't blank.";

                                ////    return Json(PostResult, JsonRequestBehavior.AllowGet);
                                ////}

                                ////else
                                ////{
                                ////    hotel_no = Modal.TravelDetailList[i].hotelbookingID.ToString();
                                ////}

                                //if (Modal.TravelDetailList[i].OtherHotel != null)
                                //{
                                //    OtherHotel = Modal.TravelDetailList[i].OtherHotel;
                                //}
                                //// upadte perdiem ammount code...
                                //PerdiemAmount = Convert.ToInt32(Modal.PerdiemAmount) / listTravelCount;

                                //// end code


                                //SaveID = Common_SPU.fnSetTravelReq_Det(TravelREQID, count, 0, Modal.TravelDetailList[i].Travel_date,
                                //   Modal.TravelDetailList[i].fromCityID, Modal.TravelDetailList[i].ToCityID, Modal.TravelDetailList[i].ToClassCity, Modal.TravelDetailList[i].Toperdiem_rate,
                                //   Modal.TravelDetailList[i].Travel_modeID, Modal.TravelDetailList[i].officePersonelID,
                                //   Modal.TravelDetailList[i].ticketbookingID, hotelbookingID,
                                //   Modal.TravelDetailList[i].ticketdetail, Modal.TravelDetailList[i].justification,
                                //  Modal.TravelDetailList[i].hotelbookingID.ToString(), OtherHotel);
                                ////SaveID = Common_SPU.fnSetTravelReq_Det(TravelREQID, count, 0, Modal.TravelDetailList[i].Travel_date,
                                ////    Modal.TravelDetailList[i].fromCityID, Modal.TravelDetailList[i].ToCityID, Modal.TravelDetailList[i].ToClassCity, Modal.TravelDetailList[i].Toperdiem_rate,
                                ////    Modal.TravelDetailList[i].Travel_modeID, Modal.TravelDetailList[i].officePersonelID,
                                ////    Modal.TravelDetailList[i].ticketbookingID, hotelbookingID,
                                ////    Modal.TravelDetailList[i].ticketdetail, Modal.TravelDetailList[i].justification,
                                ////    hotel_no, OtherHotel);
                            }
                        }

                        // Set Advance Request
                        int IsAdvanceRequest = 0;
                        if (!string.IsNullOrEmpty(Modal.AdvanceRequired))
                        {
                            IsAdvanceRequest = 1;
                        }
                        decimal TotalAmount = 0;
                        decimal.TryParse((Modal.TransportationAmount + Modal.PerdiemAmount + Modal.HotelAmount + Modal.OtherAmount1 + Modal.OtherAmount2).ToString(), out TotalAmount);

                        SaveID = Common_SPU.fnSetTravelAdvanReq(TravelREQID, IsAdvanceRequest, Modal.TransportationAmount, Modal.HotelAmount, TotalAmount, Modal.AdvanceAmount, Modal.OtherSection1, Modal.OtherAmount1,
                             Modal.OtherSection2, Modal.OtherAmount2, Modal.pay_mode, Modal.account_no, Modal.bank_name, Modal.neft_code, Modal.branch_name);
                        // fire mail for travel request create
                        Common_SPU.fnCreateMail_Travel(TravelREQID, "Submit");
                        if (SaveID > 0)
                        {
                            Common_SPU.fnSetTravelPrincipalapproval(TravelREQID);
                            Common_SPU.fnSetTravelDeskModeAir(TravelREQID);
                            status = true;
                            Msg = "Travel request has been submitted successfully";
                            PostResult.RedirectURL = Url.Action("TravelDashboard", new { src = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Travel/TravelDashboard*" + SaveID + "*" + ViewBag.RequestType) });
                        }
                    }

                    if (status)
                    {
                        TempData["Success"] = "Y";
                        TempData["SuccessMsg"] = Msg;
                        PostResult.Status = true;
                        PostResult.SuccessMessage = Msg;
                    }

                }
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _ViewTravelRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            ViewBag.status = GetQueryString[3];
            long TravelREQID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelREQID);
            ViewTravelRequest Modal = new ViewTravelRequest();
            Modal = Travel.GetViewTravelRequest(TravelREQID);
            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _ViewTravelRequest(string src, ViewTravelRequest Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            long TravelREQID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelREQID);
            PostResult.SuccessMessage = "Travel Request Action is not saved";
            if (ModelState.IsValid)
            {
                string ActionType = "";
                int Approved = 0;
                if (Command == "Approved")
                {
                    ActionType = Command;
                    Approved = 1;
                }
                else
                {
                    if (Modal.ActionType == "RFC")
                    {
                        ActionType = Modal.ActionType;

                    }
                    else if (Modal.ActionType == "RFCApproved")
                    {
                        ActionType = Modal.ActionType;
                    }
                    else if (Modal.ActionType == "ExpectApproved")
                    {
                        ActionType = Modal.ActionType;
                    }
                    else
                    {
                        ActionType = Modal.ActionType;
                        Approved = 2;
                    }

                }
                 SaveID = Common_SPU.fnSetTravelRequestAction(TravelREQID, Approved, Modal.reason, ActionType);
                // fire mail approved and reject and resubmit
                Common_SPU.fnCreateMail_Travel(TravelREQID, ActionType);
                if (SaveID > 0)
                {
                    TempData["Success"] = "Y";
                    if (Modal.ActionType == "ExpectApproved")
                    {
                        TempData["SuccessMsg"] = "Request for exceptional approval approved successfully";
                        PostResult.SuccessMessage = "Request for exceptional approval approved successfully";

                    }
                    else if (Modal.ActionType == "ExpectReject")
                    {
                        TempData["SuccessMsg"] = "Request for exceptional approval rejected successfully";
                        PostResult.SuccessMessage = "Request for exceptional approval rejected successfully";
                    }
                    else if (Modal.ActionType == "Approved")
                    {
                        TempData["SuccessMsg"] = "Travel  request approved successfully";
                        PostResult.SuccessMessage = "Travel  request approved successfully";
                    }
                    else if (Modal.ActionType == "Reject")
                    {
                        TempData["SuccessMsg"] = "Travel request rejected successfully";
                        PostResult.SuccessMessage = "Travel request rejected successfully";
                    }
                    else if (Modal.ActionType == "Resubmit")
                    {
                        TempData["SuccessMsg"] = "Travel request resubmitted successfully";
                        PostResult.SuccessMessage = "Travel request resubmitted successfully";
                    }
                    else
                    {
                        TempData["SuccessMsg"] = "Travel Request " + ActionType + " Successfully";
                        PostResult.SuccessMessage = "Travel Request " + ActionType + " Successfully";
                    }

                    PostResult.Status = true;

                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }




        public ActionResult TravelDeskDashboard(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TravelMode mode = new TravelMode();                                                       
            mode = Travel.GetTravelMode();
            return View(mode);                                                                                                                                                                                                                                                                                                     
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                                                                                           
        public ActionResult _ViewTravelDeskList_Dashboard(string src)            
        {                                
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<TravelDeskList> List = new List<TravelDeskList>();

            List = Travel.GetTravelDeskList_Dashboard(Convert.ToInt64(GetQueryString[2]));
            return PartialView(List);
        }
        public ActionResult _ViewTravelDocuments(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            if (GetQueryString.Length == 4)
            {
                ViewBag.DeleteDisabled = "Yes";
            }
            long TravelRequestID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelRequestID);
            List<TravelDocuments> List = new List<TravelDocuments>();
            List = Travel.GetTravelDocumentsList(TravelRequestID, 0);
            return PartialView(List);
        }

        public ActionResult ViewTravelAmendment(string src)
        {

            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            ViewBag.ApprovedStatus = GetQueryString[3];
            long TravelRequestID = 0;
            long ApprovedStatus = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelRequestID);
            long.TryParse(ViewBag.ApprovedStatus, out ApprovedStatus);
            PostResult.RedirectURL = "/Travel/CreateRequest?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Travel/CreateRequest*" + TravelRequestID + "*" + "Amendment" + "*" + ApprovedStatus);
            Common_SPU.fnGetTravelReq_AmendDet(TravelRequestID);
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _ViewTravelRequest_ForDesk(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            long TravelRequestID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelRequestID);
            ViewTravelRequest_ForDesk Modal = new ViewTravelRequest_ForDesk();
            Modal = Travel.GetTravelRequest_ForDesk(TravelRequestID);

            return PartialView(Modal);
        }

        public ActionResult _AddTravelDocument(string src)
        {
              ViewBag.src = src;
              string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
              ViewBag.GetQueryString = GetQueryString;
              ViewBag.MenuID = GetQueryString[0];
              ViewBag.TravelRequestID = GetQueryString[2];
              long TravelRequestID = 0;
              ViewBag.Amendversno = Convert.ToInt32(GetQueryString[3]);
              long.TryParse(ViewBag.TravelRequestID, out TravelRequestID);
              TravelDocuments Modal = new TravelDocuments();
              Modal.TravelRequestID = TravelRequestID;
              ViewBag.emplist = Budget.GetBudgetSettingEmpList();
              if (GetQueryString.Length > 4 && GetQueryString[4] != "")
              {
                Modal.TravelDocID = Convert.ToInt64(GetQueryString[4]);
                var item = Travel.GetTravelDocumentsList(TravelRequestID, Modal.TravelDocID).FirstOrDefault();
                Modal.TravelDocID = item.TravelDocID;
                Modal.BookedBy = item.BookedBy;
                Modal.StafId = item.StafId;
                Modal.AgentName = item.AgentName;
                Modal.DocumentType = item.DocumentType;
                Modal.Amount = item.Amount;
                Modal.TransactionDate = Convert.ToDateTime(item.TransactionDate).ToString("yyyy-MM-dd");
                Modal.filename = item.filename;
                Modal.AttachmentID = item.AttachmentID;
                Modal.RefundAmount = item.RefundAmount;
                Modal.CardandBank = item.CardandBank;
                if (item.BillDate == "31-12-1899")
                {
                    Modal.BillDate = null;
                }
                else
                {
                    Modal.BillDate = Convert.ToDateTime(item.BillDate).ToString("yyyy-MM-dd");
                }

                Modal.BillNo = item.BillNo;
              }
             return PartialView(Modal);
        }


        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _AddTravelDocument(TravelDocuments Modal, string src, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            bool status = true;
          
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            long TravelRequestID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelRequestID);
            PostResult.ID = TravelRequestID;
            if (Modal.BookedBy == "Travel Desk/Office Card")
            {
                if (Modal.Amount > 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                    PostResult.SuccessMessage = "Please fill amount";
                }
            }
            if (Modal.BookedBy == "Travel Desk/Other Card")
            {
                if (!string.IsNullOrEmpty(Modal.CardandBank))
                {
                    status = true;
                }
                else
                {
                    status = false;
                    PostResult.SuccessMessage = "Share Details(Card No & Bank Name) is mandatory";
                }
            }

            if (Modal.TravelDocID > 0)
            {
                if (Modal.BookedBy == "Travel Agent")
                {
                    if (Modal.Amount > 0)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                        PostResult.SuccessMessage = "Please fill amount";
                       
                    }
                    if (!string.IsNullOrEmpty(Modal.BillNo))
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                        PostResult.SuccessMessage = "Billing no. is mandatory";
                    }
                    if (!string.IsNullOrEmpty(Modal.BillDate))
                    {
                        status = true;
                        if (!string.IsNullOrEmpty(Modal.BillNo))
                        {
                            status = true;
                            if (Modal.Amount > 0)
                            {
                                status = true;
                            }
                            else
                            {
                                status = false;
                                PostResult.SuccessMessage = "Please fill amount";

                            }
                        }
                        else
                        {
                            status = false;
                            PostResult.SuccessMessage = "Billing no. is mandatory";
                        }
                    }
                    else
                    {
                        status = false;
                        PostResult.SuccessMessage = "Billing date is mandatory";
                    }
                }
                //if (Modal.BookedBy == "Travel Agent")
                //{
                //    if (!string.IsNullOrEmpty(Modal.BillDate))
                //    {
                //        status = true;
                //    }
                //    else
                //    {
                //        status = false;
                //        PostResult.SuccessMessage = "Billing date is mandatory";
                //    }
                //}
                //if (Modal.BookedBy == "Travel Agent")
                //{
                //    if (Modal.Amount > 0)
                //    {
                //        status = true;
                //    }
                //    else
                //    {
                //        status = false;
                //        PostResult.SuccessMessage = "Please fill amount";
                //    }
                //}
            }
            if (Modal.TravelDocID == 0)
            {
                if (Modal.UploadAttachment == null)
                {
                    status = false;
                    PostResult.SuccessMessage = "Attachment Can't Blank";
                }
            }

            if (Modal.StafId == null)
            {
                Modal.StafId = 0;
            }
            if (Modal.AgentName == null)
            {
                Modal.AgentName = "";
            }
            if (string.IsNullOrEmpty(Modal.CardandBank))
            {
                Modal.CardandBank = "";
            }
            if (string.IsNullOrEmpty(Modal.BillNo))
            {
                Modal.BillNo = "";
            }
            if (string.IsNullOrEmpty(Modal.BillDate))
            {
                Modal.BillDate = "1899-12-31";
            }
            try
            {
                if (status == true)
                {
                    if (ModelState.IsValid)
                    {

                        if (Modal.UploadAttachment != null)
                        {
                            var RvFile = clsApplicationSetting.ValidateFile(Modal.UploadAttachment);
                            if (RvFile.IsValid)
                            {
                                Modal.AttachmentID = Common_SPU.fnSetAttachments(Modal.AttachmentID, RvFile.FileName, RvFile.FileExt, "");
                                if (System.IO.File.Exists(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt)))
                                {
                                    System.IO.File.Delete("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt);
                                }
                                Modal.UploadAttachment.SaveAs(Server.MapPath("~/Attachments/" + Modal.AttachmentID + RvFile.FileExt));
                            }
                            else
                            {
                                PostResult.SuccessMessage = RvFile.Message;
                                return Json(PostResult, JsonRequestBehavior.AllowGet);

                            }
                        }

                        SaveID = Common_SPU.fnSetTravelDocuments(TravelRequestID, Modal.DocumentType, Modal.AttachmentID, Modal.TransactionDate, Modal.Amount, Modal.RefundAmount, Modal.BookedBy, Modal.AgentName, Convert.ToInt64(Modal.StafId), Modal.TravelDocID, Modal.CardandBank, Modal.BillNo, Modal.BillDate);
                        if (SaveID > 0)
                        {
                            PostResult.Status = true;
                            PostResult.SuccessMessage = "Document(s) saved successfully";

                        }

                    }
                    else
                    {
                        PostResult.SuccessMessage = "there is some problem, Please try again...";
                    }
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during AddTravelDocuments. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterController", "MasterController", "");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }


        public ActionResult FillExpenseReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            long TravelRequestID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelRequestID);
            int Type = 0;
            if (GetQueryString.Length == 4)
            {
                int.TryParse(GetQueryString[3], out Type);
            }
            ViewBag.Type = Type;
            ViewBag.TravelREQID = TravelRequestID;
            if (TravelRequestID != 0)
            {
                // code comment by shailendra 17/12/2022
                Common_SPU.fnSetTravelRequestsDetailsIntoExpense(TravelRequestID);
            }
            TravelExpenseCompleteRequest Modal = new TravelExpenseCompleteRequest();
            Modal = Travel.GetTravelExpenseCompleteRequest(TravelRequestID);
            
            ViewBag.projectlist = Modal.ProjectList.ToList();
            ViewBag.projectcout = Modal.ProjectList.Count();
            return View(Modal);
        }

        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult ATravelFare(List<ATRAVELFARE> Modal, string src, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            bool status = false;
            long TravelRequestID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelRequestID);
            int Type = 0;
            if (GetQueryString.Length == 4)
            {
                int.TryParse(GetQueryString[3], out Type);
            }
            ViewBag.Type = Type;
            ViewBag.TravelREQID = TravelRequestID;
            try
            {

                if (ModelState.IsValid)
                {
                    if (Command == "ASave")
                    {
                        for (int i = 0; i < Modal.Count; i++)
                        {
                            if (Modal[i].Date != null)
                            {
                                SaveID = Common_SPU.fnSetTEDetTravelFare(Modal[i].TRPN, Modal[i].Date,
                              Modal[i].TravelMode, Modal[i].ticket_bordcast, Modal[i].PerDiem_Amount, (i + 1),
                              "Travel Fare", Modal[i].TravelSource, Modal[i].FromCityID, Modal[i].ToCityID,
                              Modal[i].Isdefault, Modal[i].ProjectID);
                                status = true;
                            }
                            else
                            {
                                status = false;
                            }

                        }
                    }

                    if (SaveID > 0 && status == true)
                    {
                        PostResult.Status = true;
                        PostResult.SuccessMessage = "Travel fare details saved successfully";
                    }
                    else if (status == false)
                    {
                        PostResult.SuccessMessage = "A - Travel Fare Please select date";
                    }
                    else
                    {
                        PostResult.SuccessMessage = "there is some problem, Please try again..";
                    }

                }
                else
                {
                    PostResult.SuccessMessage = "there is some problem, Please try again..";
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during AddTravelDocuments. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterController", "MasterController", "");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult FillExpenseReport(TravelExpenseCompleteRequest Modal, string src, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            long TravelRequestID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelRequestID);
            int Type = 0;
            if (GetQueryString.Length == 4)
            {
                int.TryParse(GetQueryString[3], out Type);
            }
            ViewBag.Type = Type;
            try
            {

                if (ModelState.IsValid)
                {
                    long TravelExpenseID = Modal.TravelExpense.TRPN;
                    if (Modal.TravelExpense.rdbOther_status == "No")
                    {
                        Modal.TravelExpense.credit_details = "";
                        Modal.TravelExpense.amount = 0;
                        Modal.TravelExpense.remark = "";
                        Modal.TravelExpense.pay_mode = "By Cash";
                    }

                    SaveID = Common_SPU.fnSetTravelExpenRpt_Log(TravelExpenseID, TravelExpenseID, Modal.TravelExpense.ReqNo, Modal.TravelExpense.TravellerName, "", Modal.TravelExpense.credit_details, Modal.TravelExpense.pay_mode, Modal.TravelExpense.amount, Modal.TravelExpense.remark, "",
                         Modal.TravelExpense.travel_fare, Modal.TravelExpense.per_Diem, Modal.TravelExpense.transporation, Modal.TravelExpense.other_expense, Modal.TravelExpense.total, Modal.TravelExpense.advance_received, Modal.TravelExpense.anyOther_Credit, Modal.TravelExpense.net_receivable, Modal.TravelExpense.remark1, Command, Modal.TravelExpense.rdbOther_status, Modal.TravelExpense.Expensedistrubute_status);
                    TravelExpenseID = SaveID;
                    //Set B Per Diem
                    if (Modal.BPERDIEM != null)
                    {
                        int BCount = 0;
                        for (int i = 0; i < Modal.BPERDIEM.Count; i++)
                        {

                            if (!string.IsNullOrEmpty(Modal.BPERDIEM[i].Date))
                            {
                                BCount++;
                                SaveID = Common_SPU.fnSetTEDetPerDiem(TravelExpenseID, Modal.BPERDIEM[i].Date,
                                    Modal.BPERDIEM[i].CityName, Modal.BPERDIEM[i].ClassofCity, 0,
                                    Modal.BPERDIEM[i].PerDiemRate, Modal.BPERDIEM[i].Amount,
                                    BCount, "Per Diem", Modal.BPERDIEM[i].FreeMealID, Modal.BPERDIEM[i].ProjectID);
                            }
                        }
                        Common_SPU.fnDelTravelExpenseRptDet(TravelExpenseID, BCount, "Per Diem");
                    }
                    //Set  C TRANSPORTATION
                    if (Modal.CTRANSPORTATION != null)
                    {
                        int CCount = 0;
                        for (int i = 0; i < Modal.CTRANSPORTATION.Count; i++)
                        {

                            if (!string.IsNullOrEmpty(Modal.CTRANSPORTATION[i].Date))
                            {
                                CCount++;
                                SaveID = Common_SPU.fnSetTETransportion(TravelExpenseID, Modal.CTRANSPORTATION[i].Date, Modal.CTRANSPORTATION[i].ModeOfTransport, Modal.CTRANSPORTATION[i].Details, Modal.CTRANSPORTATION[i].AttachmentNo, Modal.CTRANSPORTATION[i].Amount, CCount, "Transport", Modal.CTRANSPORTATION[i].TravelKM, Modal.CTRANSPORTATION[i].RateofPerKM, Modal.CTRANSPORTATION[i].ProjectID, Convert.ToInt64(Modal.CTRANSPORTATION[i].TransportId));
                            }
                        }
                        Common_SPU.fnDelTravelExpenseRptDet(TravelExpenseID, CCount, "Transport");
                    }

                    //Set D OTHER EXPENDITURE
                    if (Modal.DOTHEREXPENDITURE != null)
                    {
                        int DCount = 0;
                        for (int i = 0; i < Modal.DOTHEREXPENDITURE.Count; i++)
                        {

                            if (!string.IsNullOrEmpty(Modal.DOTHEREXPENDITURE[i].Date))
                            {
                                DCount++;
                                SaveID = Common_SPU.fnSetTEOtherExpense(TravelExpenseID, Modal.DOTHEREXPENDITURE[i].Date, Modal.DOTHEREXPENDITURE[i].DetailsofExpenditure, Modal.DOTHEREXPENDITURE[i].AttachmentNo, Modal.DOTHEREXPENDITURE[i].Amount, DCount, "Other Expense", "", Modal.DOTHEREXPENDITURE[i].ProjectID);
                            }
                        }
                        Common_SPU.fnDelTravelExpenseRptDet(TravelExpenseID, DCount, "Other Expense");
                    }
                    //Set Trip Report 
                    if (Modal.TripReport != null)
                    {
                        int ECount = 0;
                        for (int i = 0; i < Modal.TripReport.Count; i++)
                        {

                            if (!string.IsNullOrEmpty(Modal.TripReport[i].Date) && !string.IsNullOrEmpty(Modal.TripReport[i].Justification))
                            {
                                ECount++;
                                SaveID = Common_SPU.fnSetTEFillReprt(TravelExpenseID, Modal.TripReport[i].DetailsID, Modal.TripReport[i].Date, "", "", i, "TripReport", Modal.TripReport[i].Justification);
                            }
                        }
                        //  Common_SPU.fnDelTravelExpenseRptDet(TravelExpenseID, ECount, "TripReport");
                    }
                    // set Expense Summary
                    if (Modal.EXPENSESUMMARY != null)
                    {
                        for (int i = 0; i < Modal.EXPENSESUMMARY.Count; i++)
                        {
                            SaveID = Common_SPU.fnSetTravelExpenseSummary(Modal.EXPENSESUMMARY[i].ID, Modal.EXPENSESUMMARY[i].TravelexpenseId, Modal.EXPENSESUMMARY[i].ProjectID, Modal.EXPENSESUMMARY[i].Travelfare, Modal.EXPENSESUMMARY[i].Perdiem, Modal.EXPENSESUMMARY[i].Transportion, Modal.EXPENSESUMMARY[i].Otherexpesnse, Modal.EXPENSESUMMARY[i].TravelId);
                        }
                    }

                    if (SaveID > 0)
                    {
                        if (Command == "Submit")
                        {
                            // fire mail for travel ter request create
                            Common_SPU.fnCreateMail_TER(TravelRequestID, "Submit");
                        }

                        if (Command == "Submit")
                        {
                            PostResult.SuccessMessage = "Travel expense report submitted successfully";
                        }
                        else if (Command == "Save")
                        {
                            PostResult.SuccessMessage = "Travel expense report saved successfully";
                        }
                        else
                        {

                        }

                        PostResult.Status = true;

                    }

                }
                else
                {
                    PostResult.SuccessMessage = "there is some problem, Please try again..";
                }
            }
            catch (Exception ex)
            {
                ClsCommon.LogError("Error during AddTravelDocuments. The query was executed :", ex.ToString(), "spu_GetConsultant", "MasterController", "MasterController", "");
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]

        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult ForwardToFinance_Desk(string src, string Command, ViewTravelRequest_ForDesk modal)
        {
            PostResponse PostResult = new PostResponse();
            ViewBag.src = src;

            if (modal.checkbox != null)
            {
                List<int> TagIds = modal.checkbox.Split(',').Select(int.Parse).ToList();
                string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
                ViewBag.GetQueryString = GetQueryString;
                ViewBag.MenuID = GetQueryString[0];
                ViewBag.TravelRequestID = GetQueryString[2];
                long TravelREQID = 0;
                long.TryParse(ViewBag.TravelRequestID, out TravelREQID);
                PostResult.SuccessMessage = "Action is not Taken";
                if (Command == "Approved")
                {
                    foreach (var item in TagIds)
                    {
                        Common_SPU.fnSetForwardToFinanceBy_Desk(TravelREQID, item);
                    }


                    PostResult.Status = true;
                    PostResult.SuccessMessage = "Information forwarded to user/ finance successfully";
                }
            }
            else
            {
                PostResult.Status = true;
                PostResult.SuccessMessage = "Information forwarded to user/ finance successfully";
            }

            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }


        public ActionResult _ViewTravelRequest_Expense(string src)
        {
            ViewBag.src = src;
            GetResponse getResponse = new GetResponse();
            getResponse.Approve = 0;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            long TravelREQID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelREQID);
            ViewTravelExpenseCompleteRequest Modal = new ViewTravelExpenseCompleteRequest();
            Modal = Travel.GetViewTravelExpenseCompleteRequest(TravelREQID);
            // ViewBag.projectlist = Project.GetProjectRegistrationList(getResponse);
            ViewBag.projectlist = Modal.ProjectList;
            return PartialView(Modal);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(ActionFor = "W")]
        public ActionResult _ViewTravelRequest_Expense(string src, ViewTravelExpenseCompleteRequest Modal, string Command)
        {
            PostResponse PostResult = new PostResponse();
            long SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelExpenseID = GetQueryString[2];
            long TravelExpenseID = 0;
            long.TryParse(ViewBag.TravelExpenseID, out TravelExpenseID);
            PostResult.SuccessMessage = "Travel Expense Request is not saved";
            if (ModelState.IsValid)
            {
                string ActionType = "";
                int Approved = 0;
                if (Command == "Approved")
                {
                    ActionType = Command;
                    Approved = 1;
                }
                else
                {
                    ActionType = Modal.ActionType;
                    Approved = 2;
                }
                SaveID = Common_SPU.fnSetTravelExpenseApproval_Action(TravelExpenseID, ActionType, Modal.reason, Approved);
                if (SaveID > 0)
                {
                    // fire mail for travel ter request create
                    Common_SPU.fnCreateMail_TER(TravelExpenseID, ActionType);
                    PostResult.Status = true;
                    if (Command == "Approved")
                    {
                        PostResult.SuccessMessage = "Travel expense request approved successfully";
                    }
                    else if (ActionType == "Reject")
                    {
                        PostResult.SuccessMessage = "Travel expense request rejected successfully";
                    }
                    else if (ActionType == "Resubmit")
                    {
                        PostResult.SuccessMessage = "Request to resubmit TER submitted successfully";
                    }
                    else
                    {
                        PostResult.SuccessMessage = "something went wrong";
                    }

                }

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

       //public static string Reverse(string input)
       // {
       //     char[] ch = input.ToCharArray();
       //     string revrsestring = string.Empty;
       //     int length, index;
       //     length = ch.Length - 1;
       //     index = length;
       //     while (index > -1)
       //     {
       //         revrsestring = revrsestring + ch[index];
       //     }
       //     return revrsestring;
       // }
       

        public ActionResult PaymentStatusToStaff(string src)
        {

          
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Type = 0;
            if (GetQueryString.Length == 3)
            {
                int.TryParse(GetQueryString[2], out Type);
            }
            ViewBag.Type = Type;
            AdvancePaymentUser Modal = new AdvancePaymentUser();
            Modal.listfinancePayment_Staffs = Travel.GetTravelRequestAdvanceFinance();
            return View(Modal);
        }

        public ActionResult _ApproveRequestStatusToStaff(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Type = 0;
            if (GetQueryString.Length == 3)
            {
                int.TryParse(GetQueryString[2], out Type);
            }
            ViewBag.Type = Type;
            AdvancePaymentUser Modal = new AdvancePaymentUser();
            Modal.listfinancePayment_Staffs = Travel.GetTravelRequestAdvanceFinance();
            return PartialView(Modal);
        }
        public ActionResult _PendingPaymentStatusToStaff(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Type = 0;
            if (GetQueryString.Length == 3)
            {
                int.TryParse(GetQueryString[2], out Type);
            }
            ViewBag.Type = Type;
            AdvancePaymentUser Modal = new AdvancePaymentUser();
            Modal.listfinancePayment_Staffs = Travel.GetTravelRequestAdvanceFinance();
            return PartialView(Modal);
        }
        public ActionResult _RejectPaymentStatusToStaff(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Type = 0;
            if (GetQueryString.Length == 3)
            {
                int.TryParse(GetQueryString[2], out Type);
            }
            ViewBag.Type = Type;
            AdvancePaymentUser Modal = new AdvancePaymentUser();
            Modal.listfinancePayment_Staffs = Travel.GetTravelRequestAdvanceFinance();
            return PartialView(Modal);
        }
        public ActionResult FinancePaymentPrint(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int TravelRequestID = 0;
            if (GetQueryString.Length == 3)
            {
                int.TryParse(GetQueryString[2], out TravelRequestID);
            }

            ViewTravelRequest Modal = new ViewTravelRequest();
            Modal = Travel.GetViewTravelAdvancedRequest(TravelRequestID);
            return View(Modal);
        }
        public ActionResult TravelExpensePaymentPrint(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int TravelRequestID = 0;
            if (GetQueryString.Length == 4)
            {
                int.TryParse(GetQueryString[2], out TravelRequestID);
            }

            TravelExpenseCompleteRequest Modal = new TravelExpenseCompleteRequest();
            Modal = Travel.GetTravelExpenseCompleteRequest(TravelRequestID);
            ViewBag.projectcout = Modal.ProjectList.Count();
            ViewBag.Status = GetQueryString[3].ToString();
            return View(Modal);
        }
        public ActionResult TravelDesk(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TravelDesk Modal = new TravelDesk();
            ViewBag.travellocations = Travel.GetTravelLocationMapList().GroupBy(x => x.EmployeeId).Select(x => x.FirstOrDefault());
            return View(Modal);
        }
        public ActionResult AddTravelDesk(string src)
        {
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EmployeeId = Convert.ToInt64(GetQueryString[2]);
            TravelDesk Modal = new TravelDesk();
            ViewBag.EMPList = Travel.GetTravelEmpList();
            string Url = clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Travel/_listTravelWorkLocation*" + PostResult.ID);
            ViewBag.Seturl = Url;
            if (EmployeeId > 0)
            {
                ViewBag.EmployeeId = EmployeeId;
                Modal.EmployeeId = EmployeeId;
            }
            return View(Modal);
        }
        [HttpPost]
        public ActionResult AddTravelDesk(string src, TravelDesk Modal)
        {
            ViewBag.src = src;
            var datalocation = Modal.travellocations.Where(x => x.Checkbox == "1").ToList();
            var dataunselectlocation = Modal.travellocations.Where(x => x.Checkbox == "0" && x.Id > 0).ToList();
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            PostResult.SuccessMessage = "Action Can't Save";
            if (datalocation.Count == 0)
            {
                ModelState.AddModelError("model.EmployeeId", "Hey! You missed this field");
                PostResult.SuccessMessage = "Hey! You missed this field";
            }

            if (ModelState.IsValid)
            {
                foreach (var locationtravel in datalocation)
                {
                    locationtravel.EmployeeId = Modal.EmployeeId;
                    locationtravel.AirTicketBooking = Modal.AirTicketBooking;
                    Travel.SetTravelLocationMap(locationtravel);

                }
                foreach (var deleteitem in dataunselectlocation)
                {
                    Travel.DelTravelLocationMap(deleteitem);
                }
                PostResult.Status = true;
            }
            if (PostResult.Status == true)
            {
                PostResult.SuccessMessage = "Added successfully";
            }
            ViewBag.EMPList = Travel.GetTravelEmpList();
            PostResult.RedirectURL = "/Travel/AddTravelDesk?src=" + clsApplicationSetting.EncryptQueryString(ViewBag.MenuID.ToString() + "*/Travel/AddTravelDesk*" + Modal.EmployeeId);
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _listTravelWorkLocation(string src, long EmployeeId)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TravelDesk Modal = new TravelDesk();
            var datalocation = Travel.GetTravellocationmap(EmployeeId);

            List<Travellocation> objlisttravellocation = new List<Travellocation>();
            foreach (var item in datalocation)
            {
                Travellocation travellocation = new Travellocation();
                if (item.EmployeeId > 0)
                {
                    travellocation.Checkbox = "1";
                    Modal.AirTicketBooking = item.AirTicketBooking;

                }
                else
                {
                    travellocation.Checkbox = "0";
                }
                Modal.AirTicketCount = item.AirTicketCount;
                travellocation.EmployeeId = item.EmployeeId;
                travellocation.LocationId = item.LocationId;
                travellocation.Id = item.Id;
                travellocation.LoctionName = item.LoctionName;
                Modal.LocationCount = item.LocationCount;
                objlisttravellocation.Add(travellocation);
            }
            Modal.travellocations = objlisttravellocation;
            return PartialView(Modal);
        }

        public ActionResult TravelRequestReminderMail()
        {

            Common_SPU.fnCreateMail_ReminderMail();
            return View();
        }
        public ActionResult FinancePaymenttoTravelDesk(string src)
        {
          

            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            return View();
        }

        public ActionResult _PendingFinancePaymenttoTravelDesk(string src)
        {
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TravelDeskToFinaceCard.TravelDeskToFinance Modal = new TravelDeskToFinaceCard.TravelDeskToFinance();
            Modal = Travel.GetTravelDeskToFinance();
            return PartialView(Modal);
        }
        public ActionResult _ApprovedFinancePaymenttoTravelDesk(string src)
        {
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TravelDeskToFinaceCard.TravelDeskToFinance Modal = new TravelDeskToFinaceCard.TravelDeskToFinance();
            Modal = Travel.GetTravelDeskToFinance();
            return PartialView(Modal);
        }
        public ActionResult _RejectFinancePaymenttoTravelDesk(string src)
        {
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TravelDeskToFinaceCard.TravelDeskToFinance Modal = new TravelDeskToFinaceCard.TravelDeskToFinance();
            Modal = Travel.GetTravelDeskToFinance();
            return PartialView(Modal);
        }
        public ActionResult FinancePaymenttoTravelDeskView(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int TravelRequestID = 0;
            int TravelDocID = 0;
            if (GetQueryString.Length == 4)
            {
                int.TryParse(GetQueryString[2], out TravelRequestID);
                int.TryParse(GetQueryString[3], out TravelDocID);
            }

            TravelDeskToFinaceCardView Modal = new TravelDeskToFinaceCardView();
            Modal = Travel.GetTravelDeskToFinaceCardView(TravelDocID, TravelRequestID);
            return View(Modal);
        }

        public ActionResult FinancePaymenttoTravelDeskPrint(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int TravelRequestID = 0;
            int TravelDocID = 0;
            if (GetQueryString.Length == 4)
            {
                int.TryParse(GetQueryString[2], out TravelRequestID);
                int.TryParse(GetQueryString[3], out TravelDocID);
            }

            TravelDeskToFinaceCardView Modal = new TravelDeskToFinaceCardView();
            Modal = Travel.GetTravelDeskToFinaceCardView(TravelDocID, TravelRequestID);
            return View(Modal);
        }
        public ActionResult FinancePaymenttoTravelDeskBilling(string src)
        {
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        public ActionResult _PendingFinancePaymenttoTravelDeskBilling(string src)
        {
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TravelDeskToFinaceCard.TravelDeskToFinance Modal = new TravelDeskToFinaceCard.TravelDeskToFinance();
            Modal = Travel.GetTravelDeskToFinanceAgentPending();
            return PartialView(Modal);
        }
        public ActionResult _ApprovedFinancePaymenttoTravelDeskBilling(string src)
        {
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TravelDeskToFinaceCard.TravelDeskToFinance Modal = new TravelDeskToFinaceCard.TravelDeskToFinance();
            Modal = Travel.GetTravelDeskToFinanceAgent();
            return PartialView(Modal);
        }
        public ActionResult _RejectFinancePaymenttoTravelDeskBilling(string src)
        {
            ViewBag.src = src;
            PostResponse PostResult = new PostResponse();
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TravelDeskToFinaceCard.TravelDeskToFinance Modal = new TravelDeskToFinaceCard.TravelDeskToFinance();
            Modal = Travel.GetTravelDeskToFinanceAgent();
            return PartialView(Modal);
        }
        public ActionResult FinancePaymenttoTravelDeskBillingPrint(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int TravelRequestID = 0;
            int TravelDocID = 0;
            if (GetQueryString.Length == 4)
            {
                int.TryParse(GetQueryString[2], out TravelRequestID);
                int.TryParse(GetQueryString[3], out TravelDocID);
            }

            TravelDeskToFinaceCardView Modal = new TravelDeskToFinaceCardView();
            Modal = Travel.GetTravelDeskToFinaceCardView(TravelDocID, TravelRequestID);
            return View(Modal);
        }
        public ActionResult FinancePaymenttoTravelDeskBillingView(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int TravelRequestID = 0;
            int TravelDocID = 0;
            if (GetQueryString.Length == 4)
            {
                int.TryParse(GetQueryString[2], out TravelRequestID);
                int.TryParse(GetQueryString[3], out TravelDocID);
            }

            TravelDeskToFinaceCardView Modal = new TravelDeskToFinaceCardView();
            Modal = Travel.GetTravelDeskToFinaceCardView(TravelDocID, TravelRequestID);
            return View(Modal);
        }

        public ActionResult TravelDeskReportPrint(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            long TravelRequestID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelRequestID);
            ViewTravelRequest_ForDesk Modal = new ViewTravelRequest_ForDesk();
            Modal = Travel.GetTravelRequest_ForDesk(TravelRequestID);
            return View(Modal);
        }
        public ActionResult TravelStatus(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MyTravelRequest> Modal = new List<MyTravelRequest>();
            Modal = Travel.GeTravelRequest_Dashboard(0);
            return View(Modal);
        }
    }
}