using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Scales;

public sealed class WsSqlScaleMap : ClassMapping<WsSqlScaleEntity>
{
    public WsSqlScaleMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Scales);

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

        Property(x => x.Description, m =>
        {
            m.Column("Description");
            m.Type(NHibernateUtil.String);
            m.Length(150);
        });

        Property(x => x.DeviceComPort, m =>
        {
            m.Column("DeviceComPort");
            m.Type(NHibernateUtil.String);
            m.Length(5);
        });

        Property(x => x.Number, m =>
        {
            m.Column("NUMBER");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });

        Property(x => x.LabelCounter, m =>
        {
            m.Column("COUNTER");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });

        Property(x => x.ClickOnce, m =>
        {
            m.Column("CLICK_ONCE");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        ManyToOne(x => x.Printer, m =>
        {
            m.Column("PRINTER_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.WorkShop, m =>
        {
            m.Column("WORKSHOP_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Device, m =>
        {
            m.Column("DEVICE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}