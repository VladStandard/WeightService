using System;

namespace Ws.StorageCore.Views.ViewRefModels.PluLines;

[DebuggerDisplay("{ToString()}")]
public sealed class SqlViewPluLineModel : SqlViewBase
{
    #region Public and private fields, properties, constructor
    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }
    public bool IsMarked { get; init; }
    public bool IsActive { get; init; }
    public ushort ScaleId { get; init; }
    public bool ScaleIsMarked { get; init; }
    public string ScaleName { get; init; }
    public Guid PluUid { get; init; }
    public bool PluIsMarked { get; init; }
    public bool PluIsWeight { get; init; }
    public ushort PluNumber { get; init; }
    public string PluName { get; init; }
    public string PluGtin { get; init; }
    public string PluEan13 { get; init; }
    public string PluItf14 { get; init; }
    public ushort TemplateId { get; init; }
    public bool TemplateIsMarked { get; init; }
    public string TemplateName { get; init; }

    public SqlViewPluLineModel() : this(Guid.Empty, DateTime.MinValue, DateTime.MinValue, default, default,
        default, default, string.Empty,
        Guid.Empty, default, default, default, string.Empty,
        string.Empty, string.Empty, string.Empty,
        default, default, string.Empty) { }

    public SqlViewPluLineModel(Guid uid, DateTime createDt, DateTime changeDt, bool isMarked, bool isActive,
        ushort scaleId, bool scaleIsMarked, string scaleName, 
        Guid pluUid, bool pluIsMarked, bool pluIsWeight, ushort pluNumber, string pluName,
        string pluGtin, string pluEan13, string pluItf14,
        ushort templateId, bool templateIsMarked, string templateName) : base(uid)
    {
        CreateDt = createDt;
        ChangeDt = changeDt;
        IsMarked = isMarked;
        IsActive = isActive;
        ScaleId = scaleId;
        ScaleIsMarked = scaleIsMarked;
        ScaleName = scaleName;
        PluUid = pluUid;
        PluIsMarked = pluIsMarked;
        PluIsWeight = pluIsWeight;
        PluNumber = pluNumber;
        PluName = pluName;
        PluGtin = pluGtin;
        PluEan13 = pluEan13;
        PluItf14 = pluItf14;
        TemplateId = templateId;
        TemplateIsMarked = templateIsMarked;
        TemplateName = templateName;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{ScaleId} {ScaleName} | {PluNumber} {PluName} | {TemplateName}";

    #endregion
}