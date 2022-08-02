// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "OrderTypes".
/// </summary>
public class OrderTypeMap : ClassMap<OrderTypeEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public OrderTypeMap()
    {
        Schema("db_scales");
        Table("OrderTypes");
        LazyLoad();
        Id(x => x.IdentityId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Description).CustomSqlType("NVARCHAR").Column("Description").Length(250).Nullable();
    }
}
