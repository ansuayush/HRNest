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
    [CheckOnlyLoginFilter]
    public class ServicesController : Controller
    {
        IServicesHelper Service;
        public ServicesController()
        {
            Service = new ServicesModal();
        }
        [HttpGet]
        public ActionResult _Notification()
        {
            NotificationComponent _messageRepository = new NotificationComponent();
            return PartialView(_messageRepository.InvokeNotification());
        }

        public ActionResult AllNotifications(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = clsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PushNotification> List = new List<PushNotification>();
             List = Service.GetPushNotificationList("All");
            return View(List);
        }
    }
}