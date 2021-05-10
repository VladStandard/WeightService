using System;
using System.Text;

namespace DeviceControl.Core.DAL.TableModels
{
    public class LabelsEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual WeithingFactEntity WeithingFact { get; set; } = new WeithingFactEntity();
        public virtual byte[] Label { get; set; }
        public virtual string LabelString
        {
            get => Label == null ? string.Empty : Encoding.Default.GetString(Label);
            set => Label = Encoding.Default.GetBytes(value);
        }
        public virtual DateTime? CreateDate { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strWeithingFact = WeithingFact != null ? WeithingFact.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(WeithingFact)}: {strWeithingFact}. " +
                   $"{nameof(Label)}: {LabelString}. " + 
                   $"{nameof(CreateDate)}: {CreateDate}. ";
        }

        public virtual bool Equals(LabelsEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   WeithingFact.Equals(entity.WeithingFact) &&
                   Equals(Label, entity.Label) &&
                   Equals(CreateDate, entity.CreateDate);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LabelsEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new LabelsEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (WeithingFact != null && !WeithingFact.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Label, default(byte[])) &&
                   Equals(CreateDate, default(DateTime?));
        }

        public override object Clone()
        {

            return new LabelsEntity
            {
                Id = Id,
                WeithingFact = (WeithingFactEntity)WeithingFact?.Clone(),
                Label = CloneBytes(Label),
                CreateDate = CreateDate,
            };
        }

        #endregion
    }
}
