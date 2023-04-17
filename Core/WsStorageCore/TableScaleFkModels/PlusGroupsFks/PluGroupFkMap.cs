// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Utils;

namespace WsStorageCore.TableScaleFkModels.PlusGroupsFks;

/// <summary>
/// Table map "PLUS_GROUPS_FK".
/// </summary>
public sealed class PluGroupFkMap : ClassMap<PluGroupFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluGroupFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusGroupsFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.PluGroup).Column("PLU_GROUP_UID").Not.Nullable();
        References(x => x.Parent).Column("PARENT_UID").Not.Nullable();
    }
}