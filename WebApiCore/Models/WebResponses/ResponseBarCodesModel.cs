using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using WebApiCore.Models.WebRequests;
using WebApiCore.Utils;


namespace WebApiCore.Models.WebResponses
{
    [XmlRoot(WebConstants.Record, Namespace = "", IsNullable = false)]
    public class ResponseBarCodeModels : SerializeBase
    {

        [XmlArray]
        public List<ResponseSingleBarCodeModel> BarCodes { get; set; }

        public ResponseBarCodeModels(List<BarCodeModel> barCodes)
        {
            BarCodes = new List<ResponseSingleBarCodeModel>();
            foreach (var barCode in barCodes)
                BarCodes.Add(WebResponseUtils.CastFromBarCodeModel(barCode));
        }

        public ResponseBarCodeModels() {}

        public ResponseBarCodeModels(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            //  BarCodes = info.GetString(nameof(BarCodes)) ?? BarCodes.Cl;
        }
    }
}
