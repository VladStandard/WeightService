using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Users;

namespace Ws.Database.Nhibernate.Entities.Ref.Users;

internal sealed class SqlUserMap : ClassMapping<User>
{
    public SqlUserMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Users);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.GuidComb);
        });

        ManyToOne(x => x.ProductionSite, m =>
        {
            m.Column("PRODUCTION_SITE_UID");
            m.NotNullable(false);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}