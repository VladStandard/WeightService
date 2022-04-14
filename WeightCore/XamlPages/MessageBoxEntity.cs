// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using MvvmHelpers;
using System.Windows.Forms;

namespace WeightCore.XamlPages
{
    public class MessageBoxEntity : BaseViewModel
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

        private DialogResult _result;
        public DialogResult Result
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

        private string _buttonYes;
        public string ButtonYes
        {
            get => _buttonYes;
            private set
            {
                _buttonYes = value;
                OnPropertyChanged();
            }
        }
        private string _buttonRetry;
        public string ButtonRetry
        {
            get => _buttonRetry;
            private set
            {
                _buttonRetry = value;
                OnPropertyChanged();
            }
        }
        private string _buttonNo;
        public string ButtonNo
        {
            get => _buttonNo;
            private set
            {
                _buttonNo = value;
                OnPropertyChanged();
            }
        }
        private string _buttonIgnore;
        public string ButtonIgnore
        {
            get => _buttonIgnore;
            private set
            {
                _buttonIgnore = value;
                OnPropertyChanged();
            }
        }
        private string _buttonCancel;
        public string ButtonCancel
        {
            get => _buttonCancel;
            private set
            {
                _buttonCancel = value;
                OnPropertyChanged();
            }
        }
        private string _buttonAbort;
        public string ButtonAbort
        {
            get => _buttonAbort;
            private set
            {
                _buttonAbort = value;
                OnPropertyChanged();
            }
        }
        private string _buttonOk;
        public string ButtonOk
        {
            get => _buttonOk;
            private set
            {
                _buttonOk = value;
                OnPropertyChanged();
            }
        }

        private VisibilitySettingsEntity _visibilitySettings;
        public VisibilitySettingsEntity VisibilitySettings
        {
            get => _visibilitySettings;
            set
            {
                _visibilitySettings = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor and destructor

        public MessageBoxEntity()
        {
            Caption = string.Empty;
            Message = string.Empty;
            FontSizeCaption = 30;
            FontSizeMessage = 26;
            FontSizeButton = 22;

            Localization();

            VisibilitySettings = new();

            Result = DialogResult.Cancel;
        }

        public void Localization()
        {
            ButtonYes = LocalizationData.Buttons.Yes;
            ButtonRetry = LocalizationData.Buttons.Retry;
            ButtonNo = LocalizationData.Buttons.No;
            ButtonIgnore = LocalizationData.Buttons.Ignore;
            ButtonCancel = LocalizationData.Buttons.Cancel;
            ButtonAbort = LocalizationData.Buttons.Abort;
            ButtonOk = LocalizationData.Buttons.Ok;
        }

        #endregion
    }
}
