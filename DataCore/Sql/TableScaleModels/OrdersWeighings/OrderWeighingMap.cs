// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.OrdersWeighings;

/// <summary>
/// Table map "ORDERS_WEIGHINGS".
/// </summary>
public class OrderWeighingMap : ClassMap<OrderWeighingModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public OrderWeighingMap()
    {
        Schema("db_scales");
        Table("ORDERS_WEIGHINGS");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.Order).Column("ORDER_UID").Not.Nullable();
        References(x => x.PluWeighing).Column("PLU_WEIGHING_UID").Not.Nullable();
    }
}
