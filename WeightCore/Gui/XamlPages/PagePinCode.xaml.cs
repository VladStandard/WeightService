// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WeightCore.Gui.XamlPages;

/// <summary>
/// Interaction logic for PagePin.xaml
/// </summary>
public partial class PagePinCode : UserControl
{
    #region Public and private fields, properties, constructor

    private SqlViewModelHelper SqlViewModel { get; }
    public RoutedEventHandler OnClose { get; set; }
    public System.Windows.Forms.DialogResult Result { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PagePinCode()
    {
        InitializeComponent();

        object context = FindResource("SqlViewModel");
        if (context is SqlViewModelHelper sqlViewModel)
        {
            SqlViewModel = sqlViewModel;
        }
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
        string num = (string)(sender as Button).Content;
        //_settings.PinCode.Input = int.Parse(_settings.PinCode.Input.ToString() + num);
    }

    private void ButtonClear_Click(object sender, RoutedEventArgs e)
    {
        //_settings.PinCode.Input = 0;
    }

    private void ButtonEnter_Click(object sender, RoutedEventArgs e)
    {
        //if (_settings.PinCode.AccessGranted)
        //{
        //    _settings.ActivePage = WpfActivePage.Settings;
        //}
        //else
        //{
        //    ButtonClear_Click(sender, e);
        //}
        OnClose?.Invoke(sender, e);
    }

    #endregion
}
