// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataProjectsCore.DAL.TableScaleModels
{
    public class OrganizationMap : ClassMap<OrganizationEntity>
    {
        public OrganizationMap()
        {
            Table("[db_scales].[Organization]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(150)").Column("CategoryID").Length(150).Not.Nullable();
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
            Map(x => x.Gln).CustomSqlType("INT").Column("GLN").Not.Nullable();
            Map(x => x.SerializedRepresentationObject).CustomSqlType("XML").Column("SerializedRepresentationObject").Nullable();
        }
    }
}
