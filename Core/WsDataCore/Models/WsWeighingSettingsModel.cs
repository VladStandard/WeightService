using MDSoft.NetUtils;

namespace WsDataCore.Models;

public sealed class WsWeighingSettingsModel
{
    #region Public and private fields and properties

    private byte KneadingMinValue => 1;
    private byte KneadingMaxValue => 140;
    private short _kneading;
    public short Kneading
    {
        get => _kneading;
        set
        {
            if (value < KneadingMinValue)
                _kneading = KneadingMinValue;
            else if (value > KneadingMaxValue)
                _kneading = KneadingMaxValue;
            else
                _kneading = value;
        }
    }
    private byte LabelsCountMinValue => 1;
    private byte LabelsCountMaxValue => 130;
    private byte _labelsCountMain;
    public byte LabelsCountMain
    {
        get => _labelsCountMain;
        set
        {
            if (value < KneadingMinValue)
                _labelsCountMain = LabelsCountMinValue;
            else if (value > KneadingMaxValue)
                _labelsCountMain = LabelsCountMaxValue;
            else
                _labelsCountMain = value;
        }
    }
    private byte _labelsCountShipping;
    public byte LabelsCountShipping
    {
        get => _labelsCountShipping;
        set
        {
            if (value < KneadingMinValue)
                _labelsCountShipping = LabelsCountMinValue;
            else if (value > KneadingMaxValue)
                _labelsCountShipping = LabelsCountMaxValue;
            else
                _labelsCountShipping = value;
        }
    }

    public WsWeighingSettingsModel()
    {
        Kneading = KneadingMinValue;
        LabelsCountMain = LabelsCountMinValue;
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Получить наименование принтера.
    /// </summary>
    private string GetPrintName(WsEnumPrintModel printBrand) =>
        printBrand switch
        {
            WsEnumPrintModel.Zebra => WsLocaleCore.Print.NameMainZebra,
            WsEnumPrintModel.Tsc => WsLocaleCore.Print.NameMainTsc,
            _ => WsLocaleCore.Print.DeviceName
        };

    public string GetPrintDescription(WsEnumPrintModel printBrand, MdPrinterModel printer,
        bool isConnected, int scaleCounter, int labelPrintedCount, byte labelCount) =>
        $"{GetPrintName(printBrand)} {printer.Name} | {printer.Ip} | " +
        $"{WsLocaleCore.Table.Status}: {(isConnected ? MdNetLocalization.Instance.StatusSuccess : MdNetLocalization.Instance.StatusUnknown)} | " +
        $"{WsLocaleCore.Table.LabelCounter}: {scaleCounter} | " +
        $"{WsLocaleCore.LabelPrint.Labels}: {labelPrintedCount} {WsLocaleCore.Strings.From} {labelCount}";

    #endregion
}