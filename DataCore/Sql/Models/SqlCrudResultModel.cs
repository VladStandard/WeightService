// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Models;

public record SqlCrudResultModel
{
    public bool IsOk { get; set; }
    public Exception? Exception { get; set; }
}