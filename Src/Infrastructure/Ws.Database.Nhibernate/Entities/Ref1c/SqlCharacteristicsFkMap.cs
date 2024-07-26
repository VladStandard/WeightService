using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Ref1c.Plus;

namespace Ws.Database.Nhibernate.Entities.Ref1c;

internal sealed class SqlCharacteristicsFkMap : ClassMapping<PluCharacteristic>
{
    public SqlCharacteristicsFkMap()
    {
        Schema(SqlSchemasUtils.Ref1C);
        Table(SqlTablesUtils.Characteristics);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.GuidComb);
        });


        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
        });

        Property(x => x.BundleCount, m =>
        {
            m.Column("BUNDLE_COUNT");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(true);
            m.Unique(true);
        });

        ManyToOne(x => x.Box, m =>
        {
            m.Column("BOX_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        Property(x => x.PluUid, m =>
        {
            m.Column("PLU_UID");
            m.Type(NHibernateUtil.Guid);
        });

        Property(x => x.CreateDt, m =>
        {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.ChangeDt, m =>
        {
            m.Column("CHANGE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });
    }
}