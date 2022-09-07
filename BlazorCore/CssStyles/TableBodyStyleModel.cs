// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorCore.CssStyles;

public class TableBodyStyleModel : TableStyleModel
{
    #region Public and private fields, properties, constructor

    public SqlFieldIdentityEnum IdentityName { get; set; }
    public bool IsShowMarked { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public TableBodyStyleModel()
    {
		IdentityName = SqlFieldIdentityEnum.Default;
		IsShowMarked = false;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityName"></param>
	/// <param name="isShowMarked"></param>
	public TableBodyStyleModel(SqlFieldIdentityEnum identityName, bool isShowMarked)
    {
        IdentityName = identityName;
        IsShowMarked = isShowMarked;
    }

    #endregion
}
