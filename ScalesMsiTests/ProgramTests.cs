// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Deployment.WindowsInstaller;
using NUnit.Framework;
using ScalesMsi.Helpers;

namespace ScalesMsiTests
{
    internal class ProgramTests
    {
        /// <summary>
        /// Помощник приложения.
        /// </summary>
        private static readonly AppHelper _app = AppHelper.Instance;

        internal static void Main()
        {
        }

        /// <summary>
        /// Setup private fields.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Setup)} start.");
            //
            TestContext.WriteLine($@"{nameof(Setup)} complete.");
        }

        /// <summary>
        /// Reset private fields to default state.
        /// </summary>
        [TearDown]
        public void Teardown()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Teardown)} start.");
            //
            TestContext.WriteLine($@"{nameof(Teardown)} complete.");
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        }

        [Test]
        public void CustomActions_InstallRegistry_AreEqual()
        {
            UtilsTests.MethodStart();
            Assert.AreEqual(ActionResult.Success, ActionResult.Success);
            UtilsTests.MethodComplete();
        }
    }
}
