namespace Ws.StorageCore.Entities.SchemaPrint.Pallets;

public sealed class SqlPalletMap : ClassMapping<SqlPalletEntity>
{
    public SqlPalletMap()
    {
        Schema(SqlSchemasUtils.Print);
        Table(SqlTablesUtils.Pallets);

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
        
        Property(x => x.Kneading, m =>
        {
            m.Column("KNEADING");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(true);
        });
        
        ManyToOne(x => x.Plu, m => {
            m.Column("PLU_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
        
        ManyToOne(x => x.Line, m => {
            m.Column("LINE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}