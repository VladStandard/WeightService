// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Fields;

/// <summary>
/// MAC address.
/// </summary>
[Serializable]
public class FieldMacAddressModel
{
    #region Public and private fields, properties, constructor

    private string _value;
    [XmlElement]
    public string Value
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

    [XmlElement] public string ValuePrettyLookSpace => GetValueAsString(' ');

    [XmlElement] public string ValuePrettyLookMinus => GetValueAsString('-');

    [XmlElement] public string ValuePrettyLookColon => GetValueAsString(':');

    /// <summary>
    /// Constructor.
    /// </summary>
    public FieldMacAddressModel()
    {
        _value = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="address"></param>
    public FieldMacAddressModel(string address)
    {
        _value = address;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{nameof(Value)}: {ValuePrettyLookMinus}. ";

    public virtual bool Equals(FieldMacAddressModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return Equals(Value, item.Value);
    }

    public override bool Equals(object obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FieldMacAddressModel)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public bool EqualsNew()
    {
        return Equals(new FieldMacAddressModel());
    }

    public bool EqualsDefault()
    {
        return Equals(Value, string.Empty);
    }

    public object Clone()
    {
        FieldMacAddressModel item = new()
        {
            Value = Value,
        };
        //item.Setup(((TableModel)this).CloneCast);
        return item;
    }

    public FieldMacAddressModel CloneCast() => (FieldMacAddressModel)Clone();

    public void Default()
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

    #endregion
}
