using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Versions;

/// <summary>
/// Table map "VERSIONS".
/// </summary>
public sealed class WsSqlVersionMap : ClassMapping<WsSqlVersionEntity>
{
    public WsSqlVersionMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Versions);
        Lazy(false);

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

        Property(x => x.ReleaseDt, m =>
        {
            m.Column("RELEASE_DT");
            m.Type(NHibernateUtil.Date);
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

        Property(x => x.Version, m =>
        {
            m.Column("VERSION");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(true);
        });
    }
}
