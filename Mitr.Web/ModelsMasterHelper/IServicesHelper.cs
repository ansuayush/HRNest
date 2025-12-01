using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface IServicesHelper
    {
        List<PushNotification> GetPushNotificationList(string ListType);
    }
}