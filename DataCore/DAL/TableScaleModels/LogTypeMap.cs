// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableScaleModels
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
