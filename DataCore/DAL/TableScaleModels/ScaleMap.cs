// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableScaleModels
{
    public class ScaleMap : ClassMap<ScaleEntity>
    {
        public ScaleMap()
        {
            Table("[db_scales].[Scales]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
            References(x => x.TemplateDefault).Column("TemplateIdDefault").Not.Nullable();
            References(x => x.TemplateSeries).Column("TemplateIdSeries").Nullable();
            References(x => x.WorkShop).Column("WorkShopId").Not.Nullable();
            References(x => x.Printer).Column("ZebraPrinterId").Nullable();
            References(x => x.Host).Column("HostId").Nullable();
            Map(x => x.Description).CustomSqlType("NVARCHAR").Column("Description").Length(150);
            Map(x => x.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IdRRef").Nullable();
            Map(x => x.DeviceIp).CustomSqlType("VARCHAR").Column("DeviceIP").Length(15);
            Map(x => x.DevicePort).CustomSqlType("SMALLINT").Column("DevicePort");
            Map(x => x.DeviceMac).CustomSqlType("VARCHAR").Column("DeviceMAC").Length(35);
            Map(x => x.DeviceSendTimeout).CustomSqlType("SMALLINT").Column("DeviceSendTimeout");
            Map(x => x.DeviceReceiveTimeout).CustomSqlType("SMALLINT").Column("DeviceReceiveTimeout");
            Map(x => x.DeviceComPort).CustomSqlType("VARCHAR").Column("DeviceComPort").Length(5);
            Map(x => x.ZebraIp).CustomSqlType("VARCHAR").Column("ZebraIP").Length(15);
            Map(x => x.ZebraPort).CustomSqlType("SMALLINT").Column("ZebraPort");
            Map(x => x.UseOrder).CustomSqlType("SMALLINT").Column("UseOrder");
            Map(x => x.VerScalesUi).CustomSqlType("VARCHAR").Column("VerScalesUI").Length(30);
            Map(x => x.DeviceNumber).CustomSqlType("INT").Column("DeviceNumber");
            Map(x => x.ScaleFactor).CustomSqlType("INT").Column("ScaleFactor").Default("1000");
            Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        }
    }
}
