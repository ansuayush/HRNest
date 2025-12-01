using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
   interface ITicketHelper
    {
        List<Location> GetTicketLocationList(long LocationGroupID);
        Helpdesk.TicketConfigration GetTicketConfigration();
        List<Helpdesk.SubCategory> GetTicket_SubCategoryList(long SubCategoryID);
        

        List<Helpdesk.TicketSetting_Assignment> GetTicket_AssignmentList(long SubSettingID, long SubCategoryID, string Doctype);
        List<Helpdesk.TicketSetting_SMA> GetTicket_SMAList(long SubSettingID, long SubCategoryID, string Doctype);
        List<Helpdesk.Ticket> GetTicketList(long TicketID);
        Helpdesk.LocationGroup GetMyLocationGroup();
        List<Helpdesk.Ticket_Status> GetTicketStatusList(long TicketStatusID, string Type, string IsActive);
        Helpdesk.TicketDetails GetTicketDetails(long TicketID, long TicketNotesID);
    }
}