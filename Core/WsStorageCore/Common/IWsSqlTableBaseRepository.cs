// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.Common;

public interface IWsSqlTableBaseRepository<T> where T: WsSqlTableBase, new()
{
    //IList<T> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}