namespace Ws.StorageCore.Entities.SchemaPrint.Labels;

public sealed class SqlLabelMap : ClassMapping<SqlLabelEntity>
{
    public SqlLabelMap()
    {
        Schema(SqlSchemasUtils.Print);
        Table(SqlTablesUtils.Labels);

        Id(x => x.IdentityValueUid, m => {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.CreateDt, m => {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });
        
        Property(x => x.Zpl, m => {
            m.Column("ZPL");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.BarcodeTop, m => {
            m.Column("BARCODE_TOP");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });
        
        Property(x => x.BarcodeRight, m => {
            m.Column("BARCODE_RIGHT");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });

        Property(x => x.BarcodeBottom, m => {
            m.Column("BARCODE_BOTTOM");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });
        
        Property(x => x.WeightNet, m => {
            m.Column("WEIGHT_NETTO");
            m.Type(NHibernateUtil.Decimal);
            m.Precision(10);
            m.Scale(3);
            m.NotNullable(true);
        });

        Property(x => x.WeightTare, m => {
            m.Column("WEIGHT_TARE");
            m.Type(NHibernateUtil.Decimal);
            m.Precision(10);
            m.Scale(3);
            m.NotNullable(true);
        });
        
        ManyToOne(x => x.Pallet, m => {
            m.Column("PALLET_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}