// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

public sealed class WsPlusViewModel : WsWpfBaseViewModel
{
    #region Public and private fields, properties, constructor

    private WsSqlPluScaleModel _pluScale;
    public WsSqlPluScaleModel PluScale { get => _pluScale; set { _pluScale = value; OnPropertyChanged(); }
    }

    public WsPlusViewModel()
    {
        //
    }

    #endregion
}