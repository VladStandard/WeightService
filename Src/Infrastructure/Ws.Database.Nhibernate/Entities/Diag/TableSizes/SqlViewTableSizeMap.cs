using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Nhibernate.Entities.Diag.TableSizes;

internal class SqlViewTableSizeMap : ClassMapping<TableSize>
{
    public SqlViewTableSizeMap()
    {
        Schema(SqlSchemasUtils.Diag);
        Table(SqlTablesUtils.ViewTablesSizes);
        Mutable(false);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
        });

        Property(x => x.Schema, m =>
        {
            m.Column("[SCHEMA]");
            m.Type(NHibernateUtil.String);
        });

        Property(x => x.Table, m =>
        {
            m.Column("[TABLE]");
            m.Type(NHibernateUtil.String);
        });

        Property(x => x.RowsCount, m =>
        {
            m.Column("ROWS_COUNT");
            m.Type(NHibernateUtil.Int64);
        });

        Property(x => x.UsedSpaceMb, m =>
        {
            m.Column("USED_SPACE_MB");
            m.Type(NHibernateUtil.Int32);
        });

        Property(x => x.FileName, m =>
        {
            m.Column("FILENAME");
            m.Type(NHibernateUtil.String);
        });
    }
}