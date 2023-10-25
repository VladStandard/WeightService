using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaDiag.LogsTypes;

public sealed class WsSqlLogTypeMap : ClassMapping<WsSqlLogTypeEntity>
{
    public WsSqlLogTypeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.LogsTypes);

        Id(x => x.IdentityValueUid, m => {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.IsMarked, m => {
            m.Column("IS_MARKED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.Number, m => {
            m.Column("NUMBER");
            m.Type(NHibernateUtil.Byte);
            m.NotNullable(true);
        });

        Property(x => x.Icon, m => {
            m.Column("ICON");
            m.Type(NHibernateUtil.String);
            m.Length(32);
            m.NotNullable(true);
        });
    }
}