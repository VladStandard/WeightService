// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// Table map "PLUS_BUNDLES_FK".
/// </summary>
public sealed class PluBundleFkMap : ClassMap<PluBundleFkModel>
{   
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluBundleFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusBundlesFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.Bundle).Column("BUNDLE_UID").Not.Nullable();
        References(x => x.Plu).Column("PLU_UID").Unique().Not.Nullable();
    }
}