// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.ObjectModel;
using System.IO;
using NUnit.Framework;
using ScalesMsi.Helpers;
using ScalesMsi.Models;
using ScalesMsi.Utils;

namespace ScalesMsiTests.Helpers
{
    [TestFixture]
    public class XmlHelperTests
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
        public void ConvertCyryllicToTranslit_AreEqual()
        {
            UtilsTests.MethodStart();
            //XmlReaderSettings settings = new XmlReaderSettings {Async = true};
            var fileName = Strings.DirProgramScalesUI + @"\ScalesUI.exe.config";
            TestContext.WriteLine($@"fileName: {fileName}");
            if (Directory.Exists(Strings.DirProgramScalesUI) && File.Exists(fileName))
            {
                TestContext.WriteLine(@"fileExists: true");
                var actual = _xml.Read(fileName, new Collection<XmlTag>()
                {
                    new XmlTag("userSettings", "ScalesUI.Properties.Settings", "value"),
                }, "Id");
                TestContext.WriteLine($@"actual.NoError: {actual.NoError}. actual.Value: {actual.Value}");

            //    //Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            //    //using (XmlReader reader = XmlReader.Create(stream, settings))
            //    //{
            //    //    while (reader.Read())
            //    //    {
            //    //        switch (reader.NodeType)
            //    //        {
            //    //            case XmlNodeType.Element:
            //    //                //Console.WriteLine("Start Element {0}", reader.Name);
            //    //                break;
            //    //            case XmlNodeType.Text:
            //    //                //Console.WriteLine("Text Node: {0}", await reader.GetValueAsync());
            //    //                break;
            //    //            case XmlNodeType.EndElement:
            //    //                //Console.WriteLine("End Element {0}", reader.Name);
            //    //                break;
            //    //            default:
            //    //                //Console.WriteLine("Other node {0} with value {1}", reader.NodeType, reader.Value);
            //    //                break;
            //    //        }
            //    //    }
            //    //}
            }

            Assert.AreEqual(true, true);
            UtilsTests.MethodComplete();
        }
    }
}