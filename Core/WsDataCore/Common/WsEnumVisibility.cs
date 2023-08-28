namespace WsDataCore.Common;

public enum WsEnumVisibility : byte
{
    /// <summary>Display the element.</summary>
    Visible,
    /// <summary>Do not display the element, but reserve space for the element in layout.</summary>
    Hidden,
    /// <summary>Do not display the element, and do not reserve space for it in layout.</summary>
    Collapsed,
}