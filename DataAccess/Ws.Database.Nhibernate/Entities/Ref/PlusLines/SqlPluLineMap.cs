using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.PlusLines;

internal sealed class SqlPluLineMap : ClassMapping<PluLineEntity>
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
                m.Column("LINE_UID");
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
            m.Column("LINE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}