// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace BlazorCore.Models;

public class RazorPageItemBase<T> : RazorPageBase where T : TableBase, new()
{
	#region Public and private fields, properties, constructor

	protected T ItemCast { get => Item is null ? new() : (T)Item; set => Item = value; }

	public RazorPageItemBase()
	{
		ItemCast = new();
		Table = AppSettings.DataAccess.GetTable(ItemCast);
	}

	#endregion
}
