namespace Ws.StorageCore.Entities.SchemaRef.Users;

public sealed class SqlUserMap : ClassMapping<SqlUserEntity>
{
    public SqlUserMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Users);

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
        
        Property(x => x.LoginDt, m =>
        {
            m.Column("LOGIN_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });
    }
}
