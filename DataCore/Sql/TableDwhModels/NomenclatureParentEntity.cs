// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDwhModels
{
    public class NomenclatureParentEntity
    {
        // {"parents":["Колбасные изделия","Колбаса п/к","яВот такая с беконом п.к. 450 г"]}
        public string[] Parents { get; set; }

        public NomenclatureParentEntity(string[] parents)
        {
            Parents = parents;
            //Parents = new string[0];
        }

        public override string ToString() => 
            $"{nameof(Parents)}: {string.Join(",", Parents)}. ";
    }
}