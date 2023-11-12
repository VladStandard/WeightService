namespace WsStorageCore.Views.ViewRefModels.PluNestings;

public interface IViewPluNestingRepository
{
    public IEnumerable<WsSqlViewPluNestingModel> GetEnumerable(ushort pluNumber = 0);
}