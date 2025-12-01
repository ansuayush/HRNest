using Mitr.Model;

namespace Mitr.Interface
{
    public interface IOnboarding : ICommon
    {
        CustomResponseModel GetDropDown(int masterTableTypeId, int? parentId, bool isMasterTableType, bool isManualTable, int manualTable, int manualTableId);
    }
}
