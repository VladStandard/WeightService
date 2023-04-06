// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleFkModels.PlusLabels;

/// <summary>
/// Table map "PLUS_LABELS".
/// </summary>
public sealed class PluLabelMap : ClassMap<PluLabelModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelMap()
    {
        Schema(WsSqlSchemaNamesUtils.DbScales);
        Table(WsSqlTableNamesUtils.PlusLabels);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Zpl).CustomSqlType("NVARCHAR").Column("ZPL").Not.Nullable().Default("");
        Map(x => x.Xml).CustomSqlType("XML").Column("XML").Nullable();
        Map(x => x.ProductDt).CustomSqlType("DATETIME").Column("PROD_DT").Not.Nullable();
        Map(x => x.ExpirationDt).CustomSqlType("DATETIME").Column("EXPIRATION_DT").Not.Nullable();
        References(x => x.PluWeighing).Column("PLU_WEIGHING_UID").Nullable();
        References(x => x.PluScale).Column("PLU_SCALE_UID").Nullable();
    }
}