namespace WsStorageCore.Tables.TableScaleFkModels.DeviceScalesFks;

public sealed class WsSqlDeviceScaleFkMap : ClassMap<WsSqlDeviceScaleFkModel>
{
    public WsSqlDeviceScaleFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.DevicesScalesFks);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        References(item => item.Device).Column("DEVICE_UID").Not.Nullable();
        References(item => item.Scale).Column("SCALE_ID").Not.Nullable();
    }
}