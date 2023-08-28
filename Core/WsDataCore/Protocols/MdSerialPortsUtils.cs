namespace WsDataCore.Protocols;

// TODO: FIX Serial Ports
public static class MdSerialPortsUtils
{
    #region Public and private methods

    public static List<string> GetListComPorts(WsEnumLanguage lang)
    {
        List<string> result = new();
        for (int i = 1; i < 256; i++)
        {
            switch (lang)
            {
                case WsEnumLanguage.Russian:
                    result.Add($"КОМ{i}");
                    break;
                default:
                    result.Add($"COM{i}");
                    break;
            }
        }
        return result;
    }

    public static List<WsEnumTypeModel<string>> GetListTypeComPorts(WsEnumLanguage lang)
    {
        List<WsEnumTypeModel<string>> result = new();
        for (int i = 1; i < 256; i++)
        {
            switch (lang)
            {
                case WsEnumLanguage.Russian:
                    result.Add(new($"КОМ{i}", GenerateComPort(i)));
                    break;
                default:
                    result.Add(new($"COM{i}", GenerateComPort(i)));
                    break;
            }
        }
        return result;
    }
        
    public static string GenerateComPort(int number) => $"COM{number}";

    #endregion
}