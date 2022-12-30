// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.TemplatesResources;

/// <summary>
/// Table map "TemplateResources".
/// </summary>
public class TemplateResourceMap : ClassMap<TemplateResourceModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplateResourceMap()
    {
        Schema("db_scales");
        Table(SqlTableNamesUtils.TemplatesResources);
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Length(50).Nullable();
        Map(x => x.Description).CustomSqlType("NVARCHAR").Column("Description").Length(500).Nullable();
        Map(x => x.Type).CustomSqlType("VARCHAR").Column("Type").Length(3).Nullable();
        Map(x => x.ImageDataValue).CustomSqlType("VARBINARY(MAX)").Column("ImageData").Nullable().Length(int.MaxValue);
        Map(x => x.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IdRRef").Nullable();
    }
}
