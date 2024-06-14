using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Users;

namespace Ws.Database.Nhibernate.Entities.Ref.PalletMen;

internal sealed class SqlPalletManMap : ClassMapping<PalletMan>
{
    public SqlPalletManMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.PalletMen);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.GuidComb);
        });

        Property(x => x.Uid1C, m =>
        {
            m.Column("UID_1C");
            m.Type(NHibernateUtil.Guid);
            m.NotNullable(true);
            m.Unique(true);
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

        Component(x => x.Fio, m =>
        {
            m.Property(y => y.Name, mm =>
            {
                mm.Column("NAME");
                mm.Type(NHibernateUtil.String);
            });

            m.Property(y => y.Surname, mm =>
            {
                mm.Column("SURNAME");
                mm.Type(NHibernateUtil.String);
            });

            m.Property(y => y.Patronymic, mm =>
            {
                mm.Column("PATRONYMIC");
                mm.Type(NHibernateUtil.String);
            });
        });

        Property(x => x.Password, m =>
        {
            m.Column("PASSWORD");
            m.Type(NHibernateUtil.String);
        });

        ManyToOne(x => x.Warehouse, m =>
        {
            m.Column("WAREHOUSE_UID");
            m.Lazy(LazyRelation.NoLazy);
            m.NotNullable(true);
        });
    }
}