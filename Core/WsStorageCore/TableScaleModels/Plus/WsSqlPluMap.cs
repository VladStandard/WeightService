// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Plus;

/// <summary>
/// Table map "PLUS".
/// </summary>
public sealed class WsSqlPluMap : ClassMap<WsSqlPluModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Plus);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.IsGroup).CustomSqlType("BIT").Column("IS_GROUP").Not.Nullable().Default("0");
        Map(item => item.Number).CustomSqlType("INT").Column("NUMBER").Not.Nullable();
        Map(item => item.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(150).Not.Nullable();
        Map(item => item.FullName).CustomSqlType("NVARCHAR").Column("FULL_NAME").Not.Nullable();
        Map(item => item.Description).CustomSqlType("NVARCHAR").Column("DESCRIPTION").Not.Nullable();
        Map(item => item.ShelfLifeDays).CustomSqlType("TINYINT").Column("SHELF_LIFE_DAYS").Not.Nullable();
        Map(item => item.Gtin).CustomSqlType("NVARCHAR").Column("GTIN").Length(150).Not.Nullable().Default(string.Empty);
        Map(item => item.Ean13).CustomSqlType("NVARCHAR").Column("EAN13").Length(150).Not.Nullable().Default(string.Empty);
        Map(item => item.Itf14).CustomSqlType("NVARCHAR").Column("ITF14").Length(150).Not.Nullable().Default(string.Empty);
        Map(item => item.IsCheckWeight).CustomSqlType("BIT").Column("IS_CHECK_WEIGHT").Not.Nullable().Default("1");
        Map(item => item.Code).CustomSqlType("NVARCHAR").Column("CODE").Length(30).Not.Nullable();
        Map(item => item.Uid1C).CustomSqlType("UNIQUEIDENTIFIER").Column("UID_1C").Not.Nullable().Default(Guid.Empty.ToString());
    }
}