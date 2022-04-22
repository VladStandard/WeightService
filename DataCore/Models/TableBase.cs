// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Models
{
    public class TableBase
    {
        #region Public and private fields and properties

        public string Name { get; set; }

        #endregion

        #region Constructor and destructor

        public TableBase(string name)
        {
            Name = name;
        }

        #endregion

        #region Public and private methods

        public override string ToString() => $"{nameof(Name)}: {Name}. ";

        #endregion
    }
}
