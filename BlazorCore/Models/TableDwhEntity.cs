// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;

namespace BlazorCore.Models
{
    public class TableDwhEntity : TableBase
    {
        public TableDwhEntity(ProjectsEnums.TableDwh value) : base(value.ToString()) { }
    }
}
