// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.DeviceTypes;

/// <summary>
/// Table map "DEVICES_TYPES".
/// </summary>
public class DeviceTypeMap : ClassMap<DeviceTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceTypeMap()
    {
        Schema("db_scales");
        Table(SqlTableNamesUtils.DevicesTypes);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(128).Not.Nullable();
        Map(x => x.PrettyName).CustomSqlType("NVARCHAR").Column("PRETTY_NAME").Length(128).Not.Nullable();
        Map(x => x.Description).CustomSqlType("NVARCHAR").Column("DESCRIPTION").Length(1024).Not.Nullable();
    }
}
