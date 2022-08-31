// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Protocols
{
    public static class SerialPortsUtils
    {
        #region Public and private methods

        public static List<string> GetListComPorts(ShareEnums.Lang lang)
        {
            List<string> result = new();
            for (int i = 1; i < 256; i++)
            {
                switch (lang)
                {
                    case ShareEnums.Lang.Russian:
                        result.Add($"КОМ{i}");
                        break;
                    default:
                        result.Add($"COM{i}");
                        break;
                }
            }
            return result;
        }

        public static List<TypeModel<string>> GetListTypeComPorts(ShareEnums.Lang lang)
        {
            List<TypeModel<string>> result = new();
            for (int i = 1; i < 256; i++)
            {
                switch (lang)
                {
                    case ShareEnums.Lang.Russian:
                        result.Add(new TypeModel<string>($"КОМ{i}", $"COM{i}"));
                        break;
                    default:
                        result.Add(new TypeModel<string>($"COM{i}", $"COM{i}"));
                        break;
                }
            }
            return result;
        }

        #endregion
    }
}
