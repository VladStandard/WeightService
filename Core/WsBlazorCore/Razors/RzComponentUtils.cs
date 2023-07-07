// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsBlazorCore.Razors;

//TODO: delete maybe
public static class RzComponentUtils
{
    public static CssStyleTableHeadModel GetTableHeadStyle(List<int> columnsWidths) => new(columnsWidths);
    
    public static CssStyleTableHeadModel GetTableHeadDefault() => new();
    
}