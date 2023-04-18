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

//        #region Public methods

//        [Test]
//        public void InitComPort_Execute_DoesNotThrow()
//        {
//            Assert.DoesNotThrow(() => _serialPort.InitComPort("COM3"));
//        }

//        [Test]
//        public void OpenComPort_Execute_Throws()
//        {
//            Assert.DoesNotThrow(() => _serialPort.InitComPort("COM124"));
//            Assert.DoesNotThrow(() => _serialPort.OpenComPort());
//            Assert.AreEqual(_serialPort.ComPortException.Message, "Порт 'COM124' не существует.");
//        }

//        [Test]
//        public void OpenComPort_Execute_DoesNotThrow()
//        {
//            Assert.DoesNotThrow(() => _serialPort.OpenComPort());
//            Assert.DoesNotThrow(() => _serialPort.InitComPort("COM1"));
//            Assert.DoesNotThrow(() => _serialPort.OpenComPort());
//        }

//        [Test]
//        public void CloseComPort_Execute_DoesNotThrow()
//        {
//            Assert.DoesNotThrow(() => _serialPort.CloseComPort());
//            Assert.DoesNotThrow(() => _serialPort.InitComPort("COM1"));
//            Assert.DoesNotThrow(() => _serialPort.CloseComPort());
//        }

//        #endregion
//    }
//}
