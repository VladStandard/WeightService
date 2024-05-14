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
        });

        Property(x => x.Surname, m =>
        {
            m.Column("SURNAME");
            m.Type(NHibernateUtil.String);
        });

        Property(x => x.Patronymic, m =>
        {
            m.Column("PATRONYMIC");
            m.Type(NHibernateUtil.String);
        });

        Property(x => x.Password, m =>
        {
            m.Column("PASSWORD");
            m.Type(NHibernateUtil.String);
        });
    }
}