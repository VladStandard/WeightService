//using System;
//using System.IO;
//using System.Text;
//using System.Xml.Serialization;

//namespace DataProjectsCore.DAL.Entities
//{
//    [Serializable]
//    public class SsccEntity
//    {

//        [XmlElement("SSCC")]
//        public String SSCC     { get; set; }

//        [XmlElement("GLN")]
//        public String GLN      { get; set; }

//        [XmlElement("UnitID")]
//        public Int32  UnitID   { get; set; }

//        [XmlElement("UnitType")]
//        public Byte   UnitType { get; set; }

//        [XmlElement("SynonymSSCC")]
//        public String SynonymSSCC  { get; set; }

//        [XmlElement("Check")]
//        public Int32  Check        { get; set; }


//        public override string ToString()
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append($"{this.SynonymSSCC}");
//            return sb.ToString();
//        }

//        public string SerializeObject()
//        {
//            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SsccEntity));
//            using (StringWriter textWriter = new StringWriter())
//            {
//                xmlSerializer.Serialize(textWriter, this);
//                return textWriter.ToString();
//            }
//        }


//    }
//}
