namespace WsStorageCore.Entities.SchemaRef.Hosts;

public sealed class SqlHostMap : ClassMapping<SqlHostEntity>
{
    public SqlHostMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Hosts);

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

        Property(x => x.ChangeDt, m =>
        {
            m.Column("CHANGE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.LoginDt, m =>
        {
            m.Column("LOGIN_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });
        
        Property(x => x.IsMarked, m =>
        {
            m.Column("IS_MARKED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });

        Property(x => x.Ip, m =>
        {
            m.Column("IP");
            m.Type(NHibernateUtil.String);
            m.Length(15);
        });
    }
}
