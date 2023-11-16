using System.Text;
using PrinterCore.Enums;

namespace PrinterCore.Zpl;

public static class ZplUtils
{
    #region Public and private methods

    private const string BlockFhFd = "^FH^FD";
    private const string BlockFs = "^FS";

    public static string ConvertStringToHex(string zpl)
    {
        if (string.IsNullOrEmpty(zpl)) return string.Empty;
        StringBuilder stringBuilder = new();
        EnumDataBlockPosition dataBlockPosition = EnumDataBlockPosition.Outside;
        List<string> hexReplace = new();
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
                                    select $"_{BitConverter.ToString(new byte[] { b }).ToUpper()}");
            }
            if (dataBlockPosition == EnumDataBlockPosition.End)
            {
                dataBlockPosition = EnumDataBlockPosition.Outside;
                string hex = string.Join("", hexReplace);
                stringBuilder.Append(hex);
                stringBuilder.Append(Environment.NewLine);
                hexReplace = new();
            }
            if (dataBlockPosition != EnumDataBlockPosition.Between)
                stringBuilder.Append(zpl[i]);
        }
        return stringBuilder.ToString().Replace("_0D_0A", string.Empty);
    }

    #endregion
}