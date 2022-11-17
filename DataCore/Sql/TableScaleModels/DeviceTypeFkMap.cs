// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "DEVICES_TYPES_FK".
/// </summary>
public class DeviceTypeFkMap : ClassMap<DeviceTypeFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceTypeFkMap()
    {
        Schema("db_scales");
        Table("DEVICES_TYPES_FK");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
		References(x => x.Device).Column("DEVICE_UID").Not.Nullable();
		References(x => x.Type).Column("TYPE_UID").Not.Nullable();
	}
}
