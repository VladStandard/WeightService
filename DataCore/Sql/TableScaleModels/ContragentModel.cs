// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "CONTRAGENTS_V2".
/// </summary>
[Serializable]
public class ContragentModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string Name { get; set; }
    [XmlElement] public virtual string FullName { get; set; }
    [XmlElement] public virtual Guid IdRRef { get; set; }
    public virtual string IdRRefAsString
    {
        get => IdRRef.ToString();
        set => IdRRef = Guid.Parse(value);
    }
    [XmlElement] public virtual int DwhId { get; set; }
    [XmlElement] public virtual string Xml { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public ContragentModel()
	{
		Name = string.Empty;
		FullName = string.Empty;
		IdRRef = Guid.Empty;
		DwhId = 0;
		Xml = string.Empty;
	}

	#endregion

	#region Public and private methods

	public new virtual string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(DwhId)}: {DwhId}. ";

    public virtual bool Equals(ContragentModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Name, item.Name) &&
               Equals(FullName, item.FullName) &&
               Equals(IdRRef, item.IdRRef) &&
               Equals(DwhId, item.DwhId) &&
               Equals(Xml, item.Xml);
    }

	public new virtual bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((ContragentModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return 
	        base.EqualsDefault() &&
			Equals(Name, string.Empty) &&
			Equals(FullName, string.Empty) &&
			Equals(IdRRef, Guid.Empty) &&
			Equals(DwhId, 0) &&
			Equals(Xml, string.Empty);
    }

    public new virtual int GetHashCode() => base.GetHashCode();

	public new virtual object Clone()
    {
        ContragentModel item = new();
        item.Name = Name;
        item.FullName = FullName;
        item.IdRRef = IdRRef;
        item.DwhId = DwhId;
        item.Xml = Xml;
		item.CloneSetup(base.CloneCast());
		return item;
    }

    public new virtual ContragentModel CloneCast() => (ContragentModel)Clone();

    #endregion
}
