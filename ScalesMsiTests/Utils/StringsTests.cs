// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using ScalesMsi.Utils;

namespace ScalesMsiTests.Utils
{
    [TestFixture]
    public class StringsTests
    {
        /// <summary>
        /// Setup private fields.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            UtilsTests.MethodStart();
            // 
            UtilsTests.MethodComplete();
        }

        /// <summary>
        /// Reset private fields to default state.
        /// </summary>
        [TearDown]
        public void Teardown()
        {
            UtilsTests.MethodStart();
            // 
            UtilsTests.MethodComplete();
        }

        [Test]
        public void ConvertCyryllicToTranslit_AreEqual()
        {
            UtilsTests.MethodStart();

            var actual = Strings.ConvertCyryllicToTranslit("Тестовое сообщение");
            var expected = "testovoe_soobshchenie";
            TestContext.WriteLine($"actual/expected: {actual}");
            Assert.AreEqual(expected, actual);

            actual = Strings.ConvertCyryllicToTranslit("Руководство пользователя.docx");
            expected = "rukovodstvo_poljzovatelya.docx";
            TestContext.WriteLine($"actual/expected: {actual}");
            Assert.AreEqual(expected, actual);

            UtilsTests.MethodComplete();
        }
    }
}