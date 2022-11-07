// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Models;

public enum EnumFilterAction
{
	Add,
	Remove
}

public class SqlCrudConfigModel
{
	#region Public and private fields, properties, constructor

	public JsonSettingsHelper JsonSettings { get; } = JsonSettingsHelper.Instance;
	public List<SqlFieldFilterModel> Filters { get; private set; }
	public List<SqlFieldOrderModel> Orders { get; }
	public bool IsGuiShowFilterAdditional { get; set; }
	public bool IsGuiShowFilterMarked { get; set; }
	public bool IsGuiShowFilterOnlyTop { get; set; }
	public bool IsGuiShowItemsCount { get; set; }
	public bool IsResultAddFieldEmpty { get; }
	public bool IsResultOrder { get; }
	private bool _isResultShowMarked;
	public bool IsResultShowMarked
	{
		get => _isResultShowMarked;
		set
		{
			_isResultShowMarked = value;
			SetFiltersIsResultShowMarked();
		}
	}

	public bool IsResultShowOnlyTop { get; set; }
	private int _resultMaxCount;
	public int ResultMaxCount
	{
		get => _resultMaxCount;
		set => _resultMaxCount = value == 1 ? 1
			: IsResultShowOnlyTop ? JsonSettings.Local.SelectTopRowsCount : value;
	}

	public SqlCrudConfigModel(List<SqlFieldFilterModel> filters, List<SqlFieldOrderModel> orders,
		bool isResultShowMarked, bool isResultShowOnlyTop, bool isResultAddFieldEmpty, bool isResultOrder, int resultMaxCount = 0)
	{
		Filters = filters;
		Orders = orders;

		IsGuiShowFilterAdditional = false;
		IsGuiShowFilterMarked = false;
		IsGuiShowFilterOnlyTop = true;
		IsGuiShowItemsCount = false;

		IsResultShowMarked = isResultShowMarked;
		IsResultShowOnlyTop = isResultShowOnlyTop;
		IsResultAddFieldEmpty = isResultAddFieldEmpty;
		IsResultOrder = isResultOrder;

		ResultMaxCount = resultMaxCount;
	}

	public SqlCrudConfigModel(List<SqlFieldFilterModel> filters,
		bool isResultShowMarked, bool isResultShowOnlyTop, bool isResultAddFieldEmpty, bool isResultOrder, int resultMaxCount = 0) :
		this(filters, new(), isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isResultOrder, resultMaxCount)
	{ }

	public SqlCrudConfigModel(List<SqlFieldOrderModel> orders,
		bool isResultShowMarked, bool isResultShowOnlyTop, bool isResultAddFieldEmpty, bool isResultOrder, int resultMaxCount = 0) :
		this(new(), orders, isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isResultOrder, resultMaxCount)
	{ }

	public SqlCrudConfigModel(bool isResultShowMarked, bool isResultShowOnlyTop, bool isResultAddFieldEmpty, bool isResultOrder, int resultMaxCount = 0) :
		this(new(), new(), isResultShowMarked, isResultShowOnlyTop, isResultAddFieldEmpty, isResultOrder, resultMaxCount)
	{ }

	#endregion

	#region Public and private methods - Change filters

	public static List<SqlFieldFilterModel> GetFilters(string className, SqlTableBase? item) =>
		item is null || string.IsNullOrEmpty(className) ? new()
			: GetFiltersIdentity(className, item.Identity.Name == SqlFieldIdentityEnum.Uid ? item.IdentityValueUid : item.IdentityValueId);

	public static List<SqlFieldFilterModel> GetFilters(string className, object? value) =>
		new() { new(className, SqlFieldComparerEnum.Equal, value) };

	public static List<SqlFieldFilterModel> GetFiltersIdentity(string className, object? value) =>
		value switch
		{
			Guid uid => new() { new($"{className}.{nameof(SqlTableBase.IdentityValueUid)}", SqlFieldComparerEnum.Equal, uid) },
			long id => new() { new($"{className}.{nameof(SqlTableBase.IdentityValueId)}", SqlFieldComparerEnum.Equal, id) },
			_ => new()
		};

	private List<SqlFieldFilterModel> GetFiltersIsResultShowMarked(bool isResultShowMarked) =>
		new() { new(nameof(SqlTableBase.IsMarked), SqlFieldComparerEnum.Equal, isResultShowMarked) };

	//private void SetFilters(List<SqlFieldFilterModel> filters)
	//{
	//	if (!filters.Any()) return;
	//	bool isFilterExists = false;
	//	foreach (SqlFieldFilterModel filter in filters)
	//	{
	//		if (Filters.Contains(filter))
	//		{
	//			isFilterExists = true;
	//			break;
	//		}
	//	}
	//	if (!isFilterExists)
	//		Filters.AddRange(filters);
	//}

	public void SetFilters(List<SqlFieldFilterModel> filters, EnumFilterAction filterAction)
	{
		switch (filterAction)
		{
			case EnumFilterAction.Remove:
				switch (Filters.Any())
				{
					case true:
						bool isFilterExists = true;
						while (isFilterExists)
						{
							isFilterExists = false;
							foreach (SqlFieldFilterModel filter in filters)
							{
								if (Filters.Contains(filter))
								{
									isFilterExists = true;
									Filters.Remove(filter);
									break;
								}
							}
						}
						break;
				}
				break;
			case EnumFilterAction.Add:
				switch (Filters.Any())
				{
					case false:
						Filters = filters;
						break;
					case true:
						foreach (SqlFieldFilterModel filter in filters)
						{
							if (!Filters.Contains(filter))
							{
								Filters.Add(filter);
							}
						}
						break;
				}
				break;
		}
	}

	public void SetFiltersIsResultShowMarked() => SetFilters(GetFiltersIsResultShowMarked(false), 
		IsResultShowMarked ? EnumFilterAction.Remove : EnumFilterAction.Add);

	public void SetFilters(string className, SqlTableBase? item, EnumFilterAction filterAction) =>
		SetFilters(GetFilters(className, item), filterAction);

	#endregion
}
