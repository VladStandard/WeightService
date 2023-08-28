namespace WsStorageCore.Views.ViewRefModels.PluStorageMethods;

public interface IViewStorageMethodsRepository
{
    List<WsSqlViewPluStorageMethodModel> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}