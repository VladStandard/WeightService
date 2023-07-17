// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Tasks;

/// <summary>
/// Table map "TASKS".
/// </summary>
public sealed class WsSqlTaskMap : ClassMap<WsSqlTaskModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTaskMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Tasks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.Enabled).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("ENABLED").Not.Nullable().Default("0");
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        References(item => item.TaskType).Column("TASK_UID").Not.Nullable();
        References(item => item.Scale).Column("SCALE_ID").Not.Nullable();
    }
}
