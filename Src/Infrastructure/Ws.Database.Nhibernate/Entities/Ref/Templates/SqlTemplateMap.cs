using Ws.Database.Nhibernate.Types;
using Ws.Database.Nhibernate.Utils;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Nhibernate.Entities.Ref.Templates;

internal sealed class SqlTemplateMap : ClassMapping<Template>
{
    public SqlTemplateMap()
    {
        Schema("ZPL");
        Table(SqlTablesUtils.Templates);

        Id(x => x.Uid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.GuidComb);
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

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(32);
        });

        Property(x => x.Width, m =>
        {
            m.Column("WIDTH");
            m.Type(NHibernateUtil.Int16);
        });

        Property(x => x.Height, m =>
        {
            m.Column("HEIGHT");
            m.Type(NHibernateUtil.Int16);
        });

        Property(x => x.IsWeight, m =>
        {
            m.Column("IS_WEIGHT");
            m.Type(NHibernateUtil.Boolean);
        });

        Property(x => x.Body, m =>
        {
            m.Column("BODY");
            m.Type(NHibernateUtil.String);
            m.NotNullable(true);
        });

        Property(x => x.BarcodeTopTemplate, m =>
        {
            m.Column("BARCODE_TOP_BODY");
            m.Type<BarcodeItemSqlType>();
            m.NotNullable(true);
        });

        Property(x => x.BarcodeBottomTemplate, m =>
        {
            m.Column("BARCODE_BOTTOM_BODY");
            m.Type<BarcodeItemSqlType>();
            m.NotNullable(true);
        });

        Property(x => x.BarcodeRightTemplate, m =>
        {
            m.Column("BARCODE_RIGHT_BODY");
            m.Type<BarcodeItemSqlType>();
            m.NotNullable(true);
        });
    }
}