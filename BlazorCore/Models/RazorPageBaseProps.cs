// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Collections.Generic;

namespace BlazorCore.Models;

public partial class RazorPageBase
{
	#region Public and private fields, properties, constructor

	[Inject] public DialogService? DialogService { get; set; }
	[Inject] public IJSRuntime? JsRuntime { get; set; }
	[Inject] public NavigationManager? NavigationManager { get; set; }
	[Inject] public NotificationService? NotificationService { get; set; }
	[Inject] public TooltipService? TooltipService { get; set; }
	[Inject] public IHttpContextAccessor? HttpContextAccess { get; set; }
	[Parameter] public bool IsShowReload { get; set; }
	[Parameter]
	public string IsShowReloadStr
	{
		get => IsShowReload ? "true" : "false";
		set => IsShowReload = value.Equals("TRUE", StringComparison.InvariantCultureIgnoreCase);
	}
	[Parameter] public RazorPageConfigModel RazorPageConfig { get; set; }
	[Parameter] public RazorFieldConfigModel RazorFieldConfig { get; set; }
	[Parameter] public ButtonSettingsModel ButtonSettings { get; set; }
	[Parameter] public SqlTableActionEnum TableAction { get; set; }
	[Parameter] public Guid? IdentityUid { get; set; }
	[Parameter] public string IdentityUidStr { get => IdentityUid?.ToString() ?? Guid.Empty.ToString(); set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty; }
	[Parameter] public long? IdentityId { get; set; }
	[Parameter] public List<DataCore.Sql.Tables.SqlTableBase>? Items { get; set; }
	[Parameter] public RazorPageBase? ParentRazor { get; set; }
	[Parameter] public UserSettingsModel UserSettings { get; set; }
	[Parameter] public string PageTitle { get; set; }
	private ItemSaveCheckModel ItemSaveCheck { get; set; }
	protected AppSettingsHelper AppSettings { get; } = AppSettingsHelper.Instance;
	public DataCore.Sql.Tables.SqlTableBase? Item { get; set; }
	private bool IsActionsInitializedFinished { get; set; }
	protected bool IsActionsParametersSetFinished { get; set; }

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

		UserSettings = new();
		ButtonSettings = new();
		RazorPageConfig = new();
		RazorFieldConfig = new();
		TableAction = SqlTableActionEnum.Empty;
		ItemSaveCheck = new();
		IsActionsInitializedFinished = false;
		IsActionsParametersSetFinished = false;
		PageTitle = string.Empty;
	}

	public void SetPropertiesFromParent()
	{
		if (ParentRazor is null) return;

		RazorPageConfig = ParentRazor.RazorPageConfig;

		if (IdentityId is null && ParentRazor.IdentityId is not null)
			IdentityId = ParentRazor.IdentityId;
		if (IdentityUid is null && ParentRazor.IdentityUid is not null)
			IdentityUid = ParentRazor.IdentityUid;
		if (ParentRazor.Item is not null)
			Item = ParentRazor.Item;
		if (ParentRazor.Items is not null)
			Items = ParentRazor.Items;
		if (ParentRazor.TableAction != SqlTableActionEnum.Empty)
			TableAction = ParentRazor.TableAction;

		ButtonSettings = ParentRazor.ButtonSettings;
	}

	#endregion
}
