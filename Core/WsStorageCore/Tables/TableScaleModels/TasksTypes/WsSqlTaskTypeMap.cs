using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.TasksTypes;

public sealed class WsSqlTaskTypeMap : ClassMapping<WsSqlTaskTypeModel>
{
    public WsSqlTaskTypeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.TasksTypes);

        Id(x => x.IdentityValueUid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.Name, m =>
        {
            m.Column("Name");
            m.Type(NHibernateUtil.String);
            m.Length(32);
            m.NotNullable(true);
        });

        Property(x => x.IsMarked, m =>
        {
            m.Column("IS_MARKED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });
    }
}
