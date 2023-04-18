// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MDSoft.WinFormsUtils;

namespace WsLabelCore.WinForms.Helpers;

public class PluginLabelsHelper : PluginHelperBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static PluginLabelsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static PluginLabelsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private Label FieldKneading { get; set; }
    private Label FieldPlu { get; set; }
    private Label FieldProductDate { get; set; }
    
    #endregion

    #region Constructor and destructor

    public PluginLabelsHelper()
    {
        TskType = TaskType.TaskLabel;
    }

    #endregion

    #region Public and private methods

    public void Init(ConfigModel configReopen, ConfigModel configRequest, ConfigModel configResponse,
        Label fieldPlu, Label fieldProductDate, Label fieldKneading)
    {
        base.Init();
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        ActionUtils.ActionTryCatch(() =>
        {
            FieldPlu = fieldPlu;
            FieldProductDate = fieldProductDate;
            FieldKneading = fieldKneading;
            MdInvokeControl.SetText(FieldPlu, LocaleCore.Scales.Plu);
            //MdInvokeControl.SetText(FieldSscc, LocaleCore.Scales.FieldSscc);
            MdInvokeControl.SetText(FieldProductDate, LocaleCore.Scales.FieldDate);
            MdInvokeControl.SetText(FieldKneading, string.Empty);
        });
    }

    public override void Execute()
    {
        base.Execute();
        ReopenItem.Execute(Reopen);
        RequestItem.Execute(Request);
    }

    private void Reopen()
    {
        //MdInvokeControl.SetText(FieldSscc, $"{LocaleCore.Scales.FieldSscc}: {UserSessionHelper.Instance.ProductSeries.Sscc.Sscc}");
        MdInvokeControl.SetVisible(FieldKneading, UserSessionHelper.Instance.Scale.IsKneading);
    }

    private void Request()
    {
        if (UserSessionHelper.Instance.PluScale.IsNew)
            MdInvokeControl.SetText(FieldPlu, LocaleCore.Scales.Plu);
        else
            MdInvokeControl.SetText(FieldPlu,
                UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight
                    ? $"{LocaleCore.Scales.PluWeight}: {UserSessionHelper.Instance.PluScale.Plu.Number} | {UserSessionHelper.Instance.PluScale.Plu.Name}"
                    : $"{LocaleCore.Scales.PluCount}: {UserSessionHelper.Instance.PluScale.Plu.Number} | {UserSessionHelper.Instance.PluScale.Plu.Name}");
        MdInvokeControl.SetText(FieldProductDate, $"{UserSessionHelper.Instance.ProductDate:dd.MM.yyyy}");
        MdInvokeControl.SetText(FieldKneading, $"{UserSessionHelper.Instance.WeighingSettings.Kneading}");
    }

    #endregion
}