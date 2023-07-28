// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewRefModels.PluLines;

public interface IViewPluLineRepository
{
    List<WsSqlViewPluLineModel> GetList(WsSqlCrudConfigModel sqlCrudConfig);
}