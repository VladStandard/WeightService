// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class ScaleDirect : BaseSerializeEntity<ScaleDirect>
    {
        #region Public and private fields and properties

        public int Id { get; set; } = default;
        public string? Description { get; set; }
        public string? DeviceIP { get; set; }
        public short? DevicePort { get; set; }
        public string? DeviceMac { get; set; }
        public int DeviceReadTimeout { get; set; }
        public int DeviceWriteTimeout { get; set; }
        public string? DeviceComPort { get; set; }
        public bool? UseOrder { get; set; }
        public string? VerScalesUI { get; set; }
        public int? ScaleFactor { get; set; }
        public int? TemplateIdDefault { get; set; }
        public int? TemplateIdSeries { get; set; }
        public ZebraPrinterHelper ZebraPrinter = ZebraPrinterHelper.Instance;

        #endregion

        #region Constructor and destructor

        public ScaleDirect()
        {
            Id = 0;
            Description = "";
            DeviceIP = "";
            DevicePort = 5001;
            DeviceMac = null;
            DeviceWriteTimeout = 500;
            DeviceReadTimeout = 1000;
            DeviceComPort = "COM1";
            UseOrder = false;
            VerScalesUI = "";
            ScaleFactor = 1000;
            TemplateIdDefault = 0;
            TemplateIdSeries = 0;
        }

        public ScaleDirect(int scaleId) : this()
        {
            Id = scaleId;
        }

        public ScaleDirect(int scaleId, string? description) : this(scaleId)
        {
            Description = description;
        }

        #endregion
    }
}