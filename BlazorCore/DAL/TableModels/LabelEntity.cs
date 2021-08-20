// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;

namespace BlazorCore.DAL.TableModels
{
    public class LabelEntity : BaseIdEntity
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

        public virtual bool Equals(LabelEntity entity)
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
            return Equals((LabelEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new LabelEntity());
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

            return new LabelEntity
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
