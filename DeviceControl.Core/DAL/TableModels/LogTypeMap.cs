using FluentNHibernate.Mapping;

namespace DeviceControl.Core.DAL.TableModels
{
    public class LogTypeMap : ClassMap<LogTypeEntity>
    {
        public LogTypeMap()
        {
            Table("[db_scales].[LOG_TYPES]");
            LazyLoad();
            Id(x => x.Uid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
            Map(x => x.Number).CustomSqlType("tinyint").Column("NUMBER").Not.Nullable();
            Map(x => x.Icon).CustomSqlType("NVARCHAR(32)").Column("ICON").Length(32).Not.Nullable();
        }
    }
}
