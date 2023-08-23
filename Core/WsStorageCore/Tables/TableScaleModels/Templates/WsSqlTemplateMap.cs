// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Templates;

public sealed class WsSqlTemplateMap : ClassMap<WsSqlTemplateModel>
{
    public WsSqlTemplateMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Templates);
        LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CreateDate").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("ModifiedDate").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("Marked").Not.Nullable().Default("0");
        Map(item => item.CategoryId).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("CategoryID").Length(150).Not.Nullable();
        Map(item => item.Title).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("Title").Length(250).Nullable();
        Map(item => item.Data).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("DATA").Not.Nullable();
    }
}