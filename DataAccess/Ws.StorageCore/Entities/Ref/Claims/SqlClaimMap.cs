using Ws.Domain.Models.Entities.Ref;
using Ws.Infrastructure.Models.Utils;

namespace Ws.StorageCore.Entities.Ref.Claims;

public sealed class SqlClaimMap : ClassMapping<ClaimEntity>
{
    public SqlClaimMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Claims);

        Id(x => x.IdentityValueUid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.CreateDt, m =>
        {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(50);
            m.NotNullable(true);
        });
    }
}
