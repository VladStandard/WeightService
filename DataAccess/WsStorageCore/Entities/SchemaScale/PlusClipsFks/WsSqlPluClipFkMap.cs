using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.PlusClipsFks;

/// <summary>
/// Маппинг таблицы связей клипс и ПЛУ.
/// </summary>
public sealed class WsSqlPluClipFkMap : ClassMapping<WsSqlPluClipFkEntity>
{
    public WsSqlPluClipFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusClipsFks);

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

        ManyToOne(x => x.Clip, m =>
        {
            m.Column("CLIP_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Plu, m =>
        {
            m.Column("PLU_UID");
            m.Unique(true);
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}