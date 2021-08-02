//using FluentNHibernate.Mapping;

//namespace BlazorCore.DAL.TableModels
//{
//    public class ContragentsCountMap : ClassMap<ContragentsCountEntity>
//    {
//        public ContragentsCountMap()
//        {
//            Table("[db_scales].[Contragents]");
//            LazyLoad();
//            //Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
//            Map(x => x.CreatedDate).CustomSqlType("DATETIME").Column("CreatedDate").Nullable();
//            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Nullable();
//            Map(x => x.Count).CustomSqlType("UNIQUEIDENTIFIER").Column("Count");
//        }
//    }
//}
