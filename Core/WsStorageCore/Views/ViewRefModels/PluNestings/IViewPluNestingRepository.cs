namespace WsStorageCore.Views.ViewRefModels.PluNestings;

public interface IViewPluNestingRepository
{
    public List<WsSqlViewPluNestingModel> GetList(ushort pluNumber = 0);
}