// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.Templates;

/// <summary>
/// Table map "Templates".
/// </summary>
public sealed class TemplateMap : ClassMap<TemplateModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplateMap()
    {
        Schema(WsSqlSchemaNamesUtils.DbScales);
        Table(WsSqlTableNamesUtils.Templates);
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(x => x.CategoryId).CustomSqlType("NVARCHAR").Column("CategoryID").Length(150).Not.Nullable();
        Map(x => x.Title).CustomSqlType("NVARCHAR").Column("Title").Length(250).Nullable();
        //Map(x => x.ImageDataValue).CustomSqlType("VARBINARY(MAX)").Column("ImageData").Nullable();
        Map(x => x.Data).CustomSqlType("NVARCHAR(MAX)").Column("DATA").Not.Nullable();
    }
}