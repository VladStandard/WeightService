using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Tasks;

public sealed class WsSqlTaskMap : ClassMapping<WsSqlTaskEntity>
{
    public WsSqlTaskMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Tasks);

        Id(x => x.IdentityValueUid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.Enabled, m =>
        {
            m.Column("ENABLED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.IsMarked, m =>
        {
            m.Column("IS_MARKED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        ManyToOne(x => x.TaskType, m =>
        {
            m.Column("TASK_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Scale, m =>
        {
            m.Column("SCALE_ID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}
