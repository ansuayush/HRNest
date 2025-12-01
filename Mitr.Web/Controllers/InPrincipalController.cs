using Mitr.CommonClass;
using Mitr.Models;
using Mitr.ModelsMaster;
using Mitr.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    [CheckLoginFilter]
    public class InPrincipalController : Controller
    {
        // GET: InPrincipal
        IPrincipalHelper principal;
        ITravelHelper Travel;
        public InPrincipalController()
        {
            principal = new PrincipalModal();
            Travel = new TravelModal();
        }


        public ActionResult PMApproval(string src)
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
            Principal Modal = new Principal();
            Modal.listprincipal = principal.GetPrincipalList();
            return View(Modal);
        }
        public ActionResult _ViewTravelRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TravelRequestID = GetQueryString[2];
            long TravelREQID = 0;
            long.TryParse(ViewBag.TravelRequestID, out TravelREQID);
            ViewTravelRequest Modal = new ViewTravelRequest();
            Modal = Travel.GetViewTravelRequest(TravelREQID);
            return PartialView(Modal);

        }
    }
}