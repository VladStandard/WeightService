namespace WsStorageCore.Models;

[Serializable]
public class WsSqlFieldMacAddressModel : WsSqlFieldBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] private string _value;
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
                    _ => value
                };
            }
        }
    }
    [XmlIgnore] public string ValuePrettyLookSpace => GetValueAsString(' ');
    [XmlIgnore] public string ValuePrettyLookMinus => GetValueAsString('-');
    [XmlIgnore] public string ValuePrettyLookColon => GetValueAsString(':');

    public WsSqlFieldMacAddressModel() : base()
    {
        FieldName = nameof(WsSqlFieldMacAddressModel);
        _value = string.Empty;
    }

    public WsSqlFieldMacAddressModel(string address) : this()
    {
        _value = address;
    }
    
    protected WsSqlFieldMacAddressModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        _value = info.GetString(nameof(Value));
    }

    public WsSqlFieldMacAddressModel(WsSqlFieldMacAddressModel item) : base(item)
    {
        FieldName = nameof(WsSqlFieldMacAddressModel);
        Value = item.Value;
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
        return Equals((WsSqlFieldMacAddressModel)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() => Equals(Value, string.Empty);

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
            _ => Value
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
    }


    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlFieldMacAddressModel item) =>
        ReferenceEquals(this, item) || Equals(Value, item.Value);

    #endregion
}