using FluentNHibernate.Mapping;

namespace BlazorCore.DAL.TableModels
{
    public class LogMap : ClassMap<LogEntity>
    {
        public LogMap()
        {
            Table("[db_scales].[LOGS]");
            LazyLoad();
            Id(x => x.Uid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
            Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
            References(x => x.Host).Column("HOST_ID").Nullable();
            References(x => x.App).Column("APP_UID").Nullable();
            Map(x => x.Version).CustomSqlType("NVARCHAR(12)").Column("VERSION").Length(12).Nullable();
            Map(x => x.File).CustomSqlType("NVARCHAR(32)").Column("FILE").Length(32).Not.Nullable();
            Map(x => x.Line).CustomSqlType("INT").Column("LINE").Not.Nullable();
            Map(x => x.Member).CustomSqlType("NVARCHAR(32)").Column("MEMBER").Length(32).Not.Nullable();
            References(x => x.LogType).Column("LOG_TYPE_UID").Nullable();
            Map(x => x.Message).CustomSqlType("NVARCHAR(1024)").Column("MESSAGE").Length(1024).Not.Nullable();
        }
    }
}
