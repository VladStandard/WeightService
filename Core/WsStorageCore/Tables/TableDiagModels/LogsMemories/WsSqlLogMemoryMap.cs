using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableDiagModels.LogsMemories;

public sealed class WsSqlLogMemoryMap : ClassMap<WsSqlLogMemoryModel>
{
    public WsSqlLogMemoryMap()
    {
        Schema(WsSqlSchemasUtils.Diag);
        Table(WsSqlTablesUtils.LogsMemories);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.SizeAppMb).CustomSqlType(WsSqlFieldTypeUtils.SmallInt).Column("SIZE_APP_MB").Not.Nullable().Default("0");
        Map(item => item.SizeFreeMb).CustomSqlType(WsSqlFieldTypeUtils.SmallInt).Column("SIZE_FREE_MB").Not.Nullable().Default("0");
        References(item => item.App).Column("APP_UID").Not.Nullable();
        References(item => item.Device).Column("DEVICE_UID").Not.Nullable();
    }
}