// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using DataShareCore.Interfaces;

namespace DataProjectsCore.Models
{
    public class TableScaleEntity : ITableEntity
    {
        public ProjectsEnums.TableScale Value { get; set; }

        public TableScaleEntity(ProjectsEnums.TableScale value)
        {
            Value = value;
        }

        public override string ToString() => $"{nameof(Value)}: {Value}. ";
    }
}
