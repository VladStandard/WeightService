// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableScaleModels
{
    public class OrderTypeMap : ClassMap<OrderTypeEntity>
    {
        public OrderTypeMap()
        {
            Table("[db_scales].[OrderTypes]");
            LazyLoad();
            Id(x => x.IdentityId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Description).CustomSqlType("NVARCHAR").Column("Description").Length(250).Nullable();
        }
    }
}
