// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WeightCore.DAL.TableModels
{
    [Serializable]
    public class HostEntity : BaseEntity<HostEntity>
    {
        public int Id { get; set; }
        public int CurrentScaleId { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public Guid IdRRef { get; set; }
        public bool Marked { get; set; }

        public override string ToString()
        {
            return $"{Name} ({IP})({MAC})";
        }

        [XmlIgnoreAttribute]
        public XDocument SettingsFile { get; set; }
    }
}
