// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDiagModels.LogsMemories;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
    #region Public and private methods

    /// <summary>
    /// Log memory info.
    /// </summary>
    /// <param name="sizeAppMb"></param>
    /// <param name="sizeFreeMb"></param>
    public void LogMemory(short sizeAppMb, short sizeFreeMb)
    {
        LogMemoryModel logMemory = new()
        {
            CreateDt = DateTime.Now,
            SizeAppMb = sizeAppMb,
            SizeFreeMb = sizeFreeMb,
            App = App,
            Device = Device,
        };
        SaveAsync(logMemory).ConfigureAwait(false);
    }

    #endregion
}