using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.TemplatesResources;

/// <summary>
/// Table map "TEMPLATES_RESOURCES".
/// </summary>
public sealed class WsSqlTemplateResourceMap : ClassMapping<WsSqlTemplateResourceModel>
{
    public WsSqlTemplateResourceMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.TemplatesResources);
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

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(64);
            m.NotNullable(true);
        });

        Property(x => x.Type, m =>
        {
            m.Column("TYPE");
            m.Type(NHibernateUtil.String);
            m.Length(8);
            m.NotNullable(true);
        });

        Property(x => x.DataValue, m =>
        {
            m.Column("DATA");
            m.Type(NHibernateUtil.BinaryBlob);
            m.NotNullable(true);
            m.Length(int.MaxValue);
        });
    }
}