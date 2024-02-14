using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.Templates;

internal sealed class SqlTemplateMap : ClassMapping<TemplateEntity>
{
    public SqlTemplateMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Templates);
        
        Id(x => x.Uid, m =>
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
        });

        Property(x => x.Body, m =>
        {
            m.Column("BODY");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });
    }
}