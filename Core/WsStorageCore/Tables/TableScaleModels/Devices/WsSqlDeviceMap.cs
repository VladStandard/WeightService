namespace WsStorageCore.Tables.TableScaleModels.Devices;

public sealed class WsSqlDeviceMap : ClassMap<WsSqlDeviceModel>
{
    public WsSqlDeviceMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Devices);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.LoginDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("LOGIN_DT").Not.Nullable();
        Map(item => item.LogoutDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("LOGOUT_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(128).Not.Nullable();
        Map(item => item.PrettyName).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("PRETTY_NAME").Length(128).Not.Nullable();
        Map(item => item.Description).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("DESCRIPTION").Length(1024).Not.Nullable();
        Map(item => item.Ipv4).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("IP_V4").Length(15).Nullable();
        Map(item => item.MacAddressValue).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("MAC").Length(12).Nullable();
    }
}
