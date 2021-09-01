// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableSystemModels
{
    public class AccessMap : ClassMap<AccessEntity>
    {
        public AccessMap()
        {
            Table("[db_scales].[ACCESS]");
            LazyLoad();
            Id(x => x.Uid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
            Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
            Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
            Map(x => x.User).CustomSqlType("NVARCHAR(32)").Column("USER").Length(32).Not.Nullable();
            Map(x => x.Level).CustomSqlType("BIT").Column("LEVEL").Nullable();
        }
    }
}
