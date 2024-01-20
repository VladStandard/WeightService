using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.Infrastructure.Models.Utils;

namespace Ws.StorageCore.Entities.Scales.PlusNestingFks;

public sealed class SqlPluNestingFkMap : ClassMapping<PluNestingEntity>
{
    public SqlPluNestingFkMap()
    {
            Schema(SqlSchemasUtils.DbScales);
            Table(SqlTablesUtils.PlusNestingFks);

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

            Property(x => x.IsDefault, m =>
            {
                m.Column("IS_DEFAULT");
                m.Type(NHibernateUtil.Boolean);
                m.NotNullable(true);
            });

            Property(x => x.BundleCount, m =>
            {
                m.Column("BUNDLE_COUNT");
                m.Type(NHibernateUtil.Int16);
                m.NotNullable(true);
                m.Unique(true);
            });

            Property(x => x.Uid1C, m =>
            {
                m.Column("UID_1C");
                m.NotNullable(true);
            });

            ManyToOne(x => x.Box, m =>
            {
                m.Column("BOX_UID");
                m.NotNullable(true);
                m.Lazy(LazyRelation.NoLazy);
            });

            ManyToOne(x => x.Plu, m =>
            {
                m.Column("PLU_UID");
                m.NotNullable(true);
                m.Lazy(LazyRelation.NoLazy);
            });
    }
}