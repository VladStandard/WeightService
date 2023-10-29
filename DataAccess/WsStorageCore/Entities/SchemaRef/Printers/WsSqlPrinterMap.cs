using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using WsStorageCore.Enums;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaRef.Printers;

public class WsSqlPrinterMap : ClassMapping<WsSqlPrinterEntity>
{
    public WsSqlPrinterMap()
    {
        Schema(WsSqlSchemasUtils.Ref);
        Table(WsSqlTablesUtils.Printers);
        
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

        Property(x => x.ChangeDt, m =>
        {
            m.Column("CHANGE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.IsMarked, m =>
        {
            m.Column("IS_MARKED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.Name, m =>
        {
            m.Column("Name");
            m.Type(NHibernateUtil.String);
            m.Length(50);
        });

        Property(x => x.Ip, m =>
        {
            m.Column("IP");
            m.Type(NHibernateUtil.String);
            m.Length(15);
        });

        Property(x => x.Port, m =>
        {
            m.Column("PORT");
            m.Type(NHibernateUtil.Int16);
        });

        Property(x => x.Type, m =>
        {
            m.Column("TYPE");
            m.NotNullable(true);
            m.Type<EnumStringType<PrinterTypeEnum>>();
            m.Length(15);
        });
    }
}