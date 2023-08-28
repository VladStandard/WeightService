namespace WsStorageCore.Tables.TableScaleModels.Tasks;

public sealed class WsSqlTaskMap : ClassMap<WsSqlTaskModel>
{
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
