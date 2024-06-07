// using Ws.Labels.Service.Features.Generate.Features.Weight.Models;
//
// namespace Ws.Labels.Tests.Features;
//
// public class XmlWeightLabelTests
// {
//     [Fact]
//     public void Check_WeightLabelModel_v1_Barcodes()
//     {
//         XmlWeightLabel label = new()
//         {
//             Weight = 16.696m,
//             Kneading = 16,
//             LineCounter = 288095,
//             LineNumber = 10430,
//             ProductDt = new DateTime(2023, 12, 5, 15, 19, 49),
//             PluGtin = "02600770000002",
//             PluNumber = 333,
//             ExpirationDt = default,
//             LineName = "",
//             LineAddress = "",
//             PluFullName = "",
//             PluDescription = ""
//         };
//         label.BarCodeRight.Should().Be("2991043000288095");
//         label.BarCodeTop.Should().Be("298104300028809523120515194933316696016");
//         label.BarCodeBottom.Should().Be("(01)02600770000002(3103)016696(11)231205(10)2312");
//     }
//
//     [Fact]
//     public void Check_WeightLabelModel_v2_Barcodes()
//     {
//         XmlWeightLabel label = new()
//         {
//             Weight = 2.360m,
//             Kneading = 1,
//             LineCounter = 200,
//             LineNumber = 12312,
//             ProductDt = new DateTime(2023, 12, 12, 16, 17, 38),
//             PluGtin = "02600914000004",
//             PluNumber = 101,
//             ExpirationDt = default,
//             LineName = "",
//             LineAddress = "",
//             PluFullName = "",
//             PluDescription = ""
//         };
//         label.BarCodeRight.Should().Be("2991231200000200");
//         label.BarCodeTop.Should().Be("298123120000020023121216173810102360001");
//         label.BarCodeBottom.Should().Be("(01)02600914000004(3103)002360(11)231212(10)2312");
//     }
// }