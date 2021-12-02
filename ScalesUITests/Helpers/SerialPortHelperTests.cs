//using System.Diagnostics;
//using NUnit.Framework;
//using ScalesUI.Helpers;

//namespace ScalesUITests.Helpers
//{
//    internal class SerialPortHelperTests
//    {
//        #region Private fields and properties

//        /// <summary>
//        /// Помощник COM-порта.
//        /// </summary>
//        private SerialPortHelper _serialPort { get; set; } = SerialPortHelper.Instance;

//        #endregion

//        #region Setup & teardown

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

//        #endregion

//        #region Public methods

//        [Test]
//        public void InitComPort_Execute_DoesNotThrow()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(InitComPort_Execute_DoesNotThrow)} start.");
//            var sw = Stopwatch.StartNew();

//            Assert.DoesNotThrow(() => _serialPort.InitComPort("COM3"));

//            sw.Stop();
//            TestContext.WriteLine($@"{nameof(InitComPort_Execute_DoesNotThrow)} complete. Elapsed time: {sw.Elapsed}");
//        }

//        [Test]
//        public void OpenComPort_Execute_Throws()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(OpenComPort_Execute_Throws)} start.");
//            var sw = Stopwatch.StartNew();

//            Assert.DoesNotThrow(() => _serialPort.InitComPort("COM124"));
//            Assert.DoesNotThrow(() => _serialPort.OpenComPort());
//            Assert.AreEqual(_serialPort.ComPortException.Message, "Порт 'COM124' не существует.");

//            sw.Stop();
//            TestContext.WriteLine($@"{nameof(OpenComPort_Execute_Throws)} complete. Elapsed time: {sw.Elapsed}");
//        }

//        [Test]
//        public void OpenComPort_Execute_DoesNotThrow()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(OpenComPort_Execute_DoesNotThrow)} start.");
//            var sw = Stopwatch.StartNew();

//            Assert.DoesNotThrow(() => _serialPort.OpenComPort());
//            Assert.DoesNotThrow(() => _serialPort.InitComPort("COM1"));
//            Assert.DoesNotThrow(() => _serialPort.OpenComPort());

//            sw.Stop();
//            TestContext.WriteLine($@"{nameof(OpenComPort_Execute_DoesNotThrow)} complete. Elapsed time: {sw.Elapsed}");
//        }

//        [Test]
//        public void CloseComPort_Execute_DoesNotThrow()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(CloseComPort_Execute_DoesNotThrow)} start.");
//            var sw = Stopwatch.StartNew();

//            Assert.DoesNotThrow(() => _serialPort.CloseComPort());
//            Assert.DoesNotThrow(() => _serialPort.InitComPort("COM1"));
//            Assert.DoesNotThrow(() => _serialPort.CloseComPort());

//            sw.Stop();
//            TestContext.WriteLine($@"{nameof(CloseComPort_Execute_DoesNotThrow)} complete. Elapsed time: {sw.Elapsed}");
//        }

//        #endregion
//    }
//}
