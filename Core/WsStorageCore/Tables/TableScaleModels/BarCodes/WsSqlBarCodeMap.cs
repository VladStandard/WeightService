using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.BarCodes;

[DebuggerDisplay("{ToString()}")]
public sealed class WsSqlBarCodeMap : ClassMap<WsSqlBarCodeModel>
{
    public WsSqlBarCodeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.BarCodes);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.TypeTop).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("TYPE_TOP").Length(128).Not.Nullable();
        Map(item => item.ValueTop).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("VALUE_TOP").Length(128).Not.Nullable();
        Map(item => item.TypeRight).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("TYPE_RIGHT").Length(128).Not.Nullable();
        Map(item => item.ValueRight).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("VALUE_RIGHT").Length(128).Not.Nullable();
        Map(item => item.TypeBottom).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("TYPE_BOTTOM").Length(128).Not.Nullable();
        Map(item => item.ValueBottom).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("VALUE_BOTTOM").Length(128).Not.Nullable();
        References(item => item.PluLabel).Column("PLU_LABEL_UID").Not.Nullable();
    }
}
