using Ws.StorageCore.Models;
namespace Ws.StorageCore.Views.ViewRefModels.PluStorageMethods;

public interface IViewStorageMethodsRepository
{
    List<SqlViewPluStorageMethodModel> GetList(SqlCrudConfigModel sqlCrudConfig);
}