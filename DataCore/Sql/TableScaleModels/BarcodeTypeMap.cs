// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;
using System;

namespace DataCore.Sql.TableScaleModels
{
    [Obsolete(@"Use BarcodeTypeMapV2")]
    public class BarcodeTypeMap : ClassMap<BarcodeTypeEntity>
    {
        public BarcodeTypeMap()
        {
            Table("[db_scales].[BarCodeTypes]");
            LazyLoad();
            Id(x => x.IdentityId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
            Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Length(150).Nullable();
        }
    }
}
