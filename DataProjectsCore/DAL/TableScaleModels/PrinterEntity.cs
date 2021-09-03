// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;

namespace DataProjectsCore.DAL.TableScaleModels
{
    public class PrinterEntity : BaseIdEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual string Name { get; set; }
        public virtual string Ip { get; set; }
        public virtual string Link => string.IsNullOrEmpty(Ip) ? string.Empty : $"http://{Ip}";
        public virtual short Port { get; set; }
        public virtual string Password { get; set; }
        public virtual PrinterTypeEntity PrinterType { get; set; } = new PrinterTypeEntity();
        public virtual string Mac { get; set; }
        public virtual bool PeelOffSet { get; set; }
        public virtual short DarknessLevel { get; set; }
        public virtual bool Marked { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strPrinterType = PrinterType != null ? PrinterType.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(ModifiedDate)}: {ModifiedDate}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Ip)}: {Ip}. " +
                   $"{nameof(Port)}: {Port}. " +
                   $"{nameof(Password)}: {Password}. " +
                   $"{nameof(PrinterType)}: {strPrinterType}. " +
                   $"{nameof(Mac)}: {Mac}. " +
                   $"{nameof(PeelOffSet)}: {PeelOffSet}. " +
                   $"{nameof(DarknessLevel)}: {DarknessLevel}. " +
                   $"{nameof(Marked)}: {Marked}. ";
        }

        public virtual bool Equals(PrinterEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   Equals(Name, entity.Name) &&
                   Equals(Ip, entity.Ip) &&
                   Equals(Port, entity.Port) &&
                   Equals(Password, entity.Password) &&
                   Equals(PrinterType, entity.PrinterType) &&
                   Equals(Mac, entity.Mac) &&
                   Equals(PeelOffSet, entity.PeelOffSet) &&
                   Equals(DarknessLevel, entity.DarknessLevel) &&
                   Equals(Marked, entity.Marked);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PrinterEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new PrinterEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (PrinterType != null && !PrinterType.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(CreateDate, default(DateTime)) &&
                   Equals(ModifiedDate, default(DateTime)) &&
                   Equals(Name, default(string)) &&
                   Equals(Ip, default(string)) &&
                   Equals(Port, default(short)) &&
                   Equals(Password, default(string)) &&
                   Equals(Mac, default(string)) &&
                   Equals(PeelOffSet, default(bool)) &&
                   Equals(DarknessLevel, default(short)) &&
                   Equals(Marked, default(bool));
        }

        public override object Clone()
        {
            return new PrinterEntity
            {
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                Name = Name,
                Ip = Ip,
                Port = Port,
                Password = Password,
                PrinterType = (PrinterTypeEntity)PrinterType?.Clone(),
                Mac = Mac,
                PeelOffSet = PeelOffSet,
                DarknessLevel = DarknessLevel,
                Marked = Marked,
            };
        }

        #endregion
    }
}
