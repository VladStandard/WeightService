namespace Ws.StorageCore.Entities.SchemaPrint.ViewLabels;

public class SqlViewLabelMap : ClassMapping<SqlViewLabel>
{
    public SqlViewLabelMap()
    {
        Schema(SqlSchemasUtils.Print);
        Table(SqlTablesUtils.ViewLabels);
        
        Id(x => x.IdentityValueUid, m => { 
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
        });

        Property(x => x.CreateDt, m => {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
        });
        
        Property(x => x.ProductDate, m => {
            m.Column("PROD_DT");
            m.Type(NHibernateUtil.DateTime);
        });
        
        Property(x => x.LineName, m => {
            m.Column("LINE");
            m.Type(NHibernateUtil.String);
        });
        
        Property(x => x.PluName, m => {
            m.Column("PLU_NAME");
            m.Type(NHibernateUtil.String);
        });

        Property(x => x.PluNumber, m => {
            m.Column("PLU_NUMBER");
            m.Type(NHibernateUtil.Int16);
        });
        
        Property(x => x.WorkShop, m => {
            m.Column("WORKSHOP");
            m.Type(NHibernateUtil.String);
        });
        
        Property(x => x.IsCheckWeight, m => {
            m.Column("IS_CHECK_WEIGHT");
            m.Type(NHibernateUtil.Boolean);
        });
        
        Property(x => x.BarcodeTop, m => {
            m.Column("BARCODE_TOP");
            m.Type(NHibernateUtil.String);
        });
        
        Property(x => x.BarcodeRight, m => {
            m.Column("BARCODE_RIGHT");
            m.Type(NHibernateUtil.String);
        });

        Property(x => x.BarcodeBottom, m => {
            m.Column("BARCODE_BOTTOM");
            m.Type(NHibernateUtil.String);
        });
    }
}