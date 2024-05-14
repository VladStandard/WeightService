using Ws.Printers.Enums;
using Ws.Printers.Features.Zebra.Constants;
using Ws.Printers.Features.Zebra.Enums;

namespace Ws.Printers.Features.Zebra.Commands;

internal partial class ZebraGetStatusCmd
{
    private static readonly char[] CmdSeparators = [',', '\n', '\r'];

    internal static PrinterStatus ParseStatusString(IReadOnlyList<string> statusArray)
    {
        try
        {
            if (statusArray.Count != ZebraBaseConsts.StatusStrLen)
                return PrinterStatus.Unknown;

            List<KeyValuePair<bool, PrinterStatus>> statusMap =
            [
                new(StateParse(statusArray, ZebraStatusIndex.PaperOut), PrinterStatus.PaperOut),
                new(StateParse(statusArray, ZebraStatusIndex.HeadOpen), PrinterStatus.HeadOpen),
                new(StateParse(statusArray, ZebraStatusIndex.Pause), PrinterStatus.Paused),
                new(StateParse(statusArray, ZebraStatusIndex.RibbonOut), PrinterStatus.RibbonOut),
                new(StateParse(statusArray, ZebraStatusIndex.HeightTemp), PrinterStatus.Unknown),
                new(StateParse(statusArray, ZebraStatusIndex.BufferFull), PrinterStatus.Unknown)
            ];

            foreach (KeyValuePair<bool, PrinterStatus> kvp in statusMap.Where(kvp => kvp.Key))
                return kvp.Value;

            return PrinterStatus.Ready;
        }
        catch (Exception)
        {
            return PrinterStatus.Unknown;
        }
    }

    internal static bool StateParse(IReadOnlyList<string> array, ZebraStatusIndex index)
    {
        if ((int)index >= array.Count) throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");
        return array[(int)index] == "1";
    }
}