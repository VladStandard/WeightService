//using System;
//using System.Collections.Generic;
//using System.Xml.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ScalesUI.Common;
//using ScalesUI.Helpers;

//namespace ScalesUITests.Entities
//{
//    [TestClass()]
//    public class PLUEntityTests
//    {
//        #region Public fields and properties

//        private Dictionary<string, object> _dict;
//        private PluEntity _plu;

//        #endregion

//        #region Test methods

//        [TestCleanup]
//        public void Clean()
//        {
//            _plu = null;
//            _dict = null;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            var template = new TemplateEntity { TemplateId = "AFE3172D-894B-11EA-9E4C-4CCC6A93A440" };
//            template.Load();

//            _dict = new Dictionary<string, object>
//            {
//                {"Id", 6},
//                {"PLU", 101},
//                {"RRefGoods", "6176B9CA-7188-11E5-941C-001E6722B449"},
//                {"ScaleID", "F1A90176-894E-11EA-9E4C-4CCC6A93A440"},
//                {"GoodsName", "Владимирская  стандарт ц/ф в/у"},
//                { "GoodsDescription","Срок годности: при температуре от 0°С до + 6°С и относительной влажности воздуха  75-78% в искусственной оболочке, упакованной под вакуумом - 30 суток" },
//                { "GoodsFullName", "Изделия колбасные варёные. Продукт мясной категории Б. Колбаса варёная “Ветчина ВЛАДИМИРСКАЯ стандарт” охлаждённая.ТУ 10.13.14-007-91005552-2016" },
//                {"GTIN", "02600078000000"},
//                {"EAN13", ""},
//                {"ITF14", ""},
//                {"GoodsShelfLifeDays", 30},
//                {"GoodsTareWeight", 0.600m},
//                {"GoodsBoxQuantly", 12},
//                {"TemplateID", "A72B1386-8B5B-47CA-B604-1405C7A1E0EC"},
//                {"Template", template},
//                {"GLN", null},
//                {"ConsumerName", null}
//            };
//            _plu = PluEntity.ObjectFromDictionary<PluEntity>(_dict);
//        }

//        #endregion

//        #region Public methods

//        [TestMethod()]
//        public void ToStringTest()
//        {
//            PluEntity plu = PluEntity.ObjectFromDictionary<PluEntity>(_dict);
//            Assert.AreEqual(_plu.ToString(), plu.ToString());
//        }

//        [TestMethod()]
//        public void ObjectToDictionaryTest()
//        {
//            Dictionary<string, object> dict = (Dictionary<string, object>)PluEntity.ObjectToDictionary(_plu);
//            Assert.AreEqual(_dict.ToString(), dict.ToString());
//        }

//        [TestMethod()]
//        public void GetPLUTest()
//        {
//            PluEntity plu = PLUHelper.GetPLU(_dict["ScaleID"].ToString(), (Int32)(_dict["PLU"]));
//            Assert.AreEqual(_plu.ToString(), plu.ToString());
//        }

//        [TestMethod()]
//        public void GetBtXmlNamedSubStringTest()
//        {
//            PluEntity plu = PluEntity.ObjectFromDictionary<PluEntity>(_dict);
//            XDocument x = plu.GetBtXmlNamedSubString();
//            Assert.IsNotNull(x);
//        }

//        [TestMethod()]
//        public void GetPLUListTest()
//        {
//            List<PluEntity> pluList = PLUHelper.GetPLUList(_dict["ScaleID"].ToString());
//            PluEntity plu = PLUHelper.GetPLU(_dict["ScaleID"].ToString(), (Int32)(_dict["PLU"]));
//            foreach (PluEntity p in pluList)
//            {
//                if (plu.Equals(p))
//                {
//                    Assert.IsTrue(true);
//                    return;
//                }
//            }
//            Assert.Fail();
//        }

//        [TestMethod()]
//        public void SerializeObjectTest()
//        {
//            string ds = _plu.SerializeObject();
//            Assert.IsNotNull(ds);
//        }

//        #endregion
//    }
//}