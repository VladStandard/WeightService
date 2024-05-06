using Ws.Printers.Enums;
using Ws.Printers.Features.Zebra.Constants;
using Ws.Printers.Features.Zebra.Enums;

namespace Ws.Printers.Features.Zebra.Commands;

internal partial class ZebraGetStatusCmd
{
    private static readonly char[] CmdSeparators = [',', '\n', '\r'];

    internal static PrinterStatusEnum ParseStatusString(IReadOnlyList<string> statusArray)
    {
        try
        {
            if (statusArray.Count != ZebraBaseConsts.StatusStrLen)
                return PrinterStatusEnum.Unknown;

            List<KeyValuePair<bool, PrinterStatusEnum>> statusMap =
            [
                new(StateParse(statusArray, ZebraStatusIndex.PaperOut), PrinterStatusEnum.PaperOut),
                new(StateParse(statusArray, ZebraStatusIndex.HeadOpen), PrinterStatusEnum.HeadOpen),
                new(StateParse(statusArray, ZebraStatusIndex.Pause), PrinterStatusEnum.Paused),
                new(StateParse(statusArray, ZebraStatusIndex.RibbonOut), PrinterStatusEnum.RibbonOut),
                new(StateParse(statusArray, ZebraStatusIndex.HeightTemp), PrinterStatusEnum.Unknown),
                new(StateParse(statusArray, ZebraStatusIndex.BufferFull), PrinterStatusEnum.Unknown)
            ];

            foreach (KeyValuePair<bool, PrinterStatusEnum> kvp in statusMap.Where(kvp => kvp.Key))
                return kvp.Value;

            return PrinterStatusEnum.Ready;
        }
        catch (Exception)
        {
            return PrinterStatusEnum.Unknown;
        }
    }

    internal static bool StateParse(IReadOnlyList<string> array, ZebraStatusIndex index)
    {
        if ((int)index >= array.Count) throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");
        return array[(int)index] == "1";
    }
}