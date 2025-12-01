using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.ModelsMasterHelper
{
    interface IExitEmployee
    {
        PostResponse fnsetAddSignatoryMaster(EmployeeExit.SignatoryMaster model);
        List<EmployeeExit.SignatoryMaster> GetSignatoryMaster(long ID);
        EmployeeExit.ResignationRequest GetResignation(long lId, long lEmpid, string sDoctype);
        List<EmployeeExit.HRList> GetExit_HRList(long Approved);
    }
}
