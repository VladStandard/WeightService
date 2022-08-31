// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDwhModels;

public class NomenclatureLightMap : ClassMap<NomenclatureLightModel>
{
    public NomenclatureLightMap()
    {
        Schema("DW");
        Table("DimNomenclatures");
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("DLM").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(x => x.Code).CustomSqlType("NVARCHAR").Length(15).Column("Code").Nullable();
        Map(x => x.Name).CustomSqlType("NVARCHAR").Length(150).Column("Name").Nullable();
        Map(x => x.Parents).CustomSqlType("NVARCHAR").Length(1024).Column("Parents").Nullable();
        Map(x => x.NameFull).CustomSqlType("NVARCHAR").Length(512).Column("NameFull").Nullable();
        Map(x => x.IsService).CustomSqlType("BIT").Column("IsService").Not.Nullable().Default("0");
        Map(x => x.IsProduct).CustomSqlType("BIT").Column("IsProduct").Not.Nullable().Default("0");
        Map(x => x.RelevanceStatus).CustomSqlType("TINYINT").Column("RelevanceStatus").Nullable();
        Map(x => x.NormalizationStatus).CustomSqlType("TINYINT").Column("NormalizationStatus").Nullable();
        Map(x => x.MasterId).CustomSqlType("INT").Column("MasterId").Nullable();
        References(x => x.InformationSystem).Column("InformationSystemID").Not.Nullable();
    }
}
