namespace WsStorageCore.Entities.SchemaDiag.Logs;

public interface IWsSqlLogRepository : IWsSqlTableBaseRepository<WsSqlLogEntity>
{
    public WsSqlLogEntity GetItemByUid(Guid uid);
}