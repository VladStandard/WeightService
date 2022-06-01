// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.Sql.TableScaleModels
{
    public class SystemMap : ClassMap<SystemEntity>
    {
        public SystemMap()
        {
            Table("[db_scales].[SYSTEM]");
            LazyLoad();
            Id(x => x.IdentityUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
            Map(x => x.ReleaseDt).CustomSqlType("DATETIME").Column("RELEASE_DT").Not.Nullable();
            Map(x => x.Version).CustomSqlType("SMALLINT").Column("VERSION").Not.Nullable();
            Map(x => x.Description).CustomSqlType("NVARCHAR").Length(128).Column("DESCRIPTION").Not.Nullable();
        }
    }
}
