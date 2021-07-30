//using FluentNHibernate.Mapping;

//namespace DeviceControlCore.DAL.DataModels
//{
//    public class DeviceMap : ClassMap<DeviceEntity>
//    {
//        public DeviceMap()
//        {
//            Table("[db_scales].[Scales]");
//            LazyLoad();
//            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity();
//            References(x => x.Scales).Column("Id");
//        }
//    }
//}
