// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;
using System.Xml.Serialization;

namespace DataBaseCore.DAL.TableModels
{
    [Serializable]
    public class SsccEntity : BaseEntity<SsccEntity>
    {

        [XmlElement("SSCC")]
        public string SSCC { get; set; }

        [XmlElement("GLN")]
        public string GLN { get; set; }

        [XmlElement("UnitID")]
        public int UnitID { get; set; }

        [XmlElement("UnitType")]
        public byte UnitType { get; set; }

        [XmlElement("SynonymSSCC")]
        public string SynonymSSCC { get; set; }

        [XmlElement("Check")]
        public int Check { get; set; }

        public SsccEntity() { }

        public SsccEntity(string _sscc)
        {
            SSCC = _sscc;
            GLN = _sscc.Substring(3, 9);
            UnitID = int.Parse(_sscc.Substring(12, 7));
            UnitType = byte.Parse(_sscc.Substring(2, 1));
            SynonymSSCC = $"(00){_sscc.Substring(3, 17)}";
            Check = int.Parse(_sscc.Substring(19, 1));
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{SynonymSSCC}");
            return sb.ToString();
        }
    }
}
