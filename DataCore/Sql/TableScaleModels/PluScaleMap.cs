// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "PLUS_SCALES".
/// </summary>
public class PluScaleMap : ClassMap<PluScaleEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluScaleMap()
    {
        Schema("db_scales");
        Table("PLUS_SCALES");
        LazyLoad();
        Id(x => x.IdentityUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.IsActive).CustomSqlType("BIT").Column("IS_ACTIVE").Not.Nullable().Default("0");
        References(x => x.Plu).Column("PLUS_UID").Not.Nullable();
        References(x => x.Scale).Column("SCALE_ID").Not.Nullable();
    }
}
