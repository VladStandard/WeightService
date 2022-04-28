// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using System.Text;
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;
//using System.Xml.Xsl;

//namespace DataCore.Sql.TableDirectModels
//{
//    /// <summary>
//    /// WeighingFactEntity.
//    /// </summary>
//    [Serializable]
//    public class WeighingFactEntity
//    {
//        #region Constructor

//        public WeighingFactEntity() : this(null, null, DateTime.Now.Date, DateTime.Now, 0, 0, 1000)
//        {
//            ExpirationDate = ProductDate.AddDays(PLU?.GoodsShelfLifeDays ?? 30);
//        }

//        public WeighingFactEntity(OrderEntity order) : this(order, order.PLU, order.ProductDate, DateTime.Now, 0, 0, 1000) { }

//        public WeighingFactEntity(OrderEntity order, int weightNetto, int weightTare = 0, int calibre = 1000) :
//            this(order, order.PLU, order.ProductDate, DateTime.Now, weightNetto - weightTare, weightTare, calibre) { }

//        public WeighingFactEntity(PluEntity plu) : this(null, plu, DateTime.Now.Date, DateTime.Now, 0, 0, 1000) { }

//        public WeighingFactEntity(PluEntity plu, int weightReal, int weightTare = 0, int calibre = 1000) :
//            this(null, plu, DateTime.Now.Date, DateTime.Now, weightReal - weightTare, weightTare, calibre) { }

//        public WeighingFactEntity(OrderEntity order, PluEntity plu, DateTime productDate, DateTime regDate, int weightNetto, int weightTare = 0, int calibre = 1000)
//        {
//            Order = order;
//            PLU = plu;
//            WeightNetto = weightNetto;
//            WeightTare = weightTare;
//            Calibre = calibre;
//            ProductDate = productDate;
//            RegDate = regDate;
//            if (PLU.GoodsShelfLifeDays != null)
//                ExpirationDate = ProductDate.AddDays((int)PLU.GoodsShelfLifeDays);
//        }

//        #endregion

//        #region Public fields and properties

//        public string RegId { get; set; }
//        public PluEntity PLU { get; set; }
//        public OrderEntity Order { get; set; }
//        public SsccEntity Sscc { get; set; }
//        public TemplateEntity Temp { get; set; }
//        public ScaleEntity Scale { get; set; }
//        public string ProductSeries { get; set; }

//        //[XmlElement(ElementName = "gs1BarCode")]
//        //public string Gs1BarCode {
//        //    get
//        //    {
//        //        return $"(01){this.PLU.GTIN}(3103){this.weightNetto.ToString("000000")}(11){this.ProductDate.ToString("yyMMdd")}(10){ this.ProductDate.ToString("MMdd")}>8(21)13265447>8";
//        //    }
//        //    set
//        //    {
//        //    }
//        //}

//        //[XmlElement(ElementName = "ssccBarCode")]
//        //public string ssccBarCode
//        //{
//        //    get
//        //    {
//        //        if (this.Sscc != null)
//        //            return $"{this.Sscc.SynonymSSCC}";
//        //        else
//        //            return null;
//        //    }
//        //    set
//        //    {
//        //    }
//        //}
//        public int KneadingNumber { get; set; }
//        public string TemplateId { get; set; }
//        public int WeightNetto { get; set; }
//        public int WeightTare { get; set; }
//        public int Calibre { get; set; }
//        public DateTime RegDate { get; set; }



//        #endregion

//        #region Public methods

//        public override bool Equals(object obj)
//        {
//            if (obj is WeighingFactEntity item)
//                return RegId.Equals(item.RegId);
//            return false;
//        }

//        public override int GetHashCode()
//        {
//            return base.GetHashCode();
//        }

//        public string GetGS1AsSting()
//        {
//            string w = @"000000" + WeightNetto.ToString();
//            w = w.Substring(w.Length - 6);
//            return "01" + PLU.GTIN +
//                    "3103" + w +
//                    "11" + RegDate.ToString("yyyyMMdd") +
//                    "10" + RegDate.ToString("MMdd");
//        }

//        public string GetWeightNettoAsSting()
//        {
//            return (WeightNetto / (double)Calibre).ToString("0.000");
//        }

//        public string GetGrossWeightAsSting()
//        {
//            return ((WeightNetto + WeightTare) / (double)Calibre).ToString("0.000");
//        }

