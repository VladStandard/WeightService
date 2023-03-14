// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.Plus;

/// <summary>
/// Table map "PLUS".
/// </summary>
public class PluMap : ClassMap<PluModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluMap()
    {
        Schema(SqlSchemaNamesUtils.DbScales);
        Table(SqlTableNamesUtils.Plus);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.IsGroup).CustomSqlType("BIT").Column("IS_GROUP").Not.Nullable().Default("0");
        Map(x => x.Number).CustomSqlType("INT").Column("NUMBER").Not.Nullable();
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(150).Not.Nullable();
        Map(x => x.FullName).CustomSqlType("NVARCHAR").Column("FULL_NAME").Not.Nullable();
        Map(x => x.Description).CustomSqlType("NVARCHAR").Column("DESCRIPTION").Not.Nullable();
        Map(x => x.ShelfLifeDays).CustomSqlType("TINYINT").Column("SHELF_LIFE_DAYS").Not.Nullable();
        Map(x => x.Gtin).CustomSqlType("NVARCHAR").Column("GTIN").Length(150).Not.Nullable().Default(string.Empty);
        Map(x => x.Ean13).CustomSqlType("NVARCHAR").Column("EAN13").Length(150).Not.Nullable().Default(string.Empty);
        Map(x => x.Itf14).CustomSqlType("NVARCHAR").Column("ITF14").Length(150).Not.Nullable().Default(string.Empty);
        Map(x => x.IsCheckWeight).CustomSqlType("BIT").Column("IS_CHECK_WEIGHT").Not.Nullable().Default("1");
        Map(x => x.Code).CustomSqlType("NVARCHAR").Column("CODE").Length(30).Not.Nullable();
        Map(x => x.Uid1c).CustomSqlType("UNIQUEIDENTIFIER").Column("UID_1C").Not.Nullable().Default(Guid.Empty.ToString());
    }
}