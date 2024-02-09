using Ws.Printers.Enums;

namespace Ws.Printers.Features.Zebra.Utils;

public static class ZebraStatusParserUtils
{
    private const int PaperOutIndex = 1;
    private const int PauseIndex = 2;
    private const int BufferFullIndex = 5;
    private const int HeightTempIndex = 11;
    private const int HeadOpenIndex = 14;
    private const int RibbonOutIndex = 15;

    public static PrinterStatusEnum ParseStatusString(string strStatus)
    {
        try
        {
            strStatus = strStatus.Replace("\"", string.Empty);
            string[] arrayStatus = strStatus.Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (arrayStatus.Length != 25)
                return PrinterStatusEnum.Unknown;

            bool paperOut = StateParse(arrayStatus, PaperOutIndex);
            bool pause = StateParse(arrayStatus, PauseIndex);
            bool buffer = StateParse(arrayStatus, BufferFullIndex);
            bool heightTemp = StateParse(arrayStatus, HeightTempIndex);
            bool headOpen = StateParse(arrayStatus, HeadOpenIndex);
            bool ribbonOut = StateParse(arrayStatus, RibbonOutIndex);

            if (paperOut) return PrinterStatusEnum.PaperOut;
            if (headOpen) return PrinterStatusEnum.HeadOpen;
            if (pause) return PrinterStatusEnum.Paused;
            if (ribbonOut) return PrinterStatusEnum.RibbonOut;
            if (heightTemp) return PrinterStatusEnum.Unknown;
            if (buffer) return PrinterStatusEnum.Unknown;

            bool ready = !paperOut && !pause && !buffer && !heightTemp && !headOpen && !ribbonOut;

            return ready ? PrinterStatusEnum.Ready : PrinterStatusEnum.Unknown;
        }
        catch (Exception)
        {
            return PrinterStatusEnum.Unknown;
        }
    }

    private static bool StateParse(IReadOnlyList<string> array, int index)
    {
        return array[index] switch
        {
            "1" => true,
            "0" => false,
            _ => throw new InvalidOperationException("Couldn't convert string to bool")
        };
    }
}