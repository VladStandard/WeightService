// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/Fody/PropertyChanged

namespace WsDataCore.Common;

/// <summary>
/// Базовый класс интерфейса MVVM INotifyPropertyChanged.
/// </summary>
#nullable enable
public class WsMvvmBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
}