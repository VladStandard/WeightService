using FluentNHibernate.Mapping;

namespace DeviceControl.Core.DAL.TableModels
{
    public class AppMap : ClassMap<AppEntity>
    {
        public AppMap()
        {
            Table("[db_scales].[APPS]");
            LazyLoad();
            Id(x => x.Uid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(32)").Column("NAME").Length(32).Not.Nullable();
        }
    }
}
