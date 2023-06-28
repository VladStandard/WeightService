// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.DeviceTypes;

/// <summary>
/// Table map "DEVICES_TYPES".
/// </summary>
public sealed class WsSqlDeviceTypeMap : ClassMap<WsSqlDeviceTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceTypeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.DevicesTypes);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(128).Not.Nullable();
        Map(item => item.PrettyName).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("PRETTY_NAME").Length(128).Not.Nullable();
        Map(item => item.Description).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("DESCRIPTION").Length(1024).Not.Nullable();
    }
}
