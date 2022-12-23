// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.Nomenclatures;

/// <summary>
/// Table map "Nomenclature".
/// </summary>
[Obsolete(@"Use NomenclatureV2Map")]
public class NomenclatureMap : ClassMap<NomenclatureModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public NomenclatureMap()
    {
        Schema("db_scales");
        Table("Nomenclature");
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Code).CustomSqlType("NVARCHAR").Column("Code").Length(30);
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Length(300);
        Map(x => x.Xml).CustomSqlType("XML").Column("SerializedRepresentationObject").Nullable();
        Map(x => x.Weighted).CustomSqlType("BIT").Column("Weighted").Not.Nullable().Default("0");
        Map(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Not.Nullable().Default(Guid.Empty.ToString());
    }
}
