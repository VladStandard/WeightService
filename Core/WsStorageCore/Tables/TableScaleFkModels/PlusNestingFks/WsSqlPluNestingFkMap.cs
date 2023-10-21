using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;

public sealed class WsSqlPluNestingFkMap : ClassMapping<WsSqlPluNestingFkModel>
{
    public WsSqlPluNestingFkMap()
    {
            Schema(WsSqlSchemasUtils.DbScales);
            Table(WsSqlTablesUtils.PlusNestingFks);

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

            Property(x => x.WeightMax, m =>
            {
                m.Column("WEIGHT_MAX");
                m.Type(NHibernateUtil.Decimal);
                m.NotNullable(true);
                m.Unique(true);
            });

            Property(x => x.WeightMin, m =>
            {
                m.Column("WEIGHT_MIN");
                m.Type(NHibernateUtil.Decimal);
                m.NotNullable(true);
                m.Unique(true);
            });

            Property(x => x.WeightNom, m =>
            {
                m.Column("WEIGHT_NOM");
                m.Type(NHibernateUtil.Decimal);
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