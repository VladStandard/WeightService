using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.PlusScales;

/// <summary>
/// Table map "PLUS_SCALES".
/// </summary>
public sealed class WsSqlPluScaleMap : ClassMapping<WsSqlPluScaleEntity>
{
    public WsSqlPluScaleMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusScales);
        
       
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

        Property(x => x.IsActive, m =>
        {
            m.Column("IS_ACTIVE");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        ManyToOne(x => x.Plu, m =>
        {
            m.Column("PLU_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Line, m =>
        {
            m.Column("SCALE_ID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}