//        public string GetWeightTareAsSting()
//        {
//            return (WeightTare / (double)Calibre).ToString("0.000");
//        }

//        public IDictionary<string, object> ObjectToDictionary<T>(T item) where T : class
//        {
//            Type myObjectType = item.GetType();
//            IDictionary<string, object> dict = new Dictionary<string, object>();
//            var indexer = new object[0];
//            PropertyInfo[] properties = myObjectType.GetProperties();
//            foreach (var info in properties)
//            {
//                var value = info.GetValue(item, indexer);
//                dict.Add(info.Name, value);
//            }
//            return dict;
//        }

//        public T ObjectFromDictionary<T>(IDictionary<string, object> dict) where T : class
//        {
//            Type type = typeof(T);
//            T result = (T)Activator.CreateInstance(type);
//            foreach (var item in dict)
//            {
//                type.GetProperty(item.Key)?.SetValue(result, item.Value, null);
//            }
//            return result;
//        }

//        public string SerializeObject()
//        {
//            var xmlSerializer = new XmlSerializer(typeof(WeighingFactEntity));
//            var settings = new XmlWriterSettings
//            {
//                ConformanceLevel = ConformanceLevel.Document,
//                OmitXmlDeclaration = false,
//                Encoding = Encoding.UTF8,
//                Indent = true,
//                IndentChars = "\t"
//            };
//            // не подавлять xml заголовок
//            // кодировка
//            // Какого то кипариса! эта настройка не работает
//            // и UTF16 записывается в шапку XML
//            // типа Visual Studio работает только с UTF16
//            // добавлять отступы
//            // сиволы отступа
//            var dummyNSs = new XmlSerializerNamespaces();
//            dummyNSs.Add(string.Empty, string.Empty);
//            using (StringWriter textWriter = new StringWriter())
//            {
//                using (XmlWriter xw = XmlWriter.Create(textWriter, settings))
//                {
//                    xmlSerializer.Serialize(xw, this, dummyNSs);
//                }
//                return textWriter.ToString();
//            }
//        }

//        //public DataSet ObjectToDataSet<T>()
//        //{
//        //    DataSet dataSet = new DataSet();
//        //    //DataTable table = new DataTable();
//        //    //table.TableName = "tbl0";
//        //    //dataSet.Tables.Add(table);
//        //    //Type myObjectType = item.GetType();
//        //    //IDictionary<string, object> dict = new Dictionary<string, object>();
//        //    //var indexer = new object[0];
//        //    //PropertyInfo[] properties = myObjectType.GetProperties();
//        //    //foreach (var info in properties)
//        //    //{
//        //    //    table.Columns.Add(info.Name, info.GetType());
//        //    //}
//        //    //DataRow row = table.Rows.Add();
//        //    //foreach (var info in properties)
//        //    //{
//        //    //    var value = info.GetValue(item, indexer);
//        //    //    row.SetField(info.Name, value);
//        //    //}
//        //    return dataSet;
//        //}

//        //public DataSet GetDataSet()
//        //{
//        //    // create simple dataset with one table
//        //    DataSet dataSet = new DataSet();
//        //    DataTable table = new DataTable();
//        //    table.TableName = "Employees";
//        //    dataSet.Tables.Add(table);
//        //    table.Columns.Add("GoodsName", typeof(string));
//        //    table.Columns.Add("SSCC", typeof(string));
//        //    table.Columns.Add("GS1", typeof(string));
//        //    table.Columns.Add("Description", typeof(string));
//        //    table.Columns.Add("ProductionDate", typeof(DateTime));
//        //    table.Columns.Add("ExpirationDate", typeof(DateTime));
//        //    table.Columns.Add("KneadingNumber", typeof(int));
//        //    table.Columns.Add("EAN13", typeof(string));
//        //    table.Columns.Add("weightNetto", typeof(decimal));
//        //    table.Columns.Add("WeightLine", typeof(string));
//        //    table.Columns.Add("GrossWeight", typeof(decimal));
//        //    table.Columns.Add("BoxCount", typeof(int));
//        //    table.Rows.Add(
//        //        PLU.GoodsFullName,
//        //        this.Sscc,
//        //       // this.GetGS1AsSting(),
//        //        PLU.GoodsDescription,
//        //        Order.ProductDate,
//        //        //Order.ProductDate.AddDays(PLU.GoodsShelfLifeDays),
//        //        this.KneadingNumber,
//        //        PLU.GTIN,
//        //        (Double)this.weightNetto / (Double)this.Сalibre,
//        //        PLU.ScaleID,
//        //        (Double)(this.weightNetto + this.weightTare) / (Double)this.Сalibre,
//        //        PLU.GoodsBoxQuantly
//        //        );
//        //    return dataSet;
//        //}


