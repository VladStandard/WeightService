// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.Tables;
using System.Collections.Generic;

namespace BlazorCore.Models;

public class RazorPageSectionBase<T> : RazorPageBase where T : TableBase, new()
{
	#region Public and private fields, properties, constructor

	protected List<T> ItemsCast
	{
		get => Items is null ? new() : Items.Select(x => (T)x).ToList();
		set => Items = !value.Any() ? null : new(value);
	}

	protected T ItemFilterCast { get => Item is null ? new() : (T)Item; set => Item = value; }
	protected string ItemsCountResult => $"{LocaleCore.Strings.ItemsCount}: {ItemsCast.Count:### ### ###}";

	public RazorPageSectionBase()
	{
		ItemsCast = new();
		Table = AppSettings.DataAccess.GetTable(ItemsCast);
	}

	#endregion
}
