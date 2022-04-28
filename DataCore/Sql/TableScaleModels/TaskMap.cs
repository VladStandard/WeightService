// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.Sql.TableScaleModels
{
    public class TaskMap : ClassMap<TaskEntity>
    {
        public TaskMap()
        {
            Table("[db_scales].[TASKS]");
            LazyLoad();
            Id(x => x.IdentityUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
            References(x => x.TaskType).Column("TASK_UID").Not.Nullable();
            References(x => x.Scale).Column("SCALE_ID").Not.Nullable();
            Map(x => x.Enabled).CustomSqlType("BIT").Column("ENABLED").Not.Nullable().Default("0");
            Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        }
    }
}
