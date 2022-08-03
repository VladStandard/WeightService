// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System.IO;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Errors".
/// </summary>
public class ErrorEntity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Id;

    public virtual string FilePath { get; set; }
    public virtual string FilePathShort => !string.IsNullOrEmpty(FilePath) ? Path.GetFileName(FilePath) : string.Empty;
    public virtual int LineNumber { get; set; }
    public virtual string MemberName { get; set; }
    public virtual string Exception { get; set; }
    public virtual string InnerException { get; set; }

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

    public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
		base.ToString() +
        $"{nameof(FilePath)}: {FilePath}. " +
        $"{nameof(LineNumber)}: {LineNumber}. " +
        $"{nameof(MemberName)}: {MemberName}. " +
        $"{nameof(Exception)}: {Exception}. " +
        $"{nameof(InnerException)}: {InnerException}. ";

    public virtual bool Equals(ErrorEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(FilePath, item.FilePath) &&
               Equals(LineNumber, item.LineNumber) &&
               Equals(MemberName, item.MemberName) &&
               Equals(Exception, item.Exception) &&
               Equals(InnerException, item.InnerException);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ErrorEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
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

    public new virtual object Clone()
    {
        ErrorEntity item = new();
        item.FilePath = FilePath;
        item.LineNumber = LineNumber;
        item.MemberName = MemberName;
        item.Exception = Exception;
        item.InnerException = InnerException;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual ErrorEntity CloneCast() => (ErrorEntity)Clone();

    #endregion
}
