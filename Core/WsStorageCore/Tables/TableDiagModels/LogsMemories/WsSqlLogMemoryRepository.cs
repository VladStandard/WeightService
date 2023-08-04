// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.LogsMemories;

public class WsSqlLogMemoryRepository : WsSqlTableRepositoryBase<WsSqlLogModel>
{
    public void Save(string appName = "", WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.IsolatedAsync)
    {
        MemorySizeModel memorySize = new();
        memorySize.Execute();
        Save(memorySize.GetMemorySizeFreeMb(), memorySize.GetMemorySizeAppMb(), appName);
    }
    
    public void Save(short sizeAppMb, short sizeFreeMb, string appName = "",
        WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.IsolatedAsync)
    {
        if (sizeAppMb.Equals(0) || sizeFreeMb.Equals(0)) return;
        WsSqlAppModel app = new();
        if (!string.IsNullOrEmpty(appName))
            app = new WsSqlAppRepository().GetItemByName(appName);
        WsSqlLogMemoryModel logMemory = new()
        {
            CreateDt = DateTime.Now,
            SizeAppMb = sizeAppMb,
            SizeFreeMb = sizeFreeMb,
            App = app.IsExists ? app : WsSqlContextItemHelper.Instance.App,
            Device = WsSqlContextItemHelper.Instance.Device,
        };
        SqlCore.Save(logMemory, sessionType);
    }
}