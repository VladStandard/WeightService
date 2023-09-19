using System.Globalization;

namespace WsStorageCore.Tables.TableScaleModels.Orders;

public sealed class WsSqlOrderMap : ClassMap<WsSqlOrderModel>
{
    public WsSqlOrderMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Orders);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Length(256).Column("NAME").Not.Nullable().Default("");
        Map(item => item.BeginDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("BEGIN_DT").Not.Nullable().Default(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Map(item => item.EndDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("END_DT").Not.Nullable().Default(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Map(item => item.ProdDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("PROD_DT").Not.Nullable().Default(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Map(item => item.BoxCount).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("BOX_COUNT").Not.Nullable().Default("1");
        Map(item => item.PalletCount).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("PALLET_COUNT").Not.Nullable().Default("1");
    }
}
