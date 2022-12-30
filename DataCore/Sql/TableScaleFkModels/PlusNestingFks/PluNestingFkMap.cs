// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleFkModels.PlusNestingFks;

/// <summary>
/// Table map "PLUS_NESTING_FK".
/// </summary>
public class PluNestingFkMap : ClassMap<PluNestingFkModel>
{   
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluNestingFkMap()
    {
        Schema("db_scales");
        Table("PLUS_NESTING_FK");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.IsDefault).CustomSqlType("BIT").Column("IS_DEFAULT").Not.Nullable().Default("0");
        References(x => x.Nesting).Column("NESTING_FK").Unique().Not.Nullable();
        References(x => x.PluBundle).Column("PLU_BUNDLE_FK").Unique().Not.Nullable();
    }
}

