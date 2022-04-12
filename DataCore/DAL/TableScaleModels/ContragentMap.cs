// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;
using System;

namespace DataCore.DAL.TableScaleModels
{
    [Obsolete(@"Use ContragentMapV2")]
    public class ContragentMap : ClassMap<ContragentEntity>
    {
        public ContragentMap()
        {
            Table("[db_scales].[Contragents]");
            LazyLoad();
            Id(x => x.IdentityId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate");
            Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate");
            Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
            Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Length(150).Not.Nullable();
            Map(x => x.SerializedRepresentationObject).CustomSqlType("XML").Column("SerializedRepresentationObject").Nullable();
        }
    }
}
