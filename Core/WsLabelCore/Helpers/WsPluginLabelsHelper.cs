// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Utils;

namespace WsLabelCore.Helpers;

/// <summary>
/// Плагин меток.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsPluginLabelsHelper : WsPluginBaseHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsPluginLabelsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsPluginLabelsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private Label FieldKneading { get; set; } = new();
    private Label FieldPlu { get; set; } = new();
    private Label FieldProductDate { get; set; } = new();
    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;

    public WsPluginLabelsHelper()
    {
        PluginType = WsEnumPluginType.Label;
        ResponseItem.PluginType = RequestItem.PluginType = ReopenItem.PluginType = PluginType;
    }

    #endregion

    #region Public and private methods

    public void Init(WsPluginConfigModel configReopen, WsPluginConfigModel configRequest, WsPluginConfigModel configResponse,
        Label fieldPlu, Label fieldProductDate, Label fieldKneading)
    {
        Init();
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            FieldPlu = fieldPlu;
            FieldProductDate = fieldProductDate;
            FieldKneading = fieldKneading;
            MdInvokeControl.SetText(FieldPlu, WsLocaleCore.Scales.Plu);
            //MdInvokeControl.SetText(FieldSscc, LocaleCore.Scales.FieldSscc);
            MdInvokeControl.SetText(FieldProductDate, WsLocaleCore.Scales.FieldDate);
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
        //MdInvokeControl.SetText(FieldSscc, $"{LocaleCore.Scales.FieldSscc}: {WsUserSessionHelper.Instance.ProductSeries.Sscc.Sscc}");
        MdInvokeControl.SetVisible(FieldKneading, LabelSession.Line.IsKneading);
    }

    private void Request()
    {
        if (LabelSession.PluLine.IsNew)
            MdInvokeControl.SetText(FieldPlu, WsLocaleCore.Scales.Plu);
        else
            MdInvokeControl.SetText(FieldPlu,
                LabelSession.PluLine.Plu.IsCheckWeight
                    ? $"{WsLocaleCore.Scales.PluWeight} | {LabelSession.PluLine.Plu.Number} | {LabelSession.PluLine.Plu.Name}"
                    : $"{WsLocaleCore.Scales.PluCount} | {LabelSession.PluLine.Plu.Number} | {LabelSession.PluLine.Plu.Name}");
        MdInvokeControl.SetText(FieldProductDate, $"{LabelSession.ProductDate:dd.MM.yyyy}");
        MdInvokeControl.SetText(FieldKneading, $"{LabelSession.WeighingSettings.Kneading}");
    }

    #endregion
}