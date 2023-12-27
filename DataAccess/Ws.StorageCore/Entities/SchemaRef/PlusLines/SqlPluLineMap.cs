namespace Ws.StorageCore.Entities.SchemaRef.PlusLines;

public sealed class SqlPluLineMap : ClassMapping<SqlPluLineEntity>
{
    public SqlPluLineMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.PlusLines);
        
       
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

        ManyToOne(x => x.Plu, m =>
        {
            m.Column("PLU_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Line, m =>
        {
            m.Column("LINE_ID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}
