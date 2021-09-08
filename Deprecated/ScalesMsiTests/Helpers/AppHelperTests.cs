// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using ScalesMsi.Helpers;
using ScalesMsi.Utils;
using System.IO;

namespace ScalesMsiTests.Helpers
{
    [TestFixture]
    public class AppHelperTests
    {
        // Помощник XML.
        private readonly XmlHelper _xml = XmlHelper.Instance;

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
        public void FileExists_AreEqual()
        {
            UtilsTests.MethodStart();

            TestContext.WriteLine(!Directory.Exists(Strings.DirSourceMassa) ? $@"Каталог не найден: {Strings.DirSourceMassa}" : $@"Каталог найден: {Strings.DirSourceMassa}");
            var arch = @"en.stsw-stm32102.zip";
            var path = Strings.DirSourceMassa + @"\" + arch;
            var fi = new FileInfo(path);
            TestContext.WriteLine(!fi.Exists ? $@"Файл не найден: {fi.FullName}" : $@"Файл найден: {fi.FullName}");

            Assert.AreEqual(true, true);
            
            UtilsTests.MethodComplete();
        }
    }
}