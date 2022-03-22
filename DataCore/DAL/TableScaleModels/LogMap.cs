// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableScaleModels
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
            Map(x => x.Version).CustomSqlType("NVARCHAR").Column("VERSION").Length(12).Nullable();
            Map(x => x.File).CustomSqlType("NVARCHAR").Column("FILE").Length(32).Not.Nullable();
            Map(x => x.Line).CustomSqlType("INT").Column("LINE").Not.Nullable();
            Map(x => x.Member).CustomSqlType("NVARCHAR").Column("MEMBER").Length(32).Not.Nullable();
            References(x => x.LogType).Column("LOG_TYPE_UID").Nullable();
            Map(x => x.Message).CustomSqlType("NVARCHAR").Column("MESSAGE").Length(1024).Not.Nullable();
        }
    }
}
