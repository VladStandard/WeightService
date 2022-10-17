// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "BARCODES".
/// </summary>
public class BarCodeMap : ClassMap<BarCodeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public BarCodeMap()
    {
        Schema("db_scales");
        Table("BARCODES");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.TypeTop).CustomSqlType("VARCHAR").Column("TYPE_TOP").Length(128).Not.Nullable();
        Map(x => x.ValueTop).CustomSqlType("VARCHAR").Column("VALUE_TOP").Length(128).Not.Nullable();
        Map(x => x.TypeRight).CustomSqlType("VARCHAR").Column("TYPE_RIGHT").Length(128).Not.Nullable();
        Map(x => x.ValueRight).CustomSqlType("VARCHAR").Column("VALUE_RIGHT").Length(128).Not.Nullable();
        Map(x => x.TypeBottom).CustomSqlType("VARCHAR").Column("TYPE_BOTTOM").Length(128).Not.Nullable();
        Map(x => x.ValueBottom).CustomSqlType("VARCHAR").Column("VALUE_BOTTOM").Length(128).Not.Nullable();
        References(x => x.PluLabel).Column("PLU_LABEL_UID").Not.Nullable();
    }
}
