namespace Ws.StorageCore.Entities.SchemaRef.Lines;

public sealed class SqlLineMap : ClassMapping<SqlLineEntity>
{
    public SqlLineMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Lines);

        Id(x => x.IdentityValueUid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Identity);
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
            m.Length(150);
        });

        Property(x => x.ComPort, m =>
        {
            m.Column("COM_PORT");
            m.Type(NHibernateUtil.String);
            m.Length(5);
        });

        Property(x => x.Number, m =>
        {
            m.Column("NUMBER");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });

        Property(x => x.Counter, m =>
        {
            m.Column("COUNTER");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });
        
        Property(x => x.Version, m =>
        {
            m.Column("VERSION");
            m.Type(NHibernateUtil.String);
            m.NotNullable(false);
        });
        
        Property(x => x.PcName, m =>
        {
            m.Column("PC_NAME");
            m.Type(NHibernateUtil.String);
            m.NotNullable(false);
        });

        ManyToOne(x => x.Printer, m =>
        {
            m.Column("PRINTER_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Warehouse, m =>
        {
            m.Column("WORKSHOP_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}