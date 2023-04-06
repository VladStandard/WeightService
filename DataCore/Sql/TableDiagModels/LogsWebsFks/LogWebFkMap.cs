// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableDiagModels.LogsWebsFks;

/// <summary>
/// Table map "LOGS_WEBS_FK".
/// </summary>
public sealed class LogWebFkMap : ClassMap<LogWebFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogWebFkMap()
    {
        Schema(WsSqlSchemaNamesUtils.DbScales);
        Table(WsSqlTableNamesUtils.LogWebFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        References(x => x.LogWebRequest).Column("LOG_WEB_REQUEST_UID").Not.Nullable();
        References(x => x.LogWebResponse).Column("LOG_WEB_RESPONSE_UID").Not.Nullable();
        References(x => x.App).Column("APP_UID").Not.Nullable();
        References(x => x.LogType).Column("LOG_TYPE_UID").Not.Nullable();
        References(x => x.Device).Column("DEVICE_UID").Not.Nullable();
    }
}