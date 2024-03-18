using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.Users;

internal sealed class SqlUserMap : ClassMapping<UserEntity>
{
    public SqlUserMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Users);

        Id(x => x.Uid, m => {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.CreateDt, m => {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.LoginDt, m => {
            m.Column("LOGIN_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.Name, m => {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });
        
        ManyToOne(x => x.ProductionSite, m => {
            m.Column("PRODUCTION_SITE_UID");
            m.NotNullable(false);
            m.Lazy(LazyRelation.NoLazy);
        });
        
        Set(x => x.Claims, m => {
            m.Schema(SqlSchemasUtils.Ref);
            m.Table("USERS_CLAIMS_FK");
            m.Cascade(Cascade.Detach);
            m.Lazy(CollectionLazy.NoLazy);
            m.Inverse(false);
            m.Key(k => k.Column("USER_UID"));
        }, r => r.ManyToMany(a => a.Column("CLAIM_UID")));
    }
}