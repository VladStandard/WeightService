//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ScalesUI.Common;

//namespace WsScalesUITests.Common
//{
//    [TestClass()]
//    public class DeviceStatusTests
//    {
//        #region Private fields and properties

//        private DeviceStatus _sessionState { get; set; } = DeviceStatus.Instance;

//        #endregion

//        #region Public methods

//        [TestMethod()]
//        public void SerializeObjectTest()
//        {
//            var x = _sessionState.SerializeObject();
//            Console.WriteLine(x);
//            Assert.IsNotNull(x);
//        }

//        #endregion
//    }
//}