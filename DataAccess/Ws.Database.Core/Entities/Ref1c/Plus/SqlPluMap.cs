using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Plus;

internal sealed class SqlPluMap : ClassMapping<PluEntity>
{
    public SqlPluMap()
    {
        Schema(SqlSchemasUtils.DbScales);
        Table(SqlTablesUtils.Plus);
        
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
        
        Property(x => x.IsGroup, m =>
        {
            m.Column("IS_GROUP");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.Number, m =>
        {
            m.Column("NUMBER");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(true);
        });

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(150);
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

        Property(x => x.ShelfLifeDays, m =>
        {
            m.Column("SHELF_LIFE_DAYS");
            m.Type(NHibernateUtil.Byte);
            m.NotNullable(true);
        });

        Property(x => x.Gtin, m =>
        {
            m.Column("GTIN");
            m.Type(NHibernateUtil.String);
            m.Length(14);
            m.NotNullable(true);
        });

        Property(x => x.Ean13, m =>
        {
            m.Column("EAN13");
            m.Type(NHibernateUtil.String);
            m.Length(13);
            m.NotNullable(true);
        });

        Property(x => x.Itf14, m =>
        {
            m.Column("ITF14");
            m.Type(NHibernateUtil.String);
            m.Length(14);
            m.NotNullable(true);
        });

        Property(x => x.IsCheckWeight, m =>
        {
            m.Column("IS_CHECK_WEIGHT");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.Code, m =>
        {
            m.Column("CODE");
            m.Type(NHibernateUtil.String);
            m.Length(11);
            m.NotNullable(true);
        });

        Property(x => x.Uid1C, m =>
        {
            m.Column("UID_1C");
            m.Type(NHibernateUtil.Guid);
            m.NotNullable(true);
        });

        ManyToOne(x => x.Bundle, m =>
        {
            m.Column("BUNDLE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
        
        ManyToOne(x => x.StorageMethod, m =>
        {
            m.Column("STORAGE_METHOD_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
        
        ManyToOne(x => x.Brand, m =>
        {
            m.Column("BRAND_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}