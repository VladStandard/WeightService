using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Organizations;

public sealed class WsSqlOrganizationMap : ClassMapping<WsSqlOrganizationEntity>
{
    public WsSqlOrganizationMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Organizations);

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
            m.Length(256);
            m.NotNullable(true);
        });

        Property(x => x.Description, m =>
        {
            m.Column("DESCRIPTION");
            m.Type(NHibernateUtil.String);
            m.Length(256);
            m.NotNullable(true);
        });

        Property(x => x.Gln, m =>
        {
            m.Column("GLN");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });
    }
}
