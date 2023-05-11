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
    public ushort PluNumber { get; init; }
    public string PluName { get; init; }

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
    /// <param name="pluNumber"></param>
    /// <param name="pluName"></param>
    public WsSqlViewPluScaleModel(Guid uid, DateTime createDt, DateTime changeDt, bool isMarked, bool isActive,
        ushort scaleId, bool scaleIsMarked, string scaleName, Guid pluUid, bool pluIsMarked, ushort pluNumber, string pluName)
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
        PluNumber = pluNumber;
        PluName = pluName;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{Uid} | {ScaleId} {ScaleName} | {PluNumber} {PluName}";

    #endregion
}