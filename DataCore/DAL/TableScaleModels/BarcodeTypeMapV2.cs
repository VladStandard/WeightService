// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableScaleModels
{
    public class BarCodeTypeMapV2 : ClassMap<BarCodeTypeEntityV2>
    {
        public BarCodeTypeMapV2()
        {
            Table("[db_scales].[BARCODE_TYPES_V2]");
            LazyLoad();
            Id(x => x.Uid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
            Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
            Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
            Map(x => x.IsMarked).CustomSqlType("BIT").Column("MARKED").Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(100).Not.Nullable();
        }
    }
}
