using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Ref.ZplResources;

internal sealed class SqlZplResourceMap : ClassMapping<ZplResource>
{
    public SqlZplResourceMap()
    {
        Schema("ZPL");
        Table(SqlTablesUtils.ZplResources);
        Lazy(false);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.GuidComb);
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

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(32);
            m.NotNullable(true);
        });

        Property(x => x.Zpl, m =>
        {
            m.Column("ZPL");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });
    }
}