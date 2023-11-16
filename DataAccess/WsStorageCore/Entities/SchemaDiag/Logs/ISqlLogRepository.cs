namespace WsStorageCore.Entities.SchemaDiag.Logs;

public interface ISqlLogRepository : ISqlTableBaseRepository<SqlLogEntity>
{
    public SqlLogEntity GetItemByUid(Guid uid);
}