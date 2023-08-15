// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.Tables.TableDiagModels.Logs;

public interface IWsSqlLogRepository : IWsSqlTableBaseRepository<WsSqlLogModel>
{
    public WsSqlLogModel GetItemByUid(Guid uid);
}