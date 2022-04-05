// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Устройства".
    /// </summary>
    public class ScaleEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual TemplateEntity? TemplateDefault { get; set; } = new();
        public virtual TemplateEntity? TemplateSeries { get; set; } = new();
        public virtual WorkshopEntity WorkShop { get; set; } = new();
        public virtual PrinterEntity? Printer { get; set; } = new();
        public virtual HostEntity? Host { get; set; } = new();
        public virtual string Description { get; set; } = string.Empty;
        public virtual Guid? IdRRef { get; set; } = null;
        public virtual string DeviceIp { get; set; } = string.Empty;
        public virtual short? DevicePort { get; set; }
        public virtual string DeviceMac { get; set; } = string.Empty;
        public virtual short? DeviceSendTimeout { get; set; }
        public virtual short? DeviceReceiveTimeout { get; set; }
        public virtual string DeviceComPort { get; set; } = string.Empty;
        public virtual string ZebraIp { get; set; } = string.Empty;
        public virtual string ZebraLink => string.IsNullOrEmpty(ZebraIp) ? string.Empty : $"http://{ZebraIp}";
        public virtual short? ZebraPort { get; set; }
        public virtual bool UseOrder { get; set; } = false;
        public virtual string VerScalesUi { get; set; } = string.Empty;
        public virtual int? DeviceNumber { get; set; }
        public virtual int? ScaleFactor { get; set; }

        #endregion

        #region Constructor and destructor

        public ScaleEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            string? strTemplateDefault = TemplateDefault != null ? TemplateDefault.Id.ToString() : "null";
            string? strTemplateSeries = TemplateSeries != null ? TemplateSeries.Id.ToString() : "null";
            string? strWorkShop = WorkShop != null ? WorkShop.Id.ToString() : "null";
            string? strPrinter = Printer != null ? Printer.Id.ToString() : "null";
            string? strHost = Host != null ? Host.Id.ToString() : "null";
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
            if (Host != null && !Host.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Description, default(string)) &&
                   Equals(IdRRef, default(Guid?)) &&
                   Equals(DeviceIp, default(string)) &&
                   Equals(DevicePort, default(short?)) &&
                   Equals(DeviceMac, default(string)) &&
                   Equals(DeviceSendTimeout, default(short?)) &&
                   Equals(DeviceReceiveTimeout, default(short?)) &&
                   Equals(DeviceComPort, default(string)) &&
                   Equals(ZebraIp, default(string)) &&
                   Equals(ZebraPort, default(short?)) &&
                   Equals(UseOrder, default(bool)) &&
                   Equals(VerScalesUi, default(string)) &&
                   Equals(DeviceNumber, default(int?)) &&
                   Equals(ScaleFactor, default(int?));
        }

        public override object Clone()
        {
            return new ScaleEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                TemplateDefault = TemplateDefault != null ? (TemplateEntity)TemplateDefault.Clone() : null,
                TemplateSeries = TemplateSeries != null ? (TemplateEntity)TemplateSeries.Clone() : null,
                WorkShop = (WorkshopEntity)WorkShop.Clone(),
                Printer = Printer != null ? (PrinterEntity)Printer.Clone() : null,
                Host = Host != null ? (HostEntity)Host.Clone() : null,
                Description = Description,
                IdRRef = IdRRef,
                DeviceIp = DeviceIp,
                DevicePort = DevicePort,
                DeviceMac = DeviceMac,
                DeviceSendTimeout = DeviceSendTimeout,
                DeviceReceiveTimeout = DeviceReceiveTimeout,
                DeviceComPort = DeviceComPort,
                ZebraIp = ZebraIp,
                ZebraPort = ZebraPort,
                UseOrder = UseOrder,
                VerScalesUi = VerScalesUi,
                DeviceNumber = DeviceNumber,
                ScaleFactor = ScaleFactor,
            };
        }

        #endregion
    }
}
