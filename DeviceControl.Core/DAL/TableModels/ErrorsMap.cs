using FluentNHibernate.Mapping;

namespace DeviceControl.Core.DAL.TableModels
{
    public class ErrorsMap : ClassMap<ErrorsEntity>
    {
        public ErrorsMap()
        {
            Table("[db_scales].[Errors]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreatedDate).CustomSqlType("DATETIME").Column("CreatedDate").Not.Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
            Map(x => x.FilePath).CustomSqlType("NVARCHAR(1024)").Column("FilePath").Length(1024).Nullable();
            Map(x => x.LineNumber).CustomSqlType("SMALLINT").Column("LineNumber").Nullable();
            Map(x => x.MemberName).CustomSqlType("NVARCHAR(128)").Column("MemberName").Length(128).Nullable();
            Map(x => x.Exception).CustomSqlType("NVARCHAR(4000)").Column("Exception").Length(4000).Not.Nullable();
            Map(x => x.InnerException).CustomSqlType("NVARCHAR(4000)").Column("InnerException").Length(4000).Nullable();
        }
    }
}
