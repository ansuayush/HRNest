using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Data;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class BoardController : Controller
    {
        IBoardHelper Board;
        public BoardController()
        {
            Board = new BoardModal();
        }
        public ActionResult MainBoardMemDetails(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            inputBoard Modal = new inputBoard();
            Modal.Approve = "0";

            return View(Modal);
        }
        [HttpPost]
        public ActionResult _MainBoardMemDetails(string src, inputBoard Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetBoardMembershipRegistration(Modal.Approve);
            return PartialView(ds);
        }

        public ActionResult MemshipRegisterRpt(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            BoardMembershipRegister Modal = new BoardMembershipRegister();
            Modal.DateAsOn = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _MemshipRegisterRpt_Partial(string src, BoardMembershipRegister Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (ModelState.IsValid)
            {
                DataSet ds = new DataSet();
                ds = Common_SPU.fnGetMembershipRegistered(Modal.DateAsOn, Modal.Type);
                return PartialView(ds);
            }
            else
            {
                return PartialView();
            }

        }

        public ActionResult MemberInfoRpt(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TempBoardShow Modal = new TempBoardShow();
            Modal.membersList = Board.GetBoardName();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _MemberInfoRpt(string src, TempBoardShow Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (ModelState.IsValid)
            {
                SingleMemberDetails modal = new SingleMemberDetails();
                modal = Board.GetSingleMemberDetails(Modal.memship_no);
                return PartialView(modal);
            }
            else
            {
                return PartialView();
            }
        }


        public ActionResult BoardMeetings(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            inputBoard Modal = new inputBoard();
            Modal.Approve = "2";
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _BoardMeetings(string src, inputBoard Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approve = Modal.Approve;
            DataSet ds = new DataSet();
            ds = Common_SPU.fnGetBoardMeeting(Modal.Approve);
            return PartialView(ds);
        }




    }
}