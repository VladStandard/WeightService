using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.TemplatesResources;

internal sealed class SqlTemplateResourceMap : ClassMapping<TemplateResourceEntity>
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

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(64);
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