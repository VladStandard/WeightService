using System;

namespace BlazorCore.DAL.TableModels
{
    public class HostsEntity : BaseIdEntity
    {
        #region Public and private fields and properties

        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual string Name { get; set; }
        public virtual string Ip { get; set; }
        public virtual string Mac { get; set; }
        public virtual Guid IdRRef { get; set; }

        public virtual string IdRRefAsString
        {
            get => IdRRef.ToString();
            set => IdRRef = Guid.Parse(value);
        }
        public virtual bool Marked { get; set; }
        public virtual string SettingsFile { get; set; }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            var strCreateDate = CreateDate != null ? CreateDate.ToString() : "null";
            var strModifiedDate = ModifiedDate != null ? ModifiedDate.ToString() : "null";
            var strSettingsFileString = SettingsFile != null ? SettingsFile.Length.ToString() : "null";
            return base.ToString() +
                   $"{nameof(CreateDate)}: {strCreateDate}. " +
                   $"{nameof(ModifiedDate)}: {strModifiedDate}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Ip)}: {Ip}. " +
                   $"{nameof(Mac)}: {Mac}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. " +
                   $"{nameof(Marked)}: {Marked}. " +
                   $"{nameof(SettingsFile)}: {strSettingsFileString}. ";
        }

        public virtual bool Equals(HostsEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   Equals(Name, entity.Name) &&
                   Equals(Ip, entity.Ip) &&
                   Equals(Mac, entity.Mac) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(Marked, entity.Marked) &&
                   Equals(SettingsFile, entity.SettingsFile);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((HostsEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new HostsEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(CreateDate, default(DateTime?)) && 
                   Equals(ModifiedDate, default(DateTime?)) &&
                   Equals(Name, default(string)) && 
                   Equals(Ip, default(string)) &&
                   Equals(Mac, default(string)) &&
                   Equals(IdRRef, default(Guid)) &&
                   Equals(Marked, default(bool)) &&
                   Equals(SettingsFile, default(byte[]));
        }

        public override object Clone()
        {
            return new HostsEntity
            {
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                Name = Name,
                Ip = Ip,
                Mac = Mac,
                IdRRef = IdRRef,
                Marked = Marked,
                SettingsFile = SettingsFile,
            };
        }

        #endregion
    }
}
