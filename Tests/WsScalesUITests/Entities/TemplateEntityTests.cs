//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ScalesUI.Common;

//namespace WsScalesUITests.Entities
//{
//    [TestClass()]
//    public class TemplateEntityTests
//    {
//        #region Private fields and properties

//        private Dictionary<string, object> _dict;
//        private TemplateEntity _order;

//        #endregion

//        #region Test methods

//        [TestCleanup]
//        public void Clean()
//        {
//            _order = null;
//            _dict = null;
//        }

//        [TestInitialize]
//        public void Init()
//        {

//            _dict = new Dictionary<string, object>
//            {
//                {"CategoryID", "Категория 1"},
//                {"Title", "Шаблон по умолчанию"},
//                {
//                    "XslContent",
//                    "<?xml version=\"1.0\" encoding=\"UTF-8\"?> <xsl:stylesheet version=\"2.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"> <xsl:output method=\"text\" encoding=\"UTF-16\" omit-xml-declaration=\"no\"/>   <xsl:template match=\"/\"> <xsl:text>^XA</xsl:text><xsl:text>&#10;</xsl:text> <xsl:text>^CFA,50 ^FO10,10 ^FB350,3,0,C,0 </xsl:text><xsl:text>&#10;</xsl:text>  <xsl:text>^FD</xsl:text> <xsl:value-of select=\"WeighingFactEntity/PLU/GoodsTareWeight\"/> <xsl:text>^FS</xsl:text><xsl:text>&#10;</xsl:text>  <xsl:text>^CFA,50 ^FO10,10 ^FB350,3,0,C,0</xsl:text><xsl:text>&#10;</xsl:text> <xsl:text>^FH^FD</xsl:text> <xsl:value-of select=\"WeighingFactEntity/PLU/GoodsName\"/> <xsl:text>^FS</xsl:text><xsl:text>&#10;</xsl:text>  <xsl:text>^XZ</xsl:text><xsl:text>&#10;</xsl:text> </xsl:template> </xsl:stylesheet>"
//                },
//                {"TemplateID", "AFE3172D-894B-11EA-9E4C-4CCC6A93A440"}
//            };
//            _order = TemplateEntity.ObjectFromDictionary<TemplateEntity>(_dict);

//        }

//        #endregion

//        #region Public methods

//        [TestMethod()]
//        public void LoadTest()
//        {
//            TemplateEntity refOrderLocal = new TemplateEntity { TemplateId = "AFE3172D-894B-11EA-9E4C-4CCC6A93A440" };
//            refOrderLocal.Load();
//            Assert.IsNotNull(refOrderLocal.XslContent);
//            Assert.AreEqual(refOrderLocal, _order);
//        }

//        [TestMethod()]
//        public void TemplateEntityTest()
//        {
//            TemplateEntity refOrderLocal = new TemplateEntity("AFE3172D-894B-11EA-9E4C-4CCC6A93A440");
//            Assert.IsNotNull(refOrderLocal.XslContent);
//            Assert.AreEqual(refOrderLocal, _order);
//        }

//        #endregion
//    }
//}

