// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusWeighingsFks;

/// <summary>
/// Table map "PLUS_WEIGHINGS".
/// </summary>
public sealed class WsSqlPluWeighingMap : ClassMap<WsSqlPluWeighingModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluWeighingMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusWeighings);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Kneading).CustomSqlType("INT").Column("KNEADING").Not.Nullable().Default("0");
        Map(item => item.Sscc).CustomSqlType("VARCHAR").Column("SSCC").Length(50).Not.Nullable().Default("");
        Map(item => item.NettoWeight).CustomSqlType("DECIMAL(10,3)").Column("NETTO_WEIGHT").Not.Nullable().Default("0");
        Map(item => item.WeightTare).CustomSqlType("DECIMAL(10,3)").Column("TARE_WEIGHT").Not.Nullable().Default("0");
        Map(item => item.RegNum).CustomSqlType("INT)").Column("REG_NUM").Not.Nullable().Default("0");
        References(item => item.PluScale).Column("PLU_SCALE_UID").Not.Nullable();
        References(item => item.Series).Column("SERIES_ID").Nullable();
    }
}
