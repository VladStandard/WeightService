// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewRefModels;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlViewPluStorageMethodModel
{
    #region Public and private fields, properties, constructor

    public Guid PluUid { get; init; }
    public bool PluIsMarked { get; init; }
    public bool PluIsWeight { get; init; }
    public ushort PluNumber { get; init; }
    public string PluName { get; init; }
    public string PluGtin { get; init; }
    public string PluEan13 { get; init; }
    public string PluItf14 { get; init; }
    public Guid StorageMethodUid { get; init; }
    public bool StorageMethodIsMarked { get; init; }
    public string StorageMethodName { get; init; }
    public short MinTemp { get; init; }
    public short MaxTemp { get; init; }
    public bool IsLeft { get; init; }
    public bool IsRight { get; init; }
    public Guid ResourceUid { get; init; }
    public bool ResourceIsMarked { get; init; }
    public string ResourceName { get; init; }
    public ushort TemplateId { get; init; }
    public bool TemplateIsMarked { get; init; }
    public string TemplateName { get; init; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public WsSqlViewPluStorageMethodModel() : this(Guid.Empty, false, false, 0, "",
        "", "", "", Guid.Empty, false, "", 
        0, 0, false, false,
        Guid.Empty, false, "",
        0, false, "") { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pluUid"></param>
    /// <param name="pluIsMarked"></param>
    /// <param name="pluIsWeight"></param>
    /// <param name="pluNumber"></param>
    /// <param name="pluName"></param>
    /// <param name="pluGtin"></param>
    /// <param name="pluEan13"></param>
    /// <param name="pluItf14"></param>
    /// <param name="storageMethodUid"></param>
    /// <param name="storageMethodIsMarked"></param>
    /// <param name="storageMethodName"></param>
    /// <param name="minTemp"></param>
    /// <param name="maxTemp"></param>
    /// <param name="isLeft"></param>
    /// <param name="isRight"></param>
    /// <param name="resourceUid"></param>
    /// <param name="resourceIsMarked"></param>
    /// <param name="resourceName"></param>
    /// <param name="templateId"></param>
    /// <param name="templateIsMarked"></param>
    /// <param name="templateName"></param>
    public WsSqlViewPluStorageMethodModel(Guid pluUid, bool pluIsMarked, bool pluIsWeight, ushort pluNumber, string pluName,
        string pluGtin, string pluEan13, string pluItf14,
        Guid storageMethodUid, bool storageMethodIsMarked, string storageMethodName, 
        short minTemp, short maxTemp, bool isLeft, bool isRight,
        Guid resourceUid, bool resourceIsMarked, string resourceName,
        ushort templateId, bool templateIsMarked, string templateName)
    {
        PluUid = pluUid;
        PluIsMarked = pluIsMarked;
        PluIsWeight = pluIsWeight;
        PluNumber = pluNumber;
        PluName = pluName;
        PluGtin = pluGtin;
        PluEan13 = pluEan13;
        PluItf14 = pluItf14;
        StorageMethodUid = storageMethodUid;
        StorageMethodIsMarked = storageMethodIsMarked;
        StorageMethodName = storageMethodName;
        MinTemp = minTemp;
        MaxTemp = maxTemp;
        IsLeft = isLeft;
        IsRight = isRight;
        ResourceUid = resourceUid;
        ResourceIsMarked = resourceIsMarked;
        ResourceName = resourceName;
        TemplateId = templateId;
        TemplateIsMarked = templateIsMarked;
        TemplateName = templateName;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{PluUid} | {PluNumber} {PluName} | {StorageMethodName} | {ResourceName} | {TemplateName}";

    #endregion
}