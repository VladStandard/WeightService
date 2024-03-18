using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Nhibernate.Entities.Diag.LogWebs;

internal sealed class WsSqlLogWebMap : ClassMapping<LogWebEntity>
{
    public WsSqlLogWebMap()
    {
        Schema(SqlSchemasUtils.Diag);
        Table(SqlTablesUtils.LogsWebs);

        Id(x => x.Uid, m => {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.CreateDt, m => {
            m.Column("CREATE_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.StampDt, m => {
            m.Column("STAMP_DT");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.Version, m => {
            m.Column("VERSION");
            m.Type(NHibernateUtil.String);
            m.Length(12);
            m.NotNullable(true);
        });

        Property(x => x.Url, m => {
            m.Column("URL");
            m.Type(NHibernateUtil.String);
            m.Length(512);
            m.NotNullable(true);
        });

        Property(x => x.DataRequest, m => {
            m.Column("DATA_REQUEST");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.DataResponse, m => {
            m.Column("DATA_RESPONSE");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.CountAll, m => {
            m.Column("COUNT_ALL");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });

        Property(x => x.CountSuccess, m => {
            m.Column("COUNT_SUCCESS");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });

        Property(x => x.CountErrors, m => {
            m.Column("COUNT_ERROR");
            m.Type(NHibernateUtil.Int32);
            m.NotNullable(true);
        });
    }
}