// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsBlazorCore.CssStyles;

public class CssStyleTableBodyModel : CssStyleBase
{
    #region Public and private fields, properties, constructor

    public WsSqlEnumFieldIdentity IdentityName { get; set; }
    public bool IsShowMarked { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public CssStyleTableBodyModel()
    {
        IdentityName = WsSqlEnumFieldIdentity.Empty;
        IsShowMarked = false;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="identityName"></param>
    /// <param name="isShowMarked"></param>
    public CssStyleTableBodyModel(WsSqlEnumFieldIdentity identityName, bool isShowMarked)
    {
        IdentityName = identityName;
        IsShowMarked = isShowMarked;
    }

    #endregion
}