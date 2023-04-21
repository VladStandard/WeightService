// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Gui;

public sealed class WsMessageBoxModel : BaseViewModel
{
	#region Public and private fields and properties

	private string _caption;
	public string Caption
	{
		get => _caption;
		set
		{
			_caption = value;
			OnPropertyChanged();
		}
	}
	private string _message;
	public string Message
	{
		get => _message;
		set
		{
			_message = value;
			OnPropertyChanged();
		}
	}

	private System.Windows.Forms.DialogResult _result;
	public System.Windows.Forms.DialogResult Result
	{
		get => _result;
		set
		{
			_result = value;
			OnPropertyChanged();
		}
	}

	private double _fontSizeCaption;
	public double FontSizeCaption
	{
		get { return _fontSizeCaption; }
		set { _fontSizeCaption = value; }
	}
	private double _fontSizeMessage;
	public double FontSizeMessage
	{
		get { return _fontSizeMessage; }
		set { _fontSizeMessage = value; }
	}
	private double _fontSizeButton;
	public double FontSizeButton
	{
		get { return _fontSizeButton; }
		set { _fontSizeButton = value; }
	}
	private double _sizeCaption;
	public double SizeCaption
	{
		get { return _sizeCaption; }
		set { _sizeCaption = value; }
	}
	private double _sizeMessage;
	public double SizeMessage
	{
		get { return _sizeMessage; }
		set { _sizeMessage = value; }
	}
	private double _sizeButton;
	public double SizeButton
	{
		get { return _sizeButton; }
		set { _sizeButton = value; }
	}

	private VisibilitySettingsModel _visibilitySettings;
	public VisibilitySettingsModel VisibilitySettings
	{
		get => _visibilitySettings;
		set
		{
			_visibilitySettings = value;
			OnPropertyChanged();
		}
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	public WsMessageBoxModel()
	{
		Caption = string.Empty;
		Message = string.Empty;
		FontSizeCaption = 30;
		FontSizeMessage = 26;
		FontSizeButton = 22;
		VisibilitySettings = new();
		Result = System.Windows.Forms.DialogResult.Cancel;
	}

	#endregion
}