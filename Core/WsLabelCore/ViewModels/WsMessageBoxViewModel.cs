// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

#nullable enable
public sealed class WsMessageBoxViewModel : WsWpfBaseViewModel
{
	#region Public and private fields and properties

	public string Caption { get; set; }
	public string Message { get; set; }
    public double FontSizeCaption { get; set; }
	public double FontSizeMessage { get; set; }
    public double FontSizeButton { get; set; }
    public double SizeCaption { get; set; }
    public double SizeMessage { get; set; }
    public double SizeButton { get; set; }
    public WsButtonVisibilityModel ButtonVisibility { get; set; }

    public WsMessageBoxViewModel()
	{
		Caption = string.Empty;
		Message = string.Empty;
		FontSizeCaption = 30;
        FontSizeMessage = 26;
        FontSizeButton = 22;
		ButtonVisibility = new();
	}

	#endregion
}