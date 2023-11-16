namespace Ws.StorageCore.Entities.SchemaScale.PlusFks;

public sealed class SqlPluFkMap : ClassMapping<SqlPluFkEntity>
{
    public SqlPluFkMap()
    {
        Schema(SqlSchemasUtils.DbScales);
        Table(SqlTablesUtils.PlusFks);

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

        ManyToOne(x => x.Plu, m =>
        {
            m.Column("PLU_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Parent, m =>
        {
            m.Column("PARENT_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Category, m =>
        {
            m.Column("CATEGORY_UID");
            m.NotNullable(false);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}