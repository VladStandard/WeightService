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
    /// <param name="isMain"></param>
    /// <param name="printBrand"></param>
    /// <returns></returns>
    private string GetPrintName(bool isMain, WsEnumPrintModel printBrand) => isMain
        ? printBrand switch
        {
            WsEnumPrintModel.Zebra => WsLocaleCore.Print.NameMainZebra,
            WsEnumPrintModel.Tsc => WsLocaleCore.Print.NameMainTsc,
            _ => WsLocaleCore.Print.DeviceName
        }
        : printBrand switch
        {
            WsEnumPrintModel.Zebra => WsLocaleCore.Print.NameShippingZebra,
            WsEnumPrintModel.Tsc => WsLocaleCore.Print.NameShippingTsc,
            _ => WsLocaleCore.Print.DeviceNameIsUnavailable
        };

    public string GetPrintDescription(bool isMain, WsEnumPrintModel printBrand, MdPrinterModel printer,
        bool isConnected, int scaleCounter, string deviceStatus, int labelPrintedCount, byte labelCount) =>
        $"{GetPrintName(isMain, printBrand)} {printer.Name} | {printer.Ip} | " +
        //$"{LocaleCore.Table.Status}: {MdNetUtils.GetPingStatus(printer.PingStatus)} | " +
        $"{WsLocaleCore.Table.Status}: {(isConnected ? MdNetLocalization.Instance.StatusSuccess : MdNetLocalization.Instance.StatusUnknown)} | " +
        $"{WsLocaleCore.Table.LabelCounter}: {scaleCounter} | " +
        //$"{deviceStatus} | " +
        $"{WsLocaleCore.LabelPrint.Labels}: {labelPrintedCount} {WsLocaleCore.Strings.From} {labelCount}";

    #endregion
}