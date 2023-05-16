// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewRefModels;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlViewPluScaleModel
{
    #region Public and private fields, properties, constructor

    public Guid Uid { get; init; }
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

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public WsSqlViewPluScaleModel() : this(Guid.Empty, DateTime.MinValue, DateTime.MinValue,
        default, default, default, default, string.Empty, 
        Guid.Empty, default, default, default, string.Empty, 
        string.Empty, string.Empty, string.Empty,
        default, default, string.Empty) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="createDt"></param>
    /// <param name="changeDt"></param>
    /// <param name="isMarked"></param>
    /// <param name="isActive"></param>
    /// <param name="scaleId"></param>
    /// <param name="scaleIsMarked"></param>
    /// <param name="scaleName"></param>
    /// <param name="pluUid"></param>
    /// <param name="pluIsMarked"></param>
    /// <param name="pluIsWeight"></param>
    /// <param name="pluNumber"></param>
    /// <param name="pluName"></param>
    /// <param name="pluGtin"></param>
    /// <param name="pluEan13"></param>
    /// <param name="pluItf14"></param>
    /// <param name="templateId"></param>
    /// <param name="templateIsMarked"></param>
    /// <param name="templateName"></param>
    public WsSqlViewPluScaleModel(Guid uid, DateTime createDt, DateTime changeDt, bool isMarked, bool isActive,
        ushort scaleId, bool scaleIsMarked, string scaleName, 
        Guid pluUid, bool pluIsMarked, bool pluIsWeight, ushort pluNumber, string pluName,
        string pluGtin, string pluEan13, string pluItf14,
        ushort templateId, bool templateIsMarked, string templateName)
    {
        Uid = uid;
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

    public override string ToString() => $"{Uid} | {ScaleId} {ScaleName} | {PluNumber} {PluName} | {TemplateName}";

    #endregion
}