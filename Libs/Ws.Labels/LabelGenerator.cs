using System.Xml;
using Ws.Labels.Dto;
using Ws.Labels.Models;
using Ws.Labels.Utils;
using Ws.StorageCore.Models;
using Ws.StorageCore.Utils;

namespace Ws.Labels;

public class LabelGenerator
{
    public LabelDto GenerateLabel(LabelDataDto dto)
    {
        string zpl = string.Empty;
        string template = dto.Template;
        
        if (dto.IsCheckWeight)
        {
            WeightLabelModel labelModel = dto.AdaptToWeightLabelModel();
            XmlDocument xmlLabelContext = DataFormatUtils.SerializeAsXmlDocument<WeightLabelModel>
                (labelModel, true, true);
            template = template.Replace("PluLabelContextModel", nameof(WeightLabelModel));
            zpl = DataFormatUtils.XsltTransformation(template, xmlLabelContext.OuterXml);
            zpl = DataFormatUtils.XmlReplaceNextLine(zpl);
            zpl = ZplUtils.ConvertStringToHex(zpl);
            zpl = ZplUtils.PrintCmdReplaceZplResources(zpl, dto.PluNumber);
            return new(zpl, labelModel);
        }
        else
        {
            LabelModel labelModel = dto.AdaptToLabelModel();
            template = template.Replace("PluLabelContextModel", nameof(LabelModel));
            XmlDocument xmlLabelContext = DataFormatUtils.SerializeAsXmlDocument<LabelModel>
                (labelModel, true, true);
            zpl = DataFormatUtils.XsltTransformation(template, xmlLabelContext.OuterXml);
            zpl = DataFormatUtils.XmlReplaceNextLine(zpl);
            zpl = ZplUtils.ConvertStringToHex(zpl);
            zpl = ZplUtils.PrintCmdReplaceZplResources(zpl, dto.PluNumber);
            return new(zpl, labelModel);
        }
    }
}