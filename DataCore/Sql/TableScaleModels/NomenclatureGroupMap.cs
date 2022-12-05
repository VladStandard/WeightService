// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "NOMENCLATURES_GROUPS".
/// </summary>
public class NomenclatureGroupMap : ClassMap<NomenclatureGroupModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public NomenclatureGroupMap()
    {
        Schema("db_scales");
        Table("NOMENCLATURES_GROUPS");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.IsGroup).CustomSqlType("BIT").Column("IS_GROUP").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(128).Not.Nullable();
        Map(x => x.Description).CustomSqlType("NVARCHAR").Column("DESCRIPTION").Length(1024).Not.Nullable();
        Map(x => x.Code).CustomSqlType("NVARCHAR").Column("CODE").Length(128).Not.Nullable();
    }
}
