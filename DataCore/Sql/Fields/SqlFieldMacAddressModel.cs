// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Fields;

/// <summary>
/// MAC address.
/// </summary>
[Serializable]
public class SqlFieldMacAddressModel : SqlFieldBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlIgnore] private string _value;
    [XmlElement] public string Value
    {
        get => _value;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                _value = string.Empty;
            }
            else
            {
                _value = value.Length switch
                {
                    // 000000000000
                    12 => value,
                    // 00-00-00-00-00-00 // 00:00:00:00:00:00
                    17 => $"{value[0]}{value[1]}{value[3]}{value[4]}{value[6]}{value[7]}" +
                          $"{value[9]}{value[10]}{value[12]}{value[13]}{value[15]}{value[16]}",
                    _ => value,
                };
            }
        }
    }

    [XmlIgnore] public string ValuePrettyLookSpace => GetValueAsString(' ');

    [XmlIgnore] public string ValuePrettyLookMinus => GetValueAsString('-');

    [XmlIgnore] public string ValuePrettyLookColon => GetValueAsString(':');

	/// <summary>
	/// Constructor.
	/// </summary>
	public SqlFieldMacAddressModel()
    {
	    FieldName = nameof(SqlFieldMacAddressModel);
		_value = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="address"></param>
    public SqlFieldMacAddressModel(string address) : this()
    {
        _value = address;
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	protected SqlFieldMacAddressModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
	    _value = info.GetString(nameof(Value));
    }

	#endregion

	#region Public and private methods

	public override string ToString() =>
		$"{nameof(Value)}: {ValuePrettyLookMinus}. ";

    public override bool Equals(object obj)
	{
	    if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlFieldMacAddressModel)obj);
    }

	public override int GetHashCode() => Value.GetHashCode();

    public override bool EqualsNew()
    {
        return Equals(new SqlFieldMacAddressModel());
    }

    public override bool EqualsDefault()
    {
        return Equals(Value, string.Empty);
    }

    public override object Clone()
    {
        SqlFieldMacAddressModel item = new()
        {
            Value = Value,
        };
        //item.Setup(((TableModel)this).CloneCast);
        return item;
    }

    public virtual void Default()
    {
        Value = "000000000000";
    }

    private string GetValueAsString(char ch)
    {
        if (string.IsNullOrEmpty(Value))
            return string.Empty;
        return Value.Length switch
        {
            12 => $"{Value[0]}{Value[1]}{ch}{Value[2]}{Value[3]}{ch}{Value[4]}{Value[5]}{ch}" +
                  $"{Value[6]}{Value[7]}{ch}{Value[8]}{Value[9]}{ch}{Value[10]}{Value[11]}",
            17 => $"{Value[0]}{Value[1]}{ch}{Value[3]}{Value[4]}{ch}{Value[6]}{Value[7]}{ch}" +
                  $"{Value[9]}{Value[10]}{ch}{Value[12]}{Value[13]}{ch}{Value[15]}{Value[16]}",
            _ => Value,
        };
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Value), Value);
    }

	public override void FillProperties()
	{
		base.FillProperties();
		Value = LocaleCore.Sql.SqlItemFieldMac;
	}


	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(SqlFieldMacAddressModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return Equals(Value, item.Value);
	}

	public new virtual SqlFieldMacAddressModel CloneCast() => (SqlFieldMacAddressModel)Clone();

	#endregion
}
