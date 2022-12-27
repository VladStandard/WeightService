// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.PlusWeighings;

/// <summary>
/// Table map "PLUS_WEIGHINGS".
/// </summary>
public class PluWeighingMap : ClassMap<PluWeighingModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluWeighingMap()
    {
        Schema("db_scales");
        Table("PLUS_WEIGHINGS");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Kneading).CustomSqlType("INT").Column("KNEADING").Not.Nullable().Default("0");
        Map(x => x.Sscc).CustomSqlType("VARCHAR").Column("SSCC").Length(50).Not.Nullable().Default("");
        Map(x => x.NettoWeight).CustomSqlType("DECIMAL(10,3)").Column("NETTO_WEIGHT").Not.Nullable().Default("0");
        Map(x => x.WeightTare).CustomSqlType("DECIMAL(10,3)").Column("TARE_WEIGHT").Not.Nullable().Default("0");
        Map(x => x.RegNum).CustomSqlType("INT)").Column("REG_NUM").Not.Nullable().Default("0");
        References(x => x.PluScale).Column("PLU_SCALE_UID").Not.Nullable();
        References(x => x.Series).Column("SERIES_ID").Nullable();
    }
}
