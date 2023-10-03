using WsPrintCore.Common;

namespace WsPrintCore.Zpl;

public static class ZplUtils
{
    #region Public and private methods

    private const string BlockFhFd = "^FH^FD";
    private const string BlockFs = "^FS";

    public static string ConvertStringToHex(string zpl)
    {
        if (string.IsNullOrEmpty(zpl)) return string.Empty;
        StringBuilder stringBuilder = new();
        WsEnumDataBlockPosition dataBlockPosition = WsEnumDataBlockPosition.Outside;
        List<string> hexReplace = new();
        for (int i = 0; i < zpl.Length; i++)
        {
            // ^FH^FD -- Field Hexadecimal Indicator & Field Data
            if (zpl.Length > i - 1 + BlockFhFd.Length)
            {
                if (zpl.Substring(i, BlockFhFd.Length) == BlockFhFd)
                    dataBlockPosition = WsEnumDataBlockPosition.Start;
            }
            // Data between ^FH^FD and ^FS.
            if (dataBlockPosition == WsEnumDataBlockPosition.Start)
            {
                if (i - BlockFhFd.Length > 0)
                {
                    if (zpl.Substring(i - BlockFhFd.Length - 1, BlockFhFd.Length) == BlockFhFd)
                        dataBlockPosition = WsEnumDataBlockPosition.Between;
                }
            }
            // ^FS -- Field Separator
            if (dataBlockPosition is WsEnumDataBlockPosition.Start or WsEnumDataBlockPosition.Between)
            {
                if (zpl.Length > i - 1 + BlockFs.Length)
                {
                    if (zpl.Substring(i, BlockFs.Length) == BlockFs)
                        dataBlockPosition = WsEnumDataBlockPosition.End;
                }
            }
            // Data between ^FH^FD and ^FS.
            if (dataBlockPosition == WsEnumDataBlockPosition.Between)
            {
                hexReplace.AddRange(from byte b in Encoding.UTF8.GetBytes(zpl[i].ToString())
                                    select $"_{BitConverter.ToString(new byte[] { b }).ToUpper()}");
            }
            if (dataBlockPosition == WsEnumDataBlockPosition.End)
            {
                dataBlockPosition = WsEnumDataBlockPosition.Outside;
                string hex = string.Join("", hexReplace);
                stringBuilder.Append(hex);
                stringBuilder.Append(Environment.NewLine);
                hexReplace = new();
            }
            if (dataBlockPosition != WsEnumDataBlockPosition.Between)
                stringBuilder.Append(zpl[i]);
        }
        return stringBuilder.ToString().Replace("_0D_0A", string.Empty);
    }

    #endregion
}