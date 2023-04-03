// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.TemplatesResources;

/// <summary>
/// Table map "TEMPLATES_RESOURCES".
/// </summary>
public sealed class TemplateResourceMap : ClassMap<TemplateResourceModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplateResourceMap()
    {
        Schema(SqlSchemaNamesUtils.DbScales);
        Table(SqlTableNamesUtils.TemplatesResources);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(64).Not.Nullable();
        Map(x => x.Type).CustomSqlType("VARCHAR").Column("TYPE").Length(8).Not.Nullable();
        Map(x => x.DataValue).CustomSqlType("VARBINARY(MAX)").Column("DATA").Not.Nullable().Length(int.MaxValue);
    }
}