// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors;

public partial class RazorActionLoad : RazorComponentBase
{
	#region Public and private fields, properties, constructor

	[Parameter] public ActionLoadEnum ActionLoad { get; set; }
	[Parameter] public bool IsShowProgress { get; set; }

	#endregion
}
