// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.BarCodes;

/// <summary>
/// Table map "BARCODES".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class WsSqlBarCodeMap : ClassMap<WsSqlBarCodeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlBarCodeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.BarCodes);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.TypeTop).CustomSqlType("VARCHAR").Column("TYPE_TOP").Length(128).Not.Nullable();
        Map(item => item.ValueTop).CustomSqlType("VARCHAR").Column("VALUE_TOP").Length(128).Not.Nullable();
        Map(item => item.TypeRight).CustomSqlType("VARCHAR").Column("TYPE_RIGHT").Length(128).Not.Nullable();
        Map(item => item.ValueRight).CustomSqlType("VARCHAR").Column("VALUE_RIGHT").Length(128).Not.Nullable();
        Map(item => item.TypeBottom).CustomSqlType("VARCHAR").Column("TYPE_BOTTOM").Length(128).Not.Nullable();
        Map(item => item.ValueBottom).CustomSqlType("VARCHAR").Column("VALUE_BOTTOM").Length(128).Not.Nullable();
        References(item => item.PluLabel).Column("PLU_LABEL_UID").Not.Nullable();
    }
}
