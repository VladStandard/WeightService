//using System.Diagnostics;
//using System.Windows.Forms;
//using NUnit.Framework;
//using ScalesUI.Common;
//using ScalesUI.Helpers;

//namespace ScalesUITests.Helpers
//{
//    internal class ThreadHelperTests
//    {
//        #region Private fields and properties

//        /// <summary>
//        /// Помощник потоков.
//        /// </summary>
//        private ThreadHelper _thread { get; set; } = ThreadHelper.Instance;
//        /// <summary>
//        /// Помощник состояния устройства.
//        /// </summary>
//        private DeviceStatus _sessionState { get; set; } = DeviceStatus.Instance;

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

//        #region Поток мониторинга даты времени

//        [Test]
//        public void UpdatedResourcesDt_Execute_DoesNotThrow()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(UpdatedResourcesDt_Execute_DoesNotThrow)} start.");
//            var sw = Stopwatch.StartNew();

//            Assert.DoesNotThrow(() => _thread.UpdatedResourcesDt(new Label()));

//            sw.Stop();
//            TestContext.WriteLine($@"{nameof(UpdatedResourcesDt_Execute_DoesNotThrow)} complete. Elapsed time: {sw.Elapsed}");
//        }

//        #endregion

//        #region Поток мониторинга COM-порта

//        [Test]
//        public void UpdatedResourcesComPort_Execute_DoesNotThrow()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(UpdatedResourcesComPort_Execute_DoesNotThrow)} start.");
//            var sw = Stopwatch.StartNew();

//            Assert.DoesNotThrow(() => _thread.UpdatedResourcesComPort(_sessionState, new Label(), new Button(), new Button(), new Button(), new Button()));

//            sw.Stop();
//            TestContext.WriteLine($@"{nameof(UpdatedResourcesComPort_Execute_DoesNotThrow)} complete. Elapsed time: {sw.Elapsed}");
//        }

//        #endregion

//        #region Поток мониторинга весов

//        [Test]
//        public void LoadResourcesScale_Execute_DoesNotThrow()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(LoadResourcesScale_Execute_DoesNotThrow)} start.");
//            var sw = Stopwatch.StartNew();

//            Assert.DoesNotThrow(() => _thread.LoadResourcesScale(new Label(), new Label(), new Label(), new Label(), new Label(), new Button(), new PictureBox()));

//            sw.Stop();
//            TestContext.WriteLine($@"{nameof(LoadResourcesScale_Execute_DoesNotThrow)} complete. Elapsed time: {sw.Elapsed}");
//        }

//        [Test]
//        public void UnloadResourcesScale_Execute_DoesNotThrow()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(UnloadResourcesScale_Execute_DoesNotThrow)} start.");
//            var sw = Stopwatch.StartNew();

//            Assert.DoesNotThrow(() => _thread.UnloadResourcesScale(new Label(), new Label(), new Label(), new Label(), new Label(), new Button(), new PictureBox()));

//            sw.Stop();
//            TestContext.WriteLine($@"{nameof(UnloadResourcesScale_Execute_DoesNotThrow)} complete. Elapsed time: {sw.Elapsed}");
//        }

//        [Test]
//        public void UpdatedResourcesScale_Execute_DoesNotThrow()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(UpdatedResourcesScale_Execute_DoesNotThrow)} start.");
//            var sw = Stopwatch.StartNew();

//            Assert.DoesNotThrow(() => _thread.UpdatedResourcesScale(_sessionState, new Label(), new Label(), new Label(), new Label(), new Label(), new Button()));

//            sw.Stop();
//            TestContext.WriteLine($@"{nameof(UpdatedResourcesScale_Execute_DoesNotThrow)} complete. Elapsed time: {sw.Elapsed}");
//        }

//        #endregion
//    }
//}
