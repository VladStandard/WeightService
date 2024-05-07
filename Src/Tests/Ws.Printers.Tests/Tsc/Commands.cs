using FluentAssertions;
using Ws.Printers.Enums;
using Ws.Printers.Features.Tsc.Commands;

namespace Ws.Printers.Tests.Tsc;

public class Commands
{
    [Theory]
    [InlineData(0x00, PrinterStatus.Ready)]
    [InlineData(0x01, PrinterStatus.HeadOpen)]
    [InlineData(0x02, PrinterStatus.PaperJam)]
    [InlineData(0x04, PrinterStatus.PaperOut)]
    [InlineData(0x08,  PrinterStatus.RibbonOut)]
    [InlineData(0x10,  PrinterStatus.Paused)]
    [InlineData(0x20,  PrinterStatus.Busy)]
    [InlineData(0x30,  PrinterStatus.Unknown)]
    [InlineData(0x25,  PrinterStatus.Unknown)]
    public void Test_Status_By_Byte(byte input, PrinterStatus expected)
    {
        TscGetStatusCmd.GetStatus(input).Should().Be(expected);
    }
}