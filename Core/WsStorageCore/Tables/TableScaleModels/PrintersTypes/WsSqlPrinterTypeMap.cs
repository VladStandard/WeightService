// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.PrintersTypes;

/// <summary>
/// Table map "ZebraPrinterType".
/// </summary>
public sealed class WsSqlPrinterTypeMap : ClassMap<WsSqlPrinterTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPrinterTypeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PrintersTypes);
        LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("Name").Length(100).Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
    }
}
