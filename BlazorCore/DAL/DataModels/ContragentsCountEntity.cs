//using System;

//namespace BlazorCore.DAL.TableModels
//{
//    public class ContragentsCountEntity : BaseEntity
//    {
//        #region Public and private fields and properties

//        public virtual DateTime? CreatedDate { get; set; }
//        public virtual DateTime? ModifiedDate { get; set; }
//        public virtual int Count { get; set; }

//        #endregion

//        #region Public and private methods

//        public override string ToString()
//        {
//            return base.ToString() + 
//                $"{nameof(CreatedDate)}: {CreatedDate}. " +
//                $"{nameof(ModifiedDate)}: {ModifiedDate}. " +
//                $"{nameof(Count)}: {Count}. ";
//        }

//        public virtual bool Equals(ContragentsCountEntity entity)
//        {
//            if (entity is null) return false;
//            if (ReferenceEquals(this, entity)) return true;
//            return base.Equals(entity) &&
//                   DateTime.Equals(CreatedDate, entity.CreatedDate) &&
//                   DateTime.Equals(ModifiedDate, entity.ModifiedDate) &&
//                   int.Equals(Count, entity.Count);
//        }

//        public override bool Equals(object obj)
//        {
//            if (obj is null) return false;
//            if (ReferenceEquals(this, obj)) return true;
//            if (obj.GetType() != GetType()) return false;
//            return Equals((ContragentsCountEntity)obj);
//        }

//        public override int GetHashCode()
//        {
//            return base.GetHashCode();
//        }

//        public virtual bool EqualsNew()
//        {
//            return Equals(new ContragentsCountEntity());
//        }

//        public new virtual bool EqualsDefault()
//        {
//            return base.EqualsDefault() &&
//                   DateTime.Equals(CreatedDate, default(DateTime?)) && 
//                   DateTime.Equals(ModifiedDate, default(DateTime?)) && 
//                   int.Equals(Count, default(int));
//        }

//        public override object Clone()
//        {
//            return new ContragentsCountEntity
//            {
//                Id = Id,
//                CreatedDate = CreatedDate,
//                ModifiedDate = ModifiedDate,
//                Count = Count,
//            };
//        }

//        #endregion
//    }
//}
