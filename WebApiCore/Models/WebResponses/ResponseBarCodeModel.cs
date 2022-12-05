// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WebApiCore.Models.WebResponses;

[Serializable]
public class ResponseBarCodeModel : SerializeBase, ICloneable, ISerializable // BarCodeModel
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string TypeTop { get; set; }
    [XmlElement] public virtual string ValueTop { get; set; }
    [XmlElement] public virtual string TypeRight { get; set; }
    [XmlElement] public virtual string ValueRight { get; set; }
    [XmlElement] public virtual string TypeBottom { get; set; }
    [XmlElement] public virtual string ValueBottom { get; set; }

    public ResponseBarCodeModel()
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private ResponseBarCodeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        TypeTop = info.GetString(nameof(TypeTop)) ?? string.Empty;
        ValueTop = info.GetString(nameof(ValueTop)) ?? string.Empty;
        TypeRight = info.GetString(nameof(TypeRight)) ?? string.Empty;
        ValueRight = info.GetString(nameof(ValueRight)) ?? string.Empty;
        TypeBottom = info.GetString(nameof(TypeBottom)) ?? string.Empty;
        ValueBottom = info.GetString(nameof(ValueBottom)) ?? string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{nameof(TypeTop)}: {TypeTop}. " +
        $"{nameof(ValueTop)}: {ValueTop}. " +
        $"{nameof(TypeRight)}: {TypeRight}. " +
        $"{nameof(ValueRight)}: {ValueRight}. " +
        $"{nameof(TypeBottom)}: {TypeBottom}. " +
        $"{nameof(ValueBottom)}: {ValueBottom}. ";

    public object Clone()
    {
        ResponseBarCodeModel item = new();
        item.TypeTop = TypeTop;
        item.ValueTop = ValueTop;
        item.TypeRight = TypeRight;
        item.ValueRight = ValueRight;
        item.TypeBottom = TypeBottom;
        item.ValueBottom = ValueBottom;
        //item.CloneSetup(base.CloneCast());
        return item;
    }

    public virtual ResponseBarCodeModel CloneCast() => (ResponseBarCodeModel)Clone();

    public virtual ResponseBarCodeModel CloneCast(BarCodeModel barCode)
    {
        ResponseBarCodeModel item = new();
        item.TypeTop = barCode.TypeTop;
        item.ValueTop = barCode.ValueTop;
        item.TypeRight = barCode.TypeRight;
        item.ValueRight = barCode.ValueRight;
        item.TypeBottom = barCode.TypeBottom;
        item.ValueBottom = barCode.ValueBottom;
        //item.CloneSetup(barCode.CloneCast());
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
        info.AddValue(nameof(TypeTop), TypeTop);
        info.AddValue(nameof(ValueTop), ValueTop);
        info.AddValue(nameof(TypeRight), TypeRight);
        info.AddValue(nameof(ValueRight), ValueRight);
        info.AddValue(nameof(TypeBottom), TypeBottom);
        info.AddValue(nameof(ValueBottom), ValueBottom);
    }

    #endregion
}