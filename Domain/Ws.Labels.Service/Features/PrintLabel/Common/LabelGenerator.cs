using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Ws.Database.Core.Entities.Scales.TemplatesResources;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Shared.Utils;

namespace Ws.Labels.Service.Features.PrintLabel.Common;


file enum EnumDataBlockPosition
{
    Outside = 0,
    Start = 1,
    Between = 2,
    End = 3
}

public static partial class LabelGenerator
{
    [GeneratedRegex(@"\[(?!@)([^[\]]+)]")]
    private static partial Regex MyRegex();

    private const string BlockFhFd = "^FH^FD";
    private const string BlockFs = "^FS";
    
    public static LabelReadyDto GetZpl<TItem>(string template, PluEntity plu, TItem labelModel) where TItem : 
        XmlLabelBaseModel, ISerializable
    {
        labelModel.PluFullName = labelModel.PluFullName.Replace("|", "");
        
        XmlDocument xmlLabelContext = XmlUtils.SerializeAsXmlDocument<TItem>(labelModel, true);
        template = template.Replace("Context", labelModel.GetType().Name);
        
        string zpl = XmlUtils.XsltTransformation(template, xmlLabelContext.OuterXml);
        zpl = ConvertStringToHex(zpl);
        zpl = PrintCmdReplaceZplResources(zpl, plu);
        return new(zpl, labelModel);
    }
    
    private static string PrintCmdReplaceZplResources(string zpl, PluEntity plu)
    {
        if (string.IsNullOrEmpty(zpl))
            throw new ArgumentException("Value must be fill!", nameof(zpl)); 
        
        MatchCollection matches = MyRegex().Matches(zpl);
        foreach (Match match in matches)
        {
            string word = match.Value;
            string replacement = new SqlTemplateResourceRepository().GetByName(word.Trim('[', ']')).Data.ValueUnicode;
            if (string.IsNullOrEmpty(replacement)) continue;
            zpl = zpl.Replace(word, ConvertStringToHex(replacement));
        }

        if (zpl.Contains("[@PLUS_STORAGE_METHODS_FK]"))
        {
            string resourceHex = ConvertStringToHex(plu.StorageMethod.Zpl);
            zpl = zpl.Replace("[@PLUS_STORAGE_METHODS_FK]", resourceHex);
        }
        return zpl;
    }
    
    private static string ConvertStringToHex(string zpl)
    {
        if (string.IsNullOrEmpty(zpl)) return string.Empty;
        StringBuilder stringBuilder = new();
        EnumDataBlockPosition dataBlockPosition = EnumDataBlockPosition.Outside;
        List<string> hexReplace = [];
        for (int i = 0; i < zpl.Length; i++)
        {
            // ^FH^FD -- Field Hexadecimal Indicator & Field Data
            if (zpl.Length > i - 1 + BlockFhFd.Length)
            {
                if (zpl.Substring(i, BlockFhFd.Length) == BlockFhFd)
                    dataBlockPosition = EnumDataBlockPosition.Start;
            }
            // Data between ^FH^FD and ^FS.
            if (dataBlockPosition == EnumDataBlockPosition.Start)
            {
                if (i - BlockFhFd.Length > 0)
                {
                    if (zpl.Substring(i - BlockFhFd.Length - 1, BlockFhFd.Length) == BlockFhFd)
                        dataBlockPosition = EnumDataBlockPosition.Between;
                }
            }
            // ^FS -- Field Separator
            if (dataBlockPosition is EnumDataBlockPosition.Start or EnumDataBlockPosition.Between)
            {
                if (zpl.Length > i - 1 + BlockFs.Length)
                {
                    if (zpl.Substring(i, BlockFs.Length) == BlockFs)
                        dataBlockPosition = EnumDataBlockPosition.End;
                }
            }
            // Data between ^FH^FD and ^FS.
            if (dataBlockPosition == EnumDataBlockPosition.Between)
            {
                hexReplace.AddRange(from byte b in Encoding.UTF8.GetBytes(zpl[i].ToString())
                                    select $"_{BitConverter.ToString([b]).ToUpper()}");
            }
            if (dataBlockPosition == EnumDataBlockPosition.End)
            {
                dataBlockPosition = EnumDataBlockPosition.Outside;
                string hex = string.Join("", hexReplace);
                stringBuilder.Append(hex);
                stringBuilder.Append(Environment.NewLine);
                hexReplace = [];
            }
            if (dataBlockPosition != EnumDataBlockPosition.Between)
                stringBuilder.Append(zpl[i]);
        }
        return stringBuilder.Replace("_0D_0A", string.Empty).Replace("|", "\\&").ToString();
    }
}