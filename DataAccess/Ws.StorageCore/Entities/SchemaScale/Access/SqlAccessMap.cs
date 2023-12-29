namespace Ws.StorageCore.Entities.SchemaScale.Access;

public sealed class SqlAccessMap : ClassMapping<SqlAccessEntity>
{
    public SqlAccessMap()
    {
        Schema(SqlSchemasUtils.DbScales);
        Table(SqlTablesUtils.Access);

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

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(32);
            m.NotNullable(true);
        });

        Property(x => x.Rights, m =>
        {
            m.Column("RIGHTS");
            m.Type(NHibernateUtil.Byte);
            m.NotNullable(true);
        });
    }
}