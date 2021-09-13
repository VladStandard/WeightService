//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ScalesUI.Common;

//namespace ScalesUITests.Common
//{
//    [TestClass()]
//    public class DeviceStatusTests
//    {
//        #region Private fields and properties

//        private readonly DeviceStatus _sessionState = DeviceStatus.Instance;

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