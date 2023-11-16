namespace WsStorageCore.Views.ViewRefModels.PluStorageMethods;

[DebuggerDisplay("{ToString()}")]
public sealed class SqlViewPluStorageMethodModel : SqlViewBase
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
    
    public SqlViewPluStorageMethodModel(Guid uid, Guid pluUid, bool pluIsMarked, bool pluIsWeight, ushort pluNumber, string pluName,
        string pluGtin, string pluEan13, string pluItf14,
        Guid storageMethodUid, bool storageMethodIsMarked, string storageMethodName, 
        short minTemp, short maxTemp, bool isLeft, bool isRight,
        Guid resourceUid, bool resourceIsMarked, string resourceName,
        ushort templateId, bool templateIsMarked, string templateName) : base(uid)
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