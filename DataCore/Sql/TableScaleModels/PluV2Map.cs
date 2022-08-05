// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "PLU_V2".
/// </summary>
public class PluV2Map : ClassMap<PluV2Entity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluV2Map()
    {
        Schema("db_scales");
        Table("PLU_V2");
        LazyLoad();
        Id(x => x.IdentityUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Number).CustomSqlType("INT").Column("NUMBER").Not.Nullable();
		Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(150).Not.Nullable();
		Map(x => x.FullName).CustomSqlType("NVARCHAR").Column("FULL_NAME").Not.Nullable();
		Map(x => x.Description).CustomSqlType("NVARCHAR").Column("DESCRIPTION").Not.Nullable();
		Map(x => x.ShelfLifeDays).CustomSqlType("TINYINT").Column("SHELF_LIFE_DAYS").Not.Nullable();
		Map(x => x.TareWeight).CustomSqlType("DECIMAL(10,3)").Column("TARE_WEIGHT").Not.Nullable();
		Map(x => x.BoxQuantly).CustomSqlType("INT").Column("BOX_QUANTLY").Not.Nullable();
		Map(x => x.Gtin).CustomSqlType("NVARCHAR").Column("GTIN").Length(150).Not.Nullable().Default(string.Empty);
        Map(x => x.Ean13).CustomSqlType("NVARCHAR").Column("EAN13").Length(150).Not.Nullable().Default(string.Empty);
        Map(x => x.Itf14).CustomSqlType("NVARCHAR").Column("ITF14").Length(150).Not.Nullable().Default(string.Empty);
        Map(x => x.UpperThreshold).CustomSqlType("DECIMAL(10,3)").Column("UPPER_THRESHOLD").Not.Nullable();
		Map(x => x.NominalWeight).CustomSqlType("DECIMAL(10,3)").Column("NOMINAL_WEIGHT").Not.Nullable();
		Map(x => x.LowerThreshold).CustomSqlType("DECIMAL(10,3)").Column("LOWER_THRESHOLD").Not.Nullable();
		Map(x => x.IsCheckWeight).CustomSqlType("BIT").Column("IS_CHECK_WEIGHT").Not.Nullable().Default("1");
        References(x => x.Template).Column("TEMPLATE_ID").Not.Nullable();
        References(x => x.Nomenclature).Column("NOMENCLATURE_ID").Not.Nullable();
    }
}
