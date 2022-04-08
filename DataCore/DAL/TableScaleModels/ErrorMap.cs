// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableScaleModels
{
    public class ErrorMap : ClassMap<ErrorEntity>
    {
        public ErrorMap()
        {
            Table("[db_scales].[Errors]");
            LazyLoad();
            Id(x => x.IdentityId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreatedDate").Not.Nullable();
            Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
            Map(x => x.FilePath).CustomSqlType("NVARCHAR").Column("FilePath").Length(1024).Nullable();
            Map(x => x.LineNumber).CustomSqlType("SMALLINT").Column("LineNumber").Nullable();
            Map(x => x.MemberName).CustomSqlType("NVARCHAR").Column("MemberName").Length(128).Nullable();
            Map(x => x.Exception).CustomSqlType("NVARCHAR").Column("Exception").Length(4000).Not.Nullable();
            Map(x => x.InnerException).CustomSqlType("NVARCHAR").Column("InnerException").Length(4000).Nullable();
        }
    }
}
