// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Settings;

public class ButtonSettingsModel
{
    #region Public and private fields, properties, constructor

    public bool IsShowCancel { get; set; }
    public bool IsShowDelete { get; set; }
    public bool IsShowMark { get; set; }
    public bool IsShowNew { get; set; }
    public bool IsShowSave { get; set; }
    
    public ButtonSettingsModel(bool isShowDelete, bool isShowMark, bool isShowNew, bool isShowSave, bool isShowCancel)
    {
        IsShowDelete = isShowDelete;
        IsShowMark = isShowMark;
        IsShowNew = isShowNew;
        IsShowSave = isShowSave;
        IsShowCancel = isShowCancel;
    }

    #endregion

    #region Public and private methods
    public static ButtonSettingsModel CreateForItem() =>
        new(false, false, false, true, true);

    public static ButtonSettingsModel CreateForStaticItem() =>
        new(false, false, false, false, true);

    public static ButtonSettingsModel CreateForSection() =>
        new(true, true, true, false, false);

    public static ButtonSettingsModel CreateForStaticSection() =>
        new(true, true, false, false, false);

    #endregion
}
