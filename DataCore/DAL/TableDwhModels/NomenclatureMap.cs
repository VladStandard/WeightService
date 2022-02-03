// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableDwhModels
{
    public class NomenclatureMap : ClassMap<NomenclatureEntity>
    {
        public NomenclatureMap()
        {
            Table("[DW].[DimNomenclatures]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Code).CustomSqlType("NVARCHAR(15)").Length(15).Column("Code").Nullable();
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked").Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(150)").Length(150).Column("Name").Nullable();
            Map(x => x.Parents).CustomSqlType("NVARCHAR(1024)").Length(1024).Column("Parents").Nullable();
            Map(x => x.Article).CustomSqlType("NVARCHAR(25)").Length(25).Column("Article").Nullable();
            Map(x => x.Weighted).CustomSqlType("BIT").Column("Weighted").Nullable();
            Map(x => x.GuidMercury).CustomSqlType("NVARCHAR(36)").Length(36).Column("GUID_Mercury").Nullable();
            Map(x => x.KeepTrackOfCharacteristics).CustomSqlType("BIT").Column("KeepTrackOfCharacteristics").Nullable();
            Map(x => x.NameFull).CustomSqlType("NVARCHAR(512)").Length(512).Column("NameFull").Nullable();
            Map(x => x.Comment).CustomSqlType("NVARCHAR(512)").Length(512).Column("Comment").Nullable();
            Map(x => x.IsService).CustomSqlType("BIT").Column("IsService").Nullable();
            Map(x => x.IsProduct).CustomSqlType("BIT").Column("IsProduct").Nullable();
            Map(x => x.AdditionalDescriptionOfNomenclature).CustomSqlType("NVARCHAR(MAX)").Length(int.MaxValue).Column("AdditionalDescriptionOfNomenclature").Nullable();
            Map(x => x.NomenclatureGroupCostBytes).CustomSqlType("VARBINARY(16)").Length(16).Column("NomenclatureGroupCost").Nullable();
            Map(x => x.NomenclatureGroupBytes).CustomSqlType("VARBINARY(16)").Length(16).Column("NomenclatureGroup").Nullable();
            Map(x => x.ArticleCost).CustomSqlType("VARBINARY(16)").Length(16).Column("ArticleCost").Nullable();
            Map(x => x.BrandBytes).CustomSqlType("VARBINARY(16)").Length(16).Column("Brand").Nullable();
            Map(x => x.NomenclatureTypeBytes).CustomSqlType("VARBINARY(16)").Length(16).Column("NomenclatureType").Nullable();
            Map(x => x.VatRate).CustomSqlType("NVARCHAR(10)").Length(10).Column("VATRate").Nullable();
            Map(x => x.Unit).CustomSqlType("NVARCHAR(150)").Length(150).Column("Unit").Nullable();
            Map(x => x.Weight).CustomSqlType("DECIMAL(15,3)").Column("Weight").Nullable();
            Map(x => x.BoxTypeId).CustomSqlType("BINARY(16)").Length(16).Column("boxTypeID").Nullable();
            Map(x => x.BoxTypeName).CustomSqlType("NVARCHAR(200)").Length(200).Column("boxTypeName").Nullable();
            Map(x => x.PackTypeId).CustomSqlType("BINARY(16)").Length(16).Column("packTypeID").Nullable();
            Map(x => x.PackTypeName).CustomSqlType("NVARCHAR(200)").Length(200).Column("packTypeName").Nullable();
            Map(x => x.SerializedRepresentationObject).CustomSqlType("XML").Column("SerializedRepresentationObject").Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.Dlm).CustomSqlType("DATETIME").Column("DLM").Not.Nullable();
            References(x => x.Status).Column("StatusID").Not.Nullable();
            References(x => x.InformationSystem).Column("InformationSystemID").Not.Nullable();
            Map(x => x.CodeInIs).CustomSqlType("VARBINARY(16)").Length(16).Column("CodeInIS").Not.Nullable();
            Map(x => x.RelevanceStatus).CustomSqlType("TINYINT").Column("RelevanceStatus").Nullable();
            Map(x => x.NormalizationStatus).CustomSqlType("TINYINT").Column("NormalizationStatus").Nullable();
            Map(x => x.MasterId).CustomSqlType("INT").Column("MasterId").Nullable();
        }
    }
}
