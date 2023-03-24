// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Models;

[DebuggerDisplay("Type = {nameof(SqlDataConfigurationModel)} | {OrderAsc} | {PageNo} | {PageSize}")]
public record SqlDataConfigurationModel
{
    public bool OrderAsc { get; init; } = true;
    public int PageNo { get; init; } = 0;
    public int PageSize { get; init; } = 10;
}