// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "Nomenclatures".
    /// </summary>
    public class NomenclatureEntity : BaseEntity<NomenclatureEntity>
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string SerializedRepresentationObject { get; set; }
        /// <summary>
        /// Is weighted or pcs.
        /// </summary>
        public virtual bool Weighted { get; set; }

        #endregion

        #region Constructor and destructor

        public NomenclatureEntity() : this(0)
        {
            //
        }

        public NomenclatureEntity(long id) : base(id)
        {
            Name = string.Empty;
            Code = string.Empty;
            SerializedRepresentationObject = string.Empty;
            Weighted = false;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                $"{nameof(Code)}: {Code}. " +
                $"{nameof(SerializedRepresentationObject)}.Length: {SerializedRepresentationObject?.Length ?? 0}. " + 
                $"{nameof(Weighted)}: {Weighted}. ";
        }

        public virtual bool Equals(NomenclatureEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Code, entity.Code) &&
                   Equals(Name, entity.Name) &&
                   Equals(SerializedRepresentationObject, entity.SerializedRepresentationObject) &&
                   Equals(Weighted, entity.Weighted);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NomenclatureEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new NomenclatureEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Code, string.Empty) &&
                   Equals(Name, string.Empty) &&
                   Equals(SerializedRepresentationObject, string.Empty) &&
                   Equals(Weighted, false);
        }

        public override object Clone()
        {
            NomenclatureEntity item = (NomenclatureEntity)base.Clone();
            item.Code = Code;
            item.Name = Name;
            item.SerializedRepresentationObject = SerializedRepresentationObject;
            item.Weighted = Weighted;
            return item;
        }

        #endregion
    }
}
