using Ws.LabelCore.Common;
using Ws.LabelCore.Models;
using Ws.LabelCore.Utils;
namespace Ws.LabelCore.Helpers;

/// <summary>
/// Плагин меток.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class PluginLabelsHelper : PluginBaseHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static PluginLabelsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static PluginLabelsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private Label FieldKneading { get; set; } = new();
    private Label FieldPlu { get; set; } = new();
    private Label FieldProductDate { get; set; } = new();
    private LabelSessionHelper LabelSession => LabelSessionHelper.Instance;

    public PluginLabelsHelper()
    {
        PluginType = EnumPluginType.Label;
        ResponseItem.PluginType = RequestItem.PluginType = ReopenItem.PluginType = PluginType;
    }

    #endregion

    #region Public and private methods

    public void Init(PluginConfigModel configReopen, PluginConfigModel configRequest, PluginConfigModel configResponse,
        Label fieldPlu, Label fieldProductDate, Label fieldKneading)
    {
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        FormNavigationUtils.ActionTryCatch(() =>
        {
            FieldPlu = fieldPlu;
            FieldProductDate = fieldProductDate;
            FieldKneading = fieldKneading;
            MdInvokeControl.SetText(FieldPlu, LocaleCore.LabelPrint.Plu);
            MdInvokeControl.SetText(FieldProductDate, LocaleCore.LabelPrint.FieldDate);
            MdInvokeControl.SetText(FieldKneading, string.Empty);
        });
    }

    public override void Execute()
    {
        base.Execute();
        ReopenItem.Execute(Reopen);
        RequestItem.Execute(Request);
    }

    private void Reopen() => MdInvokeControl.SetVisible(FieldKneading, true);

    private void Request()
    {
        if (LabelSession.PluLine.IsNew)
            MdInvokeControl.SetText(FieldPlu, LocaleCore.LabelPrint.Plu);
        else
            MdInvokeControl.SetText(FieldPlu,
                LabelSession.PluLine.Plu.IsCheckWeight
                    // Весовая ПЛУ.
                    ? $"{LocaleCore.LabelPrint.PluWeight} | {LabelSession.PluLine.Plu.Number} | {LabelSession.PluLine.Plu.Name}"
                    // Штучная ПЛУ.
                    : $"{LocaleCore.LabelPrint.PluCountNesting(LabelSession.ViewPluNesting.BundleCount)} | {LabelSession.PluLine.Plu.Number} | {LabelSession.PluLine.Plu.Name}");
        MdInvokeControl.SetText(FieldProductDate, $"{LabelSession.ProductDate:dd.MM.yyyy}");
        MdInvokeControl.SetText(FieldKneading, $"{LabelSession.WeighingSettings.Kneading}");
    }

    #endregion
}