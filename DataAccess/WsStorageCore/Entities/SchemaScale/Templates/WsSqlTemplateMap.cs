namespace WsStorageCore.Entities.SchemaScale.Templates;

public sealed class WsSqlTemplateMap : ClassMapping<WsSqlTemplateEntity>
{
    public WsSqlTemplateMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Templates);
        
        Id(x => x.IdentityValueId, m =>
        {
            m.Column("Id");
            m.Type(NHibernateUtil.Int64);
            m.Generator(Generators.Identity);
        });

        Property(x => x.CreateDt, m =>
        {
            m.Column("CreateDate");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.ChangeDt, m =>
        {
            m.Column("ModifiedDate");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.IsMarked, m =>
        {
            m.Column("Marked");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.CategoryId, m =>
        {
            m.Column("CategoryID");
            m.Type(NHibernateUtil.String);
            m.Length(150);
            m.NotNullable(true);
        });

        Property(x => x.Title, m =>
        {
            m.Column("Title");
            m.Type(NHibernateUtil.String);
            m.Length(250);
        });

        Property(x => x.Data, m =>
        {
            m.Column("DATA");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });
    }
}