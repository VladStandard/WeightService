// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsBlazorCore.Razors;

public class ChartCountModel
{
    public DateTime Date { get; set; }
    public int Count { get; set; }

    public ChartCountModel(DateTime date, int count)
    {
        Date = date;
        Count = count;
    }

    public override string ToString() => $"{nameof(Date)}: {Date}. {nameof(Count)}: {Count}. ";
}