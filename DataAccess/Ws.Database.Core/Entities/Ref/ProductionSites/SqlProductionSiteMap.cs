using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.ProductionSites;

internal class SqlProductionSiteMap : ClassMapping<ProductionSiteEntity>
{
    public SqlProductionSiteMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.ProductionSites);

        Id(x => x.Uid, m => {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.CreateDt, m => {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.ChangeDt, m => {
            m.Column("CHANGE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.Name, m => {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(150);
        });

        Property(x => x.Address, m => {
            m.Column("ADDRESS");
            m.Type(NHibernateUtil.String);
            m.Length(512);
        });
    }
}