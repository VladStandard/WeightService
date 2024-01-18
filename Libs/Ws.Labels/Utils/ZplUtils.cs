using System.Text;
using System.Text.RegularExpressions;
using Ws.Labels.Enums;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.TemplatesResources;

namespace Ws.Labels.Utils;

public static partial class ZplUtils
{
    private const string BlockFhFd = "^FH^FD";
    private const string BlockFs = "^FS";
    
    [GeneratedRegex(@"\[(?!@)([^[\]]+)]")]
    private static partial Regex MyRegex();

    
    public static string PrintCmdReplaceZplResources(string zpl, Guid uid1C)
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
            SqlPluEntity plu = new SqlPluRepository().GetByUid1C(uid1C);
            string resourceHex = ConvertStringToHex(plu.StorageMethod.Zpl);
            zpl = zpl.Replace("[@PLUS_STORAGE_METHODS_FK]", resourceHex);
        }
        return zpl;
    }
    
    public static string ConvertStringToHex(string zpl)
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
        return stringBuilder.ToString().Replace("_0D_0A", string.Empty);
    }
}