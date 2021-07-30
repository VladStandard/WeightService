using FluentNHibernate.Mapping;

namespace DeviceControlCore.DAL.TableModels
{
    public class BarCodeTypesMap : ClassMap<BarCodeTypesEntity>
    {
        public BarCodeTypesMap()
        {
            Table("[db_scales].[BarCodeTypes]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(150)").Column("Name").Length(150).Nullable();
        }
    }
}
