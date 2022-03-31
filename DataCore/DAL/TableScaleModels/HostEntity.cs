// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    public class HostEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDt { get; set; }
        public virtual DateTime ChangeDt { get; set; }
        public virtual DateTime AccessDt { get; set; }
        public virtual bool IsMarked { get; set; } = false;
        public virtual string Name { get; set; } = string.Empty;
        public virtual string Ip { get; set; } = string.Empty;
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
        public virtual string SettingsFile { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public HostEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
            MacAddress = new MacAddressEntity();
        }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            string? strCreateDt = CreateDt != null ? CreateDt.ToString() : "null";
            string? strChangeDt = ChangeDt != null ? ChangeDt.ToString() : "null";
            string? strAccessDt = AccessDt != null ? AccessDt.ToString() : "null";
            string? strSettingsFileString = SettingsFile != null ? SettingsFile.Length.ToString() : "null";
            return base.ToString() +
                   $"{nameof(CreateDt)}: {strCreateDt}. " +
                   $"{nameof(ChangeDt)}: {strChangeDt}. " +
                   $"{nameof(AccessDt)}: {strAccessDt}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Ip)}: {Ip}. " +
                   $"{nameof(MacAddress)}: {MacAddress}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. " +
                   $"{nameof(IsMarked)}: {IsMarked}. " +
                   $"{nameof(SettingsFile)}: {strSettingsFileString}. ";
        }

        public virtual bool Equals(HostEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDt, entity.CreateDt) &&
                   Equals(ChangeDt, entity.ChangeDt) &&
                   Equals(AccessDt, entity.AccessDt) &&
                   Equals(Name, entity.Name) &&
                   Equals(Ip, entity.Ip) &&
                   Equals(MacAddress, entity.MacAddress) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(IsMarked, entity.IsMarked) &&
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
                   Equals(CreateDt, default(DateTime)) &&
                   Equals(ChangeDt, default(DateTime)) &&
                   Equals(AccessDt, default(DateTime)) &&
                   Equals(Name, default(string)) &&
                   Equals(Ip, default(string)) &&
                   Equals(IdRRef, default(Guid)) &&
                   Equals(IsMarked, default(bool)) &&
                   Equals(SettingsFile, default(byte[]));
        }

        public override object Clone()
        {
            return new HostEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                AccessDt = AccessDt,
                Name = Name,
                Ip = Ip,
                MacAddress = (MacAddressEntity)MacAddress.Clone(),
                IdRRef = IdRRef,
                IsMarked = IsMarked,
                SettingsFile = SettingsFile,
            };
        }

        #endregion
    }
}
