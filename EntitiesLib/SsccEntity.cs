// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;


namespace WeightServices.Entities
{

    [Serializable]
    public class SsccEntity
    {

        [XmlElement("SSCC")]
        public String SSCC { get; set; }

        [XmlElement("GLN")]
        public String GLN { get; set; }

        [XmlElement("UnitID")]
        public Int32 UnitID { get; set; }

        [XmlElement("UnitType")]
        public Byte UnitType { get; set; }

        [XmlElement("SynonymSSCC")]
        public String SynonymSSCC { get; set; }

        [XmlElement("Check")]
        public Int32 Check { get; set; }


        public SsccEntity()
        {
        }


        public SsccEntity(string _sscc)
        {
            this.SSCC = _sscc;
            this.GLN = _sscc.Substring(3, 9);
            this.UnitID = Int32.Parse(_sscc.Substring(12, 7));
            this.UnitType = Byte.Parse(_sscc.Substring(2, 1));
            this.SynonymSSCC = $"(00){_sscc.Substring(3, 17)}";
            this.Check = Int32.Parse(_sscc.Substring(19, 1));
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{this.SynonymSSCC}");
            return sb.ToString();
        }

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SsccEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }


    }
}
