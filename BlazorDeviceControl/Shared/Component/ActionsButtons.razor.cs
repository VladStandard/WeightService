// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorDeviceControl.Shared.Item;
using DataCore.Localizations;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace BlazorDeviceControl.Shared.Component;

public partial class ActionsButtons
{
	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		SetParametersWithAction(new()
		{
			() =>
			{
				//
			}
		});
	}

	#endregion
}