//        //public string ToCsv(DataTable table, string colSep = "", string rowSep = "\r\n")
//        //{
//        //    var format = string.Join(colSep,
//        //        Enumerable.Range(0, table.Columns.Count)
//        //        .Select(i => string.Format("{{{0}}}", i)));
//        //    return string.Join(rowSep,
//        //        table.Rows
//        //        .OfType<DataRow>()
//        //        .Select(i => string.Format(format, i.ItemArray)));
//        //}

//        ////<? xml version = "1.0" encoding = "utf-8" ?>
//        ////   < XMLScript Version = "2.0" >
//        ////      < Command Name = "Job1" >
//        ////          < Print >
//        ////          < Format > c:\BarTender\Document1.btw </ Format >
//        ////          < RecordSet Name = "Text File 1" Type = "btTextFile" >
//        ////              < Delimitation > btDelimQuoteAndComma </ Delimitation >
//        ////              < UseFieldNamesFromFirstRecord > true </ UseFieldNamesFromFirstRecord >
//        ////              < TextData >
//        ////                  < ![CDATA["FirstName", "LastName", "City", "Zip Code"
//        ////                  "Adam", "Jones", "Bellevue", "98008"
//        ////                  "John", "Smith", "Kirkland", "98293"]] >
//        ////              </ TextData >
//        ////          </ RecordSet >
//        ////          </ Print >
//        ////      </ Command >
//        ////</ XMLScript >

//        public XDocument GetBtXmlCData()
//        {
//            //var value = ToCsv(GetDataSet().Tables[0]);
//            //XDocument doc = new XDocument(
//            //    new XElement("XMLScript", new XAttribute("Version", "2.0"),
//            //    new XElement("Command",
//            //    new XElement("Print",
//            //        new XElement("Format", new XAttribute("TemplateID", TemplateID)),
//            //        new XElement("RecordSet",
//            //            new XAttribute("Name", "Text File 1")),
//            //            new XAttribute("Type", "btTextFile")),
//            //           new XElement("Delimitation", "btDelimQuoteAndComma"),
//            //           new XElement("UseFieldNamesFromFirstRecord", true)
//            //           //, new XElement("TextData", new XCData(value))
//            //           )
//            //          )
//            //       );

//            XDocument doc = new XDocument();
//            return doc;
//        }

//        ////<?xml version = "1.0" encoding="utf-8"?> 
//        ////<XMLScript Version = "2.0" >
//        ////  < Command Name="Job1">
//        ////      <Print> 
//        ////          <Format>c:\BarTender\Document1.btw</Format>
//        ////          <NamedSubString Name = "Product" >
//        ////              <Value> Chai Tea</Value>
//        ////          </NamedSubString> 
//        ////          <NamedSubString Name = "Price" >
//        ////              <Value>$18.00</Value>
//        ////          </NamedSubString> 
//        ////      </Print> 
//        ////</Command> 
//        ////</XMLScript> 

//        public XDocument GetBtXmlNamedSubString()
//        {

//            //IDictionary<string, object> dict = ObjectToDictionary<WeighingFactEntity>(this);
//            ////var obj = ObjectFromDictionary<WeighingFactEntity>(dict);

//            //XDocument doc = new XDocument(
//            //    new XElement("XMLScript", new XAttribute("Version", "2.0"),
//            //    new XElement("Command",
//            //    new XElement("Print",
//            //        new XElement("Format", new XAttribute("TemplateID", TemplateID)),
//            //        dict.Select(x => new XElement("NamedSubString",
//            //                new XAttribute("Name", x.Key),
//            //                new XElement("Value", x.Value)
//            //            ))

//            //    ))));

//            XDocument doc = new XDocument();
//            return doc;
//        }

