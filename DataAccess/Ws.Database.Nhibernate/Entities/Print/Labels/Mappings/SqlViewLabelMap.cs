using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Print.Labels.Mappings;

internal class ViewLabelMap : ClassMapping<ViewLabel>
{
    public ViewLabelMap()
    {
        Schema(SqlSchemasUtils.Print);
        Table(SqlTablesUtils.ViewLabels);
        Mutable(false);

        Id(x => x.Uid, m => {
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

        Property(x => x.Warehouse, m => {
            m.Column("WAREHOUSE");
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