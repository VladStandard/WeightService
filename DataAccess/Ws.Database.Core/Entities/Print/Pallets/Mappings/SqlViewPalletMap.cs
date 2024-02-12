using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Core.Entities.Print.Pallets.Mappings;

public class SqlViewPalletMap: ClassMapping<ViewPallet>
{
    public SqlViewPalletMap()
    {
        Schema(SqlSchemasUtils.Print);
        Table(SqlTablesUtils.ViewPallets);
        Mutable(false);
        
        Id(x => x.IdentityValueUid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
        });

        Property(x => x.CreateDt, m =>
        {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
        });
        
        Property(x => x.ProdDt, m =>
        {
            m.Column("PROD_DT");
            m.Type(NHibernateUtil.DateTime);
        });
        
        Property(x => x.Counter, m =>
        {
            m.Column("COUNTER");
            m.Type(NHibernateUtil.Int32);
        });

        Property(x => x.PalletMan, m => {
            m.Column("PALLET_MAN");
            m.Type(NHibernateUtil.String);
        });
        
        Property(x => x.Barcode, m => {
            m.Column("BARCODE");
            m.Type(NHibernateUtil.String);
        });
        
        Property(x => x.Plu, m => {
            m.Column("PLU_NAME");
            m.Type(NHibernateUtil.String);
        });
        
        Property(x => x.Line, m => {
            m.Column("LINE_NAME");
            m.Type(NHibernateUtil.String);
        });
        
        Property(x => x.Warehouse, m => {
            m.Column("WAREHOUSE_NAME");
            m.Type(NHibernateUtil.String);
        });
        
        Property(x => x.WeightNet, m => {
            m.Column("WEIGHT_NETTO");
            m.Type(NHibernateUtil.String);
        });
        
        Property(x => x.WeightBrut, m => {
            m.Column("WEIGHT_BRUT");
            m.Type(NHibernateUtil.Decimal);
        });
        
        Property(x => x.WeightBrut, m => {
            m.Column("WEIGHT_BRUT");
            m.Type(NHibernateUtil.Decimal);
        });
        
        Id(x => x.LabelCount, m =>
        {
            m.Column("LABEL_COUNT");
            m.Type(NHibernateUtil.Int32);
        });
    }
}