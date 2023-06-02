// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Common;

/// <summary>
/// Интерфейс модели представления XAML.
/// </summary>
#nullable enable
public interface IWsViewModel : INotifyPropertyChanged
{
    void UpdateCommandsFromActions();
}