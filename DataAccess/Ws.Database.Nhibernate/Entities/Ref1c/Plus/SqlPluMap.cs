using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Plus;

internal sealed class SqlPluMap : ClassMapping<PluEntity>
{
    public SqlPluMap()
    {
        Schema(SqlSchemasUtils.Ref1C);
        Table(SqlTablesUtils.Plus);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.GuidComb);
        });

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.FullName, m =>
        {
            m.Column("FULL_NAME");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.Description, m =>
        {
            m.Column("DESCRIPTION");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.Number, m =>
        {
            m.Column("NUMBER");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(true);
        });

        Property(x => x.ShelfLifeDays, m =>
        {
            m.Column("SHELF_LIFE_DAYS");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(true);
        });

        Property(x => x.Ean13, m =>
        {
            m.Column("EAN_13");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.Itf14, m =>
        {
            m.Column("ITF_14");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.IsCheckWeight, m =>
        {
            m.Column("IS_WEIGHT");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        ManyToOne(x => x.Bundle, m =>
        {
            m.Column("BUNDLE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Brand, m =>
        {
            m.Column("BRAND_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Clip, m =>
        {
            m.Column("CLIP_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        OneToOne(x => x.Nesting, m =>
        {
            m.Cascade(Cascade.All);
            m.Lazy(LazyRelation.NoLazy);
        });

        Set(x => x.Characteristics, c =>
        {
            c.Key(k => k.Column("PLU_UID"));
            c.Cascade(Cascade.All);
            c.Mutable(false);
            c.Lazy(CollectionLazy.NoLazy);
            c.Inverse(true);
            c.Fetch(CollectionFetchMode.Select);
        }, r => r.OneToMany());


        Property(x => x.Weight, m =>
        {
            m.Column("WEIGHT");
            m.Type(NHibernateUtil.Decimal);
            m.NotNullable(true);
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
    }
}