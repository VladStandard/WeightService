// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace WeightCore.DAL.TableModels
{
    [Serializable]
    public class ScaleEntity : BaseEntity<ScaleEntity>
    {
        #region Public and private fields and properties

        public int Id { get; set; }
        public string Description { get; set; }
        public string DeviceIp { get; set; }
        public int DeviceId { get; set; }
        public string DeviceMac { get; set; }
        public int DevicePort { get; set; }
        public int DeviceSendTimeout { get; set; }
        public int DeviceReceiveTimeout { get; set; }
        public string DeviceComPort { get; set; }
        public bool UseOrder { get; set; }
        public int? ScaleFactor { get; set; }

        public int? TemplateIdDefault { get; set; }
        public int? TemplateIdSeries { get; set; }

        public ZebraPrinterEntity ZebraPrinter { get; set; }

        #endregion

        #region Constructor and destructor

        public ScaleEntity()
        {
            DevicePort = 5001;
            Description = "";
            DeviceSendTimeout = 500;
            DeviceReceiveTimeout = 1000;
            DeviceComPort = "COM4";
            UseOrder = false;
        }

        public ScaleEntity(int scaleId) : this()
        {
            Id = scaleId;
        }

        public ScaleEntity(int scaleId, string description) : this(scaleId)
        {
            Description = description;
        }

        #endregion
    }
}