using FluentNHibernate.Mapping;

namespace BlazorCore.DAL.TableModels
{
    public class LabelsMap : ClassMap<LabelsEntity>
    {
        public LabelsMap()
        {
            Table("[db_scales].[Labels]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            References(x => x.WeithingFact).Column("WeithingFactId").Not.Nullable();
            Map(x => x.Label).CustomSqlType("VARBINARY(MAX)").Column("Label").Nullable().Length(int.MaxValue);
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        }
    }
}
