// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Fields;

[Serializable]
public class SqlFieldBinaryModel : SqlFieldBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual byte[] Value { get; set; }

	[XmlIgnore] public virtual string ValueAscii
    {
        get => Value.Length == 0 ? string.Empty : Encoding.Default.GetString(Value);
        set => Value = Encoding.Default.GetBytes(value);
    }

    [XmlIgnore] public virtual string ValueUnicode
    {
        get => Value.Length == 0 ? string.Empty : Encoding.Unicode.GetString(Value);
        set => Value = Encoding.Unicode.GetBytes(value);
    }

    [XmlIgnore] public virtual string Info { get => DataUtils.GetBytesLength(Value); set => _ = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlFieldBinaryModel()
    {
	    FieldName = nameof(SqlFieldBinaryModel);
		Value = Array.Empty<byte>();
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	protected SqlFieldBinaryModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
	    Value = (byte[])info.GetValue(nameof(Value), typeof(byte[]));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
        $"{nameof(Info)}: {Info}. ";

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlFieldBinaryModel)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault()
    {
        return DataUtils.ByteEquals(Value, new byte[0]);
    }

    public override object Clone()
    {
        SqlFieldBinaryModel item = new()
        {
            Value = DataUtils.ByteClone(Value),
        };
        return item;
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

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(SqlFieldBinaryModel item)
	{
		//if (item is null) return false;
		if (ReferenceEquals(this, item)) return true;
		return DataUtils.ByteEquals(Value, item.Value);
	}

	public new virtual SqlFieldBinaryModel CloneCast() => (SqlFieldBinaryModel)Clone();

	public virtual void SetTemplateValue()
	{
		ValueUnicode = @"
<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet version=""2.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
<xsl:output method=""text"" encoding=""UTF-16"" omit-xml-declaration=""no""/>
<xsl:template match=""/"">

<xsl:text>^XA

^CI28
^CWK,E:COURB.TTF
^CWL,E:COURBI.TTF
^CWM,E:COURBD.TTF
^CWN,E:COUR.TTF
^CWZ,E:ARIAL.TTF
^CWW,E:ARIALBI.TTF
^CWE,E:ARIALBD.TTF
^CWR,E:ARIALI.TTF

^LH0,30
^FWR
</xsl:text>

<xsl:variable name=""length"" select=""50"" />
<xsl:variable name=""width"" select=""30"" />

<!-- Дата изготовления: метка -->
<xsl:text>
</xsl:text>

<!-- Дата изготовления: значение -->
<xsl:text>
</xsl:text>

<!-- Масса нетто: метка -->
</xsl:text>

<!-- Масса нетто: значение -->
</xsl:text>

<!-- Bar Code  -->
<xsl:text>

^PQ1

^XZ</xsl:text>

</xsl:template>
</xsl:stylesheet>
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
	}

	#endregion
}
