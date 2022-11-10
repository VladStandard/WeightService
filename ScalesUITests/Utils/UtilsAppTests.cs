//using NUnit.Framework;
//using ScalesUI.Utils;
//using System.Diagnostics;
	
//namespace ScalesUI2Tests.Utils
//{
//    [TestFixture]
//    public class UtilsAppTests
//    {
//        /// <summary>
//        /// Setup private fields.
//        /// </summary>
//        [SetUp]
//        public void Setup()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(Setup)} start.");
//            // 
//            TestContext.WriteLine($@"{nameof(Setup)} complete.");
//        }

//        /// <summary>
//        /// Reset private fields to default state.
//        /// </summary>
//        [TearDown]
//        public void Teardown()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(Teardown)} start.");
//            // 
//            TestContext.WriteLine($@"{nameof(Teardown)} complete.");
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//        }

//        [Test]
//        public void IsAdmin_AreEqual()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(IsAdmin_AreEqual)} start.");
//            var stopwatch = Stopwatch.StartNew();

//            var deviceName = System.Net.Dns.GetHostName();
//            TestContext.WriteLine($"deviceName: {deviceName}");
//            var actual = deviceName.Equals("PC208") || deviceName.Equals("PC0147");
//            var expected = UtilsApp.IsAdmin;
//            TestContext.WriteLine($"actual/expected: {actual}");
//            Assert.AreEqual(expected, actual);

//            TestContext.WriteLine($@"{nameof(IsAdmin_AreEqual)} complete. Elapsed time: {stopwatch.Elapsed}");
//            stopwatch.Stop();
//        }
//    }
//}