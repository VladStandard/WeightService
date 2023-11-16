namespace WsStorageCore.Views.ViewRefModels.PluStorageMethods;

public interface IViewStorageMethodsRepository
{
    List<SqlViewPluStorageMethodModel> GetList(SqlCrudConfigModel sqlCrudConfig);
}