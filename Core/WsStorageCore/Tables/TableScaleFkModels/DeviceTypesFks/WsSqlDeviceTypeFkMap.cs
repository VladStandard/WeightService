namespace WsStorageCore.Tables.TableScaleFkModels.DeviceTypesFks;

public sealed class WsSqlDeviceTypeFkMap : ClassMap<WsSqlDeviceTypeFkModel>
{
    public WsSqlDeviceTypeFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.DevicesTypesFks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        References(item => item.Device).Column("DEVICE_UID").Not.Nullable();
        References(item => item.Type).Column("TYPE_UID").Not.Nullable();
    }
}