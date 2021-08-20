// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace BlazorCore.DAL.TableModels
{
    public class WeithingFactMap : ClassMap<WeithingFactEntity>
    {
        public WeithingFactMap()
        {
            Table("[db_scales].[WeithingFact]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            References(x => x.Plu).Column("PluId").Not.Nullable();
            References(x => x.Scales).Column("ScaleId").Not.Nullable();
            References(x => x.Series).Column("SeriesId").Not.Nullable();
            References(x => x.Orders).Column("OrderId").Not.Nullable();
            Map(x => x.Sscc).CustomSqlType("VARCHAR(50)").Column("SSCC").Length(50).Not.Nullable();
            Map(x => x.WeithingDate).CustomSqlType("DATETIME(2,7)").Column("WeithingDate").Nullable();
            Map(x => x.NetWeight).CustomSqlType("NUMERIC(15,3)").Column("NetWeight").Not.Nullable();
            Map(x => x.TareWeight).CustomSqlType("NUMERIC(15,3)").Column("TareWeight").Nullable();
            Map(x => x.Uid).CustomSqlType("UNIQUEIDENTIFIER").Column("UUID").Nullable();
            Map(x => x.ProductDate).CustomSqlType("DATE").Column("ProductDate").Nullable();
            Map(x => x.RegNum).CustomSqlType("INT").Column("RegNum").Nullable();
            Map(x => x.Kneading).CustomSqlType("INT").Column("Kneading").Nullable();
        }
    }
}
