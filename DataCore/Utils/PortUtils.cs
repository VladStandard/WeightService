// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using System.Collections.Generic;

namespace DataCore.Utils
{
    public static class PortUtils
    {
        #region Public and private methods

        public static List<TypeEntity<string>> GetComList()
        {
            List<TypeEntity<string>> result = new();
            for (int i = 1; i < 256; i++)
            {
                result.Add(new TypeEntity<string>($"COM{i}", $"COM{i}"));
            }
            return result;
        }

        #endregion
    }
}
