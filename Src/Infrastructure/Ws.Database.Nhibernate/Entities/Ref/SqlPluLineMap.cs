using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Devices.Arms;

namespace Ws.Database.Nhibernate.Entities.Ref;

internal sealed class SqlPluLineMap : ClassMapping<ArmPlu>
{
    public SqlPluLineMap()
    {
        Table(SqlTablesUtils.PlusLines);

        ComposedId(map =>
        {
            map.ManyToOne(x => x.Plu, m =>
            {
                m.Column("PLU_UID");
                m.Lazy(LazyRelation.NoLazy);
            });

            map.ManyToOne(x => x.Line, m =>
            {
                m.Column("ARM_UID");
                m.Lazy(LazyRelation.NoLazy);
            });
        });

        ManyToOne(x => x.Plu, m =>
        {
            m.Column("PLU_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Line, m =>
        {
            m.Column("ARM_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}