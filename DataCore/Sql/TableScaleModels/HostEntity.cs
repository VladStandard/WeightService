// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "Hosts".
    /// </summary>
    public class HostEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime AccessDt { get; set; }
        public virtual string Name { get; set; }
        public virtual string HostName { get; set; }
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
            HostName = string.Empty;
            Ip = string.Empty;
            MacAddress = new();
            SettingsFile = string.Empty;
            IdRRef = Guid.Empty;
        }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            string strAccessDt = AccessDt != null ? AccessDt.ToString() : "null";
            string strSettingsFileString = SettingsFile != null ? SettingsFile.Length.ToString() : "null";
            return base.ToString() +
                   $"{nameof(AccessDt)}: {strAccessDt}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(HostName)}: {HostName}. " +
                   $"{nameof(Ip)}: {Ip}. " +
                   $"{nameof(MacAddress)}: {MacAddress}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. " +
                   $"{nameof(SettingsFile)}: {strSettingsFileString}. ";
        }

        public virtual bool Equals(HostEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            if (MacAddress != null && item.MacAddress != null && !MacAddress.Equals(item.MacAddress))
                return false;
            return base.Equals(item) &&
                   Equals(AccessDt, item.AccessDt) &&
                   Equals(Name, item.Name) &&
                   Equals(HostName, item.HostName) &&
                   Equals(Ip, item.Ip) &&
                   Equals(IdRRef, item.IdRRef) &&
                   Equals(SettingsFile, item.SettingsFile);
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
            return base.EqualsDefault(IdentityName) &&
                   Equals(AccessDt, DateTime.MinValue) &&
                   Equals(Name, string.Empty) &&
                   Equals(HostName, string.Empty) &&
                   Equals(Ip, string.Empty) &&
                   Equals(IdRRef, Guid.Empty) &&
                   Equals(SettingsFile, string.Empty);
        }

        public new virtual object Clone()
        {
            HostEntity item = new()
            {
                AccessDt = AccessDt,
                Name = Name,
                HostName = HostName,
                Ip = Ip,
                MacAddress = MacAddress.CloneCast,
                IdRRef = IdRRef,
                SettingsFile = SettingsFile,
            };
            item.Setup(((BaseEntity)this).CloneCast);
            return item;
        }

        public new virtual HostEntity CloneCast => (HostEntity)Clone();

        #endregion
    }
}
