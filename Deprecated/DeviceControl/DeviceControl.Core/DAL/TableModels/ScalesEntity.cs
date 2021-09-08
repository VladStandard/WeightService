using System;

namespace DeviceControl.Core.DAL.TableModels
{
    public class ScalesEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual TemplatesEntity TemplateDefault { get; set; } = new TemplatesEntity();
        public virtual TemplatesEntity TemplateSeries { get; set; } = new TemplatesEntity();
        public virtual WorkshopEntity WorkShop { get; set; } = new WorkshopEntity();
        public virtual ZebraPrinterEntity Printer { get; set; } = new ZebraPrinterEntity();
        public virtual HostsEntity Host { get; set; } = new HostsEntity();
        public virtual string Description { get; set; }
        public virtual Guid? IdRRef { get; set; }
        public virtual string DeviceIp { get; set; }
        public virtual short? DevicePort { get; set; }
        public virtual string DeviceMac { get; set; }
        public virtual short? DeviceSendTimeout { get; set; }
        public virtual short? DeviceReceiveTimeout { get; set; }
        public virtual string DeviceComPort { get; set; }
        public virtual string ZebraIp { get; set; }
        public virtual string ZebraLink => string.IsNullOrEmpty(ZebraIp) ? string.Empty : $"http://{ZebraIp}";
        public virtual short? ZebraPort { get; set; }
        public virtual bool? UseOrder { get; set; }
        public virtual string VerScalesUi { get; set; }
        public virtual int? DeviceNumber { get; set; }
        public virtual int? ScaleFactor { get; set; }
        public virtual bool Marked { get; set; }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            var strCreateDate = CreateDate != null ? CreateDate.ToString() : "null";
            var strModifiedDate = ModifiedDate != null ? ModifiedDate.ToString() : "null";
            var strTemplateDefault = TemplateDefault != null ? TemplateDefault.Id.ToString() : "null";
            var strTemplateSeries = TemplateSeries != null ? TemplateSeries.Id.ToString() : "null";
            var strWorkShop = WorkShop != null ? WorkShop.Id.ToString() : "null";
            var strPrinter = Printer != null ? Printer.Id.ToString() : "null";
            var strHost = Host != null ? Host.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(CreateDate)}: {strCreateDate}. " +
                   $"{nameof(ModifiedDate)}: {strModifiedDate}. " +
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
                   $"{nameof(Marked)}: {Marked}. " + 
                   $"{nameof(Host)}: {strHost}. ";
        }

        public virtual bool Equals(ScalesEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   TemplateDefault.Equals(entity.TemplateDefault) &&
                   TemplateSeries.Equals(entity.TemplateSeries) &&
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
                   Printer.Equals(entity.Printer) &&
                   Equals(Marked, entity.Marked) &&
                   Host.Equals(entity.Host);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ScalesEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ScalesEntity());
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
                   Equals(CreateDate, default(DateTime?)) && 
                   Equals(ModifiedDate, default(DateTime?)) &&
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
                   Equals(UseOrder, default(bool?)) &&
                   Equals(VerScalesUi, default(string)) &&
                   Equals(DeviceNumber, default(int?)) &&
                   Equals(ScaleFactor, default(int?)) &&
                   Equals(Marked, default(bool));
        }

        public override object Clone()
        {
            return new ScalesEntity
            {
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                TemplateDefault = (TemplatesEntity)TemplateDefault?.Clone(),
                TemplateSeries = (TemplatesEntity)TemplateSeries?.Clone(),
                WorkShop = (WorkshopEntity)WorkShop?.Clone(),
                Printer = (ZebraPrinterEntity)Printer?.Clone(),
                Host = (HostsEntity)Host?.Clone(),
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
                Marked = Marked,
            };
        }

        #endregion
    }
}
