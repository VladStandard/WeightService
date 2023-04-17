//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ScalesUI.Common;
//using ScalesUI.Helpers;

//namespace ScalesUITests.Entities
//{
//    [TestClass()]
//    public class ScaleEntityTests
//    {
//        #region Private fields and properties

//        private ScaleEntity _scale;

//        #endregion

//        #region Test methods

//        [TestCleanup]
//        public void Cleanup()
//        {
//            _scale = null;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            _scale = new ScaleEntity("F1A90176-894E-11EA-9E4C-4CCC6A93A440");
//        }

//        #endregion

//        #region Public methods

//        [TestMethod()]
//        public void SerializeObjectTest()
//        {
//            string ds = _scale.SerializeObject();
//            Assert.IsNotNull(ds);
//        }

//        [TestMethod()]
//        public void GetMacAddressTest()
//        {
//            string ds = _scale.GetMacAddress();
//            Assert.IsNotNull(ds);
//        }

//        [TestMethod()]
//        public void SendToPrinterTest()
//        {
//            var random = new Random();

//            _scale.Load("F1A90176-894E-11EA-9E4C-4CCC6A93A440", SilentUI.False);
//            PluEntity plu = PLUHelper.GetPLU("F1A90176-894E-11EA-9E4C-4CCC6A93A440", 50);
//            TemplateEntity template = new TemplateEntity("36432385-A0BC-11EA-9E4D-4CCC6A93A440");

//            WeighingFactEntity weighingFact = new WeighingFactEntity(plu, random.Next(21000))
//            {
//                KneadingNumber = random.Next(100),
//                Scale = _scale,
//                WeightTare = random.Next(2000)
//            };
//            WeightingHelper.SaveWeighingFact(weighingFact);

//            string xmlInput = weighingFact.SerializeObject();
//            string zplContent = WeighingFactEntity.GetXSLTTransform(template.XslContent, xmlInput);
//            zplContent = WeighingFactEntity.ConvertStringToHex(zplContent);
//            _scale.SendToPrinter(zplContent.Split('\n'));

//            Console.WriteLine(WeighingFactEntity.Utf16ToUtf8(zplContent));
//            Assert.IsNotNull(zplContent);
//        }

//        #endregion
//    }
//}