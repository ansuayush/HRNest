using Mitr.Areas.Career.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.Areas.Career.ModelsMasterHelper
{
    interface IApplicationHelper
    {
        JobApplication.Apply GetApplyJob(string Code);
    }
}