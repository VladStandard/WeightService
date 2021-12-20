// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataProjectsCore.DAL.TableScaleModels
{
    public class TaskTypeMap : ClassMap<TaskTypeEntity>
    {
        public TaskTypeMap()
        {
            Table("[db_scales].[TASKS_TYPES]");
            LazyLoad();
            Id(x => x.Uid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(32)").Column("Name").Length(32).Not.Nullable();
        }
    }
}
