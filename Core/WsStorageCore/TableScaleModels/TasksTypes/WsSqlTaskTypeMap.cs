// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.TasksTypes;

/// <summary>
/// Table map "TASKS_TYPES".
/// </summary>
public sealed class WsSqlTaskTypeMap : ClassMap<WsSqlTaskTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTaskTypeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.TasksTypes);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Length(32).Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
    }
}
