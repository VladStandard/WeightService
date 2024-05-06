using FluentAssertions;
using Ws.Printers.Enums;
using Ws.Printers.Features.Tsc.Commands;

namespace Ws.Printers.Tests.Tsc;

public class Commands
{
    [Theory]
    [InlineData(0x00, PrinterStatusEnum.Ready)]
    [InlineData(0x01, PrinterStatusEnum.HeadOpen)]
    [InlineData(0x02, PrinterStatusEnum.PaperJam)]
    [InlineData(0x04, PrinterStatusEnum.PaperOut)]
    [InlineData(0x08,  PrinterStatusEnum.RibbonOut)]
    [InlineData(0x10,  PrinterStatusEnum.Paused)]
    [InlineData(0x20,  PrinterStatusEnum.Busy)]
    [InlineData(0x30,  PrinterStatusEnum.Unknown)]
    [InlineData(0x25,  PrinterStatusEnum.Unknown)]
    public void Test_Status_By_Byte(byte input, PrinterStatusEnum expected)
    {
        TscGetStatusCmd.GetStatus(input).Should().Be(expected);
    }
}