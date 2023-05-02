// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.Aggregations;

public sealed record PluAggrModel
{
    #region Public and private fields, properties, constructor

    public DateTime ChangeDt { get; init; }
    public int Count { get; init; }
    public string Line { get; init; }
    public string Plu { get; init; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public PluAggrModel() : this(DateTime.MinValue)
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="changeDt"></param>
    /// <param name="count"></param>
    /// <param name="line"></param>
    /// <param name="device"></param>
    /// <param name="plu"></param>
    public PluAggrModel(DateTime changeDt, int count = 0, string line = "", string plu = "")
    {
        ChangeDt = changeDt;
        Count = count;
        Line = line;
        Plu = plu;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        string.IsNullOrEmpty(Plu)
            ? $"{nameof(Line)}: {Line}. " +
              $"{nameof(Plu)}: {Plu}. " +
              $"{nameof(Count)}: {Count}. " +
              $"{nameof(ChangeDt)}: {ChangeDt:yyyy-MM-dd}."
            : String.Empty;

    #endregion
}