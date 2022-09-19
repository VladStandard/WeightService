// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;

namespace BlazorCore.Razors;

public class ButtonSettingsModel
{
	#region Public and private fields, properties, constructor

	[Parameter] public bool IsShowCancel { get; set; }
	[Parameter] public bool IsShowCopy { get; set; }
	[Parameter] public bool IsShowDelete { get; set; }
	[Parameter] public bool IsShowEdit { get; set; }
	[Parameter] public bool IsShowMark { get; set; }
	[Parameter] public bool IsShowNew { get; set; }
	[Parameter] public bool IsShowSave { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="isShowCopy"></param>
	/// <param name="isShowDelete"></param>
	/// <param name="isShowEdit"></param>
	/// <param name="isShowMark"></param>
	/// <param name="isShowNew"></param>
	/// <param name="isShowSave"></param>
	/// <param name="isShowCancel"></param>
	public ButtonSettingsModel(bool isShowCopy, bool isShowDelete, bool isShowEdit, bool isShowMark, bool isShowNew, bool isShowSave, bool isShowCancel)
	{
		IsShowCopy = isShowCopy;
		IsShowDelete = isShowDelete;
		IsShowEdit = isShowEdit;
		IsShowMark = isShowMark;
		IsShowNew = isShowNew;
		IsShowSave = isShowSave;
		IsShowCancel = isShowCancel;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	public ButtonSettingsModel() : this(false, false, false, false, false, false, false)
	{
		//
	}

	#endregion

	#region Public and private methods

	public bool EqualsDefault() =>
		Equals(IsShowCopy, false) &&
		Equals(IsShowDelete, false) &&
		Equals(IsShowEdit, false) &&
		Equals(IsShowMark, false) &&
		Equals(IsShowNew, false) &&
		Equals(IsShowSave, false) &&
		Equals(IsShowCancel, false);

	#endregion
}
