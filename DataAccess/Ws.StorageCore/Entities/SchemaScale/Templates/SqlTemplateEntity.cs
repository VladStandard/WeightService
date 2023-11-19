namespace Ws.StorageCore.Entities.SchemaScale.Templates;

/// <summary>
/// Table "Templates".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class SqlTemplateEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual string CategoryId { get; set; } 
    public virtual string Title { get; set; }
    public virtual string Data { get; set; }
    
    public SqlTemplateEntity() : base(SqlEnumFieldIdentity.Id)
    {
        CategoryId = string.Empty;
        Title = string.Empty;
        Data = string.Empty;
    }

    public SqlTemplateEntity(SqlTemplateEntity item) : base(item)
    {
        CategoryId = item.CategoryId;
        Title = item.Title;
        Data = item.Data;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{CategoryId} | {Title}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlTemplateEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(CategoryId, string.Empty) &&
        Equals(Title, string.Empty) &&
        Equals(Data, string.Empty);

    public override void FillProperties()
    {
        base.FillProperties();
        Data = LocaleCore.Sql.SqlItemFieldTemplateData;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlTemplateEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(CategoryId, item.CategoryId) &&
        Equals(Title, item.Title) &&
        Equals(Data, item.Data);

    #endregion
}