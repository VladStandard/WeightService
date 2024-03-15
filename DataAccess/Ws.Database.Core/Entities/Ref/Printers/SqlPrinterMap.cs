using Ws.Database.Core.Types;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;

namespace Ws.Database.Core.Entities.Ref.Printers;

internal class SqlPrinterMap : ClassMapping<PrinterEntity>
{
    public SqlPrinterMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Printers);

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
            m.Column("Name");
            m.Type(NHibernateUtil.String);
            m.Length(50);
        });

        Property(x => x.Ip, m => {
            m.Column("IP");
            m.Type<IpAddressType>();
            m.NotNullable(true);
        });

        Property(x => x.Port, m => {
            m.Column("PORT");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(true);
        });
        
        ManyToOne(x => x.ProductionSite, m => {
            m.Column("PRODUCTION_SITE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        Property(x => x.Type, m => {
            m.Length(15);
            m.Column("TYPE");
            m.Type<EnumStringType<PrinterTypeEnum>>();
            m.NotNullable(true);
        });
    }
}