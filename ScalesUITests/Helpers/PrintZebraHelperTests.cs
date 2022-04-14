//using System;
//using System.Diagnostics;
//using System.Windows.Forms;
//using NUnit.Framework;
//using ScalesUI.Common;
//using ScalesUI.Helpers;

//namespace ScalesUITests.Helpers
//{
//    internal class PrintZebraHelperTests
//    {
//        #region Private fields and properties

//        /// <summary>
//        /// Помощник принтера Зебра.
//        /// </summary>
//        private PrintZebraHelper _printZebra { get; set; } = PrintZebraHelper.Instance;

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
//        public void ResetAsync_Execute_DoesNotThrow()
//        {
//            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
//            TestContext.WriteLine($@"{nameof(ResetAsync_Execute_DoesNotThrow)} start.");
//            var stopwatch = Stopwatch.StartNew();

//            foreach (EnumZebraAction action in Enum.GetValues(typeof(EnumZebraAction)))
//            {
//                Assert.DoesNotThrowAsync(() => _printZebra.ActionAsync(action, "127.0.0.1", 9100, ProjectsEnums.SilentUI.True, new Button(), null, null, null));
//                TestContext.WriteLine($@"_printZebra.ActionAsync({action}, ""127.0.0.1"", 9100, ProjectsEnums.SilentUI.True, new Button()).");
//            }

//            TestContext.WriteLine($@"{nameof(ResetAsync_Execute_DoesNotThrow)} complete. Elapsed time: {stopwatch.Elapsed}");
//            stopwatch.Stop();
//        }

//        #endregion
//    }
//}
