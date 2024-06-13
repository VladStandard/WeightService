using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Nestings;

internal sealed class SqlPluNestingFkMap : ClassMapping<PluNesting>
{
    public SqlPluNestingFkMap()
    {
        Schema(SqlSchemasUtils.Ref1C);
        Table(SqlTablesUtils.Nestings);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Generator(Generators.Foreign<Plu>(i => i.Uid));
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