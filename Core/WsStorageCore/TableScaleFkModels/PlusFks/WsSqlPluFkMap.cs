// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusFks;

/// <summary>
/// Table map "PLUS_FK".
/// </summary>
public sealed class WsSqlPluFkMap : ClassMap<WsSqlPluFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.Plu).Column("PLU_UID").Not.Nullable();
        References(x => x.Parent).Column("PARENT_UID").Not.Nullable();
        References(x => x.Category).Column("CATEGORY_UID").Nullable();
    }
}