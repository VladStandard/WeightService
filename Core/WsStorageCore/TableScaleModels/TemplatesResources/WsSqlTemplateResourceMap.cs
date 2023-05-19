// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.TemplatesResources;

/// <summary>
/// Table map "TEMPLATES_RESOURCES".
/// </summary>
public sealed class WsSqlTemplateResourceMap : ClassMap<WsSqlTemplateResourceModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTemplateResourceMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.TemplatesResources);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(64).Not.Nullable();
        Map(item => item.Type).CustomSqlType("VARCHAR").Column("TYPE").Length(8).Not.Nullable();
        Map(item => item.DataValue).CustomSqlType("VARBINARY(MAX)").Column("DATA").Not.Nullable().Length(int.MaxValue);
    }
}