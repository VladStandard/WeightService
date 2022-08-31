// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Models;

public class TableSystemModel : TableBase
{
    public TableSystemModel(ProjectsEnums.TableSystem value) : base(value.ToString()) { }
}
