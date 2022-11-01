// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Xml;

[Serializable]
public class WeithingFactSummaryModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    public virtual DateTime WeithingDate { get; set; }
    public virtual int Count { get; set; }
    public virtual string Scale { get; set; }
    public virtual string Host { get; set; }
    public virtual string Printer { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public WeithingFactSummaryModel() : base(SqlFieldIdentityEnum.Id)
    {
	    WeithingDate = DateTime.MinValue;
	    Count = 0;
	    Scale = string.Empty;
	    Host = string.Empty;
	    Printer = string.Empty;
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(WeithingDate)}: {WeithingDate}. " +
        $"{nameof(Count)}: {Count}. " +
        $"{nameof(Scale)}: {Scale}. " +
        $"{nameof(Host)}: {Host}. " +
        $"{nameof(Printer)}: {Printer}. ";

    public override bool Equals(object obj)
	{
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((WeithingFactSummaryModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
	    base.EqualsDefault() &&
	    Equals(WeithingDate, DateTime.MinValue) &&
	    Equals(Count, 0) &&
	    Equals(Scale, string.Empty) &&
	    Equals(Host, string.Empty) &&
	    Equals(Printer, string.Empty);

    public override object Clone()
    {
        WeithingFactSummaryModel item = new();
        item.WeithingDate = WeithingDate;
        item.Count = Count;
        item.Scale = Scale;
        item.Host = Host;
        item.Printer = Printer;
		item.CloneSetup(base.CloneCast());
		return item;
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(WeithingFactSummaryModel item) =>
		ReferenceEquals(this, item) || base.Equals(item) && //-V3130
		Equals(WeithingDate, item.WeithingDate) &&
		Equals(Count, item.Count) &&
		Equals(Scale, item.Scale) &&
		Equals(Host, item.Host) &&
		Equals(Printer, item.Printer);

	public new virtual WeithingFactSummaryModel CloneCast() => (WeithingFactSummaryModel)Clone();

	#endregion
}
