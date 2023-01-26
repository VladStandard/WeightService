// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleFkModels.PlusClipsFks;

/// <summary>
/// Table map "PLUS_CLIPS_FK".
/// </summary>
public class PluClipFkMap : ClassMap<PluClipFkModel>
{
    public PluClipFkMap()
    {
        Schema("db_scales");
        Table("PLUS_CLIPS_FK");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.Clip).Column("CLIP_UID").Not.Nullable();
        References(x => x.Plu).Column("PLU_UID").Unique().Not.Nullable();
    }
}