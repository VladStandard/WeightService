// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;

namespace DataCore.DAL.TableDwhModels
{
    public class InformationSystemEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual string ConnectString1 { get; set; }
        public virtual string ConnectString2 { get; set; }
        public virtual string ConnectString3 { get; set; }
        public virtual int StatusId { get; set; }

        #endregion

        #region Constructor and destructor

        public InformationSystemEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                $"{nameof(Name)}: {Name}. " +
                $"{nameof(ConnectString1)}: {ConnectString1}. " +
                $"{nameof(ConnectString2)}: {ConnectString2}. " +
                $"{nameof(ConnectString3)}: {ConnectString3}. " +
                $"{nameof(StatusId)}: {StatusId}. ";
        }

        public virtual bool Equals(InformationSystemEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Name, entity.Name) &&
                   Equals(ConnectString1, entity.ConnectString1) &&
                   Equals(ConnectString2, entity.ConnectString2) &&
                   Equals(ConnectString3, entity.ConnectString3) &&
                   Equals(StatusId, entity.StatusId);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((InformationSystemEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new InformationSystemEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Name, default(string)) &&
                   Equals(ConnectString1, default(string)) &&
                   Equals(ConnectString2, default(string)) &&
                   Equals(ConnectString3, default(string)) &&
                   Equals(StatusId, default(int));
        }

        public override object Clone()
        {
            return new InformationSystemEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Name = Name,
                ConnectString1 = Name,
                ConnectString2 = Name,
                ConnectString3 = Name,
                StatusId = StatusId,
            };
        }

        #endregion
    }
}
