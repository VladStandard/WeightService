// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class HostDirect : BaseSerializeEntity<HostDirect>
    {
        #region Public and private fields and properties

        public int Id { get; set; } = default;
        public int ScaleId { get; set; } = default;
        public string? Name { get; set; } = string.Empty;
        public string? Ip { get; set; } = string.Empty;
        public string? Mac { get; set; } = string.Empty;
        public Guid IdRRef { get; set; }
        public bool Marked { get; set; }
        [XmlIgnore]
        public XDocument? SettingsFile { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}." +
                $"{nameof(Ip)}: {Ip}." +
                $"{nameof(Mac)}: {Mac}.";
        }

        #endregion
    }
}
