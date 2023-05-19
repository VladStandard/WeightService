// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusGroupsFks;

/// <summary>
/// Table map "PLUS_GROUPS_FK".
/// </summary>
public sealed class WsSqlPluGroupFkMap : ClassMap<WsSqlPluGroupFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluGroupFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusGroupsFks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(item => item.PluGroup).Column("PLU_GROUP_UID").Not.Nullable();
        References(item => item.Parent).Column("PARENT_UID").Not.Nullable();
    }
}