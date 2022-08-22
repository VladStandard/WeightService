// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;

namespace BlazorCore.Models.CssStyles;

public class TableBodyStyleModel : IBaseStyleModel
{
    #region Public and private fields, properties, constructor

    public ColumnName IdentityName { get; set; }
    public bool IsShowMarked { get; set; }

    public TableBodyStyleModel() : this(ColumnName.Default, false)
    {
        //
    }

    public TableBodyStyleModel(ColumnName identityName, bool isShowMarked)
    {
        IdentityName = identityName;
        IsShowMarked = isShowMarked;
    }

    #endregion

    #region Public and private methods

    //

    #endregion
}
