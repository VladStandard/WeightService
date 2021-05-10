using FluentNHibernate.Mapping;

namespace DeviceControl.Core.DAL.TableModels
{
    public class TemplateResourcesMap : ClassMap<TemplateResourcesEntity>
    {
        public TemplateResourcesMap()
        {
            Table("[db_scales].[TemplateResources]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(50)").Column("Name").Length(50).Nullable();
            Map(x => x.Description).CustomSqlType("NVARCHAR(500)").Column("Description").Length(500).Nullable();
            Map(x => x.Type).CustomSqlType("VARCHAR(3)").Column("Type").Length(3).Nullable();
            Map(x => x.ImageData).CustomSqlType("VARBINARY(MAX)").Column("ImageData").Nullable().Length(int.MaxValue);
            Map(x => x.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IdRRef").Nullable();
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        }
    }
}
