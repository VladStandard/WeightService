namespace Ws.StorageCore.Entities.SchemaScale.PlusLabels;

public sealed class SqlPluLabelMap : ClassMapping<SqlPluLabelEntity>
{
    public SqlPluLabelMap()
    {
        Schema(SqlSchemasUtils.DbScales);
        Table(SqlTablesUtils.PlusLabels);

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

        Property(x => x.Zpl, m =>
        {
            m.Column("ZPL");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.ProductDt, m =>
        {
            m.Column("PROD_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.ExpirationDt, m =>
        {
            m.Column("EXPIRATION_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        ManyToOne(x => x.PluWeighing, m =>
        {
            m.Column("PLU_WEIGHING_UID");
            m.NotNullable(false);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.PluScale, m =>
        {
            m.Column("PLU_SCALE_UID");
            m.NotNullable(false);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}