//using System;
//using System.Collections.Generic;
//using System.Xml.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ScalesUI.Common;
//using ScalesUI.Helpers;

//namespace ScalesUITests.Entities
//{
//    [TestClass()]
//    public class WeighingFactEntityTests
//    {
//        #region Private fields and properties

//        private Dictionary<string, object> _dict;
//        private WeighingFactEntity _order;
//        private WeighingFactEntity _orderWithOrder;
//        private ScaleEntity _scale;

//        #endregion

//        #region Test methods

//        [TestCleanup]
//        public void Clean()
//        {
//            _order = null;
//            _dict = null;
//            _scale = null;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            _scale.Load("F1A90176-894E-11EA-9E4C-4CCC6A93A440");
//            var plu = PLUHelper.GetPLU("F1A90176-894E-11EA-9E4C-4CCC6A93A440", 101);

//            //refDict = new Dictionary<string, object>();
//            //refDict.Add("RegID", 0);
//            //refDict.Add("PLU", plu);
//            //refDict.Add("Order", null);
//            //refDict.Add("ScaleId", "F1A90176-894E-11EA-9E4C-4CCC6A93A440");

//            //refDict.Add("KneadingNumber", 11);
//            //refDict.Add("TemplateID", "A72B1386-8B5B-47CA-B604-1405C7A1E0EC");

//            //refDict.Add("Sscc", "");
//            //refDict.Add("NetWeight", 1524);
//            //refDict.Add("TareWeight", 150);
//            //refDict.Add("Calibre", 1000);
//            //refDict.Add("RegDate", DateTime.Now);

//            //refOrder = WeighingFactEntity.ObjectFromDictionary<WeighingFactEntity>(refDict);

//            _order = new WeighingFactEntity(plu, 1524, 150) { Scale = _scale };
//            WeightingHelper.SaveWeighingFact(_order);

//            _dict = new Dictionary<string, object>
//            {
//                {"Id", 26},
//                {"OrderType", 0},
//                {"RRefID", "D7051D4B-94FC-11EA-9E4C-4CCC6A93A440"},
//                {"PLU", plu},
//                {"PlaneBoxCount", 1},
//                {"FactBoxCount", 0},
//                {"PlanePalletCount", 1},
//                {"PlanePackingOperationBeginDate", DateTime.Parse("2020-05-13 00:00:00.000")},
//                {"PlanePackingOperationEndDate", DateTime.Parse("2020-05-13 11:59:59.000")},
//                {"ScaleID", "F1A90176-894E-11EA-9E4C-4CCC6A93A440"},
//                {"TemplateID", "A72B1386-8B5B-47CA-B604-1405C7A1E0EC"},
//                {"ProductDate", DateTime.Parse("2020-05-13 12:00:00.000")},
//                {"CreateDate", DateTime.Parse("2020-05-18 12:00:00.000")},
//                {"Status", OrderStatus.New}
//            };

//            var ord = OrderEntity.ObjectFromDictionary<OrderEntity>(_dict);
//            _orderWithOrder = new WeighingFactEntity(ord, 1524, 150) { Scale = _scale };
//        }

//        #endregion

//        #region Public methods

//        [TestMethod()]
//        public void GetNetWeightAsStingTest()
//        {
//            Assert.AreEqual(_order.GetWeightNettoAsSting(), "1,524");
//        }

//        [TestMethod()]
//        public void GetGrossWeightAsStingTest()
//        {
//            Assert.AreEqual(_order.GetGrossWeightAsSting(), "1,674");
//        }

//        [TestMethod()]
//        public void SerializeObjectTest()
//        {
//            _order.KneadingNumber = 10;
//            _order.Scale = _scale;
//            _order.WeightTare = 250;

//            string ds = _order.SerializeObject();
//            Assert.IsNotNull(ds);
//            Console.WriteLine($@"RESULT:${ds}");
//        }

//        [TestMethod()]
//        public void GetBtXmlCDataTest()
//        {
//            XDocument doc = _order.GetBtXmlCData();
//            Assert.IsNotNull(doc);
//            Console.WriteLine($@"RESULT: {doc}");
//        }

//        [TestMethod()]
//        public void GetBtXmlNamedSubStringTest()
//        {
//            var doc = _order.GetBtXmlNamedSubString();
//            Assert.IsNotNull(doc);
//            Console.WriteLine($@"RESULT: ${doc}");
//        }

//        [TestMethod()]
//        public void GetGS1AsStingTest()
//        {
//            string ds = _order.GetGS1AsSting();
//            Assert.IsNotNull(ds);
//            Console.WriteLine($@"RESULT: {ds}");
//        }

//        [TestMethod()]
//        public void SaveWeighingFactTest()
//        {
//            _scale.Load("F1A90176-894E-11EA-9E4C-4CCC6A93A440");
//            var plu = PLUHelper.GetPLU("F1A90176-894E-11EA-9E4C-4CCC6A93A440", 101);
//            var weighingFact = new WeighingFactEntity(plu, 1504) { KneadingNumber = 14, Scale = _scale, WeightTare = 250 };
//            WeightingHelper.SaveWeighingFact(weighingFact);
//            Assert.IsNotNull(weighingFact.Sscc);
//        }

//        [TestMethod()]
//        public void SaveWeighingFactWithOrderTest()
//        {
//            WeightingHelper.SaveWeighingFact(_orderWithOrder);
//            Assert.IsNotNull(_orderWithOrder.Sscc);
//        }

//        [TestMethod()]
//        public void GetXSLTTransformTest()
//        {
//            var xmlInput = _order.SerializeObject();
//            var template = new TemplateEntity("AFE3172D-894B-11EA-9E4C-4CCC6A93A440");
//            var zplContent = WeighingFactEntity.GetXSLTTransform(template.XslContent, WeighingFactEntity.Utf16ToUtf8(xmlInput));
//            Console.WriteLine(WeighingFactEntity.Utf16ToUtf8(zplContent));
//            Assert.IsNotNull(zplContent);
//        }

//        [TestMethod()]
//        public void ToCodePointsTest()
//        {
//            var xmlInput = _order.SerializeObject();
//            var template = new TemplateEntity("AFE3172D-894B-11EA-9E4C-4CCC6A93A440");
//            var zplContent = WeighingFactEntity.GetXSLTTransform(template.XslContent, xmlInput);
//            zplContent = WeighingFactEntity.ToCodePoints(zplContent);
//            Console.WriteLine(WeighingFactEntity.Utf16ToUtf8(zplContent));
//            Assert.IsNotNull(zplContent);
//        }

//        #endregion
//    }
//}