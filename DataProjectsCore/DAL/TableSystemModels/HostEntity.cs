// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;

namespace DataProjectsCore.DAL.TableSystemModels
{
    public class HostEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
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
        public virtual bool Marked { get; set; }
        public virtual string SettingsFile { get; set; }

        #endregion

        #region Constructor and destructor

        public HostEntity()
        {
            MacAddress = new MacAddressEntity();
        }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            string? strCreateDate = CreateDate != null ? CreateDate.ToString() : "null";
            string? strModifiedDate = ModifiedDate != null ? ModifiedDate.ToString() : "null";
            string? strSettingsFileString = SettingsFile != null ? SettingsFile.Length.ToString() : "null";
            return base.ToString() +
                   $"{nameof(CreateDate)}: {strCreateDate}. " +
                   $"{nameof(ModifiedDate)}: {strModifiedDate}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Ip)}: {Ip}. " +
                   $"{nameof(MacAddress)}: {MacAddress}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. " +
                   $"{nameof(Marked)}: {Marked}. " +
                   $"{nameof(SettingsFile)}: {strSettingsFileString}. ";
        }

        public virtual bool Equals(HostEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   Equals(Name, entity.Name) &&
                   Equals(Ip, entity.Ip) &&
                   Equals(MacAddress, entity.MacAddress) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(Marked, entity.Marked) &&
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
                   Equals(CreateDate, default(DateTime?)) &&
                   Equals(ModifiedDate, default(DateTime?)) &&
                   Equals(Name, default(string)) &&
                   Equals(Ip, default(string)) &&
                   Equals(IdRRef, default(Guid)) &&
                   Equals(Marked, default(bool)) &&
                   Equals(SettingsFile, default(byte[]));
        }

        public override object Clone()
        {
            return new HostEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                Name = Name,
                Ip = Ip,
                MacAddress = MacAddress,
                IdRRef = IdRRef,
                Marked = Marked,
                SettingsFile = SettingsFile,
            };
        }

        #endregion
    }
}
