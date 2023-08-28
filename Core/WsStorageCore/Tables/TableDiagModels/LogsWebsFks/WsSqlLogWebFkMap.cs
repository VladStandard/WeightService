namespace WsStorageCore.Tables.TableDiagModels.LogsWebsFks;

public sealed class WsSqlLogWebFkMap : ClassMap<WsSqlLogWebFkModel>
{
    public WsSqlLogWebFkMap()
    {
        Schema(WsSqlSchemasUtils.Diag);
        Table(WsSqlTablesUtils.LogWebFks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        References(item => item.LogWebRequest).Column("LOG_WEB_REQUEST_UID").Not.Nullable();
        References(item => item.LogWebResponse).Column("LOG_WEB_RESPONSE_UID").Not.Nullable();
        References(item => item.App).Column("APP_UID").Not.Nullable();
        References(item => item.LogType).Column("LOG_TYPE_UID").Not.Nullable();
        References(item => item.Device).Column("DEVICE_UID").Not.Nullable();
    }
}