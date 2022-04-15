// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Tab;e "Scales".
    /// </summary>
    public class ScaleEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual TemplateEntity? TemplateDefault { get; set; }
        public virtual TemplateEntity? TemplateSeries { get; set; }
        public virtual WorkShopEntity WorkShop { get; set; }
        public virtual PrinterEntity? Printer { get; set; }
        public virtual bool IsShipping { get; set; }
        public virtual bool IsKneading { get; set; }
        public virtual PrinterEntity? ShippingPrinter { get; set; }
        public virtual byte ShippingLength { get; set; }
        public virtual HostEntity? Host { get; set; }
        public virtual string Description { get; set; }
        public virtual Guid IdRRef { get; set; }
        public virtual string DeviceIp { get; set; }
        public virtual short DevicePort { get; set; }
        public virtual string DeviceMac { get; set; }
        public virtual short? DeviceSendTimeout { get; set; }
        public virtual short? DeviceReceiveTimeout { get; set; }
        public virtual string DeviceComPort { get; set; }
        public virtual string ZebraIp { get; set; }
        public virtual string ZebraLink => string.IsNullOrEmpty(ZebraIp) ? string.Empty : $"http://{ZebraIp}";
        public virtual short? ZebraPort { get; set; }
        public virtual bool UseOrder { get; set; }
        public virtual string VerScalesUi { get; set; }
        public virtual int? DeviceNumber { get; set; }
        public virtual int? ScaleFactor { get; set; }

        #endregion

        #region Constructor and destructor

        public ScaleEntity() : this(0)
        {
            //
        }

        public ScaleEntity(long id) : base(id)
        {
            TemplateDefault = new();
            TemplateSeries = new();
            WorkShop = new();
            Printer = new();
            ShippingPrinter = new();
            IsShipping = false;
            IsKneading = false;
            ShippingLength = 0;
            Host = new();
            Description = string.Empty;
            IdRRef = Guid.Empty;
            DeviceIp = string.Empty;
            DevicePort = 0;
            DeviceMac = string.Empty;
            DeviceSendTimeout = default;
            DeviceReceiveTimeout = default;
            DeviceComPort = string.Empty;
            ZebraIp = string.Empty;
            ZebraPort = default;
            UseOrder = false;
            VerScalesUi = string.Empty;
            DeviceNumber = default;
            ScaleFactor = default;
        }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            string? strTemplateDefault = TemplateDefault != null ? TemplateDefault.IdentityId.ToString() : "null";
            string? strTemplateSeries = TemplateSeries != null ? TemplateSeries.IdentityId.ToString() : "null";
            string? strWorkShop = WorkShop != null ? WorkShop.IdentityId.ToString() : "null";
            string? strPrinter = Printer != null ? Printer.IdentityId.ToString() : "null";
            string? strPrinterVehicle = ShippingPrinter != null ? ShippingPrinter.IdentityId.ToString() : "null";
            string? strHost = Host != null ? Host.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Description)}: {Description}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. " +
                   $"{nameof(DeviceIp)}: {DeviceIp}. " +
                   $"{nameof(DevicePort)}: {DevicePort}. " +
                   $"{nameof(DeviceMac)}: {DeviceMac}. " +
                   $"{nameof(DeviceSendTimeout)}: {DeviceSendTimeout}. " +
                   $"{nameof(DeviceReceiveTimeout)}: {DeviceReceiveTimeout}. " +
                   $"{nameof(DeviceComPort)}: {DeviceComPort}. " +
                   $"{nameof(ZebraIp)}: {ZebraIp}. " +
                   $"{nameof(ZebraPort)}: {ZebraPort}. " +
                   $"{nameof(UseOrder)}: {UseOrder}. " +
                   $"{nameof(VerScalesUi)}: {VerScalesUi}. " +
                   $"{nameof(DeviceNumber)}: {DeviceNumber}. " +
                   $"{nameof(TemplateDefault)}: {strTemplateDefault}. " +
                   $"{nameof(TemplateSeries)}: {strTemplateSeries}. " +
                   $"{nameof(ScaleFactor)}: {ScaleFactor}. " +
                   $"{nameof(WorkShop)}: {strWorkShop}. " +
                   $"{nameof(Printer)}: {strPrinter}. " +
                   $"{nameof(ShippingPrinter)}: {strPrinterVehicle}. " +
                   $"{nameof(IsShipping)}: {IsShipping}. " +
                   $"{nameof(IsKneading)}: {IsKneading}. " +
                   $"{nameof(ShippingLength)}: {ShippingLength}. " +
                   $"{nameof(Host)}: {strHost}. ";
        }

        public virtual bool Equals(ScaleEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   TemplateDefault != null && entity.TemplateDefault != null && TemplateDefault.Equals(entity.TemplateDefault) &&
                   TemplateSeries != null && entity.TemplateSeries != null && TemplateSeries.Equals(entity.TemplateSeries) &&
                   Equals(Description, entity.Description) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(DeviceIp, entity.DeviceIp) &&
                   Equals(DevicePort, entity.DevicePort) &&
                   Equals(DeviceMac, entity.DeviceMac) &&
                   Equals(DeviceSendTimeout, entity.DeviceSendTimeout) &&
                   Equals(DeviceReceiveTimeout, entity.DeviceReceiveTimeout) &&
                   Equals(DeviceComPort, entity.DeviceComPort) &&
                   Equals(ZebraIp, entity.ZebraIp) &&
                   Equals(ZebraPort, entity.ZebraPort) &&
                   Equals(UseOrder, entity.UseOrder) &&
                   Equals(VerScalesUi, entity.VerScalesUi) &&
                   Equals(DeviceNumber, entity.DeviceNumber) &&
                   Equals(ScaleFactor, entity.ScaleFactor) &&
                   WorkShop.Equals(entity.WorkShop) &&
                   Printer != null && entity.Printer != null && Printer.Equals(entity.Printer) &&
                   ShippingPrinter != null && entity.ShippingPrinter != null && ShippingPrinter.Equals(entity.ShippingPrinter) &&
                   IsShipping.Equals(entity.IsShipping) &&
                   IsKneading.Equals(entity.IsKneading) &&
                   ShippingLength.Equals(entity.ShippingLength) &&
                   Host != null && entity.Host != null && Host.Equals(entity.Host);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ScaleEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ScaleEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (TemplateDefault != null && !TemplateDefault.EqualsDefault())
                return false;
            if (TemplateSeries != null && !TemplateSeries.EqualsDefault())
                return false;
            if (WorkShop != null && !WorkShop.EqualsDefault())
                return false;
            if (Printer != null && !Printer.EqualsDefault())
                return false;
            if (ShippingPrinter != null && !ShippingPrinter.EqualsDefault())
                return false;
            if (Host != null && !Host.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Description, string.Empty) &&
                   Equals(IdRRef, Guid.Empty) &&
                   Equals(DeviceIp, string.Empty) &&
                   Equals(DevicePort, 0) &&
                   Equals(DeviceMac, string.Empty) &&
                   Equals(DeviceSendTimeout, null) &&
                   Equals(DeviceReceiveTimeout, null) &&
                   Equals(DeviceComPort, string.Empty) &&
                   Equals(ZebraIp, string.Empty) &&
                   Equals(ZebraPort, null) &&
                   Equals(UseOrder, false) &&
                   Equals(VerScalesUi, string.Empty) &&
                   Equals(DeviceNumber, null) &&
                   Equals(ScaleFactor, null) &&
                   Equals(IsShipping, false) &&
                   Equals(IsKneading, false) &&
                   Equals(ShippingLength, 0);
        }

        public override object Clone()
        {
            ScaleEntity item = (ScaleEntity)base.Clone();
            item.TemplateDefault = TemplateDefault != null ? (TemplateEntity)TemplateDefault.Clone() : null;
            item.TemplateSeries = TemplateSeries != null ? (TemplateEntity)TemplateSeries.Clone() : null;
            item.WorkShop = (WorkShopEntity)WorkShop.Clone();
            item.Printer = Printer != null ? (PrinterEntity)Printer.Clone() : null;
            item.ShippingPrinter = ShippingPrinter != null ? (PrinterEntity)ShippingPrinter.Clone() : null;
            item.IsShipping = IsShipping;
            item.IsKneading = IsKneading;
            item.ShippingLength = ShippingLength;
            item.Host = Host != null ? (HostEntity)Host.Clone() : null;
            item.Description = Description;
            item.IdRRef = IdRRef;
            item.DeviceIp = DeviceIp;
            item.DevicePort = DevicePort;
            item.DeviceMac = DeviceMac;
            item.DeviceSendTimeout = DeviceSendTimeout;
            item.DeviceReceiveTimeout = DeviceReceiveTimeout;
            item.DeviceComPort = DeviceComPort;
            item.ZebraIp = ZebraIp;
            item.ZebraPort = ZebraPort;
            item.UseOrder = UseOrder;
            item.VerScalesUi = VerScalesUi;
            item.DeviceNumber = DeviceNumber;
            item.ScaleFactor = ScaleFactor;
            return item;
        }

        #endregion
    }
}
