// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace BlazorCore.DAL.TableModels
{
    public class PluMap : ClassMap<PluEntity>
    {
        public PluMap()
        {
            Table("[db_scales].[PLU]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Nullable();
            References(x => x.Templates).Column("TemplateID").Nullable();
            References(x => x.Scale).Column("ScaleId").Not.Nullable();
            References(x => x.Nomenclature).Column("NomenclatureId").Not.Nullable();
            Map(x => x.GoodsName).CustomSqlType("NVARCHAR(150)").Column("GoodsName").Length(150).Nullable();
            Map(x => x.GoodsFullName).CustomSqlType("NVARCHAR(MAX)").Column("GoodsFullName").Nullable();
            Map(x => x.GoodsDescription).CustomSqlType("NVARCHAR(MAX)").Column("GoodsDescription").Nullable();
            Map(x => x.Gtin).CustomSqlType("VARCHAR(150)").Column("GTIN").Length(150).Nullable();
            Map(x => x.Ean13).CustomSqlType("VARCHAR(150)").Column("EAN13").Length(150).Nullable();
            Map(x => x.Itf14).CustomSqlType("VARCHAR(150)").Column("ITF14").Length(150).Nullable();
            Map(x => x.GoodsShelfLifeDays).CustomSqlType("TINYINT").Column("GoodsShelfLifeDays").Nullable();
            Map(x => x.GoodsTareWeight).CustomSqlType("DECIMAL(10,3)").Column("GoodsTareWeight").Nullable();
            Map(x => x.GoodsBoxQuantly).CustomSqlType("INT").Column("GoodsBoxQuantly").Nullable();
            Map(x => x.Plu).CustomSqlType("INT").Column("Plu").Not.Nullable();
            Map(x => x.Active).CustomSqlType("BIT").Column("Active").Nullable();
            Map(x => x.UpperWeightThreshold).CustomSqlType("DECIMAL(10,3)").Column("UpperWeightThreshold").Nullable();
            Map(x => x.NominalWeight).CustomSqlType("DECIMAL(10,3)").Column("NominalWeight").Nullable();
            Map(x => x.LowerWeightThreshold).CustomSqlType("DECIMAL(10,3)").Column("LowerWeightThreshold").Nullable();
            Map(x => x.CheckWeight).CustomSqlType("BIT").Column("CheckWeight").Nullable();
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        }
    }
}
