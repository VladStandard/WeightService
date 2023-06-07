// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Common;

/// <summary>
/// Base class for localization.
/// </summary>
public class WsLocaleBase : INotifyPropertyChanged
{
    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    //private void OnPropertyChanged([CallerMemberName] string memberName = "")
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
    //}

    #endregion

    #region Public and private fields, properties, constructor

    public WsEnumLanguage Lang { get; set; } = WsEnumLanguage.Russian;

    #endregion
}