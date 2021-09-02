// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.VisualStudio.TestTools.UnitTesting;
using terra.Controllers;

namespace terra2.Tests.Controllers
{
    [TestClass()]
    public class ContragentControllerTest
    {
        [TestMethod()]
        public void GetTest()
        {

            var controller = new ContragentController();
            var result = controller.Get(-2147483492);
            Assert.AreEqual("response", result.Content);
        }

        [TestMethod()]
        public void GetTest1()
        {
            Assert.Fail();
        }
    }
}

