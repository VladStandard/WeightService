//using NUnit.Framework;
//using ScalesUI.Utils;
//using System.Diagnostics;
	
//namespace ScalesUI2Tests.Utils
//{
//    [TestFixture]
//    public class UtilsAppTests
//    {
//        [Test]
//        public void IsAdmin_AreEqual()
//        {
//            var deviceName = System.Net.Dns.GetHostName();
//            TestContext.WriteLine($"deviceName: {deviceName}");
//            var actual = deviceName.Equals("PC208") || deviceName.Equals("PC0147");
//            var expected = UtilsApp.IsAdmin;
//            TestContext.WriteLine($"actual/expected: {actual}");
//            Assert.AreEqual(expected, actual);
//        }
//    }
//}