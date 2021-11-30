// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using MvvmHelpers;
using System.Windows;
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
            set
            {
                _buttonYes = value;
                OnPropertyChanged();
            }
        }
        private string _buttonRetry;
        public string ButtonRetry
        {
            get => _buttonRetry;
            set
            {
                _buttonRetry = value;
                OnPropertyChanged();
            }
        }
        private string _buttonNo;
        public string ButtonNo
        {
            get => _buttonNo;
            set
            {
                _buttonNo = value;
                OnPropertyChanged();
            }
        }
        private string _buttonIgnore;
        public string ButtonIgnore
        {
            get => _buttonIgnore;
            set
            {
                _buttonIgnore = value;
                OnPropertyChanged();
            }
        }
        private string _buttonCancel;
        public string ButtonCancel
        {
            get => _buttonCancel;
            set
            {
                _buttonCancel = value;
                OnPropertyChanged();
            }
        }
        private string _buttonAbort;
        public string ButtonAbort
        {
            get => _buttonAbort;
            set
            {
                _buttonAbort = value;
                OnPropertyChanged();
            }
        }
        private string _buttonOk;
        public string ButtonOk
        {
            get => _buttonOk;
            set
            {
                _buttonOk = value;
                OnPropertyChanged();
            }
        }

        private Visibility _buttonYesVisibility;
        public Visibility ButtonYesVisibility
        {
            get => _buttonYesVisibility;
            set
            {
                _buttonYesVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _buttonRetryVisibility;
        public Visibility ButtonRetryVisibility
        {
            get => _buttonRetryVisibility;
            set
            {
                _buttonRetryVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _buttonNoVisibility;
        public Visibility ButtonNoVisibility
        {
            get => _buttonNoVisibility;
            set
            {
                _buttonNoVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _buttonIgnoreVisibility;
        public Visibility ButtonIgnoreVisibility
        {
            get => _buttonIgnoreVisibility;
            set
            {
                _buttonIgnoreVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _buttonCancelVisibility;
        public Visibility ButtonCancelVisibility
        {
            get => _buttonCancelVisibility;
            set
            {
                _buttonCancelVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _buttonAbortVisibility;
        public Visibility ButtonAbortVisibility
        {
            get => _buttonAbortVisibility;
            set
            {
                _buttonAbortVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _buttonOkVisibility;
        public Visibility ButtonOkVisibility
        {
            get => _buttonOkVisibility;
            set
            {
                _buttonOkVisibility = value;
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

            ButtonYesVisibility = Visibility.Hidden;
            ButtonRetryVisibility = Visibility.Hidden;
            ButtonNoVisibility = Visibility.Hidden;
            ButtonIgnoreVisibility = Visibility.Hidden;
            ButtonCancelVisibility = Visibility.Hidden;
            ButtonAbortVisibility = Visibility.Hidden;
            ButtonOkVisibility = Visibility.Hidden;

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
