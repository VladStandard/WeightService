// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Utils;

namespace WsStorageCore.TableScaleFkModels.PlusBrandsFks;

/// <summary>
/// Table map "PLUS_BRANDS_FK".
/// </summary>
public sealed class PluBrandFkMap : ClassMap<PluBrandFkModel>
{   
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluBrandFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusBrandsFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.Plu).Column("PLU_UID").Unique().Not.Nullable();
        References(x => x.Brand).Column("BRAND_UID").Not.Nullable();
    }
}