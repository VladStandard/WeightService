// using Newtonsoft.Json;
// using Ws.Domain.Models.ValueTypes;
// using Xunit.Abstractions;
//
// namespace Ws.Domain.Models.Tests.ValueTypes;
//
// public class BarcodeItemTests(ITestOutputHelper testOutputHelper)
// {
//     [Fact]
//     public void Test_Serialize_To_String()
//     {
//         Dictionary<string, BarcodeItem> data = new()
//         {
//             { "test", new BarcodeItem { IsConst = true, Len = 10 }},
//             { "test2", new BarcodeItem { IsConst = true, Len = 10 }},
//             { "test3", new BarcodeItem { IsConst = true, Len = 10 }},
//         };
//         string jsonString = JsonConvert.SerializeObject(data);
//         jsonString.Should()
//             .Be("{\"test\":{\"Len\":10,\"IsConst\":true},\"test2\":{\"Len\":10,\"IsConst\":true},\"test3\":{\"Len\":10,\"IsConst\":true}}");
//     }
//
//     [Fact]
//     public void Test_Deserialize_From_String()
//     {
//         Dictionary<string, BarcodeItem> dataForEqual = new()
//         {
//             { "test", new BarcodeItem { IsConst = true, Len = 10 }},
//             { "test2", new BarcodeItem { IsConst = true, Len = 10 }},
//             { "test3", new BarcodeItem { IsConst = true, Len = 10 }},
//         };
//         const string jsonString = "{\"test\":{\"Len\":10,\"IsConst\":true},\"test2\":{\"Len\":10,\"IsConst\":true},\"test3\":{\"Len\":10,\"IsConst\":true}}";
//
//         Dictionary<string, BarcodeItem>? dataForTest = JsonConvert.DeserializeObject<Dictionary<string, BarcodeItem>>(jsonString);
//
//         dataForTest.Should()
//             .NotBeNull().And
//             .BeEquivalentTo(dataForEqual);
//     }
// }