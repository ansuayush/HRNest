using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.ModelsMasterHelper
{
    interface IBoardHelper
    {
        List<BoardMembersDet> GetBoardName();
        SingleMemberDetails GetSingleMemberDetails(string memshipno);
    }
}