// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;

namespace DataCore.DAL.TableScaleModels
{
    public class LogTypeEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual byte Number { get; set; }
        public virtual string Icon { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public LogTypeEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Uid);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(Number)}: {Number}. " +
                   $"{nameof(Icon)}: {Icon}. ";
        }

        public virtual bool Equals(LogTypeEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Number, entity.Number) &&
                   Equals(Icon, entity.Icon);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LogTypeEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new LogTypeEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Number, default(byte)) &&
                   Equals(Icon, default(string));
        }

        public override object Clone()
        {
            return new LogTypeEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Uid = Uid,
                Number = Number,
                Icon = Icon,
            };
        }

        #endregion
    }
}
