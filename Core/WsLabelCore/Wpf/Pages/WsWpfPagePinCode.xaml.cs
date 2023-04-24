// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Forms;

namespace WsLabelCore.Wpf.Pages;

/// <summary>
/// Interaction logic for WsWpfPagePinCode.xaml
/// </summary>
public partial class WsWpfPagePinCode
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsWpfPagePinCode()
    {
        InitializeComponent();
    }

    #endregion

    #region Private methods

    private void PagePin_OnLoaded(object sender, RoutedEventArgs e)
    {
        // Очистить.
        ButtonClear_Click(sender, e);
    }

    private void ButtonNum_Click(object sender, EventArgs e)
    {
        //if (sender is System.Windows.Button button)
        //{
        //    //string num = (string)button.Content;
        //    //_settings.PinCode.Input = int.Parse(_settings.PinCode.Input.ToString() + num);
        //}
    }

    private void ButtonClear_Click(object sender, RoutedEventArgs e)
    {
        //_settings.PinCode.Input = 0;
    }

    private void ButtonEnter_Click(object sender, RoutedEventArgs e)
    {
        Result = DialogResult.OK;
        OnClose?.Invoke(sender, e);
    }

    #endregion
}