// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// Table map "DEVICES_SCALES_FK".
/// </summary>
public sealed class DeviceScaleFkMap : ClassMap<DeviceScaleFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceScaleFkMap()
    {
        Schema(WsSqlSchemaNamesUtils.DbScales);
        Table(WsSqlTableNamesUtils.DevicesScalesFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.Device).Column("DEVICE_UID").Not.Nullable();
        References(x => x.Scale).Column("SCALE_ID").Not.Nullable();
    }
}