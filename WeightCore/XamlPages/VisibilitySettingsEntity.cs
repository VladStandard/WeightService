// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MvvmHelpers;
using System.Windows;

namespace WeightCore.XamlPages
{
    public class VisibilitySettingsEntity : BaseViewModel
    {
        #region Public and private fields and properties

        private Visibility _buttonAbortVisibility;
        private Visibility _buttonCancelVisibility;
        private Visibility _buttonIgnoreVisibility;
        private Visibility _buttonNoVisibility;
        private Visibility _buttonOkVisibility;
        private Visibility _buttonRetryVisibility;
        private Visibility _buttonYesVisibility;
        public Visibility ButtonAbortVisibility { get => _buttonAbortVisibility; set { _buttonAbortVisibility = value; OnPropertyChanged(); } }
        public Visibility ButtonCancelVisibility { get => _buttonCancelVisibility; set { _buttonCancelVisibility = value; OnPropertyChanged(); } }
        public Visibility ButtonIgnoreVisibility { get => _buttonIgnoreVisibility; set { _buttonIgnoreVisibility = value; OnPropertyChanged(); } }
        public Visibility ButtonNoVisibility { get => _buttonNoVisibility; set { _buttonNoVisibility = value; OnPropertyChanged(); } }
        public Visibility ButtonOkVisibility { get => _buttonOkVisibility; set { _buttonOkVisibility = value; OnPropertyChanged(); } }
        public Visibility ButtonRetryVisibility { get => _buttonRetryVisibility; set { _buttonRetryVisibility = value; OnPropertyChanged(); } }
        public Visibility ButtonYesVisibility { get => _buttonYesVisibility; set { _buttonYesVisibility = value; OnPropertyChanged(); } }

        #endregion

        #region Constructor and destructor

        public VisibilitySettingsEntity()
        {
            ButtonAbortVisibility = Visibility.Hidden;
            ButtonCancelVisibility = Visibility.Hidden;
            ButtonIgnoreVisibility = Visibility.Hidden;
            ButtonNoVisibility = Visibility.Hidden;
            ButtonOkVisibility = Visibility.Hidden;
            ButtonRetryVisibility = Visibility.Hidden;
            ButtonYesVisibility = Visibility.Hidden;
        }

        #endregion
    }
}
