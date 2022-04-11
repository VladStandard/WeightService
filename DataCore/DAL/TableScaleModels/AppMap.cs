// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableScaleModels
{
    public class AppMap : ClassMap<AppEntity>
    {
        public AppMap()
        {
            Table("[db_scales].[APPS]");
            LazyLoad();
            Id(x => x.IdentityUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(32).Not.Nullable();
            Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        }
    }
}
