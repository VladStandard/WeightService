// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.PlusLabels;

namespace DataCore.Sql.TableScaleModels.BarCodes;

public enum BarcodeTypeEnum
{
    Default,
    Codabar,
    Code11,
    Code128,
    Code128A,
    Code128B,
    Code128C,
    Code39,
    Code39E,
    Code93,
    EAN13,
    EAN8
}

/// <summary>
/// Table "BARCODES".
/// </summary>
[Serializable]
[DebuggerDisplay("Type = {nameof(BarCodeModel)} | " +
                 "{nameof(ValueTop)} = {ValueTop} | {nameof(ValueRight)} = {ValueRight} | " +
                 "{nameof(ValueBottom)} = {ValueBottom} | " +
                 "{nameof(PluLabel)} = {PluLabel}")]
public class BarCodeModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string TypeTop { get; set; }
    [XmlElement] public virtual string ValueTop { get; set; }
    [XmlElement] public virtual string TypeRight { get; set; }
    [XmlElement] public virtual string ValueRight { get; set; }
    [XmlElement] public virtual string TypeBottom { get; set; }
    [XmlElement] public virtual string ValueBottom { get; set; }
    [XmlElement] public virtual PluLabelModel PluLabel { get; set; }
    [XmlIgnore] private protected virtual string TemplateBarCodeTop => "^BY3  ^B2R,120,Y,N,Y";
    [XmlIgnore] private protected virtual string TemplateFd => "^FD";
    [XmlIgnore] private protected virtual string TemplateFs => "^FS";
    [XmlIgnore] private protected virtual string TypeBarCodeTop => "Interleaved 2 of 5 Bar Code";
    [XmlIgnore] private protected virtual string TemplateBarCodeRight => "^BY4  ^BCN,90,Y,Y,N";
    [XmlIgnore] private protected virtual string TypeBarCodeRight => "GS1-128"; // ""Code 128 Bar Code";
    [XmlIgnore] private protected virtual string TemplateBarCodeBottom => "^BY4  ^BCR,120,N,N,D";
    [XmlIgnore] private protected virtual string TypeBarCodeBottom => "Code 128 Bar Code";

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarCodeModel() : base(SqlFieldIdentityEnum.Uid)
    {
        TypeTop = string.Empty;
        ValueTop = string.Empty;
        TypeRight = string.Empty;
        ValueRight = string.Empty;
        TypeBottom = string.Empty;
        ValueBottom = string.Empty;
        PluLabel = new();
    }

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	protected BarCodeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        TypeTop = info.GetString(nameof(TypeTop));
        ValueTop = info.GetString(nameof(ValueTop));
        TypeRight = info.GetString(nameof(TypeRight));
        ValueRight = info.GetString(nameof(ValueRight));
        TypeBottom = info.GetString(nameof(TypeBottom));
        ValueBottom = info.GetString(nameof(ValueBottom));
        PluLabel = (PluLabelModel)info.GetValue(nameof(PluLabel), typeof(PluLabelModel));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(TypeTop)}: {TypeTop}. " +
        $"{nameof(ValueTop)}: {ValueTop}. " +
        $"{nameof(TypeRight)}: {TypeRight}. " +
        $"{nameof(ValueRight)}: {ValueRight}. " +
        $"{nameof(TypeBottom)}: {TypeBottom}. " +
        $"{nameof(ValueBottom)}: {ValueBottom}. " +
        $"{nameof(PluLabel)}: {PluLabel}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BarCodeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(TypeTop, string.Empty) &&
        Equals(ValueTop, string.Empty) &&
        Equals(TypeRight, string.Empty) &&
        Equals(ValueRight, string.Empty) &&
        Equals(TypeBottom, string.Empty) &&
        Equals(ValueBottom, string.Empty) &&
        PluLabel.EqualsDefault();

    public override object Clone()
    {
        BarCodeModel item = new();
        item.TypeTop = TypeTop;
        item.ValueTop = ValueTop;
        item.TypeRight = TypeRight;
        item.ValueRight = ValueRight;
        item.TypeBottom = TypeBottom;
        item.ValueBottom = ValueBottom;
        item.PluLabel = PluLabel.CloneCast();
        item.CloneSetup(base.CloneCast());
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(TypeTop), TypeTop);
        info.AddValue(nameof(ValueTop), ValueTop);
        info.AddValue(nameof(TypeRight), TypeRight);
        info.AddValue(nameof(ValueRight), ValueRight);
        info.AddValue(nameof(TypeBottom), TypeBottom);
        info.AddValue(nameof(ValueBottom), ValueBottom);
        info.AddValue(nameof(PluLabel), PluLabel);
    }

    public override void ClearNullProperties()
    {
        //if (PluLabel is not null && PluLabel.Identity.EqualsDefault())
        // PluLabel = null;
    }

    public override void FillProperties()
    {
        base.FillProperties();
        TypeTop = BarcodeTypeEnum.Default.ToString();
        ValueTop = LocaleCore.Sql.SqlItemFieldValue;
        TypeRight = BarcodeTypeEnum.Default.ToString();
        ValueRight = LocaleCore.Sql.SqlItemFieldValue;
        TypeBottom = BarcodeTypeEnum.Default.ToString();
        ValueBottom = LocaleCore.Sql.SqlItemFieldValue;
        PluLabel.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(BarCodeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(TypeTop, item.TypeTop) &&
        Equals(ValueTop, item.ValueTop) &&
        Equals(TypeRight, item.TypeRight) &&
        Equals(ValueRight, item.ValueRight) &&
        Equals(TypeBottom, item.TypeBottom) &&
        Equals(ValueBottom, item.ValueBottom) &&
        PluLabel.Equals(item.PluLabel);

    public new virtual BarCodeModel CloneCast() => (BarCodeModel)Clone();

    public virtual void SetBarCodeTop(PluLabelModel pluLabel)
    {
        /*
        ^FO745,20  ^BY3  ^B2R,120,Y,N,Y  ^FD  298987650000006722101713525011300335001
        ^FS
        */
        string value = SetBarCodeInside(pluLabel, TemplateBarCodeTop);
        if (!string.IsNullOrEmpty(value))
        {
            TypeTop = TypeBarCodeTop;
            ValueTop = value;
        }
    }

    public virtual void SetBarCodeRight(PluLabelModel pluLabel)
    {
        /*
        ^FO225,1060  ^BY4  ^BCN,90,Y,Y,N  ^FD>;2999876500000067
        ^FS 
        */
        string value = SetBarCodeInside(pluLabel, TemplateBarCodeRight);
        if (!string.IsNullOrEmpty(value))
        {
            TypeRight = TypeBarCodeRight;
            ValueRight = value;
        }
    }

    public virtual void SetBarCodeBottom(PluLabelModel pluLabel)
    {
        /*
        ^FO70,20  ^BY4  ^BCR,120,N,N,D  ^FD>;0112600076000000310300033511221017102210
        ^FS
        */
        string value = SetBarCodeInside(pluLabel, TemplateBarCodeBottom);
        if (!string.IsNullOrEmpty(value))
        {
            TypeBottom = TypeBarCodeBottom;
            ValueBottom = value;
        }
    }

    public virtual string SetBarCodeInside(PluLabelModel pluLabel, string template)
    {
        string value = string.Empty;
        if (pluLabel.Zpl.Contains(template)) //-V3095
        {
            string zpl = pluLabel.Zpl;
            if (string.IsNullOrEmpty(zpl)) return value;
            if (string.IsNullOrEmpty(template)) return value;
            if (string.IsNullOrEmpty(TemplateFd)) return value;
            if (!zpl.Contains(TemplateFd)) return value;

            int start = zpl.IndexOf(template, StringComparison.Ordinal) + template.Length;
            zpl = zpl.Substring(start, pluLabel.Zpl.Length - start);
            zpl = zpl.Split('\n')[0];
            start = zpl.IndexOf(TemplateFd, StringComparison.Ordinal) + TemplateFd.Length;
            zpl = zpl.Substring(start, zpl.Length - start);
            value = zpl
                .TrimStart('\r', ' ', '\n', '\t', '>', ';')
                .TrimEnd('\r', ' ', '\n', '\t', '>', ';');
        }
        return value;
    }

    #endregion
}
