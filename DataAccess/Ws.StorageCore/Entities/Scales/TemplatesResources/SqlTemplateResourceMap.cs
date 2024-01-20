using Ws.Domain.Models.Entities.SchemaScale;
using Ws.Infrastructure.Models.Utils;

namespace Ws.StorageCore.Entities.Scales.TemplatesResources;

public sealed class SqlTemplateResourceMap : ClassMapping<TemplateResourceEntity>
{
    public SqlTemplateResourceMap()
    {
        Schema(SqlSchemasUtils.DbScales);
        Table(SqlTablesUtils.TemplatesResources);
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