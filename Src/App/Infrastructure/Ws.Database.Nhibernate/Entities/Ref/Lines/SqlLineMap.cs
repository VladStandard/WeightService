using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Enums;

namespace Ws.Database.Nhibernate.Entities.Ref.Lines;

internal sealed class SqlLineMap : ClassMapping<Arm>
{
    public SqlLineMap()
    {
        Schema(SqlSchemasUtils.Ref);
        Table(SqlTablesUtils.Lines);

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
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(32);
        });

        Property(x => x.Number, m =>
        {
            m.Column("NUMBER");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });

        Property(x => x.Counter, m =>
        {
            m.Column("COUNTER");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });

        Property(x => x.Version, m =>
        {
            m.Column("VERSION");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.PcName, m =>
        {
            m.Column("PC_NAME");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.Type, m =>
        {
            m.Column("TYPE");
            m.Type<EnumStringType<LineTypeEnum>>();
            m.Length(8);
            m.NotNullable(true);
        });

        ManyToOne(x => x.Printer, m =>
        {
            m.Column("PRINTER_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Warehouse, m =>
        {
            m.Column("WAREHOUSE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}