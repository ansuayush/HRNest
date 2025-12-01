using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mitr.Model;
namespace Mitr.Interface
{
    public interface IGeneric
    {       
        ScreenDBMappingModel GetScreenSP(string screenId, int roleId, string operation, out string errorMessage);

        CustomResponseModel PerformGenericOperation(string globalXml,string xml, string screenId, int roleId, int userid, string operation, out string errorMessage);
        CustomResponseModel GetGenericRecords(string globalXml,string xml, string screenId, int roleId, int userid, string operation, out string errorMessage);
        CustomResponseModel GetDropDown(int masterTableTypeId, int? parentId, bool isMasterTableType, bool isManualTable, int manualTable, int manualTableId);
    }
}
