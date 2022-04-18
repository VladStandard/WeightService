// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "Hosts".
    /// </summary>
    public class HostEntity : BaseEntity<HostEntity>
    {
        #region Public and private fields and properties

        public virtual DateTime AccessDt { get; set; }
        public virtual string Name { get; set; }
        public virtual string Ip { get; set; }
        public virtual MacAddressEntity MacAddress { get; set; }
        public virtual string MacAddressValue
        {
            get => MacAddress.Value;
            set => MacAddress.Value = value;
        }
        public virtual Guid IdRRef { get; set; }

        public virtual string IdRRefAsString
        {
            get => IdRRef.ToString();
            set => IdRRef = Guid.Parse(value);
        }
        public virtual string SettingsFile { get; set; }

        #endregion

        #region Constructor and destructor

        public HostEntity() : this(0)
        {
            //
        }

        public HostEntity(long id) : base(id)
        {
            AccessDt = DateTime.MinValue;
            Name = string.Empty;
            Ip = string.Empty;
            MacAddress = new();
            SettingsFile = string.Empty;
            IdRRef = Guid.Empty;
        }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            string? strAccessDt = AccessDt != null ? AccessDt.ToString() : "null";
            string? strSettingsFileString = SettingsFile != null ? SettingsFile.Length.ToString() : "null";
            return base.ToString() +
                   $"{nameof(AccessDt)}: {strAccessDt}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Ip)}: {Ip}. " +
                   $"{nameof(MacAddress)}: {MacAddress}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. " +
                   $"{nameof(SettingsFile)}: {strSettingsFileString}. ";
        }

        public virtual bool Equals(HostEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(AccessDt, entity.AccessDt) &&
                   Equals(Name, entity.Name) &&
                   Equals(Ip, entity.Ip) &&
                   Equals(MacAddress, entity.MacAddress) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(SettingsFile, entity.SettingsFile);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((HostEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new HostEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (MacAddress != null && !MacAddress.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(AccessDt, DateTime.MinValue) &&
                   Equals(Name, string.Empty) &&
                   Equals(Ip, string.Empty) &&
                   Equals(IdRRef, Guid.Empty) &&
                   Equals(SettingsFile, new byte[0]);
        }

        public override object Clone()
        {
            HostEntity item = (HostEntity)base.Clone();
            item.AccessDt = AccessDt;
            item.Name = Name;
            item.Ip = Ip;
            item.MacAddress = (MacAddressEntity)MacAddress.Clone();
            item.IdRRef = IdRRef;
            item.SettingsFile = SettingsFile;
            return item;
        }

        #endregion
    }
}
