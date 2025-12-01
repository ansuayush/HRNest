using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.ModelsMasterHelper
{
    interface IPTCHelper
    {
        List<PTC.Hierarchy> GetPTC_HierarchyList(long Approved);
        PTC.Hierarchy.Update GetPTC_HierarchyUpdate(long ID,long Approved,long EMPID);

        List<PTC.CreationofObjective> GetPTC_CreationofObjective(long Approved);
        PTC.ProbationObjectives GetPTC_ProbationObjectives(long ID, long Approved, long EMPID);
        PTC.ObjectivesperiodNew GetPTC_ObjectiveUpdate(long ID);
        List<PTC.CreationofObjective> GetPTC_CreationofObjectiveMyList(long Approved);

        PTC.Appraisal.SelfAppraisal GetPTC_SelfAppraisal(long ID);
        List<PTC.Appraisal.TeamList> GetPTC_TeamList(long Approved);
        List<PTC.Appraisal.TeamList> GetPTC_ConfimerList(long Approved);

        PTC.Appraisal.SelfAppraisal GetPTC_ConfimerAppraisal(long ID, long EMPID);

        List<PTC.Appraisal.TeamList> GetPTC_CMCList(long Approved);
        PTC.Appraisal.CMCAppraisal GetPTC_CMCAppraisal(long ID, long EMPID);
        List<PTC.Appraisal.TeamList> GetPTC_HRList(long Approved);
        PTC.Appraisal.HRAppraisal GetPTC_HRAppraisal(long ID, long EMPID);
        List<PTC.Hierarchy> GetPTC_Report(string Type, long EMPID);
    }
}
