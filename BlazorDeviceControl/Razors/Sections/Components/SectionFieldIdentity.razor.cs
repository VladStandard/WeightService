﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections.Components;

public partial class SectionFieldIdentity<T> : RazorPageSectionBase<T> where T : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	public SectionFieldIdentity()
	{
		CssStyleRadzenColumn.Width = new T().Identity.Name switch
		{
			SqlFieldIdentityEnum.Id => "8%",
			SqlFieldIdentityEnum.Uid => "21%",
			_ => CssStyleRadzenColumn.Width
		};
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			//
		});
	}

	#endregion
}
