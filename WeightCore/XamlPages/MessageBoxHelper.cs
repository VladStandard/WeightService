// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using MvvmHelpers;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace WeightCore.XamlPages
{
    public class MessageBoxHelper : BaseViewModel
    {
        #region Design pattern "Lazy Singleton"

        private static MessageBoxHelper _instance;
        public static MessageBoxHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

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

        public MessageBoxHelper()
        {
            Caption = string.Empty;
            Message = string.Empty;

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
