using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Printers;

public sealed class WsSqlPrinterMap : ClassMapping<WsSqlPrinterEntity>
{
    public WsSqlPrinterMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Printers);
        
        Id(x => x.IdentityValueId, m =>
        {
            m.Column("Id");
            m.Type(NHibernateUtil.Int64);
            m.Generator(Generators.Identity);
        });

        Property(x => x.CreateDt, m =>
        {
            m.Column("CreateDate");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.ChangeDt, m =>
        {
            m.Column("ModifiedDate");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.IsMarked, m =>
        {
            m.Column("Marked");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.Name, m =>
        {
            m.Column("Name");
            m.Type(NHibernateUtil.String);
            m.Length(100);
        });

        Property(x => x.Ip, m =>
        {
            m.Column("IP");
            m.Type(NHibernateUtil.String);
            m.Length(15);
        });

        Property(x => x.Port, m =>
        {
            m.Column("Port");
            m.Type(NHibernateUtil.Int16);
        });

        Property(x => x.Password, m =>
        {
            m.Column("Password");
            m.Type(NHibernateUtil.String);
            m.Length(15);
        });

        Property(x => x.MacAddressValue, m =>
        {
            m.Column("Mac");
            m.Type(NHibernateUtil.String);
            m.Length(20);
        });

        Property(x => x.PeelOffSet, m =>
        {
            m.Column("PeelOffSet");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.DarknessLevel, m =>
        {
            m.Column("DarknessLevel");
            m.Type(NHibernateUtil.Int16);
        });

        ManyToOne(x => x.PrinterType, m =>
        {
            m.Column("PrinterTypeId");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}
