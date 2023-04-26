//using System;
//using System.Diagnostics;
//using System.Windows.Forms;
//using NUnit.Framework;
//using ScalesUI.Common;
//using ScalesUI.Helpers;

//namespace WsScalesUITests.Helpers
//{
//    internal class PrintZebraHelperTests
//    {
//        #region Private fields and properties

//        /// <summary>
//        /// Помощник принтера Зебра.
//        /// </summary>
//        private PrintZebraHelper _printZebra { get; set; } = PrintZebraHelper.Instance;

//        #endregion

//        #region Public methods

//        [Test]
//        public void ResetAsync_Execute_DoesNotThrow()
//        {
//            foreach (EnumZebraAction action in Enum.GetValues(typeof(EnumZebraAction)))
//            {
//                Assert.DoesNotThrowAsync(() => _printZebra.ActionAsync(action, "127.0.0.1", 9100, SilentUI.True, new Button(), null, null, null));
//                TestContext.WriteLine($@"_printZebra.ActionAsync({action}, ""127.0.0.1"", 9100, SilentUI.True, new Button()).");
//            }
//        }

//        #endregion
//    }
//}
