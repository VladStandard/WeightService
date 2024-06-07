using TscZebra.Plugin.Abstractions.Enums;
using Ws.Database.Nhibernate.Types;
using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Devices;

namespace Ws.Database.Nhibernate.Entities.Ref.Printers;

internal class SqlPrinterMap : ClassMapping<Printer>
{
    public SqlPrinterMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Printers);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.GuidComb);
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
            m.Column("Name");
            m.Type(NHibernateUtil.String);
            m.Length(16);
        });

        Property(x => x.Ip, m =>
        {
            m.Column("IP_V4");
            m.Type<IPAddressSqlType>();
            m.NotNullable(true);
        });

        ManyToOne(x => x.ProductionSite, m =>
        {
            m.Column("PRODUCTION_SITE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        Property(x => x.Type, m =>
        {
            m.Length(15);
            m.Column("TYPE");
            m.Type<EnumStringType<PrinterTypes>>();
            m.NotNullable(true);
        });
    }
}