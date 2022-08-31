//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCore.Sql.TableScaleModels;

///// <summary>
///// Table map "WeithingFact".
///// </summary>
//public class WeithingFactMap : ClassMap<WeithingFactEntity>
//{
//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    public WeithingFactMap()
//    {
//        Schema("db_scales");
//        Table("WeithingFact");
//        LazyLoad();
//        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
//        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
//        Map(x => x.Sscc).CustomSqlType("VARCHAR").Column("SSCC").Length(50).Not.Nullable();
//        Map(x => x.WeithingDate).CustomSqlType("DATETIME(2,7)").Column("WeithingDate").Nullable();
//        Map(x => x.NetWeight).CustomSqlType("NUMERIC(15,3)").Column("NetWeight").Not.Nullable();
//        Map(x => x.TareWeight).CustomSqlType("NUMERIC(15,3)").Column("TareWeight").Nullable();
//        Map(x => x.Identity.Uid).CustomSqlType("UNIQUEIDENTIFIER").Column("UUID").Nullable();
//        Map(x => x.ProductDate).CustomSqlType("DATE").Column("ProductDate").Nullable();
//        Map(x => x.RegNum).CustomSqlType("INT").Column("RegNum").Nullable();
//        Map(x => x.Kneading).CustomSqlType("INT").Column("Kneading").Nullable();
//        References(x => x.Plu).Column("PluId").Not.Nullable();
//        References(x => x.Scale).Column("ScaleId").Not.Nullable();
//        References(x => x.Serie).Column("SeriesId").Not.Nullable();
//    }
//}
