using System.Runtime.Serialization;
using System.Xml;
using Ws.Labels.Service.Features.PrintLabel.Common;
using Ws.Labels.Service.Features.PrintLabel.Dto;
using Ws.Labels.Service.Features.PrintLabel.Models;
using Ws.Labels.Utils;
using Ws.Shared.Utils;

namespace Ws.Labels.Service.Features.PrintLabel;

public abstract class LabelGenerator
{
    public static LabelDto GenerateLabel(LabelDataDto dto)
    {
        if (dto.IsCheckWeight)
        {
            WeightLabelModel wLabelModel = dto.AdaptToWeightLabelModel();
            wLabelModel.PluFullName = wLabelModel.PluFullName.Replace("|", "");
            return GetZpl(dto, wLabelModel);
        }
        LabelModel labelModel = dto.AdaptToLabelModel();
        labelModel.PluFullName = labelModel.PluFullName.Replace("|", "").Replace("//n", "");
        return GetZpl(dto, labelModel);
    }
    
    private static LabelDto GetZpl<TItem>(LabelDataDto dto, TItem labelModel) where TItem : ISerializable, ILabelModel
    {
        string template = dto.Template;
        XmlDocument xmlLabelContext = XmlUtils.SerializeAsXmlDocument<TItem>(labelModel, true);
        template = template.Replace("Context", labelModel.GetType().Name);
        
        string zpl = XmlUtils.XsltTransformation(template, xmlLabelContext.OuterXml);
        zpl = XmlReplaceNextLine(zpl);
        zpl = ZplUtils.ConvertStringToHex(zpl);
        zpl = ZplUtils.PrintCmdReplaceZplResources(zpl, dto.Plu1СGuid);
        return new(zpl, labelModel);
    }
    
    private static string XmlReplaceNextLine(string xml) => xml.Replace("|", "\\&");
}