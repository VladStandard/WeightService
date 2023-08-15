// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.Common;

public interface IWsSqlViewBaseRepository<T> where T: WsSqlTableBase, new()
{
    public List<T> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}