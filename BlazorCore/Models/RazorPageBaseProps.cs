// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.Tables;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Collections.Generic;
using DataCore.Sql.Core;

namespace BlazorCore.Models;

public partial class RazorPageBase : LayoutComponentBase
{
	#region Public and private fields, properties, constructor

	[Inject] public DialogService? DialogService { get; set; }
	[Inject] public IJSRuntime? JsRuntime { get; set; }
	[Inject] public NavigationManager? NavigationManager { get; set; }
	[Inject] public NotificationService? NotificationService { get; set; }
	[Inject] public TooltipService? TooltipService { get; set; }
	[Inject] public IHttpContextAccessor? HttpContextAccess { get; set; }
	[Parameter] public TableBaseModel? ItemFilter { get; set; }
	[Parameter] public bool IsShowReload { get; set; }
	[Parameter]
	public string IsShowReloadStr
	{
		get => IsShowReload ? "true" : "false";
		set => IsShowReload = value.Equals("TRUE", StringComparison.InvariantCultureIgnoreCase);
	}
	[Parameter] public bool IsShowAdditionalFilter { get; set; }
	[Parameter] public bool IsShowItemsCount { get; set; }
	[Parameter] public bool IsShowMarkedFilter { get; set; }
	[Parameter] public bool IsShowMarked { get; set; }
	[Parameter] public bool IsShowOnlyTop { get; set; }
	[Parameter] public ButtonSettingsModel ButtonSettings { get; set; }
	[Parameter] public SqlTableActionEnum TableAction { get; set; }
	[Parameter] public Guid? IdentityUid { get; set; }
	[Parameter] public string IdentityUidStr { get => IdentityUid?.ToString() ?? Guid.Empty.ToString(); set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty; }
	[Parameter] public long? IdentityId { get; set; }
	[Parameter] public List<TableBaseModel>? Items { get; set; }
	[Parameter] public List<TableBaseModel>? ItemsFilter { get; set; }
	[Parameter] public RazorPageBase? ParentRazor { get; set; }
	[Parameter] public string? FilterCaption { get; set; }
	[Parameter] public string? FilterName { get; set; }
	[Parameter] public TableBase Table { get; set; }
	private ItemSaveCheckModel ItemSaveCheck { get; set; }
	protected AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;
	public TableBaseModel? Item { get; set; }
	protected object? ItemObject { get => Item; set => Item = (TableBaseModel?)value; }
	[Parameter] public UserSettingsModel UserSettings { get; set; }
	public bool IsActionsInitializedFinished { get; set; }
	public bool IsActionsParametersSetFinished { get; set; }
	public event Action ActionChange;

	/// <summary>
	/// Constructor.
	/// </summary>
	public RazorPageBase()
	{
		NotificationService = null;
		NavigationManager = null;
		JsRuntime = null;
		DialogService = null;
		TooltipService = null;

		ActionChange += OnParametersSet;
		UserSettings = new();
		ButtonSettings = new();
		FilterCaption = string.Empty;
		FilterName = string.Empty;
		IsShowOnlyTop = true;
		Table = new(string.Empty);
		TableAction = SqlTableActionEnum.Default;
		ItemSaveCheck = new();
		IsActionsInitializedFinished = false;
		IsActionsParametersSetFinished = false;

		if (ParentRazor is not null)
		{
			if (ParentRazor.ItemFilter is not null)
				ItemFilter = ParentRazor.ItemFilter;
			if (ParentRazor.ItemsFilter is not null)
				ItemsFilter = ParentRazor.ItemsFilter;
			IsShowAdditionalFilter = ParentRazor.IsShowAdditionalFilter;
			IsShowItemsCount = ParentRazor.IsShowItemsCount;
			IsShowMarkedFilter = ParentRazor.IsShowMarkedFilter;
			IsShowMarked = ParentRazor.IsShowMarked;
			IsShowOnlyTop = ParentRazor.IsShowOnlyTop;
			if (IdentityId is null && ParentRazor.IdentityId is not null)
				IdentityId = ParentRazor.IdentityId;
			if (IdentityUid is null && ParentRazor.IdentityUid is not null)
				IdentityUid = ParentRazor.IdentityUid;
			if (ParentRazor.Item is not null)
				Item = ParentRazor.Item;
			if (!string.IsNullOrEmpty(ParentRazor.Table.Name))
				Table = ParentRazor.Table;
			if (ParentRazor.TableAction != SqlTableActionEnum.Default)
				TableAction = ParentRazor.TableAction;

			ButtonSettings = ParentRazor.ButtonSettings;
		}
	}

	#endregion
}
