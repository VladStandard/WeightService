// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewScaleModels.Aggregations;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlViewWeightingAggrModel
{
    #region Public and private fields, properties, constructor

    public DateTime ChangeDt { get; init; }
    public int Count { get; init; }
    public string Line { get; init; }
    public string PluName { get; init; }
    public int PluNumber { get; init; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="changeDt"></param>
    /// <param name="count"></param>
    /// <param name="line"></param>
    /// <param name="device"></param>
    /// <param name="plu"></param>
    // <param name="number"></param>
    public WsSqlViewWeightingAggrModel(DateTime changeDt, int count, string line, string plu, int number)
    {
        ChangeDt = changeDt;
        Count = count;
        Line = line;
        PluName = plu;
        PluNumber = number;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{Line} | {PluName} | {Count} {ChangeDt:yyyy-MM-dd}";

    #endregion
}