// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.ProductSeries;

public sealed class WsSqlProductSeriesMap : ClassMap<WsSqlProductSeriesModel>
{
    public WsSqlProductSeriesMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.ProductSeries);
        LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime27).Column("CreateDate").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Uid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UUID").Nullable();
        Map(item => item.IsClose).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IsClose").Not.Nullable().Default("0");
        Map(item => item.Sscc).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("SSCC").Length(50).Nullable();
        References(item => item.Scale).Column("ScaleID").Not.Nullable();
    }
}
