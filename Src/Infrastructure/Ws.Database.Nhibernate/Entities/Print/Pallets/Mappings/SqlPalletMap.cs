using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Print.Pallets.Mappings;

internal sealed class SqlPalletMap : ClassMapping<Pallet>
{
    public SqlPalletMap()
    {
        Schema(SqlSchemasUtils.Print);
        Table(SqlTablesUtils.Pallets);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Assigned);
        });

        Property(x => x.Barcode, m =>
        {
            m.Column("BARCODE");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.CreateDt, m =>
        {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.Counter, m =>
        {
            m.Column("COUNTER");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });

        Property(x => x.Number, m =>
        {
            m.Column("NUMBER");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.ProdDt, m =>
        {
            m.Column("PRODUCT_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.Weight, m =>
        {
            m.Column("WEIGHT_TRAY");
            m.Type(NHibernateUtil.Decimal);
            m.Precision(10);
            m.Scale(3);
            m.NotNullable(true);
        });

        ManyToOne(x => x.PalletMan, m =>
        {
            m.Column("PALLET_MAN_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Arm, m =>
        {
            m.Column("ARM_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}