// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://stackoverflow.com/questions/19517292/adding-dynamic-buttons-in-wpf

using System.Windows.Input;

namespace WsLabelCore.Models;

/// <summary>
/// Model for ObservableCollection.
/// </summary>
public class WsButtonCommandModel
{
    #region Public and private fields, properties, constructor

    public string Name { get; set; }
    public ICommand Command { get; set; }

    public WsButtonCommandModel(string name, ICommand command)
    {
        Name = name;
        Command = command;
    }

    #endregion
}