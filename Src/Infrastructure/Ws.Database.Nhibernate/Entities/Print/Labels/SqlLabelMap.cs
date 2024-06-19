using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Print.Labels;

internal sealed class SqlLabelMap : ClassMapping<Label>
{
    public SqlLabelMap()
    {
        Schema(SqlSchemasUtils.Print);
        Table(SqlTablesUtils.Labels);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.GuidComb);
        });

        Property(x => x.PalletUid, m =>
        {
            m.Column("PALLET_UID");
            m.Type(NHibernateUtil.Guid);
            m.NotNullable(false);
        });

        Property(x => x.CreateDt, m =>
        {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.Zpl, m =>
        {
            m.Column("ZPL");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.BarcodeTop, m =>
        {
            m.Column("BARCODE_TOP");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.BarcodeRight, m =>
        {
            m.Column("BARCODE_RIGHT");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.BarcodeBottom, m =>
        {
            m.Column("BARCODE_BOTTOM");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.WeightNet, m =>
        {
            m.Column("WEIGHT_NET");
            m.Type(NHibernateUtil.Decimal);
            m.Precision(10);
            m.Scale(3);
            m.NotNullable(true);
        });

        Property(x => x.WeightTare, m =>
        {
            m.Column("WEIGHT_TARE");
            m.Type(NHibernateUtil.Decimal);
            m.Precision(10);
            m.Scale(3);
            m.NotNullable(true);
        });

        Property(x => x.ProductDt, m =>
        {
            m.Column("PRODUCT_DT");
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

        Property(x => x.BundleCount, m =>
        {
            m.Column("BUNDLE_COUNT");
            m.Type(NHibernateUtil.UInt16);
            m.NotNullable(true);
        });

        ManyToOne(x => x.Plu, m =>
        {
            m.Column("PLU_UID");
            m.NotNullable(false);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Line, m =>
        {
            m.Column("ARM_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}