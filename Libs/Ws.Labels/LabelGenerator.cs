using System.Runtime.Serialization;
using System.Xml;
using Ws.Labels.Common;
using Ws.Labels.Dto;
using Ws.Labels.Models;
using Ws.Labels.Utils;
using Ws.StorageCore.Utils;

namespace Ws.Labels;

public abstract class LabelGenerator
{
    public static LabelDto GenerateLabel(LabelDataDto dto)
    {
        if (dto.IsCheckWeight)
        {
            WeightLabelModel wLabelModel = dto.AdaptToWeightLabelModel();
            return GetZpl(dto, wLabelModel);
        }
        LabelModel labelModel = dto.AdaptToLabelModel(); 
        return GetZpl(dto, labelModel);
    }
    
    private static LabelDto GetZpl<TItem>(LabelDataDto dto, TItem labelModel) where TItem : ISerializable, ILabelModel
    {
        string template = dto.Template;
        XmlDocument xmlLabelContext = DataFormatUtils.SerializeAsXmlDocument<TItem>
            (labelModel, true, true);
        template = template.Replace("Context", labelModel.GetType().Name);
        
        string zpl = DataFormatUtils.XsltTransformation(template, xmlLabelContext.OuterXml);
        zpl = DataFormatUtils.XmlReplaceNextLine(zpl);
        zpl = ZplUtils.ConvertStringToHex(zpl);
        zpl = ZplUtils.PrintCmdReplaceZplResources(zpl, dto.PluNumber);
        return new(zpl, labelModel);
    }
}