namespace WsStorageCore.Entities.SchemaScale.BarCodes;

[DebuggerDisplay("{ToString()}")]
public sealed class SqlBarCodeMap : ClassMapping<SqlBarCodeEntity>
{
 public SqlBarCodeMap()
    {
        Schema(SqlSchemasUtils.DbScales);
        Table(SqlTablesUtils.BarCodes);

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

        Property(x => x.TypeTop, m =>
        {
            m.Column("TYPE_TOP");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });

        Property(x => x.ValueTop, m =>
        {
            m.Column("VALUE_TOP");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });

        Property(x => x.TypeRight, m =>
        {
            m.Column("TYPE_RIGHT");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });

        Property(x => x.ValueRight, m =>
        {
            m.Column("VALUE_RIGHT");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });

        Property(x => x.TypeBottom, m =>
        {
            m.Column("TYPE_BOTTOM");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });

        Property(x => x.ValueBottom, m =>
        {
            m.Column("VALUE_BOTTOM");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });

        ManyToOne(x => x.PluLabel, m =>
        {
            m.Column("PLU_LABEL_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}
