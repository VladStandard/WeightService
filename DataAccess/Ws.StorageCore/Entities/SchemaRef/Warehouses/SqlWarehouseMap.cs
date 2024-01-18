namespace Ws.StorageCore.Entities.SchemaRef.Warehouses;

public class SqlWarehouseMap : ClassMapping<SqlWarehouseEntity>
{
    public SqlWarehouseMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Warehouses);

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
            m.Length(128);
            m.NotNullable(true);
        });

        ManyToOne(x => x.ProductionSite, m =>
        {
            m.Column("PRODUCTION_SITES_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy); 
        });
    }
}