// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;
using System.IO;

namespace DataProjectsCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Ошибки".
    /// </summary>
    public class ErrorEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual string? FilePath { get; set; } = null;
        public virtual string? FilePathShort => !string.IsNullOrEmpty(FilePath) ? Path.GetFileName(FilePath) : string.Empty;
        public virtual int LineNumber { get; set; }
        public virtual string? MemberName { get; set; } = null;
        public virtual string? Exception { get; set; } = null;
        public virtual string? InnerException { get; set; } = null;

        #endregion

        #region Constructor and destructor

        public ErrorEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(CreatedDate)}: {CreatedDate}. " +
                   $"{nameof(ModifiedDate)}: {ModifiedDate}. " +
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
                   Equals(CreatedDate, entity.CreatedDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
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
                   Equals(CreatedDate, default(DateTime)) &&
                   Equals(ModifiedDate, default(DateTime)) &&
                   Equals(FilePath, default(string)) &&
                   Equals(LineNumber, default(int)) &&
                   Equals(MemberName, default(string)) &&
                   Equals(Exception, default(string)) &&
                   Equals(InnerException, default(string));
        }

        public override object Clone()
        {
            return new ErrorEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                CreatedDate = CreatedDate,
                ModifiedDate = ModifiedDate,
                FilePath = FilePath,
                LineNumber = LineNumber,
                MemberName = MemberName,
                Exception = Exception,
                InnerException = InnerException,
            };
        }

        #endregion
    }
}
