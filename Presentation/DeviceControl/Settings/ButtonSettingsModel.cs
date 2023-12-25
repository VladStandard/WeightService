namespace DeviceControl.Settings;

public class ButtonSettingsModel
{
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
    
    public static ButtonSettingsModel CreateForItem() =>
        new(false, false, false, true, true);

    public static ButtonSettingsModel CreateForStaticItem() =>
        new(false, false, false, false, true);

    public static ButtonSettingsModel CreateForSection() =>
        new(true, true, true, false, false);

    public static ButtonSettingsModel CreateForStaticSection() =>
        new(true, true, false, false, false);
    
    public static ButtonSettingsModel CreateForStatic1CSection() =>
        new(false, false, false, false, false);
}
