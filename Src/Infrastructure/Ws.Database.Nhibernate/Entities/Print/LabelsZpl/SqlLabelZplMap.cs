using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Print.LabelsZpl;

public class SqlLabelZplMap : ClassMapping<LabelZpl>
{
    public SqlLabelZplMap()
    {
        Schema(SqlSchemasUtils.Print);
        Table(SqlTablesUtils.LabelsZpl);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Assigned);
        });

        Property(x => x.Height, m =>
        {
            m.Column("HEIGHT");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(false);
        });

        Property(x => x.Width, m =>
        {
            m.Column("WIDTH");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(false);
        });

        Property(x => x.Rotate, m =>
        {
            m.Column("ROTATE");
            m.Type(NHibernateUtil.Int16);
            m.NotNullable(false);
        });
    }
}