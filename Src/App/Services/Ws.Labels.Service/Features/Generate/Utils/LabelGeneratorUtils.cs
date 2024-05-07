using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Ws.Labels.Service.Features.Generate.Models;
using Ws.Labels.Service.Features.Generate.Models.XmlLabelBase;
using Ws.Shared.Utils;

namespace Ws.Labels.Service.Features.Generate.Utils;

internal static partial class LabelGeneratorUtils
{
    [GeneratedRegex(@"\[(?!@)([^[\]]+)]")]
    private static partial Regex RegexOfResources();

    public static ZplInfo GetZpl<TItem>(ZplPrintItems zplPrintItems, TItem labelModel) where TItem :
        XmlLabelBaseModel, ISerializable
    {
        string template = zplPrintItems.Template;

        labelModel.PluFullName = labelModel.PluFullName.Replace("|", "");

        XmlDocument xmlLabelContext = XmlUtil.SerializeAsXmlDocument(labelModel);
        template = template.Replace("Context", labelModel.GetType().Name);

        string zpl = XmlUtil.XsltTransformation(template, xmlLabelContext.OuterXml);

        zpl = PrintCmdReplaceZplResources(zpl, zplPrintItems);

        return new(zpl, labelModel);
    }

    private static string PrintCmdReplaceZplResources(string zpl, ZplPrintItems zplPrintItems)
    {
        MatchCollection matches = RegexOfResources().Matches(zpl);

        foreach (Match match in matches)
        {
            string word = match.Value;

            if (zplPrintItems.Resources.TryGetValue(word.Trim('[', ']'), out string? value))
                zpl = zpl.Replace(word, value);
        }

        zpl = zpl.Replace("[@PLUS_STORAGE_METHODS_FK]", zplPrintItems.StorageMethod,
            StringComparison.OrdinalIgnoreCase);

        return zpl;
    }
}