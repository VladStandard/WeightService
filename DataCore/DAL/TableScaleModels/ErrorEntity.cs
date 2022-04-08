// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System.IO;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "Errors".
    /// </summary>
    public class ErrorEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string FilePath { get; set; }
        public virtual string FilePathShort => !string.IsNullOrEmpty(FilePath) ? Path.GetFileName(FilePath) : string.Empty;
        public virtual int LineNumber { get; set; }
        public virtual string MemberName { get; set; }
        public virtual string Exception { get; set; }
        public virtual string InnerException { get; set; }

        #endregion

        #region Constructor and destructor

        public ErrorEntity() : this(0)
        {
            //
        }

        public ErrorEntity(long id) : base(id)
        {
            FilePath = string.Empty;
            LineNumber = 0;
            MemberName = string.Empty;
            Exception = string.Empty;
            InnerException = string.Empty;
        }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(FilePath)}: {FilePath}. " +
                   $"{nameof(LineNumber)}: {LineNumber}. " +
                   $"{nameof(MemberName)}: {MemberName}. " +
                   $"{nameof(Exception)}: {Exception}. " +
                   $"{nameof(InnerException)}: {InnerException}. ";
        }

        public virtual bool Equals(ErrorEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(FilePath, entity.FilePath) &&
                   Equals(LineNumber, entity.LineNumber) &&
                   Equals(MemberName, entity.MemberName) &&
                   Equals(Exception, entity.Exception) &&
                   Equals(InnerException, entity.InnerException);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ErrorEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ErrorEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(FilePath, string.Empty) &&
                   Equals(LineNumber, 0) &&
                   Equals(MemberName, string.Empty) &&
                   Equals(Exception, string.Empty) &&
                   Equals(InnerException, string.Empty);
        }

        public override object Clone()
        {
            ErrorEntity item = (ErrorEntity)base.Clone();
            item.FilePath = FilePath;
            item.LineNumber = LineNumber;
            item.MemberName = MemberName;
            item.Exception = Exception;
            item.InnerException = InnerException;
            return item;
        }

        #endregion
    }
}
