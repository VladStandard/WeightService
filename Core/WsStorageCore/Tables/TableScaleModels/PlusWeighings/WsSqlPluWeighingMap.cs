using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.PlusWeighings;

public sealed class WsSqlPluWeighingMap : ClassMapping<WsSqlPluWeighingModel>
{
    public WsSqlPluWeighingMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusWeightings);

        Id(x => x.IdentityValueUid, m => {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.CreateDt, m => {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.ChangeDt, m => {
            m.Column("CHANGE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.IsMarked, m => {
            m.Column("IS_MARKED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.Kneading, m => {
            m.Column("KNEADING");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(true);
        });

        Property(x => x.NettoWeight, m => {
            m.Column("NETTO_WEIGHT");
            m.Type(NHibernateUtil.Decimal);
            m.Precision(10);
            m.Scale(3);
            m.NotNullable(true);
        });

        Property(x => x.WeightTare, m => {
            m.Column("TARE_WEIGHT");
            m.Type(NHibernateUtil.Decimal);
            m.Precision(10);
            m.Scale(3);
            m.NotNullable(true);
        });

        ManyToOne(x => x.PluScale, m => {
            m.Column("PLU_SCALE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}
