using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface IFundRaisingHelper
    {
        List<FundRaising.Prospect.List> GetProspectList(long ID);
        FundRaising.Prospect.Add GetFundRaisingDetail(GetResponse modal);
        PostResponse SetProspect(FundRaising.Prospect.Add modal);
        PostResponse SetProspectContact(FundRaising.Prospect.ContactDetails modal);
        List<FundRaising.Lead.List> GetLeadList(long ID, string Status, long StageLevelId);
        FundRaising.Lead.Add GetLead(GetResponse modal);
        PostResponse SetLead(FundRaising.Lead.Add modal);
        PostResponse SetLeadActivity(FundRaising.Lead.LeadActivity modal);
        PostResponse SetLeadReferrals(FundRaising.Lead.Refferals modal);
        List<FundRaising.RefferalsTask> GetReferralTaskList(long ID, string Status);
        FundRaising.LeadDashboard GetLeadDashboardList();



    }
}