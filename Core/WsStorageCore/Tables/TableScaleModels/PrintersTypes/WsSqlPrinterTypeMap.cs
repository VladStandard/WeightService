using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.PrintersTypes;

public sealed class WsSqlPrinterTypeMap : ClassMapping<WsSqlPrinterTypeModel>
{
    public WsSqlPrinterTypeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PrintersTypes);
        Lazy(false);

        Id(x => x.IdentityValueId, m =>
        {
            m.Column("Id");
            m.Type(NHibernateUtil.Int64);
            m.Generator(Generators.Identity);
        });

        Property(x => x.Name, m =>
        {
            m.Column("Name");
            m.Type(NHibernateUtil.String);
            m.Length(100);
        });

        Property(x => x.IsMarked, m =>
        {
            m.Column("IS_MARKED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });
    }
}
