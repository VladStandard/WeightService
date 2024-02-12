using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Core.Entities.Print.Pallets.Mappings;

internal sealed class SqlPalletMap : ClassMapping<PalletEntity>
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
        
        Property(x => x.Counter, m =>
        {
            m.Column("COUNTER");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });

        Property(x => x.ProdDt, m =>
        {
            m.Column("PROD_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });
        
        Property(x => x.Weight, m => {
            m.Column("WEIGHT");
            m.Type(NHibernateUtil.Decimal);
            m.Precision(10);
            m.Scale(3);
            m.NotNullable(true);
        });
        
        ManyToOne(x => x.PalletMan, m => {
            m.Column("PALLET_MAN_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}