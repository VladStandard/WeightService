// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Globalization;

namespace DataCore.Sql.TableScaleModels.Orders;

/// <summary>
/// Table map "ORDERS".
/// </summary>
public class OrderMap : ClassMap<OrderModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public OrderMap()
    {
        Schema("db_scales");
        Table(SqlTableNamesUtils.Orders);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Length(256).Column("NAME").Not.Nullable().Default("");
        Map(x => x.BeginDt).CustomSqlType("DATETIME").Column("BEGIN_DT").Not.Nullable().Default(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Map(x => x.EndDt).CustomSqlType("DATETIME").Column("END_DT").Not.Nullable().Default(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Map(x => x.ProdDt).CustomSqlType("DATETIME").Column("PROD_DT").Not.Nullable().Default(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Map(x => x.BoxCount).CustomSqlType("INT").Column("BOX_COUNT").Not.Nullable().Default("1");
        Map(x => x.PalletCount).CustomSqlType("INT").Column("PALLET_COUNT").Not.Nullable().Default("1");
    }
}
