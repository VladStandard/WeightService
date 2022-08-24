// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "PLUS_LABELS".
/// </summary>
public class PluLabelMap : ClassMap<PluLabelEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelMap()
    {
        Schema("db_scales");
        Table("PLUS_LABELS");
        LazyLoad();
        Id(x => x.IdentityUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Zpl).CustomSqlType("NVARCHAR").Column("ZPL").Not.Nullable().Default("");
        References(x => x.PluWeighing).Column("PLU_WEIGHING_UID").Nullable();
    }
}
