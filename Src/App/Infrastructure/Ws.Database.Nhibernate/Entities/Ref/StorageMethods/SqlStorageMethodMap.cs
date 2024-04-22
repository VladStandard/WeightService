using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.StorageMethods;

internal sealed class SqlStorageMethodMap : ClassMapping<StorageMethodEntity>
{
    public SqlStorageMethodMap()
    {
        Schema("ZPL");
        Table(SqlTablesUtils.StorageMethods);

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

        Property(x => x.CreateDt, m =>
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
            m.Length(1024);
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });
    }
}