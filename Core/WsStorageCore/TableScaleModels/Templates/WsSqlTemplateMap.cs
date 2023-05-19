// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Templates;

/// <summary>
/// Table map "Templates".
/// </summary>
public sealed class WsSqlTemplateMap : ClassMap<WsSqlTemplateModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTemplateMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Templates);
        LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(item => item.CategoryId).CustomSqlType("NVARCHAR").Column("CategoryID").Length(150).Not.Nullable();
        Map(item => item.Title).CustomSqlType("NVARCHAR").Column("Title").Length(250).Nullable();
        //Map(item => item.ImageDataValue).CustomSqlType("VARBINARY(MAX)").Column("ImageData").Nullable();
        Map(item => item.Data).CustomSqlType("NVARCHAR(MAX)").Column("DATA").Not.Nullable();
    }
}