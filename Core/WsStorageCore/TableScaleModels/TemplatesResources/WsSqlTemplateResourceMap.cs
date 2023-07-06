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
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(64).Not.Nullable();
        Map(item => item.Type).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("TYPE").Length(8).Not.Nullable();
        Map(item => item.DataValue).CustomSqlType(WsSqlFieldTypeUtils.VarBinary).Column("DATA").Not.Nullable().Length(int.MaxValue);
    }
}