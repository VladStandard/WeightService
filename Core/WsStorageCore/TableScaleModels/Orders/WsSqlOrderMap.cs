// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Globalization;

namespace WsStorageCore.TableScaleModels.Orders;

/// <summary>
/// Table map "ORDERS".
/// </summary>
public sealed class WsSqlOrderMap : ClassMap<WsSqlOrderModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlOrderMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Orders);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType("NVARCHAR").Length(256).Column("NAME").Not.Nullable().Default("");
        Map(item => item.BeginDt).CustomSqlType("DATETIME").Column("BEGIN_DT").Not.Nullable().Default(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Map(item => item.EndDt).CustomSqlType("DATETIME").Column("END_DT").Not.Nullable().Default(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Map(item => item.ProdDt).CustomSqlType("DATETIME").Column("PROD_DT").Not.Nullable().Default(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Map(item => item.BoxCount).CustomSqlType("INT").Column("BOX_COUNT").Not.Nullable().Default("1");
        Map(item => item.PalletCount).CustomSqlType("INT").Column("PALLET_COUNT").Not.Nullable().Default("1");
    }
}