//        public string GetXSLTTransform(string xslInput, string xmlInput)
//        {
//            string result;
//            using (var srt = new StringReader(xslInput)) // xslInput is a string that contains xsl
//            using (var sri = new StringReader(xmlInput)) // xmlInput is a string that contains xml
//            {
//                using (var xrt = XmlReader.Create(srt))
//                using (var xri = XmlReader.Create(sri))
//                {
//                    XslCompiledTransform xslt = new XslCompiledTransform();
//                    xslt.Load(xrt);
//                    using (StringWriter sw = new StringWriter())
//                    using (XmlWriter xwo = XmlWriter.Create(sw, xslt.OutputSettings)) // use OutputSettings of xsl, so it can be output as HTML
//                    {
//                        xslt.Transform(xri, xwo);
//                        result = sw.ToString();
//                    }
//                }
//            }
//            return result;
//        }

//        public string ToCodePoints(string zplInput)
//        {
//            var ret = new StringBuilder();
//            var unicodeCharacterList = new Dictionary<char, string>();
//            foreach (var ch in zplInput)
//            {
//                if (!unicodeCharacterList.ContainsKey(ch))
//                {
//                    var bytes = Encoding.UTF8.GetBytes(ch.ToString());
//                    if (bytes.Length > 1)
//                    {
//                        var hexCode = string.Empty;
//                        foreach (var b in bytes)
//                        {
//                            hexCode += $"_{BitConverter.ToString(new byte[] { b }).ToLower()}";
//                        }

//                        unicodeCharacterList[ch] = hexCode;
//                    }
//                    else
//                        unicodeCharacterList[ch] = ch.ToString();

//                    ret.Append(unicodeCharacterList[ch]);
//                }
//                else
//                    ret.Append(unicodeCharacterList[ch]);
//            }
//            return ret.ToString();
//        }

//        public string Utf16ToUtf8(string utf16String)
//        {
//            /**************************************************************
//             * Every .NET string will store text with the UTF16 encoding, *
//             * known as Encoding.Unicode. Other encodings may exist as    *
//             * Byte-Array or incorrectly stored with the UTF16 encoding.  *
//             *                                                            *
//             * UTF8 = 1 bytes per char                                    *
//             *    ["100" for the ansi 'd']                                *
//             *    ["206" and "186" for the russian 'κ']                   *
//             *                                                            *
//             * UTF16 = 2 bytes per char                                   *
//             *    ["100, 0" for the ansi 'd']                             *
//             *    ["186, 3" for the russian 'κ']                          *
//             *                                                            *
//             * UTF8 inside UTF16                                          *
//             *    ["100, 0" for the ansi 'd']                             *
//             *    ["206, 0" and "186, 0" for the russian 'κ']             *
//             *                                                            *
//             * We can use the convert encoding function to convert an     *
//             * UTF16 Byte-Array to an UTF8 Byte-Array. When we use UTF8   *
//             * encoding to string method now, we will get a UTF16 string. *
//             *                                                            *
//             * So we imitate UTF16 by filling the second byte of a char   *
//             * with a 0 byte (binary 0) while creating the string.        *
//             **************************************************************/

//            // Storage for the UTF8 string
//            string utf8String = string.Empty;

//            // Get UTF16 bytes and convert UTF16 bytes to UTF8 bytes
//            byte[] utf16Bytes = Encoding.Unicode.GetBytes(utf16String);
//            byte[] utf8Bytes = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, utf16Bytes);

//            // Fill UTF8 bytes inside UTF8 string
//            for (var i = 0; i < utf8Bytes.Length; i++)
//            {
//                // Because char always saves 2 bytes, fill char with 0
//                byte[] utf8Container = new byte[2] { utf8Bytes[i], 0 };
//                utf8String += BitConverter.ToChar(utf8Container, 0);
//            }

//            // Return UTF8
//            return utf8String;
//        }

//        public string AnsiToUtf8(string text)
//        {
//            // encode the string as an ASCII byte array
//            byte[] myASCIIBytes = Encoding.ASCII.GetBytes(text);

//            // convert the ASCII byte array to a UTF-8 byte array
//            byte[] myUTF8Bytes = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, myASCIIBytes);

//            // reconstitute a string from the UTF-8 byte array 
//            return Encoding.UTF8.GetString(myUTF8Bytes);
//        }

//        #endregion
//    }
//}
