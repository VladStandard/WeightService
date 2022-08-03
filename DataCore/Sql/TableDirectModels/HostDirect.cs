// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;
using System.Xml.Serialization;

namespace DataCore.Sql.TableDirectModels
{
    public class HostDirect : BaseSerializeEntity
    {
        #region Public and private fields, properties, constructor

        public long Id { get; set; }
        public long ScaleId { get; set; }
        public string? Name { get; set; }
        [XmlIgnore] public string? HostName { get; set; }
        public string? Ip { get; set; }
        public string? Mac { get; set; }
        public Guid IdRRef { get; set; }
        public bool IsMarked { get; set; }

        #endregion

        #region Constructor and destructor

        public HostDirect()
        {
            Id = 0;
            ScaleId = 0;
            Name = string.Empty;
            HostName = string.Empty;
            Ip = string.Empty;
            Mac = string.Empty;
            IdRRef = Guid.Empty;
            IsMarked = false;
        }

        #endregion

        #region Public and private methods

        public override string ToString() =>
            $"{nameof(Name)}: {Name}. " +
            $"{nameof(HostName)}: {HostName}. " +
            $"{nameof(Ip)}: {Ip}. " +
            $"{nameof(Mac)}: {Mac}. ";

        #endregion
    }
}
