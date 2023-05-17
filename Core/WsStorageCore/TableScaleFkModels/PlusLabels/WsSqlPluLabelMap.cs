// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusLabels;

/// <summary>
/// Table map "PLUS_LABELS".
/// </summary>
public sealed class WsSqlPluLabelMap : ClassMap<WsSqlPluLabelModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluLabelMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusLabels);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Zpl).CustomSqlType("NVARCHAR").Column("ZPL").Not.Nullable().Default("");
        Map(item => item.Xml).CustomSqlType("XML").Column("XML").Nullable();
        Map(item => item.ProductDt).CustomSqlType("DATETIME").Column("PROD_DT").Not.Nullable();
        Map(item => item.ExpirationDt).CustomSqlType("DATETIME").Column("EXPIRATION_DT").Not.Nullable();
        References(item => item.PluWeighing).Column("PLU_WEIGHING_UID").Nullable();
        References(item => item.PluScale).Column("PLU_SCALE_UID").Nullable();
    }
}