// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "BARCODE_TYPES_V2".
/// </summary>
public class BarCodeTypeMapV2 : ClassMap<BarCodeTypeEntityV2>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public BarCodeTypeMapV2()
    {
        Schema("db_scales");
        Table("BARCODE_TYPES_V2");
        LazyLoad();
        Id(x => x.IdentityUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(100).Not.Nullable();
    }
}
