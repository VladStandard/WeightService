// using Ws.Database.EntityFramework.Entities.Ref.Lines;
// using Ws.Database.EntityFramework.Entities.Ref.Printers;
// using Ws.Desktop.Models.Features.Arms.Output;
//
// namespace Ws.Desktop.Api.App.Features.Arms.Extensions;
//
// internal static class LineEntityExtensions
// {
//     public static Arm ToArm(this LineEntity line, Printer printer, string warehouse)
//     {
//         return new()
//         {
//             Id = line.Id,
//             Counter = (uint)Math.Abs(line.Counter),
//             Name = line.Name,
//             PcName = line.PcName,
//             Warehouse = line.Warehouse.Name,
//             Printer = new()
//             {
//                 Ip = line.Printer.Ip,
//                 Name = line.Printer.Name,
//             }
//         };
//     }
//
//     public static Printer ToPrinter(this PrinterEntity printer)
//     {
//         return new()
//         {
//             Name = printer.Name,
//             Ip = printer.Ip
//         };
//     }
// }