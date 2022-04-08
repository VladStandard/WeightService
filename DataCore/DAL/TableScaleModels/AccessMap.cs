// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableScaleModels
{
    public class AccessMap : ClassMap<AccessEntity>
    {
        public AccessMap()
        {
            Table("[db_scales].[ACCESS]");
            LazyLoad();
            Id(x => x.IdentityUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
            Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
            Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
            Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
            Map(x => x.User).CustomSqlType("NVARCHAR").Column("USER").Length(32).Not.Nullable();
            Map(x => x.Rights).CustomSqlType("TINYINT").Column("RIGHTS").Not.Nullable().Default("0");
        }
    }
}
