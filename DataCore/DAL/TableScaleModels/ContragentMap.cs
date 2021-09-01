// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableModels
{
    public class ContragentMap : ClassMap<ContragentEntity>
    {
        public ContragentMap()
        {
            Table("[db_scales].[Contragents]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate");
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate");
            Map(x => x.Name).CustomSqlType("NVARCHAR(150)").Column("Name").Not.Nullable().Length(150);
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked");
            Map(x => x.SerializedRepresentationObject).CustomSqlType("XML").Column("SerializedRepresentationObject").Nullable();
        }
    }
}
