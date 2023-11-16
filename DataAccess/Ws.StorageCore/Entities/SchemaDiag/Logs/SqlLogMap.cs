using Ws.StorageCore.Enums;

namespace Ws.StorageCore.Entities.SchemaDiag.Logs;

public sealed class SqlLogMap : ClassMapping<SqlLogEntity>
{
    public SqlLogMap()
    {
        Schema(SqlSchemasUtils.DbScales);
        Table(SqlTablesUtils.Logs);

        Id(x => x.IdentityValueUid, m => {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.CreateDt, m => {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.IsMarked, m => {
            m.Column("IS_MARKED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.Version, m => {
            m.Column("VERSION");
            m.Type(NHibernateUtil.String);
            m.Length(12);
        });

        Property(x => x.File, m => {
            m.Column("[FILE]");
            m.Type(NHibernateUtil.String);
            m.Length(40);
            m.NotNullable(true);
        });

        Property(x => x.Line, m => {
            m.Column("LINE");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });

        Property(x => x.Member, m => {
            m.Column("MEMBER");
            m.Type(NHibernateUtil.String);
            m.Length(40);
            m.NotNullable(true);
        });

        Property(x => x.Message, m => {
            m.Column("MESSAGE");
            m.Type(NHibernateUtil.String);
            m.Length(1024);
            m.NotNullable(true);
        });

        ManyToOne(x => x.Device, m => {
            m.Column("DEVICE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
        
        Property(x => x.Type, m =>
        {
            m.Column("TYPE");
            m.NotNullable(true);
            m.Type<EnumStringType<LogTypeEnum>>();
            m.Length(15);
        });
        
        ManyToOne(x => x.App, m => {
            m.Column("APP_UID");
            m.Lazy(LazyRelation.NoLazy);
            m.NotNullable(false);
        });
    }
}