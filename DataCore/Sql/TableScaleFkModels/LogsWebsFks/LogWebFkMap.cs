// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleFkModels.LogsWebsFks;

/// <summary>
/// Table map "LOGS_WEBS_FK".
/// </summary>
public class LogWebFkMap : ClassMap<LogWebFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogWebFkMap()
    {
        Schema(SqlSchemaNamesUtils.DbScales);
        Table(SqlTableNamesUtils.LogWebFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        References(x => x.LogWeb).Column("LOG_WEB_UID").Not.Nullable();
        References(x => x.App).Column("APP_UID").Not.Nullable();
        References(x => x.LogType).Column("LOG_TYPE_UID").Not.Nullable();
        References(x => x.Device).Column("DEVICE_UID").Not.Nullable();
    }
}