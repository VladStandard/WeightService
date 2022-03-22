// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableDwhModels
{
    public class NomenclatureLightMap : ClassMap<NomenclatureLightEntity>
    {
        public NomenclatureLightMap()
        {
            Table("[DW].[DimNomenclatures]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Code).CustomSqlType("NVARCHAR").Length(15).Column("Code").Nullable();
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked").Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR").Length(150).Column("Name").Nullable();
            Map(x => x.Parents).CustomSqlType("NVARCHAR").Length(1024).Column("Parents").Nullable();
            Map(x => x.NameFull).CustomSqlType("NVARCHAR").Length(512).Column("NameFull").Nullable();
            Map(x => x.IsService).CustomSqlType("BIT").Column("IsService").Nullable();
            Map(x => x.IsProduct).CustomSqlType("BIT").Column("IsProduct").Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.Dlm).CustomSqlType("DATETIME").Column("DLM").Not.Nullable();
            References(x => x.InformationSystem).Column("InformationSystemID").Not.Nullable();
            Map(x => x.RelevanceStatus).CustomSqlType("TINYINT").Column("RelevanceStatus").Nullable();
            Map(x => x.NormalizationStatus).CustomSqlType("TINYINT").Column("NormalizationStatus").Nullable();
            Map(x => x.MasterId).CustomSqlType("INT").Column("MasterId").Nullable();
        }
    }
}
