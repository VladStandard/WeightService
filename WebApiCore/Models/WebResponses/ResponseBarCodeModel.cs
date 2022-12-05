// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using WebApiCore.Utils;

namespace WebApiCore.Models.WebResponses;

[Serializable]
public class ResponseBarCodeModel : SerializeBase, ICloneable, ISerializable // BarCodeModel
{
    #region Public and private fields, properties, constructor
    [XmlElement(WebConstants.Guid)]
    public virtual Guid IdentityValueUid { get; set; }
    [XmlElement] public virtual DateTime CreateDt { get; set; }
    [XmlElement] public virtual DateTime ChangeDt { get; set; }
    [XmlElement] public virtual bool IsMarked { get; set; }
    [XmlElement] public virtual string Name { get; set; }
    [XmlElement] public virtual string Description { get; set; }
    [XmlElement] public virtual string TypeTop { get; set; }
    [XmlElement] public virtual string ValueTop { get; set; }
    [XmlElement] public virtual string TypeRight { get; set; }
    [XmlElement] public virtual string ValueRight { get; set; }
    [XmlElement] public virtual string TypeBottom { get; set; }
    [XmlElement] public virtual string ValueBottom { get; set; }
    [XmlElement] public virtual Guid PluLabelGuid { get; set; }

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
        // (Guid)info.GetValue(nameof(Guid), typeof(Guid))
        IdentityValueUid = new Guid(info.GetString(WebConstants.Guid) ?? string.Empty);
        CreateDt = info.GetDateTime(nameof(CreateDt));
        ChangeDt = info.GetDateTime(nameof(ChangeDt));
        IsMarked = info.GetBoolean(nameof(IsMarked));
        Name = info.GetString(nameof(Name)) ?? string.Empty;
        Description = info.GetString(nameof(Description)) ?? string.Empty;
        TypeTop = info.GetString(nameof(TypeTop)) ?? string.Empty;
        ValueTop = info.GetString(nameof(ValueTop)) ?? string.Empty;
        TypeRight = info.GetString(nameof(TypeRight)) ?? string.Empty;
        ValueRight = info.GetString(nameof(ValueRight)) ?? string.Empty;
        TypeBottom = info.GetString(nameof(TypeBottom)) ?? string.Empty;
        ValueBottom = info.GetString(nameof(ValueBottom)) ?? string.Empty;
        PluLabelGuid = new Guid(info.GetString(nameof(PluLabelGuid)) ?? string.Empty);
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{WebConstants.Guid}: {IdentityValueUid}. " +
        $"{nameof(ChangeDt)}: {CreateDt}. " +
        $"{nameof(ChangeDt)}: {ChangeDt}. " +
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Description)}: {Description}. " +
        $"{nameof(TypeTop)}: {TypeTop}. " +
        $"{nameof(ValueTop)}: {ValueTop}. " +
        $"{nameof(TypeRight)}: {TypeRight}. " +
        $"{nameof(ValueRight)}: {ValueRight}. " +
        $"{nameof(TypeBottom)}: {TypeBottom}. " +
        $"{nameof(ValueBottom)}: {ValueBottom}. " +
        $"{nameof(PluLabelGuid)}: {PluLabelGuid}. ";

    public object Clone()
    {
        ResponseBarCodeModel item = new();
        item.IdentityValueUid = IdentityValueUid;
        item.CreateDt = CreateDt;
        item.ChangeDt = ChangeDt;
        item.IsMarked = IsMarked;
        item.Name = Name;
        item.Description = Description;
        item.TypeTop = TypeTop;
        item.ValueTop = ValueTop;
        item.TypeRight = TypeRight;
        item.ValueRight = ValueRight;
        item.TypeBottom = TypeBottom;
        item.ValueBottom = ValueBottom;
        item.PluLabelGuid = PluLabelGuid;
        // item.CloneCast(barCode.CloneCast());
        return item;
    }

    public virtual ResponseBarCodeModel CloneCast() => (ResponseBarCodeModel)Clone();

    public virtual ResponseBarCodeModel CloneCast(BarCodeModel barCode)
    {
        ResponseBarCodeModel item = new();
        item.IdentityValueUid = barCode.IdentityValueUid;
        item.CreateDt = barCode.CreateDt;
        item.ChangeDt = barCode.ChangeDt;
        item.IsMarked= barCode.IsMarked;
        item.Name = barCode.Name;
        item.Description = barCode.Description;
        item.TypeTop = barCode.TypeTop;
        item.ValueTop = barCode.ValueTop;
        item.TypeRight = barCode.TypeRight;
        item.ValueRight = barCode.ValueRight;
        item.TypeBottom = barCode.TypeBottom;
        item.ValueBottom = barCode.ValueBottom;
        item.PluLabelGuid = barCode.PluLabel.IdentityValueUid;
        // item.CloneCast(barCode.CloneCast());
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
        info.AddValue(WebConstants.Guid, IdentityValueUid);
        info.AddValue(nameof(CreateDt), CreateDt);
        info.AddValue(nameof(ChangeDt), ChangeDt);
        info.AddValue(nameof(IsMarked), IsMarked);
        info.AddValue(nameof(Name), Name);
        info.AddValue(nameof(Description), Description);
        info.AddValue(nameof(TypeTop), TypeTop);
        info.AddValue(nameof(ValueTop), ValueTop);
        info.AddValue(nameof(TypeRight), TypeRight);
        info.AddValue(nameof(ValueRight), ValueRight);
        info.AddValue(nameof(TypeBottom), TypeBottom);
        info.AddValue(nameof(ValueBottom), ValueBottom);
        info.AddValue(nameof(PluLabelGuid), PluLabelGuid);
    }

    #endregion
}