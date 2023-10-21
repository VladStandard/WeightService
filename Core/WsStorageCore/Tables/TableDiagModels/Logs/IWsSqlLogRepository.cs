// ReSharper disable InconsistentNaming

namespace WsStorageCore.Tables.TableDiagModels.Logs;

public interface IWsSqlLogRepository : IWsSqlTableBaseRepository<WsSqlLogModel>
{
    public WsSqlLogModel GetItemByUid(Guid uid);
}